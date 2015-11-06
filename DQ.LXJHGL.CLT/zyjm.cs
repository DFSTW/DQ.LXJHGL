using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DQ.LXJHGL.COMMON;
using Thyt.TiPLM.UIL.Common;

namespace DQ.LXJHGL.CLT
{
    public partial class zyjm : Form
    {
        public static zyjm ZYJM;
        public zyjm()
        {
            InitializeComponent();
        }
        public static void showzyjm()
        {

            if ((ZYJM == null) || ZYJM.IsDisposed)
                ZYJM = new zyjm();

            ZYJM.MdiParent = ClientData.mainForm;
            ZYJM.Show();
            ZYJM.Activate();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }
    }
}
