using Newtonsoft.Json;

namespace Intelligent.API.Framework
{
    /// <summary></summary>
    public class ValidationError
    {
        /// <summary>Initializes a new instance of the <see cref="ValidationError"/> class.</summary>
        /// <param name="key">The field.</param>
        /// <param name="message">The message.</param>
        public ValidationError(string key, string message)
        {
            Key = key != string.Empty ? key : null;
            Message = message;
        }

        /// <summary>Gets the field.</summary>
        /// <value>The field.</value>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Key { get; }

        /// <summary>Gets the message.</summary>
        /// <value>The message.</value>
        public string Message { get; }
    }
}
