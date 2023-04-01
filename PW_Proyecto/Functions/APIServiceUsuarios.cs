using Newtonsoft.Json;
using System.Text;
using PW_Proyecto.Models;

namespace PW_Proyecto.Functions
{
    public static class APIServicesUsuarios
    {
        public static int timeout = 30;
        public static string baseurl = "https://localhost:7073/api/Users";

        public static async Task<IEnumerable<User>> GetUsuarios()
        {
            HttpClientHandler clientHandler = new()
            {
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }
            };

            HttpClient httpClient = new(clientHandler)
            {
                Timeout = TimeSpan.FromSeconds(timeout)
            };

            HttpResponseMessage response = await httpClient.GetAsync(baseurl);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return JsonConvert.DeserializeObject<IEnumerable<User>>(await response.Content.ReadAsStringAsync());
            }
            else
            {
                throw new Exception(response.StatusCode.ToString());
            }
        }

        public static async Task<User> PostUsuario(User object_to_serialize)
        {
            var json_ = Newtonsoft.Json.JsonConvert.SerializeObject(object_to_serialize);
            var content = new StringContent(json_, Encoding.UTF8, "application/json");

            HttpClientHandler clientHandler = new()
            {
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }
            };

            HttpClient httpClient = new HttpClient(clientHandler)
            {
                Timeout = TimeSpan.FromSeconds(timeout)
            };
            HttpResponseMessage response = await httpClient.PostAsync($"{baseurl}", content);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)

            {
                return JsonConvert.DeserializeObject<User>(await response.Content.ReadAsStringAsync());
            }

            else

            {
                throw new Exception(response.StatusCode.ToString());
            }
        }

        public static async Task<User> GetUsuario(int id)
        {

            HttpClientHandler clientHandler = new()
            {
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }
            };

            HttpClient httpClient = new HttpClient(clientHandler)
            {
                Timeout = TimeSpan.FromSeconds(timeout)
            };
            HttpResponseMessage response = await httpClient.GetAsync($"{baseurl}/{id}");

            if (response.StatusCode == System.Net.HttpStatusCode.OK)

            {
                return JsonConvert.DeserializeObject<User>(await response.Content.ReadAsStringAsync());
            }

            else

            {
                throw new Exception(response.StatusCode.ToString());
            }
        }
        public static async Task<User> PutUsuario(User object_to_serialize, int id)
        {
            var json_ = Newtonsoft.Json.JsonConvert.SerializeObject(object_to_serialize);
            var content = new StringContent(json_, Encoding.UTF8, "application/json");

            HttpClientHandler clientHandler = new()
            {
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }
            };

            HttpClient httpClient = new HttpClient(clientHandler)
            {
                Timeout = TimeSpan.FromSeconds(timeout)
            };
            HttpResponseMessage response = await httpClient.PutAsync($"{baseurl}/{id}", content);

            if (response.StatusCode == System.Net.HttpStatusCode.Accepted)

            {
                return JsonConvert.DeserializeObject<User>(await response.Content.ReadAsStringAsync());
            }

            else

            {
                throw new Exception(response.StatusCode.ToString());
            }
        }

        public static async Task<User> DeleteUsuario(int id)
        {

            HttpClientHandler clientHandler = new()
            {
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }
            };

            HttpClient httpClient = new HttpClient(clientHandler)
            {
                Timeout = TimeSpan.FromSeconds(timeout)
            };
            HttpResponseMessage response = await httpClient.DeleteAsync($"{baseurl}/{id}");

            if (response.StatusCode == System.Net.HttpStatusCode.OK)

            {
                return JsonConvert.DeserializeObject<User>(await response.Content.ReadAsStringAsync());
            }

            else

            {
                throw new Exception(response.StatusCode.ToString());
            }
        }

    }
}
