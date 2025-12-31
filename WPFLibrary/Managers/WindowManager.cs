using System.Windows;
using System.Windows.Controls;
using WPFLibrary.Extensions;
using WPFLibrary.Interfaces;
using WPFLibrary.Record_Objects;

namespace WPFLibrary.Managers;

public class WindowManager : IAdd
{
    private readonly ListBox m_BookListBox;
    private readonly ListBox m_MovieListBox;
    private readonly ListBox m_GameListBox;

    private readonly ItemManager m_ItemManager;
    private ChangeWindow m_ChangeWindow;

    public static event Action<BookRecord?, MovieRecord?, GameRecord?> ItemUpdateEvent;
    public static event Action<List<BookRecord>, List<MovieRecord>, List<GameRecord>> RefreshWindowsEvent;

    /// <summary>
    /// Constructor
    /// </summary>
    public WindowManager(ListBox bookListBox, ListBox movieListBox, ListBox gameListBox)
    {
        m_BookListBox = bookListBox;
        m_MovieListBox = movieListBox;
        m_GameListBox = gameListBox;

        m_ItemManager = new ItemManager();
        ItemUpdateEvent += OnItemUpdateEvent;

        RefreshWindowsEvent += OnRefreshWindowsEvent;
    }

    /// <summary>
    /// Refresh window event invoke method 
    /// </summary>
    public static void RefreshWindows(List<BookRecord> books, List<MovieRecord> movies, List<GameRecord> games) => RefreshWindowsEvent.Invoke(books, movies, games);

    /// <summary>
    /// Refresh window event, refreshes the BookRecords, MovieRecords and GameRecords list boxes
    /// </summary>
    /// <param name="books">List of BookRecords</param>
    /// <param name="movies">List of MovieRecords</param>
    /// <param name="games">List of GameRecords</param>
    private void OnRefreshWindowsEvent(List<BookRecord> books, List<MovieRecord> movies, List<GameRecord> games)
    {
        m_BookListBox.Items.Clear();
        m_MovieListBox.Items.Clear();
        m_GameListBox.Items.Clear();

        foreach (BookRecord b in books.Where(book => !string.IsNullOrEmpty(book.Title)).ToList())
        {
            Add(b);
        }

        foreach (MovieRecord m in movies.Where(movie => !string.IsNullOrEmpty(movie.Title)).ToList())
        {
            Add(m);
        }

        foreach (GameRecord g in games.Where(game => !string.IsNullOrEmpty(game.Title)).ToList())
        {
            Add(g);
        }
    }

    /// <summary>
    /// Static function to be called from ChangeWindow to invoke ItemUpdateEvent if an item(book, movie, game) was changed
    /// </summary>
    /// <param name="book">The BookRecord to update</param>
    /// <param name="movie">The MovieRecord to update</param>
    /// <param name="gameRecord">The GameRecord to update</param>
    public static void ItemWasChanged(BookRecord? book, MovieRecord? movie, GameRecord? gameRecord) => ItemUpdateEvent.Invoke(book, movie, gameRecord);

    /// <summary>
    /// ItemUpdateEvent, Updates an item and updates its displayed name in the listbox
    /// </summary>
    /// <param name="book">BookRecord to update</param>
    /// <param name="movie">MovieRecord to update</param>
    /// <param name="game">GameRecord to update</param>
    private void OnItemUpdateEvent(BookRecord? book, MovieRecord? movie, GameRecord? game)
    {
        if (m_BookListBox.SelectedItem is not null && book is not null)
        {
            m_ItemManager.UpdateItemInList(book);
            m_BookListBox.UpdateItemNameInList(m_BookListBox.SelectedIndex, Extension.CreateNameForItem(book.Title, book.Rating));
            MessageBox.Show($"{book.Title} has been changed.", "Book Update", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        
        if (m_MovieListBox.SelectedItem is not null && movie is not null)
        {
            m_ItemManager.UpdateItemInList(movie);
            m_MovieListBox.UpdateItemNameInList(m_MovieListBox.SelectedIndex, Extension.CreateNameForItem(movie.Title, movie.Rating));
            MessageBox.Show($"{movie.Title} has been changed.", "Movie Update", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        
        if (m_GameListBox.SelectedItem is not null && game is not null)
        {
            m_ItemManager.UpdateItemInList(game);
            m_GameListBox.UpdateItemNameInList(m_GameListBox.SelectedIndex, Extension.CreateNameForItem(game.Title, game.Rating));
            MessageBox.Show($"{game.Title} has been changed.", "Game Update", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        
        DisableSelection(false);
    }

    /// <summary>
    /// Disables the list boxes so it becomes unable to change item while changing 
    /// </summary>
    /// <param name="disable">by default, it is true</param>
    private void DisableSelection(bool disable = true)
    {
        m_BookListBox.IsEnabled = !disable;
        m_MovieListBox.IsEnabled = !disable;
        m_GameListBox.IsEnabled = !disable;
    }

    /// <summary>
    /// Adds an item to the ItemManager, and adds the items name to a listbox
    /// </summary>
    /// <param name="item">Item to add</param>
    /// <typeparam name="T">ItemType</typeparam>
    public void Add<T>(T item)
    {
        m_ItemManager.Add(item);

        switch (item)
        {
            case BookRecord book:
                m_BookListBox.AddItemNameToList(Extension.CreateNameForItem(book.Title, book.Rating));
                break;
            case MovieRecord movie:
                m_MovieListBox.AddItemNameToList(Extension.CreateNameForItem(movie.Title, movie.Rating));
                break;
            case GameRecord game:
                m_GameListBox.AddItemNameToList(Extension.CreateNameForItem(game.Title, game.Rating));
                break;
        }
    }

    /// <summary>
    /// Removes items
    /// </summary>
    /// <param name="items">to remove</param>
    public void RemoveItems(object[] items)
    {
        foreach (object item in items)
        {
            if (m_BookListBox.Items.Contains(item))
            {
                int index = m_BookListBox.Items.IndexOf(item);
                m_ItemManager.DeleteItemAtIndex<BookRecord>(index);
                m_BookListBox.Items.Remove(item);
                continue;
            }

            if (m_MovieListBox.Items.Contains(item))
            {
                int index = m_MovieListBox.Items.IndexOf(item);
                m_ItemManager.DeleteItemAtIndex<MovieRecord>(index);
                m_MovieListBox.Items.Remove(item);
                continue;
            }

            if (m_GameListBox.Items.Contains(item))
            {
                int index = m_GameListBox.Items.IndexOf(item);
                m_ItemManager.DeleteItemAtIndex<GameRecord>(index);
                m_GameListBox.Items.Remove(item);
            }
        }
    }

    /// <summary>
    /// Saves items
    /// </summary>
    public void SaveItems() => m_ItemManager.Save();

    /// <summary>
    /// Loads items
    /// </summary>
    public void LoadItems() => m_ItemManager.Load();

    /// <summary>
    /// Opens the change window and disables item selection
    /// </summary>
    public void OpenChangeWindow()
    {
        if (m_BookListBox.SelectedItem is null && m_MovieListBox.SelectedItem is null &&
            m_GameListBox.SelectedItem is null) return;

        DisableSelection();
        
        BookRecord b = new BookRecord();
        MovieRecord m = new MovieRecord();
        GameRecord g = new GameRecord();

        if (m_BookListBox.SelectedItem is not null) b = m_ItemManager.GetItemAtIndex<BookRecord>(m_BookListBox.SelectedIndex);
        
        if (m_MovieListBox.SelectedItem is not null)
            m = m_ItemManager.GetItemAtIndex<MovieRecord>(m_MovieListBox.SelectedIndex);
        
        if (m_GameListBox.SelectedItem is not null) g = m_ItemManager.GetItemAtIndex<GameRecord>(m_GameListBox.SelectedIndex);

        m_ChangeWindow = new ChangeWindow(b, m, g);
        m_ChangeWindow.Show();
    }
}