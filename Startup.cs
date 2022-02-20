using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using mvpApi.Services;
using mvpApi.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mvpApi
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

            #region Interfaces_mapping
            //services.AddTransient<IProdutosService, ProdutosService>();
            //services.AddTransient<ICommonService, CommonService>();
            //services.AddTransient<IPedidoService, PedidoService>();
            //services.AddTransient<IValidacoesService, ValidacoesService>();
            //services.AddTransient<IEmailService, EmailService>();
            //services.AddTransient<IUtilsService, UtilsService>();
            services.AddTransient<IUsuarioService, UsuarioService>();

            //services.AddScoped<IPagamentoRepository, PagamentoRepository>();
            //services.AddScoped<IClienteRepository, ClienteRepository>();
            //services.AddScoped<IEstadoCivilRepository, EstadoCivilRepository>();
            //services.AddScoped<IEnderecoRepository, EnderecoRepository>();
            //services.AddScoped<IPedidoRepository, PedidoRepository>();
            //services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            #endregion Interfaces_mapping       

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
