﻿using Movies.Application.Models;
using Movies.Contracts.Requests;

namespace Movies.API.Mapping;


// Contains all the mapping related technologies
public static class ContractMapping
{
    public static Movie MapToMovie(this CreateMovieRequest request)
    {
        return new Movie
        {
            Id = Guid.NewGuid(),
            Title = request.Title,
            YearOfRelease = request.YearOfRelease,
            Genres = request.Genres.ToList()
        };
    }
}