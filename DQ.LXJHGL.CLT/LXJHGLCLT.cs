using DQ.LXJHGL.COMMON;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Thyt.TiPLM.Common.Interface.Addin;
using Thyt.TiPLM.PLL.Common;
using Thyt.TiPLM.UIL.Common;

namespace DQ.LXJHGL.CLT
{
    public class LXJHGLCLT : IAddinClientEntry, ISystemClientAddin
    {
        public static ILXJHGLAddin Agent = null;
        public void Activate()
        {
            try
            {
                Agent = RemoteProxy.GetObject(typeof(ILXJHGLAddin),
                                          ConstLXJHGL.RemotingURL) as ILXJHGLAddin;
                
                var userType = Agent.GetUserType(ClientData.LogonUser.LogId);
                if (userType == LXJHGLUserType.组长)
                {
                    SomeForm.ShowForm();
                }
                else if (userType == LXJHGLUserType.组员)
                {
                    //SomeForm2.ShowForm();
                }
            }
            catch (Exception e)
            {
                PrintException.Print(e);
            }
        }

        public void Config()
        {
            throw new NotImplementedException();
        }
    }
}
