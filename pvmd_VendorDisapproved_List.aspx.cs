﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ava.lib;
using Ava.lib.constant;

public partial class pvmd_VendorDisapproved_List : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserId"] == null) Response.Redirect("login.aspx");
        if (Session["SESSION_USERTYPE"].ToString() != "17") Response.Redirect("login.aspx");
    }
}