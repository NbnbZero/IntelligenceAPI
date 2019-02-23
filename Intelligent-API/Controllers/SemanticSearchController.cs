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

namespace Intelligent.API.Controllers
{
    /// <summary>
    /// API Controller for all things having to do with Augmented Reality.
    /// </summary>
    /// <see cref="IntelligentMixedRealityController"/>
//    [ApiVersion("1.0")]                                   // TODO: Un-Comment after applying settings for API versioning
    [AllowAnonymous]                                        // TODO: Remove for Authentication
                                                            //    [Route("api/v{version:apiVersion}/augmentedReality")] // TODO: The versioned API Route
    [Route("api/semanticSearch")]                         // TODO: Remove after applying settings for API versioning
    public class semanticSearchController : IntelligentMixedRealityController
    {

        private HttpClient _imrClient;

        /// <summary>Initializes a new instance of the <see cref="semanticSearchController"/> class.</summary>
        /// <param name="logger">The logger instance used to log any messages from this controller.</param>
        public semanticSearchController(IHttpClientFactory factory, ILogger<semanticSearchController> logger) : base(logger)
        {
            _imrClient = factory.CreateClient("private-api");
        }

        #region Semantic Search - Documents
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId">The User's ID.</param>
        /// <param name="documentId">The requested Document's Id.</param>
        /// <returns></returns>
        [HttpGet("{userId}/tag/{documentId}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(IList<ImageReferenceResponse>))]
        public async Task<ActionResult<IList<ImageReferenceResponse>>> GetUserImageTagSetAsync(string userId, string documentId) => throw new NotImplementedException();

        /// <summary>
        /// Gets a reference to user's stored image by <paramref name="imageTag"/> and <paramref name="index"/>.
        /// </summary>
        /// <param name="userId">The User's ID.</param>
        /// <param name="imageTag">The requested Image's tag.</param>
        /// <param name="index">The non-zero based index of the image in the tag set.</param>
        /// <returns></returns>
        [HttpGet("{userId}/tag/{documentId}/{index}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ImageReferenceResponse))]
        public async Task<ActionResult<ImageReferenceResponse>> GetUserImageAsync(string userId, string documentId, int index) => throw new NotImplementedException();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId">The User's ID.</param>
        /// <param name="imageTag">The requested Image's tag.</param>
        /// <param name="imageId">The requested Image's ID.</param>
        /// <returns></returns>
        [HttpGet("{userId}/tag/{imageTag}/image/{imageId}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ImageReferenceResponse))]
        public async Task<ActionResult<ImageReferenceResponse>> GetUserImageAsync(string userId, string imageTag, string imageId)
        {
            // Instantiate the request
            var req = new HttpRequestMessage(HttpMethod.Get,
                $"api/augmentedReality/{userId}/tag/{imageTag}/image/{imageId}");

            // Send the request via HttpClient received through Dependency Injection
            var resp = await _imrClient.SendAsync(req);

            // TODO: Handle responses based on the response code from the Private API
            if (resp.IsSuccessStatusCode)
                return Ok(resp.Content.ReadAsAsync<ImageReferenceResponse>());

            return BadRequest(resp.Content.ReadAsAsync<IntelligentMixedRealityError>());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="imageTag"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("{userId}/tag/{documentId}")]
        [Consumes(MimeTypes.Misc.FormData)]
        [ProducesResponseType((int)HttpStatusCode.Created, Type = typeof(ImageReferenceResponse))]
        public async Task<ActionResult> UploadUserImageAsync(string userId, string documentId, [FromForm]FileUploadRequest request)
        {
            // Read the File into a Byte[]
            byte[] data;
            using (var br = new BinaryReader(request.File.OpenReadStream()))
                data = br.ReadBytes((int)request.File.Length);

            // Instantiate the request
            var req = new HttpRequestMessage(HttpMethod.Post,
                $"api/augmentedReality/{userId}/tag/{documentId}")
            {
                Content = new MultipartFormDataContent { { new ByteArrayContent(data), "file", request.File.FileName } }
            };

            // Send the request via HttpClient received through Dependency Injection
            var resp = await _imrClient.SendAsync(req);

            // TODO: Handle responses based on the response code from the Private API
            if (resp.IsSuccessStatusCode)
                return Ok(resp.Content.ReadAsAsync<ImageReferenceResponse>());

            return BadRequest();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="imageTag"></param>
        /// <returns></returns>
        [HttpDelete("{userId}/tag/{documentId}")]
        public async Task<object> DeleteUserImageTagSetAsync(string userId, string documentId)// => throw new NotImplementedException();
        {
            // Instantiate the request
            var req = new HttpRequestMessage(HttpMethod.Delete,
                $"api/augmentedReality/{userId}/tag/{documentId}")
            {
                // Content = new MultipartFormDataContent { { new ByteArrayContent(data), "file", request.File.FileName } }

            };

            // Send the request via HttpClient received through Dependency Injection
            var resp = await _imrClient.SendAsync(req);

            // TODO: Handle responses based on the response code from the Private API
            if (resp.IsSuccessStatusCode)
                return Ok(resp.Content.ReadAsAsync<ImageReferenceResponse>());

            return BadRequest();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="imageTag"></param>
        /// <param name="imageId"></param>
        /// <returns></returns>
        public async Task<ActionResult<ImageReferenceResponse>> DeleteUserImageAsync(string userId, string imageTag, string imageId) => throw new NotImplementedException();
        #endregion

        #region Semantic Search - Vuforia
        // TODO: Here is where the Vuforia routes should go
        #endregion
    }
}