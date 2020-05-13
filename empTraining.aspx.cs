using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using AjaxControlToolkit;
using System.Globalization;
using System.Data;
using System.Web.Configuration;
using System.Configuration;
using System.IO;
using System.Drawing;
using System.Web.Security;
using kfmc.App_Code;

namespace kfmc.employee
{
    public partial class empTraining : System.Web.UI.Page
    {
        protected static int StaticIntInternId = 0;
        string constr = CRUD.conStr; // WebConfigurationManager.ConnectionStrings["conStrtaskDb"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            //..embedDateTimeVal(TxtCalenderAction);
            embedDateTimeVal(txtStartDate);
            embedDateTimeVal(txtEndDate);
            if (!IsPostBack)
            {
                populateDepDropList();
                if (Roles.IsUserInRole(User.Identity.Name, "admin") || Roles.IsUserInRole(User.Identity.Name, "supervisor"))
                {
                    btnShowAllInterns.Visible = true;
                    //  populateGvEmployeeDoc();
                    populateGvEmpDoc();
                }

                else
                {
                    btnShowAllInterns.Visible = false;
                }
            }
            App_Code.common.grantPermission(btnShowAllInterns);

        }
        /// <summary>
        /// By default, the date format for SQL server is in U.S. date format MM/DD/YY, unless a localized version of SQL Server has been 
        /// installed
        /// https://support.com/en-us/help/173907/inf-how-to-set-the-day-month-year-date-format-in-sql-server
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Insert(object sender, EventArgs e)
        {  // continue 
            Page.Validate();
            if (!Page.IsValid)
            {
                return;
            }
            if (String.IsNullOrEmpty(txtFName.Text) || (ddlhitDepartmentId.SelectedValue == "Select Department"))
            {
                lblOutput.Text = "Please fill the form !";
                lblOutput.ForeColor = System.Drawing.Color.Red;
                return;
            }
            else
            {
                int myhitEmployeeId = 0;
                string StrEmpEmail = (txtEmail.Text);
                int hitDepartmentId = int.Parse(ddlhitDepartmentId.SelectedValue);   // added Nov 2017
                string mySql = @"insert into hitemployee (fName,mi,lName,ext,hitDepartmentId,position,cell,
                        email,currentSkills,tgtSkills, courseName,startDate,endDate,Cost,location,companyName,companyPhone,companyEmail,
                        companyRep,active)  
                        values (@fName,@mi,@lName,@ext,@hitDepartmentId,@position,@cell,@email,@currentSkills,@tgtSkills, 
                        @courseName,@startDate,@endDate,@Cost,@location,@companyName,@companyPhone,@companyEmail,
                        @companyRep,@active)" +
                            "SELECT CAST(scope_identity() AS int)";

                Dictionary<string, Object> InsertParameters = new Dictionary<string, object>();
                InsertParameters.Add("@employeeId", (txthitemployeeId.Text));
                InsertParameters.Add("@fName", (txtFName.Text));
                InsertParameters.Add("@mi", (txtMi.Text));
                InsertParameters.Add("@lName", (txtlName.Text));
                InsertParameters.Add("@ext", (txtExt.Text));
                InsertParameters.Add("@hitDepartmentId", hitDepartmentId);  //DDLTaskName.SelectedValue
                InsertParameters.Add("@position", (txtPosition.Text));
                InsertParameters.Add("@cell", (txtCell.Text));
                InsertParameters.Add("@email", StrEmpEmail);
                InsertParameters.Add("@currentSkills", txtCurrentSkills.Text);
                InsertParameters.Add("@tgtSkills", txtTgtSkills.Text);
                InsertParameters.Add("@courseName", txtCourseName.Text);
                InsertParameters.Add("@startDate", txtStartDate.Text);
                InsertParameters.Add("@endDate", txtEndDate.Text);
                InsertParameters.Add("@cost", txtCost.Text);
                InsertParameters.Add("@location", txtLocation.Text);
                InsertParameters.Add("@companyName", txtCompanyName.Text);
                InsertParameters.Add("@companyPhone", txtCompanyPhone.Text);
                InsertParameters.Add("@companyEmail", txtCompanyEmail.Text);
                InsertParameters.Add("@companyRep", txtCompanyRep.Text);
                InsertParameters.Add("@Active", cbxActive.Checked);
                try
                {
                    CRUD myCrud = new CRUD();
                    myhitEmployeeId = myCrud.InsertUpdateDeleteViaSqlDicRtnIdentity(mySql, InsertParameters);
                    InsertDocuments(myhitEmployeeId);  // passing taskID as a return value from insert task
                    emailIntern(StrEmpEmail);
                    //.. populateGvEmployeeDoc(myhitEmployeeId); // use it for testing only
                    ClearControls();
                    lblOutput.Text = "Thank you for submitting your information!";
                    lblOutput.ForeColor = System.Drawing.Color.Green;
                }
                catch (Exception ex)
                {
                    lblOutput.Text = ex.ToString();
                }
            }
        }
        protected void InsertDocuments(int myhitEmployeeId)
        {
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
                        string mySql = @"insert into hitEmployeeDoc(hitEmployeeId,DocName,ContentType,DocData)
                                        values (@hitEmployeeId,@DocName,@ContentType,@DocData)";
                        Dictionary<string, object> myPara = new Dictionary<string, object>();
                        myPara.Add("@hitEmployeeId", myhitEmployeeId);
                        myPara.Add("@DocName", filename);
                        myPara.Add("@ContentType", contentType);
                        myPara.Add("@DocData", bytes);
                        int rtn = DocInsert.InsertUpdateDelete(mySql, myPara);
                        App_Code.common.PostMsg(lblOutput, rtn);
                    }
                }
            }
        }
        protected void emailIntern(string internEmail)
        {
        //    mailMgr myMailMgr = new mailMgr();
        //    myMailMgr.myTo = internEmail;
        //    myMailMgr.mySubject = "KFMC received document notification";
        //    myMailMgr.myBody = @"Dear applicant,
        //Thank  you for submitting your application. We will contact you once a decision is made!
        //Ali Hamidaddin ";
        //    myMailMgr.testEmailViaGmail();
        }
        protected void ClearControls()
        {
            //..  TxtCalenderAction.Text = "";
            txtStartDate.Text = "";
            txtEndDate.Text = "";
        }
        protected void CustomValidator1_ServerValidate(object sender, ServerValidateEventArgs e)
        {
            DateTime d;
            e.IsValid = DateTime.TryParseExact(e.Value, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out d);
        }
        protected void populateDepDropList()
        {
            ddlhitDepartmentId.Items.Clear();
            ddlhitDepartmentId.Items.Add("Select Department");
            CRUD myCrud = new CRUD();
            string mySql = (@" Select hitDepartmentId, department  from hitDepartment ");
            SqlDataReader dr = myCrud.getDrPassSql(mySql);
            ddlhitDepartmentId.DataTextField = "department";
            ddlhitDepartmentId.DataValueField = "hitDepartmentId";
            ddlhitDepartmentId.DataSource = dr;
            ddlhitDepartmentId.DataBind();
        }
        protected void populateGvEmployeeDoc()
        {
            // restore gui gv
            //CRUD myCrud = new CRUD();  //*i.institutionId*//
            //string MeetingSeltCom = @"select i.hitemployeeId,[fName],[mi],[lName],[ext],department,position,
            //                [cell],[email],i.[currentSkills],futureSkills,tgtCourses,[Date],[active] 
            //                from hitemployee i inner join hitdepartment dep on i.hitDepartmentId =dep.hitDepartmentId
            //                inner join hitemployeedoc idoc on i.hitemployeeId = idoc.hitemployeeId";
            //SqlDataReader dr = myCrud.getDrPassSql(MeetingSeltCom); //TaskSelect.getDrPassSql(MeetingSeltCom)
            //gvEmployeeDoc.DataSource = dr;
            //gvEmployeeDoc.DataBind();
        }
        protected void populateGvEmpDoc()  //int empId
        {
            CRUD TaskSelect = new CRUD();
            Dictionary<string, object> myPara = new Dictionary<string, object>();
            // myPara.Add("@internId", empId);
            string MeetingSeltCom = @"SELECT i.hitemployeeId, dep.department AS Department,i.fName, i.mi, i.lName, i.currentSkills, i.tgtSkills, 
                i.courseName,i.startDate, i.endDate, i.cost, i.location, i.companyName, i.companyPhone, i.companyEmail AS companyRep 
                FROM hitEmployee AS i INNER JOIN hitDepartment AS dep ON i.hitDepartmentId = dep.hitDepartmentId";
            //inner join hitemployeedoc idoc on i.hitemployeeId = idoc.hitemployeeId";
            SqlDataReader dr = TaskSelect.getDrPassSql(MeetingSeltCom, myPara); //TaskSelect.getDrPassSql(MeetingSeltCom)
            gvEmployeeDoc.DataSource = dr;
            gvEmployeeDoc.DataBind();
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
        protected void btnShowAllInterns_Click(object sender, EventArgs e)
        {
            populateGvEmployeeDoc();
        }
        protected void txtFName_Load(object sender, EventArgs e)
        {
            txtFName.Focus();
        }
        protected void btnAutoInsert_Click(object sender, EventArgs e)
        {
            ContentPlaceHolder cph = (ContentPlaceHolder)Master.FindControl("MainContent");
            crudAuto myCrudAuto = new crudAuto();
            myCrudAuto.insertAuto(cph);
        }

        //protected void grantPermission()
        //{
        //    //if (Roles.IsUserInRole("admin") || Roles.IsUserInRole("supervisor"))
        //    //{
        //    //    btnShowAllInterns.Visible = true;
        //    //}
        // }
    }
}