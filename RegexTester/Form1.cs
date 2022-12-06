using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace RegexTester
{
    public partial class Form1 : Form
    {
        private MatchCollection matches;
        public Form1()
        {
            InitializeComponent();

            rtb_information.Text = "cmd1\r\ncmd2\r\ncmd3\r\ncmd4";
            button1_Click(null, null);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            rtb_information.SelectionStart = 0;
            rtb_information.SelectionLength = rtb_information.TextLength;
            rtb_information.SelectionBackColor = Color.White;
            rtb_information.SelectionColor = Color.Black;

            if (string.IsNullOrEmpty(textBox1.Text))
            {
                lb_count.Text = "0";
                return;
            }

            try
            {
                lb_count.Text = Regex.Matches(rtb_information.Text, textBox1.Text).Count.ToString();
                matches = Regex.Matches(rtb_information.Text, textBox1.Text);
                listBox1.Items.Clear();
                for(int i=0; i<matches.Count; i++)
                    listBox1.Items.Add($"match ({i}) \"{matches[i].Value}\"");

                foreach (Match match in Regex.Matches(rtb_information.Text, textBox1.Text))
                {
                    if (match.Success)
                    {
                        rtb_information.SelectionStart = match.Index;
                        rtb_information.SelectionLength = match.Length;
                        rtb_information.SelectionColor = Color.White;
                        rtb_information.SelectionBackColor = Color.Red;
                    }
                }
            }
            catch (Exception) { lb_count.Text = "0"; }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            listBox2.Items.Clear();
            button1_Click(null, null);
        }

        private void rtb_information_TextChanged(object sender, EventArgs e)
        {
            int i = rtb_information.SelectionStart;
            textBox1_TextChanged(null, null);
            rtb_information.SelectionStart = i;
            rtb_information.SelectionLength = 0;
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            lb_groups.Text = matches[listBox1.SelectedIndex].Groups.Count.ToString();
            listBox2.Items.Clear();
            for (int i=0; i < matches[listBox1.SelectedIndex].Groups.Count; i++)
                listBox2.Items.Add($"group ({i}) \"" +
                    $"{matches[listBox1.SelectedIndex].Groups[i].Value}\"");
        }
    }
}
