using Microsoft.Win32;
using System.IO;

namespace OpenAPI2LaTeX.Service
{
    public sealed class FileService
    {
        public Stream OpenFile(string defaultExtension, string filter)
        {
            OpenFileDialog fd = new OpenFileDialog();
            fd.DefaultExt = defaultExtension;
            fd.Filter = filter;

            bool? result = fd.ShowDialog();

            return result.Value ? fd.OpenFile() : null;
        }
    }
}
