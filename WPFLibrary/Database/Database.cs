using System.IO;
using System.Text.Json;
using System.Windows;
using WPFLibrary.Record_Objects;

namespace WPFLibrary.Database;

public static class Database
{
    public static void SetJsonOptions(JsonSerializerOptions? jsonOptions) => s_jsonSerializerOptions = jsonOptions;

    private static JsonSerializerOptions? s_jsonSerializerOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        WriteIndented = true,
    };

    /// <summary>
    /// Saves a list of books, movies & games to a json file
    /// </summary>
    /// <param name="books">List of books to be saved to a file</param>
    /// /// <param name="movies">List of movies to be saved to a file</param>
    /// /// <param name="games">List of games to be saved to a file</param>
    public static void SaveItems(List<BookRecord> books, List<MovieRecord> movies, List<GameRecord> games)
    {
        if (books.Count < 1 && movies.Count < 1 && games.Count < 1)
        {
            MessageBox.Show("There is no data to be saved.", "Database", MessageBoxButton.OK, MessageBoxImage.Information);
            return;
        }

        if (books.Count > 0)
        {
            File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + @"\Books.json",
                JsonSerializer.Serialize(books, s_jsonSerializerOptions));    
        }

        if (movies.Count > 0)
        {
            File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + @"\Movies.json",
                JsonSerializer.Serialize(movies, s_jsonSerializerOptions));    
        }

        if (games.Count > 0)
        {
            File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + @"\Games.json",
                JsonSerializer.Serialize(games, s_jsonSerializerOptions));    
        }
    }

    /// <summary>
    /// Reads a saved json file and reads it contents
    /// </summary>
    /// <returns>Lists of books, movies and games</returns>
    public static (List<BookRecord> books, List<MovieRecord> movies, List<GameRecord> games) ReadDatabase()
    {
        List<BookRecord>? books = [];
        List<MovieRecord>? movies = [];
        List<GameRecord>? games = [];
        
        string jsonBooks = File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + @"\Books.json");
        string jsonMovies = File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + @"\Movies.json");
        string jsonGames = File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + @"\Games.json");

        if (string.IsNullOrEmpty(jsonBooks) && string.IsNullOrEmpty(jsonMovies) && string.IsNullOrEmpty(jsonGames))
        {
            MessageBox.Show("No Data found", "Database", MessageBoxButton.OK, MessageBoxImage.Error);
            return (books, movies, games);
        }
        
        try
        {
            books = JsonSerializer.Deserialize<List<BookRecord>>(jsonBooks, s_jsonSerializerOptions);
            movies = JsonSerializer.Deserialize<List<MovieRecord>>(jsonMovies, s_jsonSerializerOptions);
            games = JsonSerializer.Deserialize<List<GameRecord>>(jsonGames, s_jsonSerializerOptions);

            if (books.Count < 1 && movies.Count < 1 && games.Count < 1)
            {
                MessageBox.Show("There was no saved data.", "Database", MessageBoxButton.OK, MessageBoxImage.Information);
                return (books, movies, games);
            }
            
            MessageBox.Show("Loaded Data", "Database", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

        return (books, movies, games);
    }
}