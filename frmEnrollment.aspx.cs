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
public partial class frmEnrollment : System.Web.UI.Page
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
                LoadYear();
                LoadUserLeavel();
               
                
                ViewState["1"] = "ss";
                UserLevelFilter();
            }
            else
            {
                Response.Redirect("Login.aspx", false);

            }

        }
    }

    public void UserLevelFilter()
    {

        string strQry = "";
        string Cond = "Module='Enroll'";
        strQry = "Select * from MstUserRight  where " + Cond + " and Role_Id=" + Session["user_level"].ToString() + "   ";


        DataTable dtRole = objMain.LoadData(strQry);

        if (dtRole.Rows.Count > 0)
        {
            vADD = Convert.ToBoolean(dtRole.Rows[0]["AddStatus"].ToString());
            vVerify = Convert.ToBoolean(dtRole.Rows[0]["verify_Status"].ToString());
            vDelete = Convert.ToBoolean(dtRole.Rows[0]["Delete_status"].ToString());
            edit_status = Convert.ToBoolean(dtRole.Rows[0]["edit_status"].ToString());
            ViewState["vADD"] = vADD;
            ViewState["vVerify"] = vVerify;
            ViewState["vDelete"] = vDelete;
            ViewState["edit_status"] = edit_status;
        }
       
        //if (vDelete == true)
        //{

        //    btnDelete.Visible = true;
        //}
        //else
        //{

        //    btnDelete.Visible = false;
        //}

        if (vADD == true)
        {
            btnMain.Enabled = true;
          
        }
        else
        {
            btnMain.Enabled = false;

        }
        if (vVerify == true)
        {

           

        }
        if (vVerify == true || vADD == true)
        {
            btnMain.Enabled = true;

        }
        else
        {
            btnMain.Enabled = false;

        }

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
        Locking();
        LockingEdit();
    }

    public void Locking()
    {
        if (ddlYear.SelectedIndex > 0)
        {
            gvnroll.Columns[0].Visible = true;
            gvnroll.Columns[1].Visible = true;
            btnMain.Enabled = true;

          
                string strQry;
                strQry = "Select * from mstModuleLocking  where [FromName]='Enroll' and DistrictCode='" + ddlDistrict.SelectedValue + "' and Fyear='" + ddlYear.SelectedItem.Text + "'";

              
                DataTable dtModel = objMain.LoadData(strQry);
                if (dtModel.Rows.Count > 0)
                {


                    DateTime date1 = Convert.ToDateTime(dtModel.Rows[0]["lockdate"].ToString());
                    DateTime date2 = DateTime.Now.Date;





                    if (date1 < date2)
                    {
                     
                        btnMain.Enabled = false;

                        gvnroll.Columns[0].Visible = false;
                        gvnroll.Columns[1].Visible = false;

                    }

                }

            }

        
    }
    public void LockingEdit()
    {
        if (ddlYear.SelectedIndex > 0)
        {

       
            string strQry;

            strQry = "Select * from mstModuleLocking  where [FromName]='EnrollmentEditDelete' and DistrictCode='" + ddlDistrict.SelectedValue + "' and Fyear='" + ddlYear.SelectedItem.Text + "' ";


            //string Year = ddlYear.SelectedItem.Text;
            //string[] Year1 = Year.Split('-');

            //DateTime date1;
            //DateTime date2;
            //DataTable dtModel = objMain.LoadData(strQry);

            //date1 = Convert.ToDateTime(dtModel.Rows[0]["lockdate"].ToString());
            //Session["EDITLOCK"] = Convert.ToDateTime(dtModel.Rows[0]["lockdate"].ToString());


            //Int32 Ik = Convert.ToInt32(dtModel.Rows[0]["LockMonth"].ToString());
            //if (DateTime.Today.Month == 1  || DateTime.Today.Month == 3)
            //{
            //    date1 = new DateTime(Convert.ToInt32(Year1[1]), DateTime.Today.Month, 30, 0, 0, 0);
            //    date2 = new DateTime(Convert.ToInt32(Year1[1]), Ik, 30, 0, 0, 0);
            //}
            //if (DateTime.Today.Month == 2 )
            //{
            //    date1 = new DateTime(Convert.ToInt32(Year1[1]), DateTime.Today.Month, 29, 0, 0, 0);
            //    date2 = new DateTime(Convert.ToInt32(Year1[1]), Ik, 29, 0, 0, 0);
            //}
            //else
            //{
            //    date1 = new DateTime(Convert.ToInt32(Year1[0]), DateTime.Today.Month, 30, 0, 0, 0);
            //    date2 = new DateTime(Convert.ToInt32(Year1[0]), Ik, 30, 0, 0, 0);
            //}
           
            //decimal result = DateTime.Compare(date1, date2);
            //if (Math.Abs(result) > 0)
            //{
            //    ViewState["EDITDelete"] = false;
            //}
            //else
            //{
            //    ViewState["EDITDelete"] = true;
            //}



        }
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
        //            //get last  two digits (eg: 10 from 2010);

        //            dr = dtYear.NewRow();
        //            dr["Type"] = GivenYear - 2 + "-" + Convert.ToString((GivenYear - 2 + 1));
        //            dr["ID"] = y - 2;
        //            dtYear.Rows.Add(dr);
        //        }
        //        else
        //        {
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
        DataTable dtYear = objComman.Generate_Financial_Year();
        objComman.BindDLLMasterTable("mstSchool", "Type,ID", dtYear, conditions, "Type", "asc", ddlYear, "Type", "ID", "Select");

        ddlYear.SelectedIndex = 1;
        //}


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
            conditions = "StateCode ='" + ddlState.SelectedValue + "'  and Fyear= '" + ddlYear.SelectedItem.Text + "'  ";
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
        Locking();
        LockingEdit();
        FillCBBock();
    }
    protected void ddlBlock_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillCBCluster();
        Locking();
        LockingEdit();
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
        string strQry = "Select SchoolCode,Name from mstSchool  where VillageCode ='" + ddlVillage.SelectedValue + "'  and FYear ='" + ddlYear.SelectedItem.Text + "'  union Select SchoolCode,Name from mstSchool  where VillageCode ='" + ddlVillage.SelectedValue + "' ";

        DataTable dtSchool = objMain.LoadData(strQry);

        objComman.BindDLLMasterTable("mstSchool", "SchoolCode,Name", dtSchool, conditions, "Name", "asc", ddlSchool, "Name", "SchoolCode", "Select");
      

        

        //conditions = "";
        //conditions = "VillageCode ='" + ddlVillage.SelectedValue + "'  and FYear ='" + ddlYear.SelectedItem.Text + "'";
        //objComman.BindDLL("mstSchool", "SchoolCode,Name", conditions, "Name", "asc", ddlSchool, "Name", "SchoolCode", "Select");


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

    protected void gvnroll_OnRowCommand(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //Label lblUniqueChildCode = (Label)e.Row.FindControl("lblUniqueChildCode");

            ImageButton lbtn = (ImageButton)e.Row.FindControl("ImgAcc");
            lbtn.Attributes.Add("onclick", "javascript:return " + "confirm('Are you sure you want to Delete? ')");
            Label lblCreatedate = (Label)e.Row.FindControl("lblCreatedate");
            
            LinkButton LnkBtnBlock_OnClick = (LinkButton)e.Row.FindControl("lbtn");
             DateTime date1;
                 DateTime date2;
            date1=Convert.ToDateTime(lblCreatedate.Text);
            date2 = Convert.ToDateTime(Session["EDITLOCK"]);


            if (date1 < date2)
                {
                    LnkBtnBlock_OnClick.Enabled = false;
                }
                else
                {
                      LnkBtnBlock_OnClick.Enabled = true;
                }
            
            if (Session["user_level"].ToString() == "39")
            {

                if (date1 < date2)
                
                {
                lbtn.Enabled = false;
                }
                else
                {
                      lbtn.Enabled = true;
                }
            }
            else
            {
                lbtn.Enabled = false;
            }
            // Label lblStatus = (Label)e.Row.FindControl("lblStatus");
            
            //string schoolcode = lblUniqueChildCode.Text;
            //Session["UnquieId"] = lblUniqueChildCode.Text;
            //Session["StateCode"] = ddlState.SelectedValue;
            //Session["DistCode"] = ddlDistrict.SelectedValue;
            //Session["BlockCode"] = ddlBlock.SelectedValue;
            //Session["PhanyCode"] = ddlPanchayat.SelectedValue;
            //Session["VillCode"] = ddlVillage.SelectedValue;
            //Session["Schoolid"] = ddlSchool.SelectedValue;
            //if (ddlSchool.SelectedIndex > 0)
            //{
            //    Session["SchoolName"] = ddlSchool.SelectedItem.Text;
            //}
            //if (ddlPanchayat.SelectedIndex > 0)
            //{
            //    Session["PhanyName"] = ddlPanchayat.SelectedItem.Text;
            //}
            //if (ddlVillage.SelectedIndex > 0)
            //{
            //    Session["Villageame"] = ddlVillage.SelectedItem.Text;
            //}
            //Session["mYear"] = ddlYear.SelectedValue;
            //Session["EnStatus"] = lblStatus.Text;
            //   string   strURL = "frmD2dEnrollment.aspx?CommandArgument=" & CType(e.Row.FindControl("lblpksdMatItemID"), Label).Text & "," & CType(e.Row.FindControl("lblAOrder"), Label).Text & "," & CType(e.Row.FindControl("lblArticleNo"), Label).Text
            //string strURL = "frmAddEnrollment.aspx";
            //   lbtn.Attributes.Add("onclick", "window.open('" + strURL + "', 'name', 'width=1000,height=500,left=700,top=400,scrollbars=1,resizable=yes');");

          //  Puppop();
       


        }


    }

    public void Puppop()
    {
        string url = "frmAddEnrollment.aspx";

        string s = "window.open('" + url + "', 'popup_window', 'width=800,height=500,left=700,top=400,scrollbars=1,resizable=yes');";
        ClientScript.RegisterStartupScript(this.GetType(), "script", s, true);

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
        conditions = " mst5Village.StateCode='" + ddlState.SelectedValue.ToString() + "'";

        if ( ddlDistrict.SelectedIndex > 0)
        {
            conditions = conditions + "and mst5Village.DistrictCode='" + ddlDistrict.SelectedValue.ToString() + "'";
        }

        if ( ddlBlock.SelectedIndex > 0)
        {
          
                conditions = conditions + "and mst5Village.BlockCode='" + ddlBlock.SelectedValue.ToString() + "'";
            
        }



        if ( ddlPanchayat.SelectedIndex > 1)
        {
            conditions = conditions + "and mst5Village.PanchayatCode='" + ddlPanchayat.SelectedValue.ToString() + "'";
        }

      
           conditions = conditions + "and mst5Village.VillageCode='" + ddlVillage.SelectedValue.ToString() + "'";
       

        if (ddlSchool.SelectedValue != null && ddlSchool.SelectedIndex > 0)
        {
            conditions = conditions +  "and tblEnrolment.SchoolCode='" + ddlSchool.SelectedValue.ToString() + "'";
        }


        //strQry += "  SELECT mst2District.DistrictName ,mst3Block.BlockName ,mstPanchayat.PanchayatName ,mst5Village.VillageName ,tblEnrolment.SchoolCode, tblEnrolment.[UniqueChildCode], D2DCode as Uniqueid,case Gender when 1 then 'Male' else 'Female' end as Gender , tblEnrolment.Serial as  Serial,convert(varchar, tblEnrolment.[EnrolmentDate],103) as EnrolmentDate, aged.Description as Class, tblEnrolment.AsOnDate,  tblEnrolment.[HouseNo] as HHNo1, tblEnrolment.[Category], [ChildName] AS ChildName, [FatherName] AS FathersName, tblEnrolment.[Gender], tblEnrolment.[DOBAvailable], convert(varchar, tblEnrolment.[DOB],103) as DOB, tblEnrolment.[AgeAson] as Age, ES.Description AS SocialCategory, mstSchool.Name as School,  EC.Description as EnrolmentCategory, mst5Village.PanchayatCode, mst5Village.BlockCode, mst5Village.DistrictCode, ES1.Description as EduationStatus ,tblEnrolment.SysFlag,tblEnrolment.Status ";
        //strQry += "    FROM tblEnrolment ";
        //strQry += "    INNER JOIN mst5Village ON mst5Village.VillageCode = tblEnrolment.VillageCode ";
        //strQry += "      LEFT JOIN mst3Block ON mst5Village.BlockCode = mst3Block.BlockCode";
        //strQry += " LEFT JOIN mstPanchayat ON mst5Village.PanchayatCode = mstPanchayat.PanchayatCode";
        //strQry += " LEFT JOIN mst2District ON mst5Village.DistrictCode = mst2District.DistrictCode ";

        //strQry += "  LEFT JOIN mstSchool ON tblEnrolment.SchoolCode = mstSchool.SchoolCode  LEFT JOIN mstLookup aged on aged.LookupCode=Class and aged.LookupFlag='ECL'";

        //strQry += "	LEFT JOIN mstLookup ES on ES.LookupCode=Category and ES.LookupFlag='CAT'	  LEFT JOIN mstLookup EC on EC.LookupCode=EnrollCategory and EC.LookupFlag='EC'	      LEFT JOIN mstLookup ES1 on ES1.LookupCode=TYPE and ES1.LookupFlag='ES' ";
        //strQry += "	 where " + conditions + "  and mst5Village.VillageCode='" + ddlVillage.SelectedValue.ToString() + "' and DeleteFlag=1 order by D2DCode  ";

        //DataTable dt1 = objMain.LoadData(strQry);

        SqlParameter[] parm1 = new SqlParameter[]
            {
         
               new SqlParameter("@Con",  conditions),
                 new SqlParameter("@Flag",  1),
            };


        DataTable dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[LoadOnlineEnrollment]", parm1);



                if (dt.Rows.Count > 0)
                {
                    gvnroll.DataSource = dt;
                    gvnroll.DataBind();
                }
                else
                {
                    gvnroll.DataSource = null;
                    gvnroll.DataBind();
                }
        }
    protected void btnSerach_Click(object sender, EventArgs e)
    {
        LoadData();
    }
    protected void btnMain_Click(object sender, EventArgs e)
    {
        if (ddlVillage.SelectedIndex <= 0)
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Select Village')</script>", false);


            this.ddlVillage.Focus();
            return;
        }
        if (ddlSchool.SelectedIndex <= 0)
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Select School')</script>", false);


            this.ddlSchool.Focus();
            return;
        }
        Session["StateCode"] = ddlState.SelectedValue;
        Session["DistCode"] = ddlDistrict.SelectedValue;
        Session["BlockCode"] = ddlBlock.SelectedValue;
        Session["PhanyCode"] = ddlPanchayat.SelectedValue;
        Session["VillCode"] = ddlVillage.SelectedValue;
        Session["Schoolid"] = ddlSchool.SelectedValue;
        Session["SchoolName"] = ddlSchool.SelectedItem.Text;
        Session["PhanyName"] = ddlPanchayat.SelectedItem.Text;
        Session["Villageame"] = ddlVillage.SelectedItem.Text;
        Session["mYear"] = ddlYear.SelectedValue;
        Session["FYear"] = ddlYear.SelectedItem.Text;
        string strQry = "select ManagementType,WorkingStatus,SchoolLevel,SchoolCodeID from mstSchool where SchoolCode='" + ddlSchool.SelectedValue + "'   ";


        DataTable dtMangment = objMain.LoadData(strQry);

        if (dtMangment.Rows.Count > 0)
        {
            Session["ManagementType"] = dtMangment.Rows[0]["ManagementType"].ToString();
            Session["SchoolLevel"] = dtMangment.Rows[0]["SchoolLevel"].ToString();
            Session["WorkingStatus"] = dtMangment.Rows[0]["WorkingStatus"].ToString();
            Session["SchoolCodeID"] = dtMangment.Rows[0]["SchoolCodeID"].ToString();

        }

                 string url = "frmD2dEnrollment.aspx";

                 string s = "window.open('" + url + "', 'popup_window', 'width=1300,height=500,left=700,top=400,scrollbars=1,resizable=yes');";
            ClientScript.RegisterStartupScript(this.GetType(), "script", s, true);

    }

    protected void LnkBtnBlock_OnClick(object sender, EventArgs e)
    {
        LinkButton bt = (LinkButton)sender;

        GridViewRow gvr = (GridViewRow)bt.NamingContainer;

        string UniqueChildCode = (gvr.FindControl("lblCUniqueChildCode") as Label).Text;
        Label lblStatus = (Label)gvr.FindControl("lblStatus");
        Label lblSchool = (Label)gvr.FindControl("lblSchool");
        Label lblSchoolID = (Label)gvr.FindControl("lblSchoolID");

        
        Session["UnquieId"] = UniqueChildCode;
        Session["StateCode"] = ddlState.SelectedValue;
        Session["DistCode"] = ddlDistrict.SelectedValue;
        Session["BlockCode"] = ddlBlock.SelectedValue;
        Session["PhanyCode"] = ddlPanchayat.SelectedValue;
        Session["VillCode"] = ddlVillage.SelectedValue;
        Session["Schoolid"] = lblSchoolID.Text;
      
            Session["SchoolName"] = lblSchool.Text;
       
        if (ddlPanchayat.SelectedIndex > 0)
        {
            Session["PhanyName"] = ddlPanchayat.SelectedItem.Text;
        }
        if (ddlVillage.SelectedIndex > 0)
        {
            Session["Villageame"] = ddlVillage.SelectedItem.Text;
        }
        Session["mYear"] = ddlYear.SelectedValue;
        Session["EnStatus"] = lblStatus.Text;

        string strQry = "select ManagementType,WorkingStatus,SchoolLevel,SchoolCodeID from mstSchool where SchoolCode='" + ddlSchool.SelectedValue + "'   ";


        DataTable dtMangment = objMain.LoadData(strQry);

        if (dtMangment.Rows.Count > 0)
        {
            Session["ManagementType"] = dtMangment.Rows[0]["ManagementType"].ToString();
            Session["SchoolLevel"] = dtMangment.Rows[0]["SchoolLevel"].ToString();
            Session["WorkingStatus"] = dtMangment.Rows[0]["WorkingStatus"].ToString();
            Session["SchoolCodeID"] = dtMangment.Rows[0]["SchoolCodeID"].ToString();

        }



       // Session["UnquieId"] = UniqueChildCode;
        string url = "frmAddEnrollment.aspx";

        string s = "window.open('" + url + "', 'popup_window', 'width=800,height=650,left=500,top=500,scrollbars=1,resizable=yes');";
        ClientScript.RegisterStartupScript(this.GetType(), "script", s, true);


    }
    protected void btn_Delete_Click(object sender, EventArgs e)
    {
        ImageButton bt = (ImageButton)sender;

        GridViewRow gvr = (GridViewRow)bt.NamingContainer;
                
       string    UniqueChildCode = (gvr.FindControl("lblCUniqueChildCode") as Label).Text;
       string lblStatus = (gvr.FindControl("lblStatus") as Label).Text;
       string lblD2dChildCode = (gvr.FindControl("lblD2dChildCode") as Label).Text;
        
         string strQry = "";
         //strQry = "  SELECT EnrollCode from tblDTD where EnrollCode ='" + UniqueChildCode + "' ";
         //DataTable dt = objMain.LoadData(strQry);
         //if (dt.Rows.Count > 0)
         //{
         //    bt.Attributes.Add("onclick", "javascript:return " + "confirm(' Enrollment link in D2D Please confirm if you want to Deleted?  ')");
         //}
         //if (dt.Rows.Count > 0)
         //{
         //    ScriptManager.RegisterStartupScript(this, GetType(), "importingdone", "alert('You can not  Deleted because Enrollment link in D2D');", true);

         //   // bt.Attributes.Add("onclick", "javascript:return " + "confirm(' Enrollment link in D2D Please confirm if you want to Deleted?  ')");

         //    //int res1 = objMain.DeleteEnrollMentData(UniqueChildCode, "D");

         //    //if (res1 > 0)
         //    //{
         //    //    LoadData();
         //    //    ScriptManager.RegisterStartupScript(this, GetType(), "importingdone", "alert('Record Deleted');", true);

         //    //}
         //}
         //else
         //{
         if (lblStatus == "2")
         {

             strQry = "  SELECT D2dChildCode from tblEnrolment with(nolock) where D2dChildCode ='" + lblD2dChildCode + "' and DeleteFlag<>2 ";
             DataTable dted = objMain.LoadData(strQry);
             if (dted.Rows.Count > 1)
             {
                 strQry = "  SELECT EnrollCode from tblDTD with(nolock) where EnrollCode ='" + UniqueChildCode + "' ";
                 DataTable dt = objMain.LoadData(strQry);
                 if (dt.Rows.Count > 0)
                 {
         
                     ScriptManager.RegisterStartupScript(this, GetType(), "importingdone", "alert('Please delete other Duplicate Record ');", true);

                 }
                 else
                 {
                     int res1 = DeleteEnrollMentData(UniqueChildCode, lblStatus);

                     if (res1 > 0)
                     {
                         LoadData();
                         ScriptManager.RegisterStartupScript(this, GetType(), "importingdone", "alert('Record Deleted');", true);

                     }

                 }
             }
             else
             {
                 int res1 = DeleteEnrollMentData(UniqueChildCode, lblStatus);

                 if (res1 > 0)
                 {
                     LoadData();
                     ScriptManager.RegisterStartupScript(this, GetType(), "importingdone", "alert('Record Deleted');", true);

                 }

             }
         }
         else
         {

             int res1 = DeleteEnrollMentData(UniqueChildCode, lblStatus);

             if (res1 > 0)
             {
                 LoadData();
                 ScriptManager.RegisterStartupScript(this, GetType(), "importingdone", "alert('Record Deleted');", true);

             }
         }
         //}

    }
    public int DeleteEnrollMentData(string UniqueChildCode, string flag)
    {
        SqlParameter[] cmdParameters = new SqlParameter[]
		{
			new SqlParameter("@UniqueChildCode ", UniqueChildCode),
			new SqlParameter("@flag", flag),
            new SqlParameter("@UserName",  Session["username"].ToString() )
		};
        return SqlHelper.ExecuteNonQuery(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "DeleteEnrollMentDataModify", cmdParameters);
    }
 
 
}