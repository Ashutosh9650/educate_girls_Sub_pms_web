using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Web.Security;
using System.Security.Cryptography;
using System.Text;
using System.Runtime.InteropServices;

using Microsoft.Reporting.WebForms;
using System.IO;
using System.Drawing;
using Ionic.Zip;
public partial class frmSipAnnual : System.Web.UI.Page
{
    clsMain objMain = new clsMain();
    Comman objComman = new Comman();

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
                LoadYear();
                LoadUserLeavel();
                ViewState["1"] = "ss";
               
            }
            else
            {
                Response.Redirect("Login.aspx", false);
            }
          
        }

       
    }
    protected void ddlYear_SelectedIndexChanged(object sender, EventArgs e)
    {
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
    public void LoadYear()
    {
        DataTable dtYear = objComman.Generate_Financial_Year();
        objComman.BindDLLMasterTable("mstSchool", "Type,ID", dtYear, conditions, "Type", "asc", ddlYear, "Type", "ID", "Select");

        ddlYear.SelectedIndex = 1;
        //}


    }
    protected void btnImport_Click(object sender, EventArgs e)
    {
        if (ViewState["1"].ToString() == "2")
        {
            DataTable dt = (DataTable)Session["SIP"];
            ExporttoExcel(GV_DynamicGrid, dt, "SchoolRaw");
        }
        if (ViewState["1"].ToString() == "3")
        {
            DataTable dt = (DataTable)Session["SIP"];
            ExporttoExcel(GV_DynamicGrid, dt, "VillageRaw");
        }
          if (ViewState["1"].ToString() == "5")
        {
            DataTable dt = (DataTable)Session["D2DTarget"];
            ExporttoExcel(GV_DynamicGrid, dt, "D2DTarget");
        }
        
        if (ViewState["1"].ToString() == "4")
        {
            DataTable dt = (DataTable)Session["D2DAnual"];
            ExporttoExcel(gvD2d, dt, "D2D");
        }
        
    }
    protected void btnCSV_Click(object sender, EventArgs e)
    {
       
        if (ViewState["1"].ToString() == "2")
        {
            DataTable dt = (DataTable)Session["SIP"];
            //ExporttoExcel(gvD2d, dt, "D2D");
            ExportToCSVFile(dt, "SchoolRaw");
        }
        if (ViewState["1"].ToString() == "3")
        {
            DataTable dt = (DataTable)Session["SIP"];
            //ExporttoExcel(gvD2d, dt, "D2D");
            ExportToCSVFile(dt, "VillageRaw");
        }
         if (ViewState["1"].ToString() == "5")
        {
            DataTable dt = (DataTable)Session["D2DTarget"];
            //ExporttoExcel(gvD2d, dt, "D2D");
            ExportToCSVFile(dt, "D2DTarget");
        }
      
        if (ViewState["1"].ToString() == "4")
        {
            DataTable dt = (DataTable)Session["D2DAnual"];
            //ExporttoExcel(gvD2d, dt, "D2D");
            ExportToCSVFile(dt, "D2D");
        }
    }

    private void ExportToCSVFile(DataTable dtTable, string filePath)
    {
        if (dtTable != null)
        {
            StringBuilder sbldr = new StringBuilder();
            if (dtTable.Columns.Count != 0)
            {
                foreach (DataColumn col in dtTable.Columns)
                {
                    sbldr.Append(col.ColumnName + ',');
                }
                sbldr.Append("\r\n");
                foreach (DataRow row in dtTable.Rows)
                {
                    foreach (DataColumn column in dtTable.Columns)
                    {

                        sbldr.Append(Convert.ToString(row[column]).Replace(",", "  ").Replace("\r", "").Replace("\n", "") + ',');
                    }
                    sbldr.Append("\r\n");

                }
            }
            string sFileDir = Server.MapPath("~/DataBackup/");
            string Fullfilename = "" + filePath + "_" + DateTime.Now.ToString("dd_MM_yyyy_hh_mm_ss") + ".csv";
            string path = sFileDir + Fullfilename;
            File.WriteAllText(path, sbldr.ToString());

            FileStream fs = null;//, fs2=null;
            try
            {
                string path1 = Fullfilename;
                string foldername = Server.MapPath("~/DataBackup/" + path1 + "");
                string datafolder = path1.Substring(0, path1.Length - 4);
                //  string[] file = Directory.GetFiles(foldername);

                string fullPath = Request.MapPath("~/DataBackup/" + datafolder + "" + ".zip");
                using (ZipFile zip = new ZipFile())
                {
                    zip.AddFile(foldername, "");
                    //    zip.AddFiles(file, foldername);
                    zip.Save(Server.MapPath("~/DataBackup/" + datafolder + "" + ".zip"));
                }



                HttpResponse Response = HttpContext.Current.Response; Response.Clear(); Response.ClearHeaders(); Response.Charset = "UTF-8";
                fs = File.Open(fullPath, FileMode.Open);
                byte[] bytBytes = new byte[(fs.Length)];
                fs.Read(bytBytes, 0, (int)fs.Length);
                fs.Close();
                Response.AddHeader("Content-disposition", "attachment; filename=" + datafolder + "" + ".zip");
                Response.ContentType = "application/octet-stream";
                Response.BinaryWrite(bytBytes);






                if (File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }
                if (File.Exists(fullPath))
                {
                    System.IO.File.Delete(fullPath);
                }

                Response.Flush();
                Response.End();
            }

            catch (System.Exception ex)
            {
                Response.Clear();

                //string mmsg = ex.Message;
                //showEXPMessages("(crateZip)  " + mmsg); //showMessages(mmsg);
            }
            finally
            {
                fs.Dispose();
                Response.Clear();

            }

            //str.Write(sbldr.ToString());
            //Response.ContentType = "Application/x-msexcel";
            //Response.AddHeader("content-disposition", "attachment;filename=test.csv");
            //Response.Write(sbldr.ToString());
            //Response.End();
        }
    }
   

    private void ExporttoExcel(GridView Gv, DataTable table, string FileName)
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
            int columnscount = Gv.HeaderRow.Cells.Count;


            for (int j = 0; j < columnscount; j++)
            {      //write in new column
                HttpContext.Current.Response.Write("<Td>");
                //Get column headers  and make it as bold in excel columns
                HttpContext.Current.Response.Write("<B>");
                HttpContext.Current.Response.Write(Gv.HeaderRow.Cells[j].Text);
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
    public DataTable CreateDataTable()
    {

        DataTable dtYear = new DataTable();
        dtYear.Columns.Add("Type", System.Type.GetType("System.String"));

        dtYear.Columns.Add("ID", System.Type.GetType("System.Int32"));
        return dtYear;
    }

    public void LoadUserLeavel()
    {
        conditions = "";
        if (Session["user_level_Role"].ToString() == "1")
        {
            //conditions = "StateCode='" + Session["StateCode"].ToString() + "' ";
            objComman.BindDLL("mst1State", "StateCode, dbo.TitleCase(upper(StateName)) as StateName", conditions, "StateName", "Desc", ddlState, "StateName", "StateCode", "--Select--");
            ddlState.Enabled = true;
            ddlDistrict.Enabled = true;
        }
        else if (Session["user_level_Role"].ToString() == "2")
        {
            conditions = "StateCode='" + Session["StateCode"].ToString() + "' ";
            objComman.BindDLL("mst1State", "StateCode, dbo.TitleCase(upper(StateName)) as StateName", conditions, "StateName", "Desc", ddlState, "StateName", "StateCode", "--Select--");

            ddlState.SelectedIndex = 1;
            ddlState.Enabled = true;
            ddlDistrict.Enabled = true;
        }
        else
        {
            conditions = "StateCode='" + Session["StateCode"].ToString() + "' ";
            objComman.BindDLL("mst1State", "StateCode, dbo.TitleCase(upper(StateName)) as StateName", conditions, "StateName", "Desc", ddlState, "StateName", "StateCode", "--Select--");

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
            conditions = "StateCode ='" + ddlState.SelectedValue + "' and Fyear= '" + ddlYear.SelectedItem.Text + "' ";
            objComman.BindDLL("mst2District", "DistrictCode,dbo.TitleCase(upper(DistrictName)) as DistrictName", conditions, "DistrictName", "asc", ddlDistrict, "DistrictName", "DistrictCode", "--Select--");

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


        objComman.BindDLL("mst2District", "DistrictCode,dbo.TitleCase(upper(DistrictName)) as DistrictName ", conditions, "DistrictName", "asc", ddlDistrict, "DistrictName", "DistrictCode", "Select");



    }


    protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillCBDist();
    }
    protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillCBBock();
    }
    protected void ddlBlock_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillCBCluster();
    }
    protected void ddlPanchayat_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillCVillage();
    }
   
    protected void ddlVillage_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillSchool();
    }
    public void FillSchool()
    {
        conditions = "";
        conditions = "VillageCode ='" + ddlVillage.SelectedValue + "' ";
       // objComman.BindDLL("mstSchool", "SchoolCode,Name", conditions, "Name", "asc", ddlSchool, "Name", "SchoolCode", "Select");


    }
    public void FillCBBock()
    {
        conditions = "";
        conditions = "DistrictCode ='" + ddlDistrict.SelectedValue + "'";
        objComman.BindDLL("mst3Block", "BlockCode,dbo.TitleCase(upper(BlockName)) as BlockName ", conditions, "BlockName", "asc", ddlBlock, "BlockName", "BlockCode", "--All--");



    }
    public void FillCBCluster()
    {
        conditions = "";
        conditions = "DistrictCode ='" + ddlDistrict.SelectedValue + "'  and BlockCode ='" + ddlBlock.SelectedValue + "'";
        objComman.BindDLL("mstPanchayat", "PanchayatCode,dbo.TitleCase(upper(PanchayatName)) as PanchayatName", conditions, "PanchayatName", "asc", ddlPanchayat, "PanchayatName", "PanchayatCode", "--All--");



    }
    public void FillCVillage()
    {
        conditions = "";
        conditions = "DistrictCode ='" + ddlDistrict.SelectedValue + "'  and BlockCode ='" + ddlBlock.SelectedValue + "' and  PanchayatCode='" + ddlPanchayat.SelectedValue + "'  ";
        objComman.BindDLL("mst5Village", "VillageCode,dbo.TitleCase(upper(VillageName)) as VillageName", conditions, "VillageName", "asc", ddlVillage, "VillageName", "VillageCode", "--All-");



    }

   

    public void LoadData()
    {
        string strQry = "";
        //if (Program.UserLevel == 1)
        //{
        //  strQry = " Select UniqueChildCode,Serial as ID,StrConv(ChildName,3) as [Child Name] from tblEnrolment  where VillageCode='" + CBVillage.SelectedValue + "' order by ChildName ";
        //}
        //else
        //{
        //    strQry = " Select UniqueCode,ChildCode as ID,ChildName1 as [Child Name] from tblDTD  where tblEnrolment='" + CBVillage.SelectedValue + "' order by ChildName1 ";

        //}
         conditions = "";
         if (ViewState["1"].ToString() == "5")
         {
             conditions = "  mst5Village.Fyear='2016-2017 '";

         }
         else
         {
             conditions = "  mst5Village.Fyear='" + ddlYear.SelectedItem.Text + "'";

         }
        conditions += " and mst5Village.StateCode='" + ddlState.SelectedValue.ToString() + "'";
        if (ViewState["1"].ToString() == "5")
        {
         
            if (ddlDistrict.SelectedIndex > 0)
            {
                strQry = "  SELECT OldDistrictCode from mst2District where DistrictCode ='" + ddlDistrict.SelectedValue.ToString() + "'";
                DataTable dt = objMain.LoadData(strQry);
                conditions = conditions + " and mst5Village.DistrictCode='" +dt.Rows[0]["OldDistrictCode"].ToString() + "'";
            }
        }
        else
        {
            if (ddlDistrict.SelectedIndex > 0)
            {
                conditions = conditions + "and mst5Village.DistrictCode='" + ddlDistrict.SelectedValue.ToString() + "'";
            }
        }
        if ( ddlBlock.SelectedIndex > 0)
        {
          
                conditions = conditions + "and mst5Village.BlockCode='" + ddlBlock.SelectedValue.ToString() + "'";
            
        }



        if ( ddlPanchayat.SelectedIndex > 0)
        {
            conditions = conditions + " and mst5Village.PanchayatCode='" + ddlPanchayat.SelectedValue.ToString() + "'";
        }
        if (ddlVillage.SelectedIndex > 0)
        {

            conditions += " and mst5Village.VillageCode = '" + ddlVillage.SelectedValue + "' ";
        }
      
        //if (ddlSchool.SelectedIndex > 0)
        //{
        //    conditions = conditions + " and s.SchoolCode='" + ddlSchool.SelectedValue.ToString() + "'";
        //}

        //if ( ddlVillage.SelectedIndex > 0)
        //{
        //    conditions = conditions + "and mst5Village.VillageCode='" + ddlVillage.SelectedValue.ToString() + "'";
        //}

        if (ViewState["1"].ToString() == "2")
        {
            SqlParameter[] parm = new SqlParameter[]
            {
             new SqlParameter("@Condition",  conditions),
       
      
            };
            DataTable dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rptAnuanalSipData]", parm);
            if (dt.Rows.Count > 0)
            {
                Session["SIP"] = dt;
                GV_DynamicGrid.DataSource = dt;
                GV_DynamicGrid.DataBind();
                lblTotalCount.Text = (dt.Rows.Count).ToString();
            }

            else
            {
                Session["SIP"] = null;
                GV_DynamicGrid.DataSource = null;
                GV_DynamicGrid.DataBind();
            }
        }

        if (ViewState["1"].ToString() == "3")
        {
            SqlParameter[] parm = new SqlParameter[]
            {
             new SqlParameter("@Condition",  conditions),
              new SqlParameter("@Fyear",  ddlYear.SelectedValue),
       
      
            };
            DataTable dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rptVillageRawData]", parm);
            if (dt.Rows.Count > 0)
            {
                Session["SIP"] = dt;
                lblTotalCount.Text = (dt.Rows.Count).ToString();
                if (dt.Rows.Count > 5000)
                {
                    btnCSV_Click(LinkButton3, null);
                }
                else
                {
                    GV_DynamicGrid.DataSource = dt;
                    GV_DynamicGrid.DataBind();
                }
               
            }

            else
            {
                Session["SIP"] = null;
                GV_DynamicGrid.DataSource = null;
                GV_DynamicGrid.DataBind();
            }
        }

        if (ViewState["1"].ToString() == "5")
        {

       
            SqlParameter[] parm = new SqlParameter[]
            {
             new SqlParameter("@Condition",  conditions),
                 new SqlParameter("@Fyear", ddlYear.SelectedValue),
       
      
            };
            DataTable dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rptTargetD2d]", parm);
            if (dt.Rows.Count > 0)
            {
                lblTotalCount.Text = (dt.Rows.Count).ToString();
                Session["D2DTarget"] = dt;
                if (dt.Rows.Count > 5000)
                {
                    btnCSV_Click(LinkButton3, null);
                }
                else
                {
                    GV_DynamicGrid.DataSource = dt;
                    GV_DynamicGrid.DataBind();
                  
                }
            }

            else
            {
                Session["D2DTarget"] = null;
                GV_DynamicGrid.DataSource = null;
                GV_DynamicGrid.DataBind();
            }
        }
        if (ViewState["1"].ToString() == "4")
        {
            DataTable dt = objMain.ReportD2dENrollmentStatus(conditions);
            if (dt.Rows.Count > 0)
            {
                lblTotalCount.Text = (dt.Rows.Count).ToString();
                Session["D2DAnual"] = dt;
                if (dt.Rows.Count > 5000)
                {
                    btnCSV_Click(LinkButton3, null);
                }
                else
                {
                    gvD2d.DataSource = dt;
                    gvD2d.DataBind();
                }
              
            }

            else
            {
                Session["D2DAnual"] = null;
                gvD2d.DataSource = null;
                gvD2d.DataBind();
            }
        }
      }
    protected void btnSerach_Click(object sender, EventArgs e)
    {
        GV_DynamicGrid.DataSource = null;
        GV_DynamicGrid.DataBind();
        gvD2d.DataSource = null;
        gvD2d.DataBind();
        GV_DynamicGrid.Visible = true;
        gvD2d.Visible = false;
        ViewState["1"] = 2;
        LoadData();
    }

    protected void btnVillageRaw_Click(object sender, EventArgs e)
    {
        GV_DynamicGrid.DataSource = null;
        GV_DynamicGrid.DataBind();
        gvD2d.DataSource = null;
        gvD2d.DataBind();
        GV_DynamicGrid.Visible = true;
        gvD2d.Visible = false;
        ViewState["1"] = 3;
        LoadData();
    }
    protected void btnD2d_Click(object sender, EventArgs e)
    {
        GV_DynamicGrid.DataSource = null;
        GV_DynamicGrid.DataBind();
        gvD2d.DataSource = null;
        gvD2d.DataBind();
        GV_DynamicGrid.Visible = false;
        gvD2d.Visible = true;
        ViewState["1"] = 4;
        LoadData();
    }
    protected void btnD2dTarget_Click(object sender, EventArgs e)
    {
        GV_DynamicGrid.DataSource = null;
        GV_DynamicGrid.DataBind();
        gvD2d.DataSource = null;
        gvD2d.DataBind();
        GV_DynamicGrid.Visible = true;
        gvD2d.Visible = false;
        ViewState["1"] = 5;
        LoadData();
    }
    protected void btnMain_Click(object sender, EventArgs e)
    {
    

    }
    
}