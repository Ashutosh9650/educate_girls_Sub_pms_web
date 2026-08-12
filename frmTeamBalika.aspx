<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmTeamBalika.aspx.cs" Culture="en-GB" MasterPageFile="~/Site.master"
    Inherits="frmTeamBalika" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
 
    <link rel="stylesheet" href="//code.jquery.com/ui/1.13.2/themes/base/jquery-ui.css">
    <link rel="stylesheet" href="/resources/demos/style.css">
    <%--<script src="https://code.jquery.com/jquery-3.6.0.js"></script>--%>
    <script src="https://code.jquery.com/ui/1.13.2/jquery-ui.js"></script>
    <script type="text/javascript">
        function arrivaldatecheck(sender, args) {
            var depdate = 'dep';

            var departuredate = $('.' + depdate).val();
            var arrivaldate = sender._selectedDate;
            var today = new Date();




            if (sender._selectedDate > today) {
                alert("Should not be future date.");
                sender._textbox.set_Value("")

                return false;

            }

        }

    </script>
<script language="Javascript" type="text/javascript">
    $(document).ready(function () {
        $("[id$=txtJoingDate]").datepicker({ maxDate: new Date() });
        $("[id$=txtJoingDate]").datepicker({
            dateFormat: 'dd/mm/yy'
        });
        $("[id$=txtJoingDate]").datepicker();

        $("[id$=txtAlumniDate]").datepicker({
            dateFormat: 'dd/mm/yy',
            maxDate: new Date()
        });


        $("[id$=txtAlumniDate]").datepicker();
        $('#datepickers-container').css('z-index', 1045);
    });

</script>


<script type="text/javascript">
    function loadJSFunction() {
        $("[id$=txtJoingDate]").datepicker({
            dateFormat: 'dd/mm/yy',
            changeMonth: true,
            changeYear: true,
            minDate: '-60Y',
            yearRange: '1965:2026',
            defaultDate: new Date()

        });

        $("[id$=txtJoingDate]").datepicker();

        $("[id$=txtAlumniDate]").datepicker({
            dateFormat: 'dd/mm/yy',
            changeMonth: true,
            changeYear: true,
            minDate: '-60Y',
            yearRange: '1965:2026',
            defaultDate: new Date()
        });

        $("[id$=txtAlumniDate]").datepicker();


        $("[id$=txtDropDate]").datepicker({
            dateFormat: 'dd/mm/yy',
            changeMonth: true,
            changeYear: true,
            minDate: '-60Y',
            yearRange: '1965:2026',
            defaultDate: new Date()
        });

        $("[id$=txtDropDate]").datepicker();


        $("[id$=txtDate]").datepicker({
            dateFormat: 'dd/mm/yy',
            changeMonth: true,
            changeYear: true,
            minDate: '-60Y',
            yearRange: '1965:2026',
            defaultDate: new Date()
        });

        $("[id$=txtDate]").datepicker();






    }
</script>
<script language="Javascript" type="text/javascript">

    function onlyAlphabets(e, t) {
        try {
            if (window.event) {
                var charCode = window.event.keyCode;
            }
            else if (e) {
                var charCode = e.which;
            }
            else { return true; }
            if ((charCode > 64 && charCode < 91) || (charCode > 96 && charCode < 123) || charCode == 0 || charCode == 127 || charCode == 32 || charCode == 08 || charCode == 09 || charCode == 13)
                return true;
            else
                return false;
        }
        catch (err) {
            alert(err.Description);
        }
    }

</script>
<script language="Javascript" type="text/javascript">

    function onlyAlphabetsAdd(e, t) {
        try {
            if (window.event) {
                var charCode = window.event.keyCode;
            }
            else if (e) {
                var charCode = e.which;
            }
            else { return true; }
            if ((charCode > 64 && charCode < 91) || (charCode > 96 && charCode < 123) || charCode == 32 || charCode == 0 || charCode == 9 || charCode == 08 || charCode == 44 || charCode == 45 || charCode == 48)
                return true;
            else
                return false;
        }
        catch (err) {
            alert(err.Description);
        }
    }

</script>
<script type="text/javascript">


    function isNumberKey(txt, evt) {
        debugger;
        var charCode = (evt.which) ? evt.which : evt.keyCode;
        if (charCode == 46) {
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


    function phonenumber(inputtxt, txtid) {
        var phoneno = /^\d{10}$/;
        if (phoneno.test(inputtxt) && inputtxt.length == 10) {
            $("." + txtid).css("border", "solid 1px green")
            return true;
        }
        else {
            $("." + txtid).css("border", "solid 1px red")
            $("." + txtid).val('');
            alert("Mobile No. should be 10 digit");

            return false;
        }
    }

</script>
<script type="text/javascript">

    function Valdation(txtcls, txtaBoy) {
        var Eboy = 0;
        var Aboy = 0;
        $("." + txtcls).each(function (index, value) {
            if ($.trim($(this).val()) != "")
                if (!isNaN($(this).val()))

                    Eboy = parseFloat($("." + txtaBoy).val());
            Aboy = parseFloat($("." + txtcls).val());

            if (Aboy < Eboy) {

                alert("Enrollment  should be higher or equal to Appeared");
                $("." + txtcls).focus();
                $("." + txtaBoy).val('');
                return true;
            }
            else {
                return true;
            }

        });




    }
</script>

<script type="text/javascript">

    function calculate_totals(txtcls, txttotalcls) {
        var TotalCamt = 0;
        $("." + txtcls).each(function (index, value) {
            if ($.trim($(this).val()) != "")
                if (!isNaN($(this).val()))
                    TotalCamt = TotalCamt + parseFloat($(this).val());
        });
        $("." + txttotalcls).val(TotalCamt);
        return false;
    }

    function arrivaldate(arrivaldate) {

        var arrivaldate = $('#' + arrivaldate).val();

        var today = new Date();
        alert(arrivaldate);
        alert(today.getDate());
        if (arrivaldate > today.getDate()) {
            alert("Should not be future date.");
            document.getElementById("" + sender + "").value = null;
            return false;
        }


    }

    function checkDate(arrivaldate) {
        var EnteredDate = $('#' + arrivaldate).val();

        var date = EnteredDate.substring(0, 2);

        var month = EnteredDate.substring(3, 5);
        var year = EnteredDate.substring(6, 10);

        var myDate = new Date(year, month - 1, date);

        var today = new Date();

        if (myDate > today) {
            alert("Should not be future date.");
            $('#' + arrivaldate).val = '';
        }

    }




    //$type = Sys.UI.Point = function Point(x, y) {
    //    /// <summary locid="M:J#Sys.UI.Point.#ctor"></summary>
    //    /// <param name="x" type="Number" integer="true"></param>
    //    /// <param name="y" type="Number" integer="true"></param>
    //    /// <field name="x" type="Number" integer="true" locid="F:J#Sys.UI.Point.x"></field>
    //    /// <field name="y" type="Number" integer="true" locid="F:J#Sys.UI.Point.y"></field>
    //    var e = Function._validateParams(arguments, [
    //        { name: "x", type: Number, integer: true },
    //        { name: "y", type: Number, integer: true }
    //    ]);
    //    if (e) throw e;
    //    this.x = x;
    //    this.y = y;
    //}


    //function DomElement$getLocation(element)
    //var ex, ownerDoc = element.ownerDocument, documentElement = ownerDoc.documentElement,
    //    offsetX = Math.round(clientRect.left) + (documentElement.scrollLeft || (ownerDoc.body ? ownerDoc.body.scrollLeft : 0)),
    //    offsetY = Math.round(clientRect.top) + (documentElement.scrollTop || (ownerDoc.body ? ownerDoc.body.scrollTop : 0));

    //return new Sys.UI.Point(offsetX, offsetY);
</script>

<script type="text/javascript">
    function showFront(cardId) {
        document.getElementById(cardId).classList.remove("flipped");
    }

    function showBack(cardId) {
        document.getElementById(cardId).classList.add("flipped");
    }
</script>
<%-- <script type="text/javascript" language="javascript">
    //There's a bug in Microsoft's Ajax script that stops the modal popups from working
    //This overrides the the code that causes the error
    if (typeof (Sys) !== 'undefined') {
        Sys.UI.Point = function Sys$UI$Point(x, y) {

            x = Math.round(x);
            y = Math.round(y);

            var e = Function._validateParams(arguments, [
                { name: "x", type: Number, integer: true },
                { name: "y", type: Number, integer: true }
            ]);
            if (e) throw e;
            this.x = x;
            this.y = y;
        }
    }
</script>--%>
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
     .modal-dialog {
    position: absolute;
    top: 40%;
    left: 40%;
    transform: translate(-50%, -50%);
    margin: 0;
}
    .valid_rs {
        color: #ababab;
    }
</style>
       <%-- Gaurav Function--%>
    <script type="text/javascript">
        $(document).ready(function () {
            LoadCards();
        });
        function LoadCounts() {

            var stateCode = $("#<%=ddlState.ClientID%>").val();
             var districtCode = $("#<%=ddlDistrict.ClientID%>").val();
             var blockCode = $("#<%=ddlBlock.ClientID%>").val();
               var panchayatCode = $("#<%=ddlPanchayat.ClientID%>").val();
             var villageCode = $("#<%=ddlVillage.ClientID%>").val();

             $.ajax({
                 type: "POST",
                 url: "frmTeamBalika.aspx/GetCounts",
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
                     $("#HPAD").html(c["1"]);          // Pending Approval
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
           var villageCode = $("#<%=ddlVillage.ClientID%>").val();
            LoadCounts();
    $.ajax({
        type: "POST",
        url: "frmTeamBalika.aspx/GetCards",
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

            var html = "";

            $.each(cards, function (i, item) {
                console.log(item);
                console.log($("#HPAD").length);
                console.log($("#HATD").length);
                console.log($("#HRD").length);
                //$("#HPAD").html(item.HPA);
                //$("#HRD").html(item.HR);
                //$("#HATD").html(item.HA);
                html += `
                <div class="new_card">
                    <div class="new_card_between">

                        <div class="left_side">
                            <input class="chkTBCode" type="checkbox" value="${item.TBCode}" />

                            <img src="./images/criteria__md-Icon.png" />

                            <div class="flex_dir">

                                <div class="disp_flex">
                                    <p style="margin:0">${item.Name}</p>
                                    <span class="tb_id">${item.TBCode}</span>
                                </div>

                                <div class="disp_flex fc">
                                    <span>${item.District}</span>
                                    <span>${item.Village}</span>
                                    <span>${item.Cluster}</span>
                                    <span>Requested: ${item.RequestedDate}</span>
                                </div>

                            </div>
                        </div>

                        <div class="right_side">
                            <div class="flex_dir_rs">

                                <div class="pend">
    <button type="button"
        class="btn btn-primary btn-sm"
        onclick="ShowTBDetails('${item.UniqueCode}')">
        <i class="fa fa-eye"></i>&nbsp;View Details
    </button>
</div>

                                <div class="valid_rs">
                                    <span>Valid: ${item.ValidFrom}</span>
                                    <i class="fa fa-long-arrow-right"></i>
                                    <span>${item.ValidTo}</span>
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
function ShowTBDetails(tbCode) {

    document.getElementById('<%= hdnTBCode.ClientID %>').value = tbCode;

            document.getElementById('<%= btnViewDetails.ClientID %>').click();
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

            UpdateApprovalStatus(3, remark);

            $("#rejectModal").modal("hide");
        }

        function UpdateApprovalStatus(status, remark) {

            var tbCodes = [];

            $(".chkTBCode:checked").each(function () {
                tbCodes.push($(this).val());
            });
            // Check at least one selected
            if (tbCodes.length === 0) {
                alert("Please select at least one Team Balika record.");
                return false;
            }
            $.ajax({
                type: "POST",
                url: "frmTeamBalika.aspx/UpdateApprovalStatus",
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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">


    <asp:UpdatePanel runat="server" ID="mkiiainpnl121">
        <ContentTemplate>

            <asp:HiddenField ID="hdnActiveTab" runat="server" Value="#Profile Details" />
            <asp:HiddenField ID="hdnTBCode" runat="server" />

            <asp:Button ID="btnViewDetails"
                runat="server"
                Style="display: none"
                OnClick="btnViewDetails_Click" />

            <%-- <script type="text/javascript"> 

        $(document).ready(function () {

            // Initialize from storage or default to Member tab
            var activeTab = localStorage.getItem('activeTab') || '#Profile Details';
            $('.nav-tabs a[href="' + activeTab + '"]').tab('show');

            // Update storage when tab changes
            $('a[data-toggle="tab"]').on('shown.bs.tab', function (e) {
                var tabHref = $(e.target).attr('href');
                localStorage.setItem('activeTab', tabHref);
                $('#<%= hdnActiveTab.ClientID %>').val(tabHref);

                // Store current tab in all dropdowns and buttons
                $('.form-control, .btn').data('current-tab', tabHref);
            });

            // Store current tab when interacting with controls
            $('.form-control, .btn').on('focus click', function () {
                var currentTab = $('.nav-tabs li.active a').attr('href');
                $(this).data('current-tab', currentTab);
                $('#<%= hdnActiveTab.ClientID %>').val(currentTab);
            localStorage.setItem('activeTab', currentTab);
        });
    });

    // Handle ASP.NET partial postbacks
    var prm = Sys.WebForms.PageRequestManager.getInstance();
    prm.add_endRequest(function() {
        var activeTab = localStorage.getItem('activeTab') || '#Profile Details';
        $('.nav-tabs a[href="' + activeTab + '"]').tab('show');
        
        $('a[data-toggle="tab"]').off('shown.bs.tab').on('shown.bs.tab', function(e) {
            var tabHref = $(e.target).attr('href');
            localStorage.setItem('activeTab', tabHref);
            $('#<%= hdnActiveTab.ClientID %>').val(tabHref);
        });
    });
        function StoreCurrentTab(element) {
            var currentTab = $('.nav-tabs li.active a').attr('href');
            localStorage.setItem('activeTab', currentTab);
            $('#<%= hdnActiveTab.ClientID %>').val(currentTab);
            __doPostBack(element.name, '');
        }
     
    </script>--%>

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

            <div class="container-fluid">
                <%--<input type="image" id="ton-new" class="butt" src="Images/close.png"  />
       <div id="div-show-new"></div> --%>
            </div>

            <div class="container-fluid" style="margin-top: 0px;">
                <div class="row">
                    <div class="col-lg-2 col-md-2 col-sm-3" style="padding-right: 0px;">
                        <div class="thumbnail" style="min-height: 904px; width: 228px;">
                            <div style="padding-top: 0px;">
                                <%--<span style="float:left"> <asp:Label ID="lblsearch" runat="server" Text="Search:" ForeColor="Black"></asp:Label></span>--%>
                                <span style="float: right; padding-right: 1px;">
                                    <asp:TextBox ID="txtSearchName" Visible="false" runat="server" OnTextChanged="txtSearchName_Click" AutoPostBack="true" CssClass="form-control col-lg-1"></asp:TextBox></span>
                            </div>
                            <div style="overflow: auto; margin-top: 0px; height: 815px;">
                                <asp:GridView ID="GVMain" runat="server" Width="100%" AllowPaging="true" PageSize="40"
                                    BorderStyle="None" DataKeyNames="UniqueCode" GridLines="None" AutoGenerateColumns="false"
                                    OnRowCommand="GVMain_OnRowCommand" OnPageIndexChanging="GV_Project_PageIndexChanging" CssClass="table table-striped">
                                    <EmptyDataTemplate>
                                        <div style="font-family: Arial; font-size: 12px; font-weight: bold; color: Red;">
                                            Data not found
                                        </div>
                                    </EmptyDataTemplate>
                                    <FooterStyle CssClass="FooterStyle" />
                                    <HeaderStyle BackColor="#C1C1C1" ForeColor="White" Height="44px" />
                                    <RowStyle HorizontalAlign="Left" />
                                    <PagerStyle CssClass="paging" />
                                    <SelectedRowStyle BackColor="#fad669" Font-Bold="True" ForeColor="WhiteSmoke" />
                                    <AlternatingRowStyle BackColor="#f1f1f1" />
                                    <%-- <SelectedRowStyle BackColor="#fad669" Font-Bold="True" ForeColor="WhiteSmoke" />
                    <AlternatingRowStyle BackColor="#f1f1f1" />--%>
                                    <Columns>
                                        <asp:ButtonField HeaderText="Code " ItemStyle-ForeColor="#333" DataTextField="TBCode"
                                            CommandName="GVUIO">
                                            <ItemStyle CssClass="padding-lef" Height="30px" />
                                            <HeaderStyle CssClass="padding-lef" />
                                        </asp:ButtonField>
                                        <asp:ButtonField HeaderText="Name " ItemStyle-ForeColor="#333" DataTextField="TBName"
                                            CommandName="GVUIO">
                                            <ItemStyle CssClass="padding-lef" Height="30px" />
                                            <HeaderStyle CssClass="padding-lef" />
                                        </asp:ButtonField>
                                        <asp:ButtonField HeaderText="Name" Visible="false" Text="Button" DataTextField="UniqueCode"></asp:ButtonField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                    <div class="col-lg-10 col-md-10 col-sm-9">
                        <div class="row">
                            <div class="col-lg-12">
                                <div class="panel panel-default">
                                    <div class="panel-heading" style="padding: 5px;">
                                        <div class="row">
                                            <div class="col-lg-6 col-md-6 col-sm-6">
                                                <h3 class="text-danger" style="margin: 0px;">Team Balika Profile Management</h3>
                                            </div>
                                            <div class="col-lg-6 col-md-6 col-sm-6 ">
                                                <%-- <input type="image" id="" class="butt" src="Images/search-not-29.png" title="Search" />--%>

                                                <button type="button" id="ton-new" class="btn btn-primary" style="float: right; position: relative; right: 1px; color: #fff; background-color: #337ab7; border-color: #2e6da4;">
                                                    <i class="fa fa-bars"></i>
                                                </button>
                                                <asp:ImageButton ID="btnDelete" OnClick="btnDelete_Click" CssClass="btn btn-info pull-right"
                                                    ToolTip="Delete" BackColor="#f5f5f5" ImageUrl="~/images/delete-29.png" Style="margin-right: 5px; padding: 0px;"
                                                    runat="server" />
                                                <asp:ImageButton ID="btnsave" CssClass="btn btn-info pull-right" BackColor="#f5f5f5"
                                                    ToolTip="Save" ImageUrl="~/images/save-29-1.png" OnClick="btnsave_Click" ValidationGroup="saves"
                                                    Style="margin-right: 5px; padding: 0px;" runat="server" />
                                                <asp:ImageButton ID="btnAdd"  CssClass="btn btn-info pull-right" BackColor="#f5f5f5"
                                                    ToolTip="Add" ImageUrl="~/images/add-29-1.png" OnClick="btnAdd_Click" Style="margin-right: 5px; padding: 0px;"
                                                    runat="server" />


                                               


                                            </div>
                                        </div>
                                    </div>
                                    <div>
                                    </div>
                                    <div class="form-horizontal">
                                        <div class="row">
                                            <div id="div-show-new" style="text-align: left; width: calc(100% - 30px); right: 15px;">
                                                <div class="row marg search-bg" style="padding-top: 15px;">
                                                    <div class="form-horizontal">
                                                        <%-- <asp:UpdatePanel runat="server" ID="UpMain">
        <ContentTemplate>--%>

                                                        <div class="col-lg-3 col-md-3 col-sm-3 col-xs-12">
                                                            <div class="form-group" style="margin-bottom: 7px;">
                                                                <label for="email" class="col-sm-3 padd linhei">
                                                                    Year:</label>
                                                                <div class="col-sm-9 padd">
                                                                    <asp:DropDownList ID="ddlYear" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddlYear_SelectedIndexChanged" class="form-control ">
                                                                    </asp:DropDownList>

                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="col-lg-3 col-md-3 col-sm-3 cpl-xs-12">
                                                            <div class="form-group">
                                                                <label for="email" class="col-sm-3 padd linhei" style="padding-top: 2px;">
                                                                    State:</label>
                                                                <div class="col-sm-9 padd">
                                                                    <asp:DropDownList ID="ddlState" runat="server" OnSelectedIndexChanged="ddlState_SelectedIndexChanged"
                                                                        AutoPostBack="true" class="form-control ">
                                                                    </asp:DropDownList>

                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="col-lg-3 col-md-3 col-sm-3 cpl-xs-12">
                                                            <div class="form-group">
                                                                <label for="email" class="col-sm-3 padd linhei" style="padding-top: 2px;">
                                                                    District:</label>
                                                                <div class="col-sm-9 padd">
                                                                    <asp:DropDownList ID="ddlDistrict" runat="server" OnSelectedIndexChanged="ddlDistrict_SelectedIndexChanged"
                                                                        AutoPostBack="true" class="form-control " />
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="col-lg-3 col-md-3 col-sm-3 cpl-xs-12">
                                                            <div class="form-group">
                                                                <label for="email" class="col-sm-3 padd linhei" style="padding-top: 2px;">
                                                                    Block:</label>
                                                                <div class="col-sm-9 padd">
                                                                    <asp:DropDownList ID="ddlBlock" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlBlock_SelectedIndexChanged"
                                                                        class="form-control " />
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="col-lg-3 col-md-3 col-sm-3 cpl-xs-12">
                                                            <div class="form-group">
                                                                <label for="email" class="col-sm-3 padd linhei" style="padding-top: 2px;">
                                                                    Panchayat:</label>
                                                                <div class="col-sm-9 padd">
                                                                    <asp:DropDownList ID="ddlPanchayat" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlPanchayat_SelectedIndexChanged"
                                                                        class="form-control " />
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="col-lg-3 col-md-3 col-sm-3 cpl-xs-12">
                                                            <div class="form-group">
                                                                <label for="email" class="col-sm-3 padd linhei" style="padding-top: 2px;">
                                                                    Village:</label>
                                                                <div class="col-sm-9 padd">
                                                                    <asp:DropDownList ID="ddlVillage" OnSelectedIndexChanged="ddlVillage_SelectedIndexChanged"
                                                                        AutoPostBack="true" runat="server" class="form-control " />
                                                                    <span style="width: 5px; float: right; margin: -27px 27px; font-size: 21px;">
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator18" runat="server" Display="Dynamic"
                                                                            ValidationGroup="saves" ControlToValidate="ddlVillage" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                                                                    </span>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <%--</ContentTemplate>
</asp:UpdatePanel>--%>
                                                        <div class="col-lg-3 col-md-3 col-sm-3 cpl-xs-12">
                                                            <asp:ImageButton ID="btnSerach" ToolTip="Serach" runat="server" class="btn btn-danger btn-paddd"
                                                                BackColor="#f1f1f1" OnClick="btnSerach_Click" ImageUrl="~/images/search-29.png" />
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-lg-12" style="padding-top: 10px; background-color: #fbfbfb;">

                                                <ul class="nav nav-tabs" role="tablist" id="myTab">

                                                    <li id="liProfile" runat="server">
                                                        <a href="#tab1" role="tab" data-toggle="tab">Profile Details</a>
                                                    </li>

                                                    <li id="liIdCard" runat="server" class="active">
                                                        <a href="#tab2" role="tab" data-toggle="tab">ID Card</a>
                                                    </li>

                                                    <li id="liApprovalQueue" runat="server">
                                                        <a href="#tab3" role="tab" data-toggle="tab">Approval Queue
           
                                                            <span id="ApprovalQueueCNT" class="bridge"></span>
                                                        </a>
                                                    </li>

                                                </ul>

                                                <div class="tab-content">

                                                    <div runat="server" clientidmode="Static" class="tab-pane fade active in" id="tab1">
                                                        <div class="row" style="margin-left: -15px; margin-right: -15px;">
                                                            <asp:Panel ID="pnlMain" Enabled="false" runat="server">

                                                                <div class="col-md-6 col-sm-12">
                                                                    <div class="panel panel-default">
                                                                        <div class="panel-heading" style="padding-left: 12px; padding-right: 12px;">
                                                                            <div class="card_title">
                                                                                <span class="red_circle"></span>
                                                                                <span>Personal Details</span>
                                                                            </div>
                                                                        </div>
                                                                        <div class="panel-body" style="min-height: 660px;">
                                                                            <div class="form-group">
                                                                                <label class="control-label col-sm-4" for="Name">
                                                                                    TB Code</label>
                                                                                <div class="col-sm-8">
                                                                                    <asp:TextBox ID="txtIDNO" Enabled="false" runat="server" class="form-control" />
                                                                                </div>
                                                                            </div>
                                                                            <div class="form-group">
                                                                                <label class="control-label col-sm-4" for="Name">
                                                                                    Name of Team Balika <span style="color:red">*</span></label>
                                                                                <div class="col-sm-8">
                                                                                    <asp:TextBox ID="txtName" MaxLength="30" autocomplete="off" ondrop="return false;"
                                                                                        onkeypress="return onlyAlphabets(event,this);" runat="server" class="form-control" />
                                                                                    <span class="reqfield">
                                                                                        <asp:RequiredFieldValidator ID="rvtxtSchoolName" runat="server" Display="Dynamic"
                                                                                            ValidationGroup="saves" ControlToValidate="txtName" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                                                                                    </span>
                                                                                </div>
                                                                            </div>
                                                                            <div class="form-group">
                                                                                <label class="control-label col-sm-4" for="Name">
                                                                                    Contact Number <span style="color:red">*</span></label>
                                                                                <div class="col-sm-8">
                                                                                    <asp:TextBox ID="txtContact" OnKeyUp="javascript:inputtxt();" runat="server" MaxLength="10"
                                                                                        onkeypress="return isNumberKey(this,event);" onchange="javascript: phonenumber(this.value,'TeContact1');"
                                                                                        autocomplete="off" ondrop="return false;" class="form-control TeContact1 " />
                                                                                    <span class="reqfield">
                                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="Dynamic"
                                                                                            ValidationGroup="saves" ControlToValidate="txtContact" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                                                                                </div>
                                                                            </div>

                                                                            <div class="form-group">
                                                                                <label class="control-label col-sm-4" for="Name">
                                                                                    Alternate Mobile Number</label>
                                                                                <div class="col-sm-8">
                                                                                    <asp:TextBox ID="txtxAlternate" OnKeyUp="javascript:inputtxt();" runat="server" MaxLength="10"
                                                                                        onkeypress="return isNumberKey(this,event);" onchange="javascript: phonenumber(this.value,'TeContact2');"
                                                                                        autocomplete="off" ondrop="return false;" class="form-control TeContact2 " />
                                                                                </div>
                                                                            </div>

                                                                            <div class="form-group">
                                                                                <label class="control-label col-sm-4" for="Name">
                                                                                    TB have Smartphone <span style="color:red">*</span></label>
                                                                                <div class="col-sm-8">
                                                                                    <asp:DropDownList ID="ddlSmart" runat="server" class="form-control">
                                                                                        <asp:ListItem Selected="True" Value="0">--Select--</asp:ListItem>
                                                                                        <asp:ListItem Value="1">1-Yes </asp:ListItem>
                                                                                        <asp:ListItem Value="2">2-No</asp:ListItem>
                                                                                    </asp:DropDownList>
                                                                                    <span style="width: 5px; float: right; margin: -27px 27px; font-size: 21px;">
                                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator8" InitialValue="0" runat="server"
                                                                                            Display="Dynamic" ValidationGroup="saves" ControlToValidate="ddlSmart" ErrorMessage="*"
                                                                                            ForeColor="Red"></asp:RequiredFieldValidator>
                                                                                    </span>
                                                                                </div>
                                                                            </div>

                                                                            <div class="form-group">
                                                                                <label class="control-label col-sm-4" for="Name">
                                                                                    Father Name</label>
                                                                                <div class="col-sm-8">
                                                                                    <asp:TextBox ID="txtFatherName" onkeypress="return onlyAlphabets(event,this);" runat="server"
                                                                                        MaxLength="30" class="form-control" />

                                                                                </div>
                                                                            </div>
                                                                            <div class="form-group">
                                                                                <label class="control-label col-sm-4" for="Name">
                                                                                    Mother Name</label>
                                                                                <div class="col-sm-8">
                                                                                    <asp:TextBox ID="txtMotherName" onkeypress="return onlyAlphabets(event,this);" runat="server"
                                                                                        MaxLength="30" class="form-control" />

                                                                                </div>
                                                                            </div>
                                                                            <div class="form-group">
                                                                                <label class="control-label col-sm-4" for="Name">
                                                                                    Social Category <span style="color:red">*</span></label>
                                                                                <div class="col-sm-8">
                                                                                    <asp:DropDownList ID="ddlCategory" runat="server" class="form-control">
                                                                                    </asp:DropDownList>
                                                                                    <span style="width: 5px; float: right; margin: -27px 27px; font-size: 21px;">
                                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" InitialValue="0" runat="server"
                                                                                            Display="Dynamic" ValidationGroup="saves" ControlToValidate="ddlCategory" ErrorMessage="*"
                                                                                            ForeColor="Red"></asp:RequiredFieldValidator>
                                                                                    </span>
                                                                                </div>
                                                                            </div>
                                                                            <div class="form-group">
                                                                                <label class="control-label col-sm-4" for="Name">
                                                                                    Gender <span style="color:red">*</span></label>
                                                                                <div class="col-sm-8">
                                                                                    <asp:DropDownList ID="ddlGender" runat="server" class="form-control">
                                                                                        <asp:ListItem Selected="True" Value="0">--Select--</asp:ListItem>
                                                                                        <asp:ListItem Value="1">1-Male </asp:ListItem>
                                                                                        <asp:ListItem Value="2">2-Female</asp:ListItem>
                                                                                    </asp:DropDownList>
                                                                                    <span style="width: 5px; float: right; margin: -27px 27px; font-size: 21px;">
                                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator10" InitialValue="0" runat="server"
                                                                                            Display="Dynamic" ValidationGroup="saves" ControlToValidate="ddlGender" ErrorMessage="*"
                                                                                            ForeColor="Red"></asp:RequiredFieldValidator>
                                                                                    </span>
                                                                                </div>
                                                                            </div>
                                                                            <div class="form-group">
                                                                                <label class="control-label col-sm-4" for="Name">
                                                                                    Physical Status <span style="color:red">*</span></label>
                                                                                <div class="col-sm-8">
                                                                                    <asp:DropDownList ID="ddlPhysicalStatus" AutoPostBack="true" OnSelectedIndexChanged="ddlSp_SelectedIndexChanged" runat="server" class="form-control">
                                                                                        <asp:ListItem Selected="True" Value="0">--Select--</asp:ListItem>
                                                                                        <asp:ListItem Value="1">Specially Abled </asp:ListItem>
                                                                                        <asp:ListItem Value="2">Not Applicable</asp:ListItem>
                                                                                    </asp:DropDownList>
                                                                                    <span style="width: 5px; float: right; margin: -27px 27px; font-size: 21px;">
                                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator11" InitialValue="0" runat="server"
                                                                                            Display="Dynamic" ValidationGroup="saves" ControlToValidate="ddlPhysicalStatus" ErrorMessage="*"
                                                                                            ForeColor="Red"></asp:RequiredFieldValidator>
                                                                                    </span>
                                                                                </div>
                                                                            </div>
                                                                            <div class="form-group" runat="server" visible="false" id="divSp">
                                                                                <label class="control-label col-sm-4" for="Name">
                                                                                    Type of Specially Abled</label>
                                                                                <div class="col-sm-8">
                                                                                    <asp:DropDownList ID="ddlSpecially" runat="server" class="form-control">
                                                                                    </asp:DropDownList>

                                                                                </div>
                                                                            </div>
                                                                            <div class="form-group">
                                                                                <label class="control-label col-sm-4" for="Name">
                                                                                    DOB Available</label>
                                                                                <div class="col-sm-8">
                                                                                    <div style="width: 100%;">
                                                                                        <span style="float: left; width: 42%;">
                                                                                            <asp:DropDownList ID="ddlDob" runat="server" AutoPostBack="true" Style="width: 85%;"
                                                                                                OnSelectedIndexChanged="ddlDob_SelectedIndexChanged" class="form-control">
                                                                                                <asp:ListItem Selected="True" Value="0">--Select--</asp:ListItem>
                                                                                                <asp:ListItem Value="1">Yes </asp:ListItem>
                                                                                                <asp:ListItem Value="2">No</asp:ListItem>
                                                                                            </asp:DropDownList>
                                                                                        </span><span style="float: left; width: 19%; padding-top: 1px;">
                                                                                            <asp:Label runat="server" ID="lblAge" class="control-label col-sm-4" Text="Age"></asp:Label>
                                                                                        </span>
                                                                                        <asp:TextBox ID="txtAge" runat="server" Width="38%" MaxLength="2" onkeypress="return isNumberKey(this,event);"
                                                                                            autocomplete="off" ondrop="return false;" class="form-control TeContact1 " />
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                            <div class="form-group">
                                                                                <asp:Label class="control-label col-sm-4" runat="server" ID="lblDob" Text="Date"></asp:Label>
                                                                                <div class="col-sm-8">
                                                                                    <div class="input-group">
                                                                                        <asp:TextBox runat="server" ID="txtDate" autocomplete="off" ondrop="return false;"
                                                                                            class="form-control" onkeypress="return false;"></asp:TextBox>

                                                                                        <%-- <ajax:CalendarExtender ID="CalendarExtenderTourdate" runat="server" Enabled="True"
    Format="dd/MM/yyyy" TargetControlID="txtDate" PopupPosition="BottomRight">
</ajax:CalendarExtender>--%>
                                                                                        <asp:CompareValidator ID="CompareValidator1" ValidationGroup="saves" Display="Dynamic" ForeColor="Red" runat="server"
                                                                                            ControlToValidate="txtDate" ControlToCompare="txtEndDate" Operator="LessThanEqual"
                                                                                            Type="Date" ErrorMessage="Should not be future date"></asp:CompareValidator>
                                                                                        <asp:RequiredFieldValidator ID="ReqTxtDate" runat="server" ControlToValidate="txtDate"
                                                                                            Display="Dynamic" ErrorMessage="*" Font-Bold="False" Font-Size="9px" ForeColor="Red"
                                                                                            SetFocusOnError="True" ValidationGroup="saves"></asp:RequiredFieldValidator>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                            <div class="form-group">
                                                                                <label class="control-label col-sm-4" for="Name">
                                                                                    Education <span style="color:red">*</span></label>
                                                                                <div class="col-sm-8">
                                                                                    <asp:DropDownList ID="ddlEducation" AutoPostBack="true" OnSelectedIndexChanged="ddlSpecialization_SelectedIndexChanged" runat="server" class="form-control">
                                                                                    </asp:DropDownList>
                                                                                    <span style="width: 5px; float: right; margin: -27px 27px; font-size: 21px;">
                                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" InitialValue="0" runat="server"
                                                                                            Display="Dynamic" ValidationGroup="saves" ControlToValidate="ddlEducation" ErrorMessage="*"
                                                                                            ForeColor="Red"></asp:RequiredFieldValidator>
                                                                                    </span>
                                                                                </div>
                                                                            </div>

                                                                            <div class="form-group" runat="server" id="divSpc" visible="false">
                                                                                <label class="control-label col-sm-4" for="Name">
                                                                                    Specialization</label>
                                                                                <div class="col-sm-8">
                                                                                    <asp:DropDownList ID="ddlSpecialization" runat="server" class="form-control">
                                                                                    </asp:DropDownList>

                                                                                </div>
                                                                            </div>
                                                                            <div class="form-group">
                                                                                <label class="control-label col-sm-4" for="Name">
                                                                                    Family Occupation <span style="color:red">*</span></label>
                                                                                <div class="col-sm-8">
                                                                                    <asp:DropDownList ID="ddloccu" runat="server" class="form-control">
                                                                                    </asp:DropDownList>
                                                                                    <span style="width: 5px; float: right; margin: -27px 27px; font-size: 21px;">
                                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" InitialValue="0" runat="server"
                                                                                            Display="Dynamic" ValidationGroup="saves" ControlToValidate="ddloccu" ErrorMessage="*"
                                                                                            ForeColor="Red"></asp:RequiredFieldValidator>
                                                                                    </span>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>

                                                                <div class="col-md-6 col-sm-12">
                                                                    <div class="panel panel-default">
                                                                        <div class="panel-heading" style="padding-left: 12px; padding-right: 12px;">
                                                                            <div class="card_title">
                                                                                <span class="red_circle"></span>
                                                                                <span>Enrolment status</span>
                                                                            </div>
                                                                        </div>
                                                                        <div class="panel-body" style="min-height: 660px;">
                                                                            <div class="form-group">
                                                                                <asp:UpdatePanel ID="Image" runat="server" UpdateMode="Conditional">
                                                                                    <ContentTemplate>
                                                                                        <label class="control-label col-sm-4" for="Name">
                                                                                            Image <span style="color:red">*</span></label>
                                                                                        <div class="col-sm-8">
                                                                                            <asp:FileUpload ID="FileuploadAttach" runat="server" Width="160px" Font-Size="Smaller"
                                                                                                TabIndex="16" />
                                                                                            <asp:Image ID="imgMKS" runat="server" Height="80px" Width="100px" BorderColor="Black"
                                                                                                BorderStyle="Ridge" BorderWidth="1px" />
                                                                                        </div>
                                                                                    </ContentTemplate>
                                                                                    <Triggers>
                                                                                        <asp:PostBackTrigger ControlID="btnsave" />
                                                                                        <asp:PostBackTrigger ControlID="btnSUmbit" />
                                                                                    </Triggers>
                                                                                </asp:UpdatePanel>
                                                                            </div>
                                                                            <div class="form-group">
                                                                                <label class="control-label col-sm-4" for="Name">
                                                                                    Reason of Becoming <span style="color:red">*</span></label>
                                                                                <div class="col-sm-8">
                                                                                    <asp:DropDownList ID="ddlReason" runat="server" class="form-control">
                                                                                    </asp:DropDownList>
                                                                                    <span style="width: 5px; float: right; margin: -27px 27px; font-size: 21px;">
                                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" InitialValue="0" runat="server"
                                                                                            Display="Dynamic" ValidationGroup="saves" ControlToValidate="ddlReason" ErrorMessage="*"
                                                                                            ForeColor="Red"></asp:RequiredFieldValidator>
                                                                                    </span>
                                                                                </div>
                                                                            </div>
                                                                            <div class="form-group">
                                                                                <label class="control-label col-sm-4" for="Name">
                                                                                    Recruitment Process <span style="color:red">*</span></label>
                                                                                <div class="col-sm-8">
                                                                                    <asp:DropDownList ID="ddlSours" runat="server" class="form-control">
                                                                                    </asp:DropDownList>
                                                                                    <span style="width: 5px; float: right; margin: -27px 27px; font-size: 21px;">
                                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" InitialValue="0" runat="server"
                                                                                            Display="Dynamic" ValidationGroup="saves" ControlToValidate="ddlSours" ErrorMessage="*"
                                                                                            ForeColor="Red"></asp:RequiredFieldValidator>
                                                                                    </span>
                                                                                </div>
                                                                            </div>
                                                                            <div class="form-group">
                                                                                <label class="control-label col-sm-4" for="Name">
                                                                                    Work Experience</label>
                                                                                <div class="col-sm-8">
                                                                                    <asp:DropDownList ID="ddlWorkEx" AutoPostBack="true" OnSelectedIndexChanged="ddlWork_SelectedIndexChanged"
                                                                                        runat="server" class="form-control">
                                                                                        <asp:ListItem Selected="True" Value="0">--Select--</asp:ListItem>
                                                                                        <asp:ListItem Value="1">Yes </asp:ListItem>
                                                                                        <asp:ListItem Value="2">No</asp:ListItem>
                                                                                    </asp:DropDownList>
                                                                                </div>
                                                                            </div>
                                                                            <div class="form-group">
                                                                                <label class="control-label col-sm-4" for="Name">
                                                                                    Year</label>
                                                                                <div class="col-sm-8">
                                                                                    <div style="width: 100%;">
                                                                                        <span style="float: left; width: 42%;">
                                                                                            <asp:TextBox ID="txtDuartion" Enabled="false" Style="width: 85%;" runat="server"
                                                                                                MaxLength="2" onkeypress="return isNumberKey(this,event);" autocomplete="off"
                                                                                                ondrop="return false;" class="form-control TeContact1 " />
                                                                                        </span><span style="float: left; width: 19%; padding-top: 6px;">
                                                                                            <label>
                                                                                                Month:</label>
                                                                                        </span>
                                                                                        <asp:TextBox ID="txtMonth" Enabled="false" Width="39%" runat="server" MaxLength="2"
                                                                                            onkeypress="return isNumberKey(this,event);" autocomplete="off" ondrop="return false;"
                                                                                            class="form-control TeContact1 " />
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                            <div class="form-group">
                                                                                <asp:Label runat="server" ID="Label5" class="control-label col-sm-4" Text="Status:"></asp:Label>
                                                                                <div class="col-sm-8">
                                                                                    <asp:DropDownList ID="ddlWorkingStatus" OnSelectedIndexChanged="ddlWorkingStatus_SelectedIndexChanged" AutoPostBack="true"
                                                                                        runat="server" class="form-control">
                                                                                        <asp:ListItem Selected="True" Value="0">--Select--</asp:ListItem>
                                                                                        <asp:ListItem Value="1">Working </asp:ListItem>
                                                                                        <asp:ListItem Value="2">DropOut</asp:ListItem>
                                                                                    </asp:DropDownList>
                                                                                </div>
                                                                            </div>

                                                                            <div class="form-group" runat="server" id="Resone" visible="false">
                                                                                <asp:Label runat="server" ID="Label6" class="control-label col-sm-4" Text="Reason:"></asp:Label>
                                                                                <div class="col-sm-8">
                                                                                    <asp:DropDownList ID="ddlStatusReasone" OnSelectedIndexChanged="ddlStatusReasone_SelectedIndexChanged" AutoPostBack="true"
                                                                                        runat="server" class="form-control">
                                                                                    </asp:DropDownList>
                                                                                </div>
                                                                            </div>
                                                                            <div class="form-group" runat="server" id="rdate" visible="false">
                                                                                <asp:Label runat="server" ID="Label7" class="control-label col-sm-4" Text="Dropout Date:"></asp:Label>
                                                                                <div class="col-sm-8">
                                                                                    <asp:TextBox runat="server" ID="txtDropDate" autocomplete="off" ondrop="return false;"
                                                                                        class="form-control" onkeypress="return false;"></asp:TextBox>
                                                                                    <asp:CompareValidator ID="CompareValidator2" ValidationGroup="saves" Display="Dynamic" ForeColor="Red" runat="server"
                                                                                        ControlToValidate="txtDropDate" ControlToCompare="txtEndDate" Operator="LessThanEqual"
                                                                                        Type="Date" ErrorMessage="Should not be future date"></asp:CompareValidator>


                                                                                </div>
                                                                            </div>

                                                                            <div class="form-group" runat="server" id="divJob" visible="false">
                                                                                <asp:Label runat="server" ID="Label11" class="control-label col-sm-4" Text="Job:"></asp:Label>
                                                                                <div class="col-sm-8">
                                                                                    <asp:TextBox ID="txtJob" runat="server" MaxLength="20" onkeypress="return onlyAlphabets(event,this);"
                                                                                        autocomplete="off" ondrop="return false;" class="form-control TeContact1 " />
                                                                                </div>
                                                                            </div>
                                                                            <div class="form-group" runat="server" id="divbus" visible="false">
                                                                                <asp:Label runat="server" ID="Label12" class="control-label col-sm-4" Text="Business:"></asp:Label>
                                                                                <div class="col-sm-8">
                                                                                    <asp:TextBox ID="txtBus" runat="server" MaxLength="20" onkeypress="return onlyAlphabets(event,this);"
                                                                                        autocomplete="off" ondrop="return false;" class="form-control TeContact1 " />
                                                                                </div>
                                                                            </div>
                                                                            <div class="form-group" runat="server" id="divJobOp" visible="false">
                                                                                <asp:Label runat="server" ID="Label13" class="control-label col-sm-4" Text="Job Opportunity Through:"></asp:Label>
                                                                                <div class="col-sm-8">
                                                                                    <asp:DropDownList ID="ddlJobOpportunity" OnSelectedIndexChanged="ddlOther_SelectedIndexChanged" AutoPostBack="true"
                                                                                        runat="server" class="form-control">
                                                                                    </asp:DropDownList>
                                                                                </div>
                                                                            </div>

                                                                            <div class="form-group" runat="server" id="divOtherJob" visible="false">
                                                                                <asp:Label runat="server" ID="Label14" class="control-label col-sm-4" Text="Other (Specify):"></asp:Label>
                                                                                <div class="col-sm-8">
                                                                                    <asp:TextBox ID="txtotherjob" runat="server" MaxLength="50" onkeypress="return onlyAlphabets(event,this);"
                                                                                        autocomplete="off" ondrop="return false;" class="form-control TeContact1 " />
                                                                                </div>
                                                                            </div>
                                                                            <div class="form-group" runat="server" id="EmpID" visible="false">
                                                                                <asp:Label runat="server" ID="Label8" class="control-label col-sm-4" Text="EG Employee ID"></asp:Label>
                                                                                <div class="col-sm-8">
                                                                                    <asp:TextBox ID="txtEmployeeID" runat="server" MaxLength="30"
                                                                                        autocomplete="off" ondrop="return false;" class="form-control TeContact1 " />
                                                                                </div>
                                                                            </div>

                                                                            <div class="form-group">
                                                                                <asp:Label runat="server" ID="Label1" class="control-label col-sm-4" Text="Expectation:"></asp:Label>
                                                                                <div class="col-sm-8">
                                                                                    <asp:TextBox ID="txtExp" runat="server" MaxLength="30" onkeypress="return onlyAlphabets(event,this);"
                                                                                        autocomplete="off" ondrop="return false;" class="form-control TeContact1 " />
                                                                                </div>
                                                                            </div>

                                                                            <div class="form-group">
                                                                                <asp:Label runat="server" ID="Label2" class="control-label col-sm-4" Text="Ambition:"></asp:Label>
                                                                                <div class="col-sm-8">
                                                                                    <asp:TextBox ID="txtAbv" runat="server" MaxLength="30" onkeypress="return onlyAlphabets(event,this);"
                                                                                        autocomplete="off" ondrop="return false;" class="form-control TeContact1 " />
                                                                                </div>
                                                                            </div>
                                                                            <div class="form-group">
                                                                                <asp:Label class="control-label col-sm-4" runat="server" ID="Label3" Text="Joining date"></asp:Label>
                                                                                <div class="col-sm-8">

                                                                                    <asp:TextBox runat="server" ID="txtJoingDate" AutoPostBack="true" autocomplete="off" ondrop="return false;" OnTextChanged="txtJoingDate_OnTextChanged"
                                                                                        class="form-control" onkeypress="return false;"></asp:TextBox>

                                                                                    <%-- <ajax:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True" Format="dd/MM/yyyy"
    TargetControlID="txtJoingDate" OnClientDateSelectionChanged="arrivaldatecheck"
    PopupPosition="BottomRight">
</ajax:CalendarExtender>--%>
                                                                                    <asp:CompareValidator ID="CompareValidator3" ValidationGroup="saves" Display="Dynamic" ForeColor="Red" runat="server"
                                                                                        ControlToValidate="txtJoingDate" ControlToCompare="txtEndDate" Operator="LessThanEqual"
                                                                                        Type="Date" ErrorMessage="Should not be future date"></asp:CompareValidator>

                                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="txtJoingDate"
                                                                                        Display="Dynamic" ErrorMessage="*" Font-Bold="False" Font-Size="9px" ForeColor="Red"
                                                                                        SetFocusOnError="True" ValidationGroup="saves"></asp:RequiredFieldValidator>

                                                                                </div>
                                                                            </div>
                                                                            <div class="form-group">
                                                                                <asp:Label runat="server" ID="Label4" class="control-label col-sm-4" Text="No of Traning Days"></asp:Label>
                                                                                <div class="col-sm-8">
                                                                                    <asp:TextBox ID="txtday" Enabled="false" runat="server" MaxLength="30" onkeypress="return onlyAlphabets(event,this);"
                                                                                        autocomplete="off" ondrop="return false;" class="form-control TeContact1 " />
                                                                                </div>
                                                                            </div>

                                                                            <div class="form-group">
                                                                                <label class="control-label col-sm-4" for="Name">
                                                                                    TB Recruited For <span style="color:red">*</span>
                                                                                </label>
                                                                                <div class="col-sm-8">
                                                                                    <asp:DropDownList ID="ddltbRecruited" runat="server" class="form-control">
                                                                                        <asp:ListItem Selected="True" Value="0">--Select--</asp:ListItem>
                                                                                        <asp:ListItem Value="1">Enrolment </asp:ListItem>
                                                                                        <asp:ListItem Value="2">Learning</asp:ListItem>
                                                                                        <asp:ListItem Value="3">Enrolment + Learning</asp:ListItem>
                                                                                    </asp:DropDownList>
                                                                                    <span style="width: 5px; float: right; margin: -27px 27px; font-size: 21px;">
                                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" InitialValue="0" runat="server"
                                                                                            Display="Dynamic" ValidationGroup="saves" ControlToValidate="ddltbRecruited" ErrorMessage="*"
                                                                                            ForeColor="Red"></asp:RequiredFieldValidator>
                                                                                    </span>
                                                                                </div>
                                                                            </div>

                                                                            <div class="form-group" runat="server" visible="false" id="divAlumni">
                                                                                <asp:Label runat="server" ID="Label9" class="control-label col-sm-4" Text="Is Team Balika Alumni:"></asp:Label>
                                                                                <div class="col-sm-8">
                                                                                    <asp:DropDownList ID="ddlAlumni" OnSelectedIndexChanged="ddlAlumni_SelectedIndexChanged" AutoPostBack="true"
                                                                                        runat="server" class="form-control">
                                                                                        <asp:ListItem Selected="True" Value="0">--Select--</asp:ListItem>
                                                                                        <asp:ListItem Value="1">Yes </asp:ListItem>
                                                                                        <asp:ListItem Value="2">No</asp:ListItem>
                                                                                    </asp:DropDownList>
                                                                                </div>
                                                                            </div>

                                                                            <div class="form-group" visible="false" runat="server" id="divAlumni1">
                                                                                <asp:Label class="control-label col-sm-4" runat="server" ID="Label10" Text="Team Balika Alumni Date"></asp:Label>
                                                                                <div class="col-sm-8">
                                                                                    <div class="input-group">
                                                                                        <asp:TextBox runat="server" ID="txtAlumniDate" autocomplete="off" ondrop="return false;"
                                                                                            class="form-control" onkeypress="return false;"></asp:TextBox>
                                                                                        <asp:CompareValidator ID="CompareValidator4" ValidationGroup="saves" Display="Dynamic" ForeColor="Red" runat="server"
                                                                                            ControlToValidate="txtAlumniDate" ControlToCompare="txtEndDate" Operator="LessThanEqual"
                                                                                            Type="Date" ErrorMessage="Should not be future date"></asp:CompareValidator>

                                                                                        <%--<ajax:CalendarExtender ID="CalendarExtender3" runat="server" Enabled="True" Format="dd/MM/yyyy"
    TargetControlID="txtAlumniDate" OnClientDateSelectionChanged="arrivaldatecheck"
    PopupPosition="BottomRight">
</ajax:CalendarExtender>--%>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>

                                                            </asp:Panel>
                                                            <div class="col-sm-12" runat="server" visible="false"  >
                                                                <div class="thumbnail" style="float: left; width: 100%;">
                                                                    <div class="col-xs-12 col-sm-12" style="text-align: center">
                                                                        <asp:ImageButton ID="btnSUmbit" ToolTip="Save" OnClick="btnSumbit_Click" ValidationGroup="saves"
                                                                            ImageUrl="~/images/Sumbit.jpg" runat="server" />

                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <!-- /.col -->
                                                        </div>
                                                        <!-- /.row -->
                                                    </div>
                                                    <!-- /#tab1 -->

                                                    <div runat="server" clientidmode="Static" class="tab-pane fade " id="tab2">
                                                        <div class="row" style="margin-left: -15px; margin-right: -15px;">
                                                            <div class="col-lg-8 col-md-8 col-sm-12 col-xs-12">
                                                                <div class="panel panel-default">
                                                                    <div class="panel-heading" style="padding-left: 12px; padding-right: 12px;">
                                                                        <div class="card_title">
                                                                            <span class="red_circle"></span>
                                                                            <span>Member Details (Auto-filled from profile)</span>
                                                                        </div>
                                                                    </div>
                                                                    <div class="panel-body">
                                                                        <div class="profile_date">
                                                                            <i class="fa fa-check-square" aria-hidden="true"></i>
                                                                            <span>Profile data fetched. Verify before requesting ID card.</span>
                                                                        </div>
                                                                        <div class="row" style="margin-left: -15px; margin-right: -15px">
                                                                            <div class="col-lg-6 col-sm-12 col-xs-12">
                                                                                <div class="form-group">
                                                                                    <label class="col-sm-4">TB Code</label>
                                                                                    <div class="col-sm-8">
                                                                                        <asp:TextBox runat="server" ID="txtViewTBcode" Enabled="false" class="form-control" placeholder="Enter TB Code" />
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                            <div class="col-lg-6 col-sm-12 col-xs-12">
                                                                                <div class="form-group">
                                                                                    <label class="col-sm-4">Name</label>
                                                                                    <div class="col-sm-8">
                                                                                        <asp:TextBox runat="server" ID="txtViewTBName" Enabled="false" class="form-control" placeholder="Enter Name" />
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                            <div class="col-lg-6 col-sm-12 col-xs-12">
                                                                                <div class="form-group">
                                                                                    <label class="col-sm-4">Father Name</label>
                                                                                    <div class="col-sm-8"> 
                                                                                        <asp:TextBox runat="server" ID="txtViewTBFather" class="form-control" Enabled="false" placeholder="Enter Father Name" />
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                            <div class="col-lg-6 col-sm-12 col-xs-12">
                                                                                <div class="form-group">
                                                                                    <label class="col-sm-4">Date of Birth</label>
                                                                                    <div class="col-sm-8">
                                                                                        <asp:TextBox runat="server" ID="txtViewDOB" Enabled="false" class="form-control" />
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                            <div class="col-lg-6 col-sm-12 col-xs-12">
                                                                                <div class="form-group">
                                                                                    <label class="col-sm-4">Mobile</label>
                                                                                    <div class="col-sm-8">
                                                                                        <asp:TextBox runat="server" ID="txtViewMobile" Enabled="false" class="form-control" />
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                            <div class="col-lg-6 col-sm-12 col-xs-12">
                                                                                <div class="form-group">
                                                                                    <label class="col-sm-4">Joining Date</label>
                                                                                    <div class="col-sm-8">
                                                                                        <asp:TextBox runat="server" ID="txtViewJoin" Enabled="false" class="form-control" />
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                            <div class="col-lg-6 col-sm-12 col-xs-12">
                                                                                <div class="form-group">
                                                                                    <label class="col-sm-4">Village</label>
                                                                                    <div class="col-sm-8">
                                                                                        <asp:TextBox runat="server" ID="txtViewVillage" Enabled="false" class="form-control" placeholder="Enter Village Name" />
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                            <div class="col-lg-6 col-sm-12 col-xs-12">
                                                                                <div class="form-group">
                                                                                    <label class="col-sm-4">Cluster</label>
                                                                                    <div class="col-sm-8">
                                                                                        <asp:TextBox runat="server" ID="txtViewcluster" Enabled="false" class="form-control" placeholder="Enter Cluster" />
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                            <div class="col-lg-6 col-sm-12 col-xs-12">
                                                                                <div class="form-group">
                                                                                    <label class="col-sm-4">District</label>
                                                                                    <div class="col-sm-8">
                                                                                        <asp:TextBox runat="server" ID="txtViewDistrict" Enabled="false" class="form-control" placeholder="Enter District" />
                                                                                    </div>
                                                                                </div>
                                                                            </div>

                                                                        </div>

                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-12 col-xs-12 text-center">
                                                                    <asp:Button ID="btnSubmitApproval"
                                                                        runat="server"
                                                                        data-tbcode="TB12345"
                                                                        CssClass="btn btn-info"
                                                                        Text="Submit for DPO Approval"
                                                                        OnClientClick="SubmitForApproval(this); return false;" />

                                                                    <asp:Button ID="btnDownloadIdCard"
                                                                        runat="server"
                                                                        OnClick="btnAdd2_Click"
                                                                        CssClass="btn btn-success"
                                                                        Text="Download ID Card"
                                                                        Visible="false"
                                                                         />
                                                                    <%--<button type="button" class="btn btn-warning">
                                                                        <i class="fa fa-times" aria-hidden="true"></i>&nbsp;Cancel</button>--%>
                                                                    <asp:Label ID="lblRejectionRemark"
                                                                        runat="server"
                                                                        CssClass="text-danger"
                                                                        Visible="false">
                                                                    </asp:Label>
                                                               </div>
                                                            </div>
                                                            <div class="col-lg-4 col-md-4 col-sm-12 col-xs-12">
                                                               <%-- <h5 style="text-align: center">LIVE PREVIEW
                                                                </h5>--%>

                                                                <div class="flip-card card-teal" id="card1" runat="server">
                                                                    <div class="flip-card-inner">
                                                                        <div class="flip-front">
                                                                            <div class="row box" style="margin-left: 0px; margin-right: 0px;">
                                                                                <table style="font-family: arial, sans-serif; border-collapse: collapse; width: 100%; border: 1px solid #dddddd;">
                                                                                    <tbody>
                                                                                        <tr>
                                                                                            <td style="padding: 8px 15px;">
                                                                                                <div style="position: relative;">
                                                                                                    <img src="./images/backgroundsvg.png" style="width: 100%;" alt="">
                                                                                                    <asp:Image runat="server" ID="TBImagePHIM" Style="position: absolute; width: 42.5%;height: 126px; left: 29.3%; top: 28.8%; background-color: #eb2027; border-radius: 50%;"
                                                                                                         alt="" />
                                                                                                </div>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td style="text-align: center;">
                                                                                                <img src="./images/text.svg" width="30%" alt="">
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td style="text-align: center;">
                                                                                                <ul class="flip_ul" style="padding: 5px">
                                                                                                    <li>
                                                                                                        <span>Name</span>
                                                                                                        <span runat="server" id="TBNameSP"></span>
                                                                                                    </li>
                                                                                                    <li>
                                                                                                        <span>Village</span>
                                                                                                        <span runat="server" id="TBVillageSP"></span>
                                                                                                    </li>
                                                                                                    <li>
                                                                                                        <span>Team Balika Code</span>
                                                                                                        <span runat="server" id="TBTBCSP"></span>
                                                                                                    </li>
                                                                                                     <li>
                                                                                                                        <span>Date of Joining</span>
                                                                                                                        <span runat="server" id="TBDOJSP"></span>
                                                                                                                    </li>
                                                                                                    <li>
                                                                                                        <span>Cluster</span>
                                                                                                        <span runat="server" id="TBClusterSP"></span>
                                                                                                    </li>
                                                                                                </ul>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td>&nbsp;</td>
                                                                                        </tr>
                                                                                    </tbody>
                                                                                </table>
                                                                            </div>

                                                                            <%--<div class="row box" style="margin-left: 0px; margin-right: 0px">
                                                                                <div class="col-sm-12" style="position: relative; z-index: 1; padding: 15px 15px 5px 15px;">
                                                                                    <div>
                                                                                        <small style="color: #e1acad; letter-spacing: 2px;">EDUCATE GIRL - TEAM BALIKA</small>
                                                                                    </div>
                                                                                    <div class="fip_card_flex">
                                                                                        <div class="flip_img">
                                                                                            images
                                                                                        </div>
                                                                                        <div class="name_add">
                                                                                            <h4 style="margin-top: 0px; margin-bottom: 3px;">SHIVANI VARMA</h4>
                                                                                            <small style="color: #e1acad; margin-bottom: 10px">ABDULLA PUR, Hardoi</small>
                                                                                            <div class="flip_code">
                                                                                                <span>TB- 98989898-00369</span>
                                                                                            </div>
                                                                                        </div>
                                                                                    </div>
                                                                                    <div class="disp_between">
                                                                                        <div>
                                                                                            <span>i</span>
                                                                                            <small>Joined: <span  id="TBDOJSP"></span></small>
                                                                                        </div>
                                                                                        <div>
                                                                                            <small><span  id="TBCludterSP"></span></small>
                                                                                        </div>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                            <div class="row" style="width: 100%">
                                                                                <div class="col-sm-12" style="padding-top: 15px">
                                                                                    <p style="letter-spacing: 1px; font-weight: 600; color: #8b8b8b;">MEMBER INFORMATION</p>
                                                                                </div>
                                                                                <div class="col-sm-12">
                                                                                    <ul class="flip_ul">
                                                                                        <li>
                                                                                            <span>Name</span>
                                                                                            <span id="TBNameSP"></span>
                                                                                        </li>
                                                                                        <li>
                                                                                            <span>Village</span>
                                                                                            <span id="TBVillageSP"></span>
                                                                                        </li>
                                                                                        <li>
                                                                                            <span>Date of Birth</span>
                                                                                            <span  id="TBDOBSP"></span>
                                                                                        </li>
                                                                                    </ul>
                                                                                </div>
                                                                                
                                                                                
                                                                            </div>--%>
                                                                        </div>
                                                                        <div class="flip-back">
                                                                            <div class="row box" style="margin-left: 0px; margin-right: 0px;">
                                                                                <table style="border-collapse: collapse; width: 100%; border: 0px solid #dddddd;">
                                                                                    <tbody>
                                                                                        <tr>
                                                                                            <td style="position: relative; padding: 15px 15px;">
                                                                                                <img src="./images/backgroundsvg1.png" alt=""
                                                                                                    style="position: absolute; left: 0; top: 0; width: 260px; height: 125px; background-repeat: no-repeat; z-index: -1;">
                                                                                                <img src="./images/backgroundsvg2.png" alt=""
                                                                                                    style="position: absolute; right: 0; bottom: 0; width: 215px; height: 112px; background-repeat: no-repeat; z-index: -1;">
                                                                                                <table style="border-collapse: collapse; width: 100%; border: 1px solid #dddddd;">
                                                                                                    <tbody>
                                                                                                        <tr>
                                                                                                            <td style="text-align: right; padding: 15px 15px;">
                                                                                                                <img src="./images/Educategirllogo.png" style="width: 130px;" alt="">
                                                                                                            </td>
                                                                                                        </tr>
                                                                                                        <tr>
                                                                                                            <td style="text-align: center;">
                                                                                                                <ul class="flip_ul" style="padding: 5px">
                                                                                                                    <li>
                                                                                                                        <span>Father Name</span>
                                                                                                                        <span runat="server" id="TBFatherNameSP"></span>
                                                                                                                    </li>
                                                                                                                    <li>
                                                                                                        <span>Date of Birth</span>
                                                                                                        <span runat="server" id="TBDOBSP"></span>
                                                                                                    </li>
                                                                                                                   
                                                                                                                    <li>
                                                                                                                        <span>Mobile</span>
                                                                                                                        <span runat="server" id="TBMobileSP"></span>
                                                                                                                    </li>
                                                                                                                    <li>
                                                                                                                        <span>Validity From - To</span>
                                                                                                                        <span runat="server" id="TBVFTSP"></span>
                                                                                                                    </li>
                                                                                                                    <li>
                                                                                                                        <span>Address</span>
                                                                                                                        <span runat="server" id="TBAddressSP"></span>
                                                                                                                    </li>
                                                                                                                </ul>
                                                                                                            </td>
                                                                                                        </tr>
                                                                                                        <tr>
                                                                                                            <td style="text-align: center;">
                                                                                                                <div style="width: 80px; height: 80px; border: 1px solid #ccc; margin: auto;">
                                                                                                                    <asp:Image runat="server" ID="QRIMG" Style="border-width:0px;position:absolute;width:24.5%;height:96px;left:37.3%;top:70.8%;"
                                                                                                         alt="" />
                                                                                                                </div>
                                                                                                                <a href="#"
                                                                                                                    style="color: #eb2027; text-decoration: none;">www<span style="color: whitesmoke">.educategirls.ngo</span></a>
                                                                                                            </td>
                                                                                                        </tr>
                                                                                                        <tr>
                                                                                                            <td>&nbsp;</td>
                                                                                                        </tr>
                                                                                                    </tbody>
                                                                                                </table>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </tbody>
                                                                                </table>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div>
                                                                    <div class="btn-group" style="width: 100%">
                                                                        <button type="button"
                                                                            class="btn-flip1 btn btn-danger"
                                                                            style="width: 50%"
                                                                            onclick="showFront('ctl00_MainContent_card1')">
                                                                            Front
   
                                                                        </button>

                                                                        <button type="button"
                                                                            class="btn-flip1 btn-flip-back btn btn-light"
                                                                            style="width: 50%"
                                                                            onclick="showBack('ctl00_MainContent_card1')">
                                                                            Back
   
                                                                        </button>
                                                                        <%--  <button class="btn-flip1 btn btn-danger" style="width: 50%" onclick="flipCard('ctl00_MainContent_card1')">Front</button>
                                                                        <button class="btn-flip1 btn-flip-back btn btn-light" style="width: 50%" onclick="flipCard('ctl00_MainContent_card1')">back</button>--%>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <!-- /.col -->
                                                        </div>
                                                        <!-- /.row -->
                                                    </div>
                                                    <!-- /#tab2 -->

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
                                                                      <%--  <div class="new_card">
                                                                            <div class="new_card_between">
                                                                                <div class="left_side">
                                                                                    <input type="checkbox" />
                                                                                    <img src="./images/criteria__md-Icon.png" />
                                                                                    <div class="flex_dir">
                                                                                        <div class="disp_flex">
                                                                                            <p style="margin: 0px;">SHIVANI VARMA</p>
                                                                                            <span class="tb_id">TB 1234578-98765</span>
                                                                                        </div>
                                                                                        <div class="disp_flex fc">
                                                                                            <span>Hardoi</span>
                                                                                            <span>ABDULLA PUR</span>
                                                                                            <span>Cluster B</span>
                                                                                            <span>Requested: 21-May-2026</span>
                                                                                        </div>
                                                                                    </div>
                                                                                </div>
                                                                                <div class="right_side">
                                                                                    <div class="flex_dir_rs">
                                                                                        <div class="pend">
                                                                                            <i class="fa fa-exclamation-triangle" aria-hidden="true"></i>
                                                                                            <span>Pending</span>
                                                                                        </div>
                                                                                        <div class="valid_rs">
                                                                                            <span>Valid: 21-May-2026</span>
                                                                                            <i class="fa fa-long-arrow-right" aria-hidden="true"></i>
                                                                                            <span>21-May-2027</span>
                                                                                        </div>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                        </div>--%>

                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <!-- /.col -->
                                                    </div>
                                                    <!-- /#tab3 -->

                                                </div>



                                            </div>
                                        </div>
                                        <asp:TextBox ID="txtEndDate" Width="0px" Style="border: 0px" runat="server" CssClass="d-none"></asp:TextBox>
                                    </div>
                                    <!-- /#page-content-wrapper -->
                                </div>
                                <!-- /#wrapper -->
                                <!-- /#wrapper -->
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <asp:Label ID="HdnStartYear" Visible="false" runat="server" />

        </ContentTemplate>
        <Triggers>
        <asp:PostBackTrigger ControlID="btnDownloadIdCard" />
    </Triggers>
    </asp:UpdatePanel>

</asp:Content>
