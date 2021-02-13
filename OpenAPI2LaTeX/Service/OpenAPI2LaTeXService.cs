using Microsoft.OpenApi.Models;
using Microsoft.OpenApi.Readers;
using Microsoft.OpenApi.Writers;
using OpenAPI2LaTeX.Model;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace OpenAPI2LaTeX.Service
{
	public class OpenAPI2LaTeXService
	{

		public static RestApi parseToRestApi(string text)
		{
			RestApi api = new RestApi();
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

			return api;
		}


		public static string generateLaTeX(RestApi api)
		{
			StringBuilder builder = new StringBuilder();

			builder.Append("\\usepackage{rest-api}\n\n");

			foreach (RestApiPath path in api.Paths)
			{
				string urlPath = path.Path;
				urlPath = urlPath.Replace("{", "\\{").Replace("}", "\\}");

				builder.Append("\\begin{apiRoute}");
				builder.Append("{" + path.Method + "}");
				builder.Append("{" + urlPath + "}");
				builder.Append("{" + path.Description + "}\n");

				builder.Append("	\\begin{routeParameter}\n");
				if (path.Parameters.Count > 0)
				{
					foreach (RestApiParameter param in path.Parameters)
					{
						builder.Append("		\\routeParamItem");
						builder.Append("{" + param.Id + "}{" + param.Description + "}\n");
					}
				}
				else
				{
					builder.Append("		\\noRouteParameter{no parameter}\n");
				}
				builder.Append("	\\end{routeParameter}\n");

				if (path.Bodys.Count > 0)
				{
					builder.Append("	\\begin{routeRequest}");
					builder.Append("{" + path.Bodys[0].ContentType + "}\n");
					foreach(RestApiBody body in path.Bodys)
					{
						builder.Append("		\\begin{routeRequestBody}\n");
						builder.Append(body.Example + "\n");
						builder.Append("		\\end{routeRequestBody}\n");
					}
					builder.Append("	\\end{routeRequest}\n");
				}

				builder.Append("	\\begin{routeResponse}");
				if (path.Responses.Count > 0)
				{
					builder.Append("{" + path.Responses[0].ContentType + "}\n");
					foreach (RestApiResponse response in path.Responses)
					{
						builder.Append("		\\begin{routeResponseItem}");
						builder.Append("{" + response.Status + "}");
						builder.Append("{" + response.Description + "}\n");
						builder.Append("			\\begin{routeResponseItemBody}\n");
						builder.Append(response.Example + "\n");
						builder.Append("			\\end{routeResponseItemBody}\n");
						builder.Append("		\\end{routeResponseItem}\n");
					}
				}
				else
				{
					builder.Append("{}\n");
					builder.Append("		\\noRouteResponse{no response}\n");
				}
				builder.Append("	\\end{routeResponse}\n");

				builder.Append("\\end{apiRoute}\n\n");
			}
			return builder.ToString();
		}



	}
}
