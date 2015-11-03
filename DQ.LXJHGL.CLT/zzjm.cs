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

                if (count == 0)
                {
                    MessageBox.Show("请至少选择一条数据！", "提示");
                    return;
                }
                else
                {
                    if (MessageBox.Show(this, "您要更新数据么？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information).ToString() == "Yes")
                    {
                         for (int j = 0; j < dataGridView1.Rows.Count; j++)
                            {
                               if ((bool)dataGridView1.Rows[i].Cells[0].EditedFormattedValue == true)
                                      {
                                         LXJHGLCLT.Agent.UpdateTasks(Tasks);
                                       }
                       
                             }
                    }
                    else
                    {
                        return;
                    }
                }
              }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            dataGridView1.DataSource = LXJHGLCLT.Agent.UpdateTasks(Tasks); //重新绑定datagridview
            }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            dataGridView1.Columns[1].ReadOnly = true;
            dataGridView1.Columns[2].ReadOnly = true;
            dataGridView1.Columns[3].ReadOnly = true;
            dataGridView1.Columns[4].ReadOnly = true;
            dataGridView1.Columns[5].ReadOnly = true;
            dataGridView1.Columns[6].ReadOnly = true;
            dataGridView1.Columns[7].ReadOnly = true;
            dataGridView1.Columns[8].ReadOnly = true;
            dataGridView1.Columns[9].ReadOnly = true;
            dataGridView1.Columns[10].ReadOnly = true;
            dataGridView1.Columns[11].ReadOnly = true;
            dataGridView1.Columns[12].ReadOnly = true;
            dataGridView1.Columns[13].ReadOnly = true;
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

                //copy data to datagridview
                ////数据源的定义 
                string strConn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source = " + file + ";Extended Properties = Excel 8.0";
                ////Sql语句（查找表中所有数据） 
                string strExcel = "select * from [Sheet1$]";
                ////定义存放的数据表 
                DataSet ds = new DataSet();
                ////连接数据源 
                OleDbConnection conn = new OleDbConnection(strConn);
                conn.Open();
                ////适配到数据源 
                OleDbDataAdapter adapter = new OleDbDataAdapter(strExcel, conn);
                adapter.Fill(ds, "[Sheet1$]");
                conn.Close();
                ////将Excel表中数据导入DataGridView的DataSource中 
                dataGridView1.DataSource = ds.Tables["[Sheet1$]"];

                //如果有重复导入，输出到dt2
                var reback = LXJHGLCLT.Agent.ImportTasks(Tasks);
                dataGridView2.DataSource = reback;
                

            }
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }


    }
}
