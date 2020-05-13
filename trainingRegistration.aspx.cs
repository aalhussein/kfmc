using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using kfmc.App_Code;

namespace kfmc.employee
{
    public partial class trainingRegistration : System.Web.UI.Page
    {
      // SqlConnection con = new SqlConnection(CRUD.conStr); //  new SqlConnection(@"Data Source=T450S\SQLEXPRESS;persist security info=True;Integrated Security=SSPI; Initial Catalog=party;");
        protected void Page_Load(object sender, EventArgs e)
        {
            txtFname.Focus();
            if (!IsPostBack)
            {
                //FillGrid();
                populateGvContact();
                populateComboContactType();
            }
        }
        protected void populateComboContactType()
        {
            // ddlAdministration.ClearSelection();
            CRUD myCrud = new CRUD();
            string mySql = @" select contactTypeId, contactType 
                                from contactType  
                                order by contactTypeId";
            myCrud.populateCombo(ddlContactType, mySql, "contactTypeId", "contactType");
        }
        protected void populateGvContact()
        {
            CRUD myCrud = new CRUD();
            string mySql = @"Select trainingRegistrationId,fname,mi,lName, tr.contactTypeId, contactType , cell,email ,IsActive ,Note
                            from trainingRegistration tr  inner join contactType ct on tr.contactTypeId = ct.contactTypeId
                            order by fname ";
            using (SqlDataReader dr = myCrud.getDrPassSql(mySql))
            {
                gvCustomer.DataSource = dr;
                gvCustomer.DataBind();
            }
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
            if (String.IsNullOrEmpty(txtFname.Text))
            {
                lblOutput.Text = "Please fill First Name !";
                lblOutput.ForeColor = System.Drawing.Color.Red;
                txtFname.Focus();
                return;
            }
            if (String.IsNullOrEmpty(txtMi.Text))
            {
                lblOutput.Text = "Please fill Mi !";
                lblOutput.ForeColor = System.Drawing.Color.Red;
                txtMi.Focus();
                return;
            }
            if (String.IsNullOrEmpty(txtLname.Text))
            {
                lblOutput.Text = "Please fill Last Name !";
                lblOutput.ForeColor = System.Drawing.Color.Red;
                txtLname.Focus();
                return;
            }
            //if (String.IsNullOrEmpty(txtCell.Text))
            //{
            //    lblOutput.Text = "Please fill Cell number !";
            //    lblOutput.ForeColor = System.Drawing.Color.Red;
            //    txtCell.Focus();
            //    return;
            //}
            if (String.IsNullOrEmpty(txtEmail.Text))
            {
                lblOutput.Text = "Please fill the Email !";
                lblOutput.ForeColor = System.Drawing.Color.Red;
                txtEmail.Focus();
                return;
            }
            int intActive = (cbActive.Checked ? 1 : 0);
            
            try
            {
                CRUD myCrud = new CRUD();
                string mySql = @"insert into trainingRegistration (fName,mi,Lname,cell,email, IsActive,  contactTypeId,note) 
                                    values (@fName,@mI,@lName,@cell, @email, @isActive,@contactTypeId,@note)";
                Dictionary<string, object> myPara = new Dictionary<string, object>();
                myPara.Add("@fName", txtFname.Text);
                myPara.Add("@mI", txtMi.Text);
                myPara.Add("@lName", txtLname.Text);
                myPara.Add("@cell", txtCell.Text);
                myPara.Add("@email", txtEmail.Text);
                myPara.Add("@isActive", intActive);
                myPara.Add("@contactTypeId", ddlContactType.SelectedValue); // it is not selecttedItem.Value but SelectedValue
                myPara.Add("@note", txtNote.Text);
                  rtn  =  myCrud.InsertUpdateDelete(mySql, myPara);
                if (rtn >= 1)
                {
                    common.PostMsg(lblOutput,"Success ","green");
                }
                else
                {
                    common.PostMsg(lblOutput, "Failed ","red");
                }
                populateGvContact();
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
                gvChangeColor();
                ClearControls();
                Button btn = sender as Button;
                GridViewRow grow = btn.NamingContainer as GridViewRow;  // ref to the gv row 
                HdntrainingRegistrationId.Value = (grow.FindControl("lblTrainingRegistrationId") as Label).Text;
                ddlContactType.SelectedValue = (grow.FindControl("lblContactTypeId") as Label).Text;  // new
                txtFname.Text = (grow.FindControl("lblFname") as Label).Text;
                txtMi.Text = (grow.FindControl("lblMi") as Label).Text;
                txtLname.Text = (grow.FindControl("lblLname") as Label).Text;
                txtCell.Text = (grow.FindControl("lblCell") as Label).Text;
                txtEmail.Text = (grow.FindControl("lblEmail") as Label).Text;
                txtNote.Text = (grow.FindControl("lblNote") as Label).Text;
                bool ActiveValue  = (grow.FindControl("cbActive") as CheckBox).Checked ;//this is how to capture checkbox from a gridview
                cbActive.Checked = ActiveValue; //(ActiveValue == false? false: true);

                btnSave.Visible = false;
                btnUpdate.Visible = true;
                grow.BackColor = System.Drawing.Color.Yellow; //     
            }
            catch (Exception ex)
            {
                //throw ex;
                 lblOutput.Text =  ex.Message.ToString();
            }
        }
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            //  gvDepartments.BackColor = System.Drawing.Color.White; //  
            try
            {
                CRUD myCrud = new CRUD();
                string mySql = @"update trainingRegistration set fname=@fName,mi=@mI,lName =@lName,contactTypeId =@contactTypeId ,cell=@cell,
                                    email=@email,IsActive=@isActive, note=@note
                                    where trainingRegistrationId=@trainingRegistrationId";
                Dictionary<string, object> myPara = new Dictionary<string, object>();
                myPara.Add("@trainingRegistrationId", HdntrainingRegistrationId.Value);  // captured the id from edit button
                myPara.Add("@fName", txtFname.Text);
                myPara.Add("@mI", txtMi.Text);
                myPara.Add("@lName", txtLname.Text);
                myPara.Add("@contactTypeId", int.Parse(ddlContactType.SelectedValue) );
                myPara.Add("@cell", txtCell.Text);
                myPara.Add("@email", txtEmail.Text);
                myPara.Add("@isActive", cbActive.Checked == false ? 0 : 1);
                myPara.Add("@note", txtNote.Text);
                int rtn =   myCrud.InsertUpdateDelete(mySql, myPara);
                App_Code.common.PostMsg(lblOutput, rtn);
            }
            catch (Exception ex)
            {
               // App_Code.common.PostMsg(lblOutput, ex.Message.ToString());
                App_Code.common.PostMsg(lblOutput,ex.Message.ToString());
            }
            populateGvContact();
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
                int inttrainingRegistrationId = int.Parse( (grow.FindControl("lblTrainingRegistrationId") as Label).Text);
                CRUD myCrud = new CRUD();
                string mySql = @"delete  trainingRegistration  where trainingRegistrationId=@trainingRegistrationId";
                Dictionary<string, object> myPara = new Dictionary<string, object>();
                myPara.Add("@trainingRegistrationId", inttrainingRegistrationId);
               int rtn =   myCrud.InsertUpdateDelete(mySql, myPara);
                App_Code.common.PostMsg(lblOutput, rtn);
           }
            catch ( Exception ex)
            {
                //  throw ex;
                App_Code.common.PostMsg(lblOutput, ex.Message.ToString());
            }

            populateGvContact();
        }
        protected void ClearControls()
        {
            try
            {
                common.clearLblOutputContent(lblOutput);
                //lblOutput.Text = "";
                txtFname.Text = "";
                txtMi.Text = "";
                txtLname.Text = "";
                txtCell.Text = "";
                txtEmail.Text = "";
                cbActive.Checked = false;
                ddlContactType.SelectedIndex = 0;
                txtNote.Text = "";
                HdntrainingRegistrationId.Value = "";
                btnSave.Visible = true;
                btnUpdate.Visible = false;
            }
            catch
            {
                throw;
            }
        }
        #endregion
        protected void gvChangeColor()
        {
            foreach (GridViewRow row in gvCustomer.Rows)
            {
                if (row.RowIndex == gvCustomer.SelectedIndex)
               {
                   row.BackColor = System.Drawing.Color.Green;  // not applicable
                }
                else
                {
                    row.BackColor = System.Drawing.Color.White;//ColorTranslator.FromHtml("#FFFFFF");
                }
            }
        }
    } // cls
} // NS