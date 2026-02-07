using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using platformservice.SyncDataServices.Http;
using PlatformService.Data;
using PlatformService.Dtos;
using PlatformService.Models;

namespace PlatformService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PlatformsController : ControllerBase
{
    private readonly IPlatformRepo _repository;
    private readonly IMapper _mapper;
    private readonly ILogger<PlatformsController> _logger;
    private readonly ICommandDataClient _commandDataClient;
    public PlatformsController(IPlatformRepo repository, IMapper mapper, ILogger<PlatformsController> logger, ICommandDataClient commandDataClient)
    {
        _repository = repository;
        _mapper = mapper;
        _logger = logger;
        _commandDataClient = commandDataClient;
    }

    [HttpGet]
    public ActionResult<IEnumerable<PlatformReadDto>> GetAllPlatforms()
    {
        _logger.LogInformation("Getting all platforms");
        var platforms = _repository.GetAll();
        return Ok(_mapper.Map<IEnumerable<PlatformReadDto>>(platforms));
    }

    [HttpGet("{id}", Name = "GetPlatformById")]
    public ActionResult<PlatformReadDto> GetPlatformById(int id)
    {
        _logger.LogInformation("Getting platform with id: {Id}", id);
        var platform = _repository.GetById(id);
        if (platform == null)
        {
            return NotFound();
        }
        return Ok(_mapper.Map<PlatformReadDto>(platform));
    }

    [HttpPost]
    public async Task<ActionResult<PlatformReadDto>> CreatePlatform(PlatformCreateDto platformCreateDto)
    {
        _logger.LogInformation("Creating new platform");
        var platform = _mapper.Map<Platform>(platformCreateDto);
        _repository.Create(platform);
        _repository.SaveChanges();

        var platformReadDto = _mapper.Map<PlatformReadDto>(platform);

        // Send Sync Message
        try
        {
          await  _commandDataClient.SendPlatformToCommand(platformReadDto);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Could not send synchronously to CommandService: {ex.Message}");
        }

        return CreatedAtRoute(nameof(GetPlatformById), new { id = platformReadDto.Id }, platformReadDto);
    }
}
