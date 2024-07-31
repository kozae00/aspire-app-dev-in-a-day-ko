var builder = DistributedApplication.CreateBuilder(args);

// 추가
var cache = builder.AddRedis("cache");

// 추가
var config = builder.Configuration;

// 수정
var apiapp = builder.AddProject<Projects.AspireYouTubeSummariser_ApiApp>("apiapp")
                    .WithEnvironment("OpenAI__Endpoint", config["OpenAI:Endpoint"])
                    .WithEnvironment("OpenAI__ApiKey", config["OpenAI:ApiKey"])
                    .WithEnvironment("OpenAI__DeploymentName", config["OpenAI:DeploymentName"]);
                    
builder.AddProject<Projects.AspireYouTubeSummariser_WebApp>("webapp")
       .WithExternalHttpEndpoints()
       .WithReference(cache)
       .WithReference(apiapp);

builder.Build().Run();
