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

namespace kfmc.internship
{
    public partial class internProfile : System.Web.UI.Page
    {
        protected static int StaticIntInternId = 0;
        string constr = CRUD.conStr;// WebConfigurationManager.ConnectionStrings["conStrKfmc"].ConnectionString;
        public  string GstrTrainingYear = "";
        public  string GstrTrainingTerm = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            // if email exist, means need toupdate record, if email does not exits, it is a new record. now
            //btnSubmit.Visible = common.IsNullOrEmpty(txtEmail.Text) ? true : false;
            //btnUpdate.Visible = common.IsNullOrEmpty(txtEmail.Text) ? false : true;
            //btnSubmit.Visible = true;
            //btnUpdate.Visible = false;
            //  populateInternProfileForm(); //................
            //  lblOutput.Text = "Post passing intern id via static public class Person :  " + person.intPersonId.ToString();
            if (!IsPostBack)
            {
                clearInputform();
                populateInstitutionDropList();
                populateddlDegreeDropList();
                populateDegreeMajorDropList();
                populatetrainingYearDropList();
                populatetrainingTermDropList();
                populateInternStatusIdDropList();
                populateddlInternGroupDropList();
                populateddlOrganizationDepList();
                populateGenderDropList();
                //lblOutput.Text = "Enter your email and click get Profile!";
                //lblOutput.ForeColor = System.Drawing.Color.Red;
                // enable / disable controls based on roles
                if (Roles.IsUserInRole(User.Identity.Name, "admin") || Roles.IsUserInRole(User.Identity.Name, "supervisor"))
                {
                    ddInternStatus.Enabled = true;
                    ddlTrainingTerm.Enabled = true;
                    ddlTrainingYear.Enabled = true;
                    ddIAdministration.Enabled = true;
                    ddIInternGroup.Enabled = true;
                    txtInternGroup.Visible = true;
                    LbInternGroup.Visible = true;
                    txtInstitution.Visible = true;
                    LbAddInstitution.Visible = true;
                    btnShowAllInterns.Visible = true;
                    btnGetInternProfile.Visible = true; // added
                    lblEnterYourEmail.Visible = true;
                    txtEmailSearch.Visible = true;
                    btnGetInternProfile.Visible = true;
                    lblInternDoc.Visible = true;
                 //.. restoreme   populateGvIntern();
                    //btnSubmit.Visible = true;
                    //btnUpdate.Visible = true;
                }
                else
                {
                    ddInternStatus.Enabled = false;
                    btnShowAllInterns.Visible = false;
                }
                // fill form
                populateInternProfileForm();
            }
            if (!IsPostBack)
            {
                showHideSubmitUpdate();
            }

       }// end of method

        protected int  getMaxRefNo()
        {
            CRUD myCrud = new CRUD();
            return myCrud.getMaxRefNo("intern")+1;
        }
        protected void showHideSubmitUpdate()
        {
            // added today
            if (string.IsNullOrEmpty(txtEmail.Text))
            {
                btnSubmit.Visible = true;
                btnUpdate.Visible = false;
            }
            else
            {
                btnSubmit.Visible = false;
                btnUpdate.Visible = true;
            }
        }
        protected void showHideSubmitUpdate(string myAction)
        {
            switch (myAction)
            {
                case "insert":
                    btnSubmit.Visible = true;
                    btnUpdate.Visible = false;
                    break;
                case "edit":
                    btnSubmit.Visible = false;
                    btnUpdate.Visible = true;
                    break;
                 default:
                    btnSubmit.Visible = false;
                    btnUpdate.Visible = false;
                    break;
            }
        }
        protected void Page_UnLoad(object sender, EventArgs e)
        {
            // code to be executed on user leaves the page
            // to avoid max connection pool exceeded
            CRUD.clearAllPools();
            gvIntern.Visible = false;
            clearInputform(); // not working how to clear input form when leaving the page
            person.intPersonId = 0;  // reset static personid otherwise, internProfile will show all the time 
        }

        /// <summary>
        /// By default, the date format for SQL server is in U.S. date format MM/DD/YY, unless a localized version of SQL Server has been 
        /// installed
        /// https://support.com/en-us/help/173907/inf-how-to-set-the-day-month-year-date-format-in-sql-server


        public bool GpaBelowLimit()
        {
            bool rtn = false;
            double InternGpa = (string.IsNullOrEmpty(txtGpa.Text) ? 0 : double.Parse(txtGpa.Text));
            if (InternGpa < 4.0)
            {
                if (ddIInternGroup.SelectedItem.Value == "21")  // 21 is intern
                {
                    rtn = true;
                }
            }
            return rtn;
        }
        public void Insert(object sender, EventArgs e)
        {
            if (GpaBelowLimit() == true)
            {
                //common.PostMsg(lblOutput, "Your GPA must be 4/5  or 80% and above to register !", "red");
                //return;
                lblOutput.Text = "Your GPA must be 4/5  or 80% and above to register!";
                lblOutput.ForeColor = System.Drawing.Color.Red;
                txtGpa.Focus();
                //txtGpa.BorderColor = System.Drawing.Color.Red;
                //txtGpa.BorderWidth = 2;
                return;
            }
         

            // continue 
            //common.IsNullOrEmpty(txtFName, lblOutput);  // check it later,,, did not work
            #region validate
            if (String.IsNullOrEmpty(txtFName.Text))
            {
                lblOutput.Text = "Please fill the First Name !";
                lblOutput.ForeColor = System.Drawing.Color.Red;
                txtFName.Focus();
                return;
            }
            if (String.IsNullOrEmpty(txtMName.Text))
            {
                lblOutput.Text = "Please fill Middle Name !";
                lblOutput.ForeColor = System.Drawing.Color.Red;
                txtMName.Focus();
                return;
            }
            if (String.IsNullOrEmpty(txtLastName.Text))
            {
                lblOutput.Text = "Please fill the Last Name !";
                lblOutput.ForeColor = System.Drawing.Color.Red;
                txtLastName.Focus();
                return;
            }
            if (ddlGender.SelectedItem.Value == "1")  // aliChange
            {
                lblOutput.Text = "Please select Gender !";
                lblOutput.ForeColor = System.Drawing.Color.Red;
                ddlGender.Focus();
                return;
            }
            if (ddIInternGroup.SelectedItem.Value == "1")  // aliChange
            {
                lblOutput.Text = "Please select Profile Group !";
                lblOutput.ForeColor = System.Drawing.Color.Red;
                ddIInternGroup.Focus();
                return;
            }
            if (String.IsNullOrEmpty(txtCell.Text))
            {
                lblOutput.Text = "Please fill Cell Phone !";
                lblOutput.ForeColor = System.Drawing.Color.Red;
                txtCell.Focus();
                return;
            }
            if (String.IsNullOrEmpty(txtEmail.Text))
            {
                lblOutput.Text = "Please fill Email !";
                lblOutput.ForeColor = System.Drawing.Color.Red;
                txtEmail.Focus();
                return;
            }
            if (ddlinstitution.SelectedItem.Value == "1")  // aliChange
            {
                lblOutput.Text = "Please select Institution !";
                lblOutput.ForeColor = System.Drawing.Color.Red;
                ddlinstitution.Focus();
                return;
            }
            if (String.IsNullOrEmpty(txtGpa.Text))
            {
                lblOutput.Text = "Please fill the GPA !";
                lblOutput.ForeColor = System.Drawing.Color.Red;
                txtGpa.Focus();
                return;
            }

            if (ddlDegree.SelectedItem.Value == "1")  // aliChange
            {
                lblOutput.Text = "Please select Degree !";
                lblOutput.ForeColor = System.Drawing.Color.Red;
                ddlDegree.Focus();
                return;
            }
            if (ddlDegreeMajor.SelectedItem.Value == "1")  // aliChange
            {
                lblOutput.Text = "Please select Major !";
                lblOutput.ForeColor = System.Drawing.Color.Red;
                ddlDegreeMajor.Focus();
                return;
            }
            if (ddlTrainingTerm.SelectedItem.Value == "1")  // aliChange
            {
                lblOutput.Text = "Please select Training Term !";
                lblOutput.ForeColor = System.Drawing.Color.Red;
                ddlTrainingTerm.Focus();
                return;
            }
            if (ddlTrainingYear.SelectedItem.Value == "1")  // aliChange
            {
                lblOutput.Text = "Please select Training Year !";
                lblOutput.ForeColor = System.Drawing.Color.Red;
                ddlTrainingYear.Focus();
                return;
            }
            if (String.IsNullOrEmpty(txtTrainingHours.Text))
            {
                lblOutput.Text = "Please fill training Hours !";
                lblOutput.ForeColor = System.Drawing.Color.Red;
                txtTrainingHours.Focus();
                return;
            }
            if (!FileUpload.HasFiles)
            {
                lblOutput.Text = "Please attached requested Documents !";
                lblOutput.ForeColor = System.Drawing.Color.Red;
                FileUpload.Focus();
                return;
            }
            #endregion
            // if (common.IsNullOrEmpty(txtPhoneNumber, lblOutput)) return;

            Session["Email"] = txtEmail.Text;
            // check if  user exists
            string mySqlUserExists = @"select email,cell from intern
                                    where email =@email  or cell =@cell";
            Dictionary<string, Object> myPara = new Dictionary<string, object>();
            myPara.Add("@cell", (txtCell.Text));
            myPara.Add("@email", Session["Email"]);
            CRUD myCrud2 = new CRUD();
            bool rtn2 = myCrud2.authenticateUser(mySqlUserExists, myPara);
            if (rtn2 == true)
            {
                lblOutput.Text = "User already exists !  ";  // intentinoal to keep space 
                myLink.Text = " &nbsp <bR> Click to update your Information !";
                myLink.Visible = true;
                //  myLink.NavigateUrl = "~/internship/InternProfile.aspx";
                Session["Email"] = txtEmail.Text;
                myLink.NavigateUrl = "~/internship/internProfile.aspx";
                lblOutput.ForeColor = System.Drawing.Color.Red;
                //  hyperLink.Visible = true;  // ali restore it later so user can update infor
                return;
            }

            // Fill RefNo before insert
            txtRefNo.Text= getMaxRefNo().ToString();
            // capture user input
            int intInternId = 0;
                string StrInternEmail = (txtEmail.Text);
                string strApplicant = txtFName.Text + " " + txtLastName.Text;
                int intInstitutionId = int.Parse(ddlinstitution.SelectedValue);   // added Nov 2017
            GstrTrainingTerm = ddlTrainingTerm.SelectedItem.Text;
            GstrTrainingYear = ddlTrainingYear.SelectedItem.Text;
           string mySql = @"insert into intern (refno,fName,mi,lName,genderId,gpa,institutionId,cell,email,note,TrainingHours,
                            TrainingTermId,TrainingYearId,degreeId,degreeMajorId,supervisorName,supervisorCell,supervisorEmail)
                   values (@refNo,@fName,@mi,@lName,@genderId,@gpa,@institutionId,@cell,@email,@note,@TrainingHours,
                            @TrainingTermId,@TrainingYearId,@degreeId, @degreeMajorId,@supervisorName,@supervisorCell,@supervisorEmail)" +
                                                     "SELECT CAST(scope_identity() AS int)";
                Dictionary<string, Object> InsertParameters = new Dictionary<string, object>();
                InsertParameters.Add("@refNo", (txtRefNo.Text));
                InsertParameters.Add("@fName", (txtFName.Text));
                InsertParameters.Add("@mi", (txtMName.Text));
                InsertParameters.Add("@lName", (txtLastName.Text));
                InsertParameters.Add("@genderid", ddlGender.SelectedValue);
                InsertParameters.Add("@gpa", (txtGpa.Text));
                InsertParameters.Add("@institutionId", intInstitutionId);  //DDLTaskName.SelectedValue
                InsertParameters.Add("@cell", (txtCell.Text));
                InsertParameters.Add("@email", common.TrimMyString(StrInternEmail)); // added 07052019
                InsertParameters.Add("@note", (txtNote.Text));
                InsertParameters.Add("@TrainingHours", (txtTrainingHours.Text));
                InsertParameters.Add("@TrainingTermId", ddlTrainingTerm.SelectedValue );
                InsertParameters.Add("@TrainingYearId", ddlTrainingYear.SelectedValue);
                InsertParameters.Add("@degreeMajorId", ddlDegreeMajor .SelectedValue);
                InsertParameters.Add("@degreeId", ddlDegree.SelectedValue);
            InsertParameters.Add("@supervisorName", (txtSupervisorName.Text));
            InsertParameters.Add("@supervisorCell", (txtSupervisorCell.Text));
            InsertParameters.Add("@supervisorEmail", (txtSupervisorEmail.Text));

            //InsertParameters.Add("@date", (TxtCalenderAction.Text));
            //InsertParameters.Add("@profileComplete", cbActive.Checked);
            CRUD myCrud = new CRUD();
                intInternId = myCrud.InsertUpdateDeleteViaSqlDicRtnIdentity(mySql, InsertParameters);
            if (intInternId >= 1)
            {
                if (FileUpload.HasFiles)
                {
                     int rtn = InsertDocuments(intInternId);  // passing taskID as a return value from insert task
                    if (intInternId >= 1)  //rtn
                    {
                        // notify admin .. no attachement sent to any email due to insert to db first ... Prepare email
                           emailIntern("aalhussein63@gmail.com", strApplicant, txtRefNo.Text, txtFName.Text + " " + txtLastName.Text, GstrTrainingTerm, GstrTrainingYear, "Submitting your application");
                        // notify intern 
                           emailIntern(StrInternEmail, strApplicant, txtRefNo.Text, txtFName.Text + " " + txtLastName.Text, GstrTrainingTerm, GstrTrainingYear, "Submitting your application");

                        // if user is admin  or supervisor show all data
                        if (Roles.IsUserInRole(User.Identity.Name, "admin") || Roles.IsUserInRole(User.Identity.Name, "supervisor"))
                        {
                            populateGvIntern(); // use it for testing only  intInternId
                        }

                        // if usre is not admin or supervisor, only show user data
                        if (!Roles.IsUserInRole(User.Identity.Name, "admin") || !Roles.IsUserInRole(User.Identity.Name, "supervisor"))
                        {
                            populateGvIntern(intInternId); // use it for testing only  intInternId
                        }
                        clearInputform();
                        common.PostMsg(lblOutput, "Thank you for submitting your information! ", "green");
                        //lblOutput.Text = @"Thank you for submitting your information! ";
                        //lblOutput.ForeColor = System.Drawing.Color.Green;
                    }
                    else
                    {
                      //...  common.PostMsg(lblOutput, rtn, "red");
                    }
                }
                else
                {
                    common.PostMsg(lblOutput, "Thank you for submitting your information! ", "green");
                }
                InsertInternTrainingSchedule(intInternId); // to give intern training schedule added 04/2020
            }
        }
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            // form validation
            if (common.IsNullOrEmptyControlObj(txtFName, lblOutput, "Please fill first Name..... !")) return; ;  // added april 2020 ... short hand 
            if (common.IsNullOrEmptyControlObj(txtMName, lblOutput, "Please fill middle name !")) return;
            if (common.IsNullOrEmptyControlObj(txtLastName, lblOutput, "Please fill last name !")) return; // validation using common 
            if (common.IsNullOrEmptyControlObj(txtCell, lblOutput, "Please fill Cell number !")) return; // validation using common 
            if (common.IsNullOrEmptyControlObj(txtEmail, lblOutput, "Please fill Email !")) return; // validation using common common.IsNullOrEmpty(txtCell, lblOutput, "Please fill middle name !"); // validation using common 
            if (common.IsNullOrEmptyControlObj(txtGpa, lblOutput, "Please fill GPA !")) return; // validation using common 
            if (common.IsNullOrEmptyControlObj(ddlinstitution, lblOutput, "Please fill Institution !")) return;  // added april 2020 ... short hand 
            if (common.IsNullOrEmptyControlObj(ddIInternGroup, lblOutput, "Please fill Profile Group !")) return;  // added april 2020 ... short hand 
            
            if (common.IsNullOrEmptyControlObj(ddlDegree, lblOutput, "Please fill Degree !")) return;  // added april 2020 ... short hand 
            if (common.IsNullOrEmptyControlObj(ddlDegreeMajor, lblOutput, "Please fill Major !")) return;  // added april 2020 ... short hand 
            if (common.IsNullOrEmptyControlObj(ddlTrainingTerm, lblOutput, "Please fill Training Term !")) return;  // added april 2020 ... short hand 
            if (common.IsNullOrEmptyControlObj(ddlTrainingYear, lblOutput, "Please fill Training Year !")) return;  // added april 2020 ... short hand 
            // delare variables
            string refNo = "-1";
            int InternStatusId = 1;  // there is arelationship between the value and what in thecombo 
            int internGroupId = 1;
            int administrationId = 1;
            string fname = "";
            string mi = "";
            string lname = "";
            int genderId = 1;
            string cell = "";
            string email = "";
            int institutionId = 1;
            decimal gpa = 0.0M;
            int degreeId = 1;
            int degreeMajorId = 1;
            int trainingTermId = 1;
            int trainingYearId = 1;
            string registrationDate = "";//DateTime.Parse("01/01/1900");
                                         //   string UploadedDate = "";//DateTime.Parse("01/01/1900");
            string note = "";
            int intProfileUpdated = 0;
            int trainingHours = 0;

            // populate variables 
            string strEmailSearch = txtEmailSearch.Text;
            refNo = txtRefNo.Text;
            InternStatusId = int.Parse(ddInternStatus.SelectedValue.ToString());
            internGroupId = int.Parse(ddIInternGroup.SelectedValue.ToString());
            administrationId = int.Parse(ddIAdministration.SelectedValue.ToString());
            fname = txtFName.Text;
            mi = txtMName.Text;
            lname = txtLastName.Text;
            genderId = int.Parse(ddlGender.SelectedValue.ToString());
            cell = txtCell.Text;
            email = txtEmail.Text;

          string strSupervisorName =txtSupervisorName.Text;
          string strSupervisorCell =  txtSupervisorCell.Text;
          string strSupervisorEmail =   txtSupervisorEmail.Text;

            institutionId = int.Parse(ddlinstitution.SelectedValue.ToString());
            GstrTrainingYear = ddlTrainingYear.SelectedItem.Text;
            GstrTrainingTerm = ddlTrainingTerm.SelectedItem.Text;

            if (common.IsDecimalOrIntOrEmpty(txtGpa.Text))
            {
                gpa = decimal.Parse(txtGpa.Text.ToString());
            }
            else
            {
                lblOutput.Text = "Please insert gpa in the right format";
                lblOutput.ForeColor = System.Drawing.Color.Red;
                return;
                // rtn decimal    gpa = 0.0M;
            }
            degreeId = int.Parse(ddlDegree.SelectedValue.ToString());
            degreeMajorId = int.Parse(ddlDegreeMajor.SelectedValue.ToString());
            trainingTermId = int.Parse(ddlTrainingTerm.SelectedValue.ToString());
            trainingYearId = int.Parse(ddlTrainingYear.SelectedValue.ToString());
            trainingHours = int.Parse(txtTrainingHours.Text);
            note = txtNote.Text;
            intProfileUpdated = cbProfileComplete.Checked ? 1 : 0;  // nice short if
            string mySql = @"update intern 
                            set refNo=@refNo,InternStatusId=@InternStatusId,internGroupId=@internGroupId,administrationId=@administrationId,
                            fName=@fName,mi=@mi, lname=@lname,Cell=@Cell, Email=@Email ,institutionId=@institutionId, gpa=@gpa, degreeId=@degreeId,
                            degreeMajorId=@degreeMajorId,trainingTermid=@trainingTermId, trainingYearId=@trainingYearId, note=@note, 
                            ProfileUpdated=@ProfileUpdated, trainingHours =@trainingHours, genderId = @genderId, supervisorName =@supervisorName,
                            supervisorCell = @supervisorCell, supervisorEmail = @supervisorEmail
                            where  internId = @internId"; ; //  email = @strEmailSearch";

            Dictionary<string, object> myPara = new Dictionary<string, object>();
            myPara.Add("@internId", hidInternId.Value);   //  hidInternId.Value   StaticIntInternId
            myPara.Add("@strEmailSearch", txtEmailSearch.Text);
            myPara.Add("@refNo", refNo);
            myPara.Add("@InternStatusId", InternStatusId);
            myPara.Add("@internGroupId", internGroupId);
            myPara.Add("@administrationId", administrationId);
            myPara.Add("@fName", fname);
            myPara.Add("@mi", mi);
            myPara.Add("@lname", lname);
            myPara.Add("@genderId", genderId);
            myPara.Add("@Cell", cell);
            myPara.Add("@email", common.TrimMyString(email));
            myPara.Add("@institutionId", institutionId);
            myPara.Add("@gpa", gpa);  // decimal rtn   Convert.ToDecimal(gpa));  // decimal rtn   Convert.ToDecimal(gpa)); 
            myPara.Add("@degreeId", degreeId);
            myPara.Add("@degreeMajorId", degreeMajorId);
            myPara.Add("@trainingTermId", trainingTermId);
            myPara.Add("@trainingYearId", trainingYearId);
            myPara.Add("@note", note);
            myPara.Add("@trainingHours", trainingHours);
            myPara.Add("@ProfileUpdated", intProfileUpdated);
            myPara.Add("@supervisorName", strSupervisorName);
            myPara.Add("@supervisorCell", strSupervisorCell);
            myPara.Add("@supervisorEmail", strSupervisorEmail); 

            CRUD myCrud = new CRUD();
            try
            {
                InsertDocuments(int.Parse(hidInternId.Value)); // comeback  hidInternId.Value  StaticIntInternId
                int rtn = myCrud.InsertUpdateDelete(mySql, myPara);
                if (rtn >= 1)
                {
                    lblOutput.Text = "Thank you for updating your information!";
                    lblOutput.ForeColor = System.Drawing.Color.Green;
               //     btnNotifyAdminOfRegistration_Click(null, null); /// added on Mar 2020 to notify intern of the update via email 
                }
                else
                {
                    lblOutput.Text = "Update Failed!";
                    lblOutput.ForeColor = System.Drawing.Color.Red;
                }
            }
            catch (Exception ex)
            {
                //Response.Write(ex.Message.ToString());
                lblOutput.Text = ex.Message.ToString();
                lblOutput.ForeColor = System.Drawing.Color.Red;
            }

            // declare var for email notification
            string strApplicant = fname + " " + lname;

            //emailIntern("aalhussein63@gmail.com", "Ref#: " + refNo + " Email:  " + email , "Updating Intern Profile");
         //   emailIntern("aalhussein63@gmail.com", "Updating Profile for: " + strApplicant, txtRefNo.Text, txtFName.Text + " " + txtLastName.Text, GstrTrainingTerm, GstrTrainingYear, "Updating your  Profile");
            //   emailIntern("aalhussein63@gmail.com", "Updating Profile", refNo, strFullName, GstrTrainingTerm, GstrTrainingYear , "Updating your  Profile");
            // Response.Write("send email "); //need to work on email 
         emailIntern(common.TrimMyString(email), strApplicant, txtRefNo.Text, txtFName.Text + " " + txtLastName.Text, GstrTrainingTerm, GstrTrainingYear, "Updating your  Profile");
            // notify admin
        emailIntern("aalhussein63@gmail.com", strApplicant, txtRefNo.Text, txtFName.Text + " " + txtLastName.Text, GstrTrainingTerm, GstrTrainingYear, "Updating your  Profile");
            clearInputform();
            // insert new doc 
            // if user is admin  or supervisor show all data
            if (Roles.IsUserInRole(User.Identity.Name, "admin") || Roles.IsUserInRole(User.Identity.Name, "supervisor"))
            {
                populateGvIntern(); // use it for testing only  intInternId
            }

            // if usre is not admin or supervisor, only show user data
            if (!Roles.IsUserInRole(User.Identity.Name, "admin") || !Roles.IsUserInRole(User.Identity.Name, "supervisor"))
            {
                populateGvIntern(int.Parse(hidInternId.Value)); // use it for testing only  intInternId
            }
        }
        protected void callUploadDoc(int intDocOwnerId)   // new in april continue Mon april 13 2020
        {
            if (intDocOwnerId >= 1)
            {
                if (FileUpload.HasFiles)
                {
                    int rtn = InsertDocuments(intDocOwnerId);  // passing taskID as a return value from insert task
                    if (rtn >= 1)
                    {
                        // emailIntern(StrInternEmail, strApplicant," Submitting " );
                        //.. emailIntern(StrInternEmail, strApplicant, txtRefNo.Text, txtFName.Text + " " + txtLastName.Text, "Insert");
                        //populateGvIntern(); // use it for testing only  intInternId
                        //clearInputform();
                        //lblOutput.Text = @"Thank you for submitting your information! ";
                        //lblOutput.ForeColor = System.Drawing.Color.Green;
                    }
                    else
                    {
                        common.PostMsg(lblOutput, rtn, "red");
                    }
                }
            }
        }
        protected int InsertDocuments(int myInternId)
        {
            int rtn = 0;
            if (FileUpload.HasFiles)
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
                            CRUD DocInsert = new CRUD();  // added Nov 2017 
                            string mySql = @"insert into InternDoc(InternId,DocName,ContentType,DocData)
                                    values (@InternId,@DocName,@ContentType,@DocData)";
                            Dictionary<string, Object> p = new Dictionary<string, object>();
                            //p.Add("@TaskId", "get the value ");
                            p.Add("@InternId", myInternId);  // added Nov 2017
                            p.Add("@DocName", filename);
                            p.Add("@ContentType", contentType);
                            p.Add("@DocData", bytes);
                            rtn = DocInsert.InsertUpdateDelete(mySql, p);
                        }
                    }
                }
            }
                return rtn;
       }
        protected void btnEdit_Click(object sender, EventArgs e)
        {
            // to populate the inputform from 
            //Response.Write("yyyy/MM/dd"));
            //https://jqueryui.com/datepicker/
            // declare var to capture data returned from db
            string strEmailSearch = "";
            //string refNo = "";
            //int InternStatusId = 1;  // there is arelationship between the value and what in thecombo 
            //int InternGroupId = 1;  // there is arelationship between the value and what in thecombo 
            //int administrationId = 1;
            //string fname = "";
            //string mi = "";
            //string lname = "";
            //string strCell = "";
            //string email = "";
            //int institutionId = 1;
            //string gpa = "";
            //int degreeId = 1;
            //int degreeMajorId = 1;
            //int trainingTermId = 1;
            //int trainingYearId = 1;
            //string note = "";
            DateTime registrationDate = DateTime.Parse("01/01/1900");  // check me
                                                                       //     DateTime UploadedDate = DateTime.Parse("01/01/1900");  // check me
            cbProfileComplete.Checked = false;
            int trainingHours = 0;

            if (!Roles.IsUserInRole(User.Identity.Name, "admin"))
            {
                common.PostMsg(lblOutput, "Only Admin can Edit ! ","red");
                return;
            }

            // clear email search textbox
            common.clearControl(txtEmailSearch);

            // bind var to the fields
            Button btn = sender as Button;
            GridViewRow grow = btn.NamingContainer as GridViewRow;  // ref to the gv row 
            hidInternId.Value = (grow.FindControl("lblInternId") as Label).Text;
            txtRefNo.Text = (grow.FindControl("lblRefNo") as Label).Text; ;
            ddInternStatus.SelectedValue = (grow.FindControl("lblInternStatusId") as Label).Text;
            ddIInternGroup.SelectedValue = (grow.FindControl("lblInternGroupId") as Label).Text;
            ddIAdministration.SelectedValue = (grow.FindControl("lblAdministrationId") as Label).Text;
            txtFName.Text = (grow.FindControl("lblFname") as Label).Text;
            txtMName.Text = (grow.FindControl("lblMn") as Label).Text;
            txtLastName.Text = (grow.FindControl("lblLname") as Label).Text;
            txtGpa.Text = (grow.FindControl("lblGpa") as Label).Text;   //lblGpa
            ddlinstitution.SelectedValue = (grow.FindControl("lblInstitutionId") as Label).Text;
            ddlDegree.SelectedValue = (grow.FindControl("lblDegreeId") as Label).Text;
            ddlDegreeMajor.SelectedValue = (grow.FindControl("lblDegreeMajorId") as Label).Text;
            txtCell.Text = (grow.FindControl("lblCell") as Label).Text;
            txtEmail.Text = (grow.FindControl("lblEmail") as Label).Text;
            txtNote.Text = (grow.FindControl("lblNote") as Label).Text;
            ddlTrainingTerm.SelectedValue = (grow.FindControl("lblTrainingTermId") as Label).Text;
            ddlTrainingYear.SelectedValue = (grow.FindControl("lblTrainingYearId") as Label).Text;
            txtTrainingHours.Text = (grow.FindControl("lblTrainingHours") as Label).Text;
            txtRegistrationDate.Text = (grow.FindControl("lblRegistrationDate") as Label).Text;
            lblOutput.Text = "Please review & update your information!";
            lblOutput.ForeColor = System.Drawing.Color.Green;
             GstrTrainingYear = (grow.FindControl("lblTrainingYear") as Label).Text;
             GstrTrainingTerm = (grow.FindControl("lblTrainingTerm") as Label).Text;
            txtSupervisorName.Text = (grow.FindControl("lblSupervisorName") as Label).Text;
            txtSupervisorCell.Text = (grow.FindControl("lblSupervisorCell") as Label).Text;
            txtSupervisorEmail.Text = (grow.FindControl("lblSupervisorEmail") as Label).Text;

            showHideSubmitUpdate("edit");
        }
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            if (!Roles.IsUserInRole(User.Identity.Name, "admin"))
            {
                common.PostMsg(lblOutput, "Only Admin can Delete ! ","red");
                return;
            }

            // shows how to access gvr when the sender is a button inside the gv
            SqlConnection con = new SqlConnection();
            try
            {
                Button btn = sender as Button;
                GridViewRow grow = btn.NamingContainer as GridViewRow;
                int intInternId = int.Parse((grow.FindControl("lblInternId") as Label).Text);
                CRUD myCrud = new CRUD();
                string mySql = @"delete  intern  where internid=@internId";
                Dictionary<string, object> myPara = new Dictionary<string, object>();
                myPara.Add("@internId", intInternId);
                int rtn = myCrud.InsertUpdateDelete(mySql, myPara);
                App_Code.common.PostMsg(lblOutput, rtn);
            }
            catch (Exception ex)
            {
                //  throw ex;
                App_Code.common.PostMsg(lblOutput, ex.Message.ToString());
            }
            if (Roles.IsUserInRole(User.Identity.Name, "admin") || Roles.IsUserInRole(User.Identity.Name, "supervisor"))
            {
                populateGvIntern(); // use it for testing only  intInternId
            }

            // if usre is not admin or supervisor, only show user data
            if (!Roles.IsUserInRole(User.Identity.Name, "admin") || !Roles.IsUserInRole(User.Identity.Name, "supervisor"))
            {
                populateGvIntern(int.Parse(hidInternId.Value)); // use it for testing only  intInternId
            }
        }
        #region populate combos 
        protected void populateInternStatusIdDropList()
        {
            //ddlDegreeMajor.Items.Clear();
            //ddlDegreeMajor.Items.Add("Select Major");
           CRUD myCrud = new CRUD();
            string sqlInternStatus = @"select InternStatusId, InternStatus from InternStatus";
            myCrud.populatComboViaDr(sqlInternStatus, ddInternStatus, "InternStatusId", "InternStatus");
        }
        protected void populateddlInternGroupDropList()
        {
            //ddlDegreeMajor.Items.Clear();
            //ddlDegreeMajor.Items.Add("Select Major");
            CRUD myCrud = new CRUD();
            string sqlInternGroup = @"select  internGroupId,internGroup  from interngroup where active =1 or interngroupid=1 ";
            myCrud.populatComboViaDr(sqlInternGroup, ddIInternGroup, "internGroupId", "internGroup");
        }
        protected void populateddlOrganizationDepList()
        {
            //ddlDegreeMajor.Items.Clear();
            //ddlDegreeMajor.Items.Add("Select Major");
            CRUD myCrud = new CRUD();
            string SqlAdministration = @"select administrationId,Administration  from Administration";
            myCrud.populatComboViaDr(SqlAdministration, ddIAdministration, "administrationId", "administration");

        }
        protected void populateInstitutionDropList()
        {
            ddlinstitution.Items.Clear();
            //ddlinstitution.Items.Add("Select Institution");
            CRUD myCrud = new CRUD();
            string sqlInstitution = @"Select institutionId, Institution  from Institution";
            myCrud.populatComboViaDr(sqlInstitution, ddlinstitution, "institutionId", "Institution");


        }
        //protected void populateGenericDropList(string mySql, DropDownList myCombo, int myValue, string myText) // to do rtn
        //{
        //    //ddlDegree.Items.Clear();
        //    //ddlDegree.Items.Add("Select Degree");
        //    CRUD myCrud = new CRUD();
        //    SqlDataReader dr = myCrud.getDrPassSql(mySql);
        //    myCombo.DataTextField = "myText";
        //    myCombo.DataValueField = "myValue";
        //    myCombo.DataSource = dr;
        //    myCombo.DataBind();

        //}
        protected void populateddlDegreeDropList()
        {
            //ddlDegree.Items.Clear();
            //ddlDegree.Items.Add("Select Degree");
            CRUD myCrud = new CRUD();
            string sqlDegree = @"select degreeid, degree from degree";
            myCrud.populatComboViaDr(sqlDegree, ddlDegree, "degreeid", "degree");
        }
        protected void populateDegreeMajorDropList()
        {
            //ddlDegreeMajor.Items.Clear();
            //ddlDegreeMajor.Items.Add("Select Major");
            CRUD myCrud = new CRUD();
            string sqlDegreeMajor = @"select degreeMajorid, degreeMajor from degreeMajor";
            myCrud.populatComboViaDr(sqlDegreeMajor, ddlDegreeMajor, "degreeMajorid", "degreeMajor");
        }
        protected void populatetrainingTermDropList()
        {
            //ddlDegreeMajor.Items.Clear();
            //ddlDegreeMajor.Items.Add("Select Major");
            CRUD myCrud = new CRUD();
            string sqlTrainingTerm = @" select trainingTermId, trainingTerm from trainingTerm";
            myCrud.populatComboViaDr(sqlTrainingTerm, ddlTrainingTerm, "trainingTermId", "trainingTerm");
        }
        protected void populatetrainingYearDropList()
        {
            //ddlDegreeMajor.Items.Clear();
            //ddlDegreeMajor.Items.Add("Select Major");
            CRUD myCrud = new CRUD();
            string sqlTrainingYear = @"select trainingYearId, trainingYear from trainingYear";
            myCrud.populatComboViaDr(sqlTrainingYear, ddlTrainingYear, "trainingYearId", "trainingYear");

        }
        protected void populateGenderDropList()
        {
            //ddlDegreeMajor.Items.Clear();
            //ddlDegreeMajor.Items.Add("Select Major");
            CRUD myCrud = new CRUD();
            string sqlGender = @"select genderId, gender from gender";
            myCrud.populatComboViaDr(sqlGender, ddlGender, "genderId", "gender");

        }
        
        //    protected void populateinstitutionIdGv()
        //    {
        //        CRUD myCrud = new CRUD();
        //        string MeetingSeltCom = @"SELECT ta.institutionId, ta.Action, ta.EmployeeId, e.EmployeeName FROM TaskActivity ta inner join Employee e
        //                                on ta.EmployeeId = e.EmployeeId ";
        //        SqlDataReader dr = myCrud.getDrPassSql(MeetingSeltCom); //myCrud.getDrPassSql(MeetingSeltCom)
        //        TaskActivityGV.DataSource = dr;
        //        TaskActivityGV.DataBind();
        //    }
        #endregion
        protected void populateGvIntern()
        {
            // restore gui gv
            CRUD myCrud = new CRUD();  //*i.institutionId*//
            string mySql = @"SELECT  intern.internId, intern.internStatusId,InternStatus, intern.InternGroupId, internGroup.internGroup, intern.refNo, 
                            intern.administrationId,administration.administration,  intern.fName , intern.mi , intern.lName , intern.genderId, gender.gender,
                            intern.gpa, trainingHours,intern.institutionId, Institution.Institution, intern.degreeId,
                            degree.degree, intern.degreeMajorId, degreeMajor.degreeMajor, intern.cell, intern.email, intern.trainingHours, intern.note, 
                            supervisorName,supervisorCell, supervisorEmail,intern.trainingTermId, 	trainingTerm.TrainingTerm, intern.trainingYearId,
                            trainingYear.trainingYear,registrationDate, profileComplete
                            FROM  
                            intern  INNER JOIN InternStatus on intern.InternStatusId= internstatus.internstatusId
                            INNER JOIN internGroup 	ON  intern.InternGroupId  =InternGroup.InternGroupId 
                            INNER JOIN administration on intern.administrationId =  administration.administrationId	
                            INNER JOIN gender on intern.genderId = gender.genderId
                            INNER JOIN Institution on intern.institutionId = institution.institutionId
                            INNER JOIN degree on intern.degreeId = degree.degreeId
                            INNER JOIN degreeMajor on intern.degreeMajorId = degreeMajor.degreeMajorId
                            INNER JOIN trainingTerm on intern.trainingTermId= trainingTerm.TrainingTermId
                            INNER JOIN trainingYear on intern.trainingYearId = trainingYear.trainingYearId
                                order by fname asc";
            //using (SqlDataReader dr = myCrud.getDrPassSql(mySql))
            //{
            //    gvIntern.DataSource = dr;
            //    gvIntern.DataBind();
            //}

            using (DataTable dt = myCrud.getDT(mySql))
            {
                Session["myDataTable"] = dt;
                gvIntern.DataSource = Session["myDataTable"];
                gvIntern.DataBind();
            }
        }
        protected void populateGvIntern(int internId)
        {
            // restore gui gv
            CRUD myCrud = new CRUD();  //*i.institutionId*//
            string mySql = @"SELECT  intern.internId, intern.internStatusId,InternStatus, intern.InternGroupId, internGroup.internGroup, intern.refNo, 
                            intern.administrationId,administration.administration, intern.fName , intern.mi , intern.lName , intern.genderId, gender.gender,
                            intern.gpa, trainingHours,intern.institutionId, Institution.Institution, intern.degreeId,
                            degree.degree, intern.degreeMajorId, degreeMajor.degreeMajor, intern.cell, intern.email, intern.trainingHours, intern.note, 
                            supervisorName,supervisorCell, supervisorEmail,intern.trainingTermId, 	trainingTerm.TrainingTerm, intern.trainingYearId,
                            trainingYear.trainingYear,registrationDate, profileComplete
                            FROM  
                            intern  INNER JOIN InternStatus on intern.InternStatusId= internstatus.internstatusId
                            INNER JOIN internGroup 	ON  intern.InternGroupId  =InternGroup.InternGroupId 
                            INNER JOIN administration on intern.administrationId =  administration.administrationId	
                            INNER JOIN gender on intern.genderId = gender.genderId
                            INNER JOIN Institution on intern.institutionId = institution.institutionId
                            INNER JOIN degree on intern.degreeId = degree.degreeId
                            INNER JOIN degreeMajor on intern.degreeMajorId = degreeMajor.degreeMajorId
                            INNER JOIN trainingTerm on intern.trainingTermId= trainingTerm.TrainingTermId
                            INNER JOIN trainingYear on intern.trainingYearId = trainingYear.trainingYearId
                            where internId = @internId
                            order by fname asc";
                                Dictionary<string, object> myPara = new Dictionary<string, object>();
                                myPara.Add("@internId", internId);
            //using (SqlDataReader dr = myCrud.getDrPassSql(mySql,myPara)) //myCrud.getDrPassSql(MeetingSeltCom)
            //{
            //    gvIntern.DataSource = dr;
            //    gvIntern.DataBind();
            //}
            using (DataTable dt = myCrud.getDTPassSqlDic(mySql,myPara))
            {
                Session["myDataTable"] = dt;
                gvIntern.DataSource = Session["myDataTable"];
                gvIntern.DataBind();
            }
        }
        //protected void populateGvIntern(int internId)
        //{
        //    CRUD myCrud = new CRUD();
        //    Dictionary<string, object> myPara = new Dictionary<string, object>();
        //    myPara.Add("@internId", internId);
        //    string MeetingSeltCom = @"select i.internId,[fName],[mi],[lName],[gpa],i.institutionId,institution,
        //                    [cell],[email],[note],[UploadedDate],[profileComplete] 
        //                    from intern i inner join institution inst on i.institutionId =inst.institutionId
        //                    inner join internDoc idoc on i.internId = idoc.internId
        //                    where i.internId =@internId";
        //       SqlDataReader dr = myCrud.getDrPassSql(MeetingSeltCom, myPara); //myCrud.getDrPassSql(MeetingSeltCom)
        //    gvIntern.DataSource = dr;
        //    gvIntern.DataBind();
        //}
        protected void embedDateTimeVal(TextBox txtBox)
        {
            ControlCollection cc = txtBox.Parent.Controls;
            string[] spl = txtBox.ClientID.Split(new char[] { '_' });
            CalendarExtender ce = new CalendarExtender();
            ce.ID = txtBox.ID + "_ce";
            ce.TargetControlID = spl[spl.Length - 1];
            cc.Add(ce);
        }
        //    protected void DDLTaskName_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    int intTAskId = int.Parse(DDLTaskName.SelectedValue.ToString());
        //    CRUD ActOwnerDDLSelect = new CRUD();
        //    string ActOwnerSelCom = (@"SELECT t.TaskID, t.EmployeeID, e.EmployeeName FROM TaskOwner t INNER JOIN Employee e
        //                                ON t.EmployeeID = e.EmployeeID Where TaskID = " + intTAskId);
        //    SqlDataReader dr = ActOwnerDDLSelect.getDrPassSql(ActOwnerSelCom);
        //    ddlActOwner.DataSource = dr;
        //    ddlActOwner.DataTextField = "EmployeeName";
        //    ddlActOwner.DataValueField = "EmployeeID";
        //    ddlActOwner.DataBind();
        //    populateinstitutionIdGv(intTAskId);
        //}
        protected void btnShowAllInterns_Click(object sender, EventArgs e)
        {
            populateGvIntern();
        }
        protected void LbAddInstitution_Click(object sender, EventArgs e)
        {
            bool myRtn = common.InputIsEmpty(txtInstitution.Text);
            if (myRtn == true)
            {
                common.PostMsg(lblOutput, "Please fill institution field!");
                txtInstitution.Focus();
                return;
            }

            if (IsPostBack)
            {
                CRUD myCrud = new CRUD();
                string mySql = @"INSERT INTO Institution(institution)VALUES (@institution)";
                Dictionary<string, object> myPara = new Dictionary<string, object>();
                myPara.Add("@institution", txtInstitution.Text);
                int rtn = myCrud.InsertUpdateDelete(mySql, myPara);
                lblOutput.Text = rtn >= 1 ? "Success" : "Failed";
                lblOutput.ForeColor = rtn >= 1 ? System.Drawing.Color.Green : System.Drawing.Color.Red;
                populateInstitutionDropList();
            }
        }
        protected void LbAddInternGroup_Click(object sender, EventArgs e)
        {
            bool myRtn = common.InputIsEmpty(txtInternGroup.Text);
            if (myRtn == true)
            {
                common.PostMsg(lblOutput, "Please fill Intern Group field!");
                txtInstitution.Focus();
                return;
            }

            if (IsPostBack)
            {
                CRUD myCrud = new CRUD();
                string mySql = @"INSERT INTO internGroup(internGroup)VALUES (@internGroup)";
                Dictionary<string, object> myPara = new Dictionary<string, object>();
                myPara.Add("@internGroup", txtInternGroup.Text);
                int rtn = myCrud.InsertUpdateDelete(mySql, myPara);
                lblOutput.Text = rtn >= 1 ? "Success" : "Failed";
                lblOutput.ForeColor = rtn >= 1 ? System.Drawing.Color.Green : System.Drawing.Color.Red;
                populateInstitutionDropList();
            }
        }
        protected void txtRefNo_Load(object sender, EventArgs e)
        {
            txtRefNo.Focus();
            lblOutput.Text = "";
        }
        protected void btnGetInternProfile_Click(object sender, EventArgs e)
        {
            HyperLinkNextPage.Visible = false;
            clearInputform(); // first clear all controls 
           populateInternProfileForm();
            common.showHideControl(btnSubmit, false);
            common.showHideControl(btnUpdate, true);
        }

        //prepare the email 
        protected void emailIntern(string internEmail, string strSubject, string intRefNo, string strFullName, string term, string year, string myAction)
        {
            using (mailMgr myMailMgr = new mailMgr())
            {
                myMailMgr.myTo = internEmail;
                myMailMgr.mySubject = "File# " + intRefNo + " Internship Training Program - " + term + " " + year + " for " + strSubject;
                string strBody = @"Dear " + strFullName + ",\n\n";
                strBody += @" Thank  you for " + myAction + " !";

                strBody += @"


        Regards,

        Ali Mohamed Hamidaddin
        IT Consultant,Application Training Department Chairperson
        Executive Administration of Information Technology
        King Fahad Medical City
        P.O.Box 59046, Riyadh 11525
        Kingdom Of Saudi Arabia
        (+966) 11 288 9999 Ext: 19100
        (+966) 538692448
        ahameed@kfmc.med.sa";
                myMailMgr.myBody = strBody;
                myMailMgr.testEmailViaGmail(FileUpload); // ali come back
            }
        }
        protected void emailIntern2(string internEmail, string strApplicant)
        {
            using (mailMgr myMailMgr = new mailMgr())
            {
                myMailMgr.myTo = internEmail;
                myMailMgr.mySubject = "KFMC received document notification";
                string strBody = @"Dear " + strApplicant + ",\n\n";
                strBody += @" Thank  you for submitting your application!. 

        Regards,

        Ali Mohamed Hamidaddin
        IT Consultant,Application Training Department Chairperson
        Executive Administration of Information Technology
        King Fahad Medical City
        P.O.Box 59046, Riyadh 11525
        Kingdom Of Saudi Arabia
        (+966) 11 288 9999 Ext: 19100
        (+966) 538692448
        ahameed@kfmc.med.sa";
                myMailMgr.myBody = strBody;
                myMailMgr.testEmailViaGmail();
            }

            clearInputform(); // first clear all controls 
        }
        protected void btnNotifyAdminOfRegistration_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    emailIntern2("aalhussein@yahoo.com", "Ali Alhussein");  //rtn
            //    // emailIntern("aalhussein63@gmail.com", "Updating Profile", txtRefNo.Text, txtFName.Text + " " + txtLastName.Text , "Updating Intern Profile");
            //}

            //catch (Exception ex)
            //{
            //    lblOutput.Text = ex.Message.ToString();
            //}
            mailMgr myMailmgr = new mailMgr();
            myMailmgr.testEmailViaGmail(txtEmail.Text, "info@wdbcs.com", txtNote.Text, FileUpload);  // to notify user via eamil done mar 2020
        }
         // creating One combo for all 
        private void populatComboViaDr(string mySql, DropDownList comboName, string myValue, string myText)
        {
            //ddlDegree.Items.Clear();
            //ddlDegree.Items.Add("Select Degree");
            CRUD myCrud = new CRUD();
            using (SqlDataReader dr = myCrud.getDrPassSql(mySql))
            {
                comboName.DataTextField = myText.ToString();
                comboName.DataValueField = myValue.ToString();
                comboName.DataSource = dr;
                comboName.DataBind();
            }
        }
        //protected void populateInternProfileForm_saved()
        //{

        //    //Response.Write("yyyy/MM/dd"));
        //    //https://jqueryui.com/datepicker/
        //    // declare var to capture data returned from db
        //    string strEmailSearch = "";
        //    string refNo = "";
        //    int InternStatusId = 1;  // there is arelationship between the value and what in thecombo 
        //    int InternGroupId = 1;  // there is arelationship between the value and what in thecombo 
        //    int administrationId = 1;
        //    string fname = "";
        //    string mi = "";
        //    string lname = "";
        //    string strCell = "";
        //    string email = "";
        //    int institutionId = 1;
        //    string gpa = "";
        //    int degreeId = 1;
        //    int degreeMajorId = 1;
        //    int trainingTermId = 1;
        //    int trainingYearId = 1;
        //    string note = "";
        //    DateTime registrationDate = DateTime.Parse("01/01/1900");  // check me
        //    DateTime UploadedDate = DateTime.Parse("01/01/1900");  // check me
        //    cbProfileComplete.Checked = false;

        //    string myValueOfWhereClause = "";
        //    Dictionary<string, object> myPara = new Dictionary<string, object>();
        //    // runs for an intern only to view his/her profile
        //    ////if (!String.IsNullOrEmpty(Request.QueryString["internId"]))
        //    if (!String.IsNullOrEmpty(person.intPersonId.ToString()))  // replacing querystring with class person to avoid pulling other profiles by changing para of query string 
        //    {
        //        if (!Roles.IsUserInRole(User.Identity.Name, "admin"))
        //        {
        //            // hide search email if user is  not admin
        //            btnGetInternProfile.Visible = false;
        //            txtEmailSearch.Visible = false;
        //            lblEnterYourEmail.Visible = false;
        //            myValueOfWhereClause = " where internId = @internId";
        //          ////  myPara.Add("@internId", Request.QueryString["internId"]);
        //            myPara.Add("@internId", person.intPersonId.ToString());  // replaced querystring
        //        }
        //        else
        //        {
        //            btnGetInternProfile.Visible = true;
        //            txtEmailSearch.Visible = true;
        //            lblEnterYourEmail.Visible = true;
        //        }
        //    }

        //    if (Roles.IsUserInRole(User.Identity.Name, "admin") & common.InputIsEmpty(txtEmailSearch.Text) == true)
        //    {
        //        myValueOfWhereClause = " where internId = @internId";
        //      /////  myPara.Add("@internId", Request.QueryString["internId"]);
        //        myPara.Add("@internId", person.intPersonId.ToString()); //replaced querystring   person.intPersonId.ToString())
        //    } 
        //    else if (Roles.IsUserInRole(User.Identity.Name, "admin") & common.InputIsEmpty(txtEmailSearch.Text) == false)
        //    {
        //        if (common.InputIsEmpty(txtEmailSearch.Text))
        //        {
        //            common.PostMsg(lblOutput, "please enter email ", "red");
        //            txtEmailSearch.Focus();
        //            return;
        //        }
        //        else
        //        {
        //            strEmailSearch = txtEmailSearch.Text;
        //            myValueOfWhereClause = "where email = @InernEmail";// need space to avoid sql error 
        //            myPara.Add("@InernEmail", strEmailSearch.Trim());
        //        }
        //    }

        //    string mySql = @"select  internId,refNo,InternStatusId, InternGroupId,administrationId,fName,mi,lName,cell,email,institutionId,gpa,
        //                degreeId,degreeMajorId,trainingTermId,trainingYearId,note,registrationDate,UploadedDate,ProfileUpdated
        //                from intern  " + myValueOfWhereClause;
        //    //  myPara.Add("@InernEmail", strEmailSearch.Trim());   //   strEmailSearch  trim is important to ensure query output  or   common.TrimMyString(StrInternEmail));
        //    CRUD myCrud = new CRUD();
        //    using (SqlDataReader dr = myCrud.getDrPassSql(mySql, myPara))
        //    {
        //        if (dr.HasRows)
        //        {
        //            while (dr.Read())
        //            {
        //                StaticIntInternId = int.Parse(dr["internId"].ToString()); // rtn this is static var its scope for the whole page
        //                refNo = dr["refno"].ToString();
        //                InternStatusId = int.Parse(dr["InternStatusId"].ToString());
        //                InternGroupId = int.Parse(dr["InternGroupId"].ToString());
        //                administrationId = int.Parse(dr["administrationId"].ToString());
        //                fname = dr["fname"].ToString();
        //                mi = dr["mi"].ToString();
        //                lname = dr["lname"].ToString();
        //                gpa = dr["gpa"].ToString();
        //                institutionId = int.Parse(dr["institutionId"].ToString());
        //                degreeId = int.Parse(dr["degreeId"].ToString()); // need to work on i t
        //                degreeMajorId = int.Parse(dr["degreeMajorId"].ToString()); // need to work on i t
        //                strCell = dr["cell"].ToString();
        //                email = (dr["email"].ToString());
        //                note = (dr["note"]).ToString();
        //                registrationDate = DateTime.Parse((dr["registrationDate"]).ToString());
        //                UploadedDate = DateTime.Parse((dr["UploadedDate"]).ToString());
        //                trainingTermId = int.Parse(dr["trainingTermId"].ToString());
        //                trainingYearId = int.Parse(dr["trainingYearId"].ToString());
        //            }

        //            // bind var to the fields
        //            txtRefNo.Text = refNo;
        //            ddInternStatus.SelectedValue = InternStatusId.ToString();
        //            ddIInternGroup.SelectedValue = InternGroupId.ToString();
        //            ddIAdministration.SelectedValue = administrationId.ToString();
        //            txtFName.Text = fname;
        //            txtMName.Text = mi;
        //            txtLastName.Text = lname;
        //            txtGpa.Text = gpa;
        //            ddlinstitution.SelectedValue = institutionId.ToString();
        //            ddlDegree.SelectedValue = degreeId.ToString();
        //            ddlDegreeMajor.SelectedValue = degreeMajorId.ToString();
        //            txtCell.Text = strCell;
        //            txtEmail.Text = email;
        //            txtNote.Text = note;
        //            txtRegistrationDate.Text = registrationDate.ToString();
        //            ddlTrainingTerm.SelectedValue = trainingTermId.ToString();
        //            ddlTrainingYear.SelectedValue = trainingYearId.ToString();

        //            lblOutput.Text = "Please review & update your information!";
        //            lblOutput.ForeColor = System.Drawing.Color.Green;
        //        }
        //        else
        //        {
        //            // diff ways to make the link or transfer to another page 
        //            lblOutput.Text = "No Data Found !  ";
        //            HyperLinkNextPage.Visible = true;
        //            HyperLinkNextPage.Text = " Click to register !";
        //            HyperLinkNextPage.NavigateUrl = "~/internship/internEnroll.aspx";

        //            //Server.Transfer("~/internship/internEnroll.aspx");  // one way 
        //            //common.msg = "No Data Found! Please register!";  // another way
        //            //goToNextPage("internship/internEnroll.aspx");

        //            lblOutput.ForeColor = System.Drawing.Color.Red;
        //            lblOutput.ForeColor = System.Drawing.Color.Red;
        //            return;
        //        }
        //    }
        //}

        // ali you are working on this
        protected void populateInternProfileForm()
        {
            // to populate the inputform from 
            //Response.Write("yyyy/MM/dd"));
            //https://jqueryui.com/datepicker/
            // declare var to capture data returned from db
            string strEmailSearch = "";
            string refNo = "";
            int InternStatusId = 1;  // there is arelationship between the value and what in thecombo 
            int InternGroupId = 1;  // there is arelationship between the value and what in thecombo 
            int administrationId = 1;
            string fname = "";
            string mi = "";
            string lname = "";
            int genderId = 1;
            string strCell = "";
            string email = "";
            int institutionId = 1;
            string gpa = "";
            int degreeId = 1;
            int degreeMajorId = 1;
            int trainingTermId = 1;
            int trainingYearId = 1;
            string note = "";
            int profileComplete = 0;
            int trainingHours = 0;
            string supervisorName = "";
            string supervisorCell = "";
            string supervisorEmail = "";
            DateTime registrationDate = DateTime.Parse("01/01/1900");  // check me
                                                                       //    DateTime UploadedDate = DateTime.Parse("01/01/1900");  // check me
            cbProfileComplete.Checked = false;

            // delcare var and myPara
            string myValueOfWhereClause = "";
            Dictionary<string, object> myPara = new Dictionary<string, object>();

            // runs for an intern only to view his/her profile if the call came from internTrainingSchedule
            ////   if (!String.IsNullOrEmpty(Request.QueryString["internId"]))
            if (person.intPersonId>=1)  ///person.intPersonId.ToString()) means request came from internTrainingSchedule page
            {
             //   Response.Write("pass  is not empty   " + person.intPersonId.ToString()); //.................1
                if (!Roles.IsUserInRole(User.Identity.Name, "admin"))
                {
                //    Response.Write("<BR> intern user then hide buttons for not admin   " + person.intPersonId.ToString()); //.................1
                    // hide search email if user is  not admin
                    btnGetInternProfile.Visible = false;
                    txtEmailSearch.Visible = false;
                    lblEnterYourEmail.Visible = false;
                    //btnSubmit.Visible = false;
                    //btnUpdate.Visible = true;
                    //.. moved
                    myValueOfWhereClause = " where internId = @internId";
                    myPara.Add("@internId", person.intPersonId.ToString());
                }
                else
                {
                    // is user is admin 
              //      Response.Write("<BR>  user  is admin then show  buttons for  admin   " + person.intPersonId.ToString()); //.................1
                    // show button if user is admin or supervisor
                    btnGetInternProfile.Visible = true;
                    txtEmailSearch.Visible = true;
                    lblEnterYourEmail.Visible = true;
                    // if email exist, means need toupdate record, if email does not exits, it is a new record. now
                    //btnSubmit.Visible = common.IsNullOrEmpty(txtEmail.Text) ? true : false;
                    //btnUpdate.Visible = common.IsNullOrEmpty(txtEmail.Text) ? false : true;

                    // the solution 
                    //      Response.Write("<BR> role is admin  and has YES has  search email   " + person.intPersonId.ToString());
                    strEmailSearch = txtEmailSearch.Text;
                    myValueOfWhereClause = "where email = @InernEmail";// need space to avoid sql error 
                    myPara.Add("@InernEmail", strEmailSearch.Trim());
                    // process admin option
                    //       }
                    //   } //...............remove it 
                    //   else
                    // {
                    // give option to admin or supervisor either use querystring or email to pull intern profile
                    // here admin will use querystring passed from previous page 
                    if (Roles.IsUserInRole(User.Identity.Name, "admin") & common.InputIsEmpty(txtEmailSearch.Text) == true)
                    {
                      //  Response.Write("<BR> role is admin  and has no search email   " + person.intPersonId.ToString());
                        myValueOfWhereClause = " where internId = @internId";
                        myPara.Add("@internId", person.intPersonId.ToString());
                    }
                }
            }
            // here admin will use email search instead of internID
            else 
            {
                if (Roles.IsUserInRole(User.Identity.Name, "admin") & common.InputIsEmpty(txtEmailSearch.Text) == true)
                {
                    //// validate email for the admin 
                    //   //if (common.InputIsEmpty(txtEmailSearch.Text))
                    //   //{
                    common.PostMsg(lblOutput, "please enter email ", "red");
                    txtEmailSearch.Focus();
                    return;
                    ////   }
                }
                else
                {
                    //     Response.Write("<BR> role is admin  and has YES has  search email   " + person.intPersonId.ToString());
                    if (Session["email"] != null)
                    { strEmailSearch = Session["email"].ToString(); }  // added on April 10,2020 to fill existing intern to update his/her profile
                    else
                    { strEmailSearch = txtEmailSearch.Text; }
                   
                    myValueOfWhereClause = "where email = @InernEmail";// need space to avoid sql error 
                    myPara.Add("@InernEmail", strEmailSearch.Trim());
                }
            }
              string mySql = @"select  internId,refNo,InternStatusId, InternGroupId,administrationId,fName,mi,lName,genderId,cell,email,institutionId,gpa,
                                degreeId,degreeMajorId,trainingTermId,trainingYearId,note,registrationDate,UploadedDate,profileComplete,trainingHours,
                                supervisorName, supervisorCell, supervisorEmail
                                from intern " + myValueOfWhereClause;
            //  myPara.Add("@InernEmail", strEmailSearch.Trim());   //   strEmailSearch  trim is important to ensure query output  or   common.TrimMyString(StrInternEmail));
            CRUD myCrud = new CRUD();
            using (SqlDataReader dr = myCrud.getDrPassSql(mySql, myPara))
            {
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        hidInternId.Value = dr["internId"].ToString();//int.Parse(dr["internId"].ToString()); // rtn this is static var its scope for the whole page   hidInternId.Value  StaticIntInternId
                        refNo = dr["refno"].ToString();
                        InternStatusId = int.Parse(dr["InternStatusId"].ToString());
                        InternGroupId = int.Parse(dr["InternGroupId"].ToString());
                        administrationId = int.Parse(dr["administrationId"].ToString());
                        fname = dr["fname"].ToString();
                        mi = dr["mi"].ToString();
                        lname = dr["lname"].ToString();
                        genderId = int.Parse(dr["genderId"].ToString());
                        gpa = dr["gpa"].ToString();
                        institutionId = int.Parse(dr["institutionId"].ToString());
                        degreeId = int.Parse(dr["degreeId"].ToString()); // need to work on i t
                        degreeMajorId = int.Parse(dr["degreeMajorId"].ToString()); // need to work on i t
                        strCell = dr["cell"].ToString();
                        email = (dr["email"].ToString());
                        note = (dr["note"]).ToString();
                        trainingHours = string.IsNullOrEmpty(dr["trainingHours"].ToString()) == true ? 0 : int.Parse(dr["trainingHours"].ToString());
                        trainingTermId = int.Parse(dr["trainingTermId"].ToString());
                        trainingYearId = int.Parse(dr["trainingYearId"].ToString());
                        supervisorName = dr["supervisorName"].ToString();
                        supervisorCell = dr["supervisorCell"].ToString();
                        supervisorEmail = dr["supervisorEmail"].ToString();
                    }

                    // bind var to the fields
                    txtRefNo.Text = refNo;
                    ddInternStatus.SelectedValue = InternStatusId.ToString();
                    ddIInternGroup.SelectedValue = InternGroupId.ToString();
                    ddIAdministration.SelectedValue = administrationId.ToString();
                    txtFName.Text = fname;
                    txtMName.Text = mi;
                    txtLastName.Text = lname;
                    ddlGender.SelectedValue = genderId.ToString();
                    txtGpa.Text = gpa;
                    ddlinstitution.SelectedValue = institutionId.ToString();
                    ddlDegree.SelectedValue = degreeId.ToString();
                    ddlDegreeMajor.SelectedValue = degreeMajorId.ToString();
                    txtCell.Text = strCell;
                    txtEmail.Text = email;
                    txtNote.Text = note;
                    cbProfileComplete.Checked = (profileComplete == 0 ? false : true);
                    txtRegistrationDate.Text = registrationDate.ToString();
                    ddlTrainingTerm.SelectedValue = trainingTermId.ToString();
                    ddlTrainingYear.SelectedValue = trainingYearId.ToString();
                    txtTrainingHours.Text = trainingHours.ToString();
                    txtSupervisorName.Text = supervisorName.ToString();
                    txtSupervisorCell.Text = supervisorCell.ToString();
                    txtSupervisorEmail.Text = supervisorEmail.ToString();

                    lblOutput.Text = "Please review & update your information!";
                    lblOutput.ForeColor = System.Drawing.Color.Green;
                }
                else
                {
                    // diff ways to make the link or transfer to another page 
                    lblOutput.Text = "No Data Found ! , Please register by filling requested Information.!";
                    //HyperLinkNextPage.Visible = true;
                    //HyperLinkNextPage.Text = " Click to register !";
                    //HyperLinkNextPage.NavigateUrl = "~/internship/internEnroll.aspx";
                    

                    //Server.Transfer("~/internship/internEnroll.aspx");  // one way 
                    //common.msg = "No Data Found! Please register!";  // another way
                    //goToNextPage("internship/internEnroll.aspx");

                    lblOutput.ForeColor = System.Drawing.Color.Red;
                    lblOutput.ForeColor = System.Drawing.Color.Red;
                    return;
                }
            }

            // disable email search 
            // txtEmailSearch.Enabled = false;
        }
        /// <summary>
        /// one way to populate input form via using the intern key and pull the profile from the DB
        /// This is not the best way, but capturing Gv row and populate the input form is better
        /// </summary>
     
        ////     }

        ////     // disable email search 
        ////     // txtEmailSearch.Enabled = false;
        //// }
        protected void btnEdit2_Click(object sender, EventArgs e)  // to do later rtn
        {
            // this is another way to fill the inputform from the GV
            // this is how you access the gv values through btn > grow > then to find the control 
            // gvDepartments.BackColor = System.Drawing.Color.White; //  
            try
            {
               ////// gvChangeColor();
               ////////.. ClearControls();
               ////// Button btn = sender as Button;
               ////// GridViewRow grow = btn.NamingContainer as GridViewRow;  // ref to the gv row 
               ////// hidCustomerID.Value = (grow.FindControl("lblInernId") as Label).Text;
               ////// txtCustomer.Text = (grow.FindControl("lblCustomer") as Label).Text;
               ////// txtPhoneNumber.Text = (grow.FindControl("lblPhoneNumber") as Label).Text;
               ////// txtAddress.Text = (grow.FindControl("lblAddress") as Label).Text;
               ////// bool ActiveValue = (grow.FindControl("cbActive") as CheckBox).Checked;//this is how to capture checkbox from a gridview
               ////// cbActive.Checked = ActiveValue; //(ActiveValue == false? false: true);
               ////// btnSave.Visible = false;
               ////// btnUpdate.Visible = true;
               ////// grow.BackColor = System.Drawing.Color.Yellow; //     
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void clearInputform()
        {
            // clear input form  
          //..  txtEmailSearch.Text = "";
            txtRefNo.Text = "";
            ddInternStatus.SelectedValue = "1";
            ddIInternGroup.SelectedValue = "1";
            ddIAdministration.SelectedValue = "1";
            txtFName.Text = "";
            txtMName.Text = "";
            txtLastName.Text = "";
            txtCell.Text = "";
            txtEmail.Text = "";
            txtSupervisorName.Text = "";
            txtSupervisorCell.Text = "";
            txtSupervisorEmail.Text = "";
            ddlinstitution.SelectedValue = "1";
            txtGpa.Text = "";
            ddlDegree.SelectedValue = "1";
            ddlDegreeMajor.SelectedValue = "1";
            ddlTrainingTerm.SelectedValue = "1";
            ddlTrainingYear.SelectedValue = "1";
            txtNote.Text = "";
            txtTrainingHours.Text = "";
            cbProfileComplete.Checked = false;
            showHideSubmitUpdate();
            btnInternDoc.Visible = false;   // hide internDoc button so next user does not see previous intern Doc
        }
        protected void CustomValidator1_ServerValidate(object sender, ServerValidateEventArgs e)
        {
            DateTime d;
            e.IsValid = DateTime.TryParseExact(e.Value, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out d);
        }
        protected void txtInternGroup_Unload(object sender, EventArgs e)
        {
            // to avoid max connection pool exceeded
            CRUD.clearAllPools();
        }
        protected void btnClear_Click(object sender, EventArgs e)
        {
            lblOutput.Text = "";
            clearInputform();
        }
        protected void InsertInternTrainingSchedule(int IntInternId)
        {
            string mySql = @"if not exists( select internid from interntrainingschedule where internid = @internId)
                                    insert interntrainingschedule(internId) values(@internId)";
           // string mySql2 = @"insert internTrainingSchedule(internId) values (@internId)";
            CRUD myCrud = new CRUD();
            Dictionary<string, object> myPara = new Dictionary<string, object>();
            myPara.Add("@internId", IntInternId);
            int rtn = myCrud.InsertUpdateDelete(mySql, myPara);
            if (rtn >= 1)
            {
                common.PostMsg(lblOutput, "Intern Profile & Training Schedule created Successfully !", "green");
            }
            else
            {
                common.PostMsg(lblOutput, "Intern Profile & Training Schedule Failed to Create !", "red");
            }
        }
        // added for gv sort and paging
        /// <summary>
        /// Jan 26,2012
        ///  this method can be applied to any GV, No changes is needed. copy and past as is
        /// </summary>
        #region  GV Paging  GV_sort event and Sort Direction
        protected void gvIntern_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            // for paging the gridview
            gvIntern.PageIndex = e.NewPageIndex;
            populateGvIntern();
        }
        protected void gvIntern_Sorting(object sender, GridViewSortEventArgs e)
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
           // showHideButtons();
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

        #endregion
        protected void imgbtn_Click(object sender, ImageClickEventArgs e)
        {
            btnInternDoc.Visible = true; // . restore me after test true;
            ImageButton btndetails = sender as ImageButton;
            GridViewRow gvrow = (GridViewRow)btndetails.NamingContainer;
            //lblID.Text = gvdetails.DataKeys[gvrow.RowIndex].Value.ToString();
            //lblusername.Text = gvrow.Cells[1].Text;
            //txtfname.Text = gvrow.Cells[2].Text;
            //txtlname.Text = gvrow.Cells[3].Text;
            //txtCity.Text = gvrow.Cells[4].Text;
            //txtDesg.Text = gvrow.Cells[5].Text;
            Session["internId"]= gvIntern.DataKeys[gvrow.RowIndex].Value.ToString();
           // lblOutput.Text = gvIntern.DataKeys[gvrow.RowIndex].Value.ToString();
            this.ModalPopupExtender2.Show();
          //btnInternDocDoc.Visible = false;
        }

        protected void gvIntern_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            // to the BulletedList
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int intInternId = int.Parse(gvIntern.DataKeys[e.Row.RowIndex].Values[0].ToString());
                System.Web.UI.WebControls.BulletedList bl = (System.Web.UI.WebControls.BulletedList)e.Row.FindControl("bLInternDoc");
                CRUD myCrud = new CRUD();
                string mySql = @"select interndocId,docName
                                from internDoc
                                where internId=@internId";
                Dictionary<string, object> myPara = new Dictionary<string, object>();
                myPara.Add("@internId", intInternId);
                SqlDataReader dr = myCrud.getDrPassSql(mySql, myPara);
                bl.DataSource = dr;
                bl.DataBind();
                //int intInternId = int.Parse(gvIntern.DataKeys[e.Row.RowIndex].Values[0].ToString());
                //LinkButton bl = (LinkButton)e.Row.FindControl("bLInternDoc");
                //CRUD myCrud = new CRUD();
                //string mySql = @"select interndocId,docName
                //                from internDoc
                //                where internId=@internId";
                //Dictionary<string, object> myPara = new Dictionary<string, object>();
                //myPara.Add("@internId", intInternId);
                //DataTable td = myCrud.getDTPassSqlDic(mySql, myPara);
                //gvDocs.DataSource = td;
                //gvDocs.DataBind();

            }
        }
        protected void DownloadFile(object sender, EventArgs e)  // move it to common 
        {// move it to common
            if (IsPostBack)
            {
                int InternDocId = int.Parse((sender as LinkButton).CommandArgument);
                byte[] bytes;
                string fileName, contentType;
                //  string constr = WebConfigurationManager.ConnectionStrings["employeeConnectionString"].ConnectionString;
                string constr = CRUD.conStr;
                using (SqlConnection con = new SqlConnection(constr))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandText = @"  select  docName,docData from InternDoc
                                              where internDocId = @internDocId";
                        cmd.Parameters.AddWithValue("@internDocId", InternDocId);
                        cmd.Connection = con;
                        con.Open();
                        using (SqlDataReader sdr = cmd.ExecuteReader())
                        {
                            if (sdr.HasRows)
                            {
                                sdr.Read();
                                bytes = (byte[])sdr["DocData"];

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
                //Response.ContentType = contentType;
                Response.AppendHeader("Content-Disposition", "attachment; filename=" + fileName);
                Response.BinaryWrite(bytes);
                Response.Flush();
                Response.End();
            }
        }
        protected void sendEmail(object sender, EventArgs e)
        {
            LinkButton lb = sender as LinkButton;
            Session["email"] = lb.CommandArgument;
            Response.Redirect("~/contactUs.aspx");

            //   lblOutput.Text = (Session["email"].ToString());
        }
    }// cls
}// ns