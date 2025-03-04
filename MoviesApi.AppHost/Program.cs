var builder = DistributedApplication.CreateBuilder(args);

var sql = builder.AddSqlServer("sql");
var sqldb = sql.AddDatabase("sqldb");

builder.AddProject<Projects.MoviesApi>("moviesapi")
    .WithReference(sqldb);;

builder.Build().Run();
