using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using Classes;

namespace Bends
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DataIO serializer = new DataIO();
        public static BindingList<Classes.Bendovi> Bendss { get; set; }

        public MainWindow()
        {
            Bendss = serializer.DeSerializeObject<BindingList<Classes.Bendovi>>("bendss.xml");

            if (Bendss == null)
            {
                Bendss = new BindingList<Classes.Bendovi>();
            }

            DataContext = this;

            InitializeComponent();
        }

        

        private void MainButton2_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Read_OnClick(object sender, RoutedEventArgs e)
        {
            ReadWindow readWindow = new ReadWindow((Bendovi)glavniGrid.SelectedItem);
            readWindow.ShowDialog();
        }

        private void Change_OnClick(object sender, RoutedEventArgs e)
        {
            ChangeWindow changeWindow = new ChangeWindow((Bendovi)glavniGrid.SelectedItem);
            changeWindow.ShowDialog();
            glavniGrid.Items.Refresh();
        }

        private void Delete_OnClick(object sender, RoutedEventArgs e)
        {
            Bendss.RemoveAt(glavniGrid.SelectedIndex);
        }

        private void Save(object sender, CancelEventArgs e)
        {
            serializer.SerializeObject<BindingList<Classes.Bendovi>>(Bendss, "bendss.xml");
        }

        private void MainButton1_Click(object sender, RoutedEventArgs e)
        {
            AddWindow addWindow = new AddWindow();
            addWindow.ShowDialog();
        }
    }
}
