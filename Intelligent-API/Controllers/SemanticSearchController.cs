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

namespace Intelligent.API.Controllers
{
    /// <summary>
    /// API Controller for all things having to do with Semantic Search.
    /// </summary>
    /// <see cref="IntelligentMixedRealityController"/>
//    [ApiVersion("1.0")]                                   // TODO: Un-Comment after applying settings for API versioning
    [AllowAnonymous]                                        // TODO: Remove for Authentication
                                                            //    [Route("api/v{version:apiVersion}/augmentedReality")] // TODO: The versioned API Route
    [Route("api/semanticSearch")]                         // TODO: Remove after applying settings for API versioning
    public class SemanticSearchController : IntelligentMixedRealityController
    {
        /// <summary>Initializes a new instance of the <see cref="SemanticSearchController"/> class.</summary>
        /// <param name="logger">The logger instance used to log any messages from this controller.</param>
        public SemanticSearchController(ILogger<SemanticSearchController> logger) : base(logger)
        {
        }

        #region Semantic Search - Document
        /// <summary>
        /// 
        /// </summary>
        /// <param name="documents">The requested documents list.</param>
        /// <param name="documentId">The document's Id.</param>
        /// <returns></returns>
        [HttpGet("documents/{documentId}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(DocumentReferenceResponse))]
        public async Task<ActionResult<DocumentReferenceResponse>> GetUserImageAsync(string documents, string documentId)
        {
            // Query for Search File Document with ID <c>documentId</c>
            var document = await CosmosContext.Instance.GetDocumentAsync<UploadFileDocument>(UploadFileDocument.Partition, documentId);

            // TODO: Verification -- Does the User ID match what was sent? What happens if it doesn't? What about the Image Tag?
            return Ok(new DocumentReferenceResponse()
            {
                DocumentId = document.Id,
                Metadata = document.Metadata,
                DocumentReference = document.Reference.ToString()
            });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="documents">The requested documents list.</param>
        /// <param name="documentId">The document's Id.</param>
        /// <returns></returns>
        [HttpPost("documents/{documentId}")]
        [Consumes(MimeTypes.Misc.FormData)]
        public async Task<ActionResult<DocumentReferenceResponse>> UploadUserImageAsync(string document, string documentId, [FromForm]FileUploadRequest request)
        {
            // Get the document directory in Cloud File Storage
            var docDir = await CloudFileContext.Instance.GetShareUserSubDirectoryAsync("testcompany", documentId, "doc");

            // Create the Image Tag specific directory if it does not exist
            await docDir.CreateIfNotExistsAsync();

            // Get/Create a file reference
            var upload = docDir.GetFileReference(request.File.FileName);

            // TODO: What happens if a file already exists under the same name?
            await upload.DeleteIfExistsAsync();

            // Upload the image via Stream
            await upload.UploadFromStreamAsync(request.File.OpenReadStream());

            // TODO: What happens if the upload fails?

            // Create an entry in the Cosmos DB Document Database
            var doc = await CosmosContext.Instance.CreateDocumentAsync<UploadFileDocument>(new UploadFileDocument()
            {
                DocumentId = documentId,
                FileName = upload.Name, // request.File.FileName,
                ContentType = request.File.ContentType,
                Reference = upload.Uri,
                Metadata = new List<MetaTag>()
                {
                    new MetaTag() { Key = "Length", Type = typeof(long).ToString(), Value = request.File.Length }
                }
            }, UploadFileDocument.Partition);

            // Return a response to the Client
            return Ok(new DocumentReferenceResponse()
            {
                DocumentId = doc.Id,
                Metadata = doc.Metadata,
                DocumentReference = doc.Reference.ToString()
            });
        }
        #endregion

        #region Semantic Search - Vuforia
        // TODO: Here is where the Vuforia routes should go
        #endregion
    }
}