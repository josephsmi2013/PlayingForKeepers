using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(PlayingForKeepers.Areas.Identity.IdentityHostingStartup))]
namespace PlayingForKeepers.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) =>
            {
            });
        }
    }
}