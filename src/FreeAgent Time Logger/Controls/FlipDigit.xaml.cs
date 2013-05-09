using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace FreeAgent_Time_Logger.Controls
{
    public partial class FlipDigit : UserControl
    {
        private int _step;
        private int _toShow;
        private int _current;
        private DispatcherTimer _timer;
        private BitmapFrame _digitsTop;
        private BitmapFrame _digitsBottom;

        public double Pace { get; set; }

        public FlipDigit()
        {
            InitializeComponent();

            _step = 0;
            _current = 0;
            Pace = 300;
            _timer = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(Pace / 7) };
            _timer.Tick += timer_Tick;

            var digitsTop = Application.GetContentStream(new Uri(@"digits-top.png", UriKind.RelativeOrAbsolute));
            if (digitsTop != null)
                _digitsTop = BitmapFrame.Create(digitsTop.Stream);

            var digitsBottom = Application.GetContentStream(new Uri(@"digits-bottom.png", UriKind.RelativeOrAbsolute));
            if (digitsBottom != null)
                _digitsBottom = BitmapFrame.Create(digitsBottom.Stream);

            goToNumber(0, 0);
        }

        public void ShowNumber(int digitToShow)
        {
            if (digitToShow > 9)
            {
                digitToShow = digitToShow % 10;
            }

            _toShow = digitToShow;

            if (_current != _toShow)
                _timer.Start();

            _current = digitToShow;
        }

        void timer_Tick(object sender, EventArgs e)
        {
            goToNumber(_toShow, _step);
            incrementStep();
        }

        void goToNumber(int number, int step)
        {
            var topWidth = 0;
            var botWidth = 0;
            var topTop = number * 39;
            var botTop = number * 64;

            if (step < 3)
            {
                topWidth = (step * 53);
                if (number > 0)
                    botTop = (number - 1) * 64;
            }
            else
            {
                botWidth = ((step - 3) * 53);
            }
            var top = GetCroppedImage(_digitsTop, topTop, topTop + 39, topWidth, topWidth + 53);
            if (top != null)
                topImage.Source = top;

            var bot = GetCroppedImage(_digitsBottom, botTop, botTop + 64, botWidth, botWidth + 53);
            if (bot != null)
                bottomImage.Source = bot;
        }

        void incrementStep()
        {
            _step++;

            if (_step != 7) return;

            _step = 0;
            _timer.Stop();
        }

        public ImageSource GetCroppedImage(BitmapSource source, int cropTop, int cropBottom, int cropLeft, int cropRight)
        {
            if (source == null)
                return null;

            if (source.Format != PixelFormats.Bgra32)
                source = new FormatConvertedBitmap(source, PixelFormats.Bgra32, null, 0);

            if (cropRight > source.Width || cropBottom > source.Height)
                return null;

            return new CroppedBitmap(source, new Int32Rect(cropLeft, cropTop, cropRight - cropLeft, cropBottom - cropTop));
        }
    }
}
