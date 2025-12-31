namespace Airport_Simulator;

public class AirPlaneEventArgs(string message) : EventArgs
{
    public string Message { get; } = message;
}