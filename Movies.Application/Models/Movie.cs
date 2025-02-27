namespace Movies.Application.Models;

public class Movie
{
    public required Guid Id { get; init; }
    public required string Title { get; set; }
    public required int YearOfRelease { get; set; }
    
    // this approach allows to add items during runtime because 
    // it is a list mutable data structure
    public required List<string> Genres { get; init; } = [];
}