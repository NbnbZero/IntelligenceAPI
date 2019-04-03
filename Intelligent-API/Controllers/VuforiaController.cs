using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Intelligent.API.Framework;
using Intelligent.API.Models;
using Intelligent.API.Models.Request;
using Intelligent.API.Models.Response;
using Intelligent.Data.AzureFiles;
using Intelligent.Data.Cosmos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using Intelligent.Data.Cosmos.Models;
using Intelligent.Data.Generic;
using Newtonsoft.Json;
using System.Text;

namespace Intelligent.API.Controllers
{
    /// <summary>
    /// API Controller for all things having to do with Augmented Reality.
    /// </summary>
    /// <see cref="IntelligentMixedRealityController"/>
//    [ApiVersion("1.0")]                                   // TODO: Un-Comment after applying settings for API versioning
    [AllowAnonymous]                                        // TODO: Remove for Authentication
                                                            //    [Route("api/v{version:apiVersion}/augmentedReality")] // TODO: The versioned API Route
    [Route("api/vuforia")]                         // TODO: Remove after applying settings for API versioning
    public class VuforiaController : IntelligentMixedRealityController
    {

        private HttpClient _vuforiaClient;

        /// <summary>Initializes a new instance of the <see cref="VuforiaController"/> class.</summary>
        /// <param name="logger">The logger instance used to log any messages from this controller.</param>
        public VuforiaController(IHttpClientFactory factory, ILogger<AugmentedRealityController> logger) : base(logger)
        {
            _vuforiaClient = factory.CreateClient("vuforia-api");
        }

        #region Augmented Reality - Vuforia

        [HttpGet("/targets/{targetId}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(IList<ImageReferenceResponse>))]
        public async Task<ActionResult<IList<ImageReferenceResponse>>> getTargetVuforia()
        {
            string accessKey = "[server access key]";
            string secretKey = "[server secret]";
            string targetId = "[target id]";
            string url = "https://vws.vuforia.com";
            Uri vuforiaUri = new Uri(url + "/targets/" + targetId);

            var req = new HttpRequestMessage(HttpMethod.Post,
                $"api/vuforia/targets/{targetId}")
            {
                //Content = new StringContent(JsonConvert.SerializeObject(request, Formatting.Indented), Encoding.UTF8, MimeTypes.Application.Json)
            };
            //var req = new HttpRequestMessage(HttpMethod.Get, vuforiaUri);
            //HttpWebRequest request = (HttpWebRequest)WebRequest.Create(vuforiaUri);
            var resp = await _vuforiaClient.SendAsync(req);
            //HttpWebResponse webResponse = (HttpWebResponse)request.GetResponse();

            if (resp.IsSuccessStatusCode)
            {
                return Ok(resp.Content.ReadAsAsync<ImageReferenceResponse>());
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost("/targets/{targetId}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(IList<ImageReferenceResponse>))]
        public async Task<ActionResult<IList<ImageReferenceResponse>>> addTargetVuforia([FromForm]FileUploadRequest request)
        {
            string accessKey = "[server access key]";
            string secretKey = "[server secret]";
            string targetId = "[target id]";
            string url = "https://vws.vuforia.com";
            Uri vuforiaUri = new Uri(url + "/targets/" + targetId);

            var req = new HttpRequestMessage(HttpMethod.Post,
                $"api/vuforia/targets/{targetId}")
            {
                Content = new StringContent(JsonConvert.SerializeObject(request, Formatting.Indented), Encoding.UTF8, MimeTypes.Application.Json)
            };

            //var req = new HttpRequestMessage(HttpMethod.Get, vuforiaUri);
            //HttpWebRequest request = (HttpWebRequest)WebRequest.Create(vuforiaUri);
            var resp = await _vuforiaClient.SendAsync(req);
            //HttpWebResponse webResponse = (HttpWebResponse)request.GetResponse();

            if (resp.IsSuccessStatusCode)
            {
                return Ok(resp.Content.ReadAsAsync<ImageReferenceResponse>());
            }
            else
            {
                return BadRequest();
            }
        }

        #endregion
    }
}