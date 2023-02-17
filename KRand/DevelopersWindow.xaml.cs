using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace KRand
{
    /// <summary>
    /// Логика взаимодействия для DevelopersWindow.xaml
    /// </summary>
    public partial class DevelopersWindow : Window
    {
        public DevelopersWindow()
        {
            InitializeComponent();
        }

        private void GarnikOpen(object sender, MouseButtonEventArgs e)
        {
            Process.Start(new ProcessStartInfo("https://vk.com/g.astral") { UseShellExecute = true });
        }

        private void YanaOpen(object sender, MouseButtonEventArgs e)
        {
            Process.Start(new ProcessStartInfo("https://vk.com/gxdstearz") { UseShellExecute = true });
        }

        private void KatyaOpen(object sender, MouseButtonEventArgs e)
        {
            Process.Start(new ProcessStartInfo("https://vk.com/wowekies") { UseShellExecute = true });
        }

        private void MironOpen(object sender, MouseButtonEventArgs e)
        {
            Process.Start(new ProcessStartInfo("https://vk.com/mrnrg") { UseShellExecute = true });
        }

        private void SashaOpen(object sender, MouseButtonEventArgs e)
        {
            Process.Start(new ProcessStartInfo("https://vk.com/cumandoor") { UseShellExecute = true });
        }
    }
}
