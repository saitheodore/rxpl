// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AccountDetails.cs" company="Consilio">
//   All Rights Reserved.
// </copyright>
// <summary>
//   Defines the AccountDetails type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace RXPL.AD.Service
{
    using System.Runtime.Serialization;

    /// <summary>
    /// The account details.
    /// </summary>
    [DataContract]
    public class AccountDetails
    {
        /// <summary>
        /// Gets or sets the user id.
        /// </summary>
        [DataMember]
        public string UserId { get; set; }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        [DataMember]
        public string Password { get; set; } 
    }
}