// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ServicesHelper.cs" company="Consilio">
//   All Rights Reserved.
// </copyright>
// <summary>
//   Defines the ServicesHelper type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace RXPL.Services.Host
{
    using System;
    using System.Configuration;
    using System.Linq;
    using System.Net;
    using System.Net.Sockets;
    using System.ServiceModel;
    using System.ServiceModel.Web;

    using log4net.Repository.Hierarchy;

    using LoggingExtensions.Logging;

    using RXPL.AD.Service;

    /// <summary>
    /// The services helper.
    /// </summary>
    public class ServicesHelper
    {
        /// <summary>
        /// The web host.
        /// </summary>
        private static WebServiceHost webHost;

        /// <summary>
        /// The logger.
        /// </summary>
        private static ILog logger;

        /// <summary>
        /// Initializes static members of the <see cref="ServicesHelper"/> class.
        /// </summary>
        static ServicesHelper()
        {
            logger = LoggingExtensions.Logging.Log.GetLoggerFor("ServiceHelper");
        }

        /// <summary>
        /// The start service.
        /// </summary>
        public static void StartService()
        {
            var servicePort = ConfigurationManager.AppSettings["ServicePort"];
            var serviceName = ConfigurationManager.AppSettings["ServiceName"];
            var hostIp =
                Dns.GetHostEntry(Dns.GetHostName())
                    .AddressList.First(x => x.AddressFamily == AddressFamily.InterNetwork);

            var url = new Uri(string.Format("http://{0}:{1}/{2}", hostIp, servicePort, serviceName));
            webHost = new WebServiceHost(typeof(AccountService), url);
            webHost.Open();

            foreach (var address in webHost.Description.Endpoints.Select(endPoint => endPoint.Address))
            {
                var tempAddress = address;
                logger.Info(() => string.Format("Service hosted on: {0}", tempAddress));
            }
        }

        /// <summary>
        /// The stop service.
        /// </summary>
        public static void StopService()
        {
            if (webHost != null && webHost.State != CommunicationState.Closed)
            {
                webHost.Close();
            }
        }
    }
}