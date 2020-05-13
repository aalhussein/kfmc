using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using kfmc.App_Code;
using System.Web.Security;

namespace kfmc
{
    public partial class internAttendance : System.Web.UI.Page
    {
        int intInternId = 0;
        string selectedCourses = "";
        int totalSelectedItems = 0;
        string employeeSelection = "";
        int counter = 0;
        public List<string> empList = new List<string>();
        public SqlDataReader myDr;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                populatetrainingYearDropList();
                populatetrainingTermDropList();
            }

            if (intInternId > 0)
            {
                viewDetailInternAttendance();
            }

            if (Roles.IsUserInRole(User.Identity.Name, "admin") || Roles.IsUserInRole(User.Identity.Name, "supervisor"))
            {
                lblSecurity.Visible = false;
                // common.showHideControl(gvInstitutionAdvisor, true);
                Filter.Visible = true;
            }
            else
            {
                lblSecurity.Visible = true;
                securityContent.Visible = false;
            }
        }
        protected void Page_UnLoad(object sender, EventArgs e)
        {
            // code to be executed on user leaves the page
            // to avoid max connection pool exceeded
            CRUD.clearAllPools();
        }
        protected void populatetrainingTermDropList()
        {
            //ddlDegreeMajor.Items.Clear();
            //ddlDegreeMajor.Items.Add("Select Major");
            CRUD myCrud = new CRUD();
            string sqlTrainingTerm = @" select trainingTermId, trainingTerm from trainingTerm";
            myCrud.populatComboViaDr(sqlTrainingTerm, ddlTrainingTermFilter, "trainingTermId", "trainingTerm");
        }
        protected void populatetrainingYearDropList()
        {
            //ddlDegreeMajor.Items.Clear();
            //ddlDegreeMajor.Items.Add("Select Major");
            CRUD myCrud = new CRUD();
            string sqlTrainingYear = @"select trainingYearId, trainingYear from trainingYear";
            myCrud.populatComboViaDr(sqlTrainingYear, ddlTrainingYearFilter, "trainingYearId", "trainingYear");
        }
        protected void callMethodFromGUI(object sender, EventArgs e)  // move it to common 
        {//invoke method call from GUI
            if (IsPostBack)
            {
                int intInternId = int.Parse((sender as LinkButton).CommandArgument);
                //lblOutput.Text = intInternId.ToString();
                //Response.Write("learn new stuff : " + intInternId);

                // similar code this is shorter 
                string cmd = (sender as LinkButton).CommandName;
                if (cmd == "select")
                    lblOutput.Text = " you choose select ";
               else if (cmd == "update")
                    lblOutput.Text = " you choose update";

                //LinkButton myLb = (LinkButton)sender;
                //string cmd2 =  myLb.CommandName;
            }
        }
        protected void viewDetailInternAttendance()
        {
         //   Response.Write(intInternId);
        }
        protected void InternDetail(object sender, EventArgs e)
        {
            // when reference linkButton inside Gv
            ////string passValue = "";
            ////LinkButton lb = sender as LinkButton;
            ////passValue += lb.CommandName + " " ;
            ////passValue += lb.CommandArgument  + "" ;
            ////passValue += lb.ID;
            ////Response.Write(passValue);

            // to do must count the number of attendance per student

            // when reference outside GV
            //LinkButton myLinkButton = LbnInternDetail;
            //string x = myLinkButton.CommandArgument;
            //myLinkButton.CommandArgument = txtInternId.Text;
            //myLinkButton.PostBackUrl = "internAttendance.aspx";
        }
        protected void populateGvInternAttendanceLog()
        {
            // all code to get employee data from database in one method into drop down list
            CRUD myCrud = new CRUD();
            string mySql = @"select i.internid,fName + ' ' +  mi + ' '+  lname  as internName , workDate
                                from intern i inner join internAttendance ia on i.internid=ia.internid
                                where internstatusid=2  
                                order by internid";  // show only accepted interns
            using (DataTable dt = myCrud.getDT(mySql))
            {
                Session["myDataTable"] = dt;
                gvInternAttendanceLog.DataSource = Session["myDataTable"];
                gvInternAttendanceLog.DataBind();
            }
        }
        //protected void populateCBLEmployee()
        //{
        //    lblOutput.Text = "";
        //    // all code to get employee data from database in one method into radio botton list
        //    CRUD myCrud = new CRUD();
        //    string mySql = @"SELECT empid,employeeEn 
        //                    FROM employeeIt
        //                    where adminId = @adminId and depId = @depId";
        //    Dictionary<string, object> myPara = new Dictionary<string, object>();
        //    int myAdminId = int.Parse(ddlAdmin.SelectedValue);
        //    //.. int myDepId = (string.IsNullOrEmpty(ddlDep.SelectedValue.ToString())) ? 0 : int.Parse(ddlDep.SelectedValue);
        //    int myDepId = (int.Parse(ddlDep.SelectedValue) == 0) ? 0 : int.Parse(ddlDep.SelectedValue);
        //    if (myDepId == 0)
        //    {
        //        lblOutput.Text = "Shows employees that are not assiged to a department!";
        //        myPara.Add("@adminId", myAdminId);
        //        myPara.Add("@depId", myDepId);
        //        using (SqlDataReader dr = myCrud.getDrPassSql(mySql, myPara))
        //        {
        //            cblIntern.DataValueField = "empid";
        //            cblIntern.DataTextField = "employeeEn";
        //            cblIntern.DataSource = dr;
        //            cblIntern.DataBind();
        //        }
        //    }
        //}
        protected void populateCBLActivityList()
        {
          //  // all code to get employee data from database in one method into radio botton list
          ////  int intActivityTypeId = int.Parse(ddlActivityType.SelectedValue);
          //  CRUD myCrud = new CRUD();
          //  string mySql = @"select activityId, activityName from activity
          //                  where activityTypeId = @activityTypeId";
          //  Dictionary<string, object> myPara = new Dictionary<string, object>();
          //  myPara.Add("@activityTypeId", intActivityTypeId);
          //  using (SqlDataReader dr = myCrud.getDrPassSql(mySql, myPara))
          //  {
          //      cblActivity.DataValueField = "activityId";
          //      cblActivity.DataTextField = "activityName";
          //      cblActivity.DataSource = dr;
          //      cblActivity.DataBind();
          //  }
          }
        protected void btnRegister_Click(object sender, EventArgs e)
        {
            List<int> mySelectedIntern = new List<int>();
            //foreach (ListItem item in cblIntern.Items)
            //{
            //    if (item.Selected)
            //    {
            //        mySelectedIntern.Add(cblIntern.Items[i].Selected);
            //    }
            //}
            for (int i = 0; i < cblIntern.Items.Count; i++)
            {
                if (cblIntern.Items[i].Selected)
                {
                    mySelectedIntern.Add(int.Parse(cblIntern.Items[i].Value));
                }
            }

            foreach (int myInternId in mySelectedIntern)
            {
                registerIntern( myInternId);
            }
            populateGvInternAttendanceLog();
        }
        protected void registerIntern(int myInternId)
        {
            //selectedCourses += myEmployeeId + " " + myCourseName;
            //lblOutput.Text = selectedCourses;
            string mySql = @"  insert into internAttendance(internId)
                                  values (@internId)";
            CRUD myCrud = new CRUD();
            Dictionary<string, object> myPara = new Dictionary<string, object>();
            myPara.Add("@internId", myInternId);
       
          int rtn =  myCrud.InsertUpdateDelete(mySql, myPara);
            App_Code.common.PostMsg(lblOutput, rtn);
            //if (rtn >= 1)
            //{
            //    lblOutput.Text = "Sucess";
            //}
            //else
            //{
            //    lblOutput.Text = "Failed";
            //}
        }
        protected int checkIfDepHasEmployee(int mySelectedAdminId, int mySelectedDepId)
        {
            // check if depid has value >=1
            string mySql2 = @"SELECT count(*)
                            FROM employeeIt where  administrationId = @administrationId and departmentid = @departmentid";
            Dictionary<string, object> myPara1 = new Dictionary<string, object>();
            myPara1.Add("@administrationId", mySelectedAdminId);
            myPara1.Add("@departmentid", mySelectedDepId);
            CRUD myCrud1 = new CRUD();
             int rtn = myCrud1.InsertUpdateDelete(mySql2, myPara1);
            return rtn;
        }
        protected void populateEmployeeCheckBoxList2(string mySql, Dictionary<string,object> myPara)
        {
            CRUD myCrud = new CRUD();
            using (SqlDataReader dr = myCrud.getDrPassSql(mySql, myPara))
            {
                cblIntern.DataValueField = "employeeId";
                cblIntern.DataTextField = "employeeEn";
                cblIntern.DataSource = dr;
                cblIntern.DataBind();
            }
        }
        protected void populateInternCheckBoxList()
        {
            if (common.IsNullOrEmptyControlObj(ddlTrainingTermFilter, lblOutput, "Please select Training Term !")) return;  // added april 2020 ... short hand 
            if (common.IsNullOrEmptyControlObj(ddlTrainingYearFilter, lblOutput, "Please select Training Year !")) return;  // added april 2020 ... short hand 

            int trainingTerm =  int.Parse(ddlTrainingTermFilter.SelectedValue);
            int trainignYear = int.Parse(ddlTrainingYearFilter.SelectedValue);
            string mySql = @"select internid,fName + ' ' +  mi + ' '+  lname  as internName
                    from intern 
                    where internstatusid=2  and trainingTermid=@trainingTermId and 
                    trainingYearId = @trainingYearId order by fName";

            CRUD myCrud = new CRUD();
            Dictionary<string, object> myPara = new Dictionary<string, object>();
            myPara.Add("@trainingTermId", trainingTerm);
            myPara.Add("@trainingYearId", trainignYear);
            using (DataTable dt = myCrud.getDTPassSqlDic(mySql, myPara))
            {
                if (dt.Rows.Count>=1)
                {
                    lblOutput.Text = "";
                    cblIntern.DataValueField = "internId";
                    cblIntern.DataTextField = "InternName";
                    cblIntern.DataSource = dt;
                    cblIntern.DataBind();
                }
                else
                {
                    cblIntern.Items.Clear();
                    //cblIntern.DataSource = null;
                    //cblIntern.DataBind();
                    common.PostMsg(lblOutput, "No Data Found !", "red");
                }
                //for (int i = 0; i < dt.Rows.Count; i++)
                //{
                //    cblIntern.Items[i].Text  = @" <a href='internAttendance.aspx?internId=" + int.Parse(cblIntern.Items[i].Value) + 
                //         " target='_blank' title='View Document'>" + cblIntern.Items[i].Text + "</a>";
                //     //intInternId = int.Parse(cblIntern.Items[i].Value);
                //}

                // for (int i = 0; i < dt.Rows.Count; i++)
                // {
                //     LinkButton myLBtn = new LinkButton();
                //     myLBtn.ID = "lBtn_" + cblIntern.Items[i].Value;
                //     myLBtn.PostBackUrl = "internAttendance.aspx";
                //     myLBtn.Text= cblIntern.Items[i].Text;
                ////    cblIntern.Items[i].Text = @"" + int.Parse(cblIntern.Items[i].Value) + "" +  cblIntern.Items[i].Text;
                // }
                ////////////for (int i = 0; i < dt.Rows.Count; i++)
                ////////////{
                ////////////    LinkButton myLinkButton = LBtn;
                ////////////    txtInternId.Text = myLinkButton.CommandArgument;
                ////////////    myLinkButton.CommandArgument = txtInternId.Text ;
                ////////////    myLinkButton.PostBackUrl = "internAttendance.aspx";
                ////////////}
            }
            //Response.Write("selected Id 1 : " + intInternId);
            //lblOutput.Text = "selected Id  1: " + intInternId;
        }
        protected void btnShowInternAttendanceLog_Click(object sender, EventArgs e)
        {
            populateGvInternAttendanceLog();
        }
        protected void cblIntern_SelectedIndexChanged(object sender, EventArgs e)
        {
            Response.Write("selected Id 2 : " + intInternId);
            lblOutput.Text = "selected Id 2 : " + intInternId;
        }
        protected void Lbtn_Click(object sender, EventArgs e) // lai this is demo on how to call method from gui
        {
            Response.Write("hello from LinkButton");
            LinkButton myLinkButton = LBtn;
            string x = myLinkButton.CommandArgument;
         //   myLinkButton.CommandArgument = txtInternId.Text;
            myLinkButton.PostBackUrl = "internAttendance.aspx";

            lblOutput.Text = "pass Id via Link Button :  " + x;
            Response.Write("hello ali");
            Response.Write("<br/> " + x);
            Response.Write("<br/> " + (string.IsNullOrEmpty(myLinkButton.CommandArgument) ? "Empty" : myLinkButton.CommandArgument));

        }
        // added for gv sort and paging
        /// <summary>
        /// Jan 26,2012
        ///  this method can be applied to any GV, No changes is needed. copy and past as is
        /// </summary>
        #region  GV Paging  GV_sort event and Sort Direction
        protected void gvInternAttendanceLog_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            // for paging the gridview
            gvInternAttendanceLog.PageIndex = e.NewPageIndex;
            viewDetailInternAttendance();
        }
        protected void gvInternAttendanceLog_Sorting(object sender, GridViewSortEventArgs e)
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
           //showHideButtons();
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
        protected void btnGetData_Click(object sender, EventArgs e)
        {
            populateInternCheckBoxList();
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
    }
}