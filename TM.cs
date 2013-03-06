using System;
using System.Collections.Generic;
using System.Xml.Linq;
using System.Windows.Forms;
using System.Text;
using System.IO;
using System.Linq;
namespace HuntTheWumpus
{
    public class TriviaManagement
    {
        private int maxid;
        private int maxidt;
        private XElement root;
        int[] used;
        int[] usedt;
        private int qn;
        public TriviaManagement()
        {
            root = XElement.Load("qs.xml");
            XElement id = root.Element("maxid");
            XElement idt = root.Element("maxidt");
            maxid = int.Parse((string)id);
            maxidt = int.Parse((string)idt);
            used = new int[maxid];
            usedt = new int[maxidt];
            for (int loop = 0; loop < maxid; loop++)
                used[loop] = 0;
            for (int loop = 0; loop < maxidt; loop++)
                usedt[loop] = 0;
            qn=1;
        }
        public void trivia()
        {
            Random r = new Random();
            int next = 0;
            int last = 0;
            bool use = true;
            if (checkUsedAllT())
                resetUsedT();
            while (use)
            {
                use = false;
                next = r.Next(maxidt)+1;
                foreach (int a in usedt)
                {
                    if (a == next||next==last)
                        use = true;
                }                    
            }
            String disp = getTrivia(next);
            MessageBox.Show(disp);
            usedTrivia(next);
            last = next;
        }

        public int[] pitQuestions()
        {
            int wrong = 0;
            int right = 0;
            int totalQs = 0;
            int next = 0;
            int last = 0;
            int[] prev = new int[3];
            for (int x = 0; x < 3; x++)
                prev[x] = 0;
            bool use;
            Random r = new Random();
            while (wrong < 2 && right < 2)
            {
                if (checkUsedAll())
                    resetUsed();
                use = false;
                while (!use)
                {
                    use = true;
                    next = r.Next(1,maxid+1);
                    foreach (int a in used)
                    {
                        if (a == next||next==last)
                            use = false;
                    }
                    foreach (int a in prev)
                    {
                        if (a == next)
                            use = false;
                    }
                }
                usedQuestion(next);
                prev[qn - 1] = next;
                last = next;
                if (displayQuestion(next))
                    right++;
                else
                    wrong++;
                totalQs++;
                qn++;
            }
            int[] ret = new int[2];
            if (wrong == 3)
                ret[0] = 0;
            else
                ret[0] = 1;
            ret[1] = totalQs;
            qn=1;
            return ret;
        }

        public int[] wumpusQuestions()
        {
            int wrong = 0;
            int right=0;
            int totalQs=0;
            int next = 0;
            int last = 0;
            int[] prev = new int[5];
            bool use;
            Random r = new Random();
            while (wrong < 3 && right < 3)
            {
                if (checkUsedAll())
                    resetUsed();
                use = false;
                while(!use)
                {
                    use = true;
                    next=r.Next(1,maxid+1);
                    foreach(int a in used)
                    {
                        if(a==next||next==last)
                         use=false;
                    }
                    foreach(int a in prev)
                    {
                        if (a == next)
                            use = false;
                    }
                }
                usedQuestion(next);
                prev[qn - 1] = next;
                last = next;
                if(displayQuestion(next))
                    right++;
                else
                    wrong++;
                totalQs++;
                qn++;
            }
            int[] ret = new int[2];
            if (wrong == 3)
                ret[0] = 0;
            else
                ret[0] = 1;
            ret[1] = totalQs;
            qn=1;
            return ret;
        }

        public void usedQuestion(int q)
        {
            int loop = 0;
            while (used[loop] != 0)
                loop++;
            used[loop] = q;
        }

        public void usedTrivia(int t)
        {
            int loop = 0;
            while (usedt[loop] != 0)
                loop++;
            usedt[loop] = t;
        }

        public bool displayQuestion(int q)
        {
            Form2 frm = new Form2(query("question", "id", q, "question"),qn);
            frm.ShowDialog();
            while (frm.answer == 0)
                continue;
            if (frm.answer == getAnswer(q))
            {
                MessageBox.Show("Correct!");
                return true;
            }
            else
            {
                MessageBox.Show("Incorrect!");
                return false;
            }
        } 

        public int getAnswer(int q)
        {
            return int.Parse((String)query("question", "id", q, "a"));
        }   
                     
        public String getQuestion(int q)
        {
            return (String)query("question", "id", q, "q");
        }
        public XElement query(String SearchThrough, String check, int find,String slct)
        {
            IEnumerable<XElement> f;
            if(SearchThrough.Equals(slct))
                f = from a in root.Elements(SearchThrough)
                    where a.Attribute(check).Value.Equals(find+"")
                    select a;
            else
                f = from a in root.Elements(SearchThrough)
                    where a.Attribute(check).Value.Equals(find+"")
                    select a.Element(slct);
            return f.First();
        }
        public String getTrivia(int t)
        {
            return (String)query("trivia","id",t,"trivia").Element("t");
        }

        private void resetUsed()
        {
            for (int x = 0; x < used.Length; x++)
                used[x] = 0;
        }

        private bool checkUsedAll()
        {
            foreach (int a in used)
            {
                if (a == 0)
                    return false;
            }
            return true;
        }

        private bool checkUsedAllT()
        {
            foreach (int a in usedt)
            {
                if (a == 0)
                    return false;
            }
            return true;
        }

        private void resetUsedT()
        {
            for (int x = 0; x < usedt.Length; x++)
                usedt[x] = 0;
        }

    static void Main()
    {
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);
        Application.Run(new TriviaManagementTester());
    }
    }
}
    

