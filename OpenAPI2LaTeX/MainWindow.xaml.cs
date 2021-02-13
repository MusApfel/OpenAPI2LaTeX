using Microsoft.OpenApi;
using Microsoft.OpenApi.Extensions;
using Microsoft.OpenApi.Models;
using Microsoft.OpenApi.Readers;
using Microsoft.OpenApi.Writers;
using OpenAPI2LaTeX.Model;
using OpenAPI2LaTeX.ViewModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace OpenAPI2LaTeX
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        //RestApi api = new RestApi();

        public MainWindow()
        {
            InitializeComponent();

            this.DataContext = this.DataContext = new MainWindowViewModel();
        }


        /*
        private void Window_Loaded_1(object sender, RoutedEventArgs e)
        {
            string path = "./swagger-v2.yaml";
            string text = System.IO.File.ReadAllText(path);

            OpenApiStringReader reader = new OpenApiStringReader();
            var document = reader.Read(text, out var diagnostic);





            api.Title = document.Info.Title;
            api.Version = document.Info.Version;
            api.Description = document.Info.Description;

            if (document.Servers.Count > 0)
            {
                api.ServerUrl = document.Servers[0].Url;
            }

            foreach (KeyValuePair<string, OpenApiPathItem> pairPath in document.Paths)
            {
                foreach (KeyValuePair<OperationType, OpenApiOperation> pairOperation in pairPath.Value.Operations)
                {
                    RestApiPath apiPath = new RestApiPath();
                    apiPath.Path = pairPath.Key;
                    apiPath.Method = pairOperation.Key.ToString();
                    apiPath.Description = pairOperation.Value.Description;

                    foreach (OpenApiParameter para in pairOperation.Value.Parameters)
                    {
                        RestApiParameter apiParameter = new RestApiParameter();

                        apiParameter.Id = para.Name;
                        apiParameter.Description = para.Description;

                        apiPath.Parameters.Add(apiParameter);
                    }

                    if (pairOperation.Value.RequestBody != null && pairOperation.Value.RequestBody.Content.Count > 0)
                    {
                        RestApiBody apiBody = new RestApiBody();

                        foreach (KeyValuePair<string, OpenApiMediaType> bodyContentPair in pairOperation.Value.RequestBody.Content)
                        {
                            apiBody.ContentType = bodyContentPair.Key;

                            var stream = new MemoryStream();
                            IOpenApiWriter writer = new OpenApiJsonWriter(new StreamWriter(stream));

                            if (bodyContentPair.Value.Example != null)
                            {
                                writer.WriteAny(bodyContentPair.Value.Example);
                            }
                            else
                            {
                                bodyContentPair.Value.Schema.SerializeAsV2WithoutReference(writer);
                            }

                            writer.Flush();
                            stream.Position = 0;

                            var value = new StreamReader(stream).ReadToEnd();

                            apiBody.Example = value;

                            break;
                        }

                        apiPath.Bodys.Add(apiBody);
                    }


                    foreach (KeyValuePair<string, OpenApiResponse> respPair in pairOperation.Value.Responses)
                    {
                        RestApiResponse apiResponse = new RestApiResponse();
                        apiResponse.Status = respPair.Key;


                        if (respPair.Value.Content.Count > 0)
                        {
                            foreach (KeyValuePair<string, OpenApiMediaType> respBodyPair in respPair.Value.Content)
                            {
                                apiResponse.ContentType = respBodyPair.Key;

                                var stream = new MemoryStream();
                                IOpenApiWriter writer = new OpenApiJsonWriter(new StreamWriter(stream));

                                if (respBodyPair.Value.Example != null)
                                {
                                    writer.WriteAny(respBodyPair.Value.Example);
                                }
                                else if (respBodyPair.Value.Schema != null)
                                {
                                    respBodyPair.Value.Schema.SerializeAsV2WithoutReference(writer);
                                }

                                writer.Flush();
                                stream.Position = 0;

                                var value = new StreamReader(stream).ReadToEnd();

                                apiResponse.Example = value;

                                break;
                            }

                        }

                        apiPath.Responses.Add(apiResponse);

                    }


                    api.Paths.Add(apiPath);
                }
            }
        }
        */
    }
}
