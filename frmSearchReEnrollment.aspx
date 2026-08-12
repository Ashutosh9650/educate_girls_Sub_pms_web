<%@ Page Language="C#" AutoEventWireup="true"  Culture="en-GB"  CodeFile="frmSearchReEnrollment.aspx.cs" Inherits="frmSearchReEnrollment" %>
  <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
  
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<meta name="viewport" content="width=device-width, initial-scale=1">

<link rel="stylesheet" type="text/css" href="css/bootstrap.css"/>
<link rel="stylesheet" type="text/css" href="css/font-awesome.css"/>
<link rel="stylesheet" type="text/css" href="css/bootstrap-multiselect.css"/>

<script type="text/javascript" src="js/jquery-2.1.0.js"></script>
<script type="text/javascript" src="js/jquery.min.js"></script>
<script type="text/javascript" src="js/bootstrap.js"></script>
<script type="text/javascript" src="js/bootstrap-multiselect.js"></script>
<link href="http://db.onlinewebfonts.com/c/9fad9fb4f8926dd5f3e0156e4d8ba11d?family=Vagabond" rel="stylesheet" type="text/cs"/>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
     <script language="Javascript" type="text/javascript">

         function onlyAlphabetsHH(e, t) {
             try {


                 if (window.event) {
                     var charCode = window.event.keyCode;
                 }
                 else if (e) {
                     var charCode = e.which;
                 }
                 else { return true; }
                 if ((charCode > 64 && charCode < 91) || (charCode > 48 && charCode < 57) || (charCode > 96 && charCode < 123) || charCode == 32 || charCode == 32 || charCode == 0 || charCode == 9)
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

         function onlyAlphabetsSrNo(e, t) {
             try {


                 if (window.event) {
                     var charCode = window.event.keyCode;
                 }
                 else if (e) {
                     var charCode = e.which;
                 }
                 else { return true; }
                 if ((charCode > 64 && charCode < 91) || (charCode > 48 && charCode < 57) || (charCode > 96 && charCode < 123) || charCode == 55 || charCode == 47 || charCode == 32 || charCode == 8 || charCode == 0 || charCode == 9)
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
         function checkPwd(str) {


             var msg = "";
             if (str.search(/\d/) == -1) {

                 msg += 'Please enter atleast one number'; // for numeric
                 str.value = "";
             }

             if (msg != "") {
                 str.value = "";
                 alert(msg);
                 return false;
             }
             else { return true; }
         }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <ajax:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
      </ajax:ToolkitScriptManager>
     <asp:UpdatePanel runat="server" ID="UpMain">
        <ContentTemplate>
     
 
             <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="padding-left: -2px;">
                <div class="thumbnail" style="background-color: #f5f5f5;float: left; width: 100%;">
                    <div class="panel panel-default">
                       
                        <div class="form-horizontal">
                            <div class="row">
                                <div id="div-show-new">
                                    <div class="row marg search-bg">
                                        <div class="form-horizontal">
                                            
                                            <div class="row">
                                               
                                                <div class="col-lg-3 col-md-3 col-sm-3 cpl-xs-12">
                                                    <div class="form-group">
                                                        <label for="email" class="col-sm-3 padd linhei" style="padding-top: 2px;">
                                                            District:</label>
                                                        <div class="col-sm-8 padd">
                                                            <asp:DropDownList ID="ddlDistrict" runat="server" OnSelectedIndexChanged="ddlDistrict_SelectedIndexChanged"
                                                                                                                           AutoPostBack="true" class="form-control " />
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-lg-3 col-md-3 col-sm-3 cpl-xs-12">
                                                    <div class="form-group">
                                                        <label for="email" class="col-sm-3 padd linhei" style="padding-top: 2px;">
                                                            Block:</label>
                                                        <div class="col-sm-8 padd">
                                                            <asp:DropDownList ID="ddlBlock" OnSelectedIndexChanged="ddlBlock_SelectedIndexChanged" runat="server" AutoPostBack="true"                                                                class="form-control " />
                                                        </div>
                                                    </div>
                                                </div>
                                                <div id="Div1" class="col-lg-3 col-md-2 col-sm-3 cpl-xs-12" runat="server">
                                                    <div class="form-group">
                                                        <label for="email" class="col-sm-3 padd linhei" style="padding-top: 2px;">
                                                            Panchayat:</label>
                                                        <div class="col-sm-8 padd">
                                                            <asp:DropDownList ID="ddlPanchayat" OnSelectedIndexChanged="ddlPanchayat_SelectedIndexChanged" runat="server" AutoPostBack="true" 
                                                                class="form-control " />
                                                        </div>
                                                    </div>
                                                </div>

                                                  <div id="Div2" class="col-lg-3 col-md-3 col-sm-3 cpl-xs-12" runat="server">
                                                    <div class="form-group">
                                                        <label for="email" class="col-sm-3 padd linhei" style="padding-top: 2px;">
                                                            Village:</label>
                                                        <div class="col-sm-8 padd">
                                                            <asp:DropDownList ID="ddlVillage" 
                                                                AutoPostBack="true" runat="server" class="form-control " />
                                                            <span style="width: 5px; float: right; margin: -27px 27px; font-size: 21px;">
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator18" runat="server" Display="Dynamic"
                                                                    ValidationGroup="saves" ControlToValidate="ddlVillage" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                                                            </span>
                                                        </div>
                                                    </div>
                                               
                                                
                                                                  </div>

                                                 

                                            </div>
                                            
                                              
                                           
                                            <%--</ContentTemplate>
</asp:UpdatePanel>
                                            --%>
                                        </div>
                                    </div>

                                    <div class="row marg search-bg">
                                        <div class="form-horizontal">
                                            <%-- <asp:UpdatePanel runat="server" ID="UpMain">
        <ContentTemplate>--%>
                                            <div class="row">
                                                <div class="col-lg-3 col-md-3 col-sm-3 cpl-xs-12">
                                                    <div class="form-group">
                                                        <label for="email" class="col-sm-3 padd linhei" style="padding-top: 2px;">
                                                           SR No:</label>
                                                        <div class="col-sm-8 padd">
                                                            <asp:TextBox ID="txtUniqueId" class="form-control" autocomplete="off" ondrop="return false;" runat="server"></asp:TextBox>
                                                            
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-lg-3 col-md-3 col-sm-3 cpl-xs-12">
                                                    <div class="form-group">
                                                        <label for="email" class="col-sm-3 padd linhei" style="padding-top: 2px;">
                                                            ChildName:</label>
                                                        <div class="col-sm-8 padd">
                                                          <asp:TextBox ID="txtChildname" autocomplete="off" ondrop="return false;" class="form-control" runat="server"></asp:TextBox>
                                                                                                                                  
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-lg-3 col-md-3 col-sm-3 cpl-xs-12">
                                                    <div class="form-group">
                                                        <label for="email" class="col-sm-3 padd linhei" style="padding-top: 2px;">
                                                            FatherName:</label>
                                                        <div class="col-sm-8 padd">
                                                          <asp:TextBox ID="txtFather" autocomplete="off" ondrop="return false;" class="form-control" runat="server"></asp:TextBox>
                                                                                                                            
                                                        </div>
                                                    </div>
                                                </div>
                                                <div id="Div3" class="col-lg-3 col-md-2 col-sm-3 cpl-xs-12" runat="server">
                                                    <div class="form-group">
                                                        <label for="email" class="col-sm-3 padd linhei" style="padding-top: 2px;">
                                                            Class:</label>
                                                        <div class="col-sm-8 padd">
                                                                        <asp:DropDownList ID="ddlclass" 
                                                                AutoPostBack="true" runat="server" class="form-control " />
                                                         
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div id="Div4" class="col-lg-3 col-md-3 col-sm-3 cpl-xs-12" runat="server">
                                                    <div class="form-group">
                                                        <label for="email" class="col-sm-3 padd linhei" style="padding-top: 2px;">
                                                            HH No:</label>
                                                        <div class="col-sm-8 padd">
                                                           
                                                          
                                                                   <asp:TextBox ID="txtHHNo" autocomplete="off" ondrop="return false;" class="form-control" runat="server"></asp:TextBox>
                                                                      
                                                        </div>
                                                    </div>
                                                </div>
                                                
                                                <div class="col-lg-3 col-md-3 col-sm-3 cpl-xs-12">
                                                    <div class="form-group">
                                                        <label for="email" class="col-sm-3 padd linhei" style="padding-top: 2px;">
                                                            Gender:
                                                        </label>
                                                        <div class="col-sm-8 padd">
                                                             <asp:DropDownList ID="ddlGender" runat="server"  class="form-control">
                                                        <asp:ListItem Selected="True" Value="0">--Select--</asp:ListItem>
                                                        <asp:ListItem Value="1">1-Male </asp:ListItem>
                                                         <asp:ListItem Value="2">2-Female</asp:ListItem>
             
        
                                                               </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>

                                           
                                               
                                                <div class="col-lg-2 col-md-2 col-sm-2 cpl-xs-10 col-lg-offset-1 col-md-offset-1 col-sm-offset-1 col-xs-offset-0">
                                            	 <asp:ImageButton ID="btnSerach" ToolTip="Serach" runat="server" OnClick="btnSerach_Click"   class="btn btn-danger btn-paddd pull-left" BackColor="#f1f1f1"  ImageUrl="~/images/search-29.png" />
                                             
                                            
                                                        <asp:ImageButton ID="btnsave" CssClass="btn btn-info pull-right"  BackColor="#f5f5f5" ToolTip="Save"  OnClick="btSave_Click"  ImageUrl="~/images/save-29-1.png"  ValidationGroup="saves" style="margin-right: 5px; padding:0px;" runat="server"  />
                                                         <asp:ImageButton ID="btnAdd" Visible="false" CssClass="btn btn-info pull-right"  BackColor="#f5f5f5" ToolTip="Add" OnClick="btnAdd_Click" ImageUrl="~/images/add-29-1.png"    style="margin-right: 5px; padding:0px;" runat="server" />
                   
                                                 


                                                            </div>
                                             </div>
                                           
                                            <%--</ContentTemplate>
</asp:UpdatePanel>
                                            --%>
                                        </div>
                                    </div>
                                    <div class="col-lg-12 table table-hover " style="padding: 0px;">
                                        <asp:Panel ID="pnlMain"  runat="server">
                                            <div class="form-horizontal">
                                                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="padding: 0px;">
                                                    <div style=" height: 290px; overflow:auto;  width: 99%;" align="center">
                                                        <div>
                                                            <div class="Row" style="width: 100% ">
                                                                <asp:GridView ID="gvnroll" runat="server" CssClass="Grid" OnRowDataBound="GvReport_RowDataBound"   AutoGenerateColumns="False" Font-Names="Arial"
                                                                        Font-Size="12px" Width="100%" >
                                                                           <FooterStyle CssClass="FooterStyle" />
                                                    <HeaderStyle BackColor="#C1C1C1" Height="44px" />
                                                    <RowStyle HorizontalAlign="Center" />
                                                    <SelectedRowStyle BackColor="#897A7A" Font-Bold="True" ForeColor="WhiteSmoke" />
                                                    <AlternatingRowStyle BackColor="#f1f1f1" />
                                                                        <Columns>
                                                                             <asp:TemplateField >
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="Chk1" runat="server"  />
                                                              
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                                          
                                                                            <asp:TemplateField HeaderText="HHNo"  HeaderStyle-CssClass="GridHeaderClass" Visible="true">
                                                                                <ItemTemplate>
                                                                                    <asp:TextBox ID="txtHHNo" class="form-control"  ForeColor="Black" onkeypress="return onlyAlphabetsHH(event,this);" BorderStyle="None"  onchange="checkPwd(this.value);"  autocomplete="off" ondrop="return false;"   runat="server" Text='<%# Eval("HHNo") %>'></asp:TextBox>
                                                                                </ItemTemplate>
                                                                               
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Student Name"  HeaderStyle-CssClass="GridHeaderClass"  Visible="true">
                                                                                <ItemTemplate>
                                                                                    <asp:TextBox ID="txtChildName" class="form-control" onkeypress="return onlyAlphabets(event,this);"  BorderStyle="None"  autocomplete="off" ondrop="return false;" ForeColor="Black" runat="server"
                                                                                        Text='<%# Eval("ChildName") %>'></asp:TextBox>
                                                                                </ItemTemplate>
                                                                                
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Father Name"  HeaderStyle-CssClass="GridHeaderClass" Visible="true">
                                                                                <ItemTemplate>
                                                                                    <asp:TextBox ID="txtFatherName" class="form-control" onkeypress="return onlyAlphabets(event,this);" BorderStyle="None"  autocomplete="off" ondrop="return false;" ForeColor="Black" runat="server"
                                                                                        Text='<%# Eval("FatherName") %>'></asp:TextBox>
                                                                                </ItemTemplate>
                                                                                
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Class"  HeaderStyle-CssClass="GridHeaderClass" Visible="true">
                                                                                <ItemTemplate>
                                                                                    <asp:DropDownList ID="dllClass" class="form-control"  BorderStyle="None"  runat="server">
                                                                                    </asp:DropDownList>

                                                                                     <asp:TextBox ID="txtvclass" Visible="false" class="form-control" ForeColor="Black" BorderStyle="None"  autocomplete="off" ondrop="return false;" runat="server" Text='<%# Eval("DoChild") %>'></asp:TextBox>
                                                    
                                                                                </ItemTemplate>
                                                                              
                                                                            </asp:TemplateField>
                                                                            
                                                                            <asp:TemplateField HeaderText="SR. NO."  HeaderStyle-CssClass="GridHeaderClass" Visible="true">
                                                                                <ItemTemplate>
                                                                                    <asp:TextBox ID="txtSrno" class="form-control"  ForeColor="Black" runat="server"  BorderStyle="None"  onchange="checkPwd(this.value);" autocomplete="off" ondrop="return false;" Text='<%# Eval("EnrollSerialNo") %>'></asp:TextBox>
                                                                                </ItemTemplate>
                                                                                
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Admission Date"  HeaderStyle-CssClass="GridHeaderClass" Visible="true">
                                                                                <ItemTemplate>
                                                                                        <asp:TextBox runat="server"  ID="txtDate"  autocomplete="off" ondrop="return false;" BorderStyle="None"  Text='<%# Eval("EnrolmentDate") %>' class="form-control" onkeypress="return false;"                     
                                               ></asp:TextBox>
                                         
                                            <ajax:CalendarExtender ID="gg"  runat="server" Enabled="True"
                                                Format="dd/MM/yyyy" TargetControlID="txtDate" OnClientDateSelectionChanged="arrivaldatecheck"  PopupPosition="BottomRight"></ajax:CalendarExtender>

                                                   
                                                                                </ItemTemplate>
                                                                                 <ItemStyle CssClass="padding-lef" />

                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Social Category"  HeaderStyle-CssClass="GridHeaderClass" Visible="true">
                                                                                <ItemTemplate>
                                                                                   <asp:DropDownList ID="ddlScat" class="form-control"   BorderStyle="None"  runat="server">
                                                                                    </asp:DropDownList>

                                                                                          <asp:TextBox ID="txtsCate"  Visible="false" autocomplete="off" ondrop="return false;" ForeColor="Black" runat="server" Text='<%# Eval("SocialCategory") %>'></asp:TextBox>
                                                    
                                                                                </ItemTemplate>
                                                                         
                                                                            </asp:TemplateField>
                                                                         
                                                                            
                                                                            <asp:TemplateField HeaderText="DOB" Visible="true"  HeaderStyle-CssClass="GridHeaderClass">
                                                                                <ItemTemplate>
                                                                                
                                                                                                <asp:TextBox runat="server"  ID="txtDob"  autocomplete="off" ondrop="return false;"  class="form-control" onkeypress="return false;"                     
                                               ></asp:TextBox>
                                         
                                            <ajax:CalendarExtender ID="CalendarExtenderTourdate"  runat="server" Enabled="True"
                                                Format="dd/MM/yyyy" TargetControlID="txtDob" PopupPosition="BottomRight"></ajax:CalendarExtender>
                                                                                 <asp:TextBox ID="txtaddate" Visible="false"  ForeColor="Black" runat="server" Text='<%# Eval("DOB") %>'></asp:TextBox>
                                                  
                                                                                      </ItemTemplate>
                                                                                 
                                                                            </asp:TemplateField>
                                                                            
                                                                            <asp:TemplateField HeaderText="Enrollment Category"   HeaderStyle-CssClass="GridHeaderClass" Visible="true">
                                                                                <ItemTemplate>
                                                                                   <asp:DropDownList ID="ddlEnroll"  BorderStyle="None"  class="form-control" runat="server">
                                                                                    </asp:DropDownList>
                                                                                      <asp:TextBox ID="txtenroll" Visible="false"  ForeColor="Black" runat="server" Text='<%# Eval("EnrolmentCategory") %>'></asp:TextBox>
                                                  
                                                                                      </ItemTemplate>
                                                                                 
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Education Status"   HeaderStyle-CssClass="GridHeaderClass" Visible="true">
                                                                                <ItemTemplate>
                                                                                     <asp:DropDownList ID="ddlEduationStatus" class="form-control"  BorderStyle="None"  runat="server">
                                                                                    </asp:DropDownList>
                                                                                    <asp:TextBox ID="txtEduationStatus" Visible="false"  ForeColor="Black" runat="server" Text='<%# Eval("EduationStatus") %>'></asp:TextBox>
                                                  
                                                                                           </ItemTemplate>
                                                                                 
                                                                            </asp:TemplateField>
                                                                          
                                                                            <asp:TemplateField HeaderText="Education Status"  Visible="false"  HeaderStyle-CssClass="GridHeaderClass" >
                                                                                <ItemTemplate>
                                                                                   
                                                                                    <asp:Label ID="lblUniqueCode" Visible="false"  ForeColor="Black" runat="server" Text='<%# Eval("UniqueCode") %>'></asp:Label>
                                                  
                                                                                           </ItemTemplate>
                                                                                 
                                                                            </asp:TemplateField>
                                                                        </Columns>
                                                                        
                                                                    </asp:GridView>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </asp:Panel>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <!-- /#wrapper -->
                        <!-- /#wrapper -->
                    </div>
                </div></div>
                </ContentTemplate>
    </asp:UpdatePanel>
    </div>
    
    </form>
</body>
</html>
