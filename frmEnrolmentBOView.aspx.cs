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

public partial class frmEnrolmentBOView : Page
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
            if (Convert.ToString(Session["username"]) != "")
            {
                //exec rptGKgrad
                LoadYear();
                LoadUserLeavel();

                if (Convert.ToString(Session["user_level"]) == "30" || Convert.ToString(Session["user_level"]) == "1" || Convert.ToString(Session["user_level"]) == "39" || Convert.ToString(Session["user_level"]) == "136" || Convert.ToString(Session["user_level"]) == "19" || Convert.ToString(Session["user_level"]) == "137")
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
            else
            {
                Response.Redirect("Login.aspx", false);

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
        FillCVillage();
        ScriptManager.RegisterStartupScript(
         this,
         GetType(),
         "ShowTab",
         "$('#myTab a[href=\"#tab3\"]').tab('show');",
         true);
    }
    protected void ddlVillage_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillSchool();

       
    }
    public void FillSchool()
    {
        string strQry = "Select SchoolCode,Name from mstSchool  where VillageCode ='" + ddlVillage.SelectedValue + "'  and FYear ='" + ddlYear.SelectedItem.Text + "'  union Select  SchoolCode, Name from mstSchool  where VillageCode ='" + ddlVillage.SelectedValue + "'    ";

        DataTable dtSchool = objMain.LoadData(strQry);

        objComman.BindDLLMasterTable("mstSchool", "SchoolCode,Name", dtSchool, conditions, "Name", "asc", ddlschool, "Name", "SchoolCode", "Select");




        //conditions = "";
        //conditions = "VillageCode ='" + ddlVillage.SelectedValue + "'  and FYear ='" + ddlYear.SelectedItem.Text + "'";
        //objComman.BindDLL("mstSchool", "SchoolCode,Name", conditions, "Name", "asc", ddlSchool, "Name", "SchoolCode", "Select");


    }
    public void FillSchoolr()
    {
        conditions = "";
        conditions = "mst5village.DistrictCode ='" + ddlDistrict.SelectedValue + "'  and mst5village.BlockCode ='" + ddlBlock.SelectedValue + "' and mst5village.ClusterCode ='" + ddlPanchayat .SelectedValue + "'";
        BindDLLSelectAll("mstSchool inner join mst5village on mst5village.villagecode =mstSchool.villagecode", "Schoolcode,dbo.TitleCase(upper(Name)) as Name", conditions, "Name", "asc", ddlschool, "Name", "Schoolcode", "Select");



    }

    public void FillCVillage()
    {
        conditions = "";
        //conditions = "DistrictCode ='" + ddlDistrict.SelectedValue + "'  and BlockCode ='" + ddlBlock.SelectedValue + "' and  PanchayatCode='" + ddlPanchayat.SelectedValue + "'  ";
        //objComman.BindDLL("mst5Village", "VillageCode,dbo.TitleCase(upper(VillageName)) as VillageName", conditions, "VillageName", "asc", ddlVillage, "VillageName", "VillageCode", "Select");

        if (ddlPanchayat.SelectedValue.ToString() == "1")
        {
            conditions = "mst5Village.DistrictCode ='" + ddlDistrict.SelectedValue + "'  and mst5Village.BlockCode ='" + ddlBlock.SelectedValue + "'  ";

        }
        else
        {
            conditions = "mst5Village.DistrictCode ='" + ddlDistrict.SelectedValue + "'  and mst5Village.BlockCode ='" + ddlBlock.SelectedValue + "' and  mst5Village.PanchayatCode='" + ddlPanchayat.SelectedValue + "'  ";

        }

        string strQry = "  SELECT mst5Village.VillageCode, dbo.TitleCase(upper((mst5Village.VillageName))) + ' (' + dbo.TitleCase(upper(mstPanchayat.PanchayatName)) +')'   as VillageName FROM mst5Village INNER JOIN mstPanchayat ON mst5Village.PanchayatCode = mstPanchayat.PanchayatCode where " + conditions + "  order by VillageName   ";
        DataTable dtVillage = objMain.LoadData(strQry);

        objComman.BindDLLMasterTableVillage("mst5Village", "VillageName,VillageCode", dtVillage, conditions, "VillageName", "asc", ddlVillage, "VillageName", "VillageCode", "Select");


    }
    public void FillCBCluster()
    {
        //conditions = "";
        //conditions = "DistrictCode ='" + ddlDistrict.SelectedValue + "'  and BlockCode ='" + ddlBlock.SelectedValue + "'";
        //BindDLLSelectAll("mstCluster", "ClusterCode,dbo.TitleCase(upper(ClusterName)) as ClusterName", conditions, "ClusterName", "asc", ddlPanchayat, "ClusterName", "ClusterCode", "Select");
        conditions = "";
        conditions = "DistrictCode ='" + ddlDistrict.SelectedValue + "'  and BlockCode ='" + ddlBlock.SelectedValue + "'";
        objComman.BindDLLSelectAll("mstPanchayat", "PanchayatCode,dbo.TitleCase(upper(PanchayatName)) as PanchayatName", conditions, "PanchayatName", "asc", ddlPanchayat, "PanchayatName", "PanchayatCode", "Select");



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
                            string villageCode,string Schoolcode)
    {

        string user_level = HttpContext.Current.Session["user_level"].ToString();
        string str = "";
        if (user_level == "30" || user_level == "39" || user_level == "136")
        {
            str = " WHERE tblEnrolment.IOApprove =1 ";
        }
        else {
            str = " WHERE tblEnrolment.BOApprove =1 ";
        }

        if (!string.IsNullOrEmpty(stateCode) && stateCode != "0")
            str += " AND mst5Village.StateCode='" + stateCode + "'";

        str += " AND mst5Village.DistrictCode='" + districtCode + "'";

        if (!string.IsNullOrEmpty(blockCode) && blockCode != "0")
            str += " AND mst5Village.BlockCode='" + blockCode + "'";

        if (!string.IsNullOrEmpty(villageCode) && villageCode != "0")
            str += " AND mst5Village.villageCode='" + villageCode + "'";

        if (!string.IsNullOrEmpty(Schoolcode) && Schoolcode != "0")
            str += " AND  tblEnrolment.SchoolCode='" + Schoolcode + "'";

        clsMain objMain = new clsMain();
        DataTable dt = null;
        if (villageCode.Length > 0)
        {
            SqlParameter[] parm1 = new SqlParameter[]
               {

               new SqlParameter("@Con",  str),

               };


            dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rptApproveEnrollment]", parm1);


        }
      return dt;
    }
   
    [System.Web.Services.WebMethod]
    public static List<CardModel> GetCards(
  string stateCode,
  string districtCode,
  string blockCode,
  string panchayatCode,
  string villageCode, string Schoolcode)
    {




        DataTable dt = GetData(
        stateCode,
        districtCode,
        blockCode,
        panchayatCode,
        villageCode, Schoolcode);

        List<CardModel> lst = new List<CardModel>();


       /// var counts = GetDataCounts(stateCode, districtCode, blockCode, panchayatCode, villageCode);
        if (dt.Rows.Count > 0)
        {
            foreach (DataRow dr in dt.Rows)
            {

                lst.Add(new CardModel
                {
                    ChildName = dr["ChildName"].ToString(),
                    FathersName = dr["FathersName"].ToString(),
                    Gender = dr["Gender"].ToString(),
                    DOB = dr["DOB"].ToString(),
                    Age = dr["Age"].ToString(),
                    Serial = dr["Serial"].ToString(),
                  
                    SocialCategory = dr["SocialCategory"].ToString(),
                    School = dr["School"].ToString(),
                    D2dChildName = dr["D2dChildName"].ToString(),
                    D2dFatherName = dr["D2dFatherName"].ToString(),
                    D2dAge = dr["D2dAge"].ToString(),
                    D2dSocialCategory = dr["D2dSocialCategory"].ToString(),
                    D2Dgender = dr["D2Dgender"].ToString(),
                    EnrolmentDate = dr["EnrolmentDate"].ToString(),
                    UniqueChildCode = dr["UniqueChildCode"].ToString(),
                    DISECode = dr["DISECode"].ToString(),
                    FuzzyScore = dr["FuzzyScore"].ToString(),

                    ID = dr["ID"].ToString(),
                });

            }
        }
     
        return lst;
    }
    public class CardModel
    {
        public string ChildName { get; set; }
        public string FathersName { get; set; }
        public string Gender { get; set; }
        public string DOB        { get; set; }
        public string Age { get; set; }
        public string Serial { get; set; }
        public string SocialCategory { get; set; }
        public string School { get; set; }

      

        public string D2dChildName { get; set; }
        public string D2dFatherName { get; set; }
        public string D2dAge { get; set; }
        public string D2dSocialCategory { get; set; }
        public string D2Dgender { get; set; }
        public string EnrolmentDate { get; set; }
        public string UniqueChildCode { get; set; }
        public string DISECode { get; set; }
        public string FuzzyScore { get; set; }
        public string ID { get; set; }
        

    }
    
    [System.Web.Services.WebMethod(EnableSession = true)]
    public static string UpdateApprovalStatus(
   List<string> tbCodes,
   int status,
   string remark)
    {
       

            string userId = HttpContext.Current.Session["username"].ToString();
            string user_level = HttpContext.Current.Session["user_level"].ToString();
            string Status = "2";
            try
        {
            foreach (string tbCode in tbCodes)
            {
                using (SqlConnection con = new SqlConnection(SqlHelper.mainConnectionString))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("dbo.BOApprovalUpdateFinal", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@Uni", tbCode);
                        cmd.Parameters.AddWithValue("@Username", userId);
                        cmd.Parameters.AddWithValue("@UserLevel", user_level);
                        cmd.Parameters.AddWithValue("@Status", Status);

                        cmd.ExecuteNonQuery();
                    }
                }
            }
   


                return "Success";
            }
            catch (Exception ex)
            {
              
                return ex.Message;
            }
        
    }
    [System.Web.Services.WebMethod(EnableSession = true)]
    public static string UpdateApprovalStatusRe(
   List<string> tbCodes,
   int status,
   string remark)
    {
        string userId = HttpContext.Current.Session["username"].ToString();
        string user_level = HttpContext.Current.Session["user_level"].ToString();
        string Status = "3";
        try
        { 
         foreach (string tbCode in tbCodes)
        {
            using (SqlConnection con = new SqlConnection(SqlHelper.mainConnectionString))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("dbo.BOApprovalUpdateUnmatch", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Uni", tbCode);
                    cmd.Parameters.AddWithValue("@Username", userId);
                    cmd.Parameters.AddWithValue("@UserLevel", user_level);
                    cmd.Parameters.AddWithValue("@Status", Status);
                        cmd.Parameters.AddWithValue("@Remark", string.IsNullOrEmpty(remark) ? (object)DBNull.Value : remark);
                        cmd.ExecuteNonQuery();
                }
            }
        }
            return "Success";
        }
        catch (Exception ex)
        {

            return ex.Message;
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
