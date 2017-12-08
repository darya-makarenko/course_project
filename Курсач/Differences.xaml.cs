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
using System.Windows.Shapes;
using System.IO;
using System.Windows.Threading;

namespace Курсач
{
    /// <summary>
    /// Interaction logic for Differences.xaml
    /// </summary>
    public partial class Differences : Window
    {
        private int[] indexArr = new int[0];
        private int[] newIndexArr = new int[0];
        private string[] dirs = Directory.GetFiles(@"C:\Users\darya\Documents\Visual Studio 2012\Projects\Курсач\Курсач\cards", "*.jpg");
        int size = 0;

        DispatcherTimer dispTimer1 = new DispatcherTimer(DispatcherPriority.SystemIdle);
        DispatcherTimer dispTimer2 = new DispatcherTimer(DispatcherPriority.Send);

        void dispTimer_Tick(object sender, EventArgs e)
        {
            int currScore = Convert.ToInt32(lbUserScore.Content.ToString());
            lbUserScore.Content = currScore + 1;
            if (currScore > 3)
            {
                DispatcherTimer dt = (DispatcherTimer)sender;
                dt.Stop();

            }
        }

        public Differences()
        {
            InitializeComponent();
        }

        private int[] GetPicIndexes(int size)
        {
            Random myRand = new Random();
            List<int> usedNums = new List<int>();
            for (int i = 0; i < size; i++)
            {
                bool flFind = false;
                int randInt = 0;
                while (!flFind)
                {
                    randInt = myRand.Next(size);
                    if (usedNums.IndexOf(randInt) == -1)
                        flFind = true;
                }
                usedNums.Add(randInt);
            }
            return usedNums.ToArray();
        }

        private int[] GetNewPicIndexes(int[] prevPics, int size)
        {
            Random myRand = new Random();
            int[] result = new int[prevPics.Length];
            Array.Copy(prevPics, result, prevPics.Length);
            int div;
            switch (size)
            {
                case 8:
                    {
                        div = 6;
                        break;
                    }
                case 12:
                    {
                        div = 8;
                        break;
                    }
                default:
                    {
                        div = 10;
                        break;
                    }
            }
            int forSwap = myRand.Next(div / 2, div);
            for (int i = 0; i < forSwap; i++)
            {
                int index = myRand.Next(size);
                result[index] = myRand.Next(size + 1, 31);
            }

            return result;
        }

        private void ChangeGridLevel(object sender, RoutedEventArgs e)
        {
            ListBoxItem myItem = (ListBoxItem)sender;
            ListBox myListBox = (ListBox)myItem.Parent;
            GameGrid.Children.Clear();
            lbUserScore.Content = "00";

            switch (myItem.Content.ToString())
            {
                case "Лёгкий":
                    {
                        size = 8;
                        break;
                    }
                case "Средний":
                    {
                        size = 12;
                        break;
                    }
                case "Тяжёлый":
                    {
                        size = 16;
                        break;
                    }
            }

            indexArr = GetPicIndexes(size);
            int[] posArr = GetPicIndexes(16);
            for (int j = 0; j < size; j++)
            {
                Button btn = new Button();
                btn.Background = new ImageBrush(new BitmapImage(new Uri(dirs[indexArr[j]],
                 UriKind.Relative)));
                btn.SetValue(Grid.RowProperty, posArr[j] / 4);
                btn.SetValue(Grid.ColumnProperty, posArr[j] % 4);
                GameGrid.Children.Add(btn);
            }
            dispTimer2.Interval = TimeSpan.FromSeconds(1);
            dispTimer2.Tick += new EventHandler(dispTimer_Tick);
            dispTimer2.IsEnabled = true;

            newIndexArr = GetNewPicIndexes(indexArr, size);
            GameGrid.Children.Clear();
            for (int j = 0; j < size; j++)
            {
                Button btn = new Button();
                btn.Background = new ImageBrush(new BitmapImage(new Uri(dirs[newIndexArr[j]],
                 UriKind.Relative)));
                btn.SetValue(Grid.RowProperty, posArr[j] / 4);
                btn.SetValue(Grid.ColumnProperty, posArr[j] % 4);
                GameGrid.Children.Add(btn);
            }

        }

        
        public string Score
        {
            get { return lbUserScore.Content.ToString(); }
        }

        private void Accept_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }
    }
}
