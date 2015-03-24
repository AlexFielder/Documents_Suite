using Microsoft.LightSwitch;
using Microsoft.LightSwitch.Framework.Client;
using Microsoft.LightSwitch.Presentation;
using Microsoft.LightSwitch.Presentation.Extensions;
using Microsoft.VisualBasic;
using OfficeIntegration;
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Runtime.InteropServices.Automation;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interop;
using System.Windows.Media.Imaging;


namespace LightSwitchApplication
{
    public class CreateNewDocument
    {
        /// <summary>
        /// the results object we pass to the various tables in our document(s).
        /// </summary>
        public dynamic coptresults;
        /// <summary>
        /// the path our templates live at
        /// </summary>
        public string matrixDocs = @"\\MATRIXSERVER\Data\02 - OFFICE TEMPLATES\";
        /// <summary>
        /// Possibly unused variable!
        /// </summary>
        public List<string> templates = new List<string>();
        /// <summary>
        /// Generates a new document based on the document name and associated properties.
        /// Uses entitysets for the documents/tasks/reviews required.
        /// TO DO: 
        /// 1) Check whether files exist in the Vault and prompt user to check out/update as necessary.
        /// 2) 
        /// </summary>
        /// <param name="DocumentName">The name of the document we are creating.</param>
        /// <param name="LSProp">The Properties we wish to publish</param>
        /// <param name="LSDocs">The list of documents/tasks/reviews we need to publish</param>
        /// <param name="IsProjectDoc">Defines whether the document is using project data or independant</param>
        /// <param name="ListByProjectOnly">Defines whether the document (Timesheet mainly) is using project data.</param>
        public void GenerateFromAddNew(string DocumentName, dynamic LSProp, dynamic LSDocs, Boolean IsProjectDoc = true, Boolean ListByProjectOnly = false)
        {
            List<ColumnMapping> mapContent = new List<ColumnMapping>();
            List<OfficeIntegration.ColumnMapping> mappings = new List<ColumnMapping>();
            string wordDoc = "";
            dynamic doc = "";
            string filename = "";
            if (IsProjectDoc) // project-specific!
            {
                mapContent.Add(new ColumnMapping("Customer", "Customer"));
                mapContent.Add(new ColumnMapping("ProjectTitle", "Project"));
                mapContent.Add(new ColumnMapping("MatrixSalesOrderNo", "MatrixSalesOrderNo"));
                mapContent.Add(new ColumnMapping("MatrixProjectCode", "MatrixProjectCode"));
                mapContent.Add(new ColumnMapping("CustomerProjectCode", "CustomerProjectCode"));
                mapContent.Add(new ColumnMapping("CustomerOrderNo", "CustomerOrderNo"));
                mapContent.Add(new ColumnMapping("UserName", "UserName"));
                mapContent.Add(new ColumnMapping("UserPosition", "UserPosition"));
            }
            else //non project-specific!
            {
                mapContent.Add(new ColumnMapping("Customer", "Customer"));
                mapContent.Add(new ColumnMapping("MatrixSalesOrderNo", "MatrixSalesOrderNo"));
                mapContent.Add(new ColumnMapping("UserName", "UserName"));
                mapContent.Add(new ColumnMapping("UserPosition", "UserPosition"));
            }
            dynamic word = null;

            #region "Document-specific"

            switch (DocumentName)
            {
                case "CofC":
                    #region "CofC --WORKING! 2013-10-01 AF"
                    LightSwitchApplication.CofC cofC = LSProp;
                    //template name
                    wordDoc = matrixDocs + "CofC QMF 10 Issue 06.dotx";
                    //template-specifics
                    mapContent.Add(new ColumnMapping("DeliveryNoteNo", "DeliveryNoteNo"));
                    mapContent.Add(new ColumnMapping("CertificateNo", "CertificateNo"));
                    mapContent.Add(new ColumnMapping("CofCDate", "CofCDate"));
                    doc = OfficeIntegration.Word.GenerateDocument(wordDoc, cofC, mapContent);
                    //template datatable-specific columns
                    mappings.Add(new OfficeIntegration.ColumnMapping("", "Quantity"));
                    mappings.Add(new OfficeIntegration.ColumnMapping("", "DrawingNumber"));
                    mappings.Add(new OfficeIntegration.ColumnMapping("", "DrawingRevision"));
                    mappings.Add(new OfficeIntegration.ColumnMapping("", "DrawingTitle"));
                    word = OfficeIntegration.Word.GetWord();
                    Word.GetDocument(word, wordDoc);
                    //export our list of documents:
                    Microsoft.LightSwitch.Framework.EntitySet<CofCDoc> docs = LSDocs;
                    var cofcresults = from a in docs
                                    where a.CofC.CertificateNo == cofC.CertificateNo
                                    select a;
                    Word.Export(doc, "DataTable", 2, false, cofcresults, mappings);

                    filename = @"\\MATRIXSERVER\Data\01 - CAD SUPPORT FILES\CofCs\" + cofC.CertificateNo + ".docx";
                    break;
                    #endregion
                case "DT":
                    #region "Transmittal --WORKING! 2013-12-03 AF"
                    LightSwitchApplication.Transmittal trans = LSProp; //this transimittal!
                    //template name
                    wordDoc = matrixDocs + "DT QMF39 Issue 02.dotx";
                    //template-specifics
                    mapContent.Add(new ColumnMapping("TransId", "TransmittalNumber"));
                    mapContent.Add(new ColumnMapping("Month", "Month"));
                    mapContent.Add(new ColumnMapping("UserName", "UserName"));
                    mapContent.Add(new ColumnMapping("Recipient", "Recipient"));
                    mapContent.Add(new ColumnMapping("QueryDate", "QueryDate"));
                    doc = OfficeIntegration.Word.GenerateDocument(wordDoc, trans, mapContent);
                    //template datatable-specific columns
                    mappings.Add(new OfficeIntegration.ColumnMapping("", "DocNo"));
                    mappings.Add(new OfficeIntegration.ColumnMapping("", "DocRevision"));
                    mappings.Add(new OfficeIntegration.ColumnMapping("", "SheetSize"));
                    mappings.Add(new OfficeIntegration.ColumnMapping("", "MPENumber"));
                    mappings.Add(new OfficeIntegration.ColumnMapping("", "DocTitle"));
                    mappings.Add(new OfficeIntegration.ColumnMapping("", "Copies"));
                    mappings.Add(new OfficeIntegration.ColumnMapping("", "TransmittalDocReasonsForIssue"));
                    word = OfficeIntegration.Word.GetWord();
                    Word.GetDocument(word, wordDoc);
                    Microsoft.LightSwitch.Framework.EntitySet<TransmittalDoc> transDocs = LSDocs; //all the documents!
                    var transresults = from TransmittalDoc a in transDocs
                                        where a.Transmittal.TransmittalNumber == trans.TransmittalNumber
                                        select a;
                    Word.Export(doc, "DataTable", 2, false, transresults, mappings);

                    filename = @"\\MATRIXSERVER\Data\01 - CAD SUPPORT FILES\Transmittals\" + trans.TransmittalNumber + ".docx";
                    break;
                    #endregion
                case "DDA":
                    #region"Drawing and Document Approval Form --WORKING! 2013-10-02 AF"
                    LightSwitchApplication.DandDA dandDA = LSProp; //this DandDA!
                    //template name
                    wordDoc = matrixDocs + "DDA QMF47 ISsue 02.dotx";
                    //template-specifics
                    mapContent.Add(new ColumnMapping("DDAId", "DDANumber"));
                    mapContent.Add(new ColumnMapping("DDADate", "DDADate"));
                    doc = OfficeIntegration.Word.GenerateDocument(wordDoc, dandDA, mapContent);
                    //template datatable-specific columns
                    mappings.Add(new OfficeIntegration.ColumnMapping("", "DrawingNo"));
                    mappings.Add(new OfficeIntegration.ColumnMapping("", "Title"));
                    mappings.Add(new OfficeIntegration.ColumnMapping("", "Revision"));
                    word = OfficeIntegration.Word.GetWord();
                    Word.GetDocument(word, wordDoc);
                    Microsoft.LightSwitch.Framework.EntitySet<DandADoc> DandDADocs = LSDocs; //all the documents!
                    var dandDAResults = from DandADoc a in DandDADocs
                                        where a.DandDA.DDANumber == dandDA.DDANumber
                                        select a;
                    Word.Export(doc, "DataTable", 2, false, dandDAResults, mappings);

                    filename = @"\\MATRIXSERVER\Data\01 - CAD SUPPORT FILES\DDAs\" + dandDA.DDANumber + ".docx";
                    break;
                    #endregion
                case "DDC":
                    #region "Drawing and Document Control Form --WORKING! 2013-10-11 AF"
                    LightSwitchApplication.DandDC dandDC = LSProp; //this DandDC!
                    filename = @"\\MATRIXSERVER\Data\01 - CAD SUPPORT FILES\DDCs\" + dandDC.DDCNumber + ".docx";
                    if (!File.Exists(filename)) //new file
                    {
                        //template name
                        wordDoc = matrixDocs + "DDC QMF25 Issue 02.dotx";
                        mapContent.Add(new ColumnMapping("DesignId", "DDCId"));
                        mapContent.Add(new ColumnMapping("Month", "Month"));
                        mapContent.Add(new ColumnMapping("BasedOn", "BasedOn"));
                        mapContent.Add(new ColumnMapping("Essentials", "Essentials"));
                        mapContent.Add(new ColumnMapping("StatsRegs", "StatsRegs"));
                        mapContent.Add(new ColumnMapping("SpecReqs", "SpecReqs"));
                        mapContent.Add(new ColumnMapping("DandDCStartDate", "DandDCStartDate"));
                        doc = OfficeIntegration.Word.GenerateDocument(wordDoc, dandDC, mapContent);
                    }
                    else //updating the existing file.
                    {
                        wordDoc = filename;
                        mapContent.Add(new ColumnMapping("Verification", "OutputsVerification"));
                        mapContent.Add(new ColumnMapping("Validation", "OutputsValidation"));
                        mapContent.Add(new ColumnMapping("OutputsValiMDate", "OutputsValiMDate"));
                        mapContent.Add(new ColumnMapping("OutputsVeriMDate", "OutputsVeriMDate"));
                        mapContent.Add(new ColumnMapping("OutputsVeriMMatrixAttendees", "MatrixVerificationMeetingAttendees"));
                        mapContent.Add(new ColumnMapping("OutputsVeriMClientAttendees", "ClientVerificationMeetingAttendees"));
                        mapContent.Add(new ColumnMapping("OutputsValiMMatrixAttendees", "MatrixValidationMeetingAttendees"));
                        mapContent.Add(new ColumnMapping("OutputsValiMClientAttendees", "ClientValidationMeetingAttendees"));
                        doc = OfficeIntegration.Word.GenerateDocument(wordDoc, dandDC, mapContent);
                        mappings.Add(new OfficeIntegration.ColumnMapping("", "ReviewDateStr"));
                        mappings.Add(new OfficeIntegration.ColumnMapping("", "Reviewer"));
                        mappings.Add(new OfficeIntegration.ColumnMapping("", "ReviewLocation"));
                        mappings.Add(new OfficeIntegration.ColumnMapping("", "ReviewComment"));

                        word = OfficeIntegration.Word.GetWord();
                        Word.GetDocument(word, wordDoc);
                        Microsoft.LightSwitch.Framework.EntitySet<Review> reviews = LSDocs; //all the documents!
                        var dandDCResults = from Review a in reviews
                                            where a.Project.ProjectTitle == dandDC.Project.ProjectTitle
                                            select a;
                        Word.Export(doc, "Reviews", 3, false, dandDCResults, mappings);

                        filename = @"\\MATRIXSERVER\Data\01 - CAD SUPPORT FILES\DDCs\" + dandDC.DDCNumber + ".docx"; 
                    }
                    #endregion
                    break;
                case "COPT":
                    #region "CAD Office Project Report --WORKING! 2013-12-03 AF"
                    LightSwitchApplication.CADOfficeProjT copt = LSProp;
                    //var myDocs = Environment.GetFolderPath(Environment.SpecialFolder.Templates);
                    wordDoc = matrixDocs + "COPT QMF41 Issue 02.dotx";
                    //template-specifics
                    mapContent.Add(new ColumnMapping("COPTId", "COPTNumber"));
                    mapContent.Add(new ColumnMapping("Month", "Month"));
                    mapContent.Add(new ColumnMapping("Year", "Year"));
                    mapContent.Add(new ColumnMapping("UserName", "UserName"));
                    doc = OfficeIntegration.Word.GenerateDocument(wordDoc, copt, mapContent);
                    //template datatable-specific columns
                    mappings.Add(new OfficeIntegration.ColumnMapping("", "TaskDate"));
                    mappings.Add(new OfficeIntegration.ColumnMapping("", "TaskName"));
                    mappings.Add(new OfficeIntegration.ColumnMapping("", "Hours"));
                    word = OfficeIntegration.Word.GetWord();
                    Word.GetDocument(word, wordDoc);
                    //Project or month-specifics:
                    Microsoft.LightSwitch.Framework.EntitySet<Task> tasks = LSDocs;
                    //dynamic sortedGroups = getGroupedTasks(tasks);
                    if (IsProjectDoc) //project tasks
                    {
                        var filteredTasks = from Task t in tasks
                                            where t.Project != null
                                            select t;
                        if (copt.EndDate == null)
                        {
                            if (!ListByProjectOnly)
                            {
                                coptresults = from Task b in filteredTasks
                                              where b.Project.ProjectTitle == copt.Project.ProjectTitle
                                              && b.Day >= copt.StartDate
                                              && b.User == copt.UserName
                                              select b;
                            }
                            else
                            {
                                coptresults = from Task b in filteredTasks
                                              where b.Project.ProjectTitle == copt.Project.ProjectTitle
                                              && b.Day >= copt.StartDate
                                              select b;
                            }
                            
                        }
                        else
                        {
                            if (!ListByProjectOnly)
                            {
                                coptresults = from Task b in filteredTasks
                                              where b.Project.ProjectTitle == copt.Project.ProjectTitle
                                              && b.Day >= copt.StartDate
                                              && b.Day <= copt.EndDate
                                              && b.User == copt.UserName
                                              select b;
                            }
                            else
                            {
                                coptresults = from Task b in filteredTasks
                                              where b.Project.ProjectTitle == copt.Project.ProjectTitle
                                              && b.Day >= copt.StartDate
                                              && b.Day <= copt.EndDate
                                              select b;
                            }

                        }
                        filename = @"\\MATRIXSERVER\Data\01 - CAD SUPPORT FILES\Project Timesheets\" 
                            + copt.COPTNumber 
                            + ".docx";
                    }
                    else //monthly tasks
                    {
                        if (copt.EndDate == null)
                        {
                            coptresults = from Task b in tasks
                                          where b.Day >= copt.StartDate
                                          && b.User == copt.UserName
                                          orderby b.Day
                                          select b;
                        }
                        else
                        {
                            coptresults = from Task b in tasks
                                          where b.Day >= copt.StartDate
                                          && b.Day <= copt.EndDate
                                          && b.User == copt.UserName
                                          orderby b.Day
                                          select b;
                        }
                        filename = @"\\MATRIXSERVER\Data\01 - CAD SUPPORT FILES\Project Timesheets\" 
                            + copt.COPTNumber 
                            + ".docx";
                    }

                    //var coptresults = from Task t in tasks
                    //                  select t;
                    
                    Word.Export(doc, "DataTable", 2, false, coptresults, mappings);

                    
                    break;
                    #endregion
                case "":
                    break;
            }
            #endregion

            dynamic _word = AutomationFactory.GetObject("Word.Application");
            var _doc = _word.ActiveDocument;
            if (DocumentName == "DDC")
            {
                #region "checkboxes"
                if (LSProp.ProtoTypeProduced == true)
                {
                    dynamic ccs = _doc.SelectContentControlsByTitle("PrototypeProducedYes");
                    if (ccs != null)
                    {
                        dynamic cc = ccs[1];
                        cc.Checked = true;
                    }
                    ccs = _doc.SelectContentControlsByTitle("PrototypeProducedNo");
                    if (ccs != null)
                    {
                        dynamic cc = ccs[1];
                        cc.Checked = false;
                    }
                }
                else
                {
                    dynamic ccs = _doc.SelectContentControlsByTitle("PrototypeProducedNo");
                    if (ccs != null)
                    {
                        dynamic cc = ccs[1];
                        cc.Checked = true;
                    }
                }
                if (LSProp.MaterialsCosted == true)
                {
                    dynamic ccs = _doc.SelectContentControlsByTitle("MaterialsYes");
                    if (ccs != null)
                    {
                        dynamic cc = ccs[1];
                        cc.Checked = true;
                    }
                    ccs = _doc.SelectContentControlsByTitle("MaterialsNo");
                    if (ccs != null)
                    {
                        dynamic cc = ccs[1];
                        cc.Checked = false;
                    }
                }
                else
                {
                    dynamic ccs = _doc.SelectContentControlsByTitle("MaterialsNo");
                    if (ccs != null)
                    {
                        dynamic cc = ccs[1];
                        cc.Checked = true;
                    }
                }
                if (LSProp.LabourCosted == true)
                {
                    dynamic ccs = _doc.SelectContentControlsByTitle("LabourCostedYes");
                    if (ccs != null)
                    {
                        dynamic cc = ccs[1];
                        cc.Checked = true;
                    }
                    ccs = _doc.SelectContentControlsByTitle("LabourCostedNo");
                    if (ccs != null)
                    {
                        dynamic cc = ccs[1];
                        cc.Checked = false;
                    }
                }
                else
                {
                    dynamic ccs = _doc.SelectContentControlsByTitle("LabourCostedNo");
                    if (ccs != null)
                    {
                        dynamic cc = ccs[1];
                        cc.Checked = true;
                    }
                }
                if (LSProp.ClientApproved == true)
                {
                    dynamic ccs = _doc.SelectContentControlsByTitle("ClientApprovedYes");
                    if (ccs != null)
                    {
                        dynamic cc = ccs[1];
                        cc.Checked = true;
                    }
                    ccs = _doc.SelectContentControlsByTitle("ClientApprovedNo");
                    if (ccs != null)
                    {
                        dynamic cc = ccs[1];
                        cc.Checked = false;
                    }
                }
                else
                {
                    dynamic ccs = _doc.SelectContentControlsByTitle("ClientApprovedNo");
                    if (ccs != null)
                    {
                        dynamic cc = ccs[1];
                        cc.Checked = true;
                    }
                }
                #endregion

            }
            _doc.SaveAs(filename,12);
            if (!File.Exists(filename))
            {
                MessageBox.Show("The created Word File didn't save for some reason, please do a manual saveas to preserve our template!");
            }
            mapContent = null;
            mappings = null;
        }
        /// <summary>
        /// Generates a new document based on the document name and associated properties.
        /// </summary>
        /// <param name="DocumentName">The name of the document we are creating.</param>
        /// <param name="LSProp">The Properties we wish to publish.</param>
        /// <param name="IsProjectDoc">Boolean controlling whether the document is project-specific.</param>
        public void GenerateFromAddNew(string DocumentName, dynamic LSProp,Boolean IsProjectDoc = true)
        {
            List<ColumnMapping> mapContent = new List<ColumnMapping>();
            //List<OfficeIntegration.ColumnMapping> mappings = new List<ColumnMapping>();
            string wordDoc = "";
            dynamic doc = "";
            string filename = "";
            //object doc = null;
            if (IsProjectDoc) // project-specific!
            {
                mapContent.Add(new ColumnMapping("Customer", "Customer"));
                mapContent.Add(new ColumnMapping("ProjectTitle", "Project"));
                mapContent.Add(new ColumnMapping("MatrixSalesOrderNo", "MatrixSalesOrderNo"));
                mapContent.Add(new ColumnMapping("MatrixProjectCode", "MatrixProjectCode"));
                mapContent.Add(new ColumnMapping("CustomerProjectCode", "CustomerProjectCode"));
                mapContent.Add(new ColumnMapping("CustomerOrderNo", "CustomerOrderNo"));
                mapContent.Add(new ColumnMapping("UserName", "UserName"));
                mapContent.Add(new ColumnMapping("UserPosition", "UserPosition"));
            }
            else //non project-specific!
            {
                mapContent.Add(new ColumnMapping("Customer", "Customer"));
                mapContent.Add(new ColumnMapping("MatrixSalesOrderNo", "MatrixSalesOrderNo"));
                mapContent.Add(new ColumnMapping("UserName", "UserName"));
                mapContent.Add(new ColumnMapping("UserPosition", "UserPosition"));
                mapContent.Add(new ColumnMapping("CustomerOrderNo", "CustomerOrderNo"));
            }
            
            #region "Document-specific"
            switch (DocumentName)
            {
                case "TQ":
                    #region "TQ --WORKING! 2013-10-04 AF"
                    try
                    {
                        LightSwitchApplication.TQDetail tq = LSProp;
                        //template name
                        String tmpcustomerName = tq.Customer;
                        String customerName = tmpcustomerName.ToUpper();
                        if (customerName.Contains("CAVENDISH"))
                        {
                            wordDoc = matrixDocs + @"\Cavendish Nuclear\Supplier TQ.docx";
                            mapContent.Add(new ColumnMapping("QueryReturnDate", "TQReturnDate"));
                        }
                        else
                        {
                            wordDoc = matrixDocs + "TQ QMF33 Issue 06.dotx";
                        }
                        //template-specifics
                        mapContent.Add(new ColumnMapping("TQId", "TQNumber"));
                        mapContent.Add(new ColumnMapping("DrawingNumber", "DrawingNumber"));
                        mapContent.Add(new ColumnMapping("DrawingRevision", "DrawingRevision"));
                        mapContent.Add(new ColumnMapping("QueryText", "QueryText"));
                        mapContent.Add(new ColumnMapping("QueryDate", "QueryDate"));
                        mapContent.Add(new ColumnMapping("ResponseSatisfactoryYesNo", "ResponseSatisfactory"));
                        doc = OfficeIntegration.Word.GenerateDocument(wordDoc, tq, mapContent);
                        filename = @"\\MATRIXSERVER\Data\01 - CAD SUPPORT FILES\TQs\" + tq.TQNumber + ".docx";
                        
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    break;
                    #endregion
                case "RFI":
                    #region "RFI --WORKING! 2013-10-07 AF-- changed to collect list of recipients-- see table code for more..."
                    LightSwitchApplication.RFI rfi = LSProp;
                    //this file may need to be updated, so we have to see if the file for this rfi already exists:
                    filename = @"\\MATRIXSERVER\Data\01 - CAD SUPPORT FILES\RFIs\" + rfi.RFINumber + ".docx";
                    if (!File.Exists(filename)) //new file.
                    {
                        //template name
                        String tmpcustomerName = rfi.Customer;
                        String customerName = tmpcustomerName.ToUpper();
                        if (customerName.Contains("CAVENDISH"))
                        {
                            wordDoc = matrixDocs + @"\Cavendish Nuclear\Supplier RFI.dotx";
                            mapContent.Add(new ColumnMapping("QueryReturnDate", "TQReturnDate"));
                        }
                        else
                        {
                            wordDoc = matrixDocs + "RFI QMF40 Issue 03.dotx";
                        }
                        //template-specifics
                        mapContent.Add(new ColumnMapping("RFIId", "RFINumber"));
                        mapContent.Add(new ColumnMapping("RFIRecipient", "RFIRecipient"));
                        mapContent.Add(new ColumnMapping("DrawingNumber", "DrawingNumber"));
                        mapContent.Add(new ColumnMapping("DrawingRevision", "DrawingRevision"));
                        mapContent.Add(new ColumnMapping("RequestText", "RequestText"));
                        mapContent.Add(new ColumnMapping("RFIDate", "RFIDate"));
                        doc = OfficeIntegration.Word.GenerateDocument(wordDoc, rfi, mapContent);
                    }
                    else //updating the existing file.
                    {
                        wordDoc = filename;
                        //template name
                        //wordDoc = matrixDocs + "RFI QMF40 Issue 03.dotx";
                        //template-specifics
                        mapContent.Add(new ColumnMapping("RFIId", "RFINumber"));
                        mapContent.Add(new ColumnMapping("RFIRecipient", "RFIRecipient"));
                        mapContent.Add(new ColumnMapping("DrawingNumber", "DrawingNumber"));
                        mapContent.Add(new ColumnMapping("DrawingRevision", "DrawingRevision"));
                        mapContent.Add(new ColumnMapping("RequestText", "RequestText"));
                        mapContent.Add(new ColumnMapping("RFIDate", "RFIDate"));
                        mapContent.Add(new ColumnMapping("ResponseToRFI", "ResponseToRFI"));
                        mapContent.Add(new ColumnMapping("RespondentPosition", "RespondentPosition"));
                        mapContent.Add(new ColumnMapping("ResponseDate", "ResponseDate"));
                        mapContent.Add(new ColumnMapping("ResponseImplications", "ResponseImplications"));
                        mapContent.Add(new ColumnMapping("Respondent", "Respondent"));
                        mapContent.Add(new ColumnMapping("MatrixRespondent", "MatrixRespondent"));
                        mapContent.Add(new ColumnMapping("MatrixRespondentPosition", "MatrixRespondentPosition"));
                        mapContent.Add(new ColumnMapping("MatrixRespondentDate", "MatrixRespondentDate"));
                        //word = OfficeIntegration.Word.GetWord();
                        //Word.GetDocument(word, wordDoc);
                        doc = OfficeIntegration.Word.GenerateDocument(wordDoc, rfi, mapContent);
                    }
                    break;
                    #endregion
                case "":
                    break;
            }
            #endregion

            dynamic _word = AutomationFactory.GetObject("Word.Application");
            var _doc = _word.ActiveDocument;

            if (File.Exists(filename))
            {
                _doc.SaveAs(filename,12);
            }
            else
            {
                _doc.SaveAs(filename,12);
            }
            if (!File.Exists(filename))
            {
                MessageBox.Show("The created Word File didn't save for some reason, please do a manual saveas to preserve our template!");
            }
            mapContent = null;
            //mappings = null;
        }

        /// <summary>
        /// Updates the UserStrings for a bunch of collections.
        /// At the moment, it only works on The Design and Development Control Form.
        /// </summary>
        /// <param name="DocumentName">The name of the document we are updating the UserStrings for.</param>
        /// <param name="LSProp">The Properties we wish to update.</param>
        internal void UpdateUserStrings(string DocumentName, dynamic LSProp)
        {
            switch (DocumentName)
            {
                case "DDC":
                    LightSwitchApplication.DandDC dandDC = LSProp; //this DandDC!
                    if (dandDC.ClientStaffMemberValidationAttendees.Count() > 0)
                    {
                        var cvalma = from c in dandDC.ClientStaffMemberValidationAttendees
                                     where c.ClientStaffMember.Name != null
                                     select c.ClientStaffMember.Name;
                        dandDC.ClientValidationMeetingAttendees = String.Join(", ", cvalma);
                    }

                    if (dandDC.MatrixStaffMemberValidationAttendees.Count() > 0)
                    {
                        var mvalma = from c in dandDC.MatrixStaffMemberValidationAttendees
                                     where c.MatrixStaffMember.Name != null
                                     select c.MatrixStaffMember.Name;
                        dandDC.MatrixValidationMeetingAttendees = String.Join(",", mvalma); 
                    }

                    if (dandDC.ClientStaffMemberVerificationAttendees.Count() > 0)
                    {
                        var cvma = from c in dandDC.ClientStaffMemberVerificationAttendees
                                   where c.ClientStaffMember.Name != null
                                   select c.ClientStaffMember.Name;
                        dandDC.ClientVerificationMeetingAttendees = String.Join(", ", cvma); 
                    }
                    if (dandDC.MatrixStaffMemberVerificationAttendees.Count() > 0)
                    {
                        var mvma = from c in dandDC.MatrixStaffMemberVerificationAttendees
                                   where c.MatrixStaffMember.Name != null
                                   select c.MatrixStaffMember.Name;
                        dandDC.MatrixVerificationMeetingAttendees = String.Join(", ", mvma); 
                    }
                    break;
                case "SomethingElse":
                    break;
            }
        }

        /// <summary>
        /// Groups tasks together according to Project.
        /// </summary>
        /// <param name="tasks"></param>
        /// <param name="Project"></param>
        /// <param name="IsProjectDoc"></param>
        /// <returns></returns>
        public dynamic getGroupedTasks(Microsoft.LightSwitch.Framework.EntitySet<Task> tasks,String Project = "ProjectTitle",Boolean IsProjectDoc = false)
        {
            List<Task> groupedTasks = new List<Task>();
            if (IsProjectDoc)
            {
                var groupedTasksByProject = 
                    from Task t in tasks
                    where t.Project != null
                    let project = t.Project.ProjectTitle
                    group new { t.TaskName, t.User, t.Day, t.Hours, t.TaskType, t.Project } by project into groupedTask
                    orderby groupedTask.Key
                    select groupedTask;
                var summary =
                    from Task t in tasks
                    where t.Project != null
                    let k = new
                    {
                        Month = t.Day,
                        Project = t.Project.ProjectTitle
                    }
                    group t by k into tasksSum
                    select new
                    {
                        Month = tasksSum.Key.Month,
                        Project = tasksSum.Key.Project,
                        Qty = tasksSum.Sum(t => Convert.ToDecimal(t.Hours))
                    };
                foreach (var task in groupedTasksByProject)
                {
                    Console.WriteLine("Project: {0}", (task.Key));
                    foreach (var item in task)
                    {
                        Console.WriteLine("\t{0},{1},{2},{3},{4},{5}", item.Day, item.User, item.TaskName, item.Hours, item.TaskType,item.Project.ProjectTitle);
                    }
                }
                foreach (var item in summary)
                    Console.WriteLine(item);
            }
            else
            {
                foreach (Task t in tasks)
                {
                    getCommonWords(t.TaskName, t.Project.ProjectTitle);
                }
            }
            
            // Need to figure out the common words in each task description if the task isn't project-related
            return groupedTasks;
        }
        public dynamic getCommonWords(string taskDescription, String Project = "ProjectTitle")
        {
            //Split the inputstring into lowercase words using regex:
            List<string> wordList = Regex.Split(taskDescription.ToLower(), @"\W+").ToList();

            // Define and remove stopwords
            string[] stopwords = new string[] { "and", "the", "she", "for", "this", "you", "but", "is" };
            var wordsToRemove = new HashSet<string>(stopwords);
            wordList.RemoveAll(x => wordsToRemove.Contains(x));

            // Create a new Dictionary object
            Dictionary<string, int> dictionary = new Dictionary<string, int>();

            // Loop over all over the words in our wordList...
            foreach (string word in wordList)
            {
                // If the length of the word is at least three letters...
                if (word.Length >= 3)
                {
                    // ...check if the dictionary already has the word.
                    if (dictionary.ContainsKey(word))
                    {
                        // If we already have the word in the dictionary, increment the count of how many times it appears
                        dictionary[word]++;
                    }
                    else
                    {
                        // Otherwise, if it's a new word then add it to the dictionary with an initial count of 1
                        dictionary[word] = 1;
                    }

                } // End of word length check

            } // End of loop over each word in our input

            // Create a dictionary sorted by value (i.e. how many times a word occurs)
            var sortedDict = (from entry in dictionary orderby entry.Value descending select entry).ToDictionary(pair => pair.Key, pair => pair.Value);

            // Loop through the sorted dictionary and output the top 10 most frequently occurring words
            int count = 1;
            //Console.WriteLine("---- Most Frequent Terms in the File: " + filename + " ----");
            Console.WriteLine();
            foreach (KeyValuePair<string, int> pair in sortedDict)
            {
                // Output the most frequently occurring words and the associated word counts
                Console.WriteLine(count + "\t" + pair.Key + "\t" + pair.Value);
                count++;

                // Only display the top 10 words then break out of the loop!
                if (count > 10)
                {
                    break;
                }
            }

            // Wait for the user to press a key before exiting
            //Console.ReadKey();
            return sortedDict;
        } // End of Main method

        //public void GroupTasks
    }
    //#region ConcurrentDictionaryExample Not Working!
    //class MainClass
    //{
    //    public static void Main(string[] args)
    //    {
    //        Console.WriteLine("Counting words...");
    //        DateTime start_at = DateTime.Now;
    //        TrieNode root = new TrieNode(null, '?');
    //        Dictionary<DataReader, Thread> readers = new Dictionary<DataReader, Thread>();

    //        if (args.Length == 0)
    //        {
    //            args = new string[] { "war-and-peace.txt", "ulysees.txt", "les-miserables.txt", "the-republic.txt",
    //                                  "war-and-peace.txt", "ulysees.txt", "les-miserables.txt", "the-republic.txt" };
    //        }

    //        if (args.Length > 0)
    //        {
    //            foreach (string path in args)
    //            {
    //                DataReader new_reader = new DataReader(path, ref root);
    //                Thread new_thread = new Thread(new_reader.ThreadRun);
    //                readers.Add(new_reader, new_thread);
    //                new_thread.Start();
    //            }
    //        }

    //        foreach (Thread t in readers.Values) t.Join();

    //        DateTime stop_at = DateTime.Now;
    //        Console.WriteLine("Input data processed in {0} secs", new TimeSpan(stop_at.Ticks - start_at.Ticks).TotalSeconds);
    //        Console.WriteLine();
    //        Console.WriteLine("Most commonly found words:");

    //        List<TrieNode> top10_nodes = new List<TrieNode> { root, root, root, root, root, root, root, root, root, root };
    //        int distinct_word_count = 0;
    //        int total_word_count = 0;
    //        root.GetTopCounts(ref top10_nodes, ref distinct_word_count, ref total_word_count);
    //        top10_nodes.Reverse();
    //        foreach (TrieNode node in top10_nodes)
    //        {
    //            Console.WriteLine("{0} - {1} times", node.ToString(), node.m_word_count);
    //        }

    //        Console.WriteLine();
    //        Console.WriteLine("{0} words counted", total_word_count);
    //        Console.WriteLine("{0} distinct words found", distinct_word_count);
    //        Console.WriteLine();
    //        Console.WriteLine("done.");
    //    }
    //}

    //#region Input data reader

    //public class DataReader
    //{
    //    static int LOOP_COUNT = 1;
    //    private TrieNode m_root;
    //    private string m_path;

    //    public DataReader(string path, ref TrieNode root)
    //    {
    //        m_root = root;
    //        m_path = path;
    //    }

    //    public void ThreadRun()
    //    {
    //        for (int i = 0; i < LOOP_COUNT; i++) // fake large data set buy parsing smaller file multiple times
    //        {
    //            using (FileStream fstream = new FileStream(m_path, FileMode.Open, FileAccess.Read))
    //            {
    //                using (StreamReader sreader = new StreamReader(fstream))
    //                {
    //                    string line;
    //                    while ((line = sreader.ReadLine()) != null)
    //                    {
    //                        string[] chunks = line.Split(null);
    //                        foreach (string chunk in chunks)
    //                        {
    //                            m_root.AddWord(chunk.Trim());
    //                        }
    //                    }
    //                }
    //            }
    //        }
    //    }
    //}

    //#endregion

    //#region TRIE implementation

    //public class TrieNode : IComparable<TrieNode>
    //{
    //    private char m_char;
    //    public int m_word_count;
    //    private TrieNode m_parent = null;
    //    private ConcurrentDictionary<char, TrieNode> m_children = null;

    //    public TrieNode(TrieNode parent, char c)
    //    {
    //        m_char = c;
    //        m_word_count = 0;
    //        m_parent = parent;
    //        m_children = new ConcurrentDictionary<char, TrieNode>();
    //    }

    //    public void AddWord(string word, int index = 0)
    //    {
    //        if (index < word.Length)
    //        {
    //            char key = word[index];
    //            if (char.IsLetter(key)) // should do that during parsing but we're just playing here! right?
    //            {
    //                if (!m_children.ContainsKey(key))
    //                {
    //                    m_children.TryAdd(key, new TrieNode(this, key));
    //                }
    //                m_children[key].AddWord(word, index + 1);
    //            }
    //            else
    //            {
    //                // not a letter! retry with next char
    //                AddWord(word, index + 1);
    //            }
    //        }
    //        else
    //        {
    //            if (m_parent != null) // empty words should never be counted
    //            {
    //                lock (this)
    //                {
    //                    m_word_count++;
    //                }
    //            }
    //        }
    //    }

    //    public int GetCount(string word, int index = 0)
    //    {
    //        if (index < word.Length)
    //        {
    //            char key = word[index];
    //            if (!m_children.ContainsKey(key))
    //            {
    //                return -1;
    //            }
    //            return m_children[key].GetCount(word, index + 1);
    //        }
    //        else
    //        {
    //            return m_word_count;
    //        }
    //    }

    //    public void GetTopCounts(ref List<TrieNode> most_counted, ref int distinct_word_count, ref int total_word_count)
    //    {
    //        if (m_word_count > 0)
    //        {
    //            distinct_word_count++;
    //            total_word_count += m_word_count;
    //        }
    //        if (m_word_count > most_counted[0].m_word_count)
    //        {
    //            most_counted[0] = this;
    //            most_counted.Sort();
    //        }
    //        foreach (char key in m_children.Keys)
    //        {
    //            m_children[key].GetTopCounts(ref most_counted, ref distinct_word_count, ref total_word_count);
    //        }
    //    }

    //    public override string ToString()
    //    {
    //        if (m_parent == null) return "";
    //        else return m_parent.ToString() + m_char;
    //    }

    //    public int CompareTo(TrieNode other)
    //    {
    //        return this.m_word_count.CompareTo(other.m_word_count);
    //    }
    //}

    //#endregion 
    //#endregion

}
