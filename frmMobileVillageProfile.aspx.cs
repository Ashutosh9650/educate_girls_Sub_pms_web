using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Globalization;
using System.Drawing;
using System.Data.SqlClient;


public partial class frmMobileVillageProfile : System.Web.UI.Page
{
    clsMain objMain = new clsMain();
    string conditions = "";
    Comman objComman = new Comman();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            LoadData();
            ModalPopupExtender.Hide();
            this.ModalPopupExtender1.Hide();
            txtDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            txtFdate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            TxtToDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            if (Request.QueryString["ID"] != null)
            {

                //if (Session["user_level"].ToString() == "19")
                //{
                //    string Strhh = Convert.ToString(Session["BlockCodeAct"]);
                //    DataTable dt = objMain.GetActivityUserWiseMaxDateNew(ddlUser.SelectedValue, Strhh);
                //    if (dt.Rows.Count > 0)
                //    {
                //        if (Convert.ToString(dt.Rows[0]["ActivityDate"].ToString()) != "")
                //        {
                //            CalendarExtenderTourdate.StartDate = Convert.ToDateTime(dt.Rows[0]["ActivityDate"].ToString()).AddDays(1);
                //        }
                //    }

                //}
                //if (Session["user_level"].ToString() == "39" || Session["user_level"].ToString() == "30")
                //{
                //    string Strhh =Convert.ToString(Session["BlockCodeAct"]);
                //    DataTable dt = objMain.GetActivityUserWiseMaxDateNewIO(ddlUser.SelectedValue, Strhh);
                //    if (dt.Rows.Count > 0)
                //    {
                //        if (Convert.ToString(dt.Rows[0]["ActivityDate"].ToString()) != "")
                //        {
                //            CalendarExtenderTourdate.StartDate = Convert.ToDateTime(dt.Rows[0]["ActivityDate"].ToString()).AddDays(1);
                //        }
                //    }
                //}


                if (Session["user_level"].ToString() == "19")
                {
                    DataTable dt = objMain.GetActivityUpdateDateWiseBlockWiseNew(Convert.ToString(Session["BlockCodeAct"]), "2", "FC");
                    if (dt.Rows.Count > 0)
                    {
                    }
                    else
                    {

                        dt = objMain.GetActivityUserWiseMaxDateNew(ddlUser.SelectedValue, Convert.ToString(Session["BlockCodeAct"]));
                    }
                    if (dt.Rows.Count > 0)
                    {
                        if (Convert.ToString(dt.Rows[0]["ActivityDate"].ToString()) != "")
                        {

                            DateTime Activitydate1 = Convert.ToDateTime(dt.Rows[0]["ActivityDate"].ToString());
                            if (Activitydate1.Day == 1 && Activitydate1.Month == 4)
                            {
                                CalendarExtenderTourdate.StartDate = Convert.ToDateTime(dt.Rows[0]["ActivityDate"].ToString()).AddDays(0);
                            }
                            else
                            {
                                CalendarExtenderTourdate.StartDate = Convert.ToDateTime(dt.Rows[0]["ActivityDate"].ToString()).AddDays(1);
                            }


                        }
                    }

                }
                if (Session["user_level"].ToString() == "39" || Session["user_level"].ToString() == "30")
                {
                    DataTable dt = objMain.GetActivityUpdateDateWiseBlockWiseNew(Convert.ToString(Session["BlockCodeAct"]), "2", "B");
                    if (dt.Rows.Count > 0)
                    {
                    }
                    else
                    {

                        dt = objMain.GetActivityUserWiseMaxDateNewIO(ddlUser.SelectedValue, Convert.ToString(Session["BlockCodeAct"]));
                    }

                    if (dt.Rows.Count > 0)
                    {
                        if (Convert.ToString(dt.Rows[0]["ActivityDate"].ToString()) != "")
                        {
                            DateTime Activitydate1 = Convert.ToDateTime(dt.Rows[0]["ActivityDate"].ToString());
                            if (Activitydate1.Day == 1 && Activitydate1.Month == 4)
                            {
                                CalendarExtenderTourdate.StartDate = Convert.ToDateTime(dt.Rows[0]["ActivityDate"].ToString()).AddDays(0);
                            }
                            else
                            {
                                CalendarExtenderTourdate.StartDate = Convert.ToDateTime(dt.Rows[0]["ActivityDate"].ToString()).AddDays(1);
                            }

                        }
                    }
                }
                string QueryString = Request.QueryString["ID"];
                string[] a = QueryString.Split(',');
                txtDate.Text = a[0].ToString();
                LoadData(Session["Cluseter"].ToString());


                string ToDate = txtDate.Text;
                string[] c = ToDate.Split('/');
                string aToDate = c[2] + '-' + c[1] + '-' + c[0];

                string con = "";
                DataTable dtMain = null;
                if (Session["user_level"].ToString() == "19")
                {
                    con = "ActivityDate =('" + aToDate + "') and  UserEntry=2 and ApproveStatus='FC'   and mstCluster.ClusterCode='" + Session["Cluseter"].ToString() + "' ";
                    dtMain = objMain.LoadAllActivtiyDatewise(con, 2);

                }
                if (Session["user_level"].ToString() == "39" || Session["user_level"].ToString() == "30")
                {
                    con = "ActivityDate =('" + aToDate + "')  and  UserEntry=2 and ApproveStatus='B'  and mstCluster.ClusterCode='" + Session["Cluseter"].ToString() + "' ";
                    dtMain = objMain.LoadAllActivtiyDatewise(con, 2);
                    // dtMain = objMain.LoadSchoolActivtiyCluseterIO(afromDate, aToDate, ddlBlock.SelectedValue, con);
                }
                if (dtMain.Rows.Count > 0)
                {
                    ddlUser.SelectedValue = dtMain.Rows[0]["UserName"].ToString();
                    ddlUser_SelectedIndexChanged(ddlUser, null);
                    if (ddlUser.SelectedIndex > 0)
                    {
                        ddlVilage.SelectedValue = dtMain.Rows[0]["Villagecode"].ToString();
                        ddlVilage_SelectedIndexChanged(ddlVilage, null);
                        //  ddlSchool.SelectedValue = dtMain.Rows[0]["SchoolCode"].ToString();

                        btnSerach_Click(btnSerach, null);
                    }
                }
                else
                {
                    ViewState["GUID"] = "";
                }
                pnlMain.Enabled = false;
             

            }
        }
    }
    protected void btnCLT_Click(object sender, EventArgs e)
    {

        foreach (ListItem item in CBL_bookformat.Items) { item.Selected = false; }
        foreach (ListItem item in CBL_bookformatNew.Items) { item.Selected = false; }
        foreach (ListItem item in CBL_bookformatNew1.Items) { item.Selected = false; }
        //foreach (ListItem item in CBL_Muhula.Items) { item.Selected = false; }
        //foreach (ListItem item in CBL_MuhulaNew.Items) { item.Selected = false; }
        //foreach (ListItem item in CBL_MuhulaNew1.Items) { item.Selected = false; }
        txt_pbname.Text = "";
        txt_pbnameNew.Text = "";
        txt_pbnameNew1.Text = "";
        txtmainother.Text = "";
        txtV1illager.Text = "";
        TxtGSS_FeMale.Text = "";
        TxtGSS_Male.Text = "";
        txt_bookformatOther.Text = "";
        txt_bookformatOther1.Text = "";
        chkmcommmeting.Checked = false;
        chkcommmetingTB.Checked = false;
        chkcommmetingFC.Checked = false;
        rdEnrollMent.Checked = false;
        rdRetention.Checked = false;



    }

    protected void btnmm_Click(object sender, EventArgs e)
    {
        foreach (ListItem item in CBL_Muhula.Items) { item.Selected = false; }
        foreach (ListItem item in CBL_MuhulaNew.Items) { item.Selected = false; }
        foreach (ListItem item in CBL_MuhulaNew1.Items) { item.Selected = false; }
        txtMuhala.Text = "";
        txtMuhalaNew.Text = "";
        txtMuhalaNew1.Text = "";
        chkmuhala.Checked = false;
        rblmuhulaTb.Checked = false;
        rblmuhulaFC.Checked = false;
        txtVillager2.Text = "";
        txtmOther.Text = "";
        txtmOther1.Text = "";
        TxtMM_FeMale.Text = "";
        TxtMM_Male.Text = "";
        rdEnrollment1.Checked = false;
        rdRetantion1.Checked = false;

    }


    protected void btnOther_Click(object sender, EventArgs e)
    {

        foreach (ListItem item in chk_othercom.Items) { item.Selected = false; }
        foreach (ListItem item in chk_othercom_New.Items) { item.Selected = false; }
        foreach (ListItem item in chk_othercom_New1.Items) { item.Selected = false; }
        tc1.Text = "";
        txtOtherComminuty.Text = "";
        txtOtherComminutyNew.Text = "";
        txtOtherComminutyNew1.Text = "";
        chkothercomm.Checked = false;
        rblothercommTb.Checked = false;
        rblothercommfc.Checked = false;
        txtvillager3.Text = "";
        txtOtherComm.Text = "";
        txtOtherComm1.Text = "";
        TxtCm1_FeMale.Text = "";
        TxtCm1_Male.Text = "";
        rdEnrollment2.Checked = false;
        rdRetantion2.Checked = false;
    }
    public void LoadData(string ClusterName)
    {

        string fromDate = txtDate.Text;
        string[] d = fromDate.Split('/');
        string afromDate = d[2] + '-' + d[1] + '-' + d[0];




        string strQry = "";
        strQry = "Select  distinct UserName as UserId,[FristName]+' ('+ UserName +')' as [UserName]  from MstUser  where UserLevel=24 and VillageCode = '" + Session["Cluseter"].ToString() + "'  ";

        strQry += "union  ";
        strQry += " Select  distinct UserName as UserId,[FristName]+' ('+ UserName +')' as [UserName]  from MstUser  where  UserName in(  ";
        strQry += " select UserID from tblActivityUpdate_Village  ";
        strQry += " inner join mst5village on mst5village.villagecode=tblActivityUpdate_Village.villagecode  ";
        strQry += " where ActivityDate =('" + afromDate + "')  and  ";
        strQry += " mst5village.ClusterCode  = '" + Session["Cluseter"].ToString() + "')    ";


        //    conditions = "UserLevel=24 and VillageCode  in( select ClusterCode from mstCluster where ClusterName ='" + ClusterName + "') ";
        //if (Session["user_level"].ToString() == "39" || Session["user_level"].ToString() == "30" || Session["user_level"].ToString() == "30")
        //{
        //    conditions = conditions + " and DistrictCode='" + Session["DistrictCode"].ToString() + "' ";
        //}

        //if (Session["user_level"].ToString() == "19" )
        //{
        //    conditions = conditions + " and BlockCode='" + Session["BlockCode"].ToString() + "' ";
        //}
        //if (Session["user_level"].ToString() == "24" || Session["user_level"].ToString() == "30" || Session["user_level"].ToString() == "61" || Session["user_level"].ToString() == "59")
        //{
        //    conditions = conditions + " and UserName='as' ";
        //}
        DataTable dtUser = objMain.LoadData(strQry);
        objComman.BindDLLMasterTable("MstUser", "UserName as UserId,[FristName]+' ('+ UserName +')' as [UserName] ", dtUser, conditions, "", "", ddlUser, "UserName", "UserId", "Select");



    }
    protected void btnD2dSerach_Click(object sender, EventArgs e)
    {
        if (this.ddlSearch.SelectedIndex > 0)
        {
            DataTable dataTable = this.Session["D2dBind"] as DataTable;
            if (Convert.ToInt32(this.ddlSearch.SelectedValue) == 1)
            {
                string str = "UniqueIdNew";
                DataTable dataTable2 = dataTable.Copy();
                string rowFilter = str + " like '%" + this.txtSearch.Text.Trim() + "%'   ";
                dataTable2.DefaultView.RowFilter = rowFilter;
                dataTable2.DefaultView.Sort = "UniqueIdNew asc";
                Gv_Display.DataSource = dataTable2.DefaultView.ToTable();
                Gv_Display.DataBind();
            }
            if (Convert.ToInt32(this.ddlSearch.SelectedValue) == 2)
            {
                string str2 = "HHNo";
                DataTable dataTable3 = dataTable.Copy();
                string rowFilter = str2 + " like '%" + this.txtSearch.Text.Trim() + "%'   ";
                dataTable3.DefaultView.RowFilter = rowFilter;
                dataTable3.DefaultView.Sort = "HHNo asc";
                Gv_Display.DataSource = dataTable3.DefaultView.ToTable();
                Gv_Display.DataBind();
            }

            if (Convert.ToInt32(this.ddlSearch.SelectedValue) == 3)
            {
                string str2 = "ChildName";
                DataTable dataTable3 = dataTable.Copy();
                string rowFilter = str2 + " like '%" + this.txtSearch.Text.Trim() + "%'   ";
                dataTable3.DefaultView.RowFilter = rowFilter;
                dataTable3.DefaultView.Sort = "ChildName asc";
                Gv_Display.DataSource = dataTable3.DefaultView.ToTable();
                Gv_Display.DataBind();
            }

            if (Convert.ToInt32(this.ddlSearch.SelectedValue) == 4)
            {
                string str2 = "FathersName";
                DataTable dataTable3 = dataTable.Copy();
                string rowFilter = str2 + " like '%" + this.txtSearch.Text.Trim() + "%'   ";
                dataTable3.DefaultView.RowFilter = rowFilter;
                dataTable3.DefaultView.Sort = "FathersName asc";
                Gv_Display.DataSource = dataTable3.DefaultView.ToTable();
                Gv_Display.DataBind();
            }
        }
        this.ModalPopupExtender.Show();
    }
    protected void btnApprove_Click(object sender, EventArgs e)
    {
        //  btnApprove.Attributes.Add("onclick", "javascript:return " + "confirm('Please confirm if you want to approve? ')");


        Response.Redirect("~/FrmActivityDatewiseSearch.aspx?ID=" + Session["CluseterName"].ToString() + "," + Session["FromData"].ToString() + "," + Session["Todate"].ToString() + "");


    }
    protected void btnReset_Click(object sender, EventArgs e)
    {
        if (ddlRemark.SelectedIndex > 0)
        {
            pnlMain.Enabled = true;
            btnSerach_Click(btnSerach, null);
        }
        else
        {
            pnlMain.Enabled = false;
        }
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        this.ModalPopupExtender1.Show();
    }
    public void LoadData()
    {
        string strQry;
        // strQry = " select Villagecode + '-' +RIGHT('0000' +  convert(varchar,serial), 4) as UniqueId,HHNo,ChildName,FathersName  from tblDTD   where villagecode='82106996'  ";
        strQry = " select *  from [MSTtopicDiscuss]   where Flag=1 and [Language]=0  ";


        DataTable dtRole = objMain.LoadData(strQry);
        CBL_bookformat.DataSource = dtRole;
        CBL_bookformat.DataTextField = "TopicDiscussName";
        CBL_bookformat.DataValueField = "TopicDIscussID";
        CBL_bookformat.DataBind();


        CBL_Muhula.DataSource = dtRole;
        CBL_Muhula.DataTextField = "TopicDiscussName";
        CBL_Muhula.DataValueField = "TopicDIscussID";
        CBL_Muhula.DataBind();

        chk_othercom.DataSource = dtRole;
        chk_othercom.DataTextField = "TopicDiscussName";
        chk_othercom.DataValueField = "TopicDIscussID";
        chk_othercom.DataBind();

        chk_c2.DataSource = dtRole;
        chk_c2.DataTextField = "TopicDiscussName";
        chk_c2.DataValueField = "TopicDIscussID";
        chk_c2.DataBind();

        chk_comm.DataSource = dtRole;
        chk_comm.DataTextField = "TopicDiscussName";
        chk_comm.DataValueField = "TopicDIscussID";
        chk_comm.DataBind();

        strQry = " select *  from [MSTtopicDiscuss]   where Flag=4 and [Language]=0  ";


        DataTable dtTopic = objMain.LoadData(strQry);

        chk_chkconn.DataSource = dtTopic;
        chk_chkconn.DataTextField = "TopicDiscussName";
        chk_chkconn.DataValueField = "TopicDIscussID";
        chk_chkconn.DataBind();


        strQry = " select *  from [MSTtopicDiscuss]   where Flag=5 and [Language]=0  ";

        DataTable dtTopic1 = objMain.LoadData(strQry);

        chk_Suport.DataSource = dtTopic1;
        chk_Suport.DataTextField = "TopicDiscussName";
        chk_Suport.DataValueField = "TopicDIscussID";
        chk_Suport.DataBind();

        strQry = " select *  from [MSTtopicDiscuss]   where Flag=27 and [Language]=0  ";
        DataTable dtNew = objMain.LoadData(strQry);
        CBL_bookformatNew.DataSource = dtNew;
        CBL_bookformatNew.DataTextField = "TopicDiscussName";
        CBL_bookformatNew.DataValueField = "TopicDIscussID";
        CBL_bookformatNew.DataBind();

        CBL_MuhulaNew.DataSource = dtNew;
        CBL_MuhulaNew.DataTextField = "TopicDiscussName";
        CBL_MuhulaNew.DataValueField = "TopicDIscussID";
        CBL_MuhulaNew.DataBind();

        chk_othercom_New.DataSource = dtNew;
        chk_othercom_New.DataTextField = "TopicDiscussName";
        chk_othercom_New.DataValueField = "TopicDIscussID";
        chk_othercom_New.DataBind();

        strQry = " select *  from [MSTtopicDiscuss]   where Flag=28 and [Language]=0  ";
        DataTable dtNew1 = objMain.LoadData(strQry);
        CBL_bookformatNew1.DataSource = dtNew1;
        CBL_bookformatNew1.DataTextField = "TopicDiscussName";
        CBL_bookformatNew1.DataValueField = "TopicDIscussID";
        CBL_bookformatNew1.DataBind();

        CBL_MuhulaNew1.DataSource = dtNew1;
        CBL_MuhulaNew1.DataTextField = "TopicDiscussName";
        CBL_MuhulaNew1.DataValueField = "TopicDIscussID";
        CBL_MuhulaNew1.DataBind();

        chk_othercom_New1.DataSource = dtNew1;
        chk_othercom_New1.DataTextField = "TopicDiscussName";
        chk_othercom_New1.DataValueField = "TopicDIscussID";
        chk_othercom_New1.DataBind();

        strQry = " select *  from [mstLookup]   where [LookupFlag]='STN'   ";

        DataTable dtSTN = objMain.LoadData(strQry);
        Session["dtstn"] = dtSTN;


        objComman.BindDLL("mstLookup", "LookupCode,Description", "[LookupFlag]='MFo' ", "LookupCode", "asc", ddlFo, "Description", "LookupCode", "Select");

        objComman.BindDLL("mstLookup", "LookupCode,Description", "[LookupFlag]='Ine' ", "LookupCode", "asc", ddlIReasons, "Description", "LookupCode", "Select");

         if (Session["StateCode"].ToString() == "8")
        {
            objComman.BindDLL("mstLookup", "LookupCode,Description", "[LookupFlag]='DOP' and LookupCode in(1,2,3,4,6) ", "LookupCode", "asc", ddlDOproof, "Description", "LookupCode", "Select");
        }
        else
        {
            objComman.BindDLL("mstLookup", "LookupCode,Description", "[LookupFlag]='DOP' and LookupCode in(1,2,3,5,6) ", "LookupCode", "asc", ddlDOproof, "Description", "LookupCode", "Select");
        }


        objComman.BindDLL("mstLookup", "LookupCode,Description", "[LookupFlag]='MCL' ", "LookupCode", "asc", ddlMonth, "Description", "LookupCode", "Select");



        objComman.BindDLL("mstLookup", "LookupCode,Description", "[LookupFlag]='MST' ", "LookupCode", "asc", ddlFromStatus, "Description", "LookupCode", "Select");

        objComman.BindDLL("mstLookup", "LookupCode,Description", "[LookupFlag]='ECM' ", "LookupCode", "asc", ddlCategory, "Description", "LookupCode", "Select");
        objComman.BindDLL("mstLookup", "LookupCode,Description", "[LookupFlag]='CLM' ", "LookupCode", "asc", ddlClass, "Description", "LookupCode", "Select");

        objComman.BindDLL("mstLookup", "LookupCode,Description", "[LookupFlag]='INT ' ", "LookupCode", "asc", ddlMigration, "Description", "LookupCode", "Select");

        //strQry = " select UserName as UserId,[UserName]+' ('+ FristName +')' as [UserName]  from MstUser   where UserLevel=24  ";

        //DataTable dtUser = objMain.LoadData(strQry);
        //if (dtUser.Rows.Count > 0)
        //{
        //    ddlUser.DataSource = dtUser;
        //    ddlUser.DataTextField = "UserName";
        //    ddlUser.DataValueField = "UserId";
        //    ddlUser.DataBind();
        //}

        conditions = "UserLevel=24";
        if (Session["user_level"].ToString() == "39" || Session["user_level"].ToString() == "30" || Session["user_level"].ToString() == "30")
        {
            conditions = conditions + " and DistrictCode='" + Session["DistrictCode"].ToString() + "' ";
        }

        if (Session["user_level"].ToString() == "19")
        {
            conditions = conditions + " and BlockCode='" + Session["BlockCode"].ToString() + "' ";
        }
        if (Session["user_level"].ToString() == "24" || Session["user_level"].ToString() == "30" || Session["user_level"].ToString() == "61" || Session["user_level"].ToString() == "59")
        {
            conditions = conditions + " and UserName='assa' ";
        }

        // objComman.BindDLL("MstUser", "UserName as UserId,FristName +' ('+ UserName +')' as [UserName] ", conditions, "", "", ddlUser, "UserName", "UserId", "Select");


        //ModalPopupExtender.Dispose();
        //  Gv_Display.DataBind();
    }

    protected void ddlUser_SelectedIndexChanged(object sender, EventArgs e)
    {
        string strQry = "";
        if (ddlUser.SelectedIndex > 0)
        {
            strQry = "   select Villagecode  from MstUser   where UserName='" + ddlUser.SelectedValue + "' ";
            DataTable dtUserVillage = objMain.LoadData(strQry);

            string strVillage = dtUserVillage.Rows[0]["Villagecode"].ToString();
            if (strVillage == "")
            {
                strVillage = "Xgh";
            }
            //conditions = "mst5Village.ClusterCode in('" + strVillage + "') ";

            ////objComman.BindDLL("mst5Village", "VillageCode,VillageName ", conditions, "VillageName", "", ddlVilage, "VillageName", "VillageCode", "Select");
            //strQry = "   select Villagecode  from MstUser   where UserName='" + ddlUser.SelectedValue + "' ";

            //DataTable dtUserVillage = objMain.LoadData(strQry);

            //string strVillage = dtUserVillage.Rows[0]["Villagecode"].ToString();

            conditions = "mst5Village.ClusterCode in('" + strVillage + "') ";

            strQry = "";
            strQry = "select VillageCode,VillageName  from mst5Village where mst5Village.ClusterCode in('" + strVillage + "')  and len(mst5Village.ClusterCode)>2    ";
            strQry += " Union select mstActivityVillage.VillageCode,mstActivityVillage.VillageName  from mstActivityVillage    inner join mst5Village on mst5Village.VillageCode=mstActivityVillage.Villagecode where UserID='" + ddlUser.SelectedValue + "' and mst5Village.Fyear='" + Session["FinYear"].ToString() + "'   ";

            strQry += " Union ";
            strQry += "  select mst5Village.VillageCode,VillageName  from mst5Village  ";
            strQry += " inner join tblActivityUpdate_Village on tblActivityUpdate_Village.VillageCode=mst5Village.VillageCode  ";
            strQry += "  where mst5Village.ClusterCode in('" + Session["Cluseter"].ToString() + "' )   and UserID='" + ddlUser.SelectedValue + "'  order by VillageName  ";
            DataTable dtVillage = objMain.LoadData(strQry);
            //objComman.BindDLLMasterTable("MstUser", "UserName as UserId,[FristName]+' ('+ UserName +')' as [UserName] ", dtUser, conditions, "", "", ddlUser, "UserName", "UserId", "Select");

            objComman.BindDLLMasterTable("mst5Village", "VillageCode,VillageName ", dtVillage, "", "VillageName", "", ddlVilage, "VillageName", "VillageCode", "Select");

            //DataTable dt = objMain.GetActivityUserWiseMaxDate(ddlUser.SelectedValue);
            //if (dt.Rows.Count > 0)
            //{
            //    if (Convert.ToString(dt.Rows[0]["ActivityDate"].ToString()) != "")
            //    {
            //        CalendarExtenderTourdate.StartDate = Convert.ToDateTime(dt.Rows[0]["ActivityDate"].ToString()).AddDays(1);
            //    }
            //}
        }

    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {

        if (ddlUser.SelectedIndex <= 0)
        {
            ModalPopupExtender.Hide();
            this.ModalPopupExtender1.Hide();
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select User')</script>", false);

        }
        if (ddlVilage.SelectedIndex <= 0)
        {
            ModalPopupExtender.Hide();
            this.ModalPopupExtender1.Hide();
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select Village')</script>", false);

        }
        ClearData();
        pnlMain.Enabled = true;
    }
    protected void btnOther1_Click(object sender, EventArgs e)
    {


        foreach (ListItem item in chk_c2.Items)
        {

            item.Selected = false;

        }
        txtoC111.Text = "";
        txtAtt1.Text = "";
        txtoC1.Text = "";
        txtOtherCC1.Text = "";
        rblc1.Checked = false;
        rblc2.Checked = false;



    }
    public void ClearData()
    {

        foreach (ListItem item in CBL_bookformat.Items)
        {
            item.Selected = false;
        }
        foreach (ListItem item in CBL_bookformatNew.Items)
        {
            item.Selected = false;
        }
        foreach (ListItem item in CBL_bookformatNew1.Items)
        {
            item.Selected = false;
        }
        foreach (ListItem item in chk_c2.Items)
        {

            item.Selected = false;
        }


        txtoC111.Text = "";
        txtAtt1.Text = "";
        txtoC1.Text = "";
        txtOtherCC1.Text = "";
        rblc1.Checked = false;
        rblc2.Checked = false;
        rblTbhold.Checked = false;
        rblFcHold.Checked = false;
        txt_pbname.Text = "";
        txt_pbnameNew.Text = "";
        txt_pbnameNew1.Text = "";
        txtmOther.Text = "";
        txtmainother.Text = "";
        txt_bookformatOther.Text = "";
        txt_bookformatOther1.Text = "";
        chkmcommmeting.Checked = false;
        rdEnrollMent.Checked = false;
        rdEnrollment1.Checked = false;
        rdEnrollment2.Checked = false;
        rdRetention.Checked = false;
        rdRetantion1.Checked = false;
        rdRetantion2.Checked = false;

        chkcommmetingTB.Checked = false;

        chkcommmetingFC.Checked = false;

        txtV1illager.Text = "";
        txt_bookformatOther.Text = "";



        foreach (ListItem item in CBL_Muhula.Items)
        {
            item.Selected = false;
        }
        foreach (ListItem item in CBL_MuhulaNew.Items)
        {
            item.Selected = false;
        }
        foreach (ListItem item in CBL_MuhulaNew1.Items)
        {
            item.Selected = false;
        }

        txtMuhala.Text = "";
        txtMuhalaNew.Text = "";
        txtMuhalaNew1.Text = "";
        chkmuhala.Checked = false;


        rblmuhulaTb.Checked = false;

        rblmuhulaFC.Checked = false;

        txtVillager2.Text = "";
        txtmOther.Text = "";
        txtmOther1.Text = "";


        foreach (ListItem item in chk_othercom.Items)
        {

            item.Selected = false;

        }
        foreach (ListItem item in chk_othercom_New.Items)
        {
            item.Selected = false;
        }
        foreach (ListItem item in chk_othercom_New1.Items)
        {
            item.Selected = false;
        }
        txtOtherComminuty.Text = "";
        txtOtherComminutyNew.Text = "";
        txtOtherComminutyNew1.Text = "";
        chkothercomm.Checked = false;

        rblothercommTb.Checked = false;

        rblothercommfc.Checked = false;

        txtvillager3.Text = "";
        txtOtherComm.Text = "";



        foreach (ListItem item in chk_comm.Items)
        {

            item.Selected = false;

        }


        txtOtherConnect.Text = "";
        chkcoom.Checked = false;

        rblcommtb.Checked = false;

        rblCommFC.Checked = false;

        txtOtherCon.Text = "";



        foreach (ListItem item in chk_chkconn.Items)
        {

            item.Selected = false;

        }


        txt_conn.Text = "";
        chkcoom.Checked = false;

        rblcommtb.Checked = false;

        rblCommFC.Checked = false;


        txt_con_other.Text = "";





        foreach (ListItem item in chk_chkconn.Items)
        {

            item.Selected = false;

        }
        foreach (ListItem item in chk_Suport.Items)
        {

            item.Selected = false;

        }

        txtSuport.Text = "";
        chkSupoort.Checked = false;

        //   rblSupporttb.Checked = false;

        rblsupportfc.Checked = false;


        txtOtherSupport.Text = "";



        rblothertb.Checked = false;

        rblotherfc.Checked = false;

        chkother.Checked = false;

        txt_con_other.Enabled = false;


        txtOtherCon.Enabled = false;
        txtOtherComm1.Text = "";
        txtOtherComm.Enabled = false;
        txtOtherComm1.Enabled = false;
        txtmOther.Enabled = false;
        txtmOther1.Enabled = false;
        txt_bookformatOther.Enabled = false;
        txt_bookformatOther1.Enabled = false;
        txtOtherSupport.Enabled = false;
        TxtGSS_FeMale.Text = "";
        TxtGSS_Male.Text = "";
        TxtMM_FeMale.Text = "";
        TxtMM_Male.Text = "";
        TxtCm1_FeMale.Text = "";
        TxtCm1_Male.Text = "";
        tc1.Text = "";

    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        if (ViewState["GUID"].ToString().Length > 5)
        {
            int res1 = 0;
            //if (ddlRemark.SelectedIndex > 0)
            //{
            res1 = objMain.DeleteD2dDataAcctivtiyVillage(ViewState["GUID"].ToString());
            if (res1 > 0)
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Delete Sucessfully')</script>", false);
            }
            //}
            //else
            //{
            //    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select Remark')</script>", false);

            //}
        }
    }
    protected void btnSerach_Click(object sender, EventArgs e)
    {
        ClearData();

        if (ddlUser.SelectedIndex <= 0)
        {
            ModalPopupExtender.Hide();
            this.ModalPopupExtender1.Hide();
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select User')</script>", false);
            return;
        }
        if (ddlVilage.SelectedIndex <= 0)
        {
            ModalPopupExtender.Hide();
            this.ModalPopupExtender1.Hide();
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select Village')</script>", false);
            return;
        }

        if (this.ddlRemark.SelectedIndex > 0)
        {
            this.pnlMain.Enabled = true;
        }
        else
        {
            this.pnlMain.Enabled = false;
        }
        string Dateof = txtDate.Text;



        rblTbhold.Enabled = true;


        string[] b = Dateof.Split('/');

        string FcDate = b[2] + '-' + b[1] + '-' + b[0];
        string strQry = "";

        if (Session["user_level"].ToString() == "19")
        {
            strQry = "   select *  from tblActivityUpdate_Village   where UserID='" + ddlUser.SelectedValue + "' and ApproveStatus='FC' and VillageCode='" + ddlVilage.SelectedValue + "' and UserEntry=3 and ActivityDate= '" + Convert.ToDateTime(FcDate).ToString("yyyy-MM-dd") + "' ";


        }
        if (Session["user_level"].ToString() == "39" || Session["user_level"].ToString() == "30")
        {
            strQry = "   select *  from tblActivityUpdate_Village   where UserID='" + ddlUser.SelectedValue + "' and ApproveStatus='B' and VillageCode='" + ddlVilage.SelectedValue + "' and UserEntry=3 and ActivityDate= '" + Convert.ToDateTime(FcDate).ToString("yyyy-MM-dd") + "' ";


        }

        DataTable dtVillageActivtiy = objMain.LoadData(strQry);

        if (dtVillageActivtiy.Rows.Count > 0)
        {
            if (dtVillageActivtiy.Rows[0]["ApproveStatus"].ToString() == "B" || dtVillageActivtiy.Rows[0]["ApproveStatus"].ToString() == "FC" || dtVillageActivtiy.Rows[0]["ApproveStatus"].ToString() == "I")
            {
                if (Session["user_level"].ToString() == "19" && dtVillageActivtiy.Rows[0]["ApproveStatus"].ToString() == "FC")
                {
                    btnsave.Visible = true;
                }
                else
                {
                    btnsave.Visible = false;
                }
                if (Session["user_level"].ToString() == "39" || Session["user_level"].ToString() == "30")
                {
                    if (dtVillageActivtiy.Rows[0]["ApproveStatus"].ToString() == "B")
                    {
                        btnsave.Visible = true;
                    }
                    else
                    {
                        btnsave.Visible = false;
                    }
                }
            }

            imgGSS.Visible = false;
            ImgMM.Visible = false;
            imgComm1.Visible = false;
            imgComm2.Visible = false;
            lblGG.Text = "";
            lblMM.Text = "";
            lblCom.Text = "";
            lblCom1.Text = "";
            if (dtVillageActivtiy.Rows[0]["Remarks"].ToString() != "0")
            {
                ddlRemark.SelectedValue = dtVillageActivtiy.Rows[0]["Remarks"].ToString();
            }
            if (dtVillageActivtiy.Rows[0]["PhotoGSS"].ToString().Length > 0)
            {
                lblGG.Text = dtVillageActivtiy.Rows[0]["PhotoGSS"].ToString();

                //imgMKS.ImageUrl = ResolveUrl("~/TabletImage/" + imagename);

                imgGSS.Visible = true;

            }
            if (dtVillageActivtiy.Rows[0]["PhotoMM"].ToString().Length > 0)
            {
                lblMM.Text = dtVillageActivtiy.Rows[0]["PhotoMM"].ToString();
                //imgMKS.ImageUrl = ResolveUrl("~/TabletImage/" + imagename);
                ImgMM.Visible = true;
            }
            if (dtVillageActivtiy.Rows[0]["PhotoOtherComm"].ToString().Length > 0)
            {
                lblCom.Text = dtVillageActivtiy.Rows[0]["PhotoOtherComm"].ToString();
                // imgMKS.ImageUrl = ResolveUrl("~/TabletImage/" + imagename);
                imgComm1.Visible = true;
            }
            if (dtVillageActivtiy.Rows[0]["PhotoOtherComm2"].ToString().Length > 0)
            {
                lblCom1.Text = dtVillageActivtiy.Rows[0]["PhotoOtherComm2"].ToString();
                // imgMKS.ImageUrl = ResolveUrl("~/TabletImage/" + imagename);
                imgComm2.Visible = true;

            }

            if (ddlRemark.SelectedIndex > 0)
            {
                pnlMain.Enabled = true;
            }
            //else
            //{
            //    pnlMain.Enabled = false;
            //}
            #region LoadDate
            ViewState["GUID"] = dtVillageActivtiy.Rows[0]["GUID_Village"].ToString();

            if (dtVillageActivtiy.Rows[0]["GSS_Agenda"].ToString() == "1")
            {
                chkcommmetingTB.Checked = true;
            }
            if (dtVillageActivtiy.Rows[0]["GSS_Agenda"].ToString() == "2")
            {
                chkcommmetingFC.Checked = true;
            }
            if (dtVillageActivtiy.Rows[0]["GSSEnrollHault"].ToString() == "1")
            {
                rdEnrollMent.Checked = true;
            }
            else if (dtVillageActivtiy.Rows[0]["GSSEnrollHault"].ToString() == "2")
            {
                rdRetention.Checked = true;
            }
            if (dtVillageActivtiy.Rows[0]["MMEnrollHault"].ToString() == "1")
            {
                rdEnrollment1.Checked = true;
            }
            else if (dtVillageActivtiy.Rows[0]["MMEnrollHault"].ToString() == "2")
            {
                rdRetantion1.Checked = true;
            }
            if (dtVillageActivtiy.Rows[0]["OtherEnrollHault"].ToString() == "1")
            {
                rdEnrollment2.Checked = true;
            }
            else if (dtVillageActivtiy.Rows[0]["OtherEnrollHault"].ToString() == "2")
            {
                rdRetantion2.Checked = true;
            }

            string cmeeting = dtVillageActivtiy.Rows[0]["GSS_Agenda"].ToString();

            string[] meeting = cmeeting.Split(',');
            string TextMeeeting = "";
            foreach (string s in meeting)
            {
                foreach (ListItem item in CBL_bookformat.Items)
                {
                    if (item.Value == s)
                    {
                        item.Selected = true;
                        TextMeeeting += item.Text + ",";
                    }
                }
            }
            if (TextMeeeting.Length > 0)
            {
                TextMeeeting = TextMeeeting.Substring(0, TextMeeeting.LastIndexOf(","));
                txt_pbname.Text = TextMeeeting;
                chkmcommmeting.Checked = true;
            }

            string cmeeting1 = dtVillageActivtiy.Rows[0]["GSSChat"].ToString();

            string[] meeting1 = cmeeting1.Split(',');
            string TextMeeeting1 = "";
            foreach (string s in meeting1)
            {
                foreach (ListItem item in CBL_bookformatNew.Items)
                {
                    if (item.Value == s)
                    {
                        item.Selected = true;
                        TextMeeeting1 += item.Text + ",";
                    }
                }
            }
            if (TextMeeeting1.Length > 0)
            {
                TextMeeeting1 = TextMeeeting1.Substring(0, TextMeeeting1.LastIndexOf(","));
                txt_pbnameNew.Text = TextMeeeting1;
                chkmcommmeting.Checked = true;
            }
            string cmeeting2 = dtVillageActivtiy.Rows[0]["GSSImportantperson"].ToString();

            string[] meeting2 = cmeeting2.Split(',');
            string TextMeeeting2 = "";
            foreach (string s in meeting2)
            {
                foreach (ListItem item in CBL_bookformatNew1.Items)
                {
                    if (item.Value == s)
                    {
                        item.Selected = true;
                        TextMeeeting2 += item.Text + ",";
                    }
                }
            }
            if (TextMeeeting2.Length > 0)
            {
                TextMeeeting2 = TextMeeeting2.Substring(0, TextMeeeting2.LastIndexOf(","));
                txt_pbnameNew1.Text = TextMeeeting2;
                chkmcommmeting.Checked = true;
            }
            if (dtVillageActivtiy.Rows[0]["GSS_TB"].ToString() == "1")
            {
                chkcommmetingTB.Checked = true;
            }
            if (dtVillageActivtiy.Rows[0]["GSS_FC"].ToString() == "1")
            {
                chkcommmetingFC.Checked = true;
            }
            if (dtVillageActivtiy.Rows[0]["GSS_Attended"].ToString() == "0")
            {
                txtV1illager.Text = "";
            }
            else
            {
                txtV1illager.Text = dtVillageActivtiy.Rows[0]["GSS_Attended"].ToString();
            }
            if (dtVillageActivtiy.Rows[0]["GSSMale"].ToString() == "0")
            {
                TxtGSS_Male.Text = "";
            }
            else
            {
                TxtGSS_Male.Text = dtVillageActivtiy.Rows[0]["GSSMale"].ToString();
            }
            if (dtVillageActivtiy.Rows[0]["GSSFemale"].ToString() == "0")
            {
                TxtGSS_FeMale.Text = "";
            }
            else
            {
                TxtGSS_FeMale.Text = dtVillageActivtiy.Rows[0]["GSSFemale"].ToString();
            }
            if (dtVillageActivtiy.Rows[0]["MMFemale"].ToString() == "0")
            {
                TxtMM_FeMale.Text = "";
            }
            else
            {
                TxtMM_FeMale.Text = dtVillageActivtiy.Rows[0]["MMFemale"].ToString();
            }
            if (dtVillageActivtiy.Rows[0]["MMMale"].ToString() == "0")
            {
                TxtMM_Male.Text = "";
            }
            else
            {
                TxtMM_Male.Text = dtVillageActivtiy.Rows[0]["MMMale"].ToString();
            }
            if (dtVillageActivtiy.Rows[0]["OtherFemale"].ToString() == "0")
            {
                TxtCm1_FeMale.Text = "";
            }
            else
            {
                TxtCm1_FeMale.Text = dtVillageActivtiy.Rows[0]["OtherFemale"].ToString();
            }
            if (dtVillageActivtiy.Rows[0]["OtherMale"].ToString() == "0")
            {
                TxtCm1_Male.Text = "";
            }
            else
            {
                TxtCm1_Male.Text = dtVillageActivtiy.Rows[0]["OtherMale"].ToString();
            }
            txt_bookformatOther.Text = dtVillageActivtiy.Rows[0]["GSS_AgendaOther"].ToString();
            txt_bookformatOther1.Text = dtVillageActivtiy.Rows[0]["otherGSSChat"].ToString();
            if (txt_bookformatOther.Text.Length > 1)
            {
                txt_bookformatOther.Enabled = true;
            }
            else
            {
                txt_bookformatOther.Enabled = false;
            }

            if (dtVillageActivtiy.Rows[0]["TBHandholding"].ToString() == "1")
            {
                rblTbhold.Checked = true;
            }
            else
            {
                rblTbhold.Checked = false;
            }





            string MM_Agenda = dtVillageActivtiy.Rows[0]["MM_Agenda"].ToString();
            string[] MMAgenda = MM_Agenda.Split(',');
            string MM_AgendaMeeting = "";
            foreach (string s in MMAgenda)
            {
                foreach (ListItem item in CBL_Muhula.Items)
                {
                    if (item.Value == s)
                    {
                        item.Selected = true;
                        MM_AgendaMeeting += item.Text + ",";
                    }
                }
            }
            if (MM_AgendaMeeting.Length > 0)
            {
                MM_AgendaMeeting = MM_AgendaMeeting.Substring(0, MM_AgendaMeeting.LastIndexOf(","));
                txtMuhala.Text = MM_AgendaMeeting;
                chkmuhala.Checked = true;
            }
            string MM_Agenda1 = dtVillageActivtiy.Rows[0]["MMChat"].ToString();
            string[] MMAgenda1 = MM_Agenda1.Split(',');
            string MM_AgendaMeeting1 = "";
            foreach (string s in MMAgenda1)
            {
                foreach (ListItem item in CBL_MuhulaNew.Items)
                {
                    if (item.Value == s)
                    {
                        item.Selected = true;
                        MM_AgendaMeeting1 += item.Text + ",";
                    }
                }
            }
            if (MM_AgendaMeeting1.Length > 0)
            {
                MM_AgendaMeeting1 = MM_AgendaMeeting1.Substring(0, MM_AgendaMeeting1.LastIndexOf(","));
                txtMuhalaNew.Text = MM_AgendaMeeting1;
                chkmuhala.Checked = true;
            }
            string MM_Agenda2 = dtVillageActivtiy.Rows[0]["MMImportantperson"].ToString();
            string[] MMAgenda2 = MM_Agenda2.Split(',');
            string MM_AgendaMeeting2 = "";
            foreach (string s in MMAgenda2)
            {
                foreach (ListItem item in CBL_MuhulaNew1.Items)
                {
                    if (item.Value == s)
                    {
                        item.Selected = true;
                        MM_AgendaMeeting2 += item.Text + ",";
                    }
                }
            }
            if (MM_AgendaMeeting2.Length > 0)
            {
                MM_AgendaMeeting2 = MM_AgendaMeeting2.Substring(0, MM_AgendaMeeting2.LastIndexOf(","));
                txtMuhalaNew1.Text = MM_AgendaMeeting2;
                chkmuhala.Checked = true;
            }
            if (dtVillageActivtiy.Rows[0]["MM_TB"].ToString() == "1")
            {
                rblmuhulaTb.Checked = true;
            }
            if (dtVillageActivtiy.Rows[0]["MM_FC"].ToString() == "1")
            {
                rblmuhulaFC.Checked = true;
            }
            if (dtVillageActivtiy.Rows[0]["MM_Attended"].ToString() == "0")
            {
                txtVillager2.Text = "";
            }
            else
            {
                txtVillager2.Text = dtVillageActivtiy.Rows[0]["MM_Attended"].ToString();
            }
            txtmOther.Text = dtVillageActivtiy.Rows[0]["MM_AgendaOther"].ToString();
            txtmOther1.Text = dtVillageActivtiy.Rows[0]["othermmchat"].ToString();
            if (txtmOther.Text.Length > 1)
            {
                txtmOther.Enabled = true;
            }
            else
            {
                txtmOther.Enabled = false;

            }


            string Com_Agenda = dtVillageActivtiy.Rows[0]["Com_Agenda"].ToString();

            string[] ComAgenda = Com_Agenda.Split(',');
            string Com_Agendamm = "";
            foreach (string s in ComAgenda)
            {
                foreach (ListItem item in chk_othercom.Items)
                {
                    if (item.Value == s)
                    {
                        item.Selected = true;
                        Com_Agendamm += item.Text + ",";
                    }
                }
            }
            if (Com_Agendamm.Length > 0)
            {
                Com_Agendamm = Com_Agendamm.Substring(0, Com_Agendamm.LastIndexOf(","));
                txtOtherComminuty.Text = Com_Agendamm;
                chkothercomm.Checked = true;
            }
            string Com_Agenda1 = dtVillageActivtiy.Rows[0]["GSSChat"].ToString();

            string[] ComAgenda1 = Com_Agenda1.Split(',');
            string Com_Agendamm1 = "";
            foreach (string s in ComAgenda1)
            {
                foreach (ListItem item in chk_othercom_New.Items)
                {
                    if (item.Value == s)
                    {
                        item.Selected = true;
                        Com_Agendamm1 += item.Text + ",";
                    }
                }
            }
            if (Com_Agendamm1.Length > 0)
            {
                Com_Agendamm1 = Com_Agendamm1.Substring(0, Com_Agendamm1.LastIndexOf(","));
                txtOtherComminutyNew.Text = Com_Agendamm1;
                chkothercomm.Checked = true;
            }
            string Com_Agenda3 = dtVillageActivtiy.Rows[0]["GSSImportantperson"].ToString();

            string[] ComAgenda3 = Com_Agenda3.Split(',');
            string Com_Agendamm3 = "";
            foreach (string s in ComAgenda3)
            {
                foreach (ListItem item in chk_othercom_New1.Items)
                {
                    if (item.Value == s)
                    {
                        item.Selected = true;
                        Com_Agendamm3 += item.Text + ",";
                    }
                }
            }
            if (Com_Agendamm3.Length > 0)
            {
                Com_Agendamm3 = Com_Agendamm3.Substring(0, Com_Agendamm3.LastIndexOf(","));
                txtOtherComminutyNew1.Text = Com_Agendamm3;
                chkothercomm.Checked = true;
            }
            if (dtVillageActivtiy.Rows[0]["Com_TB"].ToString() == "1")
            {
                rblothercommTb.Checked = true;
            }
            if (dtVillageActivtiy.Rows[0]["Com_FC"].ToString() == "1")
            {
                rblothercommfc.Checked = true;
            }
            if (dtVillageActivtiy.Rows[0]["Com_Attended"].ToString() == "0")
            {
                txtvillager3.Text = "";
            }
            else
            {
                txtvillager3.Text = dtVillageActivtiy.Rows[0]["Com_Attended"].ToString();
            }
            txtOtherComm.Text = dtVillageActivtiy.Rows[0]["Com_AgendaOther"].ToString();
            txtOtherComm1.Text = dtVillageActivtiy.Rows[0]["otherGSSChat"].ToString();

            tc1.Text = dtVillageActivtiy.Rows[0]["Any_Other"].ToString();
            if (txtOtherComm.Text.Length > 1)
            {
                txtOtherComm.Enabled = true;
            }
            else
            {
                txtOtherComm.Enabled = false;

            }

            //---------------------com2


            string Com_Agenda2 = dtVillageActivtiy.Rows[0]["Com_Agenda2"].ToString();

            string[] ComAgenda2 = Com_Agenda2.Split(',');
            string Com_Agendamm2 = "";
            foreach (string s in ComAgenda2)
            {
                foreach (ListItem item in chk_c2.Items)
                {
                    if (item.Value == s)
                    {
                        item.Selected = true;
                        Com_Agendamm2 += item.Text + ",";
                    }
                }
            }
            if (Com_Agendamm2.Length > 0)
            {
                Com_Agendamm2 = Com_Agendamm2.Substring(0, Com_Agendamm2.LastIndexOf(","));
                txtOtherCC1.Text = Com_Agendamm2;

            }
            if (dtVillageActivtiy.Rows[0]["Com_TB2"].ToString() == "1")
            {
                rblc1.Checked = true;
            }
            if (dtVillageActivtiy.Rows[0]["Com_FC2"].ToString() == "1")
            {
                rblc2.Checked = true;
            }
            if (dtVillageActivtiy.Rows[0]["Com_Attended2"].ToString() == "0")
            {
                txtAtt1.Text = "";
            }
            else
            {
                txtAtt1.Text = dtVillageActivtiy.Rows[0]["Com_Attended2"].ToString();
            }
            txtoC1.Text = dtVillageActivtiy.Rows[0]["Any_Other2"].ToString();

            txtoC111.Text = dtVillageActivtiy.Rows[0]["Com_AgendaOther2"].ToString();
            if (txtoC111.Text.Length > 1)
            {
                txtoC111.Enabled = true;
            }
            else
            {
                txtoC111.Enabled = false;

            }

            //--------------
            string ComContact_Op = dtVillageActivtiy.Rows[0]["ComContact_Agenda"].ToString();

            string[] ComContactOp = ComContact_Op.Split(',');
            string ComContact_Opmm = "";
            foreach (string s in ComContactOp)
            {
                foreach (ListItem item in chk_comm.Items)
                {
                    if (item.Value == s)
                    {
                        item.Selected = true;
                        ComContact_Opmm += item.Text + ",";
                    }
                }
            }
            if (ComContact_Opmm.Length > 0)
            {
                ComContact_Opmm = ComContact_Opmm.Substring(0, ComContact_Opmm.LastIndexOf(","));
                txtOtherConnect.Text = ComContact_Opmm;
                chkcoom.Checked = true;
            }
            if (dtVillageActivtiy.Rows[0]["ComContact_TB"].ToString() == "1")
            {
                rblcommtb.Checked = true;
            }
            if (dtVillageActivtiy.Rows[0]["ComContact_FC"].ToString() == "1")
            {
                rblCommFC.Checked = true;
            }

            txtOtherCon.Text = dtVillageActivtiy.Rows[0]["ConContact_AgendaOther"].ToString();
            if (txtOtherCon.Text.Length > 1)
            {
                txtOtherCon.Enabled = true;
            }
            else
            {
                txtOtherCon.Enabled = false;


            }
            string ComContact = dtVillageActivtiy.Rows[0]["ComContact_Op"].ToString();

            string[] ComContactOp1 = ComContact.Split(',');
            string ComContact_Opmm1 = "";
            foreach (string s in ComContactOp1)
            {
                foreach (ListItem item in chk_chkconn.Items)
                {
                    if (item.Value == s)
                    {
                        item.Selected = true;
                        ComContact_Opmm1 += item.Text + ",";
                    }
                }
            }
            if (ComContact_Opmm1.Length > 0)
            {
                ComContact_Opmm1 = ComContact_Opmm1.Substring(0, ComContact_Opmm1.LastIndexOf(","));
                txt_conn.Text = ComContact_Opmm1;
                chkcoom.Checked = true;
            }
            if (dtVillageActivtiy.Rows[0]["ComContact_TB"].ToString() == "1")
            {
                rblcommtb.Checked = true;
            }
            if (dtVillageActivtiy.Rows[0]["ComContact_FC"].ToString() == "1")
            {
                rblCommFC.Checked = true;
            }

            txt_con_other.Text = dtVillageActivtiy.Rows[0]["ComContact_Op_Other"].ToString();

            if (txt_con_other.Text.Length > 1)
            {
                txt_con_other.Enabled = true;
            }
            else
            {
                txt_con_other.Enabled = false;


            }


            string Support_Op = dtVillageActivtiy.Rows[0]["Support_Op"].ToString();

            string[] SupportOp = Support_Op.Split(',');
            string Support_Op1 = "";
            foreach (string s in SupportOp)
            {
                foreach (ListItem item in chk_Suport.Items)
                {
                    if (item.Value == s)
                    {
                        item.Selected = true;
                        Support_Op1 += item.Text + ",";
                    }
                }
            }
            if (Support_Op1.Length > 0)
            {
                Support_Op1 = Support_Op1.Substring(0, Support_Op1.LastIndexOf(","));
                txtSuport.Text = Support_Op1;
                chkSupoort.Checked = true;
            }
            if (dtVillageActivtiy.Rows[0]["Support_FC"].ToString() == "1")
            {
                rblsupportfc.Checked = true;

            }
            //if (dtVillageActivtiy.Rows[0]["Support_TB"].ToString() == "1")
            //{
            //    rblSupporttb.Checked = true;
            //}

            txtOtherSupport.Text = dtVillageActivtiy.Rows[0]["Support_Op_Other"].ToString();
            if (txtOtherSupport.Text.Length > 1)
            {
                txtOtherSupport.Enabled = true;
            }
            else
            {
                txtOtherSupport.Enabled = false;
            }

            if (dtVillageActivtiy.Rows[0]["Others_FC"].ToString() == "1")
            {
                rblotherfc.Checked = true;

            }
            if (dtVillageActivtiy.Rows[0]["Others_TB"].ToString() == "1")
            {
                rblothertb.Checked = true;
            }
            if (dtVillageActivtiy.Rows[0]["Others_Desc"].ToString().Length > 0)
            {
                chkother.Checked = true;
            }
            txtmainother.Text = dtVillageActivtiy.Rows[0]["Others_Desc"].ToString();

            #endregion
        }
        else
        {
            imgGSS.Visible = false;
            ImgMM.Visible = false;
            imgComm1.Visible = false;
            imgComm2.Visible = false;
            lblGG.Text = "";
            lblMM.Text = "";
            lblCom.Text = "";
            lblCom1.Text = "";
            txt_con_other.Enabled = false;

            btnsave.Visible = true;
            txtOtherCon.Enabled = false;

            txtOtherComm.Enabled = false;
            txtmOther.Enabled = false;
            txt_bookformatOther.Enabled = false;
            txtOtherSupport.Enabled = false;
            pnlMain.Enabled = true;
            ViewState["GUID"] = "";
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        SaveData();
    }
    protected void ddlVilage_SelectedIndexChanged(object sender, EventArgs e)
    {
        //string strQry = "";
        //if (ddlVilage.SelectedIndex > 0)
        //{
        //    strQry = " select [mst5village].EGVillagecode + '-' +RIGHT('0000' +  convert(varchar,serial), 4) as UniqueId,UniqueCode,RIGHT('0000' +  convert(varchar,serial), 4) as  UniqueIdNew,ActivityStatus as Status,HHNo,ChildName,FathersName from  [tblDTD] inner join mst5Village on mst5village.villagecode=tblDTD.villagecode or tblDTD.villagecode=mst5village.OldUniqueCode    or tblDTD.villagecode=mst5village.RefVillageCode   where  tblDTD.Status='1' and mst5village.Villagecode= '" + ddlVilage.SelectedValue + "'    and " + DateTime.Today.Year + " - (YEAR(SurvayDate)-isnull(AgeAson,0))>=6  and (" + DateTime.Today.Year + " - (YEAR(SurvayDate)-isnull(AgeAson,0))<=14  ) and EduationStatus in(2,3,99)   and EnrollStatus=1 and DeleteFlag<>2";

        //    DataTable dtD2d = objMain.LoadData(strQry);
        //    if (dtD2d.Rows.Count > 0)
        //    {
        //        Gv_Display.DataSource = dtD2d;

        //        Gv_Display.DataBind();
        //        Session["D2dBind"] = dtD2d;
        //    }
        //    else
        //    {
        //        Gv_Display.DataSource = null;

        //        Gv_Display.DataBind();
        //    }
        //}
        // ddlRemark.SelectedIndex = 0;
    }
    protected void Gv_DisplayNew_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblActivityStatus1 = ((Label)e.Row.FindControl("lblActivityStatus1"));
            Button btnEditEnroll = ((Button)e.Row.FindControl("btnEditUndo"));
            //if (lblActivityStatus1.Text == "4" || lblActivityStatus1.Text == "1")
            //{
            //    btnEditEnroll.Visible = false;
            //}
            //else
            //{
            //    btnEditEnroll.Visible = true;
            //}

        }
    }
    protected void Gv_Display_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DropDownList ddlStatus = ((DropDownList)e.Row.FindControl("ddlStatus"));
            ImageButton btnEditEnroll = ((ImageButton)e.Row.FindControl("btnEditEnroll"));
            Label lblActivityDate = ((Label)e.Row.FindControl("lblActivityDate"));
            DataTable dt = Session["dtstn"] as DataTable;


            ddlStatus.DataTextField = "Description";
            ddlStatus.DataValueField = "LookupCode";

            ddlStatus.DataSource = dt;
            ddlStatus.DataBind();

            Label lbStatus = ((Label)e.Row.FindControl("lbStatus"));
            ddlStatus.SelectedValue = lbStatus.Text;
            Label lblTBFC = ((Label)e.Row.FindControl("lblTBFC"));

            RadioButtonList rblTBFC = ((RadioButtonList)e.Row.FindControl("rblTBFC"));
            if (lblTBFC.Text == "1")
            {
                rblTBFC.SelectedValue = "1";
            }
            if (lblTBFC.Text == "2")
            {
                rblTBFC.SelectedValue = "2";
            }


            if (lbStatus.Text == "2" || lbStatus.Text == "3")
            {
                ddlStatus.Enabled = false;
                btnEditEnroll.Visible = true;
            }
            else
            {
                ddlStatus.Enabled = true;
                btnEditEnroll.Visible = false;
            }
            if (lbStatus.Text == "1")
            {
                if (lblActivityDate.Text.Length > 5)
                {
                    if (Convert.ToDateTime(lblActivityDate.Text) == Convert.ToDateTime(DateTime.Today))
                    {
                        ddlStatus.Enabled = false;
                    }
                    else
                    {
                        ddlStatus.Enabled = true;
                    }
                }
                else
                {
                    ddlStatus.Enabled = false;
                }
            }

        }
    }
    protected void ddlFo_SelectedIndexChanged(object sender, EventArgs e)
    {
        ModalPopupExtender.Show();
        if (ddlFo.SelectedIndex > 0)
        {
            if (Convert.ToInt32(ddlFo.SelectedValue) == 1)
            {
                divF4.Visible = true;
                divF5.Visible = false;
                divF6.Visible = false;
                divF7.Visible = false;
                txtOtherVillage.Text = "";
                txtOtherSchool.Text = "";
                ddlOtherVillage.SelectedIndex = 0;

            }
            else
            {
                divF4.Visible = false;
                divF5.Visible = false;
                divF6.Visible = false;
                divF7.Visible = false;
            }
            MpexdrFollowup.Show();
        }
        else
        {

            divF4.Visible = false;
            divF5.Visible = false;
            divF6.Visible = false;
            divF7.Visible = false;

            MpexdrFollowup.Show();
        }
    }

    protected void ddlFOtherVillage_SelectedIndexChanged(object sender, EventArgs e)
    {
        ModalPopupExtender.Show();
        if (ddlOtherVillage.SelectedIndex > 0)
        {
            if (Convert.ToInt32(ddlOtherVillage.SelectedValue) == 1)
            {

                divF7.Visible = true;
                divF5.Visible = false;
                divF6.Visible = false;
                FillSchool();
            }
            else if (Convert.ToInt32(ddlOtherVillage.SelectedValue) == 2)
            {
                divF7.Visible = false;
                divF5.Visible = true;
                divF6.Visible = true;
            }
            else
            {
                divF7.Visible = false;
                divF5.Visible = false;
                divF6.Visible = false;
            }

            MpexdrFollowup.Show();
        }
        else
        {
            divF7.Visible = false;
            divF5.Visible = false;
            divF6.Visible = false;
            MpexdrFollowup.Show();
        }
    }




    protected void ddlEFOtherVillage_SelectedIndexChanged(object sender, EventArgs e)
    {
        ModalPopupExtender.Show();
        if (ddlEotherVillage.SelectedIndex > 0)
        {
            DivE2.Visible = false;
            DivE3.Visible = false;
            DivE4.Visible = false;
            if (Convert.ToInt32(ddlEotherVillage.SelectedValue) == 1)
            {

                DivE2.Visible = true;

                FillSchoolE();
            }
            else if (Convert.ToInt32(ddlEotherVillage.SelectedValue) == 2)
            {
                DivE3.Visible = true;
                DivE4.Visible = true;
            }
            else
            {
                DivE2.Visible = false;
                DivE3.Visible = false;
                DivE4.Visible = false;
            }

            MpexdrFollowup.Show();
        }
        else
        {
            DivE2.Visible = false;
            DivE3.Visible = false;
            DivE4.Visible = false;
            MpexdrFollowup.Show();
        }
    }
    public void FillSchoolE()
    {
        conditions = "";
        conditions = "VillageCode ='" + ddlVilage.SelectedValue + "' and FYear ='" + Session["FinYear"].ToString() + "' ";


        objComman.BindDLL("mstSchool", "SchoolCode,Name", conditions, "Name", "asc", ddlESchool, "Name", "SchoolCode", "Select");



    }
    public void FillSchool()
    {
        conditions = "";
        conditions = "VillageCode ='" + ddlVilage.SelectedValue + "' and FYear ='" + Session["FinYear"].ToString() + "' ";


        objComman.BindDLL("mstSchool", "SchoolCode,Name", conditions, "Name", "asc", ddlSchool, "Name", "SchoolCode", "Select");



    }

    protected void ddlIReasons_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlIReasons.SelectedIndex > 0)
        {
            ModalPopupExtender.Show();
            DivI3.Visible = false;
            DivI4.Visible = false;
            DivI5.Style.Add("display", "none");
            ddlMonth.SelectedIndex = 0;
            txtBDate.Text = "";
            ddlDOproof.SelectedIndex = 0;
            DivI7.Visible = false;
            if (Convert.ToInt32(ddlIReasons.SelectedValue) == 1)
            {
                DivI3.Visible = true;
                DivI7.Visible = true;
                MpexdrFollowup.Show();
            }
            else if (Convert.ToInt32(ddlIReasons.SelectedValue) == 2 || Convert.ToInt32(ddlIReasons.SelectedValue) == 3)
            {
                DivI4.Visible = true;
                MpexdrFollowup.Show();
            }
            else
            {
                MpexdrFollowup.Show();
            }
        }
        else
        {
            ddlMonth.SelectedIndex = 0;
            txtBDate.Text = "";
            ddlDOproof.SelectedIndex = 0;
            ModalPopupExtender.Show();
            DivI3.Visible = false;
            DivI4.Visible = false;
            DivI5.Style.Add("display", "none"); ;
            MpexdrFollowup.Show();
        }

    }
    protected void ddlDOproof_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Convert.ToInt32(ddlDOproof.SelectedValue) == 6)
        {
            DivI6.Visible = true;
        }
        else
        {
            DivI6.Visible = false;
            txtOther.Text = "";
        }
        DivI5.Style.Add("display", "block"); ;
        ModalPopupExtender.Show();
        MpexdrFollowup.Show();
    }

    protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Convert.ToInt32(ddlCategory.SelectedValue) == 99)
        {
            DivE11.Visible = true;
        }
        else
        {
            DivE11.Visible = false;
            txtEnrommentOther.Text = "";
        }
        ModalPopupExtender.Show();
        MpexdrFollowup.Show();
    }

    protected void ddlClass_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Convert.ToInt32(ddlClass.SelectedValue) == 18)
        {
            DivE10.Visible = true;
        }
        else
        {
            DivE10.Visible = false;
            txtClassOther.Text = "";
        }
        ModalPopupExtender.Show();
        MpexdrFollowup.Show();
    }
    protected void ddlStatus_SelectedIndexChanged(object sender, EventArgs e)
    {

        DropDownList ddlLabTest1 = (DropDownList)sender;
        GridViewRow row1 = (GridViewRow)ddlLabTest1.NamingContainer;

        DropDownList ddlStatus = (DropDownList)row1.FindControl("ddlStatus");
        RadioButtonList rblTBFC = (RadioButtonList)row1.FindControl("rblTBFC");

        Label lbUniqueCode = (Label)row1.FindControl("lbUniqueCode");


        lblEditActivtive.Text = DateTime.Now.ToString();
        lblGuID.Text = "";
        Label lbStatus = (Label)row1.FindControl("lbStatus");
        lblEnrollId.Text = ddlStatus.SelectedValue;
        lblRtbFc.Text = rblTBFC.SelectedValue;
        lblD2dUniqeCode.Text = lbUniqueCode.Text;

        Label lbStatusNew = (Label)row1.FindControl("lbStatusNew");
        //if (lbStatus.Text == "2" && ddlStatus.SelectedValue.ToString() == "1")
        //{
        //    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Allready Contact')</script>", false);
        //    ddlStatus.SelectedIndex = 0;
        //}
        //else if (lbStatus.Text == "0" && ddlStatus.SelectedValue.ToString() == "2")
        //{
        //    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Contact frist then Follow up')</script>", false);
        //    ddlStatus.SelectedIndex = 0;
        //}
        //else
        //{
        lbStatus.Text = "2";
        //}

        lblEditRow.Text = "0";

        lblStst.Text = ddlStatus.SelectedItem.Text;

        ModalPopupExtender.Show();
        dvIngilible.Visible = false;
        dvidFollowp.Visible = false;
        dvEnrollment.Visible = false;
        DivI10.Visible = false;
        DivI11.Visible = false;
        DivI7.Visible = false;
        if (Convert.ToInt32(ddlStatus.SelectedValue) == 1)
        {
            dvidFollowp.Visible = true;
            divF1.Visible = false;
            divF2.Visible = false;
            divF7.Visible = false;
            divF4.Visible = false;
            divF5.Visible = false;
            divF6.Visible = false;
            DivE10.Visible = false;
            DivE11.Visible = false;
            DivI10.Visible = true;
            DivI7.Visible = false;
            txtGovtID.Text = "";
            txtSamgraID.Text = "";
            ddlFo.SelectedIndex = 0;
            txtOtherVillage.Text = "";
            txtOtherSchool.Text = "";
            ddlOtherVillage.SelectedIndex = 0;
            if (Session["StateCode"].ToString() == "8")
            {


                divF1.Visible = true;
            }
            else
            {
                divF2.Visible = true;
            }

            Label lblDtdUniqid = (Label)row1.FindControl("lblDtdUniqid");

            string strQry = " select *  from [tblDTDMobileActivity]   where IsActive=0 and [GUIDDTDMobileActivity]='" + lblDtdUniqid.Text + "' and ActivityStatus=1 ";


            DataTable dtIne = objMain.LoadData(strQry);
            if (dtIne.Rows.Count > 0)
            {
                ddlFo.SelectedValue = dtIne.Rows[0]["FollowUPID"].ToString();

                ddlFo_SelectedIndexChanged(ddlIReasons, null);
                if (Session["StateCode"].ToString() == "8")
                {
                    txtGovtID.Text = dtIne.Rows[0]["GovtID"].ToString();

                }
                else
                {
                    txtSamgraID.Text = dtIne.Rows[0]["SamgraID"].ToString();
                }
                if (dtIne.Rows[0]["FollowUPID"].ToString() == "1" || dtIne.Rows[0]["FollowUPID"].ToString() == "2")
                {
                    ddlOtherVillage.SelectedValue = dtIne.Rows[0]["FollowUPID"].ToString();
                    ddlFOtherVillage_SelectedIndexChanged(ddlIReasons, null);
                    ddlSchool.SelectedValue = dtIne.Rows[0]["SchoolCode"].ToString();
                    txtOtherVillage.Text = dtIne.Rows[0]["SchoolOrVillageName"].ToString();

                    txtOtherSchool.Text = dtIne.Rows[0]["otherSchoolName"].ToString();
                }
                //    ddlMonth.SelectedValue = dtIne.Rows[0]["Months"].ToString();
            }
        }
        if (Convert.ToInt32(ddlStatus.SelectedValue) == 2)
        {


            ddlIReasons.SelectedIndex = 0;
            ddlMigration.SelectedIndex = 0;
            txtBDate.Text = "";
            ddlMonth.SelectedIndex = 0;
            ddlDOproof.SelectedIndex = 0;
            dvIngilible.Visible = true;
            if (Session["StateCode"].ToString() == "8")
            {


                DivI10.Visible = true;
            }
            else
            {
                DivI11.Visible = true;
            }
            txtOther.Text = "";
            DivI3.Visible = false;
            DivI4.Visible = false;
            DivI5.Style.Add("display", "none");
            DivI6.Visible = false;
            DivI7.Visible = false;
        }
        if (Convert.ToInt32(ddlStatus.SelectedValue) == 3)
        {
            dvEnrollment.Visible = true;
            ddlEotherVillage.SelectedIndex = 0;
            //  ddlESchool.SelectedIndex = 0;
            ddlFromStatus.SelectedIndex = 0;
            txtErollmentDate.Text = "";
            txtEvillage.Text = "";
            txtSchoolar.Text = "";
            txtSschool.Text = "";
            ddlClass.SelectedIndex = 0;
            txtErollmentDate.Text = "";
            DivE3.Visible = false;
            DivE4.Visible = false;
            DivE2.Visible = false;
            DivE10.Visible = false;
            DivE11.Visible = false;
            DivE12.Visible = false;
            DivE13.Visible = false;
            if (Session["StateCode"].ToString() == "8")
            {


                DivE12.Visible = true;
            }
            else
            {
                DivE13.Visible = true;
            }
        }
        if (ddlStatus.SelectedIndex > 0)
        {
            MpexdrFollowup.Show();
        }
    }


    protected void btnEditEnroll_Click(object sender, EventArgs e)
    {

        ImageButton ddlLabTest1 = (ImageButton)sender;
        GridViewRow row1 = (GridViewRow)ddlLabTest1.NamingContainer;

        DropDownList ddlStatus = (DropDownList)row1.FindControl("ddlStatus");
        RadioButtonList rblTBFC = (RadioButtonList)row1.FindControl("rblTBFC");

        Label lbUniqueCode = (Label)row1.FindControl("lbUniqueCode");
        Label lblDtdUniqid = (Label)row1.FindControl("lblDtdUniqid");

        lblEditRow.Text = "1";

        Label lbStatus = (Label)row1.FindControl("lbStatus");
        lblEnrollId.Text = ddlStatus.SelectedValue;
        lblRtbFc.Text = rblTBFC.SelectedValue;
        lblD2dUniqeCode.Text = lbUniqueCode.Text;
        ModalPopupExtender.Show();
        dvIngilible.Visible = false;
        dvidFollowp.Visible = false;
        dvEnrollment.Visible = false;
        if (Convert.ToInt32(ddlStatus.SelectedValue) == 1)
        {
            dvidFollowp.Visible = true;
            divF1.Visible = false;
            divF2.Visible = false;
            divF7.Visible = false;
            divF4.Visible = false;
            divF5.Visible = false;
            divF6.Visible = false;
            txtGovtID.Text = "";
            txtSamgraID.Text = "";
            ddlFo.SelectedIndex = 0;
            txtOtherVillage.Text = "";
            txtOtherSchool.Text = "";
            ddlOtherVillage.SelectedIndex = 0;
            if (Session["StateCode"].ToString() == "8")
            {


                divF1.Visible = true;
            }
            else
            {
                divF2.Visible = true;
            }


        }
        if (Convert.ToInt32(ddlStatus.SelectedValue) == 2)
        {




            ddlIReasons.SelectedIndex = 0;
            txtBDate.Text = "";
            ddlMonth.SelectedIndex = 0;
            ddlDOproof.SelectedIndex = 0;
            dvIngilible.Visible = true;


            DivI3.Visible = false;
            DivI4.Visible = false;
            DivI5.Style.Add("display", "none");
            DivI7.Visible = true;
            string strQry = " select *  from [tblDTDMobileActivity]   where IsActive=0 and [GUIDDTDMobileActivity]='" + lblDtdUniqid.Text + "' and ActivityStatus=" + lblEnrollId.Text + " ";


            DataTable dtIne = objMain.LoadData(strQry);
            if (dtIne.Rows.Count > 0)
            {

                ddlIReasons.SelectedValue = Convert.ToString(dtIne.Rows[0]["IneligibleID"]);
                ddlIReasons_SelectedIndexChanged(ddlFo, null);
                ddlMonth.SelectedValue = Convert.ToString(dtIne.Rows[0]["Months"]);
                lblGuID.Text = Convert.ToString(dtIne.Rows[0]["GUIDDTDMobileActivity"]);
                ddlDOproof.SelectedValue = Convert.ToString(dtIne.Rows[0]["DOBproof"]);
                ddlMigration.SelectedValue = Convert.ToString(dtIne.Rows[0]["Migrationplace"]);
                txtIGovtID.Text = Convert.ToString(dtIne.Rows[0]["GovtID"]);
                txtSamgra.Text = Convert.ToString(dtIne.Rows[0]["SamgraID "]);
                if (Convert.ToDateTime(dtIne.Rows[0]["DOB"]).ToString("dd-MM-yyyy") != "01-01-1900")
                {
                    txtBDate.Text = Convert.ToDateTime(dtIne.Rows[0]["DOB"]).ToString("dd-MM-yyyy");
                    //txtBDate_TextChanged(txtBDate, null);
                    DivI5.Visible = true;

                }
                if (Convert.ToInt32(ddlDOproof.SelectedValue) == 6)
                {
                    ddlDOproof_SelectedIndexChanged(ddlDOproof, null);
                }
                txtOther.Text = Convert.ToString(dtIne.Rows[0]["Other"]);
                lblEditActivtive.Text = Convert.ToString(dtIne.Rows[0]["ActivityDate"]);
            }

        }
        if (Convert.ToInt32(ddlStatus.SelectedValue) == 3)
        {
            dvEnrollment.Visible = true;
            ddlEotherVillage.SelectedIndex = 0;
            //  ddlESchool.SelectedIndex = 0;
            ddlFromStatus.SelectedIndex = 0;
            txtErollmentDate.Text = "";
            txtEvillage.Text = "";
            txtSchoolar.Text = "";
            txtSschool.Text = "";
            ddlClass.SelectedIndex = 0;
            txtErollmentDate.Text = "";
            DivE3.Visible = false;
            DivE4.Visible = false;
            DivE2.Visible = false;

            if (Session["StateCode"].ToString() == "8")
            {


                DivE12.Visible = true;
            }
            else
            {
                DivE13.Visible = true;
            }
            string strQry = " select *  from [tblDTDMobileActivity]   where IsActive=0 and [GUIDDTDMobileActivity]='" + lblDtdUniqid.Text + "' and ActivityStatus=" + lblEnrollId.Text + " ";


            DataTable dtIne = objMain.LoadData(strQry);
            if (dtIne.Rows.Count > 0)
            {

                ddlEotherVillage.SelectedValue = Convert.ToString(dtIne.Rows[0]["VillageOptionID"]);
                ddlEFOtherVillage_SelectedIndexChanged(ddlFo, null);
                txtEvillage.Text = Convert.ToString(dtIne.Rows[0]["SchoolOrVillageName"]);
                txtSschool.Text = Convert.ToString(dtIne.Rows[0]["otherSchoolName"]);
                if (Convert.ToString(dtIne.Rows[0]["SchoolCode"]).Length > 5)
                {
                    ddlESchool.SelectedValue = Convert.ToString(dtIne.Rows[0]["SchoolCode"]);
                }
                lblGuID.Text = Convert.ToString(dtIne.Rows[0]["GUIDDTDMobileActivity"]);
                ddlFromStatus.SelectedValue = Convert.ToString(dtIne.Rows[0]["FromPanding6"]);
                ddlCategory.SelectedValue = Convert.ToString(dtIne.Rows[0]["EnrollmentCategory"]);
                if (Convert.ToString(dtIne.Rows[0]["EnrollmentCategory"]) == "99")
                {
                    ddlCategory_SelectedIndexChanged(ddlCategory, null);
                }
                txtSchoolar.Text = Convert.ToString(dtIne.Rows[0]["ScholarNo"]);
                ddlClass.SelectedValue = Convert.ToString(dtIne.Rows[0]["ClassofEnrollment"]);
                txtEGovtID.Text = Convert.ToString(dtIne.Rows[0]["GovtID"]);
                txtEsamgranID.Text = Convert.ToString(dtIne.Rows[0]["SamgraID "]);
                if (Convert.ToString(dtIne.Rows[0]["ClassofEnrollment"]) == "18")
                {
                    ddlClass_SelectedIndexChanged(ddlClass, null);
                }
                if (Convert.ToString(dtIne.Rows[0]["DateofEnrollment"]) != "")
                {
                    if (Convert.ToDateTime(dtIne.Rows[0]["DateofEnrollment"]).ToString("dd-MM-yyyy") != "01-01-1900")
                    {
                        txtErollmentDate.Text = Convert.ToDateTime(dtIne.Rows[0]["DateofEnrollment"]).ToString("dd-MM-yyyy");

                    }

                }


            }

        }
        if (ddlStatus.SelectedIndex > 0)
        {
            MpexdrFollowup.Show();
        }


    }
    //protected void txtBDate_TextChanged(object sender, EventArgs e)
    //{
    //    ModalPopupExtender.Show();
    //    DivI5.Visible = true;
    //    MpexdrFollowup.Show();
    //}
    protected void lnkEnrool_OnClick(object sender, EventArgs e)
    {
        SqlParameter[] parm = new SqlParameter[]
            {
       new SqlParameter("@villagecode",   ddlVilage.SelectedValue ),
              new SqlParameter("@Flag","1"),
      
                 };
        DataTable dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "InsertActivityDTD", parm);

        SqlParameter[] parm1 = new SqlParameter[]
            {
       new SqlParameter("@villagecode",   ddlVilage.SelectedValue ),
              new SqlParameter("@Flag","2"),
      
                 };
        DataTable dataTable = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "InsertActivityDTD", parm1);


        //string  strQry = " select [mst5village].EGVillagecode + '-' +RIGHT('0000' +  convert(varchar,serial), 4) as UniqueId,UniqueCode,RIGHT('0000' +  convert(varchar,serial), 4) as  UniqueIdNew,ActivityStatus as Status,HHNo,ChildName,FathersName from  [tblDTD] inner join mst5Village on mst5village.villagecode=tblDTD.villagecode or tblDTD.villagecode=mst5village.OldUniqueCode    or tblDTD.villagecode=mst5village.RefVillageCode   where  tblDTD.Status='1' and mst5village.Villagecode= '" + ddlVilage.SelectedValue + "'    and " + DateTime.Today.Year + " - (YEAR(SurvayDate)-isnull(AgeAson,0))>=6  and (" + DateTime.Today.Year + " - (YEAR(SurvayDate)-isnull(AgeAson,0))<=14  ) and EduationStatus in(2,3,99)   and EnrollStatus=1 and DeleteFlag<>2";

        //  DataTable dataTable = objMain.LoadData(strQry);

        if (dataTable != null)
        {
            if (dataTable.Rows.Count > 0)
            {
                this.Gv_Display.DataSource = dataTable;
                this.Gv_Display.DataBind();
                Session["D2dBind"] = dataTable;
            }
        }
        this.txtSearch.Text = "";
        ModalPopupExtender.Show();
        ModalPopupExtender1.Hide();
    }
    protected void btnClose_Click(object sender, EventArgs e)
    {

        bool InsertD2d = false;

        Int32 Flag = 0;
        string SamgraID = "";
        string GovtID = "";
        if (txtGovtID.Text.Trim() != "")
        {
            GovtID = txtGovtID.Text;
        }
        if (txtSamgraID.Text.Trim() != "")
        {
            SamgraID = Convert.ToString(txtSamgraID.Text);
        }
        string UNICOde = objMain.Generate_RandomString(15);


        if (lblEnrollId.Text == "1")
        {
            #region InsertFollow
            if (ddlFo.SelectedIndex <= 0)
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select Reasons')</script>", false);
                ModalPopupExtender.Show();
                MpexdrFollowup.Show();
                return;


            }
            if (Convert.ToInt32(ddlFo.SelectedValue) == 1)
            {
                if (ddlOtherVillage.SelectedIndex <= 0)
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select Village')</script>", false);
                    ModalPopupExtender.Show();
                    MpexdrFollowup.Show();
                    return;

                }
            }

            if (Convert.ToInt32(ddlOtherVillage.SelectedValue) == 2)
            {
                if (txtOtherVillage.Text == "")
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please enter other Village')</script>", false);
                    ModalPopupExtender.Show();
                    MpexdrFollowup.Show();
                    return;

                }
            }

            SqlParameter[] cmdParameters = new SqlParameter[]
                    {
                        new SqlParameter("@UniqueCode", lblD2dUniqeCode.Text ),
                        new SqlParameter("@ActivityStatus", lblEnrollId.Text),
                        new SqlParameter("@TBorFC", lblRtbFc.Text  ),
                        new SqlParameter("@ActivityDate",  DateTime.Now.ToString("yyyy-MM-dd")),
                        new SqlParameter("@GovtID", GovtID),
                        new SqlParameter("@SamgraID", SamgraID),
                        new SqlParameter("@SchoolCode", ddlSchool.SelectedValue ),
                  
                        new SqlParameter("@SchoolOrVillageName", txtOtherVillage.Text),
                        new SqlParameter("@FollowUPID", ddlFo.SelectedValue),
                        new SqlParameter("@IneligibleID", "0" ),
                
                         new SqlParameter("@Months","0"),
                        new SqlParameter("@DOB", DBNull.Value),
                                new SqlParameter("@DOBproof", "0"),
                        new SqlParameter("@Other","" ),
                

                          new SqlParameter("@FromPanding6","0"),
                        new SqlParameter("@EnrollmentCategory","0"),
                             new SqlParameter("@ScholarNo", ""),
                        new SqlParameter("@ClassofEnrollment","0"),
                             new SqlParameter("@DateofEnrollment", DBNull.Value),
                        new SqlParameter("@CreateBy", Session["username"].ToString()),
                        new SqlParameter("@GUIDDTDMobileActivity", UNICOde),
                        new SqlParameter("@VillageOptionID", ddlOtherVillage.SelectedValue),
                          new SqlParameter("@otherSchoolName", txtOtherSchool.Text ),
                        new SqlParameter("@Fyear", Session["FinYear"].ToString() ),
                         new SqlParameter("@Flag", "1" ),
                            new SqlParameter("@Enrollmentother", "" ),
                        new SqlParameter("@Classother", ""),
                         new SqlParameter("@Migrationplace", "0" ),
                    };
            Int32 icount = SqlHelper.ExecuteNonQuery(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "InsertUpdateDTDMobileActivity", cmdParameters);
            #endregion
        }

        if (lblEnrollId.Text == "2")
        {

            #region Ineligible




            if (ddlIReasons.SelectedIndex <= 0)
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select Reasons')</script>", false);
                ModalPopupExtender.Show();
                MpexdrFollowup.Show();
                return;


            }




            if (Convert.ToInt32(ddlIReasons.SelectedValue) == 1)
            {
                if (ddlMonth.SelectedIndex <= 0)
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select Month')</script>", false);
                    ModalPopupExtender.Show();
                    MpexdrFollowup.Show();
                    return;
                }
                if (ddlMigration.SelectedIndex <= 0)
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select Migration Place')</script>", false);
                    ModalPopupExtender.Show();
                    MpexdrFollowup.Show();
                    return;


                }
            }

            if (Convert.ToInt32(ddlIReasons.SelectedValue) == 2)
            {
                if (txtBDate.Text == "")
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select birthday')</script>", false);
                    ModalPopupExtender.Show();
                    MpexdrFollowup.Show();
                    return;
                }

            }
            if (txtBDate.Text != "")
            {
                string BithDate = Convert.ToDateTime(txtBDate.Text).ToString("yyyy-MM-dd");
                string ActivitDay = Convert.ToDateTime(lblEditActivtive.Text).ToString("yyyy-MM-dd");
                if (Convert.ToDateTime(ActivitDay) <= Convert.ToDateTime(BithDate))
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please enter birthday less then activity Date')</script>", false);
                    ModalPopupExtender.Show();
                    MpexdrFollowup.Show();
                    return;
                }
                if (ddlDOproof.SelectedIndex <= 0)
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select Dob Proof')</script>", false);
                    ModalPopupExtender.Show();
                    MpexdrFollowup.Show();
                    return;


                }
            }

            if (txtIGovtID.Text.Trim() != "")
            {
                GovtID = txtIGovtID.Text;
            }
            if (txtSamgra.Text.Trim() != "")
            {
                SamgraID = Convert.ToString(txtSamgra.Text);
            }

            string dob = DBNull.Value.ToString();
            if (txtBDate.Text != "")
            {
                dob = txtBDate.Text;
            }
            if (lblEditRow.Text == "0")
            {
                Flag = 2;
            }
            else
            {
                Flag = 4;
                UNICOde = lblGuID.Text;
            }
            SqlParameter[] cmdParameters = new SqlParameter[]
                    {
                        new SqlParameter("@UniqueCode", lblD2dUniqeCode.Text ),
                        new SqlParameter("@ActivityStatus", lblEnrollId.Text),
                        new SqlParameter("@TBorFC", lblRtbFc.Text  ),
                        new SqlParameter("@ActivityDate",  DateTime.Now.ToString("yyyy-MM-dd")),
                        new SqlParameter("@GovtID", GovtID),
                        new SqlParameter("@SamgraID", SamgraID),
                        new SqlParameter("@SchoolCode",  "" ),
                  
                        new SqlParameter("@SchoolOrVillageName",  ""),
                        new SqlParameter("@FollowUPID", "0"),
                        new SqlParameter("@IneligibleID",ddlIReasons.SelectedValue  ),
                
                         new SqlParameter("@Months",ddlMonth.SelectedValue),
                        new SqlParameter("@DOB", dob),
                                new SqlParameter("@DOBproof", ddlDOproof.SelectedValue),
                        new SqlParameter("@Other",txtOther.Text ),
                

                          new SqlParameter("@FromPanding6","0"),
                        new SqlParameter("@EnrollmentCategory","0"),
                             new SqlParameter("@ScholarNo", ""),
                        new SqlParameter("@ClassofEnrollment","0"),
                             new SqlParameter("@DateofEnrollment", DBNull.Value),
                        new SqlParameter("@CreateBy", Session["username"].ToString()),
                        new SqlParameter("@GUIDDTDMobileActivity", UNICOde),
                        new SqlParameter("@VillageOptionID", "0"),
                          new SqlParameter("@otherSchoolName", "" ),
                        new SqlParameter("@Fyear", Session["FinYear"].ToString() ),
                         new SqlParameter("@Flag", Flag),
                              new SqlParameter("@Enrollmentother", "" ),
                        new SqlParameter("@Classother", ""),
                         new SqlParameter("@Migrationplace", ddlMigration.SelectedValue ),
                    };
            Int32 icount = SqlHelper.ExecuteNonQuery(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "InsertUpdateDTDMobileActivity", cmdParameters);
            #endregion
        }

        if (lblEnrollId.Text == "3")
        {
            #region Enrollment
            if (ddlFromStatus.SelectedIndex <= 0)
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select From Status')</script>", false);
                ModalPopupExtender.Show();
                MpexdrFollowup.Show();
                return;


            }
            if (ddlCategory.SelectedIndex <= 0)
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select Category')</script>", false);
                ModalPopupExtender.Show();
                MpexdrFollowup.Show();
                return;
            }
            if (txtSchoolar.Text == "")
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please enter Schoolar')</script>", false);
                ModalPopupExtender.Show();
                MpexdrFollowup.Show();
                return;
            }
            if (Convert.ToInt32(ddlFromStatus.SelectedValue) == 1)
            {

                if (ddlClass.SelectedIndex <= 0)
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select Class')</script>", false);
                    ModalPopupExtender.Show();
                    MpexdrFollowup.Show();
                    return;
                }
            }
            string dob = DBNull.Value.ToString();
            if (txtErollmentDate.Text != "")
            {
                dob = txtErollmentDate.Text;
            }
            if (lblEditRow.Text == "0")
            {
                Flag = 3;
            }
            else
            {
                Flag = 5;
                UNICOde = lblGuID.Text;
            }
            if (txtEGovtID.Text.Trim() != "")
            {
                GovtID = txtEGovtID.Text;
            }
            if (txtEsamgranID.Text.Trim() != "")
            {
                SamgraID = Convert.ToString(txtEsamgranID.Text);
            }
            SqlParameter[] cmdParameters = new SqlParameter[]
                    {
                        new SqlParameter("@UniqueCode", lblD2dUniqeCode.Text ),
                        new SqlParameter("@ActivityStatus", lblEnrollId.Text),
                        new SqlParameter("@TBorFC", lblRtbFc.Text  ),
                        new SqlParameter("@ActivityDate",  DateTime.Now.ToString("yyyy-MM-dd")),
                        new SqlParameter("@GovtID",GovtID),
                        new SqlParameter("@SamgraID",SamgraID),
                        new SqlParameter("@SchoolCode", ddlESchool.SelectedValue ),
                  
                        new SqlParameter("@SchoolOrVillageName",  txtEvillage.Text),
                        new SqlParameter("@FollowUPID", "0"),
                        new SqlParameter("@IneligibleID", "0"  ),
                
                         new SqlParameter("@Months", "0"),
                        new SqlParameter("@DOB", DBNull.Value),
                                new SqlParameter("@DOBproof", ddlDOproof.SelectedValue),
                        new SqlParameter("@Other","" ),
                

                          new SqlParameter("@FromPanding6",ddlFromStatus.SelectedValue),
                        new SqlParameter("@EnrollmentCategory",ddlCategory.SelectedValue),
                             new SqlParameter("@ScholarNo", txtSchoolar.Text),
                        new SqlParameter("@ClassofEnrollment",ddlClass.SelectedValue),
                             new SqlParameter("@DateofEnrollment", dob),
                        new SqlParameter("@CreateBy", Session["username"].ToString()),
                        new SqlParameter("@GUIDDTDMobileActivity", UNICOde),
                        new SqlParameter("@VillageOptionID", ddlEotherVillage.SelectedValue),
                          new SqlParameter("@otherSchoolName", txtSschool.Text ),
                        new SqlParameter("@Fyear", Session["FinYear"].ToString() ),
                         new SqlParameter("@Flag", Flag ),
                              new SqlParameter("@Enrollmentother", txtEnrommentOther.Text),
                        new SqlParameter("@Classother",ddlClass.SelectedValue),
                         new SqlParameter("@Migrationplace", "0" ),

                    };
            Int32 icount = SqlHelper.ExecuteNonQuery(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "InsertUpdateDTDMobileActivity", cmdParameters);
            #endregion
        }

        string StudentTSInsertQueryD2d = "";
        StudentTSInsertQueryD2d += " Update tblActivityDTD set  GUIDDTDActivityID='" + UNICOde + "', TBorFC=" + lblRtbFc.Text + ",ActivityStatus =" + lblEnrollId.Text + ",UserType='P' , ActivityDate ='" + DateTime.Now.ToString("yyyy-MM-dd") + "',UploadedDate= GETDATE() where UniqueCode ='" + lblD2dUniqeCode.Text + "' ";
        InsertD2d = objMain.AddUpdate(StudentTSInsertQueryD2d);

        if (InsertD2d == true)
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Saved sucessfully')</script>", false);
            ModalPopupExtender.Show();
        }
    }

    public void SaveData()
    {
        if (this.ddlRemark.SelectedIndex <= 0)
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select Remark')</script>", false);
            this.ddlRemark.Focus();
            return;
        }

        int GssEnrollRetan = 0;
        int MMEnrollRetan = 0;
        int Comm1EnrollRetan = 0;
        #region Variable
        string commmeeting = "";
        string commmeeting1 = "";
        string commmeeting2 = "";
        string commOther = "";
        string commOther1 = "";
        foreach (ListItem item in CBL_bookformat.Items)
        {
            if (item.Selected)
            {

                commmeeting += "" + item.Value + "" + ",";
                if (item.Value == "8")
                {
                    commOther = item.Value;
                }

            }
        }
        foreach (ListItem item in CBL_bookformatNew.Items)
        {
            if (item.Selected)
            {

                commmeeting1 += "" + item.Value + "" + ",";
                if (item.Value == "99")
                {
                    commOther1 = item.Value;
                }

            }
        }
        foreach (ListItem item in CBL_bookformatNew1.Items)
        {
            if (item.Selected)
            {

                commmeeting2 += "" + item.Value + "" + ",";


            }
        }
        if(commmeeting.Length>0){commmeeting = commmeeting.Substring(0, commmeeting.LastIndexOf(","));}
        if (commmeeting1.Length > 0) { commmeeting1 = commmeeting1.Substring(0, commmeeting1.LastIndexOf(",")); }
        if (commmeeting2.Length > 0) { commmeeting2 = commmeeting2.Substring(0, commmeeting2.LastIndexOf(",")); }
        if (commmeeting.Length > 0 || commmeeting1.Length > 0 || commmeeting2.Length>0)
        {
           //commmeeting = commmeeting.Substring(0, commmeeting.LastIndexOf(","));
            if (commmeeting.Length > 0)
            {

            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select GSS Agenda')</script>", false);
                this.txt_pbname.Focus();
                return;
            }

            if (commmeeting1.Length > 0)
            {

            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select GSS Highlights of Discussion')</script>", false);
                this.txt_pbnameNew.Focus();
                return;
            }

            if (commmeeting2.Length > 0)
            {

            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select GSS Key People Present')</script>", false);
                this.txt_pbnameNew1.Focus();
                return;
            }
            if (chkcommmetingTB.Checked == true || chkcommmetingFC.Checked == true)
            {

            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select GSS-TB/FC')</script>", false);


                this.chkcommmetingTB.Focus();
                return;
            }
            if (commOther == "8")
            {
                if (txt_bookformatOther.Text.ToString() == "")
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Enter Other(Specify) GSS')</script>", false);


                    this.txt_bookformatOther.Focus();
                    txt_bookformatOther.Enabled = true;
                    return;
                }
                else
                {
                    txt_bookformatOther.Enabled = false;
                }

            }
            if (commOther == "99")
            {
                if (txt_bookformatOther.Text.ToString() == "")
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Enter Other(Specify) GSS')</script>", false);


                    this.txt_bookformatOther.Focus();
                    txt_bookformatOther.Enabled = true;
                    return;
                }
                else
                {
                    txt_bookformatOther.Enabled = false;
                }

            }
            if (txtV1illager.Text == "")
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please ensure People Attended is more than or equal to zero')</script>", false);


                this.txtV1illager.Focus();
                return;
            }
            if (TxtGSS_Male.Text == "")
            {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select Gss Male')</script>", false);
                    this.TxtGSS_Male.Focus();
                    return;               
            }

            if (TxtGSS_FeMale.Text == "")
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select Gss FeMale')</script>", false);
                this.TxtGSS_FeMale.Focus();
                return;
            }
            if (rdEnrollMent.Checked == false && rdRetention.Checked == false)
            {

                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select GSS Enrollment or GSS Retantion')</script>", false);
                return;
            }   

        }
        if (rdEnrollMent.Checked == true || rdRetention.Checked == true)
        {
            if (commmeeting.Length > 0)
            {

            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select GSS Agenda')</script>", false);
                this.txt_pbname.Focus();
                return;
            }

            if (commmeeting1.Length > 0)
            {

            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select GSS Highlights of Discussion')</script>", false);
                this.txt_pbnameNew.Focus();
                return;
            }

            if (commmeeting2.Length > 0)
            {

            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select GSS Key People Present')</script>", false);
                this.txt_pbnameNew1.Focus();
                return;
            }
        }  


        if (txtV1illager.Text != "")
        {
            if (chkcommmetingTB.Checked == true || chkcommmetingFC.Checked == true)
            {

            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select GSS-TB/FC')</script>", false);


                this.chkcommmetingTB.Focus();
                return;
            }
            if (commmeeting.Length > 0)
            {
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Select GSS Aganda')</script>", false);

                return;
            }
        }
        string Muhula = "";
        string Muhula1 = "";
        string Muhula2 = "";
        string TempMuhulaOther = "";
        string TempMuhulaOther1 = "";
        foreach (ListItem item in CBL_Muhula.Items)
        {
            if (item.Selected)
            {

                Muhula += "" + item.Value + "" + ",";
                if (item.Value == "8")
                {
                    TempMuhulaOther = item.Value;
                }
            }
        }
        foreach (ListItem item in CBL_MuhulaNew.Items)
        {
            if (item.Selected)
            {

                Muhula1 += "" + item.Value + "" + ",";
                if (item.Value == "99")
                {
                    TempMuhulaOther1 = item.Value;
                }
            }
        }

        foreach (ListItem item in CBL_MuhulaNew1.Items)
        {
            if (item.Selected)
            {

                Muhula2 += "" + item.Value + "" + ",";

            }
        }
        if (Muhula.Length > 0){Muhula = Muhula.Substring(0, Muhula.LastIndexOf(","));}
        if (Muhula1.Length > 0) { Muhula1 = Muhula1.Substring(0, Muhula1.LastIndexOf(",")); }
        if (Muhula2.Length > 0) { Muhula2 = Muhula2.Substring(0, Muhula2.LastIndexOf(",")); }
        if (Muhula.Length > 0 ||Muhula1.Length > 0|| Muhula2.Length>0)
        {
            //Muhula = Muhula.Substring(0, Muhula.LastIndexOf(","));
            if (Muhula.Length > 0)
            {

            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select MM Agenda')</script>", false);
                this.txtMuhala.Focus();
                return;
            }

            if (Muhula1.Length > 0)
            {
              
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select MM Highlights of Discussion')</script>", false);
                this.txtMuhalaNew.Focus();
                return;
            }

            if (Muhula2.Length > 0)
            {

            }
            else
            {
                this.txtMuhalaNew1.Focus();
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select MM Key People Present')</script>", false);
                return;
            }
            if (rblmuhulaTb.Checked == true || rblmuhulaFC.Checked == true)
            {

            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select Mauhalla Meeting  TB/FC')</script>", false);


                this.rblmuhulaTb.Focus();
                return;
            }
            if (txtVillager2.Text == "")
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please ensure People Attended is more than or equal to zero')</script>", false);
                this.txtVillager2.Focus();
                return;
            }
            if (TxtMM_FeMale.Text == "")
            {              
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select MM Female')</script>", false);
                    this.TxtMM_FeMale.Focus();
                    return;              
            }
            if (TxtMM_Male.Text == "")
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select MM Male')</script>", false);
                this.TxtMM_Male.Focus();
                return;
            }

            if (TempMuhulaOther == "8")
            {
                if (txtmOther.Text.ToString() == "")
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select Other(Specify) Mauhalla')</script>", false);


                    this.txtmOther.Focus();
                    txtmOther.Enabled = true;
                    return;
                }
                else
                {
                    txtmOther.Enabled = false;
                }

            }
            if (rdEnrollment1.Checked == false && rdRetantion1.Checked == false)
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select MM Enrollment or MM Retantion')</script>", false);
                return;
            }

           
        }
        if (rdEnrollment1.Checked == true || rdRetantion1.Checked == true)
        {
            if (Muhula.Length > 0)
            {

            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select MM Agenda')</script>", false);
                this.txtMuhala.Focus();
                return;
            }

            if (Muhula1.Length > 0)
            {

            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select MM Highlights of Discussion')</script>", false);
                this.txtMuhalaNew.Focus();
                return;
            }

            if (Muhula2.Length > 0)
            {

            }
            else
            {
                this.txtMuhalaNew1.Focus();
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select MM Key People Present')</script>", false);
                return;
            }
            
        }

        if (txtVillager2.Text != "")
        {
            if (Muhula.Length > 0)
            {
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Select MM Aganda')</script>", false);
                return;

            }
        }

        string othercom = "";
        string othercom1 = "";
        string othercom2 = "";
        string Tempothercom = "";
        string Tempothercom1 = "";
        string Tempothercom2 = "";
        foreach (ListItem item in chk_othercom.Items)
        {
            if (item.Selected)
            {

                othercom += "" + item.Value + "" + ",";

                if (item.Value == "8")
                {
                    Tempothercom = item.Value;
                }
            }

        }
        foreach (ListItem item in chk_othercom_New.Items)
        {
            if (item.Selected)
            {

                othercom1 += "" + item.Value + "" + ",";

                if (item.Value == "99")
                {
                    Tempothercom1 = item.Value;
                }
            }
        }
        foreach (ListItem item in chk_othercom_New1.Items)
        {
            if (item.Selected)
            {
                othercom2 += "" + item.Value + "" + ",";
            }
        }
        if (othercom.Length > 0) {othercom = othercom.Substring(0, othercom.LastIndexOf(","));}
         if (othercom1.Length > 0) {othercom1 = othercom1.Substring(0, othercom1.LastIndexOf(","));}
         if (othercom2.Length > 0) { othercom2 = othercom2.Substring(0, othercom2.LastIndexOf(",")); }
         if (othercom.Length > 0 || othercom1.Length > 0 || othercom2.Length > 0)
        {
            //othercom = othercom.Substring(0, othercom.LastIndexOf(","));

            if (othercom.Length > 0)
            {

            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select Community Meeting 1 Agenda')</script>", false);
                this.txtOtherComminuty.Focus();
                return;
            }

            if (othercom1.Length > 0)
            {

            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select Community Meeting 1 Highlights of Discussion')</script>", false);
                this.txtOtherComminutyNew.Focus();
                return;
            }

            if (othercom2.Length > 0)
            {

            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select Community Meeting 1 Key People Present')</script>", false);
                this.txtOtherComminutyNew1.Focus();
                return;
            }
            if (rblothercommTb.Checked == true || rblothercommfc.Checked == true)
            {

            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select Other Community Meeting-TB/FC')</script>", false);


                this.rblothercommTb.Focus();
                return;
            }
            if (txtvillager3.Text == "")
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please ensure the number of People Attended is more than or equal to zero')</script>", false);


                this.txtOtherComm.Focus();
                return;
            }
            if (TxtCm1_FeMale.Text == "" && TxtCm1_Male.Text != "")
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select Community Meeting 1 Female')</script>", false);
                    this.TxtCm1_FeMale.Focus();
                    return;                
            }
            if (TxtCm1_Male.Text == "")
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select  Community Meeting 1 Male')</script>", false);
                this.TxtCm1_Male.Focus();
                return;

            }

            if (Tempothercom == "8")
            {
                if (txtOtherComm.Text.ToString() == "")
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Enter Other(specify) Community Meeting 1')</script>", false);
                    this.txtOtherComm.Focus();
                    txtOtherComm.Enabled = true;
                    return;
                }
                else
                {
                    txtOtherComm.Enabled = false;
                }

            }
            if (rdEnrollment2.Checked == false && rdRetantion2.Checked == false)
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select Comm1 Enrollment or Comm1 Retantion')</script>", false);
                return;
            }
           
        }
         if (rdEnrollment2.Checked == true || rdRetantion2.Checked == true)
        {
             if (othercom.Length > 0)
            {

            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select MM Agenda')</script>", false);
                this.txtOtherComminuty.Focus();
                return;
            }

            if (othercom1.Length > 0)
            {

            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select MM Highlights of Discussion')</script>", false);
                this.txtOtherComminutyNew.Focus();
                return;
            }

            if (othercom2.Length > 0)
            {

            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select MM Key People Present')</script>", false);
                this.txtOtherComminutyNew1.Focus();
                return;
            }
        }
        if (txtvillager3.Text != "")
        {
            if (rblothercommTb.Checked == true || rblothercommfc.Checked == true)
            {

            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select Other Community Meeting-TB/FC')</script>", false);


                this.rblothercommTb.Focus();
                return;
            }
            //if (Convert.ToInt32(txtvillager3.Text) == 0)
            //{
            //    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please ensure the number of People Attended is more than zero')</script>", false);


            //    this.chkcommmetingTB.Focus();
            //    return;
            //}

            if (othercom.Length > 0)
            {
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Select Community Meeting 1 Aganda')</script>", false);

                return;
            }
        }

        //-----COm2


        string othercom11 = "";
        string Tempothercom11 = "";
        foreach (ListItem item in chk_c2.Items)
        {
            if (item.Selected)
            {

                othercom11 += "" + item.Value + "" + ",";

                if (item.Value == "8")
                {
                    Tempothercom11 = item.Value;
                }
            }

        }
        if (othercom11.Length > 0)
        {
            othercom11 = othercom11.Substring(0, othercom11.LastIndexOf(","));


            if (rblc1.Checked == true || rblc2.Checked == true)
            {

            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select Other Community Meeting2-TB/FC')</script>", false);


                this.rblothercommTb.Focus();
                return;
            }
            if (txtAtt1.Text == "")
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please enter number of people present')</script>", false);


                this.txtAtt1.Focus();
                return;
            }

            if (Tempothercom11 == "8")
            {
                if (txtoC111.Text.ToString() == "")
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Enter Other(specify) Community Meeting2')</script>", false);


                    this.txtoC111.Focus();
                    txtoC111.Enabled = true;
                    return;
                }
                else
                {
                    txtoC111.Enabled = false;
                }

            }
        }

        if (txtAtt1.Text != "")
        {
            if (rblc1.Checked == true || rblc2.Checked == true)
            {

            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select Other Community Meeting2-TB/FC')</script>", false);


                this.rblc1.Focus();
                return;
            }
            if (Convert.ToInt32(txtAtt1.Text) == 0)
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please ensure the number of People Attended is more than zero')</script>", false);


                this.txtAtt1.Focus();
                return;
            }
            if (othercom11.Length > 0)
            {
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Select Community Meeting 2 Aganda')</script>", false);

                return;
            }
        }
        //---
        string Ambition = "";
        string AmbitionComOther = "";
        foreach (ListItem item in chk_comm.Items)
        {
            if (item.Selected)
            {

                Ambition += "" + item.Value + "" + ",";
                if (item.Value == "8")
                {
                    AmbitionComOther = item.Value;
                }
            }
        }

        if (Ambition.Length > 0)
        {
            Ambition = Ambition.Substring(0, Ambition.LastIndexOf(","));



            if (rblcommtb.Checked == true || rblCommFC.Checked == true)
            {

            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Community Contact  select TB or FC')</script>", false);


                this.rblcommtb.Focus();
                return;
            }
            if (AmbitionComOther == "8")
            {
                if (txtOtherCon.Text.ToString() == "")
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Enter Other(specify) Reason')</script>", false);


                    this.txtOtherCon.Focus();
                    txtOtherCon.Enabled = true;
                    return;
                }
                else
                {
                    txtOtherCon.Enabled = false;
                }

            }
        }


        string AmbitionComm = "";
        string OtherAmbitionComm = "";

        foreach (ListItem item in chk_chkconn.Items)
        {
            if (item.Selected)
            {

                AmbitionComm += "" + item.Value + "" + ",";
                if (item.Text == "Others")
                {
                    OtherAmbitionComm = item.Value;
                }
            }

        }

        if (AmbitionComm.Length > 0)
        {
            AmbitionComm = AmbitionComm.Substring(0, AmbitionComm.LastIndexOf(","));
            if (rblcommtb.Checked == true || rblCommFC.Checked == true)
            {

            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select TB or FC')</script>", false);


                this.rblcommtb.Focus();
                return;
            }

            if (OtherAmbitionComm == "Others")
            {
                if (txt_con_other.Text.ToString() == "")
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Enter Other(specify) Community Contact')</script>", false);


                    this.txt_con_other.Focus();
                    txt_con_other.Enabled = true;
                    return;
                }
                else
                {
                    txt_con_other.Enabled = false;
                }

            }
        }

        string Suport = "";
        string SuportOther = "";

        foreach (ListItem item in chk_Suport.Items)
        {
            if (item.Selected)
            {

                Suport += "" + item.Value + "" + ",";
                if (item.Text == "Other")
                {
                    SuportOther = item.Value;
                }
            }

        }

        if (Suport.Length > 0)
        {
            Suport = Suport.Substring(0, Suport.LastIndexOf(","));

            if (rblsupportfc.Checked == true)
            {

            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select Support TB or FC')</script>", false);


                this.rblsupportfc.Focus();
                return;
            }
            if (OtherAmbitionComm == "Other")
            {
                if (txtOtherSupport.Text.ToString() == "")
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Enter Other(specify) Support')</script>", false);


                    this.txtOtherSupport.Focus();
                    txtOtherSupport.Enabled = true;
                    return;
                }
                else
                {
                    txtOtherSupport.Enabled = false;
                }

            }
        }


        Int32 TBHoldIng = 0;
        if (rblTbhold.Checked == true)
        {
            TBHoldIng = 1;
        }
        if (rblFcHold.Checked == true)
        {
            TBHoldIng = 1;
        }


        Int32 commmetingTB = 0;
        Int32 commmetingFC = 0;
        Int32 GGS = 0;
        if (chkcommmetingTB.Checked == true)
        {
            commmetingTB = 1;
            GGS = 1;
        }
        if (chkcommmetingFC.Checked == true)
        {
            commmetingFC = 1;
            GGS = 1;
        }


        Int32 c2 = 0;
        Int32 c1 = 0;
        if (rblc1.Checked == true)
        {
            c1 = 1;
        }
        if (rblc2.Checked == true)
        {
            c2 = 1;
        }

        Int32 vill1 = 0;
        if (txtV1illager.Text != "")
        {
            vill1 = Convert.ToInt32(txtV1illager.Text);
        }
        Int32 muhulaTb = 0;
        Int32 muhula55 = 0;
        Int32 muhulaFC = 0;
        if (rblmuhulaTb.Checked == true)
        {
            muhulaTb = 1;
            muhula55 = 1;
        }
        if (rblmuhulaFC.Checked == true)
        {
            muhulaFC = 1;
            muhula55 = 1;
        }


        Int32 vill2 = 0;
        if (txtVillager2.Text != "")
        {
            vill2 = Convert.ToInt32(txtVillager2.Text);
        }

        Int32 Att1 = 0;
        if (txtAtt1.Text != "")
        {
            Att1 = Convert.ToInt32(txtAtt1.Text);
        }


        Int32 othercommTb = 0;
        Int32 othercommFC = 0;

        Int32 commNew = 0;
        if (rblothercommTb.Checked == true)
        {
            othercommTb = 1;
            commNew = 1;
        }
        if (rblothercommfc.Checked == true)
        {
            othercommFC = 1;
            commNew = 1;
        }


        Int32 vill3 = 0;
        if (txtvillager3.Text != "")
        {
            vill3 = Convert.ToInt32(txtvillager3.Text);
        }

        Int32 CommFCTB = 0;
        Int32 CommFC = 0;
        Int32 Comm = 0;
        if (rblcommtb.Checked == true)
        {
            CommFCTB = 1;
            Comm = 1;
        }
        if (rblCommFC.Checked == true)
        {
            CommFC = 1;
            Comm = 1;

        }




        Int32 rolltb = 0;
        Int32 rollFC = 0;
        if (rblemrolltb.Checked == true)
        {
            rolltb = 1;
        }
        if (rblenrollFC.Checked == true)
        {
            rollFC = 1;
        }


        Int32 Supporttb = 0;
        Int32 SupportFC = 0;
        Int32 SupportCC = 0;
        //if (rblSupporttb.Checked == true)
        //{
        //    Supporttb = 1;
        //    SupportCC = 1;
        //}
        if (rblsupportfc.Checked == true)
        {
            SupportFC = 1;

            SupportCC = 1;
        }




        Int32 lotherTB = 0;
        Int32 lotherfc = 0;
        if (rblothertb.Checked == true)
        {
            lotherTB = 1;
        }
        if (rblotherfc.Checked == true)
        {
            lotherfc = 1;
        }
        #endregion
        string Dateof = txtDate.Text;
        string[] b = Dateof.Split('/');

        string FcDate = b[2] + '-' + b[1] + '-' + b[0];
        Int32 txtGSSFe = 0, txtGssMa = 0, txtMMFe = 0, txtMMMa = 0, txtComFe = 0, txtComMa = 0;
        txtGSSFe = TxtGSS_FeMale.Text == "" ? 0 : Convert.ToInt32(TxtGSS_FeMale.Text);
        txtGssMa = TxtGSS_Male.Text == "" ? 0 : Convert.ToInt32(TxtGSS_Male.Text);
        txtMMFe = TxtMM_FeMale.Text == "" ? 0 : Convert.ToInt32(TxtMM_FeMale.Text);
        txtMMMa = TxtMM_Male.Text == "" ? 0 : Convert.ToInt32(TxtMM_Male.Text);
        txtComFe = TxtCm1_FeMale.Text == "" ? 0 : Convert.ToInt32(TxtCm1_FeMale.Text);
        txtComMa = TxtCm1_Male.Text == "" ? 0 : Convert.ToInt32(TxtCm1_Male.Text);
        if (rdEnrollMent.Checked)
        {
            GssEnrollRetan = 1;
        }
        else if (rdRetention.Checked)
        {
            GssEnrollRetan = 2;
        }
        if (rdEnrollment1.Checked)
        {
            MMEnrollRetan = 1;
        }
        else if (rdRetantion1.Checked)
        { MMEnrollRetan = 2; }
        if (rdEnrollment2.Checked)
        {
            Comm1EnrollRetan = 1;
        }
        else if (rdRetantion2.Checked)
        {
            Comm1EnrollRetan = 2;
        }

        if (ViewState["GUID"].ToString().Length > 1)
        {
            string StudentTSInsertQuery = "";
            bool InsertTS = false;
            if (Session["user_level"].ToString() == "19")
            {

                StudentTSInsertQuery = " Update tblActivityUpdate_Village set  Com_Mtg='" + commNew + "',modifyBy='" + Session["username"].ToString() + "',modifyDate='" + DateTime.Now.ToString("yyyy-MM-dd") + "', ComContact='" + Comm + "', Support='" + SupportCC + "',[GSS_Mtg] ='" + GGS + "',Com_TB2=" + c1 + ",[MM_Mtg]= " + muhula55 + ",Com_FC2=" + c2 + ",Com_Agenda2='" + othercom11 + "',Com_AgendaOther2='" + txtoC111.Text + "',Any_Other2='" + txtoC1.Text + "',Com_Attended2=" + Att1 + ", Any_Other='" + tc1.Text.Trim() + "' ,TBHandholding='" + TBHoldIng + "',GSS_Attended='" + vill1 + "',Remarks='" + ddlRemark.SelectedValue + "',GSS_Agenda='" + commmeeting + "',GSSChat='" + commmeeting1 + "',GSSImportantperson='" + commmeeting2 + "',GSS_AgendaOther='" + txt_bookformatOther.Text + "',otherGSSChat='" + txt_bookformatOther1.Text + "' ,GSS_TB=" + commmetingTB + ",GSS_FC=" + commmetingFC + ",MM_Attended=" + vill2 + ",MM_Agenda='" + Muhula + "',MMChat='" + Muhula1 + "',MMImportantperson='" + Muhula2 + "',MM_AgendaOther='" + txtmOther.Text + "',othermmchat='" + txtmOther1.Text + "',MM_TB='" + muhulaTb + "',MM_FC='" + muhulaFC + "',Com_Attended='" + vill3 + "',Com_Agenda='" + othercom + "',OtherChat='" + othercom1 + "',OtherImportantperson='" + othercom2 + "',Com_AgendaOther='" + txtOtherComm.Text + "',OtherspecifyChat='" + txtOtherComm1.Text + "',Com_TB=" + othercommTb + ",Com_FC=" + othercommFC + ",ComContact_Op='" + AmbitionComm + "',ComContact_Op_Other='" + txt_con_other.Text + "',ComContact_TB='" + CommFCTB + "',ComContact_FC='" + CommFC + "',ComContact_Agenda='" + Ambition + "',ConContact_AgendaOther='" + txtOtherCon.Text + "',Support_Op='" + Suport + "',Support_Op_Other='" + txtOtherSupport.Text + "',Support_TB=" + Supporttb + ",Support_FC=" + SupportFC + ",Others_FC=" + lotherfc + ",Others_TB=" + lotherTB + ",Others_Desc='" + txtmainother.Text + "',GSSFemale=" + txtGSSFe + ",GSSMale=" + txtGssMa + ",MMFemale=" + txtMMFe + ",MMMale=" + txtMMMa + ",OtherFemale=" + txtComFe + ",OtherMale=" + txtComMa + ", GSSEnrollHault=" + GssEnrollRetan + ",MMEnrollHault=" + MMEnrollRetan + ",OtherEnrollHault=" + Comm1EnrollRetan + " where GUID_Village='" + ViewState["GUID"].ToString() + "' ";
                InsertTS = objMain.AddUpdate(StudentTSInsertQuery);
            }

            if (Session["user_level"].ToString() == "39" || Session["user_level"].ToString() == "30")
            {
                StudentTSInsertQuery = " Update tblActivityUpdate_Village set Com_Mtg='" + commNew + "', modifyBy='" + Session["username"].ToString() + "',modifyDate='" + DateTime.Now.ToString("yyyy-MM-dd") + "',ComContact='" + Comm + "',Support='" + SupportCC + "',[GSS_Mtg] ='" + GGS + "',[MM_Mtg]= " + muhula55 + ",Com_TB2=" + c1 + ",Com_FC2=" + c2 + ",Com_Agenda2='" + othercom11 + "',Com_AgendaOther2='" + txtoC111.Text + "',Any_Other2='" + txtoC1.Text + "',Com_Attended2=" + Att1 + ", Any_Other='" + tc1.Text.Trim() + "',TBHandholding='" + TBHoldIng + "',GSS_Attended='" + vill1 + "',Remarks='" + ddlRemark.SelectedValue + "',GSS_Agenda='" + commmeeting + "',GSSChat='" + commmeeting1 + "',GSSImportantperson='" + commmeeting2 + "',GSS_AgendaOther='" + txt_bookformatOther.Text + "',otherGSSChat='" + txt_bookformatOther1.Text + "',GSS_TB=" + commmetingTB + ",GSS_FC=" + commmetingFC + ",MM_Attended=" + vill2 + ",MM_Agenda='" + Muhula + "',MMChat='" + Muhula1 + "',MMImportantperson='" + Muhula2 + "',MM_AgendaOther='" + txtmOther.Text + "',othermmchat='" + txtmOther1.Text + "',MM_TB='" + muhulaTb + "',MM_FC='" + muhulaFC + "',Com_Attended='" + vill3 + "',Com_Agenda='" + othercom + "',OtherChat='" + othercom1 + "',OtherImportantperson='" + othercom2 + "',Com_AgendaOther='" + txtOtherComm.Text + "',OtherspecifyChat='" + txtOtherComm1.Text + "',Com_TB=" + othercommTb + ",Com_FC=" + othercommFC + ",ComContact_Op='" + AmbitionComm + "',ComContact_Op_Other='" + txt_con_other.Text + "',ComContact_TB='" + CommFCTB + "',ComContact_FC='" + CommFC + "',ComContact_Agenda='" + Ambition + "',ConContact_AgendaOther='" + txtOtherCon.Text + "',Support_Op='" + Suport + "',Support_Op_Other='" + txtOtherSupport.Text + "',Support_TB=" + Supporttb + ",Support_FC=" + SupportFC + ",Others_FC=" + lotherfc + ",Others_TB=" + lotherTB + ",Others_Desc='" + txtmainother.Text + "',GSSFemale=" + txtGSSFe + ",GSSMale=" + txtGssMa + ",MMFemale=" + txtMMFe + ",MMMale=" + txtMMMa + ",OtherFemale=" + txtComFe + ",OtherMale=" + txtComMa + ", GSSEnrollHault=" + GssEnrollRetan + ",MMEnrollHault=" + MMEnrollRetan + ",OtherEnrollHault=" + Comm1EnrollRetan + " where GUID_Village='" + ViewState["GUID"].ToString() + "' ";
                InsertTS = objMain.AddUpdate(StudentTSInsertQuery);
            }
            if (InsertTS == true)
            {

                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Saved sucessfully')</script>", false);

            }
        }
        else
        {
            string UNICOde = objMain.Generate_RandomString(8);
            string StudentTSInsertQuery = "";

            bool InsertTS = false;
            if (Session["user_level"].ToString() == "39" || Session["user_level"].ToString() == "30")
            {
                if (Muhula == "")
                {
                    Muhula = "0";
                }
                StudentTSInsertQuery = " INSERT INTO tblActivityUpdate_Village([VillageCode],[UserID] ,[GUID_Village] ,[ActivityDate] ,[TBHandholding], [GSS_Mtg]  ,[GSS_Attended] ,[GSS_Agenda],[GSSChat],GSSImportantperson  ,[GSS_AgendaOther],[otherGSSChat] ,[GSS_TB] ,[GSS_FC] ,      [MM_Mtg] ,[MM_Attended] ,[MM_Agenda],[MMChat],[MMImportantperson],  [MM_AgendaOther],[othermmchat],[MM_TB] ,[MM_FC] , [Com_Mtg] ,[Com_Attended] ,[Com_Agenda],[OtherChat],[OtherImportantperson],[Com_AgendaOther] ,[OtherspecifyChat],[Com_TB],[Com_FC] , [ComContact] ,[ComContact_Op] ,[ComContact_Op_Other] ,[ComContact_TB],[ComContact_FC], ComContact_Agenda,ConContact_AgendaOther,    [Support]   ,[Support_Op]  ,[Support_Op_Other] ,[Support_TB],[Support_FC]  ,[Others_FC] ,[Others_TB]  ,[Others_Desc],UserEntry,ApproveStatus,Remarks,Any_Other,Com_TB2,Com_FC2,Com_Agenda2,Com_AgendaOther2,Any_Other2,Com_Attended2,CreateBy,GSSFemale,GSSMale,MMFemale,MMMale,OtherFemale,OtherMale,GSSEnrollHault,MMEnrollHault,OtherEnrollHault) ";
                StudentTSInsertQuery += " Values('" + ddlVilage.SelectedValue + "','" + ddlUser.SelectedValue + "','" + UNICOde + "','" + Convert.ToDateTime(FcDate).ToString("yyy/MM/dd") + "','" + TBHoldIng + "','" + GGS + "','" + vill1 + "','" + commmeeting + "','" + commmeeting1 + "','" + commmeeting2 + "','" + txt_bookformatOther.Text + "','" + txt_bookformatOther1.Text + "'," + commmetingTB + "," + commmetingFC + "," + muhula55 + "," + vill2 + ",'" + Muhula + "','" + Muhula1 + "','" + Muhula2 + "','" + txtmOther.Text + "','" + txtmOther1.Text + "','" + muhulaTb + "','" + muhulaFC + "','" + commNew + "','" + vill3 + "','" + othercom + "','" + othercom1 + "','" + othercom2 + "','" + txtOtherComm.Text + "','" + txtOtherComm1.Text + "'," + othercommTb + "," + othercommFC + ",'" + Comm + "','" + AmbitionComm + "','" + txt_con_other.Text + "','" + CommFCTB + "','" + CommFC + "','" + Ambition + "','" + txtOtherCon.Text + "'," + SupportCC + ",'" + Suport + "','" + txtOtherSupport.Text + "'," + Supporttb + "," + SupportFC + "," + lotherfc + "," + lotherTB + ",'" + txtmainother.Text + "','3','B','" + ddlRemark.SelectedValue + "','" + tc1.Text.Trim() + "'," + c1 + "," + c2 + ",'" + othercom11 + "','" + txtoC111.Text + "','" + txtoC1.Text + "'," + Att1 + ",'" + Session["username"].ToString() + "','" + txtGSSFe + "," + txtGssMa + "," + txtMMFe + "," + txtMMMa + "," + txtComFe + "," + txtComMa + "," + GssEnrollRetan + "," + MMEnrollRetan + "," + Comm1EnrollRetan + ")";
                InsertTS = objMain.AddUpdate(StudentTSInsertQuery);
            }

            if (Session["user_level"].ToString() == "19")
            {
                if (Muhula == "")
                {
                    Muhula = "0";
                }

                StudentTSInsertQuery = "";
                StudentTSInsertQuery = " INSERT INTO tblActivityUpdate_Village([VillageCode],[UserID] ,[GUID_Village] ,[ActivityDate] ,[TBHandholding], [GSS_Mtg]  ,[GSS_Attended] ,[GSS_Agenda],[GSSChat],GSSImportantperson,[GSS_AgendaOther],[otherGSSChat] ,[GSS_TB] ,[GSS_FC] ,      [MM_Mtg] ,[MM_Attended] ,[MM_Agenda],[MMChat],[MMImportantperson],  [MM_AgendaOther],[othermmchat],[MM_TB] ,[MM_FC] , [Com_Mtg] ,[Com_Attended] ,[Com_Agenda],[OtherChat],[OtherImportantperson],[Com_AgendaOther], [OtherspecifyChat],[Com_TB],[Com_FC] , [ComContact] ,[ComContact_Op] ,[ComContact_Op_Other] ,[ComContact_TB],[ComContact_FC], ComContact_Agenda,ConContact_AgendaOther,    [Support]   ,[Support_Op]  ,[Support_Op_Other] ,[Support_TB],[Support_FC]  ,[Others_FC] ,[Others_TB]  ,[Others_Desc],UserEntry,ApproveStatus,Remarks,Any_Other,Com_TB2,Com_FC2,Com_Agenda2,Com_AgendaOther2,Any_Other2,Com_Attended2,CreateBy,GSSFemale,GSSMale,MMFemale,MMMale,OtherFemale,OtherMale,GSSEnrollHault,MMEnrollHault,OtherEnrollHault) ";
                StudentTSInsertQuery += " Values('" + ddlVilage.SelectedValue + "','" + ddlUser.SelectedValue + "','" + UNICOde + "','" + Convert.ToDateTime(FcDate).ToString("yyy/MM/dd") + "','" + TBHoldIng + "','" + GGS + "','" + vill1 + "','" + commmeeting + "','" + commmeeting1 + "','" + commmeeting2 + "','" + txt_bookformatOther.Text + "','" + txt_bookformatOther1.Text + "'," + commmetingTB + "," + commmetingFC + "," + muhula55 + "," + vill2 + ",'" + Muhula + "','" + Muhula1 + "','" + Muhula2 + "','" + txtmOther.Text + "','" + txtmOther1.Text + "','" + muhulaTb + "','" + muhulaFC + "','" + commNew + "','" + vill3 + "','" + othercom + "','" + othercom1 + "','" + othercom2 + "','" + txtOtherComm.Text + "','" + txtOtherComm1.Text + "'," + othercommTb + "," + othercommFC + ",'" + Comm + "','" + AmbitionComm + "','" + txt_con_other.Text + "','" + CommFCTB + "','" + CommFC + "','" + Ambition + "','" + txtOtherCon.Text + "'," + SupportCC + ",'" + Suport + "','" + txtOtherSupport.Text + "'," + Supporttb + "," + SupportFC + "," + lotherfc + "," + lotherTB + ",'" + txtmainother.Text + "','3','FC','" + ddlRemark.SelectedValue + "','" + tc1.Text.Trim() + "'," + c1 + "," + c2 + ",'" + othercom11 + "','" + txtoC111.Text + "','" + txtoC1.Text + "'," + Att1 + ",'" + Session["username"].ToString() + "'," + txtGSSFe + "," + txtGssMa + "," + txtMMFe + "," + txtMMMa + "," + txtComFe + "," + txtComMa + "," + GssEnrollRetan + "," + MMEnrollRetan + "," + Comm1EnrollRetan + ")";
                InsertTS = objMain.AddUpdate(StudentTSInsertQuery);

                StudentTSInsertQuery = "";
                StudentTSInsertQuery = " INSERT INTO tblActivityUpdate_Village([VillageCode],[UserID] ,[GUID_Village] ,[ActivityDate] ,[TBHandholding], [GSS_Mtg]  ,[GSS_Attended] ,[GSS_Agenda],[GSSChat],GSSImportantperson  ,[GSS_AgendaOther],[otherGSSChat] ,[GSS_TB] ,[GSS_FC] ,      [MM_Mtg] ,[MM_Attended] ,[MM_Agenda],[MMChat],[MMImportantperson],  [MM_AgendaOther],[othermmchat],[MM_TB] ,[MM_FC] , [Com_Mtg] ,[Com_Attended] ,[Com_Agenda],[OtherChat],[OtherImportantperson],[Com_AgendaOther] ,[OtherspecifyChat],[Com_TB],[Com_FC] , [ComContact] ,[ComContact_Op] ,[ComContact_Op_Other] ,[ComContact_TB],[ComContact_FC], ComContact_Agenda,ConContact_AgendaOther,    [Support]   ,[Support_Op]  ,[Support_Op_Other] ,[Support_TB],[Support_FC]  ,[Others_FC] ,[Others_TB]  ,[Others_Desc],UserEntry,ApproveStatus,Remarks,Any_Other,Com_TB2,Com_FC2,Com_Agenda2,Com_AgendaOther2,Any_Other2,Com_Attended2,GSSFemale,GSSMale,MMFemale,MMMale,OtherFemale,OtherMale,GSSEnrollHault,MMEnrollHault,OtherEnrollHault) ";
                StudentTSInsertQuery += " Values('" + ddlVilage.SelectedValue + "','" + ddlUser.SelectedValue + "','" + UNICOde + "','" + Convert.ToDateTime(FcDate).ToString("yyy/MM/dd") + "','" + TBHoldIng + "','" + GGS + "','" + vill1 + "','" + commmeeting + "','" + commmeeting1 + "','" + commmeeting2 + "','" + txt_bookformatOther.Text + "','" + txt_bookformatOther1.Text + "'," + commmetingTB + "," + commmetingFC + "," + muhula55 + "," + vill2 + ",'" + Muhula + "','" + Muhula1 + "','" + Muhula2 + "','" + txtmOther.Text + "','" + txtmOther1.Text + "','" + muhulaTb + "','" + muhulaFC + "','" + commNew + "','" + vill3 + "','" + othercom + "','" + othercom1 + "','" + othercom2 + "','" + txtOtherComm.Text + "','" + txtOtherComm1.Text + "'," + othercommTb + "," + othercommFC + ",'" + Comm + "','" + AmbitionComm + "','" + txt_con_other.Text + "','" + CommFCTB + "','" + CommFC + "','" + Ambition + "','" + txtOtherCon.Text + "'," + SupportCC + ",'" + Suport + "','" + txtOtherSupport.Text + "'," + Supporttb + "," + SupportFC + "," + lotherfc + "," + lotherTB + ",'" + txtmainother.Text + "','2','FC','" + ddlRemark.SelectedValue + "','" + tc1.Text.Trim() + "'," + c1 + "," + c2 + ",'" + othercom11 + "','" + txtoC111.Text + "','" + txtoC1.Text + "'," + Att1 + "," + txtGSSFe + "," + txtGssMa + "," + txtMMFe + "," + txtMMMa + "," + txtComFe + "," + txtComMa + "," + GssEnrollRetan + "," + MMEnrollRetan + "," + Comm1EnrollRetan + ")";
                InsertTS = objMain.AddUpdate(StudentTSInsertQuery);
            }
            if (InsertTS == true)
            {


                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Saved sucessfully')</script>", false);
                ViewState["GUID"] = UNICOde;
            }
        }

    }


    protected void btnImgMM_Click(object sender, EventArgs e)
    {

        //  imgMKS.ImageUrl = ResolveUrl("~/TabletImage/" +  lblMM.Text);
        //imgMKS.ImageUrl = Server.MapPath("~/TabletImage/" + lblMM.Text);
        imgMKS.ImageUrl = "TabletImage/" + lblMM.Text;
        MpexdrDistrict.Show();

    }
    protected void btnImgGss_Click(object sender, EventArgs e)
    {


        //  imgMKS.ImageUrl = Server.MapPath("~/TabletImage/" + lblGG.Text);
        imgMKS.ImageUrl = "TabletImage/" + lblGG.Text;

        //     imgMKS.ImageUrl = ResolveUrl("~/TabletImage/" + lblGG.Text);

        MpexdrDistrict.Show();
    }
    protected void btnimgComm1_Click(object sender, EventArgs e)
    {
        //imgMKS.ImageUrl = Server.MapPath("~/TabletImage/" + lblCom.Text);
        imgMKS.ImageUrl = "TabletImage/" + lblCom.Text;
        //imgMKS.ImageUrl = ResolveUrl("~/TabletImage/" + lblCom.Text);
        MpexdrDistrict.Show();
    }
    protected void btnimgComm2_Click(object sender, EventArgs e)
    {
        // imgMKS.ImageUrl = Server.MapPath("~/TabletImage/" + lblCom1.Text);
        imgMKS.ImageUrl = "TabletImage/" + lblCom1.Text;
        //imgMKS.ImageUrl = ResolveUrl("~/TabletImage/" +  lblCom1.Text);
        MpexdrDistrict.Show();
    }

    protected void btnD2dSerachNew_Click(object sender, EventArgs e)
    {
        DataTable dataTable = this.Session["D2dBindChild"] as DataTable;
        string rowFilter = "1=1 ";
        DataTable dataTable111 = dataTable.Copy();
        if (this.ddlStatusSearch.SelectedIndex > 0)
        {

            if (Convert.ToInt32(this.ddlStatusSearch.SelectedValue) == 1)
            {
                string str = "UniqueIdNew";
                DataTable dataTable2 = dataTable.Copy();
                rowFilter += " and " + str + " like '%" + this.txtSearchNew.Text.Trim() + "%'   ";
                dataTable2.DefaultView.RowFilter = rowFilter;
                dataTable2.DefaultView.Sort = "UniqueIdNew asc";
                //Gv_DisplayNew.DataSource = dataTable2.DefaultView.ToTable();
                //Gv_DisplayNew.DataBind();
            }
            if (Convert.ToInt32(this.ddlStatusSearch.SelectedValue) == 2)
            {
                string str2 = "HHNo";
                DataTable dataTable3 = dataTable.Copy();
                rowFilter += " and " + str2 + " like '%" + this.txtSearchNew.Text.Trim() + "%'   ";
                dataTable3.DefaultView.RowFilter = rowFilter;
                dataTable3.DefaultView.Sort = "HHNo asc";
                //Gv_DisplayNew.DataSource = dataTable3.DefaultView.ToTable();
                //Gv_DisplayNew.DataBind();
            }

            if (Convert.ToInt32(this.ddlStatusSearch.SelectedValue) == 3)
            {
                string str2 = "ChildName";
                DataTable dataTable3 = dataTable.Copy();
                rowFilter += " and " + str2 + " like '%" + this.txtSearchNew.Text.Trim() + "%'   ";
                //dataTable3.DefaultView.RowFilter = rowFilter;
                //dataTable3.DefaultView.Sort = "ChildName asc";
                //Gv_DisplayNew.DataSource = dataTable3.DefaultView.ToTable();
                //Gv_DisplayNew.DataBind();
            }

            if (Convert.ToInt32(this.ddlStatusSearch.SelectedValue) == 4)
            {
                string str2 = "FathersName";
                DataTable dataTable3 = dataTable.Copy();
                rowFilter += " and " + str2 + " like '%" + this.txtSearchNew.Text.Trim() + "%'   ";
                //dataTable3.DefaultView.RowFilter = rowFilter;
                //dataTable3.DefaultView.Sort = "FathersName asc";
                //Gv_DisplayNew.DataSource = dataTable3.DefaultView.ToTable();
                //Gv_DisplayNew.DataBind();
            }

        }
        if (txtFdate.Text != "" && TxtToDate.Text != "")
        {
            string str2 = "ActivityDate";
            DataTable dataTable3 = dataTable.Copy();
            rowFilter += " and " + str2 + " >= '" + this.txtFdate.Text.Trim() + "' and  ActivityDate<='" + this.TxtToDate.Text.Trim() + "'  ";
            //dataTable3.DefaultView.RowFilter = rowFilter;
            //dataTable3.DefaultView.Sort = "FathersName asc";
            //Gv_DisplayNew.DataSource = dataTable3.DefaultView.ToTable();
            //Gv_DisplayNew.DataBind();
        }


        if (this.ddlSearchEnroll.SelectedIndex > 0)
        {
            ddlSubContact.Visible = true;
            if (Convert.ToInt32(this.ddlSearchEnroll.SelectedValue) == 1)
            {
                string str = "ActivityStatus";
                DataTable dataTable2 = dataTable.Copy();
                rowFilter += " and " + str + "  ='" + ddlSearchEnroll.SelectedValue + "'   ";
                //dataTable2.DefaultView.RowFilter = rowFilter;
                //dataTable2.DefaultView.Sort = "ActivityStatus asc";
                //Gv_DisplayNew.DataSource = dataTable2.DefaultView.ToTable();
                //Gv_DisplayNew.DataBind();
                objComman.BindDLL("mstLookup", "LookupCode,Description", "[LookupFlag]='MFo' ", "LookupCode", "asc", ddlSubContact, "Description", "LookupCode", "Select");


            }
            if (Convert.ToInt32(this.ddlSearchEnroll.SelectedValue) == 2)
            {
                string str2 = "ActivityStatus";
                DataTable dataTable3 = dataTable.Copy();
                rowFilter += " and " + str2 + " = '" + ddlSearchEnroll.SelectedValue + "'   ";
                dataTable3.DefaultView.RowFilter = rowFilter;
                dataTable3.DefaultView.Sort = "ActivityStatus asc";
                //Gv_DisplayNew.DataSource = dataTable3.DefaultView.ToTable();
                //Gv_DisplayNew.DataBind();
                ddlSubContact.Visible = false;
            }
            if (Convert.ToInt32(this.ddlSearchEnroll.SelectedValue) == 3)
            {
                string str2 = "ActivityStatus";
                DataTable dataTable3 = dataTable.Copy();
                rowFilter += " and " + str2 + " = '" + ddlSearchEnroll.SelectedValue + "'   ";
                //dataTable3.DefaultView.RowFilter = rowFilter;
                //dataTable3.DefaultView.Sort = "HHNo asc";
                //Gv_DisplayNew.DataSource = dataTable3.DefaultView.ToTable();
                //Gv_DisplayNew.DataBind();

            }
        }
        if (this.ddlSubContact.SelectedIndex > 0)
        {
            if (Convert.ToInt32(this.ddlSearchEnroll.SelectedValue) == 1)
            {
                if (Convert.ToInt32(this.ddlSubContact.SelectedValue) == 1)
                {
                    string str = "FollowUPID";
                    DataTable dataTable2 = dataTable.Copy();
                    rowFilter += " and " + str + "  ='" + ddlSearchEnroll.SelectedValue + "'   ";
                    //dataTable2.DefaultView.RowFilter = rowFilter;
                    //dataTable2.DefaultView.Sort = "ActivityStatus asc";
                    //Gv_DisplayNew.DataSource = dataTable2.DefaultView.ToTable();
                    //Gv_DisplayNew.DataBind();
                    objComman.BindDLL("mstLookup", "LookupCode,Description", "[LookupFlag]='MFo' ", "LookupCode", "asc", ddlSubContact, "Description", "LookupCode", "Select");


                }
            }
            else
            {
                if (Convert.ToInt32(this.ddlSearchEnroll.SelectedValue) == 2)
                {
                    string str2 = "VillageOptionID";
                    DataTable dataTable3 = dataTable.Copy();
                    rowFilter += " and " + str2 + " =1 ";
                    //dataTable3.DefaultView.RowFilter = rowFilter;
                    //dataTable3.DefaultView.Sort = "ActivityStatus asc";
                    //Gv_DisplayNew.DataSource = dataTable3.DefaultView.ToTable();
                    //Gv_DisplayNew.DataBind();
                    ddlSubContact.Visible = false;
                }
                if (Convert.ToInt32(this.ddlSearchEnroll.SelectedValue) == 2)
                {
                    string str2 = "FromPanding6";
                    DataTable dataTable3 = dataTable.Copy();
                    rowFilter += " and " + str2 + " > 0 ";
                    //dataTable3.DefaultView.RowFilter = rowFilter;
                    //dataTable3.DefaultView.Sort = "HHNo asc";
                    //Gv_DisplayNew.DataSource = dataTable3.DefaultView.ToTable();
                    //Gv_DisplayNew.DataBind();
                }
            }




        }
        dataTable111.DefaultView.RowFilter = rowFilter;
        dataTable111.DefaultView.Sort = "HHNo asc";
        Gv_DisplayNew.DataSource = dataTable111.DefaultView.ToTable();
        Gv_DisplayNew.DataBind();
        this.ModalPopupExtender2.Show();
    }
    protected void ddlSearchEnroll_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataTable dataTable = this.Session["D2dBindChild"] as DataTable;
        string rowFilter = "1=1 ";
        DataTable dataTable111 = dataTable.Copy();
        if (this.ddlStatusSearch.SelectedIndex > 0)
        {

            if (Convert.ToInt32(this.ddlStatusSearch.SelectedValue) == 1)
            {
                string str = "UniqueIdNew";
                DataTable dataTable2 = dataTable.Copy();
                rowFilter += " and " + str + " like '%" + this.txtSearchNew.Text.Trim() + "%'   ";
                dataTable2.DefaultView.RowFilter = rowFilter;
                dataTable2.DefaultView.Sort = "UniqueIdNew asc";
                //Gv_DisplayNew.DataSource = dataTable2.DefaultView.ToTable();
                //Gv_DisplayNew.DataBind();
            }
            if (Convert.ToInt32(this.ddlStatusSearch.SelectedValue) == 2)
            {
                string str2 = "HHNo";
                DataTable dataTable3 = dataTable.Copy();
                rowFilter += " and " + str2 + " like '%" + this.txtSearchNew.Text.Trim() + "%'   ";
                dataTable3.DefaultView.RowFilter = rowFilter;
                dataTable3.DefaultView.Sort = "HHNo asc";
                //Gv_DisplayNew.DataSource = dataTable3.DefaultView.ToTable();
                //Gv_DisplayNew.DataBind();
            }

            if (Convert.ToInt32(this.ddlStatusSearch.SelectedValue) == 3)
            {
                string str2 = "ChildName";
                DataTable dataTable3 = dataTable.Copy();
                rowFilter += " and " + str2 + " like '%" + this.txtSearchNew.Text.Trim() + "%'   ";
                //dataTable3.DefaultView.RowFilter = rowFilter;
                //dataTable3.DefaultView.Sort = "ChildName asc";
                //Gv_DisplayNew.DataSource = dataTable3.DefaultView.ToTable();
                //Gv_DisplayNew.DataBind();
            }

            if (Convert.ToInt32(this.ddlStatusSearch.SelectedValue) == 4)
            {
                string str2 = "FathersName";
                DataTable dataTable3 = dataTable.Copy();
                rowFilter += " and " + str2 + " like '%" + this.txtSearchNew.Text.Trim() + "%'   ";
                //dataTable3.DefaultView.RowFilter = rowFilter;
                //dataTable3.DefaultView.Sort = "FathersName asc";
                //Gv_DisplayNew.DataSource = dataTable3.DefaultView.ToTable();
                //Gv_DisplayNew.DataBind();
            }

        }
        if (txtFdate.Text != "" && TxtToDate.Text != "")
        {
            string str2 = "ActivityDate";
            DataTable dataTable3 = dataTable.Copy();
            rowFilter += " and " + str2 + " >= '" + this.txtFdate.Text.Trim() + "' and  ActivityDate<='" + this.TxtToDate.Text.Trim() + "'  ";
            //dataTable3.DefaultView.RowFilter = rowFilter;
            //dataTable3.DefaultView.Sort = "FathersName asc";
            //Gv_DisplayNew.DataSource = dataTable3.DefaultView.ToTable();
            //Gv_DisplayNew.DataBind();
        }


        if (this.ddlSearchEnroll.SelectedIndex > 0)
        {
            ddlSubContact.Visible = true;
            if (Convert.ToInt32(this.ddlSearchEnroll.SelectedValue) == 1)
            {
                string str = "ActivityStatus";
                DataTable dataTable2 = dataTable.Copy();
                rowFilter += " and " + str + "  ='" + ddlSearchEnroll.SelectedValue + "'   ";
                //dataTable2.DefaultView.RowFilter = rowFilter;
                //dataTable2.DefaultView.Sort = "ActivityStatus asc";
                //Gv_DisplayNew.DataSource = dataTable2.DefaultView.ToTable();
                //Gv_DisplayNew.DataBind();
                objComman.BindDLL("mstLookup", "LookupCode,Description", "[LookupFlag]='MFo' ", "LookupCode", "asc", ddlSubContact, "Description", "LookupCode", "Select");


            }
            if (Convert.ToInt32(this.ddlSearchEnroll.SelectedValue) == 2)
            {
                string str2 = "ActivityStatus";
                DataTable dataTable3 = dataTable.Copy();
                rowFilter += " and " + str2 + " = '" + ddlSearchEnroll.SelectedValue + "'   ";
                dataTable3.DefaultView.RowFilter = rowFilter;
                dataTable3.DefaultView.Sort = "ActivityStatus asc";
                //Gv_DisplayNew.DataSource = dataTable3.DefaultView.ToTable();
                //Gv_DisplayNew.DataBind();
                ddlSubContact.Visible = false;
            }
            if (Convert.ToInt32(this.ddlSearchEnroll.SelectedValue) == 3)
            {
                string str2 = "ActivityStatus";
                DataTable dataTable3 = dataTable.Copy();
                rowFilter += " and " + str2 + " = '" + ddlSearchEnroll.SelectedValue + "'   ";
                //dataTable3.DefaultView.RowFilter = rowFilter;
                //dataTable3.DefaultView.Sort = "HHNo asc";
                //Gv_DisplayNew.DataSource = dataTable3.DefaultView.ToTable();
                //Gv_DisplayNew.DataBind();

            }
        }
        if (this.ddlSubContact.SelectedIndex > 0)
        {
            if (Convert.ToInt32(this.ddlSearchEnroll.SelectedValue) == 1)
            {
                if (Convert.ToInt32(this.ddlSubContact.SelectedValue) == 1)
                {
                    string str = "FollowUPID";
                    DataTable dataTable2 = dataTable.Copy();
                    rowFilter += " and " + str + "  ='" + ddlSearchEnroll.SelectedValue + "'   ";
                    //dataTable2.DefaultView.RowFilter = rowFilter;
                    //dataTable2.DefaultView.Sort = "ActivityStatus asc";
                    //Gv_DisplayNew.DataSource = dataTable2.DefaultView.ToTable();
                    //Gv_DisplayNew.DataBind();
                    objComman.BindDLL("mstLookup", "LookupCode,Description", "[LookupFlag]='MFo' ", "LookupCode", "asc", ddlSubContact, "Description", "LookupCode", "Select");


                }
            }
            else
            {
                if (Convert.ToInt32(this.ddlSearchEnroll.SelectedValue) == 2)
                {
                    string str2 = "VillageOptionID";
                    DataTable dataTable3 = dataTable.Copy();
                    rowFilter += " and " + str2 + " =1 ";
                    //dataTable3.DefaultView.RowFilter = rowFilter;
                    //dataTable3.DefaultView.Sort = "ActivityStatus asc";
                    //Gv_DisplayNew.DataSource = dataTable3.DefaultView.ToTable();
                    //Gv_DisplayNew.DataBind();
                    ddlSubContact.Visible = false;
                }
                if (Convert.ToInt32(this.ddlSearchEnroll.SelectedValue) == 2)
                {
                    string str2 = "FromPanding6";
                    DataTable dataTable3 = dataTable.Copy();
                    rowFilter += " and " + str2 + " > 0 ";
                    //dataTable3.DefaultView.RowFilter = rowFilter;
                    //dataTable3.DefaultView.Sort = "HHNo asc";
                    //Gv_DisplayNew.DataSource = dataTable3.DefaultView.ToTable();
                    //Gv_DisplayNew.DataBind();
                }
            }




        }
        dataTable111.DefaultView.RowFilter = rowFilter;
        dataTable111.DefaultView.Sort = "HHNo asc";
        Gv_DisplayNew.DataSource = dataTable111.DefaultView.ToTable();
        Gv_DisplayNew.DataBind();
        this.ModalPopupExtender2.Show();

    }




    protected void ddlSubContact_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataTable dataTable = this.Session["D2dBindChild"] as DataTable;
        string rowFilter = "1=1 ";
        DataTable dataTable111 = dataTable.Copy();
        if (this.ddlStatusSearch.SelectedIndex > 0)
        {

            if (Convert.ToInt32(this.ddlStatusSearch.SelectedValue) == 1)
            {
                string str = "UniqueIdNew";
                DataTable dataTable2 = dataTable.Copy();
                rowFilter += " and " + str + " like '%" + this.txtSearchNew.Text.Trim() + "%'   ";
                dataTable2.DefaultView.RowFilter = rowFilter;
                dataTable2.DefaultView.Sort = "UniqueIdNew asc";
                //Gv_DisplayNew.DataSource = dataTable2.DefaultView.ToTable();
                //Gv_DisplayNew.DataBind();
            }
            if (Convert.ToInt32(this.ddlStatusSearch.SelectedValue) == 2)
            {
                string str2 = "HHNo";
                DataTable dataTable3 = dataTable.Copy();
                rowFilter += " and " + str2 + " like '%" + this.txtSearchNew.Text.Trim() + "%'   ";
                dataTable3.DefaultView.RowFilter = rowFilter;
                dataTable3.DefaultView.Sort = "HHNo asc";
                //Gv_DisplayNew.DataSource = dataTable3.DefaultView.ToTable();
                //Gv_DisplayNew.DataBind();
            }

            if (Convert.ToInt32(this.ddlStatusSearch.SelectedValue) == 3)
            {
                string str2 = "ChildName";
                DataTable dataTable3 = dataTable.Copy();
                rowFilter += " and " + str2 + " like '%" + this.txtSearchNew.Text.Trim() + "%'   ";
                //dataTable3.DefaultView.RowFilter = rowFilter;
                //dataTable3.DefaultView.Sort = "ChildName asc";
                //Gv_DisplayNew.DataSource = dataTable3.DefaultView.ToTable();
                //Gv_DisplayNew.DataBind();
            }

            if (Convert.ToInt32(this.ddlStatusSearch.SelectedValue) == 4)
            {
                string str2 = "FathersName";
                DataTable dataTable3 = dataTable.Copy();
                rowFilter += " and " + str2 + " like '%" + this.txtSearchNew.Text.Trim() + "%'   ";
                //dataTable3.DefaultView.RowFilter = rowFilter;
                //dataTable3.DefaultView.Sort = "FathersName asc";
                //Gv_DisplayNew.DataSource = dataTable3.DefaultView.ToTable();
                //Gv_DisplayNew.DataBind();
            }

        }
        if (txtFdate.Text != "" && TxtToDate.Text != "")
        {
            string str2 = "ActivityDate";
            DataTable dataTable3 = dataTable.Copy();
            rowFilter += " and " + str2 + " >= '" + this.txtFdate.Text.Trim() + "' and  ActivityDate<='" + this.TxtToDate.Text.Trim() + "'  ";
            //dataTable3.DefaultView.RowFilter = rowFilter;
            //dataTable3.DefaultView.Sort = "FathersName asc";
            //Gv_DisplayNew.DataSource = dataTable3.DefaultView.ToTable();
            //Gv_DisplayNew.DataBind();
        }


        if (this.ddlSearchEnroll.SelectedIndex > 0)
        {
            ddlSubContact.Visible = true;
            if (Convert.ToInt32(this.ddlSearchEnroll.SelectedValue) == 1)
            {
                string str = "ActivityStatus";
                DataTable dataTable2 = dataTable.Copy();
                rowFilter += " and " + str + "  ='" + ddlSearchEnroll.SelectedValue + "'   ";
                //dataTable2.DefaultView.RowFilter = rowFilter;
                //dataTable2.DefaultView.Sort = "ActivityStatus asc";
                //Gv_DisplayNew.DataSource = dataTable2.DefaultView.ToTable();
                //Gv_DisplayNew.DataBind();
                objComman.BindDLL("mstLookup", "LookupCode,Description", "[LookupFlag]='MFo' ", "LookupCode", "asc", ddlSubContact, "Description", "LookupCode", "Select");


            }
            if (Convert.ToInt32(this.ddlSearchEnroll.SelectedValue) == 2)
            {
                string str2 = "ActivityStatus";
                DataTable dataTable3 = dataTable.Copy();
                rowFilter += " and " + str2 + " = '" + ddlSearchEnroll.SelectedValue + "'   ";
                dataTable3.DefaultView.RowFilter = rowFilter;
                dataTable3.DefaultView.Sort = "ActivityStatus asc";
                //Gv_DisplayNew.DataSource = dataTable3.DefaultView.ToTable();
                //Gv_DisplayNew.DataBind();
                ddlSubContact.Visible = false;
            }
            if (Convert.ToInt32(this.ddlSearchEnroll.SelectedValue) == 3)
            {
                string str2 = "ActivityStatus";
                DataTable dataTable3 = dataTable.Copy();
                rowFilter += " and " + str2 + " = '" + ddlSearchEnroll.SelectedValue + "'   ";
                //dataTable3.DefaultView.RowFilter = rowFilter;
                //dataTable3.DefaultView.Sort = "HHNo asc";
                //Gv_DisplayNew.DataSource = dataTable3.DefaultView.ToTable();
                //Gv_DisplayNew.DataBind();

            }
        }
        if (this.ddlSubContact.SelectedIndex > 0)
        {
            if (Convert.ToInt32(this.ddlSearchEnroll.SelectedValue) == 1)
            {
                if (Convert.ToInt32(this.ddlSubContact.SelectedValue) == 1)
                {
                    string str = "FollowUPID";
                    DataTable dataTable2 = dataTable.Copy();
                    rowFilter += " and " + str + "  ='" + ddlSearchEnroll.SelectedValue + "'   ";
                    //dataTable2.DefaultView.RowFilter = rowFilter;
                    //dataTable2.DefaultView.Sort = "ActivityStatus asc";
                    //Gv_DisplayNew.DataSource = dataTable2.DefaultView.ToTable();
                    //Gv_DisplayNew.DataBind();
                    objComman.BindDLL("mstLookup", "LookupCode,Description", "[LookupFlag]='MFo' ", "LookupCode", "asc", ddlSubContact, "Description", "LookupCode", "Select");


                }
            }
            else
            {
                if (Convert.ToInt32(this.ddlSearchEnroll.SelectedValue) == 2)
                {
                    string str2 = "VillageOptionID";
                    DataTable dataTable3 = dataTable.Copy();
                    rowFilter += " and " + str2 + " =1 ";
                    //dataTable3.DefaultView.RowFilter = rowFilter;
                    //dataTable3.DefaultView.Sort = "ActivityStatus asc";
                    //Gv_DisplayNew.DataSource = dataTable3.DefaultView.ToTable();
                    //Gv_DisplayNew.DataBind();
                    ddlSubContact.Visible = false;
                }
                if (Convert.ToInt32(this.ddlSearchEnroll.SelectedValue) == 2)
                {
                    string str2 = "FromPanding6";
                    DataTable dataTable3 = dataTable.Copy();
                    rowFilter += " and " + str2 + " > 0 ";
                    //dataTable3.DefaultView.RowFilter = rowFilter;
                    //dataTable3.DefaultView.Sort = "HHNo asc";
                    //Gv_DisplayNew.DataSource = dataTable3.DefaultView.ToTable();
                    //Gv_DisplayNew.DataBind();
                }
            }




        }
        dataTable111.DefaultView.RowFilter = rowFilter;
        dataTable111.DefaultView.Sort = "HHNo asc";
        Gv_DisplayNew.DataSource = dataTable111.DefaultView.ToTable();
        Gv_DisplayNew.DataBind();
        this.ModalPopupExtender2.Show();

    }

    protected void lnkListStaus_OnClick(object sender, EventArgs e)
    {

        SqlParameter[] parm = new SqlParameter[]
             {
               new SqlParameter("@Villagecode",  ddlVilage.SelectedValue),
               new SqlParameter("@Fyear",  Session["FinYear"].ToString()),
         
      
                 };

        DataTable dtUserVillage = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rptLoadBundal]", parm);
        Session["D2dBindChild"] = dtUserVillage;
        Gv_DisplayNew.DataSource = dtUserVillage;
        Gv_DisplayNew.DataBind();
        ddlSearchEnroll.SelectedIndex = 0;

        ddlStatusSearch.SelectedIndex = 0;
        txtSearchNew.Text = "";

        this.ModalPopupExtender2.Show();
    }
    public void loadDataDropdown()
    {
        DataTable dtYear = CreateDataTable();
        DataRow dr;



        dr = dtYear.NewRow();
        dr["Type"] = "School wise enrollment";
        dr["ID"] = 1;
        dtYear.Rows.Add(dr);
        dr = dtYear.NewRow();
        dr["Type"] = "From 6 Status";
        dr["ID"] = 2;
        dtYear.Rows.Add(dr);
        //get last  two digits (eg: 10 from 2010);
        objComman.BindDLLMasterTable("mstSchool", "Type,ID", dtYear, conditions, "Type", "asc", ddlSubContact, "Type", "ID", "Select");



    }
    public DataTable CreateDataTable()
    {

        DataTable dtYear = new DataTable();
        dtYear.Columns.Add("Type", System.Type.GetType("System.String"));

        dtYear.Columns.Add("ID", System.Type.GetType("System.Int32"));
        return dtYear;
    }
    protected void btnEditUndo_Click(object sender, EventArgs e)
    {

        Button ddlLabTest1 = (Button)sender;
        GridViewRow row1 = (GridViewRow)ddlLabTest1.NamingContainer;

        string UNICOde = objMain.Generate_RandomString(15);

        Label lbUniqueCode11 = (Label)row1.FindControl("lbUniqueCode11");
        Label lblGUIDDTDMobileActivity = (Label)row1.FindControl("lblGUIDDTDMobileActivity");
        Label lblActivityStatus1 = (Label)row1.FindControl("lblActivityStatus1");

        SqlParameter[] cmdParameters = new SqlParameter[]
                    {
                          new SqlParameter("@GUIDDTDMobileActivity", lblGUIDDTDMobileActivity.Text ),
                        new SqlParameter("@UniqueCode", lbUniqueCode11.Text ),
                        new SqlParameter("@ActivityStatus", lblActivityStatus1.Text),
                         new SqlParameter("@NewGuid", UNICOde),
                     
                       
                    };
        Int32 icount = SqlHelper.ExecuteNonQuery(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "UpdateContactChild", cmdParameters);

        if (icount > 0)
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Saved sucessfully')</script>", false);
            ModalPopupExtender.Show();
        }
        lnkListStaus_OnClick(lnkEnrool, null);
    }
}