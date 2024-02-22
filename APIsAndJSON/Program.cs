using Newtonsoft.Json.Linq;
using static System.Net.WebRequestMethods;

namespace APIsAndJSON
{
    public class Program
    {
        static void Main(string[] args)
        {
            var client = new HttpClient();
            string ronUrl = "https://ron-swanson-quotes.herokuapp.com/v2/quotes";
            var ronResponse = client.GetStringAsync(ronUrl).Result;
            var ronQuote = JArray.Parse(ronResponse).ToString().Replace('[', ' ').Replace(']', ' ').Trim();
            var kanyeURL = "https://api.kanye.rest/";
            var kanyeResponse = client.GetStringAsync(kanyeURL).Result;
            var kanyeQuote = JObject.Parse(kanyeResponse).GetValue("quote").ToString();
            
            for(int i = 0;i<5;i++) 
            {
                Console.WriteLine($"Ron:{ronQuote}\n");
                Console.WriteLine($"Kanye:{kanyeQuote}\n");
                ronResponse = client.GetStringAsync(ronUrl).Result;
                ronQuote = JArray.Parse(ronResponse).ToString().Replace('[', ' ').Replace(']', ' ').Trim();
                kanyeResponse = client.GetStringAsync(kanyeURL).Result;
                kanyeQuote = JObject.Parse(kanyeResponse).GetValue("quote").ToString();
            }
            

            //Exercise Two
            var weatherMapAPI = new OpenWeatherMapAPI();
            var apiKey = weatherMapAPI.ApiKey();
            Console.WriteLine("-------------------Exercise Two------------------------\n");
            var weatherURL = $"https://api.openweathermap.org/data/2.5/weather?lat={22.633333}&lon={120.266670}&appid={apiKey}&units=imperial";
            var baseResponse = client.GetStringAsync(weatherURL).Result;
            var temperature = JObject.Parse(baseResponse).GetValue("main")["temp"].ToString();
            var windchill = JObject.Parse(baseResponse).GetValue("main")["feels_like"].ToString();
            var cityName = JObject.Parse(baseResponse).GetValue("name").ToString();
            //Console.WriteLine(baseResponse.ToString());
            Console.WriteLine($"Today's weather report for {cityName} is a temperature of {temperature} with a windchill of {windchill}");
        }
    }
}
