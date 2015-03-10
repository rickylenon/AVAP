﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Web.UI.HtmlControls;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Data.Common;
using Ava.lib;
using Ava.lib.constant;
using System.Text;
using System.Text.RegularExpressions;

public partial class vmofficer_VendorForRenewal_List : System.Web.UI.Page
{
    SqlDataReader oReader;
    string connstring = ConfigurationManager.ConnectionStrings["AVAConnectionString"].ConnectionString;
    string query;
    SqlCommand cmd;
    SqlConnection conn;
    string VendorIdstr;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserId"] == null) Response.Redirect("login.aspx");
        if (Session["SESSION_USERTYPE"].ToString() != ((int)Constant.USERTYPE.VMOFFICER).ToString()) Response.Redirect("login.aspx");

        if(IsPostBack)
        {
            if(Request["__EVENTTARGET"].ToString() == "Details" && Request["__EVENTARGUMENT"].ToString()!="")
            {
                VendorIdstr = Request["__EVENTARGUMENT"].ToString().Trim();
                string sArg = VendorIdstr.Trim();
                char[] mySeparator = new char[] { '|' };
                string[] Arr = sArg.Split(mySeparator);
                string VendorAlias = "";
                int SendToSAP_Status = 0;

                query = "SELECT CompanyName FROM tblVendor WHERE VendorId=@VendorId";
                using (conn = new SqlConnection(connstring))
                {
                    using (cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@VendorId", Convert.ToInt32(Arr[0].ToString()));
                        conn.Open();
                        oReader = cmd.ExecuteReader();
                        if (oReader.HasRows)
                        {
                            while (oReader.Read())
                            {
                                VendorAlias = oReader["CompanyName"].ToString().Replace(" ", "").Substring(0, 4).ToLower();

                            }
                            SendMail(Arr[0].ToString(), Arr[1].ToString());
			    if(Arr[1].ToString()=="6"){ SendToSAP_Status = 1; }



                //SAVE VENDOR HISTORY
                string sCommand = "";
                sCommand = "INSERT INTO tblVendorHistory SELECT * FROM (SELECT * FROM tblVendor WHERE VendorId = " + Arr[0].ToString() + ") t0";
                SqlHelper.ExecuteNonQuery(connstring, CommandType.Text, sCommand);

                query = @"UPDATE tblVendor SET NotificationSent=NULL, SendToSAP_Status=NULL, Status=0, 
                            IsAuthenticated = NULL, AuthenticationTicket = NULL, 
                            DateSubmittedToDnb = NULL, DateAuthenticatedByDnb = NULL,
                            approvedbyDnb = NULL, approvedbyDnbDate=NULL,
                            approvedbyVMOfficer = NULL, approvedbyVMOfficerDate = NULL,
                            approvedbyVMReco = NULL, approvedbyVMRecoDate = NULL, 
                            approvedbyFAALogistics = NULL, approvedbyFAALogisticsDate = NULL,
                            approvedbyFAAFinance = NULL, approvedbyFAAFinanceDate = NULL
                            --,renewaldate = NULL
                            WHERE VendorId=@VendorId";
                            using (conn = new SqlConnection(connstring))
                            {
                                using (cmd = new SqlCommand(query, conn))
                                {
                                    cmd.Parameters.AddWithValue("@VendorId", Convert.ToInt32(Arr[0].ToString()));
                                    conn.Open(); cmd.ExecuteNonQuery();
                                }
                            }
                        }
                    }
                }

                

                GridView1.DataBind();
            }
        }
    }

    void SendMail(string VendorIdx, string Statusx)
    {

        // SEND EMAIL NOTIFICATION TO VENDOR
        query = "SELECT t3.FirstName + ' ' + t3.LastName as fromName, t3.EmailAdd as fromEmail, t1.FirstName + ' ' + t1.LastName as toName, t1.EmailAdd as toEmail, t4.CompanyName, t4.AuthenticationTicket FROM tblUsers t1, tblUsersForVendors t2, tblUsers t3, tblVendor t4 WHERE t1.UserId = t2.UserId AND t2.VendorId = @VendorId AND t3.UserId = @UserId AND t4.VendorId = @VendorId";
        string fromName = "", fromEmail = "", toName = "", toEmail = "", AuthenticationTicket = "", VendorName = "";
        using (conn = new SqlConnection(connstring))
        {
            using (cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@VendorId", Convert.ToInt32(VendorIdx));
                cmd.Parameters.AddWithValue("@UserId", Convert.ToInt32(Session["UserId"]));
                conn.Open();
                //Process results
                oReader = cmd.ExecuteReader();
                if (oReader.HasRows)
                {
                    while (oReader.Read())
                    {
                        fromName = ConfigurationManager.AppSettings["AdminEmailName"].ToString();
                        fromEmail = ConfigurationManager.AppSettings["AdminNoReplyEmail"].ToString();
                        toName = oReader["toName"].ToString();
                        toEmail = oReader["toEmail"].ToString();
                        AuthenticationTicket = oReader["AuthenticationTicket"].ToString();
                        VendorName = oReader["CompanyName"].ToString();
                        if (Statusx == "6")
                        {
                            SendEmailNotification(fromName, fromEmail, toName, toEmail, AuthenticationTicket, VendorName, VendorIdx);
                        }
                        else
                        {
                            SendEmailNotificationReject(fromName, fromEmail, toName, toEmail, AuthenticationTicket, VendorName, VendorIdx);
                        }

                    }
                }
            }
        }
        // SEND EMAIL NOTIFICATION TO VM VENDOR ENDS
    }
	
    //############################################################
    //############################################################
    // SEND EMAIL NOTIFICATION TO VENDOR IF APPROVED
    private bool SendEmailNotification(string sfromName, string sfromEmail, string stoName, string stoEmail, string sAuthenticationTicket, string sVendorName, string VendorIdx)
    {
        bool success = false;

        string from = sfromName + "<" + sfromEmail + ">";
        string to = stoName + "<" + stoEmail + ">";
        string subject = "";
		
        try
        {
            subject = "Globe Telecom Vendor Accreditation for renewal.";
            if (!MailHelper.SendEmail(MailTemplate.GetDefaultSMTPServer(),
                    from,
                    to,
                    subject,
                    CreateNotificationBody(sfromName, stoName, sAuthenticationTicket, sVendorName, VendorIdx),
                    MailTemplate.GetTemplateLinkedResources(this)))
            {	//if sending failed					
                LogHelper.EventLogHelper.Log("Bid > Send Notification : Sending Failed to " + from, System.Diagnostics.EventLogEntryType.Error);
            }
            else
            {	//if sending successful
                LogHelper.EventLogHelper.Log("Bid > Send Notification : Email Sent to " + from, System.Diagnostics.EventLogEntryType.Information);
            }
            success = true;
        }
        catch (Exception ex)
        {
            success = false;
            LogHelper.EventLogHelper.Log("Bid > Send Notification : " + ex.Message, System.Diagnostics.EventLogEntryType.Error);
            Response.Write(ex.ToString());
        }
        return success;
    }

    private string CreateNotificationBody(string cfromName, string ctoName, string cAuthenticationNumber, string cVendorName, string VendorIdx)
    {
        SqlDataReader oReader;
        string connstring = ConfigurationManager.ConnectionStrings["AVAConnectionString"].ConnectionString;
        string cCeo = "", cCeoEmail = "", cAddress = "", cServices = "", cAccreDuration = "";
        query = "SELECT * FROM tblVendorInformation WHERE VendorId = @VendorId";
        using (conn = new SqlConnection(connstring))
        {
            using (cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@VendorId", Convert.ToInt32(VendorIdx));
                conn.Open();
                //Process results
                oReader = cmd.ExecuteReader();
                if (oReader.HasRows)
                {
                    while (oReader.Read())
                    {
                        cCeo = oReader["conBidName"].ToString();
                        cCeoEmail = oReader["conBidEmail"].ToString();
                        cAddress = oReader["regBldgCode"].ToString() != "" ? cAddress + "Bldg. " + oReader["regBldgCode"].ToString() + ", " : cAddress + "";
                        cAddress = oReader["regBldgRoom"].ToString() != "" ? cAddress + "Rm. " + oReader["regBldgRoom"].ToString() + ", " : cAddress + "";
                        cAddress = oReader["regBldgFloor"].ToString() != "" ? cAddress + oReader["regBldgFloor"].ToString() + " Fr, " : cAddress + "";
                        cAddress = oReader["regBldgHouseNo"].ToString() != "" ? cAddress + "No. " + oReader["regBldgHouseNo"].ToString() + " " : cAddress + "";
                        cAddress = oReader["regStreetName"].ToString() != "" ? cAddress + oReader["regStreetName"].ToString() + ", " : cAddress + "";
                        cAddress = cAddress + "<br>";
                        cAddress = oReader["regCity"].ToString() != "" ? cAddress + oReader["regCity"].ToString() + ", " : cAddress + "";
                        cAddress = oReader["regProvince"].ToString() != "" ? cAddress + oReader["regProvince"].ToString() + ", " : cAddress + "";
                        cAddress = cAddress + "<br>";
                        cAddress = oReader["regCountry"].ToString() != "" ? cAddress + oReader["regCountry"].ToString() + ", " : cAddress + "";
                        cAddress = oReader["regPostal"].ToString() != "" ? cAddress + oReader["regPostal"].ToString() + " " : cAddress + "";
                    }
                }
            }
        }
        query = "SELECT t1.*, t2.CategoryName, t3.SubCategoryName FROM tblVendorProductsAndServices t1, rfcProductCategory t2, rfcProductSubcategory t3 WHERE t2.CategoryId = t1.CategoryId AND t3.SubCategoryId = t1.SubCategoryId AND t1.VendorId = @VendorId";
        using (conn = new SqlConnection(connstring))
        {
            using (cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@VendorId", Convert.ToInt32(VendorIdx));
                conn.Open();
                //Process results
                oReader = cmd.ExecuteReader();
                if (oReader.HasRows)
                {
                    while (oReader.Read())
                    {
                        cServices = cServices + "&bull; " + oReader["CategoryName"].ToString() + " - " + oReader["SubCategoryName"].ToString() + "<br>";
                    }
                }
            }
        }

        query = "SELECT * FROM tblVendorApprovalbyVmReco  WHERE VendorId = @VendorId";
        using (conn = new SqlConnection(connstring))
        {
            using (cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@VendorId", Convert.ToInt32(VendorIdx));
                conn.Open();
                //Process results
                oReader = cmd.ExecuteReader();
                if (oReader.HasRows)
                {
                    while (oReader.Read())
                    {
                        cAccreDuration = oReader["AccreDuration"].ToString();
                    }
                }
            }
        }

        StringBuilder sb = new StringBuilder();
        string sTxt = "<table border='1' style='font-size:12px'>";
        sTxt = sTxt + "<tr>";
        sTxt = sTxt + "<td><strong>&nbsp;Vendor ID</strong></td>";
        sTxt = sTxt + "<td>&nbsp;" + VendorIdx + "&nbsp;</td>";
        sTxt = sTxt + "</tr>";
        sTxt = sTxt + "<tr>";
        sTxt = sTxt + "<td><strong>&nbsp;Company Name</strong></td>";
        sTxt = sTxt + "<td>&nbsp;" + cVendorName + "&nbsp;</td>";
        sTxt = sTxt + "</tr>";
        sTxt = sTxt + "</table>";

        sb.Append("<tr><td>");
        sb.Append("<p>");
        sb.Append("Date: " + DateTime.Now.ToLongDateString() + "<br><br>");
        sb.Append(cCeo + "<br>");
        sb.Append("<b>" + cVendorName + "</b><br>");
        sb.Append(cAddress + "<br><br>");
        sb.Append("</p>");
        sb.Append("<tr><td>");
        sb.Append("<p>");
        sb.Append("Dear " + cCeo + ":<br><br>");
        sb.Append("Please consider this email as reminders to renew/update your accreditation status with Globe Telecom.<br><br>");
        sb.Append("Looking forward for your compliance on this request before your accreditation expired. Failure to comply will temporarily suspend your company to participate on any upcoming bid events.<br><br>");
        sb.Append("Following are the general steps for Globe Vendor accreditation renewal: <br><br>");
        sb.Append("<ul>");
        sb.Append("<li>Access this site - <a href='https://gtva.globe.com.ph' target='_blank'>https://gtva.globe.com.ph</a> and log in using your username and password. Accreditation guidelines and list of required documents are available online.</li>");
        sb.Append("<li>Upon Vendor’s submission of the complete requirements and payment of the service fee, our accreditation service partner, Dun and Bradstreet, will typically have 7 days to prepare a report.");
            sb.Append("<ul>");
            sb.Append("<li>Service fee is P3,950 for Metro Manila applicants, Php5,600 for provincial</li>");
            sb.Append("<li>For Foreign Vendors, please directly coordinate with Dun and Bradstreet Phils. for the Service fee.");
                sb.Append("<ul>");
                sb.Append("<li>Mr. Rolan M. Sebastian</li>");
                sb.Append("<li>Dun & Bradstreet Philippines, Inc.</li>");
                sb.Append("<li>Unit 507 5/F Rufino Pacific Tower</li>");
                sb.Append("<li>6784 Ayala Ave., Cor. VA Rufino Makati City, 1226</li>");
                sb.Append("<li>tel.632.504.4537</li>");
                sb.Append("<li>mobile.63905.551.3668</li>");
                sb.Append("<li>Cebu Office:</li>");
                sb.Append("<li>tel.6332.233.6053:</li>");
                sb.Append("<li>fax. 6332.233.6051</li>");
                sb.Append("<li><a href='mailto:SebastianR@dnb.com.ph'>SebastianR@dnb.com.ph</a></li>");
                sb.Append("</ul>");
                sb.Append("</li>");
            sb.Append("</ul>");
            sb.Append("</li>");
            sb.Append("<li>Another 4 days evaluation period within Globe after receipt of D&B's report.</li>");
        sb.Append("</ul>");
        sb.Append("We encourage you to renew your accreditation and continue to be a business partner of Globe Telecom.<br><br>");
        sb.Append("Hope to receive positive response from your end.<br><br>");
        sb.Append("Please get in touch with Russel Toribio of Vendor Management for any clarifications.<br><br>");
        sb.Append("</p>");
        sb.Append("<br><br>");
        sb.Append("Very truly yours,<br><br>");
        sb.Append("<b>Honesto P. Oliva</b><br>");
        sb.Append("Head - Vendor Management<br><br><br><br>");
        sb.Append("Whistleblower Contact Channels:<br>");
        sb.Append("Whistleblower Hotline : 0917-8189934<br>");
        sb.Append("Globe Corporate Site: www.globe.com.ph<br>");
        sb.Append("Send Email to:  gt_whistleblower@globetel.com.ph<br>");
        sb.Append("Send  letter to Employee Relations, HR Department, 3rd Floor, GT Tower 1, Mandaluyong City<br><br><br><br>");
        //sb.Append("<b>Globe Telecom Admin<br><br>");
        sb.Append("</td></tr>");
        sb.Append("<tr><td>");
        sb.Append("<p>&nbsp;</p><span style='font-size:10px; font-style:italic;'>Please do not reply to this auto-generated  message.&nbsp;</span>");
        sb.Append("</td></tr>");
        //Response.Write(sb.ToString());
        return MailTemplate.IntegrateBodyIntoTemplate(sb.ToString());
    }

    //############################################################
    //############################################################
    // SEND EMAIL NOTIFICATION TO VENDOR IF REJECTED
    private bool SendEmailNotificationReject(string sfromName, string sfromEmail, string stoName, string stoEmail, string sAuthenticationTicket, string sVendorName, string VendorIdx)
    {
        bool success = false;

        string from = sfromName + "<" + sfromEmail + ">";
        string to = stoName + "<" + stoEmail + ">";
        string subject = "";

        try
        {
            subject = "Globe Telecom Vendor Accreditation application rejected.";
            if (!MailHelper.SendEmail(MailTemplate.GetDefaultSMTPServer(),
                    from,
                    to,
                    subject,
                    CreateNotificationBodyReject(sfromName, stoName, sAuthenticationTicket, sVendorName, VendorIdx),
                    MailTemplate.GetTemplateLinkedResources(this)))
            {	//if sending failed					
                LogHelper.EventLogHelper.Log("Bid > Send Notification : Sending Failed to " + from, System.Diagnostics.EventLogEntryType.Error);
            }
            else
            {	//if sending successful
                LogHelper.EventLogHelper.Log("Bid > Send Notification : Email Sent to " + from, System.Diagnostics.EventLogEntryType.Information);
            }
            success = true;
        }
        catch (Exception ex)
        {
            success = false;
            LogHelper.EventLogHelper.Log("Bid > Send Notification : " + ex.Message, System.Diagnostics.EventLogEntryType.Error);
            //Response.Write(ex.ToString());
        }
        return success;
    }

    private string CreateNotificationBodyReject(string cfromName, string ctoName, string cAuthenticationNumber, string cVendorName, string VendorIdx)
    {
        StringBuilder sb = new StringBuilder();
        string sTxt = "<table border='0' cellpadding='5' style='font-size:12px'>";
        sTxt = sTxt + "<tr>";
        sTxt = sTxt + "<td><strong>&nbsp;Vendor ID</strong></td>";
        sTxt = sTxt + "<td>&nbsp;" + VendorIdx + "&nbsp;</td>";
        sTxt = sTxt + "</tr>";
        sTxt = sTxt + "<tr>";
        sTxt = sTxt + "<td><strong>&nbsp;Company Name</strong></td>";
        sTxt = sTxt + "<td>&nbsp;" + cVendorName + "&nbsp;</td>";
        sTxt = sTxt + "</tr>";
        //sTxt = sTxt + "<tr>";
        //sTxt = sTxt + "<td><strong>&nbsp;Authentication Ticket</strong></td>";
        //sTxt = sTxt + "<td>&nbsp;" + cAuthenticationNumber + "&nbsp;</td>";
        //sTxt = sTxt + "</tr>";
        sTxt = sTxt + "</table>";

        sb.Append("<tr><td>");
        sb.Append("<p>");
        sb.Append("Sent: " + DateTime.Now.ToLongDateString() + "<br>From: " + cfromName + "<br>");
        sb.Append("To: " + cVendorName + "<br><br>");
        sb.Append("</p>");
        sb.Append("<tr><td>");
        sb.Append("<p>");
        sb.Append("Dear " + cVendorName + ":<br><br>");
        sb.Append("Be advised that your application to be an Accredited Globe Vendor was evaluated and found to have not met the requirements, we regret that you will not be accredited at this time.<br><br>");
        sb.Append("If you so wish, we encourage you to schedule a meeting with us to discuss the result of your application for accreditation.<br><br>");
        sb.Append("We would like to thank you for the time afforded in completing and submitting your application.  We will keep your details on record for consideration of future business opportunities.<br><br>");
        //sb.Append("Please access the link below using your username and password to start your application for Globe Telecom accreditation. <br><br>");
        //sb.Append("<a href='http://'<br><br>");
        sb.Append(sTxt);
        sb.Append("</p>");
        sb.Append("<br><br><br>");
        sb.Append("Sincerely,<br><br>");
        sb.Append("Globe Telecom<br><br>");
        sb.Append("</td></tr>");
        sb.Append("<tr><td>");
        sb.Append("<p>&nbsp;</p><span style='font-size:10px; font-style:italic;'>Please do not reply to this auto-generated  message.&nbsp;</span>");
        sb.Append("</td></tr>");

        //sb.Append("<tr><td><p>Sent: " + DateTime.Now.ToLongDateString() + "<br>From: " + cfromName + "<br> To: " + ctoName + "<br><br> Good day!<br><br> This is to inform you that application for vendor accreditation has been rejected.<br></p><br>" + sTxt + "<p>Very truly yours,<br><br><br> <strong>" + cfromName + "</strong></p><p>&nbsp;</p> <span style='font-size:10px; font-style:italic;'>Please do not reply to this auto-generated  message.&nbsp;</span></td></tr>");
        //Response.Write(sb.ToString());
        return MailTemplate.IntegrateBodyIntoTemplate(sb.ToString());
    }
}