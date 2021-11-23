using GameLibrary;
using MiniGames.Games.RPS;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace MiniGames
{
    /// <summary>
    /// Interaction logic for RockPaperScissorsPage.xaml
    /// </summary>
    public partial class RockPaperScissorsPage : Page
    {
        private readonly ApplicationContext db;

        private int score = 0;

        private enum Choices
        {
            Rock,
            Paper,
            Scissors
        }

        public RockPaperScissorsPage()
        {
            InitializeComponent();

            db = new ApplicationContext();
        }

        private Choices GenerateChoice()
        {
            Random random = new Random();
            Type type = typeof(Choices);
            Array values = type.GetEnumValues();
            int index = random.Next(values.Length);

            return (Choices)values.GetValue(index);
        }

        private string GetPicture(Choices choice)
        {
            switch (choice)
            {
                case Choices.Rock:
                    return "./Images/rock.png";

                case Choices.Paper:
                    return "./Images/paper.png";

                case Choices.Scissors:
                    return "./Images/scissors.png";

                default:
                    return "./Images/question.png";
            }
        }

        private void ChangePictures(Choices playerChoice, Choices PCChoice)
        {
            PCImg.Source = new BitmapImage(new Uri(GetPicture(PCChoice), UriKind.Relative));
            playerImg.Source = new BitmapImage(new Uri(GetPicture(playerChoice), UriKind.Relative));
        }

        private bool? PlayGame(Choices playerChoice, Choices PCChoice)
        {
            return playerChoice == PCChoice
                ? null
                : (bool?)((playerChoice == Choices.Rock && PCChoice == Choices.Scissors)
                || (playerChoice == Choices.Scissors && PCChoice == Choices.Paper)
                || (playerChoice == Choices.Paper && PCChoice == Choices.Rock));
        }

        private void SaveResult()
        {
            var NewRecord = new Records
            {
                UserId = Properties.Settings.Default.UserId,
                GameId = Properties.Settings.Default.GameId,
                Score = score
            };
            db.Records.Add(NewRecord);
            db.SaveChanges();
        }

        private void Evaluate(Choices playerChoice)
        {
            var PCchoice = GenerateChoice();
            ChangePictures(playerChoice, PCchoice);
            bool? result = PlayGame(playerChoice, PCchoice);
            if (result == true)
            {
                MessageBox.Show("Победа!");
                score++;
            }
            else if (result == false)
            {
                MessageBox.Show("Восстание машин!!!!!!!!!!!!!");
            }
            else
            {
                MessageBox.Show("Ничья!!!!!!!!");
            }
        }

        private void btnRock_Click(object sender, RoutedEventArgs e)
        {
            Evaluate(Choices.Rock);
            tbScore.Text = score.ToString();
        }

        private void btnPaper_Click(object sender, RoutedEventArgs e)
        {
            Evaluate(Choices.Paper);
            tbScore.Text = score.ToString();
        }

        private void btnScissors_Click(object sender, RoutedEventArgs e)
        {
            Evaluate(Choices.Scissors);
            tbScore.Text = score.ToString();
        }

        private void btnRecords_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new RecordsPage());
        }

        private void btnSaveAndExit_Click(object sender, RoutedEventArgs e)
        {
            SaveResult();
            NavigationService.GoBack();
        }
    }
}