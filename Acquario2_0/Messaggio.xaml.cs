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

namespace Acquario2_0
{
    /// <summary>
    /// Logica di interazione per Messaggio.xaml
    /// </summary>
    public partial class Messaggio : Window
    {
        public Messaggio()
        {
            InitializeComponent();
        }

        private async void Apertura(object sender, RoutedEventArgs e)
        {
            await Task.Delay(2000);
            Close();
        }
    }
}
