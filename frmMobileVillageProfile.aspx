<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.master" Culture="en-GB"
    CodeFile="frmMobileVillageProfile.aspx.cs" Inherits="frmMobileVillageProfile" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script type="text/javascript">
        function arrivaldatecheck(sender, args) {
            var depdate = 'dep';

            var departuredate = $('.' + depdate).val();
            var arrivaldate = sender._selectedDate;
            var today = new Date();
            debugger;
            var day = sender._selectedDate.getDate();
            var month = sender._selectedDate.getMonth() + 1;
            var year = sender._selectedDate.getFullYear();

            var date = year + "-" + month + "-" + day;



            var day1 = today.getDate();
            var month1 = today.getMonth() + 1;
            var year1 = today.getFullYear();

            var date1 = year1 + "-" + month1 + "-" + day1;

            var D1 = new Date(date);
            var D2 = new Date(date1);
            if (D1 >= D2) {
                alert("Should not be future date or today date.");
                sender._textbox.set_Value("")

                return false;

            }

        }
    </script>
    <script type="text/javascript">
        function arrivaldatecheckB(sender, args) {
            var depdate = 'dep';
            var Div787 = $('#DivI5');
            debugger;




            var departuredate = $('.' + depdate).val();
            var arrivaldate = sender._selectedDate;
            var today = new Date();

            var day = sender._selectedDate.getDate();
            var month = sender._selectedDate.getMonth() + 1;
            var year = sender._selectedDate.getFullYear();

            var date = year + "-" + month + "-" + day;



            var day1 = today.getDate();
            var month1 = today.getMonth() + 1;
            var year1 = today.getFullYear();

            var date1 = year1 + "-" + month1 + "-" + day1;

            var D1 = new Date(date);
            var D2 = new Date(date1);
            if (D1 >= D2) {
                alert("Should not be future date or today date.");
                sender._textbox.set_Value("")
                $('.clsD').hide();
                return false;

            }
            else {
                $('.clsD').show();
            }

        }
    </script>
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
            debugger;
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
            else if (Flag == 'FN') {
                if (maxSelection <= 10) {
                    $('#<%=hdn_PBIDNew.ClientID %>').val(lid);
                    $('#<%=hdn_pbnameNew.ClientID %>').val(Lngg);
                    $('#<%=txt_pbnameNew.ClientID %>').val(Lngg);

                }

                else {
                    $('.' + clsname + ' input[type="checkbox"]:checked').each(function () {
                        $(this).attr("checked", false);
                    });
                    $('#<%=hdn_PBIDNew.ClientID %>').val('');
                    $('#<%=hdn_pbnameNew.ClientID %>').val('');
                    $('#<%=txt_pbnameNew.ClientID %>').val('');


                    $find("Modal_alertB").show();
                    return false;
                }

                if (Lngg.toLowerCase().indexOf("other(detail)") >= 0) {

                    $('#<%=txt_bookformatOther1.ClientID %>').val('');
                    $('#<%=txt_bookformatOther1.ClientID %>').attr('disabled', false);
                }
                else {

                    $('#<%=txt_bookformatOther1.ClientID %>').val('');
                    $('#<%=txt_bookformatOther1.ClientID %>').attr('disabled', true);
                }
            }
            else if (Flag == 'FN1') {
                if (maxSelection <= 10) {
                    $('#<%=hdntxt_pbnameNew1_ID.ClientID %>').val(lid);
                    $('#<%=hdntxt_pbnameNew1_Name.ClientID %>').val(Lngg);
                    $('#<%=txt_pbnameNew1.ClientID %>').val(Lngg);

                }

                else {
                    $('.' + clsname + ' input[type="checkbox"]:checked').each(function () {
                        $(this).attr("checked", false);
                    });
                    $('#<%=hdntxt_pbnameNew1_ID.ClientID %>').val('');
                    $('#<%=hdntxt_pbnameNew1_Name.ClientID %>').val('');
                    $('#<%=txt_pbnameNew1.ClientID %>').val('');


                    $find("Modal_alertB").show();
                    return false;
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
            else if (Flag == 'MN') {
                if (maxSelection <= 10) {
                    $('#<%=hnd_txtMuhalaNew_ID.ClientID %>').val(lid);
                    $('#<%=hdn_txtMuhalaNew_Name.ClientID %>').val(Lngg);
                    $('#<%=txtMuhalaNew.ClientID %>').val(Lngg);
                }

                else {
                    $('.' + clsname + ' input[type="checkbox"]:checked').each(function () {
                        $(this).attr("checked", false);
                    });
                    $('#<%=hnd_txtMuhalaNew_ID.ClientID %>').val('');
                    $('#<%=hdn_txtMuhalaNew_Name.ClientID %>').val('');
                    $('#<%=txtMuhalaNew.ClientID %>').val('');


                    $find("Modal_alertB").show();
                    return false;
                }

                if (Lngg.toLowerCase().indexOf("other(detail)") >= 0) {

                    $('#<%=txtmOther1.ClientID %>').val('');
                    $('#<%=txtmOther1.ClientID %>').attr('disabled', false);
                }
                else {

                    $('#<%=txtmOther1.ClientID %>').val('');
                    $('#<%=txtmOther1.ClientID %>').attr('disabled', true);
                }
            }
            else if (Flag == 'MN1') {
                if (maxSelection <= 10) {
                    $('#<%=hdn_ID_txtMuhalaNew1.ClientID %>').val(lid);
                    $('#<%=hdn_Name_txtMuhalaNew1.ClientID %>').val(Lngg);
                    $('#<%=txtMuhalaNew1.ClientID %>').val(Lngg);
                }

                else {
                    $('.' + clsname + ' input[type="checkbox"]:checked').each(function () {
                        $(this).attr("checked", false);
                    });
                    $('#<%=hdn_ID_txtMuhalaNew1.ClientID %>').val('');
                    $('#<%=hdn_Name_txtMuhalaNew1.ClientID %>').val('');
                    $('#<%=txtMuhalaNew1.ClientID %>').val('');


                    $find("Modal_alertB").show();
                    return false;
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
            else if (Flag == 'OCN') {
                if (maxSelection <= 10) {
                    $('#<%=hdnOtherComNew_ID.ClientID %>').val(lid);
                    $('#<%=hdnOtherComNew_Name.ClientID %>').val(Lngg);
                    $('#<%=txtOtherComminutyNew.ClientID %>').val(Lngg);
                }

                else {
                    $('.' + clsname + ' input[type="checkbox"]:checked').each(function () {
                        $(this).attr("checked", false);
                    });
                    $('#<%=hdnOtherComNew_ID.ClientID %>').val('');
                    $('#<%=hdnOtherComNew_Name.ClientID %>').val('');
                    $('#<%=txtOtherComminutyNew.ClientID %>').val('');


                    $find("Modal_alertB").show();
                    return false;
                }

                if (Lngg.toLowerCase().indexOf("other(detail)") >= 0) {

                    $('#<%=txtOtherComm1.ClientID %>').val('');
                    $('#<%=txtOtherComm1.ClientID %>').attr('disabled', false);
                }
                else {

                    $('#<%=txtOtherComm1.ClientID %>').val('');
                    $('#<%=txtOtherComm1.ClientID %>').attr('disabled', true);
                }
            }
            else if (Flag == 'OCN1') {
                if (maxSelection <= 10) {
                    $('#<%=hdn_ID_txtOtherComminutyNew1.ClientID %>').val(lid);
                    $('#<%=hdn_Name_txtOtherComminutyNew1.ClientID %>').val(Lngg);
                    $('#<%=txtOtherComminutyNew1.ClientID %>').val(Lngg);
                }

                else {
                    $('.' + clsname + ' input[type="checkbox"]:checked').each(function () {
                        $(this).attr("checked", false);
                    });
                    $('#<%=hdn_ID_txtOtherComminutyNew1.ClientID %>').val('');
                    $('#<%=hdn_Name_txtOtherComminutyNew1.ClientID %>').val('');
                    $('#<%=txtOtherComminutyNew1.ClientID %>').val('');


                    $find("Modal_alertB").show();
                    return false;
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
        .multiselect
        {
            width: 20em;
            height: 15em;
            border: solid 1px #c0c0c0;
            overflow: auto;
        }
        
        .multiselect label
        {
            display: block;
        }
        
        .multiselect-on
        {
            color: #ffffff;
            background-color: #000099;</style>
    }
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:UpdatePanel runat="server" ID="UpMain">
        <ContentTemplate>
            <div class="row" >
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
                                                        runat="server" class="form-control " />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-lg-3 col-md-3 col-sm-3 cpl-xs-12">
                                            <div class="form-group" style="margin-bottom: 7px;">
                                                <label for="email" class="col-sm-3 padd linhei">
                                                    Date:</label>
                                                <div class="col-sm-9 padd">
                                                    <asp:TextBox runat="server" ID="txtDate" autocomplete="off" ondrop="return false;"
                                                        class="form-control" onkeypress="return false;"></asp:TextBox>
                                                    <ajax:CalendarExtender ID="CalendarExtenderTourdate" runat="server" Enabled="True"
                                                        Format="dd/MM/yyyy" TargetControlID="txtDate" PopupPosition="BottomRight">
                                                    </ajax:CalendarExtender>
                                                    <asp:RequiredFieldValidator ID="ReqTxtDate" runat="server" ControlToValidate="txtDate"
                                                        Display="Dynamic" ErrorMessage="*" Font-Bold="False" Font-Size="9px" ForeColor="Red"
                                                        SetFocusOnError="True" ValidationGroup="saves"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-lg-3 col-md-3 col-sm-3 cpl-xs-12">
                                            <asp:ImageButton ID="btnDelete" OnClick="btnDelete_Click" CssClass="btn btn-info pull-right"
                                                ToolTip="Delete" BackColor="#f5f5f5" ImageUrl="~/images/delete-29.png" Style="margin-right: 5px;
                                                padding: 0px;" runat="server" />
                                            <asp:Button ID="btnApprove" CssClass="btn btn-success pull-right " ToolTip="Save"
                                                Text="  Back" OnClick="btnApprove_Click" Style="margin-right: 5px; padding: 0px;"
                                                runat="server" />
                                            <asp:ImageButton ID="btnsave" CssClass="btn btn-info pull-right" OnClick="btnSave_Click"
                                                BackColor="#f5f5f5" ToolTip="Save" ImageUrl="~/images/save-29-1.png" ValidationGroup="saves"
                                                Style="margin-right: 5px; padding: 0px;" runat="server" />
                                            <asp:ImageButton ID="ImageButton1" CssClass="btn btn-info pull-right" BackColor="#f5f5f5"
                                                ToolTip="Add" Visible="false" ImageUrl="~/images/add-29-1.png" OnClick="btnAdd_Click"
                                                Style="margin-right: 5px; padding: 0px;" runat="server" />
                                            <asp:ImageButton ID="btnSerach" ToolTip="Serach" runat="server" OnClick="btnSerach_Click"
                                                class="btn btn-danger btn-paddd pull-right" BackColor="#f1f1f1" ImageUrl="~/images/search-29.png" />
                                            <asp:ImageButton ID="btnEdit" ToolTip="Edit" OnClick="btnEdit_Click" runat="server"
                                                class="btn btn-danger btn-paddd pull-right" BackColor="#f1f1f1" ImageUrl="~/images/edit.png" />
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
                                                    <asp:ImageButton ID="imgGSS" runat="server" Width="30" Height="25" OnClick="btnImgGss_Click"
                                                        Visible="false" CssClass="pull-right" ImageUrl="~/images/iconimage-128.png">
                                                    </asp:ImageButton>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-lg-3 col-md-3 col-sm-3 cpl-xs-12">
                                            <div class="form-group" style="margin-bottom: 7px;">
                                                <label for="email" class="col-sm-3 padd linhei">
                                                    MM:</label>
                                                <div class="col-sm-4 padd">
                                                    <asp:ImageButton ID="ImgMM" runat="server" Width="30" Height="25" OnClick="btnImgMM_Click"
                                                        Visible="false" CssClass="pull-right" ImageUrl="~/images/iconimage-128.png">
                                                    </asp:ImageButton>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-lg-3 col-md-3 col-sm-3 cpl-xs-12">
                                            <div class="form-group" style="margin-bottom: 7px;">
                                                <label for="email" class="col-sm-3 padd linhei">
                                                    Community :</label>
                                                <div class="col-sm-4 padd">
                                                    <asp:ImageButton ID="imgComm1" runat="server" Width="30" Height="25" OnClick="btnimgComm1_Click"
                                                        Visible="false" CssClass="pull-right" ImageUrl="~/images/iconimage-128.png">
                                                    </asp:ImageButton>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-lg-3 col-md-3 col-sm-3 cpl-xs-12">
                                            <div class="form-group" style="margin-bottom: 7px;">
                                                <label for="email" class="col-sm-3 padd linhei">
                                                    Other :</label>
                                                <div class="col-sm-4 padd">
                                                    <asp:ImageButton ID="imgComm2" runat="server" Width="30" Height="25" OnClick="btnimgComm2_Click"
                                                        Visible="false" CssClass="pull-right" ImageUrl="~/images/iconimage-128.png">
                                                    </asp:ImageButton>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-lg-3 col-md-3 col-sm-5 col-xs-12" style="padding: 0px;">
                                <table class="table table-bordered table-mb" style="height: 64px; margin-bottom: 0px;
                                    margin-left: -12px;">
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
                                                    <asp:CheckBox ID="rblTbhold" Enabled="false" CssClass="cr-icon" runat="server" />
                                                </label>
                                            </div>
                                        </td>
                                        <td style="text-align: left">
                                            <div class="checkbox">
                                                <label style="line-height: 20px; font-size: 12px; font-family: calibri;">
                                                    <asp:CheckBox ID="rblFcHold" GroupName="A" CssClass="cr-icon" runat="server" />
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
                                        <table class="table table-bordered table-mb" style="height: 150px; margin-bottom: 0px;">
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
                                            <div style="height: 97px; margin-bottom: 0px;">
                                                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="padding: 0px;">
                                                    <div class="col-sm-5 " style="border: 1px solid #DDDDDD; height: 121px">
                                                    
                                                        <div class="col-sm-6 col-md-6 col-lg-6 col-xs-12 " style="border-style: double;">
                                                            <asp:RadioButton ID="rdEnrollMent" GroupName="rdEnRe" Text="Enrollment" runat="server" />
                                                        </div>
                                                        <div class="col-sm-6 col-md-6 col-lg-6 col-xs-12" style="border-style: double;">
                                                            <asp:RadioButton ID="rdRetention" Text="Retention" GroupName="rdEnRe" runat="server" />
                                                        </div>
                                                      
                                                        <div class="col-sm-4 col-md-4 col-lg-4 col-xs-12">
                                                            <asp:Label ID="lblAgenda" runat="server" Text="Objective of Meeting"></asp:Label>
                                                              
                                                            <asp:TextBox ID="txt_pbname" autocomplete="off" ondrop="return false;" runat="server"
                                                                CssClass="form-control" onkeypress="return false;" TabIndex="5"></asp:TextBox>
                                                            <cc1:PopupControlExtender ID="PopupControltxt_pbname" runat="server" TargetControlID="txt_pbname"
                                                                PopupControlID="pnt_bookformat" OffsetY="22">
                                                            </cc1:PopupControlExtender>
                                                            <asp:Panel ID="pnt_bookformat" runat="server" Direction="LeftToRight" Style="display: none;
                                                                min-height: 60px; max-height: 300px; overflow: auto; z-index: 999999; background-color: #F1F1F1;
                                                                border: solid 1px #cccccc; width: 120%" CssClass="panel">
                                                                <span>
                                                                    <asp:CheckBoxList ID="CBL_bookformat" CssClass="_bookformat radio" runat="server"
                                                                        onclick="SetMultilanguage('F','_bookformat');">
                                                                    </asp:CheckBoxList>
                                                                </span>
                                                                <asp:HiddenField runat="server" ID="hdn_PBName" />
                                                                <asp:HiddenField runat="server" ID="hdn_PBID" />
                                                            </asp:Panel>
                                                        </div>
                                                        <div class="col-sm-4 col-md-4 col-lg-4 col-xs-12">
                                                            <asp:Label ID="LblAgenda1" runat="server" Text="Highlights of Discussion"></asp:Label>
                                                            <asp:TextBox ID="txt_pbnameNew" autocomplete="off" ondrop="return false;" runat="server"
                                                                CssClass="form-control col-sm-1" onkeypress="return false;" TabIndex="5"></asp:TextBox>
                                                            <cc1:PopupControlExtender ID="PopupControltxt_pbnameNew" runat="server" TargetControlID="txt_pbnameNew"
                                                                PopupControlID="pnt_txt_pbnameNew" OffsetY="22">
                                                            </cc1:PopupControlExtender>
                                                            <asp:Panel ID="pnt_txt_pbnameNew" runat="server" Direction="LeftToRight" Style="display: none;
                                                                min-height: 60px; max-height: 300px; overflow: auto; z-index: 999999; background-color: #F1F1F1;
                                                                border: solid 1px #cccccc; width: 158%" CssClass="panel">
                                                                <span>
                                                                    <asp:CheckBoxList ID="CBL_bookformatNew" CssClass="_bookformat9 radio" runat="server"
                                                                        onclick="SetMultilanguage('FN','_bookformat9');">
                                                                    </asp:CheckBoxList>
                                                                </span>
                                                                <asp:HiddenField runat="server" ID="hdn_pbnameNew" />
                                                                <asp:HiddenField runat="server" ID="hdn_PBIDNew" />
                                                            </asp:Panel>
                                                        </div>
                                                        <div class="col-sm-4 col-md-4 col-lg-4 col-xs-12">
                                                            <asp:Label ID="LblAgenda2" runat="server" Text="Key People Present"></asp:Label>
                                                            <asp:TextBox ID="txt_pbnameNew1" autocomplete="off" ondrop="return false;" runat="server"
                                                                CssClass="form-control col-sm-1" onkeypress="return false;" TabIndex="5"></asp:TextBox>
                                                            <cc1:PopupControlExtender ID="PopupControltxt_pbnameNew1" runat="server" TargetControlID="txt_pbnameNew1"
                                                                PopupControlID="pnt_txt_pbnameNew1" OffsetY="22">
                                                            </cc1:PopupControlExtender>
                                                            <asp:Panel ID="pnt_txt_pbnameNew1" runat="server" Direction="LeftToRight" Style="display: none;
                                                                min-height: 60px; max-height: 300px; overflow: auto; z-index: 999999; background-color: #F1F1F1;
                                                                border: solid 1px #cccccc; width: 178%" CssClass="panel">
                                                                <span>
                                                                    <asp:CheckBoxList ID="CBL_bookformatNew1" CssClass="_btxt_pbnameNew1 radio" runat="server"
                                                                        onclick="SetMultilanguage('FN1','_btxt_pbnameNew1');">
                                                                    </asp:CheckBoxList>
                                                                </span>
                                                                <asp:HiddenField runat="server" ID="hdntxt_pbnameNew1_Name" />
                                                                <asp:HiddenField runat="server" ID="hdntxt_pbnameNew1_ID" />
                                                            </asp:Panel>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-3" style="border-top: 1px solid #DDDDDD; border-bottom: 1px solid #DDDDDD;
                                                        height: 121px">
                                                        <div class="col-sm-6 col-md-6 col-lg-6 col-xs-12">
                                                            <asp:Label ID="lblOther" runat="server" autocomplete="off" ondrop="return false;"
                                                                Text="Other(Specify)"></asp:Label>
                                                            <asp:TextBox runat="server" ID="txt_bookformatOther" CssClass="form-control" MaxLength="30"
                                                                TabIndex="18"></asp:TextBox>
                                                        </div>
                                                        <div class="col-sm-6 col-md-6 col-lg-6 col-xs-12">
                                                            <asp:Label ID="Label50" runat="server" autocomplete="off" ondrop="return false;"
                                                                Text="Other(Specify1)"></asp:Label>
                                                            <asp:TextBox runat="server" ID="txt_bookformatOther1" CssClass="form-control" MaxLength="30"
                                                                TabIndex="18"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-2 " style="border: 1px solid #DDDDDD; height: 121px">
                                                        <span>
                                                            <asp:Label ID="Label1" runat="server" Text="People Attended"></asp:Label>
                                                        </span><span>
                                                            <asp:TextBox ID="txtV1illager" autocomplete="off" ondrop="return false;" MaxLength="3"
                                                                onkeypress="return isNumberKey(this,event);" CssClass="form-control" runat="server"></asp:TextBox>
                                                        </span>
                                                    </div>
                                                    <div class="col-sm-2" style="border-top: 1px solid #DDDDDD; border-bottom: 1px solid #DDDDDD;
                                                        border-right: 1px solid #DDDDDD; height: 121px">
                                                        <div class="col-sm-6 col-lg-6 col-md-6 col-xs-12">
                                                            <span>
                                                                <asp:Label ID="LblGSS_Male" runat="server" Text="Male"></asp:Label>
                                                            </span><span>
                                                                <asp:TextBox ID="TxtGSS_Male" autocomplete="off" ondrop="return false;" MaxLength="3"
                                                                    CssClass="form-control" runat="server"></asp:TextBox>
                                                                <cc1:FilteredTextBoxExtender ID="FilterTxtGSS_Male" TargetControlID="TxtGSS_Male"
                                                                    ValidChars="0123456789" runat="server" />
                                                            </span>
                                                        </div>
                                                        <div class="col-sm-6 col-lg-6 col-md-6 col-xs-12">
                                                            <span>
                                                                <asp:Label ID="LblGSS_FeMale" runat="server" Text="Female"></asp:Label>
                                                            </span><span>
                                                                <asp:TextBox ID="TxtGSS_FeMale" autocomplete="off" ondrop="return false;" MaxLength="3"
                                                                    CssClass="form-control" runat="server"></asp:TextBox>
                                                                <cc1:FilteredTextBoxExtender ID="FilterTxtGSS_FeMale" TargetControlID="TxtGSS_FeMale"
                                                                    ValidChars="0123456789" runat="server" />
                                                                <asp:ImageButton ID="ImageButton2" runat="server" OnClick="btnCLT_Click" Width="30"
                                                                    Style="height: 25px; width: 30px; border-width: 0px; margin-top: 8px;" Height="25"
                                                                    CssClass="pull-right" ImageUrl="~/images/Reset.png"></asp:ImageButton>
                                                            </span>
                                                        </div>
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
                                        <table class="table table-bordered table-mb" style="height: 121px; margin-bottom: 0px;">
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
                                            <div style="height: 97px; margin-bottom: 0px;">
                                                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="padding: 0px;">
                                                    <div class="col-sm-5 " style="border: 1px solid #DDDDDD; height: 121px">
                                                        <div class="col-sm-6 col-md-6 col-lg-6 col-xs-12" style="border-style: double;">
                                                            <asp:RadioButton ID="rdEnrollment1" Text="Enrollment" GroupName="rdEnRe1" runat="server" />
                                                        </div>
                                                        <div class="col-sm-6 col-md-6 col-lg-6 col-xs-12" style="border-style: double;">
                                                            <asp:RadioButton ID="rdRetantion1" Text="Retention" GroupName="rdEnRe1" runat="server" />
                                                        </div>
                                                        <div class="col-sm-4 col-md-4 col-lg-4 col-xs-12">
                                                            <asp:Label ID="Label2" runat="server" Text="Objective of Meeting"></asp:Label>
                                                            <asp:TextBox ID="txtMuhala" runat="server" autocomplete="off" ondrop="return false;"
                                                                CssClass="form-control" onkeypress="return false;" TabIndex="5"></asp:TextBox>
                                                            <cc1:PopupControlExtender ID="PopupControlExtender1" runat="server" TargetControlID="txtMuhala"
                                                                PopupControlID="pnt_Muhula" OffsetY="22">
                                                            </cc1:PopupControlExtender>
                                                            <asp:Panel ID="pnt_Muhula" runat="server" Direction="LeftToRight" Style="display: none;
                                                                min-height: 60px; max-height: 300px; overflow: auto; z-index: 999999; background-color: #F1F1F1;
                                                                border: solid 1px #cccccc; width: 158%" CssClass="panel">
                                                                <span>
                                                                    <asp:CheckBoxList ID="CBL_Muhula" CssClass="_bookformat1 radio" runat="server" onclick="SetMultilanguage('M','_bookformat1');">
                                                                    </asp:CheckBoxList>
                                                                </span>
                                                                <asp:HiddenField runat="server" ID="hhmuhulaid" />
                                                                <asp:HiddenField runat="server" ID="HidName" />
                                                            </asp:Panel>
                                                        </div>
                                                        <div class="col-sm-4 col-md-4 col-lg-4 col-xs-12">
                                                            <asp:Label ID="Label48" runat="server" Text="Highlights of Discussion"></asp:Label>
                                                            <asp:TextBox ID="txtMuhalaNew" autocomplete="off" ondrop="return false;" runat="server"
                                                                CssClass="form-control col-sm-1" onkeypress="return false;" TabIndex="5"></asp:TextBox>
                                                            <cc1:PopupControlExtender ID="PopupControltxtMuhalaNew" runat="server" TargetControlID="txtMuhalaNew"
                                                                PopupControlID="pnt_txtMuhalaNew" OffsetY="22">
                                                            </cc1:PopupControlExtender>
                                                            <asp:Panel ID="pnt_txtMuhalaNew" runat="server" Direction="LeftToRight" Style="display: none;
                                                                min-height: 60px; max-height: 300px; overflow: auto; z-index: 999999; background-color: #F1F1F1;
                                                                border: solid 1px #cccccc; width: 158%" CssClass="panel">
                                                                <span>
                                                                    <asp:CheckBoxList ID="CBL_MuhulaNew" CssClass="_bookformat10 radio" runat="server"
                                                                        onclick="SetMultilanguage('MN','_bookformat10');">
                                                                    </asp:CheckBoxList>
                                                                </span>
                                                                <asp:HiddenField runat="server" ID="hdn_txtMuhalaNew_Name" />
                                                                <asp:HiddenField runat="server" ID="hnd_txtMuhalaNew_ID" />
                                                            </asp:Panel>
                                                        </div>
                                                        <div class="col-sm-4 col-md-4 col-lg-4 col-xs-12">
                                                            <asp:Label ID="Label53" runat="server" Text="Key People Present"></asp:Label>
                                                            <asp:TextBox ID="txtMuhalaNew1" autocomplete="off" ondrop="return false;" runat="server"
                                                                CssClass="form-control col-sm-1" onkeypress="return false;" TabIndex="5"></asp:TextBox>
                                                            <cc1:PopupControlExtender ID="PopupControltxtMuhalaNew1" runat="server" TargetControlID="txtMuhalaNew1"
                                                                PopupControlID="pnt_txtMuhalaNew1" OffsetY="22">
                                                            </cc1:PopupControlExtender>
                                                            <asp:Panel ID="pnt_txtMuhalaNew1" runat="server" Direction="LeftToRight" Style="display: none;
                                                                min-height: 60px; max-height: 300px; overflow: auto; z-index: 999999; background-color: #F1F1F1;
                                                                border: solid 1px #cccccc; width: 178%" CssClass="panel">
                                                                <span>
                                                                    <asp:CheckBoxList ID="CBL_MuhulaNew1" CssClass="_btxtMuhalaNew1 radio" runat="server"
                                                                        onclick="SetMultilanguage('MN1','_btxtMuhalaNew1');">
                                                                    </asp:CheckBoxList>
                                                                </span>
                                                                <asp:HiddenField runat="server" ID="hdn_Name_txtMuhalaNew1" />
                                                                <asp:HiddenField runat="server" ID="hdn_ID_txtMuhalaNew1" />
                                                            </asp:Panel>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-3 " style="border-top: 1px solid #DDDDDD; border-bottom: 1px solid #DDDDDD;
                                                        height: 121px">
                                                        <div class="col-sm-6 col-md-6 col-lg-6 col-xs-12">
                                                            <asp:Label ID="Label3" runat="server" Text="Other(Specify)"></asp:Label>
                                                            <asp:TextBox runat="server" ID="txtmOther" autocomplete="off" ondrop="return false;"
                                                                CssClass="form-control" MaxLength="30" TabIndex="18"></asp:TextBox>
                                                        </div>
                                                        <div class="col-sm-6 col-md-6 col-lg-6 col-xs-12">
                                                            <asp:Label ID="Label51" runat="server" autocomplete="off" ondrop="return false;"
                                                                Text="Other(Specify1)"></asp:Label>
                                                            <asp:TextBox runat="server" ID="txtmOther1" CssClass="form-control" MaxLength="30"
                                                                TabIndex="18"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-2 " style="border: 1px solid #DDDDDD; height: 121px">
                                                        <span>
                                                            <asp:Label ID="Label4" runat="server" Text="People Attended"></asp:Label>
                                                        </span><span>
                                                            <asp:TextBox ID="txtVillager2" autocomplete="off" ondrop="return false;" MaxLength="3"
                                                                onkeypress="return isNumberKey(this,event);" CssClass="form-control" runat="server"></asp:TextBox>
                                                        </span>
                                                    </div>
                                                    <div class="col-sm-2" style="border-top: 1px solid #DDDDDD; border-bottom: 1px solid #DDDDDD;
                                                        border-right: 1px solid #DDDDDD; height: 121px">
                                                        <div class="col-sm-6 col-lg-6 col-md-6 col-xs-12">
                                                            <span>
                                                                <asp:Label ID="lblMM_Male" runat="server" Text="Male"></asp:Label>
                                                            </span><span>
                                                                <asp:TextBox ID="TxtMM_Male" autocomplete="off" ondrop="return false;" MaxLength="3"
                                                                    CssClass="form-control" runat="server"></asp:TextBox>
                                                                <cc1:FilteredTextBoxExtender ID="FilteredTxtMM_Male" TargetControlID="TxtMM_Male"
                                                                    ValidChars="0123456789" runat="server" />
                                                            </span>
                                                        </div>
                                                        <div class="col-sm-6 col-lg-6 col-md-6 col-xs-12">
                                                            <span>
                                                                <asp:Label ID="lblMM_FeMale" runat="server" Text="Female"></asp:Label>
                                                            </span><span>
                                                                <asp:TextBox ID="TxtMM_FeMale" autocomplete="off" ondrop="return false;" MaxLength="3"
                                                                    CssClass="form-control" runat="server"></asp:TextBox>
                                                                <cc1:FilteredTextBoxExtender ID="FilteredTxtMM_FeMale" TargetControlID="TxtMM_FeMale"
                                                                    ValidChars="0123456789" runat="server" />
                                                                <asp:ImageButton ID="ImageButton3" runat="server" OnClick="btnmm_Click" Width="30"
                                                                    Style="height: 25px; width: 30px; border-width: 0px; margin-top: 8px;" Height="25"
                                                                    CssClass="pull-right" ImageUrl="~/images/Reset.png"></asp:ImageButton>
                                                            </span>
                                                        </div>
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
                                        <table class="table table-bordered table-mb" style="height: 121px; margin-bottom: 0px;">
                                            <tr>
                                                <td style="width: 17%;">
                                                    <div>
                                                        <label style="line-height: 20px; font-size: 12px; font-family: calibri;">
                                                            <asp:CheckBox ID="chkothercomm" Enabled="false" runat="server" />
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
                                            <div style="height: 97px; margin-bottom: 0px;">
                                                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="padding: 0px;">
                                                    <div class="col-sm-5 " style="border: 1px solid #DDDDDD; height: 121px">
                                                        <div class="col-sm-6 col-md-6 col-lg-6 col-xs-12" style="border-style: double;">
                                                            <asp:RadioButton ID="rdEnrollment2" Text="Enrollment" GroupName="rdEnRe2" runat="server" />
                                                        </div>
                                                        <div class="col-sm-6 col-md-6 col-lg-6 col-xs-12" style="border-style: double;">
                                                            <asp:RadioButton ID="rdRetantion2" Text="Retention" GroupName="rdEnRe2" runat="server" />
                                                        </div>
                                                        <div class="col-sm-4 col-md-4 col-lg-4 col-xs-12">
                                                            <asp:Label ID="Label5" runat="server" Text="Objective of Meeting"></asp:Label>
                                                            <asp:TextBox ID="txtOtherComminuty" autocomplete="off" ondrop="return false;" runat="server"
                                                                CssClass="form-control" onkeypress="return false;" TabIndex="5"></asp:TextBox>
                                                         
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
                                                        <div class="col-sm-4 col-md-4 col-lg-4 col-xs-12">
                                                            <asp:Label ID="Label49" runat="server" Text="Highlights of Discussion"></asp:Label>
                                                            <asp:TextBox ID="txtOtherComminutyNew" autocomplete="off" ondrop="return false;"
                                                                runat="server" CssClass="form-control col-sm-1" onkeypress="return false;" TabIndex="5"></asp:TextBox>
                                                            <cc1:PopupControlExtender ID="PopupControlExtender7" runat="server" TargetControlID="txtOtherComminutyNew"
                                                                PopupControlID="pnt_txtOtherComminutyNew" OffsetY="22">
                                                            </cc1:PopupControlExtender>
                                                            <asp:Panel ID="pnt_txtOtherComminutyNew" runat="server" Direction="LeftToRight" Style="display: none;
                                                                min-height: 60px; max-height: 300px; overflow: auto; z-index: 999999; background-color: #F1F1F1;
                                                                border: solid 1px #cccccc; width: 158%" CssClass="panel">
                                                                <span>
                                                                    <asp:CheckBoxList ID="chk_othercom_New" CssClass="_bookformat11 radio" runat="server"
                                                                        onclick="SetMultilanguage('OCN','_bookformat11');">
                                                                    </asp:CheckBoxList>
                                                                </span>
                                                                <asp:HiddenField runat="server" ID="hdnOtherComNew_ID" />
                                                                <asp:HiddenField runat="server" ID="hdnOtherComNew_Name" />
                                                            </asp:Panel>
                                                        </div>
                                                        <div class="col-sm-4 col-md-4 col-lg-4 col-xs-12">
                                                            <asp:Label ID="Label54" runat="server" Text="Key People Present"></asp:Label>
                                                            <asp:TextBox ID="txtOtherComminutyNew1" autocomplete="off" ondrop="return false;"
                                                                runat="server" CssClass="form-control col-sm-1" onkeypress="return false;" TabIndex="5"></asp:TextBox>
                                                            <cc1:PopupControlExtender ID="PopupControltxtOtherComminutyNew1" runat="server" TargetControlID="txtOtherComminutyNew1"
                                                                PopupControlID="pnt_txtOtherComminutyNew1" OffsetY="22">
                                                            </cc1:PopupControlExtender>
                                                            <asp:Panel ID="pnt_txtOtherComminutyNew1" runat="server" Direction="LeftToRight"
                                                                Style="display: none; min-height: 60px; max-height: 300px; overflow: auto; z-index: 999999;
                                                                background-color: #F1F1F1; border: solid 1px #cccccc; width: 178%" CssClass="panel">
                                                                <span>
                                                                    <asp:CheckBoxList ID="chk_othercom_New1" CssClass="_btxtOtherComminutyNew1 radio"
                                                                        runat="server" onclick="SetMultilanguage('OCN1','_btxtOtherComminutyNew1');">
                                                                    </asp:CheckBoxList>
                                                                </span>
                                                                <asp:HiddenField runat="server" ID="hdn_ID_txtOtherComminutyNew1" />
                                                                <asp:HiddenField runat="server" ID="hdn_Name_txtOtherComminutyNew1" />
                                                            </asp:Panel>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-3 " style="border-top: 1px solid #DDDDDD; border-bottom: 1px solid #DDDDDD;
                                                        height: 121px">
                                                        <div class="col-sm-6 col-md-6 col-lg-6 col-xs-12">
                                                            <asp:Label ID="Label6" runat="server" Text="Other(Specify)"></asp:Label>
                                                            <asp:TextBox runat="server" ID="txtOtherComm" autocomplete="off" ondrop="return false;"
                                                                CssClass="form-control" MaxLength="30" TabIndex="18"></asp:TextBox>
                                                        </div>
                                                        <div class="col-sm-6 col-md-6 col-lg-6 col-xs-12">
                                                            <asp:Label ID="Label52" runat="server" autocomplete="off" ondrop="return false;"
                                                                Text="Other(Specify1)"></asp:Label>
                                                            <asp:TextBox runat="server" ID="txtOtherComm1" CssClass="form-control" MaxLength="30"
                                                                TabIndex="18"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-2 " style="border: 1px solid #DDDDDD; height:121px">
                                                        <span>
                                                            <asp:Label ID="Label7" runat="server" Text="People Attended"></asp:Label>
                                                        </span><span>
                                                            <asp:TextBox ID="txtvillager3" autocomplete="off" ondrop="return false;" MaxLength="3"
                                                                onkeypress="return isNumberKey(this,event);" CssClass="form-control" runat="server"></asp:TextBox>
                                                        </span>
                                                    </div>
                                                    <div class="col-sm-2" style="border-top: 1px solid #DDDDDD; border-bottom: 1px solid #DDDDDD;
                                                        border-right: 1px solid #DDDDDD; height: 121px">
                                                        <div class="col-sm-6 col-lg-6 col-md-6 col-xs-12">
                                                            <span>
                                                                <asp:Label ID="lblCM1_Male" runat="server" Text="Male"></asp:Label>
                                                            </span><span>
                                                                <asp:TextBox ID="TxtCm1_Male" autocomplete="off" ondrop="return false;" MaxLength="3"
                                                                    CssClass="form-control" runat="server"></asp:TextBox>
                                                                <cc1:FilteredTextBoxExtender ID="FilteredTxtCm1_Male" TargetControlID="TxtCm1_Male"
                                                                    ValidChars="0123456789" runat="server" />
                                                            </span>
                                                        </div>
                                                        <div class="col-sm-6 col-lg-6 col-md-6 col-xs-12">
                                                            <span>
                                                                <asp:Label ID="lblCM1_FeMale" runat="server" Text="Female"></asp:Label>
                                                            </span><span>
                                                                <asp:TextBox ID="TxtCm1_FeMale" autocomplete="off" ondrop="return false;" MaxLength="3"
                                                                    CssClass="form-control" runat="server"></asp:TextBox>
                                                                <cc1:FilteredTextBoxExtender ID="FilteredTxtCm1_FeMale" TargetControlID="TxtCm1_FeMale"
                                                                    ValidChars="0123456789" runat="server" />
                                                                <asp:ImageButton ID="ImageButton4" runat="server" Style="height: 25px; width: 30px;
                                                                    border-width: 0px; margin-top:8px;" OnClick="btnOther_Click" CssClass="pull-right"
                                                                    ImageUrl="~/images/Reset.png"></asp:ImageButton>
                                                            </span>
                                                        </div>
                                                        <span style="display: none;">
                                                            <asp:Label ID="Label10" runat="server" Text="Other Contact"></asp:Label>
                                                        </span><span style="display: none;">
                                                            <asp:TextBox ID="tc1" autocomplete="off" ondrop="return false;" MaxLength="50" CssClass="form-control"
                                                                runat="server"></asp:TextBox>
                                                            <%--  <asp:ImageButton ID="ImageButton4" runat="server" Style="height: 25px; width: 30px;
                                                                border-width: 0px; margin-top: -54px;" OnClick="btnOther_Click" CssClass="pull-right"
                                                                ImageUrl="~/images/Reset.png"></asp:ImageButton>--%>
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
                                            <div style="height: 54px; margin-bottom: 0px;">
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
                                                                <asp:CheckBoxList ID="chk_c2" CssClass="_bookformat11 radio" runat="server" onclick="SetMultilanguage('CC2','_bookformat11');">
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
                                                            <asp:ImageButton ID="ImageButton5" runat="server" Style="height: 25px; width: 30px;
                                                                border-width: 0px; margin-top: -54px;" OnClick="btnOther1_Click" CssClass="pull-right"
                                                                ImageUrl="~/images/Reset.png"></asp:ImageButton>
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
                                                            <asp:CheckBox ID="rblcommtb" Visible="false" Enabled="false" CssClass="cr-icon" runat="server" />
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
                                            <div>
                                                <%-- <h3 class="text-danger" style="padding-left: 18px; margin: 0px 0px 10px; font-size: 15px;">
                                                Ambition</h3>--%>
                                                <div class="row">
                                                    <div style="height: 54px; margin-bottom: 0px;">
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
                                                                    CssClass="form-control" MaxLength="60" TabIndex="18"></asp:TextBox>
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
                                    PopupControlID="pnlpopup3" CancelControlID="btnAdd" BackgroundCssClass="modalBackground">
                                </cc1:ModalPopupExtender>
                                <asp:HiddenField ID="Hdn_model3" runat="server" />
                                <asp:Panel ID="pnlpopup3" runat="server" Style="display: none;">
                                    <div class=" modal-lg">
                                        <div class="modal-content">
                                            <div class="modal-header">
                                                <asp:ImageButton ID="btnAdd" CssClass="btn btn-info pull-right" BackColor="#f5f5f5"
                                                    ToolTip="Add" ImageUrl="~/images/close-29.png" Style="margin-right: 5px; padding: 0px;"
                                                    runat="server" />
                                                <asp:ImageButton ID="ImageButton10" CssClass="btn btn-info pull-right" BackColor="#f5f5f5"
                                                    ToolTip="Save" ImageUrl="~/images/save-29-1.png" Visible="false" Style="margin-right: 5px;
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
                                                                        class="form-control"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-lg-2 col-md-2  col-sm-2 cpl-xs-12">
                                                        <div class="col-lg-1 col-md-1 col-sm-2 cpl-xs-12 col-lg-offset-1 col-md-offset-1 col-sm-offset-1 col-xs-offset-0">
                                                            <asp:ImageButton ID="ImageButton7" OnClick="btnD2dSerach_Click" ToolTip="Serach"
                                                                runat="server" class="btn btn-danger btn-paddd pull-left" BackColor="#f1f1f1"
                                                                ImageUrl="~/images/search-29.png" Style="margin-left: -49px; padding: 0px;" />
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row table-responsive">
                                                <div style="overflow: auto; margin-top: 35px; height: 380px;">
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
                                                                <ItemStyle HorizontalAlign="Left" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="HH No." HeaderStyle-CssClass="GridHeaderClass">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblSno" runat="server" Text='<%#Eval("HHNo") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Child Name" HeaderStyle-CssClass="GridHeaderClass">
                                                                <ItemTemplate>
                                                                    <asp:Label runat="server" ID="lbtn_ProducerD" Text='<%#Eval("ChildName") %>' Style="text-decoration: none;"></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" />
                                                            </asp:TemplateField>
                                                            <asp:BoundField HeaderText="Father name" DataField="FathersName" HeaderStyle-CssClass="GridHeaderClass">
                                                                <ItemStyle HorizontalAlign="Left" />
                                                            </asp:BoundField>
                                                            <asp:TemplateField HeaderText="Contact" HeaderStyle-CssClass="GridHeaderClass">
                                                                <ItemTemplate>
                                                                    <asp:DropDownList ID="ddlStatus" OnSelectedIndexChanged="ddlStatus_SelectedIndexChanged"
                                                                        AutoPostBack="true" runat="server" class="form-control">
                                                                        <%-- <asp:ListItem Selected="True" Value="0">--Select--</asp:ListItem>
                                            <asp:ListItem Value="1">C-Contact </asp:ListItem>
                                              <asp:ListItem Value="2">F-Follow up</asp:ListItem>
                                            <asp:ListItem Value="3">I-Ineligible </asp:ListItem>
                                            <asp:ListItem Value="4">P-Pending Format 6</asp:ListItem>

                                            <asp:ListItem Value="5">E-Enrolled</asp:ListItem>--%>
                                                                    </asp:DropDownList>
                                                                    <asp:Label runat="server" Visible="false" ID="lbStatus" Text='<%#Eval("Status") %>'
                                                                        Style="text-decoration: none;"></asp:Label>
                                                                    <asp:Label runat="server" Visible="false" ID="lbStatusNew" Text='<%#Eval("Status") %>'
                                                                        Style="text-decoration: none;"></asp:Label>
                                                                    <asp:Label runat="server" Visible="false" ID="lblActivityDate" Text='<%#Eval("ActivityDate") %>'
                                                                        Style="text-decoration: none;"></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="TBorFC" HeaderStyle-CssClass="GridHeaderClass">
                                                                <ItemTemplate>
                                                                    <asp:RadioButtonList ID="rblTBFC" runat="server" RepeatDirection="Horizontal">
                                                                        <asp:ListItem Selected="True" Value="2">FC</asp:ListItem>
                                                                        <asp:ListItem Value="1">TB</asp:ListItem>
                                                                    </asp:RadioButtonList>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Edit" HeaderStyle-CssClass="GridHeaderClass">
                                                                <ItemTemplate>
                                                                    <asp:ImageButton ID="btnEditEnroll" ToolTip="Edit" OnClick="btnEditEnroll_Click"
                                                                        runat="server" class="btn btn-danger btn-paddd pull-right" BackColor="#f1f1f1"
                                                                        ImageUrl="~/images/edit.png" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField Visible="false" HeaderStyle-CssClass="GridHeaderClass">
                                                                <ItemTemplate>
                                                                    <asp:Label runat="server" ID="lbUniqueCode" Text='<%#Eval("UniqueCode") %>' Style="text-decoration: none;"></asp:Label>
                                                                    <asp:Label runat="server" ID="lblTBFC" Text='<%#Eval("TBorFC") %>' Style="text-decoration: none;"></asp:Label>
                                                                    <asp:Label runat="server" ID="lblDtdUniqid" Text='<%#Eval("GUIDDTDActivityID") %>'
                                                                        Style="text-decoration: none;"></asp:Label>
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
                                <cc1:ModalPopupExtender ID="MpexdrFollowup" runat="server" BackgroundCssClass="modalBg "
                                    CancelControlID="ImageButton6" PopupControlID="PnlFollowup" TargetControlID="HdnFollowup">
                                </cc1:ModalPopupExtender>
                                <cc1:ModalPopupExtender ID="ModalPopupExtender2" runat="server" TargetControlID="Hdn_model33"
                                    PopupControlID="pnlpopup43" CancelControlID="btnAddNew" BackgroundCssClass="modalBackground">
                                </cc1:ModalPopupExtender>
                                <asp:HiddenField ID="Hdn_model33" runat="server" />
                                <asp:Panel ID="pnlpopup43" runat="server" Style="display: none;">
                                    <div class=" modal-lg">
                                        <div class="modal-content">
                                            <div class="modal-header">
                                                <asp:ImageButton ID="btnAddNew" CssClass="btn btn-info pull-right" BackColor="#f5f5f5"
                                                    ToolTip="Add" ImageUrl="~/images/close-29.png" Style="margin-right: 5px; padding: 0px;"
                                                    runat="server" />
                                                <h4 class="modal-title">
                                                    D2D</h4>
                                            </div>
                                            <div class="row">
                                                <div class="row marg search-bg">
                                                    <div class="col-lg-12 col-md-12 col-sm-12 cpl-xs-12" runat="server">
                                                        <div class="col-lg-3 col-md-3 col-sm-3 cpl-xs-12">
                                                            <div class="form-group" style="margin-bottom: 2px;">
                                                                <label for="email" class="col-sm-3 padd linhei">
                                                                    Date:</label>
                                                                <div class="col-sm-9 padd">
                                                                    <asp:TextBox runat="server" ID="txtFdate" autocomplete="off" ondrop="return false;"
                                                                        class="form-control" onkeypress="return false;"></asp:TextBox>
                                                                    <ajax:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True" Format="dd/MM/yyyy"
                                                                        TargetControlID="txtFdate" PopupPosition="BottomRight">
                                                                    </ajax:CalendarExtender>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="col-lg-3 col-md-3 col-sm-3 cpl-xs-12">
                                                            <div class="form-group" style="margin-bottom: 7px;">
                                                                <div class="col-sm-10 padd">
                                                                    <asp:TextBox runat="server" ID="TxtToDate" autocomplete="off" ondrop="return false;"
                                                                        class="form-control" onkeypress="return false;"></asp:TextBox>
                                                                    <ajax:CalendarExtender ID="CalendarExtender2" runat="server" Enabled="True" Format="dd/MM/yyyy"
                                                                        TargetControlID="TxtToDate" PopupPosition="BottomRight">
                                                                    </ajax:CalendarExtender>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="col-lg-3 col-md-3 col-sm-3 cpl-xs-12">
                                                            <div class="form-group" style="margin-bottom: 7px;">
                                                                <div class="col-sm-10 padd">
                                                                    <asp:DropDownList ID="ddlSearchEnroll" OnSelectedIndexChanged="ddlSearchEnroll_SelectedIndexChanged"
                                                                        AutoPostBack="true" runat="server" class="form-control">
                                                                        <asp:ListItem Selected="True" Value="0">--Select--</asp:ListItem>
                                                                        <asp:ListItem Value="1">Follow Up</asp:ListItem>
                                                                        <asp:ListItem Value="2">Ineligible </asp:ListItem>
                                                                        <asp:ListItem Value="3">Enrolled</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="col-lg-3 col-md-3 col-sm-3 cpl-xs-12">
                                                            <div class="form-group" style="margin-bottom: 7px;">
                                                                <div class="col-sm-10 padd">
                                                                    <asp:DropDownList ID="ddlSubContact" OnSelectedIndexChanged="ddlSubContact_SelectedIndexChanged"
                                                                        AutoPostBack="true" runat="server" class="form-control">
                                                                    </asp:DropDownList>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-lg-12 col-md-12 col-sm-12 cpl-xs-12">
                                                        <div class="col-lg-4 col-md-4 col-sm-4 cpl-xs-12">
                                                            <div class="form-group" style="margin-bottom: 2px;">
                                                                <label for="email" class="col-sm-3 padd linhei">
                                                                    Search:</label>
                                                                <div class="col-sm-9 padd">
                                                                    <asp:DropDownList ID="ddlStatusSearch" runat="server" class="form-control">
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
                                                                    <asp:TextBox runat="server" ID="txtSearchNew" autocomplete="off" ondrop="return false;"
                                                                        class="form-control"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="col-lg-4 col-md-4  col-sm-4 cpl-xs-12">
                                                            <div class="col-lg-1 col-md-1 col-sm-2 cpl-xs-12 col-lg-offset-1 col-md-offset-1 col-sm-offset-1 col-xs-offset-0">
                                                                <asp:ImageButton ID="ImageButton9" OnClick="btnD2dSerachNew_Click" ToolTip="Serach"
                                                                    runat="server" class="btn btn-danger btn-paddd pull-left" BackColor="#f1f1f1"
                                                                    ImageUrl="~/images/search-29.png" Style="margin-left: -49px; padding: 0px;" />
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row table-responsive">
                                                <div style="overflow: auto; margin-top: 35px; height: 380px;">
                                                    <asp:GridView ID="Gv_DisplayNew" Width="100%" runat="server" OnRowDataBound="Gv_DisplayNew_RowDataBound"
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
                                                                    <asp:Label ID="lblUniqueCode1" runat="server" Text='<%#Eval("UniqueId") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="HH No." HeaderStyle-CssClass="GridHeaderClass">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblSno11" runat="server" Text='<%#Eval("HHNo") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Child Name" HeaderStyle-CssClass="GridHeaderClass">
                                                                <ItemTemplate>
                                                                    <asp:Label runat="server" ID="lbtn_ProducerD" Text='<%#Eval("ChildName") %>' Style="text-decoration: none;"></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" />
                                                            </asp:TemplateField>
                                                            <asp:BoundField HeaderText="Father name" DataField="FathersName" HeaderStyle-CssClass="GridHeaderClass">
                                                                <ItemStyle HorizontalAlign="Left" />
                                                            </asp:BoundField>
                                                            <asp:TemplateField HeaderText="Status" HeaderStyle-CssClass="GridHeaderClass">
                                                                <ItemTemplate>
                                                                    <asp:Label runat="server" ID="lbStatusNew" Text='<%#Eval("Status") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Undo" HeaderStyle-CssClass="GridHeaderClass">
                                                                <ItemTemplate>
                                                                    <asp:Button ID="btnEditUndo" ToolTip="Undo" Text="Undo" OnClick="btnEditUndo_Click"
                                                                        runat="server" class="btn btn-danger btn-paddd pull-right" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField Visible="false" HeaderStyle-CssClass="GridHeaderClass">
                                                                <ItemTemplate>
                                                                    <asp:Label runat="server" ID="lbUniqueCode11" Text='<%#Eval("UniqueCode") %>' Style="text-decoration: none;"></asp:Label>
                                                                    <asp:Label runat="server" ID="lblGUIDDTDMobileActivity" Text='<%#Eval("GUIDDTDMobileActivity") %>'
                                                                        Style="text-decoration: none;"></asp:Label>
                                                                    <asp:Label runat="server" ID="Label40" Text='<%#Eval("FollowUPID") %>' Style="text-decoration: none;"></asp:Label>
                                                                    <asp:Label runat="server" ID="lblActivityStatus1" Text='<%#Eval("ActivityStatus") %>'
                                                                        Style="text-decoration: none;"></asp:Label>
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
                                <asp:HiddenField ID="HdnFollowup" runat="server"></asp:HiddenField>
                                <asp:Panel CssClass="model-wid mod-posi" Style="display: none; height: auto; width: 45% !important;
                                    margin-top: -75.5px !important;" ID="PnlFollowup" runat="server">
                                    <div style="width: 100%; height: auto; background-color: #f1f1f1">
                                        <div class="modal-header" style="background-color: #ddd; color: White;">
                                            <h4 class="modal-title" style="forecolor: White">
                                                <asp:Label ID="lblStst" runat="server" ForeColor="Red" Font-Names="Verdana" Font-Size="11px"></asp:Label></h4>
                                            <asp:Label ID="lblEditActivtive" Visible="false" runat="server" ForeColor="Red" Font-Names="Verdana"
                                                Font-Size="11px"></asp:Label>
                                            <asp:Label ID="lblEditRow" Visible="false" runat="server" ForeColor="Red" Font-Names="Verdana"
                                                Font-Size="11px"></asp:Label>
                                            <asp:Label ID="lblGuID" Visible="false" runat="server" ForeColor="Red" Font-Names="Verdana"
                                                Font-Size="11px"></asp:Label>
                                            <asp:Label ID="lblEnrollId" Visible="false" runat="server" ForeColor="Red" Font-Names="Verdana"
                                                Font-Size="11px"></asp:Label>
                                            <asp:Label ID="lblRtbFc" runat="server" Visible="false" ForeColor="Red" Font-Names="Verdana"
                                                Font-Size="11px"></asp:Label>
                                            <asp:Label ID="lblD2dUniqeCode" Visible="false" runat="server" ForeColor="Red" Font-Names="Verdana"
                                                Font-Size="11px"></asp:Label>
                                        </div>
                                        <div class="modal-body">
                                            <asp:Label ID="Label19" runat="server" ForeColor="Red" Font-Names="Verdana" Font-Size="11px"></asp:Label>
                                            <div class="form-horizontal" role="form">
                                                <div id="dvidFollowp" runat="server">
                                                    <div class="form-group" id="divF1" runat="server">
                                                        <asp:Label ID="Label20" class="control-label col-sm-4 lab-text-left" runat="server"
                                                            Text="Govt. ID"></asp:Label>
                                                        <div class="col-sm-6">
                                                            <asp:TextBox runat="server" ID="txtGovtID" autocomplete="off" onkeypress="return onlyAlphabets(event,this);"
                                                                ondrop="return false;" class="form-control" MaxLength="14"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="form-group" id="divF2" runat="server">
                                                        <asp:Label ID="Label21" class="control-label col-sm-4 lab-text-left" runat="server"
                                                            Text="Samgra ID"></asp:Label>
                                                        <div class="col-sm-6">
                                                            <asp:TextBox runat="server" MaxLength="9" onkeypress="return onlyAlphabets(event,this);"
                                                                ID="txtSamgraID" autocomplete="off" ondrop="return false;" class="form-control"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="form-group" id="divF3" runat="server">
                                                        <asp:Label ID="Label22" class="control-label col-sm-4 lab-text-left" runat="server"
                                                            Text="Reasons"></asp:Label>
                                                        <div class="col-sm-6">
                                                            <asp:DropDownList ID="ddlFo" OnSelectedIndexChanged="ddlFo_SelectedIndexChanged"
                                                                AutoPostBack="true" runat="server" CssClass="form-control" Font-Names="Verdana"
                                                                Font-Size="11px">
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                    <div class="form-group" runat="server" id="divF4">
                                                        <asp:Label ID="Label23" class="control-label col-sm-4 lab-text-left" runat="server"
                                                            Text="Village"></asp:Label>
                                                        <div class="col-sm-6">
                                                            <asp:DropDownList ID="ddlOtherVillage" OnSelectedIndexChanged="ddlFOtherVillage_SelectedIndexChanged"
                                                                AutoPostBack="true" runat="server" CssClass="form-control" Font-Names="Verdana"
                                                                Font-Size="11px">
                                                                <asp:ListItem Selected="True" Value="0">--Select--</asp:ListItem>
                                                                <asp:ListItem Value="1">Same village </asp:ListItem>
                                                                <asp:ListItem Value="2">Other village</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                    <div class="form-group" runat="server" id="divF7">
                                                        <asp:Label ID="Label36" class="control-label col-sm-4 lab-text-left" runat="server"
                                                            Text="School"></asp:Label>
                                                        <div class="col-sm-6">
                                                            <asp:DropDownList ID="ddlSchool" runat="server" CssClass="form-control" Font-Names="Verdana"
                                                                Font-Size="11px">
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                    <div class="form-group" runat="server" id="divF5">
                                                        <asp:Label ID="lbldist" class="control-label col-sm-4 lab-text-left" runat="server"
                                                            Text="Other Village"></asp:Label>
                                                        <div class="col-sm-6">
                                                            <asp:TextBox ID="txtOtherVillage" autocomplete="off" ondrop="return false;" onkeypress="return onlyAlphabets(event,this);"
                                                                CssClass="form-control" runat="server" MaxLength="50" BorderStyle="None"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="form-group" runat="server" id="divF6">
                                                        <asp:Label ID="Label35" class="control-label col-sm-4 lab-text-left" runat="server"
                                                            Text="School"></asp:Label>
                                                        <div class="col-sm-6">
                                                            <asp:TextBox ID="txtOtherSchool" autocomplete="off" ondrop="return false;" onkeypress="return onlyAlphabets(event,this);"
                                                                CssClass="form-control" runat="server" MaxLength="50" BorderStyle="None"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div id="dvIngilible" runat="server">
                                                    <div id="DivI2" class="form-group" runat="server">
                                                        <asp:Label ID="Label24" class="control-label col-sm-4 lab-text-left" runat="server"
                                                            Text="Reasons"></asp:Label>
                                                        <div class="col-sm-6">
                                                            <asp:DropDownList ID="ddlIReasons" OnSelectedIndexChanged="ddlIReasons_SelectedIndexChanged"
                                                                AutoPostBack="true" runat="server" CssClass="form-control" Font-Names="Verdana"
                                                                Font-Size="11px">
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                    <div class="form-group" id="DivI10" runat="server">
                                                        <asp:Label ID="Label46" class="control-label col-sm-4 lab-text-left" runat="server"
                                                            Text="Govt. ID"></asp:Label>
                                                        <div class="col-sm-6">
                                                            <asp:TextBox runat="server" ID="txtIGovtID" autocomplete="off" onkeypress="return onlyAlphabets(event,this);"
                                                                ondrop="return false;" class="form-control" MaxLength="14"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="form-group" id="DivI11" runat="server">
                                                        <asp:Label ID="Label47" class="control-label col-sm-4 lab-text-left" runat="server"
                                                            Text="Samgra ID"></asp:Label>
                                                        <div class="col-sm-6">
                                                            <asp:TextBox runat="server" MaxLength="9" onkeypress="return onlyAlphabets(event,this);"
                                                                ID="txtSamgra" autocomplete="off" ondrop="return false;" class="form-control"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div id="DivI3" class="form-group" runat="server">
                                                        <asp:Label ID="Label25" class="control-label col-sm-4 lab-text-left" runat="server"
                                                            Text="Months"></asp:Label>
                                                        <div class="col-sm-6">
                                                            <asp:DropDownList ID="ddlMonth" runat="server" CssClass="form-control" Font-Names="Verdana"
                                                                Font-Size="11px">
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                    <div class="form-group" id="DivI4" runat="server">
                                                        <asp:Label ID="Label26" class="control-label col-sm-4 lab-text-left" runat="server"
                                                            Text="Birth Date:"></asp:Label>
                                                        <div class="col-sm-6">
                                                            <asp:TextBox runat="server" ID="txtBDate" autocomplete="off" ondrop="return false;"
                                                                class="form-control" onkeypress="return false;"></asp:TextBox>
                                                            <ajax:CalendarExtender ID="CalendarExftender1" runat="server" OnClientDateSelectionChanged="arrivaldatecheckB"
                                                                Enabled="True" Format="dd/MM/yyyy" TargetControlID="txtBDate" PopupPosition="BottomRight">
                                                            </ajax:CalendarExtender>
                                                        </div>
                                                    </div>
                                                    <div id="DivI5" class="form-group  clsD" runat="server">
                                                        <asp:Label ID="Label27" class="control-label col-sm-4 lab-text-left" runat="server"
                                                            Text="DOB proof"></asp:Label>
                                                        <div class="col-sm-6">
                                                            <asp:DropDownList ID="ddlDOproof" OnSelectedIndexChanged="ddlDOproof_SelectedIndexChanged"
                                                                AutoPostBack="true" runat="server" CssClass="form-control" Font-Names="Verdana"
                                                                Font-Size="11px">
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                    <div id="DivI6" class="form-group" runat="server">
                                                        <asp:Label ID="Label39" class="control-label col-sm-4 lab-text-left" runat="server"
                                                            Text="DOB proof"></asp:Label>
                                                        <div class="col-sm-6">
                                                            <asp:TextBox ID="txtOther" autocomplete="off" ondrop="return false;" onkeypress="return onlyAlphabets(event,this);"
                                                                CssClass="form-control" runat="server" MaxLength="50" BorderStyle="None"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div id="DivI7" class="form-group" runat="server">
                                                        <asp:Label ID="Label43" class="control-label col-sm-4 lab-text-left" runat="server"
                                                            Text="Migration Place"></asp:Label>
                                                        <div class="col-sm-6">
                                                            <asp:DropDownList ID="ddlMigration" runat="server" CssClass="form-control" Font-Names="Verdana"
                                                                Font-Size="11px">
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div id="dvEnrollment" runat="server">
                                                    <div id="DivE1" class="form-group" runat="server">
                                                        <asp:Label ID="Label29" class="control-label col-sm-4 lab-text-left" runat="server"
                                                            Text="Village"></asp:Label>
                                                        <div class="col-sm-6">
                                                            <asp:DropDownList ID="ddlEotherVillage" runat="server" OnSelectedIndexChanged="ddlEFOtherVillage_SelectedIndexChanged"
                                                                AutoPostBack="true" CssClass="form-control" Font-Names="Verdana" Font-Size="11px">
                                                                <asp:ListItem Selected="True" Value="0">--Select--</asp:ListItem>
                                                                <asp:ListItem Value="1">Same village </asp:ListItem>
                                                                <asp:ListItem Value="2">Other village</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                    <div class="form-group" runat="server" id="DivE2">
                                                        <asp:Label ID="Label28" class="control-label col-sm-4 lab-text-left" runat="server"
                                                            Text="School"></asp:Label>
                                                        <div class="col-sm-6">
                                                            <asp:DropDownList ID="ddlESchool" runat="server" CssClass="form-control" Font-Names="Verdana"
                                                                Font-Size="11px">
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                    <div class="form-group" runat="server" id="DivE3">
                                                        <asp:Label ID="Label37" class="control-label col-sm-4 lab-text-left" runat="server"
                                                            Text="Other Village"></asp:Label>
                                                        <div class="col-sm-6">
                                                            <asp:TextBox ID="txtEvillage" autocomplete="off" ondrop="return false;" onkeypress="return onlyAlphabets(event,this);"
                                                                CssClass="form-control" runat="server" MaxLength="50" BorderStyle="None"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="form-group" runat="server" id="DivE4">
                                                        <asp:Label ID="Label38" class="control-label col-sm-4 lab-text-left" runat="server"
                                                            Text="School"></asp:Label>
                                                        <div class="col-sm-6">
                                                            <asp:TextBox ID="txtSschool" autocomplete="off" ondrop="return false;" onkeypress="return onlyAlphabets(event,this);"
                                                                CssClass="form-control" runat="server" MaxLength="50" BorderStyle="None"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div id="DivE5" class="form-group" runat="server">
                                                        <asp:Label ID="Label30" class="control-label col-sm-4 lab-text-left" runat="server"
                                                            Text="Panding Form Status"></asp:Label>
                                                        <div class="col-sm-6">
                                                            <asp:DropDownList ID="ddlFromStatus" runat="server" CssClass="form-control" Font-Names="Verdana"
                                                                Font-Size="11px">
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                    <div class="form-group" id="DivE12" runat="server">
                                                        <asp:Label ID="Label44" class="control-label col-sm-4 lab-text-left" runat="server"
                                                            Text="Govt. ID"></asp:Label>
                                                        <div class="col-sm-6">
                                                            <asp:TextBox runat="server" ID="txtEGovtID" autocomplete="off" onkeypress="return onlyAlphabets(event,this);"
                                                                ondrop="return false;" class="form-control" MaxLength="14"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="form-group" id="DivE13" runat="server">
                                                        <asp:Label ID="Label45" class="control-label col-sm-4 lab-text-left" runat="server"
                                                            Text="Samgra ID"></asp:Label>
                                                        <div class="col-sm-6">
                                                            <asp:TextBox runat="server" MaxLength="9" onkeypress="return onlyAlphabets(event,this);"
                                                                ID="txtEsamgranID" autocomplete="off" ondrop="return false;" class="form-control"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div id="DivE6" class="form-group" runat="server">
                                                        <asp:Label ID="Label31" class="control-label col-sm-4 lab-text-left" runat="server"
                                                            Text="Enrollment Category"></asp:Label>
                                                        <div class="col-sm-6">
                                                            <asp:DropDownList ID="ddlCategory" OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged"
                                                                AutoPostBack="true" runat="server" CssClass="form-control" Font-Names="Verdana"
                                                                Font-Size="11px">
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                    <div id="DivE11" class="form-group" runat="server">
                                                        <asp:Label ID="Label42" class="control-label col-sm-4 lab-text-left" runat="server"
                                                            Text="Other"></asp:Label>
                                                        <div class="col-sm-6">
                                                            <asp:TextBox ID="txtEnrommentOther" autocomplete="off" ondrop="return false;" onkeypress="return onlyAlphabets(event,this);"
                                                                CssClass="form-control" runat="server" MaxLength="50" BorderStyle="None"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div id="DivE7" class="form-group" runat="server">
                                                        <asp:Label ID="Label32" class="control-label col-sm-4 lab-text-left" runat="server"
                                                            Text="Scholar No"></asp:Label>
                                                        <div class="col-sm-6">
                                                            <asp:TextBox ID="txtSchoolar" autocomplete="off" ondrop="return false;" onkeypress="return onlyAlphabets(event,this);"
                                                                CssClass="form-control" runat="server" MaxLength="5" BorderStyle="None"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div id="DivE8" class="form-group" runat="server">
                                                        <asp:Label ID="Label33" class="control-label col-sm-4 lab-text-left" runat="server"
                                                            Text="Class"></asp:Label>
                                                        <div class="col-sm-6">
                                                            <asp:DropDownList ID="ddlClass" runat="server" OnSelectedIndexChanged="ddlClass_SelectedIndexChanged"
                                                                AutoPostBack="true" CssClass="form-control" Font-Names="Verdana" Font-Size="11px">
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                    <div id="DivE10" class="form-group" runat="server">
                                                        <asp:Label ID="Label41" class="control-label col-sm-4 lab-text-left" runat="server"
                                                            Text="Class Other"></asp:Label>
                                                        <div class="col-sm-6">
                                                            <asp:TextBox ID="txtClassOther" autocomplete="off" ondrop="return false;" CssClass="form-control"
                                                                runat="server" MaxLength="50" BorderStyle="None"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div id="DivE9" class="form-group" runat="server">
                                                        <asp:Label ID="Label34" class="control-label col-sm-4 lab-text-left" runat="server"
                                                            Text="Enrollment Date"></asp:Label>
                                                        <div class="col-sm-6">
                                                            <asp:TextBox ID="txtErollmentDate" autocomplete="off" ondrop="return false;" CssClass="form-control"
                                                                runat="server" MaxLength="30" BorderStyle="None"></asp:TextBox>
                                                            <ajax:CalendarExtender ID="CalendarEdxtender1" OnClientDateSelectionChanged="arrivaldatecheck"
                                                                runat="server" Enabled="True" Format="dd/MM/yyyy" TargetControlID="txtErollmentDate"
                                                                PopupPosition="BottomRight">
                                                            </ajax:CalendarExtender>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="modal-footer">
                                                <asp:ImageButton ID="btnNewUserSave" OnClick="btnClose_Click" ImageUrl="~/images/save-29-1.png"
                                                    runat="server" ToolTip="Save" Style="float: none;" ValidationGroup="validatemanageuser">
                                                </asp:ImageButton>&nbsp;
                                                <asp:ImageButton ID="ImageButton6" ImageUrl="~/images/close-29.png" runat="server"
                                                    Text="Close" ToolTip="Close" Style="float: none;"></asp:ImageButton></div>
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
                                                            <label style="line-height: 20px; font-size: 12px; font-family: calibri;">
                                                                <asp:LinkButton ID="LinkButton1" OnClick="lnkListStaus_OnClick" runat="server">List Of Status</asp:LinkButton>
                                                            </label>
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
                                                            <asp:CheckBox ID="rblsupportfc" runat="server" />
                                                        </label>
                                                    </div>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                    <div class="col-lg-9 col-md-9 col-sm-7 col-xs-12" style="padding: 0px;">
                                        <div class="row">
                                            <div style="height: 74px; margin-bottom: 0px;">
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
                                            <div style="height: 60px; margin-bottom: 0px;">
                                                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="padding: 0px;">
                                                    <div class="col-lg-3 col-md-3 col-sm-6 col-xs-12">
                                                        <div class="checkbox">
                                                            <label style="line-height: 20px; font-size: 12px; font-family: calibri;">
                                                                <asp:TextBox ID="txtmainother" CssClass="form-control" autocomplete="off" ondrop="return false;"
                                                                    Style="height: 53px; width: 129%;" TextMode="MultiLine" runat="server"></asp:TextBox>
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
                PopupControlID="PnlDistrict" CancelControlID="CancelButton" TargetControlID="HdnFild7">
            </cc1:ModalPopupExtender>
            <asp:HiddenField ID="HdnFild7" runat="server"></asp:HiddenField>
            <asp:Panel CssClass="model-wid mod-posi" Style="display: none; height: auto; width: 45% !important;
                margin-top: 125px !important;" ID="PnlDistrict" runat="server">
                <div style="width: 100%; height: auto; background-color: #f1f1f1">
                    <div class="modal-header" style="background-color: #3ac0f2; color: White;">
                    </div>
                    <div class="modal-body">
                        <asp:Label ID="lblmsg" runat="server" ForeColor="Red" Font-Names="Verdana" Font-Size="11px"></asp:Label>
                        <div class="form-horizontal">
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
                    <asp:ImageButton ID="ImageButton8" CssClass="btn btn-info pull-right" OnClick="btnReset_Click"
                        BackColor="#f5f5f5" ToolTip="Add" ImageUrl="~/images/close-29.png" Style="margin-right: 5px;
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
