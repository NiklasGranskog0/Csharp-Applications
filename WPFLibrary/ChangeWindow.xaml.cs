using System.Windows;
using System.Windows.Automation;
using WPFLibrary.Enums;
using WPFLibrary.Extensions;
using WPFLibrary.Managers;
using WPFLibrary.Record_Objects;
using static WPFLibrary.Extensions.RecordExtensions;

namespace WPFLibrary;

public partial class ChangeWindow : Window
{
    private readonly BookRecord m_BookRecord;
    private readonly MovieRecord m_MovieRecord;
    private readonly GameRecord m_GameRecord;
    
    /// <summary>
    /// Constructor
    /// </summary>
    public ChangeWindow(BookRecord book, MovieRecord movie, GameRecord gameRecord)
    {
        InitializeComponent();
        
        BookGenre.SetComboBoxEnum<Genre>();
        BookRating.SetComboBoxEnum<Rating>();
        MovieGenre.SetComboBoxEnum<Genre>();
        MovieRating.SetComboBoxEnum<Rating>();
        GameGenre.SetComboBoxEnum<Genre>();
        GameRating.SetComboBoxEnum<Rating>();
        
        m_BookRecord = book;
        m_MovieRecord = movie;
        m_GameRecord = gameRecord;

        BookGroupBox.IsEnabled = !string.IsNullOrEmpty(book.Title);
        MovieGroupBox.IsEnabled = !string.IsNullOrEmpty(movie.Title);
        GameGroupBox.IsEnabled = !string.IsNullOrEmpty(gameRecord.Title);
        
        SetValues(BookGroupBox.IsEnabled, MovieGroupBox.IsEnabled, GameGroupBox.IsEnabled);

        Closing += (sender, args) => { WindowManager.ItemWasChanged(null, null, null);};
    }
    
    /// <summary>
    /// Sets the start values of boxes for the item selected to change
    /// </summary>
    /// <param name="book">If true enables book group</param>
    /// <param name="movie">If true enables movie group</param>
    /// <param name="game">If true enables game group</param>
    private void SetValues(bool book, bool movie, bool game)
    {
        if (book)
        {
            BookTitle.Text = m_BookRecord.Title;
            BookAuthor.Text = m_BookRecord.Author;
            BookGenre.SelectedIndex = (int)m_BookRecord.Genre;
            BookPageCount.Text = m_BookRecord.Pages.ToString();
            BookRating.SelectedIndex = (int)m_BookRecord.Rating;
        }

        if (movie)
        {
            MovieTitle.Text = m_MovieRecord.Title;
            MovieActor.Text = m_MovieRecord.MovieActor;
            MovieGenre.SelectedIndex = (int)m_MovieRecord.Genre;
            MovieDuration.Text = m_MovieRecord.Duration.ToString();
            MovieRating.SelectedIndex = (int)m_MovieRecord.Rating;
        }

        if (game)
        {
            GameTitle.Text = m_GameRecord.Title;
            Publisher.Text = m_GameRecord.Publisher;
            GameGenre.SelectedIndex = (int)m_GameRecord.Genre;
            Developer.Text = m_GameRecord.Developer;
            GameRating.SelectedIndex = (int)m_GameRecord.Rating;
        }
    }

    /// <summary>
    /// Tries to change items when clicking on OK button, then closes the window
    /// </summary>
    private void Ok_Click(object sender, RoutedEventArgs e)
    {
        if (BookGroupBox.IsEnabled) ChangeBook();
        if (MovieGroupBox.IsEnabled) ChangeMovie();
        if (GameGroupBox.IsEnabled) ChangeGame();
    }

    /// <summary>
    /// Creates a new BookRecord with the new values but keeping the same Guid
    /// </summary>
    private void ChangeBook()
    {
        if (!CreateBookRecord(BookTitle.Text, BookAuthor.Text, BookPageCount.Text,
                (Genre)BookGenre.SelectedItem,
                (Rating)BookRating.SelectedItem, out BookRecord? bookRecord, m_BookRecord.Guid)) return;
        
        WindowManager.ItemWasChanged(bookRecord, null, null);
    }

    /// <summary>
    /// Creates a new MovieRecord with the new values but keeping the same Guid
    /// </summary>
    private void ChangeMovie()
    {
        if (!CreateMovieRecord(MovieTitle.Text, MovieActor.Text, MovieDuration.Text, (Genre)MovieGenre.SelectedItem,
                (Rating)MovieRating.SelectedItem, out MovieRecord? movieRecord, m_MovieRecord.Guid)) return;
        
        WindowManager.ItemWasChanged(null, movieRecord, null);
    }

    /// <summary>
    /// Creates a new GameRecord with the new values but keeping the same Guid
    /// </summary>
    private void ChangeGame()
    {
        if (!CreateGameRecord(GameTitle.Text, Publisher.Text, Developer.Text, (Genre)GameGenre.SelectedItem,
                (Rating)GameRating.SelectedItem, out GameRecord? gameRecord, m_GameRecord.Guid)) return;
        
        WindowManager.ItemWasChanged(null, null, gameRecord);
    }

    /// <summary>
    /// Closes the window
    /// </summary>
    private void Cancel_Click(object sender, RoutedEventArgs e)
    {
        MessageBoxResult result = MessageBox.Show("Close window?", "Cancel", MessageBoxButton.YesNo, MessageBoxImage.Information);
        if (result == MessageBoxResult.No) return;
        
        WindowManager.ItemWasChanged(null, null, null);
        Close();
    }
}