using System;
using System.Threading;
using System.Windows.Forms;

namespace taskList_task_1
{
    public partial class Form1 : Form
    {
        private string numbersPriority;
        private string lettersPriority;
        private string symbolsPriority;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            numbersPriority = comboBox1.SelectedItem.ToString();
            lettersPriority = comboBox2.SelectedItem.ToString();
            symbolsPriority = comboBox3.SelectedItem.ToString();


            PrintNumbers();
            PrintLetters();
            PrintSymbols();
        }

        private void PrintNumbers()
        {
            listBox1.Items.Clear();

            int[] nums = { 42, 17, 89, 5, 63, 29, 11, 76, 50, 8 };

            Thread thread = new Thread(() =>
            {
                for (int i = 0; i < 10; i++)
                {
                    Invoke((Action)(() =>
                    {
                        listBox1.Items.Add(nums[i]);

                    }));
                }
            });
            thread.Start();
            thread.Priority = GetPriority(numbersPriority);
        }
        private void PrintLetters()
        {
            string abc = "abcde";

            Thread thread = new Thread(() =>
            {

                for (int i = 0; i < 5; i++)
                {
                    Invoke((Action)(() =>
                    {
                        listBox1.Items.Add(abc[i]);

                    }));
                }
            });
            thread.Start();
            thread.Priority = GetPriority(lettersPriority);
        }

        private void PrintSymbols()
        {
            string symbols = "!+@#$%≈^&";

            Thread thread = new Thread(() =>
            {
                for (int i = 0; i < 9; i++)
                {
                    Invoke((Action)(() =>
                    {
                        listBox1.Items.Add(symbols[i]);
                    }));
                }
            });
            thread.Start();
            thread.Priority = GetPriority(symbolsPriority);
        }


        private ThreadPriority GetPriority(string priority)
        {
            if (priority == "High")
            {
                return ThreadPriority.Highest;
            }
            else if (priority == "Medium")
            {
                return ThreadPriority.Normal;
            }
            else if (priority == "Low")
            {
                return ThreadPriority.Lowest;
            }
            else
            {
                return ThreadPriority.Normal;
            }

        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}