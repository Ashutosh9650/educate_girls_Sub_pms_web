using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
public partial class frmLearningCampMasterAgp : System.Web.UI.Page
{
    clsMain objMain = new clsMain();
    Comman objComman = new Comman();
    string conditions = string.Empty, Flag = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            if (Convert.ToString(Session["username"]) != "")
            {
                GridBindLearningMasterCamp();
                CampNumber();
            }
            else
            {
                Response.Redirect("Login.aspx", false);
            }

        }
    }


    public void CampNumber()
    {
        DataTable dt = new DataTable();
        dt = objMain.GetCampNo();
        ddlCampNo.DataSource = dt;
        ddlCampNo.DataValueField = "ID";
        ddlCampNo.DataTextField = "Name";
        ddlCampNo.DataBind();
        ddlCampNo.Items.Insert(0, new ListItem("--Select--", "0"));
    }

    protected void btnAddCamp_Click(object sender, EventArgs e)
    {
        Clear();
        ModalLearningCamp.Show();
    }

    public void Clear()
    {
        ddlCampNo.SelectedIndex = 0;
       
        txtSessioninCamp.Text = string.Empty;
      
        txtHindiBaselineSessionNo.Text = string.Empty;
        txtHindiEndlineSessionNo.Text = string.Empty;
      
        btnSave.Text = "Save";

    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        Clear();
        ModalLearningCamp.Show();
    }


    public DataTable GetCampExit(int campNo)
    {
        SqlParameter[] parm = new SqlParameter[]
            {
              new SqlParameter("@CampNo",  campNo),
            };
        DataTable dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "GetCampExitAgp", parm);
        return dt;
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            string UserName = Session["username"].ToString();
            int CampNo = 0; int CampId = 0;
            string CampDurationInWeek, HindiBaselineEndlineMaxScore1, MathBaselineEndlineMaxScore1, SessioninCamp, SessioninWeek, HindiBaselineSessionNo, HindiEndlineSessionNo, MathBaselineSessionNo, MathEndlineSessionNo, HindiBaselineHeading1, HindiBaselineHeading2, MathBaselineHeading1, MathBaselineHeading2, HindiBaselineEndlineMaxScore, MathBaselineEndlineMaxScore, HindiBaselineEndlineHeading2Active, MathBaselineEndlineHeading2Active,   HindiBaselineHeading3Name   , MathBaselineHeading3Name      , HindiBaselineEndlineHeading3Active     , MathBaselineEndlineHeading3Active ,      HindiBaselineEndlineMaxScore2       , MathBaselineEndlineMaxScore2 ,operation = string.Empty;

            if (hdnCampID.Value == "")
            {
                CampId = 0;

            }
            else
            {

                CampId = Convert.ToInt32(hdnCampID.Value);
            }

            CampNo = Convert.ToInt32(ddlCampNo.SelectedValue);
          
            SessioninCamp = txtSessioninCamp.Text;
      
            HindiBaselineSessionNo = txtHindiBaselineSessionNo.Text;
            HindiEndlineSessionNo = txtHindiEndlineSessionNo.Text;
         

    
            if (CampNo != 0)
            {

                if (btnSave.Text == "Update") //Update
                {
                    operation = "U";

                    int Result = InsertUpdateLearningCampMaster(CampId, CampNo, SessioninCamp, HindiBaselineSessionNo, HindiEndlineSessionNo, UserName, operation);

                    if (Result > 0)
                        {                     
                            Clear();
                            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Updated Sucessfully')</script>", false);
                            GridBindLearningMasterCamp();
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Saved Unsuccesfull')</script>", false);
                        }


                }
                else // Insert
                {
                    operation = "I";

                    DataTable dt = new DataTable();

                    dt = GetCampExit(CampNo);

                    if (dt.Rows.Count > 0)
                    {
                        ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Camp Nuber Already Exit,Please Select Another Camp Number')</script>", false);
                        ModalLearningCamp.Show();
                    }
                    else
                    {
                        int Result = InsertUpdateLearningCampMaster(CampId, CampNo, SessioninCamp, HindiBaselineSessionNo, HindiEndlineSessionNo, UserName, operation);

                        if (Result > 0)
                        {
                                 Clear();
                                 ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Saved sucessfully')</script>", false);
                                 GridBindLearningMasterCamp();
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Saved Unsuccesfull')</script>", false);
                        }
                    }
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Select Camp Number')</script>", false);
            }



        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public int InsertUpdateLearningCampMaster(int CampId, int campNo,  string sessioninCamp,  string hindiBaselineSessionNo, string hindiEndlineSessionNo, string userName, string operation)
    {
        SqlParameter[] cmdParameters = new SqlParameter[]
        {
                    new SqlParameter("@CampId", CampId),
                    new SqlParameter("@campNo", campNo),
                   
                    new SqlParameter("@sessioninCamp", sessioninCamp),
                   
                
                    new SqlParameter("@userName", userName),
                    new SqlParameter("@operation", operation),
                       new SqlParameter("@hindiBaselineSessionNo", hindiBaselineSessionNo),
                    new SqlParameter("@hindiEndlineSessionNo", hindiEndlineSessionNo),
                    
                              
    
    };
        return SqlHelper.ExecuteNonQuery(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "InsertUpdateLearningCampMasterAgp", cmdParameters);
    }
  
    public void GridBindLearningMasterCamp()
    {
        DataTable dt = new DataTable();
        dt = GridBindLearningMasterCampAgp();
        Session["dtGridLearningCampMaster"] = dt;
        GridLearningCampMaster.DataSource = (DataTable)dt;
        GridLearningCampMaster.DataBind();
    }
    public DataTable GridBindLearningMasterCampAgp()
    {
        DataTable dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "GetBindLearningMasterCampAgp");
        return dt;
    }
    protected void GridLearningCampMaster_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            GridLearningCampMaster.DataSource = (DataTable)Session["dtGridLearningCampMaster"];
            GridLearningCampMaster.PageIndex = e.NewPageIndex;
            GridLearningCampMaster.DataBind();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void GridLearningCampMaster_RowCommand(object sender, GridViewCommandEventArgs e)
    {

        if (e.CommandName.Equals("EditData"))
        {
            try
            {
                int index = Convert.ToInt32(e.CommandArgument);
                hdnCampID.Value = this.GridLearningCampMaster.DataKeys[index]["CampID"].ToString();
                ddlCampNo.SelectedValue = this.GridLearningCampMaster.DataKeys[index]["CampNumber"].ToString();
               
                txtSessioninCamp.Text = this.GridLearningCampMaster.DataKeys[index]["SessionInCamp"].ToString();
              
                txtHindiBaselineSessionNo.Text = this.GridLearningCampMaster.DataKeys[index]["BaselineSessionNo"].ToString();
                txtHindiEndlineSessionNo.Text = this.GridLearningCampMaster.DataKeys[index]["EndSessionNo"].ToString();
             

                btnSave.Text = "Update";
                ModalLearningCamp.Show();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        if (e.CommandName.Equals("DelefffteData"))
        {
            try
            {
                int index = Convert.ToInt32(e.CommandArgument);
                hdnCampID.Value = GridLearningCampMaster.DataKeys[index]["CampID"].ToString();
                int CampID = Convert.ToInt32(hdnCampID.Value);
                string operation = "D";
                string UserName = Session["username"].ToString();
                int Result = InsertUpdateLearningCampMaster(CampID, 0, "", "", "", UserName, operation);

                if (Result > 0)
                {
                    if (operation == "D")
                    {
                        Clear();
                        ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Learning Camp Master Data Deleted sucessfully')</script>", false);
                        GridBindLearningMasterCamp();
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }

    protected void btn_Delete_Click(object sender, EventArgs e)
    {
        ImageButton bt = (ImageButton)sender;

        GridViewRow gvr = (GridViewRow)bt.NamingContainer;

        string UniqueChildCode = (gvr.FindControl("Label2") as Label).Text;


        hdnCampID.Value = UniqueChildCode;
        int CampID = Convert.ToInt32(hdnCampID.Value);
        string operation = "D";
        string UserName = Session["username"].ToString();
        int Result = InsertUpdateLearningCampMaster(CampID, 0, "", "", "",UserName, operation);

        if (Result > 0)
        {
            if (operation == "D")
            {
                Clear();
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Learning Camp Master Data Deleted sucessfully')</script>", false);
                GridBindLearningMasterCamp();
            }
        }
       

    }
    protected void btnReprot_Click(object sender, EventArgs e)
    {
        SqlParameter[] cmdParameters = new SqlParameter[]
		{
			new SqlParameter("@ffh",""),
            
            
		};
        DataTable dt = null;


        dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[GetBindLearningMasterCampRportAgp]", cmdParameters);

        if (dt.Rows.Count > 0)
        {
           // ExporttoExcel(dt, "LearningCampMaster");
            ExportToExcelNew(dt, "LearningCampMaster");
        }

    }
    public void ExportToExcelNew(DataTable dt, string FileName)
    {
        Response.ClearContent();
        Response.Buffer = true;
        string Fullfilename = "" + FileName + "_" + DateTime.Now.ToString("dd_MM_yyyy_hh_mm_ss") + ".xls";

        Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", Fullfilename));
        Response.ContentType = "application/ms-excel";
        Response.ContentEncoding = System.Text.Encoding.Unicode;
        Response.BinaryWrite(System.Text.Encoding.Unicode.GetPreamble());
        DataTable dt1 = dt;
        string str = string.Empty;
        foreach (DataColumn dtcol in dt1.Columns)
        {
            Response.Write(str + dtcol.ColumnName);
            str = "\t";
        }
        Response.Write("\n");
        foreach (DataRow dr in dt1.Rows)
        {
            str = "";
            for (int j = 0; j < dt1.Columns.Count; j++)
            {
                Response.Write(str + Convert.ToString(dr[j]));
                str = "\t";
            }
            Response.Write("\n");
        }
        Response.End();
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
                Response.ContentEncoding = System.Text.Encoding.UTF8;
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