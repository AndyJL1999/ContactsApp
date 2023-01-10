using ContactsAPI.Helpers;
using ContactsAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using System.Net.Http.Json;
using System.ComponentModel;
using ContactsApp.Resources;
using System.Windows.Input;
using System.IO;
using System.Reflection;

namespace ContactsApp.MVVM.ViewModel
{
    public class ContactsViewModel : ObservableObject
    {
        private string _nameText;
        private string _emailText;
        private string _phoneText;
        private string _successText;
        private ICommand _saveCommand;
        private ICommand _cancelCommand;


        public string NameText 
        { 
            get { return _nameText; } 
            set 
            { 
                _nameText = value;
                OnPropertyChanged(nameof(NameText));
            }
        }
        public string EmailText 
        {
            get { return _emailText; }
            set
            {
                _emailText = value;
                OnPropertyChanged(nameof(EmailText));
            }
        }
        public string PhoneText
        {
            get { return _phoneText; }
            set
            {
                _phoneText = value;
                OnPropertyChanged(nameof(PhoneText));
            }
        }
        public string SuccessText
        {
            get { return _successText; }
            set
            {
                _successText = value;
                OnPropertyChanged(nameof(SuccessText));
            }
        }

        public ICommand SaveCommand 
        {
            get
            {
                if(_saveCommand is null)
                {
                    _saveCommand = new RelayCommand(p => SaveContact((ObservableObject)p), p => CheckContact() && p is ObservableObject);
                }

                return _saveCommand;
            }
        }

        public ICommand CancelCommand
        {
            get
            {
                if (_cancelCommand is null)
                {
                    _cancelCommand = new RelayCommand(p => Exit((ObservableObject)p), p => p is ObservableObject);
                }

                return _cancelCommand;
            }
        }

        private async void SaveContact(ObservableObject p)
        {
            //Save and fill in Contact
            //TODO - fix img url path
            Contact contact = new Contact
            {
                Name = NameText,
                Email = EmailText,
                Phone = PhoneText,
                ImageUrl = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) + @"\Assets\user.png" //Use your own default image
            };

            //Make the Api call to post a new contact using ApiHelper
            using (HttpResponseMessage response = await ApiHelper.ApiClient.PostAsJsonAsync((ApiHelper.ApiClient.BaseAddress + "api/Contacts"), contact))
            {
                if (response.IsSuccessStatusCode)
                {
                    SuccessText = response.Content.ReadAsStringAsync().Result;
                }
            }

            Exit(p);
        }

        private void Exit(ObservableObject p)
        {
            p = new MainViewModel();
            App.Current.MainWindow.DataContext = p;
        }

        private bool CheckContact()
        {
            if(string.IsNullOrEmpty(NameText) || string.IsNullOrEmpty(EmailText) || string.IsNullOrEmpty(PhoneText))
            {
                return false;
            }

            return true;
        }

    }
}
