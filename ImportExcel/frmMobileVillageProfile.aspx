<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.master"  Culture="en-GB" CodeFile="frmMobileVillageProfile.aspx.cs"
    Inherits="frmMobileVillageProfile" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script type="text/javascript">


        function isNumberKey(txt, evt) {
            debugger;
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode == 46 && charCode == 127) {
                if (txt.value.indexOf('.') === 1) {
                    return true;
                }
                else {
                    return false;
                }
            }
            else {
                if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                    return false;
                }
                else {
                    return true;
                }
            }
        }
    </script>
    <script type="text/javascript">
        function SetMultilanguage(Flag, clsname) {
            var Lngg = "", lid = "";
            var maxSelection = 0;
            $('.' + clsname + ' input[type="checkbox"]:checked').each(function () {
                Lngg = Lngg + $(this).next().html() + ",";
                lid = lid + $(this).val() + ",";
                maxSelection++;
            });
           
            Lngg = Lngg.substr(0, Lngg.length - 1);
            lid = lid.substr(0, lid.length - 1);
            if (Flag == 'F') {
                if (maxSelection <= 10) {
                    $('#<%=hdn_PBID.ClientID %>').val(lid);
                    $('#<%=hdn_PBName.ClientID %>').val(Lngg);
                    $('#<%=txt_pbname.ClientID %>').val(Lngg);
                }

                else {
                    $('.' + clsname + ' input[type="checkbox"]:checked').each(function () {
                        $(this).attr("checked", false);
                    });
                    $('#<%=hdn_PBID.ClientID %>').val('');
                    $('#<%=hdn_PBName.ClientID %>').val('');
                    $('#<%=txt_pbname.ClientID %>').val('');


                    $find("Modal_alertB").show();
                    return false;
                }

                if (Lngg.toLowerCase().indexOf("others (specify)") >= 0) {

                    $('#<%=txt_bookformatOther.ClientID %>').val('');
                    $('#<%=txt_bookformatOther.ClientID %>').attr('disabled', false);
                }
                else {

                    $('#<%=txt_bookformatOther.ClientID %>').val('');
                    $('#<%=txt_bookformatOther.ClientID %>').attr('disabled', true);
                }
            }
            else if (Flag == 'M') {
                if (maxSelection <= 10) {
                    $('#<%=hhmuhulaid.ClientID %>').val(lid);
                    $('#<%=HidName.ClientID %>').val(Lngg);
                    $('#<%=txtMuhala.ClientID %>').val(Lngg);
                }

                else {
                    $('.' + clsname + ' input[type="checkbox"]:checked').each(function () {
                        $(this).attr("checked", false);
                    });
                    $('#<%=hdn_PBID.ClientID %>').val('');
                    $('#<%=HidName.ClientID %>').val('');
                    $('#<%=txtMuhala.ClientID %>').val('');


                    $find("Modal_alertB").show();
                    return false;
                }

                if (Lngg.toLowerCase().indexOf("others (specify)") >= 0) {

                    $('#<%=txtmOther.ClientID %>').val('');
                    $('#<%=txtmOther.ClientID %>').attr('disabled', false);
                }
                else {

                    $('#<%=txtmOther.ClientID %>').val('');
                    $('#<%=txtmOther.ClientID %>').attr('disabled', true);
                }
            }
            else if (Flag == 'OC') {
                if (maxSelection <= 10) {
                    $('#<%=hhOtherComid.ClientID %>').val(lid);
                    $('#<%=hhOtherComName.ClientID %>').val(Lngg);
                    $('#<%=txtOtherComminuty.ClientID %>').val(Lngg);
                }

                else {
                    $('.' + clsname + ' input[type="checkbox"]:checked').each(function () {
                        $(this).attr("checked", false);
                    });
                    $('#<%=hhOtherComid.ClientID %>').val('');
                    $('#<%=hhOtherComName.ClientID %>').val('');
                    $('#<%=txtOtherComminuty.ClientID %>').val('');


                    $find("Modal_alertB").show();
                    return false;
                }

                if (Lngg.toLowerCase().indexOf("others (specify)") >= 0) {

                    $('#<%=txtOtherComm.ClientID %>').val('');
                    $('#<%=txtOtherComm.ClientID %>').attr('disabled', false);
                }
                else {

                    $('#<%=txtOtherComm.ClientID %>').val('');
                    $('#<%=txtOtherComm.ClientID %>').attr('disabled', true);
                }
            }
            else if (Flag == 'CC') {
                if (maxSelection <= 10) {
                    $('#<%=hhcommconectId.ClientID %>').val(lid);
                    $('#<%=hhcommconectName.ClientID %>').val(Lngg);
                    $('#<%=txtOtherConnect.ClientID %>').val(Lngg);
                }

                else {
                    $('.' + clsname + ' input[type="checkbox"]:checked').each(function () {
                        $(this).attr("checked", false);
                    });
                    $('#<%=hhcommconectId.ClientID %>').val('');
                    $('#<%=hhcommconectName.ClientID %>').val('');
                    $('#<%=txtOtherConnect.ClientID %>').val('');


                    $find("Modal_alertB").show();
                    return false;
                }

                if (Lngg.toLowerCase().indexOf("others (specify)") >= 0) {

                    $('#<%=txtOtherCon.ClientID %>').val('');
                    $('#<%=txtOtherCon.ClientID %>').attr('disabled', false);
                }
                else {

                    $('#<%=txtOtherCon.ClientID %>').val('');
                    $('#<%=txtOtherCon.ClientID %>').attr('disabled', true);
                }
            }
            else if (Flag == 'CCC') {
                if (maxSelection <= 10) {
                    $('#<%=hh_connID.ClientID %>').val(lid);
                    $('#<%=hh_connName.ClientID %>').val(Lngg);
                    $('#<%=txt_conn.ClientID %>').val(Lngg);
                }

                else {
                    $('.' + clsname + ' input[type="checkbox"]:checked').each(function () {
                        $(this).attr("checked", false);
                    });
                    $('#<%=hh_connID.ClientID %>').val('');
                    $('#<%=hh_connName.ClientID %>').val('');
                    $('#<%=txt_conn.ClientID %>').val('');


                    $find("Modal_alertB").show();
                    return false;
                }

                if (Lngg.toLowerCase().indexOf("others") >= 0) {


                    $('#<%=txt_con_other.ClientID %>').val('');
                    $('#<%=txt_con_other.ClientID %>').attr('disabled', false);
                }
                else {

                    $('#<%=txt_con_other.ClientID %>').val('');
                    $('#<%=txt_con_other.ClientID %>').attr('disabled', true);
                }
            }

            else if (Flag == 'CC2') {
           
                if (maxSelection <= 10) {
                    $('#<%=hc1.ClientID %>').val(lid);
                    $('#<%=hc2.ClientID %>').val(Lngg);
                    $('#<%=txtOtherCC1.ClientID %>').val(Lngg);
                }
                     
                else {
                    $('.' + clsname + ' input[type="checkbox"]:checked').each(function () {
                        $(this).attr("checked", false);
                    });
                    $('#<%=hc1.ClientID %>').val('');
                    $('#<%=hc2.ClientID %>').val('');
                    $('#<%=txtOtherCC1.ClientID %>').val('');


                    $find("Modal_alertB").show();
                    return false;
                }

                if (Lngg.toLowerCase().indexOf("others (specify)") >= 0) {


                    $('#<%=txtoC111.ClientID %>').val('');
                    $('#<%=txtoC111.ClientID %>').attr('disabled', false);
                }
                else {

                    $('#<%=txtoC111.ClientID %>').val('');
                    $('#<%=txtoC111.ClientID %>').attr('disabled', true);
                }
            }
            else if (Flag == 'SS') {
                if (maxSelection <= 10) {
                    $('#<%=hhSuportId.ClientID %>').val(lid);
                    $('#<%=hhSuportName.ClientID %>').val(Lngg);
                    $('#<%=txtSuport.ClientID %>').val(Lngg);
                }

                else {
                    $('.' + clsname + ' input[type="checkbox"]:checked').each(function () {
                        $(this).attr("checked", false);
                    });
                    $('#<%=hhSuportId.ClientID %>').val('');
                    $('#<%=hhSuportName.ClientID %>').val('');
                    $('#<%=txtSuport.ClientID %>').val('');


                    $find("Modal_alertB").show();
                    return false;
                }

                if (Lngg.toLowerCase().indexOf("other") >= 0) {

                    $('#<%=txtOtherSupport.ClientID %>').val('');
                    $('#<%=txtOtherSupport.ClientID %>').attr('disabled', false);
                }
                else {

                    $('#<%=txtOtherSupport.ClientID %>').val('');
                    $('#<%=txtOtherSupport.ClientID %>').attr('disabled', true);
                }
            }


        }

                       
                 
    </script>
    <style type="text/css">
        .checkbox label:after, .radio label:after
        {
            content: '';
            display: table;
            clear: both;
        }
        .checkbox .cr, .radio .cr
        {
            position: relative;
            display: inline-block;
            border: 2px solid #333;
            border-radius: .25em;
            width: 1.3em;
            height: 1.3em;
            float: left;
            margin-right: .5em;
            color: red;
        }
        
        .radio .cr
        {
            border-radius: 75%;
            border-color: #333;
        }
        
        .checkbox .cr .cr-icon, .radio .cr .cr-icon
        {
            position: absolute;
            font-size: .8em;
            line-height: 0;
            top: 50%;
            left: 15%;
        }
        
        .radio .cr .cr-icon
        {
            margin-left: 0.04em;
        }
        
        .checkbox label input[type="checkbox"], .radio label input[type="radio"]
        {
            display: none;
        }
        
        .checkbox label input[type="checkbox"] + .cr > .cr-icon, .radio label input[type="radio"] + .cr > .cr-icon
        {
            transform: scale(3) rotateZ(-220deg);
            opacity: 0;
            transition: all .7s ease-in;
        }
        
        .checkbox label input[type="checkbox"]:checked + .cr > .cr-icon, .radio label input[type="radio"]:checked + .cr > .cr-icon
        {
            transform: scale(1) rotateZ(0deg);
            opacity: 1;
        }
        
        .checkbox label input[type="checkbox"]:disabled + .cr, .radio label input[type="radio"]:disabled + .cr
        {
            opacity: .5;
        }
        
        .new-navbutt
        {
            float: left !important;
            margin-top: 0px !important;
        }
        
        .row-border
        {
            border-bottom: 1px dotted rgb(221, 221, 221);
            margin-bottom: 15px;
        }
        
        .checkbox
        {
            position: relative;
            display: block;
            margin-top: 2px !important;
            margin-bottom: 5px !important;
        }
        
        
        .new-navbutt
        {
            float: left !important;
            margin-top: 0px !important;
        }
        
        .row-border
        {
            border-bottom: 1px dotted rgb(221, 221, 221);
            margin-bottom: 15px;
        }
        .checkbox .cr .cr-icon, .radio .cr .cr-icon
        {
            position: absolute;
            font-size: .8em;
            line-height: 0;
            top: 50%;
            left: 15%;
        }
        
        .radio .cr .cr-icon
        {
            margin-left: 0.04em;
        }
        
        .checkbox label input[type="checkbox"], .radio label input[type="radio"]
        {
            display: none;
        }
        
        .checkbox label input[type="checkbox"] + .cr > .cr-icon, .radio label input[type="radio"] + .cr > .cr-icon
        {
            transform: scale(3) rotateZ(-220deg);
            opacity: 0;
            transition: all .7s ease-in;
        }
        
        .checkbox label input[type="checkbox"]:checked + .cr > .cr-icon, .radio label input[type="radio"]:checked + .cr > .cr-icon
        {
            transform: scale(1) rotateZ(0deg);
            opacity: 1;
        }
        
        .checkbox label input[type="checkbox"]:disabled + .cr, .radio label input[type="radio"]:disabled + .cr
        {
            opacity: .5;
        }
        
        .new-navbutt
        {
            float: left !important;
            margin-top: 0px !important;
        }
        
        .row-border
        {
            border-bottom: 1px dotted rgb(221, 221, 221);
            margin-bottom: 15px;
        }
        
        .checkbox
        {
            position: relative;
            display: block;
            margin-top: 2px !important;
            margin-bottom: 5px !important;
        }
        .CheckBoxListCssClass
        {
            font-family: calibri;
            margin-left: 5p .checkbox { position: relative;
            display: block;
            margin-top: 2px !important;
            margin-bottom: 5px !important;
        }
        .CheckBoxListCssClass
        {
            font-family: calibri;
            margin-left: 5px;
            font-weight: bold;
            font-size: small;
            top: 53%;
            left: 3%;
        }
        .checkboxlist
        {
            position: absolute;
            font-size: .8em;
            margin-left: 10px;
            line-height: 0;
            top: 50%;
            left: 15%;
        }
        .td-widt
        {
            width: auto !important;
        }
        
        .td-width1
        {
            width: 150px !important;
        }
        
        @media (min-width:10px) and (max-width:640px)
        {
            .td-widt
            {
                width: 90px !important;
            }
        
        
            .td-width1
            {
                width: 90px !important;
            }
        }
        
        .table-mb
        {
            margin-bottom: 2px !important;
        }
        
        .thnail
        {
            padding: 0px !important;
            border-radius: 0px !important;
            margin-bottom: 0px !important;
            min-height: 60px;
        }
    </style>
    <style type="text/css">
        .ajax__calendar_container
        {
            z-index: 1000;
        }
        .modalpopupcss
        {
            background-color: #666699;
            filter: alpha(opacity=50);
            opacity: 0.7;
        }
        .modalPopup
        {
            background-color: #666699;
            filter: alpha(opacity=50);
            opacity: 0.7;
            z-index: 999999;
        }
    </style>
<style type="text/css">
    .multiselect {
    width:20em;
    height:15em;
    border:solid 1px #c0c0c0;
    overflow:auto;
}
 
.multiselect label {
    display:block;
}
 
.multiselect-on {
    color:#ffffff;
    background-color:#000099;
     </style>
}
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
        <asp:UpdatePanel runat="server" ID="UpMain">
        <ContentTemplate>
    <div class="row" style="margin-top: 120px;">
        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <h3 style="margin: 0px;">
                         Village Activity instead of village</h3>
                </div>
                <div class="panel-body">
                    <div class="row">
                        <div class="row marg search-bg" style="margin-left: -11px;">
                            <div class="form-horizontal">
                         
                                <div class="col-lg-3 col-md-3 col-sm-3 cpl-xs-12">
                                    <div class="form-group" style="margin-bottom: 7px;">
                                        <label for="email" class="col-sm-3 padd linhei">
                                            FC:</label>
                                        <div class="col-sm-9 padd">
                                            <asp:DropDownList ID="ddlUser" OnSelectedIndexChanged="ddlUser_SelectedIndexChanged"
                                                runat="server" AutoPostBack="true" class="form-control ">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-3 col-md-3 col-sm-3 cpl-xs-12">
                                    <div class="form-group" style="margin-bottom: 7px;">
                                        <label for="email" class="col-sm-3 padd linhei">
                                            Village:</label>
                                        <div class="col-sm-9 padd">
                                            <asp:DropDownList ID="ddlVilage" OnSelectedIndexChanged="ddlVilage_SelectedIndexChanged"
                                                runat="server" AutoPostBack="true" class="form-control " />
                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-3 col-md-3 col-sm-3 cpl-xs-12">
                                    <div class="form-group" style="margin-bottom: 7px;">
                                        <label for="email" class="col-sm-3 padd linhei">
                                            Date:</label>
                                        <div class="col-sm-9 padd">
                                            <asp:TextBox runat="server" ID="txtDate"  autocomplete="off" ondrop="return false;"
                                                class="form-control" onkeypress="return false;"></asp:TextBox>
                                            <ajax:CalendarExtender ID="CalendarExtenderTourdate" runat="server" Enabled="True"
                                                Format="dd/MM/yyyy" TargetControlID="txtDate"  PopupPosition="BottomRight">
                                            </ajax:CalendarExtender>
                                            <asp:RequiredFieldValidator ID="ReqTxtDate" runat="server" ControlToValidate="txtDate"
                                                Display="Dynamic" ErrorMessage="*" Font-Bold="False" Font-Size="9px" ForeColor="Red"
                                                SetFocusOnError="True" ValidationGroup="saves"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-3 col-md-3 col-sm-3 cpl-xs-12">

                                   <asp:ImageButton ID="btnDelete" OnClick="btnDelete_Click" CssClass="btn btn-info pull-right" ToolTip="Delete" BackColor="#f5f5f5" ImageUrl="~/images/delete-29.png"  style="margin-right: 5px; padding:0px;" runat="server" />
                   
                               <asp:Button ID="btnApprove"   CssClass="btn btn-success pull-right " 
                                 ToolTip="Save" Text="  Back"   OnClick="btnApprove_Click" 
                                Style="margin-right: 5px; padding: 0px;" runat="server" />   
                                    <asp:ImageButton ID="btnsave" CssClass="btn btn-info pull-right" OnClick="btnSave_Click"
                                        BackColor="#f5f5f5" ToolTip="Save" ImageUrl="~/images/save-29-1.png" ValidationGroup="saves"
                                        Style="margin-right: 5px; padding: 0px;" runat="server" />
                                   
                                          <asp:ImageButton ID="ImageButton1" CssClass="btn btn-info pull-right" BackColor="#f5f5f5"
                                        ToolTip="Add" Visible="false" ImageUrl="~/images/add-29-1.png" OnClick="btnAdd_Click" Style="margin-right: 5px;
                                        padding: 0px;" runat="server" />
                                         <asp:ImageButton ID="btnSerach" ToolTip="Serach" runat="server" 
                                        OnClick="btnSerach_Click" class="btn btn-danger btn-paddd pull-right" BackColor="#f1f1f1"
                                        ImageUrl="~/images/search-29.png" />

                                                                    
                 <asp:ImageButton ID="btnEdit"  ToolTip="Edit" OnClick="btnEdit_Click" runat="server" class="btn btn-danger btn-paddd pull-right"
                                                    BackColor="#f1f1f1" ImageUrl="~/images/edit.png" />                                 
                                                     </div>
                                </div>

                            </div>
                        </div>
                         <div class="row">
                        <div class="row marg search-bg" style="margin-left: -11px;">
                            <div class="form-horizontal">
                         
                                <div class="col-lg-3 col-md-3 col-sm-3 cpl-xs-12">
                                    <div class="form-group" style="margin-bottom: 7px;">
                                        <label for="email" class="col-sm-3 padd linhei">
                                            GSS:</label>
                                        <div class="col-sm-4 padd">
                                                <asp:ImageButton ID="imgGSS" runat="server" Width="30" Height="25"  OnClick="btnImgGss_Click" Visible="false"  CssClass="pull-right" ImageUrl="~/images/iconimage-128.png"></asp:ImageButton >
                                            
                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-3 col-md-3 col-sm-3 cpl-xs-12">
                                    <div class="form-group" style="margin-bottom: 7px;">
                                        <label for="email" class="col-sm-3 padd linhei">
                                            MM:</label>
                                        <div class="col-sm-4 padd">
                                             <asp:ImageButton ID="ImgMM" runat="server" Width="30" Height="25"  OnClick="btnImgMM_Click" Visible="false"  CssClass="pull-right" ImageUrl="~/images/iconimage-128.png"></asp:ImageButton >
                    
                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-3 col-md-3 col-sm-3 cpl-xs-12">
                                    <div class="form-group" style="margin-bottom: 7px;">
                                        <label for="email" class="col-sm-3 padd linhei">
                                            Community  :</label>
                                        <div class="col-sm-4 padd">
                                               <asp:ImageButton ID="imgComm1" runat="server" Width="30" Height="25"  OnClick="btnimgComm1_Click" Visible="false"  CssClass="pull-right" ImageUrl="~/images/iconimage-128.png"></asp:ImageButton >
                                       
                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-3 col-md-3 col-sm-3 cpl-xs-12">
                                 <div class="form-group" style="margin-bottom: 7px;">
                                        <label for="email" class="col-sm-3 padd linhei">
                                             Other :</label>
                                        <div class="col-sm-4 padd">
                                           <asp:ImageButton ID="imgComm2" runat="server" Width="30" Height="25"  OnClick="btnimgComm2_Click" Visible="false"  CssClass="pull-right" ImageUrl="~/images/iconimage-128.png"></asp:ImageButton >
                                          
                                        </div>
                                    </div>
                                                                                
                                  
                                                     </div>
                                </div>

                            </div>
                        </div>
                        <div class="col-lg-3 col-md-3 col-sm-5 col-xs-12" style="padding: 0px;">
                            <table class="table table-bordered table-mb" style="height: 64px; margin-bottom: 0px;margin-left: -12px;">
                                <tr>
                                    <td style="width: 17%;">
                                    </td>
                                    <td style="width: 50%;">
                                        STEPS
                                    </td>
                                    <td>
                                        TB
                                    </td>
                                    <td>
                                        FC
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                    <td class="td-width1" style="vertical-align: middle; background-color: black; color: white;">
                                        T.B.Hand Holding
                                    </td>
                                    <td style="text-align: left">
                                        <div>
                                            <label style="line-height: 20px; font-size: 12px; font-family: calibri;">
                                                <asp:CheckBox ID="rblTbhold"  Enabled="false" CssClass="cr-icon" runat="server" />
                                            </label>
                                        </div>
                                    </td>
                                    <td style="text-align: left">
                                        <div class="checkbox">
                                            <label style="line-height: 20px; font-size: 12px; font-family: calibri;">
                                                <asp:CheckBox ID="rblFcHold"  GroupName="A" CssClass="cr-icon" runat="server" />
                                            </label>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                    <asp:Panel ID="pnlMain" runat="server" Enabled="false">
                        <div class="navbar-header">
                            <button type="button" class="navbar-toggle new-navbutt" data-toggle="collapse" data-target="#myNavbar">
                                GSS
                            </button>
                        </div>
                        <div class="collapse navbar-collapse" id="myNavbar" style="padding: 0px;">
                            <div class="row">
                                <div class="col-lg-3 col-md-3 col-sm-5 col-xs-12" style="padding: 0px;">
                                    <table class="table table-bordered table-mb" style="height: 54px; margin-bottom: 0px;">
                                        <tr>
                                            <td style="width: 17%;">
                                                <div>
                                                    <label style="line-height: 20px; font-family: calibri;">
                                                        <asp:CheckBox ID="chkmcommmeting" Enabled="false" runat="server" />
                                                    </label>
                                                </div>
                                            </td>
                                            <td class="td-width1" style="vertical-align: top; font-size: 13px; width: 50%; font-family: calibri;
                                                background-color: Black; color: white;">
                                               GSS
                                            </td>
                                            <td>
                                                <div class="checkbox">
                                                    <label style="line-height: 20px; font-size: 12px; font-family: calibri;">
                                                        <asp:RadioButton ID="chkcommmetingTB" GroupName="B" CssClass="cr-icon" runat="server" />
                                                        <%-- <span ><asp:RadioButton ID="chkcommmetingTB" CssClass="cr-icon"  runat="server"  /></span>--%>
                                                    </label>
                                                </div>
                                            </td>
                                            <td>
                                                <div class="checkbox">
                                                    <label style="line-height: 20px; font-size: 12px; font-family: calibri;">
                                                        <span>
                                                            <asp:RadioButton ID="chkcommmetingFC" GroupName="B" CssClass="cr-icon" runat="server" /></span>
                                                    </label>
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                                <div class="col-lg-9 col-md-9 col-sm-7 col-xs-12" style="padding: 0px;">
                                    <div class="row">
                                        <div  style="height: 54px; margin-bottom: 0px;">
                                            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="padding: 0px;">
                                                <div class="col-sm-3 " style="border: 1px solid #DDDDDD; height: 54px">
                                                    <asp:Label ID="lblAgenda" runat="server" Text="Agenda"></asp:Label>
                                                    <asp:TextBox ID="txt_pbname" autocomplete="off" ondrop="return false;" runat="server"
                                                        CssClass="form-control" onkeypress="return false;" TabIndex="5"></asp:TextBox>
                                                    
                                                    <cc1:PopupControlExtender ID="PopupControltxt_pbname" runat="server" TargetControlID="txt_pbname"
                                                        PopupControlID="pnt_bookformat" OffsetY="22">
                                                    </cc1:PopupControlExtender>
                                                    <asp:Panel ID="pnt_bookformat" runat="server" Direction="LeftToRight" Style="display: none;
                                                        min-height: 60px; max-height: 300px; overflow: auto; z-index: 999999; background-color: #F1F1F1;
                                                        border: solid 1px #cccccc; width: 89.5%" CssClass="panel">
                                                        <span>
                                                            <asp:CheckBoxList ID="CBL_bookformat" CssClass="_bookformat radio" runat="server"
                                                                onclick="SetMultilanguage('F','_bookformat');">
                                                            </asp:CheckBoxList>
                                                        </span>
                                                        <asp:HiddenField runat="server" ID="hdn_PBName" />
                                                        <asp:HiddenField runat="server" ID="hdn_PBID" />
                                                    </asp:Panel>
                                                </div>
                                                <div class="col-sm-3 " style="border-top: 1px solid #DDDDDD; border-bottom: 1px solid #DDDDDD;
                                                    height: 54px">
                                                    <%-- <span class="col-sm-3" style="padding-top:2px;">--%>
                                                    <asp:Label ID="lblOther" runat="server" autocomplete="off" ondrop="return false;"
                                                        Text="Other(Specify)"></asp:Label>
                                                    <asp:TextBox runat="server" ID="txt_bookformatOther"  CssClass="form-control"
                                                        MaxLength="60" TabIndex="18"></asp:TextBox>
                                                    <%-- </span>--%>
                                                </div>
                                                <div class="col-sm-3 " style="border: 1px solid #DDDDDD; height: 54px">
                                                    <span>
                                                        <asp:Label ID="Label1" runat="server" Text="People Attended"></asp:Label>
                                                    </span><span>
                                                        <asp:TextBox ID="txtV1illager" autocomplete="off" ondrop="return false;" MaxLength="3"
                                                            onkeypress="return isNumberKey(this,event);" CssClass="form-control" runat="server"></asp:TextBox>
                                                    </span>
                                                </div>
                                                <div class="col-sm-3" style="border-top: 1px solid #DDDDDD; border-bottom: 1px solid #DDDDDD;
                                                    border-right: 1px solid #DDDDDD; height: 54px">
                                                            <asp:ImageButton ID="ImageButton2" runat="server" OnClick="btnCLT_Click" Width="30" Height="25" CssClass="pull-right" ImageUrl="~/images/Reset.png"></asp:ImageButton >
                                                              
                                       
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="navbar-header">
                            <button type="button" class="navbar-toggle new-navbutt" data-toggle="collapse" data-target="#myNavbar12">
                                Mauhalla Meeting
                            </button>
                        </div>
                        <div class="collapse navbar-collapse" id="myNavbar12" style="padding: 0px;">
                            <div class="row">
                                <div class="col-lg-3 col-md-3 col-sm-5 col-xs-12" style="padding: 0px;">
                                    <table class="table table-bordered table-mb" style="height: 54px; margin-bottom: 0px;">
                                        <tr>
                                            <td style="width: 17%;">
                                                <div>
                                                    <label style="line-height: 20px; font-size: 12px; font-family: calibri;">
                                                        <asp:CheckBox ID="chkmuhala" Enabled="false" runat="server" />
                                                    </label>
                                                </div>
                                            </td>
                                            <td style="vertical-align: top; font-size: 13px; font-family: calibri; width: 50%;
                                                background-color: Black; color: white;">
                                                Mauhalla Meeting
                                            </td>
                                            <td>
                                                <div class="checkbox">
                                                    <label style="line-height: 20px; font-size: 12px; font-family: calibri;">
                                                        <asp:RadioButton ID="rblmuhulaTb" GroupName="C" CssClass="cr-icon" runat="server" />
                                                    </label>
                                                </div>
                                            </td>
                                            <td>
                                                <div class="checkbox">
                                                    <label style="line-height: 20px; font-size: 12px; font-family: calibri;">
                                                        <asp:RadioButton ID="rblmuhulaFC" GroupName="C" CssClass="cr-icon" runat="server" />
                                                    </label>
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                                <div class="col-lg-9 col-md-9 col-sm-7 col-xs-12" style="padding: 0px;">
                                    <div class="row">
                                        <div  style="height: 54px; margin-bottom: 0px;">
                                            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="padding: 0px;">
                                                <div class="col-sm-3 " style="border: 1px solid #DDDDDD; height: 54px">
                                                    <asp:Label ID="Label2" runat="server" Text="Agenda"></asp:Label>
                                                    <asp:TextBox ID="txtMuhala" runat="server" autocomplete="off" ondrop="return false;"
                                                        CssClass="form-control" onkeypress="return false;" TabIndex="5"></asp:TextBox>
                                                    <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtMuhala"
                                        Display="Dynamic" ErrorMessage="*"
                                        >*</asp:RequiredFieldValidator>--%>
                                                    <cc1:PopupControlExtender ID="PopupControlExtender1" runat="server" TargetControlID="txtMuhala"
                                                        PopupControlID="pnt_Muhula" OffsetY="22">
                                                    </cc1:PopupControlExtender>
                                                    <asp:Panel ID="pnt_Muhula" runat="server" Direction="LeftToRight" Style="display: none;
                                                        min-height: 60px; max-height: 300px; overflow: auto; z-index: 999999; background-color: #F1F1F1;
                                                        border: solid 1px #cccccc; width: 89.5%" CssClass="panel">
                                                        <span>
                                                            <asp:CheckBoxList ID="CBL_Muhula" CssClass="_bookformat1 radio" runat="server" onclick="SetMultilanguage('M','_bookformat1');">
                                                            </asp:CheckBoxList>
                                                        </span>
                                                        <asp:HiddenField runat="server" ID="hhmuhulaid" />
                                                        <asp:HiddenField runat="server" ID="HidName" />
                                                    </asp:Panel>
                                                </div>
                                                <div class="col-sm-3 " style="border-top: 1px solid #DDDDDD; border-bottom: 1px solid #DDDDDD;
                                                    height: 54px">
                                                    <asp:Label ID="Label3" runat="server" Text="Other(Specify)"></asp:Label>
                                                    <asp:TextBox runat="server" ID="txtmOther" autocomplete="off" ondrop="return false;"
                                                         CssClass="form-control" MaxLength="60" TabIndex="18"></asp:TextBox>
                                                </div>
                                                <div class="col-sm-3 " style="border: 1px solid #DDDDDD; height: 54px">
                                                    <span>
                                                        <asp:Label ID="Label4" runat="server" Text="People Attended"></asp:Label>
                                                    </span><span>
                                                        <asp:TextBox ID="txtVillager2" autocomplete="off" ondrop="return false;" MaxLength="3"
                                                            onkeypress="return isNumberKey(this,event);" CssClass="form-control" runat="server"></asp:TextBox>
                                                    </span>
                                                </div>
                                               
                                                <div class="col-sm-3" style="border-top: 1px solid #DDDDDD; border-bottom: 1px solid #DDDDDD;
                                                    border-right: 1px solid #DDDDDD; height: 54px">
                                                      <asp:ImageButton ID="ImageButton3" runat="server" OnClick="btnmm_Click" Width="30" Height="25" CssClass="pull-right" ImageUrl="~/images/Reset.png"></asp:ImageButton >
                                                                        
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="navbar-header">
                            <button type="button" class="navbar-toggle new-navbutt" data-toggle="collapse" data-target="#myNavbar123">
                                 Community Meeting 1
                            </button>
                        </div>
                        <div class="collapse navbar-collapse" id="myNavbar123" style="padding: 0px;">
                            <div class="row">
                                <div class="col-lg-3 col-md-3 col-sm-5 col-xs-12" style="padding: 0px;">
                                    <table class="table table-bordered table-mb" style="height: 54px; margin-bottom: 0px;">
                                        <tr>
                                            <td style="width: 17%;">
                                                <div>
                                                    <label style="line-height: 20px; font-size: 12px; font-family: calibri;">
                                                        <asp:CheckBox ID="chkothercomm" Enabled="false"   runat="server" />
                                                    </label>
                                                </div>
                                            </td>
                                            <td style="vertical-align: top; font-size: 13px; font-family: calibri; width: 50%;
                                                background-color: Black; color: white;">
                                                 Community Meeting 1
                                            </td>
                                            <td>
                                                <div>
                                                    <label style="line-height: 20px; font-size: 12px; font-family: calibri;">
                                                        <asp:RadioButton ID="rblothercommTb" GroupName="D" CssClass="cr-icon" runat="server" />
                                                    </label>
                                                </div>
                                            </td>
                                            <td>
                                                <div class="checkbox">
                                                    <label style="line-height: 20px; font-size: 12px; font-family: calibri;">
                                                        <asp:RadioButton ID="rblothercommfc" GroupName="D" CssClass="cr-icon" runat="server" />
                                                    </label>
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                                <div class="col-lg-9 col-md-9 col-sm-7 col-xs-12" style="padding: 0px;">
                                    <div class="row">
                                        <div  style="height: 54px; margin-bottom: 0px;">
                                            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="padding: 0px;">
                                                <div class="col-sm-3 " style="border: 1px solid #DDDDDD; height: 54px">
                                                    <asp:Label ID="Label5" runat="server" Text="Agenda"></asp:Label>
                                                    <asp:TextBox ID="txtOtherComminuty" autocomplete="off" ondrop="return false;" runat="server"
                                                        CssClass="form-control" onkeypress="return false;" TabIndex="5"></asp:TextBox>
                                                    <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtOtherComminuty"
                                        Display="Dynamic" ErrorMessage="*"
                                        >*</asp:RequiredFieldValidator>--%>
                                                    <cc1:PopupControlExtender ID="PopupControlExtender2" runat="server" TargetControlID="txtOtherComminuty"
                                                        PopupControlID="pnt_OtherComm" OffsetY="22">
                                                    </cc1:PopupControlExtender>
                                                    <asp:Panel ID="pnt_OtherComm" runat="server" Direction="LeftToRight" Style="display: none;
                                                        min-height: 60px; max-height: 300px; overflow: auto; z-index: 999999; background-color: #F1F1F1;
                                                        border: solid 1px #cccccc; width: 89.5%" CssClass="panel">
                                                        <span>
                                                            <asp:CheckBoxList ID="chk_othercom" CssClass="_bookformat2 radio" runat="server"
                                                                onclick="SetMultilanguage('OC','_bookformat2');">
                                                            </asp:CheckBoxList>
                                                        </span>
                                                        <asp:HiddenField runat="server" ID="hhOtherComid" />
                                                        <asp:HiddenField runat="server" ID="hhOtherComName" />
                                                    </asp:Panel>
                                                </div>
                                                <div class="col-sm-3 " style="border-top: 1px solid #DDDDDD; border-bottom: 1px solid #DDDDDD;
                                                    height: 54px">
                                                    <asp:Label ID="Label6" runat="server" Text="Other(Specify)"></asp:Label>
                                                    <asp:TextBox runat="server" ID="txtOtherComm" autocomplete="off" ondrop="return false;"
                                                        CssClass="form-control" MaxLength="60" TabIndex="18"></asp:TextBox>
                                                </div>
                                                <div class="col-sm-3 " style="border: 1px solid #DDDDDD; height: 54px">
                                                    <span>
                                                        <asp:Label ID="Label7" runat="server" Text="People Attended"></asp:Label>
                                                    </span><span>
                                                        <asp:TextBox ID="txtvillager3" autocomplete="off" ondrop="return false;" MaxLength="3"
                                                            onkeypress="return isNumberKey(this,event);" CssClass="form-control" runat="server"></asp:TextBox>
                                                    </span>
                                                </div>
                                                <div class="col-sm-3" style="border-top: 1px solid #DDDDDD; border-bottom: 1px solid #DDDDDD;
                                                    border-right: 1px solid #DDDDDD; height: 54px">
                                                     <span>
                                                        <asp:Label ID="Label10" runat="server" Text="Other Contact"></asp:Label>
                                                    </span><span>
                                                        <asp:TextBox ID="tc1" autocomplete="off" ondrop="return false;" MaxLength="50"
                                                            CssClass="form-control" runat="server"></asp:TextBox>
                                                             <asp:ImageButton ID="ImageButton4" runat="server" style="height:25px;width:30px;border-width:0px;margin-top: -54px;" OnClick="btnOther_Click"  CssClass="pull-right" ImageUrl="~/images/Reset.png"></asp:ImageButton >
                                                       
                                                    </span>
                                                                  
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>



                              <div class="navbar-header">
                            <button type="button" class="navbar-toggle new-navbutt" data-toggle="collapse" data-target="#myNavbar123">
                                Community Meeting 2
                            </button>
                        </div>
                        <div class="collapse navbar-collapse" id="Div1" style="padding: 0px;">
                            <div class="row">
                                <div class="col-lg-3 col-md-3 col-sm-5 col-xs-12" style="padding: 0px;">
                                    <table class="table table-bordered table-mb" style="height: 54px; margin-bottom: 0px;">
                                        <tr>
                                            <td style="width: 17%;">
                                                <div>
                                                    <label style="line-height: 20px; font-size: 12px; font-family: calibri;">
                                                        <asp:CheckBox ID="CheckBox1" Enabled="false" runat="server" />
                                                    </label>
                                                </div>
                                            </td>
                                            <td style="vertical-align: top; font-size: 13px; font-family: calibri; width: 50%;
                                                background-color: Black; color: white;">
                                                 Community Meeting 2
                                            </td>
                                            <td>
                                                <div>
                                                    <label style="line-height: 20px; font-size: 12px; font-family: calibri;">
                                                        <asp:RadioButton ID="rblc1" GroupName="c1" CssClass="cr-icon" runat="server" />
                                                    </label>
                                                </div>
                                            </td>
                                            <td>
                                                <div class="checkbox">
                                                    <label style="line-height: 20px; font-size: 12px; font-family: calibri;">
                                                        <asp:RadioButton ID="rblc2" GroupName="c1" CssClass="cr-icon" runat="server" />
                                                    </label>
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                                <div class="col-lg-9 col-md-9 col-sm-7 col-xs-12" style="padding: 0px;">
                                    <div class="row">
                                        <div  style="height: 54px; margin-bottom: 0px;">
                                            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="padding: 0px;">
                                                <div class="col-sm-3 " style="border: 1px solid #DDDDDD; height: 54px">
                                                    <asp:Label ID="Label13" runat="server" Text="Agenda"></asp:Label>
                                                    <asp:TextBox ID="txtOtherCC1" autocomplete="off" ondrop="return false;" runat="server"
                                                        CssClass="form-control" onkeypress="return false;" TabIndex="5"></asp:TextBox>
                                                    <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtOtherComminuty"
                                        Display="Dynamic" ErrorMessage="*"  txtOtherCC1  hc1  hc2 txtoC111
                                        >*</asp:RequiredFieldValidator>--%>
                                                    <cc1:PopupControlExtender ID="PopupControlExtender6" runat="server" TargetControlID="txtOtherCC1"
                                                        PopupControlID="pnt_CommC1" OffsetY="22">
                                                    </cc1:PopupControlExtender>
                                                    <asp:Panel ID="pnt_CommC1" runat="server" Direction="LeftToRight" Style="display: none;
                                                        min-height: 60px; max-height: 300px; overflow: auto; z-index: 999999; background-color: #F1F1F1;
                                                        border: solid 1px #cccccc; width: 89.5%" CssClass="panel">
                                                        <span>
                                                            <asp:CheckBoxList ID="chk_c2" CssClass="_bookformat11 radio" runat="server"
                                                                onclick="SetMultilanguage('CC2','_bookformat11');">
                                                            </asp:CheckBoxList>
                                                        </span>
                                                        <asp:HiddenField runat="server" ID="hc1" />
                                                        <asp:HiddenField runat="server" ID="hc2" />
                                                    </asp:Panel>
                                                </div>
                                                <div class="col-sm-3 " style="border-top: 1px solid #DDDDDD; border-bottom: 1px solid #DDDDDD;
                                                    height: 54px">
                                                    <asp:Label ID="Label16" runat="server" Text="Other(Specify)"></asp:Label>
                                                    <asp:TextBox runat="server" ID="txtoC111" autocomplete="off" ondrop="return false;"
                                                        CssClass="form-control" MaxLength="60" TabIndex="18"></asp:TextBox>
                                                </div>
                                                <div class="col-sm-3 " style="border: 1px solid #DDDDDD; height: 54px">
                                                    <span>
                                                        <asp:Label ID="Label17" runat="server" Text="People Attended"></asp:Label>
                                                    </span><span>
                                                        <asp:TextBox ID="txtAtt1" autocomplete="off" ondrop="return false;" MaxLength="3"
                                                            onkeypress="return isNumberKey(this,event);" CssClass="form-control" runat="server"></asp:TextBox>
                                                    </span>
                                                </div>
                                                <div class="col-sm-3" style="border-top: 1px solid #DDDDDD; border-bottom: 1px solid #DDDDDD;
                                                    border-right: 1px solid #DDDDDD; height: 54px">
                                                     <span>
                                                        <asp:Label ID="Label18" runat="server" Text="Other Contact"></asp:Label>
                                                    </span><span>
                                                        <asp:TextBox ID="txtoC1" autocomplete="off" ondrop="return false;" MaxLength="50"
                                                            CssClass="form-control" runat="server"></asp:TextBox>
                                                             <asp:ImageButton ID="ImageButton5" runat="server" style="height:25px;width:30px;border-width:0px;margin-top: -54px;" OnClick="btnOther1_Click"  CssClass="pull-right" ImageUrl="~/images/Reset.png"></asp:ImageButton >
                                                                       </span>
                                                                  
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="navbar-header">
                            <button type="button" class="navbar-toggle new-navbutt" data-toggle="collapse" data-target="#myNavbar41">
                                Community Contact 
                            </button>
                        </div>
                        <div class="collapse navbar-collapse" id="myNavbar41" style="padding: 0px;">
                            <div class="row">
                                <div class="col-lg-3 col-md-3 col-sm-5 col-xs-12" style="padding: 0px;">
                                    <table class="table table-bordered table-mb" style="height: 54px; margin-bottom: 0px;">
                                        <tr>
                                            <td style="width: 17%;">
                                                <div>
                                                    <label style="line-height: 20px; font-size: 12px; font-family: calibri;">
                                                        <asp:CheckBox ID="chkcoom" Enabled="false" runat="server" />
                                                    </label>
                                                </div>
                                            </td>
                                            <td class="td-width1" style="vertical-align: top; width: 50%; font-size: 13px; font-family: calibri;
                                                background-color: Black; color: white;">
                                                Community Contact 
                                            </td>
                                            <td>
                                                <div>
                                                    <label style="line-height: 20px; font-size: 12px; font-family: calibri;">
                                                        <asp:CheckBox ID="rblcommtb" Enabled="false" CssClass="cr-icon" runat="server" />
                                                    </label>
                                                </div>
                                            </td>
                                            <td>
                                                <div>
                                                    <label style="line-height: 20px; font-size: 12px; font-family: calibri;">
                                                        <asp:CheckBox ID="rblCommFC" CssClass="cr-icon" runat="server" />
                                                    </label>
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                                <div class="col-lg-9 col-md-9 col-sm-7 col-xs-12" style="padding: 0px;">
                                    <div class="row">
                                        <div >
                                           <%-- <h3 class="text-danger" style="padding-left: 18px; margin: 0px 0px 10px; font-size: 15px;">
                                                Ambition</h3>--%>
                                            <div class="row">
                                                <div  style="height: 54px; margin-bottom: 0px;">
                                                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="padding: 0px;">
                                                        <div class="col-sm-3 " style="border: 1px solid #DDDDDD; height: 54px">
                                                            <asp:Label ID="Label8" runat="server" Text="Reason"></asp:Label>
                                                            <asp:TextBox ID="txtOtherConnect" autocomplete="off" ondrop="return false;" runat="server"
                                                                CssClass="form-control" onkeypress="return false;" TabIndex="5"></asp:TextBox>
                                                      
                                                            <cc1:PopupControlExtender ID="PopupControlExtender3" runat="server" TargetControlID="txtOtherConnect"
                                                                PopupControlID="pnt_CommConnect" OffsetY="22">
                                                            </cc1:PopupControlExtender>
                                                            <asp:Panel ID="pnt_CommConnect" runat="server" Direction="LeftToRight" Style="display: none;
                                                                min-height: 60px; max-height: 300px; overflow: auto; z-index: 999999; background-color: #F1F1F1;
                                                                border: solid 1px #cccccc; width: 89.5%" CssClass="panel">
                                                                <span>
                                                                    <asp:CheckBoxList ID="chk_comm" CssClass="_bookformat3 radio" runat="server" onclick="SetMultilanguage('CC','_bookformat3');">
                                                                    </asp:CheckBoxList>
                                                                </span>
                                                                <asp:HiddenField runat="server" ID="hhcommconectId" />
                                                                <asp:HiddenField runat="server" ID="hhcommconectName" />
                                                            </asp:Panel>
                                                        </div>
                                                        <div class="col-sm-3 " style="border-top: 1px solid #DDDDDD; border-bottom: 1px solid #DDDDDD;
                                                            height: 54px">
                                                            <asp:Label ID="Label9" runat="server" Text="Other(Specify)"></asp:Label>
                                                            <asp:TextBox runat="server" autocomplete="off" ondrop="return false;" ID="txtOtherCon"
                                                                CssClass="form-control" MaxLength="60" TabIndex="18"></asp:TextBox>
                                                        </div>
                                                        <div class="col-sm-3" style="border: 1px solid #DDDDDD; height: 54px">
                                                        <asp:Label ID="Label11" runat="server" Text="Community Contact"></asp:Label>
                                                        <asp:TextBox ID="txt_conn" autocomplete="off" ondrop="return false;" runat="server"
                                                            CssClass="form-control" onkeypress="return false;" TabIndex="5"></asp:TextBox>
                                                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txt_conn"
                                        Display="Dynamic" ErrorMessage="*"
                                        >*</asp:RequiredFieldValidator>--%>
                                                        <cc1:PopupControlExtender ID="PopupControlExtender4" runat="server" TargetControlID="txt_conn"
                                                            PopupControlID="pnt_Com" OffsetY="22">
                                                        </cc1:PopupControlExtender>
                                                        <asp:Panel ID="pnt_Com" runat="server" Direction="LeftToRight" Style="display: none;
                                                            min-height: 60px; max-height: 300px; overflow: auto; z-index: 999999; background-color: #F1F1F1;
                                                            border: solid 1px #cccccc; width: 89.5%" CssClass="panel">
                                                            <span>
                                                                <asp:CheckBoxList ID="chk_chkconn" CssClass="_bookformat4 radio" runat="server" onclick="SetMultilanguage('CCC','_bookformat4');">
                                                                </asp:CheckBoxList>
                                                            </span>
                                                            <asp:HiddenField runat="server" ID="hh_connID" />
                                                            <asp:HiddenField runat="server" ID="hh_connName" />
                                                        </asp:Panel>
                                                        </div>
                                                        <div class="col-sm-3" style="border-top: 1px solid #DDDDDD; border-bottom: 1px solid #DDDDDD;
                                                            border-right: 1px solid #DDDDDD; height: 54px">
                                                             <asp:Label ID="Label12" runat="server" Text="Other(Specify)"></asp:Label>
                                                        <asp:TextBox runat="server" autocomplete="off" ondrop="return false;" ID="txt_con_other"
                                                            CssClass="form-control"  MaxLength="60" TabIndex="18"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                       
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="navbar-header">
                            <button type="button" class="navbar-toggle new-navbutt" data-toggle="collapse" data-target="#myNavbar1">
                                Enrolled/Ineligible
                            </button>

                                <cc1:ModalPopupExtender ID="ModalPopupExtender" runat="server" TargetControlID="Hdn_model3"
            PopupControlID="pnlpopup3" BackgroundCssClass="modalBackground">
        </cc1:ModalPopupExtender>
        <asp:HiddenField ID="Hdn_model3" runat="server" />
       <asp:Panel ID="pnlpopup3" runat="server" Style="display: none;">
            <div class="modal-dialog modal-md">
                <div class="modal-content">
                    <div class="modal-header">
                    <asp:ImageButton ID="btnAdd" CssClass="btn btn-info pull-right" BackColor="#f5f5f5"
                            ToolTip="Add" ImageUrl="~/images/close-29.png" Style="margin-right: 5px;
                            padding: 0px;" runat="server" />

                                <asp:ImageButton ID="ImageButton10" CssClass="btn btn-info pull-right" BackColor="#f5f5f5"
                            ToolTip="Save" ImageUrl="~/images/save-29-1.png" OnClick="btnClose_Click" Style="margin-right: 5px;
                            padding: 0px;" runat="server" />
                        <h4 class="modal-title">
                            D2D</h4>
                        
                    </div>
                  <div class="row">

                        <div class="row marg search-bg">
               
                                <div class="col-lg-10 col-md-10 col-sm-10 cpl-xs-12">
                                    <div class="col-lg-6 col-md-6 col-sm-6 cpl-xs-12">
                                    <div class="form-group" style="margin-bottom: 2px;">
                                        <label for="email" class="col-sm-3 padd linhei">
                                            Search:</label>
                                        <div class="col-sm-9 padd">
                                           <asp:DropDownList ID="ddlSearch" runat="server" class="form-control">
                                            <asp:ListItem Selected="True" Value="0">--Select--</asp:ListItem>
                                            <asp:ListItem Value="1">Unique no</asp:ListItem>
                                            <asp:ListItem Value="2">HH Code </asp:ListItem>
                                            <asp:ListItem Value="3">Child Name</asp:ListItem>

                                             <asp:ListItem Value="4">Father Name</asp:ListItem>
                                        </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-lg-4 col-md-4 col-sm-4 cpl-xs-12">
                            <div class="form-group" style="margin-bottom: 7px;">
                             
                                <div class="col-sm-10 padd">
                                    <asp:TextBox runat="server" ID="txtSearch" autocomplete="off" ondrop="return false;"
                                        class="form-control" ></asp:TextBox>
                                    
                                </div>
                            </div>
                        </div>
                                </div>

                                 <div class="col-lg-2 col-md-2  col-sm-2 cpl-xs-12">
                        <div class="col-lg-1 col-md-1 col-sm-2 cpl-xs-12 col-lg-offset-1 col-md-offset-1 col-sm-offset-1 col-xs-offset-0">
                            <asp:ImageButton ID="ImageButton7" OnClick="btnD2dSerach_Click" ToolTip="Serach" runat="server"  class="btn btn-danger btn-paddd pull-left" BackColor="#f1f1f1"
                                ImageUrl="~/images/search-29.png"  Style="margin-left: -49px; padding: 0px;"   />
                                </div>
                
                           
                          
                        </div>
                        </div>
                  </div>
                    <div class="row table-responsive">
                     <div style="overflow: auto; margin-top:35px; height:480px;">
                        <asp:GridView ID="Gv_Display" Width="100%" runat="server" OnRowDataBound="Gv_Display_RowDataBound"
                            CssClass=" table table-striped table-bordered table-hover " AutoGenerateColumns="false">
                            <EmptyDataTemplate>
                                <div style="font-family: Arial; font-size: 12px; font-weight: bold; color: Red;">
                                    Data not found
                                </div>
                            </EmptyDataTemplate>
                            <FooterStyle CssClass="FooterStyle" />
                            <HeaderStyle BackColor="#C1C1C1" Height="44px" />
                            <RowStyle HorizontalAlign="Center" />
                            <SelectedRowStyle BackColor="#897A7A" Font-Bold="True" ForeColor="WhiteSmoke" />
                            <AlternatingRowStyle BackColor="#f1f1f1" />
                            <Columns>
                                <asp:TemplateField HeaderText="Unique Code" HeaderStyle-CssClass="GridHeaderClass">
                                    <ItemTemplate>
                                        <asp:Label ID="lblUniqueCode" runat="server" Text='<%#Eval("UniqueId") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="HH No." HeaderStyle-CssClass="GridHeaderClass">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSno" runat="server" Text='<%#Eval("HHNo") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Child Name" HeaderStyle-CssClass="GridHeaderClass">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lbtn_ProducerD" Text='<%#Eval("ChildName") %>' Style="text-decoration: none;"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:BoundField HeaderText="Father name" DataField="FathersName" HeaderStyle-CssClass="GridHeaderClass">
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="Contact" HeaderStyle-CssClass="GridHeaderClass">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="ddlStatus" OnSelectedIndexChanged="ddlStatus_SelectedIndexChanged" AutoPostBack="true" runat="server" class="form-control">
                                        <asp:ListItem Selected="True" Value="0">--Select--</asp:ListItem>
                                            <asp:ListItem Value="1">C-Contact </asp:ListItem>
                                              <asp:ListItem Value="2">F-Follow up</asp:ListItem>
                                            <asp:ListItem Value="3">I-Ineligible </asp:ListItem>
                                            <asp:ListItem Value="4">P-Pending Format 6</asp:ListItem>

                                             <asp:ListItem Value="5">E-Enrolled</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:Label runat="server" Visible="false" ID="lbStatus" Text='<%#Eval("Status") %>'
                                            Style="text-decoration: none;"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField Visible="false" HeaderStyle-CssClass="GridHeaderClass">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lbUniqueCode" Text='<%#Eval("UniqueCode") %>' Style="text-decoration: none;"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                        </div>
                    </div>
                    <div class="modal-footer">
                        
                    </div>
                </div>
            </div>
        </asp:Panel>
                        </div>
                        <div class="collapse navbar-collapse" id="Div3" style="padding: 0px;">
                            <div class="row">
                                <div class="col-lg-3 col-md-3 col-sm-5 col-xs-12" style="padding: 0px;">
                                    <table class="table table-bordered table-mb" style="height: 54px; margin-bottom: 0px;">
                                        <tr>
                                            <td style="width: 17%;">
                                                <div>
                                                    <label style="line-height: 20px; font-size: 12px; font-family: calibri; color: white;">
                                                        <asp:CheckBox ID="chkEnroll" Enabled="false" runat="server" />
                                                    </label>
                                                </div>
                                            </td>
                                            <td style="vertical-align: top; font-size: 13px; width: 50%; font-family: calibri;
                                                background-color: black; color: white;">
                                                Contact/Enrolled/Ineligible
                                            </td>
                                            <td>
                                                <div>
                                                    <label style="line-height: 20px; font-size: 12px; font-family: calibri;">
                                                        <asp:RadioButton ID="rblemrolltb" GroupName="F" CssClass="cr-icon" runat="server" />
                                                    </label>
                                                </div>
                                            </td>
                                            <td>
                                                <div>
                                                    <label style="line-height: 20px; font-size: 12px; font-family: calibri;">
                                                        <asp:RadioButton ID="rblenrollFC" GroupName="F" CssClass="cr-icon" runat="server" />
                                                    </label>
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                                <div class="col-lg-9 col-md-9 col-sm-7 col-xs-12" style="padding: 0px;">
                                    <div class="row">
                                        <div style="height: 60px; margin-bottom: 0px;">
                                            <div class="row">
                                                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="padding: 0px;">
                                                    <div class="col-lg-3 col-md-3 col-sm-6 col-xs-12">
                                                        <div class="checkbox">
                                                            <label style="line-height: 20px; font-size: 12px; font-family: calibri;">
                                                                <asp:LinkButton ID="lnkEnrool" OnClick="lnkEnrool_OnClick" runat="server">Click Here</asp:LinkButton>
                                                            </label>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="navbar-header">
                            <button type="button" class="navbar-toggle new-navbutt" data-toggle="collapse" data-target="#myNavbar1213">
                                Support
                            </button>
                        </div>
                        <div class="collapse navbar-collapse" id="myNavbar1213" style="padding: 0px;">
                            <div class="row">
                                <div class="col-lg-3 col-md-3 col-sm-5 col-xs-12" style="padding: 0px;">
                                    <table class="table table-bordered table-mb" style="height: 54px; margin-bottom: 0px;">
                                        <tr style="width: 17%;">
                                            <td>
                                                <div>
                                                    <label style="line-height: 20px; font-size: 12px; font-family: calibri;">
                                                        <asp:CheckBox ID="chkSupoort" Enabled="false" runat="server" />
                                                    </label>
                                                </div>
                                            </td>
                                            <td style="vertical-align: top; font-size: 13px; font-family: calibri; width: 50%;
                                                background-color: Black; color: white;">
                                                Support
                                            </td>
                                            <td>
                                                <div>
                                                    <label style="line-height: 20px; font-size: 12px; font-family: calibri;">
                                                         <asp:CheckBox ID="RadioButton1" Enabled="false" CssClass="cr-icon" runat="server" />
                                                    </label>
                                                </div>
                                            </td>
                                            <td>
                                                <div>
                                                    <label style="line-height: 20px; font-size: 12px; font-family: calibri;">
                                                        <asp:CheckBox ID="rblsupportfc"  runat="server" />
                                                    </label>
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                                <div class="col-lg-9 col-md-9 col-sm-7 col-xs-12" style="padding: 0px;">
                                    <div class="row">
                                        <div  style="height: 74px; margin-bottom: 0px;">
                                            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="padding: 0px;">
                                                <div class="col-sm-3 " style="border: 1px solid #DDDDDD; height: 54px">
                                                    <asp:Label ID="Label14" runat="server" Text="Agenda"></asp:Label>
                                                    <asp:TextBox ID="txtSuport" autocomplete="off" ondrop="return false;" runat="server"
                                                        CssClass="form-control" onkeypress="return false;" TabIndex="5"></asp:TextBox>
                                                    <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtOtherComminuty"
                                        Display="Dynamic" ErrorMessage="*"
                                        >*</asp:RequiredFieldValidator>--%>
                                                    <cc1:PopupControlExtender ID="PopupControlExtender5" runat="server" TargetControlID="txtSuport"
                                                        PopupControlID="pnt_Suport" OffsetY="22">
                                                    </cc1:PopupControlExtender>
                                                    <asp:Panel ID="pnt_Suport" runat="server" Direction="LeftToRight" Style="display: none;
                                                        min-height: 60px; max-height: 300px; overflow: auto; z-index: 999999; background-color: #F1F1F1;
                                                        border: solid 1px #cccccc; width: 89.5%" CssClass="panel">
                                                        <span>
                                                            <asp:CheckBoxList ID="chk_Suport" CssClass="_bookformat6 radio" runat="server" onclick="SetMultilanguage('SS','_bookformat6');">
                                                            </asp:CheckBoxList>
                                                        </span>
                                                        <asp:HiddenField runat="server" ID="hhSuportId" />
                                                        <asp:HiddenField runat="server" ID="hhSuportName" />
                                                    </asp:Panel>
                                                </div>
                                                <div class="col-sm-3 " style="border-top: 1px solid #DDDDDD; border-bottom: 1px solid #DDDDDD;
                                                    height: 54px">
                                                    <asp:Label ID="Label15" runat="server" Text="Other(Specify)"></asp:Label>
                                                    <asp:TextBox runat="server" autocomplete="off" ondrop="return false;" ID="txtOtherSupport"
                                                        CssClass="form-control" MaxLength="60" TabIndex="18"></asp:TextBox>
                                                </div>
                                                <div class="col-sm-3 " style="border: 1px solid #DDDDDD; height: 54px">
                                                </div>
                                                <div class="col-sm-3" style="border-top: 1px solid #DDDDDD; border-bottom: 1px solid #DDDDDD;
                                                    border-right: 1px solid #DDDDDD; height: 54px">
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="navbar-header">
                            <button type="button" class="navbar-toggle new-navbutt" data-toggle="collapse" data-target="#myNavbar1">
                                Other - Specify
                            </button>
                        </div>
                        <div class="collapse navbar-collapse" id="Div5" style="padding: 0px;">
                            <div class="row">
                                <div class="col-lg-3 col-md-3 col-sm-5 col-xs-12" style="padding: 0px;">
                                    <table class="table table-bordered table-mb" style="height: 54px; margin-bottom: 0px;">
                                        <tr>
                                            <td style="width: 17%;">
                                                <div>
                                                    <label style="line-height: 20px; font-size: 12px; font-family: calibri;">
                                                        <asp:CheckBox ID="chkother" Enabled="false" runat="server" />
                                                    </label>
                                                </div>
                                            </td>
                                            <td class="td-width1" style="vertical-align: top; font-size: 13px; width: 50%; font-family: calibri;
                                                background-color: Black; color: white;">
                                                Other - Specify
                                            </td>
                                            <td>
                                                <div>
                                                    <label style="line-height: 20px; font-size: 12px; font-family: calibri;">
                                                        <asp:RadioButton ID="rblothertb" GroupName="H" CssClass="cr-icon" runat="server" />
                                                    </label>
                                                </div>
                                            </td>
                                            <td>
                                                <div>
                                                    <label style="line-height: 20px; font-size: 12px; font-family: calibri;">
                                                        <asp:RadioButton ID="rblotherfc" GroupName="H" CssClass="cr-icon" runat="server" />
                                                    </label>
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                                <div class="col-lg-9 col-md-9 col-sm-7 col-xs-12" style="padding: 0px;">
                                    <div class="row">
                                        <div  style="height: 60px; margin-bottom: 0px;">
                                            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="padding: 0px;">
                                                <div class="col-lg-3 col-md-3 col-sm-6 col-xs-12">
                                                    <div class="checkbox">
                                                        <label style="line-height: 20px; font-size: 12px; font-family: calibri;">
                                                            <asp:TextBox ID="txtmainother"  CssClass="form-control" autocomplete="off" ondrop="return false;" Style="height: 53px;
                                                                width: 129%;" TextMode="MultiLine" runat="server"></asp:TextBox>
                                                        </label>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </asp:Panel>
       
                </div>
            </div>


            <asp:Label ID="lblMM" Visible="false" runat="server" Text="Label"></asp:Label>
            <asp:Label ID="lblGG" Visible="false" runat="server" Text="Label"></asp:Label>
            <asp:Label ID="lblCom" Visible="false" runat="server" Text="Label"></asp:Label>
            <asp:Label ID="lblCom1" Visible="false" runat="server" Text="Label"></asp:Label>
        </div>

          <cc1:ModalPopupExtender ID="MpexdrDistrict" runat="server" BackgroundCssClass="modalBg "
                    PopupControlID="PnlDistrict"   CancelControlID="CancelButton"  TargetControlID="HdnFild7">
                </cc1:ModalPopupExtender>
            
                 <asp:HiddenField ID="HdnFild7" runat="server"></asp:HiddenField>
                 <asp:Panel cssclass="model-wid mod-posi"  Style="display: none;height:auto;width: 45% !important; margin-top:125px !important;" ID="PnlDistrict" runat="server">
                   
                    <div style="width:100%;height:auto;background-color:#f1f1f1">
                    <div class="modal-header"  style="background-color:#3ac0f2;color:White;">
                  
                    </div>
                   <div class="modal-body">
                   <asp:Label ID="lblmsg" runat="server" ForeColor="Red" Font-Names="Verdana" Font-Size="11px"></asp:Label>
                   <div class="form-horizontal" >

                           </div>
                           
                        <asp:ImageMap ID="imgMKS" runat="server" Height="250px" Width="400px" BorderColor="Black"
                                                            BorderStyle="Ridge" BorderWidth="1px" />

                  
                   </div>
                    <div class="modal-footer">
                  
                            <asp:Button ID="CancelButton" runat="server" CssClass="btn bgm-cyan" Text="Close"
                                ToolTip="Close" Style="float: none;"></asp:Button></div>
                  
                    </div>
                    
                       
                       
                </asp:Panel>
            </ContentTemplate>
            
     <Triggers>
            <asp:PostBackTrigger ControlID="lnkEnrool" />
           
            </Triggers>
    </asp:UpdatePanel>
    
 
            <cc1:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="Hdn_model4"
            PopupControlID="pnlpopup4" BackgroundCssClass="modalBackground">
        </cc1:ModalPopupExtender>

        <asp:HiddenField ID="Hdn_model4" runat="server" />
           <asp:Panel ID="pnlpopup4" runat="server" Style="display: none;">
           <div class="modal-dialog modal-md">
                <div class="modal-content">
                    <div class="modal-header" style="height: 0px;">
                    <asp:ImageButton ID="ImageButton8" CssClass="btn btn-info pull-right"  OnClick="btnReset_Click" BackColor="#f5f5f5"
                            ToolTip="Add" ImageUrl="~/images/close-29.png"  Style="margin-right: 5px;
                            padding: 0px;" runat="server" />
                        <h4 class="modal-title">
                            Remarks</h4>
                        
                    </div>

                       <div class="row">

                        <div class="row marg search-bg">
               
                                <div class="col-lg-10 col-md-10 col-sm-10 cpl-xs-12">
                                    <div class="col-lg-8 col-md-8  col-sm-8 cpl-xs-12">
                                    <div class="form-group" style="margin-bottom: 2px;">
                                        <label for="email" class="col-sm-3 padd linhei">
                                            Remarks:</label>
                                        <div class="col-sm-9 padd">
                                           <asp:DropDownList ID="ddlRemark" runat="server" class="form-control">
                                            <asp:ListItem Selected="True" Value="0">--Select--</asp:ListItem>
                                            <asp:ListItem Value="1">Format not available</asp:ListItem>
                                            <asp:ListItem Value="2">Wrongly activity selected </asp:ListItem>
                                            <asp:ListItem Value="3">Typing error</asp:ListItem>

                                             <asp:ListItem Value="4">Counting error</asp:ListItem>
                                               <asp:ListItem Value="5">C Phone not available</asp:ListItem>
                                        </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>

                          
                                </div>

                                
                        </div>
                  </div>
                    </div>
           </div>
           </asp:Panel>


             
</asp:Content>
