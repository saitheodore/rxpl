// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Startup.cs" company="RXPL">
//   All Rights Reserved.
// </copyright>
// <summary>
//   The startup.
// </summary>
// --------------------------------------------------------------------------------------------------------------------



using Microsoft.Owin;

using RXPL.AD.Web;

[assembly: OwinStartup(typeof(RXPL.AD.Web.Startup))]

namespace RXPL.AD.Web
{
    using LoggingExtensions.log4net;
    using LoggingExtensions.Logging;

    using Owin;

    /// <summary>
    /// The startup.
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// The configuration.
        /// </summary>
        /// <param name="app">
        /// The app.
        /// </param>
        public void Configuration(IAppBuilder app)
        {
            Log.InitializeWith<Log4NetLog>();
        }
    }
}
