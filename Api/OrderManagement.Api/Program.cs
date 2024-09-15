using Microsoft.OpenApi.Models;
using System.Text.Json.Serialization;
using Framework.AssemblyHelper;
using Framework.DependencyInjection;
using Framework.Facade;
using Framework.Core.Domain;

var builder = WebApplication.CreateBuilder(args);

var assemblyHelper = new AssemblyHelper(nameof(OrderManagement));
Registrar(builder.Services, assemblyHelper, builder.Environment.EnvironmentName);

var mvcBuilder = builder.Services.AddMvc(option =>
{
    option.EnableEndpointRouting = false;
})
    .AddNewtonsoftJson(o =>
    {
        o.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
        o.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
    })
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

AddControllers(assemblyHelper, mvcBuilder);

mvcBuilder.AddControllersAsServices();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "OrderManagement.Api", Version = "v1" });
});



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();

}

app.UseSwagger();
app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "OrderManagement.Api v1"));

app.UseRouting();

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute("default", "{Controller=swagger}/{action=Index.html}");
});

app.UseMvc();

app.Run();

void Registrar(IServiceCollection services, AssemblyHelper assemblyHelper, string envName)
{
    var builder = new ConfigurationBuilder().SetBasePath(AppContext.BaseDirectory)
        .AddJsonFile("AppSettings.json", true, true)
        .AddJsonFile($"AppSettings.{envName}.json", true, true);

    var connectionString = builder.Build().GetConnectionString("DefaultConnection");
    var registrars = assemblyHelper.GetInstanceByInterface(typeof(IRegistrar));
    foreach (IRegistrar registrar in registrars)
        registrar.Register(services, connectionString);
}


static void AddControllers(AssemblyHelper assemblyHelper, IMvcBuilder mvcBuilder)
{
    var controllerAssemblies = assemblyHelper.GetAssemblies(typeof(FacadeCommandBase)).Distinct();

    foreach (var apiControllerAssembly in controllerAssemblies)
        mvcBuilder.AddApplicationPart(apiControllerAssembly);

    controllerAssemblies = assemblyHelper.GetAssemblies(typeof(FacadeQueryBase)).Distinct();
    foreach (var apiControllerAssembly in controllerAssemblies)
        mvcBuilder.AddApplicationPart(apiControllerAssembly);
}
