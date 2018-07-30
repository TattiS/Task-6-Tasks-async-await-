using AirportService;
using AirportService.Services;
using DALProject;
using DALProject.Interefaces;
using DALProject.UnitOfWork;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Cors;

namespace Task4WebApp
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

			services.AddDbContext<MainDBContext>();
			services.AddScoped<AsyncUnitOfWork>()
				.AddScoped<CrewService>()
				.AddScoped<DepartureService>()
				.AddScoped<FlightService>()
				.AddScoped<PilotService>()
				.AddScoped<PlaneService>()
				.AddScoped<PlaneTypeService>()
				.AddScoped<StewardessService>()
				.AddScoped<TicketService>()
				.AddCors();
				

			services.AddMvc();
			


		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
			
			app.UseCors(builder => builder.WithOrigins("http://localhost:4200").AllowCredentials().AllowAnyHeader().AllowAnyMethod());
			using (var scope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
			{
				var context = scope.ServiceProvider.GetService<MainDBContext>();
				//context.EnsureDatabaseSeeded();
			}

				app.UseMvc();
        }
    }
}
