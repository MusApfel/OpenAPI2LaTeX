using System.Collections.Generic;

namespace OpenAPI2LaTeX.Model
{
    public class RestApi
    {
        private string _title;
        private string _version;
        private string _Description;
        private string _serverUrl;
        private List<RestApiPath> _paths;

        public RestApi()
        {
            _paths = new List<RestApiPath>();
        }

        public string Title { get => _title; set => _title = value; }
        public string Version { get => _version; set => _version = value; }
        public string Description { get => _Description; set => _Description = value; }
        public string ServerUrl { get => _serverUrl; set => _serverUrl = value; }
        public List<RestApiPath> Paths { get => _paths; }
    }
}
