using GameLibrary;
using System.Data.Entity;
using System.Linq;
using System.Windows;

namespace MiniGames
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        private readonly ApplicationContext db;

        public LoginWindow()
        {
            InitializeComponent();

            db = new ApplicationContext();
            db.Users.Load();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(tbLogin.Text) || string.IsNullOrWhiteSpace(tbLogin.Text))
            {
                MessageBox.Show("Введите имя");
                return;
            }
            Users user = db.Users.Where(u => u.Name == tbLogin.Text).SingleOrDefault();
            if (user == null)
            {
                if (MessageBox.Show("Такого пользователя нет в базе. Создать?", "Вааапросик", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    Users newUser = new Users
                    {
                        Name = tbLogin.Text
                    };
                    db.Users.Add(newUser);
                    db.SaveChanges();
                    Properties.Settings.Default.UserId = newUser.Id;
                }
                else
                {
                    return;
                }
            }
            else
            {
                // TODO: REWORK THE LOGIC
                Properties.Settings.Default.UserId = user.Id;
            }

            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            Close();
        }
    }
}