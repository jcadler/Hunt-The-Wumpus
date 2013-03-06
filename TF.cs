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
    public partial class TriviaManagementTester : Form
    {
        private TriviaManagement t;
        private WikiApi w;
        public TriviaManagementTester()
        {
            InitializeComponent();
            t = new TriviaManagement();
            w = new WikiApi();
        }

        private void button1_Click(object sender, EventArgs e)
        {            
            t.usedQuestion((int)UsedQUpDown.Value);            
        }

        private void button2_Click(object sender, EventArgs e)
        {            
            MessageBox.Show(t.getQuestion((int)getQUpDown.Value));
        }

        private void button3_Click(object sender, EventArgs e)
        {            
            MessageBox.Show(t.getAnswer((int)GetAUpDown.Value).ToString());
        }

        private void button4_Click(object sender, EventArgs e)
        {                     
            String display = "";
            foreach (int x in t.wumpusQuestions())
                display += x + " ";
            MessageBox.Show(display);
        }

        private void button5_Click(object sender, EventArgs e)
        {            
            MessageBox.Show(t.getTrivia((int)TriviaUpDown.Value));
        }

        private void button6_Click(object sender, EventArgs e)
        {            
            t.trivia();
        }

        private void button7_Click(object sender, EventArgs e)
        {            
            String display="";
            foreach (int x in t.pitQuestions())
                display += x + " ";
            MessageBox.Show(display);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            t.displayQuestion((int)numericUpDown1.Value);          
        }

        private void WikiQuery_Click(object sender, EventArgs e)
        {
            w.search(WikiQueryBox.Text);
        }
    }
}
