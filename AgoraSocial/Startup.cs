using AgoraSocial.Auth;
using CorrelationId.DependencyInjection;
using IdentityModel.Client;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Raven.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;



namespace AgoraSocial
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
            services.AddRavenDbDocStore();
            services.AddRavenDbAsyncSession();

            services.AddControllers();

            //services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerGenOptions>()
            var identityUrl = Configuration.GetValue<string>("IdentityUrl");

            services.AddSingleton<IDiscoveryCache>(r =>
            {
                var factory = r.GetRequiredService<IHttpClientFactory>();
                return new DiscoveryCache(identityUrl, () => factory.CreateClient());
            });


            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(options =>
            {
                options.Authority = identityUrl;
                options.RequireHttpsMetadata = false;
                options.Audience = "AgoraSocial";
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateAudience = true
                };
                //options.SaveToken = true;
                options.Events = new JwtBearerEvents()
                {
                    OnTokenValidated = context =>
                    {
                        var accessToken = context.SecurityToken as JwtSecurityToken;
                        if (accessToken != null)
                        {
                            ClaimsIdentity identity = context.Principal.Identity as ClaimsIdentity;
                            if (identity != null)
                            {
                                identity.AddClaim(new Claim("access_token", accessToken.RawData));
                            }
                        }

                        return Task.CompletedTask;
                    }
                };
                //options.Events.OnTokenValidated = async ctx =>
                //{
                //    // TODO: store UserInfo in redis

                //};
            });

            services.AddCorrelationId();

            services.AddAuthorization(options =>
            {
                options.DefaultPolicy = new AuthorizationPolicyBuilder()
                  .RequireAuthenticatedUser()
                  .RequireClaim("scope", "AgoraSocial")
                  //.RequireClaim("scope", "AgoraSocial111")
                  .Build();

                //o.AddPolicy(_RequireAuthenticatedUserPolicy,
                //builder => builder.RequireAuthenticatedUser()

                options.AddPolicy("myPolicy", builder =>
                {
                    // require scope1
                    builder.RequireClaim("scope", "scope1");
                    builder.RequireClaim("scope", "scope2");
                });

            });

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IClaimsTransformation, UserInfoClaims>();


            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "AgoraSocial", Version = "v1" });

                //c.OperationFilter<AuthorizeOperationFilter>();

                // TODO: this almost works now that swagger-ui has been updated... see if we can get this to actually work!
                //    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                //    {
                //        Type = SecuritySchemeType.OpenIdConnect,
                //        OpenIdConnectUrl = new Uri($"{identityUrl}/.well-known/openid-configuration"),

                //        Flows = new OpenApiOAuthFlows
                //        {
                //            AuthorizationCode = new OpenApiOAuthFlow
                //            {
                //                AuthorizationUrl = new Uri($"{identityUrl}/connect/authorize"),
                //                TokenUrl = new Uri($"{identityUrl}/connect/token"),
                //                Scopes = new Dictionary<string, string>
                //                {
                //                    { "AgoraSocial", "AgoraSocial API" }
                //                }
                //            }

                //        }
                //    });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Type = SecuritySchemeType.OAuth2,
                Flows = new OpenApiOAuthFlows
                {
                    AuthorizationCode = new OpenApiOAuthFlow
                    {
                        AuthorizationUrl = new Uri($"{identityUrl}/connect/authorize"),
                        TokenUrl = new Uri($"{identityUrl}/connect/token"),
                        Scopes = new Dictionary<string, string>
                            {
                                { "AgoraSocial", "AgoraSocial API" },
                                { "openid", "OpenId Scope" },
                                { "profile", "Profile Info" }
                            }
                    }

                }
            });

            c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme()
                        {
                            Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
                        }
                        , new[] { "AgoraSocial" }
                    }
                });

            });

            //services.AddSingleton((ctx) =>
            //{
            //    IDocumentStore store = new DocumentStore
            //    {
            //        Urls = new[] { Configuration["Raven:Url"] },
            //        Database = Configuration["Raven:Database"],
            //        Certificate = Configuration.GetSection("Raven:EncryptionEnabled").Get<bool>() ? new X509Certificate2(Configuration["Raven:CertFile"], Configuration["Raven:CertPassword"]) : null
            //    };

            //    //var serializerConventions = new NewtonsoftJsonSerializationConventions();
            //    //serializerConventions.CustomizeJsonSerializer += (JsonSerializer serializer) =>
            //    //{
            //    //    serializer.Converters.Add(new ClaimConverter());
            //    //    serializer.Converters.Add(new ClaimsPrincipalConverter());
            //    //};

            //    //store.Conventions.Serialization = serializerConventions;

            //    return store.Initialize();
            //});

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => {
                    //IConfigurationSection googleAuthNSection = Configuration.GetSection("Authentication:Google");

                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "AgoraSocial v1");

                    //c.OAuthClientId(googleAuthNSection["ClientId"]);
                    //c.OAuthClientSecret(googleAuthNSection["ClientSecret"]);
                    c.OAuthClientId("AgoraSocialApiSwagger");
                    c.OAuthClientSecret(null);
                    c.OAuthUsePkce();
                });
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
