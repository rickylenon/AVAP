<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="logout.aspx.cs" Inherits="logout" %>

<%-- Add content controls here --%>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content">
    <div class="content_logo">
    <img src="images/logo_hck.png" width="229" height="105" border="0" />
    </div>
    <div style="background:#FFF; min-height:445px;" class="rounded-corners">
    <div style="width:700px; padding:5px 20px; text-transform:uppercase; background:#29c5ee; margin:0 0 0 70px; color:#FFF" class="rounded-corners-bottom">
      <h1>Welcome to Globe TELECOM Vendor Accreditation </h1>
    </div>


        <table style="width: 100%;">
            <tr>
                <td valign="top" style="border-right:#ccc 1px solid; width:50%">
                    <div style="width:370px; border-right:#ccc 1px solid; margin:25px 0 0 100px; padding-top:30px;">
                    <asp:Label runat="server" ID="txtNote" ForeColor="#3366FF" Style="font-weight: bold">You have successfully logged out.</asp:Label>
                      <div class="clearfix"></div>
                      <div style="margin-top:25px;">
                      <div style="float:left">
                      <br /><br /><p>Click here login back.</p>
                      <input name="" type="image" src="images/002_11.png" onclick="window.location = 'login.aspx'" />

                      </div>
                      </div>
                      <div class="clearfix" style="font-size:18px;"><br /><br /><p>New Vendor? <a href="vendor_signup.aspx" style="text-decoration:none;">Sign up here.</a></p></div>
                      </div>
                </td>
                <td valign="top">
                    <div style=" width:400px; margin:25px 0 0 30px; padding-top:30px; font-size:14px;">
                    <div style="font-size:20px;"> Vendor Accreditation</div>
                    <div style="margin-top:15px; padding:10px; border:1px solid #ccc; text-align:justify;">
                        <asp:Label ID="contentLanding" runat="server" Text=""></asp:Label>
                    </div><br /><br />
                    </div>
                </td>
            </tr>
        </table>
    
    
        
    <div class="clearfix"></div>
    </div>
    </div>
    </div><!-- content ends -->
</asp:Content>