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
        private List<string> accounts;
        public MainPage()
        {
            InitializeComponent();
            accounts = new List<string>() { "Apple", "Banana", "Cherry", "Donut", "Eclair", "Fruit"};
            LoadCards();
        }

        private void LoadCards()
        {
            TapGestureRecognizer tapGestureRecognizer = new TapGestureRecognizer();
            tapGestureRecognizer.Tapped += (object sender, EventArgs e) =>
            {
                Frame tFrame = (Frame)sender;
                tFrame.Resources["frameStyle"] = Application.Current.Resources["newFrameStyle"];
                //Resources["frameStyle"] = Application.Current.Resources["newFrameStyle"];
            };

            // adding cards to the list!
            for (int i = 0; i < accounts.Count; i++)
            {
                Frame frame = new Frame();
                frame.SetDynamicResource(StyleProperty, "frameStyle");
                frame.GestureRecognizers.Add(tapGestureRecognizer);

                StackLayout innerLayout = new StackLayout();
                Label accountLabel = new Label { Text = accounts[i], Style = (Style)Application.Current.Resources["labelStyle"]};
                Label showLabel = new Label { Text = "Tap to View" };

                innerLayout.Children.Add(accountLabel);
                innerLayout.Children.Add(showLabel);

                frame.Content = innerLayout;

                MainLayout.Children.Add(frame);
            }
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            DisplayAlert("Yes", "Uh", "Close");
        }
    }
}
