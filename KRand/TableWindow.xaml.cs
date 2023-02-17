using System;
using System.Collections.Generic;
using System.Data;
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
using ClosedXML.Excel;

namespace KRand
{
    /// <summary>
    /// Логика взаимодействия для TableWindow.xaml
    /// </summary>
    public partial class TableWindow : Window
    {
        //List<MyTable> result = new List<MyTable>(3);

        public TableWindow(int memAmo /*общее количество участников*/, int locAmo /*общее количество локаций*/)
        {
            InitializeComponent();
            infoLabel1.Content = "Количество участников: " + memAmo.ToString();
            infoLabel2.Content = "Количество локаций: " + locAmo.ToString();

            //переменные для +- равного разбиения количества участников в команде (количество участников в командах)
            int memInTeam1 = memAmo / locAmo;
            int memInTeam2 = memInTeam1 + 1;

            //коэффициенты для +- равного разбиения количества участников в команде 
            int coff1 = locAmo;
            int coff2 = 0;

            //номера
            int locNumb = 1; // номер локации
            int memNumb = 1; //номер участника
            int stageNumb = 1; //номер этапа
            int defaultValue = -1; //дефолтное значение для первичного заполнения двумерного массива

            //расчёт коэффициентов для +- равного разбиения количества участников в команде 
            while (((coff1 * memInTeam1) + (coff2 * memInTeam2)) != memAmo)
            {
                coff1--;
                coff2++;
            }

            //информационная панель
            infoLabel3.Content = "Количество хуёв в жопе: " + memInTeam1.ToString() + "*" + coff1.ToString() + ". Во рту: " + memInTeam2.ToString() + "*" + coff2.ToString();

            //создание двумерного массива для таблицы
            int[,] array = new int[memAmo + 1, locAmo + 1];
            string[,] data = new string[memAmo + 1, locAmo + 1];

            //массив для номеров локаций
            int[] locArr = new int[locAmo];
            for (int i = 0; i < locArr.Length; i++)
            {
                locArr[i] = i+1;
            }


            //внесение заголовков в таблицу
            for (int i = 0; i < memAmo + 1; i++)
            {
                for (int j = 0; j < locAmo + 1; j++)
                {
                    if (i == 0 && j == 0)
                    {
                        data[i, j] = "Участники";
                    }
                    else if (i == 0 && j > 0)
                    {
                        data[i, j] = "Этап " + stageNumb.ToString();
                        stageNumb++;
                    }
                    else if (j == 0 && i > 0)
                    {
                        data[i, j] = "Участник " + memNumb.ToString();
                        memNumb++;
                    }
                }
            }

            //int[] coffArr = new int[2] {coff1, coff2};

            LinkedList<int> coffList = new LinkedList<int>();
            coffList.AddLast(coff1);
            if (coff2 != 0) coffList.AddLast(coff2);

            LinkedList<int> memInTeamList = new LinkedList<int>();
            memInTeamList.AddLast(memInTeam1);
            if (coffList.Count != 1) memInTeamList.AddLast(memInTeam2);

            List<int> locList = new List<int>();
/*            for (int listCounter = 0; listCounter < locArr.Length; listCounter++)
            {
                locList.Add(locArr[listCounter]);
            }*/

            LinkedList<int>.Enumerator curCoff = coffList.GetEnumerator();
            curCoff.MoveNext();

            LinkedList<int>.Enumerator curMemInTeam = memInTeamList.GetEnumerator();
            curMemInTeam.MoveNext();

            //внесение данных в массив
            int col = locAmo;
            int row = memAmo;
            bool a = false;
            for (int j = 1, teamNumb = 0, locAmoCounter = /*locAmo*/1/*, counter = 0*/; j < col + 1; j++)
            {
/*                List<int> locList = new List<int>();
                for (int listCounter = 0; listCounter < locArr.Length; listCounter++)
                {
                    locList.Add(locArr[listCounter]);
                }*/

                //if (a) break;
                for (int i = 1; i < row + 1; i++)
                {
                    //List<int> locList = new List<int>();
                    for (int listCounter = 0; listCounter < locArr.Length; listCounter++)
                    {
                        if (listCounter + 1 != array[i, j - 1] && j != 1)
                        {
                            locList.Add(locArr[listCounter]);
                        }
                        else
                        {
                            locList.Add(locArr[(listCounter + 1) % locAmo]);
                        }
                    }
                    /*                    List<int> locList = new List<int>();
                                        for (int listCounter = 0; listCounter < locArr.Length; listCounter++)
                                        {
                                            locList.Add(locArr[listCounter]);
                                        }*/
                    array[i, j] = defaultValue;
                    //counter++;
                    if (j == 1) // заполнение первого столбца
                    {
                        if (curCoff.Current == 0) continue;
                        if (teamNumb < curCoff.Current * curMemInTeam.Current)
                        {
                            teamNumb++;
                            array[i, j] = locAmoCounter;
                            locList.Remove(array[i,j]);
                            if (teamNumb >= curCoff.Current * curMemInTeam.Current)
                            {
                                teamNumb = 0;
                                if (coffList.Count != 1)
                                {
                                    a = !curCoff.MoveNext();
                                    curMemInTeam.MoveNext();
                                }
                                if (a) break;
                            }
                            if (teamNumb % curMemInTeam.Current == 0) locAmoCounter++;
                        }
                    }
                    else // заполнение остальных столбцов в зависимости от предыдущих
                    {
                        //List<int> locList = new List<int>();
/*                        for (int listCounter = 0; listCounter < locArr.Length; listCounter++)
                        {
                            if (listCounter + 1 != array[i, j - 1] && j != 1)
                            {
                                locList.Add(locArr[listCounter]);
                            }
                            else
                            {
                                locList.Add(locArr[(listCounter + 1) % locAmo]);
                            }
                        }*/
                        //for (int k = 0; k < row-1; k++)
                        //{
                        for (int z = 2; z < col+1; z++)
                            {
                                array[i, z] = locList[0];
                                locList.Remove(array[i, z]);
                            }
                        //}
                        for (int listCounter = 0; listCounter < locArr.Length; listCounter++)
                        {
                            locList.Add(locArr[listCounter]);
                        }
                        /*                        //compare with all previous columns
                                                for (int k = 1; k < j; k++)
                                                {
                                                    if (array[i, k] == locAmoCounter)
                                                    {
                                                        //array[i, j] = locList
                                                    }
                                                }*/

                        //array[i,j] = 1;
                        /*                        for (int k = 1; k < col+1; k++)
                                                {
                                                    *//*if (k==2)
                                                    {
                                                        array[i, k] = 69;
                                                    }*//*
                                                    while (array[i,k] == array[i,k-1])
                                                    {

                                                        if (array[i, k] != array[i, k - 1])
                                                        {
                                                            break;
                                                        }
                                                    }
                                                }*/

                        /*                        while (j + 1 <= locAmo)
                                                {
                                                    while (array[i, j] == array[i, j - 1])
                                                    {
                                                        //сделать рандом
                                                        for (int k = 0; k < locArr.Length; k++)
                                                        {
                                                            if (array[i,j] != locArr[k])
                                                            {
                                                                array[i, j] = locArr[new Random().Next(0,k)];
                                                            }
                                                        }
                                                    }
                                                    j++;
                                                }*/




                        /*                        array[i, j] = defaultValue;
                                                if (array[i, j] == array[i, j - 1])
                                                {
                                                    array[i, j]++;
                                                }*//*

                        //compare with previous column
                        if (array[i, j - 1] != defaultValue)
                        {
                            array[i, j] = array[i, j - 1] + 1;
                            if (array[i, j] > locAmo) array[i, j] = 1;
                        }
                        //compare with previous row
                        if (array[i - 1, j] != defaultValue)
                        {
                            //choose anther location
                            if (array[i, j] == array[i - 1, j])
                            {
                                array[i, j]++;
                                if (array[i, j] > locAmo) array[i, j] = 1;
                            }
                        }
                        else
                        {
                            //compare with previous row
                            if (array[i - 1, j] != defaultValue)
                            {
                                array[i, j] = array[i - 1, j] + 1;
                                if (array[i, j] > locAmo) array[i, j] = 1;
                            }
                            else
                            {
                                //compare with previous row and previous column
                                if (array[i - 1, j - 1] != defaultValue)
                                {
                                    array[i, j] = array[i - 1, j - 1] + 1;
                                    if (array[i, j] > locAmo) array[i, j] = 1;
                                }
                            }
                        }*/
                    }
                }
            }

            //внесение данных в таблицу
            for (int i = 1; i < memAmo + 1; i++)
            {
                for (int j = 1; j < locAmo + 1; j++)
                {
                    data[i, j] = array[i, j].ToString();
                    //data[i, j] = "Локация " + array[i, j].ToString();
                }
            }
            membRoute.ItemsSource = data.ToDataTable().DefaultView;

        }

        //export to excel
        private void ExportToExcelButton(object sender, RoutedEventArgs e)
        {
            Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();
            excel.Visible = true;
            Microsoft.Office.Interop.Excel.Workbook workbook = excel.Workbooks.Add(System.Reflection.Missing.Value);
            Microsoft.Office.Interop.Excel.Worksheet worksheet = (Microsoft.Office.Interop.Excel.Worksheet)workbook.Sheets[1];
            for (int i = 0; i < membRoute.Items.Count; i++)
            {
                for (int j = 0; j < membRoute.Columns.Count; j++)
                {
                    Microsoft.Office.Interop.Excel.Range myRange = (Microsoft.Office.Interop.Excel.Range)worksheet.Cells[i + 2, j + 2];
                    myRange.Value2 = membRoute.Columns[j].GetCellContent(membRoute.Items[i]) is TextBlock ? (membRoute.Columns[j].GetCellContent(membRoute.Items[i]) as TextBlock).Text : "";
                }
            }
            worksheet.Columns.AutoFit();
            
            //create borders for cells
            Microsoft.Office.Interop.Excel.Range range = worksheet.UsedRange;
            range.Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
            range.Borders.Weight = 2d;
            
            //cells position center
            range.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
            range.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
            
        }
    }
}