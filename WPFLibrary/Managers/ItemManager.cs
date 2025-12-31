using WPFLibrary.Interfaces;
using WPFLibrary.Record_Objects;
using static WPFLibrary.Database.Database;

namespace WPFLibrary.Managers;

public class ItemManager : IAdd
{
    private readonly List<BookRecord> m_Books = [];
    private readonly List<MovieRecord> m_Movies = [];
    private readonly List<GameRecord> m_Games = [];

    /// <summary>
    /// Updates an item in the list Books, Movies and Games
    /// </summary>
    /// <param name="newItem"> The updated item </param>
    /// <typeparam name="T"> The type of item </typeparam>
    public void UpdateItemInList<T>(T newItem)
    {
        if (newItem is null) return;
        
        List<T> temp = [newItem];

        switch (typeof(T))
        {
            case { } type when type == typeof(BookRecord):

                List<BookRecord>? book = temp as List<BookRecord>;

                for (int i = 0; i < m_Books.Count; i++)
                {
                    if (!m_Books[i].Guid.Equals(book?[0].Guid)) continue;

                    m_Books[i] = book[0];
                }

                break;

            case { } type when type == typeof(MovieRecord):

                List<MovieRecord>? movie = temp as List<MovieRecord>;

                for (int i = 0; i < m_Movies.Count; i++)
                {
                    if (!m_Movies[i].Guid.Equals(movie?[0].Guid)) continue;

                    m_Movies[i] = movie[0];
                }

                break;

            case { } type when type == typeof(GameRecord):

                List<GameRecord>? games = temp as List<GameRecord>;

                for (int i = 0; i < m_Games.Count; i++)
                {
                    if (!m_Games[i].Guid.Equals(games?[0].Guid)) continue;

                    m_Games[i] = games[0];
                }

                break;
        }
    }

    /// <summary>
    /// Gets an item from the list Books, Movies or Games by index
    /// </summary>
    /// <param name="index"> The index of item to get </param>
    /// <typeparam name="T"> The type of item </typeparam>
    /// <returns> The item as a type of T </returns>
    public T GetItemAtIndex<T>(int index)
    {
        return typeof(T) switch
        {
            { } type when type == typeof(BookRecord) => (T)(object)m_Books[index],
            { } type when type == typeof(MovieRecord) => (T)(object)m_Movies[index],
            { } type when type == typeof(GameRecord) => (T)(object)m_Games[index],
            _ => throw new ArgumentException()
        };
    }

    /// <summary>
    /// Deletes an item from the list Books, Movies or Game
    /// </summary>
    /// <param name="index"> The index of the item to be removed </param>
    /// <typeparam name="T"> The type of item </typeparam>
    public void DeleteItemAtIndex<T>(int index)
    {
        switch (typeof(T))
        {
            case { } type when type == typeof(BookRecord):
                m_Books.RemoveAt(index);
                break;
            case { } type when type == typeof(MovieRecord):
                m_Movies.RemoveAt(index);
                break;
            case { } type when type == typeof(GameRecord):
                m_Games.RemoveAt(index);
                break;
        }
    }

    /// <summary>
    /// Adds an item to a list (BookRecords, MovieRecords, GameRecords)
    /// </summary>
    /// <param name="item">item to add</param>
    /// <typeparam name="T">The type of item</typeparam>
    public void Add<T>(T item)
    {
        switch (item)
        {
            case BookRecord book:
                if (!m_Books.Contains(book))
                {
                    m_Books.Add(book);
                }

                break;

            case MovieRecord movie:
                if (!m_Movies.Contains(movie))
                {
                    m_Movies.Add(movie);
                }

                break;

            case GameRecord game:
                if (!m_Games.Contains(game))
                {
                    m_Games.Add(game);
                }
                break;
        }
    }

    /// <summary>
    /// Saves the lists of BookRecords, MovieRecords and GameRecords
    /// </summary>
    public void Save() => SaveItems(m_Books, m_Movies, m_Games);

    /// <summary>
    /// Loads lists of BookRecords, MovieRecords and GameRecords that was saved
    /// </summary>
    /// <returns></returns>
    public void Load()
    {
        (List<BookRecord> books, List<MovieRecord> movies, List<GameRecord> games) data = ReadDatabase();

        foreach (BookRecord book in data.books.Where(book => !m_Books.Contains(book)))
        {
            m_Books.Add(book);
        }

        foreach (MovieRecord movie in data.movies.Where(movie => !m_Movies.Contains(movie)))
        {
            m_Movies.Add(movie);
        }

        foreach (GameRecord game in data.games.Where(game => !m_Games.Contains(game)))
        {
            m_Games.Add(game);
        }
        
        WindowManager.RefreshWindows(m_Books, m_Movies, m_Games);
    }
}