// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AccountService.cs" company="RXPL">
//   All Rights Reserved.
// </copyright>
// <summary>
//   Defines the AccountService type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace RXPL.AD.Service
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.IO;
    using System.Management.Automation;
    using System.Net;
    using System.ServiceModel.Web;
    using System.Text;

    using LoggingExtensions.Logging;

    /// <summary>
    /// The account service.
    /// </summary>
    public class AccountService : IAccountService
    {
        /// <summary>
        /// The logger.
        /// </summary>
        private readonly ILog logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="AccountService"/> class.
        /// </summary>
        public AccountService()
        {
            this.logger = Log.GetLoggerFor(this.GetType().Name);
        }

        /// <summary>
        /// Resets the password of the specified user.
        /// </summary>
        /// <param name="accountDetails">
        /// The account details.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool ResetPassword(AccountDetails accountDetails)
        {
            this.logger.Info(() => string.Format("Password reset invoked for user: {0}", accountDetails.UserId));

            try
            {
                var scriptFile = AppDomain.CurrentDomain.BaseDirectory
                                 + ConfigurationManager.AppSettings["PasswordResetScript"];
                using (var powerShell = PowerShell.Create())
                {
                    powerShell.AddScript(File.ReadAllText(scriptFile));
                    powerShell.AddParameters(
                        new Dictionary<string, string>
                            {
                                { "employeeID", accountDetails.UserId },
                                { "password", accountDetails.Password }
                            });
                    powerShell.Invoke();
                    if (powerShell.Streams.Error.Count > 0)
                    {
                        var errorMessage = new StringBuilder();
                        foreach (var errorRecord in powerShell.Streams.Error)
                        {
                            errorMessage.AppendLine(errorRecord.Exception.Message);
                        }

                        this.logger.Error(() => errorMessage.ToString());
                        throw new WebFaultException<string>(errorMessage.ToString(), HttpStatusCode.BadRequest);
                    }
                }

                this.logger.Info(() => string.Format("Password has been reset for user: {0}", accountDetails.UserId));
            }
            catch (Exception ex)
            {
                this.logger.Error(
                    () => string.Format("Error occured while resetting password for user: {0}", accountDetails.UserId),
                    ex);
                throw;
            }

            return true;
        }

        /// <summary>
        /// The get.
        /// </summary>
        /// <returns>
        /// The <see cref="AccountDetails"/>.
        /// </returns>
        public AccountDetails Get()
        {
            return new AccountDetails { Password = "TempPassword", UserId = "myUser" };
        }
    }
}