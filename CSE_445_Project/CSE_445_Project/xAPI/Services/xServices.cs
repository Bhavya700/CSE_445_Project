using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

public class xServices
{
    private readonly HttpClient _httpClient;
    private readonly string _bearerToken; 
    private const string ApiUrl = "https://api.twitter.com/2/tweets/search/recent";
    private const int MAX_RESULTS = 10; //unfortunately it can only be 10, i was hoping it could receive just 1 and my trial of like 100 results wouldn't be eat up. 
    //as of this comment, im 50/100, w/ a reset of 15 minutes between each call. I have the calls being saved twice, to two locations (one will display in the try it page, another goes to 'tweets.json' in this xAPI project
    //please be aware of this fact, and check either one of those locations for your calls, because if you try calling again X API will deny the request. 

    public xServices(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;

        // Decode the token properly
        _bearerToken = Uri.UnescapeDataString(configuration["TwitterApi:BearerToken"] ?? string.Empty); //get the bearer token from our appSettings

        if (string.IsNullOrWhiteSpace(_bearerToken))
        {
            throw new Exception("Twitter API Bearer Token is missing. Check appsettings.json.");
        }

        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _bearerToken); //auth. using our bearer token 
    }

    public async Task<string> GetTweetsAsync(string query)
    {
        string url = $"{ApiUrl}?query={Uri.EscapeDataString(query)}&max_results={MAX_RESULTS}"; //add the base URL + our query + the amnt of results (rn only 10) 

        try
        {
            var response = await _httpClient.GetAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"X/Twitter API error: {response.StatusCode} - {await response.Content.ReadAsStringAsync()}");
            }

            string jsonResponse = await response.Content.ReadAsStringAsync();
            await SaveResponseToFile(jsonResponse); //first stage of saving file as desc. above. Saved to this project under 'tweets.json'
            return jsonResponse; //return the read json response from service
        }
        catch (HttpRequestException ex)
        {
            throw new Exception("Error will connecting to X API (Twitter API, whatever you wan't to call it) : ", ex);
        }
    }

    private async Task SaveResponseToFile(string jsonInfo)
    {
        string filePath = "tweets.json";
        await File.WriteAllTextAsync(filePath, jsonInfo);
    }
}
