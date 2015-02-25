﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Data.Common;
using Ava.lib;
using Ava.lib.constant;

public partial class vendor_01_vendorInfo : System.Web.UI.Page
{
    SqlDataReader oReader;
    string connstring = ConfigurationManager.ConnectionStrings["AVAConnectionString"].ConnectionString;
    string query;
    SqlCommand cmd;
    SqlConnection conn;
    int numRowsTbl;
    public int VendorId;
    public string queryString;

    protected void TestShowAllSessions()
    {  //test show all session
        string str = null;
        foreach (string key in HttpContext.Current.Session.Keys)
        { str += string.Format("<b>{0}</b>: {1};  ", key, HttpContext.Current.Session[key].ToString()); }
        Response.Write("<span style='font-size:12px'>" + str + "</span>");
    }

    protected void Page_Load(object sender, EventArgs e)
    {

        queryString = (Request.QueryString["VendorId"] != null && Request.QueryString["VendorId"] != "") ? "?VendorId=" + Request.QueryString["VendorId"] : "";
        if (Request.QueryString["VendorId"] != null && Request.QueryString["VendorId"] != "")
        {
            VendorId = Convert.ToInt32(Request.QueryString["VendorId"]);
        }
        else if (Session["VendorId"] != null && Session["VendorId"].ToString() != "")
        {
            VendorId = Convert.ToInt32(Session["VendorId"].ToString());
        }


        //TestShowAllSessions();
        if (
            Session["UserId"] == null || 
            Session["UserId"].ToString() == "" || 
            VendorId.ToString() == ""
            )
        {
            Response.Redirect("login.aspx");
        }

        if (editable())
        {
            if (IsPostBack) SaveToDB();
            PopulateFields();
            repeaterBranches.DataBind();
            repeaterSubsidiary.DataBind();
            repeaterBranches_Lbl.Visible=false;
            repeaterSubsidiary_Lbl.Visible = false;
            tbl01_Lbl.Visible = false;
            tbl02_Lbl.Visible = false;
            tbl03_Lbl.Visible = false;
            tbl04_Lbl.Visible = false;
        }
        else //otherwise viewing only
        {
            PopulateFieldsLbl();
            repeaterSubsidiary_Lbl.DataBind();
            repeaterBranches_Lbl.DataBind();
            repeaterBranches.Visible=false;
            repeaterSubsidiary.Visible = false;
            tbl01.Visible = false;
            tbl02.Visible = false;
            tbl03.Visible = false;
            tbl04.Visible = false;
            add5.Visible = add6.Visible = false;
            createBt.Visible = createBt1.Visible = false;
            //(HtmlInput)VendorBranchesCounter.Visible = false;
        }
        //dstblVendorSubsidiary.SelectParameters.Add("
    }


    protected void Page_LoadComplete(object sender, EventArgs e)
    {
        
    }

    private bool editable()
    {
        bool EditMode = false;
        query = "SELECT * FROM tblVendor WHERE VendorId =  @VendorId";
        using (conn = new SqlConnection(connstring))
        {
            using (cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@VendorId", VendorId);
                conn.Open();
                oReader = cmd.ExecuteReader();
                if (oReader.HasRows)
                {
                    while (oReader.Read())
                    {
                        if (
                            (Session["SESSION_USERTYPE"].ToString() == "11" && oReader["Status"].ToString() == "0") ||
                            (Session["SESSION_USERTYPE"].ToString() == "11" && oReader["Status"].ToString() == "9")
                            )
                        {
                            EditMode = true;
                        }
                    }
                }
            }
        }

        return EditMode;
    }
    

    void PopulateFields()
    {
        CompanyName.Disabled = true;

        query = "SELECT * FROM tblVendor WHERE VendorId=@VendorId";
        //query = "sp_GetVendorInformation"; //##storedProcedure
        using (conn = new SqlConnection(connstring))
        {
            using (cmd = new SqlCommand(query, conn))
            {
                //cmd.CommandType = CommandType.StoredProcedure; //##storedProcedure
                cmd.Parameters.AddWithValue("@VendorId", VendorId);
                conn.Open();
                //Process results
                oReader = cmd.ExecuteReader();
                if (oReader.HasRows)
                {
                    while (oReader.Read())
                    {
                        CompanyName.Value = oReader["CompanyName"].ToString();
                    }
                }
            }
        }  

        query = "SELECT * FROM tblVendorInformation WHERE VendorId=  @VendorId";
        //query = "sp_GetVendorInformation"; //##storedProcedure
        using (conn = new SqlConnection(connstring))
        {
            using (cmd = new SqlCommand(query, conn))
            {
                //cmd.CommandType = CommandType.StoredProcedure; //##storedProcedure
                cmd.Parameters.AddWithValue("@VendorId", VendorId);
                conn.Open();
                //Process results
                oReader = cmd.ExecuteReader();
                if (oReader.HasRows)
                {
                    while (oReader.Read())
                    {
                        //CompanyName.Value = oReader["CompanyName"].ToString();
                        regBldgCode.Value = oReader["regBldgCode"].ToString();
                        regBldgRoom.Value = oReader["regBldgRoom"].ToString();
                        regBldgFloor.Value = oReader["regBldgFloor"].ToString();
                        regBldgHouseNo.Value = oReader["regBldgHouseNo"].ToString();
                        regStreetName.Value = oReader["regStreetName"].ToString();
                        regCity.Value = oReader["regCity"].ToString();
                        regProvince.Value = oReader["regProvince"].ToString();
                        regCountry.Value = oReader["regCountry"].ToString();
                        regPostal.Value = oReader["regPostal"].ToString();
                        regArea.Value = oReader["regArea"].ToString();
                        regOwned.SelectedValue = oReader["regOwned"].ToString();
                        if (oReader["regOwnedAttachment"].ToString() != "")
                        {
                            string[] regOwnedAttachments1 = oReader["regOwnedAttachment"].ToString().Split(';');
                            foreach (string regOwnedAttachment1 in regOwnedAttachments1)
                            {
                                regOwnedAttachmentLbl.Text = regOwnedAttachment1.Trim() != "" ? regOwnedAttachmentLbl.Text + "<div><a href='" + regOwnedAttachment1.Trim() + "' target='_blank'>Attached file</a> <img src=\"images/xicon.png\" style=\"margin-left:10px; padding-top:5px; \" id=\"regOwnedAttachmentx\" onclick=\"$(this).parent(\'div\').html(\'\');FileattchValues($(\'#ContentPlaceHolder1_regOwnedAttachment\').val(),\'" + regOwnedAttachment1.Trim() + "\',\'regOwnedAttachment\');\" /><br></div>" : "";
                            }
                        }
                        else
                        {
                            regOwnedAttachmentLbl.Text = "Attach file<br>";
                        }
                        
                        regOwnedAttachment.Value = oReader["regOwnedAttachment"].ToString();
                        conBidName.Value = oReader["conBidName"].ToString();
                        conBidPosition.Value = oReader["conBidPosition"].ToString();
                        conBidEmail.Value = oReader["conBidEmail"].ToString();
                        conBidMobile.Value = oReader["conBidMobile"].ToString();
                        conBidTelNo.Value = oReader["conBidTelNo"].ToString();
                        conBidFaxNo.Value = oReader["conBidFaxNo"].ToString();
                        conLegName.Value = oReader["conLegName"].ToString();
                        conLegPosition.Value = oReader["conLegPosition"].ToString();
                        conLegEmail.Value = oReader["conLegEmail"].ToString();
                        conLegMobile.Value = oReader["conLegMobile"].ToString();
                        conLegTelNo.Value = oReader["conLegTelNo"].ToString();
                        conLegFaxNo.Value = oReader["conLegFaxNo"].ToString();
                        parentCompanyName.Value = oReader["parentCompanyName"].ToString();
                        parentCompanyAddr.Value = oReader["parentCompanyAddr"].ToString();
                        repOfcCompanyName.Value = oReader["repOfcCompanyName"].ToString();
                        repOfcCompanyAddr.Value = oReader["repOfcCompanyAddr"].ToString();
                        repOfcEmail.Value = oReader["repOfcEmail"].ToString();
                        repOfcTelNo.Value = oReader["repOfcTelNo"].ToString();
                        repOfcFaxNo.Value = oReader["repOfcFaxNo"].ToString();

                    }
                }
            }
        }        
    }


    void PopulateFieldsLbl()
    {
        //CompanyName.Value = oReader["CompanyName"].ToString();
        //CompanyName.Disabled = true;

        query = "SELECT * FROM tblVendorInformation WHERE VendorId=  @VendorId";
        //query = "sp_GetVendorInformation"; //##storedProcedure
        using (conn = new SqlConnection(connstring))
        {
            using (cmd = new SqlCommand(query, conn))
            {
                //cmd.CommandType = CommandType.StoredProcedure; //##storedProcedure
                cmd.Parameters.AddWithValue("@VendorId", VendorId);
                conn.Open();
                //Process results
                oReader = cmd.ExecuteReader();
                if (oReader.HasRows)
                {
                    while (oReader.Read())
                    {
                        CompanyName_Lbl.Text = oReader["CompanyName"].ToString();
                        regBldgCode_Lbl.Text = oReader["regBldgCode"].ToString();
                        regBldgRoom_Lbl.Text = oReader["regBldgRoom"].ToString();
                        regBldgFloor_Lbl.Text = oReader["regBldgFloor"].ToString();
                        regBldgHouseNo_Lbl.Text = oReader["regBldgHouseNo"].ToString();
                        regStreetName_Lbl.Text = oReader["regStreetName"].ToString();
                        regCity_Lbl.Text = oReader["regCity"].ToString();
                        regProvince_Lbl.Text = oReader["regProvince"].ToString();
                        regCountry_Lbl.Text = oReader["regCountry"].ToString();
                        regPostal_Lbl.Text = oReader["regPostal"].ToString();
                        regArea_Lbl.Text = oReader["regArea"].ToString();
                        regOwned_Lbl.Text = oReader["regOwned"].ToString();
                        regOwnedAttachment_Lbl.Text = oReader["regOwnedAttachment"].ToString() != "" ? "<div style=\"float:left; width:30px;\"><img src=\"images/attachment.png\" /></div> <div style='float:left;'>" : "<div style=\"float:left; width:30px;\"><img src=\"images/attachment.png\" /></div>  No attached file";
                        if (oReader["regOwnedAttachment"].ToString() != "")
                        {
                            string[] regOwnedAttachments1 = oReader["regOwnedAttachment"].ToString().Split(';');
                            foreach (string regOwnedAttachment1 in regOwnedAttachments1)
                            {
                                if (regOwnedAttachment1 != "")
                                {
                                    regOwnedAttachment_Lbl.Text = regOwnedAttachment_Lbl.Text + regOwnedAttachment1.Trim() != "" ? regOwnedAttachment_Lbl.Text + "<a href='" + regOwnedAttachment1.Trim() + "' target='_blank'>Attached file</a><br>" : "";
                                }
                            }
                        }
                        regOwnedAttachment_Lbl.Text = oReader["regOwnedAttachment"].ToString() != "" ? regOwnedAttachment_Lbl.Text + "</div>" : regOwnedAttachment_Lbl.Text + "";
                        //regOwnedAttachment_Lbl.Text = oReader["regOwnedAttachment"].ToString() != "" ? "<div style=\"float:left; width:30px;\"><img src=\"images/attachment.png\" /></div> <a href='" + oReader["regOwnedAttachment"].ToString() + "' target='_blank'>Attached file</a>" : "<div style=\"float:left; width:30px;\"><img src=\"images/attachment.png\" /></div>  No attached file";
                        conBidName_Lbl.Text = oReader["conBidName"].ToString();
                        conBidPosition_Lbl.Text = oReader["conBidPosition"].ToString();
                        conBidEmail_Lbl.Text = oReader["conBidEmail"].ToString();
                        conBidMobile_Lbl.Text = oReader["conBidMobile"].ToString();
                        conBidTelNo_Lbl.Text = oReader["conBidTelNo"].ToString();
                        conBidFaxNo_Lbl.Text = oReader["conBidFaxNo"].ToString();
                        conLegName_Lbl.Text = oReader["conLegName"].ToString();
                        conLegPosition_Lbl.Text = oReader["conLegPosition"].ToString();
                        conLegEmail_Lbl.Text = oReader["conLegEmail"].ToString();
                        conLegMobile_Lbl.Text = oReader["conLegMobile"].ToString();
                        conLegTelNo_Lbl.Text = oReader["conLegTelNo"].ToString();
                        parentCompanyName_Lbl.Text = oReader["parentCompanyName"].ToString();
                        parentCompanyAddr_Lbl.Text = oReader["parentCompanyAddr"].ToString();
                        repOfcCompanyName_Lbl.Text = oReader["repOfcCompanyName"].ToString();
                        repOfcCompanyAddr_Lbl.Text = oReader["repOfcCompanyAddr"].ToString();
                        repOfcEmail_Lbl.Text = oReader["repOfcEmail"].ToString();
                        repOfcTelNo_Lbl.Text = oReader["repOfcTelNo"].ToString();
                        repOfcFaxNo_Lbl.Text = oReader["repOfcFaxNo"].ToString();
                    }
                }
            }
        }
        
    }

    void SaveToDB()
    {
        string connstring = ConfigurationManager.ConnectionStrings["AVAConnectionString"].ConnectionString;
        string sCommand = "";

        query = "IF NOT EXISTS (SELECT 1 FROM tblVendorInformation WHERE VendorId = @VendorId) BEGIN INSERT INTO tblVendorInformation (VendorId, VendorCode, CompanyName, regBldgCode, regBldgRoom, regBldgFloor, regBldgHouseNo, regStreetName, regCity, regProvince, regCountry, regPostal, regArea, regOwned, regOwnedAttachment, conBidName, conBidPosition, conBidEmail, conBidMobile, conBidTelNo, conBidFaxNo, conLegName, conLegPosition, conLegEmail, conLegMobile, conLegTelNo, conLegFaxNo, parentCompanyName, parentCompanyAddr, repOfcCompanyName, repOfcCompanyAddr, repOfcEmail, repOfcTelNo, repOfcFaxNo) VALUES (@VendorId, @VendorCode, @CompanyName, @regBldgCode, @regBldgRoom, @regBldgFloor, @regBldgHouseNo, @regStreetName, @regCity, @regProvince, @regCountry, @regPostal, @regArea, @regOwned, @regOwnedAttachment, @conBidName, @conBidPosition, @conBidEmail, @conBidMobile, @conBidTelNo, @conBidFaxNo, @conLegName, @conLegPosition, @conLegEmail, @conLegMobile, @conLegTelNo, @conLegFaxNo,  @parentCompanyName, @parentCompanyAddr, @repOfcCompanyName, @repOfcCompanyAddr, @repOfcEmail, @repOfcTelNo, @repOfcFaxNo) END ELSE BEGIN UPDATE tblVendorInformation SET VendorCode=@VendorCode, CompanyName=@CompanyName, regBldgCode=@regBldgCode, regBldgRoom=@regBldgRoom, regBldgFloor=@regBldgFloor, regBldgHouseNo=@regBldgHouseNo, regStreetName=@regStreetName, regCity=@regCity, regProvince=@regProvince, regCountry=@regCountry, regPostal=@regPostal, regArea=@regArea, regOwned=@regOwned, regOwnedAttachment=@regOwnedAttachment, conBidName=@conBidName, conBidPosition=@conBidPosition, conBidEmail=@conBidEmail, conBidMobile=@conBidMobile, conBidTelNo=@conBidTelNo, conBidFaxNo=@conBidFaxNo, conLegName=@conLegName, conLegPosition=@conLegPosition, conLegEmail=@conLegEmail, conLegMobile=@conLegMobile, conLegTelNo=@conLegTelNo, conLegFaxNo=@conLegFaxNo, parentCompanyName=@parentCompanyName, parentCompanyAddr=@parentCompanyAddr, repOfcCompanyName=@repOfcCompanyName, repOfcCompanyAddr=@repOfcCompanyAddr, repOfcEmail=@repOfcEmail, repOfcTelNo=@repOfcTelNo, repOfcFaxNo=@repOfcFaxNo WHERE VendorId=@VendorId END";
        //query = "sp_GetVendorInformation"; //##storedProcedure
        using (conn = new SqlConnection(connstring))
        {
            using (cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@VendorId", VendorId);
                cmd.Parameters.AddWithValue("@VendorCode", CompanyName.Value.ToString().Trim());
                cmd.Parameters.AddWithValue("@CompanyName", CompanyName.Value.ToString().Trim());
                cmd.Parameters.AddWithValue("@regBldgCode", regBldgCode.Value.ToString().Trim());
                cmd.Parameters.AddWithValue("@regBldgRoom", regBldgRoom.Value.ToString().Trim());
                cmd.Parameters.AddWithValue("@regBldgFloor", regBldgFloor.Value.ToString().Trim());
                cmd.Parameters.AddWithValue("@regBldgHouseNo", regBldgHouseNo.Value.ToString().Trim());
                cmd.Parameters.AddWithValue("@regStreetName", regStreetName.Value.ToString().Trim());
                cmd.Parameters.AddWithValue("@regCity", regCity.Value.ToString().Trim());
                cmd.Parameters.AddWithValue("@regProvince", regProvince.Value.ToString().Trim());
                cmd.Parameters.AddWithValue("@regCountry", regCountry.Value.ToString().Trim());
                cmd.Parameters.AddWithValue("@regPostal", regPostal.Value.ToString().Trim());
                cmd.Parameters.AddWithValue("@regArea", regArea.Value.ToString().Trim());
                cmd.Parameters.AddWithValue("@regOwned", regOwned.SelectedValue.ToString().Trim());
                cmd.Parameters.AddWithValue("@regOwnedAttachment", regOwnedAttachment.Value.ToString().Trim());
                cmd.Parameters.AddWithValue("@conBidName", conBidName.Value.ToString().Trim());
                cmd.Parameters.AddWithValue("@conBidPosition", conBidPosition.Value.ToString().Trim());
                cmd.Parameters.AddWithValue("@conBidEmail", conBidEmail.Value.ToString().Trim());
                cmd.Parameters.AddWithValue("@conBidMobile", conBidMobile.Value.ToString().Trim());
                cmd.Parameters.AddWithValue("@conBidTelNo", conBidTelNo.Value.ToString().Trim());
                cmd.Parameters.AddWithValue("@conBidFaxNo", conBidFaxNo.Value.ToString().Trim());
                cmd.Parameters.AddWithValue("@conLegName", conLegName.Value.ToString().Trim());
                cmd.Parameters.AddWithValue("@conLegPosition", conLegPosition.Value.ToString().Trim());
                cmd.Parameters.AddWithValue("@conLegEmail", conLegEmail.Value.ToString().Trim());
                cmd.Parameters.AddWithValue("@conLegMobile", conLegMobile.Value.ToString().Trim());
                cmd.Parameters.AddWithValue("@conLegTelNo", conLegTelNo.Value.ToString().Trim());
                cmd.Parameters.AddWithValue("@conLegFaxNo", conLegFaxNo.Value.ToString().Trim());
                cmd.Parameters.AddWithValue("@parentCompanyName", parentCompanyName.Value.ToString().Trim());
                cmd.Parameters.AddWithValue("@parentCompanyAddr", parentCompanyAddr.Value.ToString().Trim());
                cmd.Parameters.AddWithValue("@repOfcCompanyName", repOfcCompanyName.Value.ToString().Trim());
                cmd.Parameters.AddWithValue("@repOfcCompanyAddr", repOfcCompanyAddr.Value.ToString().Trim());
                cmd.Parameters.AddWithValue("@repOfcEmail", repOfcEmail.Value.ToString().Trim());
                cmd.Parameters.AddWithValue("@repOfcTelNo", repOfcTelNo.Value.ToString().Trim());
                cmd.Parameters.AddWithValue("@repOfcFaxNo", repOfcFaxNo.Value.ToString().Trim());
                conn.Open(); cmd.ExecuteNonQuery();
            }
        }
        


        //CLEAR tblVendorBranches FROM USER
        sCommand = "DELETE FROM tblVendorBranches WHERE VendorId = " + VendorId.ToString();        
        SqlHelper.ExecuteNonQuery(connstring, CommandType.Text, sCommand);

        numRowsTbl = Convert.ToInt32(Request.Form["VendorBranchesCounter"].ToString());
        for (int i = 1; i <= numRowsTbl; i++)
        {
            query = "INSERT INTO tblVendorBranches (VendorId, brAddr, brUsedAs, brEmplNo, brArea, brOwned) VALUES (@VendorId, @brAddr, @brUsedAs, @brEmplNo, @brArea, @brOwned)";
            //query = "sp_GetVendorInformation"; //##storedProcedure
            using (conn = new SqlConnection(connstring))
            {
                using (cmd = new SqlCommand(query, conn))
                {
                    //cmd.CommandType = CommandType.StoredProcedure; //##storedProcedure
                    cmd.Parameters.AddWithValue("@VendorId", VendorId);
                    cmd.Parameters.AddWithValue("@brAddr", Request.Form["brAddr" + i].ToString());
                    cmd.Parameters.AddWithValue("@brUsedAs", Request.Form["brUsedAs" + i].ToString());
                    cmd.Parameters.AddWithValue("@brEmplNo", Request.Form["brEmplNo" + i].ToString());
                    cmd.Parameters.AddWithValue("@brArea", Request.Form["brArea" + i].ToString());
                    cmd.Parameters.AddWithValue("@brOwned", Request.Form["brOwned" + i].ToString());
                    conn.Open(); cmd.ExecuteNonQuery();
                }
            }
        }




        //CLEAR tblVendorBranches FROM USER
        sCommand = "DELETE FROM tblVendorSubsidiaries WHERE VendorId = " + VendorId.ToString();
        SqlHelper.ExecuteNonQuery(connstring, CommandType.Text, sCommand);
        numRowsTbl = Convert.ToInt32(Request.Form["SubsidiaryCounter"].ToString());
        for (int i = 1; i <= numRowsTbl; i++)
        {
            query = "INSERT INTO tblVendorSubsidiaries (VendorId, subCompanyName, subAddr, subEquity, subOwned) VALUES (@VendorId, @subCompanyName, @subAddr, @subEquity, @subOwned)";
            //query = "sp_GetVendorInformation"; //##storedProcedure
            using (conn = new SqlConnection(connstring))
            {
                using (cmd = new SqlCommand(query, conn))
                {
                    //cmd.CommandType = CommandType.StoredProcedure; //##storedProcedure
                    cmd.Parameters.AddWithValue("@VendorId", VendorId);
                    cmd.Parameters.AddWithValue("@subCompanyName", Request.Form["subCompanyName" + i].ToString());
                    cmd.Parameters.AddWithValue("@subAddr", Request.Form["subAddr" + i].ToString());
                    cmd.Parameters.AddWithValue("@subEquity", Request.Form["subEquity" + i].ToString()!="" ? Convert.ToDouble(Request.Form["subEquity" + i].ToString()) : 0);
                    cmd.Parameters.AddWithValue("@subOwned", Request.Form["subOwned" + i].ToString());
                    conn.Open(); cmd.ExecuteNonQuery();
                }
            }
        }
        
        
        //foreach (RepeaterItem oRptItem in repeaterSubsidiary.Items)
        //{
        //    if (oRptItem.ItemType == ListItemType.Item || oRptItem.ItemType == ListItemType.AlternatingItem)
        //    {
        //        //HtmlInputHidden osubSubsidiaryId = (System.Web.UI.HtmlControls.HtmlInputHidden)oRptItem.FindControl("subSubsidiaryId");
        //        //HtmlInputText osubCompanyName = (System.Web.UI.HtmlControls.HtmlInputText)oRptItem.FindControl("subCompanyName");
        //        //HtmlInputText osubAddr = (System.Web.UI.HtmlControls.HtmlInputText)oRptItem.FindControl("subAddr");
        //        //HtmlInputText osubEquity = (System.Web.UI.HtmlControls.HtmlInputText)oRptItem.FindControl("subEquity");
        //        //HtmlInputText osubOwned = (System.Web.UI.HtmlControls.HtmlInputText)oRptItem.FindControl("subOwned");
        //        //sCommand = "INSERT INTO tblVendorSubsidiaries (VendorId, subCompanyName, subAddr, subEquity, subOwned) VALUES (";
        //        //sCommand = sCommand + Session["UserId"] + ", '";
        //        //sCommand = sCommand + osubCompanyName.Value.Trim().ToString() + "', '";
        //        //sCommand = sCommand + osubAddr.Value.Trim().ToString() + "', ";
        //        //sCommand = sCommand + osubEquity.Value.Trim().ToString() + ", '";
        //        //sCommand = sCommand + osubOwned.Value.Trim().ToString();
        //        //sCommand = sCommand + "')";
        //        ////Response.Write(sCommand);
        //        //SqlHelper.ExecuteNonQuery(connstring, CommandType.Text, sCommand);
        //    }
        //}

        string control1 = Request.Form["__EVENTTARGET"];
        if (control1 == "continueStp")
        {
            Response.Redirect("vendor_02_productServices.aspx");
        }
    }


    //#####################################################3

    protected void rptGeneral_Selecting(object sender, SqlDataSourceSelectingEventArgs e)
    {
        e.Command.Parameters["@VendorId"].Value = VendorId;
    }
}