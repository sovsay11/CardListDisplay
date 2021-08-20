﻿using System;
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
        private List<string> showAccounts;
        public MainPage()
        {
            InitializeComponent();
            accounts = new List<string>() { "Apple", "Banana", "Cherry", "Donut", "Eclair", "Fruit" }; // initialize the list
            showAccounts = new List<string>() { "Bunnies", "Kittens", "Puppies", "Cubs", "Babies", "Random" };
            LoadCards(); // call method to load cards
        }

        private void LoadCards()
        {
            // adding cards to the list!
            for (int i = 0; i < accounts.Count; i++)
            {

                StackLayout innerLayout = new StackLayout(); // new stack layout, this will hold the text content

                // labels with styles and text
                Label accountLabel = new Label { Text = accounts[i], Style = (Style)Application.Current.Resources["labelStyle"] };
                Label showLabel = new Label { Text = "Tap to View" };
                Label username = new Label { Text = showAccounts[i] };
                Label password = new Label { Text = showAccounts[i] };

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
