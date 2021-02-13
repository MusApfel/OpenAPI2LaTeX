using System;
using System.Collections.Generic;
using System.Text;

namespace OpenAPI2LaTeX.Model
{
    public class RestApiPath
    {
        private string _method;
        private string _path;
        private string _description;
        private List<RestApiParameter> _parameters;
        private List<RestApiBody> _bodys;
        private List<RestApiResponse> _responses;

        public RestApiPath()
        {
            _parameters = new List<RestApiParameter>();
            _bodys = new List<RestApiBody>();
            _responses = new List<RestApiResponse>();
        }

        public string Method { get => _method; set => _method = value; }
        public string Path { get => _path; set => _path = value; }
        public string Description { get => _description; set => _description = value; }
        public List<RestApiParameter> Parameters { get => _parameters; set => _parameters = value; }
        public List<RestApiBody> Bodys { get => _bodys; set => _bodys = value; }
        public List<RestApiResponse> Responses { get => _responses; set => _responses = value; }
    }
}
