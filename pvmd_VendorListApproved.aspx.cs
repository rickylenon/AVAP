﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ava.lib;
using Ava.lib.constant;

public partial class pvmd_VendorListApproved : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserId"] == null || Session["UserId"].ToString() == "") Response.Redirect("login.aspx");
        if (Session["SESSION_USERTYPE"].ToString() != "17") Response.Redirect("login.aspx");
        Session["VendorId"] = "";
    }
}