using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace SVNUtilsWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RepositoryController : ControllerBase
    {
        private readonly string _rootRepository;

        public RepositoryController(IConfiguration configuration)
        {
            _rootRepository = configuration["RootRepository"];
        }

        [HttpPost]
        public async Task<bool> CreateItemAsync(string name, [FromBody] params string[] folders)
        {
            if (await SVNUtils.SvnRepo.GetRepositoryItemsAsync(_rootRepository, $"/{name}") == null)
            {
                await SVNUtils.SvnRepo.CreateRepositoryItemAsync(_rootRepository, $"/{name}");
            }

            foreach (var folder in folders)
            {
                if (await SVNUtils.SvnRepo.GetRepositoryItemsAsync(_rootRepository, $"/{name}/{folder}") == null)
                {
                    await SVNUtils.SvnRepo.CreateRepositoryItemAsync(_rootRepository, $"/{name}/{folder}");
                }
            }
            return true;
        }
    }
}
