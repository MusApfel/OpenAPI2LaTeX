using OpenAPI2LaTeX.Service;
using System.IO;
using System.Windows.Input;

namespace OpenAPI2LaTeX.ViewModel
{
    public class FileDialogViewModel : ViewModelBase
    {
        public FileDialogViewModel()
        {
            this.OpenCommand = new ActionCommand(this.OpenFile);
        }
        #region Properties

        public Stream Stream
        {
            get;
            set;
        }

        public string Extension
        {
            get;
            set;
        }

        public string Filter
        {
            get;
            set;
        }
        public ICommand OpenCommand
        {
            get;
            set;
        }


        #endregion

        private void OpenFile(object obj)
        {
            FileService fileServices = new FileService();
            this.Stream = fileServices.OpenFile(this.Extension, this.Filter);
        }
    }
}
