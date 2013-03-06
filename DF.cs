using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Linq;

namespace HuntTheWumpus
{
    public partial class Form2 : Form
    {
        public int answer=0;
        private int chosen;
        public Form2()
        {
            InitializeComponent();
        }
        public Form2(XElement question,int n)
        {
            InitializeComponent();
            radioButton1.Text = (string)question.Element("a1");
            radioButton2.Text = (string)question.Element("a2");
            radioButton3.Text = (string)question.Element("a3");
            radioButton4.Text = (string)question.Element("a4");
            label1.Text = (string)question.Element("q");
            this.Text = "Question " + n;
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            chosen = 1;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            chosen = 2;
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            chosen = 3;
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            chosen = 4;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            answer = chosen;
            this.Close();
        }
    }
}
