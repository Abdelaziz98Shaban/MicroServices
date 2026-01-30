using PlatformService.Models;

namespace PlatformService.Data;

public interface IPlatformRepo
{
    void Create(Platform platform);
    IEnumerable<Platform> GetAll();
    Platform? GetById(int id);
    bool SaveChanges();
}
