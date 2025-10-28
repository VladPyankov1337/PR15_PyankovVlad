using Documents_Pyankov.Classes;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Documents_Pyankov.Pages
{
    /// <summary>
    /// Логика взаимодействия для Main.xaml
    /// </summary>
    public partial class Main : Page
    {
        public Main()
        {
            InitializeComponent();
            CreateUI();
        }

        public void CreateUI()
        {
            Parent.Children.Clear();
            foreach (DocumentContext item in MainWindow.init.AllDocuments)
                Parent.Children.Add(new Elements.Item(item));
        }

        private void Exit(object sender, RoutedEventArgs e)
        {
            MainWindow.init.Close();
        }

        private void Add(object sender, RoutedEventArgs e)
        {
            MainWindow.init.frame.Navigate(new Pages.Add());
        }
    }
}
