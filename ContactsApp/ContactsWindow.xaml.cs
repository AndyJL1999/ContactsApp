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
            
            //Set the owner of the contacts window
            Owner = Application.Current.MainWindow;
            
            //Set the starting position for the contacts window to the center of it's owner
            WindowStartupLocation = WindowStartupLocation.CenterOwner;
        }

        private async void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            //Save and fill in Contact
            Contact contact = new Contact
            {
                Name = nameTextBox.Text,
                Email = emailTextBox.Text,
                Phone = phoneNumberTextBox.Text
            };

            //Make the Api call to post a new contact using ApiHelper
            using (HttpResponseMessage response = await ApiHelper.ApiClient.PostAsJsonAsync((ApiHelper.ApiClient.BaseAddress + "api/Contacts"), contact))
            {
                if (response.IsSuccessStatusCode)
                {
                    successText.Text = response.Content.ReadAsStringAsync().Result;
                }
            }
            Close();
        }

        //Close Window
        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
