using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace taskList_task_6
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            int num = Int32.Parse(textBox1.Text);
            int result = await RuncalculateFactorial(num);
            label2.Text = "Result: " + result.ToString();
        }
        private Task<int> RuncalculateFactorial(int num)
        {
            return Task.Run(() => CalculateFactorial(num));
        }
        private int CalculateFactorial(int num)
        {
            if (num == 0 || num == 1)
            {
                return 1;
            }
            int result = 1;
            for (int i = 2; i <= num; i++)
            {
                result *= i;
            }
            return result;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
