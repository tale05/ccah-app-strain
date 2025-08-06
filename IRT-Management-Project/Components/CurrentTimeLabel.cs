using System;
using System.Windows.Forms;

namespace AllControls
{
    public class CurrentTimeLabel : Label
    {
        public CurrentTimeLabel()
        {
            this.Text = "Current Time:";
            this.Location = new System.Drawing.Point(10, 10);
            this.AutoSize = true;

            Timer timer = new Timer();
            timer.Interval = 1000; // Cập nhật thời gian mỗi giây
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            this.Text ="Giờ: " + DateTime.Now.ToString("HH:mm:ss");
        }

        public static void Main()
        {
            Application.Run(new Form() { Controls = { new CurrentTimeLabel() } });
        }
    }
}
