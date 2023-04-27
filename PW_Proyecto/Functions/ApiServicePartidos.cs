using Newtonsoft.Json;
using PW_Proyecto.Models;
using System.Text;

namespace PW_Proyecto.Functions
{

        public static class APIServicePartidos
        {
            public static int timeout = 30;
            public static string baseurl = "https://localhost:7073/api/Partidos";

            public static async Task<IEnumerable<Partido>> GetPartidos()
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
                    return JsonConvert.DeserializeObject<IEnumerable<Partido>>(await response.Content.ReadAsStringAsync());
                }
                else
                {
                    throw new Exception(response.StatusCode.ToString());
                }
            }

        public static async Task<IEnumerable<Partido>> GetPartidosFilterTorneo(int id)
        {
            HttpClientHandler clientHandler = new()
            {
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }
            };

            HttpClient httpClient = new(clientHandler)
            {
                Timeout = TimeSpan.FromSeconds(timeout)
            };

            HttpResponseMessage response = await httpClient.GetAsync($"{baseurl}/torneo/{id}");

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return JsonConvert.DeserializeObject<IEnumerable<Partido>>(await response.Content.ReadAsStringAsync());
            }
            else
            {
                throw new Exception(response.StatusCode.ToString());
            }
        }

        public static async Task<Partido> PostPartido(Partido object_to_serialize)
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
                    return JsonConvert.DeserializeObject<Partido>(await response.Content.ReadAsStringAsync());
                }

                else

                {
                    throw new Exception(response.StatusCode.ToString());
                }
            }

            public static async Task<Partido> GetPartido(int id)
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
                    return JsonConvert.DeserializeObject<Partido>(await response.Content.ReadAsStringAsync());
                }

                else

                {
                    throw new Exception(response.StatusCode.ToString());
                }
            }
            public static async Task<Partido> PutPartido(Partido object_to_serialize, int id)
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
                    return JsonConvert.DeserializeObject<Partido>(await response.Content.ReadAsStringAsync());
                }

                else

                {
                    throw new Exception(response.StatusCode.ToString());
                }
            }

            public static async Task<Partido> DeletePartido(int id)
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
                    return JsonConvert.DeserializeObject<Partido>(await response.Content.ReadAsStringAsync());
                }

                else

                {
                    throw new Exception(response.StatusCode.ToString());
                }
            }

        }
}
