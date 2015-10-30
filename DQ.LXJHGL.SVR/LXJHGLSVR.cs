using DQ.LXJHGL.COMMON;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting;
using System.Text;
using Thyt.TiPLM.Common;
using Thyt.TiPLM.Common.Interface.Addin;

namespace DQ.LXJHGL.SVR
{
    public class LXJHGLSVR : PLMBFRoot, ILXJHGLAddin, IAddinServiceEntry
    {
        public LXJHGLSVR() { }

        public WellKnownServiceTypeEntry[] RemoteTypes
        {
            get
            {
                WellKnownServiceTypeEntry[] entries = new WellKnownServiceTypeEntry[1];
                entries[0] = new WellKnownServiceTypeEntry(
                    typeof(LXJHGLSVR),
                    ConstLXJHGL.RemotingURL,
                    WellKnownObjectMode.SingleCall);
                return entries;
            }
        }

        List<LXJHGLInstance> ILXJHGLAddin.ImportTasks(List<LXJHGLInstance> tasks)
        {
            return new PRLXJHGLS().ImportTasks(tasks);
        }

        bool ILXJHGLAddin.UpdateTasks(List<LXJHGLInstance> tasks)
        {
            return new PRLXJHGLS().UpdateTasks(tasks);
        }


        public List<LXJHGLInstance> GetTasksByStatus(LXJHGLStatus status, string userName)
        {
            return new PRLXJHGLS().GetTasksByStatus(status,userName);
        }


        public List<LXJHGLInstance> GetAllTasks()
        {
            return new PRLXJHGLS().GetAllTasks();
        }
    }
}
