using System.Windows;
using Airport_Simulator.Managers;

namespace Airport_Simulator;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private readonly ControlTower m_ControlTower;

    public MainWindow()
    {
        InitializeComponent();

        m_ControlTower = new ControlTower(ArrivalDestination);

        FlightName.TextAlignment = TextAlignment.Center;
        FlightID.TextAlignment = TextAlignment.Center;
        FlightDestination.TextAlignment = TextAlignment.Center;
        FlightTime.TextAlignment = TextAlignment.Center;
    }

    private void AddPlane_Click(object sender, RoutedEventArgs e)
    {
        if (string.IsNullOrEmpty(FlightName.Text) ||
            string.IsNullOrEmpty(FlightID.Text) ||
            string.IsNullOrEmpty(FlightDestination.Text) ||
            !double.TryParse(FlightTime.Text, out double flightTime) || flightTime < 0.1)
        {
            MessageBox.Show("Please enter valid flight information!", "Error", MessageBoxButton.OK,
                MessageBoxImage.Error);
            return;
        }

        // if (m_ControlTower.Flights.Any(airplane => airplane.FlightID == FlightID.Text))
        // {
        //     MessageBox.Show("This Flight ID already exists", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        //     return;
        // }

        string plane = $"{FlightName.Text}, {FlightID.Text}, heading for {FlightDestination.Text}";

        AirPlane newPlane = new AirPlane
        {
            Name = FlightName.Text,
            FlightID = FlightID.Text,
            Destination = FlightDestination.Text,
            FlightTime = flightTime,
            FirstDestination = FlightDestination.Text,
            OnEventLanded = null,
            OnEventTakeOff = null,
        };

        AirPlaneInformation.Items.Add(plane);
        m_ControlTower.AddPlane(newPlane);
        AirPlaneListView.VerticalContentAlignment = VerticalAlignment.Top;
        AirPlaneListView.HorizontalContentAlignment = HorizontalAlignment.Left;
        AirPlaneListView.Items.Add(plane);
    }

    private void PlaneTakeOff_Click(object sender, RoutedEventArgs e)
    {
        if (AirPlaneInformation.SelectedItem == null) return;
        m_ControlTower.OrderTakeOff(AirPlaneInformation.SelectedIndex);
    }

    private void UpdateGUI() { }

    private void PlaneAltitudeChange_Click(object sender, RoutedEventArgs e)
    {
        if (AirPlaneInformation.SelectedItem == null) return;
        
        if (!int.TryParse(AltitudeText.Text, out int altitude) || altitude < 0)
        {
            MessageBox.Show("Invalid Altitude number. (Textbox right of the button)", "Error", MessageBoxButton.OK,
                MessageBoxImage.Error);
            return;
        }
        
        m_ControlTower.OrderAltitudeChange(AirPlaneInformation.SelectedIndex, altitude);
    }
}