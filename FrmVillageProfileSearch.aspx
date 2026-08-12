<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"  Culture="en-GB"
    CodeFile="FrmVillageProfileSearch.aspx.cs" Inherits="FrmVillageProfileSearch" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="row" >
        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <h3 style="margin: 0px;">
                        Village</h3>
                </div>
            </div>
            <div class="row">
                <div class="row marg search-bg">
                    <div class="form-horizontal">
                        <%-- <asp:UpdatePanel runat="server" ID="UpMain" UpdateMode="Conditional">
        <ContentTemplate>--%>
        <div class="col-lg-10 col-md-10 col-sm-10 cpl-xs-12">
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
                                   From Date:</label>
                                <div class="col-sm-9 padd">
                                    <asp:TextBox runat="server" ID="TxtFromDate" autocomplete="off" ondrop="return false;"
                                        class="form-control" onkeypress="return false;"></asp:TextBox>
                                    <ajax:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True"
                                        Format="dd/MM/yyyy" TargetControlID="TxtFromDate" PopupPosition="BottomRight">
                                    </ajax:CalendarExtender>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TxtFromDate"
                                        Display="Dynamic" ErrorMessage="*" Font-Bold="False" Font-Size="9px" ForeColor="Red"
                                        SetFocusOnError="True" ValidationGroup="saves"></asp:RequiredFieldValidator>
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

                        
                    </div>
                      <div class="col-lg-2 col-md-2  col-sm-2 cpl-xs-12">
                        <div class="col-lg-1 col-md-1 col-sm-2 cpl-xs-12 col-lg-offset-1 col-md-offset-1 col-sm-offset-1 col-xs-offset-0">
                            <asp:ImageButton ID="btnSerach" ToolTip="Serach" runat="server" OnClick="btnSerach_Click" class="btn btn-danger btn-paddd pull-left" BackColor="#f1f1f1"
                                ImageUrl="~/images/search-29.png"  Style="margin-left: -49px; padding: 0px;"   />
                                </div>
                      <div class="col-lg-1 col-md-1 col-sm-2 cpl-xs-12 col-lg-offset-1 col-md-offset-1 col-sm-offset-1 col-xs-offset-0">
                          <asp:Button ID="btnApprove"  CssClass="btn btn-danger pull-right " 
                                 ToolTip="Save" Text="Approve"    OnClick="btnApprove_Click" 
                                Style="margin-right: 5px; padding: 0px;" runat="server" /></div>
                                  <div class="col-lg-1 col-md-1 col-sm-2 cpl-xs-12 col-lg-offset-1 col-md-offset-1 col-sm-offset-1 col-xs-offset-0">
                            <asp:Button ID="btnsave"  CssClass="btn btn-danger pull-right "  Visible="false"
                                 ToolTip="Save" Text="Report" 
                                Style="margin-right: -21px; padding: 0px;" runat="server" />
                                </div>
                          
                        </div>
                </div>
            </div>
            <div class="row">
                <div class="col-lg-6 col-md-6 col-sm-8 col-xs-12">
                <asp:GridView id="Gv_Profile_Search" runat="server"   ShowHeader="true" AutoGenerateColumns="true" Width="100%" CssClass=" table table-striped table-bordered table-hover ">
                 <EmptyDataTemplate>
                                <div style="font-family: Arial; font-size: 12px; font-weight: bold; color: Red;">
                                    Data not found
                                </div>
                            </EmptyDataTemplate>
                            <Columns>
                            <%-- <asp:TemplateField HeaderText="SN.">
                                        <ItemTemplate>
                                            <%# Container.DataItemIndex+1 %>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                        <HeaderStyle Width="3%" />
                                    </asp:TemplateField>
                             <asp:ButtonField HeaderText="Date "  ItemStyle-ForeColor="#333" DataTextField="VDate"
                        CommandName="GVUIO">
                        <ItemStyle CssClass="padding-lef" Width="15%"  Height="30px" />
                        <HeaderStyle CssClass="padding-lef" />
                                                          </asp:ButtonField>
                             <asp:TemplateField HeaderText="Total">
                                        <ItemTemplate>
                                            <asp:Label ID="lblTemp" runat="server" Text='<%#Eval("Flag") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Width="8%" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>--%>

                            </Columns>
                </asp:GridView>
                </div>
                  <div class="col-lg-8 col-md-8 col-sm-8 col-xs-12">
                  </div>
            </div>
        </div>
    </div>
</asp:Content>
