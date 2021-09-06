using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CardListDisplay
{
    // development branch
    public partial class MainPage : ContentPage
    {
        // declare collections for account information
        private List<string> accounts;
        private List<string> showAccounts;
        private List<Account> fullAccounts;
        public MainPage()
        {
            InitializeComponent();
            // initialize collections with default values
            accounts = new List<string>() { "Apple", "Banana", "Cherry", "Donut", "Eclair", "Fruit" }; // initialize the list
            showAccounts = new List<string>() { "Bunnies", "Kittens", "Puppies", "Cubs", "Babies", "Random" };
            fullAccounts = new List<Account>();
            // Retrieve info from account text file
            LoadAccountDetails(SearchEntry.Text);
            LoadCards(); // call method to load cards
        }

        /// <summary>
        /// Retrieves the account information from the provided text file
        /// </summary>
        private void LoadAccountDetails(string searchItem)
        {
            // reset the list
            fullAccounts.Clear();

            // string for keys
            string line;

            // to view the embedded resource
            Assembly assembly = IntrospectionExtensions.GetTypeInfo(typeof(App)).Assembly;
            Stream stream = assembly.GetManifestResourceStream("CardListDisplay.pwList.txt");

            using (StreamReader reader = new StreamReader(stream))
            {
                while (!reader.EndOfStream)
                {
                    line = reader.ReadLine();
                    string[] splitLine = line.Split(',');
                    Account account = new Account() { AccountName = splitLine[0], Username = splitLine[1], Password = splitLine[2] };

                    if (searchItem == null || account.AccountName.Contains(searchItem))
                    {
                        // (searchItem != null || searchItem != "") && 
                        // need this for the listview
                        fullAccounts.Add(account);
                    }
                }
            }
        }

        /// <summary>
        /// Populates the UI with cards containing account information
        /// </summary>
        private void LoadCards()
        {
            // reset the layout
            MainLayout.Children.Clear();

            // adding cards to the list!
            foreach (var item in fullAccounts)
            {
                // Define overall layout and UI elements
                StackLayout innerLayout = new StackLayout(); // new stack layout, this will hold the text content
                // labels with styles and text
                Label accountLabel = new Label { Text = item.AccountName, Style = (Style)Application.Current.Resources["accountLabelStyle"] };
                Label showLabel = new Label { Text = "Tap to View" };
                Label username = new Label { Text = $"Username:\t\t{item.Username}" };
                Label password = new Label { Text = $"Password:\t\t\t{item.Password}" };

                // Logic for tap gestures when frame/card is pressed
                bool toggle = true;
                TapGestureRecognizer tapGestureRecognizer = new TapGestureRecognizer(); // new tap gesture recognizer
                                                                                        // add a tap event
                tapGestureRecognizer.Tapped += (object sender, EventArgs e) =>
                {
                    Frame tFrame = (Frame)sender; // grab the object details
                    if (toggle)
                    {
                        tFrame.SetDynamicResource(StyleProperty, "expandAccountCardStyle");
                        showLabel.IsVisible = false;
                        //showLabel.Text = "Tap to Hide";
                        //tFrame.Resources["accountCardStyle"] = Application.Current.Resources["expandAccountCardStyle"]; // change the resources style
                        toggle = false;
                    }
                    else
                    {
                        tFrame.SetDynamicResource(StyleProperty, "accountCardStyle");
                        showLabel.IsVisible = true;
                        //showLabel.Text = "Tap to View";
                        //tFrame.Resources["expandAccountCardStyle"] = Application.Current.Resources["accountCardStyle"]; // change the resources style
                        toggle = true;
                    }
                    //Resources["accountCardStyle"] = Application.Current.Resources["expandAccountCardStyle"];
                };

                // new frame, this is the main background of the template where all the elements will be placed
                Frame frame = new Frame();
                frame.SetDynamicResource(StyleProperty, "accountCardStyle"); // setting the style of the frame, needs to be dynamic
                frame.GestureRecognizers.Add(tapGestureRecognizer); // add a tap event for the frame

                // adding everything to the frame and layouts
                innerLayout.Children.Add(accountLabel);
                innerLayout.Children.Add(showLabel);
                innerLayout.Children.Add(username);
                innerLayout.Children.Add(password);
                frame.Content = innerLayout;
                MainLayout.Children.Add(frame);
            }
        }

        private void SearchEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            LoadAccountDetails(SearchEntry.Text);
            LoadCards();
        }
    }
}
