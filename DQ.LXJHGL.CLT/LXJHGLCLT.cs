using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Thyt.TiPLM.Common.Interface.Addin;
using Thyt.TiPLM.UIL.Common;

namespace DQ.LXJHGL.CLT
{
    public class LXJHGLCLT : IAddinClientEntry, ISystemClientAddin
    {
        public void Activate()
        {
            try
            {
                //sth;
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
