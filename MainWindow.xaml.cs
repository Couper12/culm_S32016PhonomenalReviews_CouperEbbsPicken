/*
 * Couper Ebbs-Picken  
 * 6/11/2018
 * do some problem solving with trees
 */ 
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

namespace culm_S3PhonomenalReviews_CouperEbbsPicken
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        StreamReader streamReader;
        int N;
        int M;
        int[] phoRests;
        int[] spaces;
        int tempInt;
        string tempStr;
        string[] paths;
        int time;
        int position;
        int lowestPosition;

        public MainWindow()
        {
            InitializeComponent();

            // initializing variables
            position = 0;
            streamReader = new StreamReader("Input.txt");
            string input1 = streamReader.ReadLine();
            int.TryParse(input1.Substring(0, input1.IndexOf(' ')), out N);
            int.TryParse(input1.Substring(input1.IndexOf(' ')), out M);
            string input2 = streamReader.ReadLine();
            phoRests = new int[M];
            paths = new string[N - 1];
            spaces = new int[M - 1];
            int counter = 0;

            // making array of ' ' positions
            foreach (char c in input2)
            {
                if (c == ' ')
                {
                    spaces[counter] = input2.IndexOf(c);
                    counter++;
                }
            }

            // adding first pho restaurant
            tempStr = input2.Substring(0, spaces[0]);
            int.TryParse(tempStr, out tempInt);
            phoRests[0] = tempInt;

            // adding the middle pho restaurants
            for (int i = 0; i < M - 2; i++)
            {
                tempStr = input2.Substring(spaces[i] + 1, spaces[i + 1] - (spaces[i] + 1));
                int.TryParse(tempStr, out tempInt);
                phoRests[i + 1] = tempInt;
            }

            // adding the last pho restaurant
            tempStr = input2.Substring(spaces[M - 2] + 1);
            int.TryParse(tempStr, out tempInt);
            phoRests[phoRests.Length - 1] = tempInt;

            // adds an array of all the paths
            counter = 0;
            while (!streamReader.EndOfStream)
            {
                tempStr = streamReader.ReadLine();
                paths[counter] = tempStr;
                counter++;
            }

            // sets some variables
            int lowest = 5000;
            lowestPosition = 0;

            // checks if two of the pho's are connected as a starting point
            foreach (int tempInt in phoRests)
            {
                foreach (string tempStr in paths)
                {
                    if (tempInt.ToString() == tempStr.Substring(0, tempStr.IndexOf(' ')))
                    {
                        counter++;
                    }
                    else if (tempInt.ToString() == tempStr.Substring(tempStr.IndexOf(' ')))
                    {
                        counter++;
                    } 
                }

                if (counter < lowest)
                {
                    lowest = counter;
                    lowestPosition = tempInt;
                }
                counter = 0;
            }
            position = lowestPosition;

            // will check for shortest path
            foreach(int tempInt in phoRests)
            {
                if (tempInt != position)
                {
                    for (int j = 0; j < paths.Length; j++)
                    {
                        if (paths[j].Contains(position.ToString())
                            && paths[j].Contains(tempInt.ToString()))
                        {
                            position = tempInt;
                            time++;
                            for (int i = 0; i < phoRests.Length; i++)
                            {
                                if (phoRests[i] == tempInt)
                                {
                                    phoRests[i] = -1;
                                }
                            }
                        }                            
                    }
                }
            }
            

        }
    }
}
