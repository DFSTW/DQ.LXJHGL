using DQ.LXJHGL.COMMON;
using DQ.LXJHGL.SVR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DQ.LXJHGL.TEST
{
    class Program
    {
        static void Main(string[] args)
        {
            //TestSvr();

            string file = "路线任务2015-10-16.xls";
            var result = TestExcelImport(file);
        }

        private static List<LXJHGLInstance> TestExcelImport(string file)
        {
            var x = ExcelUtil.ImportTasks(file);
            ILXJHGLAddin svr = new LXJHGLSVR();
            return svr.ImportTasks(x);
        }

        private static void TestSvr()
        {
            List<LXJHGLInstance> tasks = new List<LXJHGLInstance>();
            tasks.Add(CreateTask("TEST1", "TEST1", 1, "fukang", DateTime.Now, "tuzhi", "fukang",
                DateTime.Now, DateTime.Now, string.Empty, string.Empty, 0, DateTime.Now));
            tasks.Add(CreateTask("TEST2", "TEST1", 1, "fukang", DateTime.Now, "tuzhi", "fukang",
                DateTime.Now, DateTime.Now, string.Empty, string.Empty, 0, DateTime.Now));
            tasks.Add(CreateTask("TEST2", "TEST1", 2, "fukang", DateTime.Now, "tuzhi", "fukang",
                DateTime.Now, DateTime.Now, string.Empty, string.Empty, 0, DateTime.Now));

            ILXJHGLAddin svr = new LXJHGLSVR();
            //var result = svr.ImportTasks(tasks);
            //result.ForEach(a => Console.WriteLine(a.Id + " " + a.Version));
            //svr.UpdateTasks(tasks);
            var result = svr.GetTasksByStatus(LXJHGLStatus.未分配, string.Empty);
            result = svr.GetAllTasks();
        }
        static LXJHGLInstance CreateTask(string Id, string Name, int Version, string Releaser,
            DateTime Releasetime, string Type, string Creator, DateTime Planedtime,
            DateTime Taskcreatime, string Bz, string Process, int Difficulty, DateTime Completetime)
        {
            LXJHGLInstance task = new LXJHGLInstance();
            task.Id = Id;
            task.Name = Name;
            task.Version = Version;
            task.Releaser = Releaser;
            task.Releasetime = Releasetime;
            task.Type = Type;
            task.Creator = Creator;
            task.Planedtime = Planedtime;
            task.Taskcreatime = Taskcreatime;
            task.Status = LXJHGLStatus.未分配;
            task.Bz = Bz;
            task.Process = Process;
            task.Difficulty = Difficulty;
            task.Completetime = Completetime;
            return task;
        }
    }
}
