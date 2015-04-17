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
using System.IO;

public partial class admin_EditEmails : System.Web.UI.Page
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
        PopulateFields();
        //Response.Write(txtfile.SelectedValue);
    }

    void PopulateFields()
    {

        content.Text = "";
        if (txtfile.SelectedValue != "")
        {
            StringBuilder sb = new StringBuilder();

            string text = string.Empty;
            using (StreamReader streamReader = new StreamReader(Server.MapPath("~/email_templates/" + txtfile.SelectedValue), Encoding.UTF8))
            {
                while (streamReader.Peek() >= 0)
                {
                    //Console.WriteLine(sr.ReadLine());
                    text = text + streamReader.ReadLine() + "\n";
                }
            }
            content.Text = text;
        }
    }

   


    void SaveToDB()
    {
        if (txtfile.SelectedValue != "")
        {
            System.IO.File.WriteAllText(Server.MapPath("~/email_templates/" + txtfile.SelectedValue), content.Text);

            errNotification.Text = "Content successfully updated";
            errNotification.ForeColor = Color.Blue;
        }
    }



}