using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Data.Common;
using Ava.lib;
using Ava.lib.constant;
using System.Text;
using System.Text.RegularExpressions;

public partial class admin_EditContent : System.Web.UI.Page
{
    SqlDataReader oReader;
    string connstring = ConfigurationManager.ConnectionStrings["AVAConnectionString"].ConnectionString;
    string query;
    SqlCommand cmd;
    SqlConnection conn;
    bool UserNameExists;
    string sCommand;

    protected void TestShowAllSessions()
    {  //test show all session
        string str = null;
        foreach (string key in HttpContext.Current.Session.Keys)
        { str += string.Format("<b>{0}</b>: {1};  ", key, HttpContext.Current.Session[key].ToString()); }
        Response.Write("<span style='font-size:12px'>" + str + "</span>");
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        //TestShowAllSessions();
        if (Session["UserId"] == null) Response.Redirect("login.aspx");
        if (Session["SESSION_USERTYPE"].ToString() != "9") Response.Redirect("login.aspx");
        

        if (IsPostBack)
        {
            string control1 = Request.Form["__EVENTTARGET"];
            if (control1 == "saveContent")
            {
                SaveToDB();
            }
        }
        else
        {
            PopulateFields();
        } 
    }

    void PopulateFields()
    {
        string connstring = ConfigurationManager.ConnectionStrings["AVAConnectionString"].ConnectionString;
        //string sCommand = "";

        query = "SELECT t1.* FROM rfcLandingContent t1 WHERE t1.active=1";
        using (conn = new SqlConnection(connstring))
        {
            using (cmd = new SqlCommand(query, conn))
            {
                conn.Open();
                //cmd.Parameters.AddWithValue("@UserId", Session["UserIdVendor"]);
                oReader = cmd.ExecuteReader();
                if (oReader.HasRows)
                {
                    while (oReader.Read())
                    {
                        content.Text = oReader["content"].ToString();
                    }
                } conn.Close();
            }
        }
    }

   



    //CREATE User
    void SaveToDB()
    {
        query = "UPDATE rfcLandingContent SET content=@content";
            using (conn = new SqlConnection(connstring))
            {
                using (cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@content", content.Text.Trim());
                    conn.Open(); cmd.ExecuteNonQuery(); conn.Close();
                }
            }

            errNotification.Text = "Content successfully updated";
            errNotification.ForeColor = Color.Blue;
    }



}