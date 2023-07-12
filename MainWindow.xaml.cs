using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp7
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool isDragging;
        private Point offset;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void TextBox_TextChanged(object sender, RoutedEventArgs e)
        {
            if (double.TryParse(XTextBox.Text, out double x) && double.TryParse(YTextBox.Text, out double y))
            {
                TranslateTransform translateTransform = Rectangle.RenderTransform as TranslateTransform;
                if (translateTransform != null)
                {
                    double originalX = Rectangle.ActualWidth / 2;
                    double originalY = Rectangle.ActualHeight / 2;
                    double newX = x - originalX;
                    double newY = y - originalY;
                    translateTransform.X = newX;
                    translateTransform.Y = newY;
                }
            }
        }

        private void Rectangle_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            isDragging = true;
            offset = e.GetPosition(Rectangle);
            Rectangle.CaptureMouse();
        }

        private void Rectangle_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                Point position = e.GetPosition(Rectangle.Parent as UIElement);
                double newX = position.X - offset.X;
                double newY = position.Y - offset.Y;
                TranslateTransform translateTransform = Rectangle.RenderTransform as TranslateTransform;
                if (translateTransform != null)
                {
                    translateTransform.X = newX;
                    translateTransform.Y = newY;
                    XTextBox.Text = (newX + Rectangle.ActualWidth / 2).ToString();
                    YTextBox.Text = (newY + Rectangle.ActualHeight / 2).ToString();
                }
            }
        }

        private void Rectangle_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            isDragging = false;
            Rectangle.ReleaseMouseCapture();
        }
    }
}
