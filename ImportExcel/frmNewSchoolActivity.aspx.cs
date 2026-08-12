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

public partial class frmNewSchoolActivity : System.Web.UI.Page
{
    clsMain objMain = new clsMain();
    string conditions = "";
    Comman objComman = new Comman();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //CalendarExtender1.StartDate = DateTime.Today;
            //CalendarExtender1.EndDate = DateTime.Today.AddMonths(1);
           // UserData();
          
            LoadEnrolled(); 
           ModalPopupExtender.Hide();
        //   CalendarExtenderTourdate.StartDate = Convert.ToDateTime(Session["FromDate"].ToString());
           if (Session["user_level"].ToString() == "19")
           {
               DataTable dt = objMain.GetActivityUpdateDateWiseBlockWiseNew(Convert.ToString(Session["BlockCodeAct"]) ,"2", "FC");
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
          
            txtDate.Text = DateTime.Now.ToString("dd/MM/yyyy");

            if (Request.QueryString["ID"] != null)
            {

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
                    con = "ActivityDate =('" + aToDate + "') and UserEntry=2  and ApproveStatus='FC'  and mstCluster.ClusterCode='" + Session["Cluseter"].ToString() + "' ";
                    dtMain = objMain.LoadAllActivtiyDatewise(con, 1);

                }
                if (Session["user_level"].ToString() == "39" || Session["user_level"].ToString() == "30")
                {
                    con = "ActivityDate =('" + aToDate + "') and UserEntry=3  and ApproveStatus='B' and mstCluster.ClusterCode='" + Session["Cluseter"].ToString() + "' ";
                    dtMain = objMain.LoadAllActivtiyDatewise(con, 1);
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
                        ddlSchool.SelectedValue = dtMain.Rows[0]["SchoolCode"].ToString();
                       
                        btnSerach_Click(btnSerach, null);
                    }
                }

                //DataTable dt = objMain.GetActivityUserWiseMaxDate(ddlUser.SelectedValue);
                //if (dt.Rows.Count > 0)
                //{
                //    CalendarExtenderTourdate.StartDate = Convert.ToDateTime(dtMain.Rows[0]["Villagecode"].ToString());
                //}
                pnlMain.Enabled = false;
                //btnSerach_Click(btnSerach, null);
            }
            ViewState["GUID_School"] = "";
            
        }
    }

    protected void GKPDelete_OnClick(object sender, EventArgs e)
    {
        ImageButton bt = (ImageButton)sender;

        GridViewRow gvr = (GridViewRow)bt.NamingContainer;

        string UniqueChildCode = (gvr.FindControl("lblCUniqueChildCode") as Label).Text;


        SqlParameter[] parm = new SqlParameter[]
            {
              
              new SqlParameter("@uniquid",UniqueChildCode)
                                    
            };

        int result = SqlHelper.ExecuteNonQuery(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "DeleteGKp", parm);


        if (result > 0)
        {
            LoadData();
            ScriptManager.RegisterStartupScript(this, GetType(), "importingdone", "alert('Record Deleted');", true);

        }
       

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


    public void btnEdit_Click(object sender, EventArgs e)
    {
        ModalPopupExtender1.Show();
    }
    public void LoadData(string ClusterName)
    {

        string fromDate = txtDate.Text;
        string[] d = fromDate.Split('/');
        string afromDate = d[2] + '-' + d[1] + '-' + d[0];




        string strQry = "";
        strQry = "Select  distinct UserName as UserId,[FristName]+' ('+ UserName +')' as [UserName]  from MstUser  where UserLevel=24 and VillageCode   = '" + Session["Cluseter"].ToString() + "'   ";

        strQry += "union  ";
        strQry += " Select  distinct UserName as UserId,[FristName]+' ('+ UserName +')' as [UserName]  from MstUser  where UserLevel=24 and UserName in(  ";
        strQry += " select UserID from tblActivityUpdate_School  ";
        strQry += " inner join mst5village on mst5village.villagecode=tblActivityUpdate_School.villagecode  ";
        strQry += " where ActivityDate =('" + afromDate + "')  and  ";
        strQry += " mst5village.ClusterCode   = '" + Session["Cluseter"].ToString() + "' )    ";


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


        strQry += " Select  distinct UserName as UserId,[FristName]+' ('+ UserName +')' as [UserName]  from MstUser  where UserLevel=24 and UserName in(  ";
        strQry += " select UserID from Tbl_GKP  ";
        strQry += " inner join mst5village on mst5village.villagecode=Tbl_GKP.villagecode  ";
        strQry += " where ActivityDate =('" + afromDate + "') )   ";
        //strQry += " and mst5village.ClusterCode   = '" + Session["Cluseter"].ToString() + "' )    ";


        DataTable dtUser3 = objMain.LoadData(strQry);
        objComman.BindDLLMasterTable("MstUser", "UserName as UserId,[FristName]+' ('+ UserName +')' as [UserName] ", dtUser3, conditions, "", "", ddlUser, "UserName", "UserId", "Select");


        objComman.BindDLL("mstSubject", "SubjectID, SubjectName", conditions, "SubjectID", "asc", ddlSubject, "SubjectName", "SubjectID", "Select");
    }

    protected void btnApprove_Click(object sender, EventArgs e)
    {
        //  btnApprove.Attributes.Add("onclick", "javascript:return " + "confirm('Please confirm if you want to approve? ')");


        Response.Redirect("~/FrmActivityDatewiseSearch.aspx?ID=" + Session["CluseterName"].ToString() + "," + Session["FromData"].ToString() + "," + Session["Todate"].ToString() + "");


    }
    protected void btnSmc_Click(object sender, EventArgs e)
    {
        chkSMC.Checked = false;
        rblSMCTB.Checked = false;
        rblSMCFC.Checked = false;
        txtOtherSIPFC.Text = "";
        txtsmcmeetinFC.Text = "";
        foreach (ListItem item in CBL_bookformat.Items)
        {

            item.Selected = false;


        }

        chkNewSmc.Checked = false;
        rblSmcNew.Checked = false;
        rblSmcNew1.Checked = false;
        txtTotalMember.Text = "";
        txtTotalFmember.Text = "";
        txt_pbname.Text = "";
    }

    protected void btnCLT_Click(object sender, EventArgs e)
    {
        chkClT.Checked = false;
        rblCLTTB.Checked = false;
        rblCLTFC.Checked = false;

        chkHindiA.Checked = false;
        chkEnglishA.Checked = false;
        chkMathA.Checked = false;


        chkHindiB.Checked = false;
        chkEnglishB.Checked = false;
        chkMathB.Checked = false;

        chkHindiC.Checked = false;
        chkEnglishC.Checked = false;
        chkMathC.Checked = false;

        chkHindiD.Checked = false;
        chkEnglishD.Checked = false;
        chkMathD.Checked = false;


        chkHindiE.Checked = false;
        chkEnglishE.Checked = false;
        chkMathE.Checked = false;



        rblTestTBPre.Checked = false;
        rblTestTBMid.Checked = false;
        rblTestTBPost.Checked = false;

        rblTestpreFC.Checked = false;
        rblTestMidFC.Checked = false;
        rblTestPostFC.Checked = false;

        rblPartialPre.Checked = false;
        rblPartialMid.Checked = false;
        rblPartialPost.Checked = false;



        rblCompletePre.Checked = false;
        rblCompleteMid.Checked = false;
        rblCompletePost.Checked = false;

     
    }
    protected void btnBalSab_Click(object sender, EventArgs e)
    {
        rblBalsabaTB.Checked = false;
        rblBalsabaFC.Checked = false;
        chkBalSabhaFor.Checked = false;
        chkOrientation.Checked = false;
        chkChat.Checked = false;
        chkKit.Checked = false;
    }

    protected void btnLife(object sender, EventArgs e)
    {
        chklife.Checked = false;
        rblLifeTB.Checked = false;
        rblLifeFC.Checked = false;
        chkGame1.Checked = false;
        chkGame2.Checked = false;
        chkGame3.Checked = false;
        chkGame4.Checked = false;
        chkGame5.Checked = false;


    }

    protected void btnSacUpdate_Click(object sender, EventArgs e)
    {
        chkSACUpdate.Checked = false;
        rblSacTB.Checked = false;
        rblSacFB.Checked = false;
        txtSMCMeeting.Text = "";

        txtHealth.Text = "";

        txtAdgirls.Text = "";

        txtAdBoy.Text = "";

        txtleftGirl.Text = "";

        txtleftBoy.Text = "";

        txtGirlNot.Text = "";

        txtGirlNot.Text = "";
        txtBoyNot.Text = "";             
    }
    protected void btninfrastructure_Click(object sender, EventArgs e)
    {
        lbldriking.Text = "0";
        lblToilet.Text = "0";
        lblElectricity.Text = "0";
        lblCltKit.Text = "0";
        lblbook.Text = "0";
        lblKitchen.Text = "0";
        lblBoundaryWall.Text = "0";
        lblSlides.Text = "0";
        lblPlay.Text = "0";
        txtClassRoom.Text = "";
        txtMaleTeacher.Text = "";
        txtFemaleTeacher.Text = "";
        txtToilet.BackColor = Color.White;
        txtdrinking.BackColor = Color.White;

        txtElectricity.BackColor = Color.White;
        txtbook.BackColor = Color.White;
        txtPlay.BackColor = Color.White;
        txtSlides.BackColor = Color.White;
        txtBoundaryWall.BackColor = Color.White;
        txtKitchen.BackColor = Color.White;
        txtCltKit.BackColor = Color.White;

        chkPhysical.Checked = false;
        rblPhysicalTB.Checked = false;
        rblPhysicalFC.Checked = false;
    }


    protected void btnAnnual_Click(object sender, EventArgs e)
    {
        chkAnnual.Checked = false;
        chkSIPAnnaul.Checked = false;
        chkRetention.Checked = false;

        chkSIPTB.Checked = false;
        chkRenTB.Checked = false;
        chkSIPFC.Checked = false;

        chkRenFC.Checked = false;
        chkSipPartial.Checked = false;
        chkRenPartial.Checked = false;


        chkSipComplete.Checked = false;
        chkComplete.Checked = false;
    
    }
    private Boolean Validation()
    {
        try
        {
            #region Main
            if (ddlUser.SelectedIndex <= 0)
            {
                
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select User')</script>", false);
                return false;
            }
            if (ddlVilage.SelectedIndex <= 0)
            {
               
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select Village')</script>", false);
                return false;
            }
            if (txtDate.Text == "")
            {
              
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select Date')</script>", false);
                return false;
            }
            if (ddlSchool.SelectedIndex <= 0)
            {
               
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select School')</script>", false);
                return false;
            }
            #endregion

            #region SMC

            if (chkSMC.Checked == true)
            {
                if (rblSMCTB.Checked == true || rblSMCFC.Checked == true)
                {

                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select SMC TB or FC')</script>", false);
                    this.chkSMC.Focus();
                    return false;
                }
                if (txtOtherSIPFC.Text == "")
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please enter   Other SIP prepared')</script>", false);
                    this.txtOtherSIPFC.Focus();
                    return false;
                }
                if (txtsmcmeetinFC.Text == "")
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please enter   Other SIP completed')</script>", false);
                    this.txtsmcmeetinFC.Focus();
                    return false;
                }

                if (txt_pbname.Text == "")
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select   Other Discussions ')</script>", false);
                    this.txt_pbname.Focus();
                    return false;
                }
                //Int32 SIP=Convert.ToInt32(

              
            }
            if (txtOtherSIPFC.Text != "" || txtsmcmeetinFC.Text != "" || txtOtherSIPFC.Text != "" || txt_pbname.Text != "")
            {
                if (txtOtherSIPFC.Text == "")
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please enter   Other SIP prepared')</script>", false);
                    this.txtOtherSIPFC.Focus();
                    return false;
                }
                if (txtsmcmeetinFC.Text == "")
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please enter   Other SIP completed')</script>", false);
                    this.txtsmcmeetinFC.Focus();
                    return false;
                }

                if (txt_pbname.Text == "")
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select   Other Discussions ')</script>", false);
                    this.txt_pbname.Focus();
                    return false;
                }
                if (rblSMCTB.Checked == true || rblSMCFC.Checked == true)
                {
                   
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select SMC TB or FC')</script>", false);
                    this.chkSMC.Focus();
                    return false;
                }
                if (chkSMC.Checked == false)
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select SMC ')</script>", false);
                    this.chkSMC.Focus();
                    return false;
                }
                if (txt_pbname.Text == "")
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select   Other Discussions ')</script>", false);
                    this.txt_pbname.Focus();
                    return false;
                }
               
            }

            #endregion

            #region SMC Orientation

            if (chkSMC.Checked == true)
            {
                if (rblSMCTB.Checked == true || rblSMCFC.Checked == true)
                {

                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select SMC Orientation TB or FC')</script>", false);
                    this.chkSMC.Focus();
                    return false;
                }
                if (txtTotalMember.Text == "")
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please enter Total Trained Member')</script>", false);
                    this.txtTotalMember.Focus();
                    return false;
                }
                if (txtTotalFmember.Text == "")
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please enter Total Trained Female Member')</script>", false);
                    this.txtTotalFmember.Focus();
                    return false;
                }
                if (Convert.ToInt32(txtTotalMember.Text) <6 || Convert.ToInt32(txtTotalMember.Text) >=16)
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please ensure that Total Trained  Member number should be greater than 6 and less then 16')</script>", false);
                    this.txtTotalFmember.Focus();
                }
              
                //Int32 TotoSip = Convert.ToInt32(txtOtherSIPFC.Text) + Convert.ToInt32(txtsmcmeetinFC.Text);
                //if (TotoSip <= 0)
                //{
                //    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please enter  SIP prepared or completed Value')</script>", false);
                //    this.txtTotalFmember.Focus();
                //    return false;
                //}
                Int32 Toto = Convert.ToInt32(txtTotalMember.Text) + Convert.ToInt32(txtTotalFmember.Text);
                if (Toto <=0)
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please enter  Trained Female or Male Member Value')</script>", false);
                    this.txtTotalFmember.Focus();
                    return false;
                }
            }

           
            if (txtTotalMember.Text != "" || txtTotalFmember.Text != "")
            {
                if (rblSMCTB.Checked == true || rblSMCFC.Checked == true)
                {

                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select SMC Orientation TB or FC')</script>", false);
                    this.chkSMC.Focus();
                    return false;
                }
                if (chkSMC.Checked == false)
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select SMC Orientation')</script>", false);
                    this.chkSMC.Focus();
                    return false;
                }
            }
           

            #endregion
            
            #region Balsabha
            Int32 BalsabaTB = 0;
            Int32 BalsabFC = 0;
            if (rblBalsabaTB.Checked == true)
            {
                BalsabaTB = 1;
            }
            if (rblBalsabaFC.Checked == true)
            {
                BalsabFC = 1;
            }
            Int32 BalSabha_Formation = 0;


            if (chkBalSabhaFor.Checked == true)
            {
                BalSabha_Formation = 1;
            }
            if (chkOrientation.Checked == true)
            {
                BalSabha_Formation = 1;
            }
            if (chkChat.Checked == true)
            {
                BalSabha_Formation = 1;
            }
            if (chkKit.Checked == true)
            {
                BalSabha_Formation = 1;
            }
            if (BalSabha_Formation == 1)
            {
                if (chkBalsabha.Checked == false)
                {

                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select Balsabha')</script>", false);
                    this.chkSMC.Focus();
                    return false;
                }
                if (BalsabaTB == 0 && BalsabFC == 0)
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select FC OR TB Balsabha')</script>", false);



                    this.chkSMC.Focus();
                    return false;
                }
            }
            if (chkBalsabha.Checked == true)
            {
                if (BalSabha_Formation == 0)
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select Any one Balsabha')</script>", false);



                    this.chkSMC.Focus();
                    return false;
                }
                if (BalsabaTB == 0 && BalsabFC == 0)
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select FC OR TB Balsabha')</script>", false);



                    this.chkSMC.Focus();
                    return false;
                }
            }

            if (BalSabha_Formation == 0)
            {
                if (chkBalsabha.Checked == true)
                {

                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Select  Any One Balsabha')</script>", false);
                    this.chkSMC.Focus();
                    return false;
                }

            }

            Int32 LifeTB = 0;
            Int32 LifeFC = 0;
            if (rblLifeTB.Checked == true)
            {
                LifeTB = 1;
            }
            if (rblLifeFC.Checked == true)
            {
                LifeFC = 1;
            }
            #endregion

            #region Game

            Int32 Game_TB = 0;
            Int32 Game_FC = 0;
            if (rblLifeTB.Checked == true)
            {
                Game_TB = 1;
            }
            if (rblLifeFC.Checked == true)
            {
                Game_FC = 2;
            }

            int Game = 0;
            if (chkGame1.Checked == true)
            {
                Game = 1;
            }
            if (chkGame2.Checked == true)
            {
                Game = 1;
            }
            if (chkGame3.Checked == true)
            {
                Game = 1;
            }
            if (chkGame4.Checked == true)
            {
                Game = 1;
            }

            if (chkGame5.Checked == true)
            {
                Game = 1;
            }

            if (Game == 1)
            {
                if (chklife.Checked == false)
                {

                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select Life Skill')</script>", false);
                    this.chkSMC.Focus();
                    return false;
                }
                if (LifeTB == 0 && LifeFC == 0)
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select FC OR TB Life Skill')</script>", false);



                    this.chkSMC.Focus();
                    return false;
                }
            }

            if (chklife.Checked == true)
            {
                if (Game == 0)
                {

                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Select  Any One Life Skill')</script>", false);
                    this.chkSMC.Focus();
                    return false;
                }
                if (LifeTB == 0 && LifeFC == 0)
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select FC OR TB Life Skill')</script>", false);



                    this.chkSMC.Focus();
                    return false;
                }
            }

            #endregion

            #region SAC Update
            Int32 SACTB = 0;
            Int32 SACFC = 0;
            if (rblSacTB.Checked == true)
            {
                SACTB = 1;
            }
            if (rblSacFB.Checked == true)
            {
                SACFC = 1;
            }

            int SAC_No_Of_Attended = 0;
            if (txtSMCMeeting.Text.Trim() != "")
            {
                SAC_No_Of_Attended = 1;
            }


            if (txtHealth.Text.Trim() != "")
            {
                SAC_No_Of_Attended = 1;
            }


        


            if (SAC_No_Of_Attended == 1)
            {
                if (chkSACUpdate.Checked == false)
                {

                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select SAC')</script>", false);
                    this.chkSMC.Focus();
                    return false;
                }
                if (SACTB == 0 && SACFC == 0)
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select FC OR TB SAC')</script>", false);



                    this.chkSMC.Focus();
                    return false;
                }
            }

            if (chkSACUpdate.Checked == true )
            {
                if (SAC_No_Of_Attended == 0)
                {

                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Select  Any One SAC')</script>", false);
                    this.chkSMC.Focus();
                    return false;
                }
                if (SACTB == 0 && SACFC == 0)
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select FC OR TB SAC')</script>", false);



                    this.chkSMC.Focus();
                    return false;
                }
            }

            #endregion

            #region
            if (chkRetention.Checked == true)
            {
                if (chkRenTB.Checked == false && chkRenFC.Checked == false)
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select FC OR TB Retention')</script>", false);



                    this.chkSMC.Focus();
                    return false;
                }
                if (chkRenPartial.Checked == false && chkComplete.Checked == false)
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select Partial OR Complete Retention')</script>", false);



                    this.chkSMC.Focus();
                    return false;
                }
            }
            if (chkRetention.Checked == false)
            {
                if (chkRenTB.Checked == true || chkRenFC.Checked == true || chkRenPartial.Checked == true || chkComplete.Checked == true)
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select  Retention')</script>", false);



                    this.chkSMC.Focus();
                    return false;
                }
              
            }
            #endregion

            #region
            if (chkSIPAnnaul.Checked == true)
            {
                if (chkSIPTB.Checked == false && chkSIPFC.Checked == false)
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select FC OR TB SIPAnnaul')</script>", false);



                    this.chkSMC.Focus();
                    return false;
                }
                if (chkSipPartial.Checked == false && chkSipComplete.Checked == false)
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select Partial OR Complete SIPAnnaul')</script>", false);



                    this.chkSMC.Focus();
                    return false;
                }
            }
            if (chkSIPAnnaul.Checked == false)
            {
                if (chkSIPTB.Checked == true || chkSIPFC.Checked == true || chkSipPartial.Checked == true || chkSipComplete.Checked == true)
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select  SIPAnnaul')</script>", false);



                    this.chkSMC.Focus();
                    return false;
                }

            }
            #endregion

            #region SPF
            int Infrastructure = 0;
                int Classrooms = 0;
                int DrinkingWater = 0;
                int GirlsToilet = 0;
                int Electricity = 0;
                int Playground = 0;
                int Slide = 0;
                int BoundaryWall = 0;
                int Kitchen = 0;
                int Teachers_Male = 0;
                int Teachers_Female = 0;
                int CLT_Kit = 0;
                int bookAvl = 0;
                int Infrastructure_FC = 0; int Infrastructure_TB = 0;

                if (txtClassRoom.Text != "")
                {
                    Classrooms = Convert.ToInt32(txtClassRoom.Text);
                }
                if (lbldriking.Text == "1")
                {
                    DrinkingWater = 1;
                }
                else if (lbldriking.Text == "2")
                {

                    DrinkingWater = 2;
                }
                else if (lbldriking.Text == "3")
                {

                    DrinkingWater = 3;
                }
                else if (lbldriking.Text == "4")
                {

                    DrinkingWater = 4;
                }

                if (lblToilet.Text == "1")
                {
                    GirlsToilet = 1;
                }
                else if (lblToilet.Text == "2")
                {

                    GirlsToilet = 2;
                }
                else if (lblToilet.Text == "3")
                {

                    GirlsToilet = 3;
                }
                else if (lblToilet.Text == "4")
                {

                    GirlsToilet = 4;
                }

                if (lblElectricity.Text == "1")
                {
                    Electricity = 1;
                }
                else if (lblElectricity.Text == "2")
                {

                    Electricity = 2;
                }
                else if (lblElectricity.Text == "3")
                {

                    Electricity = 3;
                }
                else if (lblElectricity.Text == "4")
                {

                    Electricity = 4;
                }

                if (lblPlay.Text == "1")
                {
                    Playground = 1;
                }
                else if (lblPlay.Text == "2")
                {

                    Playground = 2;
                }
                else if (lblPlay.Text == "3")
                {

                    Playground = 3;
                }
                else if (lblPlay.Text == "4")
                {

                    Playground = 4;
                }


                if (lblPlay.Text == "1")
                {
                    Slide = 1;
                }
                else if (lblSlides.Text == "2")
                {

                    Slide = 2;
                }
                else if (lblSlides.Text == "3")
                {

                    Slide = 3;
                }
                else if (lblSlides.Text == "4")
                {

                    Slide = 4;
                }

                if (lblBoundaryWall.Text == "1")
                {
                    BoundaryWall = 1;
                }
                else if (lblBoundaryWall.Text == "2")
                {

                    BoundaryWall = 2;
                }
                else if (lblBoundaryWall.Text == "3")
                {

                    BoundaryWall = 3;
                }
                else if (lblBoundaryWall.Text == "4")
                {

                    BoundaryWall = 4;
                }


                if (lblSlides.Text == "1")
                {
                    Slide = 1;
                }
                else if (lblSlides.Text == "2")
                {

                    Slide = 2;
                }
                else if (lblSlides.Text == "3")
                {

                    Slide = 3;
                }
                else if (lblSlides.Text == "4")
                {

                    Slide = 4;
                }

                if (lblKitchen.Text == "1")
                {
                    Kitchen = 1;
                }
                else if (lblKitchen.Text == "2")
                {

                    Kitchen = 2;
                }
                else if (lblKitchen.Text == "3")
                {

                    Kitchen = 3;
                }
                else if (lblKitchen.Text == "4")
                {

                    Kitchen = 4;
                }
                if (lblCltKit.Text == "1")
                {
                    CLT_Kit = 1;
                }
                else if (lblCltKit.Text == "2")
                {

                    CLT_Kit = 2;
                }
                else if (lblCltKit.Text == "3")
                {

                    CLT_Kit = 3;
                }
                else if (lblCltKit.Text == "4")
                {

                    CLT_Kit = 4;
                }

                if (lblbook.Text == "1")
                {
                    bookAvl = 1;
                }
                else if (lblbook.Text == "2")
                {

                    bookAvl = 2;
                }
                else if (lblbook.Text == "3")
                {

                    bookAvl = 3;
                }
                else if (lblbook.Text == "4")
                {

                    bookAvl = 4;
                }

                if (chkPhysical.Checked == true)
                {
                    if (Infrastructure > 0 || Classrooms > 0 || DrinkingWater > 0 || GirlsToilet > 0 || Electricity > 0 || Playground > 0 || Slide > 0 || BoundaryWall > 0 || Kitchen > 0 || Teachers_Male > 0 || Teachers_Female > 0 || CLT_Kit > 0 || bookAvl > 0)
                    {



                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select All Colour')</script>", false);



                        this.txtdrinking.Focus();
                        return false; 
                    }
                     if (rblPhysicalTB.Checked == true || rblPhysicalFC.Checked == true)
                        {
                            
                        }
                     else

                     {
                         ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Select  Infrastructure TB or FC')</script>", false);



                            this.chkSMC.Focus();
                            return false;
                     }
                       
                    if (txtClassRoom.Text == "")
                    {
                         ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Enter Class')</script>", false);



                         this.txtClassRoom.Focus();
                        return false;
                    }

                    if (txtFemaleTeacher.Text.Trim() == "")
                    {
                        ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Enter Female Teacher')</script>", false);



                        this.txtFemaleTeacher.Focus();
                        return false;
                    }
                    if (txtMaleTeacher.Text == "")
                    {
                        ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Enter Male Teacher')</script>", false);



                        this.txtMaleTeacher.Focus();
                        return false;
                    }
                 
                }
                if (Infrastructure > 0 || Classrooms > 0 || DrinkingWater > 0 || GirlsToilet > 0 || Electricity > 0 || Playground > 0 || Slide > 0 || BoundaryWall > 0 || Kitchen > 0 || Teachers_Male > 0 || Teachers_Female > 0 || CLT_Kit > 0 || bookAvl > 0)
                {
                    if (rblPhysicalTB.Checked == true || rblPhysicalFC.Checked == true)
                    {

                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Select  Infrastructure TB or FC')</script>", false);



                        this.rblPhysicalTB.Focus();
                        return false;
                    }
                    if (chkPhysical.Checked == false)
                    {
                        ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Select  Infrastructure ')</script>", false);



                        this.chkPhysical.Focus();
                        return false;
                    }
                }
                #endregion

            return true;

        }
        catch (Exception ex)
        {

            return false;
        }
    }
    public void Save()
    {

        if (this.ddlRemark.SelectedIndex <= 0)
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select Remark')</script>", false);
            this.chkSMC.Focus();
            return ;

        }

        #region Main
        Int32 TBHolding = 0;
        if (chkHolding.Checked == true)
        {
            TBHolding = 1;
        }
       
    

        string Dateof = txtDate.Text;
        string[] b = Dateof.Split('/');

        string FcDate = b[2] + '-' + b[1] + '-' + b[0];
        string UNICOde = "";
        if (ViewState["GUID_School"].ToString().Length > 5)
        {
            UNICOde = ViewState["GUID_School"].ToString();
        }
        else
        {
            UNICOde = objMain.Generate_RandomString(15);
        }
        #endregion

        #region SMC
        Int32 SMC = 0;
        Int32 SMCTB = 0;
        Int32 SMCFC = 0;
        Int32 OtherSIPprepared = 0;
        Int32 OtherSIPcompleted = 0;
        string commmeeting = "";
        if (chkSMC.Checked == true)
        {
            SMC = 1;
        }
        if (rblSMCTB.Checked == true)
            {
                SMCTB = 1;
            }
        if (rblSMCFC.Checked == true)
          {
              SMCFC = 1;
          }
            if (txtOtherSIPFC.Text != "")
            {
                OtherSIPprepared = Convert.ToInt32(txtOtherSIPFC.Text);
            }
            if (txtsmcmeetinFC.Text != "")
            {
                OtherSIPcompleted = Convert.ToInt32(txtsmcmeetinFC.Text);
       
            }

            foreach (ListItem item in CBL_bookformat.Items)
            {
                if (item.Selected)
                {

                    commmeeting += "" + item.Value + "" + ",";


                }
            }
            if (commmeeting.Length > 0)
            {
                commmeeting = commmeeting.Substring(0, commmeeting.LastIndexOf(","));
            }

        #endregion

        #region SMC Orientation
            Int32 SMCOrient=0;
         Int32 SMCOrientTB=0;
         Int32 SMCOrientFC=0;
          Int32 TotalMember=0;
                Int32 TotalFemaSmcFemal=0;
                if (chkSMC.Checked == true)
                {
                    SMCOrient = 1;
                }
                if (rblSMCTB.Checked == true)
                {
                   SMCOrientTB=1;
                }
                if (rblSMCFC.Checked == true)
                {
                   SMCOrientFC=1;
                }
                if (txtTotalMember.Text != "")
                {
                    TotalMember=Convert.ToInt32(txtTotalMember.Text);
                }
                if (txtTotalFmember.Text != "")
                {
                    TotalFemaSmcFemal=Convert.ToInt32(txtTotalFmember.Text);
                }
          





            #endregion

        #region Subject
                DataTable dtSubject;
                dtSubject = CreateDataDate();
                DataRow dr;
                Int32 CLT_TB = 0;
                Int32 CLT_FC = 0;
                Int32 CLT = 0;
                string CLTHindi = "";
                #region Hindi
                if (chkHindiA.Checked == true)
                {
                    dr = dtSubject.NewRow();
                    dr["GUID_School"] = UNICOde;
                    dr["VillageCode"] = ddlVilage.SelectedValue;
                    dr["SchoolCode"] = ddlSchool.SelectedValue;
                    dr["ActivityDate"] = Convert.ToDateTime(FcDate).ToString("yyyy-MM-dd");
                    dr["Subject"] = 1;

                    dr["CLTGroup"] = "A";
                    dtSubject.Rows.Add(dr);
                    CLTHindi += "A" + ",";
                }
                if (chkHindiB.Checked == true)
                {
                    dr = dtSubject.NewRow();
                    dr["GUID_School"] = UNICOde;
                    dr["VillageCode"] = ddlVilage.SelectedValue;
                    dr["SchoolCode"] = ddlSchool.SelectedValue;
                    dr["ActivityDate"] = Convert.ToDateTime(FcDate).ToString("yyyy-MM-dd");
                    dr["Subject"] = 1;

                    dr["CLTGroup"] = "B";
                    dtSubject.Rows.Add(dr);

                    CLTHindi += "B" + ",";
                }
                if (chkHindiC.Checked == true)
                {
                    dr = dtSubject.NewRow();
                    dr["GUID_School"] = UNICOde;
                    dr["VillageCode"] = ddlVilage.SelectedValue;
                    dr["SchoolCode"] = ddlSchool.SelectedValue;
                    dr["ActivityDate"] = Convert.ToDateTime(FcDate).ToString("yyyy-MM-dd");
                    dr["Subject"] = 1;

                    dr["CLTGroup"] = "C";
                    dtSubject.Rows.Add(dr);

                    CLTHindi += "C" + ",";
                }
                if (chkHindiD.Checked == true)
                {
                    dr = dtSubject.NewRow();
                    dr["GUID_School"] = UNICOde;
                    dr["VillageCode"] = ddlVilage.SelectedValue;
                    dr["SchoolCode"] = ddlSchool.SelectedValue;
                    dr["ActivityDate"] = Convert.ToDateTime(FcDate).ToString("yyyy-MM-dd");
                    dr["Subject"] = 1;

                    dr["CLTGroup"] = "D";


                    dtSubject.Rows.Add(dr);

                    CLTHindi += "D" + ",";
                }

                if (chkHindiE.Checked == true)
                {
                    dr = dtSubject.NewRow();
                    dr["GUID_School"] = UNICOde;
                    dr["VillageCode"] = ddlVilage.SelectedValue;
                    dr["SchoolCode"] = ddlSchool.SelectedValue;
                    dr["ActivityDate"] = Convert.ToDateTime(FcDate).ToString("yyyy-MM-dd");
                    dr["Subject"] = 1;

                    dr["CLTGroup"] = "E";

                    CLTHindi += "E" + ",";
                    dtSubject.Rows.Add(dr);
                }

                if (CLTHindi.Length > 0)
                {
                    CLTHindi = CLTHindi.Substring(0, CLTHindi.LastIndexOf(","));

                }
                #endregion

                string CltEnglish = "";

                #region English
                if (chkEnglishA.Checked == true)
                {
                    dr = dtSubject.NewRow();
                    dr["GUID_School"] = UNICOde;
                    dr["VillageCode"] = ddlVilage.SelectedValue;
                    dr["SchoolCode"] = ddlSchool.SelectedValue;
                    dr["ActivityDate"] = Convert.ToDateTime(FcDate).ToString("yyyy-MM-dd");
                    dr["Subject"] = 2;

                    dr["CLTGroup"] = "A";
                    dtSubject.Rows.Add(dr);

                    CltEnglish += "A" + ",";
                }
                if (chkEnglishB.Checked == true)
                {
                    dr = dtSubject.NewRow();
                    dr["GUID_School"] = UNICOde;
                    dr["VillageCode"] = ddlVilage.SelectedValue;
                    dr["SchoolCode"] = ddlSchool.SelectedValue;
                    dr["ActivityDate"] = Convert.ToDateTime(FcDate).ToString("yyyy-MM-dd");
                    dr["Subject"] = 2;

                    dr["CLTGroup"] = "B";
                    dtSubject.Rows.Add(dr);
                    CltEnglish += "B" + ",";
                }
                if (chkEnglishC.Checked == true)
                {
                    dr = dtSubject.NewRow();
                    dr["GUID_School"] = UNICOde;
                    dr["VillageCode"] = ddlVilage.SelectedValue;
                    dr["SchoolCode"] = ddlSchool.SelectedValue;
                    dr["ActivityDate"] = Convert.ToDateTime(FcDate).ToString("yyyy-MM-dd");
                    dr["Subject"] = 2;

                    dr["CLTGroup"] = "C";

                    dtSubject.Rows.Add(dr);
                    CltEnglish += "C" + ",";
                }
                if (chkEnglishD.Checked == true)
                {
                    dr = dtSubject.NewRow();
                    dr["GUID_School"] = UNICOde;
                    dr["VillageCode"] = ddlVilage.SelectedValue;
                    dr["SchoolCode"] = ddlSchool.SelectedValue;
                    dr["ActivityDate"] = Convert.ToDateTime(FcDate).ToString("yyyy-MM-dd");
                    dr["Subject"] = 2;

                    dr["CLTGroup"] = "D";
                    dtSubject.Rows.Add(dr);
                    CltEnglish += "D" + ",";
                }

                if (chkEnglishE.Checked == true)
                {
                    dr = dtSubject.NewRow();
                    dr["GUID_School"] = UNICOde;
                    dr["VillageCode"] = ddlVilage.SelectedValue;
                    dr["SchoolCode"] = ddlSchool.SelectedValue;
                    dr["ActivityDate"] = Convert.ToDateTime(FcDate).ToString("yyyy-MM-dd");
                    dr["Subject"] = 2;

                    dr["CLTGroup"] = "E";
                    dtSubject.Rows.Add(dr);
                    CltEnglish += "E" + ",";
                }

                if (CltEnglish.Length > 0)
                {
                    CltEnglish = CltEnglish.Substring(0, CltEnglish.LastIndexOf(","));

                }
                #endregion
                string CltMath = "";
                #region Math
                if (chkMathA.Checked == true)
                {
                    dr = dtSubject.NewRow();
                    dr["GUID_School"] = UNICOde;
                    dr["VillageCode"] = ddlVilage.SelectedValue;
                    dr["SchoolCode"] = ddlSchool.SelectedValue;
                    dr["ActivityDate"] = Convert.ToDateTime(FcDate).ToString("yyyy-MM-dd");
                    dr["Subject"] = 3;

                    dr["CLTGroup"] = "A";
                    dtSubject.Rows.Add(dr);
                    CltMath += "A" + ",";
                }
                if (chkMathB.Checked == true)
                {
                    dr = dtSubject.NewRow();
                    dr["GUID_School"] = UNICOde;
                    dr["VillageCode"] = ddlVilage.SelectedValue;
                    dr["SchoolCode"] = ddlSchool.SelectedValue;
                    dr["ActivityDate"] = Convert.ToDateTime(FcDate).ToString("yyyy-MM-dd");
                    dr["Subject"] = 3;

                    dr["CLTGroup"] = "B";
                    dtSubject.Rows.Add(dr);
                    CltMath += "B" + ",";
                }
                if (chkMathC.Checked == true)
                {
                    dr = dtSubject.NewRow();
                    dr["GUID_School"] = UNICOde;
                    dr["VillageCode"] = ddlVilage.SelectedValue;
                    dr["SchoolCode"] = ddlSchool.SelectedValue;
                    dr["ActivityDate"] = Convert.ToDateTime(FcDate).ToString("yyyy-MM-dd");
                    dr["Subject"] = 3;

                    dr["CLTGroup"] = "C";
                    dtSubject.Rows.Add(dr);
                    CltMath += "C" + ",";
                }
                if (chkMathD.Checked == true)
                {
                    dr = dtSubject.NewRow();
                    dr["GUID_School"] = UNICOde;
                    dr["VillageCode"] = ddlVilage.SelectedValue;
                    dr["SchoolCode"] = ddlSchool.SelectedValue;
                    dr["ActivityDate"] = Convert.ToDateTime(FcDate).ToString("yyyy-MM-dd");
                    dr["Subject"] = 3;

                    dr["CLTGroup"] = "D";
                    dtSubject.Rows.Add(dr);
                    CltMath += "D" + ",";
                }

                if (chkMathE.Checked == true)
                {
                    dr = dtSubject.NewRow();
                    dr["GUID_School"] = UNICOde;
                    dr["VillageCode"] = ddlVilage.SelectedValue;
                    dr["SchoolCode"] = ddlSchool.SelectedValue;
                    dr["ActivityDate"] = Convert.ToDateTime(FcDate).ToString("yyyy-MM-dd");
                    dr["Subject"] = 3;

                    dr["CLTGroup"] = "E";
                    dtSubject.Rows.Add(dr);
                    CltMath += "E" + ",";
                }

                if (CltMath.Length > 0)
                {
                    CltMath = CltMath.Substring(0, CltMath.LastIndexOf(","));

                }
                #endregion

                if (dtSubject.Rows.Count > 0)
                {
                    if (rblCLTTB.Checked == true)
                    {
                        CLT_TB = 1;
                    }
                    if (rblCLTFC.Checked == true)
                    {
                        CLT_FC = 1;
                    }
                    if (chkClT.Checked == true)
                    {
                        CLT = 1;
                    }
                }
           
                #endregion

        #region Test


                Int32 CLT_Pretest_FC = 0;
                Int32 CLT_Pretest_TB = 0;
                Int32 CTL_Midtest_FC = 0;
                Int32 CTL_Midtest_TB = 0;
                Int32 CLT_Posttest_FC = 0;
                Int32 CLT_Posttest_TB = 0;

                Int32 CLT_Pretest = 0;
                Int32 CLT_Midtest = 0;
                Int32 CLT_Posttes = 0;
                string Clt_Pre_PC = "";
                string Clt_Mid_PC = "";
                string Clt_Post_PC = "";

                if (rblPartialPre.Checked == true || rblCompletePre.Checked == true)
                {
                    if (rblTestTBPre.Checked == true)
                    {
                        CLT_Pretest_TB = 1;
                    }
                    if (rblTestpreFC.Checked == true)
                    {
                        CLT_Pretest_FC = 1;
                    }
                    if (rblPartialPre.Checked == true)
                    {
                        Clt_Pre_PC = "P";
                        CLT_Pretest = 1;
                    }
                    if (rblCompletePre.Checked == true)
                    {
                        Clt_Pre_PC = "C";
                        CLT_Pretest = 1;
                    }
                }
                if (rblPartialMid.Checked == true || rblCompleteMid.Checked == true)
                {
                    if (rblTestTBMid.Checked == true)
                    {
                        CTL_Midtest_TB = 1;
                    }
                    if (rblTestMidFC.Checked == true)
                    {
                        CTL_Midtest_FC = 1;
                    }
                    if (rblPartialMid.Checked == true)
                    {
                        Clt_Mid_PC = "P";
                        CLT_Midtest = 1;
                    }
                    if (rblCompleteMid.Checked == true)
                    {
                        Clt_Mid_PC = "C";
                        CLT_Midtest = 1;
                    }
                }

                if (rblPartialPost.Checked == true || rblCompletePost.Checked == true)
                {
                    if (rblTestTBPost.Checked == true)
                    {
                        CLT_Posttest_TB = 1;
                    }
                    if (rblTestPostFC.Checked == true)
                    {
                        CLT_Posttest_FC = 1;
                    }
                    if (rblPartialPost.Checked == true)
                    {
                        Clt_Post_PC = "P";
                        CLT_Posttes = 1;
                    }
                    if (rblCompletePost.Checked == true)
                    {
                        Clt_Post_PC = "C";
                        CLT_Posttes = 1;
                    }
                }
                #endregion

        #region Balsabha
                Int32 BalsabaTB = 0;
                Int32 BalsabFC = 0;

                Int32 BalSabha_Formation = 0;
                Int32 BalSabha_Orientation = 0;
                Int32 BalSabha_Chart = 0;
                Int32 BalSabha_Kit = 0;
                Int32 Bal = 0;
                if (chkBalSabhaFor.Checked == true)
                {
                    BalSabha_Formation = 1;
                }
                if (chkOrientation.Checked == true)
                {
                    BalSabha_Orientation = 1;
                }
                if (chkChat.Checked == true)
                {
                    BalSabha_Chart = 1;
                }
                if (chkKit.Checked == true)
                {
                    BalSabha_Kit = 1;
                }
                if (chkBalSabhaFor.Checked == true || chkOrientation.Checked == true || chkChat.Checked == true || chkKit.Checked == true)
                {
                    Bal = 1;
                    if (rblBalsabaTB.Checked == true)
                    {
                        BalsabaTB = 1;
                    }
                    if (rblBalsabaFC.Checked == true)
                    {
                        BalsabFC = 1;
                    }
                }
                Int32 LifeTB = 0;
                Int32 LifeFC = 0;
                if (rblLifeTB.Checked == true)
                {
                    LifeTB = 1;
                }
                if (rblLifeFC.Checked == true)
                {
                    LifeFC = 1;
                }
                #endregion
        
        #region Game

                Int32 Game_TB = 0;
                Int32 Game_FC = 0;
                Int32 Game = 0;
                string GameEntry = "";

                DataTable dtGame = CreateDataGame();
                if (chkGame1.Checked == true)
                {
                    dr = dtGame.NewRow();
                    dr["GUID_School"] = UNICOde;
                    dr["VillageCode"] = ddlVilage.SelectedValue;
                    dr["SchoolCode"] = ddlSchool.SelectedValue;
                    dr["ActivityDate"] = Convert.ToDateTime(FcDate).ToString("yyyy-MM-dd");
                    dr["GameNo"] = 1;


                    dtGame.Rows.Add(dr);
                    GameEntry += 1 + ",";
                }
                if (chkGame2.Checked == true)
                {
                    dr = dtGame.NewRow();
                    dr["GUID_School"] = UNICOde;
                    dr["VillageCode"] = ddlVilage.SelectedValue;
                    dr["SchoolCode"] = ddlSchool.SelectedValue;
                    dr["ActivityDate"] = Convert.ToDateTime(FcDate).ToString("yyyy-MM-dd");
                    dr["GameNo"] = 2;


                    dtGame.Rows.Add(dr);
                    GameEntry += 2 + ",";
                }
                if (chkGame3.Checked == true)
                {
                    dr = dtGame.NewRow();
                    dr["GUID_School"] = UNICOde;
                    dr["VillageCode"] = ddlVilage.SelectedValue;
                    dr["SchoolCode"] = ddlSchool.SelectedValue;
                    dr["ActivityDate"] = Convert.ToDateTime(FcDate).ToString("yyyy-MM-dd");
                    dr["GameNo"] = 3;


                    dtGame.Rows.Add(dr);
                    GameEntry += 3 + ",";
                }
                if (chkGame4.Checked == true)
                {
                    dr = dtGame.NewRow();
                    dr["GUID_School"] = UNICOde;
                    dr["VillageCode"] = ddlVilage.SelectedValue;
                    dr["SchoolCode"] = ddlSchool.SelectedValue;
                    dr["ActivityDate"] = Convert.ToDateTime(FcDate).ToString("yyyy-MM-dd");
                    dr["GameNo"] = 4;


                    dtGame.Rows.Add(dr);
                    GameEntry += 4 + ",";
                }

                if (chkGame5.Checked == true)
                {
                    dr = dtGame.NewRow();
                    dr["GUID_School"] = UNICOde;
                    dr["VillageCode"] = ddlVilage.SelectedValue;
                    dr["SchoolCode"] = ddlSchool.SelectedValue;
                    dr["ActivityDate"] = Convert.ToDateTime(FcDate).ToString("yyyy-MM-dd");
                    dr["GameNo"] = 5;


                    dtGame.Rows.Add(dr);
                    GameEntry += 5 + ",";
                }

                if (GameEntry.Length > 0)
                {
                    GameEntry = GameEntry.Substring(0, GameEntry.LastIndexOf(","));

                }
                if (dtGame.Rows.Count > 0)
                {
                    if (rblLifeTB.Checked == true)
                    {
                        Game_TB = 1;
                    }
                    if (rblLifeFC.Checked == true)
                    {
                        Game_FC = 2;
                    }
                    Game = 1;
                }
                #endregion

        #region SAC Update
                Int32 SACTB = 0;
                Int32 SACFC = 0;
                Int32 SAC = 0;

                int SAC_No_Of_Attended = 0;
                if (txtSMCMeeting.Text.Trim() != "")
                {
                    SAC_No_Of_Attended = Convert.ToInt32(txtSMCMeeting.Text);
                }

                int SAC_Periodic_Checkup = 0;
                if (txtHealth.Text.Trim() != "")
                {
                    SAC_Periodic_Checkup = Convert.ToInt32(txtHealth.Text);
                }

                int SAC_Listing_Name_Of_Girls = 0;
                if (txtAdgirls.Text.Trim() != "")
                {
                    SAC_Listing_Name_Of_Girls = Convert.ToInt32(txtAdgirls.Text);
                }
                int SAC_Listing_Name_Of_Boys = 0;
                if (txtAdBoy.Text.Trim() != "")
                {
                    SAC_Listing_Name_Of_Boys = Convert.ToInt32(txtAdBoy.Text);
                }

                int SAC_Girls_Left = 0;
                if (txtleftGirl.Text.Trim() != "")
                {
                    SAC_Girls_Left = Convert.ToInt32(txtleftGirl.Text);
                }
                int SAC_Boys_Left = 0;
                if (txtleftBoy.Text.Trim() != "")
                {
                    SAC_Boys_Left = Convert.ToInt32(txtleftBoy.Text);
                }

                int SAC_Girls_Not_Joined_School = 0;
                if (txtGirlNot.Text.Trim() != "")
                {
                    SAC_Girls_Not_Joined_School = Convert.ToInt32(txtGirlNot.Text);
                }

                int SAC_Boys_Not_Joined_School = 0;
                if (txtBoyNot.Text.Trim() != "")
                {
                    SAC_Boys_Not_Joined_School = Convert.ToInt32(txtBoyNot.Text);
                }
                if (rblSacTB.Checked == true)
                {
                    SACTB = 1;
                }
                if (rblSacFB.Checked == true)
                {
                    SACFC = 1;
                }
                if (chkSACUpdate.Checked == true)
                {
                    SAC = 1;
                }
              
                #endregion

        #region SPF
                int Infrastructure = 0;
                int Classrooms = 0;
                int DrinkingWater = 0;
                int GirlsToilet = 0;
                int Electricity = 0;
                int Playground = 0;
                int Slide = 0;
                int BoundaryWall = 0;
                int Kitchen = 0;
                int Teachers_Male = 0;
                int Teachers_Female = 0;
                int CLT_Kit = 0;
                int bookAvl = 0;
                int Infrastructure_FC = 0; int Infrastructure_TB = 0;

                if (txtClassRoom.Text != "")
                {
                    Classrooms = Convert.ToInt32(txtClassRoom.Text);
                }
                if (lbldriking.Text == "1")
                {
                    DrinkingWater = 1;
                }
                else if (lbldriking.Text == "2")
                {

                    DrinkingWater = 2;
                }
                else if (lbldriking.Text == "3")
                {

                    DrinkingWater = 3;
                }
                else if (lbldriking.Text == "4")
                {

                    DrinkingWater = 4;
                }

                if (lblToilet.Text == "1")
                {
                    GirlsToilet = 1;
                }
                else if (lblToilet.Text == "2")
                {

                    GirlsToilet = 2;
                }
                else if (lblToilet.Text == "3")
                {

                    GirlsToilet = 3;
                }
                else if (lblToilet.Text == "4")
                {

                    GirlsToilet = 4;
                }

                if (lblElectricity.Text == "1")
                {
                    Electricity = 1;
                }
                else if (lblElectricity.Text == "2")
                {

                    Electricity = 2;
                }
                else if (lblElectricity.Text == "3")
                {

                    Electricity = 3;
                }
                else if (lblElectricity.Text == "4")
                {

                    Electricity = 4;
                }

                if (lblPlay.Text == "1")
                {
                    Playground = 1;
                }
                else if (lblPlay.Text == "2")
                {

                    Playground = 2;
                }
                else if (lblPlay.Text == "3")
                {

                    Playground = 3;
                }
                else if (lblPlay.Text == "4")
                {

                    Playground = 4;
                }


                if (lblPlay.Text == "1")
                {
                    Slide = 1;
                }
                else if (lblSlides.Text == "2")
                {

                    Slide = 2;
                }
                else if (lblSlides.Text == "3")
                {

                    Slide = 3;
                }
                else if (lblSlides.Text == "4")
                {

                    Slide = 4;
                }

                if (lblBoundaryWall.Text == "1")
                {
                    BoundaryWall = 1;
                }
                else if (lblBoundaryWall.Text == "2")
                {

                    BoundaryWall = 2;
                }
                else if (lblBoundaryWall.Text == "3")
                {

                    BoundaryWall = 3;
                }
                else if (lblBoundaryWall.Text == "4")
                {

                    BoundaryWall = 4;
                }


                if (lblSlides.Text == "1")
                {
                    Slide = 1;
                }
                else if (lblSlides.Text == "2")
                {

                    Slide = 2;
                }
                else if (lblSlides.Text == "3")
                {

                    Slide = 3;
                }
                else if (lblSlides.Text == "4")
                {

                    Slide = 4;
                }

                if (lblKitchen.Text == "1")
                {
                    Kitchen = 1;
                }
                else if (lblKitchen.Text == "2")
                {

                    Kitchen = 2;
                }
                else if (lblKitchen.Text == "3")
                {

                    Kitchen = 3;
                }
                else if (lblKitchen.Text == "4")
                {

                    Kitchen = 4;
                }
                if (lblCltKit.Text == "1")
                {
                    CLT_Kit = 1;
                }
                else if (lblCltKit.Text == "2")
                {

                    CLT_Kit = 2;
                }
                else if (lblCltKit.Text == "3")
                {

                    CLT_Kit = 3;
                }
                else if (lblCltKit.Text == "4")
                {

                    CLT_Kit = 4;
                }

                if (lblbook.Text == "1")
                {
                    bookAvl = 1;
                }
                else if (lblbook.Text == "2")
                {

                    bookAvl = 2;
                }
                else if (lblbook.Text == "3")
                {

                    bookAvl = 3;
                }
                else if (lblbook.Text == "4")
                {

                    bookAvl = 4;
                }




                if (txtFemaleTeacher.Text != "")
                {
                    Teachers_Female = Convert.ToInt32(txtFemaleTeacher.Text);
                }
                if (txtMaleTeacher.Text != "")
                {
                    Teachers_Male = Convert.ToInt32(txtMaleTeacher.Text);
                }
                if (rblPhysicalTB.Checked == true)
                {
                    Infrastructure_TB = 1;
                }
                if (rblPhysicalFC.Checked == true)
                {
                    Infrastructure_FC = 1;
                }
                if (chkPhysical.Checked == true)
                {
                    Infrastructure = 1;
                }
               
                #endregion

        #region Annaul
                int SIP_Annual_FC = 0; int SIP_Annual_TB = 0; int Retention_Annual_FC = 0; int Retention_Annual_TB = 0; int AnnualData = 0; int SIP_Annual = 0; int Retention_Annual = 0;
                int Other_FC = 0;
                string Retention_PC = "", SIP_PC = "", Other_TB = "";
                if (chkAnnual.Checked == true)
                {
                    AnnualData = 1;
                }

                if (chkSIPAnnaul.Checked == true)
                {
                    SIP_Annual = 1;
                }
                if (chkRetention.Checked == true)
                {
                    Retention_Annual = 1;
                }
                if (chkSIPTB.Checked == true)
                {
                    SIP_Annual_TB = 1;
                }
                if (chkRenTB.Checked == true)
                {
                    Retention_Annual_TB = 1;
                }

                if (chkSIPFC.Checked == true)
                {
                    SIP_Annual_FC = 1;
                }
                if (chkRenFC.Checked == true)
                {
                    Retention_Annual_FC = 1;
                }



                if (chkSipPartial.Checked == true)
                {
                    SIP_PC = "P";
                }

                if (chkSipComplete.Checked == true)
                {
                    SIP_PC = "C";
                }
                if (chkComplete.Checked == true)
                {
                    Retention_PC = "C";
                }
                if (chkRenPartial.Checked == true)
                {
                    Retention_PC = "P";
                }
                Other_TB = txtOther.Text;
                #endregion

        #region FinalSave
                Int32 MainResult = 0;
                if (ViewState["GUID_School"].ToString().Length > 5)
                {
                    string userid = "";
                    if (Session["user_level"].ToString() == "19")
                    {
                        userid = "2";

                        MainResult = objMain.InsertUpdateActivitySchool(ViewState["GUID_School"].ToString(), ddlVilage.SelectedValue, ddlUser.SelectedValue.ToString(), ddlSchool.SelectedValue.ToString(), Convert.ToDateTime(FcDate), TBHolding.ToString(), SMC.ToString(), SMCTB.ToString(), SMCFC.ToString(), OtherSIPprepared.ToString(), OtherSIPcompleted.ToString(), commmeeting, SMCOrient.ToString(), SMCOrientTB.ToString(), SMCOrientFC.ToString(), TotalMember.ToString(), TotalFemaSmcFemal.ToString(), CLT.ToString(), CLT_TB.ToString(), CLT_FC.ToString(), CLTHindi, CltEnglish, CltMath, CLT_Pretest_FC.ToString(), CLT_Pretest_TB.ToString(), CTL_Midtest_FC.ToString(), CTL_Midtest_TB.ToString(), CLT_Posttest_FC.ToString(), CLT_Posttest_TB.ToString(), Clt_Pre_PC.ToString(), Clt_Mid_PC.ToString(), Clt_Post_PC.ToString(), Bal.ToString(), BalsabaTB.ToString(), BalsabFC.ToString(), BalSabha_Formation.ToString(), BalSabha_Orientation.ToString(), BalSabha_Chart.ToString(), BalSabha_Kit.ToString(), Game.ToString(), Game_TB.ToString(), Game_FC.ToString(), SACTB.ToString(), SACFC.ToString(), SAC.ToString(), SAC_Periodic_Checkup.ToString(), SAC_Listing_Name_Of_Girls.ToString(), SAC_Listing_Name_Of_Boys.ToString(), SAC_Girls_Left.ToString(), SAC_Boys_Left.ToString(), SAC_Girls_Not_Joined_School.ToString(), SAC_Boys_Not_Joined_School.ToString(), SAC_No_Of_Attended.ToString(), Classrooms, DrinkingWater, GirlsToilet, Electricity, Playground, Slide, BoundaryWall, Kitchen, Teachers_Male, Teachers_Female, CLT_Kit, bookAvl, Infrastructure, Infrastructure_FC, Infrastructure_TB, SIP_Annual_FC, SIP_Annual_TB, Retention_Annual_FC, Retention_Annual_TB, AnnualData, SIP_Annual, Retention_Annual, SIP_PC, Retention_PC, txtOther.Text, GameEntry, userid, "U", "FC", CLT_Pretest, CLT_Midtest, CLT_Posttes, ddlRemark.SelectedValue, Session["username"].ToString());
                   
                        //MainResult = objMain.ActivitySchool(ViewState["GUID_School"].ToString(), ddlVilage.SelectedValue, ddlUser.SelectedValue.ToString(), ddlSchool.SelectedValue.ToString(), Convert.ToDateTime(FcDate), TBHolding.ToString(), SMC.ToString(), SMC_TB.ToString(), SMC_FC.ToString(), totalMemberTrain.ToString(), MemberTrain.ToString(), SMCMeeting.ToString(), OtherSP.ToString(), commmeeting.ToString(), CLT.ToString(), CLT_TB.ToString(), CLT_FC.ToString(), Bal.ToString(), BalsabaTB.ToString(), BalsabFC.ToString(), BalSabha_Formation.ToString(), BalSabha_Orientation.ToString(), BalSabha_Chart.ToString(), BalSabha_Kit.ToString(), "U", Game.ToString(), Game_TB.ToString(), Game_FC.ToString(), SACTB.ToString(), SACFC.ToString(), SAC.ToString(), SAC_Periodic_Checkup.ToString(), SAC_Listing_Name_Of_Girls.ToString(), SAC_Listing_Name_Of_Boys.ToString(), SAC_Girls_Left.ToString(), SAC_Boys_Left.ToString(), SAC_Girls_Not_Joined_School.ToString(), SAC_Boys_Not_Joined_School.ToString(), SAC_No_Of_Attended.ToString(), userid, CLTHindi, CltEnglish, CltMath, GameEntry, Convert.ToInt32(Session["user_level"].ToString()), Classrooms, DrinkingWater, GirlsToilet, Electricity, Playground, Slide, BoundaryWall, Kitchen, Teachers_Male, Teachers_Female, CLT_Kit, bookAvl, SIP_Annual_FC, SIP_Annual_TB, Retention_Annual_FC, Retention_Annual_TB, AnnualData, SIP_Annual, Retention_Annual, Infrastructure_FC, Infrastructure_TB, Other_TB, Other_FC, CLT_Pretest_FC.ToString(), CLT_Pretest_TB.ToString(), CTL_Midtest_FC.ToString(), CTL_Midtest_TB.ToString(), CLT_Posttest_FC.ToString(), CLT_Posttest_TB.ToString(), Clt_Pre_PC.ToString(), Infrastructure, Clt_Mid_PC.ToString(), Clt_Post_PC.ToString(), SIP_PC, Retention_PC);
                        userid = "3";
                        MainResult = objMain.InsertUpdateActivitySchool(ViewState["GUID_School"].ToString(), ddlVilage.SelectedValue, ddlUser.SelectedValue.ToString(), ddlSchool.SelectedValue.ToString(), Convert.ToDateTime(FcDate), TBHolding.ToString(), SMC.ToString(), SMCTB.ToString(), SMCFC.ToString(), OtherSIPprepared.ToString(), OtherSIPcompleted.ToString(), commmeeting, SMCOrient.ToString(), SMCOrientTB.ToString(), SMCOrientFC.ToString(), TotalMember.ToString(), TotalFemaSmcFemal.ToString(), CLT.ToString(), CLT_TB.ToString(), CLT_FC.ToString(), CLTHindi, CltEnglish, CltMath, CLT_Pretest_FC.ToString(), CLT_Pretest_TB.ToString(), CTL_Midtest_FC.ToString(), CTL_Midtest_TB.ToString(), CLT_Posttest_FC.ToString(), CLT_Posttest_TB.ToString(), Clt_Pre_PC.ToString(), Clt_Mid_PC.ToString(), Clt_Post_PC.ToString(), Bal.ToString(), BalsabaTB.ToString(), BalsabFC.ToString(), BalSabha_Formation.ToString(), BalSabha_Orientation.ToString(), BalSabha_Chart.ToString(), BalSabha_Kit.ToString(), Game.ToString(), Game_TB.ToString(), Game_FC.ToString(), SACTB.ToString(), SACFC.ToString(), SAC.ToString(), SAC_Periodic_Checkup.ToString(), SAC_Listing_Name_Of_Girls.ToString(), SAC_Listing_Name_Of_Boys.ToString(), SAC_Girls_Left.ToString(), SAC_Boys_Left.ToString(), SAC_Girls_Not_Joined_School.ToString(), SAC_Boys_Not_Joined_School.ToString(), SAC_No_Of_Attended.ToString(), Classrooms, DrinkingWater, GirlsToilet, Electricity, Playground, Slide, BoundaryWall, Kitchen, Teachers_Male, Teachers_Female, CLT_Kit, bookAvl, Infrastructure, Infrastructure_FC, Infrastructure_TB, SIP_Annual_FC, SIP_Annual_TB, Retention_Annual_FC, Retention_Annual_TB, AnnualData, SIP_Annual, Retention_Annual, SIP_PC, Retention_PC, txtOther.Text, GameEntry, userid, "U", "FC", CLT_Pretest, CLT_Midtest, CLT_Posttes, ddlRemark.SelectedValue, Session["username"].ToString());
                   

                    }
                    if (Session["user_level"].ToString() == "39" || Session["user_level"].ToString() == "30")
                    {
                        MainResult = objMain.InsertUpdateActivitySchool(ViewState["GUID_School"].ToString(), ddlVilage.SelectedValue, ddlUser.SelectedValue.ToString(), ddlSchool.SelectedValue.ToString(), Convert.ToDateTime(FcDate), TBHolding.ToString(), SMC.ToString(), SMCTB.ToString(), SMCFC.ToString(), OtherSIPprepared.ToString(), OtherSIPcompleted.ToString(), commmeeting, SMCOrient.ToString(), SMCOrientTB.ToString(), SMCOrientFC.ToString(), TotalMember.ToString(), TotalFemaSmcFemal.ToString(), CLT.ToString(), CLT_TB.ToString(), CLT_FC.ToString(), CLTHindi, CltEnglish, CltMath, CLT_Pretest_FC.ToString(), CLT_Pretest_TB.ToString(), CTL_Midtest_FC.ToString(), CTL_Midtest_TB.ToString(), CLT_Posttest_FC.ToString(), CLT_Posttest_TB.ToString(), Clt_Pre_PC.ToString(), Clt_Mid_PC.ToString(), Clt_Post_PC.ToString(), Bal.ToString(), BalsabaTB.ToString(), BalsabFC.ToString(), BalSabha_Formation.ToString(), BalSabha_Orientation.ToString(), BalSabha_Chart.ToString(), BalSabha_Kit.ToString(), Game.ToString(), Game_TB.ToString(), Game_FC.ToString(), SACTB.ToString(), SACFC.ToString(), SAC.ToString(), SAC_Periodic_Checkup.ToString(), SAC_Listing_Name_Of_Girls.ToString(), SAC_Listing_Name_Of_Boys.ToString(), SAC_Girls_Left.ToString(), SAC_Boys_Left.ToString(), SAC_Girls_Not_Joined_School.ToString(), SAC_Boys_Not_Joined_School.ToString(), SAC_No_Of_Attended.ToString(), Classrooms, DrinkingWater, GirlsToilet, Electricity, Playground, Slide, BoundaryWall, Kitchen, Teachers_Male, Teachers_Female, CLT_Kit, bookAvl, Infrastructure, Infrastructure_FC, Infrastructure_TB, SIP_Annual_FC, SIP_Annual_TB, Retention_Annual_FC, Retention_Annual_TB, AnnualData, SIP_Annual, Retention_Annual, SIP_PC, Retention_PC, txtOther.Text, GameEntry, "3", "U", "B", CLT_Pretest, CLT_Midtest, CLT_Posttes, ddlRemark.SelectedValue, Session["username"].ToString());
                   
                    }
                }
                else
                {

                    //string StudentTSInsertQuery = " INSERT INTO tblActivityUpdate_School([GUID_School],[VillageCode] ,[UserID] ,[SchoolCode],[ActivityDate] ,[TB_Handholding], [SMC]  ,[SMC_TB] ,[SMC_FC]                                                        ,[SMC_TotTrained] ,[SMC_FemaleTrained] ,      [SMC_Mtg] ,[SMC_OtherSIP] ,[SMC_OtherDiscussions],  [CLT],[CLT_TB] ,[CLT_FC] , [CLT_Pretest] ,[CLT_Posttest] ,[CTL_Midtest],[BalSabha] ,[BalSabha_TB]  ,[BalSabha_FC] ,[BalSabha_Formation] ,[BalSabha_Orientation]      ,[BalSabha_Chart]   ,[BalSabha_Kit]) ";
                    //StudentTSInsertQuery += " Values('" + UNICOde + "','" + ddlVilage.SelectedValue + "','" + ddlUser.SelectedValue + "','" + ddlSchool.SelectedValue + "','" + Convert.ToDateTime(FcDate).ToString("yyyy-MM-dd") + "','" + TBHolding + "','" + 1 + "','" + SMC_TB + "','" + SMC_FC + "','" + totalMemberTrain + "'," + MemberTrain + "," + SMCMeeting + "," + OtherSP + ",'" + commmeeting + "','1','" + CLT_TB + "','" + CLT_FC + "','" + CLT_PreTest + "','" + CLT_PostTest + "','" + CLT_MidTest + "','" + 1 + "','" + BalsabaTB + "','" + BalsabFC + "','" + BalSabha_Formation + "','" + BalSabha_Orientation + "','" + BalSabha_Chart + "','" + BalSabha_Kit + "')";
                    // InsertTS = objMain.AddUpdate(StudentTSInsertQuery);
                    if (Session["user_level"].ToString() == "19")
                    {
                        MainResult = objMain.InsertUpdateActivitySchool(UNICOde, ddlVilage.SelectedValue, ddlUser.SelectedValue.ToString(), ddlSchool.SelectedValue.ToString(), Convert.ToDateTime(FcDate), TBHolding.ToString(), SMC.ToString(), SMCTB.ToString(), SMCFC.ToString(), OtherSIPprepared.ToString(), OtherSIPcompleted.ToString(), commmeeting, SMCOrient.ToString(), SMCOrientTB.ToString(), SMCOrientFC.ToString(), TotalMember.ToString(), TotalFemaSmcFemal.ToString(), CLT.ToString(), CLT_TB.ToString(), CLT_FC.ToString(), CLTHindi, CltEnglish, CltMath, CLT_Pretest_FC.ToString(), CLT_Pretest_TB.ToString(), CTL_Midtest_FC.ToString(), CTL_Midtest_TB.ToString(), CLT_Posttest_FC.ToString(), CLT_Posttest_TB.ToString(), Clt_Pre_PC.ToString(), Clt_Mid_PC.ToString(), Clt_Post_PC.ToString(), Bal.ToString(), BalsabaTB.ToString(), BalsabFC.ToString(), BalSabha_Formation.ToString(), BalSabha_Orientation.ToString(), BalSabha_Chart.ToString(), BalSabha_Kit.ToString(), Game.ToString(), Game_TB.ToString(), Game_FC.ToString(), SACTB.ToString(), SACFC.ToString(), SAC.ToString(), SAC_Periodic_Checkup.ToString(), SAC_Listing_Name_Of_Girls.ToString(), SAC_Listing_Name_Of_Boys.ToString(), SAC_Girls_Left.ToString(), SAC_Boys_Left.ToString(), SAC_Girls_Not_Joined_School.ToString(), SAC_Boys_Not_Joined_School.ToString(), SAC_No_Of_Attended.ToString(), Classrooms, DrinkingWater, GirlsToilet, Electricity, Playground, Slide, BoundaryWall, Kitchen, Teachers_Male, Teachers_Female, CLT_Kit, bookAvl, Infrastructure, Infrastructure_FC, Infrastructure_TB, SIP_Annual_FC, SIP_Annual_TB, Retention_Annual_FC, Retention_Annual_TB, AnnualData, SIP_Annual, Retention_Annual, SIP_PC, Retention_PC, txtOther.Text, GameEntry, "2", "I", "FC", CLT_Pretest, CLT_Midtest, CLT_Posttes, ddlRemark.SelectedValue, Session["username"].ToString());
                        MainResult = objMain.InsertUpdateActivitySchool(UNICOde, ddlVilage.SelectedValue, ddlUser.SelectedValue.ToString(), ddlSchool.SelectedValue.ToString(), Convert.ToDateTime(FcDate), TBHolding.ToString(), SMC.ToString(), SMCTB.ToString(), SMCFC.ToString(), OtherSIPprepared.ToString(), OtherSIPcompleted.ToString(), commmeeting, SMCOrient.ToString(), SMCOrientTB.ToString(), SMCOrientFC.ToString(), TotalMember.ToString(), TotalFemaSmcFemal.ToString(), CLT.ToString(), CLT_TB.ToString(), CLT_FC.ToString(), CLTHindi, CltEnglish, CltMath, CLT_Pretest_FC.ToString(), CLT_Pretest_TB.ToString(), CTL_Midtest_FC.ToString(), CTL_Midtest_TB.ToString(), CLT_Posttest_FC.ToString(), CLT_Posttest_TB.ToString(), Clt_Pre_PC.ToString(), Clt_Mid_PC.ToString(), Clt_Post_PC.ToString(), Bal.ToString(), BalsabaTB.ToString(), BalsabFC.ToString(), BalSabha_Formation.ToString(), BalSabha_Orientation.ToString(), BalSabha_Chart.ToString(), BalSabha_Kit.ToString(), Game.ToString(), Game_TB.ToString(), Game_FC.ToString(), SACTB.ToString(), SACFC.ToString(), SAC.ToString(), SAC_Periodic_Checkup.ToString(), SAC_Listing_Name_Of_Girls.ToString(), SAC_Listing_Name_Of_Boys.ToString(), SAC_Girls_Left.ToString(), SAC_Boys_Left.ToString(), SAC_Girls_Not_Joined_School.ToString(), SAC_Boys_Not_Joined_School.ToString(), SAC_No_Of_Attended.ToString(), Classrooms, DrinkingWater, GirlsToilet, Electricity, Playground, Slide, BoundaryWall, Kitchen, Teachers_Male, Teachers_Female, CLT_Kit, bookAvl, Infrastructure, Infrastructure_FC, Infrastructure_TB, SIP_Annual_FC, SIP_Annual_TB, Retention_Annual_FC, Retention_Annual_TB, AnnualData, SIP_Annual, Retention_Annual, SIP_PC, Retention_PC, txtOther.Text, GameEntry, "3", "I", "FC", CLT_Pretest, CLT_Midtest, CLT_Posttes, ddlRemark.SelectedValue, Session["username"].ToString());
                    }

                    if (Session["user_level"].ToString() == "39" || Session["user_level"].ToString() == "30")
                    {

                        MainResult = objMain.InsertUpdateActivitySchool(UNICOde, ddlVilage.SelectedValue, ddlUser.SelectedValue.ToString(), ddlSchool.SelectedValue.ToString(), Convert.ToDateTime(FcDate), TBHolding.ToString(), SMC.ToString(), SMCTB.ToString(), SMCFC.ToString(), OtherSIPprepared.ToString(), OtherSIPcompleted.ToString(), commmeeting, SMCOrient.ToString(), SMCOrientTB.ToString(), SMCOrientFC.ToString(), TotalMember.ToString(), TotalFemaSmcFemal.ToString(), CLT.ToString(), CLT_TB.ToString(), CLT_FC.ToString(), CLTHindi, CltEnglish, CltMath, CLT_Pretest_FC.ToString(), CLT_Pretest_TB.ToString(), CTL_Midtest_FC.ToString(), CTL_Midtest_TB.ToString(), CLT_Posttest_FC.ToString(), CLT_Posttest_TB.ToString(), Clt_Pre_PC.ToString(), Clt_Mid_PC.ToString(), Clt_Post_PC.ToString(), Bal.ToString(), BalsabaTB.ToString(), BalsabFC.ToString(), BalSabha_Formation.ToString(), BalSabha_Orientation.ToString(), BalSabha_Chart.ToString(), BalSabha_Kit.ToString(), Game.ToString(), Game_TB.ToString(), Game_FC.ToString(), SACTB.ToString(), SACFC.ToString(), SAC.ToString(), SAC_Periodic_Checkup.ToString(), SAC_Listing_Name_Of_Girls.ToString(), SAC_Listing_Name_Of_Boys.ToString(), SAC_Girls_Left.ToString(), SAC_Boys_Left.ToString(), SAC_Girls_Not_Joined_School.ToString(), SAC_Boys_Not_Joined_School.ToString(), SAC_No_Of_Attended.ToString(), Classrooms, DrinkingWater, GirlsToilet, Electricity, Playground, Slide, BoundaryWall, Kitchen, Teachers_Male, Teachers_Female, CLT_Kit, bookAvl, Infrastructure, Infrastructure_FC, Infrastructure_TB, SIP_Annual_FC, SIP_Annual_TB, Retention_Annual_FC, Retention_Annual_TB, AnnualData, SIP_Annual, Retention_Annual, SIP_PC, Retention_PC, txtOther.Text, GameEntry, "3", "I", "B", CLT_Pretest, CLT_Midtest, CLT_Posttes, ddlRemark.SelectedValue,Session["username"].ToString());
     
                    }
                }

              
                if (MainResult > 0)
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Saved sucessfully')</script>", false);
                    ViewState["GUID_School"] = UNICOde;
                }

                //if (dtSubject.Rows.Count > 0)
                //{
                //    string SchoolDelete = " delete from tblActivityUpdate_CTLImplementation where  [GUID_School] ='" + dtSubject.Rows[0]["GUID_School"] + "' ";
                //    bool DeleteSubject = objMain.AddUpdate(SchoolDelete);
                //    foreach (DataRow drItem in dtSubject.Rows)
                //    {

                //        int Result = objMain.CTLImplementation(drItem["GUID_School"].ToString(), drItem["VillageCode"].ToString(), drItem["SchoolCode"].ToString(), Convert.ToDateTime(drItem["ActivityDate"]), Convert.ToInt32(drItem["Subject"]), drItem["CLTGroup"].ToString());
                //        //SchoolSubject += " insert into tblActivityUpdate_CTLImplementation( [GUID_School]   ,[VillageCode]    ,[SchoolCode] ,[ActivityDate]  ,[Subject]    ,[CLTGroup])  Values('" + drItem["GUID_School"] + "','" + drItem["VillageCode"] + "','" + drItem["SchoolCode"] + "','" + drItem["ActivityDate"] + "','" + drItem["Subject"] + "','" + drItem["CLTGroup"] + "') ";
                //        //bool InsertD2d = objMain.AddUpdate(SchoolSubject);
                //    }
                //}
                //if (dtGame.Rows.Count > 0)
                //{
                //    string SchoolDelete = " delete from tblActivityUpdate_LifeskillGames where  [GUID_School]='" + dtSubject.Rows[0]["GUID_School"] + "' ";
                //    bool DeleteSubject = objMain.AddUpdate(SchoolDelete);
                //    foreach (DataRow drItem in dtGame.Rows)
                //    {
                //        int Result = objMain.LifeskillGames(drItem["GUID_School"].ToString(), drItem["VillageCode"].ToString(), drItem["SchoolCode"].ToString(), Convert.ToDateTime(drItem["ActivityDate"]), Convert.ToInt32(drItem["GameNo"]));


                //    }
                //}

       
        #endregion
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (!Validation())
            return;
        Save();
        LoadDataschool();
    }
    public DataTable CreateDataDate()
    {

        DataTable dtSubject = new DataTable();


        dtSubject.Columns.Add(new DataColumn("GUID_School", System.Type.GetType("System.String")));
        dtSubject.Columns.Add(new DataColumn("VillageCode", System.Type.GetType("System.String")));
        dtSubject.Columns.Add(new DataColumn("SchoolCode", System.Type.GetType("System.String")));
        dtSubject.Columns.Add(new DataColumn("ActivityDate", System.Type.GetType("System.DateTime")));
        dtSubject.Columns.Add(new DataColumn("Subject", System.Type.GetType("System.Int32")));
        dtSubject.Columns.Add(new DataColumn("CLTGroup", System.Type.GetType("System.String")));
        ViewState["dtSubject"] = dtSubject;
        return dtSubject;
    }
    public DataTable CreateDataGame()
    {

        DataTable dtGame = new DataTable();


        dtGame.Columns.Add(new DataColumn("GUID_School", System.Type.GetType("System.String")));
        dtGame.Columns.Add(new DataColumn("VillageCode", System.Type.GetType("System.String")));
        dtGame.Columns.Add(new DataColumn("SchoolCode", System.Type.GetType("System.String")));
        dtGame.Columns.Add(new DataColumn("ActivityDate", System.Type.GetType("System.DateTime")));

        dtGame.Columns.Add(new DataColumn("GameNo", System.Type.GetType("System.Int32")));
        ViewState["dtGame"] = dtGame;
        return dtGame;
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
    public void LoadData()
    {
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
        if (txtDate.Text == "")
        {
            ModalPopupExtender.Hide();
            this.ModalPopupExtender1.Hide();
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select Date')</script>", false);
            return;
        }
        if (ddlSchool.SelectedIndex <= 0)
        {
            ModalPopupExtender.Hide();
            this.ModalPopupExtender1.Hide();
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select School')</script>", false);
            return;
        }


        ClearData();
        LoadDataschool();
        LoadDataschoolPre();
        if (this.ddlRemark.SelectedIndex > 0)
        {
            pnlMain.Enabled = true;
        }
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        if (ViewState["GUID_School"].ToString().Length > 5)
        {
            int res1 = 0;
            if (ddlRemark.SelectedIndex > 0)
            {
                 res1 = objMain.DeleteD2dDataAcctivtiyAchool(ViewState["GUID_School"].ToString());
                 if (res1 > 0)
                 {
                     ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Delete Sucessfully')</script>", false);
                 }
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select Remark')</script>", false);
 
            }
        }
    }
    protected void btnSerach_Click(object sender, EventArgs e)
    {
      
        LoadData();
     
    }
    public void LoadDataschoolPre()
    {
        string Dateof = txtDate.Text;
        string[] b = Dateof.Split('/');

        string FcDate = b[2] + '-' + b[1] + '-' + b[0];

        string strQry = "";

        string userid = "";
        if (Session["user_level"].ToString() == "19")
        {
            userid = "2";
        }
        if (Session["user_level"].ToString() == "39" || Session["user_level"].ToString() == "30")
        {
            userid = "3";
        }
        SqlParameter[] parm = new SqlParameter[]
             {
              
               new SqlParameter("@SchoolCode",  ddlSchool.SelectedValue),
               
                  new SqlParameter("@ActivityDate",Convert.ToDateTime(FcDate).ToString("yyyy-MM-dd")),
                  new SqlParameter("@UserEntry",userid),
      
                 };

        DataTable dtUserVillage = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[LoadSchoolActivityPreviousData]", parm);
        if (dtUserVillage.Rows.Count > 0)
        {     
            #region Priveous Colours
            if (Convert.ToInt32(dtUserVillage.Rows[0]["DrinkingWater"].ToString()) != 0)
            {

                if (Convert.ToInt32(dtUserVillage.Rows[0]["DrinkingWater"].ToString()) == 4)
                {
                    //txtdrinking.BackColor = Color.Green;
                    txtdrinking1.BackColor = Color.Blue;                
                    txt1.Text = "4";
                }
                if (Convert.ToInt32(dtUserVillage.Rows[0]["DrinkingWater"].ToString()) == 1)
                {
                    txtdrinking1.BackColor = Color.Green;                 
                    txt1.Text = "1";
                }
                if (Convert.ToInt32(dtUserVillage.Rows[0]["DrinkingWater"].ToString()) == 2)
                {
                    txtdrinking1.BackColor = Color.Orange;                 
                    txt1.Text = "2";
                }
                if (Convert.ToInt32(dtUserVillage.Rows[0]["DrinkingWater"].ToString()) == 3)
                {
                    txtdrinking1.BackColor = Color.Red;                  
                    txt1.Text = "3";

                }
            }
            
            if (Convert.ToInt32(dtUserVillage.Rows[0]["GirlsToilet"].ToString()) != 0)
            {
                if (Convert.ToInt32(dtUserVillage.Rows[0]["GirlsToilet"].ToString()) == 4)
                {
                    txtToilet1.BackColor = Color.Blue;               
                    txt2.Text = "4";

                }
                if (Convert.ToInt32(dtUserVillage.Rows[0]["GirlsToilet"].ToString()) == 1)
                {
                    txtToilet1.BackColor = Color.Green;
                    txt2.Text = "1";
                }
                if (Convert.ToInt32(dtUserVillage.Rows[0]["GirlsToilet"].ToString()) == 2)
                {
                    txtToilet1.BackColor = Color.Orange;
                    txt2.Text = "2";
                }
                if (Convert.ToInt32(dtUserVillage.Rows[0]["GirlsToilet"].ToString()) == 3)
                {
                    txtToilet1.BackColor = Color.Red;
                    txt2.Text = "3";
                }
            }

            if (Convert.ToInt32(dtUserVillage.Rows[0]["Electricity"].ToString()) != 0)
            {
                if (Convert.ToInt32(dtUserVillage.Rows[0]["Electricity"].ToString()) == 4)
                {
                    txtElectricity1.BackColor = Color.Blue;
                    txt3.Text = "4";
                }
                if (Convert.ToInt32(dtUserVillage.Rows[0]["Electricity"].ToString()) == 1)
                {
                    txtElectricity1.BackColor = Color.Green;
                    txt3.Text = "1";
                }
                if (Convert.ToInt32(dtUserVillage.Rows[0]["Electricity"].ToString()) == 2)
                {
                    txtElectricity1.BackColor = Color.Orange;
                    txt3.Text = "2";
                }
                if (Convert.ToInt32(dtUserVillage.Rows[0]["Electricity"].ToString()) == 3)
                {
                    txtElectricity1.BackColor = Color.Red;
                    txt3.Text = "3";
                }
            }


            if (Convert.ToInt32(dtUserVillage.Rows[0]["Playground"].ToString()) != 0)
            {
                if (Convert.ToInt32(dtUserVillage.Rows[0]["Playground"].ToString()) == 4)
                {
                    txtPlay1.BackColor = Color.Blue;
                    txt4.Text = "4";
                }
                if (Convert.ToInt32(dtUserVillage.Rows[0]["Playground"].ToString()) == 1)
                {
                    txtPlay1.BackColor = Color.Green;
                    txt4.Text = "1";
                }
                if (Convert.ToInt32(dtUserVillage.Rows[0]["Playground"].ToString()) == 2)
                {
                    txtPlay1.BackColor = Color.Orange;
                    txt4.Text = "2";
                }
                if (Convert.ToInt32(dtUserVillage.Rows[0]["Playground"].ToString()) == 3)
                {
                    txtPlay1.BackColor = Color.Red;
                    txt4.Text = "3";
                }
            }


            if (Convert.ToInt32(dtUserVillage.Rows[0]["Slide"].ToString()) != 0)
            {
                if (Convert.ToInt32(dtUserVillage.Rows[0]["Slide"].ToString()) == 4)
                {
                    txtSlides1.BackColor = Color.Blue;
                    txt5.Text = "4";
                }
                if (Convert.ToInt32(dtUserVillage.Rows[0]["Slide"].ToString()) == 1)
                {
                    txtSlides1.BackColor = Color.Green;
                    txt5.Text = "1";
                }
                if (Convert.ToInt32(dtUserVillage.Rows[0]["Slide"].ToString()) == 2)
                {
                    txtSlides1.BackColor = Color.Orange;
                    txt5.Text = "2";
                }
                if (Convert.ToInt32(dtUserVillage.Rows[0]["Slide"].ToString()) == 3)
                {
                    txtSlides1.BackColor = Color.Red;

                    txt5.Text = "3";
                }
            }

            if (Convert.ToInt32(dtUserVillage.Rows[0]["BoundaryWall"].ToString()) != 0)
            {
                if (Convert.ToInt32(dtUserVillage.Rows[0]["BoundaryWall"].ToString()) == 4)
                {
                    txtBoundaryWall1.BackColor = Color.Blue;
                    txt6.Text = "4";

                }
                if (Convert.ToInt32(dtUserVillage.Rows[0]["BoundaryWall"].ToString()) == 1)
                {
                    txtBoundaryWall1.BackColor = Color.Green;
                    txt6.Text = "1";
                }
                if (Convert.ToInt32(dtUserVillage.Rows[0]["BoundaryWall"].ToString()) == 2)
                {
                    txtBoundaryWall1.BackColor = Color.Orange;
                    txt6.Text = "2";
                }
                if (Convert.ToInt32(dtUserVillage.Rows[0]["BoundaryWall"].ToString()) == 3)
                {
                    txtBoundaryWall1.BackColor = Color.Red;
                    txt6.Text = "3";
                }
            }



            if (Convert.ToInt32(dtUserVillage.Rows[0]["Kitchen"].ToString()) != 0)
            {
                if (Convert.ToInt32(dtUserVillage.Rows[0]["Kitchen"].ToString()) == 4)
                {
                    txtKitchen1.BackColor = Color.Blue;
                    txt7.Text = "4";
                }
                if (Convert.ToInt32(dtUserVillage.Rows[0]["Kitchen"].ToString()) == 1)
                {
                    txtKitchen1.BackColor = Color.Green;
                    txt7.Text = "1";
                }
                if (Convert.ToInt32(dtUserVillage.Rows[0]["Kitchen"].ToString()) == 2)
                {
                    txtKitchen1.BackColor = Color.Orange;
                    txt7.Text = "2";
                }
                if (Convert.ToInt32(dtUserVillage.Rows[0]["Kitchen"].ToString()) == 3)
                {
                    txtKitchen1.BackColor = Color.Red;

                    txt7.Text = "3";
                }
            }

            if (Convert.ToInt32(dtUserVillage.Rows[0]["CLT_Kit"].ToString()) != 0)
            {
                if (Convert.ToInt32(dtUserVillage.Rows[0]["CLT_Kit"].ToString()) == 4)
                {
                    txtCltKit1.BackColor = Color.Blue;
                    txt8.Text = "4";
                }
                if (Convert.ToInt32(dtUserVillage.Rows[0]["CLT_Kit"].ToString()) == 1)
                {
                    txtCltKit1.BackColor = Color.Green;
                    txt8.Text = "1";
                }
                if (Convert.ToInt32(dtUserVillage.Rows[0]["CLT_Kit"].ToString()) == 2)
                {
                    txtCltKit1.BackColor = Color.Orange;
                    txt8.Text = "2";
                }
                if (Convert.ToInt32(dtUserVillage.Rows[0]["CLT_Kit"].ToString()) == 3)
                {
                    txtCltKit1.BackColor = Color.Red;
                    txt8.Text = "3";
                }
            }

            if (Convert.ToInt32(dtUserVillage.Rows[0]["Books"].ToString()) != 0)
            {
                if (Convert.ToInt32(dtUserVillage.Rows[0]["Books"].ToString()) == 4)
                {
                    txtbook1.BackColor = Color.Blue;
                    txt9.Text = "4";
                }
                if (Convert.ToInt32(dtUserVillage.Rows[0]["Books"].ToString()) == 1)
                {
                    txtbook1.BackColor = Color.Green;
                    txt9.Text = "1";
                }
                if (Convert.ToInt32(dtUserVillage.Rows[0]["Books"].ToString()) == 2)
                {
                    txtbook1.BackColor = Color.Orange;
                    txt9.Text = "2";
                }
                if (Convert.ToInt32(dtUserVillage.Rows[0]["Books"].ToString()) == 3)
                {
                    txtbook1.BackColor = Color.Red;
                    txt9.Text = "3";
                }
            }
            #endregion
        }
        SqlParameter[] parm1 = new SqlParameter[]
             {
              
               new SqlParameter("@SchoolCode",  ddlSchool.SelectedValue),
               
                  new SqlParameter("@ActivityDate",b[2] ),
               new SqlParameter("@ActivityDateNew",Convert.ToDateTime(FcDate).ToString("yyyy-MM-dd")),
      
                 };
        DataTable dtActivtyPreTest = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[LoadSchoolActivityPreTestCheck]", parm1);
        if (dtActivtyPreTest.Rows.Count > 0)
        {

            rblCompletePost.Enabled=true;
            rblCompleteMid.Enabled=true;
            rblPartialPost.Enabled=true;
            rblPartialMid.Enabled=true;
           rblTestPostFC.Enabled=true;
           rblTestMidFC.Enabled=true;
           rblTestTBPost.Enabled=true;
           rblTestTBMid.Enabled=true;
           rblCompletePre.Enabled = false;
           rblPartialPre.Enabled = false;
            rblTestpreFC.Enabled = false;
            rblTestTBPre.Enabled = false;
        }
        else
        {



            rblCompletePre.Enabled=true;
            rblPartialPre.Enabled=true;
            rblTestpreFC.Enabled=true;
           rblTestTBPre.Enabled = true;
        }

        SqlParameter[] parm3 = new SqlParameter[]
             {
              
               new SqlParameter("@SchoolCode",  ddlSchool.SelectedValue),
               
                  new SqlParameter("@ActivityDate",b[2] ),
               new SqlParameter("@ActivityDateNew",Convert.ToDateTime(FcDate).ToString("yyyy-MM-dd")),
      
                 };
        DataTable dtActivtyPreTest1 = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[LoadSchoolActivityPreTestCheckMid]", parm3);
        if (dtActivtyPreTest1.Rows.Count > 0)
        {
            rblTestTBPre.Enabled = false;
            rblTestTBMid.Enabled = false;

            rblTestpreFC.Enabled = false;
            rblTestMidFC.Enabled = false;
            rblPartialPre.Enabled = false;
            rblPartialMid.Enabled = false;
            rblCompletePre.Enabled = false;
            rblCompleteMid.Enabled = false;

            rblCompletePost.Enabled = true;
            rblPartialPost.Enabled = true;
            rblTestPostFC.Enabled = true;
            rblTestTBPost.Enabled = true;
   
        }
        string query = "   select isnull(SchoolLevel,0) as SchoolLevel  from mstSchool   where SchoolCode='" + this.ddlSchool.SelectedValue + "' ";
        DataTable dataTable2 = this.objMain.LoadData(query);
        if (dataTable2.Rows[0]["SchoolLevel"].ToString() == "3" || dataTable2.Rows[0]["SchoolLevel"].ToString() == "4")
        {
            this.pnlSmc.Enabled = false;
            this.pnlClt.Enabled = false;
            this.pnlBalshaba.Enabled = false;
            this.pnlSACUpdate.Enabled = false;
            this.pnlinfrastructure.Enabled = false;
            this.pnlAnnual.Enabled = false;
        }
        else if (dataTable2.Rows[0]["SchoolLevel"].ToString() == "1")
        {
            this.pnlSmc.Enabled = true;
            this.pnlClt.Enabled = true;
            this.pnlBalshaba.Enabled = false;
            this.pnlSACUpdate.Enabled = true;
            this.pnlinfrastructure.Enabled = true;
            this.pnlAnnual.Enabled = true;
        }
        else if (dataTable2.Rows[0]["SchoolLevel"].ToString() == "2")
        {
            this.pnlSmc.Enabled = true;
            this.pnlClt.Enabled = true;
            this.pnlBalshaba.Enabled = true;
            this.pnlSACUpdate.Enabled = true;
            this.pnlinfrastructure.Enabled = true;
            this.pnlAnnual.Enabled = true;
        }
       
        int month = 0;

        if (txtDate.Text != "")
        {
            month = Convert.ToInt32(b[1]);
        }
        if (month == 3 || month == 7 || month == 10 || month == 1)
        {
            SqlParameter[] parm4 = new SqlParameter[]
             {
              
               new SqlParameter("@SchoolCode",  ddlSchool.SelectedValue),
               
                  new SqlParameter("@ActivityDate",b[1]),
       
      
                 };
            DataTable dtSACUpdate = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rptCheckSACUpdate]", parm4);
            if (dtSACUpdate.Rows.Count > 0 )
            {
                if (ViewState["GUID_School"].ToString() == "")
                {
                    this.pnlSACUpdate.Enabled = false;
                }
                else
                {
                    this.pnlSACUpdate.Enabled = true;
                }
            }
            else
            {
                this.pnlSACUpdate.Enabled = true;
            }


        }
        else
        {
            this.pnlSACUpdate.Enabled = false;
        }
       

    }
    public void LoadDataschool()
    {
        string Dateof = txtDate.Text;
        string[] b = Dateof.Split('/');

        string FcDate = b[2] + '-' + b[1] + '-' + b[0];

              string strQry="";
              string conq = "";

             string userid = "";
            if (Session["user_level"].ToString() == "19")
             {
                  userid = "2";
                  conq = "ActivityDate =('" + FcDate + "')    and Schoolcode='" + ddlSchool.SelectedValue + "' and ApproveStatus='FC' ";

            }
            if (Session["user_level"].ToString() == "39" || Session["user_level"].ToString() == "30")
            {
                userid = "3";
                conq = "ActivityDate =('" + FcDate + "')    and Schoolcode='" + ddlSchool.SelectedValue + "' and ApproveStatus='B' ";

            }
                 SqlParameter[] parm = new SqlParameter[]
             {
               new SqlParameter("@villagecode",  ddlVilage.SelectedValue),
               new SqlParameter("@SchoolCode",  ddlSchool.SelectedValue),
                new SqlParameter("@User",ddlUser.SelectedValue),
                  new SqlParameter("@ActivityDate",Convert.ToDateTime(FcDate).ToString("yyyy-MM-dd")),
                  new SqlParameter("@UserEntry",userid),
      
                 };

                 DataTable dtUserVillage = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[LoadActivityUpdateDataNew]", parm);
       

     //   strQry = "   select * from tblActivityUpdate_School where VillageCode='" + ddlVilage.SelectedValue + "' and SchoolCode='" + ddlSchool.SelectedValue + "' and ActivityDate= '" + Convert.ToDateTime(FcDate).ToString("yyyy-MM-dd") + "'  ";
       // DataTable dtUserVillage = objMain.LoadData(strQry);
               
         

        DataTable dtGKP = objMain.LoadGKPDeatils(conq);
        if (dtGKP.Rows.Count > 0)
        {
            gvGkp.DataSource = dtGKP;
            gvGkp.DataBind();
        }
        else
        {
            gvGkp.DataSource = dtGKP;
            gvGkp.DataBind();
        }
        if (dtUserVillage.Rows.Count > 0)
        {
      
            if (dtUserVillage.Rows[0]["ApproveStatus"].ToString() == "B" || dtUserVillage.Rows[0]["ApproveStatus"].ToString() == "FC" || dtUserVillage.Rows[0]["ApproveStatus"].ToString() == "I")
            {
      
                if (Session["user_level"].ToString() == "19" && dtUserVillage.Rows[0]["ApproveStatus"].ToString() == "FC")
                {
                    btnsave.Visible = true;
                }
                else
                {
                    btnsave.Visible = false;
                }
                if (Session["user_level"].ToString() == "39" || Session["user_level"].ToString() == "30")
                {
                    if (dtUserVillage.Rows[0]["ApproveStatus"].ToString() == "B")
                    {
                        btnsave.Visible = true;
                    }
                    else
                    {
                        btnsave.Visible = false;
                    }
                }
            }
            if (Convert.ToInt32(dtUserVillage.Rows[0]["SMC_Meeting_FC"].ToString()) == 1)
            {

                rblSMCFC.Checked = true;
            }
            idImage.Visible = false;
            lblMM.Text = "";
            ViewState["GUID_School"] = dtUserVillage.Rows[0]["GUID_School"].ToString();
            idImage.Visible = false;
            lblMM.Text = "";
            if (dtUserVillage.Rows[0]["Photo"].ToString().Length > 3)
            {
                idImage.Visible = true;
                lblMM.Text = dtUserVillage.Rows[0]["Photo"].ToString();
            }
           


            #region "SMC"
            if (Convert.ToInt32(dtUserVillage.Rows[0]["SMC_Meeting"].ToString()) == 1)
            {
                chkSMC.Checked = true;
            }
            if (Convert.ToInt32(dtUserVillage.Rows[0]["SMC_Meeting_TB"].ToString()) == 1)
            {
              
                rblSMCTB.Checked = true;
            }
            if (Convert.ToInt32(dtUserVillage.Rows[0]["SMC_Meeting_FC"].ToString()) == 1)
            {
            
                rblSMCFC.Checked = true;
            }

            string cmeeting = dtUserVillage.Rows[0]["SMC_OtherDiscussions"].ToString();

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

            }
            if (Convert.ToInt32(dtUserVillage.Rows[0]["SMC_OtherSIP"].ToString()) != 0)
            {
                txtOtherSIPFC.Text = dtUserVillage.Rows[0]["SMC_OtherSIP"].ToString();
            }

            if (Convert.ToInt32(dtUserVillage.Rows[0]["SMC_Mtg"].ToString()) != 0)
            {
                txtsmcmeetinFC.Text = dtUserVillage.Rows[0]["SMC_Mtg"].ToString();
            }

            #endregion

            #region SmcOrient
            txtOther.Text=dtUserVillage.Rows[0]["Others_Description"].ToString();

            if (Convert.ToInt32(dtUserVillage.Rows[0]["SMC"].ToString()) == 1)
            {
                chkNewSmc.Checked = true;
            }
            if (Convert.ToInt32(dtUserVillage.Rows[0]["SMC_TB"].ToString()) == 1)
            {

                rblSmcNew.Checked = true;
            }
            if (Convert.ToInt32(dtUserVillage.Rows[0]["SMC_FC"].ToString()) == 1)
            {

                rblSmcNew1.Checked = true;
            }

            if (Convert.ToInt32(dtUserVillage.Rows[0]["SMC_TotTrained"].ToString()) != 0)
            {
                txtTotalMember.Text = dtUserVillage.Rows[0]["SMC_TotTrained"].ToString();

            }

            if (Convert.ToInt32(dtUserVillage.Rows[0]["SMC_FemaleTrained"].ToString()) != 0)
            {
                txtTotalFmember.Text = dtUserVillage.Rows[0]["SMC_FemaleTrained"].ToString();
            }
            #endregion 



          
           
          
            
         
            if (Convert.ToInt32(dtUserVillage.Rows[0]["CLT"].ToString()) == 1)
            {
                chkClT.Checked = true;
            }
            if (Convert.ToInt32(dtUserVillage.Rows[0]["CLT_TB"].ToString()) == 1)
            {
               
                rblCLTTB.Checked = true;
            }
            if (Convert.ToInt32(dtUserVillage.Rows[0]["CLT_FC"].ToString()) == 1)
            {
               
                rblCLTFC.Checked = true;
            }
            #region Subject

            string CltHindi = dtUserVillage.Rows[0]["CLTHindi"].ToString();
            string CLTMath = dtUserVillage.Rows[0]["CLTMath"].ToString();
            string CLTEnglish = dtUserVillage.Rows[0]["CLTEnglish"].ToString();
            string[] parts = CltHindi.Split(',');
            string[] parts1 = CLTMath.Split(',');
             string[] parts3 = CLTEnglish.Split(',');
            foreach (string part in parts)
            {

                    if (part == "A")
                    {
                        chkHindiA.Checked = true;
                    }
                    if (part == "B")
                    {
                        chkHindiB.Checked = true;
                    }
                    if (part == "C")
                    {
                        chkHindiC.Checked = true;
                    }
                    if (part == "D")
                    {
                        chkHindiD.Checked = true;
                    }
                    if (part == "E")
                    {
                        chkHindiE.Checked = true;
                    }
            }
             foreach (string part1 in parts1)
              {
                    if (part1 == "A")
                    {
                        chkEnglishA.Checked = true;
                    }
                    if (part1 == "B")
                    {
                        chkEnglishB.Checked = true;
                    }
                    if (part1 == "C")
                    {
                        chkEnglishC.Checked = true;
                    }
                    if (part1 == "D")
                    {
                        chkEnglishD.Checked = true;
                    }
                    if (part1 == "E")
                    {
                        chkEnglishE.Checked = true;
                    }
                }
             foreach (string part3 in parts3)
              {
                    if (part3== "A")
                    {
                        chkMathA.Checked = true;
                    }
                    if (part3== "B")
                    {
                        chkMathB.Checked = true;
                    }
                    if (part3 == "C")
                    {
                        chkMathC.Checked = true;
                    }
                    if (part3 == "D")
                    {
                        chkMathD.Checked = true;
                    }
                    if (part3== "E")
                    {
                        chkMathE.Checked = true;
                    }
                }
            
            #endregion

            //strQry = "   select * from tblActivityUpdate_LifeskillGames where GUID_School='" + dtUserVillage.Rows[0]["GUID_School"].ToString() + "'  ";
            //DataTable dtGame = objMain.LoadData(strQry);
            //if (dtGame.Rows.Count > 0)
            //{
                if (Convert.ToInt32(dtUserVillage.Rows[0]["Lifeskill_Games"].ToString()) == 1)
                {
                  
                    chklife.Checked = true;
                }
                if (Convert.ToInt32(dtUserVillage.Rows[0]["Lifeskill_Games_TB"].ToString()) == 1)
                {

                    rblLifeTB.Checked = true;
                }
                if (Convert.ToInt32(dtUserVillage.Rows[0]["Lifeskill_Games_FC"].ToString()) == 1)
                {

                    rblLifeFC.Checked = true;
                }
                #region Game

                string LifeSkillGameEntry = dtUserVillage.Rows[0]["LifeSkillGameEntry"].ToString();

                string[] Skill = LifeSkillGameEntry.Split(',');
                foreach (string Skill1 in Skill)
              
                {
                    if (Skill1 == "1")
                    {
                        chkGame1.Checked = true;
                    }
                    if (Skill1 == "2")
                    {
                        chkGame2.Checked = true;
                    }
                    if (Skill1 == "3")
                    {
                        chkGame3.Checked = true;
                    }
                    if (Skill1 == "4")
                    {
                        chkGame4.Checked = true;
                    }
                    if (Skill1 == "5")
                    {
                        chkGame5.Checked = true;
                    }
                }
                #endregion
            //}
            #region Balsabha
            if (Convert.ToInt32(dtUserVillage.Rows[0]["SIP_Annual"].ToString()) == 1)
            {
              //  chkPhysical.Checked = true;
            }
           
            if (Convert.ToInt32(dtUserVillage.Rows[0]["BalSabha"].ToString()) == 1)
            {
                chkBalsabha.Checked = true;
            }
            if (Convert.ToInt32(dtUserVillage.Rows[0]["BalSabha_TB"].ToString()) == 1)
            {
              
                rblBalsabaTB.Checked = true;
            }
            if (Convert.ToInt32(dtUserVillage.Rows[0]["BalSabha_TB"].ToString()) ==1)
            {
              
                rblBalsabaFC.Checked = true;
            }
            if (Convert.ToInt32(dtUserVillage.Rows[0]["BalSabha_Formation"].ToString()) != 0)
            {
                chkBalSabhaFor.Checked = true;
            }
            if (Convert.ToInt32(dtUserVillage.Rows[0]["BalSabha_Orientation"].ToString()) != 0)
            {
                chkOrientation.Checked = true;
            }
            if (Convert.ToInt32(dtUserVillage.Rows[0]["BalSabha_Chart"].ToString()) != 0)
            {
                chkChat.Checked = true;
            }
            if (Convert.ToInt32(dtUserVillage.Rows[0]["BalSabha_Kit"].ToString()) != 0)
            {
                chkKit.Checked = true;
            }
            #endregion


            #region CLTTest
            if (Convert.ToInt32(dtUserVillage.Rows[0]["CLT_Pretest_TB"].ToString()) == 1)
                {
                    rblTestTBPre.Checked = true;
                }
            if (Convert.ToInt32(dtUserVillage.Rows[0]["CLT_Pretest_FC"].ToString()) == 1)
                {
                    rblTestpreFC.Checked = true;
                }
                if (dtUserVillage.Rows[0]["Clt_Pre_PC"].ToString() =="P")
                {
                    rblPartialPre.Checked = true;
                }
                if (dtUserVillage.Rows[0]["Clt_Pre_PC"].ToString() == "C")
                {
                    rblCompletePre.Checked = true;
                }
           
           
                if (Convert.ToInt32(dtUserVillage.Rows[0]["CTL_Midtest_TB"].ToString()) == 1)
                {
                    rblTestTBMid.Checked = true;
                }
                if (Convert.ToInt32(dtUserVillage.Rows[0]["CTL_Midtest_FC"].ToString()) == 1)
                {
                    rblTestMidFC.Checked = true;
                }
                if (dtUserVillage.Rows[0]["Clt_Mid_PC"].ToString() == "P")
                {
                    rblPartialMid.Checked = true;
                }
                if (dtUserVillage.Rows[0]["Clt_Mid_PC"].ToString() == "C")
                {
                    rblCompleteMid.Checked = true;
                }
           
           
                if (Convert.ToInt32(dtUserVillage.Rows[0]["CLT_Posttest_TB"].ToString()) == 1)
                {
                    rblTestTBPost.Checked = true;
                }
                if (Convert.ToInt32(dtUserVillage.Rows[0]["CLT_Posttest_FC"].ToString()) == 1)
                {
                    rblTestPostFC.Checked = true;
                }
                if (dtUserVillage.Rows[0]["Clt_Post_PC"].ToString()== "P")
                {
                    rblPartialPost.Checked = true;
                }
                if (dtUserVillage.Rows[0]["Clt_Post_PC"].ToString()== "C")
                {
                    rblCompletePost.Checked = true;
                }
            
            #endregion

            #region SAC
           
            if (Convert.ToInt32(dtUserVillage.Rows[0]["SAC_Periodic_Checkup"].ToString()) != 0)
            {
                txtHealth.Text = dtUserVillage.Rows[0]["SAC_Periodic_Checkup"].ToString();

            }
            if (Convert.ToInt32(dtUserVillage.Rows[0]["SAC_No_Of_Attended"].ToString()) != 0)
            {
                txtSMCMeeting.Text = dtUserVillage.Rows[0]["SAC_No_Of_Attended"].ToString();

            }
            if (Convert.ToInt32(dtUserVillage.Rows[0]["SAC_Listing_Name_Of_Girls"].ToString()) != 0)
            {
                txtAdgirls.Text = dtUserVillage.Rows[0]["SAC_Listing_Name_Of_Girls"].ToString();
            }
            if (Convert.ToInt32(dtUserVillage.Rows[0]["SAC_Listing_Name_Of_Boys"].ToString()) != 0)
            {
                txtAdBoy.Text = dtUserVillage.Rows[0]["SAC_Listing_Name_Of_Boys"].ToString();
            }
            if (Convert.ToInt32(dtUserVillage.Rows[0]["SAC_Girls_Left"].ToString()) != 0)
            {
                txtleftGirl.Text = dtUserVillage.Rows[0]["SAC_Girls_Left"].ToString();
            }
            if (Convert.ToInt32(dtUserVillage.Rows[0]["SAC_Boys_Left"].ToString()) != 0)
            {
                txtleftBoy.Text = dtUserVillage.Rows[0]["SAC_Boys_Left"].ToString();
            }
            if (Convert.ToInt32(dtUserVillage.Rows[0]["SAC_Girls_Not_Joined_School"].ToString()) != 0)
            {
                txtGirlNot.Text = dtUserVillage.Rows[0]["SAC_Girls_Not_Joined_School"].ToString();
            }
            if (Convert.ToInt32(dtUserVillage.Rows[0]["SAC_Girls_Not_Joined_School"].ToString()) != 0)
            {
                txtBoyNot.Text = dtUserVillage.Rows[0]["SAC_Boys_Not_Joined_School"].ToString();
            }
            if (Convert.ToInt32(dtUserVillage.Rows[0]["SACUpdate"].ToString()) == 1)
            {
                chkSACUpdate.Checked=true;
            }
            if (Convert.ToInt32(dtUserVillage.Rows[0]["SACUpdate_TB"].ToString()) == 1)
            {
                rblSacTB.Checked = true;
            }
            if (Convert.ToInt32(dtUserVillage.Rows[0]["SACUpdate_FC"].ToString()) == 1)
            {
                rblSacFB.Checked = true;
            }
            //divSafe.Style(
          //  divSafe.Attributes.Add.Style("background-color: #090;");
           // divSafe.Attributes.Add('style','color:green');






            if (Convert.ToInt32(dtUserVillage.Rows[0]["Infrastructure"].ToString()) == 1)
            {
                chkPhysical.Checked = true;
            }
            if (Convert.ToInt32(dtUserVillage.Rows[0]["Infrastructure_TB"].ToString()) == 1)
            {
                rblPhysicalTB.Checked = true;
            }
            if (Convert.ToInt32(dtUserVillage.Rows[0]["Infrastructure_FC"].ToString()) == 1)
            {
                rblPhysicalFC.Checked = true;
            }




            if (Convert.ToInt32(dtUserVillage.Rows[0]["Classrooms"].ToString()) != 0)
            {
                txtClassRoom.Text = dtUserVillage.Rows[0]["Classrooms"].ToString();
            }
            if (Convert.ToInt32(dtUserVillage.Rows[0]["DrinkingWater"].ToString()) != 0)
            {

                if (Convert.ToInt32(dtUserVillage.Rows[0]["DrinkingWater"].ToString()) == 4)
                {
                    //txtdrinking.BackColor = Color.Green;
                    txtdrinking.BackColor = Color.Blue;
                    
                    lbldriking.Text = "4";

                    //  txt1.Text = "4";
                }
                if (Convert.ToInt32(dtUserVillage.Rows[0]["DrinkingWater"].ToString()) == 1)
                {
                    txtdrinking.BackColor = Color.Green;
                    lbldriking.Text = "1";
                    //  txt1.Text = "1";
                }
                if (Convert.ToInt32(dtUserVillage.Rows[0]["DrinkingWater"].ToString()) == 2)
                {
                    txtdrinking.BackColor = Color.Orange;
                    lbldriking.Text = "2";
                    // txt1.Text = "2";
                }
                if (Convert.ToInt32(dtUserVillage.Rows[0]["DrinkingWater"].ToString()) == 3)
                {
                    txtdrinking.BackColor = Color.Red;
                    lbldriking.Text = "3";
                  //  txt1.Text = "3";
                  
                }
            }
            
        
            if (Convert.ToInt32(dtUserVillage.Rows[0]["GirlsToilet"].ToString()) != 0)
            {
                if (Convert.ToInt32(dtUserVillage.Rows[0]["GirlsToilet"].ToString()) == 4)
                {
                    txtToilet.BackColor = Color.Blue;
                    //txtToilet.BackColor = Color.Green;
                    lblToilet.Text = "4";

                 //   txt2.Text = "4";
                   
                }
                if (Convert.ToInt32(dtUserVillage.Rows[0]["GirlsToilet"].ToString()) == 1)
                {
                    txtToilet.BackColor = Color.Green;
                    lblToilet.Text = "1";
                   // txt2.Text = "1";
                }
                if (Convert.ToInt32(dtUserVillage.Rows[0]["GirlsToilet"].ToString()) == 2)
                {
                    txtToilet.BackColor = Color.Orange;
                    lblToilet.Text = "2";
                   // txt2.Text = "2";
                }
                if (Convert.ToInt32(dtUserVillage.Rows[0]["GirlsToilet"].ToString()) ==3)
                {
                    txtToilet.BackColor = Color.Red;
                    lblToilet.Text = "3";
                 //   txt2.Text = "3";
                }
            }
                 
            if (Convert.ToInt32(dtUserVillage.Rows[0]["Electricity"].ToString()) != 0)
            {
                if (Convert.ToInt32(dtUserVillage.Rows[0]["Electricity"].ToString()) ==4)
                {
                    txtElectricity.BackColor = Color.Blue;
                    lblElectricity.Text = "4";
                    //  txt3.Text = "4";
                }
                if (Convert.ToInt32(dtUserVillage.Rows[0]["Electricity"].ToString()) == 1)
                {
                    txtElectricity.BackColor = Color.Green;
                    lblElectricity.Text = "1";
                    //  txt3.Text = "1";
                }
                if (Convert.ToInt32(dtUserVillage.Rows[0]["Electricity"].ToString()) == 2)
                {
                    txtElectricity.BackColor = Color.Orange;
                    lblElectricity.Text = "2";

                    //  txt3.Text = "2";
                }
                if (Convert.ToInt32(dtUserVillage.Rows[0]["Electricity"].ToString()) == 3)
                {
                    txtElectricity.BackColor = Color.Red;
                    lblElectricity.Text = "3";
                    //  txt3.Text = "3";
                }
            }

                
            if (Convert.ToInt32(dtUserVillage.Rows[0]["Playground"].ToString()) != 0)
            {
                if (Convert.ToInt32(dtUserVillage.Rows[0]["Playground"].ToString()) == 4)
                {
                    txtPlay.BackColor = Color.Blue;
                    lblPlay.Text = "4";
                    //   txt4.Text = "4";
                }
                if (Convert.ToInt32(dtUserVillage.Rows[0]["Playground"].ToString()) == 1)
                {
                    txtPlay.BackColor = Color.Green;
                    lblPlay.Text = "1";
                    //   txt4.Text = "1";
                }
                if (Convert.ToInt32(dtUserVillage.Rows[0]["Playground"].ToString()) == 2)
                {
                    txtPlay.BackColor = Color.Orange;
                    lblPlay.Text = "2";
                    //  txt4.Text = "2";
                }
                if (Convert.ToInt32(dtUserVillage.Rows[0]["Playground"].ToString()) == 3)
                {
                    txtPlay.BackColor = Color.Red;
                    lblPlay.Text = "3";
                    //  txt4.Text = "3";
                }
            }

        
            if (Convert.ToInt32(dtUserVillage.Rows[0]["Slide"].ToString()) != 0)
            {
                if (Convert.ToInt32(dtUserVillage.Rows[0]["Slide"].ToString()) == 4)
                {
                    txtSlides.BackColor = Color.Blue;
                    lblSlides.Text = "4";
                    //   txt5.Text = "4";
                }
                if (Convert.ToInt32(dtUserVillage.Rows[0]["Slide"].ToString()) == 1)
                {
                    txtSlides.BackColor = Color.Green;
                    lblSlides.Text = "1";
                    //   txt5.Text = "1";
                }
                if (Convert.ToInt32(dtUserVillage.Rows[0]["Slide"].ToString()) ==2)
                {
                    txtSlides.BackColor = Color.Orange;
                    lblSlides.Text = "2";
                    //  txt5.Text = "2";
                }
                if (Convert.ToInt32(dtUserVillage.Rows[0]["Slide"].ToString()) == 3)
                {
                    txtSlides.BackColor = Color.Red;
                    lblSlides.Text = "3";
                    //  txt5.Text = "3";
                }
            }

            if (Convert.ToInt32(dtUserVillage.Rows[0]["BoundaryWall"].ToString()) != 0)
            {
                if (Convert.ToInt32(dtUserVillage.Rows[0]["BoundaryWall"].ToString()) == 4)
                {
                    txtBoundaryWall.BackColor = Color.Blue;
                    lblBoundaryWall.Text = "4";
                    //  txt6.Text = "4";
                 
                }
                if (Convert.ToInt32(dtUserVillage.Rows[0]["BoundaryWall"].ToString()) == 1)
                {
                    txtBoundaryWall.BackColor = Color.Green;
                    lblBoundaryWall.Text = "1";
                    //    txt6.Text = "1";
                }
                if (Convert.ToInt32(dtUserVillage.Rows[0]["BoundaryWall"].ToString()) == 2)
                {
                    txtBoundaryWall.BackColor = Color.Orange;
                    lblBoundaryWall.Text = "2";
                    //  txt6.Text = "2";
                }
                if (Convert.ToInt32(dtUserVillage.Rows[0]["BoundaryWall"].ToString()) == 3)
                {
                    txtBoundaryWall.BackColor = Color.Red;
                    lblBoundaryWall.Text = "3";
                    //  txt6.Text = "3";
                }
            }

            
        
            if (Convert.ToInt32(dtUserVillage.Rows[0]["Kitchen"].ToString()) != 0)
            {
                if (Convert.ToInt32(dtUserVillage.Rows[0]["Kitchen"].ToString()) == 4)
                {
                    txtKitchen.BackColor = Color.Blue;
                 
                    lblKitchen.Text = "4";
                    //   txt7.Text = "4";
                }
                if (Convert.ToInt32(dtUserVillage.Rows[0]["Kitchen"].ToString()) == 1)
                {
                    txtKitchen.BackColor = Color.Green;
                    lblKitchen.Text = "1";
                    //   txt7.Text = "1";
                }
                if (Convert.ToInt32(dtUserVillage.Rows[0]["Kitchen"].ToString()) == 2)
                {
                    txtKitchen.BackColor = Color.Orange;
                    lblKitchen.Text = "2";
                    // txt7.Text = "2";
                }
                if (Convert.ToInt32(dtUserVillage.Rows[0]["Kitchen"].ToString()) == 3)
                {
                    txtKitchen.BackColor = Color.Red;
                    lblKitchen.Text = "3";
                    //   txt7.Text = "3";
                }
            }
           
            if (Convert.ToInt32(dtUserVillage.Rows[0]["CLT_Kit"].ToString()) != 0)
            {
                if (Convert.ToInt32(dtUserVillage.Rows[0]["CLT_Kit"].ToString()) == 4)
                {
                    txtCltKit.BackColor = Color.Blue;
                    
                    lblCltKit.Text = "4";
                    //  txt8.Text = "4";
                }
                if (Convert.ToInt32(dtUserVillage.Rows[0]["CLT_Kit"].ToString()) == 1)
                {
                    txtCltKit.BackColor = Color.Green;
                    lblCltKit.Text = "1";
                    //  txt8.Text = "1";
                }
                if (Convert.ToInt32(dtUserVillage.Rows[0]["CLT_Kit"].ToString()) ==2)
                {
                    txtCltKit.BackColor = Color.Orange;
                    lblCltKit.Text = "2";
                    //  txt8.Text = "2";
                }
                if (Convert.ToInt32(dtUserVillage.Rows[0]["CLT_Kit"].ToString()) == 3)
                {
                    txtCltKit.BackColor = Color.Red;
                    lblCltKit.Text = "3";
                    //  txt8.Text = "3";
                }
            }
                 
            if (Convert.ToInt32(dtUserVillage.Rows[0]["Books"].ToString()) != 0)
            {
                if (Convert.ToInt32(dtUserVillage.Rows[0]["Books"].ToString()) == 4)
                {
                    txtbook.BackColor = Color.Blue;
                  
                    lblbook.Text = "4";
                    //  txt9.Text = "4";
                }
                if (Convert.ToInt32(dtUserVillage.Rows[0]["Books"].ToString()) == 1)
                {
                    txtbook.BackColor = Color.Green;
                    lblbook.Text = "1";
                    // txt9.Text = "1";
                }
                if (Convert.ToInt32(dtUserVillage.Rows[0]["Books"].ToString()) == 2)
                {
                    txtbook.BackColor = Color.Orange;
                    lblbook.Text = "2";
                    //  txt9.Text = "2";
                }
                if (Convert.ToInt32(dtUserVillage.Rows[0]["Books"].ToString()) == 3)
                {
                    txtbook.BackColor = Color.Red;
                    lblbook.Text = "3";
                    // txt9.Text = "3";
                }
            }
            if (Convert.ToInt32(dtUserVillage.Rows[0]["Teachers_Male"].ToString()) != 0)
            {
                txtMaleTeacher.Text = dtUserVillage.Rows[0]["Teachers_Male"].ToString();
            }
            if (Convert.ToInt32(dtUserVillage.Rows[0]["Teachers_Female"].ToString()) != 0)
            {
                txtFemaleTeacher.Text = dtUserVillage.Rows[0]["Teachers_Female"].ToString(); 
            }
            #endregion


            #region SAC Update
            if (Convert.ToInt32(dtUserVillage.Rows[0]["SACUpdate"].ToString()) == 1)
            {
                chkSACUpdate.Checked = true;
            }
            if (Convert.ToInt32(dtUserVillage.Rows[0]["SACUpdate_TB"].ToString()) == 1)
            {
                rblSacTB.Checked = true;
            }
            if (Convert.ToInt32(dtUserVillage.Rows[0]["SACUpdate_FC"].ToString()) == 1)
            {
                rblSacFB.Checked = true;
            }


            if (Convert.ToInt32(dtUserVillage.Rows[0]["SAC_No_Of_Attended"].ToString()) != 0)
            {
                txtSMCMeeting.Text= dtUserVillage.Rows[0]["SAC_No_Of_Attended"].ToString();
            }

            if (Convert.ToInt32(dtUserVillage.Rows[0]["SAC_Periodic_Checkup"].ToString()) != 0)
            {
                txtHealth.Text = dtUserVillage.Rows[0]["SAC_Periodic_Checkup"].ToString();
            }
            if (Convert.ToInt32(dtUserVillage.Rows[0]["SAC_Listing_Name_Of_Girls"].ToString()) != 0)
            {
                txtAdgirls.Text = dtUserVillage.Rows[0]["SAC_Listing_Name_Of_Girls"].ToString();
            }
            if (Convert.ToInt32(dtUserVillage.Rows[0]["SAC_Listing_Name_Of_Boys"].ToString()) != 0)
            {
                txtAdBoy.Text = dtUserVillage.Rows[0]["SAC_Listing_Name_Of_Boys"].ToString();
            }

            if (Convert.ToInt32(dtUserVillage.Rows[0]["SAC_Girls_Left"].ToString()) != 0)
            {
                txtleftGirl.Text = dtUserVillage.Rows[0]["SAC_Girls_Left"].ToString();
            }

            if (Convert.ToInt32(dtUserVillage.Rows[0]["SAC_Boys_Left"].ToString()) != 0)
            {
                txtleftBoy.Text = dtUserVillage.Rows[0]["SAC_Boys_Left"].ToString();
            }

            if (Convert.ToInt32(dtUserVillage.Rows[0]["SAC_Boys_Not_Joined_School"].ToString()) != 0)
            {
                txtGirlNot.Text = dtUserVillage.Rows[0]["SAC_Boys_Not_Joined_School"].ToString();
            }
            if (Convert.ToInt32(dtUserVillage.Rows[0]["SAC_Girls_Not_Joined_School"].ToString()) != 0)
            {
                txtGirlNot.Text = dtUserVillage.Rows[0]["SAC_Girls_Not_Joined_School"].ToString();
            }

          
            #endregion

            #region Anuanl Data
            if (Convert.ToInt32(dtUserVillage.Rows[0]["SIP_Annual"].ToString()) == 1)
            {
                chkAnnual.Checked = true;
            }
            if (Convert.ToInt32(dtUserVillage.Rows[0]["SIP_Annual"].ToString()) == 1)
            {
                chkSIPAnnaul.Checked = true;
            }
            if (Convert.ToInt32(dtUserVillage.Rows[0]["Retention_Annual"].ToString()) == 1)
            {
                chkRetention.Checked = true;
            }
            if (Convert.ToInt32(dtUserVillage.Rows[0]["Retention_Annual_TB"].ToString()) == 1)
            {
                chkRenTB.Checked = true;

            }
            if (Convert.ToInt32(dtUserVillage.Rows[0]["Retention_Annual_FC"].ToString()) == 1)
            {
                chkRenFC.Checked = true;
            }

            if (Convert.ToInt32(dtUserVillage.Rows[0]["SIP_Annual_TB"].ToString()) == 1)
            {
                chkSIPTB.Checked = true;

            }
            if (Convert.ToInt32(dtUserVillage.Rows[0]["SIP_Annual_FC"].ToString()) == 1)
            {
                chkSIPFC.Checked = true;
            }

            if (Convert.ToInt32(dtUserVillage.Rows[0]["Infrastructure_TB"].ToString()) == 1)
            {
                rblPhysicalTB.Checked = true;

            }
            if (Convert.ToInt32(dtUserVillage.Rows[0]["Infrastructure_FC"].ToString()) == 1)
            {
                rblPhysicalFC.Checked = true;

                
            }
            if (dtUserVillage.Rows[0]["SIP_PC"].ToString()== "C")
            {
                chkSipComplete.Checked = true;
            }
            if (dtUserVillage.Rows[0]["SIP_PC"].ToString() == "P")
            {
                chkSipPartial.Checked = true;
            }

            
            //if (Convert.ToInt32(dtUserVillage.Rows[0]["Infrastructure_TB"].ToString()) == 1)
            //{
            //    chkSipPartial.Checked = true;

            //}
            //if (Convert.ToInt32(dtUserVillage.Rows[0]["Infrastructure_FC"].ToString()) == 1)
            //{
            //    chkRenPartial.Checked = true;
            //}

            if (dtUserVillage.Rows[0]["Retention_PC"].ToString() == "P")
            {
                chkRenPartial.Checked = true;

            }
            if (dtUserVillage.Rows[0]["Retention_PC"].ToString() == "C")
            {
                chkComplete.Checked = true;
            }
            #endregion

        }
        else
        {
            ClearData();
            btnsave.Visible = true;
            ViewState["GUID_School"] = "";
        }
    }


    public void ClearData()
    {
        txtCountDriking.Text = "0";

        rblBalsabaTB.Checked = false;
        rblBalsabaFC.Checked = false;
        TextBox1.Text = "0";
        TextBox2.Text = "0";
        TextBox3.Text = "0";
        TextBox4.Text = "0";
        TextBox5.Text = "0";
        TextBox6.Text = "0";
        TextBox7.Text = "0";
        TextBox8.Text = "0";


        txt1.Text = "0";
        txt2.Text = "0";
        txt3.Text = "0";
        txt4.Text = "0";
        txt5.Text = "0";
        txt6.Text = "0";
        txt7.Text = "0";
        txt8.Text = "0";
     
        chkRenFC.Checked = false;
        chkSMC.Checked = false;
        rblSMCTB.Checked = false;
        rblSMCFC.Checked = false;

        chkNewSmc.Checked = false;
        rblSmcNew.Checked = false;
        rblSmcNew1.Checked = false;
        chkNewSmc.Checked = false;
        rblSmcNew1.Checked = false;
        txtTotalMember.Text = "";
        txtTotalFmember.Text = "";
        rblSMCTB.Checked = false;
        rblSMCFC.Checked = false;
        rblCLTTB.Checked = false;
        rblCLTFC.Checked = false;
        lbldriking.Text = "0";
        lblToilet.Text = "0";
        lblElectricity.Text = "0";
        lblCltKit.Text = "0";
        lblbook.Text = "0";
        lblKitchen.Text = "0";
        lblBoundaryWall.Text = "0";
        lblSlides.Text = "0";
        lblPlay.Text = "0";
        txt_pbname.Text = "";
        chkAnnual.Checked = false;

        txtOther.Text = "";
       chkSIPAnnaul.Checked = false;
       
       chkRetention.Checked = false;
        
        chkSIPTB.Checked = false;
        
       chkRenTB.Checked = false;

       chkRenPartial.Checked = false;
       chkSipPartial.Checked = false;
       chkSIPFC.Checked = false;
        
       chkRenFC.Checked = false;
       
        chkSipPartial.Checked = false;
       chkRenPartial.Checked = false;
       chkSipComplete.Checked = false;

       chkComplete.Checked = false;
        
        txtdrinking.Enabled = true;
        txtToilet.Enabled = true;
        txtElectricity.Enabled = true;
        txtPlay.Enabled = true;
        txtSlides.Enabled = true;
        txtBoundaryWall.Enabled = true;
        txtKitchen.Enabled = true;
        txtCltKit.Enabled = true;
        txtbook.Enabled = true;
             txtClassRoom.Text = "";
                txtMaleTeacher.Text="";
                txtFemaleTeacher.Text= "";
        chkPhysical.Checked = false;
        rblPhysicalTB.Checked = false;
        rblPhysicalFC.Checked = false;
        chklife.Checked = false;
            ViewState["GUID_School"] ="";
           
                chkHolding.Checked = false;
           
            
                chkSMC.Checked = false;
                rblSMCTB.Checked = true;
           
            
                rblSMCFC.Checked = false;
           
        
            
                txtOtherSIPFC.Text =  "";
           
                txtsmcmeetinFC.Text =  "";
           

          
            foreach (ListItem item in CBL_bookformat.Items)
            {
               
                    item.Selected = false;
               
                
            }
           
                chkClT.Checked = false;
                rblCLTTB.Checked = true;
          
               
                rblCLTFC.Checked = false;
           
                chkHindiA.Checked = false;
           
                 chkHindiB.Checked = false;
                      
                  chkHindiC.Checked = false;
                
                   chkHindiD.Checked = false;
                    
                   chkHindiE.Checked = false;
                 
                            chkEnglishA.Checked = false;
                      
                            chkEnglishB.Checked = false;
                       
                            chkEnglishC.Checked = false;
                       
                            chkEnglishD.Checked = false;
                       
                            chkEnglishE.Checked = false;
                      
                            chkMathA.Checked = false;
                        
                            chkMathB.Checked = false;
                        
                            chkMathC.Checked = false;
                       
                            chkMathD.Checked = false;
                      
                       
                            chkMathE.Checked = false;
                       
             

           
                        chkGame1.Checked = false;
                 
                        chkGame2.Checked = false;
                   
                        chkGame3.Checked = false;
                   
                        chkGame4.Checked = false;
                   
                        chkGame5.Checked = false;
                   
                  chkBalsabha.Checked = false;
             
                rblBalsabaFC.Checked = false;
         
                chkBalSabhaFor.Checked = false;
           
                chkOrientation.Checked = false;
            
                chkChat.Checked = false;
           
                chkKit.Checked = false;
           

           
                    rblTestTBPre.Checked = false;
              
                    rblTestpreFC.Checked = false;
               
                    rblPartialPre.Checked = false;
               
                    rblCompletePre.Checked = false;
               
                    rblTestTBMid.Checked = false;
              
                    rblTestMidFC.Checked = false;
               
                    rblPartialMid.Checked = false;
               
                    rblCompleteMid.Checked = false;
                      rblTestTBPost.Checked = false;
              
                    rblTestPostFC.Checked = false;
               
                    rblPartialPost.Checked = false;
               
                    rblCompletePost.Checked = false;


                   
                        txtHealth.Text = "";

                        txtSMCMeeting.Text = "";


                        txtAdgirls.Text = "";

                        txtAdBoy.Text = "";

                        txtleftGirl.Text = "";

                        txtleftBoy.Text = "";

                        txtGirlNot.Text = "";

                        txtBoyNot.Text = "";
                        txtToilet.BackColor = Color.White;
                        txtdrinking.BackColor = Color.White;
                       
                        txtElectricity.BackColor = Color.White;
                        txtbook.BackColor = Color.White;
                        txtPlay.BackColor = Color.White;
                        txtSlides.BackColor = Color.White;
                        txtBoundaryWall.BackColor = Color.White;
                        txtKitchen.BackColor = Color.White;
                        txtCltKit.BackColor = Color.White;


                        txtToilet1.BackColor = Color.White;
                        txtdrinking1.BackColor = Color.White;

                        txtElectricity1.BackColor = Color.White;
                        txtbook1.BackColor = Color.White;
                        txtPlay1.BackColor = Color.White;
                        txtSlides1.BackColor = Color.White;
                        txtBoundaryWall1.BackColor = Color.White;
                        txtKitchen1.BackColor = Color.White;
                        txtCltKit1.BackColor = Color.White;
                        txtFemaleTeacher.Text = "";

                        txtMaleTeacher.Text = "";
                        txtClassRoom.Text = "";

                       
                            chkSACUpdate.Checked = false;
                        
                            rblSacTB.Checked = false;
                        
                            rblSacFB.Checked = false;

                            txtSMCMeeting.Text = "";

                            txtHealth.Text = "";

                            txtAdgirls.Text = "";

                            txtAdBoy.Text = "";

                            txtleftGirl.Text = "";

                            txtleftBoy.Text = "";

                            txtGirlNot.Text = "";

                            txtGirlNot.Text = "";
                            chkSMC.Checked = false;
                            rblSMCTB.Checked = false;
                            rblSMCFC.Checked = false;
                            chkClT.Checked = false;
                            rblCLTTB.Checked = false;
                            rblCLTFC.Checked = false;
                            chkNewSmc.Checked = false;
                            rblSmcNew.Checked = false;
                            rblSmcNew1.Checked = false;
                            chkAnnual.Checked = false;
                            chkSIPAnnaul.Checked = false;
                            chkRetention.Checked = false;

                            chkSIPTB.Checked = false;
                            chkRenTB.Checked = false;
                            chkSIPFC.Checked = false;

                            chkRenFC.Checked = false;
                            chkSipPartial.Checked = false;
                            chkRenPartial.Checked = false;


                            chkSipComplete.Checked = false;
                            chkComplete.Checked = false;
       
    }
    public void LoadEnrolled()
    {
        DataRow dr = null;
        DataTable dtOther = new DataTable();
        dtOther.Columns.Add(new DataColumn("ID", System.Type.GetType("System.Int32")));
        dtOther.Columns.Add(new DataColumn("Name", System.Type.GetType("System.String")));
        dr = dtOther.NewRow();
        dr["ID"] = 52;
        dr["Name"] = "Enrollment";
        dtOther.Rows.Add(dr);

        dr = dtOther.NewRow();
        dr["ID"] = 53;
        dr["Name"] = "Retention";
        dtOther.Rows.Add(dr);

        dr = dtOther.NewRow();
        dr["ID"] = 54;
        dr["Name"] = "Learning Level";
        dtOther.Rows.Add(dr);
        dr = dtOther.NewRow();

        dr["ID"] = 55;
        dr["Name"] = "others (specify)";
        dtOther.Rows.Add(dr);

        CBL_bookformat.DataSource = dtOther;
        CBL_bookformat.DataTextField = "Name";
        CBL_bookformat.DataValueField = "ID";
        CBL_bookformat.DataBind();
      
    }
    public void UserData()
    {
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

        objComman.BindDLL("MstUser", "UserName as UserId,FristName +' ('+ UserName +')' as [UserName] ", conditions, "", "", ddlUser, "UserName", "UserId", "Select");

    }
    protected void ddlUser_SelectedIndexChanged(object sender, EventArgs e)
    {
        string strQry = "";
        if (ddlUser.SelectedIndex > 0)
        {
            strQry = "   select Villagecode  from MstUser   where UserName='" + ddlUser.SelectedValue + "' ";
            DataTable dtUserVillage = objMain.LoadData(strQry);

            string strVillage = dtUserVillage.Rows[0]["Villagecode"].ToString();

            conditions = "mst5Village.ClusterCode in('" + strVillage + "') ";

            conditions = "mst5Village.ClusterCode in('" + strVillage + "') ";

            strQry = "";
            strQry = "select VillageCode,VillageName  from mst5Village where mst5Village.ClusterCode in('" + strVillage + "')     ";
            strQry += " Union select VillageCode,VillageName  from mstActivityVillage where UserID='" + ddlUser.SelectedValue + "'   ";
            strQry += " Union ";
             strQry += "  select mst5Village.VillageCode,VillageName  from mst5Village  ";
            strQry += " inner join tblActivityUpdate_School on tblActivityUpdate_School.VillageCode=mst5Village.VillageCode  ";
              strQry += "  where mst5Village.ClusterCode in('" + Session["Cluseter"].ToString() + "' )   and UserID='" + ddlUser.SelectedValue + "'   ";
             
            DataTable dtVillage = objMain.LoadData(strQry);
            //objComman.BindDLLMasterTable("MstUser", "UserName as UserId,[FristName]+' ('+ UserName +')' as [UserName] ", dtUser, conditions, "", "", ddlUser, "UserName", "UserId", "Select");

            objComman.BindDLLMasterTable("mst5Village", "VillageCode,VillageName ", dtVillage, "", "VillageName", "", ddlVilage, "VillageName", "VillageCode", "Select");

            //objComman.BindDLL("mst5Village", "VillageCode,VillageName ", conditions, "VillageName", "", ddlVilage, "VillageName", "VillageCode", "Select");


        }
        //DataTable dt = objMain.GetActivityUserWiseMaxDateNew(ddlUser.SelectedValue, Session["Cluseter"].ToString());
        //if (dt.Rows.Count > 0   )
        //{
        //    if (Convert.ToString(dt.Rows[0]["ActivityDate"].ToString())!="")
        //    {
        //    CalendarExtenderTourdate.StartDate = Convert.ToDateTime(dt.Rows[0]["ActivityDate"].ToString()).AddDays(1);
        //    }
        //}
        pnlMain.Enabled = false;
    }
  
    //protected void txtdrinking_TextChanged(object sender, EventArgs e)
    //{
    //    int icount = 0;
    //    int iwaterpre = Convert.ToInt32(lbldriking.Text);
    //    if (iwaterpre == 1)
    //    {
    //        if (icount == 0)
    //        {
    //            icount = 3;
    //        }
    //        else if (icount == 1)
    //        {
    //            icount = 3;
    //        }
    //        else if (icount == 2)
    //        {
    //            icount = 3;
    //        }
    //        if (icount == 3)
    //        {
    //            txtdrinking.BackColor = Color.Green;
    //            //btn_water.setBackgroundResource(R.drawable.bg_buttonroundreen);

    //            icount++;
    //            lbldriking.Text = "1";
    //        }
    //        else if (icount == 4)
    //        {
    //            txtdrinking.BackColor = Color.Blue;
    //       //     btn_water.setBackgroundResource(R.drawable.bg_buttonroundblue);

    //            lbldriking.Text = "4";
    //            icount = 3;
    //        }

    //    }
    //    else if (iwaterpre == 2)
    //    {
    //        if (icount == 0)
    //        {
    //            icount = 2;
    //        }
    //        else if (icount == 1)
    //        {
    //            icount = 2;
    //        }
    //        if (icount == 1)
    //        {
    //            txtdrinking.BackColor = Color.Red;
    //        //    btn_water.setBackgroundResource(R.drawable.bg_buttonroundred);
    //            icount++;
               
    //            lbldriking.Text = "3";

    //        }
    //        else if (icount == 2)
    //        {
    //            txtdrinking.BackColor = Color.Orange;
    //         //   btn_water.setBackgroundResource(R.drawable.bg_buttonroundorane);
    //            icount++;
               
    //            lbldriking.Text = "2";
    //        }
    //        else if (icount == 3)
    //        {
    //            txtdrinking.BackColor = Color.Green;
    //            //btn_water.setBackgroundResource(R.drawable.bg_buttonroundreen);
    //            icount = 2;
              
    //            lbldriking.Text = "1";
    //        }

    //    }
    //    else if (iwaterpre == 3)
    //    {
    //        if (icount == 0)
    //        {
    //            icount = 3;
    //        }/*
    //        * else if (icount == 1) { icount = 3; } else if (icount == 2) {
    //        * icount = 3; }
    //        */
    //        if (icount == 3)
    //        {
    //            txtdrinking.BackColor = Color.Red;
    //         //   btn_water.setBackgroundResource(R.drawable.bg_buttonroundred);
    //            icount--;
             
    //            lbldriking.Text = "3";
    //        }
    //        else

    //            if (icount == 2)
    //            {
    //                txtdrinking.BackColor = Color.Orange;
    //             //   btn_water.setBackgroundResource(R.drawable.bg_buttonroundorane);
    //                icount--;
     
    //                lbldriking.Text = "2";
    //            }
    //            else if (icount == 1)
    //            {
    //                txtdrinking.BackColor = Color.Green;
    //              //  btn_water.setBackgroundResource(R.drawable.bg_buttonroundreen);

    //                icount = 0;
                   
    //                lbldriking.Text = "1";
    //            }

    //    }
    //    else if (iwaterpre == 4)
    //    {
    //        if (icount == 0)
    //        {
    //            icount = 4;
    //        }
    //        else if (icount == 1)
    //        {
    //            icount = 4;
    //        }
    //        else if (icount == 2)
    //        {
    //            icount = 4;
    //        }
    //        if (icount == 3)
    //        {
    //            //btn_water.setBackgroundResource(R.drawable.bg_buttonroundreen);
    //            txtdrinking.BackColor = Color.Green;
    //              lbldriking.Text = "1";
    //            icount++;
    //        }
    //        else if (icount == 4)
    //        {
    //            txtdrinking.BackColor = Color.Blue;
    //         //   btn_water.setBackgroundResource(R.drawable.bg_buttonroundblue);
    //            lbldriking.Text = "4";
               
    //            icount = 3;
    //        }

    //    }
    //    else
    //    {
    //        if (icount == 1)
    //        {
    //            txtdrinking.BackColor = Color.Red;
    //            //btn_water.setBackgroundResource(R.drawable.bg_buttonroundred);
    //            icount++;
               
    //            lbldriking.Text = "3";

    //        }
    //        else if (icount == 2)
    //        {
    //            txtdrinking.BackColor = Color.Orange;
    //         //   btn_water.setBackgroundResource(R.drawable.bg_buttonroundorane);
    //            icount++;
          
    //            lbldriking.Text = "2";
    //        }
    //        else if (icount == 3)
    //        {
    //            txtdrinking.BackColor = Color.Green;
    //           // btn_water.setBackgroundResource(R.drawable.bg_buttonroundreen);

    //            // btn_water.setBackgroundResource(R.drawable.green_btn_radio_holo_light);
    //            icount++;
              
    //            lbldriking.Text = "1";
    //        }
    //        else if (icount == 4)
    //        {
    //          //  btn_water.setBackgroundResource(R.drawable.bg_buttonroundblue);
    //            txtdrinking.BackColor = Color.Blue;
    //            // btn_water.setBackgroundResource(R.drawable.purple_btn_radio_holo_light);
    //            icount++;
              
    //            lbldriking.Text = "4";
    //            icount = 0;
    //        }
    //        else if (icount == 0)
    //        {
    //            txtdrinking.BackColor = Color.White;
    //           /// btn_water.setBackgroundResource(R.drawable.bg_buttonroundwhite);

    //            icount++;

    //        }

    //    }
    //}
    protected void ddlVilage_SelectedIndexChanged(object sender, EventArgs e)
    {
      
        LoadSchool();
        pnlMain.Enabled = false;
        ddlRemark.SelectedIndex = 0;
    }
    protected void ddlSchool_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlRemark.SelectedIndex = 0;

    }
    protected void btnClose_Click(object sender, EventArgs e)
    {
        bool InsertD2d = false;
        for (int i = 0; i < Gv_Display.Rows.Count; i++)
        {
            DropDownList ddlStatus = ((DropDownList)Gv_Display.Rows[i].FindControl("ddlStatus"));
            Label lbUniqueCode = ((Label)Gv_Display.Rows[i].FindControl("lbUniqueCode"));
            Label lblStatus = ((Label)Gv_Display.Rows[i].FindControl("lbStatus"));
        
            if (lblStatus.Text=="2")
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

    protected void Gv_Display_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DropDownList ddlStatus = ((DropDownList)e.Row.FindControl("ddlStatus"));
            Label lbStatus = ((Label)e.Row.FindControl("lbStatus"));
            ddlStatus.SelectedValue = lbStatus.Text;

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
            }

            Session["D2dBind"] = dataTable;
        }
        this.txtSearch.Text = "";
        ModalPopupExtender.Show();
        ModalPopupExtender1.Hide();
    }
    public void LoadSchool()
    {
        conditions = " Villagecode='" + ddlVilage.SelectedValue + "'  ";

        objComman.BindDLL("Mstschool", "SchoolCode ,Name", conditions, "", "", ddlSchool, "Name", "SchoolCode", "Select");

    }
    protected void btnimgComm1_Click(object sender, EventArgs e)
    {
      
        imgMKS.ImageUrl = "TabletImage/" + lblMM.Text;
        MpexdrDistrict.Show();
    }
    protected void ddlSubject_SelectedIndexChanged(object sender, EventArgs e)
    {
        objComman.BindDLLNew("mstGKPDeatils", "Level", "SubjectID='" + ddlSubject.SelectedValue + "' ", "Level", "asc", ddlLevel, "Level", "Level", "Select");
        MpexdrDistrict8.Show();
    }
    protected void ddlLevel_SelectedIndexChanged(object sender, EventArgs e)
    {
        objComman.BindDLL("mstGKPDeatils", "'Session'+' '+ CONVERT(varchar,NoofLevel) as Session,NoofLevel", "SubjectID='" + ddlSubject.SelectedValue + "' and  Level='" + ddlLevel.SelectedValue + "' ", "'Session'+' '+ CONVERT(varchar,NoofLevel) ", "asc", ddlSSession, "Session", "NoofLevel", "Select");
        MpexdrDistrict8.Show();
    }
    protected void LnkBtnBlock_OnClick(object sender, EventArgs e)
    {
        LinkButton bt = (LinkButton)sender;

        GridViewRow gvr = (GridViewRow)bt.NamingContainer;

        string UniqueCode = (gvr.FindControl("lblCUniqueChildCode") as Label).Text;
        string lblsubjectid = (gvr.FindControl("lblsubjectid") as Label).Text;
        string lblLevelID = (gvr.FindControl("lblLevelID") as Label).Text;
        string lblSession = (gvr.FindControl("lblSession") as Label).Text;
        string lblgkp_fc = (gvr.FindControl("lblgkp_fc") as Label).Text;
        string lblgkp_tb = (gvr.FindControl("lblgkp_tb") as Label).Text;
        lblGuId.Text = UniqueCode;
        ddlSubject.SelectedValue = lblsubjectid;
        ddlSubject_SelectedIndexChanged(ddlSubject, null);
        int index = ddlLevel.Items.IndexOf(ddlLevel.Items.FindByText(lblLevelID.Trim()));
        if (index != -1)
        {
            ddlLevel.SelectedIndex = index;
        }
        ddlLevel_SelectedIndexChanged(ddlLevel, null);

        int index1 = ddlSSession.Items.IndexOf(ddlSSession.Items.FindByText(lblSession.Trim()));
        if (index1 != -1)
        {
            ddlSSession.SelectedIndex = index1;
        }
        if (lblgkp_fc == "1")
        {
            rblApprove.SelectedValue = "1";
        }
        if (lblgkp_tb == "1")
        {
            rblApprove.SelectedValue = "2";
        }
        MpexdrDistrict8.Show();
        //Label lblStatus = (Label)gvr.FindControl("lblStatus");
        //Session["UnquieId"] = UniqueChildCode;
        //Session["StateCode"] = ddlState.SelectedValue;
        //Session["DistCode"] = ddlDistrict.SelectedValue;
        //Session["BlockCode"] = ddlBlock.SelectedValue;
        //Session["PhanyCode"] = ddlPanchayat.SelectedValue;
        //Session["VillCode"] = ddlVillage.SelectedValue;


    }


    protected void btnSaveGkp_Click(object sender, EventArgs e)
    {
        SaveDataGKP();
    }
    protected void btnAddGkp_Click(object sender, EventArgs e)
    {
        ddlSubject.SelectedIndex = 0;
        ddlLevel.Items.Clear();
        ddlSSession.Items.Clear();
        lblGuId.Text = "";
        MpexdrDistrict8.Show();
    }
    public void SaveDataGKP()
    {
        string con = "";
        string Dateof = txtDate.Text;

        string[] b = Dateof.Split('/');

        string FcDate = b[2] + '-' + b[1] + '-' + b[0];

        if (ddlSubject.SelectedIndex <= 0)
        {

            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Select Subject')</script>", false);
            MpexdrDistrict8.Show();
            return;
        }
        if (ddlLevel.SelectedIndex <= 0)
        {

            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Select Level')</script>", false);
            MpexdrDistrict8.Show();
            return;
        }
        if (ddlSubject.SelectedIndex <= 0)
        {

            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Select Session')</script>", false);
            MpexdrDistrict8.Show();
            return;
        }

        if (lblGuId.Text.Length > 2)
        {
            con = "where ActivityDate =('" + FcDate + "') and GUID_GKP not in('" + ddlSchool.SelectedValue + "')     and Schoolcode='" + ddlSchool.SelectedValue + "' and  SubjectID='" + ddlSubject.SelectedValue + "'  and  LevelID='" + ddlLevel.SelectedValue + "'  and  Session='" + ddlSSession.SelectedItem.Text + "'  ";

            DataTable dt = objMain.LoadCheckGkp(con);
            if (dt.Rows.Count > 0)
            {

                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('This Activty Allreday Exit')</script>", false);
                MpexdrDistrict8.Show();
                return;
            }
        }
        else
        {
            con = "where ActivityDate =('" + FcDate + "')    and Schoolcode='" + ddlSchool.SelectedValue + "' and  SubjectID='" + ddlSubject.SelectedValue + "'  and  LevelID='" + ddlLevel.SelectedValue + "'  and  Session='" + ddlSSession.SelectedItem.Text + "'  ";
            DataTable dt = objMain.LoadCheckGkp(con);
            if (dt.Rows.Count > 0)
            {

                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('This Activty Allreday Exit')</script>", false);
                MpexdrDistrict8.Show();
                return;
            }
        }

        string GUId = "";
        string Flag = "";
        string Approve = "";
        if (Session["user_level"].ToString() == "19")
        {
            Approve = "FC";
        }
        if (Session["user_level"].ToString() == "39" || Session["user_level"].ToString() == "30")
        {
            Approve = "B";
        }
        Int32 TB = 0;
        Int32 FC = 0;
        if (lblGuId.Text.Length > 2)
        {
            GUId = lblGuId.Text;
            Flag = "P";
        }
        else
        {
            GUId = objMain.Generate_RandomString(8);
            Flag = "I";
        }
        if (Convert.ToInt32(rblApprove.SelectedValue) == 1)
        {
            FC = 1;
        }
        if (Convert.ToInt32(rblApprove.SelectedValue) == 2)
        {
            TB = 1;
        }

        SqlParameter[] parm = new SqlParameter[]
            {
           
           
            new SqlParameter("@UserID", ddlUser.SelectedValue),
            new SqlParameter("@GUID_GKP", GUId),
            new SqlParameter("@SubjectID", ddlSubject.SelectedValue),
            new SqlParameter("@LevelID", ddlLevel.SelectedItem.Text),
            new SqlParameter("@Session", ddlSSession.SelectedItem.Text),
            new SqlParameter("@GKP_fc", FC),
            new SqlParameter("@GKP_tb", TB),
            new SqlParameter("@SchoolCode", ddlSchool.SelectedValue),
             new SqlParameter("@VillageCode", ddlVilage.SelectedValue),
             new SqlParameter("@ActivityDate",Convert.ToDateTime(FcDate)),
             new SqlParameter("@ApproveStatus", Approve),  
              new SqlParameter("@Flag", Flag),
            
              };
        int result = SqlHelper.ExecuteNonQuery(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "InsertUpdateGkp", parm);

        if (result > 0)
        {
            string conq = "ActivityDate =('" + FcDate + "')    and Schoolcode='" + ddlSchool.SelectedValue + "' ";


            DataTable dtGKP = objMain.LoadGKPDeatils(conq);
            if (dtGKP.Rows.Count > 0)
            {
                gvGkp.DataSource = dtGKP;
                gvGkp.DataBind();
            }

            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Save Sucessfully')</script>", false);
         
        }
    }
}