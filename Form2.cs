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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("Заполните все поля.", "Ошибка.");
            }
            else
            {
                prepod newobject = new prepod ();
                newobject.Name = textBox1.Text;
                newobject.Group = textBox3.Text;
                newobject.Lessons = textBox2.Text;
                int n = dataGridView1.Rows.Add();

                dataGridView1.Rows[n].Cells[0].Value = newobject.Name; // столбец Name
                dataGridView1.Rows[n].Cells[1].Value = newobject.Lessons; //столбец придмет
                dataGridView1.Rows[n].Cells[2].Value = newobject.Group; // столбец группа 
            }

        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                DataSet ds = new DataSet(); // создаем пока что пустой кэш данных
                DataTable dt = new DataTable(); // создаем пока что пустую таблицу данных
                dt.TableName = "Prepod"; // название таблицы
                dt.Columns.Add("Name"); // название колонок
                dt.Columns.Add("Predmet");
                dt.Columns.Add("Group");
                ds.Tables.Add(dt); //в ds создается таблица, с названием и колонками, созданными выше

                foreach (DataGridViewRow r in dataGridView1.Rows) // пока в dataGridView1 есть строки
                {
                    DataRow row = ds.Tables["Prepod"].NewRow(); // создаем новую строку в таблице, занесенной в ds
                    row["Name"] = r.Cells[0].Value;  //в столбец этой строки заносим данные из первого столбца dataGridView1
                    row["Predmet"] = r.Cells[1].Value; // то же самое со вторыми столбцами
                    row["Group"] = r.Cells[2].Value; //то же самое с третьими столбцами
                    ds.Tables["Prepod"].Rows.Add(row); //добавление всей этой строки в таблицу ds.
                }
                ds.WriteXml("Prepod.xml");
                MessageBox.Show("XML файл успешно сохранен.", "Выполнено.");
            }
            catch
            {
                MessageBox.Show("Невозможно сохранить XML файл.", "Ошибка.");
            }
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count > 0) 
            {
                if (File.Exists("Prepod.xml")) // если существует данный файл
                {
                    DataSet ds = new DataSet(); // создаем новый пустой кэш данных
                    ds.ReadXml("Prepod.xml"); // записываем в него XML-данные из файла

                    foreach (DataRow item in ds.Tables["Prepod"].Rows)
                    {
                        int n = dataGridView1.Rows.Add(); // добавляем новую сроку в dataGridView1
                        dataGridView1.Rows[n].Cells[0].Value = item["Name"]; // заносим в первый столбец созданной строки данные из первого столбца таблицы ds.
                        dataGridView1.Rows[n].Cells[1].Value = item["Predmet"]; // то же самое со вторым столбцом
                        dataGridView1.Rows[n].Cells[2].Value = item["Group"]; // то же самое с третьим столбцом
                    }
                }
                else
                {
                    MessageBox.Show("XML файл не найден.", "Ошибка.");
                }
            }
            
        }

        private void dataGridView1_MouseClick(object sender, MouseEventArgs e)
        {
            textBox1.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            textBox2.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            textBox3.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            prepod newobject = new prepod();
            newobject.Name = textBox1.Text;
            newobject.Group = textBox3.Text;
            newobject.Lessons = textBox2.Text;

            int n = dataGridView1.CurrentRow.Index;
            dataGridView1.Rows[n].Cells[0].Value = newobject.Name; //имя
            dataGridView1.Rows[n].Cells[1].Value = newobject.Lessons;//предмет
            dataGridView1.Rows[n].Cells[2].Value = newobject.Group;//группа
        }

        private void button3_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.RemoveAt(dataGridView1.CurrentRow.Index); //удаление  
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

        
    }
}
