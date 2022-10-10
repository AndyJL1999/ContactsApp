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
    /// Interaction logic for ContactDetailsWindow.xaml
    /// </summary>
    public partial class ContactDetailsWindow : Window
    {
        private readonly Contact _contact;

        public ContactDetailsWindow(Contact contact)
        {
            InitializeComponent();
            _contact = contact;
            nameTextBox.Text = _contact.Name;
            emailTextBox.Text = _contact.Email;
            phoneNumberTextBox.Text = _contact.Phone;

            Owner = Application.Current.MainWindow;
            WindowStartupLocation = WindowStartupLocation.CenterOwner;
        }

        private async void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            _contact.Name = nameTextBox.Text;
            _contact.Email = emailTextBox.Text;
            _contact.Phone = phoneNumberTextBox.Text;

            using HttpResponseMessage response = await ApiHelper.ApiClient.PutAsJsonAsync(ApiHelper.ApiClient.BaseAddress + "api/Contacts/Update", _contact);

            Close();
        }

        private async void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            using (HttpResponseMessage response = await ApiHelper.ApiClient.DeleteAsync(ApiHelper.ApiClient.BaseAddress + $"api/Contacts/Delete/{_contact.Id}"))
            {
                if (response.IsSuccessStatusCode)
                    Close();
                else
                    throw new Exception(response.ReasonPhrase);
            }
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
