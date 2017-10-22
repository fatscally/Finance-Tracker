using System.Windows;
using PFT.Data;
using PFT.Pages;

namespace PFT
{
    /// <summary>
    /// Interaction logic for HomeWindow.xaml
    /// </summary>
    public partial class HomeWindow : Window
    {
        public HomeWindow()
        {
        //    InitializeComponent();
        //    DataContext = App.ViewModel;
        //     _NavigationFrame.Navigate(new Transactions());

        }


        private void mnuTransactionNew_Click(object sender, RoutedEventArgs e)
        {
            _NavigationFrame.Navigate(new Transactions());
        }
        private void mnuTags_Click(object sender, RoutedEventArgs e)
        {
            _NavigationFrame.Navigate(new Tags());
        }
        private void mnuItems_Click(object sender, RoutedEventArgs e)
        {
            _NavigationFrame.Navigate(new ItemsPage());
        }
        private void mnuSuppliers_Click(object sender, RoutedEventArgs e)
        {
            _NavigationFrame.Navigate(new Suppliers());
        }
        private void mnuTagGroups_Click(object sender, RoutedEventArgs e)
        {
            _NavigationFrame.Navigate(new TagGroups());
        }
        private void mnuPaymentTypes_Click(object sender, RoutedEventArgs e)
        {
            _NavigationFrame.Navigate(new PaymentTypes());
        }

        private void mnuCreateTable_Click(object sender, RoutedEventArgs e)
        {
            SqlCeUtilities s = new SqlCeUtilities();

            //PFT.Data.SqlCeUtilities s = new SqlCeUtilities();
            //s.CreateTransactionsTable();
            //s.CreateTransactionAutoRepeatTable();
            //s.CreateItemsTable();
            //s.CreateTagsTable();
            //s.CreateSuppliersTable();
            //s.CreatePaymentTypesTable();
            //s.CreateTransaction_TagsTable();
            //s.CreateSettingsTable();
            //s.CreateItemDefaultTagsTable();
            //s.CreateItemDefaultPaymentTypeTable();
            //s.CreateRepeatTypesTable();
            //s.CreateDateRanges();
            //s.InsertDateRangesTypesRows();
            s = null;

        }


    }
}
