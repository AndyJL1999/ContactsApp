using ContactsAPI.Helpers;
using ContactsAPI.Models;
using ContactsApp.Resources;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace ContactsApp.MVVM.ViewModel
{
    public class DetailsViewModel : ObservableObject
    {
        private readonly Contact _contact;
        private string _nameText;
        private string _emailText;
        private string _phoneText;
        private string _imgUrlText;
        private BitmapImage _contactImage;
        private ICommand _backCommand;
        private ICommand _updateCommand;
        private ICommand _deleteCommand;
        private ICommand _changeImageCommand;

        public DetailsViewModel(Contact contact)
        {
            //Set the information for all of the text boxes and setting the contact to the selected contact
            _contact = contact;
            _nameText = _contact.Name;
            _emailText = _contact.Email;
            _phoneText = _contact.Phone;
            _imgUrlText = _contact.ImageUrl;
            _contactImage = new BitmapImage(new Uri(ImgUrlText));
        }

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
        public string ImgUrlText
        {
            get { return _imgUrlText; }
            set
            {
                _imgUrlText = value;
                OnPropertyChanged(nameof(ImgUrlText));
            }
        }
        public BitmapImage ContactImage 
        {
            get { return _contactImage; }
            set
            {
                _contactImage = value;
                OnPropertyChanged(nameof(ContactImage));
            }
        }
        public ICommand BackCommand 
        { 
            get 
            { 
                if(_backCommand is null)
                {
                    _backCommand = new RelayCommand(p => Exit((ObservableObject)p), p => p is ObservableObject);
                }

                return _backCommand; 
            }
        }
        public ICommand UpdateCommand
        {
            get
            {
                if (_updateCommand is null)
                {
                    _updateCommand = new RelayCommand(p => UpdateContact(), p => true);
                }

                return _updateCommand;
            }
        }
        public ICommand DeleteCommand
        {
            get
            {
                if (_deleteCommand is null)
                {
                    _deleteCommand = new RelayCommand(p => DeleteContact((ObservableObject)p), p => p is ObservableObject);
                }

                return _deleteCommand;
            }
        }
        public ICommand ChangeImageCommand
        {
            get
            {
                if (_changeImageCommand is null)
                {
                    _changeImageCommand = new RelayCommand(p => ChangeImage(), p => true);
                }

                return _changeImageCommand;
            }
        }

        private void Exit(ObservableObject p)
        {
            p = new MainViewModel();
            App.Current.MainWindow.DataContext = p;
        }

        private async void UpdateContact()
        {
            //Set the new information into the Contact object
            _contact.Name = NameText;
            _contact.Email = EmailText;
            _contact.Phone = PhoneText;
            _contact.ImageUrl = ImgUrlText;

            ContactImage = new BitmapImage(new Uri(ImgUrlText));

            //Make a Api call to update the contact using ApiHelper
            using HttpResponseMessage response = await ApiHelper.ApiClient.PutAsJsonAsync(ApiHelper.ApiClient.BaseAddress + "api/Contacts/Update", _contact);
        }

        private async void DeleteContact(ObservableObject p)
        {
            using (HttpResponseMessage response = await ApiHelper.ApiClient.DeleteAsync(ApiHelper.ApiClient.BaseAddress + $"api/Contacts/Delete/{_contact.Id}"))
            {
                if (response.IsSuccessStatusCode)
                {
                    Exit(p);
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

        private void ChangeImage()
        {
            //Opens file explorer
            OpenFileDialog dialog = new OpenFileDialog();

            //Setting filter for the files that will be shown
            dialog.Filter = "Image files (*.png; *.jpg)|*.png;*.jpg;*.jpeg";
            dialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);

            //If file explorer opens successfully set the chosen file as the imgUrl text
            if (dialog.ShowDialog() == true)
            {
                string fileName = dialog.FileName;
                ImgUrlText = fileName;
            }
        }
    }
}
