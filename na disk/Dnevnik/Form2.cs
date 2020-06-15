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
    public partial class Form2 : Form
    {
        ConnectionWithDataBase connect = new ConnectionWithDataBase();   
        buffer obj1 = new buffer();
        public Form2()
        {
            
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            string[] Days = new string[6] { "Понедельник",
            "Вторник",
            "Среда",
            "Четверг",
            "Пятница",
            "Суббота"};
            obj1.Button = false;
            dataGridView2.ReadOnly = true;
            string[] mas = new string[48];
            mas = connect.GetAllLessons();
            dataGridView2.RowCount = 8;
            dataGridView2.ColumnCount = 6;
            for (int j = 0; j < 8; j++)
            {
                dataGridView2.Rows[j].HeaderCell.Value = (j+1).ToString();
            }
            int k = 0;
            for (int i = 0; i < 6; i++)
            { 
                dataGridView2.Columns[i].Width = 100;
                
                dataGridView2.Columns[i].HeaderCell.Value = Days[i];
                for (int j = 0; j<8; j++)
                {
                    dataGridView2.Rows[j].Height = 40;
                    dataGridView2.Rows[j].Cells[i].Value = mas[k];
                    k++;
                   
                }
            }
        }

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string[] mas = new string[48];
            if (obj1.Button == false)
            {
                obj1.Button = true;
                button1.Text = "Закончить редактирование";
            }
            else
            {
                int k = 0;
                for (int i = 0; i < 6; i++)
                {
                    for (int j = 0; j < 8; j++)
                    {
                        mas[k] = dataGridView2.Rows[j].Cells[i].Value == null ? " " : dataGridView2.Rows[j].Cells[i].Value.ToString();
                        k++;
                    }
                }
                connect.SubmitLessonsChanges(mas);
                obj1.Button = false;
                button1.Text = "Редактировать";
            }
            if (obj1.Button == true)
            {
                dataGridView2.ReadOnly = false;
            }
            else
            {
                dataGridView2.ReadOnly = true;
            }
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int stroka = e.RowIndex;
            int stolb = e.ColumnIndex;
            ConnectionWithDataBase.ChoosenLessonID = ((stolb) * 8 + stroka) + 1 ; //возвращамый индекс предмета
            Form3 newForm = new Form3();
            newForm.Show();
        }
         
        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
    class buffer
    {
        public bool button = false;
        public bool Button
        {
            get
            {
                return button;
            }
            set
            {
                button = value;
            }
        }
    }
}
