<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" Culture="en-GB"
    CodeFile="frmOfficeReport.aspx.cs" Inherits="frmOfficeReport" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <style type="text/css">
        .ajax__calendar_container { z-index: 1000; }

        .modalpopupcss, .modalPopup {
            background-color: #666699;
            filter: alpha(opacity=50);
            opacity: 0.7;
        }
        .modalPopup { z-index: 999999; }

        /* ---------- Filter / toolbar bar ---------- */
        .or-toolbar {
            display: flex;
            flex-wrap: wrap;
            align-items: flex-end;
            gap: 12px 16px;
            background: #f5f5f5;
            border: 1px solid #e3e3e3;
            border-radius: 4px;
            padding: 10px 14px;
            margin: 10px 0 14px;
        }
        .or-field { display: flex; flex-direction: column; min-width: 180px; flex: 0 1 220px; }
        .or-field label {
            font-size: 12px;
            font-weight: 600;
            color: #555;
            margin: 0 0 4px;
        }
        .or-field .form-control { height: 32px; padding: 4px 8px; font-size: 13px; }
        .or-field .req { color: #d9534f; font-size: 11px; }

        .or-actions {
            margin-left: auto;
            display: flex;
            align-items: center;
            gap: 6px;
        }
        .or-actions input[type=image] {
            width: 34px;
            height: 34px;
            padding: 4px !important;
            margin: 0 !important;
            border: 1px solid #ddd;
            border-radius: 4px;
            background: #fff !important;
            box-sizing: border-box;
        }
        .or-actions input[type=image]:hover { background: #eef6ff !important; border-color: #9cc4ec; }
        .or-actions .btn { height: 34px; margin: 0 !important; }

        /* ---------- Activity cards ---------- */
        .or-cards { display: flex; flex-wrap: wrap; margin: 0 -8px; }
        .or-cards > div { padding: 0 8px; margin-bottom: 16px; display: flex; }
        .or-card {
            flex: 1;
            background: #fff;
            border: 1px solid #ddd;
            border-radius: 4px;
            box-shadow: 0 1px 2px rgba(0,0,0,.05);
            display: flex;
            flex-direction: column;
        }
        .or-card-head {
            display: flex;
            align-items: center;
            justify-content: space-between;
            padding: 10px 14px;
            background: #fafafa;
            border-bottom: 1px solid #e5e5e5;
            border-radius: 4px 4px 0 0;
        }
        .or-card-head .or-title label {
            color: #a94442;
            font-weight: 700;
            font-size: 14px;
        }
        .or-card-head .or-sub label { font-weight: 600; color: #555; font-size: 12px; }
        .or-card-body { padding: 12px 14px; flex: 1; }

        .or-grid {
            display: grid;
            grid-template-columns: 1fr 1fr;
            gap: 10px 16px;
        }
        .or-grid .full { grid-column: 1 / -1; }

        /* asp:CheckBox renders <span><input/><label/></span> */
        .or-check { display: inline-flex; align-items: center; cursor: pointer; }
        .or-check input[type=checkbox] { margin: 0 6px 0 0; width: 15px; height: 15px; cursor: pointer; }
        .or-check label { margin: 0; font-weight: normal; font-size: 13px; color: #333; cursor: pointer; }

        .or-card textarea.form-control { resize: vertical; min-height: 90px; font-size: 13px; }
        .or-card input.form-control { height: 30px; font-size: 13px; }

        @media (max-width: 767px) {
            .or-actions { margin-left: 0; width: 100%; justify-content: flex-end; }
            .or-field { flex: 1 1 100%; }
            .or-grid { grid-template-columns: 1fr; }
        }
    </style>
    <script type="text/javascript">
        function arrivaldatecheck(sender, args) {
            var today = new Date();
            if (sender._selectedDate > today) {
                alert("Should not be future date.");
                sender._textbox.set_Value("");
                return false;
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:UpdatePanel ID="MainPanel" runat="server">
        <ContentTemplate>

            <!-- ===== Filter bar ===== -->
            <div class="or-toolbar">
                <div class="or-field">
                    <label for="<%= ddlUser.ClientID %>">FC</label>
                    <asp:DropDownList ID="ddlUser" OnSelectedIndexChanged="ddlUser_SelectedIndexChanged"
                        runat="server" AutoPostBack="true" CssClass="form-control">
                    </asp:DropDownList>
                </div>

                <div class="or-field">
                    <label for="<%= ddlVilage.ClientID %>">Village</label>
                    <asp:DropDownList ID="ddlVilage" OnSelectedIndexChanged="ddlVilage_SelectedIndexChanged"
                        runat="server" AutoPostBack="true" CssClass="form-control" />
                </div>

                <div class="or-field" style="flex-basis: 160px; min-width: 150px;">
                    <label for="<%= txtDate.ClientID %>">Date
                        <asp:RequiredFieldValidator ID="ReqTxtDate" runat="server" ControlToValidate="txtDate"
                            Display="Dynamic" ErrorMessage="* required" CssClass="req" ForeColor="#d9534f"
                            SetFocusOnError="True" ValidationGroup="saves"></asp:RequiredFieldValidator>
                    </label>
                    <asp:TextBox runat="server" ID="txtDate" autocomplete="off" ondrop="return false;"
                        CssClass="form-control" placeholder="dd/mm/yyyy" onkeypress="return false;"></asp:TextBox>
                    <ajax:CalendarExtender ID="CalendarExtenderTourdate" OnClientDateSelectionChanged="arrivaldatecheck"
                        runat="server" Enabled="True" Format="dd/MM/yyyy" TargetControlID="txtDate" PopupPosition="BottomRight">
                    </ajax:CalendarExtender>
                </div>

                <div class="or-actions">
                    <asp:ImageButton ID="btnEdit" ToolTip="Edit" OnClick="btnEdit_Click" runat="server"
                        ImageUrl="~/images/edit.png" />
                    <asp:ImageButton ID="btnSerach" OnClick="btnSerach_Click" ToolTip="Search" runat="server"
                        ImageUrl="~/images/search-29.png" />
                    <asp:ImageButton ID="Btnsave" OnClick="BtnSave_Click" ToolTip="Save" runat="server"
                        ImageUrl="~/images/save-29-1.png" />
                    <asp:ImageButton ID="btnDelete" OnClick="btnDelete_Click" ToolTip="Delete" runat="server"
                        ImageUrl="~/images/delete-29.png" />
                    <asp:Button ID="btnAddVillage" Visible="false" CssClass="btn btn-success"
                        ToolTip="Add Village" Text="Add Village" OnClick="btnAddVillage_Click" runat="server" />
                    <asp:Button ID="btnApprove" CssClass="btn btn-success" ToolTip="Back" Text="Back"
                        OnClick="btnApprove_Click" runat="server" />
                </div>
            </div>

            <!-- ===== Activity cards ===== -->
            <asp:Panel ID="pnlMain" runat="server">
                <div class="or-cards">

                    <!-- Meetings -->
                    <div class="col-lg-4 col-md-4 col-sm-6 col-xs-12">
                        <div class="or-card">
                            <div class="or-card-head">
                                <asp:CheckBox ID="chkMeetings" runat="server" Text="Meetings" CssClass="or-check or-title" />
                                <asp:CheckBox ID="rblMeetingsFC" runat="server" Text="F.C." CssClass="or-check or-sub" />
                            </div>
                            <div class="or-card-body">
                                <div class="or-grid">
                                    <asp:CheckBox ID="chk_FC_For" runat="server" Text="F.C." CssClass="or-check" />
                                    <asp:CheckBox ID="chk_BO" runat="server" Text="B.O." CssClass="or-check" />
                                    <asp:CheckBox ID="chk_Goverment" runat="server" Text="Government" CssClass="or-check" />
                                    <asp:CheckBox ID="chk_Other" runat="server" Text="Other (Description)" CssClass="or-check"
                                        OnCheckedChanged="chk_Other_Click" AutoPostBack="true" />
                                    <div class="full">
                                        <asp:TextBox ID="txtTraingOtherDec" Visible="false" runat="server"
                                            CssClass="form-control" placeholder="Enter description"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                    <!-- Training -->
                    <div class="col-lg-4 col-md-4 col-sm-6 col-xs-12">
                        <div class="or-card">
                            <div class="or-card-head">
                                <asp:CheckBox ID="Chk_Training" runat="server" Text="Training" CssClass="or-check or-title" />
                                <asp:CheckBox ID="rdTrainingFC" runat="server" Text="F.C." CssClass="or-check or-sub" />
                            </div>
                            <div class="or-card-body">
                                <div class="or-grid">
                                    <asp:CheckBox ID="CHkTB" runat="server" Text="Training Team Balika" CssClass="or-check" />
                                    <asp:CheckBox ID="ChkStaffTraining" runat="server" Text="Staff Training" CssClass="or-check" />
                                    <asp:CheckBox ID="Chk_Other_Training" runat="server" Text="Other (Description)" CssClass="or-check"
                                        OnCheckedChanged="ccCritica_CheckedChanged" AutoPostBack="true" />
                                    <div class="full">
                                        <asp:TextBox ID="txtTraingOther" Visible="false" runat="server"
                                            CssClass="form-control" placeholder="Enter description"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                    <!-- Other -->
                    <div class="col-lg-4 col-md-4 col-sm-12 col-xs-12">
                        <div class="or-card">
                            <div class="or-card-head">
                                <asp:CheckBox ID="chk_Other_Desc" runat="server" Text="Other (Description)" CssClass="or-check or-title" />
                            </div>
                            <div class="or-card-body">
                                <asp:TextBox ID="Txt_OtherDesc" runat="server" TextMode="MultiLine" Rows="4"
                                    CssClass="form-control" placeholder="Describe the activity"></asp:TextBox>
                            </div>
                        </div>
                    </div>

                </div>
            </asp:Panel>


            <!-- ===== Add Village popup (unchanged logic) ===== -->
            <cc1:ModalPopupExtender ID="MpexdrDistrict" runat="server" BackgroundCssClass="modalBg "
                CancelControlID="CancelButton" PopupControlID="PnlDistrict" TargetControlID="HdnFild">
            </cc1:ModalPopupExtender>
            <asp:HiddenField ID="HdnFild" runat="server"></asp:HiddenField>

            <asp:Panel CssClass="model-wid mod-posi" Style="display: none; height: auto; width: 45% !important; margin-top: 162.5px !important" ID="PnlDistrict" runat="server">
                <div style="width: 100%; height: auto; background-color: #f1f1f1">
                    <div class="modal-header" style="background-color: #3ac0f2; color: White;">
                        <asp:Button ID="ImageButton1" CssClass="btn btn-info pull-right" BackColor="#f5f5f5"
                            ToolTip="View" Text="View" OnClick="btnView_Click" Style="margin-right: 5px;"
                            runat="server" />
                        <h4 class="modal-title" style="color: White">Add Village</h4>
                    </div>
                    <div class="modal-body">
                        <asp:Label ID="lblmsg" runat="server" ForeColor="Red" Font-Names="Verdana" Font-Size="11px"></asp:Label>
                        <div class="form-horizontal" role="form">
                            <asp:Panel ID="pnlView" runat="server">
                                <div class="form-group" id="statediv" runat="server">
                                    <asp:Label ID="Label10" class="control-label col-sm-4 lab-text-left" runat="server" Text="State:"></asp:Label>
                                    <div class="col-sm-6">
                                        <asp:DropDownList ID="ddlState" runat="server" OnSelectedIndexChanged="ddlState_SelectedIndexChanged"
                                            AutoPostBack="true" CssClass="form-control" Font-Names="Verdana" Font-Size="11px">
                                        </asp:DropDownList>
                                        <span style="width: 5px; float: right; margin: -27px 27px; font-size: 21px;">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" InitialValue="0" runat="server"
                                                Display="Dynamic" ValidationGroup="saves" ControlToValidate="ddlState" ErrorMessage="*"
                                                ForeColor="Red"></asp:RequiredFieldValidator>
                                        </span>
                                    </div>
                                </div>

                                <div class="form-group">
                                    <asp:Label ID="Label11" class="control-label col-sm-4 lab-text-left" runat="server" Text="District:"></asp:Label>
                                    <div class="col-sm-6">
                                        <asp:DropDownList ID="ddlDistrict" OnSelectedIndexChanged="ddlDistrict_SelectedIndexChanged"
                                            AutoPostBack="true" runat="server" CssClass="form-control" Font-Names="Verdana" Font-Size="11px">
                                        </asp:DropDownList>
                                        <span style="width: 5px; float: right; margin: -27px 27px; font-size: 21px;">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" InitialValue="0" runat="server"
                                                Display="Dynamic" ValidationGroup="saves" ControlToValidate="ddlDistrict" ErrorMessage="*"
                                                ForeColor="Red"></asp:RequiredFieldValidator>
                                        </span>
                                    </div>
                                </div>

                                <div class="form-group">
                                    <asp:Label ID="Label12" class="control-label col-sm-4 lab-text-left" runat="server" Text="Block:"></asp:Label>
                                    <div class="col-sm-6">
                                        <asp:DropDownList ID="ddlBlock" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlBlock_SelectedIndexChanged"
                                            CssClass="form-control" Font-Names="Verdana" Font-Size="11px">
                                        </asp:DropDownList>
                                        <span style="width: 5px; float: right; margin: -27px 27px; font-size: 21px;">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" InitialValue="0" runat="server"
                                                Display="Dynamic" ValidationGroup="saves" ControlToValidate="ddlBlock" ErrorMessage="*"
                                                ForeColor="Red"></asp:RequiredFieldValidator>
                                        </span>
                                    </div>
                                </div>

                                <div class="form-group">
                                    <asp:Label ID="Label13" class="control-label col-sm-4 lab-text-left" runat="server" Text="Panchayat:"></asp:Label>
                                    <div class="col-sm-6">
                                        <asp:DropDownList ID="ddlPanchayat" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlPanchayat_SelectedIndexChanged"
                                            CssClass="form-control" Font-Names="Verdana" Font-Size="11px">
                                        </asp:DropDownList>
                                        <span style="width: 5px; float: right; margin: -27px 27px; font-size: 21px;">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" InitialValue="0" runat="server"
                                                Display="Dynamic" ValidationGroup="saves" ControlToValidate="ddlPanchayat" ErrorMessage="*"
                                                ForeColor="Red"></asp:RequiredFieldValidator>
                                        </span>
                                    </div>
                                </div>

                                <div class="form-group">
                                    <asp:Label ID="lblRseti" class="control-label col-sm-4 lab-text-left" runat="server" Text="Village:"></asp:Label>
                                    <div class="col-sm-6">
                                        <asp:DropDownList ID="ddlAddVillage" runat="server" CssClass="form-control"
                                            Font-Names="Verdana" Font-Size="11px">
                                        </asp:DropDownList>
                                        <span style="width: 5px; float: right; margin: -27px 27px; font-size: 21px;">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" InitialValue="0" runat="server"
                                                Display="Dynamic" ValidationGroup="saves" ControlToValidate="ddlAddVillage" ErrorMessage="*"
                                                ForeColor="Red"></asp:RequiredFieldValidator>
                                        </span>
                                    </div>
                                </div>
                            </asp:Panel>

                            <asp:Panel ID="pnlGridView" Visible="false" runat="server">
                                <asp:GridView ID="gvVillage" runat="server" AllowPaging="true" PageSize="100" AutoGenerateColumns="False"
                                    Font-Names="Arial" Font-Size="12px" Width="100%">
                                    <EmptyDataTemplate>
                                        <div style="font-family: Arial; font-size: 12px; font-weight: bold;">Data not found</div>
                                    </EmptyDataTemplate>
                                    <FooterStyle CssClass="FooterStyle" />
                                    <HeaderStyle BackColor="#C1C1C1" ForeColor="White" Height="40px" HorizontalAlign="Center" />
                                    <SelectedRowStyle BackColor="#fad669" Font-Bold="True" ForeColor="WhiteSmoke" />
                                    <AlternatingRowStyle BackColor="#f1f1f1" />
                                    <PagerStyle CssClass="paging" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Action" HeaderStyle-CssClass="GridHeaderClass">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="ImgAcc" runat="server" OnClick="btn_Delete_Click" ImageUrl="~/images/delete-29.png"
                                                    Width="15px" Height="15px"></asp:ImageButton>
                                                <asp:Label ID="lblCUniqueChildCode" Visible="false" runat="server" Text='<%# Bind("NewVillageCode") %>'></asp:Label>
                                                <asp:Label ID="lblUserId" Visible="false" runat="server" Text='<%# Bind("UserId") %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Width="5%" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="District Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDistrictName" class="labelGrid" ForeColor="Black" runat="server" Text='<%# Eval("DistrictName") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Block Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblBlockName" class="labelGrid" ForeColor="Black" runat="server" Text='<%# Eval("BlockName") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Panchayat Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblPanchayatName" class="labelGrid" ForeColor="Black" runat="server" Text='<%# Eval("PanchayatName") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Village Code">
                                            <ItemTemplate>
                                                <asp:Label ID="lblVis9ame" class="labelGrid" ForeColor="Black" runat="server" Text='<%# Eval("VillageCode") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Village Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblVillageN9ame" class="labelGrid" ForeColor="Black" runat="server" Text='<%# Eval("VillageName") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </asp:Panel>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <asp:Button ID="btnNewUserSave" runat="server" CssClass="btn bgm-cyan" OnClick="btnNewUserSave_Click" ValidationGroup="saves"
                            Text="Save" ToolTip="Save" Style="float: none;"></asp:Button>&nbsp;
                        <asp:Button ID="CancelButton" runat="server" CssClass="btn bgm-cyan" Text="Close"
                            ToolTip="Close" Style="float: none;"></asp:Button>
                    </div>
                </div>
            </asp:Panel>

        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnAddVillage" />
        </Triggers>
    </asp:UpdatePanel>

    <!-- ===== Remarks popup (unchanged) ===== -->
    <cc1:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="Hdn_model4"
        PopupControlID="pnlpopup4" BackgroundCssClass="modalBackground">
    </cc1:ModalPopupExtender>
    <asp:HiddenField ID="Hdn_model4" runat="server" />
    <asp:Panel ID="pnlpopup4" runat="server" Style="display: none;">
        <div class="modal-dialog modal-md">
            <div class="modal-content">
                <div class="modal-header">
                    <asp:ImageButton ID="ImageButton8" CssClass="btn btn-info pull-right" OnClick="btnReset_Click" BackColor="#f5f5f5"
                        ToolTip="Close" ImageUrl="~/images/close-29.png" Style="margin-right: 5px; padding: 0px;"
                        runat="server" />
                    <h4 class="modal-title">Remarks</h4>
                </div>
                <div class="modal-body">
                    <div class="form-group">
                        <label>Remarks:</label>
                        <asp:DropDownList ID="ddlRemark" runat="server" CssClass="form-control">
                            <asp:ListItem Selected="True" Value="0">--Select--</asp:ListItem>
                            <asp:ListItem Value="1">Format not available</asp:ListItem>
                            <asp:ListItem Value="2">Wrongly activity selected</asp:ListItem>
                            <asp:ListItem Value="3">Typing error</asp:ListItem>
                            <asp:ListItem Value="4">Counting error</asp:ListItem>
                            <asp:ListItem Value="5">C Phone not available</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
            </div>
        </div>
    </asp:Panel>

</asp:Content>
