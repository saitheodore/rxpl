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
    using System.Diagnostics;

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
                var scriptFile = @"powershell.exe";

                var process = new Process
                                  {
                                      StartInfo =
                                          {
                                              FileName = scriptFile,
                                              Arguments =
                                                  string.Format(
                                                      @"D:\Sai\Projects\RXPL.AD.Service\test.ps1 -employeeID {0} -password {1}",
                                                      accountDetails.UserId,
                                                      accountDetails.Password)
                                          }
                                  };
                process.Start();
                process.WaitForExit();

                this.logger.Info(() => string.Format("Password has been reset for user: {0}", accountDetails.UserId));
            }
            catch (System.Exception ex)
            {
                this.logger.Error(
                    () => string.Format("Error occured while resetting password for user: {0}", accountDetails.UserId),
                    ex);
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