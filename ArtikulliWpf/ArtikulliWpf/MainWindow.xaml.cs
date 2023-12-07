using ArtikulliWpf.StartupHelpers;
using ArtikulliWpf.Views;
using ArtikullServices.Services;
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

namespace ArtikulliWpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly IAbstractFactory<AddProductView> _addProductView;
        private readonly IAbstractFactory<EditProducts> _EditProducts;
        private ProduktService _produktService;

        public MainWindow(IAbstractFactory<AddProductView> addProductView, IAbstractFactory<EditProducts> editProducts, ProduktService produktService)
        {
            InitializeComponent();
            this._addProductView = addProductView;
            this._produktService=produktService;
            this._EditProducts = editProducts;
            InitListsPro();
        }
        private string GetCommands(object sends)
        {
            return ((sends as Button).CommandParameter).ToString() as string;
        }
        private void InitListsPro()
        {
            var produktLists = _produktService.getProdukts();
            produktListsView.ItemsSource = produktLists;

        }
        private void Add_Product(object sender, RoutedEventArgs e)
        {
            var showProducts = _addProductView.Create();
            showProducts.Closed += (n, s) =>
            {
                InitListsPro();
            };
            showProducts.ShowDialog();
        }


        private void search_onclicks(object sender, RoutedEventArgs e)
        {
            var produktLists = _produktService.GetProduktByName(search.Text);
            produktListsView.ItemsSource = produktLists;
        }
        private void Edit_Product(object sender, RoutedEventArgs e)
        {
            var hi = GetCommands(sender);
            var editProducts = _EditProducts.Create();
            editProducts.initId(int.Parse(hi));
            editProducts.Closed += (n, s) =>
            {
                InitListsPro();
            };
            editProducts.ShowDialog();
        }
        private void Delete_Product(object sender, RoutedEventArgs e)
        {
            MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Are you sure?", "Delete Confirmation", System.Windows.MessageBoxButton.YesNo);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                var hi = GetCommands(sender);
                _produktService.deleteProdukt(int.Parse(hi));
                InitListsPro();
            }
        }

      
    }
}
