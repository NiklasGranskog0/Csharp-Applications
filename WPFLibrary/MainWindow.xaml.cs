using System.Windows;
using System.Windows.Controls;
using WPFLibrary.Enums;
using WPFLibrary.Extensions;
using WPFLibrary.Interfaces;
using WPFLibrary.Managers;
using WPFLibrary.Record_Objects;
using static WPFLibrary.Extensions.RecordExtensions;

namespace WPFLibrary;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window, IAdd
{
    private readonly WindowManager m_WindowManager;

    /// <summary>
    /// Constructor
    /// </summary>
    public MainWindow()
    {
        InitializeComponent();

        AddComboBox.SetComboBoxEnum<Items>();

        MovieRating.SetComboBoxEnum<Rating>();
        BookRating.SetComboBoxEnum<Rating>();
        GameRating.SetComboBoxEnum<Rating>();

        MovieGenre.SetComboBoxEnum<Genre>();
        BookGenre.SetComboBoxEnum<Genre>();
        GameGenre.SetComboBoxEnum<Genre>();

        m_WindowManager = new WindowManager(BookList, MovieList, GameList);

        BookList.SelectionMode = SelectionMode.Single;
        MovieList.SelectionMode = SelectionMode.Single;
        GameList.SelectionMode = SelectionMode.Single;

        AddComboBox.SelectionChanged += OnAddComboBoxSelection;
        OnAddComboBoxSelection(this, null!);
    }

    /// <summary>
    /// Changes which item group box to be enabled based on AddComboBox 
    /// </summary>
    private void OnAddComboBoxSelection(object sender, SelectionChangedEventArgs e)
    {
        Items item = (Items)AddComboBox.SelectedIndex;

        BookGroupBox.IsEnabled = false;
        MovieGroupBox.IsEnabled = false;
        GameGroupBox.IsEnabled = false;

        switch (item)
        {
            case Items.Book:
                BookGroupBox.IsEnabled = true;
                break;
            case Items.Movie:
                MovieGroupBox.IsEnabled = true;
                break;
            case Items.Game:
                GameGroupBox.IsEnabled = true;
                break;
        }
    }

    /// <summary>
    /// Adds selected item based on AddComboBox's selected index when Clicking Add button
    /// </summary>
    private void Add_Click(object sender, RoutedEventArgs e)
    {
        Items item = (Items)AddComboBox.SelectedIndex;

        switch (item)
        {
            case Items.Book:
                AddBook();
                break;
            case Items.Movie:
                AddMovie();
                break;
            case Items.Game:
                AddGame();
                break;
        }
    }

    /// <summary>
    /// Creates a BookRecord 
    /// </summary>
    private void AddBook()
    {
        if (!CreateBookRecord(BookTitle.Text, BookAuthor.Text, BookPageCount.Text,
                (Genre)BookGenre.SelectedItem,
                (Rating)BookRating.SelectedItem, out BookRecord? bookRecord)) return;

        Add(bookRecord);
        BookTitle.Text = BookAuthor.Text = BookPageCount.Text = string.Empty;
    }

    /// <summary>
    /// Creates a MovieRecord
    /// </summary>
    private void AddMovie()
    {
        if (!CreateMovieRecord(MovieTitle.Text, MovieActor.Text, MovieDuration.Text, (Genre)MovieGenre.SelectedItem,
                (Rating)MovieRating.SelectedItem, out MovieRecord? movieRecord)) return;

        Add(movieRecord);
        MovieTitle.Text = MovieActor.Text = MovieDuration.Text = string.Empty;
    }

    /// <summary>
    /// Creates a GameRecord
    /// </summary>
    private void AddGame()
    {
        if (!CreateGameRecord(GameTitle.Text, Publisher.Text, Developer.Text, (Genre)GameGenre.SelectedItem,
                (Rating)GameRating.SelectedItem, out GameRecord? gameRecord)) return;

        Add(gameRecord);
        GameTitle.Text = Publisher.Text = Developer.Text = string.Empty;
    }

    /// <summary>
    /// Opens item changer window
    /// </summary>
    private void Change_Click(object sender, RoutedEventArgs e)
    {
        m_WindowManager.OpenChangeWindow();
    }

    /// <summary>
    /// Clears selected items
    /// </summary>
    private void Clear_Click(object sender, RoutedEventArgs e)
    {
        BookList.SelectedItem = null;
        MovieList.SelectedItem = null;
        GameList.SelectedItem = null;
    }

    /// <summary>
    /// Deletes selected items 
    /// </summary>
    private void Delete_Click(object sender, RoutedEventArgs e)
    {
        object[] selectedItems = [BookList.SelectedItem, MovieList.SelectedItem, GameList.SelectedItem];
        m_WindowManager.RemoveItems(selectedItems);
    }

    /// <summary>
    /// Saves items
    /// </summary>
    private void Save_Click(object sender, RoutedEventArgs e) => m_WindowManager.SaveItems();

    /// <summary>
    /// Load items
    /// </summary>
    private void Load_Click(object sender, RoutedEventArgs e) => m_WindowManager.LoadItems();

    /// <summary>
    /// Adds an item to the window manager class
    /// </summary>
    /// <param name="item">item to add</param>
    /// <typeparam name="T">type of item to add</typeparam>
    public void Add<T>(T item)
    {
        m_WindowManager.Add(item);
    }

    /// <summary>
    /// Closes the window
    /// </summary>
    private void Close_Click(object sender, RoutedEventArgs e) => Close();
}