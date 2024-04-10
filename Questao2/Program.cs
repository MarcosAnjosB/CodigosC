using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class Program
{
    public static void Main()
    {
        string teamName = "Paris Saint-Germain";
        int year = 2013;
        Task<int> totalGoals = getTotalScoredGoalsAsync(teamName, year);

        Console.WriteLine("Team "+ teamName +" scored "+ totalGoals.Result.ToString() + " goals in "+ year);

        teamName = "Chelsea";
        year = 2014;
        totalGoals = getTotalScoredGoalsAsync(teamName, year);

        Console.WriteLine("Team " + teamName + " scored " + totalGoals.Result.ToString() + " goals in " + year);

        // Output expected:
        // Team Paris Saint - Germain scored 109 goals in 2013
        // Team Chelsea scored 92 goals in 2014
    }

    public static async Task<int> getTotalScoredGoalsAsync(string team, int year)
    {
        string apiUrl = "https://jsonmock.hackerrank.com/api/football_matches?year=" + year + "&team1=" + team;

        // Instanciar o HttpClient
        using (HttpClient client = new HttpClient())
        {
            try
            {
                // Requisição GET para a API
                HttpResponseMessage response = client.GetAsync(apiUrl).Result;

                // Verificar se a requisição foi bem-sucedida
                if (response.IsSuccessStatusCode)
                {
                    // String recebe conteúdo da resposta. Obs: Forcei a resposta para não ser Assíncrona
                    string responseBody = response.Content.ReadAsStringAsync().Result;

                    //// Imprimir o conteúdo da resposta
                    //Console.WriteLine("Resposta da API:");
                    //Console.WriteLine(responseBody);

                    // Desserializar resposta
                    var jsonObject = JObject.Parse(responseBody);

                    // Obter a lista
                    var data = jsonObject["data"];

                    int somaGols = 0;

                    // Iterar sobre os objetos e calcular o somatório dos valores team1goals
                    foreach (var item in data)
                    {
                        somaGols += int.Parse(item["team1goals"].ToString());
                    }

                    return somaGols;
                }
                else
                {
                    Console.WriteLine($"Erro ao fazer a requisição: {response.StatusCode}");

                    return 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ocorreu um erro: {ex.Message}");

                return 0;
            }
        }

        
    }

}