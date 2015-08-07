// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DirectoryUser.cs" company="RXPL">
//   All Rights Reserved.
// </copyright>
// <summary>
//   The directory user.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace RXPL.AD.Web.Models
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// The directory user.
    /// </summary>
    public class DirectoryUser
    {
        /// <summary>
        /// Gets or sets the user id.
        /// </summary>
        [Required(ErrorMessage = "User Id is required")]
        public string UserId { get; set; }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        [Required(ErrorMessage = "Password is required.")]
        public string Password { get; set; }
    }
}
