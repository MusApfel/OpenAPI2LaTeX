using System;
using System.Collections.Generic;
using System.Text;

namespace OpenAPI2LaTeX.Model
{
    public class RestApiResponse
    {
        private string _status;
        private string _contentType;
        private string _description;
        private string _example;

        public string Status { get => _status; set => _status = value; }
        public string ContentType { get => _contentType; set => _contentType = value; }
        public string Description { get => _description; set => _description = value; }
        public string Example { get => _example; set => _example = value; }
    }
}
