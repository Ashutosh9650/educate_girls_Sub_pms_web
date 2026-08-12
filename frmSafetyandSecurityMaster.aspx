<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmSafetyandSecurityMaster.aspx.cs" Culture="en-GB"
    MasterPageFile="~/Site.master" Inherits="frmSafetyandSecurityMaster" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
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
    <script type="text/javascript">


</script>
    <script type="text/javascript">

        function DateCom(chk) {

            var startDate = $('#<%=txtFromDate.ClientID %>').val();
            var endDate = $('#<%=txtTodate.ClientID %>').val();

            alert(Date.parse(startDate));
            alert(Date.parse(endDate));
            if ((Date.parse(startDate) <= Date.parse(endDate))) {
                alert("End date should be greater than Start date");
                document.getElementById("EndDate").value = "";
            }
        }





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

                UploadFile();


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
                UploadFileDist();

            }
            else if (Flag == 'B') {
                if (maxSelection <= 10) {
                    $('#<%=hdn_PBID2.ClientID %>').val(lid);
                    $('#<%=hdn_PBName2.ClientID %>').val(Lngg);
                    $('#<%=txtMuhala1.ClientID %>').val(Lngg);
                }

                else {
                    $('.' + clsname + ' input[type="checkbox"]:checked').each(function () {
                        $(this).attr("checked", false);
                    });
                    $('#<%=hdn_PBID2.ClientID %>').val('');
                    $('#<%=hdn_PBName2.ClientID %>').val('');
                    $('#<%=txtMuhala1.ClientID %>').val('');


                    $find("Modal_alertB").show();
                    return false;
                }


            }
            else if (Flag == 'C') {

                if (maxSelection <= 10) {


                }

                else {


                    return false;
                }


            }


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
            if ($("." + txtid).val() == 0) {
                $("." + txtid).css("border", "solid 1px green")
                return true;
            }
            else if (phoneno.test(inputtxt) && inputtxt.length == 10) {
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
    </script>
    <style type="text/css">
        .ajax__calendar_container {
            z-index: 1000;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:UpdatePanel runat="server" ID="mkiiainpnl121">
        <ContentTemplate>
            <div class="container-fluid">
                <%--<input type="image" id="ton-new" class="butt" src="Images/close.png"  />
       <div id="div-show-new"></div> --%>
            </div>
            <div class="container-fluid" style="margin-top: 0px;">
                <div class="row">

                    <div class="col-lg-12 col-md-10 col-sm-9" style="width: 100%">
                        <div class="row">
                            <div class="col-lg-12">
                                <div class="panel panel-default">
                                    <div class="panel-heading" style="padding: 5px 10px;">
                                        <div class="row">
                                            <div class="col-lg-9 col-md-8 col-sm-8">
                                                <h3 class="text-danger" style="margin: 0px;">Safety and Security Master</h3>
                                            </div>
                                            <div class="col-lg-3 col-md-4 col-sm-4 cpl-xs-11 pull-right">
                                                <asp:LinkButton ID="LinkddButton1" runat="server" Text="Export to Excel" OnClick="btnReprot_Click"
                                                    class="pull-right" Style="margin-top: 5px;"></asp:LinkButton>
                                                <asp:ImageButton ID="btnAdd" ToolTip="Serach" runat="server" OnClick="btnApprove_Click"
                                                    class="btn btn-danger btn-paddd pull-right" BackColor="#f1f1f1" ImageUrl="~/images/add-29-1.png" />
                                                <asp:ImageButton ID="btnSave" runat="server" ImageUrl="~/images/save-29-1.png" Text="Save"
                                                    class="btn  btn-paddd pull-right" ToolTip="Save" OnClick="btnSave_Click" Style="float: none;"
                                                    ValidationGroup="saves"></asp:ImageButton>

                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="form-horizontal">
                                    <div class="row">
                                        <div class="col-lg-12" style="margin-bottom: 10px; padding: 0px 0px 0px 0px; border: 1px solid #ddd; border-radius: 4px;">

                                            <div class="form-horizontal">
                                                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="margin-bottom: -7px;padding: 0px 10px 0px 10px;">
                                                    <fieldset class="scheduler-border">
                                                        <legend class="scheduler-border">Safety and Security Master </legend>
                                                        <div class="Row">
                                                            <asp:Panel ID="pnlMain1" runat="server">
                                                                <div class="col-lg-4 col-md-4  col-sm-4  cpl-xs-12" runat="server" id="divE1">
                                                                    <div class="form-group">
                                                                        <label class="control-label col-sm-4" for="Name">
                                                                            Level
                                                                        </label>
                                                                        <div class="col-sm-8">
                                                                            <asp:DropDownList ID="ddInGeography" AutoPostBack="true" OnSelectedIndexChanged="ddInGeography_SelectedIndexChanged"
                                                                                runat="server" class="form-control">
                                                                                <asp:ListItem Selected="True" Value="0">--Select--</asp:ListItem>
                                                                                <asp:ListItem Value="1">State </asp:ListItem>
                                                                                <asp:ListItem Value="2">District</asp:ListItem>
                                                                                <asp:ListItem Value="3">Block</asp:ListItem>
                                                                            </asp:DropDownList>
                                                                            <span style="width: 5px; float: right; margin: -27px 27px; font-size: 21px;">
                                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" InitialValue="0" runat="server"
                                                                                    Display="Dynamic" ValidationGroup="saves" ControlToValidate="ddInGeography" ErrorMessage="*"
                                                                                    ForeColor="Red"></asp:RequiredFieldValidator>
                                                                            </span>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="col-lg-4 col-md-4  col-sm-4  cpl-xs-12" id="divState" runat="server"
                                                                    visible="false">
                                                                    <div class="form-group">
                                                                        <label class="control-label col-sm-4" for="Name">
                                                                            State
                                                                        </label>
                                                                        <div class="col-sm-8">
                                                                            <asp:TextBox ID="txt_pbname" autocomplete="off" ondrop="return false;" runat="server"
                                                                                CssClass="form-control" onkeypress="return false;" TabIndex="5"></asp:TextBox>
                                                                            <ajax:PopupControlExtender ID="PopupControltxt_pbname" runat="server" TargetControlID="txt_pbname"
                                                                                PopupControlID="pnt_bookformat" OffsetY="22">
                                                                            </ajax:PopupControlExtender>
                                                                            <asp:Panel ID="pnt_bookformat" runat="server" Direction="LeftToRight" Style="display: none; min-height: 60px; max-height: 300px; overflow: auto; z-index: 999999; background-color: #F1F1F1; border: solid 1px #cccccc; width: 100%"
                                                                                CssClass="panel">
                                                                                <span>
                                                                                    <asp:CheckBoxList ID="ChkState" OnTextChanged="txtState_TextChanged" AutoPostBack="true" CssClass="_bookformat radio" runat="server" onclick="SetMultilanguage('F','_bookformat');">
                                                                                    </asp:CheckBoxList>
                                                                                </span>
                                                                                <asp:HiddenField runat="server" ID="hdn_PBName" />
                                                                                <asp:HiddenField runat="server" ID="hdn_PBID" />
                                                                            </asp:Panel>

                                                                        </div>
                                                                    </div>
                                                                </div>

                                                                <div class="col-lg-4 col-md-4  col-sm-4  cpl-xs-12" id="divDist" runat="server" visible="false">
                                                                    <div class="form-group">
                                                                        <label class="control-label col-sm-4" for="Name">
                                                                            District
                                                                        </label>
                                                                        <div class="col-sm-8">
                                                                            <asp:TextBox ID="txtMuhala" runat="server" autocomplete="off" ondrop="return false;"
                                                                                CssClass="form-control"></asp:TextBox>
                                                                            <ajax:PopupControlExtender ID="PopupControlExtender1" runat="server" TargetControlID="txtMuhala"
                                                                                PopupControlID="pnt_Muhula" OffsetY="22">
                                                                            </ajax:PopupControlExtender>
                                                                            <asp:Panel ID="pnt_Muhula" runat="server" Direction="LeftToRight" Style="display: none; min-height: 60px; max-height: 300px; overflow: auto; z-index: 999999; background-color: #F1F1F1; border: solid 1px #cccccc; width: 100%"
                                                                                CssClass="panel">
                                                                                <span>
                                                                                    <asp:CheckBoxList ID="chkDistrict" CssClass="_bookformat1 radio" runat="server" OnTextChanged="txtDist_TextChanged" AutoPostBack="true" onclick="SetMultilanguage('M','_bookformat1');">
                                                                                    </asp:CheckBoxList>
                                                                                </span>
                                                                                <asp:HiddenField runat="server" ID="hhmuhulaid" />
                                                                                <asp:HiddenField runat="server" ID="HidName" />
                                                                            </asp:Panel>

                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="col-lg-4 col-md-4  col-sm-4  cpl-xs-12" id="divBlock" runat="server"
                                                                    visible="false">
                                                                    <div class="form-group">
                                                                        <label class="control-label col-sm-4" for="Name">
                                                                            Block
                                                                        </label>
                                                                        <div class="col-sm-8">
                                                                            <asp:TextBox ID="txtMuhala1" runat="server" autocomplete="off" ondrop="return false;"
                                                                                CssClass="form-control" onkeypress="return false;" TabIndex="5"></asp:TextBox>
                                                                            <ajax:PopupControlExtender ID="PopupControlExtender2" runat="server" TargetControlID="txtMuhala1"
                                                                                PopupControlID="pnt_Muhula1" OffsetY="22">
                                                                            </ajax:PopupControlExtender>
                                                                            <asp:Panel ID="pnt_Muhula1" runat="server" Direction="LeftToRight" Style="display: none; min-height: 60px; max-height: 300px; overflow: auto; z-index: 999999; background-color: #F1F1F1; border: solid 1px #cccccc; width: 100%"
                                                                                CssClass="panel">
                                                                                <span>
                                                                                    <asp:CheckBoxList ID="chkBlock" CssClass="_bookformat2 radio" runat="server" onclick="SetMultilanguage('B','_bookformat2');">
                                                                                    </asp:CheckBoxList>
                                                                                </span>
                                                                                <asp:HiddenField runat="server" ID="hdn_PBName2" />
                                                                                <asp:HiddenField runat="server" ID="hdn_PBID2" />
                                                                            </asp:Panel>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="col-lg-4 col-md-4  col-sm-4  cpl-xs-12" runat="server" id="divE2">
                                                                    <div class="form-group">
                                                                        <label class="control-label col-sm-4" for="Name">
                                                                            Emergency</label>
                                                                        <div class="col-sm-8">
                                                                            <asp:TextBox ID="txtDonorName" MaxLength="50" autocomplete="off" ondrop="return false;"
                                                                                runat="server" class="form-control" />
                                                                            <span class="reqfield">
                                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" Display="Dynamic"
                                                                                    ValidationGroup="saves" ControlToValidate="txtDonorName" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                                                                            </span>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="col-lg-4 col-md-4  col-sm-4  cpl-xs-12" runat="server" id="divE3">
                                                                    <div class="form-group">
                                                                        <label class="control-label col-sm-4" for="Name">
                                                                            Period
                                                                        </label>
                                                                        <div class="col-sm-8">
                                                                            <asp:DropDownList ID="ddlPeriod" runat="server" class="form-control">

                                                                                <asp:ListItem Selected="True" Value="0">--Select--</asp:ListItem>
                                                                                <asp:ListItem Value="1">One Time </asp:ListItem>
                                                                                <asp:ListItem Value="2">Daily</asp:ListItem>
                                                                                <asp:ListItem Value="3">Weekly</asp:ListItem>
                                                                                <asp:ListItem Value="4">Fortnightly</asp:ListItem>
                                                                                <asp:ListItem Value="5">Monthly</asp:ListItem>

                                                                            </asp:DropDownList>
                                                                            <span style="width: 5px; float: right; margin: -27px 27px; font-size: 21px;">
                                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" InitialValue="0" runat="server"
                                                                                    Display="Dynamic" ValidationGroup="saves" ControlToValidate="ddlPeriod" ErrorMessage="*"
                                                                                    ForeColor="Red"></asp:RequiredFieldValidator>
                                                                            </span>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="col-lg-4 col-md-4  col-sm-4  cpl-xs-12" runat="server" id="divE4">
                                                                    <div class="form-group">
                                                                        <label class="control-label col-sm-4" for="Name">
                                                                            Active Date
                                                                        </label>
                                                                        <div class="col-sm-8">
                                                                            <asp:TextBox runat="server" ID="txtFromDate" OnTextChanged="txtdatefrom_TextChanged"
                                                                                AutoPostBack="true" autocomplete="off" ondrop="return false;" class="form-control"
                                                                                onkeypress="return false;"></asp:TextBox>
                                                                            <ajax:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True" Format="dd/MM/yyyy"
                                                                                TargetControlID="txtFromDate" PopupPosition="BottomRight">
                                                                            </ajax:CalendarExtender>
                                                                            <span class="reqfield">
                                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="Dynamic"
                                                                                    ValidationGroup="saves" ControlToValidate="txtFromDate" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                                                                            </span>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="col-lg-4 col-md-4  col-sm-4  cpl-xs-12" runat="server" id="divE5">
                                                                    <div class="form-group">
                                                                        <label class="control-label col-sm-4" for="Name">
                                                                            End Date
                                                                        </label>
                                                                        <div class="col-sm-8">
                                                                            <asp:TextBox runat="server" ID="txtTodate" OnTextChanged="txtTodate_TextChanged"
                                                                                AutoPostBack="true" autocomplete="off" ondrop="return false;" class="form-control"
                                                                                onkeypress="return false;"></asp:TextBox>
                                                                            <ajax:CalendarExtender ID="CalendarExtender2" runat="server" Enabled="True" Format="dd/MM/yyyy"
                                                                                TargetControlID="txtTodate" PopupPosition="BottomRight">
                                                                            </ajax:CalendarExtender>
                                                                            <span class="reqfield">
                                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" Display="Dynamic"
                                                                                    ValidationGroup="saves" ControlToValidate="txtTodate" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                                                                            </span>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </asp:Panel>
                                                            <asp:Panel ID="dsdsf" runat="server" Enabled="true">
                                                                <div class="col-lg-4 col-md-4  col-sm-4  cpl-xs-12" runat="server" visible="false">
                                                                    <div class="form-group">
                                                                        <label class="control-label col-sm-4" for="Name">
                                                                            Status
                                                                        </label>
                                                                        <div class="col-sm-8">
                                                                            <asp:DropDownList ID="ddlStatus" runat="server" class="form-control">
                                                                            </asp:DropDownList>
                                                                            <span style="width: 5px; float: right; margin: -27px 27px; font-size: 21px;">
                                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator9" InitialValue="0" runat="server"
                                                                                    Display="Dynamic" ValidationGroup="saves" ControlToValidate="ddlStatus" ErrorMessage="*"
                                                                                    ForeColor="Red"></asp:RequiredFieldValidator>
                                                                            </span>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </asp:Panel>

                                                        </div>
                                                    </fieldset>
                                                </div>
                                            </div>


                                        </div>

                                    </div>
                                    <div class="row" style="padding: 10px 10px 10px 10px; border: 1px solid #ddd; border-radius: 4px;">
                                        <asp:GridView ID="GVMain" runat="server" Width="100%" AllowPaging="true" PageSize="40"
                                            BorderStyle="None" DataKeyNames="DID" OnRowDataBound="gvStaffScheduling_OnRowCommand" OnRowCommand="GVMain_OnRowCommand" GridLines="None"
                                            AutoGenerateColumns="false">
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
                                                <asp:ButtonField HeaderText="Level" ItemStyle-ForeColor="#333" DataTextField="Level"
                                                    CommandName="GVUIO">
                                                    <ItemStyle CssClass="padding-lef" Height="60px" />
                                                    <HeaderStyle CssClass="padding-lef" />
                                                </asp:ButtonField>
                                                <asp:ButtonField HeaderText="State Name" ItemStyle-ForeColor="#333" DataTextField="StateName"
                                                    CommandName="GVUIO">
                                                    <ItemStyle CssClass="padding-lef" Height="60px" />
                                                    <HeaderStyle CssClass="padding-lef" />
                                                </asp:ButtonField>

                                                <asp:ButtonField HeaderText="District Name" ItemStyle-ForeColor="#333" DataTextField="DistrictName"
                                                    CommandName="GVUIO">
                                                    <ItemStyle CssClass="padding-lef" Height="60px" />
                                                    <HeaderStyle CssClass="padding-lef" />
                                                </asp:ButtonField>
                                                <asp:ButtonField HeaderText="BlockName" ItemStyle-ForeColor="#333" DataTextField="BlockName"
                                                    CommandName="GVUIO">
                                                    <ItemStyle CssClass="padding-lef" Height="20px" />
                                                    <HeaderStyle CssClass="padding-lef" />
                                                </asp:ButtonField>
                                                <asp:ButtonField HeaderText="Emergency Name" ItemStyle-ForeColor="#333" DataTextField="EmergencyName"
                                                    CommandName="GVUIO">
                                                    <ItemStyle CssClass="padding-lef" Height="20px" />
                                                    <HeaderStyle CssClass="padding-lef" />
                                                </asp:ButtonField>
                                                <asp:ButtonField HeaderText="Module Active Date" ItemStyle-ForeColor="#333" DataTextField="ModuleActiveDate"
                                                    CommandName="GVUIO">
                                                    <ItemStyle CssClass="padding-lef" Height="20px" />
                                                    <HeaderStyle CssClass="padding-lef" />
                                                </asp:ButtonField>
                                                <asp:ButtonField HeaderText="Module End Date" ItemStyle-ForeColor="#333" DataTextField="ModuleEndDate"
                                                    CommandName="GVUIO">
                                                    <ItemStyle CssClass="padding-lef" Height="20px" />
                                                    <HeaderStyle CssClass="padding-lef" />
                                                </asp:ButtonField>
                                                <asp:ButtonField HeaderText="Period" ItemStyle-ForeColor="#333" DataTextField="Period"
                                                    CommandName="GVUIO">
                                                    <ItemStyle CssClass="padding-lef" Height="20px" />
                                                    <HeaderStyle CssClass="padding-lef" />
                                                </asp:ButtonField>
                                                <asp:TemplateField HeaderText="Status">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkLock" OnClick="btnLnk_Click" runat="server" />
                                                        <asp:Label ID="lblScheduleID" Visible="false" runat="server" Text='<%# Bind("DID") %>'></asp:Label>
                                                        <asp:Label ID="lblLockRecord" Visible="false" runat="server" Text='<%# Bind("ActiveStatus") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle Width="10%" />
                                                </asp:TemplateField>
                                            </Columns>


                                        </asp:GridView>
                                    </div>

                                </div>
                                <!-- /#page-content-wrapper -->

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
            <asp:PostBackTrigger ControlID="LinkddButton1" />

        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
