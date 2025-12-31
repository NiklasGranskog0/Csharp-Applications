using System.Windows.Threading;

namespace Airport_Simulator;

public class AirPlane
{
    private readonly DispatcherTimer m_DispatcherTimer;

    public bool CanLand { get; private set; }
    public required string Destination { get; set; }
    public required string FirstDestination { get; init; }
    public required string FlightID { get; set; }
    public double FlightTime { get; init; }
    private double m_TimeInAir;
    private TimeOnly LocalTime { get; set; }
    public required string Name { get; init; }
    public int Altitude { get; set; }

    public required EventHandler<AirPlaneEventArgs>? OnEventLanded;
    public required EventHandler<AirPlaneEventArgs>? OnEventTakeOff;
    
    public delegate int OnAltitudeChangeDelegate(int altitude);
    public OnAltitudeChangeDelegate AltitudeCallback;

    public AirPlane()
    {
        m_DispatcherTimer = new DispatcherTimer();
        m_DispatcherTimer.Tick += DispatcherTimer_Tick;
        LocalTime = new TimeOnly(DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
        Altitude = 36000;
    }
    
    private void DispatcherTimer_Tick(object? sender, EventArgs e)
    {
        m_TimeInAir += 1.0;

        if (m_TimeInAir >= FlightTime) OnLanding();
    }

    private void OnLanding()
    {
        m_DispatcherTimer.Stop();
        LocalTime = LocalTime.AddHours(m_TimeInAir);
        m_TimeInAir = 0;

        OnEventLanded?.Invoke(Name,
            new AirPlaneEventArgs($"{Name}({FlightID}), has landed in {Destination}, {LocalTime.ToLongTimeString()}"));

        Destination = Destination.Equals("Home") ? FirstDestination : "Home";

        CanLand = false;
    }

    public void OnTakeOff()
    {
        OnEventTakeOff?.Invoke(Name,
            new AirPlaneEventArgs(
                $"The aircraft {Name}({FlightID}) is taking off, destination {Destination}, {LocalTime.ToLongTimeString()}"));
    }

    public void SetupTimer()
    {
        m_DispatcherTimer.Interval = new TimeSpan(0, 0, 1);
        m_DispatcherTimer.Start();
        CanLand = true;
    }
    
    public void PauseTimer() => m_DispatcherTimer.IsEnabled = false;
    public void ResumeTimer() => m_DispatcherTimer.IsEnabled = true;

    public override string ToString()
    {
        return $"{Name} {FlightID} {Destination} {FlightTime}";
    }
}