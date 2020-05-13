using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Globalization;
using System.Data;
using System.Web.Configuration;
using System.IO;
using System.Web.Security;
using AjaxControlToolkit;

namespace kfmc.office
{
    public partial class docMgt : System.Web.UI.Page
    {
        protected static int StaticIntTaskActId = 0;
       // string constr = WebConfigurationManager.ConnectionStrings["conStrtaskDb"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            embedDateTimeVal(TxtCalenderAction);
            if (!IsPostBack)
            {
                populateDocTypeCombo();
              //  populateGvDocs();  // restore for development
                                   //populateTaskActivityDocGv();
                if (Roles.IsUserInRole(User.Identity.Name, "admin") || Roles.IsUserInRole(User.Identity.Name, "supervisor"))
                {
                    searchDocs.Visible = true;
                }
                else
                {
                    searchDocs.Visible = false;
                }
            }
            //grantPermission();
            App_Code.common.grantPermission(btnShowDocs);
            App_Code.common.grantPermission(btnSearch);
        }
        /// <summary>
        /// By default, the date format for SQL server is in U.S. date format MM/DD/YY, unless a localized version of SQL Server has been 
        /// installed
        /// https://support.microsoft.com/en-us/help/173907/inf-how-to-set-the-day-month-year-date-format-in-sql-server
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        //protected void grantPermission()
        //{
        //    if (Roles.IsUserInRole("admin") || Roles.IsUserInRole("supervisor"))
        //    {
        //        btnShowDocs.Visible = true;
        //        btnSearch.Visible = true;
        //    }

        //}
        protected void ClearControls()
        {
            TxtCalenderAction.Text = "";
            txtnote.Text = "";
        }
        protected void CustomValidator1_ServerValidate(object sender, ServerValidateEventArgs e)
        {
            DateTime d;
            e.IsValid = DateTime.TryParseExact(e.Value, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out d);
        }
        protected void populateDocTypeCombo()
        {
            ddlDocType.Items.Clear();
            ddlDocType.Items.Add("Select Document Type ");
            CRUD myCrud = new CRUD();
            string mySql = @" Select docTypeId, docType from docType ";
            myCrud.populateCombo(ddlDocType, mySql, "docTypeId", "docType");
        }

        protected void populateGvDocs()
        {
            CRUD myCrud = new CRUD();
            string mySql = @"select oad.OfficeArchiveDocsid,folderName,fileName,dockey, docType,docName,uploadDate,note --,docData
                                    from officeArchive oa inner join officeArchiveDocs oad  on oa.OfficeArchiveId= oad.OfficeArchiveId
                                    inner join docType dt on oa.docTypeId = dt.docTypeId ";
            myCrud.populateGv(gvDocs, mySql);
        }
        protected void populateGvDocs2()
        {
            // this is old way to populate gv
            CRUD myCrud = new CRUD();
            string MeetingSeltCom = @"select oad.OfficeArchiveDocsid,folderName,fileName,dockey, docType,docName,uploadDate,note --,docData
                                    from officeArchive oa inner join officeArchiveDocs oad  on oa.OfficeArchiveId= oad.OfficeArchiveId
                                    inner join docType dt on oa.docTypeId = dt.docTypeId ";
            using (SqlDataReader dr = myCrud.getDrPassSql(MeetingSeltCom)) // come back for sorting and paging 
            {
                gvDocs.DataSource = dr;
                gvDocs.DataBind();
            }
        }
        protected void embedDateTimeVal(TextBox txtBox)
        {
            ControlCollection cc = txtBox.Parent.Controls;
            string[] spl = txtBox.ClientID.Split(new char[] { '_' });
            CalendarExtender ce = new CalendarExtender();
            ce.ID = txtBox.ID + "_ce";
            ce.TargetControlID = spl[spl.Length - 1];
            cc.Add(ce);
        }
        protected void DownloadFile(object sender, EventArgs e)  // move it to common 
        {// move it to common
            int OfficeArchiveDocsid = int.Parse((sender as LinkButton).CommandArgument);
            byte[] bytes;
            string fileName, contentType;
            string constr = CRUD.conStr; //WebConfigurationManager.ConnectionStrings["conStrtaskDb"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = @"  select  docName,ContentType,docData from OfficeArchiveDocs
                                      where OfficeArchiveDocsid = @OfficeArchiveDocsid";
                    cmd.Parameters.AddWithValue("@OfficeArchiveDocsid", OfficeArchiveDocsid);
                    cmd.Connection = con;
                    con.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        if (sdr.HasRows)
                        {
                            sdr.Read();
                            bytes = (byte[])sdr["DocData"];
                            contentType = sdr["ContentType"].ToString();
                            fileName = sdr["docName"].ToString();   //fileName changed to docName
                        }
                        else
                        {
                            lblOutput.Text = "File not found!";
                            lblOutput.ForeColor = System.Drawing.Color.Red;
                            lblOutput.BackColor = System.Drawing.Color.Yellow;
                            return;
                        }
                    }
                    con.Close();
                }
            }
            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = contentType;
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + fileName);
            Response.BinaryWrite(bytes);
            Response.Flush();
            Response.End();
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string myDocKey = txtKeySearch.Text;
            string myFolderName = txtFolderNameSearch.Text;
            CRUD myCrud = new CRUD();
            Dictionary<string, Object> myPara = new Dictionary<string, object>();
            string mySql = @"select oad.OfficeArchiveDocsid,folderName,fileName,dockey, docType,docName,uploadDate,note --,docData
                            from officeArchive oa inner join officeArchiveDocs oad  on oa.OfficeArchiveId= oad.OfficeArchiveId
                            inner join docType dt on oa.docTypeId = dt.docTypeId ";
            if (myDocKey.Length >= 1 && myFolderName.Length >= 1)
            {
                mySql += " where dockey  like '%'+@docKey+'%' or  folderName  like'%'+@folderName+'%' ";  //'%driver%' ";
                myPara.Add("@docKey", myDocKey);
                myPara.Add("@folderName", myFolderName);
            }
            else if (myDocKey.Length >= 1 && string.IsNullOrEmpty(myFolderName))
            {
                mySql += " where dockey  like '%'+@docKey+'%' ";
                myPara.Add("@docKey", myDocKey);
            }
            else if (myFolderName.Length >= 1 && string.IsNullOrEmpty(myDocKey))
            {
                mySql += " where folderName  like '%'+@folderName+'%' ";
                myPara.Add("@folderName", myFolderName);
            }
                         //   where dockey  like '%'+@docKey+'%' or  folderName  = @folderName ";  //'%driver%'
           

            using (SqlDataReader dr = myCrud.getDrPassSql(mySql, myPara))  //   p_SearchDoc   p_getData
            {
                gvDocs.DataSource = dr;
                gvDocs.DataBind();
            }
        }

        protected void Lb_Click(object sender, EventArgs e)
        {
            CRUD myCrud = new CRUD();
            string mySql = @"  INSERT INTO docType(docType)VALUES (@docType)";
            Dictionary<string, object> myPara = new Dictionary<string, object>();
            myPara.Add("@docType", txtDocType.Text);
            int rtn = myCrud.InsertUpdateDelete(mySql, myPara);
            lblOutput.Text = rtn >= 1 ? "Success" : "Failed";
            lblOutput.ForeColor = rtn >= 1 ? System.Drawing.Color.Green : System.Drawing.Color.Red;
            populateDocTypeCombo();
        }

        #region  GV Paging
        protected void GvEmployee_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            // for paging the gridview
            gvDocs.PageIndex = e.NewPageIndex;
            populateGvDocs();
        }
        #endregion
        #region  GV_sort event and Sort Direction
        protected void GvEvent_Sorting(object sender, GridViewSortEventArgs e)
        {

            //Retrieve the table from the session object.
            using (DataTable dt = Session["Employeedt"] as DataTable)
            {
                if (dt != null)
                {
                    //Sort the data.
                    dt.DefaultView.Sort = e.SortExpression + " " + GetSortDirection(e.SortExpression);
                    //GVObligationEvent.DataSource = Session["ObligationDs"];
                    //GVObligationEvent.DataBind();
                    GridView Gv = (GridView)sender;// get the name of the gv object, then bind the session to it
                    Gv.DataSource = Session["Employeedt"];
                    Gv.DataBind();
                }
            }
        }
        /// <summary>
        /// Jan 26,2012
        ///  this method can be applied to any GV, No changes is needed. copy and past as is
        /// </summary>
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
        #endregion
        protected void gvDocs_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            // for paging the gridview
            gvDocs.PageIndex = e.NewPageIndex;
            populateGvDocs();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {

            // new validation
            if (String.IsNullOrEmpty(txtFolderName.Text))
            {
                lblOutput.Text = "Please fill Folder Name !";
                lblOutput.ForeColor = System.Drawing.Color.Red;
                txtFolderName.Focus();
                return;
            }

            if (String.IsNullOrEmpty(txtFileName.Text))
            {
                lblOutput.Text = "Please fill File Name !";
                lblOutput.ForeColor = System.Drawing.Color.Red;
                txtFileName.Focus();
                return;
            }

            if (String.IsNullOrEmpty(ddlDocType.SelectedValue= "1"))
            {
                lblOutput.Text = "Please fill  Doc Type !";
                lblOutput.ForeColor = System.Drawing.Color.Red;
                ddlDocType.Focus();
                return;
            }



            // old validation
            int rtn = 0;
            // //Input Validations
            if (string.IsNullOrWhiteSpace(TxtCalenderAction.Text))
            {
                ADateMsg.Visible = true;
                if (string.IsNullOrWhiteSpace(txtnote.Text))
                {
                    ActionMsg.Visible = true;
                    
                }
            }
            else
            {
                string mySql = @"insert into OfficeArchive (docTypeId, folderName,fileName,dockey,uploadDate,note )
                                     values (@docTypeId, @folderName,@fileName,@dockey,@uploadDate,@note)" +
                                         "SELECT CAST(scope_identity() AS int)";
                Dictionary<string, Object> InsertParameters = new Dictionary<string, object>();
                InsertParameters.Add("@docTypeId", ddlDocType.SelectedValue);
                InsertParameters.Add("@folderName", txtFolderName.Text);
                InsertParameters.Add("@fileName", txtFileName.Text);
                InsertParameters.Add("@dockey", txtDocKey.Text);
                InsertParameters.Add("@uploadDate", TxtCalenderAction.Text);
                InsertParameters.Add("@note", txtnote.Text);
                CRUD myCrud = new CRUD();
                int OfficeArchiveId = myCrud.InsertUpdateDeleteViaSqlDicRtnIdentity(mySql, InsertParameters);
                if (OfficeArchiveId >= 1)
                {
                    if (FileUpload.HasFiles)
                    {
                        rtn = InsertDocuments(OfficeArchiveId);
                        lblOutput.Text = rtn >= 1 ? "Success" : "Failed";
                        lblOutput.ForeColor = rtn >= 1 ? System.Drawing.Color.Green : System.Drawing.Color.Red;
                        lblOutput.BackColor = System.Drawing.Color.Yellow;
                    }
                }
                //.....  populateGvDocs(); // restore for development
                ClearControls();
                populateGvDocs();
            }

        }

        protected int InsertDocuments(int OfficeArchiveId)
        {
            int rtn = 0;
            foreach (HttpPostedFile postedFile in FileUpload.PostedFiles)
            {
                string filename = Path.GetFileName(postedFile.FileName);
                string contentType = postedFile.ContentType;
                using (Stream fs = postedFile.InputStream)
                {
                    using (BinaryReader br = new BinaryReader(fs))
                    {
                        byte[] bytes = br.ReadBytes((Int32)fs.Length);
                        CRUD DocInsert = new CRUD();
                        string strQuery = @"insert into OfficeArchiveDocs(OfficeArchiveId,DocName, ContentType, DocData)
                                    values (@OfficeArchiveId, @DocName, @ContentType, @DocData)";

                        Dictionary<string, Object> p = new Dictionary<string, object>();
                        //p.Add("@TaskId", "get the value ");
                        p.Add("@OfficeArchiveId", OfficeArchiveId);
                        p.Add("@DocName", filename);
                        p.Add("@ContentType", contentType);
                        p.Add("@DocData", bytes);
                        rtn = DocInsert.InsertUpdateDelete(strQuery, p);
                    }
                }
            }
            return rtn;
            //...populateGvDocs();  // restore for  development
        }

        protected void btnShowDocs_Click(object sender, EventArgs e)
        {
            populateGvDocs();
        }
    }
}