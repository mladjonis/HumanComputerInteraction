using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
using Microsoft.Win32;
using System.IO;


namespace TextEditor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static int ind;

        public MainWindow()
        {
            InitializeComponent();
            cmbFontFamily.ItemsSource = Fonts.SystemFontFamilies.OrderBy(f => f.Source);
            cmbFont.ItemsSource = new List < double > (){ 8, 9, 10, 11, 12, 14, 16, 18, 20, 22, 24, 26, 28, 36, 48, 72 };
            cmbColor.ItemsSource = typeof(Colors).GetProperties();
            cmbColor.SelectedItem = cmbColor.Items[7];
            DataContext = this;
            
        }

        private void rtbEditor_SelectionChanged(object sender, RoutedEventArgs e)
        {
            object temp = rtbEditor.Selection.GetPropertyValue(Inline.FontWeightProperty);
            btnBold.IsChecked = (temp != DependencyProperty.UnsetValue) && (temp.Equals(FontWeights.Bold));

            temp = rtbEditor.Selection.GetPropertyValue(Inline.TextDecorationsProperty);
            btnUnderline.IsChecked = (temp != DependencyProperty.UnsetValue) && (temp.Equals(TextDecorations.Underline));

            temp = rtbEditor.Selection.GetPropertyValue(Inline.FontStyleProperty);
            btnItalic.IsChecked = (temp != DependencyProperty.UnsetValue) && (temp.Equals(FontStyles.Italic));

            temp = rtbEditor.Selection.GetPropertyValue(Inline.FontFamilyProperty);
            cmbFontFamily.SelectedItem = temp;

            temp = rtbEditor.Selection.GetPropertyValue(Inline.FontSizeProperty);
            cmbFont.Text = temp.ToString();

            temp = rtbEditor.Selection.GetPropertyValue(Inline.ForegroundProperty);
            cmbColor.SelectedItem = temp;

            
        }

        private void cmbFontFamily_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbFontFamily.SelectedItem != null)
                rtbEditor.Selection.ApplyPropertyValue(Inline.FontFamilyProperty, cmbFontFamily.SelectedItem);
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            if (ind == 0)
            {
                this.Close();
            }

            else
            {
                MessageBoxResult result = MessageBox.Show("Da li zelite da sacuvate?", "", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    SaveFileDialog dlg = new SaveFileDialog();
                    dlg.Filter = "Rich Text Format (*.rtf)|*.rtf|All files (*.*)|*.*";
                    if (dlg.ShowDialog() == true)
                    {
                        FileStream fileStream = new FileStream(dlg.FileName, FileMode.Create);
                        TextRange range = new TextRange(rtbEditor.Document.ContentStart, rtbEditor.Document.ContentEnd);
                        range.Save(fileStream, DataFormats.Rtf);
                    }

                    this.Close();
                }

                else
                {
                    this.Close();
                }
            }
        }
        /*
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string richText = new TextRange(rtbEditor.Document.ContentStart, rtbEditor.Document.ContentEnd).Text;
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            if (saveFileDialog.ShowDialog() == true)
                File.WriteAllText(saveFileDialog.FileName, richText);

            saveFileDialog.Filter = "Text file (*.txt)|*.txt|C# file (*.cs)|*.cs";
        }

        /*private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            //string richText = new TextRange(rtbEditor.Document.ContentStart, rtbEditor.Document.ContentEnd).Text;
            OpenFileDialog openFileDialog = new OpenFileDialog();

            if (openFileDialog.ShowDialog() == true)
            {
                rtbEditor.Document.Blocks.Clear();
                rtbEditor.Document.Blocks.Add(new Paragraph(new Run(File.ReadAllText(openFileDialog.FileName))));
                //richText = File.ReadAllText(openFileDialog.FileName);
            }
        }
        */
        private void Date_Time(object sender, ExecutedRoutedEventArgs e)
        {
            var txtrange = new TextRange(rtbEditor.CaretPosition, rtbEditor.CaretPosition);
            //var selection = rtbEditor.Selection;
           // var whole = new TextRange(rtbEditor.Document.ContentStart, rtbEditor.Document.ContentEnd).Text;
            //var wholenotnull = whole.Substring(0, whole.Length - 2);
            rtbEditor.Selection.Text = DateTime.Now.ToString();
            //txtrange.Text = DateTime.Now.ToString();
            /*
            if (selection.Text == whole)
            {
                rtbEditor.Document.Blocks.Clear();
                rtbEditor.CaretPosition = rtbEditor.Document.ContentStart;

                txtrange.Text = DateTime.Now.ToString();

            }
            else if (selection.Text == wholenotnull)
            {
                rtbEditor.Document.Blocks.Clear();
                rtbEditor.CaretPosition = rtbEditor.Document.ContentStart;

                txtrange.Text = DateTime.Now.ToString();
            }
            else if (selection.Text != whole)
            {
                selection.Text = "";
                selection.Text = DateTime.Now.ToString();
            }*/
            if (rtbEditor.CaretPosition.GetPositionAtOffset(txtrange.Text.Length) != null)
            {
                rtbEditor.CaretPosition = rtbEditor.CaretPosition.GetPositionAtOffset(txtrange.Text.Length);
            }
            rtbEditor.Focus();

            DTL.Content = DateTime.Now.ToString();
        }

        private void rtbEditor_TextChanged(object sender, TextChangedEventArgs e)
        {
             ind = 1;

            TextRange tr = new TextRange(rtbEditor.Document.ContentStart,rtbEditor.Document.ContentEnd);

            MatchCollection wc = Regex.Matches(tr.Text,@"[\S]+");
            WCounter.Content = "Broj reci: " + wc.Count.ToString();
        }

        private void FR_Click(object sender, RoutedEventArgs e)
        {
            FindWindow FR = new FindWindow(this);
            FR.ShowDialog();

        }


        private void cmbFont_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbFont.SelectedItem != null)
                rtbEditor.Selection.ApplyPropertyValue(Inline.FontSizeProperty, cmbFont.SelectedItem);
        }

       /* private void fontColorPokusaj3(RichTextBox rb)
        {

            var colorDialog = new System.Windows.Forms.ColorDialog();
            if (colorDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                var col = Color.FromArgb(colorDialog.Color.A, colorDialog.Color.R, colorDialog.Color.G, colorDialog.Color.B);
                TextRange ra = new TextRange(rb.Selection.Start, rb.Selection.End);
                ra.ApplyPropertyValue(FlowDocument.ForegroundProperty, new SolidColorBrush(col));
                rtbEditor.Selection.ApplyPropertyValue(Inline.ForegroundProperty, new SolidColorBrush(col));
                rtbEditor.Focus();
                //nasao na netu rati ne diraj...
            }
        }

        private void ColorButtom_Click(object sender, RoutedEventArgs e)
        {
            fontColorPokusaj3(rtbEditor);
        }
        */
        private void cmbColor_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(cmbColor.SelectedItem!=null)
                rtbEditor.Selection.ApplyPropertyValue(Inline.ForegroundProperty, (SolidColorBrush)new BrushConverter().ConvertFromString(cmbColor.SelectedItem.ToString().Replace("System.Windows.Media.Color","")));

            rtbEditor.Focus();
        }

        private void NewButton_Click(object sender, RoutedEventArgs e)
        {
            if (ind == 0)
            {
                rtbEditor.Document.Blocks.Clear();
                WCounter.Content = "Broj reci: " + "0";
                ind = 0;


            }
            else
            {
                MessageBoxResult res = MessageBox.Show("Zelite li da sacuvate?", "", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (res == MessageBoxResult.Yes)
                {
                    SaveFileDialog dl = new SaveFileDialog();
                    dl.Filter = "Rich Text Format (*.rtf)|*.rtf|All files (*.*)|*.*";
                    if (dl.ShowDialog() == true)
                    {
                        FileStream fs = new FileStream(dl.FileName, FileMode.Create);
                        TextRange ra = new TextRange(rtbEditor.Document.ContentStart, rtbEditor.Document.ContentEnd);
                        ra.Save(fs, DataFormats.Rtf);
                    }
                }

                rtbEditor.Document.Blocks.Clear();
                ind = 0;
                WCounter.Content = "Broj reci: " + "0";
            }
        }

        private void OpenButton_Click(object sender, RoutedEventArgs e)
        {
            if (ind == 0)
            {
                OpenFileDialog dl = new OpenFileDialog();
                dl.Filter = "Rich Text Format (*.rtf)|*.rtf|All files (*.*)|*.*";
                if (dl.ShowDialog() == true)
                {
                    FileStream fs= new FileStream(dl.FileName, FileMode.Open);
                    TextRange range = new TextRange(rtbEditor.Document.ContentStart, rtbEditor.Document.ContentEnd);
                    range.Load(fs, DataFormats.Rtf);
                }
            }

            else
            {
                MessageBoxResult res = MessageBox.Show("Zelite li da sacuvate fajl?", "", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (res == MessageBoxResult.Yes)
                {
                    SaveFileDialog dl = new SaveFileDialog();
                    dl.Filter = "Rich Text Format (*.rtf)|*.rtf|All files (*.*)|*.*";
                    if (dl.ShowDialog() == true)
                    {
                        FileStream fs = new FileStream(dl.FileName, FileMode.Create);
                        TextRange range = new TextRange(rtbEditor.Document.ContentStart, rtbEditor.Document.ContentEnd);
                        range.Save(fs, DataFormats.Rtf);
                    }
                }

                OpenFileDialog dlg2 = new OpenFileDialog();
                dlg2.Filter = "Rich Text Format (*.rtf)|*.rtf|All files (*.*)|*.*";
                if (dlg2.ShowDialog() == true)
                {
                    FileStream fileStream = new FileStream(dlg2.FileName, FileMode.Open);
                    TextRange range = new TextRange(rtbEditor.Document.ContentStart, rtbEditor.Document.ContentEnd);
                    range.Load(fileStream, DataFormats.Rtf);
                    ind = 1;
                }
               

            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (ind == 0)
            {
                var result = MessageBox.Show("Ne mozete sacuvati prazan fajl", "", MessageBoxButton.OK, MessageBoxImage.Warning);
            }

            else
            {
                SaveFileDialog dlg = new SaveFileDialog();
                dlg.Filter = "Rich Text Format (*.rtf)|*.rtf|All files (*.*)|*.*";
                if (dlg.ShowDialog() == true)
                {
                    FileStream fileStream = new FileStream(dlg.FileName, FileMode.Create);
                    TextRange range = new TextRange(rtbEditor.Document.ContentStart, rtbEditor.Document.ContentEnd);
                    range.Save(fileStream, DataFormats.Rtf);
                    ind = 0;
                }
            }
        }


    }
        }

