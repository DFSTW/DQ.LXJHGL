using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DQ.LXJHGL.COMMON
{
    public enum LXJHGLStatus
    {
        未分配 = 1,
        未接收 = 2,
        接收 = 3,
        流程中 = 4,
        完成 = 5,
        取消 = 6
    }
    public class LXJHGLInstance
    {
        
        /// <summary>
        /// 图号
        /// </summary>
        public string Id { set; get; }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { set; get; }
        /// <summary>
        /// 版本
        /// </summary>
        public int Version { set; get; }
        /// <summary>
        /// 定版人
        /// </summary>
        public string Releaser { set; get; }
        /// <summary>
        /// 定版时间
        /// </summary>
        public DateTime Releasetime { set; get; }
        /// <summary>
        /// 类型
        /// </summary>
        public string Type { set; get; }
        /// <summary>
        /// 编制者
        /// </summary>
        public string Creator { set; get; }
        /// <summary>
        /// 计划完成时间
        /// </summary>
        public DateTime Planedtime { set; get; }
        /// <summary>
        /// 任务下达时间
        /// </summary>
        public DateTime Taskcreatime { set; get; }
        /// <summary>
        /// 执行状态
        /// </summary>
        public LXJHGLStatus Status { set; get; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Bz { set; get; }
        /// <summary>
        /// 流程信息
        /// </summary>
        public string Process { set; get; }
        /// <summary>
        /// 难易度
        /// </summary>
        public int Difficulty { set; get; }
        /// <summary>
        /// 实际完成时间
        /// </summary>
        public DateTime Completetime { set; get; }
    }
}
