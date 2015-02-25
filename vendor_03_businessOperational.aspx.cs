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

public partial class vendor_03_businessOperational : System.Web.UI.Page
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
            repeaterBankInformation.DataBind(); repeaterBankInformation_Lbl.Visible = false;
            tbl01_Lbl.Visible = false;
            tbl02_Lbl.Visible = false;
            tbl03_Lbl.Visible = false;
            tbl04_Lbl.Visible = false;
            tbl04a_Lbl.Visible = false;
            tbl06_Lbl.Visible = false;
        }
        else
        {
            PopulateFields_Lbl();
            repeaterBankInformation.Visible = false; repeaterBankInformation_Lbl.DataBind();
            tbl01.Visible = false;
            tbl02.Visible = false;
            tbl03.Visible = false;
            tbl04.Visible = false;
            tbl04a.Visible = false;
            tbl06.Visible = false;
            add5.Visible = false;
            createBt.Visible = createBt1.Visible = false;
        }
        //repeaterInsuranceInformation.DataBind();
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

        query = "SELECT * FROM tblVendorInformation WHERE VendorId = @VendorId";
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
                        manResourceRegular.Value = oReader["manResourceRegular"].ToString() != "" ? oReader["manResourceRegular"].ToString() : "0";
                        manResourceContractual.Value = oReader["manResourceContractual"].ToString() != "" ? oReader["manResourceContractual"].ToString() : "0";
                        manResourceTotal.Value = oReader["manResourceTotal"].ToString() != "" ? oReader["manResourceTotal"].ToString() : "0";

                        benefitsPagibig.SelectedValue = oReader["benefitsPagibig"].ToString();
                        benefitsPHIC.SelectedValue = oReader["benefitsPHIC"].ToString();
                        benefitsSSS.SelectedValue = oReader["benefitsSSS"].ToString();
                        benefits13th.SelectedValue = oReader["benefits13th"].ToString();
                        benefitsOtherMed.SelectedValue = oReader["benefitsOtherMed"].ToString();
                        benefitsOthers.Value = oReader["benefitsOthers"].ToString();
                        //benefitsPagibigFileNameLbl.Text = oReader["benefitsPagibigFileName"].ToString()!="" ? "<a href='" + oReader["benefitsPagibigFileName"].ToString() + "' target='_blank'>" + oReader["benefitsPagibigFileName"].ToString() + "</a>" : "Attach file";
                        if (oReader["benefitsPagibigFileName"].ToString() != "")
                        {
                            string[] benefitsPagibigFileNames1 = oReader["benefitsPagibigFileName"].ToString().Split(';');
                            foreach (string benefitsPagibigFileName1 in benefitsPagibigFileNames1)
                            {
                                benefitsPagibigFileNameLbl.Text = benefitsPagibigFileName1.Trim() != "" ? benefitsPagibigFileNameLbl.Text + "<div><a href='" + benefitsPagibigFileName1.Trim() + "' target='_blank'>PAG-IBIG Attached file</a> <img src=\"images/xicon.png\" style=\"margin-left:10px; padding-top:5px; \" id=\"benefitsPagibigFileNamex\" onclick=\"$(this).parent(\'div\').html(\'\');FileattchValues($(\'#ContentPlaceHolder1_benefitsPagibigFileName\').val(),\'" + benefitsPagibigFileName1.Trim() + "\',\'benefitsPagibigFileName\');\" /><br></div>" : "";
                            }
                        }
                        else
                        {
                            benefitsPagibigFileNameLbl.Text = "PAG-IBIG Attach file<br>";
                        }
                        benefitsPagibigFileName.Value = oReader["benefitsPagibigFileName"].ToString();
                        //benefitsPHICFileNameLbl.Text = oReader["benefitsPHICFileName"].ToString() !="" ? "<a href='" + oReader["benefitsPHICFileName"].ToString() + "' target='_blank'>" + oReader["benefitsPHICFileName"].ToString() + "</a>" : "Attach file";
                        if (oReader["benefitsPHICFileName"].ToString() != "")
                        {
                            string[] benefitsPHICFileNames1 = oReader["benefitsPHICFileName"].ToString().Split(';');
                            foreach (string benefitsPHICFileName1 in benefitsPHICFileNames1)
                            {
                                benefitsPHICFileNameLbl.Text = benefitsPHICFileName1.Trim() != "" ? benefitsPHICFileNameLbl.Text + "<div><a href='" + benefitsPHICFileName1.Trim() + "' target='_blank'>PHILHEALTH Attached file</a> <img src=\"images/xicon.png\" style=\"margin-left:10px; padding-top:5px; \" id=\"benefitsPHICFileNamex\" onclick=\"$(this).parent(\'div\').html(\'\');FileattchValues($(\'#ContentPlaceHolder1_benefitsPHICFileName\').val(),\'" + benefitsPHICFileName1.Trim() + "\',\'benefitsPHICFileName\');\" /><br></div>" : "";
                            }
                        }
                        else
                        {
                            benefitsPHICFileNameLbl.Text = "PHILHEALTH Attach file<br>";
                        }
                        benefitsPHICFileName.Value = oReader["benefitsPHICFileName"].ToString();
                        //benefitsSSSFileNameLbl.Text = oReader["benefitsSSSFileName"].ToString()!="" ? "<a href='" + oReader["benefitsSSSFileName"].ToString() + "' target='_blank'>" + oReader["benefitsSSSFileName"].ToString() + "</a>" : "Attach file";
                        if (oReader["benefitsSSSFileName"].ToString() != "")
                        {
                            string[] benefitsSSSFileNames1 = oReader["benefitsSSSFileName"].ToString().Split(';');
                            foreach (string benefitsSSSFileName1 in benefitsSSSFileNames1)
                            {
                                benefitsSSSFileNameLbl.Text = benefitsSSSFileName1.Trim() != "" ? benefitsSSSFileNameLbl.Text + "<div><a href='" + benefitsSSSFileName1.Trim() + "' target='_blank'>SSS Attached file</a> <img src=\"images/xicon.png\" style=\"margin-left:10px; padding-top:5px; \" id=\"benefitsSSSFileNamex\" onclick=\"$(this).parent(\'div\').html(\'\');FileattchValues($(\'#ContentPlaceHolder1_benefitsSSSFileName\').val(),\'" + benefitsSSSFileName1.Trim() + "\',\'benefitsSSSFileName\');\" /><br></div>" : "";
                            }
                        }
                        else
                        {
                            benefitsSSSFileNameLbl.Text = "SSS Attach file<br>";
                        }
                        benefitsSSSFileName.Value = oReader["benefitsSSSFileName"].ToString();
                        //benefitsOthersFileNameLbl.Text = oReader["benefitsOthersFileName"].ToString()!="" ? "<a href='" + oReader["benefitsOthersFileName"].ToString() + "' target='_blank'>" + oReader["benefitsOthersFileName"].ToString() + "</a>" : "Attach file";
                        if (oReader["benefitsOthersFileName"].ToString() != "")
                        {
                            string[] benefitsOthersFileNames1 = oReader["benefitsOthersFileName"].ToString().Split(';');
                            foreach (string benefitsOthersFileName1 in benefitsOthersFileNames1)
                            {
                                benefitsOthersFileNameLbl.Text = benefitsOthersFileName1.Trim() != "" ? benefitsOthersFileNameLbl.Text + "<div><a href='" + benefitsOthersFileName1.Trim() + "' target='_blank'>Attached file</a> <img src=\"images/xicon.png\" style=\"margin-left:10px; padding-top:5px; \" id=\"benefitsOthersFileNamex\" onclick=\"$(this).parent(\'div\').html(\'\');FileattchValues($(\'#ContentPlaceHolder1_benefitsOthersFileName\').val(),\'" + benefitsOthersFileName1.Trim() + "\',\'benefitsOthersFileName\');\" /><br></div>" : "";
                            }
                        }
                        else
                        {
                            benefitsOthersFileNameLbl.Text = "Attach file<br>";
                        }
                        benefitsOthersFileName.Value = oReader["benefitsOthersFileName"].ToString();
                        //assetsMachineriesFileNameLbl.Text = oReader["assetsMachineriesFileName"].ToString() != "" ? "<a href='" + oReader["assetsMachineriesFileName"].ToString() + "' target='_blank'>" + oReader["assetsMachineriesFileName"].ToString() + "</a>" : "Attach file";
                        if (oReader["assetsMachineriesFileName"].ToString() != "")
                        {
                            string[] assetsMachineriesFileNames1 = oReader["assetsMachineriesFileName"].ToString().Split(';');
                            foreach (string assetsMachineriesFileName1 in assetsMachineriesFileNames1)
                            {
                                assetsMachineriesFileNameLbl.Text = assetsMachineriesFileName1.Trim() != "" ? assetsMachineriesFileNameLbl.Text + "<div><a href='" + assetsMachineriesFileName1.Trim() + "' target='_blank'>Attached file</a> <img src=\"images/xicon.png\" style=\"margin-left:10px; padding-top:5px; \" id=\"assetsMachineriesFileNamex\" onclick=\"$(this).parent(\'div\').html(\'\');FileattchValues($(\'#ContentPlaceHolder1_assetsMachineriesFileName\').val(),\'" + assetsMachineriesFileName1.Trim() + "\',\'assetsMachineriesFileName\');\" /><br></div>" : "";
                            }
                        }
                        else
                        {
                            assetsMachineriesFileNameLbl.Text = "Attach file<br>";
                        }
                        assetsMachineriesFileName.Value = oReader["assetsMachineriesFileName"].ToString();
                        //assetsCompanyProfileFileNameLbl.Text = oReader["assetsCompanyProfileFileName"].ToString() != "" ? "<a href='" + oReader["assetsCompanyProfileFileName"].ToString() + "' target='_blank'>" + oReader["assetsCompanyProfileFileName"].ToString() + "</a>" : "Attach file";
                        if (oReader["assetsCompanyProfileFileName"].ToString() != "")
                        {
                            string[] assetsCompanyProfileFileNames1 = oReader["assetsCompanyProfileFileName"].ToString().Split(';');
                            foreach (string assetsCompanyProfileFileName1 in assetsCompanyProfileFileNames1)
                            {
                                assetsCompanyProfileFileNameLbl.Text = assetsCompanyProfileFileName1.Trim() != "" ? assetsCompanyProfileFileNameLbl.Text + "<div><a href='" + assetsCompanyProfileFileName1.Trim() + "' target='_blank'>Attached file</a> <img src=\"images/xicon.png\" style=\"margin-left:10px; padding-top:5px; \" id=\"assetsCompanyProfileFileNamex\" onclick=\"$(this).parent(\'div\').html(\'\');FileattchValues($(\'#ContentPlaceHolder1_assetsCompanyProfileFileName\').val(),\'" + assetsCompanyProfileFileName1.Trim() + "\',\'assetsCompanyProfileFileName\');\" /><br></div>" : "";
                            }
                        }
                        else
                        {
                            assetsCompanyProfileFileNameLbl.Text = "Attach file<br>";
                        }
                        assetsCompanyProfileFileName.Value = oReader["assetsCompanyProfileFileName"].ToString();
                        //assetsOthersFileNameLbl.Text = oReader["assetsOthersFileName"].ToString() != "" ? "<a href='" + oReader["assetsOthersFileName"].ToString() + "' target='_blank'>" + oReader["assetsOthersFileName"].ToString() + "</a>" : "Attach file";
                        if (oReader["assetsOthersFileName"].ToString() != "")
                        {
                            string[] assetsOthersFileNames1 = oReader["assetsOthersFileName"].ToString().Split(';');
                            foreach (string assetsOthersFileName1 in assetsOthersFileNames1)
                            {
                                assetsOthersFileNameLbl.Text = assetsOthersFileName1.Trim() != "" ? assetsOthersFileNameLbl.Text + "<div><a href='" + assetsOthersFileName1.Trim() + "' target='_blank'>Attached file</a> <img src=\"images/xicon.png\" style=\"margin-left:10px; padding-top:5px; \" id=\"assetsOthersFileNamex\" onclick=\"$(this).parent(\'div\').html(\'\');FileattchValues($(\'#ContentPlaceHolder1_assetsOthersFileName\').val(),\'" + assetsOthersFileName1.Trim() + "\',\'assetsOthersFileName\');\" /><br></div>" : "";
                            }
                        }
                        else
                        {
                            assetsOthersFileNameLbl.Text = "Attach file<br>";
                        }
                        assetsOthersFileName.Value = oReader["assetsOthersFileName"].ToString();

                        facltyLandTxt.Value = oReader["facltyLandTxt"].ToString();
                        facltyLandOwned.SelectedValue = oReader["facltyLandOwned"].ToString();
                        facltyBldgTxt.Value = oReader["facltyBldgTxt"].ToString();
                        facltyBldgOwned.SelectedValue = oReader["facltyBldgOwned"].ToString();
                        facltyLocation.SelectedValue = oReader["facltyLocation"].ToString();
                        facltyPremissesAs.SelectedValue = oReader["facltyPremissesAs"].ToString();
                        insurInfoEmplyrLia_Limit.Value = oReader["insurInfoEmplyrLia_Limit"].ToString();
                        insurInfoEmplyrLia_InsuCo.Value = oReader["insurInfoEmplyrLia_InsuCo"].ToString();
                        insurInfoPropInsu_Limit.Value = oReader["insurInfoPropInsu_Limit"].ToString();
                        insurInfoPropInsu_InsuCo.Value = oReader["insurInfoPropInsu_InsuCo"].ToString();
                        insurInfoPartyLia_Limit.Value = oReader["insurInfoPartyLia_Limit"].ToString();
                        insurInfoPartyLia_InsuCo.Value = oReader["insurInfoPartyLia_InsuCo"].ToString();
                        insurInfoOthers_Limit.Value = oReader["insurInfoOthers_Limit"].ToString();
                        insurInfoOthers_InsuCo.Value = oReader["insurInfoOthers_InsuCo"].ToString();
                    }
                }
            }
        }


        query = "SELECT * FROM tblVendorBackOnKeyPersonnel WHERE VendorId= @VendorId AND Position = @Position";
        //query = "sp_GetVendorInformation"; //##storedProcedure
        using (conn = new SqlConnection(connstring))
        {
            using (cmd = new SqlCommand(query, conn))
            {
                //cmd.CommandType = CommandType.StoredProcedure; //##storedProcedure
                cmd.Parameters.AddWithValue("@VendorId", VendorId);
                cmd.Parameters.AddWithValue("@Position", "ceo");
                conn.Open();
                //Process results
                oReader = cmd.ExecuteReader();
                if (oReader.HasRows)
                {
                    while (oReader.Read())
                    {
                        ceoName.Value = oReader["Name"].ToString();
                        ceoDegreeEarned.Value = oReader["DegreeEarned"].ToString();
                        ceoEducInstitution.Value = oReader["EducInstitution"].ToString();
                        ceoYearGraduated.Value = oReader["YearGraduated"].ToString();
                        ceoNationality.Value = oReader["Nationality"].ToString();
                        ceoAge.Value = oReader["Age"].ToString();
                        ceoPastWorkExp.Value = oReader["PastWorkExp"].ToString();
                        ceoCRLbl.Text = oReader["CurriculumVitae"].ToString() != "" ? "<a href='" + oReader["CurriculumVitae"].ToString() + "' target='_blank'>Attachment</a>" : "Attach file";
                        ceoCR.Value = oReader["CurriculumVitae"].ToString();
                    }
                }
            }
        }


        query = "SELECT * FROM tblVendorBackOnKeyPersonnel WHERE VendorId= @VendorId AND Position = @Position";
        //query = "sp_GetVendorInformation"; //##storedProcedure
        using (conn = new SqlConnection(connstring))
        {
            using (cmd = new SqlCommand(query, conn))
            {
                //cmd.CommandType = CommandType.StoredProcedure; //##storedProcedure
                cmd.Parameters.AddWithValue("@VendorId", VendorId);
                cmd.Parameters.AddWithValue("@Position", "cfo");
                conn.Open();
                //Process results
                oReader = cmd.ExecuteReader();
                if (oReader.HasRows)
                {
                    while (oReader.Read())
                    {
                        cfoName.Value = oReader["Name"].ToString();
                        cfoDegreeEarned.Value = oReader["DegreeEarned"].ToString();
                        cfoEducInstitution.Value = oReader["EducInstitution"].ToString();
                        cfoYearGraduated.Value = oReader["YearGraduated"].ToString();
                        cfoNationality.Value = oReader["Nationality"].ToString();
                        cfoAge.Value = oReader["Age"].ToString();
                        cfoPastWorkExp.Value = oReader["PastWorkExp"].ToString();
                        cfoCRLbl.Text = oReader["CurriculumVitae"].ToString() != "" ? "<a href='" + oReader["CurriculumVitae"].ToString() + "' target='_blank'>Attachment</a>" : "Attachment";
                        cfoCR.Value = oReader["CurriculumVitae"].ToString();
                    }
                }
            }
        }


        query = "SELECT * FROM tblVendorBackOnKeyPersonnel WHERE VendorId= @VendorId AND Position = @Position";
        //query = "sp_GetVendorInformation"; //##storedProcedure
        using (conn = new SqlConnection(connstring))
        {
            using (cmd = new SqlCommand(query, conn))
            {
                //cmd.CommandType = CommandType.StoredProcedure; //##storedProcedure
                cmd.Parameters.AddWithValue("@VendorId", VendorId);
                cmd.Parameters.AddWithValue("@Position", "coo");
                conn.Open();
                //Process results
                oReader = cmd.ExecuteReader();
                if (oReader.HasRows)
                {
                    while (oReader.Read())
                    {
                        cooName.Value = oReader["Name"].ToString();
                        cooDegreeEarned.Value = oReader["DegreeEarned"].ToString();
                        cooEducInstitution.Value = oReader["EducInstitution"].ToString();
                        cooYearGraduated.Value = oReader["YearGraduated"].ToString();
                        cooNationality.Value = oReader["Nationality"].ToString();
                        cooAge.Value = oReader["Age"].ToString();
                        cooPastWorkExp.Value = oReader["PastWorkExp"].ToString();
                        cooCRLbl.Text = oReader["CurriculumVitae"].ToString() != "" ? "<a href='" + oReader["CurriculumVitae"].ToString() + "' target='_blank'>Attachment</a>" : "Attachment";
                        cooCR.Value = oReader["CurriculumVitae"].ToString();
                    }
                }
            }
        }

        query = "SELECT * FROM tblVendorBackOnKeyPersonnel WHERE VendorId= @VendorId AND Position = @Position";
        //query = "sp_GetVendorInformation"; //##storedProcedure
        using (conn = new SqlConnection(connstring))
        {
            using (cmd = new SqlCommand(query, conn))
            {
                //cmd.CommandType = CommandType.StoredProcedure; //##storedProcedure
                cmd.Parameters.AddWithValue("@VendorId", VendorId);
                cmd.Parameters.AddWithValue("@Position", "ptm");
                conn.Open();
                //Process results
                oReader = cmd.ExecuteReader();
                if (oReader.HasRows)
                {
                    while (oReader.Read())
                    {
                        ptmName.Value = oReader["Name"].ToString();
                        ptmDegreeEarned.Value = oReader["DegreeEarned"].ToString();
                        ptmEducInstitution.Value = oReader["EducInstitution"].ToString();
                        ptmYearGraduated.Value = oReader["YearGraduated"].ToString();
                        ptmNationality.Value = oReader["Nationality"].ToString();
                        ptmAge.Value = oReader["Age"].ToString();
                        ptmPastWorkExp.Value = oReader["PastWorkExp"].ToString();
                        ptmCRLbl.Text = oReader["CurriculumVitae"].ToString() != "" ? "<a href='" + oReader["CurriculumVitae"].ToString() + "' target='_blank'>Attachment</a>" : "Attachment";
                        ptmCR.Value = oReader["CurriculumVitae"].ToString();
                    }
                }
            }
        }
    }

    void PopulateFields_Lbl()
    {

        query = "SELECT * FROM tblVendorInformation WHERE VendorId = @VendorId";
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
                        manResourceRegular_Lbl.Text = oReader["manResourceRegular"].ToString()!="" ? oReader["manResourceRegular"].ToString() : "0";
                        manResourceContractual_Lbl.Text = oReader["manResourceContractual"].ToString() != "" ? oReader["manResourceContractual"].ToString() : "0";
                        manResourceTotal_Lbl.Text = oReader["manResourceTotal"].ToString() != "" ? oReader["manResourceTotal"].ToString() : "0";

                        benefitsPagibig_Lbl.Text = oReader["benefitsPagibig"].ToString()=="1" ? "Yes" : "No";
                        benefitsPHIC_Lbl.Text = oReader["benefitsPHIC"].ToString() == "1" ? "Yes" : "No";
                        benefitsSSS_Lbl.Text = oReader["benefitsSSS"].ToString() == "1" ? "Yes" : "No";
                        benefits13th_Lbl.Text = oReader["benefits13th"].ToString() == "1" ? "Yes" : "No";
                        benefitsOtherMed_Lbl.Text = oReader["benefitsOtherMed"].ToString() == "1" ? "Yes" : "No";
                        benefitsOthers_Lbl.Text = oReader["benefitsOthers"].ToString() == "1" ? "Yes" : "No";
                        //benefitsPagibigFileName_Lbl.Text = oReader["benefitsPagibigFileName"].ToString() != "" ? "<div style=\"float:left; width:30px;\"><img src=\"images/attachment.png\" /></div> <a href='" + oReader["benefitsPagibigFileName"].ToString() + "' target='_blank'>Attachment</a>" : "<div style=\"float:left; width:30px;\"><img src=\"images/attachment.png\" /></div> No attached file";
                        if (oReader["benefitsPagibigFileName"].ToString() != "")
                        {
                            string[] benefitsPagibigFileNames1 = oReader["benefitsPagibigFileName"].ToString().Split(';');
                            foreach (string benefitsPagibigFileName1 in benefitsPagibigFileNames1)
                            {
                                benefitsPagibigFileName_Lbl.Text = benefitsPagibigFileName1.Trim() != "" ? benefitsPagibigFileName_Lbl.Text + "<div><img src=\"images/attachment.png\" /> <a href='" + benefitsPagibigFileName1.Trim() + "' target='_blank'>PAG-IBIG Attached file</a><br></div>" : "";
                            }
                        }
                        else
                        {
                            benefitsPagibigFileName_Lbl.Text = "<img src=\"images/attachment.png\" /> No PAG-IBIG Attached file<br>";
                        }
                        //benefitsPHICFileName_Lbl.Text = oReader["benefitsPHICFileName"].ToString() != "" ? "<div style=\"float:left; width:30px;\"><img src=\"images/attachment.png\" /></div> <a href='" + oReader["benefitsPHICFileName"].ToString() + "' target='_blank'>Attachment</a>" : "<div style=\"float:left; width:30px;\"><img src=\"images/attachment.png\" /></div> No attached file";
                        if (oReader["benefitsPHICFileName"].ToString() != "")
                        {
                            string[] benefitsPHICFileNames1 = oReader["benefitsPHICFileName"].ToString().Split(';');
                            foreach (string benefitsPHICFileName1 in benefitsPHICFileNames1)
                            {
                                benefitsPHICFileName_Lbl.Text = benefitsPHICFileName1.Trim() != "" ? benefitsPHICFileName_Lbl.Text + "<div><img src=\"images/attachment.png\" /> <a href='" + benefitsPHICFileName1.Trim() + "' target='_blank'>PHILHEALTH Attached file</a><br></div>" : "";
                            }
                        }
                        else
                        {
                            benefitsPHICFileName_Lbl.Text = "<img src=\"images/attachment.png\" /> No PHILHEALTH Attached file<br>";
                        }
                        //benefitsSSSFileName_Lbl.Text = oReader["benefitsSSSFileName"].ToString() != "" ? "<div style=\"float:left; width:30px;\"><img src=\"images/attachment.png\" /></div> <a href='" + oReader["benefitsSSSFileName"].ToString() + "' target='_blank'>Attachment</a>" : "<div style=\"float:left; width:30px;\"><img src=\"images/attachment.png\" /></div> No attached file";
                        if (oReader["benefitsSSSFileName"].ToString() != "")
                        {
                            string[] benefitsSSSFileNames1 = oReader["benefitsSSSFileName"].ToString().Split(';');
                            foreach (string benefitsSSSFileName1 in benefitsSSSFileNames1)
                            {
                                benefitsSSSFileName_Lbl.Text = benefitsSSSFileName1.Trim() != "" ? benefitsSSSFileName_Lbl.Text + "<div><img src=\"images/attachment.png\" /> <a href='" + benefitsSSSFileName1.Trim() + "' target='_blank'>SSS Attached file</a><br></div>" : "";
                            }
                        }
                        else
                        {
                            benefitsSSSFileName_Lbl.Text = "<img src=\"images/attachment.png\" /> No SSS Attached file<br>";
                        }
                        //benefitsOthersFilename_Lbl.Text = oReader["benefitsOthersFileName"].ToString() != "" ? "<div style=\"float:left; width:30px;\"><img src=\"images/attachment.png\" /></div> <a href='" + oReader["benefitsOthersFileName"].ToString() + "' target='_blank'>Attachment</a>" : "<div style=\"float:left; width:30px;\"><img src=\"images/attachment.png\" /></div> No attached file";
                        if (oReader["benefitsOthersFileName"].ToString() != "")
                        {
                            string[] benefitsOthersFileNames1 = oReader["benefitsOthersFileName"].ToString().Split(';');
                            foreach (string benefitsOthersFileName1 in benefitsOthersFileNames1)
                            {
                                benefitsOthersFilename_Lbl.Text = benefitsOthersFileName1.Trim() != "" ? benefitsOthersFilename_Lbl.Text + "<div><img src=\"images/attachment.png\" /> <a href='" + benefitsOthersFileName1.Trim() + "' target='_blank'>Attached file</a><br></div>" : "";
                            }
                        }
                        else
                        {
                            benefitsOthersFilename_Lbl.Text = "<img src=\"images/attachment.png\" /> No Attached file<br>";
                        }
                        //benefitsPagibigFileName.Value = oReader["benefitsPagibigFileName"].ToString();
                        //benefitsPHICFileNameLbl.Text = "<a href='" + oReader["benefitsPHICFileName"].ToString() + "' target='_blank'>" + oReader["benefitsPHICFileName"].ToString() + "</a>";
                        //benefitsPHICFileName.Value = oReader["benefitsPHICFileName"].ToString();
                        //benefitsSSSFileNameLbl.Text = "<a href='" + oReader["benefitsSSSFileName"].ToString() + "' target='_blank'>" + oReader["benefitsSSSFileName"].ToString() + "</a>";
                        //benefitsSSSFileName.Value = oReader["benefitsSSSFileName"].ToString();
                        //benefitsOthersFileNameLbl.Text = "<a href='" + oReader["benefitsOthersFileName"].ToString() + "' target='_blank'>" + oReader["benefitsOthersFileName"].ToString() + "</a>";
                        //benefitsOthersFileName.Value = oReader["benefitsOthersFileName"].ToString();
                        //fileuploaded3.Text = "<a href='" + oReader["assetsMachineriesFileName"].ToString() + "' target='_blank'>" + oReader["assetsMachineriesFileName"].ToString() + "</a>";
                        //assetsMachineriesFileName_Lbl.Text = oReader["assetsMachineriesFileName"].ToString() != "" ? "<div style=\"float:left; width:30px;\"><img src=\"images/attachment.png\" /></div> <a href='" + oReader["assetsMachineriesFileName"].ToString() + "' target='_blank'>Attachment</a>" : "<div style=\"float:left; width:30px;\"><img src=\"images/attachment.png\" /></div> No attached file";
                        if (oReader["assetsMachineriesFileName"].ToString() != "")
                        {
                            string[] assetsMachineriesFileNames1 = oReader["assetsMachineriesFileName"].ToString().Split(';');
                            foreach (string assetsMachineriesFileName1 in assetsMachineriesFileNames1)
                            {
                                assetsMachineriesFileName_Lbl.Text = assetsMachineriesFileName1.Trim() != "" ? assetsMachineriesFileName_Lbl.Text + "<div><img src=\"images/attachment.png\" /> <a href='" + assetsMachineriesFileName1.Trim() + "' target='_blank'>Attached file</a><br></div>" : "";
                            }
                        }
                        else
                        {
                            assetsMachineriesFileName_Lbl.Text = "<img src=\"images/attachment.png\" /> No Attached file<br>";
                        }
                        //fileuploaded4.Text = "<a href='" + oReader["assetsCompanyProfileFileName"].ToString() + "' target='_blank'>" + oReader["assetsCompanyProfileFileName"].ToString() + "</a>";
                        //assetsCompanyProfileFileName_Lbl.Text = oReader["assetsCompanyProfileFileName"].ToString() != "" ? "<div style=\"float:left; width:30px;\"><img src=\"images/attachment.png\" /></div> <a href='" + oReader["assetsCompanyProfileFileName"].ToString() + "' target='_blank'>Attachment</a>" : "<div style=\"float:left; width:30px;\"><img src=\"images/attachment.png\" /></div> No attached file";
                        if (oReader["assetsCompanyProfileFileName"].ToString() != "")
                        {
                            string[] assetsCompanyProfileFileNames1 = oReader["assetsCompanyProfileFileName"].ToString().Split(';');
                            foreach (string assetsCompanyProfileFileName1 in assetsCompanyProfileFileNames1)
                            {
                                assetsCompanyProfileFileName_Lbl.Text = assetsCompanyProfileFileName1.Trim() != "" ? assetsCompanyProfileFileName_Lbl.Text + "<div><img src=\"images/attachment.png\" /> <a href='" + assetsCompanyProfileFileName1.Trim() + "' target='_blank'>Attached file</a><br></div>" : "";
                            }
                        }
                        else
                        {
                            assetsCompanyProfileFileName_Lbl.Text = "<img src=\"images/attachment.png\" /> No Attached file<br>";
                        }
                        //fileuploaded5.Text = "<a href='" + oReader["assetsOthersFileName"].ToString() + "' target='_blank'>" + oReader["assetsOthersFileName"].ToString() + "</a>";
                        //assetsOthersFileName_Lbl.Text = oReader["assetsOthersFileName"].ToString() != "" ? "<div style=\"float:left; width:30px;\"><img src=\"images/attachment.png\" /></div> <a href='" + oReader["assetsOthersFileName"].ToString() + "' target='_blank'>Attachment</a>" : "<div style=\"float:left; width:30px;\"><img src=\"images/attachment.png\" /></div> No attached file";
                        if (oReader["assetsOthersFileName"].ToString() != "")
                        {
                            string[] assetsOthersFileNames1 = oReader["assetsOthersFileName"].ToString().Split(';');
                            foreach (string assetsOthersFileName1 in assetsOthersFileNames1)
                            {
                                assetsOthersFileName_Lbl.Text = assetsOthersFileName1.Trim() != "" ? assetsOthersFileName_Lbl.Text + "<div><img src=\"images/attachment.png\" /> <a href='" + assetsOthersFileName1.Trim() + "' target='_blank'>Attached file</a><br></div>" : "";
                            }
                        }
                        else
                        {
                            assetsOthersFileName_Lbl.Text = "<img src=\"images/attachment.png\" /> No Attached file<br>";
                        }

                        facltyLandTxt_Lbl.Text = oReader["facltyLandTxt"].ToString();
                        facltyLandOwned_Lbl.Text = oReader["facltyLandOwned"].ToString();
                        facltyBldgTxt_Lbl.Text = oReader["facltyBldgTxt"].ToString();
                        facltyBldgOwned_Lbl.Text = oReader["facltyBldgOwned"].ToString();
                        facltyLocation_Lbl.Text = oReader["facltyLocation"].ToString();
                        facltyPremissesAs_Lbl.Text = oReader["facltyPremissesAs"].ToString();
                        insurInfoEmplyrLia_Limit_Lbl.Text = oReader["insurInfoEmplyrLia_Limit"].ToString();
                        insurInfoEmplyrLia_InsuCo_Lbl.Text = oReader["insurInfoEmplyrLia_InsuCo"].ToString();
                        insurInfoPropInsu_Limit_Lbl.Text = oReader["insurInfoPropInsu_Limit"].ToString();
                        insurInfoPropInsu_InsuCo_Lbl.Text = oReader["insurInfoPropInsu_InsuCo"].ToString();
                        insurInfoPartyLia_Limit_Lbl.Text = oReader["insurInfoPartyLia_Limit"].ToString();
                        insurInfoPartyLia_InsuCo_Lbl.Text = oReader["insurInfoPartyLia_InsuCo"].ToString();
                        insurInfoOthers_Limit_Lbl.Text = oReader["insurInfoOthers_Limit"].ToString();
                        insurInfoOthers_InsuCo_Lbl.Text = oReader["insurInfoOthers_InsuCo"].ToString();
                    }
                }
            }
        }


        query = "SELECT * FROM tblVendorBackOnKeyPersonnel WHERE VendorId= @VendorId AND Position = @Position";
        //query = "sp_GetVendorInformation"; //##storedProcedure
        using (conn = new SqlConnection(connstring))
        {
            using (cmd = new SqlCommand(query, conn))
            {
                //cmd.CommandType = CommandType.StoredProcedure; //##storedProcedure
                cmd.Parameters.AddWithValue("@VendorId", VendorId);
                cmd.Parameters.AddWithValue("@Position", "ceo");
                conn.Open();
                //Process results
                oReader = cmd.ExecuteReader();
                if (oReader.HasRows)
                {
                    while (oReader.Read())
                    {
                        ceoName_Lbl.Text = oReader["Name"].ToString();
                        ceoDegreeEarned_Lbl.Text = oReader["DegreeEarned"].ToString();
                        ceoEducInstitution_Lbl.Text = oReader["EducInstitution"].ToString();
                        ceoYearGraduated_Lbl.Text = oReader["YearGraduated"].ToString();
                        ceoNationality_Lbl.Text = oReader["Nationality"].ToString();
                        ceoAge_Lbl.Text = oReader["Age"].ToString();
                        ceoPastWorkExp_Lbl.Text = oReader["PastWorkExp"].ToString();
                        ceoCR_Lbl.Text = oReader["CurriculumVitae"].ToString() != "" ? "<div style=\"float:left; width:30px;\"><img src=\"images/attachment.png\" /></div> <a href='" + oReader["CurriculumVitae"].ToString() + "' target='_blank'>Attachment</a>" : "<div style=\"float:left; width:30px;\"><img src=\"images/attachment.png\" /></div> No attached file";
                    }
                }
            }
        }


        query = "SELECT * FROM tblVendorBackOnKeyPersonnel WHERE VendorId= @VendorId AND Position = @Position";
        //query = "sp_GetVendorInformation"; //##storedProcedure
        using (conn = new SqlConnection(connstring))
        {
            using (cmd = new SqlCommand(query, conn))
            {
                //cmd.CommandType = CommandType.StoredProcedure; //##storedProcedure
                cmd.Parameters.AddWithValue("@VendorId", VendorId);
                cmd.Parameters.AddWithValue("@Position", "cfo");
                conn.Open();
                //Process results
                oReader = cmd.ExecuteReader();
                if (oReader.HasRows)
                {
                    while (oReader.Read())
                    {
                        cfoName_Lbl.Text = oReader["Name"].ToString();
                        cfoDegreeEarned_Lbl.Text = oReader["DegreeEarned"].ToString();
                        cfoEducInstitution_Lbl.Text = oReader["EducInstitution"].ToString();
                        cfoYearGraduated_Lbl.Text = oReader["YearGraduated"].ToString();
                        cfoNationality_Lbl.Text = oReader["Nationality"].ToString();
                        cfoAge_Lbl.Text = oReader["Age"].ToString();
                        cfoPastWorkExp_Lbl.Text = oReader["PastWorkExp"].ToString();
                        cfoCR_Lbl.Text = oReader["CurriculumVitae"].ToString()!="" ? "<div style=\"float:left; width:30px;\"><img src=\"images/attachment.png\" /></div> <a href='" + oReader["CurriculumVitae"].ToString() + "' target='_blank'>Attachment</a>" : "<div style=\"float:left; width:30px;\"><img src=\"images/attachment.png\" /></div> No attached file";
                    }
                }
            }
        }


        query = "SELECT * FROM tblVendorBackOnKeyPersonnel WHERE VendorId= @VendorId AND Position = @Position";
        //query = "sp_GetVendorInformation"; //##storedProcedure
        using (conn = new SqlConnection(connstring))
        {
            using (cmd = new SqlCommand(query, conn))
            {
                //cmd.CommandType = CommandType.StoredProcedure; //##storedProcedure
                cmd.Parameters.AddWithValue("@VendorId", VendorId);
                cmd.Parameters.AddWithValue("@Position", "coo");
                conn.Open();
                //Process results
                oReader = cmd.ExecuteReader();
                if (oReader.HasRows)
                {
                    while (oReader.Read())
                    {
                        cooName_Lbl.Text = oReader["Name"].ToString();
                        cooDegreeEarned_Lbl.Text = oReader["DegreeEarned"].ToString();
                        cooEducInstitution_Lbl.Text = oReader["EducInstitution"].ToString();
                        cooYearGraduated_Lbl.Text = oReader["YearGraduated"].ToString();
                        cooNationality_Lbl.Text = oReader["Nationality"].ToString();
                        cooAge_Lbl.Text = oReader["Age"].ToString();
                        cooPastWorkExp_Lbl.Text = oReader["PastWorkExp"].ToString();
                        cooCR_Lbl.Text = oReader["CurriculumVitae"].ToString() != "" ? "<div style=\"float:left; width:30px;\"><img src=\"images/attachment.png\" /></div> <a href='" + oReader["CurriculumVitae"].ToString() + "' target='_blank'>Attachment</a>" : "<div style=\"float:left; width:30px;\"><img src=\"images/attachment.png\" /></div> No attached file";
                    }
                }
            }
        }

        query = "SELECT * FROM tblVendorBackOnKeyPersonnel WHERE VendorId= @VendorId AND Position = @Position";
        //query = "sp_GetVendorInformation"; //##storedProcedure
        using (conn = new SqlConnection(connstring))
        {
            using (cmd = new SqlCommand(query, conn))
            {
                //cmd.CommandType = CommandType.StoredProcedure; //##storedProcedure
                cmd.Parameters.AddWithValue("@VendorId", VendorId);
                cmd.Parameters.AddWithValue("@Position", "ptm");
                conn.Open();
                //Process results
                oReader = cmd.ExecuteReader();
                if (oReader.HasRows)
                {
                    while (oReader.Read())
                    {
                        ptmName_Lbl.Text = oReader["Name"].ToString();
                        ptmDegreeEarned_Lbl.Text = oReader["DegreeEarned"].ToString();
                        ptmEducInstitution_Lbl.Text = oReader["EducInstitution"].ToString();
                        ptmYearGraduated_Lbl.Text = oReader["YearGraduated"].ToString();
                        ptmNationality_Lbl.Text = oReader["Nationality"].ToString();
                        ptmAge_Lbl.Text = oReader["Age"].ToString();
                        ptmPastWorkExp_Lbl.Text = oReader["PastWorkExp"].ToString();
                        ptmCR_Lbl.Text = oReader["CurriculumVitae"].ToString() != "" ? "<div style=\"float:left; width:30px;\"><img src=\"images/attachment.png\" /></div> <a href='" + oReader["CurriculumVitae"].ToString() + "' target='_blank'>Attachment</a>" : "<div style=\"float:left; width:30px;\"><img src=\"images/attachment.png\" /></div> No attached file";
                    }
                }
            }
        }
    }

    void SaveToDB()
    {
        string connstring = ConfigurationManager.ConnectionStrings["AVAConnectionString"].ConnectionString;
        string sCommand = "";

        query = "UPDATE tblVendorInformation SET manResourceRegular=@manResourceRegular, manResourceContractual=@manResourceContractual, manResourceTotal=@manResourceTotal, benefitsPagibig=@benefitsPagibig, benefitsPagibigFileName=@benefitsPagibigFileName, benefitsPHICFileName=@benefitsPHICFileName, benefitsSSSFileName=@benefitsSSSFileName, benefitsOthersFileName=@benefitsOthersFileName, assetsMachineriesFileName=@assetsMachineriesFileName, assetsCompanyProfileFileName=@assetsCompanyProfileFileName, assetsOthersFileName=@assetsOthersFileName, benefitsPHIC=@benefitsPHIC, benefitsSSS=@benefitsSSS, benefits13th=@benefits13th, benefitsOtherMed=@benefitsOtherMed, benefitsOthers=@benefitsOthers, facltyLandTxt=@facltyLandTxt, facltyLandOwned=@facltyLandOwned, facltyBldgTxt=@facltyBldgTxt, facltyBldgOwned=@facltyBldgOwned, facltyLocation=@facltyLocation, facltyPremissesAs=@facltyPremissesAs, insurInfoEmplyrLia_Limit=@insurInfoEmplyrLia_Limit, insurInfoEmplyrLia_InsuCo=@insurInfoEmplyrLia_InsuCo, insurInfoPropInsu_Limit=@insurInfoPropInsu_Limit, insurInfoPropInsu_InsuCo=@insurInfoPropInsu_InsuCo, insurInfoPartyLia_Limit=@insurInfoPartyLia_Limit, insurInfoPartyLia_InsuCo=@insurInfoPartyLia_InsuCo, insurInfoOthers_Limit=@insurInfoOthers_Limit, insurInfoOthers_InsuCo=@insurInfoOthers_InsuCo  WHERE VendorId = @VendorId";
        //query = "sp_GetVendorInformation"; //##storedProcedure
        using (conn = new SqlConnection(connstring))
        {
            using (cmd = new SqlCommand(query, conn))
            {
                //cmd.CommandType = CommandType.StoredProcedure; //##storedProcedure
                cmd.Parameters.AddWithValue("@VendorId", VendorId);
                cmd.Parameters.AddWithValue("@manResourceRegular", manResourceRegular.Value.Trim().ToString() != "" ? Convert.ToInt32(manResourceRegular.Value.Trim().ToString().Replace(",", "")) : 0);
                cmd.Parameters.AddWithValue("@manResourceContractual", manResourceContractual.Value.Trim().ToString() != "" ? Convert.ToInt32(manResourceContractual.Value.Trim().ToString().Replace(",", "")) : 0);
                cmd.Parameters.AddWithValue("@manResourceTotal", manResourceTotal.Value.Trim().ToString() != "" ? Convert.ToInt32(manResourceTotal.Value.Trim().ToString().Replace(",", "")) : 0);
                cmd.Parameters.AddWithValue("@benefitsPagibig", benefitsPagibig.SelectedValue);
                cmd.Parameters.AddWithValue("@benefitsPagibigFileName", benefitsPagibigFileName.Value.Trim().ToString());
                cmd.Parameters.AddWithValue("@benefitsPHICFileName", benefitsPHICFileName.Value.Trim().ToString());
                cmd.Parameters.AddWithValue("@benefitsSSSFileName", benefitsSSSFileName.Value.Trim().ToString());
                cmd.Parameters.AddWithValue("@benefitsOthersFileName", benefitsOthersFileName.Value.Trim().ToString());
                cmd.Parameters.AddWithValue("@assetsMachineriesFileName", assetsMachineriesFileName.Value.Trim().ToString());
                cmd.Parameters.AddWithValue("@assetsCompanyProfileFileName", assetsCompanyProfileFileName.Value.Trim().ToString());
                cmd.Parameters.AddWithValue("@assetsOthersFileName", assetsOthersFileName.Value.Trim().ToString());
                cmd.Parameters.AddWithValue("@benefitsPHIC", benefitsPHIC.SelectedValue != "" ? Convert.ToInt32(benefitsPHIC.SelectedValue) : 0);
                cmd.Parameters.AddWithValue("@benefitsSSS", benefitsSSS.SelectedValue != "" ? Convert.ToInt32(benefitsSSS.SelectedValue) : 0);
                cmd.Parameters.AddWithValue("@benefits13th", benefits13th.SelectedValue != "" ? Convert.ToInt32(benefits13th.SelectedValue) : 0);
                cmd.Parameters.AddWithValue("@benefitsOtherMed", benefitsOtherMed.SelectedValue != "" ? Convert.ToInt32(benefitsOtherMed.SelectedValue) : 0);
                cmd.Parameters.AddWithValue("@benefitsOthers", benefitsOthers.Value.Trim().ToString());
                cmd.Parameters.AddWithValue("@facltyLandTxt", facltyLandTxt.Value.Trim().ToString());
                cmd.Parameters.AddWithValue("@facltyLandOwned", facltyLandOwned.SelectedValue);
                cmd.Parameters.AddWithValue("@facltyBldgTxt", facltyBldgTxt.Value.Trim().ToString());
                cmd.Parameters.AddWithValue("@facltyBldgOwned", facltyBldgOwned.SelectedValue);
                cmd.Parameters.AddWithValue("@facltyLocation", facltyLocation.SelectedValue);
                cmd.Parameters.AddWithValue("@facltyPremissesAs", facltyPremissesAs.SelectedValue);
                cmd.Parameters.AddWithValue("@insurInfoEmplyrLia_Limit", insurInfoEmplyrLia_Limit.Value.Trim().ToString());
                cmd.Parameters.AddWithValue("@insurInfoEmplyrLia_InsuCo", insurInfoEmplyrLia_InsuCo.Value.Trim().ToString());
                cmd.Parameters.AddWithValue("@insurInfoPropInsu_Limit", insurInfoPropInsu_Limit.Value.Trim().ToString());
                cmd.Parameters.AddWithValue("@insurInfoPropInsu_InsuCo", insurInfoPropInsu_InsuCo.Value.Trim().ToString());
                cmd.Parameters.AddWithValue("@insurInfoPartyLia_Limit", insurInfoPartyLia_Limit.Value.Trim().ToString());
                cmd.Parameters.AddWithValue("@insurInfoPartyLia_InsuCo", insurInfoPartyLia_InsuCo.Value.Trim().ToString());
                cmd.Parameters.AddWithValue("@insurInfoOthers_Limit", insurInfoOthers_Limit.Value.Trim().ToString());
                cmd.Parameters.AddWithValue("@insurInfoOthers_InsuCo", insurInfoOthers_InsuCo.Value.Trim().ToString());
                conn.Open(); cmd.ExecuteNonQuery();
            }
        }


        //CLEAR tblVendorProductsAndServices FROM USER
        sCommand = "DELETE FROM tblVendorBackOnKeyPersonnel WHERE VendorId = " + Session["VendorId"];
        SqlHelper.ExecuteNonQuery(connstring, CommandType.Text, sCommand);

        query = "INSERT INTO tblVendorBackOnKeyPersonnel (VendorId, Position, Name, DegreeEarned, EducInstitution, YearGraduated, Nationality, Age, PastWorkExp, CurriculumVitae) VALUES (@VendorId, @Position, @Name, @DegreeEarned, @EducInstitution, @YearGraduated, @Nationality, @Age, @PastWorkExp, @CurriculumVitae)";
        //query = "sp_GetVendorInformation"; //##storedProcedure
        using (conn = new SqlConnection(connstring))
        {
            using (cmd = new SqlCommand(query, conn))
            {
                //cmd.CommandType = CommandType.StoredProcedure; //##storedProcedure
                cmd.Parameters.AddWithValue("@VendorId", VendorId);
                cmd.Parameters.AddWithValue("@Position", "ceo");
                cmd.Parameters.AddWithValue("@Name", ceoName.Value.Trim().ToString());
                cmd.Parameters.AddWithValue("@DegreeEarned", ceoDegreeEarned.Value.Trim().ToString());
                cmd.Parameters.AddWithValue("@EducInstitution", ceoEducInstitution.Value.Trim().ToString());
                cmd.Parameters.AddWithValue("@YearGraduated", ceoYearGraduated.Value.Trim().ToString());
                cmd.Parameters.AddWithValue("@Nationality", ceoNationality.Value.Trim().ToString());
                cmd.Parameters.AddWithValue("@Age", ceoAge.Value.Trim().ToString());
                cmd.Parameters.AddWithValue("@PastWorkExp", ceoPastWorkExp.Value.Trim().ToString());
                cmd.Parameters.AddWithValue("@CurriculumVitae", ceoCR.Value.Trim().ToString());
                conn.Open(); cmd.ExecuteNonQuery();
            }
        }
        query = "INSERT INTO tblVendorBackOnKeyPersonnel (VendorId, Position, Name, DegreeEarned, EducInstitution, YearGraduated, Nationality, Age, PastWorkExp, CurriculumVitae) VALUES (@VendorId, @Position, @Name, @DegreeEarned, @EducInstitution, @YearGraduated, @Nationality, @Age, @PastWorkExp, @CurriculumVitae)";
        //query = "sp_GetVendorInformation"; //##storedProcedure
        using (conn = new SqlConnection(connstring))
        {
            using (cmd = new SqlCommand(query, conn))
            {
                //cmd.CommandType = CommandType.StoredProcedure; //##storedProcedure
                cmd.Parameters.AddWithValue("@VendorId", VendorId);
                cmd.Parameters.AddWithValue("@Position", "cfo");
                cmd.Parameters.AddWithValue("@Name", cfoName.Value.Trim().ToString());
                cmd.Parameters.AddWithValue("@DegreeEarned", cfoDegreeEarned.Value.Trim().ToString());
                cmd.Parameters.AddWithValue("@EducInstitution", cfoEducInstitution.Value.Trim().ToString());
                cmd.Parameters.AddWithValue("@YearGraduated", cfoYearGraduated.Value.Trim().ToString());
                cmd.Parameters.AddWithValue("@Nationality", cfoNationality.Value.Trim().ToString());
                cmd.Parameters.AddWithValue("@Age", cfoAge.Value.Trim().ToString());
                cmd.Parameters.AddWithValue("@PastWorkExp", cfoPastWorkExp.Value.Trim().ToString());
                cmd.Parameters.AddWithValue("@CurriculumVitae", cfoCR.Value.Trim().ToString());
                conn.Open(); cmd.ExecuteNonQuery();
            }
        }
        query = "INSERT INTO tblVendorBackOnKeyPersonnel (VendorId, Position, Name, DegreeEarned, EducInstitution, YearGraduated, Nationality, Age, PastWorkExp, CurriculumVitae) VALUES (@VendorId, @Position, @Name, @DegreeEarned, @EducInstitution, @YearGraduated, @Nationality, @Age, @PastWorkExp, @CurriculumVitae)";
        //query = "sp_GetVendorInformation"; //##storedProcedure
        using (conn = new SqlConnection(connstring))
        {
            using (cmd = new SqlCommand(query, conn))
            {
                //cmd.CommandType = CommandType.StoredProcedure; //##storedProcedure
                cmd.Parameters.AddWithValue("@VendorId", VendorId);
                cmd.Parameters.AddWithValue("@Position", "coo");
                cmd.Parameters.AddWithValue("@Name", cooName.Value.Trim().ToString());
                cmd.Parameters.AddWithValue("@DegreeEarned", cooDegreeEarned.Value.Trim().ToString());
                cmd.Parameters.AddWithValue("@EducInstitution", cooEducInstitution.Value.Trim().ToString());
                cmd.Parameters.AddWithValue("@YearGraduated", cooYearGraduated.Value.Trim().ToString());
                cmd.Parameters.AddWithValue("@Nationality", cooNationality.Value.Trim().ToString());
                cmd.Parameters.AddWithValue("@Age", cooAge.Value.Trim().ToString());
                cmd.Parameters.AddWithValue("@PastWorkExp", cooPastWorkExp.Value.Trim().ToString());
                cmd.Parameters.AddWithValue("@CurriculumVitae", cooCR.Value.Trim().ToString());
                conn.Open(); cmd.ExecuteNonQuery();
            }
        }
        query = "INSERT INTO tblVendorBackOnKeyPersonnel (VendorId, Position, Name, DegreeEarned, EducInstitution, YearGraduated, Nationality, Age, PastWorkExp, CurriculumVitae) VALUES (@VendorId, @Position, @Name, @DegreeEarned, @EducInstitution, @YearGraduated, @Nationality, @Age, @PastWorkExp, @CurriculumVitae)";
        //query = "sp_GetVendorInformation"; //##storedProcedure
        using (conn = new SqlConnection(connstring))
        {
            using (cmd = new SqlCommand(query, conn))
            {
                //cmd.CommandType = CommandType.StoredProcedure; //##storedProcedure
                cmd.Parameters.AddWithValue("@VendorId", VendorId);
                cmd.Parameters.AddWithValue("@Position", "ptm");
                cmd.Parameters.AddWithValue("@Name", ptmName.Value.Trim().ToString());
                cmd.Parameters.AddWithValue("@DegreeEarned", ptmDegreeEarned.Value.Trim().ToString());
                cmd.Parameters.AddWithValue("@EducInstitution", ptmEducInstitution.Value.Trim().ToString());
                cmd.Parameters.AddWithValue("@YearGraduated", ptmYearGraduated.Value.Trim().ToString());
                cmd.Parameters.AddWithValue("@Nationality", ptmNationality.Value.Trim().ToString());
                cmd.Parameters.AddWithValue("@Age", ptmAge.Value.Trim().ToString());
                cmd.Parameters.AddWithValue("@PastWorkExp", ptmPastWorkExp.Value.Trim().ToString());
                cmd.Parameters.AddWithValue("@CurriculumVitae", ptmCR.Value.Trim().ToString());
                conn.Open(); cmd.ExecuteNonQuery();
            }
        }



        //CLEAR tblVendorBankInformation FROM USER
        sCommand = "DELETE FROM tblVendorBankInformation WHERE VendorId = " + Session["VendorId"];
        SqlHelper.ExecuteNonQuery(connstring, CommandType.Text, sCommand);

        numRowsTbl = Convert.ToInt32(Request.Form["BankInformationCounter"].ToString());
        for (int i = 1; i <= numRowsTbl; i++)
        {
            query = "INSERT INTO tblVendorBankInformation (VendorId, biBankName, biBranch, biAccountType, biContact, DateCreated, biAttachment) VALUES (@VendorId, @biBankName, @biBranch, @biAccountType, @biContact,  @DateCreated, @biAttachment)";
            //query = "sp_GetVendorInformation"; //##storedProcedure
            using (conn = new SqlConnection(connstring))
            {
                using (cmd = new SqlCommand(query, conn))
                {
                    //cmd.CommandType = CommandType.StoredProcedure; //##storedProcedure
                    cmd.Parameters.AddWithValue("@VendorId", VendorId);
                    cmd.Parameters.AddWithValue("@biBankName", Request.Form["biBankName" + i].ToString());
                    cmd.Parameters.AddWithValue("@biBranch", Request.Form["biBranch" + i].ToString());
                    cmd.Parameters.AddWithValue("@biAccountType", Request.Form["biAccountType" + i].ToString());
                    cmd.Parameters.AddWithValue("@biContact", Request.Form["biContact" + i].ToString());
                    cmd.Parameters.AddWithValue("@DateCreated", DateTime.Now);
                    cmd.Parameters.AddWithValue("@biAttachment", Request.Form["biAttachment" + i].ToString());
                    conn.Open(); cmd.ExecuteNonQuery();
                }
            }
        }



        ////CLEAR tblVendorInsuranceInformation FROM USER
        //sCommand = "DELETE FROM tblVendorInsuranceInformation WHERE VendorId = " + Session["VendorId"];
        //SqlHelper.ExecuteNonQuery(connstring, CommandType.Text, sCommand);

        //numRowsTbl = Convert.ToInt32(Request.Form["InsuranceInformationCounter"].ToString());
        //for (int i = 1; i <= numRowsTbl; i++)
        //{
        //    query = "INSERT INTO tblVendorInsuranceInformation (VendorId, iCompanyName, iAddress, DateCreated) VALUES (@VendorId, @iCompanyName, @iAddress, @DateCreated)";
        //    //query = "sp_GetVendorInformation"; //##storedProcedure
        //    using (conn = new SqlConnection(connstring))
        //    {
        //        using (cmd = new SqlCommand(query, conn))
        //        {
        //            //cmd.CommandType = CommandType.StoredProcedure; //##storedProcedure
        //            cmd.Parameters.AddWithValue("@VendorId", VendorId);
        //            cmd.Parameters.AddWithValue("@iCompanyName", Request.Form["InsuranceInformationCompanyName" + i].ToString());
        //            cmd.Parameters.AddWithValue("@iAddress", Request.Form["InsuranceInformationAddress" + i].ToString());
        //            cmd.Parameters.AddWithValue("@DateCreated", DateTime.Now);
        //            conn.Open(); cmd.ExecuteNonQuery();
        //        }
        //    }
        //}


        string control1 = Request.Form["__EVENTTARGET"];
        if (control1 == "continueStp")
        {
            Response.Redirect("vendor_04_Legal.aspx");
        }
    }


    //#####################################################3

    protected void rptGeneral_Selecting(object sender, SqlDataSourceSelectingEventArgs e)
    {
        e.Command.Parameters["@VendorId"].Value = VendorId;
    }
}