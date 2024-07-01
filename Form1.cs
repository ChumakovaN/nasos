using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace Насосная_станция
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                label1.Text = "Не определен";
            }
            else
            {
                textBox2.Text = "20";
                textBox3.Text = "0";
                label1.Text = "Включен";
                label7.Text = "True";
                v();
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (var item in listBox1.SelectedItems)
            {
                textBox1.Text = listBox1.SelectedItem.ToString();
                textBox2.Text = "0";
                textBox3.Text = "0";
                label4.Text = "Не определено";
                label1.Text = "Не определен";
                label7.Text = "None";
                timer1.Stop();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                label1.Text = "Не определен";
            }
            else
            {
                label1.Text = "Выключен";
                label7.Text = "False";
                timer1.Stop();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int count = listBox1.Items.Count + 1;
            listBox1.Items.Add($"Насос {count}");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex != -1)
            {
                listBox1.Items.RemoveAt(listBox1.SelectedIndex);
                timer1.Stop();
                textBox1.Text = "";
                textBox2.Text = "0";
                textBox3.Text = "20";
                label1.Text = "Не определен";
                label7.Text = "None";
            }
        }


        public void v()
        {
            Random rnd = new Random();


            double initialTemperature = double.Parse(textBox2.Text);
            double initialPressure = double.Parse(textBox3.Text);

            double step = rnd.NextDouble();
            double steps = rnd.NextDouble();

                timer1.Interval = 1000;
                timer1.Tick += (s, args) =>
                {
                    textBox2.Text = (Math.Min(initialTemperature + step * 2, 100)).ToString("F2");
                    textBox3.Text = (Math.Min(initialPressure + step * 5, 100)).ToString("F2");

                    initialTemperature += step;
                    initialPressure += steps;
                };
                timer1.Start();
        }


        private void textBox2_TextChanged(object sender, EventArgs e)
        {

            double a = Convert.ToDouble(textBox2.Text);
            if (a >= 80)
            {
                label4.Text = "Перегрет";
                label4.ForeColor = Color.Red;
            }
            else if (a >= 1)
            {
                label4.Text = "В норме";
                label4.ForeColor = Color.Black;
            }
            else
            {
                label4.Text = "Не определен";
                label4.ForeColor = Color.Black;
            }
        }

        public void SaveReportToPdf(string filePath)
        {
            Document document = new Document();
            PdfWriter.GetInstance(document, new FileStream(filePath, FileMode.Create));
            document.Open();

  
            document.Add(new Paragraph($"Pump: {textBox1.Text}, Status: {label7.Text}, Pressure: {textBox3.Text}, Temperature: {textBox2.Text}"));
            
            document.Close();
        }

        public void button5_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "PDF files (*.pdf)|*.pdf",
                Title = "Сохранить отчет"
            };

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                SaveReportToPdf(saveFileDialog.FileName);
            }
        }
    }
}
