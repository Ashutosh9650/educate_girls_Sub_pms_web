<%@ Page Language="C#" AutoEventWireup="true"  Culture="en-GB" CodeFile="frmReAddEnrollment.aspx.cs" Inherits="frmReAddEnrollment" %>
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
                
             }

             if (msg != "") {
                 str.value = "";
                 alert(msg);
                 return false;
             }
             else { return true; }
         }
    </script>
        <style type="text/css">
        .ajax__calendar_container {
            z-index: 1000;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
   
     <ajax:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
      </ajax:ToolkitScriptManager>
     <asp:UpdatePanel runat="server" ID="UpMain">
        <ContentTemplate>
      
             <div class="col-lg-10 col-md-12 col-sm-12 col-xs-12  col-xs-12 col-lg-offset-1 col-md-offset-1 col-sm-offset-1 col-xs-offset-0" style="padding-left: -2px;">
                <div class="thumbnail" style="background-color: #f5f5f5;float: left; width: 80%;">
                    <div class="panel panel-default">
                       
                        <div class="form-horizontal">
                            <div class="row">
                                <div id="div-show-new" style="width: 96%; float: left; margin-left: 25px;">
                                    <div class="row marg search-bg">
                                        <div class="form-horizontal">
                                            
                                            <div class="row" >
                                               
                                                <div class="col-lg-3 col-md-3 col-sm-3 cpl-xs-12">
                                                    <div class="form-group">
                                                        <label for="email" class="col-sm-3 padd linhei" >
                                                            Panchayat:</label>
                                                        <div class="col-sm-8 padd" style="padding-left: 15px;">
                                                            <asp:Label ID="lblPhy"  class="col-sm-3 padd linhei"  ForeColor="Black" runat="server" Text="Label"></asp:Label>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-lg-3 col-md-3 col-sm-3 cpl-xs-12">
                                                    <div class="form-group">
                                                        <label for="email" class="col-sm-3 padd linhei" >
                                                            Village:</label>
                                                        <div class="col-sm-8 padd" style="padding-left: 15px;">
                                                      <asp:Label ID="lblVillage" class="col-sm-3 padd linhei" runat="server" ForeColor="Black" Text="Label"></asp:Label>
                                                        </div>
                                                    </div>
                                                </div> 
                                                <div id="Div1" class="col-lg-4 col-md-4 col-sm-4 cpl-xs-12" runat="server">
                                                    <div class="form-group">
                                                        <label for="email" class="col-sm-3 padd linhei" >
                                                            School:</label>
                                                        <div   style="padding-left: 15px;">
                                                             <asp:Label ID="lblSchool" class="padd " ForeColor="Black"  runat="server" Text="Label" ></asp:Label>
                                                        </div>
                                                    </div>
                                                </div>

                                                  
                                                        <div class="col-lg-2 col-md-2 col-sm-2 cpl-xs-12">
                                                                 <span style="float:right">
                                                <asp:ImageButton ID="ImageButton1" ValidationGroup="Valid" class="btn btn-primary pull-right" 
                                                                                ToolTip="Save" runat="server" OnClick="btSave_Click" BackColor="#f5f5f5"  ImageUrl="~/images/save-29-1.png" /></span>

                                                                 </div>   
                                                                        
                                                        
                                                                  </div>

                                                 
                                            </div>
                                            
                                              
                             
                                            
                                        </div>
                                    </div>

                                   


<div class="col-lg-8 col-md-8col-sm-12 col-xs-12 ">
    <div class="panel panel-default">
   
            <div class="panel-body">
            <div class="row">
                <div class="col-lg-8 col-md-8 col-sm-10 col-xs-12">
                    <div   class="form-horizontal">
                                    <div id="a" runat="server" class="row">
                                	<div class="form-group">
                                      <label class="control-label col-sm-4" for="Name">Gender  <span class="req">*</span> </label>
                                      
                                       <div class="col-sm-6">
                               <asp:DropDownList ID="ddlGender" CssClass="form-control"  runat="server" AutoPostBack="True" >
                                  <asp:ListItem Selected="True" Value="0">--Select--</asp:ListItem>
                                        <asp:ListItem Value="1">1-Male </asp:ListItem>
                                         <asp:ListItem Value="2">2-Female</asp:ListItem>
                               </asp:DropDownList>
                                              
                               
                                <span style="width: 5px;float: right;margin: -27px 27px;font-size: 21px;">
                               <asp:RequiredFieldValidator ID="RequiredFieldValidatorlevel" runat="server" 
                        ControlToValidate="ddlGender" InitialValue="0" ErrorMessage="*" ForeColor="Red"
                        ValidationGroup="Valid">

                        </asp:RequiredFieldValidator></span>
                                </div></div>
                                </div>
                                  <div class="row">
                                	<div class="form-group">
                                      <label class="control-label col-sm-4" for="Name">House/Family No<span class="req">*</span></label>
                                     
                                       <div class="col-sm-6">
                                                     <asp:TextBox ID="txtHHNo" class="form-control" onkeypress="return onlyAlphabetsHH(event,this);"  onchange="checkPwd(this.value);"  autocomplete="off" ondrop="return false;"  ForeColor="Black" runat="server"></asp:TextBox>
                                                       
                                                                                                                                  
                                
                                                    
                                </div>
                                </div>
                                </div>
                                       <div class="row">
                                      <div class="form-group">
                                      <label class="control-label col-sm-4" for="Name"style="padding-top:14px">Student Name  <span class="req">*</span></label>
                                      
                                       <div class="col-sm-6">
                       <asp:TextBox ID="txtChildName" class="form-control" onkeypress="return onlyAlphabets(event,this);" autocomplete="off" ondrop="return false;" ForeColor="Black" runat="server"
                                                                                       ></asp:TextBox>
                      

                                </div></div>
                                      </div>
                                  

                                    <div  class="row">
                                      <div class="form-group">
                                      <label class="control-label col-sm-4" for="Name"style="padding-top:14px">Father Name </label>
                                      
                                       <div class="col-sm-6">
                             
                                 <asp:TextBox ID="txtFatherName" class="form-control" onkeypress="return onlyAlphabets(event,this);" autocomplete="off" ondrop="return false;" ForeColor="Black" runat="server"
                                                                                        ></asp:TextBox>
                                                    
                   
</div></div>
                                </div>
                           

                                       <div class="row" id="Div2" runat="server">
                                <div class="form-group">
                                      <label class="control-label col-sm-4" for="Name" id="Label2" runat="server">Class </label>
                                      
                                       <div class="col-sm-6">
                                                              
             <asp:DropDownList ID="dllClass" class="form-control" runat="server">
                                                                                    </asp:DropDownList>
                                
                                </div>
                                </div>
                                
                                </div>

                                    <div class="row" id="Div3" runat="server">
                                <div class="form-group">
                                      <label class="control-label col-sm-4" for="Name" id="Label3" runat="server">SR NO.</label>
                                      
                                       <div class="col-sm-6">
                              <asp:TextBox ID="txtSrno" class="form-control"  ForeColor="Black" runat="server" MaxLength="9"  onchange="checkPwd(this.value);" autocomplete="off" ondrop="return false;" ></asp:TextBox>
                                                           
                                </div>
                                </div>
                                
                                </div>


                       
                            

                                
       <div id="Div4" runat="server"  class="row">
                                	<div class="form-group">
                                      <label class="control-label col-sm-4" for="Name" id="Label4" runat="server">Admission Date  </label>
                                      
                                       <div class="col-sm-8">
                           <asp:TextBox runat="server"  ID="txtBirth" Width="73%"  autocomplete="off" ondrop="return false;"  class="form-control" onkeypress="return false;"                     
                                               ></asp:TextBox>
                                         
                                            <ajax:CalendarExtender ID="clk"  runat="server" Enabled="True"
                                                Format="dd/MM/yyyy" TargetControlID="txtBirth" OnClientDateSelectionChanged="arrivaldatecheck" PopupPosition="BottomRight"></ajax:CalendarExtender>
                                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtBirth"
                                                Display="Dynamic" ErrorMessage="*" Font-Bold="False" Font-Size="9px" ForeColor="Red"
                                                SetFocusOnError="True" ValidationGroup="saves"></asp:RequiredFieldValidator>
      

                    
                                </div>
                                </div>
                                </div>
                       

                       
                                    <div class="row" id="Div5" runat="server">
                                <div class="form-group">
                                      <label class="control-label col-sm-4" for="Name" id="Label5" runat="server">Social Category</label>
                                      
                                       <div class="col-sm-6">
                          <asp:DropDownList ID="ddlScat" class="form-control" runat="server"></asp:DropDownList>
                                
                                </div>
                                </div>
                                
                                </div>


               <div id="Divkj2" runat="server"  class="row">
                                	<div class="form-group">
                                      <label class="control-label col-sm-4" for="Name" id="Label1" runat="server">DOB<span class="req">*</span> </label>
                                      
                                       <div class="col-sm-8">
                           <asp:TextBox runat="server"  ID="txtDobDate"  Width="73%"  autocomplete="off" ondrop="return false;"  class="form-control" onkeypress="return false;"                     
                                               ></asp:TextBox>
                                         
                                            <ajax:CalendarExtender ID="CalendarExtender1"  runat="server" Enabled="True"
                                                Format="dd/MM/yyyy" TargetControlID="txtDobDate" OnClientDateSelectionChanged="arrivaldatecheck" PopupPosition="BottomRight"></ajax:CalendarExtender>
   <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="txtDobDate"
                                                Display="Dynamic" ErrorMessage="*" Font-Bold="False" Font-Size="9px" ForeColor="Red"
                                                SetFocusOnError="True" ValidationGroup="saves"></asp:RequiredFieldValidator>
      

                        
                                </div>
                                </div>
                                </div>

                              <div class="row" id="Div6" runat="server">
                                <div class="form-group">
                                      <label class="control-label col-sm-4" for="Name" id="Label6" runat="server">Enrollment Category</label>

                                       <div class="col-sm-6">
                               <asp:DropDownList ID="ddlEnroll" class="form-control" runat="server">
                                                                                    </asp:DropDownList>
                                
                                </div>
                                </div>
                                
                                </div>

      <div class="row" id="Div7" runat="server">
                                <div class="form-group">
                                      <label class="control-label col-sm-4" for="Name" id="Label7" runat="server">Previous Educational Status</label>

                                       <div class="col-sm-6">
                           <asp:DropDownList ID="ddlEduationStatus" class="form-control" runat="server">
                                                                                    </asp:DropDownList>
                                
                                </div>
                                </div>
                                
                                </div>

                                </div>
                </div>
            </div>
            
            </div>
</div>
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
  
    
    </form>
</body>
</html>
