using AjaxControlToolkit;
using kfmc.App_Code;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace kfmc.office
{
    public partial class popUpCourse : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            embedDateTimeVal(txtStartDate);      // passing the controld that hold date 
            embedDateTimeVal(txtEndDate);
            txtCourse.Focus();
        }
        protected void embedDateTimeVal(TextBox txtBox)
        { // using Ajax tools to pop the calender from a textbox
            ControlCollection cc = txtBox.Parent.Controls;
            string[] spl = txtBox.ClientID.Split(new char[] { '_' });
            CalendarExtender ce = new CalendarExtender();
            ce.ID = txtBox.ID + "_ce";
            ce.TargetControlID = spl[spl.Length - 1];
            cc.Add(ce);
        }
        #region CRUD ops
        protected void btnClear_Click(object sender, EventArgs e)
        {
            ClearControls();
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {  // sample of long code to insert data into db
            int rtn = 0;
            // validation is working but there is duplicate for last one
            //if (common.IsNullOrEmpty(txtContact, lblOutput)) return;
            //if (common.IsNullOrEmpty(txtPhoneNumber, lblOutput)) return;
            //if (common.IsNullOrEmpty(txtEmail, lblOutput)) return;
            if (String.IsNullOrEmpty(txtCourse.Text))
            {
                lblOutput.Text = "Please fill Course Name !";
                lblOutput.ForeColor = System.Drawing.Color.Red;
                txtCourse.Focus();
                return;
            }
            if (String.IsNullOrEmpty(txtStartDate.Text))
            {
                lblOutput.Text = "Please fill Start Date!";
                lblOutput.ForeColor = System.Drawing.Color.Red;
                txtStartDate.Focus();
                return;
            }
            if (String.IsNullOrEmpty(txtEndDate.Text))
            {
                lblOutput.Text = "Please fill End Date!";
                lblOutput.ForeColor = System.Drawing.Color.Red;
                txtEndDate.Focus();
                return;
            }
            //if (String.IsNullOrEmpty(txtCell.Text))
            //{
            //    lblOutput.Text = "Please fill Cell number !";
            //    lblOutput.ForeColor = System.Drawing.Color.Red;
            //    txtCell.Focus();
            //    return;
            //}
            if (String.IsNullOrEmpty(txtVenue.Text))
            {
                lblOutput.Text = "Please fill Venue!";
                lblOutput.ForeColor = System.Drawing.Color.Red;
                txtVenue.Focus();
                return;
            }
            int intActive = (cbActive.Checked ? 1 : 0);

            try
            {
                CRUD myCrud = new CRUD();
                string mySql = @"insert into course (course,startDate,endDate,venue,note) 
                                    values (@course,@startDate,@endDate,@venue,@note)" +
                                                     "SELECT CAST(scope_identity() AS int)";
                Dictionary<string, object> myPara = new Dictionary<string, object>();
                myPara.Add("@course", txtCourse.Text);
                myPara.Add("@startdate", txtStartDate.Text);
                myPara.Add("@endDate", txtEndDate.Text);
                myPara.Add("@venue", txtVenue.Text);
                myPara.Add("@note", txtNote.Text);
                int intCourseId = myCrud.InsertUpdateDeleteViaSqlDicRtnIdentity(mySql, myPara);
                int confirm = 0;
                if (intCourseId >= 1)
                {
                    lblOutput.Text = "Success!";
                    lblOutput.ForeColor = System.Drawing.Color.Green;
                    if (FileUpload.HasFiles)
                    {
                        confirm = InsertDocuments(intCourseId);
                        if (confirm >= 1)
                        {
                            lblOutput.Text = "Failed!";
                            lblOutput.ForeColor = System.Drawing.Color.Red;
                        }
                        else
                        {
                            lblOutput.Text = "Failed!";
                            lblOutput.ForeColor = System.Drawing.Color.Red;
                        }
                    }
                }
                else
                {
                    lblOutput.Text = "Failed!";
                    lblOutput.ForeColor = System.Drawing.Color.Red;
                }
                ClearControls();  // it taks out label too. so ignore it 

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void btnEdit_Click(object sender, EventArgs e)
        {
            common.clearLblOutputContent(lblOutput);
            ClearControls();
            // this is how you access the gv values through btn > grow > then to find the control 
            // gvDepartments.BackColor = System.Drawing.Color.White; //  
            try
            {
                ClearControls();
                Button btn = sender as Button;
                GridViewRow grow = btn.NamingContainer as GridViewRow;  // ref to the gv row 
                hdnCourseId.Value = (grow.FindControl("lblCourseId") as Label).Text;
                txtCourse.Text = (grow.FindControl("lblCourse") as Label).Text;
                txtStartDate.Text = (grow.FindControl("lblStartDate") as Label).Text;
                txtEndDate.Text = (grow.FindControl("lblEndDate") as Label).Text;
                txtVenue.Text = (grow.FindControl("lblVenue") as Label).Text;
                txtNote.Text = (grow.FindControl("lblNote") as Label).Text;
                btnSave.Visible = false;
                btnUpdate.Visible = true;
                grow.BackColor = System.Drawing.Color.Yellow; //     
            }
            catch (Exception ex)
            {
                //throw ex;
                lblOutput.Text = ex.Message.ToString();
            }
        }
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            //  gvDepartments.BackColor = System.Drawing.Color.White; //  
            try
            {
                CRUD myCrud = new CRUD();
                string mySql = @"update course set course=@course,startDate=@startDate,
                            endDate =@endDate, venue =@venue, note=@note
                             where trainingRegistrationId=@trainingRegistrationId";
                Dictionary<string, object> myPara = new Dictionary<string, object>();
                myPara.Add("@trainingRegistrationId", hdnCourseId.Value);  // captured the id from edit button
                myPara.Add("@course", txtCourse.Text);
                myPara.Add("@startdate", txtStartDate.Text);
                myPara.Add("@endDate", txtEndDate.Text);
                myPara.Add("@venue", txtVenue.Text);
                myPara.Add("@note", txtNote.Text);
                int rtn = myCrud.InsertUpdateDelete(mySql, myPara);
                App_Code.common.PostMsg(lblOutput, rtn);
            }
            catch (Exception ex)
            {
                // App_Code.common.PostMsg(lblOutput, ex.Message.ToString());
                App_Code.common.PostMsg(lblOutput, ex.Message.ToString());
            }
            ClearControls();
            btnSave.Visible = true;
            btnUpdate.Visible = false;
        }
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            // shows how to access gvr when the sender is a button inside the gv
            try
            {
                common.clearLblOutputContent(lblOutput);
                ClearControls();
                Button btn = sender as Button;
                GridViewRow grow = btn.NamingContainer as GridViewRow;
                int inttrainingRegistrationId = int.Parse((grow.FindControl("lblTrainingRegistrationId") as Label).Text);
                CRUD myCrud = new CRUD();
                string mySql = @"delete  trainingRegistration  where trainingRegistrationId=@trainingRegistrationId";
                Dictionary<string, object> myPara = new Dictionary<string, object>();
                myPara.Add("@trainingRegistrationId", inttrainingRegistrationId);
                int rtn = myCrud.InsertUpdateDelete(mySql, myPara);
                App_Code.common.PostMsg(lblOutput, rtn);
            }
            catch (Exception ex)
            {
                //  throw ex;
                App_Code.common.PostMsg(lblOutput, ex.Message.ToString());
            }
        }
        protected void ClearControls()
        {
            try
            {
                common.clearLblOutputContent(lblOutput);
                //lblOutput.Text = "";
                txtCourse.Text = "";
                txtStartDate.Text = "";
                txtEndDate.Text = "";
                txtVenue.Text = "";
                hdnCourseId.Value = "";
                btnSave.Visible = true;
                btnUpdate.Visible = false;
            }
            catch
            {
                throw;
            }
        }
        #endregion
        protected int InsertDocuments(int myCourseId)
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
                            string mySql = @"insert into courseDoc(courseId,DocName,ContentType,DocData)
                                    values (@courseId,@DocName,@ContentType,@DocData)";
                            Dictionary<string, Object> p = new Dictionary<string, object>();
                            //p.Add("@TaskId", "get the value ");
                            p.Add("@courseId", myCourseId);  // added Nov 2017
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
    }
}