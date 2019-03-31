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
using Microsoft.Win32;
using Classes;

namespace Bends
{
    /// <summary>
    /// Interaction logic for AddWindow.xaml
    /// </summary>
    public partial class AddWindow : Window
    {
        public AddWindow()
        {
            InitializeComponent();

            GenreComboBox.ItemsSource = Classes.Container.listaZanrova;
        }

        private string s = "";

        private void BrowseButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();

            openFile.Title = "Izaberite sliku";
            openFile.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" + "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" + "Portable Network Graphic (*.png)|*.png";

            if (openFile.ShowDialog() == true)
            {
                s = openFile.FileName;
                image.Source = new BitmapImage(new Uri(openFile.FileName));
            }
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            Classes.Bendovi noviBend;

            if (validate())
            {
               // var d = DateTime.Now;
               // var dd = d.Date.ToShortDateString();
                noviBend = new Classes.Bendovi(NameTextBox.Text, OriginTextBox.Text, GenreComboBox.SelectedValue.ToString(), int.Parse(YearTextBox.Text), DescriptionTextBox.Text, s,Vreme.Date());

                MainWindow.Bendss.Add(noviBend);
                this.Close();
            }
            else
            {
                MessageBox.Show("Podaci koje ste uneli nisu validni.", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private bool validate()
        {
            bool res = true;

            if (NameTextBox.Text.Trim().Equals(""))
            {
                res = false;
                NameTextBox.BorderBrush = Brushes.DarkRed;
                //NameTextBox.BorderThickness = new Thickness(2);
                NameError.Content = "Ovo bolje ne sme biti prazno!";

            }
            else if (CheckFirstNumber(NameTextBox.Text))
            {
                res = false;
                NameTextBox.BorderBrush = Brushes.DarkRed;
               // NameTextBox.BorderThickness = new Thickness(2);
                NameError.Content = "Ovo polje ne sme poceti sa brojem!";

            }
            else if (IsEqual(NameTextBox.Text))
            {
                res = false;
                NameTextBox.BorderBrush = Brushes.DarkRed;
                // NameTextBox.BorderThickness = new Thickness(2);
                NameError.Content = "Bend vec postoji!";
            }
            else
            {
                NameTextBox.BorderBrush = Brushes.Green;
                NameError.Content = "";
            }

            if (OriginTextBox.Text.Trim().Equals(""))
            {
                res = false;
                OriginTextBox.BorderBrush = Brushes.DarkRed;
               // OriginTextBox.BorderThickness = new Thickness(2);
                OriginError.Content = "Ovo polje ne sme biti prazno!";
            }
            else if (CheckSpecial(OriginTextBox.Text))
            {
                res = false;
                OriginTextBox.BorderBrush = Brushes.DarkRed;
               // OriginTextBox.BorderThickness = new Thickness(2);
                OriginError.Content = "Specijalni znakovi nisu validni!";
            }
            else if (Num(OriginTextBox.Text))
            {
                res = false;
                OriginTextBox.BorderBrush = Brushes.DarkRed;
               // OriginTextBox.BorderThickness = new Thickness(2);
                OriginError.Content = "Ovo polje ne sme da sadrzi brojeve!";
            }
            else
            {
                OriginTextBox.BorderBrush = Brushes.Green;
                OriginError.Content = "";
            }

            if (GenreComboBox.SelectedItem == null)
            {
                res = false;
                GenreComboBox.BorderBrush = Brushes.DarkRed;
                //GenreComboBox.BorderThickness = new Thickness(2);
                GenreError.Content = "Morate da izabrati zanr!";
            }
            else
            {
                GenreComboBox.BorderBrush = Brushes.Green;
                GenreError.Content = "";
            }

            if (s.Equals(""))
            {
                res = false;
                //ImageBorder.BorderBrush = Brushes.DarkRed;
                ImageError.Content = "Morate da izaberete sliku!";

            }
            else
            {
                //ImageBorder.BorderBrush = Brushes.Green;
                ImageError.Content = "";
            }


            if (DescriptionTextBox.Text.Trim().Equals(""))
            {
                res = false;
                DescriptionTextBox.BorderBrush = Brushes.Red;
                //DescriptionTextBox.BorderThickness = new Thickness(2);
                DescriptionError.Content = "Ovo polje ne sme da bude prazno!";
            }
            else if (DescriptionTextBox.Text.Length < 20)
            {
                res = false;
                DescriptionTextBox.BorderBrush = Brushes.Red;
                //DescriptionTextBox.BorderThickness = new Thickness(2);
                DescriptionError.Content = "Opis mora da sadrzi bar 20 karaktera!";
            }
            else
            {
                DescriptionTextBox.BorderBrush = Brushes.Green;
                DescriptionError.Content = "";
            }
            int p = 0;

            if (YearTextBox.Text.Trim().Equals(""))
            {
                res = false;
                YearTextBox.BorderBrush = Brushes.Red;
               // YearTextBox.BorderThickness = new Thickness(2);
                YearError.Content = "Ovo polje ne sme da bude prazno!";
            }
            
            else if(!int.TryParse(YearTextBox.Text.Trim(), out p) || p<=0)
            {
                res = false;
                YearTextBox.BorderBrush = Brushes.Red;
                //YearTextBox.BorderThickness = new Thickness(2);
                YearError.Content = "Morate uneti pozitivan celi broj!";
                
            }
            else
            {
                YearTextBox.BorderBrush = Brushes.Green;
                YearError.Content = "";
            }

            return res;
        }

        private bool CheckFirstNumber(string inp)
        {
            bool rcfn = false;

            char[] pom = inp.ToCharArray();

            if (Char.IsDigit(pom[0]))
            {
                rcfn = true;
            }
            return rcfn;
        }

        private bool CheckSpecial(string inp)
        {
            bool rcs = inp.IndexOfAny("[](){}*,:=;...#".ToCharArray()) != -1;

            return rcs;
        }

        private bool Num(string inp)
        {
            bool rn = inp.IndexOfAny("0123456789".ToCharArray())!=-1;

            return rn;
        }

        private bool IsEqual(string inp)
        {
            bool rie = false;

            foreach(var item in MainWindow.Bendss)
            {
                if (item.Ime.Contains(inp))
                {
                    rie = true;
                    break;
                }
            }

            return rie;
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
