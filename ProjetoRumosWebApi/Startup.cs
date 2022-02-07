using DAL.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using ProjetoRumosWebApi.Data;
using ProjetoRumosWebApi.Services.CategoryService;
using ProjetoRumosWebApi.Services.CommentService;
using ProjetoRumosWebApi.Services.DifficultyService;
using ProjetoRumosWebApi.Services.IngredientService;
using ProjetoRumosWebApi.Services.RecipeCommentService;
using ProjetoRumosWebApi.Services.RecipeService;
using ProjetoRumosWebApi.Services.UserService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoRumosWebApi
{
    public class Startup
    {
        //field para o Cors, para colucar a api a ser consumida por Angular
        readonly string CorsConfiguration = "_corsConfig";
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //Adicionar a configuração do Context tmb na api
            services.AddDbContext<ProjetoRumosContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddControllers();
           
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ProjetoRumosWebApi", Version = "v1" });

                //Configuração do Swagger para usar o atributo [Authorize] 
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 1safsfsdfdfd\"",
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] {}
                    }
                });
            });
            services.AddAutoMapper(typeof(Startup));
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IIngredientService, IngredientService>();
            services.AddScoped<IRecipeService, RecipeService>();
            services.AddScoped<IDifficultyService, DifficultyService>();
            services.AddScoped<IAuthRepo, AuthRepo>();
            services.AddScoped<IRecipeCommentService, RecipeCommentService>();
            services.AddScoped<IUserService, UserService>();

            //Adicionar Authentication Scheme ao WebService para o atributo [Authorize] 
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    //Inicializar nova instancia de validação de parametros do token
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration.GetSection("AppSettings:Token").Value)),
                        ValidateIssuer = false,
                        ValidateAudience = false,
                    };
                });

            //Preparar a api para ser consumida por Angular
            services.AddCors(options =>
            {
                options.AddPolicy(name: CorsConfiguration, builder =>
               {
                   builder.WithOrigins("http://localhost:4200");
               });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //if (env.IsDevelopment())
            //{
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ProjetoRumosWebApi v1"));
            //}

            app.UseHttpsRedirection();

            app.UseRouting();

            //Adicionar .netCore authentication Middleware
            app.UseAuthentication();

            ////Adicionar Cors Middleware
            app.UseCors(x => x
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());

            app.UseHttpsRedirection();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            //chama o metodo para fazer a migração e bd
            UpgradeDatabase(app);
        }
        //Método para fazer a migração e bd automáticamente
        private void UpgradeDatabase(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<ProjetoRumosContext>();
                if (context != null && context.Database != null)
                {
                    context.Database.Migrate();
                    context.SaveChanges();
                }
            }
        }
    }
}
