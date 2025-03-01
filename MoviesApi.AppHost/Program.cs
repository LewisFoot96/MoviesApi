var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.MoviesApi>("moviesapi");

builder.Build().Run();
