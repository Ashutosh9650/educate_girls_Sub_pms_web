<%@ Page Title="GKP School Master Update" Language="C#" MasterPageFile="~/Site.master"
    AutoEventWireup="true" CodeFile="GkpBOView.aspx.cs"
    Inherits="GkpBOView" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>


<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
        <style>
        .container-fluid {
            padding-right: 15px;
            padding-left: 15px;
            margin-right: auto;
            margin-left: auto;
        }

        .row {
            margin-right: -15px;
            margin-left: -15px;
        }

        label {
            display: inline-block;
            max-width: 100%;
            margin-bottom: 5px;
            font-weight: 600 !important;
            font-size: 16px !important;
        }

        .p-0 {
            padding-right: 0px;
            padding-left: 0px;
        }

        .form-group {
            margin-bottom: 15px;
            float: left;
            width: 100%;
        }

        .font-weight-bold {
            font-weight: bold
        }

        .btn.btn-outline-light {
            background-color: transparent;
            border: 1px solid #ddd;
            transition: 0.3s;
        }

            .btn.btn-outline-light:hover {
                background-color: #ddd;
                border: 1px solid #ccc;
                transition: 0.3s;
            }

        .disp-flex {
            display: flex;
            justify-content: space-between;
            gap: 15px;
            align-items: center;
        }

        .btm {
            font-weight: 600;
        }

        .table thead tr th {
            background-color: #eeeeee;
        }
        /* Tooltip Box Color */
        .tooltip.right .tooltip-inner {
            background-color: #c0392b;
            color: #fff;
        }

        /* Right Arrow Color */
        .tooltip.right .tooltip-arrow {
            border-right-color: #c0392b;
        }

        .paging span {
            background-color: #ed3237;
            padding: 5px 7px;
            color: #ffffff;
            border: 1px solid #ed3237;
        }

        .paging a {
            background-color: #E1E1E1;
            padding: 5px 7px;
            text-decoration: none;
            border: 1px solid #c1c1c1;
            color: #ed3237;
        }
    </style>
<style type="text/css">
    .ui-datepicker {
        z-index: 99 !important
    }
    /* #ui-datepicker-div
{
    z-index: 9999999;
}
    .ajax__calendar_container
    {
        z-index: 1045;
    }*/
    .padd {
        padding-left: 15px;
        padding-right: 15px;
    }

    .rows {
        margin-left: -15px;
        margin-right: -15px;
    }

    legend.scheduler-border {
        padding: 0px 10px;
    }

    fieldset.scheduler-border {
        padding: 10px 1.4em 10px 1.4em !important;
    }

    .d-none {
        display: none;
    }

    /*        TABS CSS */

    .nav-tabs {
        margin: 0 auto;
    }
    .modal-dialog {
    position: absolute;
    top: 40%;
    left: 40%;
    transform: translate(-50%, -50%);
    margin: 0;
}

        .nav-tabs > li {
            z-index: 2;
            float: none;
            display: inline-block;
        }

            .nav-tabs > li > a {
                padding: 16px 25px 12px;
                font-size: 14px;
                font-weight: 700;
                font-style: normal;
                text-transform: uppercase;
                color: #737c85;
                -webkit-border-radius: 0;
                -moz-border-radius: 0;
                border-radius: 0;
                border: none !important;
                border-bottom: 4px solid transparent !important;
            }

            .nav-tabs > li.active > a,
            .nav-tabs > li.active > a:hover,
            .nav-tabs > li.active > a:focus {
                background-color: transparent;
                border-bottom: 4px solid #ed3237 !important;
                color: #ed3237;
            }

    .nav > li > a:hover,
    .nav > li > a:focus {
        outline: 0;
    }

    .tab-content {
        padding-left: 0;
        padding-right: 0;
        border: none;
    }

    .tab-pane {
        padding: 12px 0;
        border-bottom: 1px solid #ecf0f1;
    }

    .nav > li > a:hover, .nav > li > a:focus {
        text-decoration: none;
        background-color: #ed3237;
        color: #fff;
    }

    .bridge {
        background-color: #ed3237;
        width: 20px;
        height: 20px;
        float: right;
        border-radius: 25px;
        display: flex;
        justify-content: center;
        align-items: center;
        color: #fff;
        font-size: 12px;
        margin-left: 5px;
    }

    .card_title {
        display: flex;
        justify-content: start;
        align-items: center;
        gap: 8px;
        font-weight: 600;
    }

    .red_circle {
        width: 10px;
        height: 10px;
        background-color: #ed3237;
        border-radius: 25px;
    }

    .profile_date i {
        color: #8BC34A;
    }

    .profile_date {
        background-color: #e7ffcc;
        border-left: 3px solid #8BC34A;
        padding: 6px 12px;
        font-weight: 500;
        border-radius: 4px;
        margin-bottom: 15px;
    }


    /*        Flip CARD CSS */

    /* ── Flip Card Shell ── */
    .flip-card {
        height: 465px;
        perspective: 900px;
        margin-bottom: 24px;
    }

    .flip-card-inner {
        width: 100%;
        height: 100%;
        position: relative;
        transform-style: preserve-3d;
        transition: transform 0.55s cubic-bezier(0.4, 0, 0.2, 1);
    }

    .flip-card.flipped .flip-card-inner {
        transform: rotateY(180deg);
    }

    /* ── Both Faces ── */
    .flip-front,
    .flip-back {
        position: absolute;
        inset: 0;
        border-radius: 12px;
        backface-visibility: hidden;
        -webkit-backface-visibility: hidden;
        display: flex;
        flex-direction: column;
        align-items: flex-start;
        justify-content: start;
        padding: 0px;
    }

    .flip-front {
        background: #ffffff;
        border: 1px solid #e3e2da;
        font-size:12px !important;  
    }

    .flip-back {
        transform: rotateY(180deg);
        font-size:13px !important;
    } 

    /* ── Front Typography ── */
    .flip-front .card-icon {
        font-size: 36px;
        margin-bottom: 10px;
        color: #888780;
    }

    .flip-front .card-title {
        font-family: 'DM Serif Display', serif;
        font-size: 17px;
        font-weight: 400;
        color: #1a1a18;
        margin: 0 0 4px;
    }

    .flip-front .card-sub {
        font-size: 12px;
        color: #b4b2a9;
        margin: 0 0 16px;
    }

    /* ── Back Typography ── */
    .flip-back .back-stat {
        font-family: 'DM Serif Display', serif;
        font-size: 40px;
        font-weight: 400;
        margin: 0 0 6px;
    }

    .flip-back .back-desc {
        font-size: 12px;
        line-height: 1.6;
        margin: 0 0 16px;
    }

    /* ── Buttons ── */
    .btn-flip {
        font-size: 12px;
        font-weight: 500;
        padding: 5px 16px;
        border-radius: 99px;
        border: 1px solid #c8c7be;
        background: transparent;
        color: #5f5e5a;
        cursor: pointer;
        transition: background 0.15s, color 0.15s;
    }

        .btn-flip:hover {
            background: #ebe9e3;
            color: #1a1a18;
        }

    .box {
        position: relative;
        width: 100%
    }

        .box::after {
            content: "";
            position: absolute;
            top: 0;
            right: 0;
            width: 100%;
            height: 100%;
            background: url(/images/flipcard_bg.svg) no-repeat top;
            background-size: cover;
            border-radius: 10px 10px 0px 0px;
        }

    .fip_card_flex {
        display: flex;
        justify-content: flex-start;
        align-items: flex-start;
        gap: 12px;
        color: #fff;
        margin: 10px 0px;
    }

    .flip_img {
        border: 2px solid #edc9c9;
        border-radius: 6px;
        height: 100px;
        display: flex;
        justify-content: center;
        align-items: center;
        width: 80px;
    }

    .flip_code {
        background-color: rgba(0, 0, 0, 0.3);
        padding: 3px 12px;
        border: 1px solid rgba(0, 0, 0, 0.3);
        border-radius: 6px;
        margin-bottom: 8px 0px 10px 0px;
    }

    .disp_between {
        display: flex;
        justify-content: space-between;
        align-items: center;
        gap: 12px;
        color: #d99495;
        margin-bottom: 12px;
    }

    ul.flip_ul {
        list-style: none;
        padding: 0px;
    }

    .flip_ul li {
        display: flex;
        justify-content: space-between;
        align-items: center;
        gap: 12px;
        padding: 8px 0px;
        border-bottom: 1px solid #ddd;
    }

        .flip_ul li span:nth-child(1) {
            color: #ababab;
            font-weight: 600;
        }

        .flip_ul li span:nth-last-child(1) {
            color: #5c5c5c;
            font-weight: 600;
        }

    .from_date {
        background-color: #991a1a;
        border-radius: 6px;
        padding: 10px;
        font-weight: bold;
        color: #fff;
        display: flex;
        justify-content: space-between;
        align-items: center;
    }

    .card_pending {
        background-color: #fff8e1;
        border: 1px solid #f9cc8a;
        border-radius: 4px;
        text-align: center;
        padding: 10px;
    }

    .card_approved {
        background-color: #f0fff4;
        border: 1px solid #9ef9b6;
        border-radius: 4px;
        text-align: center;
        padding: 10px;
    }

    .card_reject {
        background-color: #fff5f5;
        border: 1px solid #ffb3b3;
        border-radius: 4px;
        text-align: center;
        padding: 10px;
    }

    .btn-outline-danger {
        background-color: transparent;
        border: 1px solid #ed3237;
        color: #ed3237;
    }

        .btn-outline-danger:hover {
            background-color: #ed3237;
            border: 1px solid #ed3237;
            color: #ffffff;
        }

    .new_card {
        width: 100%;
        height: auto;
        float: left;
        border: 1px solid #ddd;
        border-radius: 4px;
        padding: 10px;
        margin-bottom:15px;
    }

    .new_card_between {
        display: flex;
        justify-content: space-between;
        align-items: center;
        gap: 15px;
    }

    .left_side {
        display: flex;
        justify-content: flex-start;
        gap: 10px;
        align-items: center;
    }

    .flex_dir {
        display: flex;
        justify-content: start;
        flex-direction: column;
    }

    .disp_flex {
        display: flex;
        justify-content: start;
        align-items: center;
        gap: 10px;
    }

     .disp_flexAl {
        display: flex;
        justify-content: start;
        align-items: center;
        color: #f8b119;
        gap: 10px;
        font-weight: 600;
    }



        .disp_flex .tb_id {
            border: 1px solid #ccc;
            background-color: #ddd;
            border-radius: 6px;
            padding: 1px 10px;
            font-size: 12px;
            font-weight: 600;
        }

        .disp_flex p {
            font-weight: bold;
        }

    .fc {
        color: #ababab;
    }

    .flex_dir_rs {
        display: flex;
        justify-content: end;
        flex-direction: column;
        align-items: end;
    }

    .pend {
        background-color: #fff0ba;
        border-radius: 25px;
        padding: 2px 15px;
        font-size: 12px;
        border: 1px solid #fbe69c;
    }

    .valid_rs {
        color: #ababab;
    }
</style>

       <script type="text/javascript">
           //$(document).ready(function () {
           //    LoadCards();
           //});
           function ShowAlerts(index) {
               var item = window.cardsData[index];
               var raw = (item && item.alert_messages) ? item.alert_messages : "";

               var list = raw.split("|")
                   .map(function (s) { return s.trim(); })
                   .filter(function (s) { return s.length > 0; });

               var html = "";
               $.each(list, function (i, msg) {
                   html += "<li style='margin-bottom:8px;'>" + msg + "</li>";
               });

               if (html === "") { html = "<li>No alerts</li>"; }

               $("#alertList").html(html);
               $("#alertModal").modal("show");
           }
           function LoadCounts() {

               var stateCode = $("#<%=ddlState.ClientID%>").val();
               var districtCode = $("#<%=ddlDistrict.ClientID%>").val();
               var blockCode = $("#<%=ddlBlock.ClientID%>").val();
               var panchayatCode = $("#<%=ddlPanchayat.ClientID%>").val();
               var villageCode = $("#<%=ddlschool.ClientID%>").val();

               $.ajax({
                   type: "POST",
                   url: "GkpBOView.aspx/GetCounts",
                   data: JSON.stringify({
                       stateCode: stateCode,
                       districtCode: districtCode,
                       blockCode: blockCode,
                       panchayatCode: panchayatCode,
                       villageCode: villageCode
                   }),
                   contentType: "application/json; charset=utf-8",
                   dataType: "json",
                   success: function (response) {
                       var c = response.d;               // { "0": HPA, "2": HA, "3": HR }
                       $("#HPAD").html(c["0"]);          // Pending Approval
                       $("#HATD").html(c["2"]);          // Approved
                       $("#HRD").html(c["3"]);           // Rejected
                   },
                   error: function (xhr) {
                       console.log(xhr.responseText);
                   }
               });
           }

           function LoadCards() {

               var stateCode = $("#<%=ddlState.ClientID%>").val();
            var districtCode = $("#<%=ddlDistrict.ClientID%>").val();
            var blockCode = $("#<%=ddlBlock.ClientID%>").val();
            var panchayatCode = $("#<%=ddlPanchayat.ClientID%>").val();
               var villageCode = $("#<%=ddlschool.ClientID%>").val();
               LoadCounts();
               
    $.ajax({
        type: "POST",
        url: "GkpBOView.aspx/GetCards",
        data: JSON.stringify({
            stateCode: stateCode,
            districtCode: districtCode,
            blockCode: blockCode,
            panchayatCode: panchayatCode,
            villageCode: villageCode
        }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {

            var cards = response.d;

            $("#ApprovalQueueCNT").text(cards.length);
            window.cardsData = cards;
            var html = "";

            $.each(cards, function (i, item) {
                console.log(item);
                //console.log($("#HPAD").length);
                //console.log($("#HATD").length);
                //console.log($("#HRD").length);
                //$("#HPAD").html(item.HPA);
                //$("#HRD").html(item.HR);
                //$("#HATD").html(item.HA);
                html += `
                <div class="new_card">
                    <div class="new_card_between">

                        <div class="left_side">
                            <input class="chkTBCode" type="checkbox" value="${item.SchoolCode}" />

                            <img src="./images/criteria__md-Icon.png" />

                            <div class="flex_dir">

                                <div class="disp_flex">
                                    <p style="margin:0">${item.Name}</p>
                                    <span class="tb_id">${item.disecode}</span>
                                </div>

                                <div class="disp_flex fc">
                                    <span> ${item.District}</span>
                                    <span> .${item.BlockName}</span>
                                    <span> .${item.Cluster}</span>
                                   <span> FC:${item.FCname}</span>
                                    <span>Submitted:${item.Status}</span>
                                   <span>Children:${item.TotalBasline}</span>
                                 
                                </div>
                                    
                                    <div class="disp_flexAl ">
                                  <span>Alert:${(item.alert_messages || "").split("|")[0].trim()}</span>
                                </div>

                            </div>
                        </div>

                        <div class="right_side">
                            <div class="flex_dir_rs">

                                <div class="pend">
    <button type="button"
        class="btn btn-primary btn-sm"
        onclick="ShowAlerts(${i})">
        <i class="fa fa-eye"></i>&nbsp;View Details
    </button>
</div>

                              

                            </div>
                        </div>

                    </div>

                   

                </div>`;
            });

            $("#divCards").html(html);
        },
        error: function (xhr) {
            console.log(xhr.responseText);
            alert("Error loading cards.");
        }
    });
}

           function ShowRejectPopup() {

               var cnt = $(".chkTBCode:checked").length;

               if (cnt == 0) {
                   alert("Please select at least one record.");
                   return;
               }

               $("#txtRejectRemark").val("");
               $("#rejectModal").modal("show");
           }

           function SaveRejection() {

               var remark = $("#txtRejectRemark").val();

               if ($.trim(remark) == "") {
                   alert("Please enter rejection reason.");
                   return;
               }

               UpdateApprovalStatusRe(3, remark);

               $("#rejectModal").modal("hide");
           }
           function UpdateApprovalStatusRe(status, remark) {

               var tbCodes = [];

               $(".chkTBCode:checked").each(function () {
                   tbCodes.push($(this).val());
               });

               // Check at least one selected
               if (tbCodes.length === 0) {
                   alert("Please select at least one  record.");
                   return false;
               }
               $.ajax({
                   type: "POST",
                   url: "GkpBOView.aspx/UpdateApprovalStatusRe",
                   data: JSON.stringify({
                       tbCodes: tbCodes,
                       status: status,
                       remark: remark || ""
                   }),
                   contentType: "application/json; charset=utf-8",
                   dataType: "json",
                   success: function (response) {

                       alert("Updated Successfully");
                       LoadCards();
                   }
               });
           }
           function UpdateApprovalStatus(status, remark) {

               var tbCodes = [];

               $(".chkTBCode:checked").each(function () {
                   tbCodes.push($(this).val());
               });
              
               // Check at least one selected
               if (tbCodes.length === 0) {
                   alert("Please select at least one  record.");
                   return false;
               }
               $.ajax({
                   type: "POST",
                   url: "GkpBOView.aspx/UpdateApprovalStatus",
                   data: JSON.stringify({
                       tbCodes: tbCodes,
                       status: status,
                       remark: remark || ""
                   }),
                   contentType: "application/json; charset=utf-8",
                   dataType: "json",
                   success: function (response) {

                       alert("Updated Successfully");
                       LoadCards();
                   }
               });
           }
           function GetActionButtons(item) {

               switch (parseInt(item.ApprovalStatus)) {

                   case 1:
                       return '<button type="button" class="btn btn-warning" disabled>' +
                           '<i class="fa fa-clock-o"></i>&nbsp;Pending for Approval</button>';

                   case 2:
                       return '<button type="button" class="btn btn-success" onclick="DownloadIdCard(\'' + item.TBCode + '\')">' +
                           '<i class="fa fa-download"></i>&nbsp;Download ID Card</button>';

                   case 3:
                       return '<button type="button" class="btn btn-danger" disabled>' +
                           '<i class="fa fa-times"></i>&nbsp;Approval Request Rejected</button>' +
                           '<div class="text-danger" style="margin-top:5px;">' +
                           (item.RejectionRemark || '') +
                           '</div>';

                   default:
                       return '<button type="button" class="btn btn-info" data-tbcode="' + item.TBCode + '" onclick="SubmitForApproval(this)">' +
                           '<i class="fa fa-floppy-o"></i>&nbsp;Submit for DPO Approval</button>';
               }
           }

           function SubmitForApproval(btn) {

               var tbCode = $(btn).data("tbcode");

               $.ajax({
                   type: "POST",
                   url: "frmTeamBalika.aspx/SubmitForApproval",
                   data: JSON.stringify({ tbCode: tbCode }),
                   contentType: "application/json; charset=utf-8",
                   dataType: "json",
                   success: function (response) {

                       if (response.d == "Success") {

                           $(btn)
                               .prop("disabled", true)
                               .removeClass("btn-info")
                               .addClass("btn-warning")
                               .val("Pending for Approval");
                           LoadCards();
                           alert("" + response.d + "");
                       }
                       else {
                           alert("" + response.d + "");
                       }
                   },
                   error: function () {
                       alert("Error while submitting.");
                   }
               });
           }
       </script>
    <script type="text/javascript">

        function UpdateHiddenField(ctrl) {

            var row = ctrl.closest('tr');

            var ddls = row.getElementsByTagName('select');

            var isSelected = false;

            for (var i = 0; i < ddls.length; i++) {

                if (ddls[i].value != "" &&
                    ddls[i].value != "0" &&
                    ddls[i].selectedIndex > 0) {

                    isSelected = true;
                    break;
                }
            }

            var hidden = row.querySelector("input[type='hidden']");

            if (hidden) {
                hidden.value = isSelected ? "1" : "0";
            }

        }




    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">

    <asp:UpdatePanel ID="MainPanel" runat="server">
        <ContentTemplate>
            <div class="container-fluid">
                <div class="row">
                    <div class="col-sm-12">
                        <div class="panel panel-default">
                            <div class="panel-heading" style="padding-left: 15px; padding-right: 15px;">
                                <div class="row">
                                    <div class="col-sm-12">

                                        <div class="disp-flex">

                                            <h3 class="text-danger font-weight-bold" style="margin: 0">Baseline  Approval</h3>

                                            
                                        </div>

                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="panel-body" style="padding-top: 0px">

                            <div class="row">
                                <div class="col-sm-12" style="padding: 0px">
                                    <div class="panel panel-default">
                                        <div class="panel-heading">
                                            <div class="row" style="padding-top: 15px">
                                                <div class="col-lg-3 col-md-3 col-sm-6" style="padding: 0px">
                                                    <div class="form-group">
                                                        <label class="col-sm-3">Year</label>
                                                        <div class="col-sm-9 ">
                                                            <asp:DropDownList ID="ddlYear" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddlYear_SelectedIndexChanged"
                                                                class="form-control ">
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="col-lg-3 col-md-3 col-sm-6" style="padding: 0px">
                                                    <div class="form-group">
                                                        <label class="col-sm-3">State</label>
                                                        <div class="col-sm-9">
                                                            <asp:DropDownList ID="ddlState" runat="server" OnSelectedIndexChanged="ddlState_SelectedIndexChanged"
                                                                AutoPostBack="true" class="form-control ">
                                                            </asp:DropDownList>

                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-lg-3 col-md-3 col-sm-6" style="padding: 0px">
                                                    <div class="form-group">
                                                        <label class="col-sm-3">Districts</label>
                                                        <div class="col-sm-9">
                                                            <asp:DropDownList ID="ddlDistrict" runat="server" OnSelectedIndexChanged="ddlDistrict_SelectedIndexChanged"
                                                                AutoPostBack="true" class="form-control " />
</div>
                                                    </div>
                                                </div>
                                                <div class="col-lg-3 col-md-3 col-sm-6" style="padding: 0px">
                                                    <div class="form-group">
                                                        <label class="col-sm-3">Block</label>
                                                        <div class="col-sm-9">
                                                            <asp:DropDownList ID="ddlBlock" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlBlock_SelectedIndexChanged"
                                                                class="form-control " />
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-lg-3 col-md-3 col-sm-6" style="padding: 0px">
                                                    <div class="form-group">
                                                        <label class="col-sm-3">Cluster</label>
                                                        <div class="col-sm-9">

                                                            <asp:DropDownList ID="ddlPanchayat" runat="server" OnSelectedIndexChanged="ddlPanchayat_SelectedIndexChanged" AutoPostBack="true"
                                                                class="form-control " />

                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-lg-3 col-md-3 col-sm-6" style="padding: 0px">
                                                    <div class="form-group">
                                                        <label class="col-sm-3">School</label>
                                                        <div class="col-sm-9">
                                                            <asp:DropDownList ID="ddlschool" runat="server"
                                                                class="form-control " />
                                                        </div>
                                                    </div>
                                                </div>


                                                <div class="col-lg-3 col-md-3 col-sm-6" style="padding: 0px">
                                                    <div class="form-group" style="display: flex; align-items: center; gap: 12px">

                                                        <asp:LinkButton ID="LinkButton1" OnClick="btnSerach_Click" CssClass="btn btn-sm btn-primary" runat="server" Text="Search">Search</asp:LinkButton>
                                               


                                                    </div>
                                                </div>

                                               


                                            </div>
                                        </div>
                                    </div>
                                </div>
                                  <div class="panel-body scroll" style="min-height: 404px; padding: 0px">
                                <div class="col-lg-12" style="padding-top: 10px; background-color: #fbfbfb;">

                                                <ul class="nav nav-tabs" role="tablist" id="myTab">

                                                   
                                                    <li id="liApprovalQueue" runat="server">
                                                        <a href="#tab3" role="tab" data-toggle="tab" >Approval Queue
           
                                                            <span id="ApprovalQueueCNT" class="bridge"></span>
                                                        </a>
                                                    </li>

                                                </ul>
                                    <div class="tab-content">
                              
                                <div runat="server" clientidmode="Static" class="tab-pane fade" id="tab3">
                                                        <div class="row">
                                                            <div class="row">
                                                                <div class="col-lg-4 col-md-4.col-sm-6 col-xs-12">
                                                                    <div class="card_pending">
                                                                        <h2  id="HPAD"  style="margin: 0; color: #f8b119; font-weight: 600;">-</h2>
                                                                        <small style="font-weight: 600; color: #ababab;">Pending Approval</small>
                                                                    </div>
                                                                </div>
                                                                <div class="col-lg-4 col-md-4.col-sm-6 col-xs-12">
                                                                    <div class="card_approved">
                                                                        <h2 id="HATD" style="margin: 0; color: #17a34a; font-weight: 600;">-</h2>
                                                                        <small style="font-weight: 600; color: #ababab;">Approval</small>
                                                                    </div>
                                                                </div>
                                                                <div class="col-lg-4 col-md-4.col-sm-6 col-xs-12">
                                                                    <div class="card_reject">
                                                                        <h2  id="HRD"  style="margin: 0; color: #ce0000; font-weight: 600;">-</h2>
                                                                        <small style="font-weight: 600; color: #ababab;">Rejected</small>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="col-sm-12" style="padding-top: 25px">
                                                                <div class="panel panel-default">
                                                                    <div class="panel-heading">
                                                                        <div class="disp_between" style="color: inherit; margin-bottom: 0px">
                                                                            <div class="card_title">
                                                                                <span class="red_circle"></span>
                                                                                <span>Personal Details</span>
                                                                            </div>
                                                                            <div>
                                                                                <button type="button"
                                                                                    class="btn btn-success"
                                                                                    onclick="UpdateApprovalStatus(2)">
                                                                                    Approve Selected
                                                                                </button>

                                                                                <button type="button"
                                                                                    class="btn btn-danger"
                                                                                    onclick="ShowRejectPopup()">
                                                                                    Reject Selected
                                                                                </button>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div id="divCards" class="panel-body">
                                                               

                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <!-- /.col -->
                                                    </div>
                                        </div>
                                    </div>
                                </div>

                            </div>
                        </div>
                    </div>
                </div>

                      <div class="modal fade" id="rejectModal" tabindex="-1">
    <div class="modal-dialog">
        <div class="modal-content">

            <div class="modal-header">
                <h4 class="modal-title">Reject Approval</h4>
            </div>

            <div class="modal-body">

                <textarea id="txtRejectRemark"
                    class="form-control"
                    rows="5"
                    placeholder="Enter rejection reason"></textarea>

            </div>

            <div class="modal-footer">

                <button type="button"
                    class="btn btn-secondary"
                    data-dismiss="modal">
                    Close
                </button>

                <button type="button"
                    class="btn btn-danger"
                    onclick="SaveRejection();">
                    Save Rejection
                </button>

            </div>

        </div>
    </div>
</div>


                <div class="modal fade" id="alertModal" tabindex="-1">
    <div class="modal-dialog">
        <div class="modal-content">

            <div class="modal-header">
                <h4 class="modal-title">Alerts</h4>
                <button type="button" class="close" data-dismiss="modal">&times;</button>
            </div>

            <div class="modal-body">
                <ol id="alertList" style="padding-left:18px; margin:0;"></ol>
            </div>

            <div class="modal-footer">
                <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
            </div>

        </div>
    </div>
</div>

             
        </ContentTemplate>
  
    </asp:UpdatePanel>
</asp:Content>




