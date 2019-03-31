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
using Classes;

namespace Bends
{
    /// <summary>
    /// Interaction logic for ReadWindow.xaml
    /// </summary>
    public partial class ReadWindow : Window
    {
        public ReadWindow(Classes.Bendovi bendovi)
        {
            InitializeComponent();
           
            var uri = new Uri(bendovi.Putanja, UriKind.RelativeOrAbsolute);
            image.Source = new BitmapImage(uri);
            NameTextBox.Text = bendovi.Ime;
            DescriptionTextBox.Text = bendovi.Opis;
        }

        

        

        private void ReadWindowExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
