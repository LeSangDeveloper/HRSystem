using HRSystem.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace HRSystem
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {    // 註冊模擬資料庫服務
            services.AddSingleton<Data>();

            // 註冊模擬Data Context服務
            services.AddScoped<DataContext>();
            services.AddRazorPages(); // 註冊Razor Page Servcies
            services.AddDistributedMemoryCache(); // 必要的後端儲機制
            services.AddSession(opts => {
                opts.Cookie.IsEssential = true; // 使Session Cookie無法被客戶端腳本訪問
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
                // 當處理中有Exception , 註冊網頁呈現錯誤
				app.UseExceptionHandler("/Errors/500");
			}

            // 註冊網頁呈現404錯誤
            app.UseStatusCodePagesWithReExecute("/Errors/{0}");

            // 允許使用wwwroot檔案
			app.UseStaticFiles();

            // 用Session
            app.UseSession();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages(); // 映射路徑跟Razor pages
            });
        }
    }
}
