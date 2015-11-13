using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;
using DQ.LXJHGL.COMMON;
using Thyt.TiPLM.UIL.Common;
using System.Globalization;


namespace DQ.LXJHGL.CLT
{
    public partial class zzjm : Form
    {
        //声明一个全局的DataSet
        DataSet ds = new DataSet();
        //声明一个全局的SqlDataAdapter
        SqlDataAdapter da = new SqlDataAdapter();
        //声明一个全局的SQLParameter
        SqlParameter param = new SqlParameter();
        public static zzjm ZZJM;
        private List<LXJHGLInstance> Tasks;
        public zzjm()
        {
            InitializeComponent();
        }

        public static void showzzjm()
        {

            if ((ZZJM == null) || ZZJM.IsDisposed)
                ZZJM = new zzjm();

            ZZJM.MdiParent = ClientData.mainForm;
            ZZJM.Show();
            ZZJM.Activate();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                //判断checkbox是否选中
                int count = 0;
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    if ((bool)dataGridView1.Rows[i].Cells[0].EditedFormattedValue == true)
                    {
                        count++;
                    }
                }

                if (count == 0)
                {
                    MessageBox.Show("请至少选择一条数据！", "提示");
                    return;
                }
                else
                {
                    if (MessageBox.Show(this, "您要更新数据么？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information).ToString() == "Yes")
                    {
                        List<LXJHGLInstance> updateList = new List<LXJHGLInstance>();
                        var task = new LXJHGLInstance();

                        for (int j = 0; j < dataGridView1.Rows.Count; j++)
                        {
                            if ((bool)dataGridView1.Rows[j].Cells[0].EditedFormattedValue == true)
                            {
                                //for (int k = 0; k < dataGridView1.Columns.Count; k++)
                                {

                                    Tasks[j].Creator = dataGridView1.Rows[j].Cells[7].Value.ToString();
                                    if (dataGridView1.Rows[j].Cells[11].Value != null)
                                        Tasks[j].Bz = dataGridView1.Rows[j].Cells[11].Value.ToString();
                                    //dataGridView1.Rows[j].Cells[0].Value.ToString();
                                    // task.Id = dataGridView1.Rows[j].Cells[1].Value.ToString();
                                    // task.Name = dataGridView1.Rows[j].Cells[2].Value.ToString();
                                    // //cell = row == null ? null : row.GetCell(2);

                                    //     task.Version = (int)(dataGridView1.Rows[j].Cells[3].Value);
                                    // //if (cell.CellType == CellType.Numeric)
                                    // //    task.Version = (int)(row.GetCell(2).NumericCellValue);
                                    // task.Releaser = dataGridView1.Rows[j].Cells[4].Value.ToString();
                                    // task.Releasetime = (DateTime)(dataGridView1.Rows[j].Cells[5].Value);
                                    //task.Type = dataGridView1.Rows[j].Cells[6].Value.ToString();
                                    //task.Creator = dataGridView1.Rows[j].Cells[7].Value.ToString(); ;
                                    //task.Status = (LXJHGLStatus)(dataGridView1.Rows[j].Cells[8].Value);
                                    //task.Taskcreatime = (DateTime)(dataGridView1.Rows[j].Cells[9].Value);
                                    //updateList.Add(task);
                                    Tasks[j].Creator = dataGridView1.Rows[j].Cells[7].Value.ToString();
                                    
                                    //Tasks[j].Difficulty =(int)( dataGridView1.Rows[j].Cells[13].Value);
                                }
                            }

                        }

                    }
                    else
                    {
                        return;
                    }


                }
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            //改变数据库中数据
            LXJHGLCLT.Agent.UpdateTasks(Tasks);
            //重新绑定datagridview
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            dataGridView1.Columns[1].ReadOnly = true;
            dataGridView1.Columns[2].ReadOnly = true;
            dataGridView1.Columns[3].ReadOnly = true;
            dataGridView1.Columns[4].ReadOnly = true;
            dataGridView1.Columns[5].ReadOnly = true;
            dataGridView1.Columns[6].ReadOnly = true;
            dataGridView1.Columns[7].ReadOnly = false;
            dataGridView1.Columns[8].ReadOnly = true;
            dataGridView1.Columns[9].ReadOnly = true;
            dataGridView1.Columns[10].ReadOnly = true;
            dataGridView1.Columns[11].ReadOnly = false;
            dataGridView1.Columns[12].ReadOnly = true;
            dataGridView1.Columns[13].ReadOnly = false;
            dataGridView1.Columns[14].ReadOnly = true;
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


                var result = LXJHGLCLT.Agent.ImportTasks(Tasks);
                if (result.Count > 0)
                {
                    //如果有重复导入，输出到dt2
                    dataGridView2.DataSource = result;
                }
                //Tasks = LXJHGLCLT.Agent.GetAllTasks();


                dataGridView1.DataSource = Tasks;

            }
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }


    }
}
