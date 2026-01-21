using System.Net.Http.Json;
using System.Text.Json;

namespace GithubUserActivity;

class Program
{
    public class GithubEvent
    {
        public string Id { get; set; }
        public string Type { get; set; }
    }
    
    static async Task Main(string[] args)
    {
        // Checks an arg was provided
        if (args.Length != 1)
        {
            Console.WriteLine("Please provide a username");
            return;
        }
        
        // Create the http client
        using HttpClient client = new();
        
        // Sets up for calling the web api
        client.DefaultRequestHeaders.Add("Accept", "application/vnd.github+json");
        client.DefaultRequestHeaders.Add("User-Agent", "GithubUserActivityTest");
        string path = $"https://api.github.com/users/{args[0]}/events/public";

        try
        {
            // Call the api with the username provided
            var events = await client.GetFromJsonAsync<List<GithubEvent>>(path);
            if (events == null) return;
            
            // Collect data about the information
            Dictionary<string, int> eventCallTotal = new();
            foreach (var evt in events)
            {
                // Check if the event type is already in the dictionary, if so just ++
                // Else add it with a starting value of 1
                if (eventCallTotal.TryGetValue(evt.Type, out var count))
                {
                    eventCallTotal[evt.Type] = count + 1;
                }
                else
                {
                    eventCallTotal[evt.Type] = 1;
                }
            }
            
            // Display data
            var sortedByValue = eventCallTotal.OrderByDescending(evt => evt.Value);
            foreach (var evt in sortedByValue)
            {
                Console.Write($"{evt.Key}: ");
                Console.WriteLine(evt.Value);
            }
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine($"Request failed: {ex.Message}");
        }
        catch (JsonException ex)
        {
            Console.WriteLine($"Request failed: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Request failed: {ex.Message}");
        }
    }
}