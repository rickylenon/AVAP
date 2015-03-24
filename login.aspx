<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="login.aspx.cs" Inherits="login" %>

<%-- Add content controls here --%>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content">
    <div class="content_logo">
    <img src="images/logo_hck.png" width="229" height="105" border="0" />
    </div>
    <div style="background:#FFF; min-height:445px;" class="rounded-corners">
    <div style="width:700px; padding:5px 20px; text-transform:uppercase; background:#29c5ee; margin:0 0 0 70px; color:#FFF" class="rounded-corners-bottom">
      <h1>Welcome to Globe Telecom Vendor Accreditation </h1>
    </div>
        <table style="width: 100%;">
            <tr>
                <td valign="top" style="border-right:#ccc 1px solid; width:50%">
                    <form runat="server" id="form1" name="form1" action="#" method="post"> 
                    <div style="float:left; width:370px; min-height:300px; margin:25px 0 0 100px; padding-top:30px; padding-right:10px;">
                    <asp:Label runat="server" ID="txtNote" ForeColor="#FF0000" Style="font-weight: bold"></asp:Label>
                      <asp:TextBox ID="txtUserName" runat="server" CausesValidation="True" value="ENTER YOUR USERNAME" onclick="if($(this).val()=='ENTER YOUR USERNAME'){ $(this).select(); } " onblur="if($(this).val()==''){ $(this).val('ENTER YOUR USERNAME'); }" ></asp:TextBox>
                      <asp:TextBox runat="server" ID="txtPassword" TextMode="Password" CausesValidation="True" value="" onclick="$(this).select();" onblur="if($(this).val()==''){ $(this).val(''); }" ></asp:TextBox>
                      <div class="clearfix"></div>
                      <div style="margin-top:25px;">
                      <div style="float:none">
                      <input id="Image1" name="" type="image" src="images/002_11.png" runat="server"  onserverclick="btnLogin_Click" />

                      </div>
                          <div style="margin:5px 20px 0 0; font-size:18px;"> <a href="forgotPwd.aspx" style="font-size:18px">Forgot your password?</a></div>
                      </div>
                      <div class="clearfix" style="font-size:18px;"><br /><br /><p>New Vendor? <a href="vendor_signup.aspx" style="text-decoration:none;">Sign up here.</a></p>
                          <div style="width:400px; margin:5px 0 0 0px; padding-top:30px; padding-right:10px; font-size:14px;">
                    </div>
                        </div>
                      </div>
                      </form>
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
	<table width="135" border="0" cellpadding="2" cellspacing="0" title="Click to Verify - This site chose Symantec SSL for secure e-commerce and confidential communications.">
<tr>
<td width="135" align="center" valign="top"><script type="text/javascript" src="https://seal.verisign.com/getseal?host_name=https://gtva.globe.com.ph/&amp;size=L&amp;use_flash=NO&amp;use_transparent=NO&amp;lang=en"></script><br />
<a href="http://www.symantec.com/ssl-certificates" target="_blank"  style="color:#000000; text-decoration:none; font:bold 7px verdana,sans-serif; letter-spacing:.5px; text-align:center; margin:0px; padding:0px;">ABOUT SSL CERTIFICATES</a></td>
</tr>
</table>
        
    <div class="clearfix"></div>
    </div>
    </div>
    <div class="clearfix"></div>
    </div><!-- content ends -->
    <script>$(document).ready(function () { $("#ContentPlaceHolder1_txtUserName").focus().select(); });</script>
    <div class="clearfix"></div>
</asp:Content>