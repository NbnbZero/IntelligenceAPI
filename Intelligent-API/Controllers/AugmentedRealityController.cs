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

//TODO: Add usings for vuforia services
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
    [Route("api/augmentedReality")]                         // TODO: Remove after applying settings for API versioning
    public class AugmentedRealityController : IntelligentMixedRealityController
    {

        private HttpClient _imrClient;

        /// <summary>Initializes a new instance of the <see cref="AugmentedRealityController"/> class.</summary>
        /// <param name="logger">The logger instance used to log any messages from this controller.</param>
        public AugmentedRealityController(IHttpClientFactory factory, ILogger<AugmentedRealityController> logger) : base(logger)
        {
            _imrClient = factory.CreateClient("private-api");
        }

        #region Augmented Reality - Images
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId">The User's ID.</param>
        /// <param name="imageTag">The requested Image's tag.</param>
        /// <returns></returns>
        /// Ben Method
        [HttpGet("{userId}/tag/{imageTag}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(IList<ImageReferenceResponse>))]
        public async Task<ActionResult<IList<ImageReferenceResponse>>> GetUserImageTagSetAsync(string userId, string imageTag, string imageId)
        {
            // Query for Upload File Document with ID <c>imageId</c>
            var document = await CosmosContext.Instance.GetDocumentAsync<UploadFileDocument>(UploadFileDocument.Partition, imageId);

            // TODO: Verification -- Does the User ID match what was sent? What happens if it doesn't? What about the Image Tag?
            //Document should contain parameters that match the variables -- access and compare through `document`
            //info may be in the metadata -- check postman
            if(document.imageTag != imageTag){
                //return response for bad parameters
                return BadRequest();
            }
            if(document.userId != userId){
                //return response for bad authetication
                return BadRequest();
            }

            return Ok(new ImageReferenceResponse()
            {
                ImageTag = imageTag,
                FileName = document.FileName,
                Metadata = document.Metadata,
                ImageReference = document.Reference.ToString()
            });
        }

        /// <summary>
        /// Gets a reference to user's stored image by <paramref name="imageTag"/> and <paramref name="index"/>.
        /// </summary>
        /// <param name="userId">The User's ID.</param>
        /// <param name="imageTag">The requested Image's tag.</param>
        /// <param name="index">The non-zero based index of the image in the tag set.</param>
        /// <returns></returns>
        /// sophie
        [HttpGet("{userId}/tag/{imageTag}/{index}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ImageReferenceResponse))]
        public async Task<ActionResult<ImageReferenceResponse>> GetUserImageAsync(string userId, string imageTag, int index)
        {
            // Instantiate the request
            var req = new HttpRequestMessage(HttpMethod.Get,
                $"api/augmentedReality/{userId}/tag/{imageTag}/{index}");

            // Send the request via HttpClient received through Dependency Injection
            var resp = await _imrClient.SendAsync(req);

            // TODO: Handle responses based on the response code from the Private API
            if (resp.IsSuccessStatusCode)
                return Ok(resp.Content.ReadAsAsync<ImageReferenceResponse>());

            return BadRequest(resp.Content.ReadAsAsync<IntelligentMixedRealityError>());
        }


//            // TODO: Verification -- Does the User ID match what was sent? What happens if it doesn't? What about the Image Tag?
//            //Document should contain parameters that match the variables -- access and compare through `document`
//            //info may be in the metadata -- check postman
//            if(document.imageTag != imageTag){
//                //return response for bad parameters
//                return BadRequest();
//            }
//            if(document.userId != userId){
//                //return response for bad authetication
//                return BadRequest();
//            }
//            return Ok(new ImageReferenceResponse()
//            {
//                Index = document.index,
//                ImageTag = imageTag,
//                FileName = document.FileName,
//                Metadata = document.Metadata,
//                ImageReference = document.Reference.ToString()
//            });
//        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId">The User's ID.</param>
        /// <param name="imageTag">The requested Image's tag.</param>
        /// <param name="imageId">The requested Image's ID.</param>
        /// <returns></returns>
        /// implemented
        [HttpGet("{userId}/tag/{imageTag}/image/{imageId}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ImageReferenceResponse))]
        public async Task<ActionResult<ImageReferenceResponse>> GetUserImageAsync(string userId, string imageTag, string imageId)
        {
            // Instantiate the request
            var req = new HttpRequestMessage(HttpMethod.Get,
<<<<<<< HEAD
                $"api/augmentedReality/{userId}/tag/{imageTag}/image/{imageId}"  );
=======
                $"api/augmentedReality/{userId}/tag/{imageTag}/image/{imageId}");
>>>>>>> master

            // Send the request via HttpClient received through Dependency Injection
            var resp = await _imrClient.SendAsync(req);

            // TODO: Handle responses based on the response code from the Private API
            if (resp.IsSuccessStatusCode)
<<<<<<< HEAD
                return Ok(new ImageReferenceResponse()
                {
                    ImageId = document.Id,
                    ImageTag = imageTag,
                    FileName = document.FileName,
                    Metadata = document.Metadata,
                    ImageReference = document.Reference.ToString()
                });
=======
                return Ok(resp.Content.ReadAsAsync<ImageReferenceResponse>());
>>>>>>> master

            return BadRequest(resp.Content.ReadAsAsync<IntelligentMixedRealityError>());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="imageTag"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        /// implemented
        [HttpPost("{userId}/tag/{imageTag}")]
        [Consumes(MimeTypes.Misc.FormData)]
        [ProducesResponseType((int)HttpStatusCode.Created, Type = typeof(ImageReferenceResponse))]
        public async Task<ActionResult> UploadUserImageAsync(string userId, string imageTag, [FromForm]FileUploadRequest request)
        {
            // Read the File into a Byte[]
            byte[] data;
            using (var br = new BinaryReader(request.File.OpenReadStream()))
                data = br.ReadBytes((int)request.File.Length);

            // Instantiate the request
            var req = new HttpRequestMessage(HttpMethod.Post,
                $"api/augmentedReality/{userId}/tag/{imageTag}")
            {
                Content = new MultipartFormDataContent { { new ByteArrayContent(data), "file", request.File.FileName } }
            };

            // Send the request via HttpClient received through Dependency Injection
            var resp = await _imrClient.SendAsync(req);

            // TODO: Handle responses based on the response code from the Private API
            if (resp.IsSuccessStatusCode)
                return Ok(resp.Content.ReadAsAsync<ImageReferenceResponse>());

<<<<<<< HEAD
            // Create an entry in the Cosmos DB Document Database
            var document = await CosmosContext.Instance.CreateDocumentAsync<UploadFileDocument>(new UploadFileDocument()
            {
                UserId = userId,
                FileName = upload.Name, // request.File.FileName,
                ContentType = request.File.ContentType,
                Reference = upload.Uri,
                Metadata = new List<MetaTag>()
                {
                    new MetaTag() { Key = "ImageTag", Type = typeof(string).ToString(), Value = imageTag },
                    new MetaTag() { Key = "Length", Type = typeof(long).ToString(), Value = request.File.Length }
                }
            }, UploadFileDocument.Partition);
            
            // Return a response to the Client
            return Ok(new ImageReferenceResponse()
            {
                ImageId = document.Id,
                ImageTag = imageTag,
                FileName = document.FileName,
                Metadata = document.Metadata,
                ImageReference = document.Reference.ToString()
            });
=======
            return BadRequest();
>>>>>>> master
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="imageTag"></param>
        /// <returns></returns>
        [HttpDelete("{userId}/tag/{imageTag}")]
        public async Task<object> DeleteUserImageTagSetAsync(string userId, string imageTag)// => throw new NotImplementedException();
        {
            // Instantiate the request
            var req = new HttpRequestMessage(HttpMethod.Delete,
                $"api/augmentedReality/{userId}/tag/{imageTag}")
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
        /// ben
        [HttpDelete("{userId}/tag/{imageTag}/image/{imageId}")]
        public async Task<ActionResult<ImageReferenceResponse>> DeleteUserImageAsync(string userId, string imageTag, string imageId)
        {
            // Instantiate the request
            var req = new HttpRequestMessage(HttpMethod.Delete,
                $"api/augmentedReality/{userId}/tag/{imageTag}/image/{imageId}")
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

        #endregion

        #region Augmented Reality - Models
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="imageTag"></param>
        /// <returns></returns>
        public async Task<object> GetTagAssociatedModelsAsync(string userId, string imageTag) 
        {
            // Instantiate the request
            var req = new HttpRequestMessage(HttpMethod.Get,
                $"api/augmentedReality/{userId}/tag/{imageTag}");

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
        /// <param name="index"></param>
        /// <returns></returns>
        /// ben
        [HttpGet("{userId}/tag/{imageTag}/index/{index}")]
        public async Task<object> GetTagAssociatedModelAsync(string userId, string imageTag, int index)
        {
            // Query for Upload File Document with ID <c>imageId</c>
            var document = await CosmosContext.Instance.GetDocumentAsync<UploadFileDocument>(UploadFileDocument.Partition, imageId);

            // TODO: Verification -- Does the User ID match what was sent? What happens if it doesn't? What about the Image Tag?
            //Document should contain parameters that match the variables -- access and compare through `document`
            //info may be in the metadata -- check postman
            if(document.imageTag != imageTag){
                //return response for bad parameters
                return BadRequest();
            }
            if(document.userId != userId){
                //return response for bad authetication
                return BadRequest();
            }
            if(document.index != index){
                return BadRequst();
            }
            return Ok(new ImageReferenceResponse()
            {
                ImageTag = imageTag,
                FileName = document.FileName,
                Metadata = document.Metadata,
                ImageReference = document.Reference.ToString()
            });

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="imageTag"></param>
        /// <param name="modelId"></param>
        /// <returns></returns>
        public async Task<object> GetTagAssociatedModelAsync(string userId, string imageTag, string modelId) 
        {
            // Instantiate the request
            var req = new HttpRequestMessage(HttpMethod.Get,
                $"api/augmentedReality/{userId}/tag/{imageTag}/{modelId}");

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
        /// <returns></returns>
        /// ben
        [HttpPost("{userId}/tag/{imageTag}")]
        public async Task<object> UploadTagAssociatedModelAsync(string userId, string imageTag,[FromForm]FileUploadRequest request)
        {
            // Get the User's image directory in Cloud File Storage
            var imgDir = await CloudFileContext.Instance.GetShareUserSubDirectoryAsync("testcompany", userId, "img");

            // Get the Image Tag specific directory from the image directory
            var tagDir = imgDir.GetDirectoryReference(imageTag);

            // Create the Image Tag specific directory if it does not exist
            await tagDir.CreateIfNotExistsAsync();

            // Get/Create a file reference
            var upload = tagDir.GetFileReference(request.File.FileName);

            //Check to see if File exists under the same name already.
            try
            {
                var fileExists = upload.Exists();
            }
            catch (Exception e)
            {
                System.Console.WriteLine(e);
                System.Console.WriteLine("File already exists under the specified name.");
                throw;
            }
            // Upload the image via Stream
            // Check to see if upload fails.
            try
            {
                await upload.UploadFromStreamAsync(request.File.OpenReadStream());
            }
            catch (Exception e)
            {
                System.Console.WriteLine(e);
                System.Console.WriteLine("File upload failed.");
                throw;
            }
            

            // Create an entry in the Cosmos DB Document Database
            var document = await CosmosContext.Instance.CreateDocumentAsync<UploadFileDocument>(new UploadFileDocument()
            {
                UserId = userId,
                FileName = upload.Name, // request.File.FileName,
                ContentType = request.File.ContentType,
                Reference = upload.Uri,
                Metadata = new List<MetaTag>()
                {
                    new MetaTag() { Key = "ImageTag", Type = typeof(string).ToString(), Value = imageTag },
                    new MetaTag() { Key = "Length", Type = typeof(long).ToString(), Value = request.File.Length }
                }
            }, UploadFileDocument.Partition);

            // Return a response to the Client
            return Ok(new ImageReferenceResponse()
            {
                ImageId = document.Id,
                ImageTag = imageTag,
                FileName = document.FileName,
                Metadata = document.Metadata,
                ImageReference = document.Reference.ToString()
            });



        }




        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="imageTag"></param>
        /// <returns></returns>
        public async Task<object> UpdateTagAssociatedModelAsync(string userId, string imageTag, [FromForm]FileUploadRequest request) 
        {
            // Read the File into a Byte[]
            byte[] data;
            using (var br = new BinaryReader(request.File.OpenReadStream()))
                data = br.ReadBytes((int)request.File.Length);

            // Instantiate the request
            var req = new HttpRequestMessage(HttpMethod.Put,
                $"api/augmentedReality/{userId}/tag/{imageTag}")
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
        public async Task<object> DeleteTagAssociatedModelAsync(string userId, string imageTag) 
        {
            // Instantiate the request
            var req = new HttpRequestMessage(HttpMethod.Delete,
                $"api/augmentedReality/{userId}/tag/{imageTag}")
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
        #endregion

        #region Augmented Reality - Vuforia
        [HttpGet("{userId}/tag/{imageTag}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(IList<ImageReferenceResponse>))]
        public async Task<ActionResult<IList<ImageReferenceResponse>>> GetUserImageTagSetAsyncVuforia(string userId, string imageTag, string imageId)
        {
             string accessKey = "[server access key]";

             string secretKey = "[server secret]";

             string targetId = "[target id]";

             string url = "https://vws.vuforia.com";

            HttpClient client = new DefaultHttpClient();

            Uri vuforiaUri = new Uri(url + "/targets/" + targetId);

            var req = new HttpRequestMessage(vuforiaUri);

            HttpWebRequest request = (HttpWebRequest) WebRequest.Create(vuforiaUri);

             request.Headers.Add(vuforiaUri);

             HttpWebResponse webResponse = (HttpWebResponse) request.GetResponse();

             if (webResponse.IsSuccessStatusCode)
             {
                 return Ok(webResponse.Content.ReadAsAsync<ImageReferenceResponse>());
             }
             else
             {
                 return BadRequest(resp.Content.ReadAsAsync<IntelligentMixedRealityError>());
             }
             

        }
        #endregion
    }
}