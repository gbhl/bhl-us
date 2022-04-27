// Supporting multiple API versions at once with Swagger
// https://referbruv.com/blog/posts/integrating-aspnet-core-api-versions-with-swagger-ui
// https://stackoverflow.com/questions/58834430/c-sharp-net-core-swagger-trying-to-use-multiple-api-versions-but-all-end-point

// Hosting .NET 6 WebAPIs in IIS
// https://docs.microsoft.com/en-us/aspnet/core/host-and-deploy/?view=aspnetcore-6.0
// https://docs.microsoft.com/en-us/aspnet/core/tutorials/publish-to-iis?view=aspnetcore-6.0&tabs=visual-studio
// https://docs.microsoft.com/en-us/aspnet/core/host-and-deploy/iis/?view=aspnetcore-6.0
// https://stackify.com/how-to-deploy-asp-net-core-to-iis/

// Automating swagger definition creation during build
// https://tonylunt.medium.com/swashbuckle-cli-automating-asp-net-core-api-swagger-definitions-during-build-f3ee2b8e857a

// Generating API clients
// https://devblogs.microsoft.com/dotnet/generating-http-api-clients-using-visual-studio-connected-services/

using Microsoft.AspNetCore.Mvc;

[assembly: ApiConventionType(typeof(DefaultApiConventions))]

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddTransient<BHL.SiteServicesREST.v1.Services.IMailService, BHL.SiteServicesREST.v1.Services.MailService>();
builder.Services.AddTransient<BHL.SiteServicesREST.v1.Services.IQueueService, BHL.SiteServicesREST.v1.Services.QueueService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "Site Service API", Version = "v1" });
});
builder.Services.AddSwaggerGenNewtonsoftSupport();

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
    app.UseSwagger();
    app.UseSwaggerUI();
//}

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
