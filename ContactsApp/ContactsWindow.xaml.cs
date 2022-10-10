using ContactsAPI.Helpers;
using ContactsAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
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

namespace ContactsApp
{
    /// <summary>
    /// Interaction logic for ContactsWindow.xaml
    /// </summary>
    public partial class ContactsWindow : Window
    {
        public ContactsWindow()
        {
            InitializeComponent();

            Owner = Application.Current.MainWindow;
            WindowStartupLocation = WindowStartupLocation.CenterOwner;
        }

        private async void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            //Save Contact
            Contact contact = new Contact
            {
                Name = nameTextBox.Text,
                Email = emailTextBox.Text,
                Phone = phoneNumberTextBox.Text
            };

            using (HttpResponseMessage response = await ApiHelper.ApiClient.PostAsJsonAsync((ApiHelper.ApiClient.BaseAddress + "api/Contacts"), contact))
            {
                if (response.IsSuccessStatusCode)
                {
                    successText.Text = response.Content.ReadAsStringAsync().Result;
                }
            }
            Close();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
