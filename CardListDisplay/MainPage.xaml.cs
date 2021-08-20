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
    public partial class MainPage : ContentPage
    {
        private List<string> accounts; // declare new list for account names
        private List<string> showAccounts;
        private List<Account> fullAccounts;
        public MainPage()
        {
            InitializeComponent();
            accounts = new List<string>() { "Apple", "Banana", "Cherry", "Donut", "Eclair", "Fruit" }; // initialize the list
            showAccounts = new List<string>() { "Bunnies", "Kittens", "Puppies", "Cubs", "Babies", "Random" };
            fullAccounts = new List<Account>();
            LoadAccountDetails();
            LoadCards(); // call method to load cards
        }

        private void LoadAccountDetails()
        {
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

                    // need this for the listview
                    fullAccounts.Add(account);
                }
            }
        }

        private void LoadCards()
        {
            // adding cards to the list!
            foreach (var item in fullAccounts)
            {

                StackLayout innerLayout = new StackLayout(); // new stack layout, this will hold the text content

                // labels with styles and text
                Label accountLabel = new Label { Text = item.AccountName, Style = (Style)Application.Current.Resources["labelStyle"] };
                Label showLabel = new Label { Text = "Tap to View" };
                Label username = new Label { Text = $"Username:\t\t{item.Username}" };
                Label password = new Label { Text = $"Password:\t\t\t{item.Password}" };

                bool toggle = true;
                TapGestureRecognizer tapGestureRecognizer = new TapGestureRecognizer(); // new tap gesture recognizer
                                                                                        // add a tap event
                tapGestureRecognizer.Tapped += (object sender, EventArgs e) =>
                {
                    Frame tFrame = (Frame)sender; // grab the object details
                    if (toggle)
                    {
                        tFrame.SetDynamicResource(StyleProperty, "newFrameStyle");
                        showLabel.IsVisible = false;
                        //showLabel.Text = "Tap to Hide";
                        //tFrame.Resources["frameStyle"] = Application.Current.Resources["newFrameStyle"]; // change the resources style
                        toggle = false;
                    }
                    else
                    {
                        tFrame.SetDynamicResource(StyleProperty, "frameStyle");
                        showLabel.IsVisible = true;
                        //showLabel.Text = "Tap to View";
                        //tFrame.Resources["newFrameStyle"] = Application.Current.Resources["frameStyle"]; // change the resources style
                        toggle = true;
                    }
                    //Resources["frameStyle"] = Application.Current.Resources["newFrameStyle"];
                };

                // new frame, this is the main background of the template
                Frame frame = new Frame();
                frame.SetDynamicResource(StyleProperty, "frameStyle"); // setting the style of the frame, needs to be dynamic
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
    }
}
