using ContactsAPI.Helpers;
using ContactsAPI.Models;
using ContactsApp.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ContactsApp.MVVM.ViewModel
{
    public class MainViewModel : ObservableObject
    {
        private IEnumerable<Contact> _contacts;//holds all contacts but may change regularly
        private IEnumerable<Contact> _allContacts;//used to hold all contacts unchanged
        private Contact _selectedContact;
        private string _searchBoxText;
        private ICommand _addContactCommand;
        private object? _viewModel;

        public event EventHandler? SelectedContactChanged;
        public event EventHandler? SearchBoxTextChanged;

        public MainViewModel()
        {
            SelectedContactChanged += SelectionChanged;
            SearchBoxTextChanged += TextChanged;

            ViewModel = this;

            LoadAllContacts();
        }

        public object? ViewModel 
        { 
            get { return _viewModel; }
            set
            {
                _viewModel = value;
                OnPropertyChanged(nameof(ViewModel));
            } 
        }

        public string SearchBoxText 
        {
            get { return _searchBoxText; }
            set
            {
                _searchBoxText = value;
                OnPropertyChanged(nameof(SearchBoxText));
                SearchBoxTextChanged?.Invoke(this, new EventArgs());
            }
        }

        public Contact SelectedContact 
        { 
            get { return _selectedContact; }
            set
            {
                _selectedContact = value;
                OnPropertyChanged(nameof(SelectedContact));
                SelectedContactChanged?.Invoke(this, new EventArgs());
            }
        }

        public IEnumerable<Contact> Contacts 
        { 
            get { return _contacts; }
            set
            {
                _contacts = value;
                OnPropertyChanged(nameof(Contacts));
            }
        }

        public ICommand AddContactCommand 
        {
            get
            {
                if(_addContactCommand is null)
                {
                    _addContactCommand = new RelayCommand(p => AddContact(), p => true);
                }

                return _addContactCommand;
            }
        }

        //Opens a window for adding contacts
        private void AddContact()
        {
            ViewModel = new ContactsViewModel();
        }

        //Makes an api call using the ApiHelper to get all contacts and place them in a IEnumerable container
        private async void LoadAllContacts()
        {
            using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync(ApiHelper.ApiClient.BaseAddress + "api/Contacts/GetAll"))
            {
                if (response.IsSuccessStatusCode)
                {
                    Contacts = await response.Content.ReadFromJsonAsync<IEnumerable<Contact>>();
                    Contacts = Contacts.OrderBy(c => c.Name);
                    _allContacts = Contacts;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

        //Whenever the inside of the search box changes => filter the list view
        private void TextChanged(object? sender, EventArgs e)
        {
            var filteredList = _allContacts.Where(c => c.Name.ToLower().Contains(SearchBoxText.ToLower())).ToList();

            Contacts = filteredList;
        }

        //Open the details window when a contact is selected
        private void SelectionChanged(object? sender, EventArgs e)
        {
            if(SelectedContact != null)
            {
                ViewModel = new DetailsViewModel(SelectedContact);
            }
        }

    }
}
