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
using System.Linq;
using System.Web;

public partial class GkpBOView : Page
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
            //exec rptGKgrad
            LoadYear();
            LoadUserLeavel();
            if (Convert.ToString(Session["user_level"]) == "19" || Convert.ToString(Session["user_level"]) == "146")
            {
                liApprovalQueue.Visible = true;
                ScriptManager.RegisterStartupScript(
             this,
             GetType(),
             "ShowTab",
             "$('#myTab a[href=\"#tab3\"]').tab('show');",
             true);
            }
            else
            {
                liApprovalQueue.Visible = false;
            }
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
        ScriptManager.RegisterStartupScript(
           this,
           GetType(),
           "ShowTab",
           "$('#myTab a[href=\"#tab3\"]').tab('show');",
           true);
    }
    protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
       
        FillCBBock();
       
            ScriptManager.RegisterStartupScript(
           this,
           GetType(),
           "ShowTab",
           "$('#myTab a[href=\"#tab3\"]').tab('show');",
           true);

     


    }
    protected void ddlBlock_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillCBCluster();
        ScriptManager.RegisterStartupScript(
         this,
         GetType(),
         "ShowTab",
         "$('#myTab a[href=\"#tab3\"]').tab('show');",
         true);
    }
    protected void ddlPanchayat_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillSchoolr();
        ScriptManager.RegisterStartupScript(
         this,
         GetType(),
         "ShowTab",
         "$('#myTab a[href=\"#tab3\"]').tab('show');",
         true);
    }
    public void FillSchoolr()
    {
        conditions = "";
        conditions = "mst5village.DistrictCode ='" + ddlDistrict.SelectedValue + "'  and mst5village.BlockCode ='" + ddlBlock.SelectedValue + "'  and GkpVal=1 and mst5village.ClusterCode ='" + ddlPanchayat .SelectedValue + "'";
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


    protected void btnSerach_Click(object sender, EventArgs e)
    {

       
        ScriptManager.RegisterStartupScript(
          this,
          GetType(),
          "ShowTab",
          "$('#myTab a[href=\"#tab3\"]').tab('show');",
          true);

        ScriptManager.RegisterStartupScript(
                this,
                this.GetType(),
                "LoadCards",
                "LoadCards();",
                true);

    }

    public static DataTable GetData(string stateCode,
                            string districtCode,
                            string blockCode,
                            string panchayatCode,
                            string villageCode)
    {
        string str = " WHERE isnull(ApprovalStatus,0)in (0) ";

        if (!string.IsNullOrEmpty(stateCode) && stateCode != "0")
            str += " AND mst5Village.StateCode='" + stateCode + "'";

        str += " AND mst5Village.DistrictCode='" + districtCode + "'";

        if (!string.IsNullOrEmpty(blockCode) && blockCode != "0")
            str += " AND mst5Village.BlockCode='" + blockCode + "'";

        if (!string.IsNullOrEmpty(panchayatCode) && panchayatCode != "0")
            str += " AND mst5Village.Clustercode='" + panchayatCode + "'";

        if (!string.IsNullOrEmpty(villageCode) && villageCode != "0")
            str += " AND  mstSchool.SchoolCode='" + villageCode + "'";

        clsMain objMain = new clsMain();
        DataTable dt = objMain.LoadData(" select   mstSchool.Name,mstSchool.disecode,alert_messages,mstSchool.SchoolCode,mst2District.DistrictName,blk.BlockName ,mstCluster.ClusterName,FCname,convert(varchar,Sdate,103)Sdate,TotalBasline from mstMasterGKPLevel  LEFT JOIN mstSchool ON mstSchool.Schoolcode = mstMasterGKPLevel.Schoolcode inner join mst5Village on  mst5Village.VillageCode=mstSchool.VillageCode and mstSchool.VillageCode = mst5Village.VillageCode    inner join mst2District on  mst2District.DistrictCode = mst5Village.DistrictCode  inner join mst3Block as blk on blk.BlockCode = mst5Village.BlockCode      inner join mstPanchayat as phy on mst5Village.PanchayatCode = phy.PanchayatCode and phy.BlockCode = mst5Village.BlockCode    left join mstCluster  on mstCluster.ClusterCode = mst5Village.ClusterCode " + str + " and len([alert_messages])>1"); // Your DB Method
        return dt;
    }
    [System.Web.Services.WebMethod]
    public static Dictionary<string, string> GetCounts(
    string stateCode,
    string districtCode,
    string blockCode,
    string panchayatCode,
    string villageCode)
    {
        // returns { "0": HPA, "2": HA, "3": HR }
        return GetDataCounts(stateCode, districtCode, blockCode, panchayatCode, villageCode);
    }
    public static Dictionary<string, string> GetDataCounts(
    string stateCode,
    string districtCode,
    string blockCode,
    string panchayatCode,
    string villageCode)
    {
        string str = " WHERE isnull(ApprovalStatus,0) in (0,2,3) ";

        if (!string.IsNullOrEmpty(stateCode) && stateCode != "0")
            str += " AND mst5Village.StateCode='" + stateCode + "'";

        if (!string.IsNullOrEmpty(districtCode) && districtCode != "0")
            str += " AND mst5Village.DistrictCode='" + districtCode + "'";

        if (!string.IsNullOrEmpty(blockCode) && blockCode != "0")
            str += " AND mst5Village.BlockCode='" + blockCode + "'";

        if (!string.IsNullOrEmpty(panchayatCode) && panchayatCode != "0")
            str += " AND mst5Village.Clustercode='" + panchayatCode + "'";

        if (!string.IsNullOrEmpty(villageCode) && villageCode != "0")
            str += " AND mstSchool.SchoolCode='" + villageCode + "'";

        clsMain objMain = new clsMain();
        DataTable dt = objMain.LoadData(
            " select isnull(ApprovalStatus,0) as ApprovalStatus, COUNT(*) as Cnt " +
            " from mstMasterGKPLevel " +
            " LEFT JOIN mstSchool ON mstSchool.schoolcode = mstMasterGKPLevel.schoolcode  " +
            " inner join mst5Village on mst5Village.VillageCode = mstSchool.VillageCode and mstSchool.VillageCode = mst5Village.VillageCode " +
            " inner join mst2District on mst2District.DistrictCode = mst5Village.DistrictCode " +
            " inner join mst3Block as blk on blk.BlockCode = mst5Village.BlockCode " +
            " inner join mstPanchayat as phy on mst5Village.PanchayatCode = phy.PanchayatCode and phy.BlockCode = mst5Village.BlockCode " +
            " left join mstCluster on mstCluster.ClusterCode = mst5Village.ClusterCode " +
            str + " and len(alert_messages)>1 " +
            " group by isnull(ApprovalStatus,0) ");

        // start all at 0 so missing statuses still return "0"
        var counts = new Dictionary<string, string>
    {
        { "0", "0" },   // HPA
        { "2", "0" },   // HA
        { "3", "0" }    // HR
    };

        foreach (DataRow dr in dt.Rows)
        {
            counts[dr["ApprovalStatus"].ToString()] = dr["Cnt"].ToString();
        }

        return counts;
    }
    [System.Web.Services.WebMethod]
    public static List<CardModel> GetCards(
  string stateCode,
  string districtCode,
  string blockCode,
  string panchayatCode,
  string villageCode)
    {




        DataTable dt = GetData(
        stateCode,
        districtCode,
        blockCode,
        panchayatCode,
        villageCode);

        List<CardModel> lst = new List<CardModel>();


       /// var counts = GetDataCounts(stateCode, districtCode, blockCode, panchayatCode, villageCode);
        if (dt.Rows.Count > 0)
        {
            foreach (DataRow dr in dt.Rows)
            {

                lst.Add(new CardModel
                {
                    Name = dr["Name"].ToString(),
                    disecode = dr["disecode"].ToString(),
                    District = dr["DistrictName"].ToString(),
                    SchoolCode = dr["SchoolCode"].ToString(),
                    BlockName = dr["BlockName"].ToString(),
                    Cluster = dr["ClusterName"].ToString(),
                    FCname = dr["FCname"].ToString(),
                    Status = dr["Sdate"].ToString(),
                    TotalBasline = dr["TotalBasline"].ToString(),
                    alert_messages = dr["alert_messages"].ToString(),
                    //HA = GetDataCount("1", stateCode, districtCode, blockCode, panchayatCode, villageCode),
                    //HPA = GetDataCount("0", stateCode, districtCode, blockCode, panchayatCode, villageCode),
                    //HR = GetDataCount("3", stateCode, districtCode, blockCode, panchayatCode, villageCode)
                  

                });

            }
        }
     
        return lst;
    }
    public class CardModel
    {
        public string Name { get; set; }
        public string disecode { get; set; }
        public string SchoolCode { get; set; }
        public string District { get; set; }
        public string BlockName { get; set; }
        public string Cluster { get; set; }
        public string Status { get; set; }
        public string TotalBasline { get; set; }
        public string FCname { get; set; }
        public string alert_messages { get; set; }
        public string UniqueCode { get; set; }
        public string HPA { get; set; }
        public string HR { get; set; }
        public string HA { get; set; }
    }
    public static string GetDataCount(string AST, string stateCode,
                           string districtCode,
                           string blockCode,
                           string panchayatCode,
                           string villageCode)
    {
        string str = " WHERE isnull(ApprovalStatus,0) in (" + AST + ") ";

        if (!string.IsNullOrEmpty(stateCode) && stateCode != "0")
            str += " AND mst5Village.StateCode='" + stateCode + "'";

        if (!string.IsNullOrEmpty(districtCode) && districtCode != "0")
            str += " AND mst5Village.DistrictCode='" + districtCode + "'";

        if (!string.IsNullOrEmpty(blockCode) && blockCode != "0")
            str += " AND mst5Village.BlockCode='" + blockCode + "'";

        if (!string.IsNullOrEmpty(panchayatCode) && panchayatCode != "0")
            str += " AND mst5Village.Clustercode='" + panchayatCode + "'";

        if (!string.IsNullOrEmpty(villageCode) && villageCode != "0")
            str += " AND mstSchool.SchoolCode='" + villageCode + "'";

        clsMain objMain = new clsMain();
        DataTable dt = objMain.LoadData(" select  mstSchool.Name,mstSchool.disecode,alert_messages,mstSchool.SchoolCode,mst2District.DistrictName,blk.BlockName ,mstCluster.ClusterName,FCname,convert(varchar,Sdate,103)Sdate,TotalBasline from mstGKPGrade  LEFT JOIN mstSchool ON mstSchool.disecode = mstGKPGrade.dise_code and mstSchool.fyear='2026-2027'inner join mst5Village on  mst5Village.VillageCode=mstSchool.VillageCode and mstSchool.VillageCode = mst5Village.VillageCode    inner join mst2District on  mst2District.DistrictCode = mst5Village.DistrictCode  inner join mst3Block as blk on blk.BlockCode = mst5Village.BlockCode      inner join mstPanchayat as phy on mst5Village.PanchayatCode = phy.PanchayatCode and phy.BlockCode = mst5Village.BlockCode    left join mstCluster  on mstCluster.ClusterCode = mst5Village.ClusterCode " + str + " and len(alert_messages) >0"); // Your DB Method

        return dt.Rows.Count.ToString();
    }
    [System.Web.Services.WebMethod(EnableSession = true)]
    public static string UpdateApprovalStatus(
   List<string> tbCodes,
   int status,
   string remark)
    {
        using (SqlConnection con =
            new SqlConnection(SqlHelper.mainConnectionString))
        {
            con.Open();

            SqlTransaction tran = con.BeginTransaction();
            string userId = HttpContext.Current.Session["username"].ToString();
            try
            {
                foreach (string tbCode in tbCodes)
                {
                    // Update Main Table
                    SqlCommand cmd1 = new SqlCommand(
                    @"UPDATE mstMasterGKPLevel
                  SET ApprovalStatus = @Status,
                  ApprovalBy = @userId,
                  ApprovalDate = GETDATE(),
                    FinalFlag=2

                  WHERE Schoolcode = @TBCode",
                    con, tran);

                    cmd1.Parameters.AddWithValue("@TBCode", tbCode);
                    cmd1.Parameters.AddWithValue("@Status", status);
                    cmd1.Parameters.AddWithValue("@userId", userId);
                    cmd1.ExecuteNonQuery();

                    // Insert Log
                    SqlCommand cmd2 = new SqlCommand(
                    @"INSERT INTO mstGKPApprovalLog
                  (
                      SchoolCode,
                      ApprovalStatus,
                      RejectRemark,
                      ApprovedRejectBy,
                      CreatedOn
                  )
                  VALUES
                  (
                      @SchoolCode,
                      @Status,
                      @Remark,
                      @ApprovedRejectBy,
                      GETDATE()
                  )",
                    con, tran);

                    cmd2.Parameters.AddWithValue("@SchoolCode", tbCode);
                    cmd2.Parameters.AddWithValue("@Status", status);
                    cmd2.Parameters.AddWithValue("@Remark", string.IsNullOrEmpty(remark) ? (object)DBNull.Value : remark);
                    cmd2.Parameters.AddWithValue("@ApprovedRejectBy", userId);
                    cmd2.ExecuteNonQuery();
                }

                tran.Commit();

                return "Success";
            }
            catch (Exception ex)
            {
                tran.Rollback();
                return ex.Message;
            }
        }
    }
    [System.Web.Services.WebMethod(EnableSession = true)]
    public static string UpdateApprovalStatusRe(
   List<string> tbCodes,
   int status,
   string remark)
    {
        using (SqlConnection con =
            new SqlConnection(SqlHelper.mainConnectionString))
        {
            con.Open();

            SqlTransaction tran = con.BeginTransaction();
            string userId = HttpContext.Current.Session["username"].ToString();
            try
            {
                foreach (string tbCode in tbCodes)
                {
                    // Update Main Table
                    SqlCommand cmd1 = new SqlCommand(
                    @"UPDATE mstMasterGKPLevel
                  SET ApprovalStatus = @Status,
                  ApprovalBy = @userId,
                     FinalFlag=3,
                  ApprovalDate = GETDATE(),
                    RejectReason= @Remark
                  WHERE Schoolcode = @TBCode",
                    con, tran);

                    cmd1.Parameters.AddWithValue("@TBCode", tbCode);
                    cmd1.Parameters.AddWithValue("@Status", status);
                    cmd1.Parameters.AddWithValue("@userId", userId);
                    cmd1.Parameters.AddWithValue("@Remark", string.IsNullOrEmpty(remark) ? (object)DBNull.Value : remark);
                    cmd1.ExecuteNonQuery();

                    // Insert Log
                    SqlCommand cmd2 = new SqlCommand(
                    @"INSERT INTO mstGKPApprovalLog
                  (
                      SchoolCode,
                      ApprovalStatus,
                      RejectRemark,
                      ApprovedRejectBy,
                      CreatedOn
                  )
                  VALUES
                  (
                      @SchoolCode,
                      @Status,
                      @Remark,
                      @ApprovedRejectBy,
                      GETDATE()
                  )",
                    con, tran);

                    cmd2.Parameters.AddWithValue("@SchoolCode", tbCode);
                    cmd2.Parameters.AddWithValue("@Status", status);
                    cmd2.Parameters.AddWithValue("@Remark", string.IsNullOrEmpty(remark) ? (object)DBNull.Value : remark);
                    cmd2.Parameters.AddWithValue("@ApprovedRejectBy", userId);
                    cmd2.ExecuteNonQuery();
                }

                tran.Commit();

                return "Success";
            }
            catch (Exception ex)
            {
                tran.Rollback();
                return ex.Message;
            }
        }
    }
    public class IdCardModel
    {
        public string Name { get; set; }
        public string Village { get; set; }
        public string TeamCode { get; set; }
        public string DateOfJoining { get; set; }
        public string Cluster { get; set; }

        public string FatherName { get; set; }
        public string DOB { get; set; }
        public string ContactNo { get; set; }
        public string Validity { get; set; }
        public string OfficeAddress { get; set; }

        public string PhotoPath { get; set; }
    }
}
