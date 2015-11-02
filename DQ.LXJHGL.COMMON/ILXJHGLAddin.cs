using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DQ.LXJHGL.COMMON
{
    public class ConstLXJHGL
    {
        public const string RemotingURL = "DQ/Common/LXJHGL.rem";
    }
    public enum LXJHGLUserType
    {
        其他 = 1,
        组员 = 2,
        组长 = 3,
    }
    public interface ILXJHGLAddin
    {
        /// <summary>
        /// 导入路线任务,返回重复的
        /// </summary>
        /// <param name="tasks"></param>
        /// <returns></returns>
        List<LXJHGLInstance> ImportTasks(List<LXJHGLInstance> tasks);

        /// <summary>
        /// 更新路线任务
        /// </summary>
        /// <param name="tasks"></param>
        /// <returns></returns>
        bool UpdateTasks(List<LXJHGLInstance> tasks);

        List<LXJHGLInstance> GetTasksByStatus(LXJHGLStatus status, string userName);

        List<LXJHGLInstance> GetAllTasks();
        //List<LXJHGLInstance> GetTasksByUser(string user);
        //List<LXJHGLInstance> GetTasksByTime(DateTime fromtime, DateTime totime);

        LXJHGLUserType GetUserType(string userLogId);
    } 
}
