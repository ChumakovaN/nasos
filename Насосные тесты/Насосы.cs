using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Windows.Forms;
using Насосная_станция;

namespace Насосные_тесты
{
    [TestClass]
    public class Насосы
    {
        [TestMethod]
        public void Вычисления()
        {
            Form1 form1 = new Form1();
            form1.v();
        }

        [TestMethod]
        public void Сейв()
        {
            string p = "test";
            Button button = new Button();
            Form1 form1 = new Form1();
            button.Click += form1.button5_Click;
            form1.SaveReportToPdf(p);
        }

        [TestMethod]
        public void Тестовый_текст_бокс()
        {
            TextBox textBox = new TextBox();
            textBox.Text = "test";

            Assert.AreEqual("test", textBox.Text);
        }

        [TestMethod]
        public void Открытие_формы_с_тестовым_текст_боксом()
        {
            TextBox textBox = new TextBox();
            textBox.Text = "test";
            Form1 form1 = new Form1();
            form1.Controls.Add(textBox);

            Assert.AreEqual("test", textBox.Text);
        }
    }
}
