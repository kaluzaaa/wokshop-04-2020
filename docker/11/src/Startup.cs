using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Net.Http;
using StackExchange.Redis;

namespace test
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Hello World!");
                });
                endpoints.MapGet("/api/test", async context =>
                {
                    HttpClient client = new HttpClient();
                    string responseBody = null;
                    string url = context.Request.Query["url"].ToString();
                    try
                    {
                        client.DefaultRequestHeaders.Add("Accept", "application/json");
                        HttpResponseMessage response = await client.GetAsync(url);
                        response.EnsureSuccessStatusCode();
                        responseBody = await response.Content.ReadAsStringAsync();
                    }
                    catch (HttpRequestException e)
                    {
                        responseBody = String.Format("Error :{0} ", e.Message);
                    }
                    await context.Response.WriteAsync(responseBody);
                });
                endpoints.MapGet("/api/redis", async context =>
                    {
                        HttpClient client = new HttpClient();
                        string responseBody = null;

                        try
                        {
                            string redis = context.Request.Query["redis"].ToString();
                            if (string.IsNullOrEmpty(redis))
                            {
                                redis = Environment.GetEnvironmentVariable("redis");
                            }
                            ConnectionMultiplexer redisConnection = ConnectionMultiplexer.Connect(
                                new ConfigurationOptions
                                {
                                    EndPoints = { string.Format("{0}:6379", redis) }
                                });
                            var db = redisConnection.GetDatabase();
                            var pong = await db.PingAsync();
                            responseBody = pong.ToString();
                        }
                        catch (Exception e)
                        {
                            responseBody = String.Format("Error :{0} ", e.Message);
                        }
                        await context.Response.WriteAsync(responseBody);
                    });

            });
        }
    }
}
