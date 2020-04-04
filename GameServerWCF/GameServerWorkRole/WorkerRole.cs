using System;
using System.Diagnostics;
using System.Net;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.Threading;
using GameServerWorkRole.Contract;
using Microsoft.WindowsAzure.ServiceRuntime;

[assembly: CLSCompliant(true)] 
namespace GameServerWorkRole
{
    public class WorkerRole : RoleEntryPoint, IDisposable
    {
        private ServiceHost _serviceHost;
        private bool _disposed = false;

        public override void Run()
        {
            // Это образец реализации рабочего процесса. Замените его собственной логикой.
            Trace.WriteLine("GameServerWorkRole entry point called", "Information");

            StartGameService(3);

            while (true)
            {
                Thread.Sleep(10000);
                Trace.WriteLine("Working", "Information");
            }
        }

        public override bool OnStart()
        {
            // Задайте максимальное число одновременных подключений 
            ServicePointManager.DefaultConnectionLimit = 12;

            // Дополнительные сведения по управлению изменениями конфигурации
            // см. раздел MSDN по ссылке http://go.microsoft.com/fwlink/?LinkId=166357.

            return base.OnStart();
        }

        private void StartGameService(int retries)
        {
         
            if (retries == 0)
            {
                RoleEnvironment.RequestRecycle();
                return;
            }

            Trace.TraceInformation("Starting game service host...");

            _serviceHost = new ServiceHost(typeof(GameService));

            _serviceHost.Faulted += (sender, e) =>
            {
                Trace.TraceError("Host fault occured. Aborting and restarting the host. Retry count: {0}", retries);
                _serviceHost.Abort();
                StartGameService(--retries);
            };

            var binding = new NetTcpBinding(SecurityMode.None);

            RoleInstanceEndpoint externalEndPoint =
                RoleEnvironment.CurrentRoleInstance.InstanceEndpoints["GameServer"];
            RoleInstanceEndpoint mexpEndPoint = RoleEnvironment.CurrentRoleInstance.InstanceEndpoints["mexport"];

            var metadatabehavior = new ServiceMetadataBehavior();
            _serviceHost.Description.Behaviors.Add(metadatabehavior);

            Binding mexBinding = MetadataExchangeBindings.CreateMexTcpBinding();
            string mexendpointurl = string.Format("net.tcp://{0}/GameServerMetadata", mexpEndPoint.IPEndpoint);
            _serviceHost.AddServiceEndpoint(typeof(IMetadataExchange), mexBinding, mexendpointurl);

            _serviceHost.AddServiceEndpoint(
               typeof(IGameService),
               binding,
               string.Format("net.tcp://{0}/GameServer", externalEndPoint.IPEndpoint));

            try
            {
                _serviceHost.Open();
                Trace.TraceInformation("Game service host started successfully.");
            }
            catch (TimeoutException timeoutException)
            {
                Trace.TraceError(
                    "The service operation timed out. {0}",
                     timeoutException.Message);
            }
            catch (CommunicationException communicationException)
            {
                Trace.TraceError(
                                "Could not start game service host. {0}",
                                communicationException.Message);
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _serviceHost.Close();
                }

                // shared cleanup logic
                _disposed = true;
            }
        }
    }
}
