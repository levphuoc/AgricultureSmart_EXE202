using AgricultureSmart.Repositories.DbAgriContext;
using AgricultureSmart.Repositories.Repositories.Interfaces;
using AgricultureSmart.Repositories.Repositories;
using AgricultureSmart.Services.Interfaces;
using AgricultureSmart.Services.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using System.Security.Claims;

namespace AgricultureSmart.API
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            // Add DbContext
            services.AddDbContext<AgricultureSmartDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            // Add JWT Authentication
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = Configuration["JWT:ValidIssuer"],
                    ValidAudience = Configuration["JWT:ValidAudience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWT:Secret"])),
                    ClockSkew = TimeSpan.Zero,
                    RoleClaimType = "http://schemas.microsoft.com/ws/2008/06/identity/claims/role"

                };

                // Configure JWT Bearer to read token from cookie
                options.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        if (context.Request.Cookies.ContainsKey("accessToken"))
                        {
                            context.Token = context.Request.Cookies["accessToken"];
                        }
                        return Task.CompletedTask;
                    }
                };
            });

            // Add controllers
            services.AddControllers();

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            // Configure CORS if needed
            /*services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", builder =>
                {
                    builder.WithOrigins("http://localhost:3000", "https://agriculture-smart-fe.vercel.app")
                           .AllowAnyMethod()
                           .AllowAnyHeader()
                           .AllowCredentials();
                });
            });*/

            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", builder =>
                {
                    builder.AllowAnyOrigin()
                           .AllowAnyMethod()
                           .AllowAnyHeader()
                           .AllowCredentials();
                });
            });

            // Add Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Agriculture Smart API", Version = "v1" });

                // Add JWT Authentication to Swagger
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Enter 'Bearer' [space] and then your valid token in the text input below.\r\n\r\nExample: \"Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9\""
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

            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            // Register Repositories
            services.AddScoped<IProductCategoryRepository, ProductCategoryRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<ICartRepository, CartRepository>();
            services.AddScoped<ICartItemRepository, CartItemRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IOrderItemRepository, OrderItemRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<INewsRepository, NewsRepository>();
            services.AddScoped<INewsCategoryRepository, NewsCategoryRepository>();
            services.AddScoped<IReviewRepository, ReviewRepository>();


            // Register Services
            services.AddScoped<IAuthServices, AuthService>();
            services.AddScoped<IBlogService, BlogService>();
            services.AddScoped<IBlogCategoryService, BlogCategoryService>();
            services.AddScoped<ITicketService, TicketService>();
            services.AddScoped<IFarmerService, FarmerService>();
            services.AddScoped<IEngineerService, EngineerService>();
            services.AddScoped<IEngineerFarmerAssignmentService, EngineerFarmerAssignmentService>();
            services.AddScoped<IProductCategoryService, ProductCategoryService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ICartService, CartService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<INewsCategoryService, NewsCategoryService>();
            services.AddScoped<INewsService, NewsService>();
            services.AddScoped<IReviewService, ReviewService>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Agriculture Smart API v1"));
            }

            //if (!env.IsProduction())
            //{
            //    app.UseHttpsRedirection();
            //}
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseCors("AllowAll");

            // Add authentication middleware
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}