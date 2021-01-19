using HC11.GraphQL;
using HotChocolate.Execution.Options;
using HotChocolate.Types;
using HotChocolate.Types.Pagination;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HC11
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "HC11", Version = "v1" });
            });
            services.AddGraphQLServer()
                .AddQueryType<Query>()
                .BindRuntimeType<DateTime, DateTimeType>()
                .BindRuntimeType<int, IntType>()
                .BindRuntimeType<string, StringType>()
                .BindRuntimeType<bool, BooleanType>()
                .BindRuntimeType<long, LongType>()
                .AddProjections()
                .SetPagingOptions(
                new PagingOptions()
                {
                    MaxPageSize = 100,
                    DefaultPageSize = 30,
                    IncludeTotalCount = true
                }
                )
                .EnableRelaySupport()
                .ModifyRequestOptions(x => x.TracingPreference = TracingPreference.OnDemand);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "HC11 v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            app.UseWebSockets();
            app.UseEndpoints(x => x.MapGraphQL());
        }
    }
}