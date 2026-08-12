<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="frmLearningCampMasterAgp.aspx.cs" Inherits="frmLearningCampMasterAgp" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:UpdatePanel runat="server" ID="UpdatePanel1">
        <ContentTemplate>
            <div class="row">
                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                    <div class="panel panel-default">
                        <div class="panel-heading" style="padding: 5px 5px 5px 10px;">
                            <div class="row">
                                <div class="col-lg-4">
                                    <h3 class="text-danger" style="margin: 0px;">
                                        <asp:Label ID="lblMain" runat="server" Text=" Learning Camp Master AGP"></asp:Label>
                                    </h3>

                                </div>
                                <div class="col-lg-4"></div>
                                <div class="col-lg-4">
                                    <div class="pull-right">
                                        <asp:Button ID="btnAddCamp" runat="server" Text="Add Camp" CssClass="btn btn-danger btn-sm" Style="margin-right: 15px;" OnClick="btnAddCamp_Click" />
                                        <asp:LinkButton ID="LinkddButton1" runat="server" Text="Export to Excel" OnClick="btnReprot_Click"
                                            class="pull-right"></asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="padding: 0px;">
                                    <div style="overflow: auto; height: 350px;">
                                        <asp:GridView ID="GridLearningCampMaster" runat="server" AutoGenerateColumns="False" AllowPaging="True" PageSize="12"
                                            OnPageIndexChanging="GridLearningCampMaster_PageIndexChanging" OnRowCommand="GridLearningCampMaster_RowCommand" DataKeyNames="CampID,CampNumber,CampNumberName,SessionInCamp,BaselineSessionNo,EndSessionNo" CssClass="table table-striped table-bordered table-condensed" Width="100%">
                                            <PagerSettings Position="Bottom" PageButtonCount="5"></PagerSettings>
                                            <Columns>
                                                <asp:TemplateField HeaderText="S.No.">
                                                    <ItemStyle Width="5%" />
                                                    <ItemTemplate>
                                                        <%#Container.DataItemIndex+1 %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Camp No.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="GvlbCampNumber" runat="server" Text='<%# Eval("CampNumberName") %>'></asp:Label>
                                                        <asp:Label ID="Label2" Visible="false" runat="server" Text='<%# Eval("CampID") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" Width="90px" />
                                                    <HeaderStyle Font-Bold="true" Height="30px" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="#Session">
                                                    <ItemTemplate>
                                                        <asp:Label ID="GvlbSessionInCamp" runat="server" Text='<%# Eval("SessionInCamp") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" Width="90px" />
                                                    <HeaderStyle Font-Bold="true" Height="30px" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="BL Session No.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="GvlbHindiBaselineSessionNo" runat="server" Text='<%# Eval("BaselineSessionNo") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" Width="90px" />
                                                    <HeaderStyle Font-Bold="true" Height="30px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="EL Session No.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="GvlbHindiEndSessionNo" runat="server" Text='<%# Eval("EndSessionNo") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" Width="90px" />
                                                    <HeaderStyle Font-Bold="true" Height="30px" />
                                                </asp:TemplateField>





                                                <asp:TemplateField HeaderText="Edit">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="GvImgbtnEdit" CommandName="EditData" runat="server" ImageUrl="~/images/edit.png" CommandArgument='<%# Container.DataItemIndex %>'
                                                            ToolTip="Edit" Style="margin-top: 10px;"></asp:ImageButton>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" Width="10px" />
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Delete" HeaderStyle-Width="10px">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="GvImgbtnDelete" OnClick="btn_Delete_Click" runat="server" ImageUrl="~/images/delete-29.png" CommandArgument='<%# Eval("CampID") %>'
                                                            OnClientClick="return confirm('Are you sure you want to delete?');" ToolTip="Delete"></asp:ImageButton>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                            </Columns>
                                            <PagerStyle CssClass="pgr"></PagerStyle>
                                            <EmptyDataTemplate>
                                                <table style="border: 0px;">
                                                    <tr>
                                                        <td style="border: 0px;">
                                                            <asp:Label ID="lblEmptySearch" runat="server">No results found</asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>

                                            </EmptyDataTemplate>
                                        </asp:GridView>
                                    </div>
                                    <asp:HiddenField ID="hdnCampID" runat="server" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="row">
                <div class="col-lg-12">
                    <cc1:ModalPopupExtender ID="ModalLearningCamp" runat="server" BackgroundCssClass="modalBg " CancelControlID="CancelButton" PopupControlID="PnlLearningCamp" TargetControlID="HdnFild">
                    </cc1:ModalPopupExtender>
                    <asp:HiddenField ID="HdnFild" runat="server"></asp:HiddenField>
                    <asp:Panel CssClass="model-wid mod-posi" Style="display: none; height: auto; width: 80% !important; margin-top: -40%;" ID="PnlLearningCamp" runat="server">
                        <div style="width: 100%; height: auto; background-color: #f1f1f1">
                            <div class="modal-header" style="background-color: #ddd; padding: 10px;">
                                <asp:Label ID="lblFormName" runat="server" Text="Add Learning Master" CssClass="text-danger" Font-Bold="true"></asp:Label>
                                <asp:LinkButton ID="CancelButton" CssClass="btn btn-sm btn-danger pull-right" runat="server"> <span class="glyphicon glyphicon-remove"></span></asp:LinkButton>
                            </div>
                            <div class="modal-body">
                                <div class="row">
                                    <div class="col-lg-3">
                                        <asp:Label ID="lblCampNo" runat="server" Text="Camp Number"></asp:Label>
                                        <asp:DropDownList ID="ddlCampNo" runat="server" CssClass="form-control"></asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ForeColor="red" runat="server" ControlToValidate="ddlCampNo"
                                            CssClass="failureNotification" ErrorMessage="Select Camp No" ToolTip="Camp No" ValidationGroup="Valid" Font-Size="Small" Font-Bold="true"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="col-lg-3">
                                        <asp:Label ID="lblSessioninCamp" runat="server" Text="#Session"></asp:Label>
                                        <asp:TextBox ID="txtSessioninCamp" runat="server" CssClass="form-control" onkeypress="return isNumberKey(this,event);" MaxLength="2"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ForeColor="red" runat="server" ControlToValidate="txtSessioninCamp"
                                            CssClass="failureNotification" ErrorMessage="Enter Session in Camp" ToolTip="Session in Camp" ValidationGroup="Valid" Font-Size="Small" Font-Bold="true"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="col-lg-3">
                                        <asp:Label ID="lblHindiBaselineSessionNo" runat="server" Text="BL Session No."></asp:Label>
                                        <asp:TextBox ID="txtHindiBaselineSessionNo" runat="server" CssClass="form-control" onkeypress="return isNumberKey(this,event);" MaxLength="2"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ForeColor="red" runat="server" ControlToValidate="txtHindiBaselineSessionNo"
                                            CssClass="failureNotification" ErrorMessage="Enter Hindi Baseline Session No." ToolTip="Hindi Baseline Session No." ValidationGroup="Valid" Font-Size="Small" Font-Bold="true"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="col-lg-3">
                                        <asp:Label ID="lblHindiEndlineSessionNo" runat="server" Text="EL Session No."></asp:Label>
                                        <asp:TextBox ID="txtHindiEndlineSessionNo" runat="server" CssClass="form-control" onkeypress="return isNumberKey(this,event);" MaxLength="2"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ForeColor="red" runat="server" ControlToValidate="txtHindiEndlineSessionNo"
                                            CssClass="failureNotification" ErrorMessage="Enter Hindi Endline Session No." ToolTip="Hindi Endline Session No." ValidationGroup="Valid" Font-Size="Small" Font-Bold="true"></asp:RequiredFieldValidator>
                                    </div>
                                    <br />

                                    <br />

                                    <div class="modal-footer" style="padding: 10px;">
                                        <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-success btn-sm" OnClick="btnSave_Click" ValidationGroup="Valid" />&nbsp;
       <asp:Button ID="btnClear" runat="server" Text="Clear" CssClass="btn btn-info btn-sm" OnClick="btnClear_Click" />&nbsp;
       <asp:Button ID="btnClose" runat="server" Text="Close" CssClass="btn btn-danger btn-sm" />
                                    </div>
                    </asp:Panel>
                </div>
            </div>
        </ContentTemplate>

        <Triggers>
            <asp:PostBackTrigger ControlID="GridLearningCampMaster" />
            <asp:PostBackTrigger ControlID="btnSave" />
            <asp:PostBackTrigger ControlID="LinkddButton1" />

        </Triggers>
    </asp:UpdatePanel>
</asp:Content>

