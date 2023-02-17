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

namespace KRand
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int memAmount;
        int locAmount;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void TextBoxInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = "0123456789".IndexOf(e.Text) < 0;
        }

        private void OpenDevWindowButton(object sender, RoutedEventArgs e)
        {
            DevelopersWindow devWind = new DevelopersWindow();
            devWind.Show();
        }
        private void DeleteSpaces(object sender, TextChangedEventArgs e)
        {
            membNumb.Text = membNumb.Text.Replace(" ", string.Empty);
            membNumb.SelectionStart = membNumb.Text.Length;
        }

        private void TableGenerationButton(object sender, RoutedEventArgs e)
        {
            TableGeneration();
        }

        private void EnterButton(object sender, KeyEventArgs e)
        {
            TableGeneration();
        }

        private void TableGeneration()
        {
            memAmount = Convert.ToInt32(membNumb.Text);

            if (memAmount == 0)
            {
                memAmount = 1;
            }

            string caseAmo = locationBox.Text;
            switch (caseAmo)
            {
                case "1 локация":
                    locAmount = 1;
                    break;
                case "2 локации":
                    locAmount = 2;
                    break;
                case "3 локации":
                    locAmount = 3;
                    break;
                case "4 локации":
                    locAmount = 4;
                    break;
                case "5 локаций":
                    locAmount = 5;
                    break;
                case "6 локаций":
                    locAmount = 6;
                    break;
                case "7 локаций":
                    locAmount = 7;
                    break;
                case "8 локаций":
                    locAmount = 8;
                    break;
                case "9 локаций":
                    locAmount = 9;
                    break;
                case "10 локаций":
                    locAmount = 10;
                    break;
                case "11 локаций":
                    locAmount = 11;
                    break;
                case "12 локаций":
                    locAmount = 12;
                    break;
                case "13 локаций":
                    locAmount = 13;
                    break;
                case "14 локаций":
                    locAmount = 14;
                    break;
                case "15 локаций":
                    locAmount = 15;
                    break;
                case "16 локаций":
                    locAmount = 16;
                    break;
                case "17 локаций":
                    locAmount = 17;
                    break;
                case "18 локаций":
                    locAmount = 18;
                    break;
                case "19 локаций":
                    locAmount = 19;
                    break;
                case "20 локаций":
                    locAmount = 20;
                    break;
                default:
                    locAmount = 0;
                    break;
            }

            TableWindow tableWindow = new TableWindow(memAmount, locAmount);
            tableWindow.Show();
        }
    }
}
