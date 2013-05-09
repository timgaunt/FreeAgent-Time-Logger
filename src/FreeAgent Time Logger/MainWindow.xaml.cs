using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace FreeAgent_Time_Logger
{
    public partial class MainWindow : Window
    {
        private DispatcherTimer _timer;
        private DateTime _startTime;
        private string startedAtText = "(Started at: {0:hh}:{0:mm}{0:tt})";

        public MainWindow()
        {
            InitializeComponent();
            lblStatus.Content = "(Stopped)";
            startTimer();
        }

        private void lblStarted_MouseDown(object sender, MouseButtonEventArgs e)
        {
            startTimer();
        }

        private void startTimer()
        {
            _startTime = DateTime.Now;
            lblStarted.Content = string.Format(startedAtText, _startTime);
            _timer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(1) };
            _timer.Tick += timer_Tick;
            _timer.Start();
            lblStatus.Content = "►";
        }

        void timer_Tick(object sender, EventArgs e)
        {
            var timeSinceStart = DateTime.Now - _startTime;
            var elapsed = new TimeSpan(timeSinceStart.Hours, timeSinceStart.Minutes, timeSinceStart.Seconds);
            flipper.ShowTime(elapsed);
        }
    }
}
