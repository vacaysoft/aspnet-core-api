using Elasticsearch.Net;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Formatting.Elasticsearch;
using Serilog.Sinks.Elasticsearch;
using System;

namespace VacaySoft.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseSerilog((context, loggerConfiguration) =>
                {
                    var indexFormat =
                        $"{context.Configuration["ApplicationName"]}-" +
                        $"logs-{context.HostingEnvironment.EnvironmentName?.ToLower().Replace(".", "-")}-" +
                        $"{DateTime.UtcNow:yyyy-MM}";
                    var elasticSearchUri = new Uri(context.Configuration["ElasticConfiguration:Uri"]);
                    var elasticSearchUsername = context.Configuration["ElasticConfiguration:Username"];
                    var elasticSearchPassword = context.Configuration["ElasticConfiguration:Password"];
                    loggerConfiguration
                        .Enrich.FromLogContext()
                        .Enrich.WithMachineName()
                        .WriteTo.Console()
                        .WriteTo.File($"logs/logs-{DateTime.Now:yyyy-MM-dd}.log")
                        .WriteTo.Elasticsearch(
                            new ElasticsearchSinkOptions(elasticSearchUri)
                            {
                                IndexFormat = indexFormat,
                                AutoRegisterTemplate = true,
                                AutoRegisterTemplateVersion = AutoRegisterTemplateVersion.ESv7,
                                CustomFormatter = new ExceptionAsObjectJsonFormatter(renderMessage: true),
                                Serializer = new LowLevelRequestResponseSerializer(),
                                FailureCallback = e => Console.WriteLine("Unable to submit event " + e.MessageTemplate),
                                EmitEventFailure = EmitEventFailureHandling.WriteToSelfLog |
                                                   EmitEventFailureHandling.WriteToFailureSink |
                                                   EmitEventFailureHandling.RaiseCallback,
                                ModifyConnectionSettings = connectionConfiguration => connectionConfiguration.BasicAuthentication(elasticSearchUsername, elasticSearchPassword)
                            }).WriteTo.Console(new ElasticsearchJsonFormatter())
                        .Enrich.WithProperty("Environment", context.HostingEnvironment.EnvironmentName)
                        .ReadFrom.Configuration(context.Configuration);
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
