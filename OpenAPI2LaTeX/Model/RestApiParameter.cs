using System;
using System.Collections.Generic;
using System.Text;

namespace OpenAPI2LaTeX.Model
{
    public class RestApiParameter
    {
        private string _id;
        private string _description;

        public string Id { get => _id; set => _id = value; }
        public string Description { get => _description; set => _description = value; }
    }
}
