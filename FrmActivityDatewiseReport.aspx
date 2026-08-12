<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.master" CodeFile="FrmActivityDatewiseReport.aspx.cs"  Culture="en-GB" Inherits="FrmActivityDatewiseReport" %>

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
                                                <asp:LinkButton ID="btnexcel" Visible="false"  runat="server" Text="Export to Excel" ></asp:LinkButton>
                                                
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
                                   Year:</label>
                                <div class="col-sm-9 padd">
                                        <asp:DropDownList ID="ddlYear" runat="server"  class="form-control ">
                                                                     </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-3 col-md-3 col-sm-3 cpl-xs-12">
                            <div class="form-group" style="margin-bottom: 7px;">
                                <label for="email" class="col-sm-3 padd linhei">
                                    Month:</label>
                                <div class="col-sm-9 padd">
                                     <asp:DropDownList ID="ddlMonth" runat="server" class="form-control">
                                                                <asp:ListItem Selected="True" Value="0">--Select--</asp:ListItem>
                                                                <asp:ListItem Value="1">Jan </asp:ListItem>
                                                                <asp:ListItem Value="2">Feb </asp:ListItem>
                                                                <asp:ListItem Value="3">Mar</asp:ListItem>
                                                                <asp:ListItem Value="4">Apr</asp:ListItem>
                                                                     <asp:ListItem Value="5">May</asp:ListItem>
                                                                      <asp:ListItem Value="6">Jun</asp:ListItem>
                                                                      <asp:ListItem Value="7">Jul</asp:ListItem>
                                                                       <asp:ListItem Value="8">Aug</asp:ListItem>
                                                                          <asp:ListItem Value="9">Sep</asp:ListItem>
                                                                             <asp:ListItem Value="10">Oct</asp:ListItem>
                                                                                <asp:ListItem Value="11">Nov</asp:ListItem>
                                                                                  <asp:ListItem Value="12">Dec</asp:ListItem>
                                                            </asp:DropDownList>
                                </div>
                            </div>
                            
                        </div>

                               <div class="col-lg-2 col-md-2  col-sm-2 cpl-xs-12">
                                <asp:Button ID="btnBack" CssClass="btn btn-success pull-right " Text="Back"
                                     OnClick="btnBack_Click" Style="margin-right: 5px; padding: 0px;"
                                    runat="server" />
                                       <asp:ImageButton ID="btnSerach" ToolTip="Serach" runat="server"  OnClick="btnSerach_Click"  class="btn btn-danger btn-paddd pull-left" BackColor="#f1f1f1"
                                ImageUrl="~/images/search-29.png"  Style="margin-left: 5px; padding: 0px;"   />
                         
            </div>
                    </div>
                   
                </div>
                

            </div>


            <div class="row">
          
            <%--    <div class="col-lg-6 col-md-6 col-sm-8 col-xs-12">--%>
             <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                 <h4> <asp:Label ID="lblSchool" Visible="false" runat="server" Text="School Activity"></asp:Label> </h4>
          
                       <div style="height: 350px; overflow: auto; width: 99%;" align="center">
                                                 
           <asp:GridView id="Gv_Profile_Search" runat="server"  ShowHeader="true" AutoGenerateColumns="true" Width="100%" CssClass=" table table-striped table-bordered table-hover ">
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
             <div class="row">
              <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                    <h4> <asp:Label ID="lblVillage" Visible="false" runat="server" Text="Village Activity"></asp:Label> </h4>
                         <div style="height: 350px; overflow: auto; width: 99%;" align="center">
       
            <asp:GridView id="gvVillageActivity" runat="server"  ShowHeader="true" AutoGenerateColumns="true" Width="100%" CssClass=" table table-striped table-bordered table-hover ">
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
     
          <div class="row">
            <%--    <div class="col-lg-6 col-md-6 col-sm-8 col-xs-12">--%>
             <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                       <h4> <asp:Label ID="lblOffice" Visible="false" runat="server" Text="Office Activity"></asp:Label> </h4>
 
                       <div style="height: 200px; overflow: auto; width: 99%;" align="center">
                     
                <asp:GridView id="gvOffice" runat="server"  ShowHeader="true" AutoGenerateColumns="true" Width="100%" CssClass=" table table-striped table-bordered table-hover ">
                 <EmptyDataTemplate>
                     <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
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
    </div>
       </div>
<%--    </ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>
