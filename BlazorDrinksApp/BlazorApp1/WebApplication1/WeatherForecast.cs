namespace WebApplication1
{
    public class WeatherForecast
    {
        public DateOnly m_Date { get; set; }

        public int m_TemperatureC { get; set; }

        public int m_TemperatureF => 32 + (int)(m_TemperatureC / 0.5556);

        public string? m_Summary { get; set; }
    }
}
