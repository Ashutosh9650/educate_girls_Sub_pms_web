using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;
using ClosedXML.Excel;   // NuGet: ClosedXML  (used for template download + upload parse)
using ExcelDataReader;

public partial class GkpSchoolMasterUpdate : Page
{
    clsMain objMain = new clsMain();
    Comman objComman = new Comman();
    public bool vADD = false;
    public bool vVerify = false;
    public bool vDelete = false;
    public bool edit_status = false;
    string conditions = "";
    string flag = "";
    Password objPass = new Password();
    public DataTable dtUserDeatils;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadYear();
            LoadUserLeavel();
        }
    }

    public void LoadYear()
    {

        DataTable dtYear = objComman.Generate_Financial_Year();
        objComman.BindDLLMasterTable("mstSchool", "Type,ID", dtYear, conditions, "Type", "asc", ddlYear, "Type", "ID", "Select");

        ddlYear.SelectedIndex = 1;
        //}


    }

    protected void ddlYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlYear.SelectedIndex > 0)
        {
            AlllStateCode();
            ddlState.SelectedIndex = 1;
            ddlState_SelectedIndexChanged(ddlDistrict, null);
            if (Session["user_level_Role"].ToString() == "3" || Session["user_level_Role"].ToString() == "4")
            {
                ddlDistrict.SelectedIndex = 1;
                ddlDistrict_SelectedIndexChanged(ddlDistrict, null);
            }

            ddlPanchayat.Items.Clear();
      
        }
        else
        {
            ddlState.SelectedIndex = 0;
            ddlDistrict.Items.Clear();
            ddlBlock.Items.Clear();
            ddlPanchayat.Items.Clear();
           
        }
        
    }
    public void AlllStateCode()
    {
        conditions = "";
        if (Session["user_level_Role"].ToString() == "1")
        {
            SqlParameter[] par1 = new SqlParameter[]
               {
                      new SqlParameter("@user_level_Role",  Convert.ToString(Session["user_level_Role"])),
                      new SqlParameter("@UserName", "" ),
                    new SqlParameter("@StateCode",  ""),
                       new SqlParameter("@Year",  ddlYear.SelectedValue),
               };
            DataTable dtAllState = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "rptLoadAllState", par1);
            objComman.BindDLLDatatable("mst1State", dtAllState, "StateCode, dbo.TitleCase(upper(StateName)) as StateName", conditions, "StateName", "Desc", ddlState, "StateName", "StateCode", "--Select--");

        }
        else if (Session["user_level_Role"].ToString() == "2")
        {

            SqlParameter[] par1 = new SqlParameter[]
               {
                      new SqlParameter("@user_level_Role",  Convert.ToString(Session["user_level_Role"])),
                      new SqlParameter("@UserName", Convert.ToString(Session["username"]) ),
                    new SqlParameter("@StateCode",  ""),
                       new SqlParameter("@Year",  ddlYear.SelectedValue),
               };
            DataTable dtAllState = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "rptLoadAllState", par1);
            objComman.BindDLLDatatable("mst1State", dtAllState, "StateCode, dbo.TitleCase(upper(StateName)) as StateName", conditions, "StateName", "Desc", ddlState, "StateName", "StateCode", "--Select--");

        }
        else
        {
            SqlParameter[] par1 = new SqlParameter[]
                  {
                      new SqlParameter("@user_level_Role",  Convert.ToString(Session["user_level_Role"])),
                      new SqlParameter("@UserName", Convert.ToString(Session["username"]) ),
                    new SqlParameter("@StateCode", Convert.ToString(Session["StateCode"]) ),
                       new SqlParameter("@Year",  ddlYear.SelectedValue),
                  };
            DataTable dtAllState = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "rptLoadAllState", par1);
            objComman.BindDLLDatatable("mst1State", dtAllState, "StateCode, dbo.TitleCase(upper(StateName)) as StateName", conditions, "StateName", "Desc", ddlState, "StateName", "StateCode", "--Select--");


        }

    }
    public void LoadUserLeavel()
    {
        AlllStateCode();
        conditions = "";
        if (Session["user_level_Role"].ToString() == "1")
        {
            //conditions = "StateCode='" + Session["StateCode"].ToString() + "' ";
            //objComman.BindDLL("mst1State", "StateCode, dbo.TitleCase(upper(StateName)) as StateName", conditions, "StateName", "Desc", ddlState, "StateName", "StateCode", "--Select--");
            ddlState.Enabled = true;
            ddlDistrict.Enabled = true;
        }
        else if (Session["user_level_Role"].ToString() == "2")
        {
            //conditions = "StateCode='" + Session["StateCode"].ToString() + "' ";
            //objComman.BindDLL("mst1State", "StateCode, dbo.TitleCase(upper(StateName)) as StateName", conditions, "StateName", "Desc", ddlState, "StateName", "StateCode", "--Select--");

            ddlState.SelectedIndex = 0;
            ddlState.Enabled = true;
            ddlDistrict.Enabled = true;
        }
        else
        {
            //conditions = "StateCode='" + Session["StateCode"].ToString() + "' ";
            //objComman.BindDLL("mst1State", "StateCode, dbo.TitleCase(upper(StateName)) as StateName", conditions, "StateName", "Desc", ddlState, "StateName", "StateCode", "--Select--");

            ddlState.SelectedIndex = 1;
            ddlState.Enabled = false;
            ddlDistrict.Enabled = false;
        }


        if (Session["user_level_Role"].ToString() == "1")
        {
        }
        else if (Session["user_level_Role"].ToString() == "2")
        {
            conditions = "";
            //conditions = "StateCode ='" + ddlState.SelectedValue + "'  and Fyear= '" + ddlYear.SelectedItem.Text + "'  ";
            //objComman.BindDLL("mst2District", "DistrictCode,dbo.TitleCase(upper(DistrictName)) as DistrictName", conditions, "DistrictName", "asc", ddlDistrict, "DistrictName", "DistrictCode", "--Select--");

            ddlDistrict.SelectedIndex = 0;

            //ddlDistrict.SelectedIndex = 1;
            //conditions = "";
            //conditions = "DistrictCode ='" + ddlDistrict.SelectedValue + "' and BlockCode ='" + Session["BlockCode"].ToString() + "' ";
            //objComman.BindDLL("mst3Block", "BlockCode,dbo.TitleCase(upper(BlockName)) as BlockName  ", conditions, "BlockName", "asc", ddlBlock, "BlockName", "BlockCode", "--Select--");

        }

        else
        {
            conditions = "";
            conditions = "StateCode ='" + ddlState.SelectedValue + "' and DistrictCode in(" + Session["DistrictCode"].ToString() + ") and Fyear= '" + ddlYear.SelectedItem.Text + "' ";
            objComman.BindDLL("mst2District", "DistrictCode,dbo.TitleCase(upper(DistrictName)) as DistrictName", conditions, "DistrictName", "asc", ddlDistrict, "DistrictName", "DistrictCode", "--Select--");
            string strQry;
            strQry = "Select * from mst2District where   DistrictCode in(" + Session["DistrictCode"].ToString() + ")";
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
            //objComman.BindDLL("mst3Block", "BlockCode,dbo.TitleCase(upper(BlockName)) as BlockName  ", conditions, "BlockName", "asc", ddlBlock, "BlockName", "BlockCode", "--Select--");

        }





    }

    public void FillCBState()
    {
        conditions = "";
        objComman.BindDLL("mst1State", "StateCode,dbo.TitleCase(upper(StateName)) as StateName ", conditions, "StateName", "asc", ddlState, "StateName", "StateCode", "--Select--");



    }
    public void FillCBDist()
    {

        conditions = "";
        if (Session["user_level_Role"].ToString() == "1")
        {

            conditions = "StateCode ='" + ddlState.SelectedValue + "' and mst2District.FYear ='" + ddlYear.SelectedItem.Text + "'";
        }
        else if (Session["user_level_Role"].ToString() == "2")
        {
            conditions = "StateCode ='" + ddlState.SelectedValue + "'  and mst2District.FYear ='" + ddlYear.SelectedItem.Text + "'";
        }
        else
        {
            conditions = "StateCode ='" + ddlState.SelectedValue + "' and DistrictCode in(" + Session["DistrictCode"].ToString() + ") and mst2District.FYear ='" + ddlYear.SelectedItem.Text + "' ";


        }
        if (Session["user_level_Role"].ToString() == "2")
        {
            conditions = "";
            conditions = " mst2District.StateCode ='" + ddlState.SelectedValue + "' and UserName='" + Session["username"].ToString() + "' ";
            string strQry1 = "       sELECT distinct mst2District.DistrictCode as DistrictCode, dbo.TitleCase(upper(DistrictName))  as DistrictName FROM MstusermultipleDist  ";
            strQry1 += " inner join mst2District on mst2District.OldDistrictCode=MstusermultipleDist.OldDistrictCode  where " + conditions + "  and  Fyear='" + ddlYear.SelectedItem.Text + "' order by DistrictName  ";
            DataTable dtDistrict = objMain.LoadData(strQry1);

            objComman.BindDLLDatatable("mst2District", dtDistrict, "DistrictCode, dbo.TitleCase(upper(DistrictName)) as DistrictName", conditions, "DistrictName", "Desc", ddlDistrict, "DistrictName", "DistrictCode", "--Select--");

        }
        else
        {

            objComman.BindDLL("mst2District", "DistrictCode,dbo.TitleCase(upper(DistrictName)) as DistrictName ", conditions, "DistrictName", "asc", ddlDistrict, "DistrictName", "DistrictCode", "Select");

        }

    }





    protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillCBDist();
      
    }
    protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
       
        FillCBBock();
        LockIapproval();
    }
    protected void ddlBlock_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillCBCluster();
        
    }
    protected void ddlPanchayat_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillSchoolr();

    }
    public void FillSchoolr()
    {
        conditions = "";
        conditions = "mst5village.DistrictCode ='" + ddlDistrict.SelectedValue + "' and GKPVal=1   and mst5village.BlockCode ='" + ddlBlock.SelectedValue + "' and mst5village.ClusterCode ='" + ddlPanchayat .SelectedValue + "'";
        BindDLLSelectAll("mstSchool inner join mst5village on mst5village.villagecode =mstSchool.villagecode", "Schoolcode,dbo.TitleCase(upper(Name)) as Name", conditions, "Name", "asc", ddlschool, "Name", "Schoolcode", "Select");



    }
    public void FillCBCluster()
    {
        conditions = "";
        conditions = "DistrictCode ='" + ddlDistrict.SelectedValue + "'  and BlockCode ='" + ddlBlock.SelectedValue + "'";
        BindDLLSelectAll("mstCluster", "ClusterCode,dbo.TitleCase(upper(ClusterName)) as ClusterName", conditions, "ClusterName", "asc", ddlPanchayat, "ClusterName", "ClusterCode", "Select");



    }
    public DataTable LoadData(string Query)
    {
        DataTable dtcombo = new DataTable();
        try
        {
            dtcombo = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.Text, Query);


        }
        catch (Exception ex)
        {
            //string mmsg = ex.Message; showMessages(mmsg);
            //showMessages("(SelectAllData)  " + mmsg);
        }
        return dtcombo;
    }
    public bool BindDLLSelectAll(string dtname, string fieldname, string Condition, string orberbyfield, string orderby, DropDownList ddl, string textData, string valData, string ZeroIndex)
    {
        bool status = false;
        string conditions = Condition == "" ? "" : " where " + Condition;
        string orberbyfields = orberbyfield == "" ? "" : " order by " + orberbyfield;
        string orderbys = orderby == "" ? "" : orderby;


        string strQry = "Select  distinct " + fieldname + " from " + dtname + " " + conditions + " " + orberbyfields + " " + orderbys + "";
        DataTable dt = LoadData(strQry);
        if (ZeroIndex != "")
        {
            DataRow dr;
            dr = dt.NewRow();
            dr[textData] = "--" + ZeroIndex + "--";
            dr[valData] = "0";
            dt.Rows.InsertAt(dr, 0);

            //if (dt.Rows.Count > 0)
            //{
            //    dr = dt.NewRow();
            //    dr[textData] = "--" + "All" + "--";
            //    dr[valData] = "1";
            //    dt.Rows.InsertAt(dr, 1);
            //    dt.AcceptChanges();
            //}
        }
        if (dt.Rows.Count > 0)
        {
            ddl.DataTextField = textData;
            ddl.DataValueField = valData;

            ddl.DataSource = dt;
            ddl.DataBind();
            status = true;
        }
        return status;

    }


    public void FillCBBock()
    {
        conditions = "";
        if (Session["user_level_Role"].ToString() == "1")
        {

            conditions = "DistrictCode ='" + ddlDistrict.SelectedValue + "'  ";
        }
        else if (Session["user_level_Role"].ToString() == "2")
        {
            conditions = "DistrictCode ='" + ddlDistrict.SelectedValue + "'  ";
        }
        else if (Session["user_level_Role"].ToString() == "4")
        {
            conditions = "DistrictCode ='" + ddlDistrict.SelectedValue + "'and BlockCode in( " + Session["BlockCode"].ToString() + " ) and FYear ='" + ddlYear.SelectedItem.Text + "' ";
        }
        else if (Session["user_level_Role"].ToString() == "6")
        {
            conditions = " BlockCode in( " + Session["blockCodeMul"].ToString() + " ) and FYear ='" + ddlYear.SelectedItem.Text + "' ";
        }
        else
        {
            conditions = "DistrictCode ='" + ddlDistrict.SelectedValue + "' ";
        }
        objComman.BindDLL("mst3Block", "BlockCode,dbo.TitleCase(upper(BlockName)) as BlockName ", conditions, "BlockName", "asc", ddlBlock, "BlockName", "BlockCode", "--Select--");



    }

    protected void btnNewImport_Click(object sender, EventArgs e)
    {

        if (ddlDistrict.SelectedIndex <= 0)
        {

            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select District ')</script>", false);
            return;
        }
        conditions = "";
        if (ddlYear.SelectedIndex > 0)
        {
            conditions += "  v.Fyear = '" + ddlYear.SelectedItem.Text + "' ";

        }
        if (ddlState.SelectedIndex > 0)
        {
            conditions += " and  v.StateCode = '" + ddlState.SelectedValue + "' ";

        }
        if (ddlDistrict.SelectedIndex > 0)
        {
            conditions += " and v.DistrictCode = '" + ddlDistrict.SelectedValue + "' ";

        }
        //if (ddlBlock.SelectedIndex > 0)
        //{
        //    conditions += " and v.Blockcode = '" + ddlBlock.SelectedValue + "' ";

        //}
        //if (ddlPanchayat.SelectedIndex > 0)
        //{
        //    conditions += " and v.ClusterCode = '" + ddlPanchayat.SelectedValue + "' ";

        //}
        DataSet dt = LoadMasterImport(conditions);

        MultipuExeclTrack(dt);
    }


    public void LoadData()
    {
        if (ddlDistrict.SelectedIndex <= 0)
        {

            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select District ')</script>", false);
            return;
        }
        conditions = "";
        if (ddlYear.SelectedIndex > 0)
        {
            conditions += "  v.Fyear = '" + ddlYear.SelectedItem.Text + "' ";

        }
        if (ddlState.SelectedIndex > 0)
        {
            conditions += " and  v.StateCode = '" + ddlState.SelectedValue + "' ";

        }
        if (ddlDistrict.SelectedIndex > 0)
        {
            conditions += " and v.DistrictCode = '" + ddlDistrict.SelectedValue + "' ";

        }
        if (ddlBlock.SelectedIndex > 0)
        {
            conditions += " and v.Blockcode = '" + ddlBlock.SelectedValue + "' ";

        }
        if (ddlPanchayat.SelectedIndex > 0)
        {
            conditions += " and v.ClusterCode = '" + ddlPanchayat.SelectedValue + "' ";

        }
        if (ddlschool.SelectedIndex > 0)
        {
            conditions += " and schoolcode = '" + ddlschool.SelectedValue + "' ";

        }
        DataTable dt = LoadMasterImportLoad(conditions);
        if (dt.Rows.Count > 0)
        {
            gvGkpMaster.DataSource = dt;
            gvGkpMaster.DataBind();
        }
        else
        {
            gvGkpMaster.DataSource = null;
            gvGkpMaster.DataBind();
        }
    
        Session["GridViewDataGKP"] = dt;
    }

    public void UpdateData()
    {
        DataTable dt = Session["GridViewDataGKP"] as DataTable;


        for (int i = 0; i < gvGkpMaster.Rows.Count; i++)
        {



            DropDownList ddlAssessmentType = (DropDownList)gvGkpMaster.Rows[i].FindControl("ddlAssessmentType");
            DropDownList ddlWorkingHindi = (DropDownList)gvGkpMaster.Rows[i].FindControl("ddlWorkingHindi");
            DropDownList ddlEnglish = (DropDownList)gvGkpMaster.Rows[i].FindControl("ddlEnglish");
            DropDownList ddMath = (DropDownList)gvGkpMaster.Rows[i].FindControl("ddMath");
            Label lblSchoolcode = (Label)gvGkpMaster.Rows[i].FindControl("lblSchoolcode");

            HiddenField hdnUpdated = (HiddenField)gvGkpMaster.Rows[i].FindControl("hdnUpdated");

                DataRow[] dr = dt.Select("Schoolcode='" + Convert.ToString(lblSchoolcode.Text) + "'");
                if (dr.Length > 0)
                {

                    dr[0]["TempID"] = hdnUpdated.Value;
                    dr[0]["AssessmentTypeID"] = ddlAssessmentType.SelectedValue;
                    dr[0]["GKPLevelHindi"] = ddlWorkingHindi.SelectedValue;


                    dr[0]["GKPLevelMath"] = ddlEnglish.SelectedValue;
                    dr[0]["GKPLevelEnglish"] = ddMath.SelectedValue;
                    
                }

         



        }
        Session["GridViewDataGKP"] = dt;

    }

    protected void btnsave_Click(object sender, EventArgs e)
    {

        if (Session["GridViewDataGKP"] != null)
        {
            UpdateData();
            int ret = 0;
            DataTable Dt = Session["GridViewDataGKP"] as DataTable;
            for (int i = 0; i < Dt.Rows.Count; i++)
            {



                string Schoolcode = Dt.Rows[i]["Schoolcode"].ToString();
                string GKPLevelHindi = Dt.Rows[i]["GKPLevelHindi"].ToString();
                string GKPLevelMath = Dt.Rows[i]["GKPLevelMath"].ToString();
                string GKPLevelEnglish = Dt.Rows[i]["GKPLevelEnglish"].ToString();
                string AssessmentTypeID = Dt.Rows[i]["AssessmentTypeID"].ToString();

                string TempID = Dt.Rows[i]["TempID"].ToString();

                if (AssessmentTypeID=="0" || GKPLevelHindi == "0" || GKPLevelMath == "0" || GKPLevelEnglish == "0")
                {
                    if (AssessmentTypeID == "0")
                    {
                        ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select Assessment Type ')</script>", false);
                        return;
                    }
                    if(GKPLevelHindi == "0")
                    {
                        ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select GKP Level Hindi')</script>", false);
                        return;
                    }
                    if (GKPLevelMath == "0")
                    {
                        ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select GKP Level Math')</script>", false);
                        return;
                    }
                    if (GKPLevelEnglish == "0")
                    {
                        ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select GKP Level English ')</script>", false);
                        return;
                    }
                    // ret = Update_VillageCluster(VillageCode, ClusterCode, VillageGeography, VillageOperational, CBlVillage, FunctionalStatus, AGPStatus, TeacherContactNo, PanchayatSamiti);


                }



            }
            // DataRow[] dr = Dt.Select(Cond);
            for (int i = 0; i < Dt.Rows.Count; i++)
            {
                
               

                    string Schoolcode = Dt.Rows[i]["Schoolcode"].ToString();
                    string GKPLevelHindi = Dt.Rows[i]["GKPLevelHindi"].ToString();
                    string GKPLevelMath = Dt.Rows[i]["GKPLevelMath"].ToString();
                    string AssessmentTypeID = Dt.Rows[i]["AssessmentTypeID"].ToString();
                string GKPLevelEnglish = Dt.Rows[i]["GKPLevelEnglish"].ToString(); 
                string TempID = Dt.Rows[i]["TempID"].ToString();
                  
                    if (TempID == "1")
                    {

                    SqlParameter[] cmdParameters = new SqlParameter[]
                     {
                            new SqlParameter("@Schoolcode",Schoolcode),
                            new SqlParameter("@GKPLevelHindi", GKPLevelHindi),
                            new SqlParameter("@GKPLevelMath", GKPLevelMath),
                            new SqlParameter("@GKPLevelEnglish", GKPLevelEnglish),
                            new SqlParameter("@AssessmentTypeID", AssessmentTypeID),
                            
                             new SqlParameter("@UserName", Convert.ToString(Session["username"])),
                             


                     };
                    ret = SqlHelper.ExecuteNonQuery(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "InsertUpdateMasterGKP", cmdParameters);

                   }
                


            }

            if (ret > 0)
            { 
                int icount = 0;
                            SqlParameter[] cmdParameters = new SqlParameter[]
                           {
                    new SqlParameter("@DistrictCode", ddlDistrict.SelectedValue),
                    new SqlParameter("@approveStataus", "0"),
                    new SqlParameter("@Remark", ""),
                     new SqlParameter("@UserName", Convert.ToString(Session["username"])),
                       new SqlParameter("@Flag", "1"),
             


                           };
                    icount = SqlHelper.ExecuteNonQuery(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "InsertUpdatefMasterGkpFinalApproveSave", cmdParameters);
                    LockIapproval();
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Saved sucessfully')</script>", false);
            }
        }
    }
    protected void GV_Cluster_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        //UpdateData();
        gvGkpMaster.PageIndex = e.NewPageIndex;
        if (Session["GridViewDataGKP"] != null)
        {
            DataTable dt = Session["GridViewDataGKP"] as DataTable;
            gvGkpMaster.DataSource = dt;
            gvGkpMaster.DataBind();
        }


    }
    protected void GV_luster_OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

                DropDownList ddlAssessmentType = (DropDownList)e.Row.FindControl("ddlAssessmentType");
                DropDownList ddlWorkingHindi = (DropDownList)e.Row.FindControl("ddlWorkingHindi");
                DropDownList ddlEnglish = (DropDownList)e.Row.FindControl("ddlEnglish");
                DropDownList ddMath = (DropDownList)e.Row.FindControl("ddMath");



                Label lblGKPLevelHindi = (Label)e.Row.FindControl("lblGKPLevelHindi");
                Label lblGKPLevelMath = (Label)e.Row.FindControl("lblGKPLevelMath");
                Label lblGKPLevelEnglish = (Label)e.Row.FindControl("lblGKPLevelEnglish");

                Label AssessmentTypeID = (Label)e.Row.FindControl("lblAssessmentTypeID");

            ddlAssessmentType.SelectedValue = AssessmentTypeID.Text;
            ddlWorkingHindi.SelectedValue = lblGKPLevelHindi.Text;
            ddlEnglish.SelectedValue = lblGKPLevelEnglish.Text;
            ddMath.SelectedValue = lblGKPLevelMath.Text;
                           

        }
        
    }
    public DataTable LoadMasterImportLoad(string Frist)
    {
        SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("@con", Frist),
            new SqlParameter("@Fyear", ddlYear.SelectedItem.Text),
                                    new SqlParameter("@DistrictCode", ddlDistrict.SelectedValue),
                                                                new SqlParameter("@StateCode", ddlState.SelectedValue),
        };
        return SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rptschoolmaster202627]", cmdParameters);
    }
    public DataSet LoadMasterImport(string Frist)
    {
        SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("@con", Frist),
            new SqlParameter("@Fyear", ddlYear.SelectedItem.Text),
                        new SqlParameter("@DistrictCode", ddlDistrict.SelectedValue),
                            new SqlParameter("@StateCode", ddlState.SelectedValue),

        };
        return SqlHelper.GetDataSet(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rptschoolmaster2026]", cmdParameters);
    }

    protected void btnNewImport1_Click(object sender, EventArgs e)
    {
        if (Convert.ToInt32(ddlYear.SelectedValue) != 2026)
        {

            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select Fyear 2026 ')</script>", false);
            return;
        }
        if (FileUpload1.FileName.Length > 0)
        {

        }
        else
        {
            return;
        }

        // need to pass relative path after deploying on server
        string path = System.IO.Path.GetFullPath(Server.MapPath(FileUpload1.FileName));
        
        string sDirectory = Server.MapPath("~/Mou//");

        string FilePath = sDirectory + FileUpload1.FileName;
        FileUpload1.PostedFile.SaveAs(FilePath);
        ViewState["FileName"] = FileUpload1.FileName + "_" + DateTime.Now.ToString("dd_MM_yyyy_hh_mm_ss");


        // Required for .NET Framework

        DataSet ds;

        using (var stream = File.Open(FilePath, FileMode.Open, FileAccess.Read))
        {
            using (var reader = ExcelReaderFactory.CreateReader(stream))
            {
                ds = reader.AsDataSet(new ExcelDataSetConfiguration()
                {
                    ConfigureDataTable = (_) => new ExcelDataTableConfiguration()
                    {
                        UseHeaderRow = true   // 👈 First row as header for ALL sheets
                    }
                });
            }
        }
       
        DataTable dt = ds.Tables[0];
        int Flag = 0;
        string[] requiredColumns =
   {
    "District Name",
    "District Code",
    "Block Name",
    "Block Code",
    "Cluster Name",
    "Cluster Code",
    "Panchayat Name",
    "Panchayat Code",
    "Village Name",
    "Village Code",
    "School Name",
    "Dise Code",
    "Govt. Dise Code",
    "School Level",
    "School Working Status",
     "Last Year GKP Sschool",
    "Assessment Type",
   
    "GKP Level Hindi",
    "GKP Level Math",
    "GKP Level English"
};

        List<string> missingOrChanged = new List<string>();

        // Check column count
        if (dt.Columns.Count != requiredColumns.Length)
        {

            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Invalid Excel template. Please upload the original template')</script>", false);
            return;
          
        }

        // Check each header (name and order)
        for (int i = 0; i < requiredColumns.Length; i++)
        {
            string actualHeader = dt.Columns[i].ColumnName.Trim();

            if (!actualHeader.Equals(requiredColumns[i], StringComparison.OrdinalIgnoreCase))
            {
                missingOrChanged.Add(     string.Format("Column {0}: Expected '{1}', Found '{2}'",     i + 1,     requiredColumns[i],     actualHeader) );
            }
        }

        if (missingOrChanged.Count > 0)
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Invalid Excel template')</script>", false);
            return;
           
   
        }
        dt.Columns.Add("Remark", typeof(string));
        dt.Columns["Remark"].SetOrdinal(0);
        int value;
        foreach (DataRow dr in dt.Rows)
        {
            string Errormsg = "";
            string assessmentType = Convert.ToString(dr["Assessment Type"]).Trim();
            if (string.IsNullOrEmpty(assessmentType))
            {
                Errormsg += "Assessment Type cannot be blank   ";
                Flag = 1;
            }
            else if (!int.TryParse(assessmentType, out value))
            {
                Errormsg += "  Assessment Type must be numeric (1 or 2 only)";
                Flag = 1;

            }
            else if (!int.TryParse(assessmentType, out value) || (value != 1 && value != 2))
            {
                Errormsg += "  Invalid Assessment Type. Only 1 or 2 are allowed.";
                Flag = 1;
            }
            string GKPLevelHindi = Convert.ToString(dr["GKP Level Hindi"]).Trim();
            if (string.IsNullOrEmpty(GKPLevelHindi))
            {
                Errormsg += " GKP Level Hindi cannot be blank";
                Flag = 1;
            }
            else if (!int.TryParse(GKPLevelHindi, out value))
            {
                Errormsg += "  GKP Level Hindi must be numeric (1 or 2 only)";
                Flag = 1;

            }
            else if (!int.TryParse(GKPLevelHindi, out value) || (value != 1 && value != 2 && value != 3))
            {
                Errormsg += "  Invalid GKP Level Hindi. Only 1 or 2 or 3 are allowed.";
                Flag = 1;
            }
            string GKPLevelMath = Convert.ToString(dr["GKP Level Math"]).Trim();
            if (string.IsNullOrEmpty(GKPLevelMath))
            {
                Errormsg += " GKP Level Math cannot be blank";
                Flag = 1;
            }
            else if (!int.TryParse(GKPLevelMath, out value))
            {
                Errormsg += " GKP Level Math must be numeric (1 or 2 only)";
                Flag = 1;

            }
            else if (!int.TryParse(GKPLevelMath, out value) || (value != 1 && value != 2 && value != 3))
            {
                Errormsg += " Invalid GKP Level Math. Only 1 or 2 or 3 are allowed.";
                Flag = 1;
            }
            string GKPLevelEnglish = Convert.ToString(dr["GKP Level English"]).Trim();
            if (string.IsNullOrEmpty(GKPLevelEnglish))
            {
                Errormsg += " GKP Level English cannot be blank";
                Flag = 1;
            }
            else if (!int.TryParse(GKPLevelEnglish, out value))
            {
                Errormsg += " GKP Level English must be numeric (1 or 2 only)";
                Flag = 1;

            }
            else if (!int.TryParse(GKPLevelEnglish, out value) || (value != 1 && value != 2 && value != 3))
            {
                Errormsg += " Invalid GKP Level English. Only 1 or 2 or 3 are allowed.";
                Flag = 1;
            }
            dr["Remark"] = Errormsg;

        }
        if (Flag == 1)
        {

            MultipuExeclTrackError(dt);
            return;
        }
        else
        {
            DataTable bulkTable = new DataTable();

            bulkTable.Columns.Add("DiseCode");
            bulkTable.Columns.Add("AssessmentType", typeof(int));
            bulkTable.Columns.Add("GKPLevelHindi", typeof(int));
            bulkTable.Columns.Add("GKPLevelMath", typeof(int));
            bulkTable.Columns.Add("GKPLevelEnglish", typeof(int));

            foreach (DataRow dr in dt.Rows)
            {
                bulkTable.Rows.Add(
                    dr["Dise Code"].ToString().Trim(),
                    Convert.ToInt32(dr["Assessment Type"]),
                    dr["GKP Level Hindi"].ToString().Trim(),
                    dr["GKP Level Math"].ToString().Trim(),
                    dr["GKP Level English"].ToString().Trim()
                );
            }
            int icount = 0;
            int Parti_Success = Insert_USP_master(bulkTable);
            if (Parti_Success > 0)
            {
                SqlParameter[] cmdParameters15 = new SqlParameter[]
               {
            new SqlParameter("@DistrictCode", ddlDistrict.SelectedValue),
            new SqlParameter("@approveStataus", "0"),
            new SqlParameter("@Remark", ""),
             new SqlParameter("@UserName", Convert.ToString(Session["username"])),
               new SqlParameter("@Flag", "1"),



               };
                icount = SqlHelper.ExecuteNonQuery(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "InsertUpdatefMasterGkpFinalApproveSave", cmdParameters15);

            }
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Data Saved sucessfully')</script>", false);
            LockIapproval();
        }
     
        


    }
    protected void btnSerach_Click(object sender, EventArgs e)
    {

       
        if (ddlBlock.SelectedIndex <= 0)
        {

            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select Block')</script>", false);
            return;
        }
        LoadData();
    }
    protected void btnReject_Click(object sender, EventArgs e)
    {
        ModalPopupExtender3.Show();
    }
    protected void btnsaveReject_Click(object sender, EventArgs e)
    {
        if (Convert.ToString(Session["username"]) != "")
        {
        }
        else
        {
            Response.Redirect("Login.aspx", false);
        }
        if (ddlDistrict.SelectedIndex <= 0)
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Select District')</script>", false);

            return;

        }
        int approveStataus = 0;
        
        if (Convert.ToString(Session["user_level"]) == "91" || Convert.ToString(Session["user_level"]) == "146")
        {
            approveStataus = 3;
        }
        
        int icount = 0;
        SqlParameter[] cmdParameters = new SqlParameter[]
       {
            new SqlParameter("@DistrictCode", ddlDistrict.SelectedValue),
            new SqlParameter("@approveStataus", approveStataus),
            new SqlParameter("@Remark", txtRemark.Text),
             new SqlParameter("@UserName", Convert.ToString(Session["username"])),
               new SqlParameter("@Flag", "2"),
                  new SqlParameter("@user_level",Convert.ToString(Session["user_level"])),


       };
        icount = SqlHelper.ExecuteNonQuery(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "InsertUpdatefMasterGKPFinalApprove", cmdParameters);




        if (icount > 0)
        {
      
            if (Convert.ToString(Session["user_level"]) == "91" || Convert.ToString(Session["user_level"]) == "146")
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Reject Successfully!!')</script>", false);

            }
    

            ddlDistrict_SelectedIndexChanged(ddlDistrict, null);

        }


    }

    protected void btnSubmitted_Click(object sender, EventArgs e)
    {
        if (Convert.ToString(Session["username"]) != "")
        {
        }
        else
        {
            Response.Redirect("Login.aspx", false);
        }
        if (ddlDistrict.SelectedIndex <= 0)
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Select District')</script>", false);

            return;

        }
        int approveStataus = 0;
        int Flag = 0;
        if (Convert.ToString(Session["user_level"]) == "60" || Convert.ToString(Session["user_level"]) == "136")
        {
            SqlParameter[] cmdParameters1 = new SqlParameter[]
             {

                      new SqlParameter("@DistrictCode", ddlDistrict.SelectedValue),
                       new SqlParameter("@Flag",Convert.ToString(Session["user_level"])),

             };
            DataTable dtSchool = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "LoadMasterApprovalStatusGKP", cmdParameters1);
            if (dtSchool.Rows.Count > 0)
            {
               
            }
            approveStataus = 1;
        }
        if (Convert.ToString(Session["user_level"]) == "91" || Convert.ToString(Session["user_level"]) == "146")
        {
            approveStataus = 2;
        }
       
        int icount = 0;
        SqlParameter[] cmdParameters = new SqlParameter[]
       {
            new SqlParameter("@DistrictCode", ddlDistrict.SelectedValue),
            new SqlParameter("@approveStataus", approveStataus),
            new SqlParameter("@Remark", ""),
             new SqlParameter("@UserName", Convert.ToString(Session["username"])),
               new SqlParameter("@Flag", "1"),
                  new SqlParameter("@user_level",Convert.ToString(Session["user_level"])),



       };
        icount = SqlHelper.ExecuteNonQuery(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "InsertUpdatefMasterGKPFinalApprove", cmdParameters);




        if (icount > 0)
        {
            if (Convert.ToString(Session["user_level"]) == "60" || Convert.ToString(Session["user_level"]) == "136")
            {
               
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Data Successfully Submitted to DOL!!')</script>", false);

              
            }
            if (Convert.ToString(Session["user_level"]) == "91" || Convert.ToString(Session["user_level"]) == "146")
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Master Data successfully Lock!!')</script>", false);

            }
       
            ddlDistrict_SelectedIndexChanged(ddlDistrict, null);

        }


    }
    public void LockIapproval()
    {
        if (Convert.ToString(Session["user_level"]) == "60"  || Convert.ToString(Session["user_level"]) == "136")
        { 
            SqlParameter[] cmdParameters = new SqlParameter[]
       {

              new SqlParameter("@DistrictCode", ddlDistrict.SelectedValue),
               new SqlParameter("@Flag",Convert.ToString(Session["user_level"])),

       };
            DataTable dtSchool = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[LoadMasterApprovalStatusGKP]", cmdParameters);
            if (dtSchool.Rows.Count > 0)
            {

                if (dtSchool.Rows[0]["ApproveSatus"].ToString() == "0")
                {
                    btnSubmit.Enabled = true;
                    btnSubmit.Visible = true;
                    btnSubmit.Text = "Submit to DOL";
                    Button2.Visible = true;
                    FileUpload1.Visible = true;

                }
           
                else if (dtSchool.Rows[0]["ApproveSatus"].ToString() == "1")
                {
                    btnSubmit.Enabled = false;
                    btnSubmit.Visible = true;
                    btnsave.Visible = false;
                    Button2.Visible = false;
                    btnSubmit.Text = "Pending DOL Review";

                    FileUpload1.Visible = false;

                }
                else if (dtSchool.Rows[0]["ApproveSatus"].ToString() == "2" )
                {
                    btnSubmit.Enabled = false;
                    btnSubmit.Visible = true;
                    btnsave.Visible = false;
                    Button2.Visible = false;
                    btnSubmit.Text = "Master Data Lock";
                    FileUpload1.Visible = false;

                }

                else
                {
                    btnSubmit.Enabled = true;
                    btnSubmit.Visible = true;
                    btnSubmit.Text = "Submit to DOL";
                    Button2.Visible = true;
                    btnsave.Visible = true;
                    FileUpload1.Visible = false;
                }
            }
            else
            {
              
                btnSubmit.Visible = false;
                btnsave.Visible = true;
                Button2.Visible = true;
                FileUpload1.Visible = true;

            }

        }
     
        else if (Convert.ToString(Session["user_level"]) == "91" || Convert.ToString(Session["user_level"]) == "146")
        {

            SqlParameter[] cmdParameters = new SqlParameter[]
      {

              new SqlParameter("@DistrictCode", ddlDistrict.SelectedValue),
               new SqlParameter("@Flag",Convert.ToString(Session["user_level"])),

      };
            DataTable dtSchool = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[LoadMasterApprovalStatusGKP]", cmdParameters);
            if (dtSchool.Rows.Count > 0)
            {
                if (dtSchool.Rows[0]["ApproveSatus"].ToString() == "1")
                {
                    btnSubmit.Enabled = true;
                    btnSubmit.Visible = true;
                    btnsave.Visible = false;
                    Button2.Visible = false;
                    // btnSubmit.Text = "Submitted to DOL for Approval";
                    btnSubmit.Text = "Approve";
                    btnReject.Visible = true;
                    FileUpload1.Visible = false;
                }
                else if (dtSchool.Rows[0]["ApproveSatus"].ToString() == "2" )
                {
                    btnSubmit.Enabled = false;
                    btnSubmit.Visible = true;
                    btnsave.Visible = false;
                    Button2.Visible = false;
                    btnSubmit.Text = "Master GKP Data Lock";
                    btnReject.Visible = false;
                    FileUpload1.Visible = false;
                }
               
                else
                {
                    btnSubmit.Enabled = true;
                    btnSubmit.Visible = false;
                    btnsave.Visible = false;
                    btnReject.Visible = false;
                    FileUpload1.Visible = false;
                }
            }
            else
            {
                btnSubmit.Enabled = true;
                btnSubmit.Visible = false;
                btnsave.Visible = false;
                FileUpload1.Visible = false;
                btnReject.Visible = false;
            }
        }
        else
        {
            btnSubmit.Visible = false;
            btnsave.Visible = false;
            btnReject.Visible = false;
            Button2.Visible = false;
            FileUpload1.Visible = false;
        }
      }

    public int Insert_USP_master(DataTable tbl_Tarining_Participarticipate)
    {
        DataTable dtcombo = new DataTable();

        SqlParameter[] cmdParameters = new SqlParameter[]
    {
            new SqlParameter("@CreateBy", Convert.ToString(Session["username"] )),
             new SqlParameter("@Fyear", ddlYear.SelectedItem.Text),
            new SqlParameter("@mstschoolmasterGKP", tbl_Tarining_Participarticipate)
    };
        return SqlHelper.ExecuteNonQuery(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "USP_Schoolmaster", cmdParameters);

    }
    public void MultipuExeclTrackError(DataTable dt)
    {
        try
        {
            string StartupPath = Server.MapPath("~/Export");
            string filepath = "";
            XLWorkbook wb = new XLWorkbook();
            wb = new XLWorkbook(StartupPath + "\\GKPMasterError.xlsx");
            var ws = wb.Worksheet(1);




            ws.Cell(2, 1).InsertData(dt.Rows);
            Int32 ii = Convert.ToInt32(dt.Rows.Count) + 1;
            string str = "A2:U" + ii;
            ws.Range(str).Style.Border.SetTopBorder(XLBorderStyleValues.Thin);
            ws.Range(str).Style.Border.SetBottomBorder(XLBorderStyleValues.Thin);

            ws.Range(str).Style.Border.SetLeftBorder(XLBorderStyleValues.Thin);
            ws.Range(str).Style.Border.SetRightBorder(XLBorderStyleValues.Thin);





            filepath = StartupPath + "\\GKPMasterError" + "_" + System.DateTime.Now.ToString("hhssmmfff") + ".xlsx";
            wb.SaveAs(filepath);
            Response.ContentType = ContentType;
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(filepath));
            Response.WriteFile(filepath);

            Response.End();
            if (File.Exists(filepath))
            {
                System.IO.File.Delete(filepath);
            }
        }
        catch (Exception ex)
        {

        }

    }
    public void MultipuExeclTrack(DataSet dtMain)
    {
        try
        {
            string StartupPath = Server.MapPath("~/Export");
            string filepath = "";
            XLWorkbook wb = new XLWorkbook();
            wb = new XLWorkbook(StartupPath + "\\GKPMaster.xlsx");
            var ws = wb.Worksheet(1);
           

            DataTable dt = dtMain.Tables[0];
   
            ws.Cell(2, 1).InsertData(dt.Rows);
            Int32 ii = Convert.ToInt32(dt.Rows.Count) + 1;
            string str = "A2:T" + ii;
            ws.Range(str).Style.Border.SetTopBorder(XLBorderStyleValues.Thin);
            ws.Range(str).Style.Border.SetBottomBorder(XLBorderStyleValues.Thin);

            ws.Range(str).Style.Border.SetLeftBorder(XLBorderStyleValues.Thin);
            ws.Range(str).Style.Border.SetRightBorder(XLBorderStyleValues.Thin);





            filepath = StartupPath + "\\GKPMaster" + "_" + System.DateTime.Now.ToString("hhssmmfff") + ".xlsx";
            wb.SaveAs(filepath);
            Response.ContentType = ContentType;
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(filepath));
            Response.WriteFile(filepath);

            Response.End();
            if (File.Exists(filepath))
            {
                System.IO.File.Delete(filepath);
            }
        }
        catch (Exception ex)
        {

        }

    }

}
