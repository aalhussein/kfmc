using kfmc.App_Code;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Collections;
using System.Text;
using System.util;
using System.Text.RegularExpressions;

using System.IO;
using iText;
using iText.Layout;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using System.Web.Security;

namespace kfmc.internship
{
    public partial class InstitutionAdvisor : System.Web.UI.Page
    {
      // SqlConnection con = new SqlConnection(CRUD.conStr); //  new SqlConnection(@"Data Source=T450S\SQLEXPRESS;persist security info=True;Integrated Security=SSPI; Initial Catalog=party;");
        protected void Page_Load(object sender, EventArgs e)
        {
               //txtCustomer.Focus();
                if (!IsPostBack)
                {
                  //  FillGrid();
                    populateGvAdvisor();
                    populateInstitutionDropList();
                }

            showHideButtons();

            if (Roles.IsUserInRole(User.Identity.Name, "admin") || Roles.IsUserInRole(User.Identity.Name, "supervisor"))
            {
                common.showHideGvColumn(gvInstitutionAdvisor, 9); // pass gv and intended col to hide action col
                common.showHideControl(gvInstitutionAdvisor, true);
            }
        }
        protected void Page_UnLoad(object sender, EventArgs e)
        {
            // code to be executed on user leaves the page
            // to avoid max connection pool exceeded
            CRUD.clearAllPools();
        }

        protected void showHideButtons()
        {
            if (!Roles.IsUserInRole(User.Identity.Name, "admin") || !Roles.IsUserInRole(User.Identity.Name, "supervisor"))
            {
                // hide/ show controls
                //////foreach (GridViewRow row in gvInstitutionAdvisor.Rows)
                //////{
                //////    ((Button)row.FindControl("btnDelete")).Visible=false;
                //////    //((Button)row.FindControl("btnEdit")).Visible = false;
                //////    common.showHideControl((Button)row.FindControl("btnEdit"), false);
                //////}

                // hide / show gv columns
                gvInstitutionAdvisor.Columns[8].Visible = false;  // hide action column - last one that has edit and delete 
            }
        }
        protected void populateInstitutionDropList()
        {
            ddlinstitutionId.Items.Clear();
            //ddlinstitution.Items.Add("Select Institution");
            CRUD myCrud = new CRUD();
            string sqlInstitution = @"Select institutionId, Institution  from Institution 
                                where institutionId not in (1) order by Institution ";
                   myCrud.populatComboViaDr(sqlInstitution, ddlinstitutionId, "institutionId", "Institution");

        }
        protected void populateGvAdvisor()
        {
            CRUD myCrud = new CRUD();
            string mySql = @"select institutionAdvisorId, ia.institutionId,institution ,fname,mi, lname,cell,officePhone, email, note
                            from institutionAdvisor ia inner join institution i on ia.institutionId=i.institutionId ";
            using (DataTable dt = myCrud.getDT (mySql))
            {
                Session["myDataTable"] = dt;
                gvInstitutionAdvisor.DataSource = Session["myDataTable"];
                gvInstitutionAdvisor.DataBind();
            }
        }
        protected   void ClearControls()
        {
            try
            {
                ddlinstitutionId.SelectedIndex = 0;
                txtFName.Text = "";
                txtMName.Text = "";
                txtLName.Text = "";
                txtCell.Text = "";
                txtOfficePhone.Text = "";
                txtEmail.Text = "";
                txtNote.Text = "";
                btnSave.Visible = true;
                btnUpdate.Visible = false;
            }
            catch
            {
                throw;
            }
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            #region Validate
            if (ddlinstitutionId.SelectedValue == "1")   // validate combo   notSelectedItem.Value but use .SelectedValue
            {
                lblOutput.Text = "Please select Institution !";
                lblOutput.ForeColor = System.Drawing.Color.Red;
                ddlinstitutionId.Focus();
                return;
            }
            if (String.IsNullOrEmpty(txtFName.Text))
            {
                lblOutput.Text = "Please fill the First Name !";
                lblOutput.ForeColor = System.Drawing.Color.Red;
                txtFName.Focus();
                return;
            }
            //if (String.IsNullOrEmpty(txtMName.Text))
            //{
            //    lblOutput.Text = "Please fill  Middle  Name !";
            //    lblOutput.ForeColor = System.Drawing.Color.Red;
            //    txtMName.Focus();
            //    return;
            //}
            if (String.IsNullOrEmpty(txtLName.Text))
            {
                lblOutput.Text = "Please fill Last Name !";
                lblOutput.ForeColor = System.Drawing.Color.Red;
                txtLName.Focus();
                return;
            }
            //if (String.IsNullOrEmpty(txtCell.Text))
            //{
            //    lblOutput.Text = "Please fill Cell number !";
            //    lblOutput.ForeColor = System.Drawing.Color.Red;
            //    txtCell.Focus();
            //    return;
            //}
            //if (String.IsNullOrEmpty(txtOfficePhone.Text))
            //{
            //    lblOutput.Text = "Please fill office Phone !";
            //    lblOutput.ForeColor = System.Drawing.Color.Red;
            //    txtOfficePhone.Focus();
            //    return;
            //}
            if (String.IsNullOrEmpty(txtEmail.Text))
            {
                lblOutput.Text = "Please fill Email !";
                lblOutput.ForeColor = System.Drawing.Color.Red;
                txtEmail.Focus();
                return;
            }
            #endregion
            // sample of long code to insert data into db
            // catpure input values
            int institutionId =  int.Parse(ddlinstitutionId.SelectedValue);
            string strFname = txtFName.Text;
            string strMn = txtMName.Text;
            string strLname  = txtLName.Text;
            string strCell = txtCell.Text;
            string strOfficeNo = txtOfficePhone.Text;
            string strEmail = txtEmail.Text;
            string strNote = txtNote.Text;

            try
            {
                CRUD myCRud = new CRUD();
                string mySql = @"insert into institutionAdvisor(institutionId,fname,mi,lname,cell,officePhone,email,note)
                                    values (@institutionId,@txtFName,@txtMName,@txtLastName,@txtCell,@txtOfficePhone,@txtEmail,@txtNote)";
                Dictionary<string, object> myPara = new Dictionary<string, object>();
                myPara.Add("@institutionId", int.Parse(ddlinstitutionId.SelectedValue));
                myPara.Add("@txtFName", strFname);
                myPara.Add("@txtMName", strMn);
                myPara.Add("@txtLastName", strLname);
                myPara.Add("@txtCell", strCell);
                myPara.Add("@txtOfficePhone", strOfficeNo);
                myPara.Add("@txtEmail", strEmail);
                myPara.Add("@txtNote", strNote);
                int rtn =  myCRud.InsertUpdateDelete(mySql, myPara);
                if (rtn >= 1)
                {
                    populateGvAdvisor();
                   //lblOutput.Text = "Insert Successfull !";
                    common.PostMsg(lblOutput, "Insert Successfull !", "green");
                }
            }
            catch (Exception ex)
            {
                common.PostMsg(lblOutput, ex.Message.ToString(), "red");
            }

            // ali try automation  to do 
            ////ContentPlaceHolder cph = (ContentPlaceHolder)Master.FindControl("MainContent");
            ////crudAuto myCrudAuto = new crudAuto();
            ////myCrudAuto.insertAuto(cph, lblOutput);
           //   ClearControls();
        }
        protected void btnClear_Click(object sender, EventArgs e)
        {
            try
            {
                lblOutput.Text = "";
                ClearControls();
            }
            catch
            {

            }
        }
        protected void btnEdit_Click(object sender, EventArgs e)
        {
            // this is how you access the gv values through btn > grow > then to find the control 
           // gvDepartments.BackColor = System.Drawing.Color.White; //  
            try
            {
                gvChangeColor();
                common.PostMsg(lblOutput, ""); // clear control
                ClearControls();
                Button btn = sender as Button;
                GridViewRow grow = btn.NamingContainer as GridViewRow;  // ref to the gv row 
                ddlinstitutionId.SelectedValue = (grow.FindControl("lblInstitutionId") as Label).Text;
                txtFName.Text = (grow.FindControl("lblFName") as Label).Text;
                txtMName.Text = (grow.FindControl("lblMi") as Label).Text;
                txtLName.Text = (grow.FindControl("lblLName") as Label).Text;
                txtCell.Text = (grow.FindControl("lblCell") as Label).Text;
                txtOfficePhone.Text = (grow.FindControl("lblOfficePhone") as Label).Text;
                txtEmail.Text = (grow.FindControl("lblEmail") as Label).Text;
                txtNote.Text = (grow.FindControl("lblNote") as Label).Text;
                hidinstitutionAdvisorId.Value  = (grow.FindControl("lblInstitutionAdvisorId") as Label).Text; 

                btnSave.Visible = false;
                btnUpdate.Visible = true;
                grow.BackColor = System.Drawing.Color.Yellow; //     
            }
            catch (Exception ex)
            {
                lblOutput.Text = ex.Message.ToString();
               // throw ex;
            }
        }
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            ////  gvDepartments.BackColor = System.Drawing.Color.White; //  
            try
            {
                CRUD myCrud = new CRUD();
                string mySql = @"update institutionAdvisor 
                                 set institutionId = @institutionId, fname = @fname,mi=@mi,lname =@lname,
                                 cell=@cell,officePhone=@officePhone,email=@email,note=@note
                                 where institutionAdvisorid = @institutionAdvisorIdD";
                Dictionary<string, object> myPara = new Dictionary<string, object>();
                myPara.Add("@institutionId", ddlinstitutionId.SelectedValue);
                myPara.Add("@fname", txtFName.Text);
                myPara.Add("@mi", txtMName.Text);
                myPara.Add("@lname", txtLName.Text);
                myPara.Add("@cell", txtCell.Text);
                myPara.Add("@officePhone", txtOfficePhone.Text);
                myPara.Add("@email", txtEmail.Text);
                myPara.Add("@note", txtNote.Text);
                //  myPara.Add("@lname", cbActive.Checked == false ? 0 : 1);
                myPara.Add("@institutionAdvisorIdD", hidinstitutionAdvisorId.Value);
                int rtn = myCrud.InsertUpdateDelete(mySql, myPara);
                App_Code.common.PostMsg(lblOutput, rtn);
                ClearControls();
            }

            catch (Exception ex)
            {
                App_Code.common.PostMsg(lblOutput, ex.Message.ToString());
            }
            populateGvAdvisor();
            btnSave.Visible = true;
            btnUpdate.Visible = false;
        }
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            // shows how to access gvr when the sender is a button inside the gv
            SqlConnection con = new SqlConnection();
            try
            {
                ClearControls();
                Button btn = sender as Button;
                GridViewRow grow = btn.NamingContainer as GridViewRow;
                int intInstitutionAdvisorId = int.Parse( (grow.FindControl("lblInstitutionAdvisorId") as Label).Text);
                CRUD myCrud = new CRUD();
                string mySql = @"delete  InstitutionAdvisor where InstitutionAdvisorId=@InstitutionAdvisorId";
                Dictionary<string, object> myPara = new Dictionary<string, object>();
                myPara.Add("@InstitutionAdvisorId", intInstitutionAdvisorId);
               int rtn =   myCrud.InsertUpdateDelete(mySql, myPara);
                App_Code.common.PostMsg(lblOutput, rtn);
           }
            catch ( Exception ex)
            {
                //  throw ex;
                App_Code.common.PostMsg(lblOutput, ex.Message.ToString());
            }

            populateGvAdvisor();
        }
        //protected void btnSave2_Click(object sender, EventArgs e)
        //{
        //    string strCustomer = txtCustomer.Text;
        //   string strPhoneNum =  txtPhoneNumber.Text;
        //    string strAddress = txtAddress.Text;
        //    int intActive = (cbActive.Checked ? 1 : 0);
        //            CRUD myCrud = new CRUD();
        //    string mySql = @"insert into Customer (Customer,PhoneNumber,Address,IsActive) 
        //                            values (@Customer,@PhoneNumber,@Address,@isActive)";
        //    Dictionary<string, object> myPara = new Dictionary<string, object>();
        //    myPara.Add("@customer", strCustomer);
        //    myPara.Add("@phoneNumber",strPhoneNum);
        //    myPara.Add("@Address", strAddress);
        //    myPara.Add("@isActive", intActive);
        //    int rtn = myCrud.InsertUpdateDelete(mySql, myPara);
        //    confirmCrudOutput(rtn);
        //}
        protected void confirmCrudOutput (int rtn)
        {
            ////if (rtn >= 1)
            ////{
            ////    lblMessage.Text = "Success";
            ////    lblMessage.ForeColor = System.Drawing.Color.Green;
            ////}
            ////else
            ////{
            ////    lblMessage.Text = "Failed";
            ////    lblMessage.ForeColor = System.Drawing.Color.Red;
            ////}
        }
        protected void gvChangeColor()
        {
            foreach (GridViewRow row in gvInstitutionAdvisor.Rows)
            {
                if (row.RowIndex == gvInstitutionAdvisor.SelectedIndex)
               {
                   row.BackColor = System.Drawing.Color.Green;  // not applicable
                }
                else
                {
                    row.BackColor = System.Drawing.Color.White;//ColorTranslator.FromHtml("#FFFFFF");
                }
            }
        }

        protected void btnShowAllInterns_Click(object sender, EventArgs e)
        {

        }
        //protected void btnUpdate2_Click(object sender, EventArgs e)
        //{
        //    string strCustomer = txtCustomer.Text;
        //    string strPhoneNum = txtPhoneNumber.Text;
        //    string strAddress = txtAddress.Text;
        //    int intActive = (cbActive.Checked ? 1 : 0);
        //    CRUD myCrud = new CRUD();
        //    string mySql = @"UPDATE dbo.Customer
        //                    SET customer = @customer,PhoneNumber =@PhoneNumber,Address = @Address,isActive =@isActive
        //                    where customerId = @customerId";
        //    Dictionary<string, object> myPara = new Dictionary<string, object>();
        //    myPara.Add("@customer", strCustomer);
        //    myPara.Add("@phoneNumber", strPhoneNum);
        //    myPara.Add("@Address", strAddress);
        //    myPara.Add("@isActive", intActive);
        //    int rtn = myCrud.InsertUpdateDelete(mySql, myPara);
        //    App_Code.common.PostMsg(lblMessage, rtn);
        //}

        ////protected void btnPopulateInputForm_Click(object sender, EventArgs e)
        ////{
        ////    // this is how you access the gv values through btn > grow > then to find the control 
        ////    // gvDepartments.BackColor = System.Drawing.Color.White; //  
        ////    try
        ////    {
        ////        gvChangeColor();
        ////        ClearControls();
        ////        Button btn = sender as Button;
        ////        GridViewRow grow = btn.NamingContainer as GridViewRow;  // ref to the gv row 
        ////        hidCustomerID.Value = (grow.FindControl("lblCustomerID") as Label).Text;
        ////        txtCustomer.Text = (grow.FindControl("lblCustomer") as Label).Text;
        ////        txtPhoneNumber.Text = (grow.FindControl("lblPhoneNumber") as Label).Text;
        ////        txtAddress.Text = (grow.FindControl("lblAddress") as Label).Text;
        ////        bool ActiveValue = (grow.FindControl("cbActive") as CheckBox).Checked;//this is how to capture checkbox from a gridview
        ////        cbActive.Checked = ActiveValue; //(ActiveValue == false? false: true);
        ////        btnSave.Visible = false;
        ////        btnUpdate.Visible = true;
        ////        grow.BackColor = System.Drawing.Color.Yellow; //     
        ////    }
        ////    catch (Exception ex)
        ////    {
        ////        throw ex;
        ////    }
        ////}
        ///
        // added for gv sort and paging
        /// <summary>
        /// Jan 26,2012
        ///  this method can be applied to any GV, No changes is needed. copy and past as is
        /// </summary>
        #region  GV Paging  GV_sort event and Sort Direction
        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            // for paging the gridview
            gvInstitutionAdvisor.PageIndex = e.NewPageIndex;
            populateGvAdvisor();
        }
        protected void GvEvent_Sorting(object sender, GridViewSortEventArgs e)
        {
               try
            {
                //Retrieve the table from the session object.
                DataTable dt = Session["myDataTable"] as DataTable;
                if (dt != null)
                {
                    //Sort the data.
                    dt.DefaultView.Sort = e.SortExpression + " " + GetSortDirection(e.SortExpression);
                    //GVObligationEvent.DataSource = Session["ObligationDs"];
                    //GVObligationEvent.DataBind();
                    GridView Gv = (GridView)sender;// get the name of the gv object, then bind the session to it
                    Gv.DataSource = Session["myDataTable"];
                    Gv.DataBind();
                }
            }
            catch (Exception ex)
            {
                common.PostMsg(lblOutput, ex.Message.ToString(), "red");
            }
            showHideButtons();
        }
        private string GetSortDirection(string column)
        {
            // By default, set the sort direction to ascending.
            string sortDirection = "ASC";
            // Retrieve the last column that was sorted.
            string sortExpression = ViewState["SortExpression"] as string;
            if (sortExpression != null)
            {
                // Check if the same column is being sorted.
                // Otherwise, the default value can be returned.
                if (sortExpression == column)
                {
                    string lastDirection = ViewState["SortDirection"] as string;
                    if ((lastDirection != null) && (lastDirection == "ASC"))
                    {
                        sortDirection = "DESC";
                    }
                }
            }
            // Save new values in ViewState.
            ViewState["SortDirection"] = sortDirection;
            ViewState["SortExpression"] = column;
            return sortDirection;
        }

        protected void ddlinstitutionId_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblOutput.Text = "";
        }
        #endregion
        //  #region Export to Excel, Word,Pdf, and Csv
        //  /// <summary>
        //  /// using iTextSharp.text; 
        //  ///using iTextSharp.text.pdf;
        //  ///using iTextSharp.text.xml;
        //  ///using iTextSharp.text.html;
        //  ///using iTextSharp.text.html.simpleparser;
        //  ///using System.Collections;
        //  ///using System.Text;
        //  ///using System.util;
        //  ///using System.Text.RegularExpressions;
        //  /// </summary>
        //  protected void btnExportToExcel_Click(object sender, EventArgs e)
        //  {
        //      // ................. 
        //      //Button myBtnExportToExcel = btnExportToExcel;
        //      //myBtnExportToExcel.Attributes.Add("onclick", "javascript:return confirm('Are you sure you want to Export?')");
        //      managerExport mgrExport = new managerExport();
        //      mgrExport.btnExportToExcel(gvInstitutionAdvisor, this);
        //      mgrExport = null;
        //  }
        //  /// <summary>
        //  /// http://www.aspdotnet-suresh.com/2011/04/how-to-export-gridview-data-to-excel-or.html
        //  /// If you observe above code I added one function that is VerifyRenderingInServerForm 
        //  /// this function is used to avoid the error like “control must be placed in inside of
        //  /// form tag”. If we set VerifyRenderingInServerForm function then compiler will think 
        //  /// that controls rendered before exporting and our functionality will work perfectly.
        //  /// Here I used basic code to export gridview data to word document and for excel code is
        //  /// different but we can use the same code (Export to Word) for excel also to import gridview 
        //  /// data just by replacing Customers.doc to Customers.xls and application/ms-word to 
        //  /// application/ms-excel but here we have problem that is row background color is applied
        //  /// throughout excel for that reason I made some small code modification and applied color
        //  /// only to the particular columns based on rows
        //  /// </summary>
        //  /// System.Web.UI.Control control
        //  public override void VerifyRenderingInServerForm(Control control)
        //  {
        //      /* Verifies that the control is rendered */
        //      /* This event is used to export gridview data to word document*/
        //      //confirms that an HtmlForm control is rendered for the
        //      //specified ASP.NET server control at run time.
        //  }
        //  protected void btnExportToWord_Click(object sender, EventArgs e)
        //  {
        //      managerExport ObjbtnExportToWord = new managerExport();
        //      ObjbtnExportToWord.btnExportToWord(gvInstitutionAdvisor, this);
        //      ObjbtnExportToWord = null;
        //  }
        //  protected void ImageBtnExportToExcel_Click(object sender, ImageClickEventArgs e)
        //  {
        //      managerExport ObjbtnExportToWord = new managerExport();
        //      ObjbtnExportToWord.ImageBtnExportToExcel(gvInstitutionAdvisor, this);
        //      ObjbtnExportToWord = null;
        //  }
        //  protected void ImageBtnExportToWord_Click(object sender, ImageClickEventArgs e)
        //  {
        //      managerExport ObjbtnExportToWord = new managerExport();
        //      ObjbtnExportToWord.ImageBtnExportToWord(gvInstitutionAdvisor, this);
        //      ObjbtnExportToWord = null;
        //  }
        //  protected void ImageBtnExportToPdf_Click(object sender, ImageClickEventArgs e)
        //  { //http://www.aspdotnet-suresh.com/2011/04/how-to-export-gridview-data-to-pdf.html

        //      managerExport ObjbtnExportToWord = new managerExport();
        //      ObjbtnExportToWord.ImageBtnExportToPdf(gvInstitutionAdvisor, this);
        //      ObjbtnExportToWord = null;
        //  }
        //  protected void ImageBtnExportToCsv_Click(object sender, ImageClickEventArgs e)
        //  {
        //      managerExport ObjbtnExportToWord = new managerExport();
        //      ObjbtnExportToWord.ImageBtnExportToCsv(gvInstitutionAdvisor, this);  //.ImageBtnExportToCsv(gvInstitutionAdvisor, this);
        //      ObjbtnExportToWord = null;
        //  }
        //  //private void ExportToPdf2()
        //  //{
        //  //    //Get the HTML from gvInstitutionAdvisor 
        //  //    StringWriter sw = new StringWriter();
        //  //    HtmlTextWriter htw = new HtmlTextWriter(sw);
        //  //    gvInstitutionAdvisor.RenderControl(htw);
        //  //    string html = "&lt;html><body>" + sw.ToString() + "&lt;/body></html>";

        //  //    //Set up the response 
        //  //    Response.Clear();
        //  //    Response.ContentType = "application/pdf";

        //  //    //Create pdf document 
        //  //    Document document = new Document(PageSize.A4, 80, 50, 30, 65);

        //  //    //Create pdf writer, output directly to OutputStream 
        //  //    PdfWriter writer = PdfWriter.GetInstance(document, Response.OutputStream);
        //  //    document.Open();
        //  //    //Create tempfile to hold the HTML: 
        //  //    string tempFile = Path.GetTempFileName();
        //  //    using (StreamWriter tempwriter = new StreamWriter(tempFile, false))
        //  //    {
        //  //        tempwriter.Write(html);
        //  //    }

        //  //    //Parse the HTML into the document 
        //  //    //*****  HtmlParser.Parse(document, tempFile); // to do... fix it ali

        //  //    //Cleanup 
        //  //    document.Close();
        //  //    writer.Close();

        //  //    //Delete the tempfile: 
        //  //    File.Delete(tempFile);

        //  //    writer = null;
        //  //    document = null;
        //  //    Response.End();

        //  //}

        //  #region Export Text to PDF
        //  /// <summary>
        //  /// Create anykind of text and pass it as HTMLCode so whatever is the content will create a pdf
        //  /// file in the directory specified, you can also send the pdf via email
        //  /// to do: convert DS to a html file then call the function
        //  /// </summary>

        //  protected void btnExportTextToPDF_Click(object sender, EventArgs e)
        //  {
        //      // todo 
        //      /// need to find out how to export a text to a pdf , text can be created from data
        //      /// extraction and formated
        //      string HTMLCode = "~\\docExported\\HTML-To-PDF-using-iTextSharp.aspx";
        //      ConvertHTMLToPDF(HTMLCode);
        //  }
        //  protected void ConvertHTMLToPDF(string HTMLCode)
        //  {
        //      HttpContext context = HttpContext.Current;
        //      //Render PlaceHolder to temporary stream          
        //      System.IO.StringWriter stringWrite = new StringWriter();
        //      System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
        //      StringReader reader = new StringReader(HTMLCode);
        //      //Create PDF document          
        //      Document doc = new Document(PageSize.A4);
        //      HTMLWorker parser = new HTMLWorker(doc);
        //      //  PdfWriter.GetInstance(doc, new FileStream(Server.MapPath("~") + "/App_Data/HTMLToPDF.pdf",    FileMode.Create));   
        //      PdfWriter.GetInstance(doc, new FileStream(Server.MapPath("~") + "/docExported/HTMLToPDF.pdf", FileMode.Create));
        //      doc.Open();
        //      /********************************************************************************/
        //      var interfaceProps = new Dictionary<string, Object>();
        //      var ih = new ImageHander() { BaseUri = Request.Url.ToString() };
        //      interfaceProps.Add(HTMLWorker.IMG_PROVIDER, ih);
        //      foreach (IElement element in HTMLWorker.ParseToList(
        //      new StringReader(HTMLCode), null))
        //      {
        //          doc.Add(element);
        //      }
        //      doc.Close();
        //      Response.End();
        //      /********************************************************************************/
        //  }
        //  ////handle Image relative and absolute URL's    
        //  public class ImageHander : IImageProvider
        //  {
        //      public string BaseUri;
        //      public iTextSharp.text.Image GetImage(string src, IDictionary<string, string> h, ChainedProperties cprops, IDocListener doc)
        //      {
        //          string imgPath = string.Empty;
        //          if (src.ToLower().Contains("http://") == false)
        //          {
        //              imgPath = HttpContext.Current.Request.Url.Scheme + "://" +
        //                  HttpContext.Current.Request.Url.Authority + src;
        //          }
        //          else
        //          {
        //              imgPath = src;
        //          }
        //          return iTextSharp.text.Image.GetInstance(imgPath);
        //      }
        //  }
        //#endregion
        //   #endregion

    } // cls
} // NS