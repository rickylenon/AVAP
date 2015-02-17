<%@ Page Title="" Language="C#" MasterPageFile="~/MasterSubPage.master" AutoEventWireup="true" CodeFile="vendor_requirements.aspx.cs" Inherits="vendor_requirements" %>
<%@ Register TagPrefix="Ava" TagName="Tabsnav" Src="usercontrols/tabs.ascx" %>
<%@ Register TagPrefix="Ava" TagName="TopNav" Src="usercontrols/TopNav_vendor.ascx" %>
<%@ Register TagPrefix="Ava" TagName="StepNav" Src="usercontrols/StepNav_vendor.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitlePlaceHolder1" runat="Server">
Globe Automated Vendor Accreditation :: Vendor Information
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%-- Add content controls here --%>
<!--### TOOLTIP STARTS ###-->
    <script type="text/javascript" src="Scripts/jquery.tooltip.min.js"></script>
    <link href="Styles/jquery.tooltip.css" rel="stylesheet" type="text/css" />
	<script type="text/javascript">
	    $(function () {
	        $('#stepnav li').tooltip({
	            track: true,
	            delay: 0,
	            showURL: false,
	            showBody: " - ",
	            fade: 250
	        });
	    });
    </script>
	<!--### TOOLTIP ENDS ###-->
    
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

    <script src="Scripts/jquery.numeric.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $(".numeric").css('text-align', 'right');
            $(".integer").css('text-align', 'right');
            $(".numeric").numeric();
            $(".integer").numeric(false, function () { alert("Integers only"); this.value = ""; this.focus(); });

            $(".integer").digits();
            $(".integer").blur(function () {
                $(".integer").digits(); $(".integer").css('text-align', 'right');
            });
            $(".numeric").digits();
            $(".numeric").blur(function () {
                $(".numeric").digits(); $(".numeric").css('text-align', 'right');
            });
        });
        function reloadNumeric() {
            $(".numeric").numeric();
            $(".integer").numeric(false, function () { alert("Integers only"); this.value = ""; this.focus(); });
        }
        $.fn.digits = function () {
            return this.each(function () {
                $(this).val($(this).val().replace(/(\d)(?=(\d\d\d)+(?!\d))/g, "$1,"));
            })
        }
    </script>
	<script type="text/javascript" src="Scripts/jquery.table.addrow.js" ></script>
<link href="Styles/ava_pages.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        ol li {
            margin-top:10px;
            font-weight:bold;
            border-top: 1px dotted #ccc;
            padding-top:10px;
        }
        ul li {
            font-weight:normal;
            border-top:none;
            padding-top:3px;
        }
    </style>
<div class="content">
<div class="content_logo">
<a href=""><img src="images/logo_hck.png" width="229" height="105" border="0" /></a> 
<div style="float:right"></div>
</div>
<div class="rounded-corners-top" id="menuAVA">
<ava:tabsnav ID="Tabsnav1" runat="server" />
</div>
<div style="background:#FFF; min-height:445px; padding:10px;" class="rounded-corners-bottom2 menu">
<!--##################-->
<!--BODY CONTENT STARTS-->
<ava:topnav ID="TopNav1" runat="server" />
<div style="margin:10px 0px; color:#333; font-size:18px; width:480px; float:left;">List Of Required Documents Globe Accreditation Process</div>


<form id="formVendorInfo" runat="server">

    <input id="VendorBranchesCounter" class="rowCount" name="VendorBranchesCounter" type="hidden" /><input id="SubsidiaryCounter" class="rowCount" name="SubsidiaryCounter" type="hidden" />

<!--Business activities STARTS-->
<div class="separator1"></div>
<h3 style="margin:10px 0px;">A.	Accomplish the following forms</h3>
    <ol>
        <li>VENDOR INFORMATION SHEET </li>
        <li>UNDERTAKINGS </li>
        <li>SUPPLIER'S DECLARATION ON SAFETY, HEALTH, AND ENVIRONMENTAL POLICIES AND PRACTICES </li>
        <li>SUPPLIER’S DECLARATION ON BUSINESS CONTINUITY MANAGEMENT </li>
    </ol>

<br />



<div class="separator1"></div>
<h3 style="margin:10px 0px;">B. Submit the following documents (most recent dated)</h3>
    
    <ol>
        <li>FINANCIAL STATEMENTS
            <ul>
                <li>Audited FINANCIAL STATEMENTS for the last three (3) consecutive years (with ITR (Annual Income Tax Return), proof  of BIR Payment and SEC stamp) and all its attachments</li>
                <li>For Sole Proprietor: Include a copy of Annual Income Tax Return (BIR Form 1701) – FS should have BIR stamp</li>
                <li>For Corporations: Include all the attachments – Annual Income Tax Return (BIR Form 1702),FS should have BIR and SEC Stamp</li>
                <li>For NEW Companies: Please provide Financial Forecast for the next 3 consecutive years (i.e. Balance Sheet, Income Statement, Owner’s equity and Cash flow)</li>
            </ul>
        </li>
        <li>BUSINESS REGISTRATION 
                <table style="width: 600px;" cellpadding="10">
                    <tr>
                        <td width="300"><b>For Corporations: </b></td>
                        <td width="300"><b>For Sole Proprietor:</b></td>
                    </tr>
                    <tr>
                        <td>SEC Registration (i.e. SEC Certificate, Articles of Incorporation and By Laws)
                            <ul>
                                <li>General Information Sheet</li>
                                <li>Competent evidence of identity
                                    <ul>
                                        <li>Authorized signatories - Any government issued ID with picture</li>
                                        <li>Company – Community Tax Certificate</li>
                                    </ul>
                                </li>
                                <li>Board Resolution / Secretary certificate of authorized signatories</li>
                            </ul>
                        </td>
                        <td>
                            <ul>
                                <li>DTI Registration – attach letter of certification from Proprietor stating registered / authorized capital</li>
                                <li>Owner’s Identification ** valid IDs include: Driver’s License, Passport or NBI Clearance</li>
                                <li>Community Tax Certificate of the owner (CTC)</li>
                            </ul>

                        </td>
                    </tr>
                </table>
            </li>
        <li>Business Permit / Mayor’s Permit </li>
        <li>Regulatory Requirements
            <ul>
                <li>For Contractors (at least an “A” License from the Philippine Contractor’s Accreditation Board (PCAB)</li>
                <li>For Couriers/Forwarders: Department of Transportation and Communication (DOTC) </li>
                <li>For Brokers: Port Accreditation Certificate</li>
                <li>For Manpower Services : Department of Labor and Employment (DOLE)</li>
                <li>For VAS providers (NTC permit)</li>
            </ul>
        </li>
        <li>PROOF OF BUSINESS ADDRESS
            <br>Any of the following:
            <ul>
                <li>Transfer Certificate Title (TCT)</li>
                <li>Lease Agreement</li>
            </ul>
        </li>
        <li>BIR CERTIFICATE (BIR Form 2303 or 1556)</li>
        <li>Last 6 months SSS / Pag-ibig / Philhealth  remittances (receipt only)</li>
        <li>Last 6 months utility billing statement (water, telco, power) </li>
        <li>Copies of Certifications ( ISO 9000/Other)</li>
        <li>Company Profile</li>
        <li>Table of Organization with names and designation</li>
        <li>Certification and Warranty- If vendor opts to present original copies of the submitted documents.</li>
        <li>On-going and Completed Projects for contractors
            <table style="width: 600px;" cellpadding="10">
                    <tr>
                        <td width="300"><b>I. List of Completed Projects</b></td>
                        <td width="300"><b>II. List of On-going Projects  </b></td>
                    </tr>
                    <tr>
                        <td>
                            <ul>
                                <li>Client:</li>
                                <li>Project Nature:</li>
                                <li>Project Location:</li>
                                <li>Project Value:</li>
                                <li>Start Date:</li>
                                <li>Date Completed:</li>
                            </ul>
                        </td>
                        <td>
                            <ul>
                                <li>Client:</li>
                                <li>Project Nature:</li>
                                <li>Project Location:</li>
                                <li>Project Value:</li>
                                <li>Start Date:</li>
                                <li>Target Date Completed:</li>
                                <li>Percentage Accomplished:</li>
                            </ul>
                        </td>
                    </tr>
                </table>

        </li>
    </ol>

    <br />
    <br />
<script type="text/javascript">
    $("document").ready(function () {
        $(".alternativeRow").btnAddRow({ inputBoxAutoNumber: true, inputBoxAutoId: true, displayRowCountTo: "rowCount" });
        $(".delRow").btnDelRow();
    });
</script>



<div class="separator1"></div>
<br />
<br />
<br />
    &nbsp;&nbsp;
<a href="vendor_Home.aspx<%= queryString %>" class="bt1"><span>BACK</span></a>
<br />
<br />
<br />
</form>




<!--BODY CONTENT ENDS-->
<!--##################-->
</div>
</div>
</div><!-- content ends --> 

    </div>

    </div>

</asp:Content>