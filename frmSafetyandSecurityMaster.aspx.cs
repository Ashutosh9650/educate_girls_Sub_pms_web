using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Drawing;
using System.IO;


public partial class frmSafetyandSecurityMaster : System.Web.UI.Page
{
    clsMain objMain = new clsMain();
    Comman objComman = new Comman();
    public bool vADD = false;
    public bool vVerify = false;
    public bool vDelete = false;

    string conditions = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Convert.ToString(Session["username"]) != "")
            {
                FillCBState();
              //  LoadData();
                ViewState["Save"] = "Save";
                FillActive(1);
                LoadDataMain();
                ViewState["DonorID"] = "";
            }
            else
            {
                Response.Redirect("Login.aspx", false);

            }

        }
     
    }

    public void FillActive(Int32 Flag)
   {
       conditions = "";
       conditions = "LookupFlag ='IA' and Active=1 ";
       if (Flag == 1)
       {
           conditions += " and LookupCode=1";
       }
       objComman.BindDLL("mstLookup", "LookupCode,Description", conditions, "LookupCode", "asc", ddlStatus, "Description", "LookupCode", "Select");



    }
    public void Upload(object sender, EventArgs e)
    {
        txtMuhala.Text = "";
        FillCBDist();
        chkBlock.Items.Clear();
        txtMuhala1.Text = "";

    }
    protected void btnApprove_Click(object sender, EventArgs e)
    {
        FillActive(1);
        ClearData();
    
        ViewState["Save"] = "Save";
        ViewState["DonorID"] = "";
    }

    protected void btnNewSerach_Click(object sender, EventArgs e)
    {
        LoadData();
    }
    protected void gvStaffScheduling_OnRowCommand(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
      

          
            Label lblLockRecord = (Label)e.Row.FindControl("lblLockRecord");
            LinkButton lnkLock = (LinkButton)e.Row.FindControl("lnkLock");
            if (lblLockRecord.Text == "1")
            {
                lnkLock.Text = "Active";
            }
            else
            {
                lnkLock.Text = "InActive";
            }
            
        }
    }
    protected void btnLnk_Click(object sender, EventArgs e)
    {
        LinkButton bt = (LinkButton)sender;

        GridViewRow gvr = (GridViewRow)bt.NamingContainer;
        string UniqueChildCode = (gvr.FindControl("lblScheduleID") as Label).Text;
        string Status = (gvr.FindControl("lnkLock") as LinkButton).Text;

        Int32 iStatus = 0;
        if (Status == "Active")
        {
            iStatus = 2;
        }
        if (Status == "InActive")
        {
            iStatus = 1;
        }
        SqlParameter[] parm = new SqlParameter[]
            {
           
           
            new SqlParameter("@cId",UniqueChildCode),
            
            new SqlParameter("@Status",iStatus),
       
                    
     
           
              };
        int result = SqlHelper.ExecuteNonQuery(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "UpdateSafetySecurity", parm);
        if (result > 0)
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Save Successfully')</script>", false);
            LoadDataMain();
        }
    }
    protected void GVMain_OnRowCommand(object sender, GridViewCommandEventArgs e)
    {

        if (e.CommandName == "GVUIO")
        {
            int iIndex = Convert.ToInt32(e.CommandArgument);
            string DID = GVMain.DataKeys[iIndex]["DID"].ToString();
            FillControls(DID);
            ViewState["Save"] = "Edit";

            pnlMain1.Enabled = false;

            for (int i = 0; i < GVMain.Rows.Count; i++)
            {
                GridViewRow RowD = GVMain.Rows[i];
                if (i % 2 == 0)
                {
                    RowD.BackColor = Color.White;
                }
                else
                {
                    RowD.BackColor = Color.FromArgb(245, 245, 245);
                }

            }
            GridViewRow row = GVMain.Rows[iIndex];
            row.BackColor = Color.LightYellow;
        }
    }

    public void ClearData()
    {
        txtDonorName.Text ="";

        txtFromDate.Text = "";
        ddlPeriod.SelectedIndex = 0;
     
        txtTodate.Text = "";
        ddInGeography.SelectedIndex = 0;
        ddInGeography_SelectedIndexChanged(ddInGeography, null);
        foreach (ListItem item in ChkState.Items)
        {
           
                item.Selected = false;
            
        }
        pnlMain1.Enabled = true;
        txt_pbname.Text = "";
        txtMuhala.Text = "";
        txtMuhala1.Text = "";
        ViewState["Save"] = "Save";
        ViewState["DonorID"] = "";
    }
    public void FillControls(string ID)
    {
        DataTable dtMain = objMain.LoadData("select * FROM [mstSafetySecurity]  where SID=" + ID + "   ");
        if (dtMain.Rows.Count > 0)
        {
            ClearData();
            FillActive(0);
            pnlMain1.Enabled = false;
            ViewState["DonorID"] = ID;
            txtDonorName.Text = dtMain.Rows[0]["EmergencyName"].ToString();
            DateTime fDate = Convert.ToDateTime(dtMain.Rows[0]["FromDate"].ToString());
            txtFromDate.Text = fDate.ToString("dd/MM/yyy");

            DateTime tDate = Convert.ToDateTime(dtMain.Rows[0]["ToDate"].ToString());
            txtTodate.Text = tDate.ToString("dd/MM/yyy");
            ddInGeography.SelectedValue = dtMain.Rows[0]["Type"].ToString();
            ddInGeography_SelectedIndexChanged(ddInGeography, null);

            ddlPeriod.SelectedValue = dtMain.Rows[0]["Period"].ToString();
            ddlStatus.SelectedValue = dtMain.Rows[0]["ActiveStatus"].ToString();

            if (Convert.ToInt32(ddInGeography.SelectedValue) == 1)
            {
                DataTable dtState = objMain.LoadData("select Distinct StateCode from mstSafetySecurityDistrictProfile  where SID=" + ID + "   ");
                string Stcode = "";
                    string stname="";
                foreach (DataRow dr in dtState.Rows)
                {
                    foreach (ListItem item in ChkState.Items)
                    {
                        if (item.Value == dr["StateCode"].ToString())
                        {
                            item.Selected = true;
                            Stcode += item.Text + ",";
                        }
                    }
                }
                if (Stcode.Length > 0)
                {
                    Stcode = Stcode.Substring(0, Stcode.LastIndexOf(","));
                    txt_pbname.Text = Stcode;
                }
            }
                if (Convert.ToInt32(ddInGeography.SelectedValue) == 2)
                {
                    DataTable dtState = objMain.LoadData("select Distinct mst2District.StateCode from mstSafetySecurityDistrictProfile inner join mst2District on mst2District.DistrictCode=mstSafetySecurityDistrictProfile.DistrictCode  where SID=" + ID + "   ");
                    DataTable dtDistr = objMain.LoadData("select mstSafetySecurityDistrictProfile.DistrictCode,mst2District.StateCode from mstSafetySecurityDistrictProfile inner join mst2District on mst2District.DistrictCode=mstSafetySecurityDistrictProfile.DistrictCode  where SID=" + ID + "   ");
                    string State = "";
                    foreach (DataRow dr in dtState.Rows)
                    {
                        foreach (ListItem item in ChkState.Items)
                        {
                            if (item.Value == dr["StateCode"].ToString())
                            {
                                item.Selected = true;
                                State += item.Text + ",";
                            }
                        }
                    }
                    if (State.Length > 0)
                    {
                        State = State.Substring(0, State.LastIndexOf(","));
                        txt_pbname.Text = State;
                    }
                    FillCBDist();
                    string Dist = "";
                    foreach (DataRow dr in dtDistr.Rows)
                    {
                        foreach (ListItem item in chkDistrict.Items)
                        {
                            if (item.Value == dr["DistrictCode"].ToString())
                            {
                                item.Selected = true;
                                Dist += item.Text + ",";
                            }
                        }
                    }
                    if (Dist.Length > 0)
                    {
                        Dist = Dist.Substring(0, Dist.LastIndexOf(","));
                        txtMuhala.Text = Dist;
                    }
                }
                if (Convert.ToInt32(ddInGeography.SelectedValue) == 3)
                {
                    DataTable dtState = objMain.LoadData("select Distinct mst2District.StateCode from mstSafetySecurityDistrictProfile inner join mst3Block on mst3Block.BlockCOde=mstSafetySecurityDistrictProfile.BlockCOde inner join mst2District on mst2District.DistrictCode=mst3Block.DistrictCode   where SID=" + ID + "   ");
                    DataTable dtDistr = objMain.LoadData("select  Distinct mst3Block.DistrictCode from mstSafetySecurityDistrictProfile inner join mst3Block on mst3Block.BlockCOde=mstSafetySecurityDistrictProfile.BlockCOde inner join mst2District on mst2District.DistrictCode=mst3Block.DistrictCode  where SID=" + ID + "   ");
                    DataTable dtBlcok = objMain.LoadData("select mstSafetySecurityDistrictProfile.BlockCOde,mst3Block.StateCode,mst3Block.DistrictCode from mstSafetySecurityDistrictProfile inner join mst3Block on mst3Block.BlockCOde=mstSafetySecurityDistrictProfile.BlockCOde inner join mst2District on mst2District.DistrictCode=mst3Block.DistrictCode  where SID=" + ID + "   ");
                    string State = "";
                    foreach (DataRow dr in dtState.Rows)
                    {
                        foreach (ListItem item in ChkState.Items)
                        {
                            if (item.Value == dr["StateCode"].ToString())
                            {
                                item.Selected = true;
                                State += item.Text + ",";
                            }
                        }
                    }
                    if (State.Length > 0)
                    {
                        State = State.Substring(0, State.LastIndexOf(","));
                        txt_pbname.Text = State;
                    }
                    FillCBDist();
                    string Dist = "";
                    foreach (DataRow dr in dtDistr.Rows)
                    {
                        foreach (ListItem item in chkDistrict.Items)
                        {
                            if (item.Value == dr["DistrictCode"].ToString())
                            {
                                item.Selected = true;
                                Dist += item.Text + ",";
                            }
                        }
                    }
                    if (Dist.Length > 0)
                    {
                        Dist = Dist.Substring(0, Dist.LastIndexOf(","));
                        txtMuhala.Text = Dist;
                    }
                    FillCBBock();
                    string BCode = "";
                    foreach (DataRow dr in dtBlcok.Rows)
                    {
                        foreach (ListItem item in chkBlock.Items)
                        {
                            if (item.Value == dr["BlockCode"].ToString())
                            {
                                item.Selected = true;
                                BCode += item.Text + ",";
                            }
                        }
                    }
                    if (BCode.Length > 0)
                    {
                        BCode = BCode.Substring(0, BCode.LastIndexOf(","));
                        txtMuhala1.Text = BCode;
                    }
                }
           

           
        }
         
    
    }
    public void LoadData()
    {
        bool Flag = false;
        string Oid = "";
        //foreach (ListItem item in chkID.Items)
        //{
        //    if (item.Selected)
        //    {
        //        if (item.Value == "0")
        //        {
        //            Flag = true;
        //            Oid += "" + item.Text + "" + ",";
        //            break;
                   
        //        }
        //        else
        //        {
        //            Oid += "" + item.Value + "" + ",";
        //        }

        //    }
        //}
        if (Oid.Length > 0)
        {
            Oid = Oid.Substring(0, Oid.LastIndexOf(","));
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Select any one Indicator in Indicator Box!! ')</script>", false);


            this.chkBlock.Focus();
            return;
        }

        if (ViewState["Save"].ToString() == "Save")
        {
            if (Flag == true)
            {
                DataTable dtCheck = objMain.LoadData(" SELECT 0 MainID, [mstDonorSuboutcome].[SoutComeID],OutcomeName,      [mstDonorSuboutcome].[DOutcomeID]      ,[SubID]    ,[SSubOutcomeName]  FROM [mstDonorSuboutcome]  inner join mstDonorOutcome on mstDonorOutcome.[DOutcomeID]=[mstDonorSuboutcome].[DOutcomeID]   ");
                //GV_DynamicGrid.DataSource = dtCheck;
                //GV_DynamicGrid.DataBind();
                ViewState["dtselect"] = dtCheck;
            }
            else
            {
                if (Oid.Length > 0)
                {
                    DataTable dtCheck = objMain.LoadData(" SELECT 0 MainID, [mstDonorSuboutcome].[SoutComeID],OutcomeName,      [mstDonorSuboutcome].[DOutcomeID]      ,[SubID]    ,[SSubOutcomeName]  FROM [mstDonorSuboutcome]  inner join mstDonorOutcome on mstDonorOutcome.[DOutcomeID]=[mstDonorSuboutcome].[DOutcomeID]    where [mstDonorSuboutcome].[DOutcomeID] in(" + Oid + ")  ");
                    //GV_DynamicGrid.DataSource = dtCheck;
                    //GV_DynamicGrid.DataBind();
                    ViewState["dtselect"] = dtCheck;
                }
            }
        }
        else
        {
            if (Flag == true)
            {
                DataTable dtCheck = objMain.LoadData(" SELECT 0 MainID, [mstDonorSuboutcome].[SoutComeID],OutcomeName,      [mstDonorSuboutcome].[DOutcomeID]      ,[SubID]    ,[SSubOutcomeName]  FROM [mstDonorSuboutcome]  inner join mstDonorOutcome on mstDonorOutcome.[DOutcomeID]=[mstDonorSuboutcome].[DOutcomeID]    left join mstIndicatorDeatils on mstIndicatorDeatils.[OSID]= [mstDonorSuboutcome].[DOutcomeID] and [mstDonorSuboutcome].SubID=mstIndicatorDeatils.OSubID and mstIndicatorDeatils.OID=" + ViewState["DonorID"].ToString() + "   where   mstIndicatorDeatils.[OID] is null  and  mstIndicatorDeatils.OSubID is null");
                //GV_DynamicGrid.DataSource = dtCheck;
                //GV_DynamicGrid.DataBind();
                ViewState["dtselect"] = dtCheck;
            }
            else
            {
                if (Oid.Length > 0)
                {
                    DataTable dtCheck = objMain.LoadData(" SELECT 0 MainID, [mstDonorSuboutcome].[SoutComeID],OutcomeName,      [mstDonorSuboutcome].[DOutcomeID]      ,[SubID]    ,[SSubOutcomeName]  FROM [mstDonorSuboutcome]  inner join mstDonorOutcome on mstDonorOutcome.[DOutcomeID]=[mstDonorSuboutcome].[DOutcomeID] left join mstIndicatorDeatils on mstIndicatorDeatils.[OSID]= [mstDonorSuboutcome].[DOutcomeID] and [mstDonorSuboutcome].SubID=mstIndicatorDeatils.OSubID and mstIndicatorDeatils.OID=" + ViewState["DonorID"].ToString() + "   where [mstDonorSuboutcome].[DOutcomeID] in(" + Oid + ")  and  mstIndicatorDeatils.[OID] is null  and  mstIndicatorDeatils.OSubID is null ");
                    //GV_DynamicGrid.DataSource = dtCheck;
                    //GV_DynamicGrid.DataBind();
                    ViewState["dtselect"] = dtCheck;
                }
            }
        }
    }
    protected void txtdatefrom_TextChanged(object sender, EventArgs e)
    {
        if (txtFromDate.Text != "" && txtTodate.Text != "")
        {
            DateTime startDate = Convert.ToDateTime(txtFromDate.Text);
            DateTime endDate = Convert.ToDateTime(txtTodate.Text);
            if (endDate >= startDate)
            { }
            else
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Invalid Selection')</script>", false);
                txtFromDate.Text = "";
                txtTodate.Text = "";
                return;
            }
        }
    }
    protected void txtTodate_TextChanged(object sender, EventArgs e)
    {
        if (txtFromDate.Text != "" && txtTodate.Text != "")
        {
            DateTime startDate = Convert.ToDateTime(txtFromDate.Text);
            DateTime endDate = Convert.ToDateTime(txtTodate.Text);
            if (endDate >= startDate)
            { }
            else
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Invalid Selection')</script>", false);
                txtFromDate.Text = "";
                txtTodate.Text = "";
                return;
            }
        }
    }

    public void txtState_TextChanged(object sender, EventArgs e)
    {
        FillCBDist();
    }
    public void txtDist_TextChanged(object sender, EventArgs e)
    {
        FillCBBock();
    }
    public void LoadDataMain()
    {
        SqlParameter[] cmdParameters = new SqlParameter[]
		{
			new SqlParameter("@ffh",""),
            
            
		};
        DataTable dt = null;


        dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rptSafetySecurityReportLoad]", cmdParameters);


     //   DataTable dtCheck = objMain.LoadData("select SID as  DID, EmergencyName as DonorName,convert (varchar(10),[FromDate] ,105) as [FromDate], convert (varchar(10),todate ,105) as todate,ActiveStatus  FROM [mstSafetySecurity]    ");
        GVMain.DataSource = dt;
        GVMain.DataBind();
               
    }
    protected void UploadDist(object sender, EventArgs e)
    {
        FillCBBock();
    }
    public void FillCBBock()
    {
        
        string ddlDistrict = "";

        foreach (ListItem item in chkDistrict.Items)
        {
            if (item.Selected)
            {

                ddlDistrict += "'" + item.Value + "'" + ",";


            }
        }

        if (ddlDistrict.Length > 0)
        {
            ddlDistrict = ddlDistrict.Substring(0, ddlDistrict.LastIndexOf(","));
        }

     


        
     

         string   conditions = "DistrictCode in(" + ddlDistrict + ") ";
          string  ConAdmin = "AdminDistrictCode in(" + ddlDistrict + ") ";
       

            string strQry = "  SELECT BlockCode, dbo.TitleCase(upper(BlockName))  as BlockName FROM mst3Block where " + conditions + "  order by BlockName   ";
            DataTable dtDistrict = objMain.LoadData(strQry);
            // objComman.BindDLL("mst2District", "DistrictCode,dbo.TitleCase(upper(DistrictName)) as DistrictName ", conditions, "DistrictName", "asc", ddlDistrict, "DistrictName", "DistrictCode", "Select");

            chkBlock.DataSource = dtDistrict;
            chkBlock.DataTextField = "BlockName";
            chkBlock.DataValueField = "BlockCode";
            chkBlock.DataBind();

        



    }
    public void FillCBDist()
    {
        string ddlState = "";
        DataTable dtDistrict = null;
        foreach (ListItem item in ChkState.Items)
        {
            if (item.Selected)
            {

                ddlState += "'" + item.Value + "'" + ",";


            }
        }
        if (ddlState.Length > 0)
        {
            ddlState = ddlState.Substring(0, ddlState.LastIndexOf(","));
        }

      
          
           
                conditions = "StateCode in(" + ddlState + ") and mst2District.FYear ='" + Session["FinYear"].ToString() + "'";
                string strQry = "  SELECT DistrictCode, dbo.TitleCase(upper(DistrictName))  as DistrictName FROM mst2District where " + conditions + "  order by DistrictName   ";
                dtDistrict = objMain.LoadData(strQry);
           
        

        // objComman.BindDLL("mst2District", "DistrictCode,dbo.TitleCase(upper(DistrictName)) as DistrictName ", conditions, "DistrictName", "asc", ddlDistrict, "DistrictName", "DistrictCode", "Select");

        chkDistrict.DataSource = dtDistrict;
        chkDistrict.DataTextField = "DistrictName";
        chkDistrict.DataValueField = "DistrictCode";
        chkDistrict.DataBind();


    }
    public void FillCBState()
    {
        conditions = "";
        // objComman.BindDLL("mst1State", "StateCode,dbo.TitleCase(upper(StateName)) as StateName ", conditions, "StateName", "asc", ddlState, "StateName", "StateCode", "--Select--");


        //objComman.BindDLL("mst1State", "StateCode, dbo.TitleCase(upper(StateName)) as StateName", conditions, "StateName", "Desc", ddlState, "StateName", "StateCode", "--Select--");
        string strQry1 = "  SELECT StateCode, dbo.TitleCase(upper(StateName))  as StateName FROM mst1State   order by StateName   ";
        DataTable dtState = objMain.LoadData(strQry1);
        ChkState.DataSource = dtState;
        ChkState.DataTextField = "StateName";
        ChkState.DataValueField = "StateCode";
        ChkState.DataBind();

     

    }
    protected void rblDist_SelectedIndexChanged(object sender, EventArgs e)
    {
       
            FillCBDist();
            chkBlock.Items.Clear();
            txtMuhala1.Text = "";
            txtMuhala.Text = "";
       
    }
   
    protected void ddInGeography_SelectedIndexChanged(object sender, EventArgs e)
    {
        divState.Visible = false;
        divDist.Visible = false;
        divBlock.Visible = false;

        if (Convert.ToInt32(ddInGeography.SelectedValue) == 1)
        {
            divState.Visible = true;
            

        }
        
        if (Convert.ToInt32(ddInGeography.SelectedValue) == 2)
        {
            divState.Visible = true;
            divDist.Visible = true;
          
        }
        if (Convert.ToInt32(ddInGeography.SelectedValue) == 3)
        {
            divState.Visible = true;
            divDist.Visible = true;
            divBlock.Visible = true;
          
        }
    }
   
    protected void btnSave_Click(object sender, EventArgs e)
    {
        Save_Update(0);
    }
    private void Save_Update(int SchoolCode)
    {
        string Dist = "";
        foreach (ListItem item in chkDistrict.Items)
        {
            if (item.Selected)
            {

                Dist += "" + item.Value + "" + ",";


            }
        }
        string Block = "";
        foreach (ListItem item in chkBlock.Items)
        {
            if (item.Selected)
            {

                Block += "" + item.Value + "" + ",";


            }
        }

        string StateCode = "";
        foreach (ListItem item in ChkState.Items)
        {
            if (item.Selected)
            {

                StateCode += "" + item.Value + "" + ",";


            }
        }
        if (Convert.ToInt32(ddInGeography.SelectedValue) == 3)
        {
            if (Block.Length > 0)
            {

            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Select Block !! ')</script>", false);


                this.chkBlock.Focus();
                return;
            }

        }
        if (Convert.ToInt32(ddInGeography.SelectedValue) == 2)
        {
            if (Dist.Length > 0)
            {

            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Select District !! ')</script>", false);


                this.chkDistrict.Focus();
                return;
            }

        }
        if (Convert.ToInt32(ddInGeography.SelectedValue) == 1)
        {
            if (StateCode.Length > 0)
            {

            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Select State !! ')</script>", false);


                this.chkDistrict.Focus();
                return;
            }

        }

        if (StateCode.Length > 0)
        {
            StateCode = StateCode.Substring(0, StateCode.LastIndexOf(","));
        }
        if (Dist.Length > 0)
        {
            Dist = Dist.Substring(0, Dist.LastIndexOf(","));
        }

        if (Block.Length > 0)
        {
            Block = Block.Substring(0, Block.LastIndexOf(","));
        }

     
        
        int mainResult = 0;
       
      

        if (ViewState["Save"].ToString() == "Save")
        {
            DataTable dtCheck = objMain.LoadData(" SELECT * FROM [dbo].[mstSafetySecurity]  where ActiveStatus=1 ");


            if (dtCheck.Rows.Count > 0)
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please De-Active Another Emergency Exit')</script>", false);
                return;
            }

          
            
            ViewState["Save"] = "fff";



         
            //System.Drawing.Bitmap bmpPostedImage = new System.Drawing.Bitmap(FileuploadAttach.PostedFile.InputStream);
            //System.Drawing.Image objImage = ScaleImage(bmpPostedImage, 81);

            string INActiveDate;

            string ActivieDate = "";

            string FromDate="";

            string TOdate = "";


            if (txtFromDate.Text != "")
            {
                string FromDate1 = txtFromDate.Text;
                string[] b = FromDate1.Split('/');
                FromDate = b[2] + '-' + b[1] + '-' + b[0];

            }
            if (txtTodate.Text != "")
            {
                string TOdate1 = txtTodate.Text;
                string[] b = TOdate1.Split('/');
                TOdate = b[2] + '-' + b[1] + '-' + b[0];

            }

            mainResult = DonorProfile(0, txtDonorName.Text, Convert.ToDateTime(FromDate), Convert.ToDateTime(TOdate), Convert.ToInt32(ddInGeography.SelectedValue), Convert.ToInt32(ddlPeriod.SelectedValue), 1, Session["username"].ToString(), StateCode, Dist, Block);
            if (mainResult > 0)
            {
                ViewState["DonorID"] = mainResult;
                string Oid="";
                if (Convert.ToInt32(ddInGeography.SelectedValue) == 3)
                {
                    foreach (ListItem item in chkBlock.Items)
                    {
                        if (item.Selected)
                        {

                            Oid += "" + item.Value + "" + "";

                            string TSDInsertQuery = " INSERT INTO mstSafetySecurityDistrictProfile([SID],[BlockCode])Values('" + mainResult + "','" + item.Value + "')";
                            bool InsertTSD = objMain.AddUpdate(TSDInsertQuery);
                        }

                    }
                }
                string District="";
                if (Convert.ToInt32(ddInGeography.SelectedValue) == 2)
                {
                    foreach (ListItem item in chkDistrict.Items)
                    {
                        if (item.Selected)
                        {

                            District += "" + item.Value + "" + "";

                            string TSDInsertQuery = " INSERT INTO mstSafetySecurityDistrictProfile([SID],[DistrictCode])Values('" + mainResult + "','" + item.Value + "')";
                            bool InsertTSD = objMain.AddUpdate(TSDInsertQuery);
                        }
                    }
                }

                string State = "";
                if (Convert.ToInt32(ddInGeography.SelectedValue) ==1)
                {
                    foreach (ListItem item in ChkState.Items)
                    {
                        if (item.Selected)
                        {

                            State += "" + item.Value + "" + "";

                            string TSDInsertQuery = " INSERT INTO mstSafetySecurityDistrictProfile([SID],[StateCode])Values('" + mainResult + "','" + item.Value + "')";
                            bool InsertTSD = objMain.AddUpdate(TSDInsertQuery);
                        }
                    }
                }
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Saved sucessfully')</script>", false);
                ClearData();
                LoadDataMain();
                //txtIDNO.Text = TBCode;
            }
        }
        else
        {
          

          
            string FromDate="";

            string TOdate = "";


            if (txtFromDate.Text != "")
            {
                string FromDate1 = txtFromDate.Text;
                string[] b = FromDate1.Split('/');
                FromDate = b[2] + '-' + b[1] + '-' + b[0];

            }
            if (txtTodate.Text != "")
            {
                string TOdate1 = txtTodate.Text;
                string[] b = TOdate1.Split('/');
                TOdate = b[2] + '-' + b[1] + '-' + b[0];

            }

            //string deleteInsertQuery = " delete from mstSafetySecurityDistrictProfile where SID='" + ViewState["DonorID"].ToString() + "' ";
            //bool InsertDel = objMain.AddUpdate(deleteInsertQuery);

        
            //string Oid = "";
            //if (Convert.ToInt32(ddInGeography.SelectedValue) == 3)
            //{
            //    foreach (ListItem item in chkBlock.Items)
            //    {
            //        if (item.Selected)
            //        {

            //            Oid += "" + item.Value + "" + "";

            //            string TSDInsertQuery = " INSERT INTO mstSafetySecurityDistrictProfile([SID],[BlockCode])Values('" + ViewState["DonorID"].ToString() + "','" + item.Value + "')";
            //            bool InsertTSD = objMain.AddUpdate(TSDInsertQuery);
            //        }

            //    }
            //}
            //string District = "";
            //if (Convert.ToInt32(ddInGeography.SelectedValue) == 2)
            //{
            //    foreach (ListItem item in chkDistrict.Items)
            //    {
            //        if (item.Selected)
            //        {

            //            District += "" + item.Value + "" + "";

            //            string TSDInsertQuery = " INSERT INTO mstSafetySecurityDistrictProfile([SID],[DistrictCode])Values('" + ViewState["DonorID"].ToString() + "','" + item.Value + "')";
            //            bool InsertTSD = objMain.AddUpdate(TSDInsertQuery);
            //        }
            //    }
            //}

            //if (Convert.ToInt32(ddInGeography.SelectedValue) ==1)
            //{
            //    foreach (ListItem item in ChkState.Items)
            //    {
            //        if (item.Selected)
            //        {

            //            District += "" + item.Value + "" + "";

            //            string TSDInsertQuery = " INSERT INTO mstSafetySecurityDistrictProfile([SID],[StateCode])Values('" + ViewState["DonorID"].ToString() + "','" + item.Value + "')";
            //            bool InsertTSD = objMain.AddUpdate(TSDInsertQuery);
            //        }
            //    }
            //}
            mainResult = DonorProfile(Convert.ToInt32(ViewState["DonorID"].ToString()), txtDonorName.Text, Convert.ToDateTime(FromDate), Convert.ToDateTime(TOdate), Convert.ToInt32(ddInGeography.SelectedValue), Convert.ToInt32(ddlPeriod.SelectedValue), Convert.ToInt32(ddlStatus.SelectedValue), Session["username"].ToString(), txt_pbname.Text, txtMuhala.Text, txtMuhala1.Text);
            if (mainResult > 0)
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Saved sucessfully')</script>", false);
                ClearData();
                //txtIDNO.Text = TBCode;
            }
        }



    }
    public int DonorProfile(Int32 DID, string DonorName, DateTime FromDate, DateTime ToDate, Int32 GeographyID, Int32 DistrictType, int ActiveStatus, string createby,string State,string Dist,string Block)
    {
        SqlParameter[] cmdParameters = new SqlParameter[]
		{
			new SqlParameter("@ID", DID),
			new SqlParameter("@DonorName", DonorName),
            new SqlParameter("@FromDate", FromDate),
               new SqlParameter("@ToDate", ToDate),
			new SqlParameter("@GeographyID", GeographyID),
			new SqlParameter("@Period", DistrictType),
					new SqlParameter("@createby", createby),
            new SqlParameter("@State", State),
            new SqlParameter("@District", Dist),
            new SqlParameter("@Block", Block),
             new SqlParameter("@ActiveStatus", ActiveStatus),
            
         
		};
        Object Icount;

        Icount = SqlHelper.ExecuteScaler(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "InsertUpdateSafetySecurity", cmdParameters);
        return Convert.ToInt32(Icount);
    }
    public int DonorProfileUpdate(Int32 DID, string DonorName, DateTime FromDate, DateTime ToDate, Int32 GeographyID, Int32 DistrictType, Int32 FrequencyID, int QualitativeID, int AGPID, int PhaseID, int ActiveStatus, DateTime ActiveDate, DateTime DeActiveDate, string createby, string State, string Dist, string Block)
    {
        SqlParameter[] cmdParameters = new SqlParameter[]
		{
			new SqlParameter("@ID", DID),
			new SqlParameter("@DonorName", DonorName),
            new SqlParameter("@FromDate", FromDate),
               new SqlParameter("@ToDate", ToDate),
			new SqlParameter("@GeographyID", GeographyID),
			new SqlParameter("@DistrictType", DistrictType),
			new SqlParameter("@FrequencyID", FrequencyID),
			new SqlParameter("@QualitativeID", QualitativeID),
			new SqlParameter("@AGPID", AGPID),
			new SqlParameter("@PhaseID", PhaseID),
			new SqlParameter("@ActiveStatus", ActiveStatus),
			new SqlParameter("@ActiveDate", ActiveDate),
			new SqlParameter("@DeActiveDate", DeActiveDate),
			
			
			new SqlParameter("@createby", createby),
               new SqlParameter("@State", State),
            new SqlParameter("@District", Dist),
            new SqlParameter("@Block", Block),
         
		};
       

      return  SqlHelper.ExecuteNonQuery(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "InserUpdateDonor", cmdParameters);
       
    }
    protected void btnReprot_Click(object sender, EventArgs e)
    {
        SqlParameter[] cmdParameters = new SqlParameter[]
		{
			new SqlParameter("@ffh",""),
            
            
		};
        DataTable dt = null;


        dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rptSafetySecurityReport]", cmdParameters);

        if (dt.Rows.Count > 0)
        {
            ExporttoExcel(dt, "SafetySecurityMaster");
        }
        
    }
    private void ExporttoExcel(DataTable table, string FileName)
    {
        try
        {

       
        if (table != null)
        {
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.ClearContent();
            HttpContext.Current.Response.ClearHeaders();
            HttpContext.Current.Response.Buffer = true;
            HttpContext.Current.Response.ContentType = "application/ms-excel";
            HttpContext.Current.Response.Write(@"<!DOCTYPE HTML PUBLIC ""-//W3C//DTD HTML 4.0 Transitional//EN"">");
            string Fullfilename = "" + FileName + "_" + DateTime.Now.ToString("dd_MM_yyyy_hh_mm_ss") + ".xls";

            HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=" + Fullfilename + " ");

            HttpContext.Current.Response.Charset = "utf-8";
            HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("windows-1250");
            //sets font
            HttpContext.Current.Response.Write("<font style='font-size:10.0pt; font-family:Calibri;'>");
            HttpContext.Current.Response.Write("<BR><BR><BR>");
            //sets the table border, cell spacing, border color, font of the text, background, foreground, font height
            HttpContext.Current.Response.Write("<Table border='1' bgColor='#ffffff' " +
              "borderColor='#000000' cellSpacing='0' cellPadding='0' " +
              "style='font-size:10.0pt; font-family:Calibri; background:white;'> <TR>");
            //am getting my grid's column headers
            int columnscount = table.Columns.Count;


            for (int j = 0; j < columnscount; j++)
            {      //write in new column
                HttpContext.Current.Response.Write("<Td>");
                //Get column headers  and make it as bold in excel columns
                HttpContext.Current.Response.Write("<B>");
                HttpContext.Current.Response.Write(table.Columns[j]);
                HttpContext.Current.Response.Write("</B>");
                HttpContext.Current.Response.Write("</Td>");
            }
            HttpContext.Current.Response.Write("</TR>");
            foreach (DataRow row in table.Rows)
            {//write in new row
                HttpContext.Current.Response.Write("<TR>");
                for (int i = 0; i < table.Columns.Count; i++)
                {
                    HttpContext.Current.Response.Write("<Td>");
                    HttpContext.Current.Response.Write(row[i].ToString());
                    HttpContext.Current.Response.Write("</Td>");
                }

                HttpContext.Current.Response.Write("</TR>");
            }
            HttpContext.Current.Response.Write("</Table>");
            HttpContext.Current.Response.Write("</font>");
            HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.End();
        }
        }
        catch (Exception ex)
        {

            throw;
        }
    }
}