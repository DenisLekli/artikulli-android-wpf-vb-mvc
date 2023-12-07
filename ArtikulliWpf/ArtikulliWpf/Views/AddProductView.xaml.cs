using ArtikullServices.Models;
using ArtikullServices.Services;
using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ArtikulliWpf.Views
{
    /// <summary>
    /// Interaction logic for AddProductView.xaml
    /// </summary>
    public partial class AddProductView : Window
    {
        private ProduktService _produktService;

        public AddProductView(ProduktService produktService)
        {
            InitializeComponent();
            this._produktService = produktService;
            Lloj.ItemsSource = Enum.GetValues(typeof(Lloj)).Cast<Lloj>();
            Tipi.ItemsSource = Enum.GetValues(typeof(ETipiProdukt)).Cast<ETipiProdukt>();
        }
        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            var textBox = sender as TextBox;
            e.Handled = Regex.IsMatch(e.Text, "[^0-9]+");
        }
        private void Add_Clicked(object sender, RoutedEventArgs e)
        {
            bool checkBox = false;
            if (DataSkadences.SelectedDate == null)
            {
                MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("DataSkadences", "DataSkadences null", System.Windows.MessageBoxButton.OK);
                return;
            }
            if (KaTvsh.IsChecked == null)
            {
                checkBox=false;
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
            if (Cmimi.Text == null||Cmimi.Text=="")
            {
                MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Cmimi", "Cmimi null", System.Windows.MessageBoxButton.OK);
                return;

            }


            //var ELoji = (ArtikullServices.Models.Lloj)Enum.Parse(typeof(ArtikullServices.Models.Lloj),(string) Lloj.SelectedItem);

            Produkt lawjers = new Produkt()
            {
                Emertimi = Emertimi.Text,
                Njesia = Njesia.Text,
                DataSkadences = DataSkadences.SelectedDate.Value,
                Cmimi = double.Parse(Cmimi.Text),
                Lloj = (ArtikullServices.Models.Lloj)Lloj.SelectedItem,
                KaTvsh = checkBox,
                Tipi = (ETipiProdukt)Tipi.SelectedItem,
                BarkodKod = BarkodKod.Text,
            };
            _produktService.addProdukt(lawjers);
            this.Close();

        }
    }
}
