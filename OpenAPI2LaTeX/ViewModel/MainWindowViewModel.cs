using OpenAPI2LaTeX.Model;
using OpenAPI2LaTeX.Service;
using System.IO;
using System.Windows.Input;

namespace OpenAPI2LaTeX.ViewModel
{
    public class MainWindowViewModel : ViewModelBase
    {



        private ICommand _loadOpenAPICommand;

        private string _laTeXSource;






        public ICommand LoadOpenAPICommand
        {
            get => _loadOpenAPICommand;
            set
            {
                _loadOpenAPICommand = value;
                OnPropertyChanged();
            }
        }

        public string LaTeXSource
        {
            get => _laTeXSource; 
            set 
            { 
                _laTeXSource = value;
                OnPropertyChanged();
            }
        }



        public MainWindowViewModel()
        {
            LoadOpenAPICommand = new ActionCommand(LoadOpenAPIFile);
        }



        private void LoadOpenAPIFile(object obj)
        {
            FileDialogViewModel fdvm = new FileDialogViewModel();
            fdvm.Extension = "*.json, *.yaml";
            fdvm.Filter = "Json files (*.json)|*.json|YAML files (*.yaml)|*.yaml";

            fdvm.OpenCommand.Execute(null);

            TextReader textReader = new StreamReader(fdvm.Stream);
            string text = textReader.ReadToEnd();

            RestApi api = OpenAPI2LaTeXService.parseToRestApi(text);

            LaTeXSource = OpenAPI2LaTeXService.generateLaTeX(api);
        }


    }
}
