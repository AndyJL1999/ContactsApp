using ContactsAPI.DTOs;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ContactsApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        IEnumerable<Contact> contacts;

        public MainWindow()
        {
            InitializeComponent();
            contacts = new List<Contact>();
            ApiHelper.InitializeClient();
            LoadAllContacts();
        }

        private void AddContactButton_Click(object sender, RoutedEventArgs e)
        {
            ContactsWindow newContactsWindow = new ContactsWindow();
            newContactsWindow.ShowDialog();

            LoadAllContacts();
        }

        private async void LoadAllContacts()
        {
            using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync(ApiHelper.ApiClient.BaseAddress + "api/Contacts/GetAll"))
            {
                if (response.IsSuccessStatusCode)
                {
                    contacts = await response.Content.ReadFromJsonAsync<IEnumerable<Contact>>();
                    contacts = contacts.OrderBy(c => c.Name);
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }

            if(contacts != null)
            {
                contactsListView.ItemsSource = contacts;
            }
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox searchBox = sender as TextBox;

            var filteredList = contacts.Where(c => c.Name.ToLower().Contains(searchBox.Text.ToLower())).ToList();

            contactsListView.ItemsSource = filteredList;
        }

        private void contactsListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Contact selectedContact = (Contact)contactsListView.SelectedItem;

            if(selectedContact != null)
            {
                ContactDetailsWindow detailsWindow = new ContactDetailsWindow(selectedContact);
                detailsWindow.ShowDialog();

                LoadAllContacts();
            }
        }
    }
}
