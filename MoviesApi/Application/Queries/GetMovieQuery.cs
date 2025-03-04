using MediatR;
using MoviesApi.Application.DTOs;
using MoviesApi.Application.Responses;

namespace MoviesApi.Application.Queries
{
    public record GetMovieQuery(string MovieName, int PageCount, int PageNumber) : IRequest<MovieResponse>;
}
