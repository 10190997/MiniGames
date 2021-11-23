using GameLibrary;
using GameLibrary.Models;
using System.Linq;
using System.Windows.Controls;

namespace MiniGames.Games.RPS
{
    /// <summary>
    /// Interaction logic for RecordsPage.xaml
    /// </summary>
    public partial class RecordsPage : Page
    {
        private ApplicationContext db;

        public RecordsPage()
        {
            InitializeComponent();

            db = new ApplicationContext();

            var results =
                from r in db.Records
                join u in db.Users on r.UserId equals u.Id
                join g in db.Games on r.GameId equals g.Id
                where g.Id == 1
                select new RecordInfoView()
                {
                    Username = u.Name,
                    UserScore = r.Score
                };

            lvRecords.ItemsSource = results.ToList();
        }
    }
}