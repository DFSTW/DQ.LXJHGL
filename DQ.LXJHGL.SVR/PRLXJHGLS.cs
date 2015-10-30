using DQ.LXJHGL.COMMON;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Thyt.TiPLM.BRL.Common;
using Thyt.TiPLM.BRL.Product;

namespace DQ.LXJHGL.SVR
{
    public class PRLXJHGLS : PRBase
    {
        public List<LXJHGLInstance> ImportTasks(List<LXJHGLInstance> tasks)
        {
            DALXJHGLS daTask = new DALXJHGLS(base.dbParam);

            //QRItem qrItem = new QRItem(base.dbParam);
            //foreach (var task in tasks)
            //{
            //    var item = qrItem.GetItemMaster(task.Id, "TIPART", Guid.Empty);
            //    if (item == null)
            //    {
            //        task.Bz = "PLM中不存在此零件";
            //        duplicatedItems.Add(task);
            //    }

            //}
            return daTask.ImportTasks(tasks);
        }

        internal bool UpdateTasks(List<LXJHGLInstance> tasks)
        {
            return new DALXJHGLS(base.dbParam).UpdateTasks(tasks);
        }

        internal List<LXJHGLInstance> GetTasksByStatus(LXJHGLStatus status, string userName)
        {
            return new DALXJHGLS(base.dbParam).GetTasksByStatus(status, userName);
        }

        internal List<LXJHGLInstance> GetAllTasks()
        {
            return new DALXJHGLS(base.dbParam).GetAllTasks();
        }
    }
}
