<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmNewSchoolActivity.aspx.cs"  Culture="en-GB" MasterPageFile="~/Site.master" Inherits="frmNewSchoolActivity" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script type="text/javascript">
        function SetMultilanguage(Flag, clsname) {

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

                if (Lngg.toLowerCase().indexOf("others (specify)") >= 0) {

                    $('#<%=TxtSmcOther.ClientID %>').val('');
                    $('#<%=TxtSmcOther.ClientID %>').attr('disabled', false);
                }
                else {
                   
                    $('#<%=TxtSmcOther.ClientID %>').val('');
                    $('#<%=TxtSmcOther.ClientID %>').attr('disabled', true);
                }
            }
        }
         </script>  
             <script type="text/javascript">
                 function checkFilledNew(id, lablId,lblDr,lblMain) {
                   
                     var inputVal = document.getElementById(id);
                     var icount = $("." + lblDr).val();
                   //  var iwaterpre = $("." + lablId).val();
                     var iwaterpre = $("." + lblMain).val();
                     debugger;
                     if (iwaterpre == 1) {
                         if (icount == 0) {
                             icount = 3;
                         }
                         else if (icount == 1) {
                             icount = 3;
                         }
                         else if (icount == 2) {
                             icount = 3;
                         }
                         if (icount == 3) {
                          
                             inputVal.style.backgroundColor = "Green"
                             //btn_water.setBackgroundResource(R.drawable.bg_buttonroundreen);

                             icount++;
                             $("." + lblDr).val(icount);
                             $("." + lablId).val(1);
                           
                         
                         }
                         else if (icount == 4) {
                             
                             inputVal.style.backgroundColor = "Blue"
                        

                             $("." + lablId).val(4);
                             icount = 3;
                             $("." + lblDr).val(icount);
                         }

                     }
                     else if (iwaterpre == 2) {
                         if (icount == 0) {
                             icount = 2;
                         }
                         else if (icount == 1) {
                             icount = 2;
                         }
                         if (icount == 1) {
                            
                             //    btn_water.setBackgroundResource(R.drawable.bg_buttonroundred);
                             icount++;
                             inputVal.style.backgroundColor = "Red"


                             $("." + lablId).val(3);

                             $("." + lblDr).val(icount);
                         }
                         else if (icount == 2) {
                           
                             //   btn_water.setBackgroundResource(R.drawable.bg_buttonroundorane);


                             inputVal.style.backgroundColor = "Orange"

                             icount++;


                             $("." + lablId).val(2);
                            
                             $("." + lblDr).val(icount);
                         }
                         else if (icount == 3) {
                           
                             //btn_water.setBackgroundResource(R.drawable.bg_buttonroundreen);
                             icount = 2;

                             inputVal.style.backgroundColor = "Green"


                             $("." + lablId).val(1);
                             $("." + lblDr).val(icount);
                         }

                     }
                     else if (iwaterpre == 3) {
                       
                         if (icount == 0) {
                           
                             icount = 3;
                         } 
			
                         if (icount == 2) {

                             //   btn_water.setBackgroundResource(R.drawable.bg_buttonroundorane);
                             icount=4;
                           

                             $("." + lblDr).val(icount);
                             inputVal.style.backgroundColor = "Orange"


                             $("." + lablId).val(2);

                         }
//                         if (icount == 1) {

//                             //  btn_water.setBackgroundResource(R.drawable.bg_buttonroundreen);

//                             icount = 0;
//                           
//                             $("." + lblDr).val(icount);

//                             inputVal.style.backgroundColor = "Green"


//                             $("." + lablId).val(1);
//                         }

                         else
                            
                              if (icount == 1) {

                                 //  btn_water.setBackgroundResource(R.drawable.bg_buttonroundreen);
                               
                                 icount = 0;

                                 $("." + lblDr).val(icount);

                                 inputVal.style.backgroundColor = "Green"


                                 $("." + lablId).val(1);
                             }
                             else if (icount == 2) {

                                 //   btn_water.setBackgroundResource(R.drawable.bg_buttonroundorane);
                                 icount++;
                              
                                 $("." + lblDr).val(icount);
                                 inputVal.style.backgroundColor = "Orange"


                                 $("." + lablId).val(2);

                             }
                           else  if (icount ==3) {

                                 //   btn_water.setBackgroundResource(R.drawable.bg_buttonroundorane);
                                 inputVal.style.backgroundColor = "Red"
                               
                                 icount = 2;
                                 $("." + lablId).val(3);
                                 $("." + lblDr).val(icount);

                             }
                             else if (icount == 4) {

                                 //   btn_water.setBackgroundResource(R.drawable.bg_buttonroundblue);

                                 inputVal.style.backgroundColor = "Blue"

                                 icount=1;
                                 $("." + lablId).val(4);
                               
                                 $("." + lblDr).val(icount);
                             }
                            

                     }
                     else if (iwaterpre == 4) {
                         if (icount == 0) {
                             icount = 4;
                         }
                         else if (icount == 1) {
                             icount = 4;
                         }
                         else if (icount == 2) {
                             icount = 4;
                         }
                         if (icount == 3) {
                             //btn_water.setBackgroundResource(R.drawable.bg_buttonroundreen);
                   
                             inputVal.style.backgroundColor = "Green"

                             icount++;
                             $("." + lablId).val(1);
                             
                             $("." + lblDr).val(icount);
                         }
                         else if (icount == 4) {
     
                             //   btn_water.setBackgroundResource(R.drawable.bg_buttonroundblue);
                            
                             inputVal.style.backgroundColor = "Blue"


                             $("." + lablId).val(4);
                             icount = 3;
                             $("." + lblDr).val(icount);
                         }

                     }
                     else {
                         if (icount == 1) {
                            
                             //btn_water.setBackgroundResource(R.drawable.bg_buttonroundred);
                             icount++;

                     
                             inputVal.style.backgroundColor = "Red"


                             $("." + lablId).val(3);
                             $("." + lblDr).val(icount);
                         }
                         else if (icount == 2) {
                         
                             //   btn_water.setBackgroundResource(R.drawable.bg_buttonroundorane);
                             icount++;

                       

                             inputVal.style.backgroundColor = "Orange"


                             $("." + lablId).val(2);
                             $("." + lblDr).val(icount);
                         }
                         else if (icount == 3) {
                           
                             // btn_water.setBackgroundResource(R.drawable.bg_buttonroundreen);

                             // btn_water.setBackgroundResource(R.drawable.green_btn_radio_holo_light);
                             icount++;

                          

                             inputVal.style.backgroundColor = "Green"


                             $("." + lablId).val(1);
                             $("." + lblDr).val(icount);
                         }
                         else if (icount == 4) {
                             //  btn_water.setBackgroundResource(R.drawable.bg_buttonroundblue);
                          
                             // btn_water.setBackgroundResource(R.drawable.purple_btn_radio_holo_light);
                             icount++;

                     


                             inputVal.style.backgroundColor = "Blue"


                             $("." + lablId).val(4);
                             icount = 0;
                             $("." + lblDr).val(icount);
                         }
                         else if (icount == 0) {
                           
                             inputVal.style.backgroundColor = "White"
                             /// btn_water.setBackgroundResource(R.drawable.bg_buttonroundwhite);

                             icount++;
                             $("." + lblDr).val(icount);

                         }

                     }
                   
                 }
    </script>
          <script type="text/javascript">
              function checkFilled(id, lablId) {
                  var inputVal = document.getElementById(id);

                  if (inputVal.style.backgroundColor == "white") {
                      inputVal.style.backgroundColor = "RED";
                      $("." + lablId).val(3);
                  }

                  else if (inputVal.style.backgroundColor == "RED") {
                      inputVal.style.backgroundColor = "Orange";
                      $("." + lablId).val(2);

                  }
                  else if (inputVal.style.backgroundColor == "Orange") {

                      inputVal.style.backgroundColor = "Blue";
                      $("." + lablId).val(4);

                  }
                  else if (inputVal.style.backgroundColor == "Blue") {
                      inputVal.style.backgroundColor = "Green";
                      $("." + lablId).val(1);
                  }
                  else if (inputVal.style.backgroundColor == "Green") {
                      inputVal.style.backgroundColor = "white";

                  }
                  else {
                      inputVal.style.backgroundColor = "white";

                  }
              }
    </script>
 <script type="text/javascript">


     function DiscCode(inputtxt) {

         var lodgingtot = document.getElementById('<%=txtOtherSIPFC.ClientID %>');



         if (lodgingtot.value <= 20) {

             return true;
         }
         else {

             lodgingtot.value = '';
             alert("Please ensure that  Other SIP prepared number should be less than 20 ");

             return false;
         }
     }  
    
    </script>
    <script type="text/javascript">


        function SMC(inputtxt) {

            var lodgingtot = document.getElementById('<%=txtSMCMeeting.ClientID %>');



            if (lodgingtot.value <= 25) {

                return true;
            }
            else {

                lodgingtot.value = '';
                alert("Please ensure that  number should be less than 25 ");

                return false;
            }
        }  
    
    </script>
        <script type="text/javascript">


            function SMCOrient(inputtxt) {

                var lodgingtot = document.getElementById('<%=txtTotalMember.ClientID %>');
               

                if (lodgingtot.value <= 16) {

                    return true;
                }
                else {

                    lodgingtot.value = '';
                    alert("Please ensure that  Total Trained Member number should be less than 16 ");

                    return false;
                }
            }  
    
    </script>

       <script type="text/javascript">


           function SMCOrientNew1(inputtxt) {

               var lodgingtot = document.getElementById('<%=txtTotalFmember.ClientID %>');

               if (lodgingtot.value >= 6) {
               }
               else {
                   lodgingtot.value = '';
                   alert("Please ensure that Total Trained Female Member number should be greater than 6 ");

                   return false;
               }

               if (lodgingtot.value <= 16) {

                   return true;
               }
               else {

                   lodgingtot.value = '';
                   alert("Please ensure that Total Trained Female Member number should be less than 16 ");

                   return false;
               }
           }  
    
    </script>
     <script type="text/javascript">


         function SMCOrientNew(inputtxt) {

             var lodgingtot = document.getElementById('<%=txtTotalFmember.ClientID %>');

            
             if (lodgingtot.value <= 16) {

                 return true;
             }
             else {

                 lodgingtot.value = '';
                 alert("Please ensure that Total Trained Female Member number should be less than 16 ");

                 return false;
             }
         }  
    
    </script>
    
     <script type="text/javascript">


         function OtherSIp(inputtxt) {

             var lodgingtot = document.getElementById('<%=txtsmcmeetinFC.ClientID %>');



             if (lodgingtot.value <= 20) {

                 return true;
             }
             else {

                 lodgingtot.value = '';
                 alert("Please ensure that  Other SIP completed number should be less than 20  ");

                 return false;
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
    <style type="text/css">
         .ajax__calendar .ajax__calendar_invalid .ajax__calendar_day { background-color:gray; color:White; text-decoration:none; cursor:default; } 
        </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">



<div class="row" style="margin-top: 120px;">
<%--<asp:UpdatePanel ID="UpdatePanel1"  runat="server">
        <ContentTemplate>--%>
        
    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
        <div class="row marg search-bg">
            <div class="form-horizontal">
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
                <div class="col-lg-2 col-md-2 col-sm-2 cpl-xs-12">
                    <div class="form-group" style="margin-bottom: 7px;">
                        <label for="email" class="col-sm-3 padd linhei">
                            Village:</label>
                        <div class="col-sm-9 padd">
                             <asp:DropDownList ID="ddlVilage" OnSelectedIndexChanged="ddlVilage_SelectedIndexChanged"
                                                runat="server" AutoPostBack="true" class="form-control " />
                        </div>
                    </div>
                </div>
                <div class="col-lg-2 col-md-2 col-sm-2 cpl-xs-12">
                    <div class="form-group" style="margin-bottom: 7px;">
                        <label for="email" class="col-sm-3 padd linhei">
                            Date:</label>
                        <div class="col-sm-9 padd">
                           <asp:TextBox runat="server" ID="txtDate"  autocomplete="off" ondrop="return false;"
                                                class="form-control" onkeypress="return false;"></asp:TextBox>
                                          
                                            <ajax:CalendarExtender ID="CalendarExtenderTourdate" OnClientDateSelectionChanged="arrivaldatecheck" runat="server" Enabled="True"
                                                Format="dd/MM/yyyy" TargetControlID="txtDate" PopupPosition="BottomRight">
                                            </ajax:CalendarExtender>
                                            <asp:RequiredFieldValidator ID="ReqTxtDate" runat="server" ControlToValidate="txtDate"
                                                Display="Dynamic" ErrorMessage="*" Font-Bold="False" Font-Size="9px" ForeColor="Red"
                                                SetFocusOnError="True" ValidationGroup="saves"></asp:RequiredFieldValidator>
                            <span id="ctl00_MainContent_ReqTxtDate" style="color:Red;font-size:9px;font-weight:normal;display:none;">*</span>
                        </div>
                    </div>
                </div>
                <div class="col-lg-2 col-md-2 col-sm-2 cpl-xs-12">
                    <div class="form-group" style="margin-bottom: 7px;">
                        <label for="email" class="col-sm-3 padd linhei">
                            School:</label>
                        <div class="col-sm-9 padd">
                          <asp:DropDownList ID="ddlSchool"   OnSelectedIndexChanged="ddlSchool_SelectedIndexChanged" AutoPostBack="true"   runat="server" class="form-control " />
                        </div>
                    </div>
                </div>
                <div class="col-lg-3 col-md-3 col-sm-3 cpl-xs-12">
                       <asp:ImageButton ID="btnDelete" OnClick="btnDelete_Click" CssClass="btn btn-info pull-right" ToolTip="Delete" BackColor="#f5f5f5" ImageUrl="~/images/delete-29.png"  style="margin-right: 5px; padding:0px;" runat="server" />
                                                 
                  <asp:Button ID="btnApprove"   CssClass="btn btn-success pull-right " 
                                 ToolTip="Save" Text="  Back"   OnClick="btnApprove_Click" 
                                Style="margin-right: 5px; padding: 0px;" runat="server" />      
                             <asp:ImageButton ID="btnsave" CssClass="btn btn-info pull-right" OnClick="btnSave_Click"
                                        BackColor="#f5f5f5" ToolTip="Save" ImageUrl="~/images/save-29-1.png" ValidationGroup="saves"
                                        Style="margin-right: 5px; padding: 0px;" runat="server" />
                                   
                 <asp:ImageButton ID="btnSerach"  OnClick="btnSerach_Click" ToolTip="Serach" runat="server" class="btn btn-danger btn-paddd pull-right"
                                                    BackColor="#f1f1f1" ImageUrl="~/images/search-29.png" />  
                                                    
                                        
                 <asp:ImageButton ID="btnEdit"  ToolTip="Edit" OnClick="btnEdit_Click" runat="server" class="btn btn-danger btn-paddd pull-right"
                                                    BackColor="#f1f1f1" ImageUrl="~/images/edit.png" />    
                                                                    
                                                     </div>
            </div>
        </div>
        </div>

     
        
    <%--      </ContentTemplate>
    
    </asp:UpdatePanel>--%> 
    </div>
    <div class="row" id="idImage" runat="server" visible="false">
    <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Conditional" runat="server">
        <ContentTemplate>
      <div class="col-lg-3 col-md-3 col-sm-3 cpl-xs-12">
                                    <div class="form-group" style="margin-bottom: 7px;">
                                        <label for="email" class="col-sm-3 padd linhei">
                                             Image :</label>
                                        <div class="col-sm-4 padd" style="padding-left: 31px;">
                                               <asp:ImageButton ID="imgComm1" runat="server" Width="30" Height="25"   OnClick="btnimgComm1_Click" Visible="false"   CssClass="pull-right" ImageUrl="~/images/iconimage-128.png"></asp:ImageButton >
                                          <asp:Label ID="lblMM" Visible="false" runat="server" Text="Label"></asp:Label>
                                        </div>
                                    </div>
                                </div>
                                 <cc1:ModalPopupExtender ID="MpexdrDistrict" runat="server" BackgroundCssClass="modalBg "
                    PopupControlID="PnlDistrict"   CancelControlID="CancelButton"  TargetControlID="HdnFild7">
                </cc1:ModalPopupExtender>
            
                 <asp:HiddenField ID="HdnFild7" runat="server"></asp:HiddenField>
                 <asp:Panel cssclass="model-wid mod-posi"  Style="display: none;height:auto;width: 45% !important; margin-top:125px !important;" ID="PnlDistrict" runat="server">
                   
                    <div style="width:100%;height:auto;background-color:#f1f1f1">
                    <div class="modal-header"  style="background-color:#3ac0f2;color:White;">
                  
                    </div>
                   <div class="modal-body">
                   <asp:Label ID="lblmsg" runat="server" ForeColor="Red" Font-Names="Verdana" Font-Size="11px"></asp:Label>
                   <div class="form-horizontal" >

                           </div>
                           
                        <asp:ImageMap ID="imgMKS" runat="server" Height="250px" Width="400px" BorderColor="Black"
                                                            BorderStyle="Ridge" BorderWidth="1px" />

                  
                   </div>
                    <div class="modal-footer">
                  
                            <asp:Button ID="CancelButton" runat="server" CssClass="btn bgm-cyan" Text="Close"
                                ToolTip="Close" Style="float: none;"></asp:Button></div>
                  
                    </div>
                    
                       
                       
                </asp:Panel>

</ContentTemplate>
    
    </asp:UpdatePanel> 
    </div>
    <asp:Panel ID="pnlMain" runat="server"  >
    <asp:UpdatePanel ID="UpdatePanel2" UpdateMode="Conditional" runat="server">
        <ContentTemplate>
       <div class="row">
        <div class="col-lg-4 col-md-4 col-sm-6 col-xs-12">
            <div class="navbar-header">
                <button type="button" class="navbar-toggle" data-toggle="collapse" data-target="#myNavbar">
                    <span class="">
                        <p class="text-danger" style="margin: 0px; font-weight: bold;">
                         T.B. Handholding
                           
                           </p>
                    </span>
                </button>
            </div>
            <div class="collapse navbar-collapse" id="Div6">
                <div style="overflow: auto;">
                    <table class="table table-striped table-bordered table-hover">
                        <tbody><tr>
                            <td colspan="3">
                                <p class="text-danger" style="margin: 0px; font-weight: bold;">
                                <asp:CheckBox ID="chkHolding" runat="server" />   T.B. Handholding</p>
                            </td>
                        </tr>
                       
                        

                    </tbody></table>
                </div>
            </div>
        </div>

        <div class="col-lg-4 col-md-4 col-sm-6 col-xs-12">
            <div class="navbar-header">
                <button type="button" class="navbar-toggle" data-toggle="collapse" data-target="#myNavbar">
                    <span class="">
                        <p class="text-danger" style="margin: 0px; font-weight: bold;">
                           Enrolled/Ineligible</p>
                    </span>
                </button>
            </div>
            <div class="collapse navbar-collapse" id="Div7">
                <div>
                    <table class="table table-striped table-bordered table-hover">
                        <tbody><tr>
                            <td colspan="2">
                                <p class="text-danger" style="margin: 0px; font-weight: bold;">
                           Enrolled/Ineligible</p>
                            </td>
                            <td>
                                            <asp:LinkButton ID="lnkEnrool" OnClick="lnkEnrool_OnClick" runat="server">Click Here</asp:LinkButton>
                            </td>
                        </tr>
                        
                     
                    </tbody></table>
                </div>
            </div>
        </div>
      
      <div class="col-lg-4 col-md-4 col-sm-6 col-xs-12">
            <div class="navbar-header">
                <button type="button" class="navbar-toggle" data-toggle="collapse" data-target="#myNavbar">
                    <span class="">
                        <p class="text-danger" style="margin: 0px; font-weight: bold;">
                           Others </p>
                    </span>
                </button>
            </div>
            <div class="collapse navbar-collapse" id="Div9">
                <div style="overflow: auto; min-height: 65px">
                    <table class="table table-striped table-bordered table-hover">
                        <tbody><tr>
                            <td colspan="1" style="padding: 7px;">
                                <p class="text-danger" style="margin: 0px; font-weight: bold;">
                           Others</p>
                            </td>
                            <td>
                            <asp:TextBox ID="txtOther" style="height: 22px;" runat="server" CssClass="form-control" ></asp:TextBox>
                           
                            </td>
                        </tr>
                       
                        

              

                    </tbody></table>
                </div>
            </div>
        </div>
         
    </div>

         <cc1:ModalPopupExtender ID="ModalPopupExtender" runat="server" TargetControlID="Hdn_model3"
            PopupControlID="pnlpopup3" BackgroundCssClass="modalBackground">
        </cc1:ModalPopupExtender>
        <asp:HiddenField ID="Hdn_model3" runat="server" />
        <asp:Panel ID="pnlpopup3" runat="server" Style="display: none;">
            <div class="modal-dialog modal-md">
                <div class="modal-content">
                    <div class="modal-header">
                    <asp:ImageButton ID="btnAdd" CssClass="btn btn-info pull-right" BackColor="#f5f5f5"
                            ToolTip="Add" ImageUrl="~/images/close-29.png"   Style="margin-right: 5px;
                            padding: 0px;" runat="server" />

                                <asp:ImageButton ID="ImageButton10" CssClass="btn btn-info pull-right" BackColor="#f5f5f5"
                            ToolTip="Save" ImageUrl="~/images/save-29-1.png" OnClick="btnClose_Click" Style="margin-right: 5px;
                            padding: 0px;" runat="server" />
                        <h4 class="modal-title">
                            D2D</h4>
                        
                    </div>
                  <div class="row">

                        <div class="row marg search-bg">
               
                                <div class="col-lg-10 col-md-10 col-sm-10 cpl-xs-12">
                                    <div class="col-lg-6 col-md-6 col-sm-6 cpl-xs-12">
                                    <div class="form-group" style="margin-bottom: 2px;">
                                        <label for="email" class="col-sm-3 padd linhei">
                                            Search:</label>
                                        <div class="col-sm-9 padd">
                                           <asp:DropDownList ID="ddlSearch" runat="server" class="form-control">
                                            <asp:ListItem Selected="True" Value="0">--Select--</asp:ListItem>
                                            <asp:ListItem Value="1">Unique no</asp:ListItem>
                                            <asp:ListItem Value="2">HH Code </asp:ListItem>
                                            <asp:ListItem Value="3">Child Name</asp:ListItem>

                                             <asp:ListItem Value="4">Father Name</asp:ListItem>
                                        </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-lg-4 col-md-4 col-sm-4 cpl-xs-12">
                            <div class="form-group" style="margin-bottom: 7px;">
                             
                                <div class="col-sm-10 padd">
                                    <asp:TextBox runat="server" ID="txtSearch" autocomplete="off" ondrop="return false;"
                                        class="form-control" ></asp:TextBox>
                                    
                                </div>
                            </div>
                        </div>
                                </div>

                                 <div class="col-lg-2 col-md-2  col-sm-2 cpl-xs-12">
                        <div class="col-lg-1 col-md-1 col-sm-2 cpl-xs-12 col-lg-offset-1 col-md-offset-1 col-sm-offset-1 col-xs-offset-0">
                            <asp:ImageButton ID="ImageButton8" OnClick="btnD2dSerach_Click" ToolTip="Serach" runat="server"  class="btn btn-danger btn-paddd pull-left" BackColor="#f1f1f1"
                                ImageUrl="~/images/search-29.png"  Style="margin-left: -49px; padding: 0px;"   />
                                </div>
                
                           
                          
                        </div>
                        </div>
                  </div>
                    <div class="row table-responsive">
                     <div style="overflow: auto; margin-top:35px; height:437px;">
                        <asp:GridView ID="Gv_Display" Width="100%" runat="server" OnRowDataBound="Gv_Display_RowDataBound"
                            CssClass=" table table-striped table-bordered table-hover " AutoGenerateColumns="false">
                            <EmptyDataTemplate>
                                <div style="font-family: Arial; font-size: 12px; font-weight: bold; color: Red;">
                                    Data not found
                                </div>
                            </EmptyDataTemplate>
                            <FooterStyle CssClass="FooterStyle" />
                            <HeaderStyle BackColor="#C1C1C1" Height="44px" />
                            <RowStyle HorizontalAlign="Center" />
                            <SelectedRowStyle BackColor="#897A7A" Font-Bold="True" ForeColor="WhiteSmoke" />
                            <AlternatingRowStyle BackColor="#f1f1f1" />
                            <Columns>
                                <asp:TemplateField HeaderText="Unique Code" HeaderStyle-CssClass="GridHeaderClass">
                                    <ItemTemplate>
                                        <asp:Label ID="lblUniqueCode" runat="server" Text='<%#Eval("UniqueId") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="HH No." HeaderStyle-CssClass="GridHeaderClass">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSno" runat="server" Text='<%#Eval("HHNo") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Child Name" HeaderStyle-CssClass="GridHeaderClass">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lbtn_ProducerD" Text='<%#Eval("ChildName") %>' Style="text-decoration: none;"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:BoundField HeaderText="Father name" DataField="FathersName" HeaderStyle-CssClass="GridHeaderClass">
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="Contact" HeaderStyle-CssClass="GridHeaderClass">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="ddlStatus"  OnSelectedIndexChanged="ddlStatus_SelectedIndexChanged" AutoPostBack="true" runat="server" class="form-control">
                                            <asp:ListItem Selected="True" Value="0">--Select--</asp:ListItem>
                                            <asp:ListItem Value="1">C-Contact </asp:ListItem>
                                              <asp:ListItem Value="2">F-Follow up</asp:ListItem>
                                            <asp:ListItem Value="3">I-Ineligible </asp:ListItem>
                                            <asp:ListItem Value="4">P-Pending Format 6</asp:ListItem>

                                             <asp:ListItem Value="5">E-Enrolled</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:Label runat="server" Visible="false" ID="lbStatus" Text='<%#Eval("Status") %>'
                                            Style="text-decoration: none;"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField Visible="false" HeaderStyle-CssClass="GridHeaderClass">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lbUniqueCode" Text='<%#Eval("UniqueCode") %>' Style="text-decoration: none;"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                        </div>
                    </div>
                    <div class="modal-footer">
                        
                    </div>
                </div>
            </div>
        </asp:Panel>
   </ContentTemplate>
    
    </asp:UpdatePanel> 

    <div class="row">
        <asp:Panel ID="pnlSmc" runat="server">
        <asp:UpdatePanel ID="UpdatePanel3" UpdateMode="Conditional" runat="server">
        <ContentTemplate>
        <div class="col-lg-4 col-md-4 col-sm-6 col-xs-12">
        
            <div class="navbar-header">
                <button type="button" class="navbar-toggle" data-toggle="collapse" data-target="#myNavbar">
                    <span class="">
                        <p class="text-danger" style="margin: 0px; font-weight: bold;">
                            SMC</p>
                    </span>
                </button>
            </div>
            <div class="collapse navbar-collapse" id="Div4">
                <div class="thumbnail" style="height: 344px;overflow:auto">
                
                <asp:ImageButton ID="ImageButton1" runat="server" OnClick="btnSmc_Click" Width="30" Height="25" CssClass="pull-right" ImageUrl="~/images/Reset.png"></asp:ImageButton >
                    <table class="table table-striped table-bordered table-hover">
                        <tbody>
                        <tr>
                            <td >
                                <p class="text-danger" style="margin: 0px; font-weight: bold;">
                                <asp:CheckBox ID="chkSMC" runat="server" />      SMC</p>

                            </td>
                          <td style="padding-left: 33px;">
                            <asp:RadioButton ID="rblSMCTB"  GroupName="smcTB" CssClass="radio" runat="server" />
                                   <%--<input name="" value="" type="radio">--%>
                                TB
                            </td>
                            <td style="padding-left: 33px;">
                                  <asp:RadioButton ID="rblSMCFC" GroupName="smcTB" CssClass="radio" runat="server" />
                                FC
                            </td>
                        </tr>
                        
                     
                     <tr>
                            <td style="width: 60%;">
                              Other SIP prepared
                            </td>
                            <td>
                                 
                            </td>
                            <td>
                                <asp:TextBox ID="txtOtherSIPFC"  onchange="javascript:DiscCode(this.value);"  autocomplete="off" ondrop="return false;" MaxLength="2"
                                                            onkeypress="return isNumberKey(this,event);" runat="server" CssClass="form-control" ></asp:TextBox>
                            </td>
                        </tr>
                   
                        <tr>
                            <td style="width: 60%;">
                               Other SIP completed
                            </td>
                            <td>
                              
                            </td>
                            <td>
                               <asp:TextBox ID="txtsmcmeetinFC"  onchange="javascript:OtherSIp(this.value);"  autocomplete="off" ondrop="return false;" MaxLength="2"
                                                            onkeypress="return isNumberKey(this,event);" runat="server" CssClass="form-control" ></asp:TextBox>
                            </td>
                        </tr>
                                <tr>
                            <td style="width: 60%;">
                              Total Trained Member
                            </td>
                            <td>
                                 
                            </td>
                            <td>
                                <asp:TextBox ID="txtTotalMember"   onchange="javascript:SMCOrientNew1(this.value);" autocomplete="off" ondrop="return false;" MaxLength="2"
                                                            onkeypress="return isNumberKey(this,event);" runat="server" CssClass="form-control" ></asp:TextBox>
                            </td>
                        </tr>
                   
                        <tr>
                            <td style="width: 60%;">
                               Total Trained Female Member
                            </td>
                            <td>
                              
                            </td>
                            <td>
                               <asp:TextBox ID="txtTotalFmember"  onchange="javascript:SMCOrientNew(this.value);"  autocomplete="off" ondrop="return false;" MaxLength="2"
                                                            onkeypress="return isNumberKey(this,event);" runat="server" CssClass="form-control" ></asp:TextBox>
                            </td>
                        </tr>
                      
                      
                    </tbody></table>
                     <table class="table table-striped table-bordered table-hover">
                     <tr>
                     
                     <td>  Other Discussions 
                     </td>
                     <td>  <asp:TextBox ID="txt_pbname" autocomplete="off" ondrop="return false;" runat="server"
                                                        CssClass="form-control" onkeypress="return false;" TabIndex="5"></asp:TextBox>
                                                    
                                                    <cc1:PopupControlExtender ID="PopupControltxt_pbname" runat="server" TargetControlID="txt_pbname"
                                                        PopupControlID="pnt_bookformat" OffsetY="22">
                                                    </cc1:PopupControlExtender>
                                                    <asp:Panel ID="pnt_bookformat" runat="server" Direction="LeftToRight" Style="display: none;
                                                        min-height: 60px; max-height: 300px; overflow: auto; z-index: 999999; background-color: #F1F1F1;
                                                        border: solid 1px #cccccc; width: 40.5%" CssClass="panel">
                                                        <span>
                                                            <asp:CheckBoxList ID="CBL_bookformat" CssClass="_bookformat radio" runat="server"
                                                                onclick="SetMultilanguage('F','_bookformat');">
                                                            </asp:CheckBoxList>
                                                        </span>
                                                        <asp:HiddenField runat="server" ID="hdn_PBName" />
                                                        <asp:HiddenField runat="server" ID="hdn_PBID" />

                                                    </asp:Panel></td>
                     </tr>
                             <tr>
                            <td style="width: 60%;">
                               Other
                            </td>
                          
                            <td>
                               <asp:TextBox ID="TxtSmcOther"  MaxLength="50"
                                                             runat="server" CssClass="form-control" ></asp:TextBox>
                            </td>
                        </tr>
                     </table>

                     
                      <table class="table table-striped table-bordered table-hover" Style="display: none;">
                        <tbody>
                        <tr>
                            <td >
                                <p class="text-danger" style="margin: 0px; font-weight: bold;">
                                <asp:CheckBox ID="chkNewSmc"  runat="server" /> SMC Orientation     </p>

                            </td>
                          <td style="padding-left: 33px;">
                            <asp:RadioButton ID="rblSmcNew"  GroupName="smcTB1" CssClass="radio" runat="server" />
                                   <%--<input name="" value="" type="radio">--%>
                                TB
                            </td>
                            <td style="padding-left: 33px;">
                                  <asp:RadioButton ID="rblSmcNew1" GroupName="smcTB1" CssClass="radio" runat="server" />
                                FC
                            </td>
                        </tr>
                        
                     
           
                      
                    </tbody></table>
                </div>
            </div>
        </div>
         </ContentTemplate>
    
    </asp:UpdatePanel> 
        </asp:Panel>
          <asp:Panel ID="pnlClt" runat="server">
            <asp:UpdatePanel ID="UpdatePanel4" UpdateMode="Conditional" runat="server">
        <ContentTemplate>
        <div class="col-lg-4 col-md-4 col-sm-6 col-xs-12">
            <div class="navbar-header">
                <button type="button" class="navbar-toggle" data-toggle="collapse" data-target="#myNavbar">
                    <span class="">
                        <p class="text-danger" style="margin: 0px; font-weight: bold;">
                           CLT Activity</p>
                    </span>
                </button>
            </div>
            <div class="collapse navbar-collapse" id="Div2">
                <div class="thumbnail" style="overflow: auto; height: 344px">
                   <asp:ImageButton ID="ImageButton2" runat="server" OnClick="btnCLT_Click" Width="30" Height="25" CssClass="pull-right" ImageUrl="~/images/Reset.png"></asp:ImageButton >
                    <table class="table table-striped table-bordered table-hover">
                        <tbody>
                        <tr runat="server" >
                            <td >
                                  GKP
                                    <asp:CheckBox ID="chkClT" Visible="false" runat="server" />
                                     <asp:ImageButton ID="ImageButton11" CssClass="btn btn-info pull-right"
                                        BackColor="#f5f5f5" ToolTip="Add"  OnClick="btnAddGkp_Click" ImageUrl="~/images/add-29-1.png" 
                                        Style="margin-right: 5px; padding: 0px;" runat="server" />
                            </td>
                              <td style="padding-left: 33px;" runat="server" visible="false">
                               <asp:RadioButton ID="rblCLTTB" Visible="false" GroupName="CLTTB" CssClass="radio" runat="server" />
                                 

                            </td>
                            <td style="padding-left: 33px;"  runat="server" visible="false">
                                  <asp:RadioButton ID="rblCLTFC" Visible="false" GroupName="CLTTB" CssClass="radio" runat="server" />
                                 
                            </td>
                        </tr>
                        
                       
                       
                      
                      <tr style="text-align:center">
                      <td>
                        <asp:GridView ID="gvGkp" runat="server"  CssClass="table table-striped table-bordered table-hover" DataKeyNames="GUID_GKP"    AutoGenerateColumns="False"  Font-Names="Arial"
                                                                        Font-Size="12px" Width="100%" >
                                                                          <EmptyDataTemplate>
                                                                            <div style="font-family: Arial; font-size: 12px; font-weight: bold;">
                                                                                Data not found</div>
                                                                        </EmptyDataTemplate>
                                                                        <HeaderStyle BackColor="#C1C1C1" ForeColor="White" Height="40px" HorizontalAlign="Center" />
                                                                        <SelectedRowStyle BackColor="#fad669" Font-Bold="True" ForeColor="WhiteSmoke" />
                                                                        <AlternatingRowStyle BackColor="#f1f1f1" />
                                                                        <Columns>
                                                                          <asp:TemplateField >
                                                                           <ItemTemplate>
                                                                             <asp:LinkButton ID="lbtn"   runat="server" Text="EDIT" OnClick="LnkBtnBlock_OnClick"  CommandArgument='<%# Bind("GUID_GKP") %>'  ></asp:LinkButton>
                                                                                <asp:Label ID="lblCUniqueChildCode" Visible="false" BackColor="Transparent" runat="server" Text='<%# Bind("GUID_GKP") %>' CssClass="form-controlAbhi"></asp:Label>
                                                                             </ItemTemplate>
                                                                              </asp:TemplateField>

                                                                                 <asp:TemplateField HeaderText="Action"    HeaderStyle-Width="15%" HeaderStyle-CssClass="GridHeaderClass">
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="ImgAcc" runat="server"  OnClick="GKPDelete_OnClick" ImageUrl="~/images/delete-29.png"
                                                                Width="15px" Height="15px"></asp:ImageButton>
                                                            
                                                        </ItemTemplate>
                                                       <HeaderStyle Width="5%" />
                                                        <ItemStyle  HorizontalAlign="Center"/>
                                                    </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="SubjectName"  Visible="true">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblSubjectName" ForeColor="Black" runat="server"
                                                                                        Text='<%# Eval("SubjectName") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                    <ItemStyle CssClass="padding-lef" />

                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Level"  Visible="true">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblLevelID" ForeColor="Black" runat="server"
                                                                                        Text='<%# Eval("LevelID") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                  <ItemStyle CssClass="padding-lef" />

                                                                            </asp:TemplateField>

                                                                              <asp:TemplateField HeaderText="Session"  Visible="true">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblSession" ForeColor="Black" runat="server"
                                                                                        Text='<%# Eval("Doc") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                  <ItemStyle CssClass="padding-lef" />

                                                                            </asp:TemplateField>
                                                                             <asp:TemplateField HeaderText="TB/FC"  Visible="true">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblTBFC" ForeColor="Black" runat="server"
                                                                                        Text='<%# Eval("TBFC") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                  <ItemStyle CssClass="padding-lef" />

                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="subjectid" Visible="false">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblsubjectid"  ForeColor="Black" runat="server" Text='<%# Eval("SubjectID") %>'></asp:Label>
                                                                                    <asp:Label ID="lblgkp_fc"  ForeColor="Black" runat="server" Text='<%# Eval("gkp_fc") %>'></asp:Label>
                                                                                          <asp:Label ID="lblgkp_tb"  ForeColor="Black" runat="server" Text='<%# Eval("gkp_tb") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                  <ItemStyle CssClass="padding-lef" />

                                                                            </asp:TemplateField>
                                                                            
                                                                            
                                                                         
                                                                         
                                                                           
                                                                        </Columns>
                                                                        
                                                                    </asp:GridView>
                      </td>

                     <%-- <td>
                      English
                      </td>
                      <td>
                      Maths
                      </td>--%>
                      </tr>
                     <tr style="text-align:center" runat="server" visible="false">
                     <td>
                    <asp:CheckBox ID="chkHindiA" runat="server" /> <label for="ctl00_MainContent_CheckBox11">&nbsp;A</label>
                     </td>
                      <td>
                      <asp:CheckBox ID="chkEnglishA" runat="server" /> <label for="ctl00_MainContent_CheckBox1">&nbsp;A</label>
                     </td>
                      <td>
                     <asp:CheckBox ID="chkMathA" runat="server" /><label for="ctl00_MainContent_CheckBox2">&nbsp;A</label>
                     </td>
                     </tr>

                     <tr style="text-align:center" runat="server" visible="false">
                     <td>
                      <asp:CheckBox ID="chkHindiB" runat="server" /> <label for="ctl00_MainContent_CheckBox3">&nbsp;B</label>
                     </td>
                      <td>
                     <asp:CheckBox ID="chkEnglishB" runat="server" /> <label for="ctl00_MainContent_CheckBox4">&nbsp;B</label>
                     </td>
                      <td>
                     <asp:CheckBox ID="chkMathB" runat="server" /> <label for="ctl00_MainContent_CheckBox5">&nbsp;B</label>
                     </td>
                     </tr>

                     <tr style="text-align:center" runat="server" visible="false">
                     <td>
                    <asp:CheckBox ID="chkHindiC" runat="server" /><label for="ctl00_MainContent_CheckBox6">&nbsp;C</label>
                     </td>
                      <td>
                      <asp:CheckBox ID="chkEnglishC" runat="server" /><label for="ctl00_MainContent_CheckBox7">&nbsp;C</label>
                     </td>
                      <td>
                     <asp:CheckBox ID="chkMathC" runat="server" /><label for="ctl00_MainContent_CheckBox8">&nbsp;C</label>
                     </td>
                     </tr>

                     <tr style="text-align:center" runat="server" visible="false">
                     <td>
                      <asp:CheckBox ID="chkHindiD" runat="server" /><label for="ctl00_MainContent_CheckBox9">&nbsp;D</label>
                     </td>
                      <td>
                     <asp:CheckBox ID="chkEnglishD" runat="server" /><label for="ctl00_MainContent_CheckBox10">&nbsp;D</label>
                     </td>
                      <td>
                      <asp:CheckBox ID="chkMathD" runat="server" /><label for="ctl00_MainContent_CheckBox12">&nbsp;D</label>
                     </td>
                     </tr>

                     <tr style="text-align:center" runat="server" visible="false">
                     <td>
                     <asp:CheckBox ID="chkHindiE" runat="server" /><label for="ctl00_MainContent_CheckBox13">&nbsp;E</label>
                     </td>
                      <td>
                      <asp:CheckBox ID="chkEnglishE" runat="server" /><label for="ctl00_MainContent_CheckBox14">&nbsp;E</label>
                     </td>
                      <td>
                     <asp:CheckBox ID="chkMathE" runat="server" /><label for="ctl00_MainContent_CheckBox15">&nbsp;E</label>
                     </td>
                     </tr>
            </tbody></table>
             <table class="table table-striped table-bordered table-hover">
                        <tbody>
                 <tr>
                      <td colspan="3">
                      &nbsp;
                      </td>
                      </tr>
                   <tr style="text-align:center;background-color:#f7f7f7 ">
                      <td>
                      Baseline-Test
                      </td>

                      <td>
                      Midline-Test
                      </td>
                      <td>
                      Endline-Test
                      </td>
                      </tr>
                      
                      <tr style="text-align:center;background-color:transparent !important">
                     <td>
                      <asp:RadioButton ID="rblTestTBPre" GroupName="Test1" CssClass="radio" runat="server" /><label for="ctl00_MainContent_CheckBox16">&nbsp;T.B.</label>
                     </td>
                      <td>
                     <asp:RadioButton ID="rblTestTBMid" GroupName="Test2" CssClass="radio" Enabled="false" runat="server" /><label for="ctl00_MainContent_CheckBox17">&nbsp;T.B.</label>
                     </td>
                      <td>
                    <asp:RadioButton ID="rblTestTBPost" GroupName="Test3" CssClass="radio" Enabled="false" runat="server" /><label for="ctl00_MainContent_CheckBox18">&nbsp;T.B.</label>
                     </td>
                     </tr>

                     <tr style="text-align:center;background-color:#f7f7f7  !important">
                     <td>
                     <asp:RadioButton ID="rblTestpreFC" GroupName="Test1" CssClass="radio" runat="server" /><label for="ctl00_MainContent_RadioButton1">&nbsp;F.C.</label>
                     </td>
                      <td>
                      <asp:RadioButton ID="rblTestMidFC" GroupName="Test2" CssClass="radio" Enabled="false" runat="server" /><label for="ctl00_MainContent_RadioButton2">&nbsp;F.C.</label>
                     </td>
                      <td>
                     <asp:RadioButton ID="rblTestPostFC" GroupName="Test3" CssClass="radio" Enabled="false" runat="server" /> <label for="ctl00_MainContent_RadioButton3">&nbsp;F.C.</label>
                     </td>
                     </tr>

                     <tr style="text-align:center;background-color:#Transparent !important">
                     <td>
                     <asp:RadioButton ID="rblPartialPre" GroupName="Test6" CssClass="radio" runat="server" /> <label for="ctl00_MainContent_RadioButton4">&nbsp;Partial</label>
                     </td>
                      <td>
                          <asp:RadioButton ID="rblPartialMid"  CssClass="radio" Enabled="false" GroupName="Test7" runat="server" /><label for="ctl00_MainContent_RadioButton5">&nbsp;Partial</label>
                     </td>
                      <td>
                     <asp:RadioButton ID="rblPartialPost"  CssClass="radio" Enabled="false" GroupName="Test8" runat="server" /><label for="ctl00_MainContent_RadioButton6">&nbsp;Partial</label>
                     </td>
                     </tr>
                     <tr style="text-align:center;background-color:#f7f7f7  !important">
                     <td>
                     <asp:RadioButton ID="rblCompletePre"  CssClass="radio" GroupName="Test6" runat="server" /><label for="ctl00_MainContent_RadioButton7">&nbsp;Complete</label>
                     </td>
                      <td>
                      <asp:RadioButton ID="rblCompleteMid"  CssClass="radio" Enabled="false" GroupName="Test7" runat="server" /><label for="ctl00_MainContent_RadioButton8">&nbsp;Complete</label>
                     </td>
                      <td>
                     <asp:RadioButton ID="rblCompletePost"  CssClass="radio" Enabled="false" GroupName="Test8" runat="server" /><label for="ctl00_MainContent_RadioButton9">&nbsp;Complete</label>
                     </td>
                     </tr>

                    </tbody></table>
                </div>
            </div>
        </div>

             <asp:Label ID="lblGuId"  Visible="false" ForeColor="Black" runat="server" ></asp:Label>
        
            <cc1:ModalPopupExtender ID="MpexdrDistrict8" runat="server" BackgroundCssClass="modalBg "
                    CancelControlID="CancelButton1" PopupControlID="PnlDistrict8" TargetControlID="HdnFild8">
                </cc1:ModalPopupExtender>
                <asp:HiddenField ID="HdnFild8" runat="server"></asp:HiddenField>

                <asp:Panel cssclass="model-wid mod-posi"  Style="display: none;height:auto;width: 45% !important; margin-top: 220px !important;" ID="PnlDistrict8" runat="server">
                   
                    <div style="width:100%;height:auto;background-color:#f1f1f1">
                    <div class="modal-header"  style="background-color:#ddd;color:White;">
                    <h4 class="modal-title" style="ForeColor:White">GKP</h4>
                    </div>
                   <div class="modal-body">
                   <asp:Label ID="Label1" runat="server" ForeColor="Red" Font-Names="Verdana" Font-Size="11px"></asp:Label>
                   <div class="form-horizontal" role="form">

                    <div class="form-group">
 
                      <asp:Label ID="Label12" class="control-label col-sm-4 lab-text-left" runat="server" Text="TBorFC"></asp:Label>
                                 <div class="col-sm-6 ">
                               <asp:RadioButtonList RepeatDirection="Horizontal"    ForeColor="Black" ID="rblApprove" runat="server">
                                                      
                                                         <asp:ListItem  Selected="True" Value="1">FC   </asp:ListItem>
                                                            <asp:ListItem  style="padding-right: -55px;margin-left: 9px;" Value="2">TB</asp:ListItem>
                                                         
                                                        </asp:RadioButtonList>
                                        
                                   
                           </div>
                      </div>


                  <div class="form-group" id="statediv" runat="server">
 
                     <asp:Label ID="Label10" class="control-label col-sm-4 lab-text-left" runat="server" Text="Subject"></asp:Label>
                    <div class="col-sm-6">
                                                            <asp:DropDownList ID="ddlSubject" runat="server"  AutoPostBack="true" OnSelectedIndexChanged="ddlSubject_SelectedIndexChanged" CssClass="form-control"
                                                            Font-Names="Verdana" Font-Size="11px" 
                                                            >
                                                        </asp:DropDownList>
                                        
                                     
                    </div>
                  </div>
 
 

                      <div class="form-group" id="blockdiv" runat="server">
 
                         <asp:Label ID="lblBlock" class="control-label col-sm-4 lab-text-left" runat="server" Text="Level"></asp:Label>
                        <div class="col-sm-6">
                                                                   <asp:DropDownList ID="ddlLevel"  AutoPostBack="true" OnSelectedIndexChanged="ddlLevel_SelectedIndexChanged" runat="server"  class="form-control">
      
                                                            </asp:DropDownList>
                                        
                        </div>
                      </div>



                              <div class="form-group">
 
                                 <asp:Label ID="Label11" class="control-label col-sm-4 lab-text-left" runat="server" Text="Session"></asp:Label>
                                <div class="col-sm-6">
                                                                      <asp:DropDownList ID="ddlSSession" runat="server" CssClass="form-control" Font-Names="Verdana"
                                                                        Font-Size="11px"  >
                                                                    </asp:DropDownList>
                                       
                                </div>
                              </div>

 
  
</div>

                  
                   </div>
                    <div class="modal-footer">
                     <asp:ImageButton ID="btnNewUserSave" OnClick="btnSaveGkp_Click"  ImageUrl="~/images/save-29-1.png"  runat="server"
                           ToolTip="Save"  Style="float: none;" ValidationGroup="validatemanageuser">
                            </asp:ImageButton>&nbsp;
                            <asp:ImageButton ID="CancelButton1" ImageUrl="~/images/close-29.png" runat="server"  Text="Close"
                              ToolTip="Close" Style="float: none;"></asp:ImageButton></div>
                    </div>
                       
                       
                </asp:Panel>
         </ContentTemplate>
    
    </asp:UpdatePanel> 
         </asp:Panel>
       <asp:Panel ID="pnlBalshaba" runat="server">
              
        
            <div class="col-lg-4 col-md-4 col-sm-6 col-xs-12">
            <asp:UpdatePanel ID="UpdatePanel5" UpdateMode="Conditional" runat="server">
               <ContentTemplate>
            <div class="navbar-header">
                <button type="button" class="navbar-toggle" data-toggle="collapse" data-target="#myNavbar">
                    <span class="">
                        <p class="text-danger" style="margin: 0px; font-weight: bold;">
                           Balsabha </p>
                    </span>
                </button>
            </div>
            <div class="collapse navbar-collapse" id="Div5">
                <div class="thumbnail" style="overflow: auto; height: 344px">
                  <asp:ImageButton ID="ImageButton3" runat="server" OnClick="btnBalSab_Click" Width="30" Height="25" CssClass="pull-right" ImageUrl="~/images/Reset.png"></asp:ImageButton >
           
                    <table class="table table-striped table-bordered table-hover">
                        <tbody><tr>
                            <td >
                                <p class="text-danger" style="margin: 0px; font-weight: bold;">
                                   <asp:CheckBox ID="chkBalsabha" runat="server" />  Balsabha</p>
                            </td>
                            <td>
                                <asp:RadioButton ID="rblBalsabaTB"  style="margin-left: 19px;" GroupName="TestBal" CssClass="radio" runat="server" />
                                T.B.
                                  <asp:RadioButton ID="rblBalsabaFC"  style="margin-left: 19px;" GroupName="TestBal" CssClass="radio" runat="server" />
                               F.C.

                            </td>
                            
                               
                            
                        </tr>
                       
                        
<%--
                        <tr style="text-align:left">
                           
                            <td>
                                <asp:RadioButton ID="rblBalsabaTB" Checked="true" style="margin-left: 19px;" GroupName="TestBal" CssClass="radio" runat="server" />
                                T.B.
                            </td>
                            <td>
                                 <asp:RadioButton ID="rblBalsabaFC"  style="margin-left: 19px;" GroupName="TestBal" CssClass="radio" runat="server" />
                               F.C.
                            </td>
                        </tr>--%>
                    <tr style="text-align:left">
                    
                    <td>
                     <asp:CheckBox ID="chkBalSabhaFor" runat="server" /> <label for="ctl00_MainContent_chkbalsabha">&nbsp;Balsabha Formation</label> 
                    </td>

                     <td>
                    <asp:CheckBox ID="chkOrientation" runat="server" /> <label for="ctl00_MainContent_CheckBox28">&nbsp;Orientation</label> 
                    </td>
                    
                    </tr>

                    <tr style="text-align:left">
                    
                    <td>
                      <asp:CheckBox ID="chkChat" runat="server" /> <label for="ctl00_MainContent_CheckBox30">&nbsp;Chart</label> 
                    </td>

                     <td>
                    <asp:CheckBox ID="chkKit" runat="server" /> <label for="ctl00_MainContent_CheckBox32">&nbsp;Kit</label> 
                    </td>
                    
                    </tr>

                   
                      <tr> <td><td>      <asp:ImageButton ID="ImageButton4" OnClick="btnLife" runat="server" Width="30" Height="25" CssClass="pull-right" ImageUrl="~/images/Reset.png"></asp:ImageButton >
           </td></td></tr>
                        
                             <tr>
                            <td >
                                <p class="text-danger" style="margin: 0px; font-weight: bold;">
                                   <asp:CheckBox ID="chklife" runat="server" />   Life skill Games</p>
                            </td>
                            <td>
                               <asp:RadioButton ID="rblLifeTB" style="margin-left: 19px;" GroupName="TestLife" CssClass="radio" runat="server" />
                                T.B.
                                 <asp:RadioButton ID="rblLifeFC"  style="margin-left: 19px;" GroupName="TestLife" CssClass="radio" runat="server" />
                               F.C.

                            </td>
                        </tr>
                   

                      <%--  <tr style="text-align:left">
                           
                            <td>
                               <asp:RadioButton ID="rblLifeTB" Checked="true" style="margin-left: 19px;" GroupName="TestLife" CssClass="radio" runat="server" />
                                T.B.
                            </td>
                            <td>
                               <asp:RadioButton ID="rblLifeFC"  style="margin-left: 19px;" GroupName="TestLife" CssClass="radio" runat="server" />
                               F.C.
                            </td>
                        </tr>--%>
                    <tr style="text-align:left">
                    
                    <td>
                    <asp:CheckBox ID="chkGame1" runat="server" /><label for="ctl00_MainContent_CheckBox27">&nbsp;Game1</label> 
                    </td>

                     <td>
                    <asp:CheckBox ID="chkGame2" runat="server" /><label for="ctl00_MainContent_CheckBox29">&nbsp;Game2</label> 
                    </td>
                    
                    </tr>

                    <tr style="text-align:left">
                    
                    <td>
                     <asp:CheckBox ID="chkGame3" runat="server" /><label for="ctl00_MainContent_CheckBox31">&nbsp;Game3</label> 
                    </td>

                     <td>
                   <asp:CheckBox ID="chkGame4" runat="server" /><label for="ctl00_MainContent_CheckBox33">&nbsp;Game4</label> 
                    </td>
                    
                    </tr>
                <tr style="text-align:left">
                 <td>
                     <asp:CheckBox ID="chkGame5" runat="server" /><label for="ctl00_MainContent_CheckBox33"><label for="ctl00_MainContent_CheckBox34">&nbsp;Game5</label> 
                    </td>
                </tr>
                    </tbody></table>
                </div>
            </div>
              </ContentTemplate>
    
             </asp:UpdatePanel> 
        </div>
        
        </asp:Panel>
    </div>
    <div class="row" id="dvidnew" runat="server">
     <asp:Panel ID="pnlSACUpdate" runat="server">
       
        <div class="col-lg-4 col-md-4 col-sm-6 col-xs-12"  id="dvidnew45"  runat="server"  >
        <asp:UpdatePanel ID="UpdatePanel6" UpdateMode="Conditional" runat="server">
           <ContentTemplate>
            <div class="navbar-header">
                <button type="button" class="navbar-toggle" data-toggle="collapse" data-target="#myNavbar">
                    <span class="">
                        <p class="text-danger" style="margin: 0px; font-weight: bold;">
                            <input name="" value="" type="checkbox">SAC Update</p>
                    </span>
                </button>
            </div>
            <div class="collapse navbar-collapse" id="Div1">
                <div class="thumbnail" style="height: 535px">
                <asp:ImageButton ID="ImageButton5" OnClick="btnSacUpdate_Click" runat="server" Width="30" Height="25" CssClass="pull-right" ImageUrl="~/images/Reset.png"></asp:ImageButton >
                    <table class="table table-striped table-bordered table-hover">
                        <tbody><tr>
                            <td >
                                <p class="text-danger" style="margin: 0px; font-weight: bold;">
                                   <asp:CheckBox ID="chkSACUpdate"  runat="server" />    SAC Update</p>
                            </td>
                             <td>
                                <asp:RadioButton ID="rblSacTB" style="margin-left: 19px;"    GroupName="sacTB" CssClass="radio" runat="server" />
                                TB
                                <asp:RadioButton ID="rblSacFB" style="margin-left: 19px;"    GroupName="sacTB" CssClass="radio" runat="server" />
                                FC
                            </td>
                        </tr>
                        
                     
                        <tr>
                            <td style="width: 60%;">
                                # S.M.C. Meetings
                            </td>
                           
                            <td>
                               <asp:TextBox ID="txtSMCMeeting"   onchange="javascript:SMC(this.value);" autocomplete="off" ondrop="return false;" MaxLength="2"
                                                            onkeypress="return isNumberKey(this,event);" runat="server" CssClass="form-control" ></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 60%;">
                                Regular Health Checkup
                            </td>
                          
                            <td>
                                  <asp:TextBox ID="txtHealth"   autocomplete="off" ondrop="return false;" MaxLength="1"
                                                            onkeypress="return isNumberKey(this,event);" runat="server" CssClass="form-control" ></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 60%;">
                                Admission of girls
                            </td>
                           
                            <td>
                                <asp:TextBox ID="txtAdgirls"   autocomplete="off" ondrop="return false;" MaxLength="3"
                                                            onkeypress="return isNumberKey(this,event);" runat="server" CssClass="form-control" ></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 60%;">
                                Admission of boys
                            </td>
                            
                            <td>
                                 <asp:TextBox ID="txtAdBoy"   autocomplete="off" ondrop="return false;" MaxLength="3"
                                                            onkeypress="return isNumberKey(this,event);" runat="server" CssClass="form-control" ></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 60%;">
                                # girls left school
                            </td>
                           
                            <td>
                                  <asp:TextBox ID="txtleftGirl"   autocomplete="off" ondrop="return false;" MaxLength="3"
                                                            onkeypress="return isNumberKey(this,event);" runat="server" CssClass="form-control" ></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 60%;">
                                # boys left school
                            </td>
                          
                            <td>
                                 <asp:TextBox ID="txtleftBoy"   autocomplete="off" ondrop="return false;" MaxLength="3"
                                                            onkeypress="return isNumberKey(this,event);" runat="server" CssClass="form-control" ></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 60%;">
                                # Girls not entred in School
                            </td>
                            
                            <td>
                                <asp:TextBox ID="txtGirlNot"   autocomplete="off" ondrop="return false;" MaxLength="3"
                                                            onkeypress="return isNumberKey(this,event);" runat="server" CssClass="form-control" ></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 60%;">
                                # Boys not entred in School
                            </td>
                            
                            <td>
                                 <asp:TextBox ID="txtBoyNot"  autocomplete="off" ondrop="return false;" MaxLength="3"
                                                            onkeypress="return isNumberKey(this,event);" runat="server" CssClass="form-control" ></asp:TextBox>
                            </td>
                        </tr>
                    </tbody></table>
                </div>
            </div>
                  </ContentTemplate>
    
    </asp:UpdatePanel>
        </div>
 
     </asp:Panel>

   <asp:Panel ID="pnlinfrastructure" runat="server">
    <div class="col-lg-4 col-md-4 col-sm-6 col-xs-12">
          <asp:UpdatePanel ID="UpdatePaneel1" UpdateMode="Conditional" runat="server">
        <ContentTemplate>
            <div class="navbar-header">
                <button type="button" class="navbar-toggle" data-toggle="collapse" data-target="#myNavbar">
                    <span class="">
                        <p class="text-danger" style="margin: 0px; font-weight: bold;">
                            School infrastructure facility</p>
                    </span>
                </button>
            </div>
            <div class="collapse navbar-collapse" id="myNavbar">
                <div class="thumbnail" style="overflow: auto; height: 535px">
                   <asp:ImageButton ID="ImageButton6" OnClick="btninfrastructure_Click" runat="server" Width="30" Height="25" CssClass="pull-right" ImageUrl="~/images/Reset.png"></asp:ImageButton >
  
                    <table class="table table-striped table-bordered table-hover">
                        <tbody><tr>
                            <td >
                                <p class="text-danger" style="margin: 0px; font-weight: bold;">
                                  <asp:CheckBox ID="chkPhysical" runat="server" />       School infrastructure facility</p>
                            </td>
                            <td>
                              
                            </td>
                            <td></td>
                        </tr>
                       
                        <tr>
                            <td >
                           <asp:RadioButton ID="rblPhysicalTB" style="margin-left: 19px;"   GroupName="sac2TB" CssClass="radio" runat="server" />
                                TB
                                <asp:RadioButton ID="rblPhysicalFC" style="margin-left: 19px;"   GroupName="sac2TB" CssClass="radio" runat="server" />
                                FC
                            </td>
                            <td>
                              <p class="text-danger" style="margin: 0px; font-weight: bold;">Previous </p>
                            </td>
                            <td>                              <p class="text-danger" style="margin: 0px; font-weight: bold;">Current </p></td>
                        </tr>
                       
                        <tr>
                            <td style="width: 60%;">
                                Class Room
                            </td>
                            <td></td>
                            <td>
                               <asp:TextBox ID="txtClassRoom"    onkeypress="return isNumberKey(this,event);" autocomplete="off" ondrop="return false;" MaxLength="2"   runat="server" class="form-control" ></asp:TextBox>
                            </td>
                        </tr>
                         <%--    #334cff  gr
 #ff3355 red
  #ffc733 bb
   #4c33ff blu--%>
                        <tr>
                            <td style="width: 60%;">
                                Safe drinking water
                            </td>
                          
                            <td align="center">
                                <div  runat="server" >
                                  
                                <asp:TextBox ID="txtdrinking1"  autocomplete="off" ondrop="return false;" Enabled="false"  onClick="checkFilledNew(this.id,'addcss','dr1','v1');"  runat="server" style="width: 30px;border: none; height: 30px; border-radius: 4px;"></asp:TextBox>
                              
                                </div>
                            </td>
                             <td align="center">
                                <div id="Div8"  runat="server" >
                               
                               <asp:TextBox ID="txtdrinking"  autocomplete="off" ondrop="return false;"  onClick="checkFilledNew(this.id,'addcss','dr1','v1');"  runat="server" style="width: 30px;border: none; height: 30px; border-radius: 4px;"></asp:TextBox>
                            
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 60%;">
                                Seprate Toilet for girls
                            </td>
                            <td align="center">
                                <div >
                                  <asp:TextBox ID="txtToilet1"  runat="server" Enabled="false" autocomplete="off" ondrop="return false;"   onClick="checkFilledNew(this.id,'addcss1','t1','v2');"  style="width: 30px;border: none;   height: 30px;   border-radius: 4px;"></asp:TextBox>
                                 
                                </div>
                            </td>
                            <td align="center">
                                <div >
                                  <asp:TextBox ID="txtToilet"  runat="server" autocomplete="off" ondrop="return false;"   onClick="checkFilledNew(this.id,'addcss1','t1','v2');"  style="width: 30px;border: none;   height: 30px;   border-radius: 4px;"></asp:TextBox>
                                         
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 60%;">
                                Electricity
                            </td>
                             <td align="center">
                                <div >
                                  <asp:TextBox ID="txtElectricity1"  runat="server" Enabled="false" autocomplete="off" ondrop="return false;"   onClick="checkFilledNew(this.id,'addcss1','t1','v2');"  style="width: 30px;border: none;   height: 30px;   border-radius: 4px;"></asp:TextBox>
                                 
                                </div>
                            </td>
                            <td align="center">
                                <div >
                                       <asp:TextBox ID="txtElectricity" autocomplete="off" ondrop="return false;"   runat="server" onClick="checkFilledNew(this.id,'addcss2','t2','v3');" style="width: 30px;border: none;   height: 30px;   border-radius: 4px;"></asp:TextBox>
                                  
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 60%;">
                                Play Ground
                            </td>
                            <td align="center">
                                <div >
                                  <asp:TextBox ID="txtPlay1"  runat="server" Enabled="false" autocomplete="off" ondrop="return false;"   onClick="checkFilledNew(this.id,'addcss1','t1','v2');"  style="width: 30px;border: none;   height: 30px;   border-radius: 4px;"></asp:TextBox>
                                 
                                </div>
                            </td>
                            <td align="center">
                                <div>
                                     <asp:TextBox ID="txtPlay" autocomplete="off" ondrop="return false;"  runat="server"   onClick="checkFilledNew(this.id,'addcss3','t3','v4');" style="width: 30px;border: none;  height: 30px;   border-radius: 4px;"></asp:TextBox>
                                  
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 60%;">
                                Swings &amp; Slides
                            </td>
                            <td align="center">
                                <div >
                                  <asp:TextBox ID="txtSlides1"  runat="server" Enabled="false" autocomplete="off" ondrop="return false;"   onClick="checkFilledNew(this.id,'addcss1','t1','v2');"  style="width: 30px;border: none;   height: 30px;   border-radius: 4px;"></asp:TextBox>
                                 
                                </div>
                            </td>
                            <td align="center">
                                <div >
                                  <asp:TextBox ID="txtSlides" autocomplete="off" ondrop="return false;"  onClick="checkFilledNew(this.id,'addcss4','t4','v5');" runat="server" style="width: 30px;border: none;  height: 30px;  border-radius: 4px;"></asp:TextBox>
                                        
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 60%;">
                                Boundarywall
                            </td>
                        <td align="center">
                                <div >
                                  <asp:TextBox ID="txtBoundaryWall1"  runat="server" Enabled="false" autocomplete="off" ondrop="return false;"   onClick="checkFilledNew(this.id,'addcss1','t1','v2');"  style="width: 30px;border: none;   height: 30px;   border-radius: 4px;"></asp:TextBox>
                                 
                                </div>
                            </td>
                            <td align="center">
                                <div >
                                 <asp:TextBox ID="txtBoundaryWall" autocomplete="off" ondrop="return false;"   onClick="checkFilledNew(this.id,'addcss5','t5','v6');"   runat="server" style="width: 30px;border: none;height: 30px;   border-radius: 4px;"></asp:TextBox>
                          
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 60%;">
                                Kitchen
                            </td>
                            <td align="center">
                                <div >
                                  <asp:TextBox ID="txtKitchen1"  runat="server" Enabled="false" autocomplete="off" ondrop="return false;"   onClick="checkFilledNew(this.id,'addcss1','t1','v2');"  style="width: 30px;border: none;   height: 30px;   border-radius: 4px;"></asp:TextBox>
                                 
                                </div>
                            </td>
                            <td align="center">
                                <div >
                                   <asp:TextBox  ID="txtKitchen" autocomplete="off" ondrop="return false;"  onClick="checkFilledNew(this.id,'addcss6','t6','v7');"  runat="server" style="width: 30px;border: none; height: 30px;   border-radius: 4px;"></asp:TextBox>
                            
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 60%;">
                                Male Teacher
                            </td>
                             <td></td>
                            <td>
                                <asp:TextBox  ID="txtMaleTeacher" autocomplete="off" ondrop="return false;" MaxLength="2"
                                                            onkeypress="return isNumberKey(this,event);" runat="server"  class="form-control" ></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 60%;">
                                Female Teacher
                            </td>
                            <td></td>
                            <td>
                          <asp:TextBox  ID="txtFemaleTeacher" autocomplete="off" ondrop="return false;" MaxLength="2"
                                                            onkeypress="return isNumberKey(this,event);" runat="server"  class="form-control" ></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 60%;">
                                availablity of books
                            </td>
                             <td align="center">
                                <div >
                                  <asp:TextBox ID="txtbook1"  runat="server" Enabled="false" autocomplete="off" ondrop="return false;"   onClick="checkFilledNew(this.id,'addcss1','t1','v2');"  style="width: 30px;border: none;   height: 30px;   border-radius: 4px;"></asp:TextBox>
                                 
                                </div>
                            </td>
                            <td align="center">
                                <div >
                                  <asp:TextBox  ID="txtbook" autocomplete="off" ondrop="return false;"   onClick="checkFilledNew(this.id,'addcss7','t7','v9');" style="width: 30px;border: none; height: 30px;     border-radius: 4px;" runat="server" ></asp:TextBox>
                               
                                </div> 
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 60%;">
                                Use of GKP Kit
                            </td>
                             <td align="center">
                                <div >
                                  <asp:TextBox ID="txtCltKit1"  runat="server" Enabled="false" autocomplete="off" ondrop="return false;"   onClick="checkFilledNew(this.id,'addcss1','t1','v2');"  style="width: 30px;border: none;   height: 30px;   border-radius: 4px;"></asp:TextBox>
                                 
                                </div>
                            </td>
                            <td align="center">
                                <div >
                                  <asp:TextBox  ID="txtCltKit"  autocomplete="off" ondrop="return false;"  onClick="checkFilledNew(this.id,'addcss8','t8','v8');" style="width: 30px;border: none; height: 30px;    border-radius: 4px;" runat="server"  ></asp:TextBox>
                                  
                                </div>
                            </td>
                        </tr>
                    </tbody></table>
                </div>
            </div>
            </ContentTemplate>
    
    </asp:UpdatePanel>        
        </div>
        </asp:Panel>
          
   <asp:Panel ID="pnlAnnual" runat="server">
      <asp:UpdatePanel ID="UpdatePanel7" UpdateMode="Conditional" runat="server">
        <ContentTemplate>
           <div class="col-lg-4 col-md-4 col-sm-6 col-xs-12">
            <div class="navbar-header">
                <button type="button" class="navbar-toggle" data-toggle="collapse" data-target="#myNavbar">
                    <span class="">
                        <p class="text-danger" style="margin: 0px; font-weight: bold;">
                          Annual Data
                           
                           </p>
                    </span>
                </button>
            </div>
            <div class="collapse navbar-collapse" id="Div3">
                <div class="thumbnail" style="overflow: auto; height: 277px">
                  <asp:ImageButton ID="ImageButton7" OnClick="btnAnnual_Click" runat="server" Width="30" Height="25" CssClass="pull-right" ImageUrl="~/images/Reset.png"></asp:ImageButton >
  
                    <table class="table table-striped table-bordered table-hover">
                        <tbody><tr>
                            <td colspan="3">
                                <p class="text-danger" style="margin: 0px; font-weight: bold;">
                                    <asp:CheckBox ID="chkAnnual" runat="server" />   Annual Data</p>
                            </td>
                        </tr>
                       
                        

                        <tr align="center">
                           
                            <td>
                                <asp:CheckBox ID="chkSIPAnnaul" runat="server" /> 
                                SIP Annual Data
                            </td>
                            <td>
                                <asp:CheckBox ID="chkRetention" runat="server" /> 
                                Retention Annual Data
                            </td>
                        </tr>
                   <tr style="text-align:center;background-color:#f7f7f7  !important">
                  <td>
                     <asp:CheckBox ID="chkSIPTB"  Enabled="false" CssClass="radio" runat="server" /><label for="ctl00_MainContent_CheckBox19">&nbsp;T.B.</label>
                     </td>
                      <td>
                     <asp:CheckBox ID="chkRenTB" Enabled="false" CssClass="radio" runat="server" /><label for="ctl00_MainContent_CheckBox20">&nbsp;T.B.</label>
                     </td>
                      </tr> 

                        <tr style="text-align:center;background-color:#f7f7f7  !important">
                  <td>
                     <asp:CheckBox ID="chkSIPFC"  CssClass="radio" runat="server" /><label for="ctl00_MainContent_CheckBox23">&nbsp;F.C.</label>
                     </td>
                      <td>
                     <asp:CheckBox ID="chkRenFC"  CssClass="radio" runat="server" /><label for="ctl00_MainContent_CheckBox24">&nbsp;F.C.</label>
                     </td>
                      </tr> 
                      <tr style="text-align:center;background-color:#f7f7f7  !important">
                  <td>
                     <asp:RadioButton ID="chkSipPartial" GroupName="SIP" CssClass="radio" runat="server" /><label for="ctl00_MainContent_CheckBox21">&nbsp;Partial</label>
                     </td>
                      <td>
             <asp:RadioButton ID="chkRenPartial" GroupName="REN" CssClass="radio" runat="server" /><label for="ctl00_MainContent_CheckBox22">&nbsp;Partial</label>
                     </td>
                      </tr> 

                        <tr style="text-align:center;background-color:#f7f7f7  !important">
                  <td>
                <asp:RadioButton ID="chkSipComplete" GroupName="SIP" CssClass="radio" runat="server" /><label for="ctl00_MainContent_CheckBox25">&nbsp;Complete</label>
                     </td>
                      <td>
                    <asp:RadioButton ID="chkComplete" GroupName="REN" CssClass="radio" runat="server" /><label for="ctl00_MainContent_CheckBox26">&nbsp;Complete</label>
                     </td>
                      </tr> 
                    </tbody></table>
                </div>
            </div>
        </div>


       
        </ContentTemplate>
    
    </asp:UpdatePanel> 
    </asp:Panel>
     
    </div>
    </asp:Panel>
      <asp:TextBox ID="lbldriking" Width="1"  BorderStyle="None" runat="server" class="addcss" ></asp:TextBox>
                                  <asp:TextBox ID="lblToilet" BorderStyle="None" Width="1"  runat="server" class="addcss1" ></asp:TextBox>
                                     <asp:TextBox ID="lblElectricity" BorderStyle="None"  Width="1" runat="server" class="addcss2" ></asp:TextBox>

                                       <asp:TextBox ID="lblCltKit" BorderStyle="None"  Width="1" runat="server" class="addcss8" ></asp:TextBox>
                             <asp:TextBox ID="lblbook" BorderStyle="None"  Width="1" runat="server" class="addcss7" ></asp:TextBox>
                                  <asp:TextBox ID="lblKitchen"  BorderStyle="None" Width="1"  runat="server" class="addcss6" ></asp:TextBox>
                                  <asp:TextBox ID="lblBoundaryWall"  BorderStyle="None" Width="1"  runat="server" class="addcss5" ></asp:TextBox>
                               <asp:TextBox ID="lblSlides"  BorderStyle="None" Width="1"  runat="server" class="addcss4" ></asp:TextBox>
                                           <asp:TextBox ID="lblPlay"  BorderStyle="None" Width="1"  runat="server" class="addcss3" ></asp:TextBox>

                     <asp:TextBox ID="txtCountDriking" Width="1"  BorderStyle="None" runat="server" class="dr1" ></asp:TextBox>



                                     <asp:TextBox ID="TextBox1" BorderStyle="None" Width="1"  runat="server" class="t1" ></asp:TextBox>
                                     <asp:TextBox ID="TextBox2" BorderStyle="None"  Width="1" runat="server" class="t2" ></asp:TextBox>

                                       <asp:TextBox ID="TextBox3" BorderStyle="None"  Width="1" runat="server" class="t3" ></asp:TextBox>
                             <asp:TextBox ID="TextBox4" BorderStyle="None"  Width="1" runat="server" class="t4" ></asp:TextBox>
                                  <asp:TextBox ID="TextBox5"  BorderStyle="None" Width="1"  runat="server" class="t5" ></asp:TextBox>
                                  <asp:TextBox ID="TextBox6"  BorderStyle="None" Width="1"  runat="server" class="t6" ></asp:TextBox>
                               <asp:TextBox ID="TextBox7"  BorderStyle="None" Width="1"  runat="server" class="t7" ></asp:TextBox>
                                           <asp:TextBox ID="TextBox8"  BorderStyle="None" Width="1"  runat="server" class="t8" ></asp:TextBox>
     <asp:TextBox ID="TextBox9"  BorderStyle="None" Width="1"  runat="server" class="t9" ></asp:TextBox>



                                     <asp:TextBox ID="txt1" BorderStyle="None" Width="1"  runat="server" class="v1" ></asp:TextBox>
                                     <asp:TextBox ID="txt2" BorderStyle="None"  Width="1" runat="server" class="v2" ></asp:TextBox>

                                       <asp:TextBox ID="txt3" BorderStyle="None"  Width="1" runat="server" class="v3" ></asp:TextBox>
                             <asp:TextBox ID="txt4" BorderStyle="None"  Width="1" runat="server" class="v4" ></asp:TextBox>
                                  <asp:TextBox ID="txt5"  BorderStyle="None" Width="1"  runat="server" class="v5" ></asp:TextBox>
                                  <asp:TextBox ID="txt6"  BorderStyle="None" Width="1"  runat="server" class="v6" ></asp:TextBox>
                               <asp:TextBox ID="txt7"  BorderStyle="None" Width="1"  runat="server" class="v7" ></asp:TextBox>
                                           <asp:TextBox ID="txt8"  BorderStyle="None" Width="1"  runat="server" class="v8" ></asp:TextBox>
                                             <asp:TextBox ID="txt9"  BorderStyle="None" Width="1"  runat="server" class="v9" ></asp:TextBox>

       

          <cc1:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="Hdn_model4"
            PopupControlID="pnlpopup4" BackgroundCssClass="modalBackground">
        </cc1:ModalPopupExtender>
        <asp:HiddenField ID="Hdn_model4" runat="server" />
           <asp:Panel ID="pnlpopup4" runat="server" Style="display: none;">
           <div class="modal-dialog modal-md">
                <div class="modal-content">
                    <div class="modal-header" style="height: 0px;">
                    <asp:ImageButton ID="ImageButton9" CssClass="btn btn-info pull-right"  OnClick="btnReset_Click" BackColor="#f5f5f5"
                            ToolTip="Add" ImageUrl="~/images/close-29.png"  Style="margin-right: 5px;
                            padding: 0px;" runat="server" />
                        <h4 class="modal-title">
                            Remarks</h4>
                        
                    </div>

                       <div class="row">

                        <div class="row marg search-bg">
               
                                <div class="col-lg-10 col-md-10 col-sm-10 cpl-xs-12">
                                    <div class="col-lg-8 col-md-8  col-sm-8 cpl-xs-12">
                                    <div class="form-group" style="margin-bottom: 2px;">
                                        <label for="email" class="col-sm-3 padd linhei">
                                            Remarks:</label>
                                        <div class="col-sm-9 padd">
                                           <asp:DropDownList ID="ddlRemark" runat="server" class="form-control">
                                            <asp:ListItem Selected="True" Value="0">--Select--</asp:ListItem>
                                            <asp:ListItem Value="1">Format not available</asp:ListItem>
                                            <asp:ListItem Value="2">Wrongly activity selected </asp:ListItem>
                                            <asp:ListItem Value="3">Typing error</asp:ListItem>

                                             <asp:ListItem Value="4">Counting error</asp:ListItem>
                                               <asp:ListItem Value="5">C Phone not available</asp:ListItem>
                                        </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>

                          
                                </div>

                                
                        </div>
                  </div>
                    </div>
           </div>
           
           </asp:Panel>

 
</asp:Content>