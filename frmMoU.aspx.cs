using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;

public partial class frmMoU : System.Web.UI.Page
{
    string conditions = string.Empty;
    clsMain objMain = new clsMain();
    Comman objComman = new Comman();
    string Cond = string.Empty, GR_UID = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Form.Attributes.Add("enctype", "multipart/form-data");
        if (!IsPostBack)
        {
            if (Convert.ToString(Session["username"]) != "")
            {
                LoadUserLeavel();
                ViewState["Save"] = "I";

            }
            else
            {
                Response.Redirect("Login.aspx", false);
            }
        }
    }
    #region Button Click Events
    protected void btnSerach_Click(object sender, EventArgs e)
    {
        if (ddl_State.SelectedIndex > 0)
        {
            Cond = "where D.StateCode='" + ddl_State.SelectedValue + "'";
        }
        if (ddl_District.SelectedIndex > 0)
        {
            Cond = Cond + " and D.DistrictCode='" + ddl_District.SelectedValue + "'";
        }
       
        SqlParameter[] paramv = new SqlParameter[]
                    {                            
                            new SqlParameter("@Cond",Cond),                           
                          
                    };
        DataTable dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "EG_Govt_Mou", paramv);
        GV_MOU_Main.DataSource = dt;
        GV_MOU_Main.DataBind();
        if (GV_MOU_Main.Rows.Count > 0)
        {
            LinkButton lnk = (LinkButton)GV_MOU_Main.Rows[0].Cells[0].FindControl("lblDistrictName");
            //lblDistrictName_OnClick(lnk, null);
            ViewState["Save"] = "U";
        }
        else
        {
            ViewState["Save"] = "I";
        }
        
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
    }
    protected void btnsave_Click(object sender, EventArgs e)
    {
        SaveData();
    }    
    protected void btnAdd_Click(object sender, EventArgs e)
    {

        ViewState["Save"] = "I";
        GV_Display.DataSource = null;
        GV_Display.DataBind();
        GV_TaskForce_Left.DataSource = null;
        GV_TaskForce_Left.DataBind();
        GV_TaskForce_Right.DataSource = null;
        GV_TaskForce_Right.DataBind();
        BtnAddTasKforce.Visible = false;
      
        lnkUpload.Visible = false;
        lnkMeetingDow.Visible = false;
        txtStartDate.Text = "";
        TxtEndDate.Text = "";
        BtnAddparticipant.Visible = false;
    }
    protected void BtnAddparticipant_Click(object sender, EventArgs e)
    {
        DataTable DtDist = ViewState["CurrentTable"] as DataTable;
        if (GV_Display.Rows.Count > 0)
        {
            GV_MOU.DataSource = ViewState["CurrentTable"] as DataTable;
            GV_MOU.DataBind();
        }
        else
        {

            FirstGridViewRow(1, 0);
        }

        ModalPopupExtender.Show();
    }
    protected void AddTasKforce_Click(object sender, EventArgs e)
    {

        FirstGridViewRow(2, 1);
        ViewState["Task"] = "I";
        TxtDatePopup.Text = "";
        TxtMinutes.Text = "";
        DTF();
        ModalPopupExtender1.Show();

    }
    protected void Lnk_RowNumber_OnClick(object sender, EventArgs e)
    {
        try
        {

            LinkButton lb = (LinkButton)sender;
            GridViewRow gvr = (GridViewRow)lb.Parent.Parent;
            int Index = gvr.RowIndex;
            ViewState["RowNumber"] = Convert.ToInt32(lb.Text);
            FirstGridViewRow(3, Convert.ToInt32(ViewState["RowNumber"]));
        }
        catch (Exception)
        {

            throw;
        }

    }
    protected void lblDistrictName_OnClick(object sender, EventArgs e)
    {
        try
        {
            GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
            int index = gvRow.RowIndex;
            GR_UID = GV_MOU_Main.DataKeys[index]["GR_UID"].ToString();
            FillDateSubGrid(GR_UID);
        }
        catch (Exception)
        {

            throw;
        }
    }
    protected void btnsavePopup_OnClick(object sender, EventArgs e)
    {
        DataTable dt;
        int ret = 0;
        string GRRep_UID = string.Empty, Level = string.Empty, Desig = string.Empty, Name1 = string.Empty, Phone = string.Empty, E_mail = string.Empty;
        try
        {
            if (ViewState["GR_UID"].ToString() != "")
            {
                if (GV_MOU.Rows.Count > 0)
                {
                    dt = (DataTable)ViewState["CurrentTable"];
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        DropDownList FirstLevel = (DropDownList)GV_MOU.Rows[i].Cells[0].FindControl("ddlFirstLevel");
                        TextBox Designation = (TextBox)GV_MOU.Rows[i].Cells[1].FindControl("Txt_Designation");
                        TextBox Name = (TextBox)GV_MOU.Rows[i].Cells[2].FindControl("Txt_Name");
                        TextBox Phone_No = (TextBox)GV_MOU.Rows[i].Cells[3].FindControl("Txt_Phone_No");
                        TextBox Email = (TextBox)GV_MOU.Rows[i].Cells[4].FindControl("Txt_E_mail");

                        dt.Rows[i]["Level"] = FirstLevel.SelectedValue;
                        dt.Rows[i]["Designation"] = Designation.Text;
                        dt.Rows[i]["Name"] = Name.Text;
                        dt.Rows[i]["PhoneNo"] = Phone_No.Text;
                        dt.Rows[i]["Email"] = Email.Text;
                    }


                    if (dt.Rows.Count > 0)
                    {

                        for (int i = 0; i < GV_MOU.Rows.Count; i++)
                        {
                            dt.Rows[i]["GR_UID"] = ViewState["GR_UID"].ToString();
                        }


                    }

                    ViewState["CurrentTable"] = dt;
                }
                //[GRRep_UID],[GR_UID]
                for (int i = 0; i < GV_MOU.Rows.Count; i++)
                {
                    dt = (DataTable)ViewState["CurrentTable"];
                    GRRep_UID = dt.Rows[i]["GRRep_UID"].ToString();
                    GR_UID = dt.Rows[i]["GR_UID"].ToString();
                    Level = dt.Rows[i]["Level"].ToString();
                    Desig = dt.Rows[i]["Designation"].ToString();
                    Name1 = dt.Rows[i]["Name"].ToString();
                    Phone = dt.Rows[i]["PhoneNo"].ToString();
                    E_mail = dt.Rows[i]["Email"].ToString();
                    if (dt.Rows[i]["GRRep_UID"].ToString() != "")
                    {
                        ret = Insert_GovtRep_Add_Update(GRRep_UID, GR_UID, Level, Desig, Name1, Phone, E_mail, "U");

                    }
                    else
                    {
                        ret = Insert_GovtRep_Add_Update(GRRep_UID, GR_UID, Level, Desig, Name1, Phone, E_mail, "I");
                    }
                }


                //string row = INSERT_ImportDataSingle(dt, "IU_IMPORT_tblGovtRelationsRep", "tblGovtRelationsRep", ViewState["Save"].ToString());
                if (ret > 0)
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Saved sucessfully')</script>", false);
                    ModalPopupExtender.Hide();
                    
                    FillDateSubGrid(ViewState["GR_UID"].ToString());
                    BtnAddTasKforce.Visible = true;
                   
                }
            }

        }
        catch (Exception)
        {

            throw;
        }



    }
    public void ButtonAdd_Click1(object sender, EventArgs e)
    {
        AddNewRow(1, 0);

        ModalPopupExtender.Show();

    }
    public void ButtonAdd_Click2(object sender, EventArgs e)
    {
        AddNewRow(2, 0);
    }
    public void ButtonAdd_Click3(object sender, EventArgs e)
    {
        AddNewRow(3, Convert.ToInt32(ViewState["RowNumber"]));
    }
    public void BtnImageEdit_OnClick(object sender, EventArgs e)
    {
        try
        {
            DTF();
            ViewState["CurrentTable1"] = null;
            GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
            int index = gvRow.RowIndex;
            //GRMtg_UID
            string GRMtg_UID = GV_TaskForce_Left.DataKeys[index]["GRMtg_UID"].ToString();
            DataTable DtTaskForce = objMain.LoadData("SELECT  [GRMtgAction_UID],[GRMtg_UID],[ActionPoint],[Status]   FROM [tblGovtRelationsMtgAction] where GRMtg_UID='" + GRMtg_UID + "'");
            DataTable DtTaskForce1 = objMain.LoadData("SELECT  [GRMtg_UID],[GR_UID],convert(varchar(30),Date,107)as Date,[Minutes],MtgType,MinutesUpload FROM [tblGovtRelationsMtg] where GRMtg_UID='" + GRMtg_UID + "'");
            if (DtTaskForce.Rows.Count > 0)
            {
                ViewState["GRMtg_UID"] = GRMtg_UID;
                TxtDatePopup.Text = DtTaskForce1.Rows[0]["Date"].ToString();
                TxtMinutes.Text = DtTaskForce1.Rows[0]["Minutes"].ToString();
                ddlRBT.SelectedValue = DtTaskForce1.Rows[0]["MtgType"].ToString();
                if (DtTaskForce1.Rows[0]["MinutesUpload"].ToString() == "True")
                {
                    lnkMeetingDow.Visible = true;
                }
                else
                {
                    lnkMeetingDow.Visible = false;
                }
                Gv_TaskForce_Right1.DataSource = DtTaskForce;
                Gv_TaskForce_Right1.DataBind();
                ModalPopupExtender1.Show();
                ViewState["Task"] = "U";
                ViewState["CurrentTable1"] = DtTaskForce;
            }
            else
            {
                FirstGridViewRow(2, 1);
                ViewState["Task"] = "U";
                TxtDatePopup.Text = DtTaskForce1.Rows[0]["Date"].ToString();
                TxtMinutes.Text = DtTaskForce1.Rows[0]["Minutes"].ToString();
                ModalPopupExtender1.Show();
            }

        }
        catch (Exception)
        {

            throw;
        }

    }
    public void BtnSaveTask_OnClick(object sender, EventArgs e)
    {
        DataTable DTask = ViewState["CurrentTable1"] as DataTable;
        int ret = 0; string row = string.Empty;
        string Date;
        string GRMtgAction_UID = string.Empty, GRMtg_UID = string.Empty, ActionPoint = string.Empty, Sta = string.Empty;
        try
        {
            if (ViewState["Task"].ToString() == "U")
            {
                if (ViewState["GR_UID"].ToString() != null)
                {

                    DataTable DtSelect = objMain.LoadData("Select GRMtg_UID,GR_UID,Convert(varchar(15),Date,107) as Date,Minutes From tblGovtRelationsMtg where GR_UID='" + ViewState["GR_UID"].ToString() + "'");
                     Date = Convert.ToDateTime(TxtDatePopup.Text).ToString("yyyy-MM-dd");
                    if (DtSelect.Rows.Count > 0)
                    {
                        if (Mtg_Upload.HasFile)
                        {

                            ret = Insert_TaskForce_Add_Update(ViewState["GRMtg_UID"].ToString(), ViewState["GR_UID"].ToString(), Date, TxtMinutes.Text, ddlRBT.SelectedValue, true, "U");
                        }
                        else
                        {
                            ret = Insert_TaskForce_Add_Update(ViewState["GRMtg_UID"].ToString(), ViewState["GR_UID"].ToString(), Date, TxtMinutes.Text, ddlRBT.SelectedValue, false, "U");
                        }
                        //ViewState["GRMtg_UID"] = ret.ToString();
                        if (Mtg_Upload.HasFile)
                        {
                            Mtg_Upload.SaveAs(Server.MapPath("Meetings/" + ViewState["GRMtg_UID"].ToString() + ".pdf"));
                        }
                    }
                    if (ret > 0 && Gv_TaskForce_Right1.Rows.Count > 0)
                    {
                        for (int i = 0; i < Gv_TaskForce_Right1.Rows.Count; i++)
                        {
                            LinkButton Lbk = (LinkButton)Gv_TaskForce_Right1.Rows[i].Cells[0].FindControl("Lnk_RowNumber");
                            TextBox Txt = (TextBox)Gv_TaskForce_Right1.Rows[i].Cells[1].FindControl("Txt_Date");
                            DropDownList Status = (DropDownList)Gv_TaskForce_Right1.Rows[i].Cells[2].FindControl("ddlStatus");
                            DTask.Rows[i]["GRMtg_UID"] = ViewState["GRMtg_UID"].ToString();
                            DTask.Rows[i]["ActionPoint"] = Txt.Text;
                            DTask.Rows[i]["Status"] = Status.SelectedValue;
                        }

                        for (int i = 0; i < DTask.Rows.Count; i++)
                        {
                            GRMtgAction_UID = DTask.Rows[i]["GRMtgAction_UID"].ToString();
                            GRMtg_UID = DTask.Rows[i]["GRMtg_UID"].ToString();
                            ActionPoint = DTask.Rows[i]["ActionPoint"].ToString();
                            Sta = DTask.Rows[i]["Status"].ToString();
                            if (GRMtgAction_UID != "")
                            {
                                ret = Insert_TaskForceMeeting_Add_Update(GRMtgAction_UID, GRMtg_UID, ActionPoint, Sta, "U");


                            }
                            else
                            {
                                ret = Insert_TaskForceMeeting_Add_Update(GRMtgAction_UID, GRMtg_UID, ActionPoint, Sta, "I");
                            }
                        }
                        // row = INSERT_ImportDataSingle(DTask, "IU_IMPORT_tblGovtRelationsMtgAction", "tblGovtRelationsMtgAction", "I");
                        ViewState["CurrentTable1"] = DTask;
                    }
                    if (Convert.ToInt32(ret) == -1 || Convert.ToInt32(ret) == 1)
                    {
                        ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Save sucessfully')</script>", false);

                        ModalPopupExtender1.Hide();
                        FillDateSubGrid(ViewState["GR_UID"].ToString());
                    }

                }




            }
            else if (ViewState["Task"].ToString() == "I")
            {

                if (ViewState["GR_UID"] != null)
                {
                    DataTable DtSelect = objMain.LoadData("Select GRMtg_UID,GR_UID,Date,Minutes,MtgType,MinutesUpload From tblGovtRelationsMtg where GR_UID='" + ViewState["GR_UID"].ToString() + "'");
                    if (DtSelect.Rows.Count > 0)
                    {
                        DataTable DateMax = objMain.LoadData("Select MAX(Date) as Date from tblGovtRelationsMtg inner join tblGovtRelations on tblGovtRelations.GR_UID=tblGovtRelationsMtg.GR_UID where DistrictCode='" + ddl_District.SelectedValue + "'");
                        DateTime MaxDate = Convert.ToDateTime(DateMax.Rows[0]["Date"].ToString());
                         Date = Convert.ToDateTime(TxtDatePopup.Text).ToString("yyyy-MM-dd");
                        if (MaxDate > Convert.ToDateTime(Date))
                        {
                            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('New Meeting Date should be greater then Last meeting')</script>", false);

                            return;

                        }
                    }
                     Date = Convert.ToDateTime(TxtDatePopup.Text).ToString("yyyy-MM-dd");
                   
                    if (ViewState["GRMtg_UID"] != null)
                    {
                        ViewState["GRMtg_UID"] = 0;
                        
                        if (Mtg_Upload.HasFile)
                        {
                            ret = Insert_TaskForce_Add_Update(ViewState["GRMtg_UID"].ToString(), ViewState["GR_UID"].ToString(), Date, TxtMinutes.Text, ddlRBT.SelectedValue, true, "I");

                        }
                        else
                        {
                            ret = Insert_TaskForce_Add_Update(ViewState["GRMtg_UID"].ToString(), ViewState["GR_UID"].ToString(), Date, TxtMinutes.Text, ddlRBT.SelectedValue, false, "I");
                        }
                        ViewState["GRMtg_UID"] = ret.ToString();
                        if (ViewState["GRMtg_UID"] != null)
                        {
                            Mtg_Upload.SaveAs(Server.MapPath("Meetings/" + ret + ".pdf"));
                        }
                    
                       
                    }
                    else
                    {
                        ViewState["GRMtg_UID"] = 0;
                        if (Mtg_Upload.HasFile)
                        {
                            ret = Insert_TaskForce_Add_Update(ViewState["GRMtg_UID"].ToString(), ViewState["GR_UID"].ToString(), Date, TxtMinutes.Text, ddlRBT.SelectedValue, true, "I");

                        }
                        else
                        {
                            ret = Insert_TaskForce_Add_Update(ViewState["GRMtg_UID"].ToString(), ViewState["GR_UID"].ToString(), Date, TxtMinutes.Text, ddlRBT.SelectedValue, false, "I");
                        }
                        ViewState["GRMtg_UID"] = ret.ToString();
                        if (ViewState["GRMtg_UID"] != null)
                        {
                            Mtg_Upload.SaveAs(Server.MapPath("Meetings/" + ret + ".pdf"));
                        }
                    }

                    if (ret > 0 && Gv_TaskForce_Right1.Rows.Count > 0)
                    {
                        for (int i = 0; i < Gv_TaskForce_Right1.Rows.Count; i++)
                        {
                            LinkButton Lbk = (LinkButton)Gv_TaskForce_Right1.Rows[i].Cells[0].FindControl("Lnk_RowNumber");
                            TextBox Txt = (TextBox)Gv_TaskForce_Right1.Rows[i].Cells[1].FindControl("Txt_Date");
                            DropDownList Status = (DropDownList)Gv_TaskForce_Right1.Rows[i].Cells[2].FindControl("ddlStatus");
                            DTask.Rows[i]["GRMtg_UID"] = ret;
                            DTask.Rows[i]["ActionPoint"] = Txt.Text;
                            DTask.Rows[i]["Status"] = Status.SelectedValue;
                        }
                        for (int i = 0; i < DTask.Rows.Count; i++)
                        {
                            GRMtgAction_UID = DTask.Rows[i]["GRMtgAction_UID"].ToString();
                            GRMtg_UID = DTask.Rows[i]["GRMtg_UID"].ToString();
                            ActionPoint = DTask.Rows[i]["ActionPoint"].ToString();
                            Sta = DTask.Rows[i]["Status"].ToString();
                            if (GRMtgAction_UID != "")
                            {
                                ret = Insert_TaskForceMeeting_Add_Update(GRMtgAction_UID, GRMtg_UID, ActionPoint, Sta, "U");


                            }
                            else
                            {
                                ret = Insert_TaskForceMeeting_Add_Update(GRMtgAction_UID, GRMtg_UID, ActionPoint, Sta, "I");
                            }
                        }
                        // row = INSERT_ImportDataSingle(DTask, "[IU_IMPORT_tblGovtRelationsMtgAction]", "tblGovtRelationsMtgAction", "I");
                        ViewState["CurrentTable1"] = DTask;
                    }
                    if (Convert.ToInt32(ret) == -1 || Convert.ToInt32(ret) == 1)
                    {
                        ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Saved sucessfully')</script>", false);
                        ModalPopupExtender1.Hide();
                        FillDateSubGrid(ViewState["GR_UID"].ToString());
                    }


                }
            }

        }
        catch (Exception)
        {

            throw;
        }
    }
    public void btnDownload_Click(object sender, EventArgs e)
    {
        if (ViewState["GR_UID"] != null)
        {
            string filename = ViewState["GR_UID"].ToString();
            Response.ContentType = "application/octet-stream";
            Response.AppendHeader("Content-Disposition", "attachment;filename=" + filename + ".pdf");
            string aaa = Server.MapPath("~/Mou/" + filename + ".pdf");
            Response.TransmitFile(Server.MapPath("~/Mou/" + filename + ".pdf"));
            Response.End();
        }
    }
    public void Meeting_Download(object sender, EventArgs e)
    {
        if (ViewState["GRMtg_UID"] != null)
        {
            string filename = ViewState["GRMtg_UID"].ToString();
            Response.ContentType = "application/octet-stream";
            Response.AppendHeader("Content-Disposition", "attachment;filename=" + filename + ".pdf");
            string aaa = Server.MapPath("~/Meetings/" + filename + ".pdf");
            Response.TransmitFile(Server.MapPath("~/Meetings/" + filename + ".pdf"));
            Response.End();
        }
    }
    protected void Lnk_Click(object sender, EventArgs e)
    {
        try
        {
            GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
            for (int i = 0; i < GV_TaskForce_Left.Rows.Count; i++)
            {
                GridViewRow RowD = GV_TaskForce_Left.Rows[i];
                if (i % 2 == 0)
                {
                    RowD.BackColor = Color.White;

                }
                else
                    RowD.BackColor = Color.FromArgb(241, 241, 241); ;
                RowD.ForeColor = Color.FromArgb(51, 51, 51);
                ImageButton ImgBut = (RowD.FindControl("BtnImageEdit") as ImageButton);
                ImageButton ImgAcc = (RowD.FindControl("BTnImagDelete") as ImageButton);
                ImgAcc.Enabled = false;
                ImgBut.Enabled = false;

            }

            //gvRow.BackColor = Color.Black;
            //gvRow.Enabled = false;
            int index = gvRow.RowIndex;
            GridViewRow Row = GV_TaskForce_Left.Rows[index];
            Row.BackColor = Color.FromArgb(220, 211, 211);
            Row.ForeColor = Color.FromArgb(51, 51, 51);

            ImageButton ImgBut1 = (Row.FindControl("BtnImageEdit") as ImageButton);
            ImageButton ImgAcc1 = (Row.FindControl("BTnImagDelete") as ImageButton);
            //DropDownList ddlUserName = (Row1.FindControl("ddlUserName") as DropDownList);
            ImgAcc1.Enabled = true;
            // ddlUserName.Enabled = true;
            ImgBut1.Enabled = true;
            GridViewRow gvRow1 = (GridViewRow)(sender as Control).Parent.Parent;
            int index1 = gvRow1.RowIndex;
            string GRMtg_UID = GV_TaskForce_Left.DataKeys[index1]["GRMtg_UID"].ToString();
            DataTable DtTaskForce = objMain.LoadData("SELECT  [GRMtgAction_UID],[GRMtg_UID],[ActionPoint],[Status]   FROM [tblGovtRelationsMtgAction] where GRMtg_UID='" + GRMtg_UID + "'");
            DataTable DtTaskForce1 = objMain.LoadData("SELECT  [GRMtg_UID],[GR_UID],[Date],[Minutes] FROM [tblGovtRelationsMtg] where GRMtg_UID='" + GRMtg_UID + "'");
            if (DtTaskForce1.Rows.Count > 0)
            {
                TxtDatePopup.Text = DtTaskForce1.Rows[0]["Date"].ToString();
                TxtMinutes.Text = DtTaskForce1.Rows[0]["Minutes"].ToString();
                GV_TaskForce_Right.DataSource = DtTaskForce;
                GV_TaskForce_Right.DataBind();
                ViewState["Task"] = "U";
            }
        }
        catch (Exception)
        {

            throw;
        }
    }
    protected void BtnImageDelete_OnClick(object sender, EventArgs e)
    {
        try
        {
            if (txtconformmessageValue.Value == "Yes")
            {
                ViewState["CurrentTable1"] = null;
                GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
                int index = gvRow.RowIndex;
                //GRMtg_UID
                string GRMtg_UID = GV_TaskForce_Left.DataKeys[index]["GRMtg_UID"].ToString();
                SqlParameter[] para9 = new SqlParameter[] 
                { 
                new SqlParameter("@Condition",GRMtg_UID),
               
                };

                int DtTaskForce = SqlHelper.ExecuteNonQuery(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[EG_Delete_MOU]", para9);
                FillDateSubGrid(ViewState["GR_UID"].ToString());
            }


        }
        catch (Exception)
        {

            throw;
        }
    }
    protected void Btn_Delete_OnClick(object sender, EventArgs e)
    {
        try
        {
            if (txtconformmessageValue.Value == "Yes")
            {

                GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
                int index = gvRow.RowIndex;
                //GRMtg_UID
                string GRRep_UID = GV_Display.DataKeys[index]["GRRep_UID"].ToString();
                SqlParameter[] para9 = new SqlParameter[] 
                { 
                new SqlParameter("@Condition",GRRep_UID),
               
                };

                int DtTaskForce = SqlHelper.ExecuteNonQuery(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[EG_Delete_RelationsRep_MOU]", para9);
                FillDateSubGrid(ViewState["GR_UID"].ToString());
            }


        }
        catch (Exception)
        {

            throw;
        }
    }
    protected void Btn_Edit_OnClick(object sender, EventArgs e)
    {
        DataTable DtDist = ViewState["CurrentTable"] as DataTable;
        if (GV_Display.Rows.Count > 0)
        {
            GV_MOU.DataSource = ViewState["CurrentTable"] as DataTable;
            GV_MOU.DataBind();
        }
        else
        {

            FirstGridViewRow(1, 0);
        }

        ModalPopupExtender.Show();

    }
    protected void btn_Delete(object sender, EventArgs e)
    {
        if (txtconformmessageValue.Value == "Yes")
        {
            GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
            SetRowData(1);
            if (ViewState["CurrentTable"] != null)
            {
                DataTable dt = (DataTable)ViewState["CurrentTable"];
                DataRow drCurrentRow = null;
                int rowIndex = gvRow.RowIndex;
                if (dt.Rows.Count > 1)
                {
                    dt.Rows.Remove(dt.Rows[rowIndex]);
                    drCurrentRow = dt.NewRow();
                    ViewState["CurrentTable"] = dt;
                    GV_MOU.DataSource = dt;
                    GV_MOU.DataBind();
                    SetPreviousData(1);
                }
            }
        }
    }
    protected void BtnImagDel_Click(object sender, EventArgs e)
    {
        if (txtconformmessageValue.Value == "Yes")
        {
            GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
            SetRowData(3);
            if (ViewState["CurrentTable1"] != null)
            {
                DataTable dt = (DataTable)ViewState["CurrentTable1"];
                DataRow drCurrentRow = null;
                int rowIndex = gvRow.RowIndex;
                if (dt.Rows.Count > 1)
                {
                    dt.Rows.Remove(dt.Rows[rowIndex]);
                    drCurrentRow = dt.NewRow();
                    ViewState["CurrentTable1"] = dt;
                    Gv_TaskForce_Right1.DataSource = dt;
                    Gv_TaskForce_Right1.DataBind();
                    SetPreviousData(3);
                }
            }
        }
    }
    protected void BtnRightDel_Click(object sender, EventArgs e)
    {
        if (txtconformmessageValue.Value == "Yes")
        {
            GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
            int index = gvRow.RowIndex;
            string GRMtgAction_UID = GV_TaskForce_Right.DataKeys[index]["GRMtgAction_UID"].ToString();
            SqlParameter[] para9 = new SqlParameter[] 
                { 
                new SqlParameter("@Condition",GRMtgAction_UID),
               
                };

            int DtTaskForce = SqlHelper.ExecuteNonQuery(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[EG_Delete_MOU_Action]", para9);
            FillDateSubGrid(ViewState["GR_UID"].ToString());
        }

    }
    #endregion
    public void SaveData()
    {
        try
        {
            DataTable dt;
            int ID = 0;

            string StateCode = string.Empty, DistrictCode = string.Empty, StartDate = string.Empty, EndDate = string.Empty;
            DateTime EndDateChk1, EndDateChk2;

            if (ddl_State.SelectedIndex > 0)
            {
                StateCode = ddl_State.SelectedValue;
            }
            if (ddl_District.SelectedIndex > 0)
            {
                DistrictCode = ddl_District.SelectedValue;
            }
            if (txtStartDate.Text != "")
            {
                StartDate = Convert.ToDateTime(txtStartDate.Text).ToString("yyyy-MM-dd");
            }
            if (TxtEndDate.Text != "")
            {
                EndDate = Convert.ToDateTime(TxtEndDate.Text).ToString("yyyy-MM-dd");
            }
           
            if (ViewState["Save"].ToString() == "I" && ddl_State.SelectedIndex>0 && ddl_District.SelectedIndex>0)
            {
                if (FileUpload_Mou.HasFile)
                {
                    ID = Insert_Update_MOU(GR_UID, StateCode, DistrictCode, StartDate, EndDate, true, ViewState["Save"].ToString());
                }
                else
                {
                    ID = Insert_Update_MOU(GR_UID, StateCode, DistrictCode, StartDate, EndDate, false, ViewState["Save"].ToString());
                }
                ViewState["GR_UID"] = ID;
                if (ID > 0)
                {

                    FileUpload_Mou.SaveAs(Server.MapPath("Mou/" + ID + ".pdf"));
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Saved sucessfully')</script>", false);
                    ViewState["Save"] = "U";
                    BtnAddTasKforce.Visible = true;
                    FillDateSubGrid(ViewState["GR_UID"].ToString());
                    
                }


            }
            else if (ViewState["Save"].ToString() == "U" && ddl_State.SelectedIndex > 0 && ddl_District.SelectedIndex > 0)
            {
                string row = string.Empty;
                if (GV_Display.Rows.Count > 0)
                {
                    if (FileUpload_Mou.HasFile)
                    {
                        FileUpload_Mou.SaveAs(Server.MapPath("Mou/" + ViewState["GR_UID"].ToString() + ".pdf"));
                        ID = Insert_Update_MOU(ViewState["GR_UID"].ToString(), StateCode, DistrictCode, StartDate, EndDate, true, ViewState["Save"].ToString());
                    }
                    else
                    {
                        ID = Insert_Update_MOU(ViewState["GR_UID"].ToString(), StateCode, DistrictCode, StartDate, EndDate, false, ViewState["Save"].ToString());
                    }
                   
                    dt = (DataTable)ViewState["CurrentTable"];
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < GV_Display.Rows.Count; i++)
                        {
                            dt.Rows[i]["GR_UID"] = ViewState["GR_UID"].ToString();
                        }

                        //row = INSERT_ImportDataSingle(dt, "IU_IMPORT_tblGovtRelationsRep", "tblGovtRelationsRep", ViewState["Save"].ToString());
                    }

                    ViewState["CurrentTable"] = dt;


                }
                if (ID > 0)
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Saved sucessfully')</script>", false);
                    ViewState["Save"] = "U";
                    BtnAddparticipant.Visible = true;
                    FillDateSubGrid(ViewState["GR_UID"].ToString());
                }
            }
        }
        catch (Exception)
        {

            throw;
        }
    }
    #region Fill Master Data
    public void FillCBState()
    {
        conditions = "";
      //  objComman.BindDLL("mst1State", "StateCode,dbo.TitleCase(upper(StateName)) as StateName ", conditions, "StateName", "asc", ddlState, "StateName", "StateCode", "--Select--");
        objComman.BindDLL("mst1State", "StateCode,dbo.TitleCase(upper(StateName)) as StateName ", conditions, "StateName", "asc", ddl_State, "StateName", "StateCode", "--Select--");
    }
    public void FillCBDist()
    {
        conditions = "";
        if (Session["user_level_Role"].ToString() == "1")
        {

            conditions = "StateCode ='" + ddl_State.SelectedValue + "' and Fyear= '" + Session["FinYear"] + "'";

        }
        else
        {
            conditions = "StateCode ='" + ddl_State.SelectedValue + "' and DistrictCode=  '" + Session["NewDistrictCode"].ToString() + "' ";


        }

        objComman.BindDLL("mst2District", "DistrictCode,dbo.TitleCase(upper(DistrictName)) as DistrictName ", conditions, "DistrictName", "asc", ddl_District, "DistrictName", "DistrictCode", "--Select--");
     //   objComman.BindDLL("mst2District", "DistrictCode,dbo.TitleCase(upper(DistrictName)) as DistrictName ", conditions, "DistrictName", "asc", ddlDistrict, "DistrictName", "DistrictCode", "--Select--");



    }


    #endregion
    public void LoadUserLeavel()
    {
        conditions = "";
        if (Session["user_level_Role"].ToString() == "1")
        {
            //conditions = "StateCode='" + Session["StateCode"].ToString() + "' ";
            objComman.BindDLL("mst1State", "StateCode, dbo.TitleCase(upper(StateName)) as StateName", conditions, "StateName", "asc", ddl_State, "StateName", "StateCode", "--Select--");
          //  objComman.BindDLL("mst1State", "StateCode, dbo.TitleCase(upper(StateName)) as StateName", conditions, "StateName", "asc", ddlState, "StateName", "StateCode", "--Select--");

        }
        else
        {
            conditions = "StateCode='" + Session["StateCode"].ToString() + "' ";
            objComman.BindDLL("mst1State", "StateCode, dbo.TitleCase(upper(StateName)) as StateName", conditions, "StateName", "asc", ddl_State, "StateName", "StateCode", "--Select--");
          //  objComman.BindDLL("mst1State", "StateCode, dbo.TitleCase(upper(StateName)) as StateName", conditions, "StateName", "asc", ddlState, "StateName", "StateCode", "--Select--");

            ddl_State.SelectedIndex = 1;
            
        }


        if (Session["user_level_Role"].ToString() == "1")
        {
        }
        else
        {
            conditions = "";
            conditions = "StateCode ='" + ddl_State.SelectedValue + "' and DistrictCode ='" + Session["NewDistrictCode"].ToString() + "'";
            objComman.BindDLL("mst2District", "DistrictCode,dbo.TitleCase(upper(DistrictName)) as DistrictName", conditions, "DistrictName", "asc", ddl_District, "DistrictName", "DistrictCode", "--ALL--");
         //   objComman.BindDLL("mst2District", "DistrictCode,dbo.TitleCase(upper(DistrictName)) as DistrictName", conditions, "DistrictName", "asc", ddlDistrict, "DistrictName", "DistrictCode", "--Select--");

            ddl_District.SelectedIndex = 1;


        }
    }
    #region SelectedIndexChanged
    protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillCBDist();
    }
    #endregion
    #region Grid view Add row events
    private void AddNewRow(int Val, int Val1)
    {
        int rowIndex = 0;
        DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
        DataTable dtCurrentTable1 = (DataTable)ViewState["CurrentTable1"];
        //DataTable dtCurrentTable2 = (DataTable)ViewState["CurrentTable2"];


        DataRow drCurrentRow = null, drCurrentRow1 = null;
        //, drCurrentRow2 = null;
        if (Val == 1)
        {
            if (ViewState["CurrentTable"] != null)
            {


                #region  Add row in Grid1
                if (dtCurrentTable.Rows.Count > 0)
                {
                    for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
                    {
                        DropDownList FirstLevel = (DropDownList)GV_MOU.Rows[rowIndex].Cells[0].FindControl("ddlFirstLevel");
                        TextBox Designation = (TextBox)GV_MOU.Rows[rowIndex].Cells[1].FindControl("Txt_Designation");
                        TextBox Name = (TextBox)GV_MOU.Rows[rowIndex].Cells[2].FindControl("Txt_Name");
                        TextBox Phone_No = (TextBox)GV_MOU.Rows[rowIndex].Cells[3].FindControl("Txt_Phone_No");
                        TextBox Email = (TextBox)GV_MOU.Rows[rowIndex].Cells[4].FindControl("Txt_E_mail");
                        drCurrentRow = dtCurrentTable.NewRow();
                        // drCurrentRow["RowNumber"] = i + 1;

                        dtCurrentTable.Rows[i - 1]["Level"] = FirstLevel.SelectedValue;
                        dtCurrentTable.Rows[i - 1]["Designation"] = Designation.Text;
                        dtCurrentTable.Rows[i - 1]["Name"] = Name.Text;
                        dtCurrentTable.Rows[i - 1]["PhoneNo"] = Phone_No.Text;
                        dtCurrentTable.Rows[i - 1]["Email"] = Email.Text;


                        rowIndex++;
                    }
                    dtCurrentTable.Rows.Add(drCurrentRow);
                    ViewState["CurrentTable"] = dtCurrentTable;

                    GV_MOU.DataSource = dtCurrentTable;
                    GV_MOU.DataBind();
                }
                #endregion
            }
            else
            {
                Response.Write("ViewState is null");
            }
        }

        if (Val == 3)
        {
            if (ViewState["CurrentTable1"] != null)
            {
                #region Add row in Grid3
                if (dtCurrentTable1.Rows.Count > 0)
                {
                    for (int i = 1; i <= dtCurrentTable1.Rows.Count; i++)
                    {
                        LinkButton Lbk = (LinkButton)Gv_TaskForce_Right1.Rows[rowIndex].Cells[0].FindControl("Lnk_RowNumber");
                        TextBox Date = (TextBox)Gv_TaskForce_Right1.Rows[rowIndex].Cells[1].FindControl("Txt_Date");
                        DropDownList Status = (DropDownList)Gv_TaskForce_Right1.Rows[rowIndex].Cells[2].FindControl("ddlStatus");
                        drCurrentRow1 = dtCurrentTable1.NewRow();
                        //drCurrentRow["RowNumber"] = i + 1;
                        dtCurrentTable1.Rows[i - 1]["GRMtg_UID"] = Val1.ToString();
                        dtCurrentTable1.Rows[i - 1]["ActionPoint"] = Date.Text;
                        dtCurrentTable1.Rows[i - 1]["Status"] = Status.SelectedValue;

                        rowIndex++;
                    }
                    dtCurrentTable1.Rows.Add(drCurrentRow1);
                    ViewState["CurrentTable1"] = dtCurrentTable1;

                    Gv_TaskForce_Right1.DataSource = dtCurrentTable1;
                    Gv_TaskForce_Right1.DataBind();
                }
                #endregion
            }
            else
            {
                Response.Write("ViewState is null");
            }
        }


        SetPreviousData(Val);
    }
    private void SetPreviousData(int val)
    {
        int rowIndex = 0;

        if (val == 1)
        {
            if (ViewState["CurrentTable"] != null)
            {
                DataTable dt = (DataTable)ViewState["CurrentTable"];
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        DropDownList FirstLevel = (DropDownList)GV_MOU.Rows[rowIndex].Cells[0].FindControl("ddlFirstLevel");
                        TextBox Designation = (TextBox)GV_MOU.Rows[rowIndex].Cells[1].FindControl("Txt_Designation");
                        TextBox Name = (TextBox)GV_MOU.Rows[rowIndex].Cells[2].FindControl("Txt_Name");
                        TextBox Phone_No = (TextBox)GV_MOU.Rows[rowIndex].Cells[3].FindControl("Txt_Phone_No");
                        TextBox Email = (TextBox)GV_MOU.Rows[rowIndex].Cells[4].FindControl("Txt_E_mail");

                        FirstLevel.SelectedValue = dt.Rows[i]["Level"].ToString();
                        Designation.Text = dt.Rows[i]["Designation"].ToString();
                        Name.Text = dt.Rows[i]["Name"].ToString();
                        Phone_No.Text = dt.Rows[i]["PhoneNo"].ToString();
                        Email.Text = dt.Rows[i]["Email"].ToString();
                        rowIndex++;
                    }
                }
            }
        }

        if (val == 2)
        {
            if (ViewState["CurrentTable1"] != null)
            {

                DataTable dt1 = (DataTable)ViewState["CurrentTable1"];
                if (dt1.Rows.Count > 0)
                {
                    for (int i = 0; i < dt1.Rows.Count; i++)
                    {
                        LinkButton RowNumber = (LinkButton)GV_TaskForce_Left.Rows[rowIndex].Cells[0].FindControl("Lnk_RowNumber");
                        LinkButton Date = (LinkButton)GV_TaskForce_Left.Rows[rowIndex].Cells[1].FindControl("Txt_Date");
                        LinkButton Minutes = (LinkButton)GV_TaskForce_Left.Rows[rowIndex].Cells[2].FindControl("Txt_Minutes");
                        RowNumber.Text = dt1.Rows[i]["RowNumber"].ToString();
                        Date.Text = dt1.Rows[i]["Date"].ToString();
                        Minutes.Text = dt1.Rows[i]["Minutes"].ToString();
                        rowIndex++;
                    }
                }
            }
        }
        if (val == 3)
        {
            if (ViewState["CurrentTable1"] != null)
            {
                DataTable dt2 = (DataTable)ViewState["CurrentTable1"];
                if (dt2.Rows.Count > 0)
                {
                    for (int i = 0; i < dt2.Rows.Count; i++)
                    {
                        TextBox Action = (TextBox)Gv_TaskForce_Right1.Rows[rowIndex].Cells[1].FindControl("Txt_Date");
                        DropDownList Status = (DropDownList)Gv_TaskForce_Right1.Rows[rowIndex].Cells[2].FindControl("ddlStatus");
                        Action.Text = dt2.Rows[i]["ActionPoint"].ToString();
                        Status.Text = dt2.Rows[i]["Status"].ToString();
                        rowIndex++;
                    }
                }
            }
        }

    }
    private void FirstGridViewRow(int Val, int Rownumber)
    {
        if (Val == 1)
        {
            #region SubGrid
            DataTable dt = new DataTable();
            DataRow dr = null;
            dt.Columns.Add(new DataColumn("GRRep_UID", typeof(string)));
            dt.Columns.Add(new DataColumn("GR_UID", typeof(string)));
            dt.Columns.Add(new DataColumn("Level", typeof(Int32)));
            dt.Columns.Add(new DataColumn("Designation", typeof(string)));
            dt.Columns.Add(new DataColumn("Name", typeof(string)));
            dt.Columns.Add(new DataColumn("PhoneNo", typeof(string)));
            dt.Columns.Add(new DataColumn("Email", typeof(string)));

            dr = dt.NewRow();
            dr["GR_UID"] = string.Empty;
            dr["Level"] = 0;
            dr["Designation"] = string.Empty;
            dr["Name"] = string.Empty;
            dr["PhoneNo"] = string.Empty;
            dr["Email"] = string.Empty;
            dt.Rows.Add(dr);
            ViewState["CurrentTable"] = dt;
            GV_MOU.DataSource = dt;
            GV_MOU.DataBind();
            #endregion
        }
        if (Val == 2)
        {
            #region SubGrid1
            DataTable dt1 = new DataTable();
            DataRow dr1 = null;
            dt1.Columns.Add(new DataColumn("GRMtgAction_UID"));
            dt1.Columns.Add(new DataColumn("GRMtg_UID"));
            dt1.Columns.Add(new DataColumn("ActionPoint"));
            dt1.Columns.Add(new DataColumn("Status"));
            dr1 = dt1.NewRow();
            dr1["GRMtgAction_UID"] = string.Empty;
            dr1["GRMtg_UID"] = Rownumber;
            dr1["ActionPoint"] = string.Empty;
            dr1["Status"] = string.Empty;
            dt1.Rows.Add(dr1);
            ViewState["CurrentTable1"] = dt1;
            Gv_TaskForce_Right1.DataSource = dt1;
            Gv_TaskForce_Right1.DataBind();
            #endregion
        }
        if (Val == 3)
        {
            #region SubGrid2
            DataTable dt2 = new DataTable();
            DataRow dr2 = null;
            dt2.Columns.Add(new DataColumn("RowNumber", typeof(string)));
            dt2.Columns.Add(new DataColumn("Date", typeof(string)));
            dt2.Columns.Add(new DataColumn("Minutes", typeof(string)));
            dr2 = dt2.NewRow();
            dr2["RowNumber"] = string.Empty;
            dr2["Date"] = string.Empty;
            dr2["Minutes"] = string.Empty;
            dt2.Rows.Add(dr2);
            ViewState["CurrentTable2"] = dt2;
            Gv_TaskForce_Right1.DataSource = dt2;
            Gv_TaskForce_Right1.DataBind();
            #endregion
        }
    }
    public void DTF()
    {
        conditions = "";
        conditions = "LookupFlag ='DTF' ";
        objComman.BindDLL("mstlookup", "LookupCode, Description", conditions, "LookupCode", "asc", ddlRBT, "Description", "LookupCode", "Select");
    }
    protected void GV_MOU_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            DropDownList ddl = (DropDownList)e.Row.FindControl("ddlFirstLevel");
            HiddenField Hdn = (HiddenField)e.Row.FindControl("hdnFirstLevel");
            if (ddl != null)
            {
                conditions = "";
                conditions = "LookupFlag ='GOV' ";
                objComman.BindDLL("mstlookup", "LookupCode, Description", conditions, "LookupCode", "asc", ddl, "Description", "LookupCode", "Select");
            }

            ddl.SelectedValue = Hdn.Value;
        }
    }
    protected void GV_Display_OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            DropDownList ddl = (DropDownList)e.Row.FindControl("ddlFirstLevel");
            HiddenField Hdn = (HiddenField)e.Row.FindControl("hdnFirstLevel");
            if (ddl != null)
            {
                conditions = "";
                conditions = "LookupFlag ='GOV' ";
                objComman.BindDLL("mstlookup", "LookupCode, Description", conditions, "LookupCode", "asc", ddl, "Description", "LookupCode", "Select");
            }

            ddl.SelectedValue = Hdn.Value;
        }
    }
    protected void Gv_TaskForce_Right1_OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            DropDownList ddl = (DropDownList)e.Row.FindControl("ddlStatus");
            HiddenField Hdn = (HiddenField)e.Row.FindControl("hdnStatus");
            if (ddl != null)
            {
                conditions = "";
                conditions = "LookupFlag ='ST' ";
                objComman.BindDLL("mstlookup", "LookupCode, Description", conditions, "LookupCode", "asc", ddl, "Description", "LookupCode", "Select");
            }

            ddl.SelectedValue = Hdn.Value;
        }
    }
    protected void GV_TaskForce_Left_OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            ImageButton ImgBut = (ImageButton)e.Row.FindControl("BtnImageEdit");
            ImageButton ImgAcc = (ImageButton)e.Row.FindControl("BTnImagDelete");
            ImgAcc.Enabled = false;
            ImgBut.Enabled = false;

        }
    }
    private void SetRowData(int Bal)
    {
        if (Bal == 1)
        {
            int rowIndex = 0;

            if (ViewState["CurrentTable"] != null)
            {
                DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
                DataRow drCurrentRow = null;
                if (dtCurrentTable.Rows.Count > 0)
                {
                    for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
                    {

                        DropDownList FirstLevel = (DropDownList)GV_MOU.Rows[rowIndex].Cells[0].FindControl("ddlFirstLevel");
                        TextBox Designation = (TextBox)GV_MOU.Rows[rowIndex].Cells[1].FindControl("Txt_Designation");
                        TextBox Name = (TextBox)GV_MOU.Rows[rowIndex].Cells[2].FindControl("Txt_Name");
                        TextBox Phone_No = (TextBox)GV_MOU.Rows[rowIndex].Cells[3].FindControl("Txt_Phone_No");
                        TextBox Email = (TextBox)GV_MOU.Rows[rowIndex].Cells[4].FindControl("Txt_E_mail");

                        drCurrentRow = dtCurrentTable.NewRow();
                        dtCurrentTable.Rows[i - 1]["Level"] = FirstLevel.SelectedValue;
                        dtCurrentTable.Rows[i - 1]["Designation"] = Designation.Text;
                        dtCurrentTable.Rows[i - 1]["Name"] = Name.Text;
                        dtCurrentTable.Rows[i - 1]["PhoneNo"] = Phone_No.Text;
                        dtCurrentTable.Rows[i - 1]["Email"] = Email.Text;
                        rowIndex++;
                    }

                    ViewState["CurrentTable"] = dtCurrentTable;
                    //grvStudentDetails.DataSource = dtCurrentTable;
                    //grvStudentDetails.DataBind();
                }
            }
            else
            {
                Response.Write("ViewState is null");
            }
        }
        if (Bal == 3)
        {
            int rowIndex = 0;

            if (ViewState["CurrentTable1"] != null)
            {
                DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable1"];
                DataRow drCurrentRow = null;
                if (dtCurrentTable.Rows.Count > 0)
                {
                    for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
                    {
                        TextBox Action = (TextBox)Gv_TaskForce_Right1.Rows[rowIndex].Cells[1].FindControl("Txt_Date");
                        DropDownList Status = (DropDownList)Gv_TaskForce_Right1.Rows[rowIndex].Cells[0].FindControl("ddlStatus");



                        drCurrentRow = dtCurrentTable.NewRow();
                        dtCurrentTable.Rows[i - 1]["ActionPoint"] = Action.Text;
                        dtCurrentTable.Rows[i - 1]["Status"] = Status.SelectedValue;

                        rowIndex++;
                    }

                    ViewState["CurrentTable1"] = dtCurrentTable;

                }
            }
            else
            {
                Response.Write("ViewState is null");
            }
        }
        //SetPreviousData();
    }
    #endregion
    private void FillDateSubGrid(string GR_UID)
    {
        DataSet dtSubGridData = null;
        string Cond = "where GR_UID='" + GR_UID + "'";
        int Flag = 0;
        SqlParameter[] pa = new SqlParameter[]
        {
             new SqlParameter("@Cond",Cond),             
             new SqlParameter("@Flag",Flag),
        };
        dtSubGridData = SqlHelper.GetDataSet(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "EG_Fill_SubGrid_Data_MOU", pa);
        DataTable DataSub = dtSubGridData.Tables[0];
        DataTable DataSub1 = dtSubGridData.Tables[1];
        DataTable DataSub2 = dtSubGridData.Tables[2];
        ViewState["CurrentTable"] = DataSub;
        ViewState["GR_UID"] = GR_UID;
        if (DataSub2.Rows.Count > 0)
        {
            txtStartDate.Text = DataSub2.Rows[0]["StartDate"].ToString();
            TxtEndDate.Text = DataSub2.Rows[0]["EndDate"].ToString();
        }
        if (DataSub.Rows.Count > 0)
        {
            //ddlState.SelectedValue = DataSub2.Rows[0]["StateCode"].ToString();
            //ddlDistrict.SelectedValue = DataSub2.Rows[0]["DistrictCode"].ToString();
            
            if (DataSub2.Rows[0]["MoU"].ToString() == "True")
            {
                lnkUpload.Visible = true;
            }
            else
            {
                lnkUpload.Visible = false;
            }
            GV_Display.DataSource = DataSub;
            GV_Display.DataBind();
            BtnAddparticipant.Visible = true;
            BtnAddTasKforce.Visible = true;
            // SetRowData();
        }
        else
        {
            GV_Display.DataSource = null;
            GV_Display.DataBind();
            BtnAddparticipant.Visible = true;
            BtnAddTasKforce.Visible = false;
        }
        if (DataSub1.Rows.Count > 0)
        {
            GV_TaskForce_Left.DataSource = DataSub1;
            GV_TaskForce_Left.DataBind();

            DataTable DtTaskForce = objMain.LoadData("SELECT  [GRMtgAction_UID],[GRMtg_UID],[ActionPoint],[Status]   FROM [tblGovtRelationsMtgAction] where GRMtg_UID='" + DataSub1.Rows[0]["GRMtg_UID"].ToString() + "'");
            if (DtTaskForce.Rows.Count > 0)
            {
                GV_TaskForce_Right.DataSource = DtTaskForce;
                GV_TaskForce_Right.DataBind();

            }

        }
        else
        {
            GV_TaskForce_Left.DataSource = null;
            GV_TaskForce_Left.DataBind();
            GV_TaskForce_Right.DataSource = null;
            GV_TaskForce_Right.DataBind();
        }
    }
    #region Insert Update
    public string INSERT_ImportDataSingle(DataTable dt, string strSP_Name, string strParentTable_Name, string Flag)
    {
        string getresult = "";
        string R_Import = string.Empty;
        string strtemptblmstGroupChk = "IF OBJECT_ID('tempdb.#temp_" + strParentTable_Name + "') IS NOT NULL DROP TABLE #temp_" + strParentTable_Name + "";
        string strtemptblmstGroup = string.Empty;
        SqlConnection ConStr = new SqlConnection();
        ConStr = new SqlConnection(SqlHelper.mainConnectionString);
        if (strParentTable_Name == "tblGovtRelationsRep")
        {
            strtemptblmstGroup = "";
            strtemptblmstGroup = "Select [GRRep_UID],[GR_UID],[Level],[Designation],[Name],[ContactNo],[Email]";
            strtemptblmstGroup += " INTO #temp_" + strParentTable_Name + " FROM " + strParentTable_Name + " ";
            strtemptblmstGroup += " where [tblGovtRelationsRep].[GRRep_UID] is null ";
        }
        if (strParentTable_Name == "tblGovtRelationsMtgAction")
        {

            // strtemptblmstGroup = "Select [GRMtgAction_UID],[GRMtg_UID] ,[ActionPoint],[Status]  INTO #temp_tblGovtRelationsMtgAction from tblGovtRelationsMtgAction where tblGovtRelationsMtgAction.[GRMtgAction_UID] is null";
            strtemptblmstGroup = "Select [GRMtgAction_UID],[GRMtg_UID] ,[ActionPoint],[Status]";
            strtemptblmstGroup += " INTO #temp_" + strParentTable_Name + " FROM " + strParentTable_Name + " ";
            strtemptblmstGroup += " where [tblGovtRelationsMtgAction].[GRMtgAction_UID] is null ";

        }

        //SqlCommand cmd = new SqlCommand();
        //cmd.Connection = ConStr;
        //if (cmd.Connection.State.ToString()=="Closed")
        //{
        //    cmd.Connection.Open();        
        //}
        ////cmd.Connection.Open();
        //cmd.CommandText = "CREATE TABLE #temp_tblGovtRelationsMtgAction([GRMtgAction_UID] [int] NOT NULL,[GRMtg_UID] [int] NULL,[ActionPoint] [varchar](50) NULL,[Status] [int] NULL)";
        //cmd.ExecuteNonQuery();
        //using (SqlBulkCopy bc = new SqlBulkCopy(ConStr))
        //{
        //    bc.BulkCopyTimeout = 3000000;
        //    bc.DestinationTableName = "#temp_tblGovtRelationsMtgAction";
        //    bc.WriteToServer(dt);
        //    bc.Close();       
        //}
        //cmd.CommandTimeout = 30000;
        //cmd.CommandType = CommandType.StoredProcedure; //"update g set g.ActionPoint=t.ActionPoint,g.Status=t.Status from  tblGovtRelationsMtgAction g inner join #temp_tblGovtRelationsMtgAction t on t.GRMtgAction_UID=g.GRMtgAction_UID and t.GRMtg_UID=g.GRMtg_UID";
        //cmd.CommandText = "tt";
        //int pp= cmd.ExecuteNonQuery();
        //cmd.Connection.Close();

        getresult = objComman.INSERT_ImportDataSingleSP(dt, strSP_Name, strParentTable_Name, strtemptblmstGroupChk, strtemptblmstGroup, Flag, ConStr);
        return getresult;
    }
    public int Insert_Update_MOU(string GR_UID, string StateCode, string DistrictCode, string StartDate, string EndDate, bool MOU, string Flag)
    {
        int iReturnValue = 0;
        try
        {
            iReturnValue = objComman.Insert_Update_MOU(GR_UID, StateCode, DistrictCode, StartDate, EndDate, MOU, Flag);
        }
        catch (Exception exp)
        {

        }
        return iReturnValue;
    }
    public int Insert_TaskForce_Add_Update(string GRMtg_UID, string GR_UID, string Date, string Minutes, string MtgType, bool MinutesUpload, string Flag)
    {
        int iReturnValue = 0;
        try
        {
            iReturnValue = objComman.Insert_TaskForce_Add_Update(GRMtg_UID, GR_UID, Date, Minutes,MtgType,MinutesUpload, Flag);
        }
        catch (Exception)
        {

            throw;
        }
        return iReturnValue;
    }
    public int Insert_TaskForceMeeting_Add_Update(string GRMtgAction_UID, string GRMtg_UID, string ActionPoint, string Status, string Flag)
    {
        int iReturnValue = 0;
        try
        {
            iReturnValue = objComman.Insert_Meeting_Add_Update(GRMtgAction_UID, GRMtg_UID, ActionPoint, Status, Flag);
        }
        catch (Exception)
        {

            throw;
        }
        return iReturnValue;
    }


    public int Insert_GovtRep_Add_Update(string GRRep_UID, string GR_UID, string Level, string Desig, string Name1, string Phone, string E_mail, string Flag)
    {
        int iReturnValue = 0;
        try
        {
            iReturnValue = objComman.Insert_GovtRep_Add_Update(GRRep_UID, GR_UID, Level, Desig, Name1, Phone, E_mail, Flag);
        }
        catch (Exception)
        {

            throw;
        }
        return iReturnValue;
    }
    #endregion
}

