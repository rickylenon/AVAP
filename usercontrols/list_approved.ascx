﻿<%@ Control Language="C#" AutoEventWireup="true" CodeFile="list_approved.ascx.cs" Inherits="usercontrols_list_approved" %>
<div class="separator1"></div>
<h3 style="margin:10px 0px;">Vendors Approved</h3>
<form id="Form1" action="" method="post" runat="server">
<asp:SqlDataSource ID="dsVendorAuthenticated" runat="server" ConnectionString="<%$ ConnectionStrings:AVAConnectionString %>"
			SelectCommand="SELECT *, dbo.CalculateNumberOFWorkDays(DateSubmittedToDnb , DateAuthenticatedByDnb) as datediff1,  dbo.CalculateNumberOFWorkDays(DateAuthenticatedByDnb, approvedbyDnbDate) as datediff2,  dbo.CalculateNumberOFWorkDays(approvedbyDnbDate, approvedbyVMOfficerDate) as datediff3,  dbo.CalculateNumberOFWorkDays(approvedbyVMOfficerDate, approvedbyVMRecoDate) as datediff4,  dbo.CalculateNumberOFWorkDays(approvedbyVMRecoDate, approvedbyFAALogisticsDate) as datediff5,  dbo.CalculateNumberOFWorkDays(approvedbyFAALogisticsDate, approvedbyFAAFinanceDate) as datediff6,  dbo.CalculateNumberOFWorkDays(DateSubmittedToDnb, approvedbyFAALogisticsDate) as datetotal1,  dbo.CalculateNumberOFWorkDays(DateSubmittedToDnb, approvedbyFAAFinanceDate) as datetotal2 FROM tblVendor WHERE IsAuthenticated = 1 AND Status = 6 ORDER BY approvedbyFAAFinanceDate, approvedbyFAALogisticsDate  DESC" >
		</asp:SqlDataSource>
<asp:GridView ID="GridView1" runat="server" DataSourceID="dsVendorAuthenticated" 
	AllowPaging="True" AllowSorting="True" BorderColor="Silver" OnRowCommand="gvVendors_RowCommand"
	BorderStyle="Dotted" BorderWidth="1px" CellPadding="5" ClientIDMode="AutoID"  EmptyDataText="No approved vendors to display."
	Width="100%" AutoGenerateColumns="False" PageSize="15" ShowFooter="True">
	<AlternatingRowStyle BackColor="#EEEEEE" />
	<Columns>
		<asp:TemplateField HeaderText="Company Name" InsertVisible="False" SortExpression="CompanyName" ItemStyle-HorizontalAlign="Center">
			<HeaderStyle HorizontalAlign="Center" Width="90px" />
			<ItemTemplate>
				&nbsp;<%--<asp:Label ID="lnkRefNo" runat="server" Text='<%# Bind("CompanyName") %>' ></asp:Label>--%><%--&nbsp;<asp:Label ID="Label0" runat="server" Text='<%# Bind("CompanyName") %>'></asp:Label>--%><a href="<%# "vendor_Home.aspx?VendorId=" + Eval("VendorId") %>" target="_blank"><%# Eval("CompanyName") %></a> 
			</ItemTemplate>
		</asp:TemplateField>
		<asp:TemplateField HeaderText="Submitted to D&B" InsertVisible="False" SortExpression="DateSubmittedToDnb" ItemStyle-HorizontalAlign="Center" ItemStyle-Font-Size="10px">
			<HeaderStyle HorizontalAlign="Center" Width="90px" />
			<ItemTemplate>
				&nbsp;<asp:Label ID="Label1" runat="server" Text='<%# String.Format("{0:M/d/yyyy<br><i>&nbsp;&nbsp;HH:mm tt</i>}", Eval("DateSubmittedToDnb")) %>'></asp:Label>
			</ItemTemplate>
		</asp:TemplateField>
		<asp:TemplateField HeaderText="Authenticated" InsertVisible="False" SortExpression="DateAuthenticatedByDnb" ItemStyle-HorizontalAlign="Center" ItemStyle-Font-Size="10px">
			<HeaderStyle HorizontalAlign="Center" Width="90px" />
			<ItemTemplate>
				&nbsp;<asp:Label ID="Label2" runat="server" Text='<%# Eval("DateAuthenticatedByDnb").ToString()!="" ? "["+Eval("datediff1").ToString() + "]" + String.Format("{0:M/d/yyyy<br><i>&nbsp;&nbsp;HH:mm tt</i>}", Eval("DateAuthenticatedByDnb")) : "" %>'></asp:Label>
			</ItemTemplate>
		</asp:TemplateField>
		<asp:TemplateField HeaderText="Submitted to VM Officer" InsertVisible="False" SortExpression="approvedbyDnbDate" ItemStyle-HorizontalAlign="Center" ItemStyle-Font-Size="10px">
			<HeaderStyle HorizontalAlign="Center" Width="90px" />
			<ItemTemplate>
				&nbsp;<asp:Label ID="Label12" runat="server" Text='<%# Eval("approvedbyDnbDate").ToString()!="" ? "["+Eval("datediff2").ToString() + "]" +  String.Format("{0:M/d/yyyy<br><i>&nbsp;&nbsp;HH:mm tt</i>}", Eval("approvedbyDnbDate")) : "" %>'></asp:Label>
			</ItemTemplate>
		</asp:TemplateField>
		<asp:TemplateField HeaderText="Endorsed to VM Head" InsertVisible="False" SortExpression="approvedbyVMOfficerDate" ItemStyle-HorizontalAlign="Center" ItemStyle-Font-Size="10px">
			<HeaderStyle HorizontalAlign="Center" Width="90px" />
			<ItemTemplate>
				&nbsp;<asp:Label ID="Label13" runat="server" Text='<%# Eval("approvedbyVMOfficerDate").ToString()!="" ? "["+Eval("datediff3").ToString() + "]" + String.Format("{0:M/d/yyyy<br><i>&nbsp;&nbsp;HH:mm tt</i>}", Eval("approvedbyVMOfficerDate")) : "" %>'></asp:Label>
			</ItemTemplate>
		</asp:TemplateField>
		<asp:TemplateField HeaderText="Endorsed to PVMD" InsertVisible="False" SortExpression="approvedbyVMRecoDate" ItemStyle-HorizontalAlign="Center" ItemStyle-Font-Size="10px">
			<HeaderStyle HorizontalAlign="Center" Width="90px" />
			<ItemTemplate>
				&nbsp;<asp:Label ID="Label131" runat="server" Text='<%# Eval("approvedbyVMRecoDate").ToString()!="" ? "["+Eval("datediff4").ToString() + "]" + String.Format("{0:M/d/yyyy<br><i>&nbsp;&nbsp;HH:mm tt</i>}", Eval("approvedbyVMRecoDate")) : "" %>'></asp:Label>
			</ItemTemplate>
		</asp:TemplateField>
		<asp:TemplateField HeaderText="Endorsed to CFO" InsertVisible="False" SortExpression="approvedbyFAALogisticsDate" ItemStyle-HorizontalAlign="Center" ItemStyle-Font-Size="10px">
			<HeaderStyle HorizontalAlign="Center" Width="90px" />
			<ItemTemplate>
				&nbsp;<asp:Label ID="Label14" runat="server" Text='<%# Eval("approvedbyFAALogisticsDate").ToString()!=""? "["+Eval("datediff5").ToString() + "]" + String.Format("{0:M/d/yyyy<br><i>&nbsp;&nbsp;HH:mm tt</i>}", Eval("approvedbyFAALogisticsDate")) : "" %>'></asp:Label>
			</ItemTemplate>
		</asp:TemplateField>
		<asp:TemplateField HeaderText="Status" InsertVisible="False" SortExpression="approvedbyFAALogisticsDate" ItemStyle-HorizontalAlign="Center" ItemStyle-Font-Size="10px">
			<HeaderStyle HorizontalAlign="Center" Width="90px" />
			<ItemTemplate>
				&nbsp;<asp:Label ID="Label15" runat="server" Text='<%# Eval("Status").ToString()!="8" ? Eval("Status").ToString()=="6"?"Approved: <br>"+ "["+Eval("datediff6").ToString() + "]" + String.Format("{0:M/d/yyyy<br><i>&nbsp;&nbsp;HH:mm tt</i>}", Eval("approvedbyFAAFinanceDate")) : "" : Eval("approvedbyFAAFinanceDate").ToString()!="" ? "Disapproved: <br>"+  "["+Eval("datediff1").ToString() + "]" + String.Format("{0:M/d/yyyy <i>HH:mm tt</i>}", Eval("approvedbyFAAFinanceDate")) : "Disapproved: <br>"+  "["+Eval("datediff5").ToString() + "]" + String.Format("{0:M/d/yyyy <i>HH:mm tt</i>}", Eval("approvedbyFAALogisticsDate")) %>'></asp:Label>
			</ItemTemplate>
		</asp:TemplateField>
		<asp:TemplateField HeaderText="Total" InsertVisible="False" SortExpression="approvedbyFAALogisticsDate" ItemStyle-HorizontalAlign="Center" ItemStyle-Font-Size="10px">
			<HeaderStyle HorizontalAlign="Center" Width="20px" />
			<ItemTemplate>
				&nbsp;<asp:Label ID="Label16" runat="server" Text='<%# (Eval("Status").ToString()=="8" || Eval("Status").ToString()=="6") && Eval("approvedbyFAAFinanceDate").ToString()!="" ? Eval("datetotal2").ToString() :  Eval("datetotal1").ToString() %>'></asp:Label>
			</ItemTemplate>
		</asp:TemplateField>
		<%--<asp:TemplateField HeaderText="Endorsed to CFO" InsertVisible="False" SortExpression="DateCreated" ItemStyle-HorizontalAlign="Center">
			<HeaderStyle HorizontalAlign="Center" Width="90px" />
			<ItemTemplate>
				&nbsp;<asp:Label ID="Label2" runat="server" Text='<%# Bind("approvedbyFAALogisticsDate") %>'></asp:Label>
			</ItemTemplate>
		</asp:TemplateField>--%>
		<asp:TemplateField HeaderText="Action" InsertVisible="False" ItemStyle-HorizontalAlign="Center" ItemStyle-Font-Size="10px">
			<HeaderStyle HorizontalAlign="Center" Width="10px" />
			<ItemTemplate>
				&nbsp;<asp:LinkButton ID="lnkRefNo1" runat="server" Text='View' CommandArgument='<%# Bind("VendorId") %>' CommandName="Details"></asp:LinkButton><%-- | <%# Eval("Status").ToString()!="8" ? Eval("Status").ToString()!="6" ? "":"Approved" : "Disapproved" %>--%>
			</ItemTemplate>
		</asp:TemplateField>
	</Columns>
</asp:GridView>
	
</form>