using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Intelligent.API.Framework;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Intelligent.API.Controllers
{
    [ApiController]
    [Authorize]  // TODO: Uncomment when ready for authorization
    [Produces(MimeTypes.Application.Json, MimeTypes.Application.Xml)]
    public class IntelligentMixedRealityController : ControllerBase
    {
        /// <summary>The access token name as it will be passed in the request headers.</summary>
        protected const string AccessTokenName = "Authorization";

        /// <summary>The HTTP Header, <c>Content-Type</c>.</summary>
        protected const string ContentTypeHeader = "Content-Type";

        /// <summary>The HTTP Header, <c>X-Forwarded-For</c>.</summary>
        protected const string ForwardedForHeader = "X-Forwarded-For";

        /// <summary>The logger instance used to log any messages from this controller.</summary>
        protected readonly ILogger Logger;

        /// <summary>Initializes a new instance of the <see cref="IntelligentMixedRealityController"/> class.</summary>
        /// <param name="logger">The logger instance used to log any messages from this controller.</param>
        public IntelligentMixedRealityController(ILogger logger)
        {
            Logger = logger;
        }
    }
}