using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using kfmc.App_Code;

namespace kfmc.employee
{
    public partial class employeeDirectory : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                populateAdminDDL();
              //  PopulateDepDDL();
            }
        }

        protected void populateAdminDDL()
        {
            // all code to get employee data from database in one method into drop down list
            CRUD myCrud = new CRUD();
            string mySql = @"select administrationId,administration from administration ";
            using (SqlDataReader dr = myCrud.getDrPassSql(mySql))
            {
                ddlAdmin.DataValueField = "administrationId";
                ddlAdmin.DataTextField = "administration";
                ddlAdmin.DataSource = dr;
                ddlAdmin.DataBind();
            }
        }
        protected void PopulateDepDDL(int adminId)
        {
            // all code to get employee data from database in one method into drop down list
            CRUD myCrud = new CRUD();
            string mySql = @"select departmentId, department from department
                                where administrationId = @administrationId";
            Dictionary<string, object> myPara = new Dictionary<string, object>();
            myPara.Add("@administrationId", adminId);
            using (SqlDataReader dr = myCrud.getDrPassSql(mySql,myPara))
            {
                ddlDep.DataValueField = "departmentId";
                ddlDep.DataTextField = "department";
                ddlDep.DataSource = dr;
                ddlDep.DataBind();
            }
        }
        protected void ddlAdmin_SelectedIndexChanged(object sender, EventArgs e)
        {
            int adminId = int.Parse(ddlAdmin.SelectedValue);
            PopulateDepDDL(adminId);
            common.clearLblOutputContent(lblOutput);
  
        }

        protected void btn_Click(object sender, EventArgs e)
        {
            // all code to get employee data from database in one method into drop down list
            CRUD myCrud = new CRUD();
            string mySql = @"  select employeeNo,employeeAr,employeeEn,JobEn,ext,email ,cell
                             from employee where departmentId =@departmentId";
            int myDepId = ( string.IsNullOrEmpty(ddlDep.SelectedValue.ToString()))? 0 : int.Parse(ddlDep.SelectedValue);
            if (myDepId == 0)
            {
                //lblOutput.Text = "Please make department selection";
                App_Code.common.PostMsg(lblOutput, "Please select a department!");
                return;
            }
            Dictionary<string, object> myPara = new Dictionary<string, object>();
            myPara.Add("@departmentId", myDepId);
            using (SqlDataReader dr = myCrud.getDrPassSql(mySql,myPara))
            {
                gvEmployee.DataSource = dr;
                gvEmployee.DataBind();
            }
            common.clearLblOutputContent(lblOutput);
        }


    }
}