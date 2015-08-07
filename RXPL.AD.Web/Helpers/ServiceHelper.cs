// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ServiceHelper.cs" company="RXPL">
//   All Rights Reserved.
// </copyright>
// <summary>
//   The service helper.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace RXPL.AD.Web.Helpers
{
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Text;

    using LoggingExtensions.Logging;

    using Newtonsoft.Json;

    /// <summary>
    /// The service helper.
    /// </summary>
    public class ServiceHelper
    {
        /// <summary>
        /// The service url.
        /// </summary>
        private readonly string serviceUrl;

        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceHelper"/> class.
        /// </summary>
        /// <param name="serviceUrl">
        /// The service url.
        /// </param>
        public ServiceHelper(string serviceUrl)
        {
            this.serviceUrl = serviceUrl;
            Log.GetLoggerFor(this.GetType().Name)
                .Info(() => string.Format("Service helper invoked with url: {0}", serviceUrl));
        }

        /// <summary>
        /// The post.
        /// </summary>
        /// <param name="request">
        /// The request.
        /// </param>
        /// <param name="mediaType">
        /// The media Type.
        /// </param>
        /// <typeparam name="T">
        /// The type of response object.
        /// </typeparam>
        /// <returns>
        /// The <see cref="T"/>.
        /// </returns>
        public T Post<T>(string request, string mediaType = "application/json")
        {
            var httpClient = new HttpClient { BaseAddress = new Uri(this.serviceUrl) };
            var response = httpClient.PostAsync(new Uri(this.serviceUrl), new StringContent(request, Encoding.UTF8, mediaType));
            Log.GetLoggerFor(this.GetType().Name).Info(() => string.Format("Service invoked at {0}", this.serviceUrl));
            if (response.Result.StatusCode == HttpStatusCode.Accepted || response.Result.StatusCode == HttpStatusCode.OK)
            {
                Log.GetLoggerFor(this.GetType().Name)
                    .Info(() => "Service invoke successful.");
                return JsonConvert.DeserializeObject<T>(response.Result.Content.ReadAsStringAsync().Result);
            }

            Log.GetLoggerFor(this.GetType().Name)
                    .Info(() => "Service invoke failed.");
            throw new Exception(response.Result.Content.ReadAsStringAsync().Result);
        }
    }
}
