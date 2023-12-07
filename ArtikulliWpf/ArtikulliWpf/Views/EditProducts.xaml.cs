using ArtikullServices.Models;
using ArtikullServices.Services;
using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ArtikulliWpf.Views
{
    /// <summary>
    /// Interaction logic for EditProducts.xaml
    /// </summary>
    public partial class EditProducts : Window
    {

        private ProduktService _produktService;
        public int id { get; set; }
        public EditProducts(ProduktService produktService)
        {
            InitializeComponent();
            this._produktService = produktService;
        }
        public void initId(int id)
        {
            this.id = id;
            InitComponent();
            Lloj.ItemsSource = Enum.GetValues(typeof(Lloj)).Cast<Lloj>();
            Tipi.ItemsSource = Enum.GetValues(typeof(ETipiProdukt)).Cast<ETipiProdukt>();
        }
        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            var textBox = sender as TextBox;
            e.Handled = Regex.IsMatch(e.Text, "[^0-9]+");
        }
        private void InitComponent()
        {
            Produkt produkt = _produktService.getProductById(id);
            Emertimi.Text = produkt.Emertimi;
            Njesia.Text = produkt.Njesia;
            DataSkadences.SelectedDate=produkt.DataSkadences;
            Cmimi.Text = ""+ produkt.Cmimi;
            Lloj.SelectedItem = produkt.Lloj;
            KaTvsh.IsChecked = produkt.KaTvsh;
            Tipi.SelectedItem = produkt.Tipi;
            BarkodKod.Text = produkt.BarkodKod;
        }

        private void Change_Clicked(object sender, RoutedEventArgs e)
        {
            if (this.id == null)
            {
                MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("error", "errors", System.Windows.MessageBoxButton.OK);
                return;
            }
            bool checkBox = false;
            if (DataSkadences.SelectedDate == null)
            {
                MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("DataSkadences", "DataSkadences null", System.Windows.MessageBoxButton.OK);
                return;
            }
            if (KaTvsh.IsChecked == null)
            {
                checkBox = false;
            }
            if (KaTvsh.IsChecked == true)
            {
                checkBox = true;
            }
            if (Lloj.SelectedItem == null)
            {
                MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Lloj", "Lloj null", System.Windows.MessageBoxButton.OK);
                return;

            }
            if (Tipi.SelectedItem == null)
            {
                MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Tipi", "Lloj null", System.Windows.MessageBoxButton.OK);
                return;

            }
            if (Cmimi.Text == null || Cmimi.Text == "")
            {
                MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Cmimi", "Cmimi null", System.Windows.MessageBoxButton.OK);
                return;

            }
            Produkt produkt = new Produkt()
            {
                id = this.id,
                Emertimi = Emertimi.Text,
                Njesia = Njesia.Text,
                DataSkadences = DataSkadences.SelectedDate.Value,
                Cmimi = double.Parse(Cmimi.Text),
                Lloj = (ArtikullServices.Models.Lloj)Lloj.SelectedItem,
                KaTvsh = checkBox,
                Tipi = (ETipiProdukt)Tipi.SelectedItem,
                BarkodKod = BarkodKod.Text,
            };
            _produktService.EditProdukt(produkt, this.id);
            this.Close();
        }
    }
}
