using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using AutoLotModel;
using System.Data.Entity;

namespace Ghidora_Artur_Lab6
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    enum ActionState
    {
        New,
        Edit,
        Delete,
        Nothing
    }
    public partial class MainWindow : Window
    {
        ActionState action = ActionState.Nothing;
        AutoLotEntitiesModel ctx = new AutoLotEntitiesModel();
        CollectionViewSource customerViewSource;

        CollectionViewSource customerOrdersViewSource;

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //using System.Data.Entity;
            customerViewSource =
           ((System.Windows.Data.CollectionViewSource)(this.FindResource("customerViewSource")));
            customerViewSource.Source = ctx.Customers.Local;

            customerOrdersViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("customerOrdersViewSource")));

            customerOrdersViewSource.Source = ctx.Orders.Local;

            ctx.Customers.Load();

            ctx.Orders.Load();
            ctx.Inventories.Load();
            cmbCustomers.ItemsSource = ctx.Customers.Local;
            cmbCustomers.DisplayMemberPath = "FirstName";
            cmbCustomers.SelectedValuePath = "CustId";
            cmbInventory.ItemsSource = ctx.Inventories.Local;
            cmbInventory.DisplayMemberPath = "Make";
            cmbInventory.SelectedValuePath = "CarId";

            System.Windows.Data.CollectionViewSource inventoryViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("inventoryViewSource")));
            // Load data by setting the CollectionViewSource.Source property:
            // inventoryViewSource.Source = [generic data source]
        }

        private void btnNew_Click(object sender, RoutedEventArgs e)
        {

        }





        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            Customer customer = null;
            customer = new Customer()
            {
                FirstName = firstNameTextBox.Text.Trim(),
                LastName = lastNameTextBox.Text.Trim()
            };
            //adaugam entitatea nou creata in context
            ctx.Customers.Add(customer);
            customerViewSource.View.Refresh();
            //salvam modificarile
            ctx.SaveChanges();
            customer = (Customer)customerDataGrid.SelectedItem;
            customer.FirstName = firstNameTextBox.Text.Trim();
            customer.LastName = lastNameTextBox.Text.Trim();
            customerViewSource.View.Refresh();
            // pozitionarea pe item-ul curent
            customerViewSource.View.MoveCurrentTo(customer);
            customer = (Customer)customerDataGrid.SelectedItem;
            ctx.Customers.Remove(customer);
            customerViewSource.View.Refresh();
        }
        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            customerViewSource.View.MoveCurrentToNext();
        }
        private void btnPrevious_Click(object sender, RoutedEventArgs e)
        {
            customerViewSource.View.MoveCurrentToPrevious();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            
        }

        private void TextBox_TextChanged_1(object sender, TextChangedEventArgs e)
        {

        }

        private void btnSave2_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
// am ajuns la punctul 31.
//n am facut functiile pentru inventory