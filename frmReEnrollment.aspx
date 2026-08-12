<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="frmReEnrollment.aspx.cs" Inherits="frmReEnrollment" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script type="text/javascript">
        $(document).ready(function () {
            $('#ddlCars').multiselect();
            $('#ddlCars1').multiselect({
                numberDisplayed: 2

            });
            $('#ddlCars2').multiselect({
                includeSelectAllOption: true,
                enableFiltering: true

            });
            $('#ddlCars3').multiselect({
                nonSelectedText: 'Select Cars'

            });
        });
    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#ddlCars').multiselect();
            $('#ddlCars1').multiselect({
                numberDisplayed: 2

            });
            $('#ddlCars2').multiselect({
                includeSelectAllOption: true,
                enableFiltering: true

            });
            $('#ddlCars3').multiselect({
                nonSelectedText: 'Select Cars'

            });
        });
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

        function onlyAlphabetsAdd(e, t) {
            try {
                if (window.event) {
                    var charCode = window.event.keyCode;
                }
                else if (e) {
                    var charCode = e.which;
                }
                else { return true; }
                if ((charCode > 64 && charCode < 91) || (charCode > 96 && charCode < 123) || charCode == 32 || charCode == 0 || charCode == 9 || charCode == 08 || charCode == 44 || charCode == 45 || charCode == 48)
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

        function Valdation(txtcls, txtaBoy) {
            var Eboy = 0;
            var Aboy = 0;
            $("." + txtcls).each(function (index, value) {
                if ($.trim($(this).val()) != "")
                    if (!isNaN($(this).val()))

                        Eboy = parseFloat($("." + txtaBoy).val());
                Aboy = parseFloat($("." + txtcls).val());

                if (Aboy < Eboy) {

                    alert("Enrollment  should be higher or equal to Appeared");
                    $("." + txtcls).focus();
                    $("." + txtaBoy).val('');
                    return true;
                }
                else {
                    return true;
                }

            });




        }
    </script>
    <script type="text/javascript">

        function calculate_totals(txtcls, txttotalcls) {
            var TotalCamt = 0;
            $("." + txtcls).each(function (index, value) {
                if ($.trim($(this).val()) != "")
                    if (!isNaN($(this).val()))
                        TotalCamt = TotalCamt + parseFloat($(this).val());
            });
            $("." + txttotalcls).val(TotalCamt);
            return false;
        }

        function arrivaldate(arrivaldate) {

            var arrivaldate = $('#' + arrivaldate).val();

            var today = new Date();
            alert(arrivaldate);
            alert(today.getDate());
            if (arrivaldate > today.getDate()) {
                alert("Should not be future date.");
                document.getElementById("" + sender + "").value = null;
                return false;
            }


        }

        function checkDate(arrivaldate) {
            var EnteredDate = $('#' + arrivaldate).val();

            var date = EnteredDate.substring(0, 2);

            var month = EnteredDate.substring(3, 5);
            var year = EnteredDate.substring(6, 10);

            var myDate = new Date(year, month - 1, date);

            var today = new Date();

            if (myDate > today) {
                alert("Should not be future date.");
                $('#' + arrivaldate).val = '';
            }

        }
    </script>
    <style type="text/css">
        .ajax__calendar_container {
            z-index: 1000;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:UpdatePanel ID="MainPanel" runat="server">
        <ContentTemplate>


            <div class="container-fluid" style="">
                <div class="row">
                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                        <div class="thumbnail" style="background-color: #f5f5f5; margin-bottom: 3px !important;">
                            <div class="panel-heading" style="padding: 0px;">
                                <div class="row">
                                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                        <h3 class="text-danger" style="margin: 0px;">Re-Enrollment</h3>
                                    </div>

                                </div>
                            </div>
                        </div>

                    </div>
                </div>

                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="padding-left: -2px;">
                    <div class="thumbnail" style="background-color: #f5f5f5; float: left; width: 100%;">
                        <div class="panel panel-default">

                            <div class="form-horizontal">
                                <div class="row">
                                    <div style="padding: 0px 10px 0px 10px;">
                                        <div class="row marg search-bg">
                                            <div class="form-horizontal">
                                                <%-- <asp:UpdatePanel runat="server" ID="UpMain">
        <ContentTemplate>--%>
                                                <%--  <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
	<ContentTemplate>
                                                --%>
                                                <div class="row">
                                                    <div class="col-lg-3 col-md-3 col-sm-3 cpl-xs-12">
                                                        <div class="form-group">
                                                            <label for="email" class="col-sm-3 padd linhei" style="padding-top: 2px;">
                                                                Year:
                                                            </label>
                                                            <div class="col-sm-8 padd">
                                                                <asp:DropDownList ID="ddlYear" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddlYear_SelectedIndexChanged" class="form-control ">
                                                                </asp:DropDownList>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-lg-3 col-md-3 col-sm-3 cpl-xs-12">
                                                        <div class="form-group">
                                                            <label for="email" class="col-sm-3 padd linhei" style="padding-top: 2px;">
                                                                State:</label>
                                                            <div class="col-sm-8 padd">
                                                                <asp:DropDownList ID="ddlState" runat="server" OnSelectedIndexChanged="ddlState_SelectedIndexChanged"
                                                                    AutoPostBack="true" class="form-control ">
                                                                </asp:DropDownList>
                                                            </div>
                                                        </div>
                                                    </div>
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
                                                                <asp:DropDownList ID="ddlBlock" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlBlock_SelectedIndexChanged"
                                                                    class="form-control " />
                                                            </div>
                                                        </div>
                                                    </div>

                                                </div>
                                                <div class="row">
                                                    <div id="Div1" class="col-lg-3 col-md-2 col-sm-3 cpl-xs-12" runat="server">
                                                        <div class="form-group">
                                                            <label for="email" class="col-sm-3 padd linhei" style="padding-top: 2px;">
                                                                Panchayat:</label>
                                                            <div class="col-sm-8 padd">
                                                                <asp:DropDownList ID="ddlPanchayat" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlPanchayat_SelectedIndexChanged"
                                                                    class="form-control " />
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div id="Div2" class="col-lg-3 col-md-3 col-sm-3 cpl-xs-12" runat="server">
                                                        <div class="form-group">
                                                            <label for="email" class="col-sm-3 padd linhei" style="padding-top: 2px;">
                                                                Village:</label>
                                                            <div class="col-sm-8 padd">
                                                                <asp:DropDownList ID="ddlVillage" OnSelectedIndexChanged="ddlVillage_SelectedIndexChanged"
                                                                    AutoPostBack="true" runat="server" class="form-control " />
                                                                <span style="width: 5px; float: right; margin: -27px 27px; font-size: 21px;">
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator18" runat="server" Display="Dynamic"
                                                                        ValidationGroup="saves" ControlToValidate="ddlVillage" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                                                                </span>
                                                            </div>
                                                        </div>
                                                    </div>



                                                    <div class="col-lg-3 col-md-3 col-sm-3 cpl-xs-12">
                                                        <div class="form-group">
                                                            <label for="email" class="col-sm-3 padd linhei" style="padding-top: 2px;">
                                                                School:
                                                            </label>
                                                            <div class="col-sm-8 padd">
                                                                <asp:DropDownList ID="ddlSchool" runat="server" class="form-control ">
                                                                </asp:DropDownList>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="col-lg-2 col-md-2 col-sm-2 cpl-xs-10 ">
                                                        <asp:ImageButton ID="btnSerach" ToolTip="Serach" runat="server" OnClick="btnSerach_Click" class="btn btn-danger btn-paddd pull-left" BackColor="#f1f1f1" ImageUrl="~/images/search-29.png" />

                                                        <asp:Button ID="btnMain" OnClick="btnMain_Click" runat="server" Width="65%" Text="Search Enrollment"
                                                            CssClass="btn btn-danger pull-right btn-sm " />
                                                    </div>
                                                </div>

                                                <%--</ContentTemplate>
</asp:UpdatePanel>
                                                --%>
                                            </div>
                                        </div>
                                        <div class="col-lg-12 table table-hover " style="padding: 0px;">
                                            <asp:Panel ID="pnlMain" runat="server">
                                                <asp:UpdatePanel runat="server" ID="UpdatedddddddPanel1">
                                                    <ContentTemplate>
                                                        <div class="form-horizontal">
                                                            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="padding: 0px;">
                                                                <div style="height: 290px; overflow: auto; width: 99%;" align="center">
                                                                    <div>
                                                                        <div class="Row" style="width: 100%">
                                                                            <asp:GridView ID="gvnroll" runat="server" CssClass="table table-striped table-bordered table-hover" DataKeyNames="UniqueChildCode" OnRowDataBound="gvnroll_OnRowCommand" AutoGenerateColumns="False" Font-Names="Arial"
                                                                                Font-Size="12px" Width="100%">
                                                                                <EmptyDataTemplate>
                                                                                    <div style="font-family: Arial; font-size: 12px; font-weight: bold;">
                                                                                        Data not found
                                                                                    </div>
                                                                                </EmptyDataTemplate>
                                                                                <HeaderStyle BackColor="#C1C1C1" ForeColor="White" Height="40px" HorizontalAlign="Center" />
                                                                                <SelectedRowStyle BackColor="#fad669" Font-Bold="True" ForeColor="WhiteSmoke" />
                                                                                <AlternatingRowStyle BackColor="#f1f1f1" />
                                                                                <Columns>
                                                                                    <asp:TemplateField Visible="false">
                                                                                        <ItemTemplate>
                                                                                            <asp:LinkButton ID="lbtn" runat="server" Text="EDIT" OnClick="LnkBtnBlock_OnClick" CommandArgument='<%# Bind("UniqueChildCode") %>'></asp:LinkButton>
                                                                                            <asp:Label ID="lblCUniqueChildCode" Visible="false" BackColor="Transparent" runat="server" Text='<%# Bind("UniqueChildCode") %>' CssClass="form-controlAbhi"></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>

                                                                                    <asp:TemplateField HeaderText="Action" Visible="false" HeaderStyle-Width="15%" HeaderStyle-CssClass="GridHeaderClass">
                                                                                        <ItemTemplate>
                                                                                            <asp:ImageButton ID="ImgAcc" runat="server" OnClick="btn_Delete_Click" ImageUrl="~/images/delete-29.png"
                                                                                                Width="15px" Height="15px"></asp:ImageButton>

                                                                                        </ItemTemplate>
                                                                                        <HeaderStyle Width="5%" />
                                                                                        <ItemStyle HorizontalAlign="Center" />
                                                                                    </asp:TemplateField>


                                                                                    <asp:TemplateField HeaderText="Village Name" Visible="true">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lblPanchayatName" ForeColor="Black" runat="server"
                                                                                                Text='<%# Eval("VillageName") %>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                        <ItemStyle CssClass="padding-lef" />

                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="HHNo" Visible="true">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lblBlockName" ForeColor="Black" runat="server" Text='<%# Eval("HHNo1") %>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                        <ItemStyle CssClass="padding-lef" />

                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Student Name" Visible="true">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lblVillageName" ForeColor="Black" runat="server"
                                                                                                Text='<%# Eval("ChildName") %>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                        <ItemStyle CssClass="padding-lef" />

                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Father Name" Visible="true">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lblSurvayD3ate" ForeColor="Black" runat="server"
                                                                                                Text='<%# Eval("FathersName") %>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                        <ItemStyle CssClass="padding-lef" />

                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Class" Visible="true">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lblMauhalla2" ForeColor="Black" runat="server" Text='<%# Eval("Class") %>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                        <ItemStyle CssClass="padding-lef" />

                                                                                    </asp:TemplateField>

                                                                                    <asp:TemplateField HeaderText="SR. NO." Visible="true">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lblHouse2" ForeColor="Black" runat="server" Text='<%# Eval("Serial") %>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                        <ItemStyle CssClass="padding-lef" />

                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Admission Date" Visible="true">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="ddlEmployee2Code" class="labelGrid" ForeColor="Black" ItemStyle-ForeColor="#333"
                                                                                                runat="server" Text='<%# Eval("EnrolmentDate") %>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                        <ItemStyle CssClass="padding-lef" />

                                                                                    </asp:TemplateField>



                                                                                    <asp:TemplateField HeaderText="Social Category" Visible="true">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lblSalaryPayaeble" ForeColor="Black" runat="server"
                                                                                                Text='<%# Eval("SocialCategory") %>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                        <ItemStyle CssClass="padding-lef" />

                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Gender" Visible="true">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lblBasirrc" ForeColor="Black" runat="server" Text='<%# Eval("Gender") %>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                        <ItemStyle CssClass="padding-lef" />

                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="DOB" Visible="true">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lblHRAyye" ForeColor="Black" runat="server" Text='<%# Eval("DOB") %>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                        <ItemStyle CssClass="padding-lef" />

                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="School Name" Visible="true">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lblConveyaence" class="labelGrid" ForeColor="Black" runat="server"
                                                                                                Text='<%# Eval("School") %>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                        <ItemStyle CssClass="padding-lef" />

                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Enrollment Category" Visible="true">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lblAlflowaeecee" ForeColor="Black" runat="server" Text='<%# Eval("EnrolmentCategory") %>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                        <ItemStyle CssClass="padding-lef" />

                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Education Status" Visible="true">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lblMediecal" ForeColor="Black" runat="server" Text='<%# Eval("EduationStatus") %>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                        <ItemStyle CssClass="padding-lef" />

                                                                                    </asp:TemplateField>

                                                                                    <asp:TemplateField HeaderText="Education Status" Visible="false">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lblUniqueChildCode" ForeColor="Black" runat="server" Text='<%# Eval("UniqueChildCode") %>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                        <ItemStyle CssClass="padding-lef" />

                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Education Status" Visible="false">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lblStatus" ForeColor="Black" runat="server" Text='<%# Eval("Status") %>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                        <ItemStyle CssClass="padding-lef" />

                                                                                    </asp:TemplateField>


                                                                                </Columns>

                                                                            </asp:GridView>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </ContentTemplate>
                                                    <Triggers>
                                                        <asp:PostBackTrigger ControlID="gvnroll" />
                                                    </Triggers>
                                                </asp:UpdatePanel>
                                            </asp:Panel>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <!-- /#wrapper -->
                            <!-- /#wrapper -->
                        </div>
                    </div>
                </div>

                <script type="text/javascript">
                    $(function () {
                        $('#datetimepicker4').datetimepicker();
                    });
                </script>
            </div>

        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnMain" />
            <asp:PostBackTrigger ControlID="btnMain" />

        </Triggers>

    </asp:UpdatePanel>

</asp:Content>
