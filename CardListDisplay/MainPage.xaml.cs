using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CardListDisplay
{
    public partial class MainPage : ContentPage
    {
        private List<string> accounts; // declare new list for account names
        public MainPage()
        {
            InitializeComponent();
            accounts = new List<string>() { "Apple", "Banana", "Cherry", "Donut", "Eclair", "Fruit"}; // initialize the list
            LoadCards(); // call method to load cards
        }

        private void LoadCards()
        {
            TapGestureRecognizer tapGestureRecognizer = new TapGestureRecognizer(); // new tap gesture recognizer
            // add a tap event
            tapGestureRecognizer.Tapped += (object sender, EventArgs e) =>
            {
                Frame tFrame = (Frame)sender; // grab the object details
                tFrame.Resources["frameStyle"] = Application.Current.Resources["newFrameStyle"]; // change the resources style
                //Resources["frameStyle"] = Application.Current.Resources["newFrameStyle"];
            };

            // adding cards to the list!
            for (int i = 0; i < accounts.Count; i++)
            {
                // new frame, this is the main background of the template
                Frame frame = new Frame();
                frame.SetDynamicResource(StyleProperty, "frameStyle"); // setting the style of the frame, needs to be dynamic
                frame.GestureRecognizers.Add(tapGestureRecognizer); // add a tap event for the frame

                StackLayout innerLayout = new StackLayout(); // new stack layout, this will hold the text content

                // labels with styles and text
                Label accountLabel = new Label { Text = accounts[i], Style = (Style)Application.Current.Resources["labelStyle"]};
                Label showLabel = new Label { Text = "Tap to View" };

                // adding everything to the frame and layouts
                innerLayout.Children.Add(accountLabel);
                innerLayout.Children.Add(showLabel);
                frame.Content = innerLayout;
                MainLayout.Children.Add(frame);
            }
        }
    }
}
