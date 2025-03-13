using FluentValidation;
using Movies.Application.Models;
using Movies.Application.Repositories;

namespace Movies.Application.Services;

public class MovieService : IMovieService
{
    private readonly IMovieRepository _movieRepository;
    // inject the validator 
    private readonly IValidator<Movie> _movieValidator;

    public MovieService(IMovieRepository movieRepository)
    {
        _movieRepository = movieRepository;
    }
    
    
    public async Task<bool> CreateAsync(Movie movie)
    {
        // instead of returning the result as a 404
        // var result = await _movieValidator.ValidateAsync(movie);
        // can also be done like this: 
        await _movieValidator.ValidateAndThrowAsync(movie);
        return await _movieRepository.CreateAsync(movie);
    }

    public Task<Movie?> GetByIdAsync(Guid id)
    {
        return _movieRepository.GetByIdAsync(id);
    }

    public Task<Movie?> GetBySlugAsync(string slug)
    {
        return _movieRepository.GetBySlug(slug);
    }

    public Task<IEnumerable<Movie>> GetAllAsync()
    {
        return _movieRepository.GetAllAsync();
    }

    public async Task<Movie?> UpdateAsync(Movie movie)
    {
        await _movieValidator.ValidateAndThrowAsync(movie); 
        var movieExists = await _movieRepository.ExistsByIdAsync(movie.Id);

        if (!movieExists)
        {
            return null;
        }

        await _movieRepository.UpdateAsync(movie);
        return movie;
    }

    public Task<bool> DeleteByAsync(Guid id)
    {
        return _movieRepository.DeleteByIdAsync(id);
    }
}