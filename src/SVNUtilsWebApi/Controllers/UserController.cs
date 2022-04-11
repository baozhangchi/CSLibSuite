using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SVNUtils.Models;

namespace SVNUtilsWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {/// <summary>
     /// 获取所有用户
     /// </summary>
     /// <returns></returns>
        [HttpGet("list")]
        public async Task<List<MemberInfo>> GetListAsync()
        {
            return await SVNUtils.SvnUser.GetUsersAsync();
        }

        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="password">密码</param>
        /// <returns></returns>
        [HttpPost("add")]
        public async Task<bool> AddUserAsync(string userName, string password)
        {
            await SVNUtils.SvnUser.CreateUserAsync(userName, password);
            return true;
        }

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <returns></returns>
        [HttpDelete("delete")]
        public async Task<bool> DeleteUserAsync(string userName)
        {
            MemberInfo item = await SVNUtils.SvnUser.GetUserAsync(userName);
            if (item == null)
            {
                return true;
            }

            await SVNUtils.SvnUser.DeleteUserAsync(userName);
            return true;
        }

        /// <summary>
        /// 修改用户密码
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="password">新密码</param>
        /// <returns></returns>
        [HttpPost("update")]
        public async Task<bool> UpdateUserPasswordAsync(string userName, string password)
        {
            var item = await SVNUtils.SvnUser.GetUserAsync(userName);
            if (item == null)
            {
                return await AddUserAsync(userName, password);
            }

            await SVNUtils.SvnUser.UpdateUserPasswordAsync(userName, password);
            return true;
        }

        /// <summary>
        /// 获取用户所有权限
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        [HttpGet("rules")]
        public async Task<List<RuleInfo>> GetRulesAsync(string userName)
        {
            List<RuleInfo> rules = new List<RuleInfo>();
            var groupConfigFile = SVNUtils.SvnConfig.GroupConfigFile;
            Regex regex = new Regex(@"(?<group>\S+)=(?<members>\S+)");
            var content = System.IO.File.ReadAllText(groupConfigFile);
            if (regex.IsMatch(content))
            {
                var matches = regex.Matches(content);
                foreach (Match match in matches)
                {
                    var members = match.Groups["members"].Value;
                    if (members.Split(',').Any(x => x.Equals(userName)))
                    {
                        rules.AddRange(await SVNUtils.SvnRule.GetRulesByAccountIdAsync($"@{match.Groups["group"].Value}"));
                    }
                }
            }

            return rules;
        }
    }
}
