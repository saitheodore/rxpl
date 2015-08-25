// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Program.cs" company="RXPL">
//   All Rights Reserved.
// </copyright>
// <summary>
//   The program.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace RXPL.Services.Host
{
    using System;
    using System.Runtime.Remoting.Services;
    using System.ServiceProcess;

    using LoggingExtensions.Logging;

    /// <summary>
    /// The program.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// The main.
        /// </summary>
        /// <param name="args">
        /// The args.
        /// </param>
        public static void Main(string[] args)
        {
            Log.InitializeWith<LoggingExtensions.log4net.Log4NetLog>();
            Log.GetLoggerFor("Program").Info(() => "Starting the service.");

#if DEBUG
            ServicesHelper.StartService();
            Console.ReadKey();
#else
            var services = new ServiceBase[] { new AccountWinService() };
            ServiceBase.Run(services);
#endif
        }
    }
}
