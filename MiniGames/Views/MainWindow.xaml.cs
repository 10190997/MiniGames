using System;
using System.Windows;

namespace MiniGames
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void gameFrame_ContentRendered(object sender, EventArgs e)
        {
            if (gameFrame.CanGoBack)
            {
                btnBack.Visibility = Visibility.Visible;
            }
            else
            {
                btnBack.Visibility = Visibility.Collapsed;
            }
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            gameFrame.GoBack();
        }
    }
}