# ContactsWPF (Updated)

<h1>Expanding a bit</h1>

<p><b>UPDATE:</b> Recently returned to this app and applied the MVVM pattern and removed all the additional windows that appeared when adding and updating contacts. The contacts, add contacts, and details view are now all displayed in the main window. The new views are now displayed below the old design.</p>

<p>This WPF app displays a list of contacts. You can add contacts, delete contacts, or modify the details of each contact. Currently the details of a contact are limited to name, email, phone number, and a contact image. Their is also a search bar so that users can find specific contacts more effectively.</p>

<img src="https://user-images.githubusercontent.com/88408654/195422505-48a69753-21b6-4e0b-8ea6-ba0dfd3f5c75.PNG"/>

<p>The app was originally rather plain and didn't include a contact image, but I decided to test myself a bit and added this feature. Even with the changes, the app is still not very presentable.</p>

<Label>Old:</Label>

<img src="https://user-images.githubusercontent.com/88408654/195425946-c79b46b8-c6d4-432a-b0cf-c4339fd1313a.PNG"/>

<Label>New:</Label>

<img src="https://user-images.githubusercontent.com/88408654/212171516-ed8b04cd-085d-4624-8d7a-56349cbe65fd.PNG"/>

<h1>Learning new things</h1>

<p>I am currently using HttpClient to make calls to my api and data binding to display the data coming in. The HttpClient is currently a global variable so that I can avoid repetition when making calls to the api. I also used a OpenFileDialog to open file explorer so that users can choose images from their computer to set contact images. All of these actions are made by button event handlers.</p>

<Label>Old:</Label>

<img src="https://user-images.githubusercontent.com/88408654/195424976-1079b373-adaa-4cae-ab86-ee9c89fa3136.PNG"/>

<Label>New:</Label>

<img src="https://user-images.githubusercontent.com/88408654/212171532-126caafa-2060-405a-9349-63a56af77551.PNG"/>

<h1>About the API</h1>

<p>I'm using a REST web API. It has a single controller for the contacts. I am using the repository pattern for data access alongside a MSSQLLocalDB server for my database. I took a code first approach when constructing my database. Using EntityFrameworkCore I was able to make migrations and update my database. I also created DTOs(Data Transfer Objects) to use in my API calls. AutoMapper was used to map my DTOs to my entities.</p>

<h1>And so...</h1>

<p>I will say that this project is not yet finished. There are still many improvements to be made and some quality of life behaviours that should be in the app now. While I would like to do this soon there is still much for me to learn and improve upon, so for now I will leave this project as it is and return to it at a later date.</p>
