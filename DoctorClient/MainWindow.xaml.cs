using Newtonsoft.Json;
using Shared;
using System.Collections.Generic;
using System.Net.Http;
using System.Windows;
using System.Windows.Controls;

namespace DoctorClient
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            GetPeople();
        }

        private void GetPeople()
        {
            using (var client = new HttpClient())
            {
                var result = client.GetAsync("http://localhost:59398/api/surgery").Result;
                if (result.IsSuccessStatusCode)
                {
                    var content = result.Content.ReadAsStringAsync().Result;
                    var people = JsonConvert.DeserializeObject<IEnumerable<Person>>(content);
                    PeopleBox.ItemsSource = people;
                }
                else
                {
                    MessageBox.Show("Nem sikerült lekérdezni a betegeket! A hiba részletei: " + result.ReasonPhrase, "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void PeopleBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var person = PeopleBox.SelectedItem as Person;
            NameLabel.Content = person?.Name;
            SocialNumberLabel.Content = person?.SocialSecurityNumber;
            AddressLabel.Content = person?.Address;
            ComplaintLabel.Content = person?.Complaint;
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            GetPeople();
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Biztosan törölni akarja a kiválasztott beteget?", "Beteg törlése", MessageBoxButton.YesNo, MessageBoxImage.Question,
                MessageBoxResult.No) == MessageBoxResult.No)
            {
                return;
            }

            using (var client = new HttpClient())
            {
                var person = PeopleBox.SelectedItem as Person;
                var result = client.DeleteAsync("http://localhost:59398/api/surgery/" + person.SocialSecurityNumber).Result;
                if (!result.IsSuccessStatusCode)
                {
                    MessageBox.Show("A beteg törlése nem sikerült! A hiba részletei: " + result.ReasonPhrase, "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }
            GetPeople();
        }
    }
}
