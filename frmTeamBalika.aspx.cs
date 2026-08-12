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
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;

using QRCoder;

using System.Drawing.Imaging;
using Image = iTextSharp.text.Image;
using Font = iTextSharp.text.Font;
using System.Configuration;

public partial class frmTeamBalika : System.Web.UI.Page
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

                //GVMainBind();
                LoadYear();
                LoadUserLeavel();

                FillSocialCat();
                FillDropResone();
                ViewState["Save"] = "Save";
                FillFaimlyCat();
                FillEdu();
                FillSours();
                FillReasone();
                btnDelete.Attributes.Add("onclick", "javascript:return " + "confirm('Are you sure you want to Delete? ')");
                ValdateUserLavel();
                txtEndDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                FillSpecially();
                liProfile.Visible = false;
                liIdCard.Visible = false;
                liApprovalQueue.Visible = false;
                if (Convert.ToString(Session["user_level"]) == "60")
                {
                    liProfile.Visible = false;
                    liIdCard.Visible = true;
                    liApprovalQueue.Visible = true;
                
                
                }
            else   if (Convert.ToString(Session["user_level"]) == "138")
                {
                    liProfile.Visible = true;
                    liIdCard.Visible = true;
                    liApprovalQueue.Visible = false;
                }
                    else
                    {
                        liProfile.Visible = true;
                        liIdCard.Visible = false;
                        liApprovalQueue.Visible = false;
                   }
                liProfile.Attributes["class"] = "active";
                liIdCard.Attributes["class"] = "";
                liApprovalQueue.Attributes["class"] = "";
            
              
            }
            else
            {
                Response.Redirect("Login.aspx", false);

            }

        }


        ShowTab("#tab2");
        ScriptManager.RegisterStartupScript(Page, GetType(), Guid.NewGuid().ToString(), "loadJSFunction();", true);

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
    private void ShowTab(string href)
    {
        ScriptManager.RegisterStartupScript(this, GetType(), "tab" + Guid.NewGuid().ToString("N"),
            "$('.nav-tabs a[href=\"" + href + "\"]').tab('hide');", true);
    }
    public DataTable CreateDataTable()
    {

        DataTable dtYear = new DataTable();
        dtYear.Columns.Add("Type", System.Type.GetType("System.String"));

        dtYear.Columns.Add("ID", System.Type.GetType("System.Int32"));
        return dtYear;
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
        AlllStateCode();
        if (ddlYear.SelectedIndex > 0)
        {
            ddlState.SelectedIndex = 1;
            ddlState_SelectedIndexChanged(ddlDistrict, null);
            ddlDistrict.SelectedIndex = 1;
            ddlDistrict_SelectedIndexChanged(ddlDistrict, null);

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
    }

    public void FillSours()
    {
        conditions = "";
        conditions = "LookupFlag ='RSO' and Active=1 ";
        objComman.BindDLL("mstLookup", "LookupCode,Description", conditions, "Description", "asc", ddlSours, "Description", "LookupCode", "Select");



    }
    public void FillDropResone()
    {
        conditions = "";
        conditions = "LookupFlag ='TMR' and Active=1 ";
        objComman.BindDLL("mstLookup", "LookupCode,Description,SeqNo", conditions, "SeqNo", "asc", ddlStatusReasone, "Description", "LookupCode", "Select");

        conditions = "";
        conditions = "LookupFlag ='TJB' and Active=1 ";
        objComman.BindDLL("mstLookup", "LookupCode,Description,SeqNo", conditions, "LookupCode", "asc", ddlJobOpportunity, "Description", "LookupCode", "Select");


    }

    public void FillSpecially()
    {
        conditions = "";
        conditions = "LookupFlag ='SPS' and Active=1 ";
        objComman.BindDLL("mstLookup", "LookupCode,Description", conditions, "LookupCode", "asc", ddlSpecially, "Description", "LookupCode", "Select");



    }
    public void FillReasone()
    {
        conditions = "";
        conditions = "LookupFlag ='RTB' and Active=1 ";
        objComman.BindDLL("mstLookup", "LookupCode,Description", conditions, "Description", "asc", ddlReason, "Description", "LookupCode", "Select");



    }
    public void FillSocialCat()
    {
        conditions = "";
        conditions = "LookupFlag ='CAT' and Active=1 ";
        objComman.BindDLL("mstLookup", "LookupCode,Description", conditions, "LookupCode", "asc", ddlCategory, "Description", "LookupCode", "Select");



    }
    public void FillEdu()
    {
        conditions = "";
        conditions = "LookupFlag ='Edu' and Active=1 ";
        objComman.BindDLL("mstLookup", "LookupCode,Description", conditions, "LookupCode", "asc", ddlEducation, "Description", "LookupCode", "Select");



    }

    public void FillFaimlyCat()
    {
        conditions = "";
        conditions = "LookupFlag ='FO' and Active=1 ";
        objComman.BindDLL("mstLookup", "LookupCode,Description", conditions, "LookupCode", "asc", ddloccu, "Description", "LookupCode", "Select");



    }


    public void ValdateUserLavel()
    {

        string strQry = "";
        string Cond = "Module='TeamBalika' ";
        strQry = "Select * from MstUserRight  where " + Cond + " and Role_Id=" + Session["user_level"].ToString() + "   ";


        DataTable dtRole = objMain.LoadData(strQry);

        if (dtRole.Rows.Count > 0)
        {
            vADD = Convert.ToBoolean(dtRole.Rows[0]["AddStatus"].ToString());
            vVerify = Convert.ToBoolean(dtRole.Rows[0]["verify_Status"].ToString());
            vDelete = Convert.ToBoolean(dtRole.Rows[0]["Delete_status"].ToString());
            ViewState["vADD"] = vADD;
            ViewState["vVerify"] = vVerify;
            ViewState["vDelete"] = vDelete;
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
            btnAdd.Enabled = true;
            btnsave.Enabled = true;
            //lblMain.Text = "School Information Campaign";
        }
        else
        {
            btnAdd.Enabled = false;
            btnsave.Enabled = false;
        }
        //if (Session["user_level"].ToString() == "1")
        //{
        //    btnAdd.Enabled = true;
        //    btnDelete.Enabled = true;
        //    lblMain.Text = "School Information Campaign";
        //}
        if (vVerify == true)
        {

            btnsave.Enabled = true;

            //lblMain.Text = "School Information Campaign(Verify)";
            //stid.Style.Add("background-color", "#FFFFE0");
            //stmid.Style.Add("background-color", "#FFFFE0");
            //stinfid.Style.Add("background-color", "#FFFFE0");
            //stAvailability.Style.Add("background-color", "#FFFFE0");
            //stsmc.Style.Add("background-color", "#FFFFE0");
            //stdr.Style.Add("background-color", "#FFFFE0");
            //srlm.Style.Add("background-color", "#FFFFE0");
            //stbdfid.Style.Add("background-color", "#FFFFE0");
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

            //conditions = "UserName='" + Session["username"].ToString() + "' ";

            //string strQry1 = "  SELECT distinct mst1State.StateCode, dbo.TitleCase(upper(StateName))  as StateName FROM MstusermultipleDist inner join mst2District on mst2District.DistrictCode=MstusermultipleDist.DistrictCode inner join mst1State on mst1State.StateCode=mst2District.StateCode where   " + conditions + "   order by StateName   ";
            //DataTable dtTb = objMain.LoadData(strQry1);
            //objComman.BindDLLMasterTableVillage("mst1State", "StateCode,StateName", dtTb, conditions, "StateName", "asc", ddlState, "StateName", "StateCode", "Select");

            //    ddlDistrict.SelectedIndex = 0;


            ddlState.SelectedIndex = 1;
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
            //conditions = "";
            //conditions = "StateCode ='" + ddlState.SelectedValue + "' ";
            //objComman.BindDLL("
            //", "DistrictCode,dbo.TitleCase(upper(DistrictName)) as DistrictName", conditions, "DistrictName", "asc", ddlDistrict, "DistrictName", "DistrictCode", "--Select--");
            conditions = "StateCode ='" + ddlState.SelectedValue + "'  and Fyear= '" + ddlYear.SelectedItem.Text + "'  ";
            // objComman.BindDLL("mst2District", "DistrictCode,dbo.TitleCase(upper(DistrictName)) as DistrictName", conditions, "DistrictName", "asc", ddlDistrict, "DistrictName", "DistrictCode", "--Select--");

            //string conditions1 = "StateCode ='" + ddlState.SelectedValue + "' ";

            //DataTable dtTb = objMain.LoadData(" SELECT DistrictCode,  dbo.TitleCase(upper(DistrictName)) as  DistrictName FROM [mst2District] where  " + conditions + "   order by DistrictName ");



            //objComman.BindDLLMasterTableVillage("mst2District", "DistrictCode,DistrictName", dtTb, conditions, "DistrictName", "asc", ddlDistrict, "DistrictName", "DistrictCode", "Select");
            conditions = "";
            conditions = " mst2District.StateCode ='" + ddlState.SelectedValue + "' and UserName='" + Session["username"].ToString() + "' ";
            string strQry1 = "       sELECT distinct mst2District.DistrictCode as DistrictCode, dbo.TitleCase(upper(DistrictName))  as DistrictName FROM MstusermultipleDist  ";
            strQry1 += " inner join mst2District on mst2District.OldDistrictCode=MstusermultipleDist.OldDistrictCode  where " + conditions + "  and  Fyear='" + ddlYear.SelectedItem.Text + "' order by DistrictName  ";
            DataTable dtDistrict = objMain.LoadData(strQry1);

            objComman.BindDLLDatatable("mst2District", dtDistrict, "DistrictCode, dbo.TitleCase(upper(DistrictName)) as DistrictName", conditions, "DistrictName", "Desc", ddlDistrict, "DistrictName", "DistrictCode", "--Select--");

            ddlDistrict.SelectedIndex = 0;
            ddlDistrict.Enabled = true;
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
            strQry = "Select DistrictCode from mst2District where   DistrictCode in(" + Session["DistrictCode"].ToString() + ")";
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

        pnlMain.Enabled = false;

    }
    public void FillCBDist()
    {

        //conditions = "";
        //if (Session["user_level_Role"].ToString() == "1")
        //{

        //    conditions = "StateCode ='" + ddlState.SelectedValue + "' and mst2District.FYear ='" + ddlYear.SelectedItem.Text + "'";
        //}
        //else if (Session["user_level_Role"].ToString() == "2")
        //{
        //    conditions = "StateCode ='" + ddlState.SelectedValue + "' and DistrictCode ='" + Session["DistrictCode"].ToString() + "'  and mst2District.FYear ='" + ddlYear.SelectedItem.Text + "'";
        //}
        //else
        //{
        //    conditions = "StateCode ='" + ddlState.SelectedValue + "' and DistrictCode in(" + Session["DistrictCode"].ToString() + ") and mst2District.FYear ='" + ddlYear.SelectedItem.Text + "' ";


        //}
        DataTable dtDistrict;
        conditions = "";
        if (Session["user_level_Role"].ToString() == "1")
        {

            conditions = "StateCode in('" + ddlState.SelectedValue + "') and mst2District.FYear ='" + ddlYear.SelectedItem.Text + "'";
        }
        else if (Session["user_level_Role"].ToString() == "2")
        {
            conditions = " mst2District.StateCode in('" + ddlState.SelectedValue + "') and UserName='" + Session["username"].ToString() + "' ";
        }
        else
        {
            conditions = "StateCode  in('" + ddlState.SelectedValue + "') and DistrictCode in(" + Session["DistrictCode"].ToString() + ") and mst2District.FYear ='" + ddlYear.SelectedItem.Text + "' ";


        }
        if (Session["user_level_Role"].ToString() == "2")
        {
            //if (ddlYear.SelectedValue.ToString() == "2016")
            //{

            //    string strQry1 = "  SELECT distinct mst2District.DistrictCode, dbo.TitleCase(upper(DistrictName))  as DistrictName FROM mst2District where EGDistrictCode in(     SELECT distinct mst2District.EGDistrictCode  FROM MstusermultipleDist     where   " + conditions + " )  and Fyear='" + ddlYear.SelectedItem.Text + "' order by DistrictName   ";
            //    dtDistrict = objMain.LoadData(strQry1);
            //}

            //if (ddlYear.SelectedValue.ToString() == "2017")
            //{

            //    string strQry1 = "  SELECT distinct mst2District.DistrictCode, dbo.TitleCase(upper(DistrictName))  as DistrictName FROM mst2District where EGDistrictCode in(     SELECT distinct mst2District.EGDistrictCode  FROM MstusermultipleDist     where   " + conditions + " )  and Fyear='" + ddlYear.SelectedItem.Text + "' order by DistrictName   ";
            //    dtDistrict = objMain.LoadData(strQry1);
            //}
            //if (ddlYear.SelectedValue.ToString() == "2018")
            //{

            //    string strQry1 = "  SELECT distinct mst2District.DistrictCode, dbo.TitleCase(upper(DistrictName))  as DistrictName FROM MstusermultipleDist   inner join mst2District on mst2District.DistrictCode=MstusermultipleDist.DistrictCode  where   " + conditions + " and Fyear='" + ddlYear.SelectedItem.Text + "' order by DistrictName   ";
            //    dtDistrict = objMain.LoadData(strQry1);
            //}
            string strQry1 = "  SELECT distinct mst2District.DistrictCode as DistrictCode, dbo.TitleCase(upper(DistrictName))  as DistrictName FROM MstusermultipleDist  ";
            strQry1 += " inner join mst2District on mst2District.OldDistrictCode=MstusermultipleDist.OldDistrictCode  where " + conditions + "  and  Fyear='" + ddlYear.SelectedItem.Text + "' order by DistrictName  ";
            dtDistrict = objMain.LoadData(strQry1);
        }
        else
        {
            string strQry = "  SELECT DistrictCode, dbo.TitleCase(upper(DistrictName))  as DistrictName FROM mst2District where " + conditions + "  order by DistrictName   ";
            dtDistrict = objMain.LoadData(strQry);
        }

        objComman.BindDLLMasterTableVillage("mst2District", "DistrictCode,DistrictName", dtDistrict, conditions, "DistrictName", "asc", ddlDistrict, "DistrictName", "DistrictCode", "Select");


        //  objComman.BindDLL("mst2District", "DistrictCode,dbo.TitleCase(upper(DistrictName)) as DistrictName ", conditions, "DistrictName", "asc", ddlDistrict, "DistrictName", "DistrictCode", "Select");

        Session["TDist"] = ddlDistrict.SelectedValue;

    }

    //public void FillCBDist()
    //{
    //    conditions = "";
    //    conditions = "StateCode ='" + ddlState.SelectedValue + "'";
    //    objComman.BindDLL("mst2District", "DistrictCode,dbo.TitleCase(upper(DistrictName)) as DistrictName ", conditions, "DistrictName", "asc", ddlDistrict, "DistrictName", "DistrictCode", "--Select--");



    //}
    protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillCBDist();
    }
    protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Convert.ToString(Session["user_level"]) == "60")
        {
            ScriptManager.RegisterStartupScript(
           this,
           GetType(),
           "ShowTab",
           "$('#myTab a[href=\"#tab3\"]').tab('show');",
           true);
        }
        FillCBBock();
        pnlMain.Enabled = false;
    }
    protected void ddlBlock_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Convert.ToString(Session["user_level"]) == "60")
        {
            ScriptManager.RegisterStartupScript(
           this,
           GetType(),
           "ShowTab",
           "$('#myTab a[href=\"#tab3\"]').tab('show');",
           true);
        }
        FillCBCluster();
        pnlMain.Enabled = false;
    }
    protected void ddlPanchayat_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Convert.ToString(Session["user_level"]) == "60")
        {
            ScriptManager.RegisterStartupScript(
           this,
           GetType(),
           "ShowTab",
           "$('#myTab a[href=\"#tab3\"]').tab('show');",
           true);
        }
        FillCVillage();
        pnlMain.Enabled = false;
    }
    protected void ddlVillage_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Convert.ToString(Session["user_level"]) == "60")
        {
            ScriptManager.RegisterStartupScript(
           this,
           GetType(),
           "ShowTab",
           "$('#myTab a[href=\"#tab3\"]').tab('show');",
           true);
        }
        string strQry = "  SELECT VillageGeographyOperational FROM mst5Village where villagecode ='" + ddlVillage.SelectedValue + "'     ";
        DataTable dtDistrict = objMain.LoadData(strQry);
        if (dtDistrict.Rows.Count > 0)
        {
            Session["VillageGeographyOperational"] = Convert.ToString(dtDistrict.Rows[0]["VillageGeographyOperational"]);
        }
        pnlMain.Enabled = false;
        //Unique();
    }

    public void FillCBBock()
    {
        conditions = "";
        if (Session["user_level_Role"].ToString() == "1")
        {

            conditions = "DistrictCode ='" + ddlDistrict.SelectedValue + "' and  DividedBlock=1 ";
        }
        else if (Session["user_level_Role"].ToString() == "2")
        {
            conditions = "DistrictCode ='" + ddlDistrict.SelectedValue + "' and  DividedBlock=1 ";
        }
        else if (Session["user_level_Role"].ToString() == "6")
        {
            conditions = " BlockCode in( " + Session["blockCodeMul"].ToString() + " ) and FYear ='" + ddlYear.SelectedItem.Text + "' ";
        }
        else if (Session["user_level_Role"].ToString() == "4")
        {
            conditions = "DistrictCode ='" + ddlDistrict.SelectedValue + "' and  DividedBlock=1 and BlockCode in( " + Session["BlockCode"].ToString() + " ) and FYear ='" + ddlYear.SelectedItem.Text + "' ";
        }
        else
        {
            conditions = "DistrictCode ='" + ddlDistrict.SelectedValue + "' and  DividedBlock=1 ";
        }
        objComman.BindDLL("mst3Block", "BlockCode,dbo.TitleCase(upper(BlockName)) as BlockName ", conditions, "BlockName", "asc", ddlBlock, "BlockName", "BlockCode", "--Select--");



    }
    public void FillCBCluster()
    {
        conditions = "";
        conditions = "DistrictCode ='" + ddlDistrict.SelectedValue + "'  and BlockCode ='" + ddlBlock.SelectedValue + "'";
        objComman.BindDLLSelectAll("mstPanchayat", "PanchayatCode,dbo.TitleCase(upper(PanchayatName)) as PanchayatName", conditions, "PanchayatName", "asc", ddlPanchayat, "PanchayatName", "PanchayatCode", "Select");



    }
    public void FillCVillage()
    {
        conditions = "";
        //conditions = "DistrictCode ='" + ddlDistrict.SelectedValue + "'  and BlockCode ='" + ddlBlock.SelectedValue + "' and  PanchayatCode='" + ddlPanchayat.SelectedValue + "'  ";
        //objComman.BindDLL("mst5Village", "VillageCode,dbo.TitleCase(upper(VillageName)) as VillageName", conditions, "VillageName", "asc", ddlVillage, "VillageName", "VillageCode", "--Select--");

        if (ddlPanchayat.SelectedValue.ToString() == "1")
        {
            conditions = "mst5Village.DistrictCode ='" + ddlDistrict.SelectedValue + "'  and mst5Village.BlockCode ='" + ddlBlock.SelectedValue + "'  ";

        }
        else
        {
            conditions = "mst5Village.DistrictCode ='" + ddlDistrict.SelectedValue + "'  and mst5Village.BlockCode ='" + ddlBlock.SelectedValue + "' and  mst5Village.PanchayatCode='" + ddlPanchayat.SelectedValue + "'  ";

        }

        string strQry = "  SELECT mst5Village.VillageCode, dbo.TitleCase(upper((mst5Village.VillageName))) + ' (' + dbo.TitleCase(upper(mst5Village.EGvillagecode)) +')'   as VillageName FROM mst5Village INNER JOIN mstPanchayat ON mst5Village.PanchayatCode = mstPanchayat.PanchayatCode where " + conditions + "  order by VillageName   ";
        DataTable dtVillage = objMain.LoadData(strQry);

        objComman.BindDLLMasterTableVillage("mst5Village", "VillageName,VillageCode", dtVillage, conditions, "VillageName", "asc", ddlVillage, "VillageName", "VillageCode", "Select");


    }

    private void GVMainBind()
    {

        string str = "";

        if (ddlState.SelectedValue != null && ddlState.SelectedIndex > 0)
        {
            str = "where mst5Village.StateCode='" + ddlState.SelectedValue.ToString() + "'";
        }
        if (ddlDistrict.SelectedValue != null && ddlDistrict.SelectedIndex > 0)
        {
            str = str + "and mst5Village.DistrictCode='" + ddlDistrict.SelectedValue.ToString() + "'";
        }

        if (ddlBlock.SelectedValue != null && ddlBlock.SelectedIndex > 0)
        {
            str = str + "and mst5Village.BlockCode='" + ddlBlock.SelectedValue.ToString() + "'";
        }

        if (ddlPanchayat.SelectedValue != null && ddlPanchayat.SelectedIndex > 1)
        {
            str = str + "and mst5Village.PanchayatCode='" + ddlPanchayat.SelectedValue.ToString() + "'";
        }

        if (ddlVillage.SelectedValue != null && ddlVillage.SelectedIndex > 0)
        {
            str = str + "and mst5Village.VillageCode='" + ddlVillage.SelectedValue.ToString() + "'";
        }
        DataTable dtmstM = objMain.LoadData(" SELECT TBCode,UniqueCode, TBName,mst5Village.VillageCode +'-'+ [TBCode] as UniqueId FROM [dbo].[mstTeamBalika] inner join mst5Village on mst5Village.VillageCode=mstTeamBalika.VillageCode 	or  mst5Village.refVillage16=mstTeamBalika.VillageCode	or  mst5Village.refVillage17=mstTeamBalika.VillageCode	or  mst5Village.refVillage18=mstTeamBalika.VillageCode	or  mst5Village.refVillage19=mstTeamBalika.VillageCode	or  mst5Village.refVillage20=mstTeamBalika.VillageCode	 	or  mst5Village.refVillage21=mstTeamBalika.VillageCode or  mst5Village.refVillage22=mstTeamBalika.VillageCode	or  mst5Village.refVillage23=mstTeamBalika.VillageCode or  mst5Village.refVillage24=mstTeamBalika.VillageCode  or  mst5Village.refVillage25=mstTeamBalika.VillageCode left join mst1State on mst1State.StateCode=mst5Village.StateCode left join mst2District on mst2District.DistrictCode=mst5Village.DistrictCode   left join (select distinct blockcode,blockname from mst3Block) blk ON mst5Village.BlockCode = blk.BlockCode LEFT JOIN (select distinct PanchayatCode,PanchayatName from mstPanchayat) phy  ON mst5Village.PanchayatCode  = phy.PanchayatCode  " + str + " ");

        //DataTable dt = SqlHelper.GetDataTable(strcon, CommandType.Text, "select schoolcode, Name,PrincipalName,PrincipalContact from mstSchool");
        if (dtmstM.Rows.Count > 0)
        {
            GVMain.DataSource = dtmstM;
            GVMain.DataBind();
            ViewState["Serach"] = dtmstM;
        }
        else
        {
            GVMain.DataSource = null;
            GVMain.DataBind();
            ViewState["Serach"] = "";
        }
    }

    public static System.Drawing.Image ScaleImage(System.Drawing.Image image, int maxHeight)
    {
        var ratio = (double)maxHeight / image.Height;
        var newWidth = (int)(image.Width * ratio);
        var newHeight = (int)(image.Height * ratio);
        var newImage = new Bitmap(newWidth, newHeight);
        using (var g = Graphics.FromImage(newImage))
        {
            g.DrawImage(image, 0, 0, newWidth, newHeight);
        }
        return newImage;
    }


    protected void btnsave_Click(object sender, EventArgs e)
    {


        Save_Update(0);
    }

    protected void btnSumbit_Click(object sender, EventArgs e)
    {
        Save_Update(0);
    }
    private void Save_Update(int SchoolCode)
    {

        if (Convert.ToInt32(ddlDob.SelectedValue) == 2 && txtAge.Text == "")
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Enter Age')</script>", false);


            this.txtAge.Focus();
            return;
        }
        if (Convert.ToInt32(ddlWorkEx.SelectedValue) == 1 && txtDuartion.Text == "")
        {
            if (txtDuartion.Text == "")
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Enter Duration')</script>", false);


                this.txtDuartion.Focus();
                return;
            }
            if (Convert.ToInt32(ddlWorkEx.SelectedValue) <= 0)
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Select Year/Month')</script>", false);


                this.ddlWorkEx.Focus();
                return;
            }
        }
        if (Convert.ToInt32(ddlWorkingStatus.SelectedValue) <= 0)
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Select Wroking Status')</script>", false);


            this.ddlWorkEx.Focus();
            return;
        }
        if (Convert.ToInt32(ddlSmart.SelectedValue) <= 0)
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Select Smart phone available')</script>", false);


            this.ddlWorkEx.Focus();
            return;
        }
        if (Convert.ToInt32(ddlPhysicalStatus.SelectedValue) <= 0)
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Select Team Balika Physical Status.')</script>", false);


            this.ddlWorkEx.Focus();
            return;
        }
        if (Convert.ToInt32(ddlPhysicalStatus.SelectedValue) > 0)
        {
            if (Convert.ToInt32(ddlPhysicalStatus.SelectedValue) == 1)
            {
                if (Convert.ToInt32(ddlSpecially.SelectedValue) <= 0)
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert(' Please Select Type of Specially Abled')</script>", false);


                    this.ddlWorkEx.Focus();
                    return;
                }
            }
        }
                if (ddlWorkingStatus.SelectedIndex > 0)
        {
            if (Convert.ToInt32(ddlWorkingStatus.SelectedValue) == 1)
            {

            }
            else
            {
                if (Convert.ToInt32(ddlStatusReasone.SelectedValue) <= 0)
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Select Dropout Reason')</script>", false);


                    this.ddlWorkEx.Focus();
                    return;
                }
                if (txtDropDate.Text.Trim() == "")
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Enter Dropout Date')</script>", false);


                    this.txtDuartion.Focus();
                    return;
                }

                if (Convert.ToInt32(ddlStatusReasone.SelectedValue) == 14)
                {
                    if (txtJob.Text.Trim() == "")
                    {
                        ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please enter job type')</script>", false);


                        this.txtDuartion.Focus();
                        return;
                    }
                }
                if (Convert.ToInt32(ddlStatusReasone.SelectedValue) == 15)
                {
                    if (txtBus.Text.Trim() == "")
                    {
                        ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please enter Business Type')</script>", false);


                        this.txtDuartion.Focus();
                        return;
                    }
                }

                if (Convert.ToInt32(ddlEducation.SelectedValue) == 5 || Convert.ToInt32(ddlEducation.SelectedValue) == 7 || Convert.ToInt32(ddlEducation.SelectedValue) == 9)
                {
                    if (Convert.ToInt32(ddlSpecialization.SelectedValue) <= 0)
                    {
                        ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Select Education Specialization')</script>", false);


                        this.ddlWorkEx.Focus();
                        return;
                    }
                }
                    if (Convert.ToInt32(ddlStatusReasone.SelectedValue) == 14 || Convert.ToInt32(ddlStatusReasone.SelectedValue) == 15)
                {
                    if (divJobOp.Visible == true)
                    {
                        if (Convert.ToInt32(ddlJobOpportunity.SelectedValue) <= 0)
                        {
                            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select Team Balika Other opportunity through')</script>", false);


                            this.ddlWorkEx.Focus();
                            return;
                        }

                        if (Convert.ToInt32(ddlJobOpportunity.SelectedValue) == 4)
                        {
                            if (txtotherjob.Text.Trim() == "")
                            {
                                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Enter the Other Detail on the Team Balika Job Opportunity Through')</script>", false);


                                this.txtDuartion.Focus();
                                return;
                            }
                        }
                    }
                }


                string DropOutData1 = txtDropDate.Text;
                string[] D1;




                if (txtDropDate.Text != "")
                {
                    D1 = DropOutData1.Split('/');
                    if (Convert.ToInt32(D1[2]) < 2016)
                    {
                        ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Enter valid Dropout Date')</script>", false);


                        this.txtDuartion.Focus();
                        return;
                    }
                }
            }
        }
        if (ddlWorkingStatus.SelectedIndex > 0)
        {
            //if (Convert.ToInt32(ddlWorkingStatus.SelectedValue) == 2)
            //{
            if (Convert.ToInt32(ddlAlumni.SelectedValue) == 1)
            {
                if (txtAlumniDate.Text == "")
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Select Team Balika Alumni Date')</script>", false);


                    this.txtDuartion.Focus();
                    return;
                }
            }
            //}
        }

        if (txtFatherName.Text.Trim() == "" && txtMotherName.Text == "")
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Enter Mother/Father Name')</script>", false);

            this.txtFatherName.Focus();
            return;
        }

        string Name = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(txtName.Text);
        string FatherName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(txtFatherName.Text);
        string MotherName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(txtMotherName.Text);
        string Exp = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(txtExp.Text);
        string Abv = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(txtAbv.Text);
        string EmpName = "";
        string Designation = "";
        DateTime DateJoined = DateTime.MinValue;



        if (txtEmployeeID.Text.Length > 0)
        {
            DataTable dtEmpl = objMain.LoadData(" SELECT [Employee Name] as EMP,Designation,[Date Joined] as DateJoined FROM [dbo].[mstTempCurrentUser] where EmployeeCode ='" + txtEmployeeID.Text + "'");
            if (dtEmpl.Rows.Count > 0)
            {
                EmpName = dtEmpl.Rows[0]["EMP"].ToString();
                Designation = dtEmpl.Rows[0]["Designation"].ToString();
                DateJoined = Convert.ToDateTime(dtEmpl.Rows[0]["DateJoined"]);
            }
        }
        string DateofJoining1 = txtJoingDate.Text;
        string[] b = DateofJoining1.Split('/');
        string DateofJoining = b[2] + '-' + b[1] + '-' + b[0];


        string DropOutData = txtDropDate.Text;
        string[] D;
        string AlumniDateData = txtAlumniDate.Text;
        string[] A;
        string DropOutDate;
        string AlumniDate;
        DateTime DropOuEntryDate;
        if (txtAlumniDate.Text != "")
        {
            A = AlumniDateData.Split('/');
            AlumniDate = A[2] + '-' + A[1] + '-' + A[0];
        }
        else
        {
            AlumniDate = "1900-01-01";
        }

        if (txtDropDate.Text != "")
        {
            D = DropOutData.Split('/');
            DropOutDate = D[2] + '-' + D[1] + '-' + D[0];
            DropOuEntryDate = DateTime.Now;
        }
        else
        {
            DropOutDate = "1900-01-01";
            DropOuEntryDate = DateTime.MinValue;
        }
        DateTime DOB;
        DateTime AsDob;
        Int32 Age = 0;
        Int32 mmonth = 0;
        int WrokExp = 0;

        int mainResult = 0;
        string type = "";
        string strMainIDNo = "";

        if (Convert.ToInt32(ddlWorkEx.SelectedValue) == 1)
        {
            if (txtDuartion.Text != "")
            {
                WrokExp = Convert.ToInt32(txtDuartion.Text);
            }

            if (txtMonth.Text != "")
            {
                mmonth = Convert.ToInt32(txtMonth.Text);
            }
        }
        if (Convert.ToInt32(ddlDob.SelectedValue) == 1)
        {
            string DateB = txtDate.Text;
            string[] a = DateB.Split('/');
            string BithDate = a[2] + '-' + a[1] + '-' + a[0];



            Age = DateTime.Now.Year - Convert.ToInt32(a[2]);
            DOB = Convert.ToDateTime(a[2] + '-' + a[1] + '-' + a[0]);

            Int32 iyear = Convert.ToInt32(a[2]) + Age;
            string dyear = iyear.ToString();

            AsDob = DOB;
            Int32 Total = Convert.ToInt32(b[2]) - Convert.ToInt32(a[2]);
            if (Convert.ToInt32(ddlWorkingStatus.SelectedValue) == 1)
            {
                if (Total < 18)
                {

                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Age should be 18 years')</script>", false);


                    this.txtAge.Focus();
                    return;

                }
            }

            //    AsDob = Convert.ToDateTime(dyear + '-' + a[1] + '-' + a[0]);

            //if (Age < 3)
            //{

            //    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please ensure Age is between 3 and 14 years')</script>", false);


            //    this.txtAge.Focus();
            //    return;

            //}
            //if (Age > 14)
            //{
            //    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please ensure Age is between 3 and 14 years')</script>", false);


            //    this.txtAge.Focus();
            //    return;
            //}

        }
        else
        {
            string DateB = txtDate.Text;
            string[] a = DateB.Split('/');
            string BithDate = a[2] + '-' + a[1] + '-' + a[0];

            Age = Convert.ToInt32(txtAge.Text);
            AsDob = Convert.ToDateTime(a[2] + '-' + a[1] + '-' + a[0]);

            Int32 iyear = Convert.ToInt32(a[2]) - Age;
            string dyear = iyear.ToString();
            DOB = Convert.ToDateTime(dyear + '-' + a[1] + '-' + a[0]);

            Int32 Total = Convert.ToInt32(Convert.ToInt32(b[2]) - iyear);
            if (Total < 18)
            {

                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Age should be 18 years')</script>", false);


                this.txtAge.Focus();
                return;

            }


        }

        bool PriorWorkExperience = false;
        int Duartion = 0;
        int Specialization = 0;
        if (txtDuartion.Text != "")
        {
            Duartion = Convert.ToInt32(txtDuartion.Text);
        }
        if (Convert.ToInt32(ddlWorkEx.SelectedValue) == 1)
        {
            PriorWorkExperience = true;
        }

        if (Convert.ToInt32(ddlEducation.SelectedValue) == 5 || Convert.ToInt32(ddlEducation.SelectedValue) == 7 || Convert.ToInt32(ddlEducation.SelectedValue) == 9)
        {
            Specialization = Convert.ToInt32(ddlSpecialization.SelectedValue);
        }
            if (ViewState["Save"].ToString() == "Save")
        {
            DataTable dtCheck = objMain.LoadData(" SELECT * FROM [dbo].[mstTeamBalika]  inner join mst5Village on  mst5Village.VillageCode=mstTeamBalika.VillageCode or  mst5Village.refVillage16=mstTeamBalika.VillageCode	or  mst5Village.refVillage17=mstTeamBalika.VillageCode	or  mst5Village.refVillage18=mstTeamBalika.VillageCode	or  mst5Village.refVillage19=mstTeamBalika.VillageCode	or  mst5Village.refVillage20=mstTeamBalika.VillageCode  	or  mst5Village.refVillage21=mstTeamBalika.VillageCode			  where TBName='" + Name + "' and FatherName='" + FatherName + "' and   mst5Village.VillageCode='" + ddlVillage.SelectedValue + "' ");
            if (dtCheck.Rows.Count > 0)
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('TB Name Allready Exit')</script>", false);
                return;
            }
            Unique();
            string TBCode = ViewState["TBCode"].ToString();
            string schoolod = ViewState["NumNo"].ToString();
            string Fullfilename = "";

            if (FileuploadAttach.PostedFile != null && FileuploadAttach.PostedFile.FileName != "")
            {
                string ext = System.IO.Path.GetExtension(FileuploadAttach.PostedFile.FileName).ToLower();
                if (FileuploadAttach.PostedFile.ContentLength < 102400)
                {
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Image size must be less than 100kb')</script>", false);
                    return;
                }
                if (ext != ".jpeg" && ext != ".jpg" && ext != ".png" && ext != ".gif")
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Invalid Images')</script>", false);
                    return;
                }
                string exten = Path.GetExtension(FileuploadAttach.PostedFile.FileName);
                Fullfilename = "" + TBCode + "_" + DateTime.Now.ToString("ddMMyyyy_hhmmss") + exten;
            }

            ViewState["Save"] = "fff";


            strMainIDNo = objMain.Generate_RandomString(8);
            ViewState["TMCode"] = strMainIDNo;
            type = "I";

            #region Attach image
            //System.Drawing.Bitmap bmpPostedImage = new System.Drawing.Bitmap(FileuploadAttach.PostedFile.InputStream);
            //System.Drawing.Image objImage = ScaleImage(bmpPostedImage, 81);


            string sFileDir = Server.MapPath("~/DataBackup/");

            if (FileuploadAttach.PostedFile != null && FileuploadAttach.PostedFile.FileName != "")
            {
                string exten = Path.GetExtension(FileuploadAttach.PostedFile.FileName);
                // string Imagefile1 = "LeaveDoc" + "_" + Convert.ToString(Session["EMP_ID"]) + "_" + DateTime.Now.ToString("ddMMyyyy_hhmmss") + exten;

                //create directory

                if (Directory.Exists(sFileDir)) { }
                else { System.IO.Directory.CreateDirectory(sFileDir); }

                //======update the file =====\\

                if (System.IO.File.Exists(sFileDir + "\\" + Fullfilename))
                {
                    try { System.IO.File.Delete(sFileDir + "\\" + Fullfilename); }
                    catch (Exception ex)
                    {
                        //ShowMessage.Visible = true;
                        //ShowMessage.Style.Add("background-color", "#FFBABA");
                        //MessageLBL.Style.Add("Color", "#D8000C");
                        //MessageLBL.Text = ex.ToString();

                    }
                }
                FileuploadAttach.PostedFile.SaveAs(sFileDir + Fullfilename);

            }

            #endregion
            mainResult = SaveDataTeamBalika(strMainIDNo, schoolod, TBCode, ddlVillage.SelectedValue, Name, Convert.ToInt32(ddlGender.SelectedValue), FatherName, Convert.ToInt32(ddlCategory.SelectedValue), Convert.ToInt32(ddlEducation.SelectedValue), Convert.ToInt32(ddloccu.SelectedValue), Convert.ToInt32(ddlDob.SelectedValue), DOB, Age, AsDob, Convert.ToInt32(ddlReason.SelectedValue), Convert.ToInt32(ddlSours.SelectedValue), PriorWorkExperience, Duartion, mmonth, txtContact.Text, type, Exp, Abv, MotherName, Fullfilename, Convert.ToDateTime(DateofJoining), Convert.ToInt32(ddlWorkingStatus.SelectedValue), Convert.ToInt32(ddlStatusReasone.SelectedValue), Convert.ToDateTime(DropOutDate), Session["username"].ToString(), Convert.ToInt32(ddltbRecruited.SelectedValue), EmpName, Designation, DateJoined, ddlAlumni.SelectedValue, AlumniDate, DropOuEntryDate, Specialization);




            if (mainResult > 0)
            {


                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Saved sucessfully')</script>", false);
                GVMainBind();
                txtIDNO.Text = TBCode;
            }
        }
        else
        {
            type = "U";

            #region Attach image

            //  string sFileDir = Request.PhysicalApplicationPath + "ApplyLeaveDoc\\";
            string Fullfilename = Convert.ToString(ViewState["ImagePath"]);

            if (FileuploadAttach.PostedFile != null && FileuploadAttach.PostedFile.FileName != "")
            {

                string ext = System.IO.Path.GetExtension(FileuploadAttach.PostedFile.FileName).ToLower();
                if (ext != ".jpeg" && ext != ".jpg" && ext != ".png" && ext != ".gif")
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Invalid Images')</script>", false);
                    return;
                }
                string exten = Path.GetExtension(FileuploadAttach.PostedFile.FileName);
                Fullfilename = "" + txtIDNO.Text.Trim() + "_" + DateTime.Now.ToString("ddMMyyyy_hhmmss") + exten;
            }
            string sFileDir = Server.MapPath("~/DataBackup/");

            if (FileuploadAttach.PostedFile != null && FileuploadAttach.PostedFile.FileName != "")
            {
                string exten = Path.GetExtension(FileuploadAttach.PostedFile.FileName);
                // string Imagefile1 = "LeaveDoc" + "_" + Convert.ToString(Session["EMP_ID"]) + "_" + DateTime.Now.ToString("ddMMyyyy_hhmmss") + exten;

                //create directory

                if (Directory.Exists(sFileDir)) { }
                else { System.IO.Directory.CreateDirectory(sFileDir); }

                //======update the file =====\\

                if (System.IO.File.Exists(sFileDir + "\\" + Fullfilename))
                {
                    try { System.IO.File.Delete(sFileDir + "\\" + Fullfilename); }
                    catch (Exception ex)
                    {


                    }
                }
                FileuploadAttach.PostedFile.SaveAs(sFileDir + Fullfilename);

            }

            #endregion

            mainResult = SaveDataTeamBalika(ViewState["TMCode"].ToString(), "", "", ddlVillage.SelectedValue, Name, Convert.ToInt32(ddlGender.SelectedValue), FatherName, Convert.ToInt32(ddlCategory.SelectedValue), Convert.ToInt32(ddlEducation.SelectedValue), Convert.ToInt32(ddloccu.SelectedValue), Convert.ToInt32(ddlDob.SelectedValue), DOB, Age, AsDob, Convert.ToInt32(ddlReason.SelectedValue), Convert.ToInt32(ddlSours.SelectedValue), PriorWorkExperience, Duartion, mmonth, txtContact.Text, type, Exp, Abv, MotherName, Fullfilename, Convert.ToDateTime(DateofJoining), Convert.ToInt32(ddlWorkingStatus.SelectedValue), Convert.ToInt32(ddlStatusReasone.SelectedValue), Convert.ToDateTime(DropOutDate), Session["username"].ToString(), Convert.ToInt32(ddltbRecruited.SelectedValue), EmpName, Designation, DateJoined, ddlAlumni.SelectedValue, AlumniDate, DropOuEntryDate, Specialization);


            if (mainResult > 0)
            {

                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Update sucessfully')</script>", false);
                GVMainBind();
            }

        }



    }
    public int SaveDataTeamBalika(string strMainIDNo, string TcodeSerial, string Tcode, string VillageCode, string TBName, int Gender, string strFatherName, int SocialCategory, int EducationLevel, int FamilyOccupation, int DOBAvailable, DateTime DOB, int AgeAson, DateTime AsOnDate, int ReasonForTBChoice, int RecruitmentReferalInfo, bool PriorWorkExperience, int TotalPriorWorkExperience, int PriorWorkYearMonth, string Contact, string flag, string Expectation, string Abvision, string MotherName, string ImagePath, DateTime DateofJoining, int dropOutStatus, int DroupOutRe, DateTime DropoutResone, string createby, Int32 TbRecruited, string EmpName, string Designation, DateTime DateJoined, string Alumni, string AlumniDate, DateTime DropOuEntryDate, int Specialization)
    {
        SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("@UniqueCode", strMainIDNo),
            new SqlParameter("@TBCode", Tcode),
            new SqlParameter("@TBName", TBName),
            new SqlParameter("@VillageCode", VillageCode),
            new SqlParameter("@Gender", Gender),
            new SqlParameter("@FatherMotherName", strFatherName),
            new SqlParameter("@SocialCategory", SocialCategory),
            new SqlParameter("@EducationLevel", EducationLevel),
            new SqlParameter("@FamilyOccupation", FamilyOccupation),
            new SqlParameter("@DOBAvailable", DOBAvailable),
            new SqlParameter("@DOB", DOB),
            new SqlParameter("@AgeAson", AgeAson),
            new SqlParameter("@AsOnDate", AsOnDate),
            new SqlParameter("@ReasonForTBChoice", ReasonForTBChoice),
            new SqlParameter("@RecruitmentReferalInfo", RecruitmentReferalInfo),
            new SqlParameter("@PriorWorkExperience", PriorWorkExperience),
            new SqlParameter("@TotalPriorWorkExperience", TotalPriorWorkExperience),
            new SqlParameter("@PriorWorkYearMonth", PriorWorkYearMonth),
            new SqlParameter("@Contact", Contact),
            new SqlParameter("@flag", flag),
            new SqlParameter("@Expectation", Expectation),
            new SqlParameter("@Abvision", Abvision),
            new SqlParameter("@MotherName", MotherName),
            new SqlParameter("@TcodeSerial", TcodeSerial),
            new SqlParameter("@ImagePath", ImagePath),
            new SqlParameter("@DateofJoining", DateofJoining.ToString("yyyy-MM-dd")),
            new SqlParameter("@dropOutStatus", dropOutStatus),
            new SqlParameter("@DroupOutRe", DroupOutRe),
            new SqlParameter("@DropoutResone", DropoutResone),
            new SqlParameter("@createby", createby),
                new SqlParameter("@TbRecruited", TbRecruited),

                new SqlParameter("@AlternetPhoneNo", txtxAlternate.Text),
                new SqlParameter("@IsSmartPhone", ddlSmart.SelectedValue),
                  new SqlParameter("@EmpID", txtEmployeeID.Text),
                 new SqlParameter("@EmpName", EmpName),
 new SqlParameter("@Designation", Designation),
 new SqlParameter("@DateJoined",  DateJoined.ToString("yyyy-MM-dd")),
  new SqlParameter("@Alumni", Alumni),
 new SqlParameter("@AlumniDate",Convert.ToDateTime(AlumniDate).ToString("yyyy-MM-dd")),

   new SqlParameter("@rjob", txtJob.Text),
     new SqlParameter("@rBusiness", txtBus.Text),
       new SqlParameter("@rJobOpportunity", ddlJobOpportunity.SelectedValue),
         new SqlParameter("@rjobother", txtotherjob.Text),

  new SqlParameter("@dropoutEntrydate",DropOuEntryDate.ToString("yyyy-MM-dd")),
    new SqlParameter("@PhysicalStatus", ddlPhysicalStatus.SelectedValue),
      new SqlParameter("@Specially", ddlSpecially.SelectedValue),
       new SqlParameter("@Specialization", Specialization),
      


        };
        return SqlHelper.ExecuteNonQuery(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "InsertUpdateTeamBalikaNew2025", cmdParameters);
    }

    protected void ddlWork_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlWorkEx.SelectedIndex > 0)
        {
            if (Convert.ToInt32(ddlWorkEx.SelectedValue) == 1)
            {
                txtDuartion.Enabled = true;
                txtMonth.Enabled = true;
            }
            else
            {
                txtDuartion.Enabled = false;
                txtMonth.Enabled = false;
                txtDuartion.Text = "";
                txtMonth.Text = "";

            }
        }
        else
        {
            txtDuartion.Enabled = false;
            txtMonth.Enabled = false;
            txtDuartion.Text = "";
            txtMonth.Text = "";
        }
    }

    protected void ddlWorkingStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        divJob.Visible = false;
        txtJob.Text = "";

        divbus.Visible = false;
        txtBus.Text = "";
        divJobOp.Visible = false;
        ddlJobOpportunity.SelectedIndex = 0;
        divOtherJob.Visible = false;
        txtotherjob.Text = "";

        if (ddlWorkingStatus.SelectedIndex > 0)
        {


            if (Convert.ToInt32(ddlWorkingStatus.SelectedValue) == 1 && Convert.ToString(Session["VillageGeographyOperational"]) == "2")

            {
                rdate.Visible = false;
                Resone.Visible = false;
                divAlumni.Visible = true;
                txtDropDate.Text = "";
                ddlStatusReasone.SelectedIndex = 0;
                ddlAlumni.SelectedIndex = 0;
                divAlumni1.Visible = false;

                divJob.Visible = false;
                txtJob.Text = "";
                divbus.Visible = false;
                txtBus.Text = "";

            }
            else if (Convert.ToInt32(ddlWorkingStatus.SelectedValue) == 1)
            {
                rdate.Visible = false;
                Resone.Visible = false;
                divAlumni.Visible = false;

                ddlStatusReasone.SelectedIndex = 0;
                ddlAlumni.SelectedIndex = 0;
                divAlumni1.Visible = false;
                divJob.Visible = false;
                txtJob.Text = "";
                divbus.Visible = false;
                txtBus.Text = "";
            }
            else
            {
                divAlumni.Visible = true;
                txtEmployeeID.Text = "";
                divAlumni1.Visible = false;
                rdate.Visible = true;
                Resone.Visible = true;
                divJob.Visible = false;
                txtJob.Text = "";
                divbus.Visible = false;
                txtBus.Text = "";
            }
        }
        else
        {
            divAlumni1.Visible = false;
            divAlumni.Visible = false;
            txtEmployeeID.Text = "";
            rdate.Visible = false;
            Resone.Visible = false;
            txtDropDate.Text = "";
            divJob.Visible = false;
            txtJob.Text = "";
            divbus.Visible = false;
            txtBus.Text = "";

            ddlStatusReasone.SelectedIndex = 0;
        }
    }
    protected void ddlAlumni_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlAlumni.SelectedIndex > 0)
        {
            if (Convert.ToInt32(ddlAlumni.SelectedValue) == 1)
            {
                divAlumni1.Visible = true;
                txtAlumniDate.Text = "";
            }
            else
            {
                divAlumni1.Visible = false;
                txtAlumniDate.Text = "";
            }
        }
        else
        {
            divAlumni1.Visible = false;
            txtAlumniDate.Text = "";
        }
    }
    protected void ddlStatusReasone_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlWorkingStatus.SelectedIndex > 0)

        {
            divOtherJob.Visible = false;
            txtotherjob.Text = "";
            if (Convert.ToInt32(ddlWorkingStatus.SelectedValue) == 2 && Convert.ToInt32(ddlStatusReasone.SelectedValue) == 3)
            {
                txtEmployeeID.Text = "";
                EmpID.Visible = true;
                divJob.Visible = false;
                txtJob.Text = "";

                divbus.Visible = false;
                txtBus.Text = "";

                divJobOp.Visible = false;
                ddlJobOpportunity.SelectedIndex = 0;
                divOtherJob.Visible = false;
                txtotherjob.Text = "";

            }
            else if (Convert.ToInt32(ddlStatusReasone.SelectedValue) == 14)
            {
                EmpID.Visible = false;
                txtEmployeeID.Text = "";
                divJob.Visible = true;
                txtJob.Text = "";
                divbus.Visible = false;
                txtBus.Text = "";
                divJobOp.Visible = false;
                ddlJobOpportunity.SelectedIndex = 0;

                if (ViewState["Save"].ToString() == "Save")
                {
                    divJobOp.Visible = true;
                    ddlJobOpportunity.SelectedIndex = 0;
                }
                else
                {
                    DataTable dtCheck = objMain.LoadData(" SELECT  *FROm [tblTraining] inner join tblTrainingDetail on tblTrainingDetail.[TBUniqueCode]=UniqueCode where  convert(int, isnull(tblTraining.DeleteFlag,0))<>2 and [Learningtype] =8 and fromdate>='2024-04-01' and TBid='" + ViewState["TMCode"].ToString() + "' ");
                    if (dtCheck.Rows.Count > 0)
                    {
                        divJobOp.Visible = false;
                        ddlJobOpportunity.SelectedIndex = 0;
                    }
                    else
                    {
                        divJobOp.Visible = true;
                        ddlJobOpportunity.SelectedIndex = 0;
                    }
                }


            }
            else if (Convert.ToInt32(ddlStatusReasone.SelectedValue) == 15)
            {
                EmpID.Visible = false;
                txtEmployeeID.Text = "";
                divJob.Visible = false;
                txtJob.Text = "";

                divbus.Visible = true;
                txtBus.Text = "";

                divJobOp.Visible = false;
                ddlJobOpportunity.SelectedIndex = 0;

                if (ViewState["Save"].ToString() == "Save")
                {
                    divJobOp.Visible = true;
                    ddlJobOpportunity.SelectedIndex = 0;
                }
                else
                {
                    DataTable dtCheck = objMain.LoadData(" SELECT  *FROm [tblTraining] inner join tblTrainingDetail on tblTrainingDetail.[TBUniqueCode]=UniqueCode where [Learningtype] =8 and fromdate>='2024-04-01' and TBid='" + ViewState["TMCode"].ToString() + "' ");
                    if (dtCheck.Rows.Count > 0)
                    {
                        divJobOp.Visible = false;
                        ddlJobOpportunity.SelectedIndex = 0;
                    }
                    else
                    {
                        divJobOp.Visible = true;
                        ddlJobOpportunity.SelectedIndex = 0;
                    }
                }
            }
            else
            {

                EmpID.Visible = false;
                txtEmployeeID.Text = "";

                divJob.Visible = false;
                txtJob.Text = "";
                divbus.Visible = false;
                txtBus.Text = "";
                divJobOp.Visible = false;
                ddlJobOpportunity.SelectedIndex = 0;
            }
        }
        else
        {
            divJob.Visible = false;
            txtJob.Text = "";
            txtEmployeeID.Text = "";
            EmpID.Visible = false;
            divbus.Visible = false;
            txtBus.Text = "";
            divJobOp.Visible = false;
            ddlJobOpportunity.SelectedIndex = 0;
            divOtherJob.Visible = false;
            txtotherjob.Text = "";
        }
    }

    protected void ddlOther_SelectedIndexChanged(object sender, EventArgs e)
    {

        if (Convert.ToInt32(ddlJobOpportunity.SelectedValue) == 4)
        {
            divOtherJob.Visible = true;
            txtotherjob.Text = "";
        }
        else
        {
            divOtherJob.Visible = false;
            txtotherjob.Text = "";
        }

    }
    protected void ddlDob_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Convert.ToInt32(ddlDob.SelectedValue) == 1)
        {
            lblDob.Text = "DOB";
            lblAge.Enabled = false;
            txtAge.Enabled = false;
            txtDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            txtDate.Enabled = true;
        }
        else
        {
            txtDate.Enabled = false;
            DateTime ydate = new DateTime(DateTime.Now.Year, 05, 01);

            txtDate.Text = ydate.ToString("dd/MM/yyyy");
            lblDob.Text = "As On";
            lblAge.Enabled = true;
            txtAge.Enabled = true;
        }
    }
    private void RefreshControl()
    {
        #region RefreshControl
        txtday.Text = "";
        ViewState["TMCode"] = null;
        ViewState["TBCode"] = null;
        ViewState["ImagePath"] = null;
        txtExp.Text = ""; txtAbv.Text = "";
        txtIDNO.Text = "Auto generated number";
        txtName.Text = string.Empty;
        txtDate.Text = string.Empty;
        ddlDob.SelectedIndex = 2;
        DateTime ydate = new DateTime(DateTime.Now.Year, 05, 01);
        txtJoingDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
        txtDate.Text = ydate.ToString("dd/MM/yyyy");


        ddlWorkEx.SelectedIndex = 0;
        txtDate.Enabled = false;
        txtFatherName.Text = string.Empty;
        txtContact.Text = string.Empty;
        txtAge.Text = string.Empty;
        txtxAlternate.Text = string.Empty;
        ddlSmart.SelectedIndex = 0;
        txtDuartion.Text = string.Empty;

        ddlGender.SelectedIndex = 0;
        ddlEducation.SelectedIndex = 0;
        ddloccu.SelectedIndex = 0;
        ddlCategory.SelectedIndex = 0;
        ddlReason.SelectedIndex = 0;
        ddlSours.SelectedIndex = 0;
        ddlWorkingStatus.SelectedIndex = 0;
        txtMonth.Text = "";
        txtMotherName.Text = "";

        ddlAlumni.SelectedIndex = 0;
        txtAlumniDate.Text = "";
        txtEmployeeID.Text = "";
        divAlumni.Visible = false;
        divAlumni1.Visible = false;
        ViewState["Save"] = "Save";
        divJob.Visible = false;
        txtJob.Text = "";

        divbus.Visible = false;
        txtBus.Text = "";
        divJobOp.Visible = false;
        ddlJobOpportunity.SelectedIndex = 0;
        divOtherJob.Visible = false;
        txtotherjob.Text = "";
        ViewState["TMCode"] = null;
        divSp.Visible = false;
        ddlSpecially.SelectedIndex = 0;
        #endregion
    }

    #region Gaurav TBIDCard

    protected void btnAdd2_Click(object sender, EventArgs e)
    {
        string templatePdf =
                Server.MapPath("~/Templates/Template.pdf");
        

        string folder =
            Server.MapPath("~/GeneratedPDF");

        if (!Directory.Exists(folder))
            Directory.CreateDirectory(folder);

        string outputPdf =
            Path.Combine(folder, "IdCard.pdf");
        DataTable dtmstMNew = objMain.LoadData(" SELECT[UniqueCode],ImagePath,DistirctOffice,egvillagecode,RequestedDate, [TBCode],[TBName],[Villagename], mstCluster.ClusterName, DistrictName,[Gender],[Active],[FatherName],[MotherName], RIGHT('0' + CAST(DAY([DOB]) AS VARCHAR(2)), 2) + '-' +    LEFT(DATENAME(MONTH, [DOB]), 3) + '-' + CAST(YEAR([DOB]) AS VARCHAR(4)) AS DOB,[AgeAson],[AsOnDate],[Contact], RIGHT('0' + CAST(DAY([DOB]) AS VARCHAR(2)), 2) +'-' + LEFT(DATENAME(MONTH, DateofJoining), 3) + '-' + CAST(YEAR(DateofJoining) AS VARCHAR(4)) AS DateofJoining,[PriorWorkYearMonth],[TBCode] as UniqueId ,isnull(IsSmartPhone, 0) IsSmartPhone,AlternetPhoneNo FROM[dbo].[mstTeamBalika]        inner join mst5Village on mst5Village.VillageCode = mstTeamBalika.VillageCode inner join mst2District on mst2District.DistrictCode = mst5Village.DistrictCode left join mstCluster on mstCluster.ClusterCode = mst5Village.ClusterCode where UniqueCode ='" + ViewState["TBCode"] + "'");
        IdCardModel model = new IdCardModel();

        if (dtmstMNew.Rows.Count > 0)
        {
            DateTime requestedDate = Convert.ToDateTime(dtmstMNew.Rows[0]["RequestedDate"]);
            DateTime oneYearLater = requestedDate.AddYears(1);
            string imagePath = Server.MapPath("~/images/blank_img.png");

            if (!string.IsNullOrWhiteSpace(dtmstMNew.Rows[0]["ImagePath"].ToString()))
            {
                imagePath = Server.MapPath(
                    "~/DataBackup/" + dtmstMNew.Rows[0]["ImagePath"].ToString().Trim());
            }

                model.Name = dtmstMNew.Rows[0]["TBName"].ToString().Trim();
                model.Village = dtmstMNew.Rows[0]["Villagename"].ToString().Trim();
                model.TeamCode = dtmstMNew.Rows[0]["TBCode"].ToString().Trim();
                model.DateOfJoining = dtmstMNew.Rows[0]["DateofJoining"].ToString().Trim();
                model.Cluster = dtmstMNew.Rows[0]["ClusterName"].ToString().Trim();

                model.FatherName = dtmstMNew.Rows[0]["FatherName"].ToString().Trim();
                model.DOB = dtmstMNew.Rows[0]["DOB"].ToString().Trim();
                model.ContactNo = dtmstMNew.Rows[0]["Contact"].ToString().Trim();
                model.Validity = requestedDate.ToString("dd-MM-yyyy") + " - " + oneYearLater.ToString("dd-MM-yyyy");
                model.OfficeAddress =  Scalar("select DistirctOffice from mst2district where DistrictCode = '" + ddlDistrict.SelectedValue+ "' and DistirctOffice is not null");
            model.Cluster = Scalar("select ClusterName from mstcluster where DistrictCode = '" + ddlDistrict.SelectedValue + "' and clustercode in( select clustercode from mst5village where fyear='2026-2027' and egvillagecode='" + dtmstMNew.Rows[0]["egvillagecode"].ToString().Trim() + "' )  "); ;

            model.PhotoPath = imagePath;


        }



        byte[] pdfBytes = GenerateIdCardFromTemplate(model, templatePdf);

        string sql = @"INSERT INTO DownloadAudit
                (TBCode, DownloadedBy)
                VALUES
                (@TBCode, @DownloadedBy)";

        
        SqlConnection con = new SqlConnection(SqlHelper.mainConnectionString);
        SqlCommand cmd = new SqlCommand(sql, con);
        cmd.Parameters.AddWithValue("@TBCode", model.TeamCode);
        cmd.Parameters.AddWithValue("@DownloadedBy", Convert.ToString(Session["UserID"]));

        con.Open();
        cmd.ExecuteNonQuery();
        con.Close();

        Response.Clear();
        Response.ContentType = "application/pdf";
        Response.AddHeader(
            "Content-Disposition",
            "attachment; filename="+ model.TeamCode + "_IdCard.pdf");

        Response.BinaryWrite(pdfBytes);
        Response.Flush();
        HttpContext.Current.ApplicationInstance.CompleteRequest();


    }

    private string Scalar(string sql)
    {
        using (var cn = new SqlConnection(SqlHelper.mainConnectionString))
        using (var cmd = new SqlCommand(sql, cn))
        { cn.Open(); var o = cmd.ExecuteScalar(); return o == null || o == DBNull.Value ? "" : o.ToString(); }
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
    public static byte[] GenerateIdCardFromTemplatehhh(IdCardModel model, string templatePdf)
    {
        var reader = new PdfReader(templatePdf);

        // ---- SANITY CHECK for the front/back size problem ----------------
        // If the two template pages are different sizes, every card will be
        // misaligned. Fix the TEMPLATE (both pages same MediaBox); this only
        // surfaces the problem so it isn't silent.
        iTextSharp.text.Rectangle p1 = reader.GetPageSizeWithRotation(1);
        iTextSharp.text.Rectangle p2 = reader.GetPageSizeWithRotation(2);
        //if (Math.Abs(p1.Width - p2.Width) > 1f || Math.Abs(p1.Height - p2.Height) > 1f)
        //{
        //    System.Diagnostics.Debug.WriteLine(                $"WARNING: template front ({p1.Width}x{p1.Height}) and back " +                $"({p2.Width}x{p2.Height}) are different sizes. Fix the template.");
        //}

        using (var ms = new MemoryStream())
        {
            var stamper = new PdfStamper(reader, ms);

            BaseFont bf = BaseFont.CreateFont(
                BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
            var font = new Font(bf, 12);
            var addressFont = new Font(bf, 10); // smaller so the address fits

            // ---------------- PAGE 1 (FRONT) ----------------
            PdfContentByte cb1 = stamper.GetOverContent(1);
            cb1.BeginText();
            cb1.SetFontAndSize(bf, 12);
            cb1.ShowTextAligned(Element.ALIGN_LEFT, model.Name ?? "", 130, 450, 0);
            cb1.ShowTextAligned(Element.ALIGN_LEFT, model.Village ?? "", 130, 395, 0);
            cb1.ShowTextAligned(Element.ALIGN_LEFT, model.TeamCode ?? "", 130, 340, 0);
            cb1.ShowTextAligned(Element.ALIGN_LEFT, model.DateOfJoining ?? "", 130, 285, 0);
            cb1.ShowTextAligned(Element.ALIGN_LEFT, model.Cluster ?? "", 130, 230, 0);
            cb1.EndText();
            if (!string.IsNullOrEmpty(model.PhotoPath) && File.Exists(model.PhotoPath))
            {
                Image photo = iTextSharp.text.Image.GetInstance(model.PhotoPath);

                float diameter = 130f;
                float x = 237f;              // lower-left of the photo box (same as before)
                float y = 565f;
                float r = diameter / 2f;     // radius
                float cx = x + r;            // circle center X
                float cy = y + r;            // circle center Y

                photo.ScaleAbsolute(diameter, diameter);
                photo.SetAbsolutePosition(x, y);

                cb1.SaveState();
                cb1.Circle(cx, cy, r);       // define circular clip path
                cb1.Clip();
                cb1.NewPath();               // apply clip without drawing the circle
                cb1.AddImage(photo);         // image now shows only inside the circle
                cb1.RestoreState();          // stop clipping so nothing else is affected
            }
            //if (!string.IsNullOrEmpty(model.PhotoPath) && File.Exists(model.PhotoPath))
            //{
            //    Image photo = iTextSharp.text.Image.GetInstance(model.PhotoPath);
            //    photo.ScaleAbsolute(130f, 130f);
            //    photo.SetAbsolutePosition(237, 565);
            //    cb1.AddImage(photo);
            //}

            // ---------------- PAGE 2 (BACK) ----------------
            PdfContentByte cb2 = stamper.GetOverContent(2);
            cb2.BeginText();
            cb2.SetFontAndSize(bf, 12);
            cb2.ShowTextAligned(Element.ALIGN_LEFT, model.FatherName ?? "", 142, 605, 0);
            cb2.ShowTextAligned(Element.ALIGN_LEFT, model.DOB ?? "", 142, 536, 0);
            cb2.ShowTextAligned(Element.ALIGN_LEFT, model.ContactNo ?? "", 142, 470, 0);
            cb2.ShowTextAligned(Element.ALIGN_LEFT, model.Validity ?? "", 142, 400, 0);
            cb2.EndText();

            // QR code
            string qrText = "www.educategirls.ngo";
            var qrCode = new BarcodeQRCode(qrText, 150, 150, null);
            Image qrImage = qrCode.GetImage();
            qrImage.ScaleAbsolute(145f, 145f);
            qrImage.SetAbsolutePosition(227, 167);
            cb2.AddImage(qrImage);

            // ---- OFFICE ADDRESS: wrap inside a box so it never clips -------
            // SetSimpleColumn(llx, lly, urx, ury). urx is bound to the page
            // width so long addresses wrap onto multiple lines instead of
            // running off the right edge ("...Uttar Prades").
            float leftX = 142f;
            float rightX = p2.Width - 20f;   // right margin of the card
            float topY = 335f;             // start (same as before)
            float bottomY = 250f;            // room for ~3 wrapped lines
            var ct = new ColumnText(cb2);
            ct.SetSimpleColumn(leftX, bottomY, rightX, topY);
            ct.Leading = 12f;
            ct.Alignment = Element.ALIGN_LEFT;
            ct.SetText(new Phrase(model.OfficeAddress ?? "", addressFont));
            ct.Go();

            stamper.Close();
            reader.Close();
            return ms.ToArray();
        }
    }

    // Calibrated for the NEW card template (Template.pdf):
    //   Page size : 253.42 x 341.29 pt  (both pages)
    //   MediaBox  : [-36, -36, 217.42, 305.29]  <-- origin is NOT (0,0)
    // All coordinates below are TRUE PDF user-space values, verified against the template
    // artwork. Because content is drawn in user space, the negative MediaBox origin is
    // handled automatically by iTextSharp; the numbers just have to be correct.
    //
    // Pass Template.pdf as `templatePdf` and the output inherits its size/width.
    public static byte[] GenerateIdCardFromTemplate(IdCardModel model, string templatePdf)
    {
        using (PdfReader reader = new PdfReader(templatePdf))
        using (MemoryStream ms = new MemoryStream())
        {
            using (PdfStamper stamper = new PdfStamper(reader, ms))
            {
                // Helvetica cannot render Devanagari; the Hindi text is baked into the
                // template, so this is only used for the Latin model values.
                BaseFont bf = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                const float SIZE = 11f;   // matches the label size on this A4 layout

                //---------------- PAGE 1 (front) ----------------//
                const float X1 = 132f;    // left edge, aligned under the field labels
                PdfContentByte cb1 = stamper.GetOverContent(1);
                cb1.BeginText();
                cb1.SetFontAndSize(bf, SIZE);
                cb1.ShowTextAligned(Element.ALIGN_LEFT, model.Name, X1, 446, 0); // NAME
                cb1.ShowTextAligned(Element.ALIGN_LEFT, model.Village, X1, 390, 0); // VILLAGE
                cb1.ShowTextAligned(Element.ALIGN_LEFT, model.TeamCode, X1, 334, 0); // TEAM BALIKA CODE
                cb1.ShowTextAligned(Element.ALIGN_LEFT, model.DateOfJoining, X1, 279, 0); // DATE OF JOINING
                cb1.ShowTextAligned(Element.ALIGN_LEFT, model.Cluster, X1, 223, 0); // CLUSTER
                cb1.EndText();

                if (!string.IsNullOrEmpty(model.PhotoPath) && File.Exists(model.PhotoPath))
                {
                    Image photo = iTextSharp.text.Image.GetInstance(model.PhotoPath);

                    float diameter = 130f;
                    float x = 237f;              // lower-left of the photo box (same as before)
                    float y = 565f;
                    float r = diameter / 2f;     // radius
                    float cx = x + r;            // circle center X
                    float cy = y + r;            // circle center Y

                    photo.ScaleAbsolute(diameter, diameter);
                    photo.SetAbsolutePosition(x, y);

                    cb1.SaveState();
                    cb1.Circle(cx, cy, r);       // define circular clip path
                    cb1.Clip();
                    cb1.NewPath();               // apply clip without drawing the circle
                    cb1.AddImage(photo);         // image now shows only inside the circle
                    cb1.RestoreState();          // stop clipping so nothing else is affected
                }
                //---------------- PAGE 2 (back) ----------------//
                const float X2 = 143f;
                PdfContentByte cb2 = stamper.GetOverContent(2);
                cb2.BeginText();
                cb2.SetFontAndSize(bf, SIZE);
                cb2.ShowTextAligned(Element.ALIGN_LEFT, model.FatherName, X2, 597, 0); // FATHER'S NAME
                cb2.ShowTextAligned(Element.ALIGN_LEFT, model.DOB, X2, 531, 0); // DATE OF JOINING/BIRTH
                cb2.ShowTextAligned(Element.ALIGN_LEFT, model.ContactNo, X2, 465, 0); // CONTACT NUMBER
                cb2.ShowTextAligned(Element.ALIGN_LEFT, model.Validity, X2, 399, 0); // VALIDITY PERIOD FROM - TO
                cb2.EndText();

                // OFFICE ADDRESS — value wraps automatically in the gap ABOVE the underline
                // (between the label at ~365 and the underline at ~330). The QR box starts at
                // the underline, so all address text must stay above it. Fits ~3 lines.
                ColumnText ct = new ColumnText(cb2);
                ct.SetSimpleColumn(X2, 330f, 400f, 361f); // left, bottom, right, top
                ct.Leading = 9.5f;
                ct.Alignment = Element.ALIGN_LEFT;
                ct.AddText(new Phrase(model.OfficeAddress, new Font(bf, 8.5f)));
                ct.Go();
                // QR — fills the white placeholder square (measured x:225-377, y:165-331).
                BarcodeQRCode qr = new BarcodeQRCode("www.educategirls.ngo", 150, 150, null);
                Image qrImage = qr.GetImage();
                qrImage.ScaleAbsolute(150f, 150f);
                qrImage.SetAbsolutePosition(226f, 173f);
                cb2.AddImage(qrImage);
            }
            return ms.ToArray();
        }
    }


    //public static byte[] GenerateIdCardFromTemplate(IdCardModel model, string templatePdf)
    //{
    //    using (PdfReader reader = new PdfReader(templatePdf))
    //    using (MemoryStream ms = new MemoryStream())
    //    {
    //        using (PdfStamper stamper = new PdfStamper(reader, ms))
    //        {
    //            // See note below about Devanagari — Helvetica cannot render Hindi.
    //            BaseFont bf = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
    //            const float SIZE = 8f;
    //            const float X = 28f;

    //            //---------------- PAGE 1 ----------------//
    //            PdfContentByte cb1 = stamper.GetOverContent(1);
    //            cb1.BeginText();
    //            cb1.SetFontAndSize(bf, SIZE);
    //            cb1.ShowTextAligned(Element.ALIGN_LEFT, model.Name, X, 113, 0);
    //            cb1.ShowTextAligned(Element.ALIGN_LEFT, model.Village, X, 97, 0);
    //            cb1.ShowTextAligned(Element.ALIGN_LEFT, model.TeamCode, X, 78, 0);
    //            cb1.ShowTextAligned(Element.ALIGN_LEFT, model.DateOfJoining, X, 60, 0);
    //            cb1.ShowTextAligned(Element.ALIGN_LEFT, model.Cluster, X, 41, 0);
    //            cb1.EndText();
    //            if (File.Exists(model.PhotoPath))
    //            {
    //                const float cx = 126.5f, cy = 224.5f, rad = 38.5f;
    //                Image photo = iTextSharp.text.Image.GetInstance(model.PhotoPath);
    //                photo.ScaleAbsolute(rad * 2f, rad * 2f);          // 77 x 77
    //                photo.SetAbsolutePosition(cx - rad, cy - rad);    // = (88, 186)  <-- this is the bit that's wrong now
    //                cb1.SaveState();
    //                cb1.Circle(cx, cy, rad);   // clip to the circle so nothing spills onto the red
    //                cb1.Clip();
    //                cb1.NewPath();
    //                cb1.AddImage(photo);
    //                cb1.RestoreState();
    //            }


    //            //---------------- PAGE 2 ----------------//
    //            PdfContentByte cb2 = stamper.GetOverContent(2);
    //            cb2.BeginText();
    //            cb2.SetFontAndSize(bf, SIZE);
    //            cb2.ShowTextAligned(Element.ALIGN_LEFT, model.FatherName, X, 197, 0);
    //            cb2.ShowTextAligned(Element.ALIGN_LEFT, model.DOB, X, 177, 0);
    //            cb2.ShowTextAligned(Element.ALIGN_LEFT, model.ContactNo, X, 154, 0);
    //            cb2.ShowTextAligned(Element.ALIGN_LEFT, model.Validity, X, 132, 0);
    //            cb2.EndText();

    //            // OFFICE ADDRESS — left column, under its label
    //            ColumnText ct = new ColumnText(cb2);
    //            ct.SetSimpleColumn(24f, 42f, 96f, 114f); // left, bottom, right, top — left of the box
    //            ct.Leading = 8.5f;
    //            ct.Alignment = Element.ALIGN_LEFT;
    //            ct.AddText(new Phrase(model.OfficeAddress, new Font(bf, 7f)));
    //            ct.Go();

    //            // QR — sized for the card, bottom-right of the address band
    //            BarcodeQRCode qr = new BarcodeQRCode("www.educategirls.ngo", 150, 150, null);
    //            Image qrImage = qr.GetImage();
    //            qrImage.ScaleAbsolute(55f, 55f);
    //            qrImage.SetAbsolutePosition(99f, 79f);

    //            // sits inside the template's box
    //            cb2.AddImage(qrImage);
    //        }
    //        // stamper flushed on dispose; reader disposed by using
    //        return ms.ToArray();
    //    }
    //}
    //public static byte[] GenerateIdCardFromTemplate(
    //    IdCardModel model,
    //    string templatePdf)
    //{
    //    PdfReader reader = new PdfReader(templatePdf);

    //    using (MemoryStream ms = new MemoryStream())
    //    {
    //        PdfStamper stamper = new PdfStamper(reader, ms);

    //        BaseFont bf = BaseFont.CreateFont(
    //            BaseFont.HELVETICA,
    //            BaseFont.CP1252,
    //            BaseFont.NOT_EMBEDDED);

    //        //---------------- PAGE 1 ----------------//

    //        PdfContentByte cb1 = stamper.GetOverContent(1);

    //        cb1.BeginText();
    //        cb1.SetFontAndSize(bf, 12);

    //        cb1.ShowTextAligned(Element.ALIGN_LEFT, model.Name, 130, 450, 0);
    //        cb1.ShowTextAligned(Element.ALIGN_LEFT, model.Village, 130, 395, 0);
    //        cb1.ShowTextAligned(Element.ALIGN_LEFT, model.TeamCode, 130, 340, 0);
    //        cb1.ShowTextAligned(Element.ALIGN_LEFT, model.DateOfJoining, 130, 285, 0);
    //        cb1.ShowTextAligned(Element.ALIGN_LEFT, model.Cluster, 130, 230, 0);

    //        cb1.EndText();

    //        if (File.Exists(model.PhotoPath))
    //        {
    //            Image photo = iTextSharp.text.Image.GetInstance(model.PhotoPath);

    //            photo.ScaleAbsolute(130f, 130f);
    //            photo.SetAbsolutePosition(237, 565);

    //            stamper.GetOverContent(1).AddImage(photo);
    //        }

    //        //---------------- PAGE 2 ----------------//

    //        PdfContentByte cb2 = stamper.GetOverContent(2);

    //        cb2.BeginText();
    //        cb2.SetFontAndSize(bf, 12);

    //        cb2.ShowTextAligned(Element.ALIGN_LEFT, model.FatherName, 142, 605, 0);
    //        cb2.ShowTextAligned(Element.ALIGN_LEFT, model.DOB, 142, 536, 0);
    //        cb2.ShowTextAligned(Element.ALIGN_LEFT, model.ContactNo, 142, 470, 0);
    //        cb2.ShowTextAligned(Element.ALIGN_LEFT, model.Validity, 142, 400, 0);

    //        cb2.EndText();

    //        string qrText = "www.educategirls.ngo";

    //        BarcodeQRCode qrCode = new BarcodeQRCode(qrText, 150, 150, null);
    //        Image qrImage = qrCode.GetImage();

    //        qrImage.ScaleAbsolute(145f, 145f);
    //        qrImage.SetAbsolutePosition(227, 167);

    //        cb2.AddImage(qrImage);

    //            Font addressFont = new Font(bf, 10);

    //            ColumnText ct = new ColumnText(cb2);

    //            // Left, Bottom, Right, Top
    //            ct.SetSimpleColumn(
    //                142f,   // Left
    //                305f,   // Bottom (just above QR code)
    //                390f,   // Right
    //                360f    // Top (just below "OFFICE ADDRESS")
    //            );

    //            ct.Leading = 11f;   // Reduce line spacing
    //            ct.Alignment = Element.ALIGN_LEFT;
    //            ct.AddText(new Phrase(model.OfficeAddress, addressFont));

    //            ct.Go();
    //            //string address = model.OfficeAddress;

    //            //int maxLength = 38;

    //            //if (address.Length > maxLength)
    //            //{
    //            //    int split = address.LastIndexOf(' ', maxLength);
    //            //    if (split < 0) split = maxLength;

    //            //    string line1 = address.Substring(0, split);
    //            //    string line2 = address.Substring(split).Trim();

    //            //    cb2.BeginText();
    //            //    cb2.SetFontAndSize(bf, 10);

    //            //    cb2.ShowTextAligned(Element.ALIGN_LEFT, line1, 142, 335, 0);
    //            //    cb2.ShowTextAligned(Element.ALIGN_LEFT, line2, 142, 322, 0);

    //            //    cb2.EndText();
    //            //}
    //            //else
    //            //{
    //            //    cb2.BeginText();
    //            //    cb2.SetFontAndSize(bf, 10);
    //            //    cb2.ShowTextAligned(Element.ALIGN_LEFT, address, 142, 335, 0);
    //            //    cb2.EndText();
    //            //}


    //            stamper.Close();
    //        reader.Close();

    //        return ms.ToArray();
    //    }
    //}
    //    public static void GenerateIdCardFromTemplate(
    //    IdCardModel model,
    //    string templatePdf,
    //    string outputPdf)
    //    {
    //        PdfReader reader = new PdfReader(templatePdf);
    //        using (FileStream fs = new FileStream(outputPdf, FileMode.Create))
    //        {
    //            PdfStamper stamper = new PdfStamper(reader, fs);

    //            BaseFont bf = BaseFont.CreateFont(
    //                BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);

    //            //---------------- PAGE 1 (FRONT) ----------------//
    //            // Measured field underlines (pts from bottom): 443.5, 388.1, 331.9, 276.5, 221.0
    //            // Text baseline sits ~6-7 pt above each line.
    //            PdfContentByte cb1 = stamper.GetOverContent(1);
    //            cb1.BeginText();
    //            cb1.SetFontAndSize(bf, 12);
    //            cb1.ShowTextAligned(Element.ALIGN_LEFT, model.Name,          142, 427, 0);
    //cb1.ShowTextAligned(Element.ALIGN_LEFT, model.Village,       142, 371, 0);
    //cb1.ShowTextAligned(Element.ALIGN_LEFT, model.TeamCode,      142, 314, 0);
    //cb1.ShowTextAligned(Element.ALIGN_LEFT, model.DateOfJoining, 142, 258, 0);
    //cb1.ShowTextAligned(Element.ALIGN_LEFT, model.Cluster,       142, 202, 0);

    //            cb1.EndText();

    //            // Photo (in the circle on the front)
    //            if (File.Exists(model.PhotoPath))
    //            {
    //                iTextSharp.text.Image photo =
    //                    iTextSharp.text.Image.GetInstance(model.PhotoPath);
    //                photo.ScaleAbsolute(130f, 130f);
    //                photo.SetAbsolutePosition(237, 565);   // adjust to center in the circle
    //                cb1.AddImage(photo);
    //            }

    //            //---------------- PAGE 2 (BACK) ----------------//
    //            // Measured field underlines: 594.7, 529.2, 463.0, 396.7, 331.2 (OFFICE ADDRESS)
    //            PdfContentByte cb2 = stamper.GetOverContent(2);
    //            cb2.BeginText();
    //            cb2.SetFontAndSize(bf, 12);
    //            cb2.ShowTextAligned(Element.ALIGN_LEFT, model.FatherName, 145, 601, 0);
    //            cb2.ShowTextAligned(Element.ALIGN_LEFT, model.DOB, 145, 536, 0);
    //            cb2.ShowTextAligned(Element.ALIGN_LEFT, model.ContactNo, 145, 470, 0);
    //            cb2.ShowTextAligned(Element.ALIGN_LEFT, model.Validity, 145, 404, 0);
    //            cb2.EndText();

    //            // OFFICE ADDRESS — WRAPPED in a bounded box so long addresses don't overflow.
    //            // Box: x 145..455 (card width), y 335 (just above the underline) up to 378 (below the label).
    //            // DELETE any line like this if it still exists:
    //            // ColumnText.ShowTextAligned(cb2, Element.ALIGN_LEFT,
    //            //     new Phrase(model.OfficeAddress, new Font(bf, 12)), 142, 335, 0);

    //            // OFFICE ADDRESS — wrapped box. Narrower urx = forces wrapping onto multiple lines.

    //            // Manual word-wrap: break the address into lines that fit the card width, then draw each line.
    //            string addr = model.OfficeAddress ?? "";
    //            float x = 142f;          // left
    //            float startY = 375f;     // top line baseline
    //            float lineGap = 11f;     // space between lines
    //            float maxWidth = 310f;   // card width available (452 - 142)
    //            float fontSize = 8.5f;

    //            cb2.BeginText();
    //            cb2.SetFontAndSize(bf, fontSize);

    //            string[] words = addr.Split(' ');
    //            string line = "";
    //            float y = startY;

    //            foreach (string word in words)
    //            {
    //                string test = (line.Length == 0) ? word : line + " " + word;
    //                float w = bf.GetWidthPoint(test, fontSize);   // width of the test line in points

    //                if (w > maxWidth && line.Length > 0)
    //                {
    //                    // current line is full -> draw it, start a new line
    //                    cb2.ShowTextAligned(Element.ALIGN_LEFT, line, x, y, 0);
    //                    y -= lineGap;
    //                    line = word;
    //                }
    //                else
    //                {
    //                    line = test;
    //                }
    //            }
    //            // draw the last line
    //            if (line.Length > 0)
    //                cb2.ShowTextAligned(Element.ALIGN_LEFT, line, x, y, 0);

    //            cb2.EndText();
    //            // QR CODE — always draw it (it comes from TeamCode, NOT from the photo file).
    //            string qrText = model.TeamCode; // or a URL
    //            BarcodeQRCode qrCode = new BarcodeQRCode(qrText, 150, 150, null);
    //            Image qrImage = qrCode.GetImage();
    //            qrImage.ScaleAbsolute(145f, 145f);
    //            qrImage.SetAbsolutePosition(227, 167);
    //            cb2.AddImage(qrImage);

    //            // ---------- OPTIONAL: force front & back to the SAME size + center ----------
    //            // Software workaround for the front/back mismatch. Both faces share center x≈300.
    //            // Nudge these 4 numbers by eye until printed front & back edges line up, then cut.
    //            // float cx = 300f;                    // shared horizontal center
    //            // float halfW = 185f;                 // half card width  -> width = 370
    //            // stamper.Reader.GetPageN(1).Put(PdfName.CROPBOX,
    //            //     new PdfRectangle(cx - halfW, 175, cx + halfW, 730));   // FRONT crop
    //            // stamper.Reader.GetPageN(2).Put(PdfName.CROPBOX,
    //            //     new PdfRectangle(cx - halfW, 150, cx + halfW, 705));   // BACK crop
    //            // // Keep both crop boxes the SAME width & height so the two faces match.

    //            stamper.Close();
    //            reader.Close();
    //        }
    //    }


    public static DataTable GetData(string stateCode,
                                string districtCode,
                                string blockCode,
                                string panchayatCode,
                                string villageCode)
    {
        string str = " WHERE ApprovalStatus in (1) ";

        if (!string.IsNullOrEmpty(stateCode) && stateCode != "0")
            str += " AND mst5Village.StateCode='" + stateCode + "'";

            str += " AND mst5Village.DistrictCode='" + districtCode + "'";

        if (!string.IsNullOrEmpty(blockCode) && blockCode != "0")
            str += " AND mst5Village.BlockCode='" + blockCode + "'";

        if (!string.IsNullOrEmpty(panchayatCode) && panchayatCode != "0")
            str += " AND mst5Village.PanchayatCode='" + panchayatCode + "'";

        if (!string.IsNullOrEmpty(villageCode) && villageCode != "0")
            str += " AND mst5Village.VillageCode='" + villageCode + "'";

        clsMain objMain = new clsMain();
        DataTable dt = objMain.LoadData(" SELECT[UniqueCode],ImagePath,RequestedDate,ApprovalStatus as Status, [TBCode],[TBName],[Villagename], mstCluster.ClusterName, DistrictName,[Gender],[Active],[FatherName],[MotherName], RIGHT('0' + CAST(DAY([DOB]) AS VARCHAR(2)), 2) + '-' +    LEFT(DATENAME(MONTH, [DOB]), 3) + '-' + CAST(YEAR([DOB]) AS VARCHAR(4)) AS DOB,[AgeAson],[AsOnDate],[Contact], RIGHT('0' + CAST(DAY([DOB]) AS VARCHAR(2)), 2) +'-' + LEFT(DATENAME(MONTH, DateofJoining), 3) + '-' + CAST(YEAR(DateofJoining) AS VARCHAR(4)) AS DateofJoining,[PriorWorkYearMonth],[TBCode] as UniqueId ,isnull(IsSmartPhone, 0) IsSmartPhone,AlternetPhoneNo FROM[dbo].[mstTeamBalika]            inner join mst5Village on mst5Village.VillageCode=mstTeamBalika.VillageCode 	or  mst5Village.refVillage16=mstTeamBalika.VillageCode	or  mst5Village.refVillage17=mstTeamBalika.VillageCode	or  mst5Village.refVillage18=mstTeamBalika.VillageCode	or  mst5Village.refVillage19=mstTeamBalika.VillageCode	or  mst5Village.refVillage20=mstTeamBalika.VillageCode	 	or  mst5Village.refVillage21=mstTeamBalika.VillageCode or  mst5Village.refVillage22=mstTeamBalika.VillageCode	or  mst5Village.refVillage23=mstTeamBalika.VillageCode or  mst5Village.refVillage24=mstTeamBalika.VillageCode  or  mst5Village.refVillage25=mstTeamBalika.VillageCode inner join mst2District on mst2District.DistrictCode = mst5Village.DistrictCode left join mstCluster on mstCluster.ClusterCode = mst5Village.ClusterCode " + str + ""); // Your DB Method
        return dt;
    }
    public static Dictionary<string, string> GetDataCounts(
    string stateCode,
    string districtCode,
    string blockCode,
    string panchayatCode,
    string villageCode)
    {
        string str = " WHERE ApprovalStatus in (1,2,3) ";

        if (!string.IsNullOrEmpty(stateCode) && stateCode != "0")
            str += " AND mst5Village.StateCode='" + stateCode + "'";

        str += " AND mst5Village.DistrictCode='" + districtCode + "'";

        if (!string.IsNullOrEmpty(blockCode) && blockCode != "0")
            str += " AND mst5Village.BlockCode='" + blockCode + "'";

        if (!string.IsNullOrEmpty(panchayatCode) && panchayatCode != "0")
            str += " AND mst5Village.PanchayatCode='" + panchayatCode + "'";

        if (!string.IsNullOrEmpty(villageCode) && villageCode != "0")
            str += " AND mst5Village.VillageCode='" + villageCode + "'";

        clsMain objMain = new clsMain();
        DataTable dt = objMain.LoadData(" SELECT isnull(ApprovalStatus,0) as ApprovalStatus, COUNT(*) as Cnt FROM[dbo].[mstTeamBalika]            inner join mst5Village on mst5Village.VillageCode=mstTeamBalika.VillageCode or  mst5Village.refVillage16=mstTeamBalika.VillageCode	or  mst5Village.refVillage17=mstTeamBalika.VillageCode	or  mst5Village.refVillage18=mstTeamBalika.VillageCode	or  mst5Village.refVillage19=mstTeamBalika.VillageCode	or  mst5Village.refVillage20=mstTeamBalika.VillageCode	 	or  mst5Village.refVillage21=mstTeamBalika.VillageCode or  mst5Village.refVillage22=mstTeamBalika.VillageCode	or  mst5Village.refVillage23=mstTeamBalika.VillageCode or  mst5Village.refVillage24=mstTeamBalika.VillageCode  or  mst5Village.refVillage25=mstTeamBalika.VillageCode inner join mst2District on mst2District.DistrictCode = mst5Village.DistrictCode left join mstCluster on mstCluster.ClusterCode = mst5Village.ClusterCode " + str + "  group by ApprovalStatus"); // Your DB Method

        // start all at 0 so missing statuses still return "0"
        var counts = new Dictionary<string, string>
    {
        { "2", "0" },   // HPA
        { "1", "0" },   // HA
        { "3", "0" }    // HR
    };

        foreach (DataRow dr in dt.Rows)
        {
            counts[dr["ApprovalStatus"].ToString()] = dr["Cnt"].ToString();
        }

        return counts;
    }
    public static string GetDataCount(string  AST, string stateCode,
                               string districtCode,
                               string blockCode,
                               string panchayatCode,
                               string villageCode)
    {
        string str = " WHERE ApprovalStatus in ("+ AST + ") ";

        if (!string.IsNullOrEmpty(stateCode) && stateCode != "0")
            str += " AND mst5Village.StateCode='" + stateCode + "'";

        if (!string.IsNullOrEmpty(districtCode) && districtCode != "0")
            str += " AND mst5Village.DistrictCode='" + districtCode + "'";

        if (!string.IsNullOrEmpty(blockCode) && blockCode != "0")
            str += " AND mst5Village.BlockCode='" + blockCode + "'";

        if (!string.IsNullOrEmpty(panchayatCode) && panchayatCode != "0")
            str += " AND mst5Village.PanchayatCode='" + panchayatCode + "'";

        if (!string.IsNullOrEmpty(villageCode) && villageCode != "0")
            str += " AND mst5Village.VillageCode='" + villageCode + "'";

        clsMain objMain = new clsMain();
        DataTable dt = objMain.LoadData(" SELECT[UniqueCode],ImagePath,RequestedDate,ApprovalStatus as Status, [TBCode],[TBName],[Villagename], mstCluster.ClusterName, DistrictName,[Gender],[Active],[FatherName],[MotherName], RIGHT('0' + CAST(DAY([DOB]) AS VARCHAR(2)), 2) + '-' +    LEFT(DATENAME(MONTH, [DOB]), 3) + '-' + CAST(YEAR([DOB]) AS VARCHAR(4)) AS DOB,[AgeAson],[AsOnDate],[Contact], RIGHT('0' + CAST(DAY([DOB]) AS VARCHAR(2)), 2) +'-' + LEFT(DATENAME(MONTH, DateofJoining), 3) + '-' + CAST(YEAR(DateofJoining) AS VARCHAR(4)) AS DateofJoining,[PriorWorkYearMonth],[TBCode] as UniqueId ,isnull(IsSmartPhone, 0) IsSmartPhone,AlternetPhoneNo FROM[dbo].[mstTeamBalika]        inner join mst5Village on mst5Village.VillageCode=mstTeamBalika.VillageCode 	or  mst5Village.refVillage16=mstTeamBalika.VillageCode	or  mst5Village.refVillage17=mstTeamBalika.VillageCode	or  mst5Village.refVillage18=mstTeamBalika.VillageCode	or  mst5Village.refVillage19=mstTeamBalika.VillageCode	or  mst5Village.refVillage20=mstTeamBalika.VillageCode	 	or  mst5Village.refVillage21=mstTeamBalika.VillageCode or  mst5Village.refVillage22=mstTeamBalika.VillageCode	or  mst5Village.refVillage23=mstTeamBalika.VillageCode or  mst5Village.refVillage24=mstTeamBalika.VillageCode  or  mst5Village.refVillage25=mstTeamBalika.VillageCode inner join mst2District on mst2District.DistrictCode = mst5Village.DistrictCode left join mstCluster on mstCluster.ClusterCode = mst5Village.ClusterCode " + str + ""); // Your DB Method
        return dt.Rows.Count.ToString();
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

        List <CardModel> lst = new List<CardModel>();
        DateTime requestedDate;

      ///  var counts = GetDataCounts(stateCode, districtCode, blockCode, panchayatCode, villageCode);
        if (dt.Rows.Count > 0)
        {
            foreach (DataRow dr in dt.Rows)
            {
                if (DateTime.TryParse(dr["RequestedDate"].ToString(), out requestedDate)) { }
                else
                {
                    lst.Add(new CardModel
                    {
                        RequestedDate = "--",
                        ValidFrom = "--",
                        ValidTo = "--"
                    });
                }
                lst.Add(new CardModel
                {
                    Name = dr["TBName"].ToString(),
                    TBCode = dr["TBCode"].ToString(),
                    District = dr["DistrictName"].ToString(),
                    Village = dr["Villagename"].ToString(),
                    Cluster = dr["ClusterName"].ToString(),
                    Status = dr["Status"].ToString(),
                    UniqueCode = dr["UniqueCode"].ToString(),
                    RequestedDate = requestedDate.ToString("dd-MM-yyyy"),
                    ValidFrom = requestedDate.ToString("dd-MM-yyyy"),
                    ValidTo = requestedDate.AddYears(1).ToString("dd-MM-yyyy"),

                    //HA = counts["2"],
                    //HPA = counts["1"],
                    //HR = counts["3"]


                });

            }
        }
  

        return lst;
    }

    public class CardModel
    {
        public string Name { get; set; }
        public string TBCode { get; set; }
        public string District { get; set; }
        public string Village { get; set; }
        public string Cluster { get; set; }
        public string RequestedDate { get; set; }
        public string ValidFrom { get; set; }
        public string ValidTo { get; set; }
        public string Status { get; set; }
        public string UniqueCode { get; set; }
        public string HPA { get; set; }
        public string HR { get; set; }
        public string HA { get; set; }
    }

    [System.Web.Services.WebMethod]
    public static string SubmitForApproval(string tbCode)
    {
        using (SqlConnection con = new SqlConnection(SqlHelper.mainConnectionString))
        {
            con.Open();

            // 1) Validate photo exists for this record
            string checkSql = @"SELECT ImagePath FROM mstTeamBalika WHERE TBCode = @TBCode";
            using (SqlCommand checkCmd = new SqlCommand(checkSql, con))
            {
                checkCmd.Parameters.AddWithValue("@TBCode", tbCode);
                object result = checkCmd.ExecuteScalar();

                if (result == null)
                    return "Please Upload Image";   // no such TBCode

                string imagePath = result == DBNull.Value ? "" : result.ToString().Trim();
                if (string.IsNullOrWhiteSpace(imagePath))
                    return "Please Upload Image";    // photo is blank -> alert on client
            }

            // 2) Photo present -> submit for approval
            string updateSql = @"UPDATE mstTeamBalika
                             SET ApprovalStatus = 1, RequestedDate = GETDATE()
                             WHERE TBCode = @TBCode";
            using (SqlCommand cmd = new SqlCommand(updateSql, con))
            {
                cmd.Parameters.AddWithValue("@TBCode", tbCode);
                cmd.ExecuteNonQuery();
            }
            string userId = HttpContext.Current.Session["UserID"].ToString();
            //string TDist = HttpContext.Current.Session["TDist"].ToString();
           
            //string Str="select DistirctOffice from mst2district where DistrictCode = '" + TDist + "' and DistirctOffice is not null";

            //using (var cn1 = new SqlConnection(SqlHelper.mainConnectionString))
            //using (var cmd1 = new SqlCommand(Str, cn1))
            //{ cn1.Open(); var o = cmd1.ExecuteScalar(); return o == null || o == DBNull.Value ? "" : o.ToString(); }


            using (var cn = new SqlConnection(SqlHelper.mainConnectionString))
            using (var cmd = new SqlCommand(
                "INSERT INTO TBApprovalLog\r\n                  (\r\n                      TBCode,\r\n                      ApprovalStatus,\r\n                      RejectRemark,\r\n                      ApprovedRejectBy,\r\n                      CreatedOn\r\n                  )\r\n                  VALUES\r\n                  (\r\n                      @TBCode,\r\n                      @Status,\r\n                      @Remark,\r\n                      @ApprovedRejectBy,\r\n                      GETDATE()\r\n                  )", cn))
            {
                cmd.Parameters.AddWithValue("@TBCode", tbCode);
                cmd.Parameters.AddWithValue("@Status", 1);
                cmd.Parameters.AddWithValue("@Remark", DBNull.Value);
                cmd.Parameters.AddWithValue("@ApprovedRejectBy", userId);
                cn.Open(); cmd.ExecuteNonQuery();
            }
        }
        return "Success";
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
            string userId = HttpContext.Current.Session["UserID"].ToString();
            try
            {
                foreach (string tbCode in tbCodes)
                {
                    // Update Main Table
                    SqlCommand cmd1 = new SqlCommand(
                    @"UPDATE mstTeamBalika
                  SET ApprovalStatus = @Status
                  WHERE TBCode = @TBCode",
                    con, tran);

                    cmd1.Parameters.AddWithValue("@TBCode", tbCode);
                    cmd1.Parameters.AddWithValue("@Status", status);
                    cmd1.ExecuteNonQuery();

                    // Insert Log
                    SqlCommand cmd2 = new SqlCommand(
                    @"INSERT INTO TBApprovalLog
                  (
                      TBCode,
                      ApprovalStatus,
                      RejectRemark,
                      ApprovedRejectBy,
                      CreatedOn
                  )
                  VALUES
                  (
                      @TBCode,
                      @Status,
                      @Remark,
                      @ApprovedRejectBy,
                      GETDATE()
                  )",
                    con, tran);

                    cmd2.Parameters.AddWithValue("@TBCode", tbCode);
                    cmd2.Parameters.AddWithValue("@Status", status);
                    cmd2.Parameters.AddWithValue("@Remark", string.IsNullOrEmpty(remark) ? (object)DBNull.Value : remark);
                    cmd2.Parameters.AddWithValue("@ApprovedRejectBy", Convert.ToInt32(userId));
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

    #endregion
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        if (ddlDistrict.SelectedIndex <= 0)
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Select District')</script>", false);
            return;
        }

        if (ddlBlock.SelectedIndex <= 0)
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Select Block')</script>", false);
            return;
        }
        if (ddlPanchayat.SelectedIndex <= 0)
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Select Panchayat')</script>", false);
            return;
        }
        if (ddlVillage.SelectedIndex <= 0)
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Select Village')</script>", false);
            return;
        }
        pnlMain.Enabled = true;
        RefreshControl();
        //  Session["VillageGeographyOperational"] = "";
        Resone.Visible = false;
        rdate.Visible = false;

        ViewState["Save"] = "Save";
        //Unique();
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {

        if (ViewState["TMCode"].ToString() != null)
        {
            objMain.DeleteTM(ViewState["TMCode"].ToString());
            GVMainBind();
        }
    }

    protected void GVMain_OnRowCommand(object sender, GridViewCommandEventArgs e)
    {

        if (e.CommandName == "GVUIO")
        {
            int iIndex = Convert.ToInt32(e.CommandArgument);
            string TBCode = GVMain.DataKeys[iIndex]["UniqueCode"].ToString();
            FillControls(TBCode);
            ViewState["Save"] = "Edit";

            ViewState["TBCode"] = TBCode;

            pnlMain.Enabled = true;

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
    private void FillControls(string pSchoolCOde)
    {
        DataTable dtmstM = null;

        dtmstM = objMain.LoadData(" SELECT  [UniqueCode],rjob,rBusiness,isnull(PhysicalStatus,0)PhysicalStatus,isnull(Specialization,0)Specialization,isnull(Specially,0)Specially ,isnull(rJobOpportunity,0)rJobOpportunity,rjobother,isnull(IsTeamBalikaAlumni,0) IsTeamBalikaAlumni,AlumniDate,EmpID,Status, WorkingStatus,TbRecruited,DropOutReason,DropoutDate ,ImagePath,DateofJoining,Expectation,Abvision ,mst5Village.[StateCode],mst5Village.[DistrictCode] ,mst5Village.[BlockCode] ,mst5Village.[PanchayatCode]  ,[TBCode] ,[TBName] ,[mstTeamBalika].[VillageCode] ,[Gender] ,[Active],[FatherName] ,[MotherName] ,[SocialCategory]    ,[EducationLevel] ,[FamilyOccupation]  ,[DOBAvailable]  ,[DOB]   ,[AgeAson]  ,[AsOnDate]   ,[Contact]  ,[ReasonForTBChoice]    ,[RecruitmentReferalInfo]  ,[PriorWorkExperience]    ,[TotalPriorWorkExperience]   ,[PriorWorkYearMonth],[TBCode] as UniqueId ,isnull(IsSmartPhone,0) IsSmartPhone,AlternetPhoneNo FROM [dbo].[mstTeamBalika] inner join mst5Village on mst5Village.VillageCode=mstTeamBalika.VillageCode where UniqueCode ='" + pSchoolCOde + "'");

        if (dtmstM.Rows.Count > 0)
        {

            #region School

            string strQry = "  SELECT VillageGeographyOperational FROM mst5Village where villagecode ='" + ddlVillage.SelectedValue + "'     ";
            DataTable dtDistrict = objMain.LoadData(strQry);
            if (dtDistrict.Rows.Count > 0)
            {
                Session["VillageGeographyOperational"] = Convert.ToString(dtDistrict.Rows[0]["VillageGeographyOperational"]);
            }

            //if (Session["user_level"].ToString() == "1")
            //{
            if (dtmstM.Rows[0]["Status"].ToString() == "1")
            {
                btnsave.Enabled = true;
                btnDelete.Enabled = true;
            }
            else
            {
                btnsave.Enabled = false;
                btnDelete.Enabled = false;
            }
            DataTable dt = objMain.LoadData("SELECT  * from tblAttendance where TBID='" + pSchoolCOde + "'");
            if (dt.Rows.Count > 0)
            {
                txtday.Text = dt.Rows.Count.ToString();
            }
            else
            {
                txtday.Text = "";
            }
            //ddlState.SelectedValue = dtmstM.Rows[0]["StateCode"].ToString();
            //FillCBDist();
            //ddlDistrict.SelectedValue = dtmstM.Rows[0]["DistrictCode"].ToString().Trim();
            //FillCBBock();
            //ddlBlock.SelectedValue = dtmstM.Rows[0]["BlockCode"].ToString();
            //FillCBCluster();
            //ddlPanchayat.SelectedValue = dtmstM.Rows[0]["PanchayatCode"].ToString().Trim();
            //FillCVillage();
            //ddlVillage.SelectedValue = dtmstM.Rows[0]["VillageCode"].ToString().Trim();

            ViewState["TMCode"] = pSchoolCOde;
            txtIDNO.Text = dtmstM.Rows[0]["UniqueId"].ToString();
            txtName.Text = dtmstM.Rows[0]["TBName"].ToString().Trim();
            ddlGender.SelectedValue = dtmstM.Rows[0]["Gender"].ToString();
            ddltbRecruited.SelectedValue = dtmstM.Rows[0]["TbRecruited"].ToString();
            ddlSmart.SelectedValue = dtmstM.Rows[0]["IsSmartPhone"].ToString();

            txtxAlternate.Text = dtmstM.Rows[0]["AlternetPhoneNo"].ToString().Trim();
            ddloccu.SelectedValue = dtmstM.Rows[0]["FamilyOccupation"].ToString();
            ddlWorkingStatus.SelectedValue = dtmstM.Rows[0]["WorkingStatus"].ToString();
            ddlPhysicalStatus.SelectedValue = dtmstM.Rows[0]["PhysicalStatus"].ToString();
            ddlSp_SelectedIndexChanged(ddlPhysicalStatus, null);
            ddlSpecially.SelectedValue = dtmstM.Rows[0]["Specially"].ToString();
              
            EmpID.Visible = false;
            divbus.Visible = false;
            txtBus.Text = "";
            divJobOp.Visible = false;
            ddlJobOpportunity.SelectedIndex = 0;
            divOtherJob.Visible = false;
            txtotherjob.Text = "";
            if (ddlWorkingStatus.SelectedIndex > 0)
            {
                if (Convert.ToInt32(ddlWorkingStatus.SelectedValue) == 1 && Convert.ToString(Session["VillageGeographyOperational"]) == "2")
                {
                    ddlStatusReasone.SelectedValue = dtmstM.Rows[0]["DropOutReason"].ToString();
                    ddlStatusReasone_SelectedIndexChanged(ddlBlock, null);
                    DateTime DateDrop = Convert.ToDateTime(dtmstM.Rows[0]["DropoutDate"].ToString());
                    txtDropDate.Text = DateDrop.ToString("dd/MM/yyy");

                    Resone.Visible = false;
                    rdate.Visible = false;
                    ddlAlumni.SelectedValue = dtmstM.Rows[0]["IsTeamBalikaAlumni"].ToString();
                    ddlAlumni_SelectedIndexChanged(ddlAlumni, null);
                    if (ddlAlumni.SelectedIndex > 0)
                    {
                        if (Convert.ToInt32(ddlAlumni.SelectedValue) == 1)
                        {
                            DateTime AlumniDate = Convert.ToDateTime(dtmstM.Rows[0]["AlumniDate"].ToString());
                            txtAlumniDate.Text = AlumniDate.ToString("dd/MM/yyy");
                        }
                        else
                        {
                            txtAlumniDate.Text = "";
                        }
                    }

                    divAlumni.Visible = true;
                }
                else if (Convert.ToInt32(ddlWorkingStatus.SelectedValue) == 2)
                {
                    if (dtmstM.Rows[0]["DropOutReason"].ToString() == "4")
                    {
                        ddlStatusReasone.SelectedIndex = 0;
                    }
                    else
                    {
                        ddlStatusReasone.SelectedValue = dtmstM.Rows[0]["DropOutReason"].ToString();
                    }
                    ddlStatusReasone_SelectedIndexChanged(ddlBlock, null);
                    DateTime DateDrop = Convert.ToDateTime(dtmstM.Rows[0]["DropoutDate"].ToString());
                    txtDropDate.Text = DateDrop.ToString("dd/MM/yyy");

                    Resone.Visible = true;
                    rdate.Visible = true;
                    ddlAlumni.SelectedValue = dtmstM.Rows[0]["IsTeamBalikaAlumni"].ToString();
                    ddlAlumni_SelectedIndexChanged(ddlAlumni, null);
                    if (ddlAlumni.SelectedIndex > 0)
                    {
                        if (Convert.ToInt32(ddlAlumni.SelectedValue) == 1)
                        {
                            DateTime AlumniDate = Convert.ToDateTime(dtmstM.Rows[0]["AlumniDate"].ToString());
                            txtAlumniDate.Text = AlumniDate.ToString("dd/MM/yyy");
                        }
                        else
                        {
                            txtAlumniDate.Text = "";
                        }
                    }

                    txtJob.Text = dtmstM.Rows[0]["rjob"].ToString().Trim();
                    txtBus.Text = dtmstM.Rows[0]["rBusiness"].ToString().Trim();
                    ddlJobOpportunity.SelectedValue = dtmstM.Rows[0]["rJobOpportunity"].ToString();
                    if (ddlJobOpportunity.SelectedIndex > 0)
                    {
                        ddlOther_SelectedIndexChanged(ddlAlumni, null);

                    }
                    txtotherjob.Text = dtmstM.Rows[0]["rjobother"].ToString().Trim();

                    divAlumni.Visible = true;
                }
                else
                {
                    Resone.Visible = false;
                    rdate.Visible = false;
                    txtDropDate.Text = "";
                    txtEmployeeID.Text = "";
                    EmpID.Visible = false;
                    ddlStatusReasone.SelectedIndex = 0;
                    ddlAlumni.SelectedIndex = 0;
                    txtAlumniDate.Text = "";
                    ddlAlumni.SelectedIndex = 0;
                    divAlumni.Visible = false;
                    divAlumni1.Visible = false;
                }
            }
            else
            {
                Resone.Visible = false;
                rdate.Visible = false;
                txtAlumniDate.Text = "";
                divAlumni1.Visible = false;
            }




            txtEmployeeID.Text = dtmstM.Rows[0]["EmpID"].ToString().Trim();
            ddlEducation.SelectedValue = dtmstM.Rows[0]["EducationLevel"].ToString();
            ddlCategory.SelectedValue = dtmstM.Rows[0]["SocialCategory"].ToString();
            ddlReason.SelectedValue = dtmstM.Rows[0]["ReasonForTBChoice"].ToString();

            ddlSpecialization_SelectedIndexChanged(ddlEducation, null);
            ddlSpecialization.SelectedValue = dtmstM.Rows[0]["Specialization"].ToString();

            ddlSours.SelectedValue = dtmstM.Rows[0]["RecruitmentReferalInfo"].ToString();
            if (Convert.ToBoolean(dtmstM.Rows[0]["PriorWorkExperience"].ToString()) == true)
            {
                ddlWorkEx.SelectedIndex = 1;
            }
            else
            {
                ddlWorkEx.SelectedIndex = 2;
            }
            txtFatherName.Text = dtmstM.Rows[0]["FatherName"].ToString().Trim();
            txtMotherName.Text = dtmstM.Rows[0]["MotherName"].ToString().Trim();
            txtContact.Text = dtmstM.Rows[0]["Contact"].ToString().Trim();
            txtDuartion.Text = "";
            txtMonth.Text = "";
            if (dtmstM.Rows[0]["TotalPriorWorkExperience"].ToString() == "0")
            {
            }
            else
            {
                txtDuartion.Text = dtmstM.Rows[0]["TotalPriorWorkExperience"].ToString().Trim();
            }
            if (dtmstM.Rows[0]["PriorWorkYearMonth"].ToString() == "0")
            {
            }
            else
            {
                txtMonth.Text = dtmstM.Rows[0]["PriorWorkYearMonth"].ToString().Trim();
            }

            if (dtmstM.Rows[0]["DateofJoining"].ToString() != "")
            {
                DateTime DateJoing = Convert.ToDateTime(dtmstM.Rows[0]["DateofJoining"].ToString());
                txtJoingDate.Text = DateJoing.ToString("dd/MM/yyy");
            }
            else
            {
                txtJoingDate.Text = "";
            }

            ddlDob.SelectedValue = dtmstM.Rows[0]["DOBAvailable"].ToString();
            txtExp.Text = dtmstM.Rows[0]["Expectation"].ToString().Trim();
            txtAbv.Text = dtmstM.Rows[0]["Abvision"].ToString().Trim();
            if (dtmstM.Rows[0]["ImagePath"].ToString() != "")
            {
                //string sFileDir = Server.MapPath("~/images/" + dtmstM.Rows[0]["ImagePath"].ToString().Trim() + "");
                //string sFileDir = Request.PhysicalApplicationPath + "images\\";
                string imagename = dtmstM.Rows[0]["ImagePath"].ToString().Trim();
                ViewState["ImagePath"] = imagename;
                imgMKS.ImageUrl = ResolveUrl("~/DataBackup/" + imagename);
            }
            else
            {
                ViewState["ImagePath"] = "";

                imgMKS.ImageUrl = null;
            }
            if (Convert.ToInt32(ddlDob.SelectedValue) == 1)
            {
                DateTime dob = Convert.ToDateTime(dtmstM.Rows[0]["DOB"].ToString());
                txtDate.Text = dob.ToString("dd/MM/yyy");
                lblDob.Text = "DOB";
                lblAge.Enabled = false;
                txtAge.Enabled = false;
                txtAge.Text = "";
                txtDate.Enabled = true;
            }
            else
            {
                lblDob.Text = "As On";

                txtAge.Text = dtmstM.Rows[0]["AgeAson"].ToString();
                DateTime dob = Convert.ToDateTime(dtmstM.Rows[0]["AsOnDate"].ToString());
                txtDate.Text = dob.ToString("dd/MM/yyy");
                lblAge.Enabled = true;
                txtAge.Enabled = true;
                txtDate.Enabled = false;
            }
            #endregion
        }


        DataTable   dtmstMNew = objMain.LoadData(" SELECT [UniqueCode], RequestedDate,[TBCode],EgVillagecode,[TBName],[Villagename],ApprovalStatus, mstCluster.ClusterName, DistrictName,[Gender],[Active],[FatherName],[MotherName], RIGHT('0' + CAST(DAY([DOB]) AS VARCHAR(2)), 2) + '-' +    LEFT(DATENAME(MONTH, [DOB]), 3) + '-' + CAST(YEAR([DOB]) AS VARCHAR(4)) AS DOB,[AgeAson],[AsOnDate],[Contact], RIGHT('0' + CAST(DAY([DOB]) AS VARCHAR(2)), 2) +'-' + LEFT(DATENAME(MONTH, DateofJoining), 3) + '-' + CAST(YEAR(DateofJoining) AS VARCHAR(4)) AS DateofJoining,[PriorWorkYearMonth],[TBCode] as UniqueId ,isnull(IsSmartPhone, 0) IsSmartPhone,AlternetPhoneNo FROM[dbo].[mstTeamBalika] inner join mst5Village on mst5Village.VillageCode = mstTeamBalika.VillageCode inner join mst2District on mst2District.DistrictCode = mst5Village.DistrictCode left join mstCluster on mstCluster.ClusterCode = mst5Village.ClusterCode where UniqueCode ='" + pSchoolCOde + "'");

        if (dtmstMNew.Rows.Count > 0)
        {
            txtViewTBName.Text = dtmstMNew.Rows[0]["TBName"].ToString().Trim();
            txtViewTBcode.Text = dtmstMNew.Rows[0]["TBCode"].ToString().Trim();
            txtViewTBFather.Text = dtmstMNew.Rows[0]["FatherName"].ToString().Trim();
            txtViewDOB.Text = dtmstMNew.Rows[0]["DOB"].ToString().Trim();
            txtViewMobile.Text = dtmstMNew.Rows[0]["Contact"].ToString().Trim();
            txtViewJoin.Text = dtmstMNew.Rows[0]["DateofJoining"].ToString().Trim();
            txtViewVillage.Text = dtmstMNew.Rows[0]["Villagename"].ToString().Trim();
      
            txtViewDistrict.Text = dtmstMNew.Rows[0]["DistrictName"].ToString().Trim();
            if (dtmstMNew.Rows[0]["ApprovalStatus"].ToString() == "0")
            {
                TBVFTSP.InnerText = "";
            }
            else
            {
                DateTime requestedDate = Convert.ToDateTime(dtmstMNew.Rows[0]["RequestedDate"]);
                DateTime oneYearLater = requestedDate.AddYears(1);
                TBVFTSP.InnerText = requestedDate.ToString("dd-MM-yyyy") + " - " + oneYearLater.ToString("dd-MM-yyyy");
            }
            //-----Gaurav 10/06/2026

            TBNameSP.InnerText = dtmstMNew.Rows[0]["TBName"].ToString().Trim();
            TBTBCSP.InnerText = dtmstMNew.Rows[0]["TBCode"].ToString().Trim();
            TBFatherNameSP.InnerText = dtmstMNew.Rows[0]["FatherName"].ToString().Trim();
            TBDOBSP.InnerText = dtmstMNew.Rows[0]["DOB"].ToString().Trim();
            TBMobileSP.InnerText = dtmstMNew.Rows[0]["Contact"].ToString().Trim();
            TBDOJSP.InnerText = dtmstMNew.Rows[0]["DateofJoining"].ToString().Trim();
            TBVillageSP.InnerText = dtmstMNew.Rows[0]["Villagename"].ToString().Trim();
         
            TBAddressSP.InnerText = Scalar("select DistirctOffice from mst2district where DistrictCode = '" + ddlDistrict.SelectedValue + "' and DistirctOffice is not null");

            txtViewcluster.Text = Scalar("select ClusterName from mstcluster where DistrictCode = '" + ddlDistrict.SelectedValue + "' and clustercode in( select clustercode from mst5village where fyear='2026-2027' and egvillagecode='" + dtmstMNew.Rows[0]["EgVillagecode"].ToString().Trim() + "' )  ");  ;
            TBClusterSP.InnerText = txtViewcluster.Text;

            SetApprovalButtons(Convert.ToInt32(dtmstMNew.Rows[0]["ApprovalStatus"]),dtmstMNew.Rows[0]["TBCode"].ToString().Trim());

            string qrText = "www.educategirls.ngo";
            using (var qrGen = new QRCoder.QRCodeGenerator())
            using (var data = qrGen.CreateQrCode(qrText, QRCoder.QRCodeGenerator.ECCLevel.Q))
            using (var png = new QRCoder.PngByteQRCode(data))
            {
                byte[] pngBytes = png.GetGraphic(20);   // 20 px per module
                QRIMG.ImageUrl = "data:image/png;base64," + Convert.ToBase64String(pngBytes);
            }
        }

        if (dtmstM.Rows.Count > 0)
        {
            if (dtmstM.Rows[0]["ImagePath"].ToString() != "")
            {
                //string sFileDir = Server.MapPath("~/images/" + dtmstM.Rows[0]["ImagePath"].ToString().Trim() + "");
                //string sFileDir = Request.PhysicalApplicationPath + "images\\";
                string imagename = dtmstM.Rows[0]["ImagePath"].ToString().Trim();
                ViewState["ImagePath"] = imagename;
                TBImagePHIM.ImageUrl = ResolveUrl("~/DataBackup/" + imagename);
            }
            else
            {
                ViewState["ImagePath"] = "";

                TBImagePHIM.ImageUrl = "~/images/blank_img.png;";
            }
        }
        

    }
    private void SetApprovalButtons(int approvalStatus, string TBCode)
    {
        btnSubmitApproval.Attributes["data-tbcode"] = TBCode;
        btnSubmitApproval.Visible = false;
        btnDownloadIdCard.Visible = false;
        lblRejectionRemark.Visible = false;

        switch (approvalStatus)
        {
            case 0:
                btnSubmitApproval.Visible = true;
                btnSubmitApproval.Enabled = true;
                btnSubmitApproval.Text = "Submit for DPO Approval";
                break;

            case 1:
                btnSubmitApproval.Visible = true;
                btnSubmitApproval.Enabled = false;
                btnSubmitApproval.Text = "Pending for Approval";
                break;

            case 2:
                btnDownloadIdCard.Visible = true;
                break;

            case 3:
                DataTable dtmstRemarkApproval = objMain.LoadData("SELECT * FROM [dbo].[TBApprovalLog] where TBCode = '" + TBCode + "' and len(RejectRemark)>0");
                string rejectionRemark = dtmstRemarkApproval.Rows[0]["RejectRemark"].ToString();
                lblRejectionRemark.Visible = true;
                lblRejectionRemark.Text = "Rejection reason: " + rejectionRemark;
                btnSubmitApproval.Visible = true;
                btnSubmitApproval.Enabled = true;
                btnSubmitApproval.Text = "Resubmit for DPO Approval";
                break;
        }
    }
    protected void txtSearchName_Click(object sender, EventArgs e)
    {
        DataTable dt = ViewState["Serach"] as DataTable;
        string strFilter = "";

        string str = "TBName";
        DataTable dtfilter = dt.Copy();


        strFilter = str + " like '%" + txtSearchName.Text.Trim() + "%'   ";

        //dtSoSaleOrder.Select(txtSearch.SelectedValue.ToString() + " like '" + txtSearch.Text + "%'";


        dtfilter.DefaultView.RowFilter = strFilter;
        dtfilter.DefaultView.Sort = "TBName asc";
        GVMain.DataSource = dtfilter.DefaultView.ToTable();
        GVMain.DataBind();

    }
    protected void txtJoingDate_OnTextChanged(object sender, EventArgs e)
    {
        DataTable dt = objMain.LoadData("Select StartYear from mst2District where DistrictCode ='" + ddlDistrict.SelectedValue + "'");
        if (dt.Rows.Count > 0)
        {
            HdnStartYear.Text = dt.Rows[0]["StartYear"].ToString();
        }
       // ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('" + txtJoingDate.Text + "')</script>", false);

        string[] Jd = (txtJoingDate.Text).Split('/');

        int JoiningYear = Convert.ToInt32(Jd[2].Trim());
        if (txtJoingDate.Text != "")
        {
            if (JoiningYear < Convert.ToInt32(HdnStartYear.Text))
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Joining year can not be Less than district start year')</script>", false);
                txtJoingDate.Text = "";
            }

        }
    }
    protected void btnSerach_Click(object sender, EventArgs e)
    {
        if (Convert.ToString(Session["user_level"]) == "60")
        {
            ScriptManager.RegisterStartupScript(
           this,
           GetType(),
           "ShowTab",
           "$('#myTab a[href=\"#tab3\"]').tab('show');",
           true);
        }
        ScriptManager.RegisterStartupScript(
                this,
                this.GetType(),
                "LoadCards",
                "LoadCards();",
                true);
      

        if (Convert.ToString(Session["user_level"]) == "60")
        {
            pnlMain.Visible = false;
        }
        else
        {
            GVMainBind();
            pnlMain.Visible = true;
        }

    }
    protected void btnViewDetails_Click(object sender, EventArgs e)
    {
        string tbCode = hdnTBCode.Value;

        FillControls(tbCode);

        // Optional: switch to Profile tab
        ScriptManager.RegisterStartupScript(
            this,
            GetType(),
            "ShowTab",
            "$('#myTab a[href=\"#tab2\"]').tab('show');",
            true);
        ScriptManager.RegisterStartupScript(
               this,
               this.GetType(),
               "LoadCards",
               "LoadCards();",
               true);
    }

    protected void ddlSpecialization_SelectedIndexChanged(object sender, EventArgs e)
    {
        conditions = "";
        conditions = "MainID ='"+ddlEducation.SelectedValue +"'  ";
        objComman.BindDLL("mstEducationStatusdetails", "EID,StatusName", conditions, "EID", "asc", ddlSpecialization, "StatusName", "EID", "Select");
        divSpc.Visible = false;
        if (Convert.ToInt32(ddlEducation.SelectedValue) == 5 || Convert.ToInt32(ddlEducation.SelectedValue) == 7 || Convert.ToInt32(ddlEducation.SelectedValue) == 9)
        {
            divSpc.Visible=true;
        }
      
    }
    protected void btnAdd_Click1(object sender, EventArgs e)
    {

        // ddllevel_selectindexchange(sender, e);
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
    public void Unique()
    {
        if (ViewState["Save"].ToString() == "Save")
        {
            if (ddlVillage.SelectedIndex > 0)
            {
                Int32 mNewNo = 0;
                string strAlias;
                string strQry = " Select top 1 isnull(max(Serial),0) as Serial from mstTeamBalika inner join mst5Village on  mst5Village.VillageCode=mstTeamBalika.VillageCode 	or  mst5Village.refVillage16=mstTeamBalika.VillageCode	or  mst5Village.refVillage17=mstTeamBalika.VillageCode	or  mst5Village.refVillage18=mstTeamBalika.VillageCode	or  mst5Village.refVillage19=mstTeamBalika.VillageCode	or  mst5Village.refVillage20=mstTeamBalika.VillageCode	 	or  mst5Village.refVillage21=mstTeamBalika.VillageCode  or  mst5Village.refVillage22=mstTeamBalika.VillageCode or  mst5Village.refVillage23=mstTeamBalika.VillageCode	or  mst5Village.refVillage24=mstTeamBalika.VillageCode or  mst5Village.refVillage25=mstTeamBalika.VillageCode	 inner join mst3Block on  mst3Block.BlockCode=mst5Village.BlockCode where mst5Village.DistrictCode='" + ddlDistrict.SelectedValue + "'   ";
                //string strQry = " Select top 1 Serial from tblDTD   order by Serial desc ";
                DataTable dt = objMain.LoadData(strQry);

                string strQry1 = " Select EGVillageCode,VillageCode  from mst5Village where VillageCode='" + ddlVillage.SelectedValue + "' ";
                DataTable dtVillage = objMain.LoadData(strQry1);
                if (dt.Rows.Count > 0)
                {
                    if (dt.Rows[0]["Serial"].ToString() == "" || dt.Rows[0]["Serial"].ToString() == "-1")
                    {
                        mNewNo += 1;
                        strAlias = mNewNo.ToString().PadLeft(5, '0');
                        ViewState["TBCode"] = "TB" + "-" + dtVillage.Rows[0]["EGVillageCode"] + "-" + strAlias;
                        ViewState["NumNo"] = strAlias;
                    }
                    else
                    {
                        mNewNo = Convert.ToInt32(dt.Rows[0]["Serial"].ToString());
                        mNewNo += 1;
                        strAlias = mNewNo.ToString().PadLeft(5, '0');

                        ViewState["NumNo"] = strAlias;
                        ViewState["TBCode"] = "TB" + "-" + dtVillage.Rows[0]["EGVillageCode"] + "-" + strAlias;

                    }

                }
                else
                {
                    mNewNo += 1;
                    strAlias = mNewNo.ToString().PadLeft(5, '0');
                    ViewState["TBCode"] = "TB" + "-" + strAlias;
                    ViewState["NumNo"] = strAlias;
                }
            }
        }

    }
    protected void btnDownloadPDF_Click(object sender, EventArgs e)
    {
        Document pdfDoc = new Document(PageSize.A4); MemoryStream ms = new MemoryStream(); 
        PdfWriter writer = PdfWriter.GetInstance(pdfDoc, ms); pdfDoc.Open(); 
             pdfDoc.Add(new Paragraph("Team Balika Details"));
         pdfDoc.Add(new Paragraph(" "));     
        pdfDoc.Add(new Paragraph("TB Code: " + txtIDNO.Text));   
        pdfDoc.Add(new Paragraph("Name: " + txtName.Text));   
        pdfDoc.Add(new Paragraph("Contact Number: " + txtContact.Text));   
        pdfDoc.Add(new Paragraph("Alternate Number: " + txtxAlternate.Text));   
        pdfDoc.Add(new Paragraph("Father Name: " + txtFatherName.Text));   
        pdfDoc.Add(new Paragraph("Mother Name: " + txtMotherName.Text));   
        pdfDoc.Add(new Paragraph("Gender: " + ddlGender.SelectedItem.Text));  
        pdfDoc.Add(new Paragraph("DOB: " + txtDate.Text));    
        pdfDoc.Add(new Paragraph("Status: " + ddlWorkingStatus.SelectedItem.Text));
        pdfDoc.Close();

        byte[] bytes = ms.ToArray();

        Response.ContentType = "application/pdf";
        Response.AddHeader("content-disposition", "attachment;filename=TeamBalika.pdf");
        Response.Buffer = true;
        Response.Clear();
        Response.BinaryWrite(bytes);
        Response.End();

    }
    protected void ddlSp_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlPhysicalStatus.SelectedIndex > 0)
        {
            if (ddlPhysicalStatus.SelectedValue == "1")
            {
                divSp.Visible = true;
            }
            else
            {
                ddlSpecially.SelectedIndex = 0;
                divSp.Visible = false;
            }
        }
        else
        {
            ddlSpecially.SelectedIndex = 0;
            divSp.Visible = false;
        }
    }
}