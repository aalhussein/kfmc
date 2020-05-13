using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using kfmc.App_Code;
using System.Web.Security;

namespace kfmc.internship
{
    public partial class internTrainingSchedule : System.Web.UI.Page
    {
        bool IamAdmin = false;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // showGvInternTrainingSchedule();
                populatetrainingYearDropList();
                populatetrainingTermDropList();
            }
            if (Roles.IsUserInRole(User.Identity.Name, "admin") || Roles.IsUserInRole(User.Identity.Name, "supervisor"))
            {
                IamAdmin = true;
                cbxGetSingleSchedule.Visible = true;
            }
        }
        protected void populatetrainingTermDropList()
        {
            CRUD myCrud = new CRUD();
            string sqlTrainingTerm = @" select trainingTermId, trainingTerm from trainingTerm";
            myCrud.populatComboViaDr(sqlTrainingTerm, ddlTrainingTermFilter, "trainingTermId", "trainingTerm");
        }
        protected void populatetrainingYearDropList()
        {
            CRUD myCrud = new CRUD();
            string sqlTrainingYear = @"select trainingYearId, trainingYear from trainingYear";
            myCrud.populatComboViaDr(sqlTrainingYear, ddlTrainingYearFilter, "trainingYearId", "trainingYear");
        }
        protected void Page_UnLoad(object sender, EventArgs e)
        {
            // code to be executed on user leaves the page
            // to avoid max connection pool exceeded
            CRUD.clearAllPools();
        }
        protected void showGvInternTrainingSchedule()
        {
            // for validation
            if (common.IsNullOrEmptyControlObj(ddlTrainingTermFilter, lblOutput, "Please fill Training Term !")) return;  // added april 2020 ... short hand 
            if (common.IsNullOrEmptyControlObj(ddlTrainingYearFilter, lblOutput, "Please fill Training Year !")) return;  // added april 2020 ... short hand 

            string myEmail = txtEmail.Text;
            Session["email"] = myEmail;
            if (common.InputIsEmpty(myEmail)   & IamAdmin == false)
            {
                common.PostMsg(lblOutput, "Please enter a valid email !");
                return;
            }
            else
            {
                common.clearLblOutputContent(lblOutput);
                try
                {
                    CRUD myCrud = new CRUD();
                    string mySql = @"  select its.internid,refNo,fName + ' ' +  mi + ' '+  lname  as Name, sunday,monday,tuesday,Wednesday,thursday
                        from intern i inner join  internTrainingSchedule its on i.internid = its.internid";
                   
                    Dictionary<string, object> myPara = new Dictionary<string, object>();
                    DataSet ds = null;
                    if (IamAdmin == false || (IamAdmin== true & cbxGetSingleSchedule.Checked == true & common.InputIsEmpty(myEmail) == false))
                    {
                        mySql += " where email = @email  and TrainingTermId =@TrainingTermId and TrainingYearId =@TrainingYearId ";
                        mySql += " order by name ";
                        myPara.Add("@email", Session["email"].ToString().Trim());
                        myPara.Add("@TrainingYearId", int.Parse(ddlTrainingYearFilter.SelectedValue) );
                        myPara.Add("@TrainingTermId",int.Parse(ddlTrainingTermFilter.SelectedValue));
                        ds = myCrud.getDataSetPassSqlDic(mySql, myPara);
                        if ((ds == null || ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0))
                            common.PostMsg(lblOutput, "No Data Found!","red");
                    }
                    else if (IamAdmin == true  & common.InputIsEmpty(myEmail) & cbxGetSingleSchedule.Checked == false)
                    {
                        mySql += " where  TrainingTermId =@TrainingTermId and TrainingYearId =@TrainingYearId ";
                        mySql += " order by name ";
                        myPara.Add("@TrainingYearId", int.Parse(ddlTrainingYearFilter.SelectedValue));
                        myPara.Add("@TrainingTermId", int.Parse(ddlTrainingTermFilter.SelectedValue));

                        ds = myCrud.getDataSetPassSqlDic(mySql, myPara);
                        if ((ds == null || ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0))
                            common.PostMsg(lblOutput, "No Data Found!", "red");
                    }
               
                    Session["InternDt"] = ds.Tables[0];
                    gvInternTrainingSchedule.DataSource = Session["InternDt"];
                    gvInternTrainingSchedule.DataBind();
                }
                catch (Exception ex)
                {
                    common.PostMsg(lblOutput, ex.Message.ToString());
                }
            }
        }
        protected void populateComboInsideGv()
        {
            //       //Find the DropDownList in the Row
            //   DropDownList ddlGender = gvInternTrainingSchedule.Columns[[//(e.Row.FindControl("ddlGenderEdit") as DropDownList);
            //   DataSet ds = employeeDal.getDsCombo("select genderId, gender from gender");
            //   ddlGender.DataSource = ds.Tables[0];
            //   ddlGender.DataTextField = "gender";
            //   ddlGender.DataValueField = "genderId";
            //   ddlGender.DataBind();

            //   //Add Default Item in the DropDownList
            //   ddlGender.Items.Insert(0, new ListItem("Please select"));
            //   // THIS IS IMPORTANT : to find the index via the text in lable
            //   ddlGender.SelectedIndex = ddlGender.Items.IndexOf(ddlGender.Items.FindByValue(DataBinder.Eval(e.Row.DataItem, "genderId").ToString()));
            ////this was updated on thurs 3/7/2014
        }
        protected void gvInternTrainingSchedule_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //if ((e.Row.RowState & DataControlRowState.Edit) > 0)
                //{
                //    //Find the DropDownList in the Row
                //    DropDownList ddlGender = (e.Row.FindControl("ddlGenderEdit") as DropDownList);
                //    DataSet ds = employeeDal.getDsCombo("select genderId, gender from gender");
                //    ddlGender.DataSource = ds.Tables[0];
                //    ddlGender.DataTextField = "gender";
                //    ddlGender.DataValueField = "genderId";
                //    ddlGender.DataBind();

                //    //Add Default Item in the DropDownList
                //    ddlGender.Items.Insert(0, new ListItem("Please select"));
                //    // THIS IS IMPORTANT : to find the index via the text in lable
                //    ddlGender.SelectedIndex = ddlGender.Items.IndexOf(ddlGender.Items.FindByValue(DataBinder.Eval(e.Row.DataItem, "genderId").ToString()));
                //    //this was updated on thurs 3/7/2014
                //}
            }
        }
        //protected void gvInternTrainingSchedule_RowEditing(object sender, GridViewEditEventArgs e)
        //{
        //    gvInternTrainingSchedule.EditIndex = e.NewEditIndex;
        //    //showGvInternTrainingSchedule();// your gridview binding function
        //}
        protected void gvInternTrainingSchedule_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            if (e.CommandName == "viewProfile")
            {
                person.intPersonId = -1; // reset
              // Response.Redirect();
              // I created a class called person to move values between pages
                person.intPersonId = Convert.ToInt32(e.CommandArgument);
                Response.Redirect(@"~/internship/internProfile.aspx"); // rtn
            }

            else if (e.CommandName == "EditRow")
            {
                int rowIndex = ((GridViewRow)((LinkButton)e.CommandSource).NamingContainer).RowIndex;
                gvInternTrainingSchedule.EditIndex = rowIndex;
                showGvInternTrainingSchedule();
            }
            else if (e.CommandName == "DeleteRow")
            {
                employeeDal.deleteEmployee(Convert.ToInt32(e.CommandArgument));
                showGvInternTrainingSchedule();
            }
            else if (e.CommandName == "CancelUpdate")
            {
                gvInternTrainingSchedule.EditIndex = -1;
                showGvInternTrainingSchedule();
            }
            else if (e.CommandName == "UpdateRow")
            {   // shows how to get column values from gv
                int rowIndex = ((GridViewRow)((LinkButton)e.CommandSource).NamingContainer).RowIndex;
                int internId = Convert.ToInt32(e.CommandArgument);
               // string name = ((TextBox)gvInternTrainingSchedule.Rows[rowIndex].FindControl("txtEditName")).Text;
                int sun = (((CheckBox)gvInternTrainingSchedule.Rows[rowIndex].FindControl("cbSunEdit")).Checked) == true ? 1 : 0;
                int mon = (((CheckBox)gvInternTrainingSchedule.Rows[rowIndex].FindControl("cbMonEdit")).Checked) == true ? 1 : 0;
                int tue = (((CheckBox)gvInternTrainingSchedule.Rows[rowIndex].FindControl("cbTueEdit")).Checked) == true ? 1 : 0;
                int wed = (((CheckBox)gvInternTrainingSchedule.Rows[rowIndex].FindControl("cbWedEdit")).Checked) == true ? 1 : 0;
                int thu = (((CheckBox)gvInternTrainingSchedule.Rows[rowIndex].FindControl("cbThuEdit")).Checked) == true ? 1 : 0;
                Dictionary<string, object> myPara = new Dictionary<string, object>();
                myPara.Add("@internId", internId);
               // myPara.Add("@name", name);
                myPara.Add("@sun", sun);
                myPara.Add("@mon", mon);
                myPara.Add("@tue", tue);
                myPara.Add("@wed", wed);
                myPara.Add("@thu", thu);
                CRUD myCrud = new CRUD();
                string mySql = @"update interntrainingSchedule
		                   set sunday=@sun, monday=@mon, tuesday=@tue, wednesday=@wed, thursday=@thu
		                   where internId =@internId";
                myCrud.InsertUpdateDelete(mySql, myPara);
                gvInternTrainingSchedule.EditIndex = -1; // go out of edit mode 
                showGvInternTrainingSchedule();
            }

        }
        #region repeated code
        /// <summary>
        ///  repeated code always put it in a separate method, then call it from any where in the same class
        /// </summary>
        protected string verifyOps(int confirm)
        {
            if (confirm >= 1)
                return "Sucess";
            else
                return "Failed";
        }
        protected void postMsg(string msg)
        {
            // lblOutput.Text = msg;
        }
        #endregion
        #region  GV Paging
        protected void gvInternTrainingSchedule_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            // for paging the gridview
            gvInternTrainingSchedule.PageIndex = e.NewPageIndex;
            showGvInternTrainingSchedule();
        }
        protected void gvInternTrainingSchedule_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        #endregion
        #region  GV_sort event and Sort Direction
        protected void gvInternTrainingSchedule_Sorting(object sender, GridViewSortEventArgs e)
        {
            //Retrieve the table from the session object.
            DataTable dt = Session["InternDt"] as DataTable;
            if (dt != null)
            {
                //Sort the data.
                dt.DefaultView.Sort = e.SortExpression + " " + GetSortDirection(e.SortExpression);
                //GridView Gv = (GridView)sender;// get the name of the gv object, then bind the session to it
                //Gv.DataSource = Session["InternDt"];
                //Gv.DataBind();
                gvInternTrainingSchedule.DataSource= Session["InternDt"];
                gvInternTrainingSchedule.DataBind();
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
        public string strDocName { get; set; }
        public void getShowDocs(object sender, EventArgs e)
        {

        }
        protected void btnGetTrainingSchedule_Click(object sender, EventArgs e)
        {
            showGvInternTrainingSchedule();
        }
        protected void ddlTrainingTermFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblOutput.Text = "";
        }
        protected void ddlTrainingYearFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            common.PostMsg(lblOutput, "");
        }
    }// Cls
}// NS