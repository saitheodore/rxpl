// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AccountWinService.cs" company="RXPL">
//   All Rights Reserved.
// </copyright>
// <summary>
//   The account win service.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace RXPL.Services.Host
{
    using System.ServiceProcess;

    /// <summary>
    /// The account win service.
    /// </summary>
    public partial class AccountWinService : ServiceBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AccountWinService"/> class.
        /// </summary>
        public AccountWinService()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// The on start.
        /// </summary>
        /// <param name="args">
        /// The args.
        /// </param>
        protected override void OnStart(string[] args)
        {
            ServicesHelper.StartService();
        }

        /// <summary>
        /// The on stop.
        /// </summary>
        protected override void OnStop()
        {
            ServicesHelper.StopService();
        }
    }
}
