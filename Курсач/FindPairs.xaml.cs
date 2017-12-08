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
using System.Windows.Media.Animation;
using System.Threading;

namespace Курсач
{
    /// <summary>
    /// Interaction logic for FindPairs.xaml
    /// </summary>
    public partial class FindPairs : Window
    {
        public FindPairs()
        {
            InitializeComponent();
        }

        public string Score 
        {
            get { return lbUserScore.Content.ToString(); }
        }

        private const int  RIGHT_PAIR = 8;
        private const int WRONG_PAIR = -1;

        private int[] indexArr = new int[0];
        private string[] dirs = Directory.GetFiles(@"C:\Users\darya\Documents\Visual Studio 2012\Projects\Курсач\Курсач\cards", "*.jpg");
        private int size = 0;
        private int numClicks = 0;
        int pic1X = 0, pic2X = 0;
        int pic1Y = 0, pic2Y = 0;
        bool DoHide = false;


        private int[] GetPicIndexes (string[] numPic, int size)
        {
            Random myRand = new Random();
            List<int> usedNums = new List<int>();
            for (int i = 0; i < size * size; i++)
            {
                bool flFind = false;
                int randInt = 0;
                while (!flFind)
                {
                    randInt = myRand.Next(size * size / 2);
                    if (usedNums.IndexOf(randInt) != -1)
                    {
                        if ((usedNums.LastIndexOf(randInt)) != (usedNums.IndexOf(randInt)))
                            flFind = false;
                        else
                            flFind = true;
                    }
                    else
                        flFind = true;
                }
                usedNums.Add(randInt);
            }
            return usedNums.ToArray();
        }

        private void ChangeGridLevel(object sender, RoutedEventArgs e)
        {
            ListBoxItem myItem = (ListBoxItem)sender;
            ListBox myListBox = (ListBox)myItem.Parent;
            GameGrid.Children.Clear();
            lbUserScore.Content = "00";
            numClicks = 0;
            pic1X = pic2X = pic1Y = pic2Y = 0;

            switch (myItem.Content.ToString())
            {
                case "Лёгкий":
                    {
                        size = 4;
                        GameGrid.Columns = 4;
                        GameGrid.Rows = 4;
                        break;
                    }
                case "Средний":
                    {
                        size = 6;
                        GameGrid.Columns = 6;
                        GameGrid.Rows = 6;
                        break;
                    }
                case "Тяжёлый":
                    {
                        size = 8;
                        GameGrid.Columns = 8;
                        GameGrid.Rows = 8;
                        break;
                    }
            }
                 
            indexArr = GetPicIndexes(dirs, size);
            ImageBrush myBrush = new ImageBrush(new BitmapImage(new Uri(@"C:\Users\darya\Documents\Visual Studio 2012\Projects\Курсач\Курсач\cloud.jpg",
                              UriKind.Relative)));
            for (int i = 0; i < size; i++)
                for (int j = 0; j < size; j++)
                {
                    Button btn = new Button();
                    //btn.SetValue(BackgroundProperty, myBrush);
                    btn.Background = new ImageBrush(new BitmapImage(new Uri(dirs[indexArr[i * size + j]],
                     UriKind.Relative)));
                    btn.SetValue(Grid.RowProperty, i);
                    btn.SetValue(Grid.ColumnProperty, j);
                    btn.Click += InputImage;
                    GameGrid.Children.Add(btn);
                }
            
        }


        private void InputImage(object sender, RoutedEventArgs e)
        {
            Button myButton = (Button)sender;
            Button prevButton = new Button();
            System.Windows.Controls.Primitives.UniformGrid myGrid = (System.Windows.Controls.Primitives.UniformGrid)myButton.Parent;
            int i = Grid.GetRow(myButton);
            int j = Grid.GetColumn(myButton);

            ImageBrush changeBrush = new ImageBrush(new BitmapImage(new Uri(dirs[indexArr[i * size + j]],
                               UriKind.Relative)));
            ImageBrush myBrush = new ImageBrush(new BitmapImage(new Uri(@"C:\Users\darya\Documents\Visual Studio 2012\Projects\Курсач\Курсач\cloud.jpg",
                               UriKind.Relative)));


            myButton.Background = new ImageBrush(new BitmapImage(new Uri(dirs[indexArr[i * size + j]],
                     UriKind.Relative)));
            numClicks++;
          
            if (numClicks == 2)
            {
                pic2X = Grid.GetRow(myButton);
                pic2Y = Grid.GetColumn(myButton);

                if (!(indexArr[pic1X * size + pic1Y] == indexArr[i * size + j]))
                {

                    foreach (Button x in myGrid.Children)
                    {
                        if ((Grid.GetColumn(x) == pic1Y) && (Grid.GetRow(x) == pic1X))
                        {
                            prevButton = x;
                            myButton.Background = new ImageBrush(new BitmapImage(new Uri(dirs[indexArr[i * size + j]],
                                     UriKind.Relative)));

                            x.Background = myBrush;
                            break;
                        }
                    }

                    int currScore = Convert.ToInt32(lbUserScore.Content.ToString());
                    lbUserScore.Content = currScore + WRONG_PAIR;
                 
                    myButton.Background = new ImageBrush(new BitmapImage(new Uri(dirs[indexArr[i * size + j]],
                             UriKind.Relative)));
                    prevButton.Background = myBrush;
                    myButton.Background = myBrush;
                }
                else
                {
                    foreach (Button x in myGrid.Children)
                    {
                        if ((Grid.GetColumn(x) == pic1Y) && (Grid.GetRow(x) == pic1X))
                        {
                            x.Click -= InputImage;
                            break;
                        }
                    }
                    myButton.Click -= InputImage;
                    int currScore = Convert.ToInt32(lbUserScore.Content.ToString());
                    lbUserScore.Content = currScore + RIGHT_PAIR;
                }
                numClicks = 0;
            }
            else
                if (numClicks == 1)
                {
                    pic1X = Grid.GetRow(myButton);
                    pic1Y = Grid.GetColumn(myButton);
                }
        }

     
        private void Accept_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

    }
}
