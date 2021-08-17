using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(BulgarianProducers.Areas.Identity.IdentityHostingStartup))]
namespace BulgarianProducers.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
            });
        }
    }
}