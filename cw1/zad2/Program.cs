using System;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace zad2
{
    class Program
    {
        public static async Task Main(string[] args)
        {
            var client = new HttpClient();
            Console.WriteLine("Sending GET");
            var url = args[0].Length > 0 ? args[0] : "https://www.pja.edu.pl/";
            var response = await client.GetAsync(args[0]);
            Console.WriteLine($"Response: {response.IsSuccessStatusCode}");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var regex = new Regex("[a-z]+[a-z0-9]*@[a-z0-9]+\\.[a-z]+", RegexOptions.IgnoreCase);

                var matches = regex.Matches(content);
                foreach(var match in matches)
                {
                    Console.WriteLine(match.ToString());
                }
                
            }
        }
           
    }
}
