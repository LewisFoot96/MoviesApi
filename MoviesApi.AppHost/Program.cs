using k8s.Models;

var builder = DistributedApplication.CreateBuilder(args);

var sql = builder.AddSqlServer("sql");
var sqldb = sql.AddDatabase("sqldb");

var apiService = builder.AddProject<Projects.MoviesApi>("moviesapi")
    .WithReference(sqldb); ;

builder.AddProject<Projects.MoviesUI>("moviesui")
        .WithExternalHttpEndpoints()
    .WithReference(apiService); ;

builder.Build().Run();
