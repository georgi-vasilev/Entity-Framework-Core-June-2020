namespace JsonDemo
{
    using System;
    using System.IO;
    using System.Text.Json;
    using Newtonsoft.Json;

    public class StartUp
    {
        public static void Main(string[] args)
        {
            var weather = new WeatherForecast();
            //string json = JsonSerializer.Serialize(weather);
            //File.WriteAllText("weather.json", json);
            //Console.WriteLine(json);

            //var jsonFromFile = File.ReadAllText("weather.json");
            //var forecast = JsonSerializer.Deserialize<WeatherForecast>(jsonFromFile);
            //Console.WriteLine(JsonConvert.SerializeObject(weather, Formatting.Indented));

            var json = File.ReadAllText("weather.json");
            var forecast = JsonConvert.DeserializeObject<WeatherForecast>(json);
        }
    }
}
