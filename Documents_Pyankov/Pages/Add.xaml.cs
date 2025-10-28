using Documents_Pyankov.Classes;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace Documents_Pyankov.Pages
{
    /// <summary>
    /// Логика взаимодействия для Add.xaml
    /// </summary>
    public partial class Add : Page
    {
        DocumentContext Document;
        string s_src = "";

        public Add(DocumentContext documentContext = null)
        {
            InitializeComponent();

            if (documentContext != null)
            {
                Document = documentContext;

                if (File.Exists(documentContext.Src))
                {
                    s_src = documentContext.Src;
                    src.Source = new BitmapImage(new Uri(documentContext.Src));
                }

                tbName.Text = documentContext.Name;
                tbUser.Text = documentContext.User;
                tbCode.Text = documentContext.IdDocument.ToString();
                tbDate.Text = documentContext.Date;
                tbStatus.SelectedIndex = documentContext.Status;
                tbDirection.Text = documentContext.Direction;
            }
        }

        private void Back(object sender, RoutedEventArgs e) =>
            MainWindow.init.frame.Navigate(new Main());

        private void SelectImage(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.InitialDirectory = "c:\\";
            ofd.Filter = "PNG (*.png)|*.png|JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|All files (*.*)|*.*";
            ofd.FilterIndex = 1;
            ofd.ShowDialog();

            if (!string.IsNullOrEmpty(ofd.FileName))
            {
                try
                {
                    src.Source = new BitmapImage(new Uri(ofd.FileName));
                    s_src = ofd.FileName;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка загрузки изображения: {ex.Message}");
                }
            }
        }

        private void AddDocument(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(s_src))
            {
                MessageBox.Show("Необходимо выбрать изображение");
                return;
            }
            if (string.IsNullOrWhiteSpace(tbName.Text))
            {
                MessageBox.Show("Необходимо указать наименование");
                return;
            }
            if (string.IsNullOrWhiteSpace(tbUser.Text))
            {
                MessageBox.Show("Необходимо указать ответственного");
                return;
            }
            if (string.IsNullOrWhiteSpace(tbCode.Text))
            {
                MessageBox.Show("Необходимо указать код документа");
                return;
            }
            if (string.IsNullOrWhiteSpace(tbDate.Text))
            {
                MessageBox.Show("Необходимо указать дату поступления");
                return;
            }
            if (tbStatus.SelectedIndex == -1)
            {
                MessageBox.Show("Необходимо выбрать статус документа");
                return;
            }
            if (string.IsNullOrWhiteSpace(tbDirection.Text))
            {
                MessageBox.Show("Необходимо указать направление");
                return;
            }

            if (!int.TryParse(tbCode.Text, out int documentId))
            {
                MessageBox.Show("Код документа должен быть целым числом");
                return;
            }

            try
            {
                if (Document == null)
                {
                    Document = new DocumentContext()
                    {
                        Src = s_src,
                        Name = tbName.Text.Trim(),
                        User = tbUser.Text.Trim(),
                        IdDocument = documentId,
                        Date = tbDate.Text.Trim(),
                        Status = tbStatus.SelectedIndex,
                        Direction = tbDirection.Text.Trim()
                    };
                    Document.Save();
                    MessageBox.Show("Документ добавлен");
                }
                else
                {
                    Document.Src = s_src;
                    Document.Name = tbName.Text.Trim();
                    Document.User = tbUser.Text.Trim();
                    Document.IdDocument = documentId;
                    Document.Date = tbDate.Text.Trim();
                    Document.Status = tbStatus.SelectedIndex;
                    Document.Direction = tbDirection.Text.Trim();
                    Document.Save(true);
                    MessageBox.Show("Документ изменён");
                }

                MainWindow.init.AllDocuments = new DocumentContext().AllDocuments();
                MainWindow.init.frame.Navigate(new Main());
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка: {ex.Message}");
            }
        }
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void tbCode_Pasting(object sender, DataObjectPastingEventArgs e)
        {
            if (e.DataObject.GetDataPresent(typeof(string)))
            {
                string text = (string)e.DataObject.GetData(typeof(string));
                if (!int.TryParse(text, out _))
                {
                    e.CancelCommand();
                }
            }
            else
            {
                e.CancelCommand();
            }
        }
    }
}
