using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Intelligent.API.Framework
{
    /// <summary></summary>
    public class ApiError
    {
        /// <summary>Initializes a new instance of the <see cref="ApiError"/> class.</summary>
        /// <param name="message">The message.</param>
        public ApiError(string message)
        {
            ExceptionMessage = message;
            IsError = true;
        }

        /// <summary>Initializes a new instance of the <see cref="ApiError"/> class.</summary>
        /// <param name="modelState">State of the model.</param>
        public ApiError(ModelStateDictionary modelState)
        {
            IsError = true;
            if (modelState != null && modelState.Any(m => m.Value.Errors.Count > 0))
            {
                ExceptionMessage = "Please correct the specified validation errors and try again.";
                ValidationErrors = modelState.Keys
                .SelectMany(key => modelState[key].Errors.Select(x => new ValidationError(key, x.ErrorMessage)))
                .ToList();
            }
        }

        /// <summary>Gets or sets the details.</summary>
        /// <value>The details.</value>
        public string Details { get; set; }

        /// <summary>Gets or sets the exception message.</summary>
        /// <value>The exception message.</value>
        public string ExceptionMessage { get; set; }

        /// <summary>Gets or sets a value indicating whether this instance is error.</summary>
        /// <value><c>true</c> if this instance is error; otherwise, <c>false</c>.</value>
        public bool IsError { get; set; }

        /// <summary>Gets or sets the reference document link.</summary>
        /// <value>The reference document link.</value>
        public string ReferenceDocumentLink { get; set; }

        /// <summary>Gets or sets the reference error code.</summary>
        /// <value>The reference error code.</value>
        public string ReferenceErrorCode { get; set; }

        /// <summary>Gets or sets the validation errors.</summary>
        /// <value>The validation errors.</value>
        public IEnumerable<ValidationError> ValidationErrors { get; set; }
    }
}
