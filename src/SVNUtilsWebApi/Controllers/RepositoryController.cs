using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace SVNUtilsWebApi.Controllers
{
    /// <summary>
    /// 存储库相关操作(公司项目用)
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class RepositoryController : ControllerBase
    {
        private readonly string _rootRepository;

        /// <inheritdoc />
        public RepositoryController(IConfiguration configuration)
        {
            _rootRepository = configuration["RootRepository"];
        }

        /// <summary>
        /// 创建仓库成员
        /// </summary>
        /// <param name="name"></param>
        /// <param name="folders"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<bool> CreateItemAsync(string name, [FromBody] params string[] folders)
        {
            await SVNUtils.SvnRepo.CreateRepositoryItemIfNotExistAsync(_rootRepository, $"/{name}");

            foreach (var folder in folders)
            {
                await SVNUtils.SvnRepo.CreateRepositoryItemIfNotExistAsync(_rootRepository, $"/{name}/{folder}");
            }
            return true;
        }
    }
}
