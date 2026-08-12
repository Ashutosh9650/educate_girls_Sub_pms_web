<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="FrmAnnualPlan_Agp.aspx.cs" Inherits="FrmAnnualPlan_Agp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script type="text/javascript">
        function onEvent(th) {
            var idx = -1, stidx = 0, lstidx = 0;
            var v1 = 0;

            var k1 = 0;
            var k2 = 0;
            var Start = 0;
            var End = 0;
            var MaxVal = 0;
            var txt1idx = 0;
            var Type = $('.clsType').val();
            var uh = 0; var TxtName = "";
            $(th).closest('tr').find('td').each(function (i) {

                if (k1 == 0) {
                    var R = $(this).find("span[class='S']").text();
                    k1 = 1;
                    Start = R;
                    var R1 = $(this).find("span[class='E']").text();
                    k2 = 1;
                    End = R1;
                    var M1 = $(this).find("span[class='M']").text();
                    MaxVal = M1;
                }



                if (Type == "1") {

                    stidx = 0;
                    lstidx = 12;

                }
                else if (Type == "2") {
                    stidx = Start;
                    lstidx = End;
                    if (MaxVal == 7 || MaxVal == 5 || MaxVal == 3 || MaxVal == 1 || MaxVal == 15) {
                        stidx = 0;
                        lstidx = 12;


                    }

                }
                else if (Type == "3") {

                    stidx = Start;
                    lstidx = End;

                    if (MaxVal == 5 || MaxVal == 3 || MaxVal == 2 || MaxVal == 6) {
                        stidx = 0;
                        lstidx = 12;
                    }

                }
                if (i > 0) {
                    var jui = 0;


                    //                  


                    if (idx >= stidx && idx <= lstidx && !isNaN(parseFloat($(this).find("input[type='text']").val()))) {
                        txt1idx = i;

                        v1 += parseFloat($(this).find("input[type='text']").val());
                        if (Type == "1") {


                            if (MaxVal == 2) {


                                if (parseFloat($(this).find("input[type='text']").val()) >= 1) {
                                    uh = uh + 1;

                                    if (uh > 2) {
                                        $(th).val('0');
                                        alert('Entry allowed in any two months!!.');
                                        return false;
                                    }
                                }
                            }


                        }
                        else if (Type == "2") {

                            if (MaxVal == 1) {

                                if (parseFloat($(this).find("input[type='text']").val()) > 1) {
                                    uh = uh + 1;


                                    $(th).val('0');
                                    alert('Only 1 can be entered!! ');
                                    return false;

                                }

                            }


                            else if (MaxVal == 3) {

                                if (parseFloat($(this).find("input[type='text']").val()) > 1) {
                                    uh = uh + 1;


                                    $(th).val('0');
                                    alert('Only 1 can be entered!! ');
                                    return false;

                                }
                                if (parseFloat($(this).find("input[type='text']").val()) >= 1) {
                                    uh = uh + 1;

                                    if (uh > 2) {
                                        $(th).val('0');
                                        alert('Entry allowed in any Two month!!.');
                                        return false;
                                    }
                                }



                            }

                            else if (MaxVal == 4) {

                                if (parseFloat($(this).find("input[type='text']").val()) > 1) {
                                    uh = uh + 1;


                                    $(th).val('0');
                                    alert('Only 1 can be entered!! ');
                                    return false;

                                }
                                if (parseFloat($(this).find("input[type='text']").val()) >= 1) {
                                    uh = uh + 1;

                                    if (uh > 1) {
                                        $(th).val('0');
                                        alert('Entry allowed in any one month!!.');
                                        return false;
                                    }
                                }



                            }
                            else if (MaxVal == 9) {

                                if (parseFloat($(this).find("input[type='text']").val()) == 2) {


                                }
                                else if (parseFloat($(this).find("input[type='text']").val()) == 1) {
                                    uh = uh + 1;


                                    $(th).val('0');
                                    alert('Only 2 can be entered!');
                                    return false;
                                }
                                else if (parseFloat($(this).find("input[type='text']").val()) > 2) {
                                    uh = uh + 1;


                                    $(th).val('0');
                                    alert('Only 2 can be entered!');
                                    return false;
                                }




                            }
                            else if (MaxVal == 5) {

                                if (parseFloat($(this).find("input[type='text']").val()) > 1) {
                                    uh = uh + 1;


                                    $(th).val('0');
                                    alert('Only 1 can be entered!! ');
                                    return false;

                                }
                                var uh12 = parseFloat($(this).find("input[type='text']").val());

                                var jk = ppneedjjLearningValue("Advisory Council Members Identification", i, parseFloat($(this).find("input[type='text']").val()), uh12);

                                var kkkkk = LearningValue("Advisory Council Members Identification", i, parseFloat($(this).find("input[type='text']").val()), uh12);

                                if (kkkkk > 1) {


                                    $(th).val('0');
                                    alert('Only 1 can be entered!! ');
                                    return false;

                                }
                                if (jk >= 1) {
                                    uh = uh + 1;

                                    if (jk > 2) {
                                        $(th).val('0');

                                        alert('Entry allowed in any Two month!!.');

                                        return false;
                                    }
                                }
                                if (jk <= 0) {

                                    pp("Advisory Council Members Meeting", i, 0, 0);

                                }

                                pp("Advisory Council Members Orientation", i, parseFloat($(this).find("input[type='text']").val()), v1);


                            }

                            else if (MaxVal == 7) {

                                if (parseFloat($(this).find("input[type='text']").val()) > 1) {
                                    uh = uh + 1;


                                    $(th).val('0');
                                    alert('Only 1 can be entered!! ');
                                    return false;

                                }
                                var uh12 = parseFloat($(this).find("input[type='text']").val());

                                var jk = ppneedjjLearningValue("Advisory Council Members Orientation", i, parseFloat($(this).find("input[type='text']").val()), uh12);
                                var kkkkk = LearningValue("Advisory Council Members Orientation", i, parseFloat($(this).find("input[type='text']").val()), uh12);

                                if (kkkkk > 1) {


                                    $(th).val('0');
                                    alert('Only 1 can be entered!! ');
                                    return false;

                                }
                                if (jk >= 1) {
                                    uh = uh + 1;

                                    if (jk > 2) {
                                        $(th).val('0');

                                        alert('Entry allowed in any Two month!!.');

                                        return false;
                                    }
                                }


                                pp("Advisory Council Members Meeting", i, parseFloat($(this).find("input[type='text']").val()), v1);



                            }

                            else if (MaxVal == 6) {

                                if (parseFloat($(this).find("input[type='text']").val()) > 1) {
                                    uh = uh + 1;


                                    $(th).val('0');
                                    alert('Only 1 can be entered!! ');
                                    return false;

                                }
                                if (parseFloat($(this).find("input[type='text']").val()) >= 1) {
                                    uh = uh + 1;

                                    if (uh > 6) {
                                        $(th).val('0');
                                        alert('Entry allowed in any Six month!!.');
                                        return false;
                                    }
                                }



                            }







                        }
                        else if (Type == "3") {



                            if (MaxVal == 6) {

                                if (parseFloat($(this).find("input[type='text']").val()) > 1) {
                                    uh = uh + 1;


                                    $(th).val('0');
                                    alert('Only 1 can be entered!! ');
                                    return false;

                                }
                                if (parseFloat($(this).find("input[type='text']").val()) >= 1) {
                                    uh = uh + 1;

                                    if (uh > 1) {
                                        $(th).val('0');
                                        alert('Entry allowed in any one month!!.');
                                        return false;
                                    }
                                }



                            }




                        }
                    }
                }
                idx++;
            });
        }
        function LearningValue(txt, stidx, val, SumValue) {
            var idx = 0;
            var RVal = 0;
            $("[id*=GV_AnnualPlan] tr").each(function (r) {
                if ($(this).find('span').html() == txt) {
                    $(this).eq(0).find('td').each(function (i) {


                        if (txt == "Advisory Council Members Identification") {


                            if (!isNaN(parseFloat($(this).find("input[type='text']").val()))) {
                                if (parseFloat($(this).find("input[type='text']").val()) > 1) {
                                    RVal = parseFloat($(this).find("input[type='text']").val());

                                }
                            }

                        }

                        if (txt == "Advisory Council Members Orientation") {


                            if (!isNaN(parseFloat($(this).find("input[type='text']").val()))) {
                                if (parseFloat($(this).find("input[type='text']").val()) > 1) {
                                    RVal = parseFloat($(this).find("input[type='text']").val());

                                }
                            }

                        }


                        idx++;
                    });
                }
            });
            return RVal;
        }

        function ppneedjjLearningValue(txt, stidx, val, SumValue) {
            var idx = 0;
            var RVal = 0;
            $("[id*=GV_AnnualPlan] tr").each(function (r) {
                if ($(this).find('span').html() == txt) {
                    $(this).eq(0).find('td').each(function (i) {


                        if (txt == "Advisory Council Members Identification") {


                            if (!isNaN(parseFloat($(this).find("input[type='text']").val()))) {
                                RVal += parseFloat($(this).find("input[type='text']").val());


                            }

                        }

                        if (txt == "Advisory Council Members Orientation") {


                            if (!isNaN(parseFloat($(this).find("input[type='text']").val()))) {
                                RVal += parseFloat($(this).find("input[type='text']").val());


                            }

                        }


                        idx++;
                    });
                }
            });
            return RVal;
        }

        function pp(txt, stidx, val, SumValue) {
            var idx = 0;
            $("[id*=GV_AnnualPlan] tr").each(function (r) {
                if ($(this).find('span').html() == txt) {
                    $(this).eq(0).find('td').each(function (i) {
                        if (txt == "Learning Endline for GKP") {
                            if (i == 12) {
                                if (idx >= i && SumValue > 0) {

                                    $(this).find("input[type='text']").val(1);
                                }
                                else if (SumValue == 0) {
                                    $(this).find("input[type='text']").val(0);
                                }
                            }
                        }

                        else if (txt == "Advisory Council Members Orientation") {
                            if (i > 0) {
                                if (idx >= stidx && val == 0 && SumValue == 0) {
                                    $(this).find("input[type='text']").attr("disabled", "disabled");
                                    $(this).find("input[type='text']").val('0');
                                } else if (idx >= stidx && val > 0 && SumValue > 0) {
                                    $(this).find("input[type='text']").removeAttr("disabled");
                                }
                            }
                        }
                        else if (txt == "Advisory Council Members Meeting") {
                            if (i > 0) {
                                if (idx >= stidx && val == 0 && SumValue == 0) {
                                    $(this).find("input[type='text']").attr("disabled", "disabled");
                                    $(this).find("input[type='text']").val('0');
                                } else if (idx >= stidx && val > 0 && SumValue > 0) {
                                    $(this).find("input[type='text']").removeAttr("disabled");
                                }
                            }
                        }
                        else if (i > 0) {
                            if (idx >= stidx) {
                                $(this).find("input[type='text']").val(val * 2);
                            }
                        }
                        idx++;
                    });
                }
            });
        }


        function isNumberKey(txt, evt) {
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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:UpdatePanel runat="server" ID="Upnl">
        <ContentTemplate>
            <div class="container-fluid" style="margin-top: 0px;">
                <div class="row">
                    <div class="col-lg-2 col-md-2 col-sm-3 clsmain" style="padding-right: 0px;">
                        <div class="thumbnail" style="min-height: 750px; width: 228px;">
                            <div style="padding-top: 3px;">
                                <%--<span style="float:left"> <asp:Label ID="lblsearch" runat="server" Text="Search:" ForeColor="Black"></asp:Label></span>--%>
                                <span style="float: right; padding-right: 1px;">
                                    <asp:TextBox ID="txtSearchName" Visible="false" runat="server" OnTextChanged="txtSearchName_Click"
                                        AutoPostBack="true" CssClass="form-control col-lg-1"></asp:TextBox></span>
                            </div>
                            <div style="overflow: auto; margin-top: 35px; height: 750px;">
                                <asp:GridView ID="GVMain" runat="server" Width="100%" AllowPaging="true" PageSize="40"
                                    BorderStyle="None" DataKeyNames="VillageCode,DISECode,RowNo,SchoolLevel,BAlVal,GKP,GKPLevel,ManagementType"
                                    GridLines="None" AutoGenerateColumns="false" OnRowCommand="GVMain_OnRowCommand"
                                    OnPageIndexChanging="GV_Project_PageIndexChanging">
                                    <EmptyDataTemplate>
                                        <div style="font-family: Arial; font-size: 12px; font-weight: bold; color: Red;">
                                            Data not found
                                        </div>
                                    </EmptyDataTemplate>
                                    <FooterStyle CssClass="FooterStyle" />
                                    <HeaderStyle BackColor="#C1C1C1" ForeColor="White" Height="44px" />
                                    <RowStyle HorizontalAlign="Left" />
                                    <PagerStyle CssClass="paging" />
                                    <SelectedRowStyle BackColor="#fad669" Font-Bold="True" ForeColor="WhiteSmoke" />
                                    <AlternatingRowStyle BackColor="#f1f1f1" />
                                    <%-- <SelectedRowStyle BackColor="#fad669" Font-Bold="True" ForeColor="WhiteSmoke" />
                    <AlternatingRowStyle BackColor="#f1f1f1" />--%>
                                    <Columns>
                                        <asp:ButtonField HeaderText="Village Name " ItemStyle-ForeColor="#333" DataTextField="VillageName"
                                            CommandName="GVUIO">
                                            <ItemStyle CssClass="padding-lef" Height="30px" />
                                            <HeaderStyle CssClass="padding-lef" />
                                        </asp:ButtonField>
                                        <asp:ButtonField HeaderText="School Name" ItemStyle-ForeColor="#333" DataTextField="SchoolName"
                                            CommandName="GVUIO">
                                            <ItemStyle CssClass="padding-lef" Height="30px" />
                                            <HeaderStyle CssClass="padding-lef" />
                                        </asp:ButtonField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                    <div class="col-lg-10 col-md-10 col-sm-9">
                        <div class="row">
                            <div class="col-lg-12">
                                <div class="panel panel-default">
                                    <div class="panel-heading">
                                        <div class="row">
                                            <div class="col-lg-6 col-md-6 col-sm-6" style="padding: 0px;">
                                                <h3 class="text-danger" style="margin: 0px;">AGP Annual Plan</h3>
                                            </div>
                                            <div class="col-lg-6 col-md-6 col-sm-6 " style="padding: 0px">
                                                <button type="button" id="ton-new" class="btn btn-primary" style="float: right; position: relative; margin-right: 5px; right: 1px; color: #fff; background-color: #337ab7; border-color: #2e6da4;">
                                                    <i class="fa fa-bars"></i>
                                                </button>
                                                <%--     <input type="image" id="ton-new" class="butt" src="Images/search-not-29.png" title="Search" />--%>
                                                <asp:ImageButton ID="btnDelete" OnClick="btnDelete_Click" CssClass="btn btn-info pull-right"
                                                    ToolTip="Delete" BackColor="#f5f5f5" ImageUrl="~/images/delete-29.png" Style="margin-right: 5px; padding: 0px;"
                                                    runat="server" />
                                                <asp:ImageButton ID="btnsave" CssClass="btn btn-info pull-right" BackColor="#f5f5f5"
                                                    ToolTip="Save" ImageUrl="~/images/save-29-1.png" OnClick="btnsave_Click"
                                                    Style="margin-right: 5px; padding: 0px;" runat="server" />

                                            </div>
                                        </div>
                                    </div>
                                    <div>
                                    </div>
                                    <div class="form-horizontal">
                                        <div class="row">
                                            <div id="div-show-new">
                                                <div class="row marg search-bg">
                                                    <div class="form-horizontal">
                                                        <div class="col-lg-3 col-md-3 col-sm-3 col-xs-12">
                                                            <div class="form-group" style="margin-bottom: 7px;">
                                                                <label for="email" class="col-sm-3 padd linhei">
                                                                    Year:</label>
                                                                <div class="col-sm-9 padd">
                                                                    <asp:DropDownList ID="ddlYear" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddlYear_SelectedIndexChanged"
                                                                        class="form-control ">
                                                                    </asp:DropDownList>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="col-lg-3 col-md-3 col-sm-3 col-xs-12">
                                                            <div class="form-group" style="margin-bottom: 7px;">
                                                                <label for="email" class="col-sm-3 padd linhei">
                                                                    Level:</label>
                                                                <div class="col-sm-9 padd">
                                                                    <asp:DropDownList ID="ddlType" runat="server" OnSelectedIndexChanged="ddlType_SelectedIndexChanged"
                                                                        AutoPostBack="true" CssClass="form-control clsType">
                                                                        <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                                                        <asp:ListItem Text="District Level" Value="1"></asp:ListItem>
                                                                        <asp:ListItem Text="Village Level" Value="2"></asp:ListItem>

                                                                    </asp:DropDownList>
                                                                </div>
                                                            </div>
                                                        </div>


                                                        <div class="col-lg-3 col-md-3 col-sm-3 cpl-xs-12">
                                                            <div class="form-group">
                                                                <label for="email" class="col-sm-3 padd linhei" style="padding-top: 2px;">
                                                                    State:</label>
                                                                <div class="col-sm-9 padd">
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
                                                                <div class="col-sm-9 padd">
                                                                    <asp:DropDownList ID="ddlDistrict" runat="server" OnSelectedIndexChanged="ddlDistrict_SelectedIndexChanged"
                                                                        AutoPostBack="true" class="form-control " />
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="col-lg-3 col-md-3 col-sm-3 cpl-xs-12 ">
                                                            <div runat="server" id="divBlock" style="display: none;">
                                                                <div class="form-group">
                                                                    <label for="email" class="col-sm-3 padd linhei" style="padding-top: 2px;">
                                                                        Block:</label>
                                                                    <div class="col-sm-9 padd">
                                                                        <asp:DropDownList ID="ddlBlock" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlBlock_SelectedIndexChanged"
                                                                            class="form-control " />
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="col-lg-3 col-md-3 col-sm-3 cpl-xs-12">
                                                            <div runat="server" id="divPhy" style="display: none;">
                                                                <div class="form-group">
                                                                    <label for="email" class="col-sm-3 padd linhei" style="padding-top: 2px;">
                                                                        Panchayat:</label>
                                                                    <div class="col-sm-9 padd">
                                                                        <asp:DropDownList ID="ddlPanchayat" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlPanchayat_SelectedIndexChanged"
                                                                            class="form-control " />
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="col-lg-3 col-md-3 col-sm-3 cpl-xs-12">
                                                            <div runat="server" id="divVill" style="display: none;">
                                                                <div class="form-group">
                                                                    <label for="email" class="col-sm-3 padd linhei" style="padding-top: 2px;">
                                                                        Village:</label>
                                                                    <div class="col-sm-9 padd">
                                                                        <asp:DropDownList ID="ddlVillage" OnSelectedIndexChanged="ddlVillage_SelectedIndexChanged"
                                                                            AutoPostBack="true" runat="server" class="form-control " />
                                                                        <span style="width: 5px; float: right; margin: -27px 27px; font-size: 21px;">
                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator18" runat="server" Display="Dynamic"
                                                                                ValidationGroup="saves" ControlToValidate="ddlVillage" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                                                                        </span>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="col-lg-1 col-md-1 col-sm-2 cpl-xs-12 col-lg-offset-1 col-md-offset-1 col-sm-offset-1 col-xs-offset-0">
                                                            <asp:ImageButton ID="btnSerach" ToolTip="Serach" runat="server" class="btn btn-danger btn-paddd pull-right"
                                                                BackColor="#f1f1f1" OnClick="btnSerach_Click" ImageUrl="~/images/search-29.png" />
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-lg-12">
                                                <asp:Panel ID="pnlMain" Enabled="false" runat="server">
                                                    <div class="row">
                                                        <asp:Label ID="lblMsg" CssClass="pull-right" Style="font-size: medium; color: red; margin-right: 469px;" Visible="false" Text="Please enter no. of participants here" runat="server"></asp:Label>
                                                        <div id="DVEE" runat="server" class="thumbnail clsAnnualPlan" style="float: left; width: 100%;">
                                                            <asp:GridView ID="GV_AnnualPlan" Width="100%" ShowFooter="true" runat="server" BorderStyle="None"
                                                                OnRowDataBound="GV_AnnualPlan_OnRowDataBound" GridLines="None" AutoGenerateColumns="false">
                                                                <EmptyDataTemplate>
                                                                </EmptyDataTemplate>
                                                                <FooterStyle CssClass="FooterStyle" />
                                                                <HeaderStyle BackColor="#f5f5f5" ForeColor="Black" Height="25px" />
                                                                <RowStyle HorizontalAlign="Center" />
                                                                <SelectedRowStyle BackColor="#fad669" ForeColor="WhiteSmoke" />
                                                                <AlternatingRowStyle BackColor="#f1f1f1" />
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="Description">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="LblDesc" CssClass="D" Text='<%#Bind("Description") %>' runat="server"></asp:Label>
                                                                            <asp:Label ID="LblLookUp" Text='<%#Bind("LookupCode") %>' Visible="false" runat="server"></asp:Label>
                                                                            <asp:Label ID="lblStartMonth" CssClass="S" Style="color: blue; display: none;" Text='<%#Bind("StartMonth") %>' runat="server"></asp:Label>
                                                                            <asp:Label ID="lblEndMonth" CssClass="E" Style="color: blue; display: none;" Text='<%#Bind("EndMonth") %>' runat="server"></asp:Label>
                                                                            <asp:Label ID="lblMaxVal" CssClass="M" Style="color: blue; display: none;" Text='<%#Bind("MaxVal") %>' runat="server"></asp:Label>
                                                                            <asp:Label ID="lblLookupType" Style="color: blue; display: none;" Text='<%#Bind("LookupType") %>' runat="server"></asp:Label>
                                                                            <asp:Label ID="lblPhageFlag" Style="color: blue; display: none;" Text='<%#Bind("PhageFlag") %>' runat="server"></asp:Label>


                                                                        </ItemTemplate>
                                                                        <ItemStyle Width="20%" HorizontalAlign="Left" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Apr">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="TxtApr" MaxLength="3" Enabled="false" CssClass="form-control cMay"
                                                                                onchange="return onEvent(this);" Text='<%#Bind("Apr") %>' runat="server" onkeypress="return isNumberKey(this,event);"></asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="May">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="TxtMay" MaxLength="3" Enabled="false" CssClass="form-control cMay"
                                                                                onchange="return onEvent(this);" Text='<%#Bind("May") %>' runat="server" onkeypress="return isNumberKey(this,event);"></asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Jun">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="TxtJun" MaxLength="3" Enabled="false" CssClass="form-control cMay"
                                                                                onchange="return onEvent(this);" Text='<%#Bind("Jun") %>' runat="server" onkeypress="return isNumberKey(this,event);"></asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Jul">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="TxtJul" MaxLength="3" Enabled="false" CssClass="form-control cMay"
                                                                                onchange="return onEvent(this);" Text='<%#Bind("Jul") %>' runat="server" onkeypress="return isNumberKey(this,event);"></asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Aug">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="TxtAug" MaxLength="3" Enabled="false" CssClass="form-control cMay"
                                                                                onchange="return onEvent(this);" Text='<%#Bind("Aug") %>' runat="server" onkeypress="return isNumberKey(this,event);"></asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Sep">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="TxtSep" MaxLength="3" Enabled="false" CssClass="form-control cMay"
                                                                                onchange="return onEvent(this);" Text='<%#Bind("Sep") %>' runat="server" onkeypress="return isNumberKey(this,event);"></asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Oct">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="TxtOct" MaxLength="3" Enabled="false" CssClass="form-control cMay"
                                                                                onchange="return onEvent(this);" Text='<%#Bind("Oct") %>' runat="server" onkeypress="return isNumberKey(this,event);"></asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Nov">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="TxtNov" MaxLength="3" Enabled="false" CssClass="form-control cMay"
                                                                                onchange="return onEvent(this);" Text='<%#Bind("Nov") %>' runat="server" onkeypress="return isNumberKey(this,event);"></asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Dec">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="TxtDec" MaxLength="3" Enabled="false" CssClass="form-control cMay"
                                                                                onchange="return onEvent(this);" Text='<%#Bind("Dec") %>' runat="server" onkeypress="return isNumberKey(this,event);"></asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Jan">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="TxtJan" MaxLength="3" Enabled="false" CssClass="form-control cMay"
                                                                                onchange="return onEvent(this);" Text='<%#Bind("Jan") %>' runat="server" onkeypress="return isNumberKey(this,event);"></asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Feb">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="TxtFeb" MaxLength="3" Enabled="false" CssClass="form-control cMay"
                                                                                onchange="return onEvent(this);" Text='<%#Bind("Feb") %>' runat="server" onkeypress="return isNumberKey(this,event);"></asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Mar">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="TxtMar" MaxLength="3" Enabled="false" CssClass="form-control cMay"
                                                                                onchange="return onEvent(this);" Text='<%#Bind("Mar") %>' runat="server" onkeypress="return isNumberKey(this,event);"></asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                            </asp:GridView>
                                                        </div>
                                                    </div>
                                                </asp:Panel>
                                            </div>
                                        </div>
                                    </div>
                                    <!-- /#page-content-wrapper -->
                                </div>
                                <!-- /#wrapper -->
                                <!-- /#wrapper -->
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
