using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

namespace kfmc
{
    public partial class activityRegistration : System.Web.UI.Page
    {
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
                 populateddlAdmin();
                //  populateCBLEmployee();
                populateddlActivityType();
                populateCBLActivityList();
              
            }
        }
        protected void populateddlActivityType()
        {
            CRUD myCrud = new CRUD();
            string mySql = @"SELECT activityTypeId ,activityType
                                FROM activityType";
            using (SqlDataReader dr = myCrud.getDrPassSql(mySql))
            {
                ddlActivityType.DataValueField = "activityTypeId";
                ddlActivityType.DataTextField = "activityType";
                ddlActivityType.DataSource = dr;
                ddlActivityType.DataBind();
            }
        }
        protected void populateddlAdmin()
        {
            // all code to get employee data from database in one method into drop down list
            CRUD myCrud = new CRUD();
            string mySql = @"SELECT distinct  administrationId, administration
                                FROM     administration   ";
            using (SqlDataReader dr = myCrud.getDrPassSql(mySql))
            {
                ddlAdmin.DataValueField = "administrationId";
                ddlAdmin.DataTextField = "administration";
                ddlAdmin.DataSource = dr;
                ddlAdmin.DataBind();
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
        //            cblEmployee.DataValueField = "empid";
        //            cblEmployee.DataTextField = "employeeEn";
        //            cblEmployee.DataSource = dr;
        //            cblEmployee.DataBind();
        //        }
        //    }
        //}
        protected void populateCBLActivityList()
        {
            // all code to get employee data from database in one method into radio botton list
            int intActivityTypeId = int.Parse(ddlActivityType.SelectedValue);
            CRUD myCrud = new CRUD();
            string mySql = @"select activityId, activityName from activity
                            where activityTypeId = @activityTypeId";
            Dictionary<string, object> myPara = new Dictionary<string, object>();
            myPara.Add("@activityTypeId", intActivityTypeId);
            using (SqlDataReader dr = myCrud.getDrPassSql(mySql, myPara))
            {
                cblActivity.DataValueField = "activityId";
                cblActivity.DataTextField = "activityName";
                cblActivity.DataSource = dr;
                cblActivity.DataBind();
            }
          }
        protected void btnRegister_Click(object sender, EventArgs e)
        {
            foreach (ListItem item in cblEmployee.Items)
            {
                if (item.Selected)
                {
                    totalSelectedItems += 1;
                }
            }
              for (int i = 0; i< cblEmployee.Items.Count; i++)
            {
                if (cblEmployee.Items[i].Selected)
                {
                    int empId = 0;
                    counter += 1;
                    empId =  int.Parse(cblEmployee.Items[i].Value);
                     for (int ii = 0; ii < cblActivity.Items.Count; ii++)
                    {
                        if (cblActivity.Items[ii].Selected)
                        {
                            int myActivityId = 0;
                            myActivityId = int.Parse(cblActivity.Items[ii].Value);
                            //call method to regiser
                            registerEmp(empId, myActivityId);
                        }
                    }
                }
            }
        }
        protected void registerEmp(int myEmployeeId, int myActivityId)
        {
            //selectedCourses += myEmployeeId + " " + myCourseName;
            //lblOutput.Text = selectedCourses;
            string mySql = @"INSERT INTO activityRegistration
                           (employeeId ,activityId )
                            VALUES(@employeeId,@activityId)";
            CRUD myCrud = new CRUD();
            Dictionary<string, object> myPara = new Dictionary<string, object>();
            myPara.Add("@employeeId", myEmployeeId);
            myPara.Add("@activityId", myActivityId);
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
                cblEmployee.DataValueField = "employeeId";
                cblEmployee.DataTextField = "employeeEn";
                cblEmployee.DataSource = dr;
                cblEmployee.DataBind();
            }
        }

        protected void populateEmployeeCheckBoxList(string mySql, Dictionary<string, object> myPara)
        {
            CRUD myCrud = new CRUD();
            using (DataTable dt = myCrud.getDTPassSqlDic(mySql, myPara))
            {
                cblEmployee.DataValueField = "employeeId";
                cblEmployee.DataTextField = "employeeEn";
                cblEmployee.DataSource = dt;
                cblEmployee.DataBind();


                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    cblEmployee.Items[i].Text = @" <a href='employee/empTraining.aspx" + "' " +
                        "                               target='_blank' title='View Document'>"
                                                        + cblEmployee.Items[i].Text + "</a>";
                }
            }
        }

    protected void ddlAdmin_SelectedIndexChanged(object sender, EventArgs e)
        {
            // clear emplList 
            cblEmployee.Items.Clear();
            int mySelectedAdminId = int.Parse(ddlAdmin.SelectedValue);
            PopulateDepDDL(mySelectedAdminId);
            int mySelectedDepId = int.Parse(ddlDep.SelectedValue); 
            string mySql = @"SELECT employeeId,employeeEn 
                            FROM employee";
            Dictionary<string, object> myPara = new Dictionary<string, object>();
            if ( mySelectedDepId == 0)
            {
                mySql += " where administrationId = @administrationId order by employeeEn";
                myPara.Add("@administrationId", mySelectedAdminId);
                populateEmployeeCheckBoxList(mySql, myPara);
            }
        }
        protected void ddlDep_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblOutput.Text = "";
            // if no depid is selected, choose zero 
            int mySelectedAdminId = int.Parse(ddlAdmin.SelectedValue);
            int mySelectedDepId = int.Parse(ddlDep.SelectedValue); //(int.Parse(ddlDep.SelectedValue)) >= 1 ? int.Parse(ddlDep.SelectedValue) : 0;
          //  lblOutput.Text = mySelectedAdminId + "" + mySelectedDepId;
            string mySql = @"SELECT employeeId,employeeEn 
                            FROM employee";
            // create another dic for 2nd query 
            Dictionary<string, object> myPara = new Dictionary<string, object>();
            int NoOfDepForAdminId = ddlDep.Items.Count;
            if (NoOfDepForAdminId > 1 &  mySelectedDepId >=1)
            {
                //lblOutput.Text = NoOfDepForAdminId.ToString();
                mySql += " where administrationId = @administrationId and departmentId = @departmentId order by employeeEn";
                myPara.Add("@administrationId", mySelectedAdminId);
                myPara.Add("@departmentId", mySelectedDepId);
                populateEmployeeCheckBoxList(mySql, myPara);
              /// populateCBLEmployee();
            }
           else if (NoOfDepForAdminId > 1 & mySelectedDepId == 0)
            {
                mySql += " where administrationId = @administrationId order by employeeEn";
                myPara.Add("@administrationId", mySelectedAdminId);
                populateEmployeeCheckBoxList(mySql, myPara);
            }
        }
        protected void PopulateDepDDL(int adminId)
        {
            // all code to get employee data from database in one method into drop down list
            CRUD myCrud = new CRUD();
            string mySql = @"SELECT departmentId, department
                            FROM department
                                where administrationId = @administrationId or departmentId=0";
            Dictionary<string, object> myPara = new Dictionary<string, object>();
            myPara.Add("@administrationId", adminId);
            using (SqlDataReader dr = myCrud.getDrPassSql(mySql, myPara))
            {
                ddlDep.DataValueField = "departmentId";
                ddlDep.DataTextField = "department";
                ddlDep.DataSource = dr;
                ddlDep.DataBind();
            }
        }
        protected void ddlActivity_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblActivityName.Text = "Available  " +  ddlActivityType.SelectedItem.Text;
            //select activityId, name from activitylist
            populateCBLActivityList();

        }
        protected void btnShowActivityLog_Click(object sender, EventArgs e)
        {
            string mySql = @"select activityRegistrationId, employeeEn,  activityName
                    from activityRegistration ar inner join employee e on ar.employeeId = e.employeeId
                    inner join activity ac on ar.activityId = ac.activityId
                    order by employeeEn, activityName";
            CRUD myCrud = new CRUD();
            using (SqlDataReader dr = myCrud.getDrPassSql(mySql))
            {
                gvActivityRegistration.DataSource = dr;
                gvActivityRegistration.DataBind();
            }
        }
    }
}