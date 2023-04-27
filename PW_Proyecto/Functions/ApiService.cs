using PW_Proyecto.Models.Auth;
using PW_Proyecto;
using Newtonsoft.Json;
using System.Security.Policy;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using PW_Proyecto.Models;

public class ApiService
{
    private static HttpClient _client = new();
    private static HttpClientHandler _clientHandler = new();
    private static string baseurl = "https://localhost:7073/api/";

    public static string token = "";

    private static async Task<T> Get<T>(string path)
    {
        _clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
        if (!token.IsNullOrEmpty())
        {
            _client.DefaultRequestHeaders.Authorization = null;
            _client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
        }
        var response = await _client.GetAsync(path);
        int statusCode = (int)response.StatusCode;
        if (statusCode >= 200 && statusCode < 300)
        {
            return JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync())!;
        }
        else
        {
            throw new Exception(response.StatusCode.ToString());
        }
    }

    private static async Task<T> Post<T>(string path, object? data)
    {
        var json_ = JsonConvert.SerializeObject(data);
        var content = new StringContent(json_, Encoding.UTF8, "application/json");
        _clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
        if (!token.IsNullOrEmpty())
        {
            _client.DefaultRequestHeaders.Authorization = null;
            _client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
        }
        var response = await _client.PostAsync(path, content);
        int statusCode = (int)response.StatusCode;
        if (statusCode >= 200 && statusCode < 300)
        {
            return JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync())!;
        }
        else
        {
            throw new Exception(response.StatusCode.ToString());
        }
    }

    private static async Task<T> Put<T>(string path, object? data)
    {
        var json_ = JsonConvert.SerializeObject(data);
        var content = new StringContent(json_, Encoding.UTF8, "application/json");
        _clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
        if (!token.IsNullOrEmpty())
        {
            _client.DefaultRequestHeaders.Authorization = null;
            _client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
        }
        var response = await _client.PutAsync(path, content);
        int statusCode = (int)response.StatusCode;
        if (statusCode >= 200 && statusCode < 300)
        {
            return JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync())!;
        }
        else
        {
            throw new Exception(response.StatusCode.ToString());
        }
    }

    private static async Task<T> Delete<T>(string path)
    {
        _clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
        if (!token.IsNullOrEmpty())
        {
            _client.DefaultRequestHeaders.Authorization = null;
            _client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
        }
        var response = await _client.DeleteAsync(path);
        int statusCode = (int)response.StatusCode;
        if (statusCode >= 200 && statusCode < 300)
        {
            return JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync())!;
        }
        else
        {
            throw new Exception(response.StatusCode.ToString());
        }
    }

    protected async Task<T> PostNoAuth<T>(string path, object? data)
    {
        var json_ = JsonConvert.SerializeObject(data);
        var content = new StringContent(json_, Encoding.UTF8, "application/json");
        _clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
        var response = await _client.PostAsync(path, content);
        int statusCode = (int)response.StatusCode;
        if (statusCode >= 200 && statusCode < 500)
        {
            return JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync())!;
        }
        else
        {
            throw new Exception(response.StatusCode.ToString());
        }
    }

    public static async Task<UserToken?> Login(UserAuth credentials)
    {
        return await Post<UserToken?>(baseurl + "auth/login", credentials);
    }

    public static async Task<UserToken?> Register(User personalInformation)
    {
        return await Post<UserToken?>(baseurl + "auth/register", personalInformation);
    }

}
