using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using notes_api.Models;
using notes_api.Services;

namespace notes_api
{
    public class Startup
    {
        readonly string MyAllowSpecificOrigins = "_reactClient";

        public Startup(IConfiguration configuration){
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services){

            services.Configure<NotesDatabaseSettings>(
                Configuration.GetSection(nameof(NotesDatabaseSettings)));

            services.AddSingleton<INotesDatabaseSettings>(sp => 
                sp.GetRequiredService<IOptions<NotesDatabaseSettings>>().Value);

            services.AddSingleton<NoteService>();

            services.AddCors(options => {
                options.AddPolicy(name: MyAllowSpecificOrigins,
                    builder => {
                        builder.WithOrigins("http://192.168.0.19:3000").AllowAnyHeader().AllowAnyMethod().AllowCredentials();
                    });
            });

            services.AddControllers();

            services.AddMvc(options => {
                options.RespectBrowserAcceptHeader = true;
            });

            services.AddSwaggerGen(c => {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Notes API", Version = "v1" });
                c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
            });
            }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env){

            if (env.IsDevelopment()){
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Notes API v1");
                });
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(MyAllowSpecificOrigins);

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>{
                //endpoints.MapControllers();
                endpoints.MapDefaultControllerRoute();
            });


        }
    }
}
