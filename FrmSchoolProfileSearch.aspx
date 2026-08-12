<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"  Culture="en-GB"
    CodeFile="FrmSchoolProfileSearch.aspx.cs" Inherits="FrmSchoolProfileSearch" %>

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
<%--<asp:UpdatePanel ID="MainPanel" runat="server">
        <ContentTemplate>--%>
    <div class="row" >
        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <h3 class="text-danger" style="margin: 0px;">
                                          School and Village Activity <span class="pull-right" style="font-size: 17px;">
                                                <asp:LinkButton ID="btnexcel" Visible="false"  runat="server" Text="Export to Excel" OnClick="Export_To_Excel"></asp:LinkButton>
                                                
                                             </span>
                                        </h3>
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
                                    <asp:DropDownList ID="ddlUser"  AutoPostBack="true"  OnSelectedIndexChanged="ddlUser_SelectedIndexChanged"
                                        runat="server" class="form-control ">
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
                          <asp:Button ID="btnApprove"  CssClass="btn btn-success pull-right " 
                                 ToolTip="Save" Text="Approve"    OnClick="btnApprove_Click" 
                                Style="margin-right: 5px; padding: 0px;" runat="server" /></div>
                                  <div class="col-lg-1 col-md-1 col-sm-2 cpl-xs-12 col-lg-offset-1 col-md-offset-1 col-sm-offset-1 col-xs-offset-0">
                            <asp:Button ID="btnsave"  CssClass="btn btn-danger pull-right " Visible="false"  OnClick="btnSave_Click" 
                                 ToolTip="Save" Text="Report" 
                                Style="margin-right: -21px; padding: 0px;" runat="server" />
                                </div>
                          
                        </div>
                </div>
            </div>
            <div class="row">
              <h4> School Activity</h4>
            <%--    <div class="col-lg-6 col-md-6 col-sm-8 col-xs-12">--%>
             <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                
                        
                <asp:GridView id="Gv_Profile_Search" runat="server"  ShowHeader="true" AutoGenerateColumns="true" Width="60%" CssClass=" table table-striped table-bordered table-hover ">
                 <EmptyDataTemplate>
                                <div style="font-family: Arial; font-size: 12px; font-weight: bold; color: Red;">
                                    Data not found
                                </div>
                            </EmptyDataTemplate>
                            <Columns>
                           

                            </Columns>
                </asp:GridView>

               
                          <asp:GridView ID="DGV_Report" AllowPaging="true" PageSize="30"  runat="server" ForeColor="#333333" CellPadding="1" 
                                CssClass="table table-striped table table-hover table-bordered" AutoGenerateColumns="true" Width="99.7%"   >
                               
                                <EmptyDataTemplate>
                                    <div style="font-family: Arial; font-size: 12px; font-weight: bold; color: Red;">
                                        Data not found</div>
                                </EmptyDataTemplate>
                                <Columns>
                                </Columns>
                            </asp:GridView>
                           
                </div>
                 
            </div>

              <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                <h4> Village Activity</h4>
                        
                <asp:GridView id="gvVillageActivity" runat="server"  ShowHeader="true" AutoGenerateColumns="true" Width="60%" CssClass=" table table-striped table-bordered table-hover ">
                 <EmptyDataTemplate>
                                <div style="font-family: Arial; font-size: 12px; font-weight: bold; color: Red;">
                                    Data not found
                                </div>
                            </EmptyDataTemplate>
                            <Columns>
                           

                            </Columns>
                </asp:GridView>

                              
                </div>
        </div>
    </div>
    </div>
<%--    </ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>
