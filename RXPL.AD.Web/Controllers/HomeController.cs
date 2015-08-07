// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HomeController.cs" company="RXPL">
//   All Rights Reserved.
// </copyright>
// <summary>
//   The home controller.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace RXPL.AD.Web.Controllers
{
    using System;
    using System.Configuration;
    using System.Web.Mvc;

    using LoggingExtensions.Logging;

    using Newtonsoft.Json;

    using RXPL.AD.Web.Helpers;
    using RXPL.AD.Web.Models;

    /// <summary>
    /// The home controller.
    /// </summary>
    public class HomeController : Controller
    {
        /// <summary>
        /// The index.
        /// </summary>
        /// <param name="userId">
        /// The user Id.
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        public ActionResult Index(string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                Log.GetLoggerFor(this.GetType().Name)
                    .Info(() => "Get invoked without a user id; returning 400 exception.");
                return new HttpStatusCodeResult(400, "User Id is required");
            }

            userId = this.Server.HtmlDecode(userId.Trim());
            Log.GetLoggerFor(this.GetType().Name)
                .Info(() => string.Format("Get invoked with user id: {0}", userId));
            this.TempData["UserId"] = userId;
            return this.RedirectToAction("Intermediate");
        }

        /// <summary>
        /// The index.
        /// </summary>
        /// <param name="directoryUser">
        /// The directory user.
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        [HttpPost]
        public ActionResult Index(DirectoryUser directoryUser)
        {
            try
            {
                var serviceUrl = ConfigurationManager.AppSettings["ADServiceUrl"];
                var serviceHelper = new ServiceHelper(serviceUrl);
                var userDetails = new { directoryUser.UserId, directoryUser.Password };
                var request = JsonConvert.SerializeObject(userDetails);
                var result = serviceHelper.Post<bool>(request);
                this.ViewBag.Message = result
                                      ? "The password has been reset successfully"
                                      : "The password could not be reset at the moment. Please contact the administrator.";
            }
            catch (Exception ex)
            {
                Log.GetLoggerFor(this.GetType().Name)
                    .Error(() => ex.ToString());
                this.ViewBag.Message =
                    "An error occured while processing the request. Please contact the administrator.";
            }

            return this.View(directoryUser);
        }

        /// <summary>
        /// The intermediate action.
        /// </summary>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        public ActionResult Intermediate()
        {
            var directoryUser = new DirectoryUser { UserId = (string)this.TempData["UserId"] };
            return this.View("Index", directoryUser);
        }
    }
}