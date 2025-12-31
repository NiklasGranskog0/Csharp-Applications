using System.Windows;
using WPFLibrary.Enums;
using WPFLibrary.Record_Objects;

namespace WPFLibrary.Extensions;

public static class RecordExtensions
{
    /// <summary>
    /// Creates a BookRecord
    /// </summary>
    /// <returns>True if a BookRecord was created</returns>
    public static bool CreateBookRecord(string title, string author, string pageCount, Genre genre, Rating rating, out BookRecord? bookRecord, Guid guid = default)
    {
        bookRecord = new BookRecord();
        if (guid.Equals(Guid.Empty)) guid = Guid.NewGuid();
        
        if (string.IsNullOrEmpty(title))
        {
            MessageBox.Show("Invalid Book Title!", "Invalid Book Title Text", MessageBoxButton.OK,
                MessageBoxImage.Error);
            return false;
        }

        if (string.IsNullOrEmpty(author))
        {
            MessageBox.Show("Invalid Book Author!", "Invalid Book Author Text", MessageBoxButton.OK,
                MessageBoxImage.Error);
            return false;
        }

        if (string.IsNullOrEmpty(pageCount) || !int.TryParse(pageCount, out int pagesCount) ||
            pagesCount < 1)
        {
            MessageBox.Show("Invalid Number of Pages!", "Invalid Book Pages", MessageBoxButton.OK,
                MessageBoxImage.Error);
            return false;
        }

        bookRecord = new BookRecord
        {
            Title = title,
            Author = author,
            Pages = pagesCount,
            Genre = genre,
            Rating = rating,
            Guid = guid
        };

        return true;
    }

    /// <summary>
    /// Creates a MovieRecord
    /// </summary>
    /// <returns>True if a MovieRecord was created</returns>
    public static bool CreateMovieRecord(string title, string actor, string duration, Genre genre, Rating rating, out MovieRecord? movieRecord, Guid guid = default)
    {
        movieRecord = new MovieRecord();
        if (guid.Equals(Guid.Empty)) guid = Guid.NewGuid();
        
        if (string.IsNullOrEmpty(title))
        {
            MessageBox.Show("Invalid Movie Title!", "Invalid Movie Title Text", MessageBoxButton.OK,
                MessageBoxImage.Error);
            return false;
        }

        if (string.IsNullOrEmpty(actor))
        {
            MessageBox.Show("Invalid Main Actor Name!", "Invalid Main Actor Text", MessageBoxButton.OK,
                MessageBoxImage.Error);
            return false;
        }

        if (!int.TryParse(duration, out int time) && time > 0)
        {
            MessageBox.Show("Invalid Movie Duration!", "Invalid Movie Duration", MessageBoxButton.OK,
                MessageBoxImage.Error);
            return false;
        }

        movieRecord = new MovieRecord
        {
            Title = title,
            MovieActor = actor,
            Genre = genre,
            Duration = time,
            Rating = rating,
            Guid = guid
        };

        return true;
    }

    /// <summary>
    /// Creates a GameRecord
    /// </summary>
    /// <returns>True if a GameRecord was created</returns>
    public static bool CreateGameRecord(string title, string publisher, string developer, Genre genre, Rating rating, out GameRecord? gameRecord, Guid guid = default)
    {
        gameRecord = new GameRecord();
        if (guid.Equals(Guid.Empty)) guid = Guid.NewGuid();
        
        if (string.IsNullOrEmpty(title))
        {
            MessageBox.Show("Invalid Game Title!", "Invalid Game Title", MessageBoxButton.OK,
                MessageBoxImage.Error);
            return false;
        }

        if (string.IsNullOrEmpty(publisher))
        {
            MessageBox.Show("Invalid Publisher Name!", "Invalid Publisher", MessageBoxButton.OK,
                MessageBoxImage.Error);
            return false;
        }

        if (string.IsNullOrEmpty(developer))
        {
            MessageBox.Show("Invalid Developer Name!", "Invalid Developer", MessageBoxButton.OK,
                MessageBoxImage.Error);
            return false;
        }

        gameRecord = new GameRecord
        {
            Title = title,
            Publisher = publisher,
            Genre = genre,
            Developer = developer,
            Rating = rating,
            Guid = guid
        };

        return true;
    }
}