using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PathMappingExp {
    public class Program {
        public static void Main(string[] args) {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((hostingContext, config) => {
                    // カスタム構成プロバイダを構築し、デフォルト設定に加えて
                    // appsettings.{env}.{ASPNETCORE_APPL_PATH}.json を設定パスに加える
                    //  https://docs.microsoft.com/ja-jp/azure/azure-sql/database/metrics-diagnostic-telemetry-logging-streaming-export-configure?tabs=azure-cli#configure-the-streaming-export-of-diagnostic-telemetry[診断テレメトリのストリーミング エクスポートを構成する]

                    // ASPNETCORE_APPL_PATH を読む為、一時的な Config をビルド
                    var _config = config.Build();

                    var env = hostingContext.HostingEnvironment;
                    config.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                          .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true)
                          .AddJsonFile($"appsettings.{env.EnvironmentName}.{_config["ASPNETCORE_APPL_PATH"].Replace("/", "")}.json",
                            optional: true, reloadOnChange: true)
                          .AddEnvironmentVariables();
                })
                .ConfigureWebHostDefaults(webBuilder => {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
