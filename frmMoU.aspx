<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    Culture="en-GB" CodeFile="frmMoU.aspx.cs" Inherits="frmMoU" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script type="text/javascript">
        $(document).ready(function () {
            $(document).data('is_show', true);
            $('#x').removeClass("wappx");
            $('#x').addClass("wappx1").css("margin-left", "20.5%");
            $("#left-pln").click(function (e) {
                e.preventDefault();
                if ($(document).data('is_show') == true) {
                    $('#div-show-left').hide(1000);
                    $(document).data('is_show', false);
                    $('.left-butt').attr("src", "Images/add-29.png");
                    $('#x').removeClass("wappx1");
                    $('#x').addClass("wappx").css("margin-left", "1%"); ;

                } else {
                    $('#div-show-left').show(1000);
                    $(document).data('is_show', true);
                    $('.left-butt').attr("src", "Images/close-29.png");
                    $('#x').removeClass("wappx");
                    $('#x').addClass("wappx1").css("margin-left", "20.5%");
                }
            });
        });
    </script>
    <style type="text/css">
        .wappx
        {
            float: left;
            width: 98%;
            margin-left: 1%;
            padding: 0px;
            border: 0px solid #000;
            position: relative;
            top: 0px !important;
        }
        .wappx1
        {
            float: left;
            width: 79%;
            margin-left: 1%;
            padding: 0px;
            border: 0px solid #000;
            position: relative;
            top: 2px !important;
        }
    </style>
    <%-- <style type="text/css">
        .WordWrap {
            width: 100%;
            word-break: break-all;
        }
    </style>--%>
    <style type="text/css">
        .modalBackground
        {
            position: fixed;
            background-color: #000;
            filter: alpha(opacity=50);
            opacity: 0.5;
        }
    </style>
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
    <style type="text/css">
        .Grid th
        {
            color: #333333;
            background-color: #C1C1C1;
        }
        
        /* CSS to change the GridLines color */
        .Grid, .Grid th, .Grid td
        {
            border: 1px solid #F1F1F1 !important;
            padding: 5px;
        }
    </style>
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
    <style type="text/css">
        .ajax__calendar_container
        {
            z-index: 1000;
        }
    </style>
    <script type="text/javascript">
        function checkDate() {
            debugger;
            var startDate = $('#<%=txtStartDate.ClientID %>').val();
            var EndDate = $('#<%= TxtEndDate.ClientID %>').val();
            var result = startDate.indexOf("-");
            var result1 = startDate.indexOf("/");
            var result2 = EndDate.indexOf("-");
            var result3 = EndDate.indexOf("/");
            if (EndDate != null) {
                var myDate_array;
                var myDate_array1;
                if (Number(result) > 0) {
                    myDate_array = startDate.split("-");
                    myDate_array1 = EndDate.split("-");

                }
                else {
                    myDate_array = startDate.split("/");
                    myDate_array1 = EndDate.split("/");


                }

                var StDate = new Date(myDate_array[2], parseInt(myDate_array[1]) - 1, myDate_array[0]);
                var EdDate = new Date(myDate_array1[2], parseInt(myDate_array1[1]) - 1, myDate_array1[0]);




                if (StDate > EdDate) {
                    alert("End Date Should not be Greater then Start Date.");
                    $('#<%= TxtEndDate.ClientID %>').val('');
                    document.getElementById("" + sender + "").value = null;
                    return false;
                }
            }


        }
        function checkDate1() {
            debugger;
            var startDate = $('#<%=txtStartDate.ClientID %>').val();
            var EndDate = $('#<%=TxtEndDate.ClientID %>').val();
            var PopDate = $('#<%=TxtDatePopup.ClientID %>').val();
            var result = startDate.indexOf("-");
            if (PopDate != null) {

                var myDate_array;
                var myDate_array1;
                var myDate_array2;
                if (Number(result) > 0) {
                    myDate_array = startDate.split("-");
                    myDate_array1 = EndDate.split("-");
                    myDate_array2 = PopDate.split("-");

                }
                else {
                    myDate_array = startDate.split("/");
                    myDate_array1 = EndDate.split("/");
                    myDate_array2 = PopDate.split("/");


                }

                var StDate = new Date(myDate_array[2], parseInt(myDate_array[1]) - 1, myDate_array[0]);
                var EdDate = new Date(myDate_array1[2], parseInt(myDate_array1[1]) - 1, myDate_array1[0]);
                var PODate = new Date(myDate_array2[2], parseInt(myDate_array2[1]) - 1, myDate_array2[0]);




                if (PODate < StDate) {
                    alert("Meeting Date must be between start date and end date");
                    $('#<%= TxtDatePopup.ClientID %>').val('');

                }
                if (PODate > EdDate) {
                    alert("Meeting Date must be between start date and end date");
                    $('#<%= TxtDatePopup.ClientID %>').val('');

                }
            }
        }
        
    </script>
    <script type="text/javascript">
        function ConfirmMessage() {
            var selectedvalue = confirm("Do you want to Delete data?");
            if (selectedvalue) {
                document.getElementById('<%=txtconformmessageValue.ClientID %>').value = "Yes";
            } else {
                document.getElementById('<%=txtconformmessageValue.ClientID %>').value = "No";
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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="container-fluid">
        <input type="image" id="left-pln" class="left-butt" src="Images/close-29.png" />
        <div id="div-show-left">
            <div style="overflow: auto">
                <asp:GridView ID="GV_MOU_Main" runat="server" AutoGenerateColumns="false" Width="100%"
                    CssClass="Grid" AllowPaging="true" PageSize="40" BorderStyle="None" DataKeyNames="GR_UID"
                    GridLines="None">
                    <EmptyDataTemplate>
                        <div style="font-family: Arial; font-size: 12px; font-weight: bold; color: Red;">
                            Data not found
                        </div>
                    </EmptyDataTemplate>
                    <FooterStyle CssClass="FooterStyle" />
                    <HeaderStyle BackColor="#C1C1C1" ForeColor="White" Height="44px" />
                    <RowStyle HorizontalAlign="Left" />
                    <SelectedRowStyle BackColor="#fad669" Font-Bold="True" ForeColor="WhiteSmoke" />
                    <AlternatingRowStyle BackColor="#f1f1f1" />
                    <Columns>
                        <asp:TemplateField HeaderText="District Name" HeaderStyle-CssClass="GridHeaderClass">
                            <ItemTemplate>
                                <asp:LinkButton ID="lblDistrictName" runat="server" Text='<%#Eval("DistrictName") %>'
                                    OnClick="lblDistrictName_OnClick"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Start Date" HeaderStyle-CssClass="GridHeaderClass">
                            <ItemTemplate>
                                <asp:LinkButton ID="lblStartDate" runat="server" Text='<%#Eval("StartDate") %>' OnClick="lblDistrictName_OnClick"></asp:LinkButton>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="End Date" HeaderStyle-CssClass="GridHeaderClass">
                            <ItemTemplate>
                                <asp:LinkButton ID="lblEndDate" runat="server" Text='<%#Eval("EndDate") %>' OnClick="lblDistrictName_OnClick"></asp:LinkButton>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
        <div id="x" class="wappx">
            <div class="row">
                <div class="col-lg-12">
                    <div class="panel panel-default" style="height: 625px;">
                        <div class="panel-heading">
                            <div class="row">
                                <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                                    <h3 class="text-danger" style="margin: 0px;">
                                        <asp:Label ID="lblMain" runat="server" Text="Government Relations"></asp:Label>
                                    </h3>
                                </div>
                                <div class="col-lg-6 col-md-6 col-sm-6 " style="padding: 0px">
                                    <input type="image" id="ton-new" class="butt" src="Images/search-not-29.png" title="Search" />
                                    <asp:ImageButton ID="btnDelete" OnClick="btnDelete_Click" CssClass="btn btn-info pull-right"
                                        Visible="false" ToolTip="Delete" BackColor="#f5f5f5" ImageUrl="~/images/delete-29.png"
                                        Style="margin-right: 5px; padding: 0px;" runat="server" />
                                    <asp:ImageButton ID="btnsave" CssClass="btn btn-info pull-right" BackColor="#f5f5f5"
                                        ValidationGroup="saves" ToolTip="Save" ImageUrl="~/images/save-29-1.png" OnClick="btnsave_Click"
                                        Style="margin-right: 5px; padding: 0px;" runat="server" />
                                    <asp:ImageButton ID="btnAdd" CssClass="btn btn-info pull-right" BackColor="#f5f5f5"
                                        ToolTip="Add" ImageUrl="~/images/add-29-1.png" OnClick="btnAdd_Click" Style="margin-right: 5px;
                                        padding: 0px;" runat="server" />
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div id="div-show-new">
                                <div class="row marg search-bg">
                                    <div class="form-horizontal">
                                        <div class="col-lg-3 col-md-3 col-sm-3 cpl-xs-12">
                                            <div class="form-group">
                                                <label for="email" class="col-sm-3 padd linhei" style="padding-top: 2px;">
                                                    State:</label>
                                                <div class="col-sm-9">
                                                    <asp:DropDownList ID="ddl_State" runat="server" class="form-control " AutoPostBack="true"
                                                        OnSelectedIndexChanged="ddlState_SelectedIndexChanged" />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-lg-3 col-md-3 col-sm-3 cpl-xs-12">
                                            <div class="form-group">
                                                <label for="email" class="col-sm-3 padd linhei" style="padding-top: 2px;">
                                                    District:</label>
                                                <div class="col-sm-9 padd">
                                                    <asp:DropDownList ID="ddl_District" runat="server" class="form-control " />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-lg-1 col-md-1 col-sm-2 cpl-xs-12 col-lg-offset-5 col-md-offset-5 col-sm-offset-5 col-xs-offset-0">
                                            <asp:ImageButton ID="btnSerach" ToolTip="Serach" runat="server" class="btn btn-danger btn-paddd pull-right"
                                                BackColor="#f1f1f1" OnClick="btnSerach_Click" ImageUrl="~/images/search-29.png" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div style="margin-left: 15px; margin-right: 12px;">
                                <div class="row marg search-bg">
                                    <div class="form-horizontal">
                                   
                                        <div class="col-lg-3 col-md-3 col-sm-3 cpl-xs-12">
                                            <div class="form-group">
                                                <label for="email" class="col-sm-5 padd linhei" style="padding-top: 2px;">
                                                    Start Date:</label>
                                                <div class="col-sm-7 padd">
                                                    <asp:TextBox runat="server" ID="txtStartDate" autocomplete="off" ondrop="return false;"
                                                        class="form-control c" onkeypress="return false;"></asp:TextBox>
                                                    <ajax:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True" Format="dd/MM/yyyy"
                                                        TargetControlID="txtStartDate" PopupPosition="BottomRight">
                                                    </ajax:CalendarExtender>
                                                    <asp:RequiredFieldValidator ID="ReqTxtDate" runat="server" ControlToValidate="txtStartDate"
                                                        Display="Dynamic" ErrorMessage="*" Font-Bold="False" Font-Size="9px" ForeColor="Red"
                                                        SetFocusOnError="True" ValidationGroup="saves"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-lg-3 col-md-3 col-sm-3 cpl-xs-12">
                                            <div class="form-group">
                                                <label for="email" class="col-sm-3 padd linhei" style="padding-top: 2px;">
                                                    End Date:</label>
                                                <div class="col-sm-9 padd">
                                                    <asp:TextBox ID="TxtEndDate" runat="server" onchange="checkDate()" class="form-control c" />
                                                    <ajax:CalendarExtender ID="CalendarExtender2" runat="server" Enabled="True" Format="dd/MM/yyyy"
                                                        TargetControlID="TxtEndDate" PopupPosition="BottomRight">
                                                    </ajax:CalendarExtender>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TxtEndDate"
                                                        Display="Dynamic" ErrorMessage="*" Font-Bold="False" Font-Size="9px" ForeColor="Red"
                                                        SetFocusOnError="True" ValidationGroup="saves"></asp:RequiredFieldValidator>
                                                    <%--  <asp:CompareValidator ControlToCompare="txtStartDate" ControlToValidate="TxtEndDate"
                                                        Display="Dynamic" ErrorMessage="*" ID="CompareValidator1" Operator="GreaterThan"
                                                        Type="Date" runat="server" />--%>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-lg-3 col-md-3 col-sm-3 cpl-xs-12">
                                            <div class="form-group">
                                                <label for="email" class="col-sm-5 padd linhei" style="padding-top: 2px;">
                                                    MoU Upload:</label>
                                                <div class="col-sm-6 padd">
                                                    <asp:FileUpload ID="FileUpload_Mou" Width="78px" runat="server" Font-Overline="false"
                                                        ForeColor="#333333" />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-lg-2 col-md-2 col-sm-2 cpl-xs-12" style="padding-left: 5px;">
                                            <asp:LinkButton ID="lnkUpload" runat="server" Text="Download" ForeColor="#7B97B0"
                                                OnClick="btnDownload_Click" Visible="false" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <asp:UpdatePanel ID="ChildPanel" runat="server">
                            <ContentTemplate>
                                <div class="panel panel-default" style="height: 200px; margin-left: 15px; margin-right: 10px;">
                                    <div class="panel-heading">
                                        <div class="row">
                                            <div class="col-lg-4 col-md-4 col-sm-4 col-xs-12">
                                                <h4 class="text-danger" style="margin: 0px;">
                                                    <asp:Label ID="Label2" runat="server" Text="Representatives"></asp:Label>
                                                </h4>
                                            </div>
                                            <div class="col-lg-8 col-md-8 col-sm-8 col-xs-12">
                                                <asp:Button ID="BtnAddparticipant" runat="server" Visible="false" Text="Add Representatives"
                                                    class="btn btn-danger btn-paddd pull-right" OnClick="BtnAddparticipant_Click" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row mar ">
                                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="padding-top: 6px;">
                                            <div style="max-height: 150px; overflow: auto;">
                                                <asp:GridView ID="GV_Display" runat="server" AutoGenerateColumns="false" DataKeyNames="GRRep_UID"
                                                    OnRowDataBound="GV_Display_OnRowDataBound" CssClass="Grid" Width="100%">
                                                    <HeaderStyle BackColor="#C1C1C1" ForeColor="White" Height="44px" />
                                                    <RowStyle HorizontalAlign="Left" />
                                                    <SelectedRowStyle BackColor="#fad669" Font-Bold="True" ForeColor="WhiteSmoke" />
                                                    <AlternatingRowStyle BackColor="#f1f1f1" />
                                                    <HeaderStyle BackColor="#C1C1C1" ForeColor="White" HorizontalAlign="Center" />
                                                    <SelectedRowStyle BackColor="#fad669" Font-Bold="True" ForeColor="WhiteSmoke" />
                                                    <AlternatingRowStyle BackColor="#f1f1f1" />
                                                    <EmptyDataTemplate>
                                                    </EmptyDataTemplate>
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Row Number" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="Lnk_Number" runat="server" Text='<%#Bind("GRRep_UID") %>'>
                                                                </asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Level" HeaderStyle-CssClass="GridHeaderClass">
                                                            <ItemTemplate>
                                                                <asp:DropDownList ID="ddlFirstLevel" runat="server" CssClass="form-control">
                                                                </asp:DropDownList>
                                                                <asp:HiddenField ID="HdnFirstLevel" runat="server" Value='<%# Bind ("Level") %>' />
                                                            </ItemTemplate>
                                                            <HeaderStyle Width="20%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Designation" HeaderStyle-CssClass="GridHeaderClass">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblDesignation" runat="server" Text='<%# Bind("Designation") %>'>
                                                                </asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle Width="19%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Name" HeaderStyle-CssClass="GridHeaderClass">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_Name" runat="server" Text='<%# Bind("Name") %>'>
                                                                </asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle Width="19%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Contact No." HeaderStyle-CssClass="GridHeaderClass">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_Phone_No" runat="server" MaxLength="10" Text='<%# Bind("PhoneNo") %>'>
                                                                </asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle Width="13%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="E-mail" HeaderStyle-CssClass="GridHeaderClass">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_E_mail" runat="server" Text='<%# Bind("Email") %>'>
                                                                </asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle Width="21%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:ImageButton ID="Btn_Edit" runat="server" ImageUrl="~/images/edit.png" OnClick="Btn_Edit_OnClick">
                                                                </asp:ImageButton>
                                                                <asp:ImageButton ID="Btn_Delete" runat="server" ImageUrl="~/images/delete-29.png"
                                                                    OnClick="Btn_Delete_OnClick" OnClientClick="javascript:ConfirmMessage();"></asp:ImageButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </ContentTemplate>
                            <%-- <Triggers>
                            <asp:PostBackTrigger ControlID="BtnAddparticipant" />
                            </Triggers>--%>
                        </asp:UpdatePanel>
                        <asp:UpdatePanel ID="pnlLeftRight" runat="server">
                            <ContentTemplate>
                                <div class="panel panel-default" style="height: 262; margin-left: 15px; margin-right: 10px;">
                                    <div class="panel-heading">
                                        <div class="row">
                                            <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                                                <h4 class="col-lg-5 text-danger" style="margin: 0px;">
                                                    <asp:Label ID="Label1" runat="server" Text="Meetings"></asp:Label>
                                                </h4>
                                                <asp:Button ID="BtnAddTasKforce" runat="server" Visible="false" Text="Add Meetings"
                                                    Width="21%" class="col-lg-2 btn btn-danger btn-paddd pull-right" OnClick="AddTasKforce_Click" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row mar ">
                                        <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12" style="padding-top: 6px;">
                                            <div style="max-height: 150px; overflow: auto;">
                                                <asp:GridView ID="GV_TaskForce_Left" runat="server" DataKeyNames="GRMtg_UID" AutoGenerateColumns="false"
                                                    OnRowDataBound="GV_TaskForce_Left_OnRowDataBound" CssClass="Grid" Width="100%">
                                                    <HeaderStyle BackColor="#C1C1C1" ForeColor="White" Height="44px" />
                                                    <RowStyle HorizontalAlign="Left" />
                                                    <SelectedRowStyle BackColor="#fad669" Font-Bold="True" ForeColor="WhiteSmoke" />
                                                    <AlternatingRowStyle BackColor="#f1f1f1" />
                                                    <HeaderStyle BackColor="#C1C1C1" ForeColor="White" HorizontalAlign="Center" />
                                                    <SelectedRowStyle BackColor="#fad669" Font-Bold="True" ForeColor="WhiteSmoke" />
                                                    <AlternatingRowStyle BackColor="#f1f1f1" />
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Row Number" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="Lnk_RowNumber" runat="server" OnClick="Lnk_RowNumber_OnClick" Text='<%#Bind("GRMtg_UID") %>'>
                                                                </asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Date" HeaderStyle-CssClass="GridHeaderClass">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="Txt_Date" runat="server" Text='<%#Bind("Date") %>'>
                                                                </asp:LinkButton>
                                                            </ItemTemplate>
                                                            <HeaderStyle Width="21%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Minutes" HeaderStyle-CssClass="GridHeaderClass">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="Txt_Minutes" OnClick="Lnk_Click" runat="server" Text='<%#Bind("Minutes") %>'>
                                                                </asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Action" HeaderStyle-CssClass="GridHeaderClass">
                                                            <ItemTemplate>
                                                                <asp:ImageButton ID="BtnImageEdit" runat="server" ImageUrl="~/images/edit.png" OnClick="BtnImageEdit_OnClick">
                                                                </asp:ImageButton>
                                                                <asp:ImageButton ID="BTnImagDelete" runat="server" ImageUrl="~/images/delete-29.png"
                                                                    OnClick="BtnImageDelete_OnClick" OnClientClick="javascript:ConfirmMessage();">
                                                                </asp:ImageButton>
                                                            </ItemTemplate>
                                                            <HeaderStyle Width="21%" />
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </div>
                                        <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12" style="padding-top: 6px;">
                                            <div style="max-height: 150px; overflow: auto;">
                                                <asp:GridView ID="GV_TaskForce_Right" runat="server" AutoGenerateColumns="false"
                                                    DataKeyNames="GRMtgAction_UID" CssClass="Grid" OnRowDataBound="Gv_TaskForce_Right1_OnRowDataBound"
                                                    Width="100%">
                                                    <HeaderStyle BackColor="#C1C1C1" ForeColor="White" Height="44px" />
                                                    <RowStyle HorizontalAlign="Left" />
                                                    <SelectedRowStyle BackColor="#fad669" Font-Bold="True" ForeColor="WhiteSmoke" />
                                                    <AlternatingRowStyle BackColor="#f1f1f1" />
                                                    <HeaderStyle BackColor="#C1C1C1" ForeColor="White" HorizontalAlign="Center" />
                                                    <SelectedRowStyle BackColor="#fad669" Font-Bold="True" ForeColor="WhiteSmoke" />
                                                    <AlternatingRowStyle BackColor="#f1f1f1" />
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Row Number" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="Lnk_RowNumber" runat="server" Text='<%#Bind("GRMtg_UID") %>'>
                                                                </asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Action Point" HeaderStyle-CssClass="GridHeaderClass">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_Date" runat="server" Text='<%#Bind("ActionPoint") %>'>
                                                                </asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Status" HeaderStyle-CssClass="GridHeaderClass">
                                                            <ItemTemplate>
                                                                <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control">
                                                                </asp:DropDownList>
                                                                <asp:HiddenField ID="hdnStatus" runat="server" Value='<%# Bind ("Status") %>'></asp:HiddenField>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Action" HeaderStyle-CssClass="GridHeaderClass">
                                                            <ItemTemplate>
                                                                <asp:ImageButton ID="BtnRight" runat="server" ImageUrl="~/images/delete-29.png" OnClick="BtnRightDel_Click"
                                                                    OnClientClick="javascript:ConfirmMessage();"></asp:ImageButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    </div>
                </div>
            </div>
        </div>
        <ajax:ModalPopupExtender ID="ModalPopupExtender" runat="server" TargetControlID="Hdn_model3"
            PopupControlID="pnlpopup3" BackgroundCssClass="modalBackground" CancelControlID="btn_cancelparDetail">
        </ajax:ModalPopupExtender>
        <ajax:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="Hdn_model4"
            PopupControlID="PnlPopupDIV" BackgroundCssClass="modalBackground" CancelControlID="BtnCancelTask">
        </ajax:ModalPopupExtender>
        <asp:HiddenField ID="Hdn_model3" runat="server" />
        <asp:HiddenField ID="Hdn_model4" runat="server" />
        <div id="pnlpopup3" runat="server" cssclass="modal fade ">
            <asp:UpdatePanel ID="PnlPopup" runat="server">
                <ContentTemplate>
                    <div class="modal-dialog modal-md" style="width: 100%">
                        <div class="modal-content">
                            <div class="modal-header" style="background: #F5F5F5; padding: 4px;">
                                <asp:Label ID="LblHeader" Text="Representatives" Font-Bold="true" ForeColor="#A94442"
                                    runat="server" for="Name"></asp:Label>
                                <asp:ImageButton ID="btn_cancelparDetail" CssClass="btn btn-info pull-right" BackColor="#f5f5f5"
                                    ToolTip="Add" ImageUrl="~/images/close-29.png" Style="margin-right: 5px; padding: 0px;"
                                    runat="server" />
                                <asp:ImageButton ID="btnsavePopup" CssClass="btn btn-info pull-right" OnClick="btnsavePopup_OnClick"
                                    ValidationGroup="vgSubmit" BackColor="#f5f5f5" ToolTip="Save" ImageUrl="~/images/save-29-1.png"
                                    Style="margin-right: 5px; padding: 0px;" runat="server" />
                                <asp:ImageButton ID="ButtonAdd" CssClass="btn btn-info pull-right" BackColor="#f5f5f5"
                                    ToolTip="Add" ImageUrl="~/images/add-29-1.png" OnClick="ButtonAdd_Click1" Style="margin-right: 5px;
                                    padding: 0px;" runat="server" />
                            </div>
                            <div class="modal-body" style="padding: 5px;">
                                <div style="max-height: 150px; overflow: auto;">
                                    <asp:GridView ID="GV_MOU" runat="server" AutoGenerateColumns="false" CssClass="Grid"
                                        Width="100%" OnRowDataBound="GV_MOU_RowDataBound">
                                        <EmptyDataTemplate>
                                        </EmptyDataTemplate>
                                        <HeaderStyle BackColor="#C1C1C1" ForeColor="White" Height="44px" />
                                        <RowStyle HorizontalAlign="Left" />
                                        <SelectedRowStyle BackColor="#fad669" Font-Bold="True" ForeColor="WhiteSmoke" />
                                        <AlternatingRowStyle BackColor="#f1f1f1" />
                                        <HeaderStyle BackColor="#C1C1C1" ForeColor="White" HorizontalAlign="Center" />
                                        <SelectedRowStyle BackColor="#fad669" Font-Bold="True" ForeColor="WhiteSmoke" />
                                        <AlternatingRowStyle BackColor="#f1f1f1" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Level" HeaderStyle-CssClass="GridHeaderClass">
                                                <ItemTemplate>
                                                    <asp:DropDownList ID="ddlFirstLevel" runat="server" CssClass="form-control">
                                                    </asp:DropDownList>
                                                    <asp:HiddenField ID="hdnFirstLevel" runat="server" Value='<%#Bind("Level")%>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Designation" HeaderStyle-CssClass="GridHeaderClass">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="Txt_Designation" BorderStyle="None" MaxLength="30" runat="server"
                                                        CssClass="form-control" onkeypress="return onlyAlphabets(event,this);" Text='<%# Bind("Designation") %>'>
                                                    </asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Name" HeaderStyle-CssClass="GridHeaderClass">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="Txt_Name" runat="server" onkeypress="return onlyAlphabets(event,this);"
                                                        BorderStyle="None" MaxLength="30" CssClass="form-control" Text='<%# Bind("Name") %>'>
                                                    </asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Contact No." HeaderStyle-CssClass="GridHeaderClass">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="Txt_Phone_No" runat="server" BorderStyle="None" MaxLength="10" CssClass="form-control"
                                                        Text='<%# Bind("PhoneNo") %>' onkeypress="return isNumberKey(this,event);" OnKeyUp="javascript:inputtxt();"
                                                        onchange="javascript: phonenumber(this.value,'TeContact1');">
                                                    </asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="E-mail" HeaderStyle-CssClass="GridHeaderClass">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="Txt_E_mail" runat="server" BorderStyle="None" MaxLength="50" CssClass="form-control"
                                                        Text='<%# Bind("Email") %>'>

                                                    </asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="rfvEmail" runat="server" ErrorMessage="*" ControlToValidate="Txt_E_mail"
                                                        Display="Dynamic" ValidationGroup="vgSubmit" ForeColor="Red"></asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="Please Enter Valid Email ID"
                                                        Display="Dynamic" ValidationGroup="vgSubmit" ControlToValidate="Txt_E_mail" CssClass="requiredFieldValidateStyle"
                                                        ForeColor="Red" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">
                                                    </asp:RegularExpressionValidator>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Action" HeaderStyle-CssClass="GridHeaderClass">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="BTnImagDelete" runat="server" ImageUrl="~/images/delete-29.png"
                                                        OnClick="btn_Delete" OnClientClick="javascript:ConfirmMessage();"></asp:ImageButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
                <Triggers>
                    <asp:PostBackTrigger ControlID="btn_cancelparDetail" />
                </Triggers>
            </asp:UpdatePanel>
        </div>
        <div id="PnlPopupDIV" runat="server" cssclass="modal fade ">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <div class="modal-dialog modal-md" style="width: 100%">
                        <div class="modal-content">
                            <div class="modal-header" style="background: #F5F5F5; padding: 4px;">
                                <asp:Label ID="Label3" Text="Add Task Force" runat="server" for="Name" Font-Bold="true"
                                    ForeColor="#A94442"></asp:Label>
                                <asp:ImageButton ID="BtnCancelTask" CssClass="btn btn-info pull-right" BackColor="#f5f5f5"
                                    ToolTip="Add" ImageUrl="~/images/close-29.png" Style="margin-right: 5px; padding: 0px;"
                                    runat="server" />
                                <asp:ImageButton ID="BtnSaveTask" CssClass="btn btn-info pull-right" OnClick="BtnSaveTask_OnClick"
                                    ValidationGroup="saves1" BackColor="#f5f5f5" ToolTip="Save" ImageUrl="~/images/save-29-1.png"
                                    Style="margin-right: 5px; padding: 0px;" runat="server" />
                                <asp:ImageButton ID="Button_Add1" CssClass="btn btn-info pull-right" BackColor="#f5f5f5"
                                    ToolTip="Add" ImageUrl="~/images/add-29-1.png" OnClick="ButtonAdd_Click3" Style="margin-right: 5px;
                                    padding: 0px;" runat="server" />
                            </div>
                            <div class="modal-body" style="padding: 5px; background-color: #F5F5F5;">
                                <div class="panel-heading" style="border: 1px solid #C1C1C1; height: 122px; box-shadow: 0px 2px 4px #545454;">
                                    <div class="col-lg-4 col-md-4 col-sm-4 col-xs-12" style="padding-left: 0px;">
                                        <div class="form-group">
                                            <asp:Label ID="Lbl_Date" runat="server" Text="Date:"></asp:Label>
                                            <span style="float: right">
                                                <asp:TextBox ID="TxtDatePopup" CssClass="form-control" runat="server"></asp:TextBox>
                                                <ajax:CalendarExtender ID="CalendarExtender3" runat="server" Enabled="True" Format="dd/MM/yyyy"
                                                    OnClientDateSelectionChanged="arrivaldatecheck" TargetControlID="TxtDatePopup"
                                                    PopupPosition="BottomRight">
                                                </ajax:CalendarExtender>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="TxtDatePopup"
                                                    Display="Dynamic" ErrorMessage="*" Font-Bold="False" Font-Size="9px" ForeColor="Red"
                                                    SetFocusOnError="True" ValidationGroup="saves1"></asp:RequiredFieldValidator>
                                            </span>
                                        </div>
                                        <div class="form-group">
                                            <asp:Label ID="lblDBT" runat="server" Text="District Task force:"></asp:Label>
                                            <asp:DropDownList ID="ddlRBT" runat="server" CssClass="form-control">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12 ">
                                        <asp:Label ID="lbl_Minutes" runat="server" Text="Minutes:"></asp:Label>
                                        <span style="float: right; width: 100%;">
                                            <asp:TextBox ID="TxtMinutes" Width="100%" TextMode="MultiLine" Rows="3" runat="server"></asp:TextBox>
                                        </span>
                                    </div>
                                    <div class="col-lg-2 col-md-2 col-sm-2 col-xs-12 ">
                                        <div class="form-group">
                                            <asp:FileUpload ID="Mtg_Upload" runat="server" Width="78px" />
                                        </div>
                                        <div class="form-group" style="margin-left: 12px;">
                                            <asp:LinkButton ID="lnkMeetingDow" runat="server" Text="Download" Visible="false"
                                                OnClick="Meeting_Download"></asp:LinkButton>
                                        </div>
                                    </div>
                                </div>
                                <div style="max-height: 150px; overflow: auto; width: 100%; padding-top: 7px;">
                                    <asp:GridView ID="Gv_TaskForce_Right1" runat="server" AutoGenerateColumns="false"
                                        CssClass="Grid" OnRowDataBound="Gv_TaskForce_Right1_OnRowDataBound" ShowFooter="true"
                                        Width="100%">
                                        <HeaderStyle BackColor="#C1C1C1" ForeColor="White" Height="44px" />
                                        <RowStyle HorizontalAlign="Left" />
                                        <SelectedRowStyle BackColor="#fad669" Font-Bold="True" ForeColor="WhiteSmoke" />
                                        <AlternatingRowStyle BackColor="#f1f1f1" />
                                        <HeaderStyle BackColor="#C1C1C1" ForeColor="White" HorizontalAlign="Center" />
                                        <SelectedRowStyle BackColor="#fad669" Font-Bold="True" ForeColor="WhiteSmoke" />
                                        <AlternatingRowStyle BackColor="#f1f1f1" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="GRMtg UID" Visible="false">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="Lnk_RowNumber" runat="server" Text='<%#Bind("GRMtg_UID") %>'>
                                                    </asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Action Point" HeaderStyle-CssClass="GridHeaderClass">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="Txt_Date" runat="server" CssClass="form-control" Text='<%# Bind ("ActionPoint") %>'>
                                                    </asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Status " HeaderStyle-CssClass="GridHeaderClass">
                                                <ItemTemplate>
                                                    <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control">
                                                    </asp:DropDownList>
                                                    <asp:HiddenField ID="hdnStatus" runat="server" Value='<%# Bind ("Status") %>'></asp:HiddenField>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Action" HeaderStyle-CssClass="GridHeaderClass">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="BtnImagDel" runat="server" ImageUrl="~/images/delete-29.png"
                                                        OnClick="BtnImagDel_Click" OnClientClick="javascript:ConfirmMessage();"></asp:ImageButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
                <Triggers>
                    <asp:PostBackTrigger ControlID="BtnSaveTask" />
                    <asp:PostBackTrigger ControlID="lnkMeetingDow" />
                    <asp:PostBackTrigger ControlID="BtnCancelTask" />
                </Triggers>
            </asp:UpdatePanel>
            <asp:HiddenField ID="txtconformmessageValue" runat="server" />
        </div>
    </div>
</asp:Content>
