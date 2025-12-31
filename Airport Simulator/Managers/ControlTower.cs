using System.Windows;
using System.Windows.Controls;

namespace Airport_Simulator.Managers;

public class ControlTower
{
    private readonly ListManager<AirPlane> m_Flights;
    private readonly ListBox m_DestinationListBox;

    public ControlTower(ListBox destinationListBox)
    {
        m_DestinationListBox = destinationListBox;
        m_Flights = new();
    }

    public void AddPlane(AirPlane plane)
    {
        m_Flights.Add(plane);
        plane.OnEventTakeOff += OnDisplayInfo;
        plane.OnEventLanded += OnDisplayInfo;
        plane.AltitudeCallback += OnAltitudeChanged;
    }

    private int OnAltitudeChanged(int altitude)
    {
        StopAllFlights(false);

        return altitude;
    }

    private void OnDisplayInfo(object? sender, AirPlaneEventArgs e)
    {
        m_DestinationListBox.Items.Add(e.Message);
    }

    public void OrderAltitudeChange(int index, int altitude)
    {
        AirPlane plane = m_Flights.Get(index);

        if (!plane.CanLand)
        {
            MessageBox.Show($"{plane.Name}({plane.FlightID}) this airplane is not airborne.", "Error",
                MessageBoxButton.OK, MessageBoxImage.Error);
            return;
        }

        if (plane.Altitude == plane.AltitudeCallback(altitude)) return;
        
        StopAllFlights();
        plane.Altitude = plane.AltitudeCallback(altitude);
        OnDisplayInfo(plane.Name,
            new AirPlaneEventArgs($"{plane.Name}({plane.FlightID}), has changed its altitude to {plane.Altitude}."));
    }

    public void OrderTakeOff(int index)
    {
        AirPlane plane = m_Flights.Get(index);
        if (plane.CanLand) return;

        plane.SetupTimer();
        plane.OnTakeOff();
    }

    private void StopAllFlights(bool stop = true)
    {
        for (int i = 0; i < m_Flights.Count; i++)
        {
            AirPlane plane = m_Flights.Get(i);

            switch (stop)
            {
                case true:
                    plane.PauseTimer();
                    break;
                case false:
                    plane.ResumeTimer();
                    break;
            }
        }
    }
}