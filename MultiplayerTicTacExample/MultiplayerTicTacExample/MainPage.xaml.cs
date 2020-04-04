using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceModel;
using MultiplayerTicTacExample.GameServer;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// Шаблон элемента пустой страницы задокументирован по адресу http://go.microsoft.com/fwlink/?LinkId=234238

namespace MultiplayerTicTacExample
{
    /// <summary>
    /// Пустая страница, которую можно использовать саму по себе или для перехода внутри фрейма.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private string uid = Guid.NewGuid().ToString();
        private DispatcherTimer _keepAliveTimer;
        private GameServiceClient _client;
        public ObservableCollection<GameInformation> GamesInfo { get; set; }

        private GameServiceClient Client
        {
            get
            {
                if (_client == null || _client.State == CommunicationState.Faulted)
                {
                    CreateClient();
                    //_client.InnerChannel.
                    _client.InnerChannel.Faulted += delegate
                    {
                        Abort();
                    };
                }
                return _client;
            }
        }


        public MainPage()
        {
            DataContext = this;
            this.InitializeComponent();
            GamesInfo = new ObservableCollection<GameInformation>();
            RefreshList();
            _keepAliveTimer = new DispatcherTimer { Interval = new TimeSpan(0, 0, 10) };
            _keepAliveTimer.Tick += KeepAliveTimerTick;
            _keepAliveTimer.Start();
        }

        private async void RefreshList()
        {
            ClientInformation info = await Client.RegisterAsync(uid, "Player");
            Debug.WriteLine(info.RoleId);
            Refresh();
            tbNewGame.IsEnabled = true;
        }
        private void CreateClient()
        {
            _client = new GameServiceClient();
            _client.DeliverGameMessageReceived += ClientDeliverGameMessageReceived;
            _client.UpdateGameListReceived += ClientUpdateGameListReceived;

        }

        public void Abort()
        {
            _client.DeliverGameMessageReceived -= ClientDeliverGameMessageReceived;
            _client.UpdateGameListReceived -= ClientUpdateGameListReceived;
            _client.InnerChannel.Abort();
            _client = null;
        }

        private void ClientUpdateGameListReceived(object sender, UpdateGameListReceivedEventArgs e)
        {
            //посмотрим, есть ли в списке
            var findedGame = GamesInfo.FirstOrDefault(x => x.SessionId == e.gameInfo.SessionId);
            if (findedGame != null)
            {
                if (e.gameInfo.Players.Count != 1 || !e.gameInfo.IsActive)
                {
                    GamesInfo.Remove(findedGame);
                }
            }
            else
            {
                if (e.gameInfo.Players.Count > 0)
                {
                    GamesInfo.Add(e.gameInfo);
                }
            }

            var findedGameForDelete = GamesInfo.FirstOrDefault(x => x.SessionId == "1");
            GamesInfo.Remove(findedGameForDelete);

            if (GamesInfo.Count == 0)
            {
                GamesInfo.Add(new GameInformation() { SessionId = "1", Parameters = "нет игр" });
            }

        }

        private void ClientDeliverGameMessageReceived(object sender, DeliverGameMessageReceivedEventArgs e)
        {
            if (e.type == "turn")
            {
                var tb = (TextBlock) gameField.FindName(e.message);
                tb.Text = "O";
            }
        }


        async void KeepAliveTimerTick(object sender, object e)
        {
            await Client.RegisterAsync(uid, "Player");
        }

        public async void Refresh()
        {
            var list = await Client.GetGamesAsync();
            GamesInfo.Clear();
            if (list == null)
            {
                GamesInfo.Add(new GameInformation(){SessionId = "1",Parameters = "сервер не доступен"});
            }
            else
            {
                foreach (var gameInformation in list)
                {
                   GamesInfo.Add(gameInformation);
                }

                if (GamesInfo.Count == 0)
                {
                    GamesInfo.Add(new GameInformation() { SessionId = "1", Parameters = "нет игр" });
                }
            }

        }


        /// <summary>
        /// Вызывается перед отображением этой страницы во фрейме.
        /// </summary>
        /// <param name="e">Данные о событиях, описывающие, каким образом была достигнута эта страница.  Свойство Parameter
        /// обычно используется для настройки страницы.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }

        private void tb0_Tapped(object sender, TappedRoutedEventArgs e)
        {
            var tb = (sender as TextBlock);
            if (String.IsNullOrWhiteSpace(tb.Text))
            {
                tb.Text = "X";
                Client.MakeTurnAsync(uid,"turn", tb.Name);
            }  
        }

        private void tbNewGame_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Client.CreateGameAsync(uid, "моя игра");
        }

        private async void ListView_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            var selectedGame = (GameInformation)e.AddedItems.FirstOrDefault();
            if (selectedGame != null)
            {
                if (selectedGame.SessionId == "1")
                {
                    var clienInfo = await Client.RegisterAsync(uid, "Player");
                    if (clienInfo != null)
                    {
                        GamesInfo.Clear();
                        Refresh();
                    }
                }
                else
                {
                    //OnSelectGame(selectedGame);
                    Client.JoinGameAsync(uid, selectedGame.SessionId);
                }

            }

        }
    }
}
