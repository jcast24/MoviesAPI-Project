﻿using FluentValidation;
using Movies.Application.Models;
using Movies.Application.Repositories;
using Movies.Application.Validators;

namespace Movies.Application.Services;

public class MovieService : IMovieService
{
    private readonly IMovieRepository _movieRepository;
    // inject the validator 
    private readonly IValidator<Movie> _movieValidator;
    private readonly IRatingRepository _ratingRepository;
    private readonly IValidator<GetAllMoviesOptions> _optionsValidator;

    public MovieService(IMovieRepository movieRepository, IValidator<Movie> movieValidator, 
        IValidator<GetAllMoviesOptions> optionsValidator, IRatingRepository ratingRepository ,CancellationToken token = default)
    {
        _movieRepository = movieRepository;
        _movieValidator = movieValidator;
        _optionsValidator = optionsValidator;
        _ratingRepository = ratingRepository;

    }
    
    
    public async Task<bool> CreateAsync(Movie movie, CancellationToken token = default)
    {
        // instead of returning the result as a 404
        // var result = await _movieValidator.ValidateAsync(movie);
        // can also be done like this: 
        await _movieValidator.ValidateAndThrowAsync(movie, cancellationToken: token);
        return await _movieRepository.CreateAsync(movie, token);
    }

    public Task<Movie?> GetByIdAsync(Guid id, Guid? userid = default, CancellationToken token = default)
    {
        return _movieRepository.GetByIdAsync(id, userid, token);
    }

    public Task<Movie?> GetBySlugAsync(string slug, Guid? userid = default, CancellationToken token = default)
    {
        return _movieRepository.GetBySlug(slug, userid ,token);
    }

    public async Task<IEnumerable<Movie>> GetAllAsync(GetAllMoviesOptions options, CancellationToken token = default)
    {
        await _optionsValidator.ValidateAndThrowAsync(options, token);
        return await _movieRepository.GetAllAsync(options, token);
    }

    public async Task<Movie?> UpdateAsync(Movie movie, Guid? userid = default, CancellationToken token = default)
    {
        await _movieValidator.ValidateAndThrowAsync(movie, cancellationToken: token); 
        var movieExists = await _movieRepository.ExistsByIdAsync(movie.Id, token);

        if (!movieExists)
        {
            return null;
        }

        await _movieRepository.UpdateAsync(movie, token);

        if (!userid.HasValue)
        {
            var rating = await _ratingRepository.GetRatingAsync(movie.Id, token);
            movie.Rating = rating;
            return movie;
        }

        var ratings = await _ratingRepository.GetRatingAsync(movie.Id, userid.Value, token);
        movie.Rating = ratings.Rating;
        movie.UserRating = ratings.UserRating;
        
        
        return movie;
    }

    public Task<bool> DeleteByAsync(Guid id, CancellationToken token = default)
    {
        return _movieRepository.DeleteByIdAsync(id, token);
    }

    public Task<int> GetCountAsync(string? title, int? yearOfRelease, CancellationToken token = default)
    {
        return _movieRepository.GetCountAsync(title, yearOfRelease, token);
    }
}