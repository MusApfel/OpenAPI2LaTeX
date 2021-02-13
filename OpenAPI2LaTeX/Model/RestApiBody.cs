using System;
using System.Collections.Generic;
using System.Text;

namespace OpenAPI2LaTeX.Model
{
    public class RestApiBody
    {
        private string _contentType;
        private string _example;

        public string ContentType { get => _contentType; set => _contentType = value; }
        public string Example { get => _example; set => _example = value; }
    }
}
