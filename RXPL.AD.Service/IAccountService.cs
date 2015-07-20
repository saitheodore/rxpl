// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IAccountService.cs" company="Consilio">
//   All Rights Reserved.
// </copyright>
// <summary>
//   The AccountService interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace RXPL.AD.Service
{
    using System.ServiceModel;
    using System.ServiceModel.Web;

    /// <summary>
    /// The AccountService interface.
    /// </summary>
    [ServiceContract]
    public interface IAccountService
    {
        /// <summary>
        /// When implemented, resets the password of the specified user.
        /// </summary>
        /// <param name="accountDetails">
        /// The account details.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        [OperationContract]
        [WebInvoke(UriTemplate = "reset", ResponseFormat = WebMessageFormat.Json)]
        bool ResetPassword(AccountDetails accountDetails);

        /// <summary>
        /// The get.
        /// </summary>
        /// <returns>
        /// The <see cref="AccountDetails"/>.
        /// </returns>
        [OperationContract]
        [WebGet(UriTemplate = "/")]
        AccountDetails Get();
    }
}
