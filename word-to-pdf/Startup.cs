using System;
using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace word_to_pdf
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            // string licenseString = Configuration["ASPOSE_LICENSE"];
            // byte[] licenseBytes = Convert.FromBase64String(licenseString);
            // Aspose.Words.License license = new Aspose.Words.License();
            // try
            // {
            //     // Initializes a license from a stream 
            //     MemoryStream stream = new MemoryStream(licenseBytes);
            //     license.SetLicense(stream);
            //     Console.WriteLine("License set successfully.");
            // }
            // catch (Exception e)
            // {
            //     // We do not ship any license with this example, visit the Aspose site to obtain either a temporary or permanent license. 
            //     Console.WriteLine("\nThere was an error setting the license: " + e.Message);
            // }
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
