using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Dnevnik
{
    public partial class Form3 : Form
    {
        ConnectionWithDataBase connect = new ConnectionWithDataBase();
        buffer obj1 = new buffer();
        public Form3()
        {
            InitializeComponent();
        }
        private void Form3_Load(object sender, EventArgs e)
        {
            string[] homeworks = connect.GetLessonHomeworkPointsNames();
            textBox1.Text +=  connect.GetChoosenSubjectName();
            textBox2.Text += connect.GetNote();
            dataGridView1.RowCount = 6;
            for (int j = 0; j < 5; j++)
            {
                dataGridView1.Rows[j].HeaderCell.Value = (j + 1).ToString();
            }
            for (int i = 0; i < 5; i++)
            {
                dataGridView1.Rows[i].Cells[0].Value = homeworks[i];
            }
            DataGridViewCheckBoxColumn x = new DataGridViewCheckBoxColumn();
            x.HeaderCell.Value = "Сделано/Нет";
            dataGridView1.Columns.Insert(1, x);
            bool[] mass = new bool[5];
            for (int i = 0; i < 5; i++)
            {
                mass[i] = Convert.ToBoolean(dataGridView1.Rows[i].Cells[1].Value);
            }
            dataGridView1.AllowUserToAddRows = false;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        private void button1_Click(object sender, EventArgs e)
        {
            ConnectionWithDataBase.HomeworkPointInfo[] mas = new ConnectionWithDataBase.HomeworkPointInfo[5];
            if (obj1.Button == false)
            {
                obj1.Button = true;
                button1.Text = "Закончить редактирование";
                connect.SubmitHomeworkPointChanges(mas);
            }
            else
            {
                int k = 0;
                for (int i = 0; i < 5; i++)
                {
                    mas[i].Name = dataGridView1.Rows[i].Cells[0].Value == null ? " " : dataGridView1.Rows[i].Cells[0].Value.ToString();
                    mas[i].IsDone =Convert.ToBoolean(dataGridView1.Rows[i].Cells[1].Value == null ? false : dataGridView1.Rows[i].Cells[1].Value);
                }
                obj1.Button = false;
                button1.Text = "Редактировать";
                connect.SubmitHomeworkPointChanges(mas);
            }
            if (obj1.Button == true)
            {
                dataGridView1.ReadOnly = false;
                textBox2.ReadOnly = false;
                connect.SubmitHomeworkPointChanges(mas);
            }
            else
            {
                dataGridView1.ReadOnly = true;
                textBox2.ReadOnly = true;
                connect.SubmitHomeworkPointChanges(mas);
            }
            connect.SubmitHomeworkPointChanges(mas);
        }
        private void DataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }
        private void TextBox2_TextChanged(object sender, EventArgs e)
        {
            connect.ChangeNote(textBox2.Text);
        }
    }
}
