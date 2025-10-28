using Documents_Pyankov.Classes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
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

namespace Documents_Pyankov.Elements
{
    /// <summary>
    /// Логика взаимодействия для Item.xaml
    /// </summary>
    public partial class Item : UserControl
    {
        DocumentContext Document;
        public Item(DocumentContext documentContext)
        {
            InitializeComponent();

            try
            {
                if (File.Exists(documentContext.Src))
                {
                    img.Source = new BitmapImage(new Uri(documentContext.Src));
                }
                else
                {
                    img.Source = new BitmapImage(new Uri("pack://application:,,,/images/icon-black.png"));
                }
            }
            catch
            {
                img.Source = new BitmapImage(new Uri("pack://application:,,,/images/icon-black.png"));
            }

            lName.Content = documentContext.Name;
            lUser.Content = "Ответственный: " + documentContext.User;
            lCode.Content = "Код документа: " + documentContext.IdDocument;
            lDate.Content = "Дата поступления: " + documentContext.Date;
            lStatus.Content = documentContext.Status == 0 ? "Статус: входящий" : "Статус: исходящий";
            lDirection.Content = "Направление: " + documentContext.Direction;

            this.Document = documentContext;
        }

        private void EditDocument(object sender, RoutedEventArgs e) =>
            MainWindow.init.frame.Navigate(new Pages.Add(Document));

        private void DeleteDocument(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Вы уверены, что хотите удалить этот документ?",
                                       "Подтверждение удаления",
                                       MessageBoxButton.YesNo,
                                       MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    Document.Delete();
                    MainWindow.init.AllDocuments = new DocumentContext().AllDocuments();
                    MainWindow.init.frame.Navigate(new Pages.Main());
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при удалении: {ex.Message}", "Ошибка",
                                  MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}
