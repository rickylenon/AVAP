using System;
using System.Data;
using System.Collections;
using System.Web;
using System.Configuration;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Ava;
using Ava.lib.utils;
using Ava.lib.user.trans;
using Ava.lib.constant;
using Ava.lib;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Net.Mail;

public partial class logout : System.Web.UI.Page
{
    protected void TestShowAllSessions()
    {  //test show all session
        string str = null;
        foreach (string key in HttpContext.Current.Session.Keys)
        { str += string.Format("<b>{0}</b>: {1};  ", key, HttpContext.Current.Session[key].ToString()); }
        Response.Write("<span style='font-size:12px'>" + str + "</span>");
    }
    string connstring = ConfigurationManager.ConnectionStrings["AVAConnectionString"].ConnectionString; 


    protected void Page_Load(object sender, EventArgs e)
    {
        //TestShowAllSessions(); 
        Session["SESSION_USERNAME"] = "";
        Session["SESSION_PASSWORD"] = "";
        Session["SESSION_FULLNAME"] = "";
        Session["SESSION_USERTYPE"] = "";
        Session["UserId"] = "";
        Session["VendorId"] = "";
        Session["UserIdDetails"] = "";
        getLandingContent();
    }


    private void getLandingContent()
    {
        SqlDataReader oReader;
        connstring = ConfigurationManager.ConnectionStrings["AVAConnectionString"].ConnectionString;
        string sCommand = "SELECT content FROM rfcLandingContent WHERE active = 1";
        oReader = SqlHelper.ExecuteReader(connstring, CommandType.Text, sCommand);

        if (oReader.HasRows)
        {
            oReader.Read();
            contentLanding.Text = contentLanding.Text + oReader["content"].ToString().Replace(System.Environment.NewLine, "<br>") + "<br><br>";
        }
    }
}
