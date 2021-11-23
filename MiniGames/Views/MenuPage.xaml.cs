using GameLibrary;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace MiniGames
{
    /// <summary>
    /// Interaction logic for MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        private ApplicationContext db;

        public MainPage()
        {
            InitializeComponent();

            db = new ApplicationContext();
        }

        private void btnRPS_Click(object sender, RoutedEventArgs e)
        {
            GameLibrary.Games game = db.Games.Where(g => g.Title == "RPS").SingleOrDefault();
            Properties.Settings.Default.GameId = game.Id;
            NavigationService.Navigate(new RockPaperScissorsPage());
        }
    }
}