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
    public partial class CreateNewRFI
    {
        partial void CreateNewRFI_InitializeDataWorkspace(global::System.Collections.Generic.List<global::Microsoft.LightSwitch.IDataService> saveChangesTo)
        {
            // Write your code here.
            this.RFIProperty = new RFI();
            IsProjectRFI = true;
        }

        partial void CreateNewRFI_Saved()
        {
            this.Close(false);
            Application.Current.ShowDefaultScreen(this.RFIProperty);
            CreateNewDocument nd = new CreateNewDocument();
            nd.GenerateFromAddNew("RFI", this.RFIProperty, IsProjectRFI);
        }

        partial void CreateNewRFI_Activated()
        {

            IContentItemProxy Custproxy = this.FindControl("Customer");
            Custproxy.ControlAvailable += new EventHandler<ControlAvailableEventArgs>((s1, e1) =>
            {
                MatrixControls.NPCustomerInternal custTb =
                    (MatrixControls.NPCustomerInternal)e1.Control;
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

            IContentItemProxy Recipientproxy = this.FindControl("RFIRecipient");
            Recipientproxy.ControlAvailable += new EventHandler<ControlAvailableEventArgs>((s4, e4) =>
            {
                MatrixControls.NPRFIRecipientInternal recTb = (MatrixControls.NPRFIRecipientInternal)e4.Control;
                recTb.SelectedItemPath = "ClientStaffMember.Name";
                recTb.ItemsSource = this.ClientStaffMember;
                recTb.LostFocus += recTb_LostFocus;
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
        void recTb_LostFocus(object sender, System.Windows.RoutedEventArgs e)
        {
            MatrixControls.NPRFIRecipientInternal tb = sender as MatrixControls.NPRFIRecipientInternal;
            ManualRFIRecipient = tb.NPRFIRecipient;
            this.RFIProperty.RFIRecipient = tb.GetValue(MatrixControls.NPRFIRecipientInternal.NPRFIRecipientProperty).ToString();
        }

        void sonTb_LostFocus(object sender, System.Windows.RoutedEventArgs e)
        {
            MatrixControls.NPSalesOrderNumberInternal tb = sender as MatrixControls.NPSalesOrderNumberInternal;
            ManualSalesOrderNo = tb.NPSalesOrderNumber;
            this.RFIProperty.MatrixSalesOrderNo = tb.GetValue(MatrixControls.NPSalesOrderNumberInternal.NPSalesOrderProperty).ToString();
        }

        void custTb_LostFocus(object sender, System.Windows.RoutedEventArgs e)
        {
            MatrixControls.NPCustomerInternal tb = sender as MatrixControls.NPCustomerInternal;

            ManualCustomerName = tb.NPCustomer;
            this.RFIProperty.Customer = tb.GetValue(MatrixControls.NPCustomerInternal.NPCustomerProperty).ToString();
        }
        private void custONTb_LostFocus(object sender, System.Windows.RoutedEventArgs e)
        {
            MatrixControls.NPCustomerOrderNumberInternal tb = sender as MatrixControls.NPCustomerOrderNumberInternal;
            ManualCustomerOrderNo = tb.NPCustomerOrderNumber;
            this.RFIProperty.CustomerOrderNo = tb.GetValue(MatrixControls.NPCustomerOrderNumberInternal.NPCustomerOrderNumberProperty).ToString();
        }

        partial void CreateNewRFI_Saving(ref bool handled)
        {
            if (!IsProjectRFI)
            {
                this.RFIProperty.MatrixSalesOrderNo = RFIProperty.MatrixSalesOrderNo;
                this.RFIProperty.Customer = RFIProperty.Customer;
                //add data to the matrixsalesordernumber table
                IEnumerable<MatrixSalesOrderNumber> foundSON = (from MatrixSalesOrderNumber a in this.DataWorkspace.MDSData.MatrixSalesOrderNumbers
                                                                where a.SalesOrderNo == RFIProperty.MatrixSalesOrderNo
                                                                select a).AsEnumerable();
                if (foundSON.Count<MatrixSalesOrderNumber>() == 0)
                {
                    MatrixSalesOrderNumber mson = this.DataWorkspace.MDSData.MatrixSalesOrderNumbers.AddNew();
                    mson.Customer = RFIProperty.Customer;
                    mson.SalesOrderNo = RFIProperty.MatrixSalesOrderNo;
                    this.DataWorkspace.MDSData.SaveChanges();
                }

                //add data to the customer table
                IEnumerable<Customer> foundCust = (from Customer c in this.DataWorkspace.MDSData.Customers
                                                   where c.CustomerName == RFIProperty.Customer
                                                   select c).AsEnumerable();
                if (foundCust.Count<Customer>() == 0)
                {
                    Customer cust = this.DataWorkspace.MDSData.Customers.AddNew();
                    cust.CustomerName = RFIProperty.Customer;
                    this.DataWorkspace.MDSData.SaveChanges();
                }
                //add data to the Customer staff table
                IEnumerable<ClientStaffMember> foundStaff = (from ClientStaffMember d in this.DataWorkspace.MDSData.ClientStaffMembers
                                                             where d.Name == RFIProperty.RFIRecipient
                                                             select d).AsEnumerable();
                if (foundStaff.Count<ClientStaffMember>() == 0)
                {
                    ClientStaffMember staff = this.DataWorkspace.MDSData.ClientStaffMembers.AddNew();
                    staff.Name = RFIProperty.RFIRecipient;
                    staff.Customer.CustomerName = RFIProperty.Customer;
                    this.DataWorkspace.MDSData.SaveChanges();
                }
            }
            else
            {
                this.RFIProperty.Customer = SelectedCustomer.CustomerName;
                //calculate the list of recipients now!
                //var recipients = from r in ClientStaffMemberRFIRecipients
                //                 where r.ClientStaffMember.Name != null
                //                 select r.ClientStaffMember.Name;
                //this.RFIProperty.RFIRecipient = String.Join(", ", recipients);
            }
        }

        partial void IsProjectRFI_Changed()
        {
            this.FindControl("Project").IsVisible = IsProjectRFI;
            this.FindControl("SelectedCustomer").IsVisible = IsProjectRFI;
            this.FindControl("CustomerOrderNo1").IsVisible = IsProjectRFI;
            this.FindControl("CustomerProjectCode").IsVisible = IsProjectRFI;
            this.FindControl("MatrixSalesOrderNo1").IsVisible = IsProjectRFI;
            this.FindControl("MatrixProjectCode").IsVisible = IsProjectRFI;
            this.FindControl("ClientStaffMember4").IsVisible = IsProjectRFI;
            if (IsProjectRFI)
            {
                this.FindControl("MatrixSalesOrderNo").IsVisible = false;
                this.FindControl("Customer").IsVisible = false;
                this.FindControl("RFIRecipient").IsVisible = false;
                this.FindControl("CustomerOrderNo").IsVisible = false;
            }
            else
            {
                //need to empty the values that may have been picked by the user before switching 
                // to non-project TQ!
                this.FindControl("MatrixSalesOrderNo").IsVisible = true;
                this.FindControl("Customer").IsVisible = true;
                this.FindControl("RFIRecipient").IsVisible = true;
                this.FindControl("CustomerOrderNo").IsVisible = true;
            }
            this.RFIProperty.UserName = this.Application.User.FullName;
        }

        partial void ManualCustomerName_Changed()
        {
            this.RFIProperty.Customer = ManualCustomerName;
            if (ManualCustomerName.ToUpper().Contains("CAVENDISH"))
            {
                //turn on the extra controls needed for them!
                this.FindControl("ConceptDesign").IsVisible = true;
                this.FindControl("DetailDesign").IsVisible = true;
                this.FindControl("Supply").IsVisible = true;
                this.FindControl("Construction").IsVisible = true;
                this.FindControl("ImmediateAttention").IsVisible = true;
                this.FindControl("ThreeDayTurnaround").IsVisible = true;
                this.FindControl("SevenDayTurnaround").IsVisible = true;
                this.FindControl("FourteenDayTurnaround").IsVisible = true;
                this.FindControl("MatrixSalesOrderNo").IsVisible = false;
            }
            else
            {
                this.FindControl("ConceptDesign").IsVisible = false;
                this.FindControl("DetailDesign").IsVisible = false;
                this.FindControl("Supply").IsVisible = false;
                this.FindControl("Construction").IsVisible = false;
                this.FindControl("ImmediateAttention").IsVisible = false;
                this.FindControl("ThreeDayTurnaround").IsVisible = false;
                this.FindControl("SevenDayTurnaround").IsVisible = false;
                this.FindControl("FourteenDayTurnaround").IsVisible = false;
                this.FindControl("MatrixSalesOrderNo").IsVisible = true;
            }
        }

        partial void ManualSalesOrderNo_Changed()
        {
            this.RFIProperty.MatrixSalesOrderNo = ManualSalesOrderNo;
            var RFI = (from RFI a in this.DataWorkspace.MDSData.RFIs
                       where a.MatrixSalesOrderNo == ManualSalesOrderNo
                       orderby a.RFINumber
                       select a).AsEnumerable();
            string RFIsId = Convert.ToString(RFI.Count() + 1);
            char pad = '0';
            string str = RFIsId.PadLeft(5, pad);
            RFIProperty.RFINumber = RFIProperty.MatrixSalesOrderNo + "-RFI" + str;
            RFIProperty.RFIDate = DateTime.Today.ToString("ddd d MMM yyyy");
        }

        partial void ManualRFIRecipient_Changed()
        {
            this.RFIProperty.RFIRecipient = ManualRFIRecipient;
        }

        partial void AddRecipient_Execute()
        {
            Application.Current.ShowCreateNewClientStaffMember(false);
        }

        partial void SelectedCustomer_Changed()
        {
            if (SelectedCustomer.CustomerName.ToUpper().Contains("CAVENDISH"))
            {
                //turn on the extra controls needed for them!
                this.FindControl("ConceptDesign").IsVisible = true;
                this.FindControl("DetailDesign").IsVisible = true;
                this.FindControl("Supply").IsVisible = true;
                this.FindControl("Construction").IsVisible = true;
                this.FindControl("ImmediateAttention").IsVisible = true;
                this.FindControl("ThreeDayTurnaround").IsVisible = true;
                this.FindControl("SevenDayTurnaround").IsVisible = true;
                this.FindControl("FourteenDayTurnaround").IsVisible = true;
                this.FindControl("MatrixSalesOrderNo").IsVisible = false;
            }
            else
            {
                this.FindControl("ConceptDesign").IsVisible = false;
                this.FindControl("DetailDesign").IsVisible = false;
                this.FindControl("Supply").IsVisible = false;
                this.FindControl("Construction").IsVisible = false;
                this.FindControl("ImmediateAttention").IsVisible = false;
                this.FindControl("ThreeDayTurnaround").IsVisible = false;
                this.FindControl("SevenDayTurnaround").IsVisible = false;
                this.FindControl("FourteenDayTurnaround").IsVisible = false;
                this.FindControl("MatrixSalesOrderNo").IsVisible = true;
            }
        }

        partial void AddNewClientStaffMember_Execute()
        {
            Application.Current.ShowCreateNewClientStaffMember(false);
        }

        partial void ClientStaffMember_Changed()
        {
            this.RFIProperty.RFIRecipient = ClientStaffMember.Name;
        }
    }
}