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
        private List<LXJHGLInstance> Tasks;
        public zyjm()
        {
            InitializeComponent();

            Tasks = LXJHGLCLT.Agent.GetTasksByStatus(LXJHGLStatus.未接收, string.Empty);
            dataGridView1.DataSource = Tasks;
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

        private void button2_Click(object sender, EventArgs e)
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
                    if (MessageBox.Show(this, "您要取消数据么？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information).ToString() == "Yes")
                    {//取消数据
                        List<LXJHGLInstance> updateList = new List<LXJHGLInstance>();
                        var task = new LXJHGLInstance();

                        for (int j = 0; j < dataGridView1.Rows.Count; j++)
                        {
                            if ((bool)dataGridView1.Rows[j].Cells[0].EditedFormattedValue == true)
                            {
                                Tasks[j].Status = LXJHGLStatus.未分配;
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
            //取消数据库中数据
            LXJHGLCLT.Agent.UpdateTasks(Tasks);
            //重新绑定datagridview
            dataGridView1.DataSource = Tasks;
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
                    if (MessageBox.Show(this, "您要接收数据么？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information).ToString() == "Yes")
                    {//接收数据
                        List<LXJHGLInstance> updateList = new List<LXJHGLInstance>();
                        var task = new LXJHGLInstance();

                        for (int j = 0; j < dataGridView1.Rows.Count; j++)
                        {
                            if ((bool)dataGridView1.Rows[j].Cells[0].EditedFormattedValue == true)
                            {
                                Tasks[j].Status = LXJHGLStatus.接收;
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
            //取消数据库中数据
            LXJHGLCLT.Agent.UpdateTasks(Tasks);
            //重新绑定datagridview
            dataGridView1.DataSource = Tasks;
        }
    }
}
