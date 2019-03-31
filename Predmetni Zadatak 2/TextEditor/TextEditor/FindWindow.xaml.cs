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

namespace TextEditor
{
    /// <summary>
    /// Interaction logic for FindWindow.xaml
    /// </summary>
    public partial class FindWindow : Window
    {
        MainWindow mw = null;

        public FindWindow(MainWindow mainW)
        {
            InitializeComponent();
            mw = mainW;
            
        }

        private void CloseFW_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ExecuteFW_Click(object sender, RoutedEventArgs e)
        {
            if (FindRecT.Text.Trim().Equals("") || ReplaceRecT.Text.Trim().Equals(""))
            {
                MessageBoxResult result =MessageBox.Show("Nijedno od navedenih polja se ne moze ostaviti prazno!","Greska",MessageBoxButton.OK,MessageBoxImage.Error) ;
            }
            else
            {
                string texttr = new TextRange(mw.rtbEditor.Document.ContentStart,mw.rtbEditor.Document.ContentEnd).Text.Replace(FindRecT.Text,ReplaceRecT.Text);
                mw.rtbEditor.Document.Blocks.Clear();
                mw.rtbEditor.Document.Blocks.Add(new Paragraph(new Run(texttr)));
            }
        }
    }
}
