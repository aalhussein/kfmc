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
    public partial class contactDirectory : System.Web.UI.Page
    {
      // SqlConnection con = new SqlConnection(CRUD.conStr); //  new SqlConnection(@"Data Source=T450S\SQLEXPRESS;persist security info=True;Integrated Security=SSPI; Initial Catalog=party;");
        protected void Page_Load(object sender, EventArgs e)
        {
            txtContact.Focus();
            if (!IsPostBack)
            {
                //FillGrid();
                populateGvContact();
                populateComboAdministration();
                populateComboDepartment();
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
        protected void populateComboAdministration()
        {
           // ddlAdministration.ClearSelection();
            CRUD myCrud = new CRUD();
            string mySql = @" select administrationId, administration 
                from administration3  
               
                order by administrationid";
            myCrud.populateCombo(ddlAdministration, mySql, "AdministrationId", "Administration");
        }
        protected void populateComboDepartment()
        {
            //ddlDepartment.ClearSelection();     where administrationId= @administrationId
            CRUD myCrud = new CRUD();
            string mySql = @"select departmentId, department 
                            from department3
                            order by departmentid";
            //Dictionary<string, object> myPara = new Dictionary<string, object>();
            //myPara.Add("@administrationId", ddlAdministration.SelectedValue);
            myCrud.populateCombo(ddlDepartment, mySql, "departmentId", "department");  //,myPara);
        }
        protected void populateGvContact()
        {
            CRUD myCrud = new CRUD();
            string mySql = @"Select contactId,contact, c.contactTypeId, contactType,c.administrationid,administration, c.departmentId,department
                            , PhoneNumber,Address ,IsActive ,Note
                    from contact c inner join administration a on c.administrationId=a.administrationId
                    inner join department d on c.departmentId = d.departmentId
                    inner join contactType ct on c.contactTypeId = ct.contactTypeId
                    order by contact ";
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
            if (String.IsNullOrEmpty(txtContact.Text))
            {
                lblOutput.Text = "Please fill the Contact !";
                lblOutput.ForeColor = System.Drawing.Color.Red;
                txtContact.Focus();
                return;
            }
            if (String.IsNullOrEmpty(txtPhoneNumber.Text))
            {
                lblOutput.Text = "Please fill the Phone Number !";
                lblOutput.ForeColor = System.Drawing.Color.Red;
                txtPhoneNumber.Focus();
                return;
            }
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
                string mySql = @"insert into contact (contact,PhoneNumber,Address,IsActive, administrationId, departmentId, contactTypeId,note) 
                                    values (@contact,@PhoneNumber,@Address,@isActive,@administrationId, @departmentId,@contactTypeId,@note)";
                Dictionary<string, object> myPara = new Dictionary<string, object>();
                myPara.Add("@contact", txtContact.Text);
                myPara.Add("@PhoneNumber", txtPhoneNumber.Text);
                myPara.Add("@Address", txtEmail.Text);
                myPara.Add("@isActive", intActive);
                myPara.Add("@administrationId", ddlAdministration.SelectedValue); // it is not selecttedItem.Value but SelectedValue
                myPara.Add("@departmentId", ddlDepartment.SelectedValue);
                myPara.Add("@contactTypeId", ddlContactType.SelectedValue);
                myPara.Add("@note", txtNote.Text);
                  rtn  =  myCrud.InsertUpdateDelete(mySql, myPara);
                if (rtn >= 1)
                {
                    common.PostMsg(lblOutput,rtn);
                }
                else
                {
                    common.PostMsg(lblOutput, rtn);
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
                hidCustomerID.Value = (grow.FindControl("lblContactId") as Label).Text;
                ddlAdministration.SelectedValue  = (grow.FindControl("lblAdministrationId") as Label).Text; // to populate inputform from gv
                ddlDepartment.SelectedValue = (grow.FindControl("lblDepartmentId") as Label).Text;  // new
                ddlContactType.SelectedValue = (grow.FindControl("lblContactTypeId") as Label).Text;  // new
                txtContact.Text = (grow.FindControl("lblCustomer") as Label).Text;
                txtPhoneNumber.Text = (grow.FindControl("lblPhoneNumber") as Label).Text;
                txtEmail.Text = (grow.FindControl("lblAddress") as Label).Text;
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
                string mySql = @"update contact set contact=@contact,PhoneNumber=@PhoneNumber,
                                    Address=@Address,IsActive=@isActive, administrationId = @administrationId, 
                                    departmentId=@departmentId,contactTypeId=@contactTypeId, note=@note
                                    where contactId=@contactId";
                Dictionary<string, object> myPara = new Dictionary<string, object>();
                myPara.Add("@contact", txtContact.Text);
                myPara.Add("@PhoneNumber", txtPhoneNumber.Text);
                myPara.Add("@Address", txtEmail.Text);
                myPara.Add("@isActive", cbActive.Checked == false ? 0 : 1);
                myPara.Add("@contactId", hidCustomerID.Value);
                myPara.Add("@administrationId",  ddlAdministration.SelectedValue); // not selectedItem.Value     .SelectedItem.Value
                myPara.Add("@departmentId", ddlDepartment.SelectedValue);
                myPara.Add("@contactTypeId", ddlContactType.SelectedValue); // not selectedItem.Value     .SelectedItem.Value
                myPara.Add("@note", txtNote.Text);
                int rtn =   myCrud.InsertUpdateDelete(mySql, myPara);
                App_Code.common.PostMsg(lblOutput, rtn);
            }

            catch (Exception ex)
            {
                App_Code.common.PostMsg(lblOutput, ex.Message.ToString());
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
                int intContactId = int.Parse( (grow.FindControl("lblContactId") as Label).Text);
                CRUD myCrud = new CRUD();
                string mySql = @"delete  contact  where contactId=@contactId";
                Dictionary<string, object> myPara = new Dictionary<string, object>();
                myPara.Add("@contactId", intContactId);
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
                txtContact.Text = "";
                txtPhoneNumber.Text = "";
                txtEmail.Text = "";
                cbActive.Checked = false;
                ddlAdministration.SelectedIndex = 0;  // not selectedItem.value
                ddlDepartment.SelectedIndex = 0;
                ddlContactType.SelectedIndex = 0;
                txtNote.Text = "";
                hidCustomerID.Value = "";
                btnSave.Visible = true;
                btnUpdate.Visible = false;
            }
            catch
            {
                throw;
            }
        }
        #endregion
        //protected void btnSave2_Click(object sender, EventArgs e)
        //{
        //    string strCustomer = txtCustomer.Text;
        //   string strPhoneNum =  txtPhoneNumber.Text;
        //    string strAddress = txtEmail.Text;
        //    int intActive = (cbActive.Checked ? 1 : 0);
        //            CRUD myCrud = new CRUD();
        //    string mySql = @"insert into contact (contact,PhoneNumber,Address,IsActive) 
        //                            values (@contact,@PhoneNumber,@Address,@isActive)";
        //    Dictionary<string, object> myPara = new Dictionary<string, object>();
        //    myPara.Add("@contact", strCustomer);
        //    myPara.Add("@phoneNumber",strPhoneNum);
        //    myPara.Add("@Address", strAddress);
        //    myPara.Add("@isActive", intActive);
        //    int rtn = myCrud.InsertUpdateDelete(mySql, myPara);
        //    confirmCrudOutput(rtn);
        //}
        protected void confirmCrudOutput (int rtn)
        {
            if (rtn >= 1)
            {
                lblOutput.Text = "Success";
                lblOutput.ForeColor = System.Drawing.Color.Green;
            }
            else
            {
                lblOutput.Text = "Failed";
                lblOutput.ForeColor = System.Drawing.Color.Red;
            }
        }
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
        protected void ddlAdministration_SelectedIndexChanged(object sender, EventArgs e)
        {
            common.PostMsg(lblOutput, "");
         //   ddlDepartment.ClearSelection();
            string mySql = @"Select departmentId, department
                    from Department 
                    where administrationid=@administrationId order by departmentId";
            Dictionary<string, object> myPara = new Dictionary<string, object>();
            myPara.Add("@administrationId", int.Parse(ddlAdministration.SelectedValue));  // not selectedItem.value
            CRUD myCrud = new CRUD();
            myCrud.populateCombo(ddlDepartment, mySql, "departmentId", "department", myPara);
        }
        //protected void btnUpdate2_Click(object sender, EventArgs e)
        //{
        //    string strCustomer = txtCustomer.Text;
        //    string strPhoneNum = txtPhoneNumber.Text;
        //    string strAddress = txtEmail.Text;
        //    int intActive = (cbActive.Checked ? 1 : 0);
        //    CRUD myCrud = new CRUD();
        //    string mySql = @"UPDATE dbo.contact
        //                    SET contact = @contact,PhoneNumber =@PhoneNumber,Address = @Address,isActive =@isActive
        //                    where contactId = @contactId";
        //    Dictionary<string, object> myPara = new Dictionary<string, object>();
        //    myPara.Add("@contact", strCustomer);
        //    myPara.Add("@phoneNumber", strPhoneNum);
        //    myPara.Add("@Address", strAddress);
        //    myPara.Add("@isActive", intActive);
        //    int rtn = myCrud.InsertUpdateDelete(mySql, myPara);
        //    App_Code.common.PostMsg(lblOutput, rtn);
        //}
    } // cls
} // NS