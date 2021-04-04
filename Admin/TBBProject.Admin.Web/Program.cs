using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using TBBProject.Core.Web;

namespace TBBProject.Admin.Web
{
    public class Program
    {
        public static void Main(string[] args) => CreateWebHostBuilder(args).Build().Run();

        public static IWebHostBuilder CreateWebHostBuilder(string[] args)
        {
            var host = WebHost.CreateDefaultBuilder(args)
                .AddTBBProjectMvc()
                .UseStartup<Startup>()
                ;

            return host;
        }  
    }
       
}
