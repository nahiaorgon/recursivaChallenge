using System;
using System.Net.Http;
using System.Threading.Tasks;

class ShodanClient
{
    private static readonly HttpClient http = new HttpClient();

    public static async Task Main(string[] args)
    {
        Console.WriteLine("Ingrese una consulta para SHODAN:");
        string query = Console.ReadLine();

        Console.Write("Ingrese su API Key: ");
        string apiKey = Console.ReadLine();

        string url = $"https://api.shodan.io/shodan/host/search?key={apiKey}&query={Uri.EscapeDataString(query)}";

        try
        {
            http.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0");

            var response = await http.GetAsync(url);
            response.EnsureSuccessStatusCode();

            string json = await response.Content.ReadAsStringAsync();
            Console.WriteLine("\n=== RESULTADO SHODAN ===");
            Console.WriteLine(json);
        }
        catch (Exception ex)
        {
            Console.WriteLine("\nError realizando la consulta:");
            Console.WriteLine(ex.Message);
        }
    }
}
