using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace kfmc.internship
{
    public partial class popUpDoc : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            populateGvDocs();
        }
        protected void populateGvDocs()
        {
            int intInternId = 0;
            if (Session["internId"] != null)
            {
                lblOutput.Text = Session["internId"].ToString();
                intInternId = int.Parse(Session["internId"].ToString());
            }
            //..  int intTaskId = int.Parse(ddlDocType.SelectedItem.Value.ToString());  //int.Parse(ddlGender.SelectedItem.Value.ToString());
            CRUD myCrud = new CRUD();
            //..   Dictionary<string, object> myPara = new Dictionary<string, object>();
            //..  myPara.Add("@intTaskId", intTaskId);
            //string MeetingSeltCom = @"SELECT clientDocId, client.client, groupType.groupType, clientDoc.docName
            //                            FROM   client INNER JOIN
            //                            clientDoc ON client.clientId = clientDoc.clientId INNER JOIN
            //                            groupType ON client.groupTypeId = groupType.groupTypeId";
            string mySql = @"select InternDocId, docName, dateUploaded
                                     from InternDoc
                                     where internId = @internId";
            Dictionary<string, object> myPara = new Dictionary<string, object>();
            myPara.Add("@internId", intInternId);

            using (SqlDataReader dr = myCrud.getDrPassSql(mySql, myPara)) // come back for sorting and paging 
            {
                gvDocs.DataSource = dr;
                gvDocs.DataBind();
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
    }
}