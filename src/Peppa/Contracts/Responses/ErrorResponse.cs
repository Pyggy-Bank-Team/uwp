using System.Collections.Generic;

namespace Peppa.Contracts.Responses
{
    public class ErrorResponse
    {
        public string Type { get; set; }
        public IEnumerable<string> Errors { get; set; }
    }
}