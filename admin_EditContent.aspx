<%@ Page Title="" Language="C#" MasterPageFile="~/MasterSubPage.master" AutoEventWireup="true" CodeFile="admin_EditContent.aspx.cs" Inherits="admin_EditContent" ValidateRequest="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitlePlaceHolder1" runat="Server">
Globe Automated Vendor Accreditation :: Vendor
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content">
    <!--### FORM STYLING ###-->
	<link rel="stylesheet" href="plugins/jqtransformplugin/jqtransform.css" type="text/css" media="all" />
	<link rel="stylesheet" href="plugins/jqtransformplugin/demo.css" type="text/css" media="all" />
	<script type="text/javascript" src="plugins/jqtransformplugin/jquery.jqtransform.js" ></script>
	<script language="javascript">
	    $(function () {
	        $('form').jqTransform({ imgPath: 'plugins/jqtransformplugin/img/' });
	    });
	</script>
    <!--### FORM STYLING ENDS ###-->
    <script type="text/javascript" src="Scripts/jquery.table.addrow.js" ></script>
<div class="content_logo">
<img src="images/logo_hck.png" width="229" height="105" border="0" />
</div>
<div class="rounded-corners-top" id="menuAVA">
    <ul>
    <li id="current" class="rounded-corners-top-left" onclick="window.location='admin_Home.aspx'"><span class="rounded-corners-top-left">Home</span></li>
    <li onclick="window.location='admin_UsersList.aspx'"><span class="rounded-corners-top-left">List of Users</span></li>
    <li onclick="window.location='admin_VendorUsersList.aspx'"><span>Vendor Users</span></li>
    <%--<li><span>D&amp;B</span></li>
    <li><span>Legal</span></li>
    <li><span>VM Technical Review</span></li>
    <li><span>VM Issue Management</span></li>
    <li><span style="padding:0px 15px;">VM Recommending<br />Approval</span></li>
    <li><span style="padding:0px 15px;">Final Approving <br />Authority
Logistics</span></li>
    <li style="border-right:none;"><span style="padding:0px 15px;">Final Approving <br />AuthorityFinance</span></li>--%>
    </ul>
</div>
<div style="background:#FFF;   padding:10px;" class="rounded-corners-bottom2 menu">
<!--##################-->
<!--BODY CONTENT STARTS-->
<div class="topnav"><a href="admin_Home.aspx?UserId=0">Create a User</a>&nbsp;&nbsp;&nbsp;|&nbsp;&nbsp;&nbsp;<a href="admin_UsersList.aspx">List of Users</a>&nbsp;&nbsp;&nbsp;|&nbsp;&nbsp;&nbsp;<a href="admin_VendorUsersList.aspx">Vendor Users</a>&nbsp;&nbsp;&nbsp;|&nbsp;&nbsp;&nbsp;<a href="admin_EditContent.aspx">Home Content</a>&nbsp;&nbsp;&nbsp;|&nbsp;&nbsp;&nbsp;<a href="admin_EditEmails.aspx">Email Content</a></div>
<div style="width:475px; margin:25px 0 0 30px; padding-top:0px; font-size:14px;" >
<form action="" method="post" name="formVendorInfo" id="formVendorInfo" runat="server">
  <h3><asp:Label ID="Label1" runat="server" Text="Edit Homepage Content"></asp:Label></h3><br />
  <asp:label id="errNotification" runat="server"></asp:label> 
    
  <div class="separator1"></div>
  <div style="clear:both; height:4px;"></div>
  <label for="content" style="clear:both; width:110px;">Content</label><br /><br />
    <asp:TextBox ID="content" runat="server" Rows="20" TextMode="MultiLine" Width="475px"></asp:TextBox>
  <div style="clear:both; height:4px;"></div>
      

  
  <div style="clear:both; height:2px;"></div>
  <asp:LinkButton ID="createBt1" runat="server" CssClass="bt1"  onclientclick="javascript: __doPostBack('saveContent', ''); return false;" style="float:left"><asp:Label ID="btnSaveContent" runat="server" Text="SAVE"></asp:Label></asp:LinkButton>
  <div class="clearfix"></div>
</form><br />
<br />

</div>

<!--BODY CONTENT ENDS-->
<!--##################-->
</div>
</div>
<!-- content ends --> 

</asp:Content>