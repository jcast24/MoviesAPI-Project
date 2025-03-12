using FluentValidation;
using Movies.Application.Models;
using Movies.Application.Services;

namespace Movies.Application.Validators;

public class MovieValidator : AbstractValidator<Movie>
{
    private readonly IMovieService _movieService;
    
    public MovieValidator()
    {
        // Id should not be empty
        RuleFor(x => x.Id).NotEmpty(); 
        
        // Movies should have at least one genre
        RuleFor(x => x.Genres).NotEmpty();
        
        // Title should not be empty
        RuleFor(x => x.Title).NotEmpty();
        
        // Only accept movies that have been already released
        RuleFor(x => x.YearOfRelease).LessThanOrEqualTo(DateTime.UtcNow.Year);

        RuleFor(x => x.Slug).MustAsync(ValidateSlug).WithMessage("This movie already exists in the system");

    }

    private async Task<bool> ValidateSlug(Movie movie, string slug, CancellationToken token = default)
    {
        var existingMovie = await _movieService.GetBySlugAsync(slug);

        if (existingMovie is not null)
        {
            return existingMovie.Id == movie.Id;
        }

        return existingMovie is null;
    }
}