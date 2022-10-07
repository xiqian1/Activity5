using System;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Linq.Expressions;


namespace WebAPIClient
{

    class Movie
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("director")]
        public string Director { get; set; }

        [JsonProperty("release_date")]
        public int Release_date { get; set; }
    }

    public class Type
    {
        [JsonProperty("id")]
        public string Id { get; set; }
    }

    class Program {
        private static readonly HttpClient client = new HttpClient();

        static async Task Main(string[] arg)
        {
            await ProcessRepositories();
        }
        private static async Task ProcessRepositories()
        {
            while (true)
            {
                try {
                    Console.WriteLine("Enter movie Id. Press enter without writing a name to quit the program.(Example:2baf70d1-42bb-4437-b551-e5fed5a87abe)");
                    var movieId = Console.ReadLine();
                    if (string.IsNullOrEmpty(movieId))
                    {
                        break;
                    }

                    var result = await client.GetAsync("https://ghibliapi.herokuapp.com/films/" + movieId.ToLower());
                    var resultRead = await result.Content.ReadAsStringAsync();

                    var movie = JsonConvert.DeserializeObject<Movie>(resultRead);

                    Console.WriteLine("---");
                    Console.WriteLine("Id: " + movie.Id);
                    Console.WriteLine("Description: " + movie.Description);
                    Console.WriteLine("Director: " + movie.Director);
                    Console.WriteLine("Release Date: " + movie.Release_date);
                    Console.WriteLine("\n---");
                }
                catch (Exception)
                {
                    Console.WriteLine("Enter valid a movie Id.");
                }
            }
        }
    }
}