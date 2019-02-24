using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Intelligent.API.Framework;
using Intelligent.API.Models.Request;
using Intelligent.API.Models.Response;
using Intelligent.Data.AzureFiles;
using Intelligent.Data.Cosmos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using Intelligent.Data.Cosmos.Models;
using Intelligent.Data.Generic;
using System.Net.Http;
using Intelligent.API.Models;
using System.IO;

namespace Intelligent.API.Controllers
{
    /// <summary>
    /// API Controller for all things having to do with AIML Chatbot.
    /// </summary>
    /// <see cref="IntelligentMixedRealityController"/>
//    [ApiVersion("1.0")]                                   // TODO: Un-Comment after applying settings for API versioning
    [AllowAnonymous]                                        // TODO: Remove for Authentication
//    [Route("api/v{version:apiVersion}/augmentedReality")] // TODO: The versioned API Route
    [Route("api/AIMLChatbot")]                         // TODO: Remove after applying settings for API versioning
    public class AIMLChatbotController : IntelligentMixedRealityController
    {

        private HttpClient _imrClient;

        /// <summary>Initializes a new instance of the <see cref="AugmentedRealityController"/> class.</summary>
        /// <param name="logger">The logger instance used to log any messages from this controller.</param>
        public AIMLChatbotController(IHttpClientFactory factory, ILogger<AugmentedRealityController> logger) : base(logger)
        {
            _imrClient = factory.CreateClient("private-api");
        }
        /// <summary>Initializes a new instance of the <see cref="AIMLChatbotController"/> class.</summary>
        /// <param name="logger">The logger instance used to log any messages from this controller.</param>
        public AIMLChatbotController(ILogger<AIMLChatbotController> logger) : base(logger)
        {

        }

        #region AIML Chatbot - Messaging
        /// <summary>
        /// Gets a reference to all of a user's info
        /// </summary>
        /// <param name="userId">The User's ID.</param>
        /// <returns></returns>
        [HttpGet("conversation/userId")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(IList<ImageReferenceResponse>))]
        public async Task<object> GetUserAsync(string userId, string conversationTag)
        {
            // Instantiate the request
            var req = new HttpRequestMessage(HttpMethod.Get,
                $"api/AIMLChatbot/conversationTag/userId");

            // Send the request via HttpClient received through Dependency Injection
            var resp = await _imrClient.SendAsync(req);

            // TODO: Handle responses based on the response code from the Private API
            if (resp.IsSuccessStatusCode)
                return Ok(resp.Content.ReadAsAsync<ImageReferenceResponse>());

            return BadRequest(resp.Content.ReadAsAsync<IntelligentMixedRealityError>());

        }
        //=> throw new NotImplementedException();

        /// <summary>
        /// Gets a reference to user's stored conversation.
        /// </summary>
        /// <param name="userId">The User's ID.</param>
        /// <param name="conversationId">The conversation ID.</param>
        /// <returns></returns>
        [HttpGet("conversation/{userId}/{conversationId}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ImageReferenceResponse))]
        public async Task<object> GetUserConversationAsync(string userId, string conversationId)
        {
            // Instantiate the request
            var req = new HttpRequestMessage(HttpMethod.Get,
                $"api/AIMLChatbot/{userId}/{conversationId}");

            // Send the request via HttpClient received through Dependency Injection
            var resp = await _imrClient.SendAsync(req);

            // TODO: Handle responses based on the response code from the Private API
            if (resp.IsSuccessStatusCode)
                return Ok(resp.Content.ReadAsAsync<ImageReferenceResponse>());

            return BadRequest(resp.Content.ReadAsAsync<IntelligentMixedRealityError>());
        }

        /// <summary>
        /// Gets a reference to user's stored message.
        /// </summary>
        /// <param name="userId">The User's ID.</param>
        /// <param name="conversationId">The conversation ID.</param>
        /// <param name="messageId">The individual message ID.</param>
        /// <returns></returns>
        [HttpGet("conversation/{userId}/{conversationId}/{messageId}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ImageReferenceResponse))]
        public async Task<object> GetUserMessageAsync(string userId, string conversationId, string messageId)
        {
            // Instantiate the request
            var req = new HttpRequestMessage(HttpMethod.Get,
                $"api/AIMLChatbot/{userId}/{conversationId}/{messageId}");

            // Send the request via HttpClient received through Dependency Injection
            var resp = await _imrClient.SendAsync(req);

            // TODO: Handle responses based on the response code from the Private API
            if (resp.IsSuccessStatusCode)
                return Ok(resp.Content.ReadAsAsync<ImageReferenceResponse>());

            return BadRequest(resp.Content.ReadAsAsync<IntelligentMixedRealityError>());
        }

        /// <summary>
        /// Gets a reference to user's stored message.
        /// </summary>
        /// <param name="userId">The User's ID.</param>
        /// <param name="conversationId">The conversation ID.</param>
        /// <returns></returns>
        [HttpPost("conversation/{userId}/{conversationId}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ImageReferenceResponse))]
        public async Task<object> PostUserConversationAsync(string userId, string conversationId, string messageId, [FromForm]FileUploadRequest request)
        {
            // Read the File into a Byte[]
            byte[] data;
            using (var br = new BinaryReader(request.File.OpenReadStream()))
                data = br.ReadBytes((int)request.File.Length);

            // Instantiate the request
            var req = new HttpRequestMessage(HttpMethod.Post,
                $"api/AIMLChatbot/{userId}/{conversationId}")
            {
                Content = new MultipartFormDataContent { { new ByteArrayContent(data), "file", request.File.FileName } }
            };

            // Send the request via HttpClient received through Dependency Injection
            var resp = await _imrClient.SendAsync(req);

            // TODO: Handle responses based on the response code from the Private API
            if (resp.IsSuccessStatusCode)
                return Ok(resp.Content.ReadAsAsync<ImageReferenceResponse>());

            return BadRequest(resp.Content.ReadAsAsync<IntelligentMixedRealityError>());
        }

        /// <summary>
        /// Gets a reference to user's stored message.
        /// </summary>
        /// <param name="userId">The User's ID.</param>
        /// <param name="conversationId">The conversation ID.</param>
        /// <param name="messageId">The individual message ID.</param>
        /// <returns></returns>
        [HttpPost("conversation/{userId}/{conversationId}/{messageId}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ImageReferenceResponse))]
        public async Task<object> PostUserMessageAsync(string userId, string conversationId, string messageId, [FromForm]FileUploadRequest request)
        {
            // Read the File into a Byte[]
            byte[] data;
            using (var br = new BinaryReader(request.File.OpenReadStream()))
                data = br.ReadBytes((int)request.File.Length);

            // Instantiate the request
            var req = new HttpRequestMessage(HttpMethod.Post,
                $"api/AIMLChatbot/{userId}/{conversationId}")
            {
                Content = new MultipartFormDataContent { { new ByteArrayContent(data), "file", request.File.FileName } }
            };

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
        /// <param name="conversationId"></param>
        /// <returns></returns>
        [HttpDelete("conversation/{userId}/{conversationId}")]
        [Consumes(MimeTypes.Misc.FormData)]
        public async Task<object> DeleteUserConversationAsync(string userId, string conversationId) => throw new NotImplementedException();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="conversationId"></param>
        /// <returns></returns>
        [HttpPut("conversation/{userId}/{conversationId}")]
        public async Task<object> HideUserConversationAsync(string userId, string conversationId) => throw new NotImplementedException();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="conversationId"></param>
        /// <param name="messageId"></param>
        /// <returns></returns>
        [HttpDelete("conversation/{userId}/{conversationId}/{messageId}")]
        public async Task<object> DeleteMessageAsync(string userId, string conversationId, string messageId) => throw new NotImplementedException();
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="conversationId"></param>
        /// <param name="messageId"></param>
        /// <returns></returns>
        [HttpPut("conversation/{userId}/{conversationId}/{messageId}")]
        public async Task<object> HideMessageAsync(string userId, string conversationId, string messageId) => throw new NotImplementedException();
        #endregion
    }
}