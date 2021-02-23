namespace JsonDemo
{
    using System;

    class WeatherForecast
    {
        public DateTime Date { get; set; } = DateTime.Now; //equivalent of writing this.Date = DateTime.Now in the constructor. C# 7 or 8??

        public int TemperatureC { get; set; } = 30;

        public string Summary { get; set; } = "Hot summer day.";
    }
}
