using DQ.LXJHGL.COMMON;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Thyt.TiPLM.UIL.Common;

namespace DQ.LXJHGL.CLT
{
    public partial class SomeForm : Form
    {
        public static SomeForm someForm;

        private List<LXJHGLInstance> Tasks;

        public SomeForm()
        {
            InitializeComponent();
        }
        public static void ShowForm(){

            if ((someForm == null) || someForm.IsDisposed)
                someForm = new SomeForm();

            someForm.MdiParent = ClientData.mainForm;
            someForm.Show();
            someForm.Activate();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Create an instance of the open file dialog box.
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            // Set filter options and filter index.
            openFileDialog1.Filter = "Excel File (.xls)|*.xls|All Files (*.*)|*.*";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.Multiselect = false;

            // Call the ShowDialog method to show the dialog box.
            var userClickedOK = openFileDialog1.ShowDialog();

            // Process input if the user clicked OK.
            if (userClickedOK == DialogResult.OK)
            {
                // Open the selected file to read.
                string file = openFileDialog1.FileName;
                Tasks = ExcelUtil.ImportTasks(file);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var result = LXJHGLCLT.Agent.ImportTasks(Tasks);
        }

        private void SomeForm_Load(object sender, EventArgs e)
        {

        }
    }
}
