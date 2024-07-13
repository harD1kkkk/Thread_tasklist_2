using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace taskList_task_4_5
{
    public partial class Form1 : Form
    {
        private NumericUpDown[] numericUpDowns;
        private ProgressBar[] progressBars;
        private CancellationTokenSource cancellationTokenSource = null;
        private ManualResetEventSlim pauseEvent = new ManualResetEventSlim(true);

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int threadNumber = (int)numericUpDown1.Value;
            createProgressBar(threadNumber);

        }
        private void createProgressBar(int threadNumber)
        {
            numericUpDowns = new NumericUpDown[threadNumber];
            progressBars = new ProgressBar[threadNumber];

            for (int i = 1; i <= threadNumber; i++)
            {
                int index = i - 1;

                ProgressBar progressBar = new ProgressBar();
                NumericUpDown numericUpDown = new NumericUpDown();
                Label label = new Label();

                label.Text = "Thread " + index + " speed:";
                label.Location = new Point(0, 60 + (index * 30));

                numericUpDown.Location = new Point(93, 60 + (index * 30));
                numericUpDown.Width = 50;

                progressBar.Location = new Point(180, 60 + (index * 30));
                progressBar.Width = 400;

                this.Controls.Add(numericUpDown);
                this.Controls.Add(progressBar);
                this.Controls.Add(label);

                numericUpDowns[index] = numericUpDown;
                progressBars[index] = progressBar;

                numericUpDown.Show();
                progressBar.Show();
                label.Show();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            pauseEvent.Set();
        }

        private void startThreads(int threadNumber)
        {
            cancellationTokenSource = new CancellationTokenSource();
            var token = cancellationTokenSource.Token;

            for (int i = 0; i < threadNumber; i++)
            {
                int index = i;

                Thread thread = new Thread(() => UpdateProgressBar(index, token));
                thread.Start();
            }
        }
        private void UpdateProgressBar(int index, CancellationToken token)
        {
            while (progressBars[index].Value < 100)
            {
                try
                {
                    Thread.Sleep(1000 / (int)numericUpDowns[index].Value);

                    pauseEvent.Wait();

                    if (token.IsCancellationRequested) break;


                    Invoke((Action)(() =>
                    {
                        if (progressBars[index].Value < 100)
                        {
                            progressBars[index].Value += 1;
                        }
                        showOrHideNumeric();
                    }));
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                    break;
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int threadNumber = (int)numericUpDown1.Value;
            startThreads(threadNumber);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            reset();
        }
        private void reset()
        {
            for (int i = 0; i < progressBars.Length; i++)
            {
                progressBars[i].Value = 0;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            pauseEvent.Reset();
        }

        private void showOrHideNumeric()
        {
            bool allCompleteOrReset = true;

            for (int i = 0; i < progressBars.Length; i++)
            {
                if (progressBars[i].Value != 0 && progressBars[i].Value != 100)
                {
                    allCompleteOrReset = false;
                    break;
                }
            }

            Invoke((Action)(() =>
            {
                foreach (var numericUpDown in numericUpDowns)
                {
                    if (allCompleteOrReset)
                    {
                        numericUpDown.Show();
                    }
                    else
                    {
                        numericUpDown.Hide();
                    }
                }
            }));
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
