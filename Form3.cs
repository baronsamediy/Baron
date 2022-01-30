using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace LABA1
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                DataSet ds = new DataSet(); // создаем пока что пустой кэш данных
                DataTable dt = new DataTable(); // создаем пока что пустую таблицу данных
                dt.TableName = "Student"; // название таблицы
                dt.Columns.Add("Name"); // название колонок
                dt.Columns.Add("Bilet");
                dt.Columns.Add("Group");
                ds.Tables.Add(dt); //в ds создается таблица, с названием и колонками, созданными выше

                foreach (DataGridViewRow r in dataGridView1.Rows) // пока в dataGridView1 есть строки
                {
                    DataRow row = ds.Tables["Student"].NewRow(); // создаем новую строку в таблице, занесенной в ds
                    row["Name"] = r.Cells[0].Value;  //в столбец этой строки заносим данные из первого столбца dataGridView1
                    row["Bilet"] = r.Cells[1].Value; // то же самое со вторыми столбцами
                    row["Group"] = r.Cells[2].Value; //то же самое с третьими столбцами
                    ds.Tables["Student"].Rows.Add(row); //добавление всей этой строки в таблицу ds.
                }
                ds.WriteXml("Student.xml");
                MessageBox.Show("XML файл успешно сохранен.", "Выполнено.");
            }
            catch
            {
                MessageBox.Show("Невозможно сохранить XML файл.", "Ошибка.");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("Заполните все поля.", "Ошибка.");
            }
            else
            {
                students newobj = new students();
                newobj.Name = textBox1.Text;
                newobj.Ticket = textBox2.Text;
                newobj.Group = textBox3.Text;

                int n = dataGridView1.Rows.Add();
                dataGridView1.Rows[n].Cells[0].Value = newobj.Name; // столбец Name
                dataGridView1.Rows[n].Cells[1].Value = newobj.Ticket; //столбец придмет
                dataGridView1.Rows[n].Cells[2].Value = newobj.Group; // столбец группа   
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            students newobj = new students();
            newobj.Name = textBox1.Text;
            newobj.Ticket = textBox2.Text;
            newobj.Group = textBox3.Text;

            int n = dataGridView1.CurrentRow.Index;
            dataGridView1.Rows[n].Cells[0].Value = newobj.Name;
            dataGridView1.Rows[n].Cells[1].Value = newobj.Ticket;
            dataGridView1.Rows[n].Cells[2].Value = newobj.Group;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.RemoveAt(dataGridView1.CurrentRow.Index); //удаление  
        }
       
        private void Form3_Load(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count > 0)
            {
                if (File.Exists("Student.xml")) // если существует данный файл
                {
                    DataSet ds = new DataSet(); // создаем новый пустой кэш данных
                    ds.ReadXml("Student.xml"); // записываем в него XML-данные из файла

                    foreach (DataRow item in ds.Tables["Student"].Rows)
                    {
                        int n = dataGridView1.Rows.Add(); // добавляем новую сроку в dataGridView1
                        dataGridView1.Rows[n].Cells[0].Value = item["Name"]; // заносим в первый столбец созданной строки данные из первого столбца таблицы ds.
                        dataGridView1.Rows[n].Cells[1].Value = item["Bilet"]; // то же самое со вторым столбцом
                        dataGridView1.Rows[n].Cells[2].Value = item["Group"]; // то же самое с третьим столбцом
                    }
                }
                else
                {
                    MessageBox.Show("XML файл не найден.", "Ошибка.");
                }
            }
        }
  
        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            dataGridView1.ClearSelection();

            if (string.IsNullOrWhiteSpace(textBox4.Text))
                return;

            var values = textBox4.Text.Split(new char[] { ' ' },
                StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < dataGridView1.RowCount - 1; i++)
            {
                foreach (string value in values)
                {
                    var row = dataGridView1.Rows[i];

                    if (row.Cells["Name"].Value.ToString().Contains(value) || row.Cells["Grupa"].Value.ToString().Contains(value))
                    {
                        row.Selected = true;
                    }
                }
            }
        }

        private void dataGridView1_MouseClick(object sender, MouseEventArgs e)
        {
            textBox1.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            textBox2.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            textBox3.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
        }
    }
}
