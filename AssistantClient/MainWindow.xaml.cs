using Newtonsoft.Json;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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

namespace AssistantClient
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(SocialNumberBox.Text) || string.IsNullOrEmpty(NameBox.Text) || string.IsNullOrEmpty(AddressBox.Text)
                || string.IsNullOrEmpty(ComplaintBox.Text))
            {
                MessageBox.Show("Minden mezőt ki kell tölteni!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            
            if (!NameBox.Text.Contains(" "))
            {
                MessageBox.Show("Érvénytelen név!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            using (var client = new HttpClient())
            {
                var person = new Person(SocialNumberBox.Text, NameBox.Text, AddressBox.Text, ComplaintBox.Text);

                var json = JsonConvert.SerializeObject(person);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var result = client.PostAsync("http://localhost:59398/api/surgery", content).Result;
                if (!result.IsSuccessStatusCode)
                {
                    MessageBox.Show("A beteg felvétel nem sikerült! A hiba részletei: " + result.ReasonPhrase, "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }

            MessageBox.Show("Sikeres betegfelvétel!", "Betegfelvétel", MessageBoxButton.OK, MessageBoxImage.Information);
            SocialNumberBox.Text = "";
            NameBox.Text = "";
            AddressBox.Text = "";
            ComplaintBox.Text = "";
        }
    }
}
