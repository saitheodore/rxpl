// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AccountService.cs" company="Consilio">
//   All Rights Reserved.
// </copyright>
// <summary>
//   Defines the AccountService type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace RXPL.AD.Service
{
    using System.Collections.Generic;
    using System.Configuration;
    using System.Diagnostics;
    using System.IO;
    using System.Management.Automation;

    using LoggingExtensions.Logging;

    /// <summary>
    /// The account service.
    /// </summary>
    public class AccountService : IAccountService
    {
        /// <summary>
        /// The logger.
        /// </summary>
        private ILog logger;

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
                var scriptFile = ConfigurationManager.AppSettings["PasswordResetScript"];
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
                }

                this.logger.Info(() => string.Format("Password has been reset for user: {0}", accountDetails.UserId));
            }
            catch (System.Exception ex)
            {
                this.logger.Error(
                    () => string.Format("Error occured while resetting password for user: {0}", accountDetails.UserId),
                    ex);
                return false;
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