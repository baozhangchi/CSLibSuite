using PowershellHost;
using SVNUtils.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SVNUtils
{
    /// <summary>
    /// SVN用户操作类
    /// </summary>
    public static class SvnUser
    {
        // ReSharper disable once InconsistentNaming
        private static readonly object _lock = new object();

        /// <summary>
        /// 获取所有用户
        /// </summary>
        /// <returns><see cref="MemberInfo"/>的集合</returns>
        // ReSharper disable once MemberCanBePrivate.Global
        public static async Task<List<MemberInfo>> GetUsersAsync()
        {
            var ps = CustomHostedRunspace.Default;
            var result = await ps.RunCommandAsync("Get-SvnLocalUser");
            return result.ToList<MemberInfo>();
        }

        public static async Task<MemberInfo> GetUserAsync(string userName)
        {
            var ps = CustomHostedRunspace.Default;
            var result = await ps.RunCommandAsync($"Get-SvnLocalUser", new Dictionary<string, object> { { "Name", userName } });
            return result.ToList<MemberInfo>().FirstOrDefault();
        }


        /// <summary>
        /// 创建用户
        /// </summary>
        /// <param name="name">用户名</param>
        /// <param name="password">密码</param>
        public static async Task CreateUserAsync(string name, string password)
        {
            SecureString secureString = password.ToSecureString();
            var ps = CustomHostedRunspace.Default;
            await ps.RunCommandAsync($"New-SvnLocalUser", new Dictionary<string, object>() { { "Name", name }, { "Password", secureString } });
        }

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="userId">用户名</param>
        /// <returns></returns>
        public static async Task DeleteUserAsync(string userId)
        {
            var ps = CustomHostedRunspace.Default;
            await ps.RunCommandAsync($"Remove-SvnLocalUser", new Dictionary<string, object>() { { "Id", userId } });
        }

        /// <summary>
        /// 修改用户密码
        /// </summary>
        /// <param name="userId">用户名</param>
        /// <param name="password">密码</param>
        public static async Task UpdateUserPasswordAsync(string userId, string password)
        {
            if (string.IsNullOrEmpty(userId))
            {
                throw new ArgumentException($"用户名不能为 null 或空。", nameof(userId));
            }

            if (string.IsNullOrEmpty(password))
            {
                throw new ArgumentException($"新密码不能为 null 或空。", nameof(password));
            }

            SecureString secureString = password.ToSecureString();
            var ps = CustomHostedRunspace.Default;
            await ps.RunCommandAsync($"Set-SvnLocalUser", new Dictionary<string, object>() { { "Id", userId }, { "Password", secureString } });
        }

        /// <summary>
        /// 修改用户密码
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="oldPassword">旧密码</param>
        /// <param name="newPassword">新密码</param>
        public static void UpdateUserPassword(string userId, string oldPassword, string newPassword)
        {
            if (string.IsNullOrEmpty(userId))
            {
                throw new ArgumentException($"用户名不能为 null 或空。", nameof(userId));
            }

            if (string.IsNullOrEmpty(oldPassword))
            {
                throw new ArgumentException($"旧密码不能为 null 或空。", nameof(oldPassword));
            }

            if (string.IsNullOrEmpty(newPassword))
            {
                throw new ArgumentException($"新密码不能为 null 或空。", nameof(newPassword));
            }

            lock (_lock)
            {
                var lines = File.ReadAllLines(SvnConfig.UserPasswordConfigFile);
                var index = lines.ToList().FindIndex(x => x.StartsWith(userId) || x.StartsWith($"#disabled#{userId}"));
                if (index < 0)
                {
                    throw new Exception($"用户 {userId} 不存在。");
                }

                var line = lines[index];
                var array = line.Split(':');
                if (!PasswordHelper.CheckPassword(oldPassword, array[1]))
                {
                    throw new Exception("旧密码输入错误");
                }

                lines[index] = $"{array[0]}:{PasswordHelper.Crypt(newPassword)}";
                File.WriteAllLines(SvnConfig.UserPasswordConfigFile, lines);
            }
        }
    }
}
