<%@ Page Title="GKP School Master Update" Language="C#" MasterPageFile="~/Site.master"
    AutoEventWireup="true" CodeFile="GkpSchoolMasterUpdate.aspx.cs"
    Inherits="GkpSchoolMasterUpdate" %>

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

                                            <h3 class="text-danger font-weight-bold" style="margin: 0">GKP Subject Level Update</h3>

                                            <div style="display: flex; gap: 12px">


                                                <asp:LinkButton ID="Button1" OnClick="btnNewImport_Click" class="Download Master Update Sheet"
                                                    ToolTip="Save"
                                                    runat="server">Download Upload Format</asp:LinkButton>


                                                <asp:LinkButton ID="btnSubmit" Visible="false" class="btn btn-sm btn-primary pull-right"
                                                    ToolTip="Save"
                                                    OnClick="btnSubmitted_Click" runat="server">Submit to DOL</asp:LinkButton>

                                                <asp:LinkButton ID="btnsave" Visible="false" OnClick="btnsave_Click" class="btn btn-sm btn-primary pull-right"
                                                    ToolTip="Save"
                                                    runat="server">Save</asp:LinkButton>


                                                <asp:LinkButton ID="btnReject" CssClass="btn btn-sm btn-primary pull-right" OnClick="btnReject_Click" Visible="false" runat="server" Text="Reject">Reject</asp:LinkButton>
                                                <asp:LinkButton ID="LinkButton3" CssClass="btn btn-sm btn-primary pull-right" Visible="false" runat="server" Text="Unlock">Unlock</asp:LinkButton>

                                            </div>
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
                                                        <asp:FileUpload ID="FileUpload1" runat="server" Visible="false"   style=" margin-left: 111px;"/>


                                                    </div>
                                                </div>

                                                <div class="col-lg-1 col-md-1 col-sm-6">
                                                    <div class="form-group">
                                                        <asp:LinkButton ID="Button2" Visible="false" OnClick="btnNewImport1_Click" class="btn btn-sm btn-primary pull-right"
                                                            ToolTip="Save"
                                                            runat="server">Upload Excel</asp:LinkButton>

                                                    </div>
                                                </div>


                                            </div>
                                        </div>
                                    </div>
                                </div>



                                <div class="panel-body scroll" style="min-height: 404px; padding: 0px">
                                    <asp:Panel ID="pnlMain" Enabled="true" runat="server">
                                        <div class="gkp-grid-wrap">
                                            <div>
                                                <div class="Row" style="width: 100%">
                                                    <asp:GridView ID="gvGkpMaster" OnRowDataBound="GV_luster_OnRowDataBound" OnPageIndexChanging="GV_Cluster_PageIndexChanging" AllowPaging="true" PageSize="10" ShowFooter="false"
                                                        CssClass="table table-striped table-bordered" GridLines="None" Width="100%" runat="server" AutoGenerateColumns="false">
                                                        <EmptyDataTemplate>
                                                        </EmptyDataTemplate>
                                                        <FooterStyle CssClass="FooterStyle" />
                                                        <HeaderStyle Height="40px" Wrap="true" BackColor="#C1C1C1" HorizontalAlign="Center" />
                                                        <RowStyle HorizontalAlign="Left" Wrap="true" VerticalAlign="Middle" />
                                                        <SelectedRowStyle BackColor="#fad669" Font-Bold="True" ForeColor="#5a3d00" />
                                                        <PagerStyle CssClass="paging" HorizontalAlign="Right" />
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="School Name">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="La2belPosition" runat="server" Text='<%# Bind("Name") %>' />
                                                                </ItemTemplate>


                                                                <ItemStyle HorizontalAlign="left" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Dise Code">

                                                                <ItemTemplate>
                                                                    <asp:Label ID="Labe3lTeam" runat="server" Text='<%# Bind("Disecode") %>' />
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                </FooterTemplate>

                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Block">

                                                                <ItemTemplate>
                                                                    <asp:Label ID="LabelTeam" runat="server" Text='<%# Bind("EG_Block") %>' />
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                </FooterTemplate>

                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Cluster">

                                                                <ItemTemplate>
                                                                    <asp:Label ID="LabelPositi1on" runat="server" Text='<%# Bind("ClusterName") %>' />
                                                                </ItemTemplate>


                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Working Status">

                                                                <ItemTemplate>


                                                                    <asp:Label ID="LabelPdf1n" runat="server" Text='<%# Bind("WorkingStatus") %>' />
                                                                </ItemTemplate>

                                                            </asp:TemplateField>


                                                            <asp:TemplateField HeaderText="Last Year GKP">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Lab4elPosition" runat="server" Text='<%# Bind("LastYearGKPSschool") %>' />
                                                                    <asp:Label ID="lblGKPLevelHindi" Visible="false" runat="server" Text='<%# Bind("GKPLevelHindi") %>' />
                                                                    <asp:Label ID="lblGKPLevelMath" Visible="false" runat="server" Text='<%# Bind("GKPLevelMath") %>' />
                                                                    <asp:Label ID="lblGKPLevelEnglish" Visible="false" runat="server" Text='<%# Bind("GKPLevelEnglish") %>' />
                                                                    <asp:Label ID="lblAssessmentTypeID" Visible="false" runat="server" Text='<%# Bind("AssessmentTypeID") %>' />
                                                                    <asp:Label ID="lblSchoolcode" Visible="false" runat="server" Text='<%# Bind("SchoolCode") %>' />
                                                                </ItemTemplate>
                                                                <HeaderStyle Width="6%" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Assessment Type">

                                                                <ItemTemplate>
                                                                    <asp:DropDownList ID="ddlAssessmentType" onchange="UpdateHiddenField(this);" runat="server" class="form-control flagTrigger ">
                                                                        <asp:ListItem Selected="True" Value="0">--Select--</asp:ListItem>
                                                                        <asp:ListItem Value="1">Without Micro Skill </asp:ListItem>
                                                                        <asp:ListItem Value="2">With Micro Skill</asp:ListItem>


                                                                    </asp:DropDownList>
                                                                    <asp:HiddenField ID="hdnUpdated" runat="server" Value="0" />
                                                                </ItemTemplate>
                                                                <HeaderStyle Width="14%" />
                                                            </asp:TemplateField>
                                                            
                                                            <asp:TemplateField HeaderText="GKP Level Hindi">
                                                                <ItemTemplate>
                                                                    <asp:DropDownList ID="ddlWorkingHindi" onchange="UpdateHiddenField(this);" runat="server" class="form-control flagTrigger ">
                                                                        <asp:ListItem Selected="True" Value="0">--Select--</asp:ListItem>
                                                                        <asp:ListItem Value="1">Srijan/Bodh </asp:ListItem>
                                                                        <asp:ListItem Value="2">Bodh/Pravah</asp:ListItem>
                                                                        <asp:ListItem Value="3">Pravah/Utsav </asp:ListItem>


                                                                    </asp:DropDownList>
                                                                </ItemTemplate>

                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="GKP Level Math">
                                                                <ItemTemplate>
                                                                    <asp:DropDownList ID="ddMath" onchange="UpdateHiddenField(this);" runat="server" class="form-control flagTrigger ">
                                                                        <asp:ListItem Selected="True" Value="0">--Select--</asp:ListItem>
                                                                        <asp:ListItem Value="1">Srijan/Bodh </asp:ListItem>
                                                                        <asp:ListItem Value="2">Bodh/Pravah</asp:ListItem>
                                                                        <asp:ListItem Value="3">Pravah/Utsav </asp:ListItem>


                                                                    </asp:DropDownList>
                                                                </ItemTemplate>

                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="GKP Level English">
                                                                <ItemTemplate>
                                                                    <asp:DropDownList ID="ddlEnglish" onchange="UpdateHiddenField(this);" runat="server" class="form-control flagTrigger ">
                                                                        <asp:ListItem Selected="True" Value="0">--Select--</asp:ListItem>
                                                                        <asp:ListItem Value="1">Srijan/Bodh </asp:ListItem>
                                                                        <asp:ListItem Value="2">Bodh/Pravah</asp:ListItem>
                                                                        <asp:ListItem Value="3">Pravah/Utsav </asp:ListItem>


                                                                    </asp:DropDownList>
                                                                </ItemTemplate>

                                                            </asp:TemplateField>

                                                        </Columns>
                                                    </asp:GridView>
                                                </div>
                                            </div>
                                        </div>
                                    </asp:Panel>
                                </div>

                            </div>
                        </div>
                    </div>
                </div>






                <cc1:ModalPopupExtender ID="ModalPopupExtender3" runat="server" TargetControlID="HiddenField1"
                    PopupControlID="pnlpopup10" CancelControlID="CancelButton2" BackgroundCssClass="modalBackground">
                </cc1:ModalPopupExtender>
                <asp:HiddenField ID="HiddenField1" runat="server" />
                <asp:Panel ID="pnlpopup10" runat="server" Style="display: none; width: 80%">
                    <div class="modal-dialog modal-md">
                        <div class="modal-content">
                            <div class="modal-header" style="height: 0px;">
                                <asp:ImageButton ID="CancelButton2" ImageUrl="~/images/close-29.png" runat="server"
                                    Text="Close" ToolTip="Close" Style="border-width: 0px; float: none; margin-left: 547px; margin-top: -8px;"></asp:ImageButton>

                            </div>
                            <div class="row">
                                <div class="row ">
                                    <div class="col-lg-10 col-md-10 col-sm-10 cpl-xs-12">
                                        <div class="col-lg-8 col-md-8  col-sm-8 cpl-xs-12">
                                            <div class="form-group" >
                                                <label for="email" class="col-sm-3 padd linhei">
                                                    Remarks:</label>
                                                <div class="col-sm-9 padd">
                                                    <asp:TextBox ID="txtRemark" runat="server" Width="171%" TextMode="MultiLine" MaxLength="100"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="ReqTxtDate" runat="server" ControlToValidate="txtRemark"
                                                        Display="Dynamic" ErrorMessage="Please Enter Remark for Rejection" Font-Bold="False" Font-Size="9px" ForeColor="Red"
                                                        SetFocusOnError="True" ValidationGroup="Savdata"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                </div>
                                <div class="col-lg-12 col-md-12 col-sm-12 cpl-xs-12">

                                    <asp:LinkButton ID="ImageButton2" ValidationGroup="Savdata" CssClass="btn btn-sm btn-primary Pull-right"
                                        ToolTip="Save" OnClick="btnsaveReject_Click"
                                        Style="margin-left: 500px; padding: 0px; width: 45px; margin-top: 2px" runat="server">Save</asp:LinkButton>
                                </div>
                            </div>
                        </div>
                    </div>
                </asp:Panel>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="Button1" />
            <asp:PostBackTrigger ControlID="Button2" />




        </Triggers>
    </asp:UpdatePanel>
</asp:Content>




