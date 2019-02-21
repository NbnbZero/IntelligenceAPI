using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Intelligent.API.Framework
{
    public abstract class BaseResponse
    {
        /// <summary>Initializes a new instance of the <see cref="BaseResponse"/> class.</summary>
        protected BaseResponse() : this(null)
        {
        }

        /// <summary>Initializes a new instance of the <see cref="BaseResponse"/> class.</summary>
        /// <param name="request">The request that this instance is in response to.</param>
        protected BaseResponse(BaseRequest request)
        {
            RequestId = request?.RequestId ?? Guid.NewGuid();
        }

        /// <summary>Gets or sets the request identifier.</summary>
        /// <value>The request identifier.</value>
        public Guid RequestId { get; set; }
    }
}
