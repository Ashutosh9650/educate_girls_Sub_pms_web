using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class FrmAnnualPlan_Agp : System.Web.UI.Page
{
    clsMain objMain = new clsMain();
    Comman objComman = new Comman();
    string conditions = "";
    DataTable dtSearchVill = null;
    DataTable dtGKPPlan = null;
    public string RowNo = "", SchoolLeavel = "", BalSacha = "", GKP = "";
    public bool vADD = false;
    public bool vVerify = false;
    public bool vDelete = false;
    public bool vPhase = false;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Convert.ToString(Session["username"]) != "")
            {
                LoadYear();
                LoadUserLeavel();
              //  LoadGKPDetails();
            }
            else
            {
                Response.Redirect("Login.aspx", false);
            }

        }
    }
    public void LoadGKPDetails()
    {
        string strQry = "Select * from mstGKPPlan ";
         dtGKPPlan = objMain.LoadData(strQry);
         Session["dtGKPPlan"] = dtGKPPlan;
    }
    public void Locking()
    {
        if (ddlYear.SelectedIndex > 0)
        {

            btnDelete.Enabled = true;
            btnsave.Enabled = true;
            string strQry;
            if (Convert.ToInt32(ddlType.SelectedValue) == 1)
            {
                strQry = "Select * from mstModuleLocking  where [FromName]='Agp Annual Plan District Wise' and DistrictCode='" + ddlDistrict.SelectedValue + "' and Fyear='" + ddlYear.SelectedItem.Text + "' ";
                #region  District Wise


                string Year = ddlYear.SelectedItem.Text;
                string[] Year1 = Year.Split('-');



                DateTime date1;
                DateTime date2;
                DataTable dtModel = objMain.LoadData(strQry);
                if (dtModel.Rows.Count > 0)
                {

                    date1 = Convert.ToDateTime(dtModel.Rows[0]["lockdate"].ToString());
                    date2 = DateTime.Now.Date;
                    
                    if (date2>date1)
                    {
                        btnDelete.Enabled = false;
                        btnsave.Enabled = false;
                     }

                }
                #endregion

            }
            if (Convert.ToInt32(ddlType.SelectedValue) == 2)
            {
                strQry = "Select * from mstModuleLocking  where [FromName]='Agp Annual Plan Village Wise' and DistrictCode='" + ddlDistrict.SelectedValue + "' and Fyear='" + ddlYear.SelectedItem.Text + "' ";
                #region  Village Wise


                string Year = ddlYear.SelectedItem.Text;
                string[] Year1 = Year.Split('-');



                DateTime date1;
                DateTime date2;
                DataTable dtModel = objMain.LoadData(strQry);
                if (dtModel.Rows.Count > 0)
                {


                    date1 = Convert.ToDateTime(dtModel.Rows[0]["lockdate"].ToString());
                    date2 = DateTime.Now.Date;


                    if (date2>date1)
                    
                    {
                        btnDelete.Enabled = false;
                        btnsave.Enabled = false;

                    }
                }
                #endregion

            }
            if (Convert.ToInt32(ddlType.SelectedValue) == 3)
            {
                strQry = "Select * from mstModuleLocking  where [FromName]='Agp Annual Plan School Wise' and DistrictCode='" + ddlDistrict.SelectedValue + "' and Fyear='" + ddlYear.SelectedItem.Text + "' ";
                #region  School Wise


                string Year = ddlYear.SelectedItem.Text;
                string[] Year1 = Year.Split('-');



                DateTime date1;
                DateTime date2;
                DataTable dtModel = objMain.LoadData(strQry);
                if (dtModel.Rows.Count > 0)
                {

                    date1 = Convert.ToDateTime(dtModel.Rows[0]["lockdate"].ToString());
                    date2 = DateTime.Now.Date;

                   
                    if (date2 > date1)
                    {
                        btnDelete.Enabled = false;
                        btnsave.Enabled = false;

                    }
                }
                #endregion

            }
            string strQry1 = "  SELECT * FROM [mst2District] where rPhase=3   and  Fyear='" + ddlYear.SelectedItem.Text + "' and DistrictCode='" + ddlDistrict.SelectedValue + "'  ";
            DataTable dtPhage = objMain.LoadData(strQry1);
            if (dtPhage.Rows.Count > 0)
            {
                vPhase = true;
                ViewState["vPhase"] = "1";
            }
            else
            {
                ViewState["vPhase"] = "2";
            }
        }
    }
    public void UserLevelFilter()
    {

        string strQry = "";
        string Cond = "Module='Annual Plan Entry'";
        strQry = "Select * from MstUserRight  where " + Cond + " and Role_Id=" + Session["user_level"].ToString() + "   ";


        DataTable dtRole = objMain.LoadData(strQry);

        if (dtRole.Rows.Count > 0)
        {
            vADD = Convert.ToBoolean(dtRole.Rows[0]["AddStatus"].ToString());
            vVerify = Convert.ToBoolean(dtRole.Rows[0]["verify_Status"].ToString());
            vDelete = Convert.ToBoolean(dtRole.Rows[0]["Delete_status"].ToString());


        }
        if (vDelete == true)
        {

            btnDelete.Visible = true;
        }
        else
        {

            btnDelete.Visible = false;
        }

        if (vADD == true)
        {

            //  btnsave.Enabled = true;

        }
        else
        {
            //  btnAdd.Enabled = false;

        }
        if (vVerify == true)
        {

            btnsave.Enabled = true;


        }
        if (vVerify == true || vADD == true)
        {
            btnsave.Enabled = true;

        }
        else
        {
            btnsave.Enabled = false;

        }

    }
    #region Fill Method
    public void LoadUserLeavel()
    {
        conditions = "";
        if (Session["user_level_Role"].ToString() == "1")
        {
            //conditions = "StateCode='" + Session["StateCode"].ToString() + "' ";
            objComman.BindDLL("mst1State", "StateCode, dbo.TitleCase(upper(StateName)) as StateName", " Statecode in(select distinct statecode from mst5VillageAgp where AGPStatus=1)", "StateName", "Desc", ddlState, "StateName", "StateCode", "--Select--");

        }
        else if (Session["user_level_Role"].ToString() == "2")
        {
            conditions = "StateCode='" + Session["StateCode"].ToString() + "' ";
            objComman.BindDLL("mst1State", "StateCode, dbo.TitleCase(upper(StateName)) as StateName", "Statecode in(select distinct statecode from mst5VillageAgp where AGPStatus=1)", "StateName", "Desc", ddlState, "StateName", "StateCode", "--Select--");

            ddlState.SelectedIndex = 1;
        }
        else
        {
            conditions = "StateCode='" + Session["StateCode"].ToString() + "' ";
            objComman.BindDLL("mst1State", "StateCode, dbo.TitleCase(upper(StateName)) as StateName", "Statecode in(select distinct statecode from mst5VillageAgp where AGPStatus=1)", "StateName", "Desc", ddlState, "StateName", "StateCode", "--Select--");

            ddlState.SelectedIndex = 1;
        }


        if (Session["user_level_Role"].ToString() == "1")
        {
        }
        else if (Session["user_level_Role"].ToString() == "2")
        {
            conditions = "";
            conditions = "StateCode ='" + ddlState.SelectedValue + "' and Fyear= '" + ddlYear.SelectedItem.Text + "' ";
            objComman.BindDLL("mst2DistrictAgp", "DistrictCode,dbo.TitleCase(upper(DistrictName)) as DistrictName", "DistrictCode in(select distinct DistrictCode from mst5VillageAgp where AGPStatus=1)", "DistrictName", "asc", ddlDistrict, "DistrictName", "DistrictCode", "--Select--");

            ddlDistrict.SelectedIndex = 0;

            //ddlDistrict.SelectedIndex = 1;
            //conditions = "";
            //conditions = "DistrictCode ='" + ddlDistrict.SelectedValue + "' and BlockCode ='" + Session["BlockCode"].ToString() + "' ";
            //objComman.BindDLL("mst3BlockAgp", "BlockCode,dbo.TitleCase(upper(BlockName)) as BlockName  ", conditions, "BlockName", "asc", ddlBlock, "BlockName", "BlockCode", "--Select--");

        }

        else
        {


            conditions = "";
            //conditions = "StateCode ='" + ddlState.SelectedValue + "'  and Fyear= '2019-2020' ";

            conditions = "StateCode ='" + ddlState.SelectedValue + "' and DistrictCode in(" + Session["DistrictCode"].ToString() + ") and DistrictCode in(select distinct DistrictCode from mst5VillageAgp where AGPStatus=1) and Fyear= '" + ddlYear.SelectedItem.Text + "' ";
            objComman.BindDLL("mst2DistrictAgp", "DistrictCode,dbo.TitleCase(upper(DistrictName)) as DistrictName", conditions, "DistrictName", "asc", ddlDistrict, "DistrictName", "DistrictCode", "--Select--");

            string strQry;
            strQry = "Select * from mst2DistrictAgp where   DistrictCode in(" + Session["DistrictCode"].ToString() + ")";
            DataTable dtcountCheck = objMain.LoadData(strQry);
            if (dtcountCheck.Rows.Count > 0)
            {
                if (dtcountCheck.Rows.Count == 1)
                {
                    ddlYear.Enabled = false;
                }
                else
                {
                    ddlYear.Enabled = true;
                }
            }
            else
            {
                ddlYear.Enabled = true;
            }
            ddlDistrict.SelectedIndex = 1;
            ddlDistrict_SelectedIndexChanged(ddlDistrict, null);
            //ddlDistrict.SelectedIndex = 1;
            //conditions = "";
            //conditions = "DistrictCode ='" + ddlDistrict.SelectedValue + "' and BlockCode ='" + Session["BlockCode"].ToString() + "' ";
            //objComman.BindDLL("mst3BlockAgp", "BlockCode,dbo.TitleCase(upper(BlockName)) as BlockName  ", conditions, "BlockName", "asc", ddlBlock, "BlockName", "BlockCode", "--Select--");

        }





    }
    public void LoadYear()
    {
        DateTime GivenDate = DateTime.Now;
        int GivenYear = GivenDate.Year;
        int m = GivenDate.Month;

        DataTable dt = null;
        //ddlYear.Items.Add("--Select--","0");
        int y = GivenDate.Year;


        DateTime GivenDate1 = DateTime.Now;
        int GivenYear1 = GivenDate1.Year;
        DataTable dtYear = CreateDataTable();
        DataRow dr;
        if (ddlYear.SelectedIndex < 0)
        {

            string mYear1 = GivenYear1.ToString();
            for (int j = 0; j < 1; j++)
            {
                if (m > 3)
                {
                    dr = dtYear.NewRow();
                    dr["Type"] = GivenYear.ToString() + "-" + Convert.ToString((GivenYear + 1));
                    dr["ID"] = y;
                    dtYear.Rows.Add(dr);
                    dr = dtYear.NewRow();
                    dr["Type"] = GivenYear - 1 + "-" + Convert.ToString((GivenYear - 1 + 1));
                    dr["ID"] = y - 1;
                    dtYear.Rows.Add(dr);
                    //get last  two digits (eg: 10 from 2010);

                }
                else
                {

                    Int32 m7 = y + 1;
                    dr = dtYear.NewRow();
                    dr["Type"] = Convert.ToString((y)) + "-" + m7.ToString();
                    //y = y - 1;
                    dr["ID"] = y;
                    dtYear.Rows.Add(dr);
                    dr = dtYear.NewRow();
                    dr["Type"] = Convert.ToString((y - 1)) + "-" + y.ToString();
                    //y = y - 1;
                    dr["ID"] = y - 1;

                    dtYear.Rows.Add(dr);


                }

            }

        }
        //DataTable dtYear = objComman.Generate_Financial_Year();

        objComman.BindDLLMasterTable("mstSchool", "Type,ID", dtYear, conditions, "Type", "asc", ddlYear, "Type", "ID", "Select");

        ddlYear.SelectedIndex = 1;
        //DateTime GivenDate = DateTime.Now;
        //int GivenYear = GivenDate.Year;
        //int m = GivenDate.Month;

        //DataTable dt = null;
        ////ddlYear.Items.Add("--Select--","0");
        //int y = GivenDate.Year;


        //DateTime GivenDate1 = DateTime.Now;
        //int GivenYear1 = GivenDate1.Year;
        //DataTable dtYear = CreateDataTable();
        //DataRow dr;
        //if (ddlYear.SelectedIndex < 0)
        //{

        //    string mYear1 = GivenYear1.ToString();
        //    for (int j = 0; j < 1; j++)
        //    {

        //        if (m > 3)
        //        {
        //            dr = dtYear.NewRow();
        //            dr["Type"] = GivenYear.ToString() + "-" + Convert.ToString((GivenYear + 1));
        //            dr["ID"] = y;
        //            dtYear.Rows.Add(dr);
        //            dr = dtYear.NewRow();
        //            dr["Type"] = GivenYear - 1 + "-" + Convert.ToString((GivenYear - 1 + 1));
        //            dr["ID"] = y - 1;
        //            dtYear.Rows.Add(dr);

        //            dr = dtYear.NewRow();
        //            dr["Type"] = GivenYear - 2 + "-" + Convert.ToString((GivenYear - 2 + 1));
        //            dr["ID"] = y - 2;
        //            dtYear.Rows.Add(dr);
        //            //get last  two digits (eg: 10 from 2010);

        //        }
        //        else
        //        {

        //            Int32 m7 = y + 1;
        //            dr = dtYear.NewRow();
        //            dr["Type"] = Convert.ToString((y)) + "-" + m7.ToString();
        //            //y = y - 1;
        //            dr["ID"] = y;
        //            dtYear.Rows.Add(dr);


        //            dr = dtYear.NewRow();
        //            dr["Type"] = Convert.ToString((y - 1)) + "-" + y.ToString();
        //            //y = y - 1;
        //            dr["ID"] = y - 1;

        //            dtYear.Rows.Add(dr);

        //            dr = dtYear.NewRow();
        //            dr["Type"] = GivenYear - 2 + "-" + Convert.ToString((GivenYear - 2 + 1));
        //            dr["ID"] = y - 2;
        //            dtYear.Rows.Add(dr);
        //        }


        //    }

        //}
        //DataTable dtYear = objComman.Generate_Financial_Year();

        //objComman.BindDLLMasterTable("mstSchool", "Type,ID", dtYear, conditions, "Type", "asc", ddlYear, "Type", "ID", "Select");

        //ddlYear.SelectedIndex = 1;
        //}


    }

    public DataTable CreateDataTable()
    {

        DataTable dtYear = new DataTable();
        dtYear.Columns.Add("Type", System.Type.GetType("System.String"));

        dtYear.Columns.Add("ID", System.Type.GetType("System.Int32"));
        return dtYear;
    }
    public void FillCBState()
    {
        conditions = "statecode in(select distinct statecode from mst5VillageAgp where AGPStatus=1)";
        objComman.BindDLL("mst1State", "StateCode,dbo.TitleCase(upper(StateName)) as StateName ", conditions, "StateName", "asc", ddlState, "StateName", "StateCode", "--Select--");
    }
    public void FillCBDist()
    {

        conditions = "";
        if (Session["user_level_Role"].ToString() == "1")
        {

            conditions = "StateCode ='" + ddlState.SelectedValue + "' and mst2DistrictAgp.FYear ='" + ddlYear.SelectedItem.Text + "'";
        }
        else if (Session["user_level_Role"].ToString() == "2")
        {
            conditions = "StateCode ='" + ddlState.SelectedValue + "' and  mst2DistrictAgp.FYear ='" + ddlYear.SelectedItem.Text + "'";
        }
        //else
        //{
        //    conditions = "StateCode ='" + ddlState.SelectedValue + "' and DistrictCode in(" + Session["DistrictCodeNew"].ToString() + ") and mst2DistrictAgp.FYear ='" + ddlYear.SelectedItem.Text + "' ";


        //}
        else
        {
            conditions = "StateCode ='" + ddlState.SelectedValue + "' and mst2DistrictAgp.FYear  ='" + ddlYear.SelectedItem.Text + "'";


        }
        conditions = conditions + " and DistrictCode in(select distinct DistrictCode from mst5VillageAgp where AGPStatus=1)";
        objComman.BindDLL("mst2DistrictAgp", "DistrictCode,dbo.TitleCase(upper(DistrictName)) as DistrictName ", conditions, "DistrictName", "asc", ddlDistrict, "DistrictName", "DistrictCode", "Select");



    }
    public void FillCBBock()
    {
        conditions = " ";
        if (Session["user_level_Role"].ToString() == "1")
        {

            conditions = "DistrictCode ='" + ddlDistrict.SelectedValue + "'";
        }
        if (Session["user_level"].ToString() == "19")
        {
            conditions = "DistrictCode ='" + ddlDistrict.SelectedValue + "'and BlockCode in(" + Session["DistrictCodeNew"].ToString() + ")";
        }
        else
        {
            conditions = "DistrictCode ='" + ddlDistrict.SelectedValue + "' ";
        }


        objComman.BindDLL("mst3BlockAgp", "BlockCode,dbo.TitleCase(upper(BlockName)) as BlockName ", conditions, "BlockName", "asc", ddlBlock, "BlockName", "BlockCode", "--Select--");



    }
    public void FillCBCluster()
    {
        conditions = "";
        conditions = "DistrictCode ='" + ddlDistrict.SelectedValue + "'  and BlockCode ='" + ddlBlock.SelectedValue + "' and  PanchayatCode in(select distinct PanchayatCode from mst5VillageAgp where AGPStatus=1)";
        objComman.BindDLLSelectAll("mstPanchayatAgp", "PanchayatCode,dbo.TitleCase(upper(PanchayatName)) as PanchayatName", conditions, "PanchayatName", "asc", ddlPanchayat, "PanchayatName", "PanchayatCode", "--Select--");



    }
    public void FillCVillage()
    {
        conditions = "";
        //conditions = "DistrictCode ='" + ddlDistrict.SelectedValue + "'  and BlockCode ='" + ddlBlock.SelectedValue + "' and  PanchayatCode='" + ddlPanchayat.SelectedValue + "'  ";
        //objComman.BindDLL("mst5Village", "VillageCode,dbo.TitleCase(upper(VillageName)) as VillageName", conditions, "VillageName", "asc", ddlVillage, "VillageName", "VillageCode", "--Select--");

        //if (ddlPanchayat.SelectedValue.ToString() == "1")
        //{
        //    conditions = "mst5VillageAgp.DistrictCode ='" + ddlDistrict.SelectedValue + "'  and mst5VillageAgp.BlockCode ='" + ddlBlock.SelectedValue + "'  ";

        //}
        //else
        //{
        //    conditions = "mst5VillageAgp.DistrictCode ='" + ddlDistrict.SelectedValue + "'  and mst5VillageAgp.BlockCode ='" + ddlBlock.SelectedValue + "' and  mst5VillageAgp.PanchayatCode='" + ddlPanchayat.SelectedValue + "'  ";

        //}

        //string strQry = "  SELECT mst5VillageAgp.VillageCode, dbo.TitleCase(upper((mst5VillageAgp.VillageName))) + ' (' + dbo.TitleCase(upper(mstPanchayat.PanchayatName)) +')'   as VillageName FROM mst5Village INNER JOIN mstPanchayat ON mst5VillageAgp.PanchayatCode = mstPanchayat.PanchayatCode where " + conditions + "  order by VillageName   ";
        //DataTable dtVillage = objMain.LoadData(strQry);

        //objComman.BindDLLMasterTableVillage("mst5Village", "VillageName,VillageCode", dtVillage, conditions, "VillageName", "asc", ddlVillage, "VillageName", "VillageCode", "Select");



    }
    public void Bindgrid()
    {
        string str = string.Empty;
        if (ddlState.SelectedValue != null && ddlState.SelectedIndex > 0)
        {
            str = "where mst5VillageAgp.StateCode='" + ddlState.SelectedValue.ToString() + "'";
        }
        if (ddlDistrict.SelectedValue != null && ddlDistrict.SelectedIndex > 0)
        {
            str = str + "and mst5VillageAgp.DistrictCode='" + ddlDistrict.SelectedValue.ToString() + "'";
        }

        if (ddlBlock.SelectedValue != null && ddlBlock.SelectedIndex > 0)
        {
            str = str + "and mst5VillageAgp.BlockCode='" + ddlBlock.SelectedValue.ToString() + "'";
        }

        if (ddlPanchayat.SelectedValue != null && ddlPanchayat.SelectedIndex > 1)
        {
            str = str + "and mst5VillageAgp.PanchayatCode='" + ddlPanchayat.SelectedValue.ToString() + "'";
        }

        if (ddlVillage.SelectedValue != null && ddlVillage.SelectedIndex > 0)
        {
            str = str + "and mst5VillageAgp.VillageCode='" + ddlVillage.SelectedValue.ToString() + "'";
        }
        string strQry = "";
        if (ddlType.SelectedValue == "2")
        {

            GVMain.Columns[1].Visible = false;
            strQry = "select  VillageName +' ('+ EGVillagecode +')' as VillageName, Villagecode,'' SchoolName,'' as DISECode ,'' RowNo, '' SchoolLevel,'' BAlVal,'' GKP,'' GKPLevel,'' as ManagementType FROM mst5VillageAgp " + str + " and AGPStatus=1";
        }
       
        DataTable dtSchool = objComman.LoadData(strQry);
        if (dtSchool.Rows.Count > 0)
        {

            GVMain.DataSource = dtSchool;
            GVMain.DataBind();
            GV_AnnualPlan.DataSource = null;
            GV_AnnualPlan.DataBind();
        }
        else
        {
            GVMain.DataSource = null;
            GVMain.DataBind();
        }
    }
    public void FillControls()
    {
    }
    public void LoadData()
    {
        string strQry = "";
        string Condtion = "";
        Int32 iCount = 0;
        Condtion = "where  mst5VillageAgp.StateCode='" + ddlState.SelectedValue.ToString() + "'";
        if (ddlBlock.SelectedValue != null && ddlBlock.SelectedIndex > 0)
        {
            Condtion = Condtion + " and mst5VillageAgp.BlockCode='" + ddlBlock.SelectedValue.ToString() + "'";
        }

        if (ddlDistrict.SelectedValue != null && ddlDistrict.SelectedIndex >= 0)
        {
            Condtion = Condtion + " and mst5VillageAgp.DistrictCode='" + ddlDistrict.SelectedValue.ToString() + "'";
        }

        if (ddlVillage.SelectedValue != null && ddlVillage.SelectedIndex > 0)
        {
            Condtion = Condtion + " and mst5VillageAgp.VillageCode='" + ddlVillage.SelectedValue.ToString() + "'";
        }
        //if (ddlType.SelectedValue == "2")
        //{
        //    strQry = " select Description,RowNo as LookupCode,  [Apr], [May], [Jun], [Jul], [Aug], [Sep], [Oct], [Nov], [Dec], [Jan], [Feb], [Mar],[RowNo] from tblAnualPlanDataDetailAgp where VillageCode='" + ViewState["VillageCode"].ToString() + "' and PlanType=2 order by RowNo ";
        //}
        //else if (ddlType.SelectedValue == "3")
        //{

        //    strQry = " select Description,RowNo as LookupCode,  [Apr], [May], [Jun], [Jul], [Aug], [Sep], [Oct], [Nov], [Dec], [Jan], [Feb], [Mar],[RowNo] from tblAnualPlanDataDetailAgp where SchoolCode='" + ViewState["SchoolId"].ToString() + "' and PlanType=3 order by RowNo ";
        //}

        DataTable dtPreLoad;
        //if (dtPreLoad.Rows.Count > 0)
        //{
         if (ddlType.SelectedValue == "2")
            {
                string SubType = "";
             
                string strQry4 = " select mstLookupAnnaulPlanAgp.Description,mstLookupAnnaulPlanAgp.LookupCode,  [Apr], [May], [Jun], [Jul], [Aug], [Sep], [Oct], [Nov], [Dec], [Jan], [Feb], [Mar],SysFlag,StartMonth,EndMonth,MaxVal,mstLookupAnnaulPlanAgp.LookupType,PhageFlag from mstLookupAnnaulPlanAgp   left join (select *  from tblAnualPlanDataDetailAgp where Villagecode='" + Convert.ToString(ViewState["VillageCode"]) + "' and PlanType=2 )   as tblAnualPlanDataDetailAgp on mstLookupAnnaulPlanAgp.LookUpcode =tblAnualPlanDataDetailAgp.RowNo where LookupFlag='APLV'  " + SubType + "  order by seqno ";

                dtSearchVill = objComman.LoadData(strQry4);
            }

         if (ddlType.SelectedValue == "3")
         {
             string SubType = "";
            

             string strQry4 = " select mstLookupAnnaulPlanAgp.Description,mstLookupAnnaulPlanAgp.LookupCode,  [Apr], [May], [Jun], [Jul], [Aug], [Sep], [Oct], [Nov], [Dec], [Jan], [Feb], [Mar],SysFlag,StartMonth,EndMonth,MaxVal,mstLookupAnnaulPlanAgp.LookupType,PhageFlag from mstLookupAnnaulPlanAgp   left join (select *  from tblAnualPlanDataDetailAgp where schoolcode='" + Convert.ToString( ViewState["SchoolId"]) + "' and PlanType=3 )   as tblAnualPlanDataDetailAgp on mstLookupAnnaulPlanAgp.LookUpcode =tblAnualPlanDataDetailAgp.RowNo where LookupFlag='APLS'  " + SubType + "  order by seqno ";

             dtSearchVill = objComman.LoadData(strQry4);
         }
        
        if (dtSearchVill.Rows.Count > 0)
        {
            GV_AnnualPlan.DataSource = dtSearchVill;
            GV_AnnualPlan.DataBind();
        }
        Session["dtSearchVill"] = dtSearchVill;


        TraingGPp();
        TraingGPpMeeting();
       

    }
    public void TraingGPp()
    {
        DataTable dt = Session["dtSearchVill"] as DataTable;
        Int32 Apr = 0;
        Int32 May = 0;
        Int32 Jun = 0;

        Int32 Jul = 0;
        Int32 Aug = 0;
        Int32 Sep = 0;
        Int32 Oct = 0;
        Int32 Nov = 0;
        Int32 Dec = 0;
        Int32 Jan = 0;
        Int32 Feb = 0;
        Int32 Mar = 0;

        for (int i = 0; i < GV_AnnualPlan.Rows.Count; i++)
        {


            TextBox TxtApr = (TextBox)GV_AnnualPlan.Rows[i].FindControl("TxtApr");
            TextBox TxtMay = (TextBox)GV_AnnualPlan.Rows[i].FindControl("TxtMay");
            TextBox TxtJun = (TextBox)GV_AnnualPlan.Rows[i].FindControl("TxtJun");
            TextBox TxtJul = (TextBox)GV_AnnualPlan.Rows[i].FindControl("TxtJul");
            TextBox TxtAug = (TextBox)GV_AnnualPlan.Rows[i].FindControl("TxtAug");
            TextBox TxtSep = (TextBox)GV_AnnualPlan.Rows[i].FindControl("TxtSep");
            TextBox TxtOct = (TextBox)GV_AnnualPlan.Rows[i].FindControl("TxtOct");
            TextBox TxtNov = (TextBox)GV_AnnualPlan.Rows[i].FindControl("TxtNov");
            TextBox TxtDec = (TextBox)GV_AnnualPlan.Rows[i].FindControl("TxtDec");
            TextBox TxtJan = (TextBox)GV_AnnualPlan.Rows[i].FindControl("TxtJan");
            TextBox TxtFeb = (TextBox)GV_AnnualPlan.Rows[i].FindControl("TxtFeb");
            TextBox TxtMar = (TextBox)GV_AnnualPlan.Rows[i].FindControl("TxtMar");
           
            if (dt.Rows[i]["Description"].ToString() == "Advisory Council Members Identification")
            {

                
                if (TxtApr.Text != "")
                {
                    Apr = Convert.ToInt32(TxtApr.Text);
                }
                if (TxtMay.Text != "")
                {
                    May = Convert.ToInt32(TxtMay.Text);
                }
              
               
                if (TxtJun.Text != "")
                {
                    Jun = Convert.ToInt32(TxtJun.Text);
                }
                if (TxtJul.Text != "")
                {
                    Jul = Convert.ToInt32(TxtJul.Text);
                }
                if (TxtAug.Text != "")
                {
                    Aug = Convert.ToInt32(TxtAug.Text);
                }
                if (TxtSep.Text != "")
                {

                    Sep = Convert.ToInt32(TxtSep.Text);
                }
                if (TxtOct.Text != "")
                {

                    Oct = Convert.ToInt32(TxtOct.Text);
                }
                if (TxtNov.Text != "")
                {
                    Nov = Convert.ToInt32(TxtNov.Text);
                }
                if (TxtDec.Text != "")
                {
                    Dec = Convert.ToInt32(TxtDec.Text);
                }
                if (TxtJan.Text != "")
                {
                    Jan = Convert.ToInt32(TxtJan.Text);
                }
                if (TxtFeb.Text != "")
                {
                    Feb = Convert.ToInt32(TxtFeb.Text);
                }
                if (TxtMar.Text != "")
                {
                    Mar = Convert.ToInt32(TxtMar.Text);
                }

                if (Apr > 0)
                {
                    break;
                }
                if (May > 0)
                {
                    break;
                }
                if (Jun > 0)
                {
                    break;
                }
                if (Jul > 0)
                {
                    break;
                }
                if (Aug > 0)
                {
                    break;
                }
                if (Sep > 0)
                {
                    break;
                }
                if (Oct > 0)
                {
                    break;
                }
                if (Dec > 0)
                {
                    break;
                }
                if (Jan > 0)
                {
                    break;
                }
                if (Feb > 0)
                {
                    break;
                }
                if (Mar > 0)
                {
                    break;
                }
            }


        }
        SIPtd(Apr, May, Jun, Jul, Aug, Sep, Oct, Nov, Dec, Jan, Feb, Mar);
    }

    public void TraingGPpMeeting()
    {
        DataTable dt = Session["dtSearchVill"] as DataTable;
        Int32 Apr = 0;
        Int32 May = 0;
        Int32 Jun = 0;

        Int32 Jul = 0;
        Int32 Aug = 0;
        Int32 Sep = 0;
        Int32 Oct = 0;
        Int32 Nov = 0;
        Int32 Dec = 0;
        Int32 Jan = 0;
        Int32 Feb = 0;
        Int32 Mar = 0;

        for (int i = 0; i < GV_AnnualPlan.Rows.Count; i++)
        {


            TextBox TxtApr = (TextBox)GV_AnnualPlan.Rows[i].FindControl("TxtApr");
            TextBox TxtMay = (TextBox)GV_AnnualPlan.Rows[i].FindControl("TxtMay");
            TextBox TxtJun = (TextBox)GV_AnnualPlan.Rows[i].FindControl("TxtJun");
            TextBox TxtJul = (TextBox)GV_AnnualPlan.Rows[i].FindControl("TxtJul");
            TextBox TxtAug = (TextBox)GV_AnnualPlan.Rows[i].FindControl("TxtAug");
            TextBox TxtSep = (TextBox)GV_AnnualPlan.Rows[i].FindControl("TxtSep");
            TextBox TxtOct = (TextBox)GV_AnnualPlan.Rows[i].FindControl("TxtOct");
            TextBox TxtNov = (TextBox)GV_AnnualPlan.Rows[i].FindControl("TxtNov");
            TextBox TxtDec = (TextBox)GV_AnnualPlan.Rows[i].FindControl("TxtDec");
            TextBox TxtJan = (TextBox)GV_AnnualPlan.Rows[i].FindControl("TxtJan");
            TextBox TxtFeb = (TextBox)GV_AnnualPlan.Rows[i].FindControl("TxtFeb");
            TextBox TxtMar = (TextBox)GV_AnnualPlan.Rows[i].FindControl("TxtMar");

            if (dt.Rows[i]["Description"].ToString() == "Advisory Council Members Orientation")
            {


                if (TxtApr.Text != "")
                {
                    Apr = Convert.ToInt32(TxtApr.Text);
                }
                if (TxtMay.Text != "")
                {
                    May = Convert.ToInt32(TxtMay.Text);
                }


                if (TxtJun.Text != "")
                {
                    Jun = Convert.ToInt32(TxtJun.Text);
                }
                if (TxtJul.Text != "")
                {
                    Jul = Convert.ToInt32(TxtJul.Text);
                }
                if (TxtAug.Text != "")
                {
                    Aug = Convert.ToInt32(TxtAug.Text);
                }
                if (TxtSep.Text != "")
                {

                    Sep = Convert.ToInt32(TxtSep.Text);
                }
                if (TxtOct.Text != "")
                {

                    Oct = Convert.ToInt32(TxtOct.Text);
                }
                if (TxtNov.Text != "")
                {
                    Nov = Convert.ToInt32(TxtNov.Text);
                }
                if (TxtDec.Text != "")
                {
                    Dec = Convert.ToInt32(TxtDec.Text);
                }
                if (TxtJan.Text != "")
                {
                    Jan = Convert.ToInt32(TxtJan.Text);
                }
                if (TxtFeb.Text != "")
                {
                    Feb = Convert.ToInt32(TxtFeb.Text);
                }
                if (TxtMar.Text != "")
                {
                    Mar = Convert.ToInt32(TxtMar.Text);
                }

                if (Apr > 0)
                {
                    break;
                }
                if (May > 0)
                {
                    break;
                }
                if (Jun > 0)
                {
                    break;
                }
                if (Jul > 0)
                {
                    break;
                }
                if (Aug > 0)
                {
                    break;
                }
                if (Sep > 0)
                {
                    break;
                }
                if (Oct > 0)
                {
                    break;
                }
                if (Dec > 0)
                {
                    break;
                }
                if (Jan > 0)
                {
                    break;
                }
                if (Feb > 0)
                {
                    break;
                }
                if (Mar > 0)
                {
                    break;
                }
            }


        }
        SIPtdMeeting(Apr, May, Jun, Jul, Aug, Sep, Oct, Nov, Dec, Jan, Feb, Mar);
    }
    public void SIPtd( Int32 Apr, Int32 May, Int32 Jun, Int32 Jul, Int32 Aug, Int32 Sep, Int32 Oct, Int32 Nov, Int32 Dec, Int32 Jan, Int32 Feb, Int32 Mar)
    {
        DataTable dt = Session["dtSearchVill"] as DataTable;
        Int32 Total = Apr + May + Jun + Jul + Aug + Sep + Oct + Nov + Dec + Jan + Feb + Mar;
          for (int i = 0; i < GV_AnnualPlan.Rows.Count; i++)
            {



                if (dt.Rows[i]["Description"].ToString() == "Advisory Council Members Orientation" && Total > 0)
                {

                    TextBox TxtApr = (TextBox)GV_AnnualPlan.Rows[i].FindControl("TxtApr");
                    TextBox TxtMay = (TextBox)GV_AnnualPlan.Rows[i].FindControl("TxtMay");
                    TextBox TxtJun = (TextBox)GV_AnnualPlan.Rows[i].FindControl("TxtJun");
                    TextBox TxtJul = (TextBox)GV_AnnualPlan.Rows[i].FindControl("TxtJul");
                    TextBox TxtAug = (TextBox)GV_AnnualPlan.Rows[i].FindControl("TxtAug");
                    TextBox TxtSep = (TextBox)GV_AnnualPlan.Rows[i].FindControl("TxtSep");
                    TextBox TxtOct = (TextBox)GV_AnnualPlan.Rows[i].FindControl("TxtOct");
                    TextBox TxtNov = (TextBox)GV_AnnualPlan.Rows[i].FindControl("TxtNov");
                    TextBox TxtDec = (TextBox)GV_AnnualPlan.Rows[i].FindControl("TxtDec");
                    TextBox TxtJan = (TextBox)GV_AnnualPlan.Rows[i].FindControl("TxtJan");
                    TextBox TxtFeb = (TextBox)GV_AnnualPlan.Rows[i].FindControl("TxtFeb");
                    TextBox TxtMar = (TextBox)GV_AnnualPlan.Rows[i].FindControl("TxtMar");
                    if (Apr > 0)
                    {
                        TxtApr.Enabled = true;
                        TxtMay.Enabled = true;
                        TxtJun.Enabled = true;
                        TxtJul.Enabled = true;
                        TxtSep.Enabled = true;
                        TxtAug.Enabled = true;
                        TxtOct.Enabled = true;
                        TxtNov.Enabled = true;
                        TxtDec.Enabled = true;
                        TxtJan.Enabled = true;
                        TxtFeb.Enabled = true;
                        TxtMar.Enabled = true;
                    }
                    if (May > 0)
                    {
                    
                        TxtMay.Enabled = true;
                        TxtJun.Enabled = true;
                        TxtJul.Enabled = true;
                        TxtSep.Enabled = true;
                        TxtAug.Enabled = true;
                        TxtOct.Enabled = true;
                        TxtNov.Enabled = true;
                        TxtDec.Enabled = true;
                        TxtJan.Enabled = true;
                        TxtFeb.Enabled = true;
                        TxtMar.Enabled = true;
                    }
                    if (Jun > 0)
                    {

                       
                        TxtJun.Enabled = true;
                        TxtJul.Enabled = true;
                        TxtSep.Enabled = true;
                        TxtAug.Enabled = true;
                        TxtOct.Enabled = true;
                        TxtNov.Enabled = true;
                        TxtDec.Enabled = true;
                        TxtJan.Enabled = true;
                        TxtFeb.Enabled = true;
                        TxtMar.Enabled = true;
                    }
                    if (Jul > 0)
                    {


                       
                        TxtJul.Enabled = true;
                        TxtSep.Enabled = true;
                        TxtAug.Enabled = true;
                        TxtOct.Enabled = true;
                        TxtNov.Enabled = true;
                        TxtDec.Enabled = true;
                        TxtJan.Enabled = true;
                        TxtFeb.Enabled = true;
                        TxtMar.Enabled = true;
                    }
                    if (Aug > 0)
                    {

                        TxtAug.Enabled = true;
                        TxtSep.Enabled = true;                  
                        TxtOct.Enabled = true;
                        TxtNov.Enabled = true;
                        TxtDec.Enabled = true;
                        TxtJan.Enabled = true;
                        TxtFeb.Enabled = true;
                        TxtMar.Enabled = true;
                    }
                    if (Sep > 0)
                    {

                         TxtSep.Enabled = true;
                        TxtOct.Enabled = true;
                        TxtNov.Enabled = true;
                        TxtDec.Enabled = true;
                        TxtJan.Enabled = true;
                        TxtFeb.Enabled = true;
                        TxtMar.Enabled = true;
                    }
                    if (Oct > 0)
                    {

                      
                        TxtOct.Enabled = true;
                        TxtNov.Enabled = true;
                        TxtDec.Enabled = true;
                        TxtJan.Enabled = true;
                        TxtFeb.Enabled = true;
                        TxtMar.Enabled = true;
                    }
                    if (Nov > 0)
                    {


                        
                        TxtNov.Enabled = true;
                        TxtDec.Enabled = true;
                        TxtJan.Enabled = true;
                        TxtFeb.Enabled = true;
                        TxtMar.Enabled = true;
                    }
                    if (Dec > 0)
                    {


                       
                        TxtDec.Enabled = true;
                        TxtJan.Enabled = true;
                        TxtFeb.Enabled = true;
                        TxtMar.Enabled = true;
                    }
                    if (Jan > 0)
                    {



                       
                        TxtJan.Enabled = true;
                        TxtFeb.Enabled = true;
                        TxtMar.Enabled = true;
                    }
                    if (Feb > 0)
                    {
                                               
                        TxtFeb.Enabled = true;
                        TxtMar.Enabled = true;
                    }
                    if (Mar > 0)
                    {

                       
                        TxtMar.Enabled = true;
                    }

                }

          
        }
    }
    public void SIPtdMeeting(Int32 Apr, Int32 May, Int32 Jun, Int32 Jul, Int32 Aug, Int32 Sep, Int32 Oct, Int32 Nov, Int32 Dec, Int32 Jan, Int32 Feb, Int32 Mar)
    {
        DataTable dt = Session["dtSearchVill"] as DataTable;
        Int32 Total = Apr + May + Jun + Jul + Aug + Sep + Oct + Nov + Dec + Jan + Feb + Mar;
        for (int i = 0; i < GV_AnnualPlan.Rows.Count; i++)
        {



            if (dt.Rows[i]["Description"].ToString() == "Advisory Council Members Meeting" && Total > 0)
            {

                TextBox TxtApr = (TextBox)GV_AnnualPlan.Rows[i].FindControl("TxtApr");
                TextBox TxtMay = (TextBox)GV_AnnualPlan.Rows[i].FindControl("TxtMay");
                TextBox TxtJun = (TextBox)GV_AnnualPlan.Rows[i].FindControl("TxtJun");
                TextBox TxtJul = (TextBox)GV_AnnualPlan.Rows[i].FindControl("TxtJul");
                TextBox TxtAug = (TextBox)GV_AnnualPlan.Rows[i].FindControl("TxtAug");
                TextBox TxtSep = (TextBox)GV_AnnualPlan.Rows[i].FindControl("TxtSep");
                TextBox TxtOct = (TextBox)GV_AnnualPlan.Rows[i].FindControl("TxtOct");
                TextBox TxtNov = (TextBox)GV_AnnualPlan.Rows[i].FindControl("TxtNov");
                TextBox TxtDec = (TextBox)GV_AnnualPlan.Rows[i].FindControl("TxtDec");
                TextBox TxtJan = (TextBox)GV_AnnualPlan.Rows[i].FindControl("TxtJan");
                TextBox TxtFeb = (TextBox)GV_AnnualPlan.Rows[i].FindControl("TxtFeb");
                TextBox TxtMar = (TextBox)GV_AnnualPlan.Rows[i].FindControl("TxtMar");
                if (Apr > 0)
                {
                    TxtApr.Enabled = true;
                    TxtMay.Enabled = true;
                    TxtJun.Enabled = true;
                    TxtJul.Enabled = true;
                    TxtSep.Enabled = true;
                    TxtAug.Enabled = true;
                    TxtOct.Enabled = true;
                    TxtNov.Enabled = true;
                    TxtDec.Enabled = true;
                    TxtJan.Enabled = true;
                    TxtFeb.Enabled = true;
                    TxtMar.Enabled = true;
                }
                if (May > 0)
                {

                    TxtMay.Enabled = true;
                    TxtJun.Enabled = true;
                    TxtJul.Enabled = true;
                    TxtSep.Enabled = true;
                    TxtAug.Enabled = true;
                    TxtOct.Enabled = true;
                    TxtNov.Enabled = true;
                    TxtDec.Enabled = true;
                    TxtJan.Enabled = true;
                    TxtFeb.Enabled = true;
                    TxtMar.Enabled = true;
                }
                if (Jun > 0)
                {


                    TxtJun.Enabled = true;
                    TxtJul.Enabled = true;
                    TxtSep.Enabled = true;
                    TxtAug.Enabled = true;
                    TxtOct.Enabled = true;
                    TxtNov.Enabled = true;
                    TxtDec.Enabled = true;
                    TxtJan.Enabled = true;
                    TxtFeb.Enabled = true;
                    TxtMar.Enabled = true;
                }
                if (Jul > 0)
                {



                    TxtJul.Enabled = true;
                    TxtSep.Enabled = true;
                    TxtAug.Enabled = true;
                    TxtOct.Enabled = true;
                    TxtNov.Enabled = true;
                    TxtDec.Enabled = true;
                    TxtJan.Enabled = true;
                    TxtFeb.Enabled = true;
                    TxtMar.Enabled = true;
                }
                if (Aug > 0)
                {

                    TxtAug.Enabled = true;
                    TxtSep.Enabled = true;
                    TxtOct.Enabled = true;
                    TxtNov.Enabled = true;
                    TxtDec.Enabled = true;
                    TxtJan.Enabled = true;
                    TxtFeb.Enabled = true;
                    TxtMar.Enabled = true;
                }
                if (Sep > 0)
                {

                    TxtSep.Enabled = true;
                    TxtOct.Enabled = true;
                    TxtNov.Enabled = true;
                    TxtDec.Enabled = true;
                    TxtJan.Enabled = true;
                    TxtFeb.Enabled = true;
                    TxtMar.Enabled = true;
                }
                if (Oct > 0)
                {


                    TxtOct.Enabled = true;
                    TxtNov.Enabled = true;
                    TxtDec.Enabled = true;
                    TxtJan.Enabled = true;
                    TxtFeb.Enabled = true;
                    TxtMar.Enabled = true;
                }
                if (Nov > 0)
                {



                    TxtNov.Enabled = true;
                    TxtDec.Enabled = true;
                    TxtJan.Enabled = true;
                    TxtFeb.Enabled = true;
                    TxtMar.Enabled = true;
                }
                if (Dec > 0)
                {



                    TxtDec.Enabled = true;
                    TxtJan.Enabled = true;
                    TxtFeb.Enabled = true;
                    TxtMar.Enabled = true;
                }
                if (Jan > 0)
                {




                    TxtJan.Enabled = true;
                    TxtFeb.Enabled = true;
                    TxtMar.Enabled = true;
                }
                if (Feb > 0)
                {

                    TxtFeb.Enabled = true;
                    TxtMar.Enabled = true;
                }
                if (Mar > 0)
                {


                    TxtMar.Enabled = true;
                }

            }


        }
    }

    #endregion
    #region Button Click Events
    protected void     btnSerach_Click(object sender, EventArgs e)
    {
        Locking();
        DataTable dtSchool = new DataTable();
        pnlMain.Enabled = true;


        if (ddlType.SelectedIndex <= 0)
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Select Plan Type')</script>", false);
          
            return;

        }
        if (ddlDistrict.SelectedIndex <= 0)
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Select District')</script>", false);

            return;

        }
        if (ddlType.SelectedValue == "2" || ddlType.SelectedValue == "3")
        {
            lblMsg.Visible = false;
            if (ddlBlock.SelectedIndex <= 0)
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Select Block')</script>", false);

                return;

            }
        }
        if (ddlType.SelectedValue == "1")
        {
            lblMsg.Visible = true;
            string SubType = "";
           

            string strQry = " select mstLookupAnnaulPlanAgp.Description,mstLookupAnnaulPlanAgp.LookupCode,  [Apr], [May], [Jun], [Jul], [Aug], [Sep], [Oct], [Nov], [Dec], [Jan], [Feb], [Mar],SysFlag,StartMonth,EndMonth,MaxVal,mstLookupAnnaulPlanAgp.LookupType,PhageFlag from mstLookupAnnaulPlanAgp   left join (select *  from tblAnualPlanDataDetailAgp where Districtcode='" + ddlDistrict.SelectedValue + "' and PlanType=1 )   as tblAnualPlanDataDetailAgp on mstLookupAnnaulPlanAgp.LookUpcode =tblAnualPlanDataDetailAgp.RowNo where LookupFlag='APLD'  " + SubType + "  order by seqno ";
            dtSchool = objComman.LoadData(strQry);
            //if (dtSchool.Rows.Count > 0)
            //{
            //}
            //else
            //{
               
            //    strQry = " select Description,LookupCode, 0 as [Apr],0 as [May],0 as [Jun],0 as [Jul],0 as [Aug],0 as [Sep],0 as [Oct],0 as [Nov],0 as [Dec],0 as [Jan],0 as [Feb],0 as [Mar],0 as SysFlag,StartMonth,EndMonth,MaxVal,LookupType from mstLookupAnnaulPlanAgp where LookupFlag='APLD' " + SubType + " order by seqno ";
            //    dtSchool = objComman.LoadData(strQry);
            //}
            if (dtSchool.Rows.Count > 0)
            {
                GVMain.DataSource = null;
                GVMain.DataBind();
                GV_AnnualPlan.DataSource = dtSchool;
                GV_AnnualPlan.DataBind();

            }
            else
            {

                GV_AnnualPlan.DataSource = null;
                GV_AnnualPlan.DataBind();

            }
        }
        else
        {
            Bindgrid();

            string strQry = " select  [Jun], [Jul], [Aug], [Sep]  from tblAnualPlanDataDetailAgp where Districtcode='" + ddlDistrict.SelectedValue + "' and PlanType=1 and RowNo=10  ";
            DataTable dtLearing = objComman.LoadData(strQry);
            Session["dtLearing"] = dtLearing;
        }

       // ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "ddlTypeOnChangeEvent()", true);
       //DVEE.Attributes.Add("style", "display:block");
      
       
    }

    
    public void GKPDATA()
    {
        DataTable dt = Session["dtSearchVill"] as DataTable;
        for (int i = 0; i < GV_AnnualPlan.Rows.Count; i++)
        {


            TextBox TxtApr = (TextBox)GV_AnnualPlan.Rows[i].FindControl("TxtApr");
            TextBox TxtMay = (TextBox)GV_AnnualPlan.Rows[i].FindControl("TxtMay");
            TextBox TxtJun = (TextBox)GV_AnnualPlan.Rows[i].FindControl("TxtJun");
            TextBox TxtJul = (TextBox)GV_AnnualPlan.Rows[i].FindControl("TxtJul");
            TextBox TxtAug = (TextBox)GV_AnnualPlan.Rows[i].FindControl("TxtAug");
            TextBox TxtSep = (TextBox)GV_AnnualPlan.Rows[i].FindControl("TxtSep");
            TextBox TxtOct = (TextBox)GV_AnnualPlan.Rows[i].FindControl("TxtOct");
            TextBox TxtNov = (TextBox)GV_AnnualPlan.Rows[i].FindControl("TxtNov");
            TextBox TxtDec = (TextBox)GV_AnnualPlan.Rows[i].FindControl("TxtDec");
            TextBox TxtJan = (TextBox)GV_AnnualPlan.Rows[i].FindControl("TxtJan");
            TextBox TxtFeb = (TextBox)GV_AnnualPlan.Rows[i].FindControl("TxtFeb");
            TextBox TxtMar = (TextBox)GV_AnnualPlan.Rows[i].FindControl("TxtMar");


            if (dt.Rows[i]["Description"].ToString() == "Learning Baseline for GKP")
            {


                Int32 Jun = 0;
                Int32 Jul = 0;
                Int32 Aug = 0;
                Int32 Sep = 0;
                Int32 Oct = 0;

                if (TxtJun.Text != "")
                {
                    Jun = Convert.ToInt32(TxtJun.Text);
                }
                if (TxtJul.Text != "")
                {
                    Jul = Convert.ToInt32(TxtJul.Text);
                }
                if (TxtAug.Text != "")
                {
                    Aug = Convert.ToInt32(TxtAug.Text);
                }
                if (TxtSep.Text != "")
                {

                    Sep = Convert.ToInt32(TxtSep.Text);
                }
                if (TxtOct.Text != "")
                {

                    Oct = Convert.ToInt32(TxtOct.Text);
                }
              
                LEARNINGMIDLINE(dt, Jun,Jul, Sep, Aug, Oct);

            }


        }
    }

    public void LEARNINGMIDLINE(DataTable dt, Int32 jun,Int32 jul, Int32 Sep, Int32 Aug, Int32 Oct)
    {

        dtGKPPlan = Session["dtGKPPlan"] as DataTable;
        for (int i = 0; i < GV_AnnualPlan.Rows.Count; i++)
        {


            TextBox TxtApr = (TextBox)GV_AnnualPlan.Rows[i].FindControl("TxtApr");
            TextBox TxtMay = (TextBox)GV_AnnualPlan.Rows[i].FindControl("TxtMay");
            TextBox TxtJun = (TextBox)GV_AnnualPlan.Rows[i].FindControl("TxtJun");
            TextBox TxtJul = (TextBox)GV_AnnualPlan.Rows[i].FindControl("TxtJul");
            TextBox TxtAug = (TextBox)GV_AnnualPlan.Rows[i].FindControl("TxtAug");
            TextBox TxtSep = (TextBox)GV_AnnualPlan.Rows[i].FindControl("TxtSep");
            TextBox TxtOct = (TextBox)GV_AnnualPlan.Rows[i].FindControl("TxtOct");
            TextBox TxtNov = (TextBox)GV_AnnualPlan.Rows[i].FindControl("TxtNov");
            TextBox TxtDec = (TextBox)GV_AnnualPlan.Rows[i].FindControl("TxtDec");
            TextBox TxtJan = (TextBox)GV_AnnualPlan.Rows[i].FindControl("TxtJan");
            TextBox TxtFeb = (TextBox)GV_AnnualPlan.Rows[i].FindControl("TxtFeb");
            TextBox TxtMar = (TextBox)GV_AnnualPlan.Rows[i].FindControl("TxtMar");



            if (dt.Rows[i]["Description"].ToString() == "GKP L0/L1" && Convert.ToString(ViewState["GKPLevel"]) == "1")
            {
                if (Convert.ToString(ViewState["GKP"]) == "1" && Convert.ToString(ViewState["GKPLevel"]) == "1")
                {
                    #region

                    DataRow[] dr = dtGKPPlan.Select("GKPID=" + Convert.ToString(ViewState["GKPLevel"]) + "");
                    TxtJun.Text = "0";
                    TxtJul.Text = "0";
                    TxtSep.Text = "0";
                    TxtAug.Text = "0";
                    TxtOct.Text = "0";
                    TxtNov.Text = "0";
                    TxtDec.Text = "0";
                    TxtJan.Text = "0";
                    TxtFeb.Text = "0";
                    TxtMar.Text = "0";
                    if (jun > 0)
                    {
                        if (dr.Length > 0)
                        {
                            TxtJun.Text = dr[0]["Month1"].ToString();
                            TxtJul.Text = dr[0]["Month1"].ToString();
                            TxtSep.Text = dr[0]["Month2"].ToString();
                            TxtAug.Text = dr[0]["Month3"].ToString();
                            TxtOct.Text = dr[0]["Month4"].ToString();
                            TxtNov.Text = dr[0]["Month5"].ToString();
                        
                        }



                    }
                    if (jul > 0)
                    {
                        if (dr.Length > 0)
                        {
                            TxtJul.Text = dr[0]["Month1"].ToString();
                            TxtSep.Text = dr[0]["Month2"].ToString();
                            TxtAug.Text = dr[0]["Month3"].ToString();
                            TxtOct.Text = dr[0]["Month4"].ToString();
                            TxtNov.Text = dr[0]["Month5"].ToString();
                            TxtDec.Text = dr[0]["Month6"].ToString();
                        }
                    


                    }
                    if (Sep > 0)
                    {
                        if (dr.Length > 0)
                        {
                            TxtSep.Text = dr[0]["Month1"].ToString();
                            TxtOct.Text = dr[0]["Month2"].ToString();
                            TxtNov.Text = dr[0]["Month3"].ToString();
                            TxtDec.Text = dr[0]["Month4"].ToString();
                            TxtJan.Text = dr[0]["Month5"].ToString();

                            TxtFeb.Text = dr[0]["Month6"].ToString();
                        
                        
                        }
                    }
                    if (Aug > 0)
                    {
                        if (dr.Length > 0)
                        {
                         
                            TxtSep.Text = dr[0]["Month1"].ToString();
                            TxtAug.Text = dr[0]["Month2"].ToString();
                            TxtOct.Text = dr[0]["Month3"].ToString();
                            TxtNov.Text = dr[0]["Month4"].ToString();
                            TxtDec.Text = dr[0]["Month5"].ToString();
                            TxtJan.Text = dr[0]["Month6"].ToString();
                         
                        }
                    }
                    if (Oct > 0)
                    {


                        if (dr.Length > 0)
                        {

                            TxtOct.Text = dr[0]["Month1"].ToString();
                            TxtNov.Text = dr[0]["Month2"].ToString();
                            TxtDec.Text = dr[0]["Month3"].ToString();
                            TxtJan.Text = dr[0]["Month4"].ToString();

                            TxtFeb.Text = dr[0]["Month5"].ToString();
                            TxtMar.Text = dr[0]["Month6"].ToString();

                        }
                    }
               
                    #endregion
                }
            }

            if (dt.Rows[i]["Description"].ToString() == "GKP L1/L2" && Convert.ToString(ViewState["GKPLevel"]) == "2")
            {
                if (Convert.ToString(ViewState["GKP"]) == "1" && Convert.ToString(ViewState["GKPLevel"]) == "2")
                {
                    #region

                    DataRow[] dr = dtGKPPlan.Select("GKPID=" + Convert.ToString(ViewState["GKPLevel"]) + "");
                  
                   TxtJul.Text = "0";
                    TxtSep.Text = "0";
                    TxtAug.Text = "0";
                    TxtOct.Text = "0";
                    TxtNov.Text = "0";
                    TxtDec.Text = "0";
                    TxtJan.Text = "0";
                    TxtFeb.Text = "0";
                    TxtMar.Text = "0";
                    if (jul > 0)
                    {

                        if (dr.Length > 0)
                        {
                            TxtJul.Text = dr[0]["Month1"].ToString();
                            TxtSep.Text = dr[0]["Month2"].ToString();
                            TxtAug.Text = dr[0]["Month3"].ToString();
                            TxtOct.Text = dr[0]["Month4"].ToString();
                            TxtNov.Text = dr[0]["Month5"].ToString();
                            TxtDec.Text = dr[0]["Month6"].ToString();
                        

                        }


                    }
                    if (Sep > 0)
                    {
                        if (dr.Length > 0)
                        {
                            TxtSep.Text = dr[0]["Month1"].ToString();
                            TxtOct.Text = dr[0]["Month2"].ToString();
                            TxtNov.Text = dr[0]["Month3"].ToString();
                            TxtDec.Text = dr[0]["Month4"].ToString();
                            TxtJan.Text = dr[0]["Month5"].ToString();

                            TxtFeb.Text = dr[0]["Month6"].ToString();
                        
                        }
                    }
                    if (Aug > 0)
                    {
                        if (dr.Length > 0)
                        {
                         
                            TxtSep.Text = dr[0]["Month1"].ToString();
                            TxtAug.Text = dr[0]["Month2"].ToString();
                            TxtOct.Text = dr[0]["Month3"].ToString();
                            TxtNov.Text = dr[0]["Month4"].ToString();
                            TxtDec.Text = dr[0]["Month5"].ToString();
                            TxtJan.Text = dr[0]["Month6"].ToString();
                         
                        }
                    }
                    if (Oct > 0)
                    {


                        if (dr.Length > 0)
                        {

                            TxtOct.Text = dr[0]["Month1"].ToString();
                            TxtNov.Text = dr[0]["Month2"].ToString();
                            TxtDec.Text = dr[0]["Month3"].ToString();
                            TxtJan.Text = dr[0]["Month4"].ToString();

                            TxtFeb.Text = dr[0]["Month5"].ToString();
                            TxtMar.Text = dr[0]["Month6"].ToString();
                        }
                    }
                    
                }
                    #endregion
            }
            if (dt.Rows[i]["Description"].ToString() == "GKP L2/L3" && Convert.ToString(ViewState["GKPLevel"]) == "3")
            {
                if (Convert.ToString(ViewState["GKP"]) == "1" && Convert.ToString(ViewState["GKPLevel"]) == "3")
                {
                    #region

                    DataRow[] dr = dtGKPPlan.Select("GKPID=" + Convert.ToString(ViewState["GKPLevel"]) + "");
                   TxtJul.Text = "0";
                    TxtSep.Text = "0";
                    TxtAug.Text = "0";
                    TxtOct.Text = "0";
                    TxtNov.Text = "0";
                    TxtDec.Text = "0";
                    TxtJan.Text = "0";
                    TxtFeb.Text = "0";
                    TxtMar.Text = "0";
                    if (jul > 0)
                    {

                        if (dr.Length > 0)
                        {
                            TxtJul.Text = dr[0]["Month1"].ToString();
                            TxtSep.Text = dr[0]["Month2"].ToString();
                            TxtAug.Text = dr[0]["Month3"].ToString();
                            TxtOct.Text = dr[0]["Month4"].ToString();
                            TxtNov.Text = dr[0]["Month5"].ToString();
                            TxtDec.Text = dr[0]["Month6"].ToString();
                        

                        }


                    }
                    if (Sep > 0)
                    {
                        if (dr.Length > 0)
                        {
                            TxtSep.Text = dr[0]["Month1"].ToString();
                            TxtOct.Text = dr[0]["Month2"].ToString();
                            TxtNov.Text = dr[0]["Month3"].ToString();
                            TxtDec.Text = dr[0]["Month4"].ToString();
                            TxtJan.Text = dr[0]["Month5"].ToString();

                            TxtFeb.Text = dr[0]["Month6"].ToString();
                        
                        
                          

                        }
                    }
                    if (Aug > 0)
                    {
                        if (dr.Length > 0)
                        {
                         
                            TxtSep.Text = dr[0]["Month1"].ToString();
                            TxtAug.Text = dr[0]["Month2"].ToString();
                            TxtOct.Text = dr[0]["Month3"].ToString();
                            TxtNov.Text = dr[0]["Month4"].ToString();
                            TxtDec.Text = dr[0]["Month5"].ToString();
                            TxtJan.Text = dr[0]["Month6"].ToString();
                         
                        }
                    }
                    if (Oct > 0)
                    {


                        if (dr.Length > 0)
                        {

                            TxtOct.Text = dr[0]["Month1"].ToString();
                            TxtNov.Text = dr[0]["Month2"].ToString();
                            TxtDec.Text = dr[0]["Month3"].ToString();
                            TxtJan.Text = dr[0]["Month4"].ToString();

                            TxtFeb.Text = dr[0]["Month5"].ToString();
                            TxtMar.Text = dr[0]["Month6"].ToString();
                        }
                    }
                }
                    #endregion
            }

        }
            
    }
    protected void btnsave_Click(object sender, EventArgs e)
    {
      
        SaveData();
        if (ddlType.SelectedValue == "2")
            {
              
                TraingGPp();
                TraingGPpMeeting();
            
            }
          
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        //string condition = "";
        //SqlParameter[] para = new SqlParameter[] { 

        //    new SqlParameter("@Villagecode","88770EE01C254309904B12A72"),

        //    };


        //string sReturn = string.Empty;
        ////try
        ////{
        //DataSet dttabletdata = new DataSet();

        //dttabletdata = SqlHelper.GetDataSet(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "GetAnualData", para);
        //DataTable dtState = dttabletdata.Tables[0].Copy();
        //if (dtState.Rows.Count > 0)
        //{

        //    foreach (DataRow dr in dtState.Rows)
        //    {

        //        string str = "insert into tblSummarySchoolAnualData (Villagecode,Name,[DISECode] ,[CRITICALSIP], [OTHERSIP], [TOTALSIP],[SixOOSG] ,[SevenOOSG],[TotalOOSG] ,[SevenOOSB],SchoolCode,SchooLevel,SACQ1,SACQ2,SACQ3,SACQ4,RowNo) values('" + dr["Villagecode"] + "','" + dr["Name"] + "','" + dr["DISECode"] + "'," + dr["CRITICALSIP"] + "," + dr["OTHERSIP"] + "," + dr["TOTALSIP"] + "," + dr["SixOOSG"] + "," + dr["SevenOOSG"] + "," + dr["TotalOOSG"] + "," + dr["SevenOOSB"] + ",'" + dr["SchoolCode"] + "','" + dr["SchooLevel"] + "'," + dr["SACQ1"] + "," + dr["SACQ2"] + "," + dr["SACQ3"] + "," + dr["SACQ4"] + "," + dr["RowNo"] + ") ";


        //        bool res = objMain.AddUpdate(str);


        //    }
        //}
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        try
        {
            bool InsertTS = false;
            string SchoolId = Convert.ToString(ViewState["SchoolId"]);
            string Vilagecode = Convert.ToString(ViewState["VillageCode"]); ;
            if (Convert.ToInt32(ddlType.SelectedValue)== 3)
            {
                //string StudentTSInsertQuery1 = " delete from  tblAnualPlanDataDetailAgp where schoolCode ='" + SchoolId + "' and Plantype=3";
                //InsertTS = objMain.AddUpdate(StudentTSInsertQuery1);
            }
            if (Convert.ToInt32(ddlType.SelectedValue) == 2)
            {
                //string StudentTSInsertQuery1 = " delete from  tblAnualPlanDataDetailAgp where VillageCode ='" + Vilagecode + "' and Plantype=2 ";
                //InsertTS = objMain.AddUpdate(StudentTSInsertQuery1);
            }
            if (Convert.ToInt32(ddlType.SelectedValue) == 1)
            {
                //string StudentTSInsertQuery1 = " delete from  tblAnualPlanDataDetailAgp where DistrictCode ='" + ddlDistrict.SelectedValue + "' and Plantype=1 ";
                //InsertTS = objMain.AddUpdate(StudentTSInsertQuery1);
            }
            if (InsertTS == true)
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Delete sucessfully')</script>", false);
                GV_AnnualPlan.DataSource = null;
                GV_AnnualPlan.DataBind();
            }

        }
        catch (Exception)
        {

            throw;
        }
    }
    protected void btnSumbit_Click(object sender, EventArgs e)
    {
    }
    protected void txtSearchName_Click(object sender, EventArgs e)
    {
    }
   
    #endregion
    #region Gridview Events
    protected void GVMain_OnRowCommand(object sender, GridViewCommandEventArgs e)
    {

        if (e.CommandName == "GVUIO")
        {
            int iIndex = Convert.ToInt32(e.CommandArgument);
            string SchoolId = Convert.ToString(GVMain.DataKeys[iIndex]["DISECode"]);
            string VillageCode = Convert.ToString(GVMain.DataKeys[iIndex]["VillageCode"]);
            RowNo = Convert.ToString(GVMain.DataKeys[iIndex]["RowNo"]);
            SchoolLeavel = Convert.ToString(GVMain.DataKeys[iIndex]["SchoolLevel"]);
            BalSacha = Convert.ToString(GVMain.DataKeys[iIndex]["BAlVal"]);
            GKP = Convert.ToString(GVMain.DataKeys[iIndex]["GKP"]);
          string  GKPLevel= Convert.ToString(GVMain.DataKeys[iIndex]["GKPLevel"]);
          string ManagementType = Convert.ToString(GVMain.DataKeys[iIndex]["ManagementType"]);
            ViewState["SchoolId"] = SchoolId;
            ViewState["VillageCode"] = VillageCode;
            ViewState["RowNo"] = RowNo;
            ViewState["SchoolLeavel"] = SchoolLeavel;
            ViewState["BalSacha"] = BalSacha;

            ViewState["GKP"] = GKP;
            ViewState["GKPLevel"] = GKPLevel;
            ViewState["ManagementType"] = ManagementType;
            
            LoadData();
            ViewState["Save"] = "Edit";

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
      //  ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "ddlTypeOnChangeEvent()", true);
    }
    protected void GV_Project_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GVMain.PageIndex = e.NewPageIndex;
        if (ViewState["Serach"] != null)
        {
            DataTable dt = ViewState["Serach"] as DataTable;
            GVMain.DataSource = dt;
            GVMain.DataBind();
        }

    }
    protected void GV_AnnualPlan_OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lb = ((Label)e.Row.FindControl("LblDesc"));
            TextBox TxtApr = ((TextBox)e.Row.FindControl("TxtApr"));
            TextBox TxtMay = ((TextBox)e.Row.FindControl("TxtMay"));
            TextBox TxtJun = ((TextBox)e.Row.FindControl("TxtJun"));
            TextBox TxtJul = ((TextBox)e.Row.FindControl("TxtJul"));
            TextBox TxtAug = ((TextBox)e.Row.FindControl("TxtAug"));
            TextBox TxtSep = ((TextBox)e.Row.FindControl("TxtSep"));
            TextBox TxtOct = ((TextBox)e.Row.FindControl("TxtOct"));
            TextBox TxtNov = ((TextBox)e.Row.FindControl("TxtNov"));
            TextBox TxtDec = ((TextBox)e.Row.FindControl("TxtDec"));
            TextBox TxtJan = ((TextBox)e.Row.FindControl("TxtJan"));
            TextBox TxtFeb = ((TextBox)e.Row.FindControl("TxtFeb"));
            TextBox TxtMar = ((TextBox)e.Row.FindControl("TxtMar"));
            Label lblStartMonth = ((Label)e.Row.FindControl("lblStartMonth"));
            Label lblEndMonth = ((Label)e.Row.FindControl("lblEndMonth"));
             Label lblPhageFlag = ((Label)e.Row.FindControl("lblPhageFlag"));
           
            if (ddlType.SelectedValue == "1")
            {
               
                    LoadDataEnable(TxtApr, TxtMay, TxtJun, TxtJul, TxtAug, TxtSep, TxtOct, TxtNov, TxtDec, TxtJan, TxtFeb, TxtMar, Convert.ToInt32(lblStartMonth.Text), Convert.ToInt32(lblEndMonth.Text));

                
           
            }
            else if (ddlType.SelectedValue == "2")
            {
              
                    LoadDataEnable(TxtApr, TxtMay, TxtJun, TxtJul, TxtAug, TxtSep, TxtOct, TxtNov, TxtDec, TxtJan, TxtFeb, TxtMar, Convert.ToInt32(lblStartMonth.Text), Convert.ToInt32(lblEndMonth.Text));
               
                
            }
           

        }
    }

     public void LEARNIOpenMonth(DataTable dt, Int32 Jun, Int32 Jul, Int32 Aug, Int32 sep)
    {

     
        for (int i = 0; i < GV_AnnualPlan.Rows.Count; i++)
        {



            Label LblDesc = (Label)GV_AnnualPlan.Rows[i].FindControl("LblDesc");
            TextBox TxtJun = (TextBox)GV_AnnualPlan.Rows[i].FindControl("TxtJun");
            TextBox TxtJul = (TextBox)GV_AnnualPlan.Rows[i].FindControl("TxtJul");
            TextBox TxtAug = (TextBox)GV_AnnualPlan.Rows[i].FindControl("TxtAug");
            TextBox TxtSep = (TextBox)GV_AnnualPlan.Rows[i].FindControl("TxtSep");
            TextBox TxtOct = (TextBox)GV_AnnualPlan.Rows[i].FindControl("TxtOct");
            TextBox TxtNov = (TextBox)GV_AnnualPlan.Rows[i].FindControl("TxtNov");
            TextBox TxtDec = (TextBox)GV_AnnualPlan.Rows[i].FindControl("TxtDec");
            TextBox TxtJan = (TextBox)GV_AnnualPlan.Rows[i].FindControl("TxtJan");
            TextBox TxtFeb = (TextBox)GV_AnnualPlan.Rows[i].FindControl("TxtFeb");
            if (LblDesc.Text == "Learning Baseline for GKP")
            {
                if (Jun > 0)
                {
                    TxtJun.Enabled = true;
                    TxtJul.Enabled = true;
                }
                if (Jul > 0)
                {
                    TxtJul.Enabled = true;
                    TxtAug.Enabled = true;
                }
                if (Aug > 0)
                {
                    TxtAug.Enabled = true;
                    TxtSep.Enabled = true;
                }
                if (sep > 0)
                {
                    TxtOct.Enabled = true;
                    TxtSep.Enabled = true;
                }
            }
        }
    }
    public void LoadDataEnable(TextBox TxtApr, TextBox TxtMay, TextBox TxtJun, TextBox TxtJul, TextBox TxtAug, TextBox TxtSep, TextBox TxtOct, TextBox TxtNov, TextBox TxtDec, TextBox TxtJan, TextBox TxtFeb, TextBox TxtMar,int StartMonth, int EndMonth)
    {
        int i = StartMonth;
        for (StartMonth = i; StartMonth <= EndMonth - 1; StartMonth++)
        {

            if (StartMonth == 0)
            {
                TxtApr.Enabled = true;
            }
            if (StartMonth == 1)
            {

                TxtMay.Enabled = true;
            }
            if (StartMonth == 2)
            {
                TxtJun.Enabled = true;
            }
            if (StartMonth == 3)
            {
                TxtJul.Enabled = true;
            }
            if (StartMonth == 4)
            {
                TxtAug.Enabled = true;
            }
            if (StartMonth == 5)
            {
                TxtSep.Enabled = true;
            }
            if (StartMonth == 6)
            {
                TxtOct.Enabled = true;
            }
            if (StartMonth == 7)
            {
                TxtNov.Enabled = true;
                }
                if (StartMonth == 8)
                {
                    TxtDec.Enabled = true;
                }

                if (StartMonth == 9)
                {
                    TxtJan.Enabled = true;
                }
                if (StartMonth == 10)
                {
                    TxtFeb.Enabled = true;
                }
                if (StartMonth == 11)
                {

                    TxtMar.Enabled = true;
                }
        
        }
    }
    protected void EnableDisableMonth(TextBox TxtApr, TextBox TxtMay, TextBox TxtJun, TextBox TxtJul, TextBox TxtAug, TextBox TxtSep, TextBox TxtOct, TextBox TxtNov, TextBox TxtDec, TextBox TxtJan, TextBox TxtFeb, TextBox TxtMar, bool Apr, bool May, bool Jun, bool Jul, bool Aug, bool Sep, bool Oct, bool Nov, bool Dec, bool Jan, bool Feb, bool Mar)
    {
        TxtApr.Enabled = Apr;
        TxtMay.Enabled = May;
        TxtJun.Enabled = Jun;
        TxtJul.Enabled = Jul;
        TxtAug.Enabled = Aug;
        TxtSep.Enabled = Sep;
        TxtOct.Enabled = Oct;
        TxtNov.Enabled = Nov;
        TxtDec.Enabled = Dec;
        TxtJan.Enabled = Jan;
        TxtFeb.Enabled = Feb;
        TxtMar.Enabled = Mar;
    }
    protected void BalEnableDisableMonth()
    {
        Int32 Aug = 0, Sep = 0, Oct = 0;
        for (int i = 0; i < dtSearchVill.Rows.Count; i++)
        {
            TextBox TxtApr = (TextBox)GV_AnnualPlan.Rows[i].FindControl("TxtApr");
            TextBox TxtMay = (TextBox)GV_AnnualPlan.Rows[i].FindControl("TxtMay");
            TextBox TxtJun = (TextBox)GV_AnnualPlan.Rows[i].FindControl("TxtJun");
            TextBox TxtJul = (TextBox)GV_AnnualPlan.Rows[i].FindControl("TxtJul");
            TextBox TxtAug = (TextBox)GV_AnnualPlan.Rows[i].FindControl("TxtAug");
            TextBox TxtSep = (TextBox)GV_AnnualPlan.Rows[i].FindControl("TxtSep");
            TextBox TxtOct = (TextBox)GV_AnnualPlan.Rows[i].FindControl("TxtOct");
            TextBox TxtNov = (TextBox)GV_AnnualPlan.Rows[i].FindControl("TxtNov");
            TextBox TxtDec = (TextBox)GV_AnnualPlan.Rows[i].FindControl("TxtDec");
            TextBox TxtJan = (TextBox)GV_AnnualPlan.Rows[i].FindControl("TxtJan");
            TextBox TxtFeb = (TextBox)GV_AnnualPlan.Rows[i].FindControl("TxtFeb");
            TextBox TxtMar = (TextBox)GV_AnnualPlan.Rows[i].FindControl("TxtMar");
            if (dtSearchVill.Rows[i]["Description"].ToString() == "Bal Sabha")
            {

                if (TxtSep.Text!="")
                {
                    Sep = Convert.ToInt32(TxtSep.Text);
                }
                if (TxtAug.Text != "")
                {
                    Aug = Convert.ToInt32(TxtAug.Text);
                }
                if (TxtOct.Text != "")
                {
                    Oct = Convert.ToInt32(TxtOct.Text);
                }
            }

            if (dtSearchVill.Rows[i]["Description"].ToString() == "LSG")
            {

                if (Sep > 0)
                {

                    TxtSep.Enabled = true;
                    TxtOct.Enabled = true;
                    TxtNov.Enabled = true;
                    TxtDec.Enabled = true;
                    TxtJan.Enabled = true;
                    TxtFeb.Enabled = true;
                    TxtMar.Enabled = true;


                }
                if (Aug > 0)
                {
                    TxtAug.Enabled = true;
                    TxtSep.Enabled = true;
                    TxtOct.Enabled = true;
                    TxtNov.Enabled = true;
                    TxtDec.Enabled = true;
                    TxtJan.Enabled = true;
                    TxtFeb.Enabled = true;
                    TxtMar.Enabled = true;
                }
                if (Oct > 0)
                {

                    TxtOct.Enabled = true;
                    TxtNov.Enabled = true;
                    TxtDec.Enabled = true;
                    TxtJan.Enabled = true;
                    TxtFeb.Enabled = true;
                    TxtMar.Enabled = true;
                }
            }
        }
    }
    protected void GKPEnableDisableMonth()
    {
        Int32 Jul = 0, Aug = 0, Sep = 0, Oct = 0;
        for (int i = 0; i < dtSearchVill.Rows.Count; i++)
        {
            TextBox TxtApr = (TextBox)GV_AnnualPlan.Rows[i].FindControl("TxtApr");
            TextBox TxtMay = (TextBox)GV_AnnualPlan.Rows[i].FindControl("TxtMay");
            TextBox TxtJun = (TextBox)GV_AnnualPlan.Rows[i].FindControl("TxtJun");
            TextBox TxtJul = (TextBox)GV_AnnualPlan.Rows[i].FindControl("TxtJul");
            TextBox TxtAug = (TextBox)GV_AnnualPlan.Rows[i].FindControl("TxtAug");
            TextBox TxtSep = (TextBox)GV_AnnualPlan.Rows[i].FindControl("TxtSep");
            TextBox TxtOct = (TextBox)GV_AnnualPlan.Rows[i].FindControl("TxtOct");
            TextBox TxtNov = (TextBox)GV_AnnualPlan.Rows[i].FindControl("TxtNov");
            TextBox TxtDec = (TextBox)GV_AnnualPlan.Rows[i].FindControl("TxtDec");
            TextBox TxtJan = (TextBox)GV_AnnualPlan.Rows[i].FindControl("TxtJan");
            TextBox TxtFeb = (TextBox)GV_AnnualPlan.Rows[i].FindControl("TxtFeb");
            TextBox TxtMar = (TextBox)GV_AnnualPlan.Rows[i].FindControl("TxtMar");
            if (dtSearchVill.Rows[i]["Description"].ToString() == "Learning Baseline")
            {
                if (Convert.ToInt32(TxtJul.Text) > 0)
                {
                    Jul = Convert.ToInt32(TxtJul.Text);
                }
                if (Convert.ToInt32(TxtSep.Text) > 0)
                {
                    Sep = Convert.ToInt32(TxtSep.Text);
                }
                if (Convert.ToInt32(TxtAug.Text) > 0)
                {
                    Aug = Convert.ToInt32(TxtAug.Text);
                }
                if (Convert.ToInt32(TxtOct.Text) > 0)
                {
                    Oct = Convert.ToInt32(TxtOct.Text);
                }
            }

            if (dtSearchVill.Rows[i]["Description"].ToString() == "GKP L0" || dtSearchVill.Rows[i]["Description"].ToString() == "GKP L1" || dtSearchVill.Rows[i]["Description"].ToString() == "GKP L2" || dtSearchVill.Rows[i]["Description"].ToString() == "GKP L3")
       
            {
                if (Jul > 0)
                {



                    TxtJul.Enabled = true;
                    TxtAug.Enabled = true;
                    TxtSep.Enabled = true;
                    TxtOct.Enabled = true;
                    TxtNov.Enabled = true;
                    TxtDec.Enabled = true;
                    TxtJan.Enabled = true;
                    TxtFeb.Enabled = true;
                    TxtMar.Enabled = true;
                }
                if (Sep > 0)
                {

                    TxtSep.Enabled = true;
                    TxtOct.Enabled = true;
                    TxtNov.Enabled = true;
                    TxtDec.Enabled = true;
                    TxtJan.Enabled = true;
                    TxtFeb.Enabled = true;
                    TxtMar.Enabled = true;


                }
                if (Aug > 0)
                {
                    TxtAug.Enabled = true;
                    TxtSep.Enabled = true;
                    TxtOct.Enabled = true;
                    TxtNov.Enabled = true;
                    TxtDec.Enabled = true;
                    TxtJan.Enabled = true;
                    TxtFeb.Enabled = true;
                    TxtMar.Enabled = true;
                }
                if (Oct > 0)
                {

                    TxtOct.Enabled = true;
                    TxtNov.Enabled = true;
                    TxtDec.Enabled = true;
                    TxtJan.Enabled = true;
                    TxtFeb.Enabled = true;
                    TxtMar.Enabled = true;
                }
            }
        }
    }

    protected void BalEnableDisableMonth(TextBox TxtApr, TextBox TxtMay, TextBox TxtJun, TextBox TxtJul, TextBox TxtAug, TextBox TxtSep, TextBox TxtOct, TextBox TxtNov, TextBox TxtDec, TextBox TxtJan, TextBox TxtFeb, TextBox TxtMar, bool Apr, bool May, bool Jun, bool Jul, bool Aug, bool Sep, bool Oct, bool Nov, bool Dec, bool Jan, bool Feb, bool Mar)
    {

        if (Convert.ToInt32(TxtJul.Text) > 0)
        {
          

       
            TxtJul.Enabled = true;
            TxtAug.Enabled = true;
            TxtSep.Enabled = true;
            TxtOct.Enabled = true;
            TxtNov.Enabled = true;
            TxtDec.Enabled = true;
            TxtJan.Enabled = true;
            TxtFeb.Enabled = true;
            TxtMar.Enabled = true;
        }
        if (Convert.ToInt32(TxtSep.Text) > 0)
        {

            TxtSep.Enabled = true;
            TxtOct.Enabled = true;
            TxtNov.Enabled = true;
            TxtDec.Enabled = true;
            TxtJan.Enabled = true;
            TxtFeb.Enabled = true;
            TxtMar.Enabled = true;

            TxtApr.Enabled = Apr;
            TxtMay.Enabled = May;
            TxtJun.Enabled = Jun;
            TxtJul.Enabled = Jul;
            TxtAug.Enabled = Aug;
            TxtSep.Enabled = Sep;
            TxtOct.Enabled = Oct;
            TxtNov.Enabled = Nov;
            TxtDec.Enabled = Dec;
            TxtJan.Enabled = Jan;
            TxtFeb.Enabled = Feb;
            TxtMar.Enabled = Mar;
          
        }
        if (Convert.ToInt32(TxtAug.Text) > 0)
        {
            TxtAug.Enabled = true;
            TxtSep.Enabled = true;
            TxtOct.Enabled = true;
            TxtNov.Enabled = true;
            TxtDec.Enabled = true;
            TxtJan.Enabled = true;
            TxtFeb.Enabled = true;
            TxtMar.Enabled = true;
        }

       
    }
    #endregion
    #region Selected Index Changed Events
    protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
    {
        // pnlMain.Enabled = false;
        // GVMain.Enabled = false;
        FillCBDist();
        GVMain.DataSource = null;
        GVMain.DataBind();
        GV_AnnualPlan.DataSource = null;
        GV_AnnualPlan.DataBind();
      //  ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "ddlTypeOnChangeEvent()", true);

    }
    protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillCBBock();
        Locking();
        GVMain.DataSource = null;
        GVMain.DataBind();
        GV_AnnualPlan.DataSource = null;
        GV_AnnualPlan.DataBind();
      //  ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "ddlTypeOnChangeEvent()", true);
    }
    protected void ddlSubType_SelectedIndexChanged(object sender, EventArgs e)
    {
        GV_AnnualPlan.DataSource = null;
        GV_AnnualPlan.DataBind();
        GVMain.DataSource = null;
        GVMain.DataBind();
    }
    protected void ddlType_SelectedIndexChanged(object sender, EventArgs e)
    {
            if (Convert.ToInt32(ddlType.SelectedValue) == 1) 
            {
                divBlock.Attributes.Add("style", "display:none");
                divPhy.Attributes.Add("style", "display:none");
                divVill.Attributes.Add("style", "display:none");
                lblMsg.Visible = false;
            GV_AnnualPlan.DataSource=null;
            GV_AnnualPlan.DataBind();
             GVMain.DataSource=null;
            GVMain.DataBind();

                
            }
            else if (Convert.ToInt32(ddlType.SelectedValue) == 2) 
            {
                lblMsg.Visible = false;
                divBlock.Attributes.Add("style", "display:block");
                divPhy.Attributes.Add("style", "display:block");
                divVill.Attributes.Add("style", "display:none");
             GV_AnnualPlan.DataSource=null;
             GV_AnnualPlan.DataBind();
             GVMain.DataSource=null;
            GVMain.DataBind();
                  }
            else if (Convert.ToInt32(ddlType.SelectedValue) == 3)
            {
                lblMsg.Visible = false;
                divBlock.Attributes.Add("style", "display:block");
                divPhy.Attributes.Add("style", "display:block");
                divVill.Attributes.Add("style", "display:block");
                GV_AnnualPlan.DataSource = null;
                GV_AnnualPlan.DataBind();
                GVMain.DataSource = null;
                GVMain.DataBind();
                               
            }
           

       
    }
    protected void ddlBlock_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillCBCluster();
        GVMain.DataSource = null;
        GVMain.DataBind();
        GV_AnnualPlan.DataSource = null;
        GV_AnnualPlan.DataBind();
       // ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "ddlTypeOnChangeEvent()", true);
    }
    protected void ddlPanchayat_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillCVillage();
        GVMain.DataSource = null;
        GVMain.DataBind();
        GV_AnnualPlan.DataSource = null;
        GV_AnnualPlan.DataBind();
        //ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "ddlTypeOnChangeEvent()", true);
    }
    protected void ddlVillage_SelectedIndexChanged(object sender, EventArgs e)
    {
        //ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "ddlTypeOnChangeEvent()", true);
        // FillSchool();
    }
    protected void ddlYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlYear.SelectedIndex > 0)
        {
            ddlState.SelectedIndex = 1;
            ddlState_SelectedIndexChanged(ddlDistrict, null);
            if (Session["user_level_Role"].ToString() == "3" || Session["user_level_Role"].ToString() == "4")
            {
                ddlDistrict.SelectedIndex = 1;
                ddlDistrict_SelectedIndexChanged(ddlDistrict, null);
            }

            ddlPanchayat.Items.Clear();
            ddlVillage.Items.Clear();
        }
        else
        {
            ddlState.SelectedIndex = 0;
            ddlDistrict.Items.Clear();
            ddlBlock.Items.Clear();
            ddlPanchayat.Items.Clear();
            ddlVillage.Items.Clear();
        }
        //ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "ddlTypeOnChangeEvent()", true);
    }

    #endregion
    #region Save
    public void SaveData()
    {
        try
        {
            bool InsertTSEnroll = false;
            string SchoolId = Convert.ToString(ViewState["SchoolId"]);
            string Vilagecode = Convert.ToString(ViewState["VillageCode"]); ;
            if (ddlType.SelectedValue=="3")
            {
                //string StudentTSInsertQuery1 = " delete from  tblAnualPlanDataDetailAgp where schoolCode ='" + SchoolId + "' and PlanType=3 ";
                //bool InsertTS = objMain.AddUpdate(StudentTSInsertQuery1);
            }
            else if (ddlType.SelectedValue == "2")
            {
                string ggg = "";
             
                //string StudentTSInsertQuery1 = " delete from  tblAnualPlanDataDetailAgp where Villagecode ='" + Vilagecode + "'  and PlanType=2 ";
                //bool InsertTS = objMain.AddUpdate(StudentTSInsertQuery1);
            }
            else if (ddlType.SelectedValue == "1")
            {
                string ggg="";
               
                //string StudentTSInsertQuery1 = " delete from  tblAnualPlanDataDetailAgp where DistrictCode ='" + ddlDistrict.SelectedValue + "' and PlanType=1 ";
                //bool InsertTS = objMain.AddUpdate(StudentTSInsertQuery1);
            }
            string UniqueID = "";
            UniqueID = objComman.Generate_RandomStringAnu(8);


            for (int i = 0; i < GV_AnnualPlan.Rows.Count; i++)
            {

                Label Description1 = (Label)GV_AnnualPlan.Rows[i].FindControl("LblDesc");
                string Description = Convert.ToString(Description1.Text);
                Label dLookCode = (Label)GV_AnnualPlan.Rows[i].FindControl("LblLookUp");
                string LookCode = Convert.ToString(dLookCode.Text);
                TextBox dApr = (TextBox)GV_AnnualPlan.Rows[i].FindControl("TxtApr");
                string Apr = Convert.ToString(dApr.Text);
                TextBox dMay = (TextBox)GV_AnnualPlan.Rows[i].FindControl("TxtMay");
                string May = Convert.ToString(dMay.Text);
                TextBox dJun = (TextBox)GV_AnnualPlan.Rows[i].FindControl("TxtJun");
                string Jun = Convert.ToString(dJun.Text);
                TextBox dJul = (TextBox)GV_AnnualPlan.Rows[i].FindControl("TxtJul");
                string Jul = Convert.ToString(dJul.Text);
                TextBox dAug = (TextBox)GV_AnnualPlan.Rows[i].FindControl("TxtAug");
                string Aug = Convert.ToString(dAug.Text);
                TextBox dSep = (TextBox)GV_AnnualPlan.Rows[i].FindControl("TxtSep");
                string Sep = Convert.ToString(dSep.Text);
                TextBox dOct = (TextBox)GV_AnnualPlan.Rows[i].FindControl("TxtOct");
                string Oct = Convert.ToString(dOct.Text);
                TextBox dNov = (TextBox)GV_AnnualPlan.Rows[i].FindControl("TxtNov");
                string Nov = Convert.ToString(dNov.Text);
                TextBox dDec = (TextBox)GV_AnnualPlan.Rows[i].FindControl("TxtDec");
                string Dec = Convert.ToString(dDec.Text);
                TextBox dJan = (TextBox)GV_AnnualPlan.Rows[i].FindControl("TxtJan");
                string Jan = Convert.ToString(dJan.Text);
                TextBox dFeb = (TextBox)GV_AnnualPlan.Rows[i].FindControl("TxtFeb");
                string Feb = Convert.ToString(dFeb.Text);
                TextBox dMar = (TextBox)GV_AnnualPlan.Rows[i].FindControl("TxtMar");
                string Mar = Convert.ToString(dMar.Text);


                //Label lblStartMonth = (Label)GV_AnnualPlan.Rows[i].FindControl("lblStartMonth");
                //string StartMonth = Convert.ToString(lblStartMonth.Text);
                //Label lblEndMonth = (Label)GV_AnnualPlan.Rows[i].FindControl("lblEndMonth");
                //string EndMonth = Convert.ToString(lblEndMonth.Text);

                //Label lblMaxVal = (Label)GV_AnnualPlan.Rows[i].FindControl("lblMaxVal");
                //string MaxVal = Convert.ToString(lblMaxVal.Text);

                Label lblLookupType = (Label)GV_AnnualPlan.Rows[i].FindControl("lblLookupType");
                string LookupType = Convert.ToString(lblLookupType.Text);

                Int32 Apr1 = 0; Int32 May1 = 0; Int32 Jun1 = 0; Int32 Jul1 = 0; Int32 Aug1 = 0; Int32 Sep1 = 0; Int32 Oct1 = 0; Int32 Nov1 = 0; Int32 Dec1 = 0; Int32 Jan1 = 0; Int32 Feb1 = 0; Int32 Mar1 = 0;


                if (Apr != "")
                {
                    Apr1 = Convert.ToInt32(Apr);
                }
                if (May != "")
                {
                    May1 = Convert.ToInt32(May);
                }
                if (Jun != "")
                {
                    Jun1 = Convert.ToInt32(Jun);
                }
                if (Jul != "")
                {
                    Jul1 = Convert.ToInt32(Jul);
                }
                if (Aug != "")
                {
                    Aug1 = Convert.ToInt32(Aug);
                }

                if (Sep != "")
                {
                    Sep1 = Convert.ToInt32(Sep);
                }
                if (Oct != "")
                {
                    Oct1 = Convert.ToInt32(Oct);
                }
                if (Nov != "")
                {
                    Nov1 = Convert.ToInt32(Nov);
                }
                if (Dec != "")
                {
                    Dec1 = Convert.ToInt32(Dec);
                }

                if (Jan != "")
                {
                    Jan1 = Convert.ToInt32(Jan);
                }

                if (Feb != "")
                {
                    Feb1 = Convert.ToInt32(Feb);
                }
                if (Mar != "")
                {
                    Mar1 = Convert.ToInt32(Mar);
                }
                //string StudentTSInsertQuery = " INSERT INTO tblAnualPlanDataDetailAgp([AnnualPlanGUID],[Description],SchoolCode,VillageCode,Myear,[RowNo],[Apr],[May],[Jun],[Jul],[Aug],[Sep],[Oct],[Nov],[Dec],[Jan],Feb,[Mar],Createdate,CreateBy,[PlanType],[DistrictCode],LookupType)Values('" + UniqueID + "','" + Description + "','" + SchoolId + "','" + Vilagecode + "','" + ddlYear.SelectedValue + "','" + LookCode + "'," + Apr1 + "," + May1 + "," + Jun1 + "," + Jul1 + "," + Aug1 + "," + Sep1 + "," + Oct1 + "," + Nov1 + "," + Dec1 + "," + Jan1 + "," + Feb1 + "," + Mar1 + ",'" + DateTime.Now.ToString("yyyy-MM-dd") + "','" + Convert.ToString(Session["username"]) + "','" + ddlType.SelectedValue + "','" + ddlDistrict.SelectedValue + "' ,'" + LookupType + "')";
                //InsertTSEnroll = objMain.AddUpdate(StudentTSInsertQuery);


            }
            if (InsertTSEnroll == true)
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Saved sucessfully')</script>", false);

            }

        }
        catch (Exception)
        {

            throw;
        }

    }
    #endregion
}