using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace taskList_task_7
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            double num = Int32.Parse(textBox1.Text);
            int power = Int32.Parse(textBox2.Text);
            double result = await RunCalculatePower(num, power);
            label3.Text = "Result: " + result.ToString();
        }

        private Task<double> RunCalculatePower(double num, int power)
        {
            return Task.Run(() => CalculatePower(num, power));
        }
        private double CalculatePower(double num, int power)
        {
            double result = 1;
            for (int i = 0; i < power; i++)
            {
                result *= num;
            }
            return result;
        }
    }
}
