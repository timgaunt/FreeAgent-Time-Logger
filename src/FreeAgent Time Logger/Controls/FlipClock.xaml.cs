using System;
using System.Windows.Controls;

namespace FreeAgent_Time_Logger.Controls
{
    public partial class FlipClock : UserControl
    {
        public FlipClock()
        {
            InitializeComponent();
        }

        public void ShowTime(TimeSpan timeToShow)
        {
            incrementNumber(Hour1, Hour2, timeToShow.Hours);
            incrementNumber(Minute1, Minute2, timeToShow.Minutes);
            incrementNumber(Second1, Second2, timeToShow.Seconds);
        }

        private void incrementNumber(FlipDigit tensDigit, FlipDigit unitDigit, int number)
        {
            var tens = 0;
            
            if (number >= 10)
                tens = (number - (number % 10)) / 10;

            tensDigit.ShowNumber(tens);
            unitDigit.ShowNumber(number);
        }
    }
}
