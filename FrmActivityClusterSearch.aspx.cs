using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.IO;
using System.Data.SqlClient;

public partial class FrmActivityClusterSearch : System.Web.UI.Page
{
    clsMain objMain = new clsMain();
    string conditions = "";
    Comman objComman = new Comman();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Convert.ToString(Session["username"]) != "")
        {

            if (!IsPostBack)
            {
                LoadData();
                if (Session["user_level"].ToString() == "24")
                {
                    btnApprove.Visible = false;
                }
                else
                {
                    btnApprove.Visible = false;
                }
                TxtFromDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                txtDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                btnApprove.Visible = false;
                btnsave.Visible = false;
            }
        }
        else
        {
            Response.Redirect("Login.aspx", false);

        }
    }
   
    public void LoadData()
    {
        
       
        conditions = "";
        if (Session["user_level"].ToString() == "39")
        {
            conditions =  "  DistrictCode='" + Session["DistrictCode"].ToString() + "' ";

           

            objComman.BindDLL("mst3Block", "BlockCode,dbo.TitleCase(upper(BlockName)) as BlockName ", conditions, "BlockName", "asc", ddlBlock, "BlockName", "BlockCode", "--Select--");
            ddlBlock.Enabled = true;
        }
        else
        {

            conditions = conditions + "  DistrictCode='" + Session["DistrictCode"].ToString() + "'  and BlockCode ='" + Session["BlockCode"].ToString() + "' ";

            

            objComman.BindDLL("mst3Block", "BlockCode,dbo.TitleCase(upper(BlockName)) as BlockName ", conditions, "BlockName", "asc", ddlBlock, "BlockName", "BlockCode", "--Select--");
       
            ddlBlock.Enabled = false;

            ddlBlock.SelectedValue = Session["BlockCode"].ToString();
        }

        

        
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
      //  DGV_Report.Visible = true;
        Gv_Profile_Search.Visible = false;
        string fromDate = TxtFromDate.Text;
        string[] d = fromDate.Split('/');
        string afromDate = d[2] + '-' + d[1] + '-' + d[0];

        string ToDate = txtDate.Text;
        string[] c = ToDate.Split('/');
        string aToDate = c[2] + '-' + c[1] + '-' + c[0];

        SqlParameter[] parm = new SqlParameter[]
             {
               new SqlParameter("@fDate",  afromDate),
               new SqlParameter("@todate",  aToDate),
              
      
                 };

        DataTable dtUserVillage = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rptActivityUdate]", parm);
        //DGV_Report.DataSource = dtUserVillage;
        //DGV_Report.DataBind();
        //ViewState["dtUserVillage"] = dtUserVillage;
    }
    protected void Gv_Profile_Search_OnRowCommand(object sender, GridViewRowEventArgs e)
    {
       
    }


    protected void Export_To_Excel(object sender, EventArgs e)
    {
        DataTable dt= ViewState["dtUserVillage"] as DataTable;
       // ExporttoExcel(DGV_Report, dt);
        

    }
    private void ExporttoExcel(GridView Gv, DataTable table)
    {


        HttpContext.Current.Response.Clear();
        HttpContext.Current.Response.ClearContent();
        HttpContext.Current.Response.ClearHeaders();
        HttpContext.Current.Response.Buffer = true;
        HttpContext.Current.Response.ContentType = "application/ms-excel";
        HttpContext.Current.Response.Write(@"<!DOCTYPE HTML PUBLIC ""-//W3C//DTD HTML 4.0 Transitional//EN"">");
        HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=Reports.xls");

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
    

    public void LoadExecel()
    {
      

        //Response.Clear();
        //Response.Buffer = true;
        //Response.AddHeader("content-disposition", "attachment;filename=EG_Report_" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + ".xls");
        //Response.Charset = "";
        //Response.ContentType = "application/vnd.ms-excel";
        //using (StringWriter sw = new StringWriter())
        //{
        //    HtmlTextWriter hw = new HtmlTextWriter(sw);

        //    //To Export all pages
        //    DGV_Report.AllowPaging = false;
           

        //    DGV_Report.HeaderRow.BackColor = Color.White;
        //    foreach (TableCell cell in DGV_Report.HeaderRow.Cells)
        //    {
        //        cell.BackColor = DGV_Report.HeaderStyle.BackColor;
        //    }
        //    foreach (GridViewRow row in DGV_Report.Rows)
        //    {
        //        row.BackColor = Color.White;
        //        foreach (TableCell cell in row.Cells)
        //        {
        //            if (row.RowIndex % 2 == 0)
        //            {
        //                cell.BackColor = DGV_Report.AlternatingRowStyle.BackColor;
        //            }
        //            else
        //            {
        //                cell.BackColor = DGV_Report.RowStyle.BackColor;
        //            }
        //            cell.CssClass = "textmode";
        //        }
        //    }

        //    DGV_Report.RenderControl(hw);

        //    //style to format numbers to string
        //    string style = @"<style> .textmode { } </style>";
        //    Response.Write(style);
        //    Response.Output.Write(sw.ToString());
        //    Response.Flush();
        //    Response.End();
        //}
    }
    protected void btnApprove_Click(object sender, EventArgs e)
    {
        //if (ddlBlock.SelectedIndex <= 0)
        //{

        //    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select User')</script>", false);
        //    return;
        //}
        string fromDate = TxtFromDate.Text;
        string[] d = fromDate.Split('/');
        string afromDate = d[2] + '-' + d[1] + '-' + d[0];

        string ToDate = txtDate.Text;
        string[] c = ToDate.Split('/');
        string aToDate = c[2] + '-' + c[1] + '-' + c[0];

        string condation = "";
        if (Session["user_level"].ToString() == "19")
        {
            condation = "ActivityDate between('" + afromDate + "') and '" + aToDate + "' and UserID='" + ddlBlock.SelectedValue + "' and  UserEntry ='2'  ";
        }
        if (Session["user_level"].ToString() == "39")
        {
            condation = "ActivityDate between('" + afromDate + "') and '" + aToDate + "' and UserID='" + ddlBlock.SelectedValue + "' and  UserEntry ='3' ";
        }

        DataTable dtApprove =null;
    //    DataTable dtApprove = objMain.LoadSchoolActivtiyApprove(condation);

        if (dtApprove.Rows.Count > 0)
        {
            int MainResult = 0;
                String[] arColoumn = { "ApproveStatus" };
              DataTable dtDistinct = dtApprove.DefaultView.ToTable(true, arColoumn);
              string Statas = "";
              foreach (DataRow Item in dtDistinct.Rows)
              {
                  if (Session["user_level"].ToString() == "19")
                  {
                      Statas = "B";
                      if (Item["ApproveStatus"].ToString() == "FC")
                      {

                      }
                      //if (Item["ApproveStatus"].ToString() == "B")
                      //{
                      //    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Allready Approve ')</script>", false);
                      //    btnSerach_Click(btnSerach, null);
                      //    return;
                      //}
                      if (Item["ApproveStatus"].ToString() == "I")
                      {
                          ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Impact Officer Allready Approve ')</script>", false);
                          btnSerach_Click(btnSerach, null);
                          return;
                        
                      }
                  }

                  if (Session["user_level"].ToString() == "39")
                  {
                      Statas = "I";
                      if (Item["ApproveStatus"].ToString() == "FC")
                      {
                          ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Frist  Approve By BO ')</script>", false);
                          btnSerach_Click(btnSerach, null);
                          return;
                         
                      }

                      //if (Item["ApproveStatus"].ToString() == "E")
                      //{
                      //    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Impact Officer Allready Approver ')</script>", false);
                      //    btnSerach_Click(btnSerach, null);
                      //    return;

                      //}
                  }
              }
              string Newcondation = "ActivityDate between('" + afromDate + "') and '" + aToDate + "' and UserID='" + ddlBlock.SelectedValue + "'";
            MainResult = objMain.ActivitySchoolStatusUpdate(Statas,Newcondation);
            if (MainResult>0)
            {
                  ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Approve sucessfully')</script>", false);
                  btnSerach_Click(btnSerach, null);
            }



        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('No Record Found ')</script>", false);
            return;
        }

    }
    protected void btnSerach_Click(object sender, EventArgs e)
    {
        LoadSerarchSchoolActivity();
        LoadSearchVillageActivity();
    }
    public void LoadSerarchSchoolActivity()
    {
        Session["dt"] = null;
       // DGV_Report.Visible = false;
        Gv_Profile_Search.Visible = true;

        if (ddlBlock.SelectedIndex <= 0)
        {

            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select User')</script>", false);
            return;
        }



        string fromDate = TxtFromDate.Text;
        string[] d = fromDate.Split('/');
        string afromDate = d[2] + '-' + d[1] + '-' + d[0];

        string ToDate = txtDate.Text;
        string[] c = ToDate.Split('/');
        string aToDate = c[2] + '-' + c[1] + '-' + c[0];


        DateTime d1 = Convert.ToDateTime(afromDate);
        DateTime d2 = Convert.ToDateTime(aToDate);
        int month = Convert.ToInt32(c[1]) - Convert.ToInt32(d[1]);
        TimeSpan t = d2 - d1;

        double Days = Convert.ToDouble(t.TotalDays);
        if (Math.Sign(Days) == -1)
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select Vaild Date')</script>", false);
            return;
        }
        if (Math.Round(Days) >= 7)
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select Date rang 7 Day')</script>", false);
            return;
        }
        DataTable dtMain = null;
        string con = "";
        if (Session["user_level"].ToString() == "19")
        {
            con = "ActivityDate between('" + afromDate + "') and '" + aToDate + "' and mst5village.BlockCode='" + Session["BlockCode"].ToString() + "' ";
            dtMain = objMain.LoadSchoolActivtiyCluseter(afromDate, aToDate, ddlBlock.SelectedValue, con);

        }
        if (Session["user_level"].ToString() == "39")
        {
            con = "ActivityDate between('" + afromDate + "') and '" + aToDate + "' and mst5village.BlockCode='" + ddlBlock.SelectedValue + "' ";
            dtMain = objMain.LoadSchoolActivtiyCluseter(afromDate, aToDate, ddlBlock.SelectedValue, con);
           // dtMain = objMain.LoadSchoolActivtiyCluseterIO(afromDate, aToDate, ddlBlock.SelectedValue, con);
        }
        string condation = "";
        //if (Session["user_level"].ToString() == "19" )
        //{
        //     condation = "ActivityDate between('" + afromDate + "') and '" + aToDate + "' and UserID='" + ddlBlock.SelectedValue + "' and  UserEntry ='2'  ";
        //}
        // if (Session["user_level"].ToString() == "39" )
        //{
        //      condation= "ActivityDate between('" + afromDate + "') and '" + aToDate + "' and UserID='" + ddlBlock.SelectedValue + "' and  UserEntry ='3' ";
        //}

        // DataTable dtApprove = objMain.LoadSchoolActivtiyApprove(condation);

        // Session["dtApprove"] = dtApprove;
        int count = 0;
        if (dtMain.Rows.Count > 0)
        {
            string strGSS = "TB Handholding";
            DataRow[] dr = dtMain.Select("School='" + strGSS + "'");
            if (dr.Length > 0)
            {


            }
            else
            {
                DataRow Item1;
                Item1 = dtMain.NewRow();
                dtMain.Rows.Add(Item1);


                Item1["SRNo"] = 2;
                Item1["School"] = "TB Handholding";
            }

            string strGSS3 = "School Count";
            DataRow[] dr3 = dtMain.Select("School='" + strGSS3 + "'");
            if (dr3.Length > 0)
            {


            }
            else
            {
                DataRow Item1;
                Item1 = dtMain.NewRow();
                dtMain.Rows.Add(Item1);


                Item1["SRNo"] = 1;
                Item1["School"] = "School Count";
            }

            string strGSS4 = "SMC";
            DataRow[] dr4 = dtMain.Select("School='" + strGSS4 + "'");
            if (dr4.Length > 0)
            {


            }
            else
            {
                DataRow Item1;
                Item1 = dtMain.NewRow();
                dtMain.Rows.Add(Item1);


                Item1["SRNo"] = 3;
                Item1["School"] = "SMC";
            }

            string strGSS5 = "CLT";
            DataRow[] dr5 = dtMain.Select("School='" + strGSS5 + "'");
            if (dr5.Length > 0)
            {


            }
            else
            {
                DataRow Item1;
                Item1 = dtMain.NewRow();
                dtMain.Rows.Add(Item1);


                Item1["SRNo"] = 4;
                Item1["School"] = "CLT";
            }
            string strGSS56 = "Balsabha";
            DataRow[] dr6 = dtMain.Select("School='" + strGSS56 + "'");
            if (dr6.Length > 0)
            {


            }
            else
            {
                DataRow Item1;
                Item1 = dtMain.NewRow();
                dtMain.Rows.Add(Item1);

                Item1["SRNo"] = 5;
                Item1["School"] = "Balsabha";
            }


            string strGSS1 = "Life skill Games";
            DataRow[] dr1 = dtMain.Select("School='" + strGSS1 + "'");
            if (dr1.Length > 0)
            {


            }
            else
            {
                DataRow Item1;
                Item1 = dtMain.NewRow();
                dtMain.Rows.Add(Item1);

                Item1["SRNo"] = 6;

                Item1["School"] = "Life skill Games";
            }


            string strGSS123 = "SAC Update";
            DataRow[] dr21 = dtMain.Select("School='" + strGSS123 + "'");
            if (dr21.Length > 0)
            {


            }
            else
            {
                DataRow Item1;
                Item1 = dtMain.NewRow();
                dtMain.Rows.Add(Item1);


                Item1["SRNo"] = 7;
                Item1["School"] = "SAC Update";
            }
            string strGSS1231 = "School Physical Facility";
            DataRow[] dr211 = dtMain.Select("School='" + strGSS1231 + "'");
            if (dr211.Length > 0)
            {


            }
            else
            {
                DataRow Item1;
                Item1 = dtMain.NewRow();
                dtMain.Rows.Add(Item1);


                Item1["SRNo"] = 8;
                Item1["School"] = "School Physical Facility";
            }

            string strGSS12311 = "Annual Data";
            DataRow[] dr2111 = dtMain.Select("School='" + strGSS12311 + "'");
            if (dr2111.Length > 0)
            {


            }
            else
            {
                DataRow Item1;
                Item1 = dtMain.NewRow();
                dtMain.Rows.Add(Item1);


                Item1["SRNo"] = 9;
                Item1["School"] = "Annual Data";
            }
            DataView dataview = dtMain.DefaultView;
            dataview.Sort = "SRNo";
            DataTable dt = dataview.ToTable();
            Gv_Profile_Search.DataSource = dt;
            Gv_Profile_Search.DataBind();
            DataRow[] drApp = null;
            //   Gv_Profile_Search.HeaderRow.Cells[count].Text = "T.B.Hand Holding";
            for (int Index = 1; Index < Gv_Profile_Search.HeaderRow.Cells.Count; Index++)
            {
                #region ApproveBy
                var firstCell = Gv_Profile_Search.HeaderRow.Cells[Index];

                #endregion
                firstCell.Controls.Clear();
                firstCell.Controls.Add(new HyperLink { NavigateUrl = "./FrmSchoolProfileSearch.aspx?ID=" + firstCell.Text + "," + TxtFromDate.Text + "," + txtDate.Text + "", Text = firstCell.Text });
            }

            //Gv_Profile_Search.Rows[1]["	OrderCount"].Visible = false;

        }
        else
        {
            Gv_Profile_Search.DataSource = null;
            Gv_Profile_Search.DataBind();
        }
     

  
    }

    public void LoadSearchVillageActivity()
    {
        Session["dt"] = null;

        if (ddlBlock.SelectedIndex <= 0)
        {

            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select User')</script>", false);
            return;
        }


        string fromDate = TxtFromDate.Text;
        string[] d = fromDate.Split('/');
        string afromDate = d[2] + '-' + d[1] + '-' + d[0];

        string ToDate = txtDate.Text;
        string[] c = ToDate.Split('/');
        string aToDate = c[2] + '-' + c[1] + '-' + c[0];


        DateTime d1 = Convert.ToDateTime(afromDate);
        DateTime d2 = Convert.ToDateTime(aToDate);
        int month = Convert.ToInt32(c[1]) - Convert.ToInt32(d[1]);
        TimeSpan t = d2 - d1;

        double Days = Convert.ToDouble(t.TotalDays);
        if (Math.Sign(Days) == -1)
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select Vaild Date')</script>", false);
            return;
        }
        if (Math.Round(Days) >= 7)
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select Date rang 7 Day')</script>", false);
            return;
        }
        string con = " ";
        DataTable dtMain =null;

        if (Session["user_level"].ToString() == "19")
        {
            con = "ActivityDate between('" + afromDate + "') and '" + aToDate + "' and mst5village.BlockCode='" + Session["BlockCode"].ToString() + "' ";
            dtMain = objMain.LoadVillageActivtiyCluseter(afromDate, aToDate, ddlBlock.SelectedValue, con);

        }
        if (Session["user_level"].ToString() == "39")
        {
            con = "ActivityDate between('" + afromDate + "') and '" + aToDate + "' and mst5village.BlockCode='" + ddlBlock.SelectedValue + "' ";
           // dtMain = objMain.LoadVillageActivtiyCluseterIO(afromDate, aToDate, ddlBlock.SelectedValue, con);
            dtMain = objMain.LoadVillageActivtiyCluseter(afromDate, aToDate, ddlBlock.SelectedValue, con);
        }

   //     DataTable dtApprove = objMain.LoadVillageActivtiyApprove(condation);

        int count = 0;
        if (dtMain.Rows.Count > 0)
        {
            string strGSS = "GSS";
            DataRow[] dr = dtMain.Select("Village='" + strGSS + "'");
            if (dr.Length > 0)
            {


            }
            else
            {
                DataRow Item1;
                Item1 = dtMain.NewRow();
                dtMain.Rows.Add(Item1);



                Item1["Village"] = "GSS";
            }

            string strGSS3 = "Village Count";
            DataRow[] dr3 = dtMain.Select("Village='" + strGSS3 + "'");
            if (dr3.Length > 0)
            {


            }
            else
            {
                DataRow Item1;
                Item1 = dtMain.NewRow();
                dtMain.Rows.Add(Item1);



                Item1["Village"] = "Village Count";
            }

            string strGSS4 = "Mauhalla Meeting";
            DataRow[] dr4 = dtMain.Select("Village='" + strGSS4 + "'");
            if (dr4.Length > 0)
            {


            }
            else
            {
                DataRow Item1;
                Item1 = dtMain.NewRow();
                dtMain.Rows.Add(Item1);



                Item1["Village"] = "Mauhalla Meeting";
            }

            string strGSS5 = "Other Community Meeting";
            DataRow[] dr5 = dtMain.Select("Village='" + strGSS5 + "'");
            if (dr5.Length > 0)
            {


            }
            else
            {
                DataRow Item1;
                Item1 = dtMain.NewRow();
                dtMain.Rows.Add(Item1);



                Item1["Village"] = "Mauhalla Meeting";
            }
            string strGSS56 = "Community Contact";
            DataRow[] dr6 = dtMain.Select("Village='" + strGSS56 + "'");
            if (dr6.Length > 0)
            {


            }
            else
            {
                DataRow Item1;
                Item1 = dtMain.NewRow();
                dtMain.Rows.Add(Item1);



                Item1["Village"] = "Community Contact";
            }


            string strGSS1 = "Support";
            DataRow[] dr1 = dtMain.Select("Village='" + strGSS1 + "'");
            if (dr1.Length > 0)
            {


            }
            else
            {
                DataRow Item1;
                Item1 = dtMain.NewRow();
                dtMain.Rows.Add(Item1);



                Item1["Village"] = "Support";
            }
            DataView dataview = dtMain.DefaultView;
            dataview.Sort = "Village";
            DataTable dt = dataview.ToTable();
            gvVillageActivity.DataSource = dt;
            gvVillageActivity.DataBind();
            // gvVillageActivity.HeaderRow.Cells[count].Text = "T.B.Hand Holding";
            DataRow[] drApp = null;
            for (int Index = 1; Index < gvVillageActivity.HeaderRow.Cells.Count; Index++)
            {
                #region ApproveBy
                var firstCell = gvVillageActivity.HeaderRow.Cells[Index];

                #endregion
                firstCell.Controls.Clear();

                firstCell.Controls.Add(new HyperLink { NavigateUrl = "./FrmSchoolProfileSearch.aspx?ID=" + firstCell.Text + "," + TxtFromDate.Text + "," + txtDate.Text + "", Text = firstCell.Text });

            }
        }
        else
        {
            gvVillageActivity.DataSource = null;
            gvVillageActivity.DataBind();
        }
       
    }
    //protected void ddlBlock_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    string strQry = "";
    //    if (ddlBlock.SelectedIndex > 0)
    //    {
    //        strQry = "   select Villagecode  from MstUser   where UserName='" + ddlBlock.SelectedValue + "' ";
    //        DataTable dtUserVillage = objMain.LoadData(strQry);

    //        string strVillage = dtUserVillage.Rows[0]["Villagecode"].ToString();

    //        conditions = "mst5Village.VillageCode in(" + strVillage + ") ";

    //     //   objComman.BindDLL("mst5Village", "VillageCode,VillageName ", conditions, "", "", ddlVilage, "VillageName", "VillageCode", "Select");


    //    }
    //}
  

    protected void TestGridView_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            //string quantity = e.Row.Cells[3].Text;
          
            //foreach (TableCell cell in e.Row.Cells)
            //{
               
            //        cell.BackColor = Color.Red;
               
            //}
        }
    }
    //protected void Gv_Profile_Search_OnRowCommand(object sender, GridViewCommandEventArgs e)
    //{
    //    if (e.CommandName == "GVUIO")
    //    {
    //        int iIndex = Convert.ToInt32(e.CommandArgument);
    //        string VDate = Gv_Profile_Search.DataKeys[iIndex]["VDate"].ToString();
    //        Response.Redirect("./frmMobileVillageProfile.aspx?ID=" + ddlVilage.SelectedValue + "," + ddlBlock.SelectedValue + "," + VDate + "");
    //    }
 
}