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
using System.IO;


namespace Курсач
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

       /* private void MainWindow_Log(object Sender, RoutedEventArgs e)
        {
            LogWindow windowLogin = new LogWindow();
            windowLogin.Owner = this;
            windowLogin.ShowDialog();
            if (windowLogin.DialogResult == true)
            {
                MessageBox.Show("Привет, {0}", windowLogin.Name);
            }
            else
            {
                MessageBox.Show("Вы не авторизировались");
            }
        }*/

    
        private void Window_Loaded_1(object sender, RoutedEventArgs e)
        {
            LogWindow windowLogin = new LogWindow();
            windowLogin.Owner = this;
            windowLogin.ShowDialog();
            if (windowLogin.DialogResult == true)
            {
                MessageBox.Show("Привет, " + windowLogin.UserName);
            }
            else
            {
                MessageBox.Show("Вы не авторизировались");
            }
        }

        private void Click_Memory_Game(object sender, RoutedEventArgs e)
        {
            FindPairs windowGame = new FindPairs();
            windowGame.Owner = this;
            windowGame.ShowDialog();
          /* if (windowGame.DialogResult == true)
            {
                MessageBox.Show("Ваш счёт: , " + windowGame.Score.ToString());
            }
            else
            {
                MessageBox.Show("Вы не набрали очков");
            }*/
        }

        private void Click_Different_Game(object sender, RoutedEventArgs e)
        {
            Differences windowGame = new Differences();
            windowGame.Owner = this;
            windowGame.ShowDialog();
            /* if (windowGame.DialogResult == true)
              {
                  MessageBox.Show("Ваш счёт: , " + windowGame.Score.ToString());
              }
              else
              {
                  MessageBox.Show("Вы не набрали очков");
              }*/
        }

        private void Click_Show_Statistics(object sender, RoutedEventArgs e)
        {
            FindPairs windowGame = new FindPairs();
            windowGame.Owner = this;
            windowGame.ShowDialog();
            /* if (windowGame.DialogResult == true)
              {
                  MessageBox.Show("Ваш счёт: , " + windowGame.Score.ToString());
              }
              else
              {
                  MessageBox.Show("Вы не набрали очков");
              }*/
        }



      
        
    }
}
