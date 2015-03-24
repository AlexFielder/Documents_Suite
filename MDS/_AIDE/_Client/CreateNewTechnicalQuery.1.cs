using System;
using System.Linq;
using System.IO;
using System.IO.IsolatedStorage;
using System.Collections.Generic;
using Microsoft.LightSwitch;
using Microsoft.LightSwitch.Framework.Client;
using Microsoft.LightSwitch.Presentation;
using Microsoft.LightSwitch.Presentation.Extensions;
namespace LightSwitchApplication
{
    public partial class CreateNewTechnicalQuery
    {
        partial void CreateNewTechnicalQuery_InitializeDataWorkspace(List<IDataService> saveChangesTo)
        {
            this.TQDetailProperty = new TQDetail();
            IsProjectTQ = true;
            if (this.TQDetailProperty.Project != null)
            {
                this.SelectedCustomer = this.TQDetailProperty.Project.Customer;
            }

        }

        partial void CreateNewTechnicalQuery_Activated()
        {
            IContentItemProxy Custproxy = this.FindControl("Customer1");
            Custproxy.ControlAvailable += new EventHandler<ControlAvailableEventArgs>((s1, e1) =>
            {
                MatrixControls.NPCustomerInternal custTb = (MatrixControls.NPCustomerInternal)e1.Control;
                custTb.SelectedItemPath = "SortedCustomers.Customer.CustomerName";
                custTb.ItemsSource = this.SortedCustomers;
                custTb.LostFocus += custTb_LostFocus;
            });
            IContentItemProxy Salesproxy = this.FindControl("MatrixSalesOrderNo");
            Salesproxy.ControlAvailable += new EventHandler<ControlAvailableEventArgs>((s2, e2) =>
            {
                MatrixControls.NPSalesOrderNumberInternal sonTb = (MatrixControls.NPSalesOrderNumberInternal)e2.Control;
                sonTb.SelectedItemPath = "SortedSalesOrderNumbers.SalesOrderNo";
                sonTb.ItemsSource = this.SortedSalesOrderNumbers;
                sonTb.LostFocus += sonTb_LostFocus;
            });
            IContentItemProxy CustOrderNumberproxy = this.FindControl("CustomerOrderNo");
            CustOrderNumberproxy.ControlAvailable += new EventHandler<ControlAvailableEventArgs>((s2, e2) =>
            {
                MatrixControls.NPCustomerOrderNumberInternal custONTb = (MatrixControls.NPCustomerOrderNumberInternal)e2.Control;
                custONTb.SelectedItemPath = "SortedCustomerOrderNumbers.CustomerOrderNumber";
                custONTb.ItemsSource = this.SortedCustomerOrderNumbers;
                custONTb.LostFocus += custONTb_LostFocus;
            });
        }
        private void custONTb_LostFocus(object sender, System.Windows.RoutedEventArgs e)
        {
            MatrixControls.NPCustomerOrderNumberInternal tb = sender as MatrixControls.NPCustomerOrderNumberInternal;
            ManualCustomerOrderNo = tb.NPCustomerOrderNumber;
            this.TQDetailProperty.CustomerOrderNo = tb.GetValue(MatrixControls.NPCustomerOrderNumberInternal.NPCustomerOrderNumberProperty).ToString();
        }

        private void sonTb_LostFocus(object sender, System.Windows.RoutedEventArgs e)
        {
            MatrixControls.NPSalesOrderNumberInternal tb = sender as MatrixControls.NPSalesOrderNumberInternal;
            ManualSalesOrderNo = tb.NPSalesOrderNumber;
            this.TQDetailProperty.MatrixSalesOrderNo = tb.GetValue(MatrixControls.NPSalesOrderNumberInternal.NPSalesOrderProperty).ToString();
        }

        private void custTb_LostFocus(object sender, System.Windows.RoutedEventArgs e)
        {
            MatrixControls.NPCustomerInternal tb = sender as MatrixControls.NPCustomerInternal;

            ManualCustomerName = tb.NPCustomer;
            this.TQDetailProperty.Customer = tb.GetValue(MatrixControls.NPCustomerInternal.NPCustomerProperty).ToString();
        }

        partial void CreateNewTechnicalQuery_Saving(ref bool handled)
        {
            //here we need to capture what little information we may have from the screen when this is a non-project TQ.
            if (!IsProjectTQ)
            {
                this.TQDetailProperty.MatrixSalesOrderNo = ManualSalesOrderNo;
                this.TQDetailProperty.Customer = ManualCustomerName;
                this.TQDetailProperty.CustomerOrderNo = ManualCustomerOrderNo;
                IEnumerable<MatrixSalesOrderNumber> foundSON = (from MatrixSalesOrderNumber a in this.DataWorkspace.MDSData.MatrixSalesOrderNumbers
                                                                where a.SalesOrderNo == TQDetailProperty.MatrixSalesOrderNo
                                                                select a).AsEnumerable();
                if (foundSON.Count<MatrixSalesOrderNumber>() == 0)
                {
                    MatrixSalesOrderNumber mson = this.DataWorkspace.MDSData.MatrixSalesOrderNumbers.AddNew();
                    mson.Customer = TQDetailProperty.Customer;
                    mson.SalesOrderNo = TQDetailProperty.MatrixSalesOrderNo;
                    this.DataWorkspace.MDSData.SaveChanges();
                }

                //add data to the customer table
                IEnumerable<Customer> foundCust = (from Customer c in this.DataWorkspace.MDSData.Customers
                                                   where c.CustomerName == TQDetailProperty.Customer
                                                   select c).AsEnumerable();
                if (foundCust.Count<Customer>() == 0)
                {
                    Customer cust = this.DataWorkspace.MDSData.Customers.AddNew();
                    cust.CustomerName = TQDetailProperty.Customer;
                    this.DataWorkspace.MDSData.SaveChanges();
                }
            }
            else
            {
                this.TQDetailProperty.Customer = SelectedCustomer.CustomerName;
            }
        }

        partial void CreateNewTechnicalQuery_Saved()
        {
            this.Close(false);
            Application.Current.ShowDefaultScreen(this.TQDetailProperty);
            CreateNewDocument nd = new CreateNewDocument();
            nd.GenerateFromAddNew("TQ", this.TQDetailProperty, IsProjectTQ);
        }

        partial void ManualSalesOrderNo_Changed()
        {
            var TQ = (from TQDetail a in this.DataWorkspace.MDSData.TQDetails
                      where a.MatrixSalesOrderNo == ManualSalesOrderNo
                      orderby a.TQNumber
                      select a).AsEnumerable();
            string TQsId = Convert.ToString(TQ.Count() + 1);
            char pad = '0';
            string str = TQsId.PadLeft(5, pad);
            this.TQDetailProperty.TQNumber = ManualSalesOrderNo + "-TQ" + str;
            this.TQDetailProperty.QueryDate = DateTime.Today.ToString("ddd d MMM yyyy");
            this.TQDetailProperty.Customer = ManualCustomerName;
        }

        partial void ManualCustomerName_Changed()
        {
            this.TQDetailProperty.Customer = ManualCustomerName;
        }

        partial void ManualCustomerOrderNo_Changed()
        {

        }

        partial void SelectedCustomer_Changed()
        {
            String tmpcustomerName = SelectedCustomer.CustomerName;
            String customerName = tmpcustomerName.ToUpper();
            if (SelectedCustomer.CustomerName.Contains("CAVENDISH"))
            {
                this.FindControl("TQReturnDate").IsVisible = true;
            }
            else
            {
                this.FindControl("TQReturnDate").IsVisible = false;
            }
        }

        partial void IsProjectTQ_Changed()
        {
            this.FindControl("Project").IsVisible = IsProjectTQ;
            this.FindControl("SelectedCustomer").IsVisible = IsProjectTQ;
            this.FindControl("TQCustomerOrderNo").IsVisible = IsProjectTQ;
            this.FindControl("CustomerProjectCode").IsVisible = IsProjectTQ;
            this.FindControl("TQMatrixSalesOrderNo").IsVisible = IsProjectTQ;
            this.FindControl("MatrixProjectCode").IsVisible = IsProjectTQ;
            if (IsProjectTQ)
            {
                this.FindControl("MatrixSalesOrderNo").IsVisible = false;
                this.FindControl("Customer1").IsVisible = false;
                this.FindControl("CustomerOrderNo").IsVisible = false;
            }
            else
            {
                //need to empty the values that may have been picked by the user before switching 
                // to non-project TQ!
                this.FindControl("MatrixSalesOrderNo").IsVisible = true;
                this.FindControl("Customer1").IsVisible = true;
                this.FindControl("CustomerOrderNo").IsVisible = true;
            }
            this.TQDetailProperty.UserName = this.Application.User.FullName;
        }

    }
}
