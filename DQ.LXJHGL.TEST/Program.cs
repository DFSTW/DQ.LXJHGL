using DQ.LXJHGL.CLT;
using DQ.LXJHGL.COMMON;
using DQ.LXJHGL.SVR;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Windows.Forms;
using Thyt.TiPLM.CLT.Admin.BPM;
using Thyt.TiPLM.UIL.Common;
using Thyt.TiPLM.UIL.Product;
using Thyt.TiPLM.PLL.Common;


namespace DQ.LXJHGL.TEST
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            //TestSvr();
            
            //List<LXJHGLInstance> x = new List<LXJHGLInstance>();
            //var type = x.GetType();
            //Console.WriteLine(type.IsSerializable);
            //var form = new BinaryFormatter();
            // throws
            //form.Serialize(new MemoryStream(), x);
            
            TestClt();

            //Test GetUserType
            //var x = new LXJHGLSVR();
            //var type = x.GetUserType("15600");
            //type = x.GetUserType("02309");
            //type = x.GetUserType("11621");

            //TEST IMPORT
            //string file = "路线任务2015-10-16.xls";
            //var result = TestExcelImport(file);
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
        public static ILXJHGLAddin Agent = null;
        private static void TestClt()
        {
            
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            bool isLoged = Login();
            if (!isLoged) return;

            //加入判断权限
         
            LXJHGLCLT client = new LXJHGLCLT();
            //client.Activate();
            LXJHGLCLT.Agent = RemoteProxy.GetObject(typeof(ILXJHGLAddin),
                                          ConstLXJHGL.RemotingURL) as ILXJHGLAddin;

            var userType = LXJHGLCLT.Agent.GetUserType(ClientData.LogonUser.LogId);
                if (userType == LXJHGLUserType.组长)
                {
                    zzjm.showzzjm();
                    Application.Run(zzjm.ZZJM);
                }
                else if (userType == LXJHGLUserType.组员)
                {
                    zyjm.showzyjm();
                    Application.Run(zyjm.ZYJM);
                }
            
            //SomeForm.ShowForm();
            //Application.Run(SomeForm.someForm);
        }
        /// <summary>
        /// 初始PLM公共数据
        /// </summary>
        /// <param name="user">登陆用户</param>
        /// <returns></returns>
        private static bool Init()
        {
            try
            {
                //this.curUser = user;
                PSInit.InitPS(ClientData.LogonUser, false);
                BPMEventInit.InitBPMEvent();
                Thyt.TiPLM.UIL.TiMessage.UIMessage.Instance.InitilizeMessage(null);
                //Thyt.TiPLM.UIL.Addin.AddinDeployment.Instance.SyncAddinsWithServer();
                return true;
            }
            catch (Exception ex)
            {
                //System.Diagnostics.EventLog.WriteEntry("PLM集成控件",ex.ToString(),System.Diagnostics.EventLogEntryType.Error);
                PrintException.Print(ex);
                return false;
            }
        }

        /// <returns>
        /// 登录成功返回true
        /// </returns>
        public static bool Login()
        {
            string product = "TiDesk";
            bool isLogin = false;
            try
            {
                //product += "Unknown";
                //   MessageBox.Show("1");
                if (FrmLogon.Logon(product, true))
                {

                    if (Init())
                    {
                        isLogin = true;
                    }
                }
            }
            catch (Exception ex)
            {
                PrintException.Print(ex);
            }
            return isLogin;
        }
        /// <summary>
        /// 注销登录
        /// </summary>
    }
}
