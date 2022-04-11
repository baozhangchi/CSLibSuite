using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SVNUtils.Models;

namespace SVNUtilsWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccessRuleController : ControllerBase
    {
        [HttpGet("list")]
        public async Task<List<RuleInfo>> GetRulesAsync(string repository, string path)
        {
            return await SVNUtils.SvnRule.GetRulesAsync(repository, path);
        }

        [HttpPost("add")]
        public async Task<bool> AddRuleAsync(string repository, string path, string accountId, Rule rule)
        {
            await SVNUtils.SvnRule.AddRuleAsync(repository, path, accountId, rule);
            return true;
        }

        [HttpPost("update")]
        public async Task<bool> UpdateRuleAsync(string repository, string path, string accountId, Rule rule)
        {
            await SVNUtils.SvnRule.UpdateRuleAsync(repository, path, accountId, rule);
            return true;
        }

        [HttpDelete("remove")]
        public async Task<bool> RemoveAsync(string repository, string path, string accountId)
        {
            await SVNUtils.SvnRule.RemoveAsync(repository, path, accountId);
            return true;
        }
    }
}
