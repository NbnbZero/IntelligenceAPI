using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Intelligent.API.Framework
{
    /// <summary>Base class for all API requests.</summary>
    public abstract class BaseRequest
    {
        /// <summary>Initializes a new instance of the <see cref="BaseRequest"/> class.</summary>
        protected BaseRequest()
        {
            RequestId = Guid.NewGuid();
        }

        /// <summary>Gets or sets the request identifier.</summary>
        /// <value>The request identifier.</value>
        public Guid RequestId { get; set; }
    }
}
