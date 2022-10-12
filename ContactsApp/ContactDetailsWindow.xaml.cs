using ContactsAPI.Helpers;
using ContactsAPI.Models;
using Microsoft.Win32;
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
            
            //Set the information for all of the text boxes and setting the contact to the selected contact
            _contact = contact;
            nameTextBox.Text = _contact.Name;
            emailTextBox.Text = _contact.Email;
            phoneNumberTextBox.Text = _contact.Phone;
            imgUrlTextBlock.Text = _contact.ImageUrl;

            //Set the owner for the contact details window
            Owner = Application.Current.MainWindow;
            
            //Set the location for the contact details window to the center of it's owner
            WindowStartupLocation = WindowStartupLocation.CenterOwner;
        }

        private async void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            //Set the new information into the Contact object
            _contact.Name = nameTextBox.Text;
            _contact.Email = emailTextBox.Text;
            _contact.Phone = phoneNumberTextBox.Text;
            _contact.ImageUrl = imgUrlTextBlock.Text;

            //Make a Api call to update the contact using ApiHelper
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

        private void ChangeImageButton_Click(object sender, RoutedEventArgs e)
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
                imgUrlTextBlock.Text = fileName;
            }
        }
    }
}
