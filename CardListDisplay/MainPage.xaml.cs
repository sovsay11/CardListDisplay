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
    // custom listview branch, based on development branch
    public partial class MainPage : ContentPage
    {
        // declare collections for account information
        private List<Account> fullAccounts;
        public MainPage()
        {
            InitializeComponent();
            // initialize collections with default values
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
            LstViewAccounts.ItemsSource = fullAccounts;
        }

        private void SearchEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            LoadAccountDetails(SearchEntry.Text);
            LoadCards();
        }
    }
}
