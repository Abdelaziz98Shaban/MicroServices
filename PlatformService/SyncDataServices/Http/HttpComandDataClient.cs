using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using PlatformService.Dtos;

namespace platformservice.SyncDataServices.Http;

public class HttpComandDataClient(HttpClient httpClient, IConfiguration configuration) : ICommandDataClient
{
    private readonly HttpClient _httpClient = httpClient;
    private readonly IConfiguration _configuration = configuration;

    public async Task SendPlatformToCommand(PlatformReadDto platform)
    {
      var content = new StringContent(
        JsonSerializer.Serialize(platform),
        Encoding.UTF8,
        "application/json"
      );

        var response = await _httpClient.PostAsync($"{_configuration["CommandService"]}/api/c/platforms", content);

        if (response.IsSuccessStatusCode)
        {
            Console.WriteLine("--> Sync POST to CommandService was OK!");
        }
        else
        {
            Console.WriteLine("--> Sync POST to CommandService was NOT OK!");
        }
    }
}
