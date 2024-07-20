using Movies.API.Models;

namespace Movies.API.Data
{
    public class MoviesContextSeed
    {
        public static void SeedAsync(MoviesAPIContext moviesAPIContext)
        {
            if (!moviesAPIContext.Movie.Any())
            {
                var movies = new List<Movie>
                { 
                    new Movie{ Id=1, Genre="Drama", Title="HandsomeMan", ImageUrl="src/image.jpg", Owner="Michal", Rating="4", ReleaseDate = new DateTime(2020, 5, 5)},
                    new Movie{ Id=2, Genre="Action", Title="Fishman", ImageUrl="src/image.jpg", Owner="Jorden", Rating="4", ReleaseDate = new DateTime(2020, 5, 5)},
                    new Movie{ Id=3, Genre="Romantic", Title="Ironman", ImageUrl="src/image.jpg", Owner="Stephan", Rating="4", ReleaseDate = new DateTime(2020, 5, 5)},
                    new Movie{ Id=4, Genre="Horrer", Title="Spiderman", ImageUrl="src/image.jpg", Owner="Elbert", Rating="4", ReleaseDate = new DateTime(2020, 5, 5)},
                    new Movie{ Id=5, Genre="Thriller", Title="Antman", ImageUrl="src/image.jpg", Owner="Joseph", Rating="4", ReleaseDate = new DateTime(2020, 5, 5)},
                };

                moviesAPIContext.Movie.AddRange(movies);
                moviesAPIContext.SaveChangesAsync();
            }
        }
    }
}
