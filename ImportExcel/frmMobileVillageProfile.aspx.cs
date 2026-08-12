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
                            CalendarExtenderTourdate.StartDate = Convert.ToDateTime(dt.Rows[0]["ActivityDate"].ToString()).AddDays(1);
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
                            CalendarExtenderTourdate.StartDate = Convert.ToDateTime(dt.Rows[0]["ActivityDate"].ToString()).AddDays(1);
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

        foreach (ListItem item in CBL_bookformat.Items)
        {

            item.Selected = false;


        }


        txt_pbname.Text = "";
        txtmOther.Text = "";
        txtmainother.Text = "";
        txt_bookformatOther.Text = "";
        chkmcommmeting.Checked = false;


        chkcommmetingTB.Checked = false;

        chkcommmetingFC.Checked = false;

        txtV1illager.Text = "";
        txt_bookformatOther.Text = "";


    }

    protected void btnmm_Click(object sender, EventArgs e)
    {


        foreach (ListItem item in CBL_Muhula.Items)
        {

            item.Selected = false;


        }


        txtMuhala.Text = "";
        chkmuhala.Checked = false;


        rblmuhulaTb.Checked = false;

        rblmuhulaFC.Checked = false;

        txtVillager2.Text = "";
        txtmOther.Text = "";



    }


    protected void btnOther_Click(object sender, EventArgs e)
    {


        foreach (ListItem item in chk_othercom.Items)
        {

            item.Selected = false;

        }
       
        tc1.Text = "";
        txtOtherComminuty.Text = "";
        chkothercomm.Checked = false;

        rblothercommTb.Checked = false;

        rblothercommfc.Checked = false;

        txtvillager3.Text = "";
        txtOtherComm.Text = "";



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

            //conditions = "mst5Village.ClusterCode in('" + strVillage + "') ";

            ////objComman.BindDLL("mst5Village", "VillageCode,VillageName ", conditions, "VillageName", "", ddlVilage, "VillageName", "VillageCode", "Select");
            //strQry = "   select Villagecode  from MstUser   where UserName='" + ddlUser.SelectedValue + "' ";

            //DataTable dtUserVillage = objMain.LoadData(strQry);

            //string strVillage = dtUserVillage.Rows[0]["Villagecode"].ToString();

            conditions = "mst5Village.ClusterCode in('" + strVillage + "') ";

            strQry = "";
            strQry = "select VillageCode,VillageName  from mst5Village where mst5Village.ClusterCode in('" + strVillage + "')  and len(mst5Village.ClusterCode)>2    ";
            strQry += " Union select VillageCode,VillageName  from mstActivityVillage where UserID='" + ddlUser.SelectedValue + "'   ";
            strQry += " Union ";
            strQry += "  select mst5Village.VillageCode,VillageName  from mst5Village  ";
            strQry += " inner join tblActivityUpdate_Village on tblActivityUpdate_Village.VillageCode=mst5Village.VillageCode  ";
            strQry += "  where mst5Village.ClusterCode in('" + Session["Cluseter"].ToString() + "' )   and UserID='" + ddlUser.SelectedValue + "'   ";
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
            txtmOther.Text = "";
            txtmainother.Text = "";
            txt_bookformatOther.Text = "";
            chkmcommmeting.Checked = false;
       
       
            chkcommmetingTB.Checked = false;

            chkcommmetingFC.Checked = false;
     
            txtV1illager.Text = "";
            txt_bookformatOther.Text ="";



            foreach (ListItem item in CBL_Muhula.Items)
            {
               
                    item.Selected = false;
                   
              
            }
       
       
            txtMuhala.Text = "";
            chkmuhala.Checked = false;


            rblmuhulaTb.Checked = false;

            rblmuhulaFC.Checked = false;

            txtVillager2.Text = "";
            txtmOther.Text = "";



             foreach (ListItem item in chk_othercom.Items)
            {
                
                    item.Selected = false;
                 
            }
      
      
            txtOtherComminuty.Text = "";
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

            txtOtherComm.Enabled = false;
            txtmOther.Enabled = false;
            txt_bookformatOther.Enabled = false;
            txtOtherSupport.Enabled = false;
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
            if (dtVillageActivtiy.Rows[0]["Remarks"].ToString().Length > 0)
            {
                ddlRemark.SelectedValue = dtVillageActivtiy.Rows[0]["Remarks"].ToString();
            }
            if (dtVillageActivtiy.Rows[0]["PhotoGSS"].ToString().Length > 0)
            {
                lblGG.Text= dtVillageActivtiy.Rows[0]["PhotoGSS"].ToString();
             
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
            txt_bookformatOther.Text = dtVillageActivtiy.Rows[0]["GSS_AgendaOther"].ToString();
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

            string[] Com_Agenda3 = Com_Agenda2.Split(',');
            string Com_Agendamm3 = "";
            foreach (string s in Com_Agenda3)
            {
                foreach (ListItem item in chk_c2.Items)
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
                txtOtherCC1.Text = Com_Agendamm3;
               
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
        ddlRemark.SelectedIndex = 0;
    }
    protected void Gv_Display_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DropDownList ddlStatus = ((DropDownList)e.Row.FindControl("ddlStatus"));
            Label lbStatus = ((Label)e.Row.FindControl("lbStatus"));
            ddlStatus.SelectedValue =lbStatus.Text;
            
        }
    }
    protected void ddlStatus_SelectedIndexChanged(object sender, EventArgs e)
    {

        DropDownList ddlLabTest1 = (DropDownList)sender;
        GridViewRow row1 = (GridViewRow)ddlLabTest1.NamingContainer;

        DropDownList ddlStatus = (DropDownList)row1.FindControl("ddlStatus");




        Label lbStatus = (Label)row1.FindControl("lbStatus");



        lbStatus.Text = "2";




        ModalPopupExtender.Show();
    }
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
        for (int i = 0; i < Gv_Display.Rows.Count; i++)
        {
            DropDownList ddlStatus = ((DropDownList)Gv_Display.Rows[i].FindControl("ddlStatus"));
            Label lbUniqueCode = ((Label)Gv_Display.Rows[i].FindControl("lbUniqueCode"));
            Label lblStatus = ((Label)Gv_Display.Rows[i].FindControl("lbStatus"));

            if (lblStatus.Text == "2")
            {

                string StudentTSInsertQueryD2d = "";
                StudentTSInsertQueryD2d += " Update tblActivityDTD set ActivityStatus =" + ddlStatus.SelectedValue + ",UserType='P' , ActivityDate ='" + DateTime.Now.ToString("yyyy-MM-dd") + "' where UniqueCode ='" + lbUniqueCode.Text + "' ";
                InsertD2d = objMain.AddUpdate(StudentTSInsertQueryD2d);
            }
        }
        if (InsertD2d == true)
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Saved sucessfully')</script>", false);

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

        #region Variable
        string commmeeting = "";
        string commOther = "";
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
        if (commmeeting.Length > 0)
        {
            commmeeting = commmeeting.Substring(0, commmeeting.LastIndexOf(","));

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
            if (txtV1illager.Text == "")
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please ensure People Attended is more than zero')</script>", false);


                this.txtV1illager.Focus();
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
            if (Convert.ToInt32(txtV1illager.Text)==0)
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please ensure People Attended is more than zero')</script>", false);


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
        string TempMuhulaOther = "";
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


        if (Muhula.Length > 0)
        {
            Muhula = Muhula.Substring(0, Muhula.LastIndexOf(","));

          

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
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please enter number of villagers present')</script>", false);


                this.txtVillager2.Focus();
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
        }

        if (txtVillager2.Text != "")
        {
        
            if (Convert.ToInt32(txtVillager2.Text) == 0)
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please ensure People Attended is more than zero')</script>", false);


                this.chkcommmetingTB.Focus();
                return;
            }

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
        string Tempothercom = "";
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
        if (othercom.Length > 0)
        {
            othercom = othercom.Substring(0, othercom.LastIndexOf(","));


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
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please enter number of people present')</script>", false);


                this.txtOtherComm.Focus();
                return;
            }

            if (Tempothercom == "8")
            {
                if (txtOtherComm.Text.ToString() == "")
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Enter Other(specify) Community Meeting')</script>", false);


                    this.txtOtherComm.Focus();
                    txtOtherComm.Enabled = true;
                    return;
                }
                else
                {
                    txtOtherComm.Enabled = false;
                }

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
            if (Convert.ToInt32(txtvillager3.Text) == 0)
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please ensure the number of People Attended is more than zero')</script>", false);


                this.chkcommmetingTB.Focus();
                return;
            }

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
        string Dateof= txtDate.Text;
        string[] b = Dateof.Split('/');

        string FcDate = b[2] + '-' + b[1]  +'-' + b[0];

     
        if (ViewState["GUID"].ToString().Length > 1)
        {
            string StudentTSInsertQuery = "";
            bool InsertTS = false;
            if (Session["user_level"].ToString() == "19")
            {

                StudentTSInsertQuery = " Update tblActivityUpdate_Village set  Com_Mtg='" + commNew + "',modifyBy='" + Session["username"].ToString() + "',modifyDate='" + DateTime.Now.ToString("yyyy-MM-dd") + "', ComContact='" + Comm + "', Support='" + SupportCC + "',[GSS_Mtg] ='" + GGS + "',Com_TB2=" + c1 + ",[MM_Mtg]= " + muhula55 + ",Com_FC2=" + c2 + ",Com_Agenda2='" + othercom11 + "',Com_AgendaOther2='" + txtoC111.Text + "',Any_Other2='" + txtoC1.Text + "',Com_Attended2=" + Att1 + ", Any_Other='" + tc1.Text.Trim() + "' ,TBHandholding='" + TBHoldIng + "',GSS_Attended='" + vill1 + "',Remarks='" + ddlRemark.SelectedValue + "',GSS_Agenda='" + commmeeting + "',GSS_AgendaOther='" + txt_bookformatOther.Text + "',GSS_TB=" + commmetingTB + ",GSS_FC=" + commmetingFC + ",MM_Attended=" + vill2 + ",MM_Agenda='" + Muhula + "',MM_AgendaOther='" + txtmOther.Text + "',MM_TB='" + muhulaTb + "',MM_FC='" + muhulaFC + "',Com_Attended='" + vill3 + "',Com_Agenda='" + othercom + "',Com_AgendaOther='" + txtOtherComm.Text + "',Com_TB=" + othercommTb + ",Com_FC=" + othercommFC + ",ComContact_Op='" + AmbitionComm + "',ComContact_Op_Other='" + txt_con_other.Text + "',ComContact_TB='" + CommFCTB + "',ComContact_FC='" + CommFC + "',ComContact_Agenda='" + Ambition + "',ConContact_AgendaOther='" + txtOtherCon.Text + "',Support_Op='" + Suport + "',Support_Op_Other='" + txtOtherSupport.Text + "',Support_TB=" + Supporttb + ",Support_FC=" + SupportFC + ",Others_FC=" + lotherfc + ",Others_TB=" + lotherTB + ",Others_Desc='" + txtmainother.Text + "' where GUID_Village='" + ViewState["GUID"].ToString() + "' ";
                 InsertTS = objMain.AddUpdate(StudentTSInsertQuery);
            }
            
            if (Session["user_level"].ToString() == "39" || Session["user_level"].ToString() == "30")
            {
                StudentTSInsertQuery = " Update tblActivityUpdate_Village set Com_Mtg='" + commNew + "', modifyBy='" + Session["username"].ToString() + "',modifyDate='" + DateTime.Now.ToString("yyyy-MM-dd") + "',ComContact='" + Comm + "',Support='" + SupportCC + "',[GSS_Mtg] ='" + GGS + "',[MM_Mtg]= " + muhula55 + ",Com_TB2=" + c1 + ",Com_FC2=" + c2 + ",Com_Agenda2='" + othercom11 + "',Com_AgendaOther2='" + txtoC111.Text + "',Any_Other2='" + txtoC1.Text + "',Com_Attended2=" + Att1 + ", Any_Other='" + tc1.Text.Trim() + "',TBHandholding='" + TBHoldIng + "',GSS_Attended='" + vill1 + "',Remarks='" + ddlRemark.SelectedValue + "',GSS_Agenda='" + commmeeting + "',GSS_AgendaOther='" + txt_bookformatOther.Text + "',GSS_TB=" + commmetingTB + ",GSS_FC=" + commmetingFC + ",MM_Attended=" + vill2 + ",MM_Agenda='" + Muhula + "',MM_AgendaOther='" + txtmOther.Text + "',MM_TB='" + muhulaTb + "',MM_FC='" + muhulaFC + "',Com_Attended='" + vill3 + "',Com_Agenda='" + othercom + "',Com_AgendaOther='" + txtOtherComm.Text + "',Com_TB=" + othercommTb + ",Com_FC=" + othercommFC + ",ComContact_Op='" + AmbitionComm + "',ComContact_Op_Other='" + txt_con_other.Text + "',ComContact_TB='" + CommFCTB + "',ComContact_FC='" + CommFC + "',ComContact_Agenda='" + Ambition + "',ConContact_AgendaOther='" + txtOtherCon.Text + "',Support_Op='" + Suport + "',Support_Op_Other='" + txtOtherSupport.Text + "',Support_TB=" + Supporttb + ",Support_FC=" + SupportFC + ",Others_FC=" + lotherfc + ",Others_TB=" + lotherTB + ",Others_Desc='" + txtmainother.Text + "' where GUID_Village='" + ViewState["GUID"].ToString() + "' ";
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
                StudentTSInsertQuery = " INSERT INTO tblActivityUpdate_Village([VillageCode],[UserID] ,[GUID_Village] ,[ActivityDate] ,[TBHandholding], [GSS_Mtg]  ,[GSS_Attended] ,[GSS_Agenda]  ,[GSS_AgendaOther] ,[GSS_TB] ,[GSS_FC] ,      [MM_Mtg] ,[MM_Attended] ,[MM_Agenda],  [MM_AgendaOther],[MM_TB] ,[MM_FC] , [Com_Mtg] ,[Com_Attended] ,[Com_Agenda],[Com_AgendaOther] ,[Com_TB],[Com_FC] , [ComContact] ,[ComContact_Op] ,[ComContact_Op_Other] ,[ComContact_TB],[ComContact_FC], ComContact_Agenda,ConContact_AgendaOther,    [Support]   ,[Support_Op]  ,[Support_Op_Other] ,[Support_TB],[Support_FC]  ,[Others_FC] ,[Others_TB]  ,[Others_Desc],UserEntry,ApproveStatus,Remarks,Any_Other,Com_TB2,Com_FC2,Com_Agenda2,Com_AgendaOther2,Any_Other2,Com_Attended2,CreateBy) ";
                StudentTSInsertQuery += " Values('" + ddlVilage.SelectedValue + "','" + ddlUser.SelectedValue + "','" + UNICOde + "','" + Convert.ToDateTime(FcDate).ToString("yyy/MM/dd") + "','" + TBHoldIng + "','" + GGS + "','" + vill1 + "','" + commmeeting + "','" + txt_bookformatOther.Text + "'," + commmetingTB + "," + commmetingFC + "," + muhula55 + "," + vill2 + ",'" + Muhula + "','" + txtmOther.Text + "','" + muhulaTb + "','" + muhulaFC + "','" + commNew + "','" + vill3 + "','" + othercom + "','" + txtOtherComm.Text + "'," + othercommTb + "," + othercommFC + ",'" + Comm + "','" + AmbitionComm + "','" + txt_con_other.Text + "','" + CommFCTB + "','" + CommFC + "','" + Ambition + "','" + txtOtherCon.Text + "'," + SupportCC + ",'" + Suport + "','" + txtOtherSupport.Text + "'," + Supporttb + "," + SupportFC + "," + lotherfc + "," + lotherTB + ",'" + txtmainother.Text + "','3','B','" + ddlRemark.SelectedValue + "','" + tc1.Text.Trim() + "'," + c1 + "," + c2 + ",'" + othercom11 + "','" + txtoC111.Text + "','" + txtoC1.Text + "'," + Att1 + ",'" + Session["username"].ToString() + "')";
                InsertTS = objMain.AddUpdate(StudentTSInsertQuery);
            }

            if (Session["user_level"].ToString() == "19")
            {
                if (Muhula == "")
                {
                    Muhula = "0";
                }
                StudentTSInsertQuery = "";
                StudentTSInsertQuery = " INSERT INTO tblActivityUpdate_Village([VillageCode],[UserID] ,[GUID_Village] ,[ActivityDate] ,[TBHandholding], [GSS_Mtg]  ,[GSS_Attended] ,[GSS_Agenda]  ,[GSS_AgendaOther] ,[GSS_TB] ,[GSS_FC] ,      [MM_Mtg] ,[MM_Attended] ,[MM_Agenda],  [MM_AgendaOther],[MM_TB] ,[MM_FC] , [Com_Mtg] ,[Com_Attended] ,[Com_Agenda],[Com_AgendaOther] ,[Com_TB],[Com_FC] , [ComContact] ,[ComContact_Op] ,[ComContact_Op_Other] ,[ComContact_TB],[ComContact_FC], ComContact_Agenda,ConContact_AgendaOther,    [Support]   ,[Support_Op]  ,[Support_Op_Other] ,[Support_TB],[Support_FC]  ,[Others_FC] ,[Others_TB]  ,[Others_Desc],UserEntry,ApproveStatus,Remarks,Any_Other,Com_TB2,Com_FC2,Com_Agenda2,Com_AgendaOther2,Any_Other2,Com_Attended2,CreateBy) ";
                StudentTSInsertQuery += " Values('" + ddlVilage.SelectedValue + "','" + ddlUser.SelectedValue + "','" + UNICOde + "','" + Convert.ToDateTime(FcDate).ToString("yyy/MM/dd") + "','" + TBHoldIng + "','" + GGS + "','" + vill1 + "','" + commmeeting + "','" + txt_bookformatOther.Text + "'," + commmetingTB + "," + commmetingFC + "," + muhula55 + "," + vill2 + ",'" + Muhula + "','" + txtmOther.Text + "','" + muhulaTb + "','" + muhulaFC + "','" + commNew + "','" + vill3 + "','" + othercom + "','" + txtOtherComm.Text + "'," + othercommTb + "," + othercommFC + ",'" + Comm + "','" + AmbitionComm + "','" + txt_con_other.Text + "','" + CommFCTB + "','" + CommFC + "','" + Ambition + "','" + txtOtherCon.Text + "'," + SupportCC + ",'" + Suport + "','" + txtOtherSupport.Text + "'," + Supporttb + "," + SupportFC + "," + lotherfc + "," + lotherTB + ",'" + txtmainother.Text + "','3','FC','" + ddlRemark.SelectedValue + "','" + tc1.Text.Trim() + "'," + c1 + "," + c2 + ",'" + othercom11 + "','" + txtoC111.Text + "','" + txtoC1.Text + "'," + Att1 + ",'" + Session["username"].ToString() + "')";
                InsertTS = objMain.AddUpdate(StudentTSInsertQuery);

                StudentTSInsertQuery = "";
                StudentTSInsertQuery = " INSERT INTO tblActivityUpdate_Village([VillageCode],[UserID] ,[GUID_Village] ,[ActivityDate] ,[TBHandholding], [GSS_Mtg]  ,[GSS_Attended] ,[GSS_Agenda]  ,[GSS_AgendaOther] ,[GSS_TB] ,[GSS_FC] ,      [MM_Mtg] ,[MM_Attended] ,[MM_Agenda],  [MM_AgendaOther],[MM_TB] ,[MM_FC] , [Com_Mtg] ,[Com_Attended] ,[Com_Agenda],[Com_AgendaOther] ,[Com_TB],[Com_FC] , [ComContact] ,[ComContact_Op] ,[ComContact_Op_Other] ,[ComContact_TB],[ComContact_FC], ComContact_Agenda,ConContact_AgendaOther,    [Support]   ,[Support_Op]  ,[Support_Op_Other] ,[Support_TB],[Support_FC]  ,[Others_FC] ,[Others_TB]  ,[Others_Desc],UserEntry,ApproveStatus,Remarks,Any_Other,Com_TB2,Com_FC2,Com_Agenda2,Com_AgendaOther2,Any_Other2,Com_Attended2) ";
                StudentTSInsertQuery += " Values('" + ddlVilage.SelectedValue + "','" + ddlUser.SelectedValue + "','" + UNICOde + "','" + Convert.ToDateTime(FcDate).ToString("yyy/MM/dd") + "','" + TBHoldIng + "','" + GGS + "','" + vill1 + "','" + commmeeting + "','" + txt_bookformatOther.Text + "'," + commmetingTB + "," + commmetingFC + "," + muhula55 + "," + vill2 + ",'" + Muhula + "','" + txtmOther.Text + "','" + muhulaTb + "','" + muhulaFC + "','" + commNew + "','" + vill3 + "','" + othercom + "','" + txtOtherComm.Text + "'," + othercommTb + "," + othercommFC + ",'" + Comm + "','" + AmbitionComm + "','" + txt_con_other.Text + "','" + CommFCTB + "','" + CommFC + "','" + Ambition + "','" + txtOtherCon.Text + "'," + SupportCC + ",'" + Suport + "','" + txtOtherSupport.Text + "'," + Supporttb + "," + SupportFC + "," + lotherfc + "," + lotherTB + ",'" + txtmainother.Text + "','2','FC','" + ddlRemark.SelectedValue + "','" + tc1.Text.Trim() + "'," + c1 + "," + c2 + ",'" + othercom11 + "','" + txtoC111.Text + "','" + txtoC1.Text + "'," + Att1 + ")";
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
       imgMKS.ImageUrl ="TabletImage/" + lblGG.Text;
            
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
}