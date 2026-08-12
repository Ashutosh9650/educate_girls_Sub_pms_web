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
public partial class frmSearchReEnrollment : System.Web.UI.Page
{
    clsMain objMain = new clsMain();
    Comman objComman = new Comman();

    string conditions = "";
    string flag = "";
    Password objPass = new Password();
    public string FNewYear;
    public DataTable dtUserDeatils;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            FillCBDist();
            FillClass();
            ddlDistrict.SelectedValue = Session["DistCode"].ToString();
            ddlDistrict_SelectedIndexChanged(ddlDistrict, null);
            ddlBlock.SelectedValue = Session["BlockCode"].ToString();
            ddlBlock_SelectedIndexChanged(ddlBlock,null);
            
            ddlPanchayat.SelectedValue = Session["PhanyCode"].ToString();
            ddlPanchayat_SelectedIndexChanged(ddlPanchayat, null);

          
           
            
            ddlVillage.SelectedValue=Session["VillCode"].ToString() ;
            LoadEnrolled();

           // ddlGender.SelectedIndex = 2;
            LoadData();
        }
    }
    protected void btnSerach_Click(object sender, EventArgs e)
    {
        LoadData();
    }
    public void LoadData()
    {
        string strQry = "";

        string Condtion = "";
        string ageCondtion = "";

        if ( ddlBlock.SelectedIndex >= 0)
        {

            Condtion = Condtion + "and mst5Village.BlockCode='" + ddlBlock.SelectedValue.ToString() + "'";

        }



        if ( ddlPanchayat.SelectedIndex > 1)
        {
            Condtion = Condtion + "and mst5Village.PanchayatCode='" + ddlPanchayat.SelectedValue.ToString() + "'";
        }

        if (ddlVillage.SelectedIndex >= 0)
        {
            Condtion = Condtion + "and mst5Village.VillageCode='" + ddlVillage.SelectedValue.ToString() + "'";
        }

        if (ddlclass.SelectedIndex >= 0)
        {
            if (Convert.ToInt32(ddlclass.SelectedValue) > 0)
            {

                Condtion += " and DoChild=" + ddlclass.SelectedValue + " ";

            }
        }
        if (txtUniqueId.Text != "")
        {
            Condtion += " and Serial like '%" + txtUniqueId.Text + "%' ";

        }

        if (txtChildname.Text != "")
        {
            Condtion += " and ChildName like '%" + txtChildname.Text + "%' ";

        }

        if (txtFather.Text != "")
        {
            Condtion += " and FatherName like '%" + txtFather.Text + "%' ";

        }

        if (txtHHNo.Text != "")
        {
            Condtion += " and HouseNo like '%" + txtHHNo.Text + "%' ";

        }
       
        Int32 Gender = 0;
        if (Convert.ToInt32(ddlGender.SelectedValue) == 1)
        {
            Gender = 1;
        }
        else
        {
            Gender = 2;
        }

        if (Gender > 0)
        {
            Condtion += " and Gender = " + Gender + " ";

        }

        ageCondtion = "";
       
        Condtion += ageCondtion;
        //if (Program.UserLevel == 1)
        //{
        //strQry = " Select UniqueCode,HHNo as HHNo,StrConv(ChildName,3) as [Child Name],FathersName as [Fathers Name],ES.Description as EduationStatus,'' as Serial,Gender from tblDTD  LEFT JOIN (select * from mstLookup where LookupFlag='ES')  AS ES ON tblDTD.[EduationStatus] = ES.LookupCode  where VillageCode='" + CBVillage.SelectedValue + "' and  EnrollStatus in(1) and DeleteFlag=1  " + Condtion + "   order by ChildName ";
        //strQry = "SELECT  UniqueCode,HHNo ,tblDTD.ChildName as [Name of Enroll], tblDTD.FathersName,'' as Serial,Ageason as Age ";

        //strQry = "SELECT  UniqueCode,HHNo ,tblDTD.ChildName ,Gender,DOB,Serial as D2Serial,tblDTD.FathersName,'' as EnrollSerialNo,Ageason as Age,DoChild,SocialCategory,";
        //strQry += " EnrolmentCategory,EduationStatus";
        //strQry += " FROM ((((((((mst5Village INNER JOIN tblDTD ON mst5Village.VillageCode = tblDTD.VillageCode) ";

        //strQry += " LEFT JOIN (select * from mstLookup where LookupFlag='AGE')  AS aged ON tblDTD.[AgeProof] = aged.LookupCode) LEFT JOIN (select * from mstLookup where LookupFlag='CAT')  AS cat ON tblDTD.[SocialCategory] = cat.LookupCode) LEFT JOIN (select * from mstLookup where LookupFlag='FO')  AS cat1 ON tblDTD.[FamilyOccupation] = cat1.LookupCode) LEFT JOIN (select * from mstLookup where LookupFlag='ES')  AS cat2 ON tblDTD.[Eduationstatus] = cat2.LookupCode) LEFT JOIN (select * from mstLookup where LookupFlag='RE')  AS cat3 ON tblDTD.[ReasonDO_NE] = cat3.LookupCode) LEFT JOIN (select * from mstLookup where LookupFlag='EC')  AS cat4 ON tblDTD.[EnrolmentCategory] = cat4.LookupCode)  LEFT JOIN  (select * from mstLookup where LookupFlag='CL')  AS cat5   ON tblDTD.DoChild = Cat5.LookupCode) LEFT JOIN mstSchool ON (tblDTD.School = mstSchool.SchoolCode) AND (tblDTD.VillageCode = mstSchool.VillageCode) where   EnrollStatus in(1) and DeleteFlag=1    " + Condtion + " ORDER BY Serial";
        strQry = "SELECT HouseNo as HHNo ,tblEnrolment.ChildName ,Gender,DOB,UniqueChildCode as UniqueCode,Serial as D2Serial,tblEnrolment.FatherName,Serial as EnrollSerialNo,Ageason as Age, Class as DoChild,";
        strQry += " [Category] as  SocialCategory, Category as EnrolmentCategory, Type as EduationStatus,[DOB],[EnrolmentDate] FROM tblEnrolment  LEFT JOIN mst5Village ON mst5Village.VillageCode = tblEnrolment.VillageCode or mst5Village.OldUniqueCode = tblEnrolment.VillageCode";
        strQry += " LEFT JOIN mst2District ON mst5Village.DistrictCode = mst2District.DistrictCode       LEFT JOIN mstLookup cat on cat.LookupCode=Category and cat.LookupFlag='CAT' ";
        strQry += "         LEFT JOIN mstLookup cat2 on cat2.LookupCode=[Category] and cat2.LookupFlag='ES'  ";
        strQry += " LEFT JOIN mstLookup cat4 on cat4.LookupCode=Category and cat4.LookupFlag='EC'     LEFT JOIN mstLookup cat5 on cat5.LookupCode=Class and cat5.LookupFlag='CL' ";

        strQry += "     where   [AnnualExamStatus] =2  and ReenrollStatus=0   " + Condtion + " ";

              DataTable dt = objMain.LoadData(strQry);

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
        
        //dtSearchVill = dbt.VGridFill(strQry);
        

    }


    public void LoadEnrolled()
    {
        DataRow dr = null;
        DataTable dtGender = new DataTable();
        dtGender.Columns.Add(new DataColumn("ID", System.Type.GetType("System.Int32")));
        dtGender.Columns.Add(new DataColumn("Name", System.Type.GetType("System.String")));
        dr = dtGender.NewRow();
        dr["ID"] = 0;
        dr["Name"] = "--Select--";
        dtGender.Rows.Add(dr);
        dr = dtGender.NewRow();
        dr["ID"] = 1;
        dr["Name"] = "Enrolled";
        dtGender.Rows.Add(dr);
        dr = dtGender.NewRow();
        dr["ID"] = 2;
        dr["Name"] = "Not Enrolled";
        dtGender.Rows.Add(dr);

      
    }
    public void FillClass()
    {
        conditions = "";
        conditions = "LookupFlag ='CL' and Active=1";
        objComman.BindDLL("mstLookup", "LookupCode,Description", conditions, "LookupCode", "asc", ddlclass, "Description", "LookupCode", "Select");



    }
    public void FillCBDist()
    {
        conditions = "";
        conditions = "StateCode ='" + Session["StateCode"].ToString() + "' and  Fyear='" + Session["FYear"].ToString() + "'";
        objComman.BindDLL("mst2District", "DistrictCode,dbo.TitleCase(upper(DistrictName)) as DistrictName ", conditions, "DistrictName", "asc", ddlDistrict, "DistrictName", "DistrictCode", "Select");



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
    public void FillCBBock()
    {
        conditions = "";
        conditions = "DistrictCode ='" + ddlDistrict.SelectedValue + "'";
        objComman.BindDLL("mst3Block", "BlockCode,dbo.TitleCase(upper(BlockName)) as BlockName ", conditions, "BlockName", "asc", ddlBlock, "BlockName", "BlockCode", "Select");



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
        //objComman.BindDLL("mst5Village", "VillageCode,dbo.TitleCase(upper(VillageName)) as VillageName", conditions, "VillageName", "asc", ddlVillage, "VillageName", "VillageCode", "--All-");

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

    protected void GvReport_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DropDownList dllClass = (DropDownList)e.Row.FindControl("dllClass");
            TextBox txtclass = (TextBox)e.Row.FindControl("txtvclass");

            TextBox txtDate = (TextBox)e.Row.FindControl("txtDate");

            DropDownList ddlScat = (DropDownList)e.Row.FindControl("ddlScat");
            TextBox txtsCate = (TextBox)e.Row.FindControl("txtsCate");

            TextBox txtaddate = (TextBox)e.Row.FindControl("txtaddate");
            TextBox txtDob = (TextBox)e.Row.FindControl("txtDob");
            
            DropDownList ddlEnroll = (DropDownList)e.Row.FindControl("ddlEnroll");
            TextBox txtenroll = (TextBox)e.Row.FindControl("txtenroll");

            DropDownList ddlEduationStatus = (DropDownList)e.Row.FindControl("ddlEduationStatus");
            TextBox txtEduationStatus = (TextBox)e.Row.FindControl("txtEduationStatus");
            conditions = "";
            conditions = "LookupFlag ='ECL' and Active=1";
            objComman.BindDLL("mstLookup", "LookupCode,Description", conditions, "LookupCode", "asc", dllClass, "Description", "LookupCode", "Select");
            dllClass.SelectedValue = txtclass.Text;
            conditions = "";
            conditions = "LookupFlag ='CAT' and Active=1";
            objComman.BindDLL("mstLookup", "LookupCode,Description", conditions, "LookupCode", "asc", ddlScat, "Description", "LookupCode", "Select");

            ddlScat.SelectedValue = txtsCate.Text;
            conditions = "";
            conditions = "LookupFlag ='ES' and Active=1 and LookupCode in(1,2,3) ";
            objComman.BindDLL("mstLookup", "LookupCode,Description", conditions, "LookupCode", "asc", ddlEnroll, "Description", "LookupCode", "Select");
            ddlEnroll.SelectedValue = txtenroll.Text;


            conditions = "";
            conditions = "LookupFlag ='EC' and Active=1";
            objComman.BindDLL("mstLookup", "LookupCode,Description", conditions, "LookupCode", "asc", ddlEduationStatus, "Description", "LookupCode", "Select");
            ddlEduationStatus.SelectedValue = txtEduationStatus.Text;

            txtDate.Text = Convert.ToDateTime(txtDate.Text).ToString("dd/MM/yyyy"); ;
            txtDob.Text = Convert.ToDateTime(txtaddate.Text).ToString("dd/MM/yyyy");

           
        }
    }


    protected void btSave_Click(object sender, EventArgs e)
    {
        if (!Validation())
            return;
        SaveData();
        
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        //string url = "frmAddEnrollment.aspx";
        //string s = "window.open('" + url + "', 'popup_window', 'width=1300,height=500,left=700,top=400,resizable=yes');";
        //ClientScript.RegisterStartupScript(this.GetType(), "script", s, true);
        int intId = 100;
        Session["UnquieId"] = null;
            string strPopup = "<script language='javascript' ID='script1'>"
   
            // Passing intId to popup window.
            + "window.open('frmAddEnrollment.aspx?data=" + HttpUtility.UrlEncode(intId.ToString())

            + "','new window', ' width=1100,height=650,left=600,top=450,dependant=no, scrollbars=1,location=0, alwaysRaised=no, menubar=no, resizeable=no,  toolbar=no, status=no, center=yes')"

            + "</script>";

            ScriptManager.RegisterStartupScript((Page)HttpContext.Current.Handler, typeof(Page), "Script1", strPopup, false);

    }
    public void SaveData()
    {

        string StudentTSInsertQuery = "";
        string ChildCode = "";
        bool InsertTSEnroll = false;
        Boolean flag = false;
        for (int i = 0; i < gvnroll.Rows.Count; i++)
        {
            CheckBox Chk1 = ((CheckBox)gvnroll.Rows[i].FindControl("Chk1"));
            string HHNo = ((TextBox)gvnroll.Rows[i].FindControl("txtHHNo")).Text;
            string ChildName = ((TextBox)gvnroll.Rows[i].FindControl("txtChildName")).Text;
            string FathersName = ((TextBox)gvnroll.Rows[i].FindControl("txtFatherName")).Text;
            string strSerial = ((TextBox)gvnroll.Rows[i].FindControl("txtSrno")).Text;
            string txtDate = ((TextBox)gvnroll.Rows[i].FindControl("txtDate")).Text;
            string txtDob = ((TextBox)gvnroll.Rows[i].FindControl("txtDob")).Text;
            string dllClasss = ((DropDownList)gvnroll.Rows[i].FindControl("dllClass")).SelectedValue;
            string ddlScat = ((DropDownList)gvnroll.Rows[i].FindControl("ddlScat")).SelectedValue;
            string ddlEnroll = ((DropDownList)gvnroll.Rows[i].FindControl("ddlEnroll")).SelectedValue;
            string ddlEduationStatus = ((DropDownList)gvnroll.Rows[i].FindControl("ddlEduationStatus")).SelectedValue;
            string lblUniqueCode = ((Label)gvnroll.Rows[i].FindControl("lblUniqueCode")).Text;

            


            Boolean DoAv = true;
            Int32 Gender = 2;
            //if (cmbGender == "2")
            //{
            //    Gender = 2;
            //}
            //else
            //{
            //    Gender = 1;
            //}

            DateTime DOB;
            DateTime AsDob;
            Int32 Age = 0;

            DoAv = true;

            string MainSarveyDate = txtDate;
            string[] d = MainSarveyDate.Split('/');


            string SarveyDate = d[2] + '-' + d[1] + '-' + d[0];


          //  DateTime AdmissionDate = Convert.ToDateTime(SarveyDate);

            DateTime Adminision = Convert.ToDateTime(SarveyDate);
        

            DateTime DOBStudent = Convert.ToDateTime(txtDob);
            DateTime dtason = DOBStudent;
            Age = DateTime.Now.Year - dtason.Year;
            DOB = DOBStudent;
            string DOB1 = DOBStudent.ToString();
            string[] words = DOB1.Split('/');
            Int32 iyear = Convert.ToInt32(dtason.Year) + Age;
            AsDob = Convert.ToDateTime(words[0] + '/' + words[1] + '/' + iyear.ToString());
            if (Chk1.Checked==true)
            {
                flag = true;

                string UNICOde = objMain.Generate_RandomString(8);


              
                //string strQry = " Select [UniqueCode],tblDTD.[VillageCode],Migration,SurvayDate,DoChild,AsOnDate,StrConv(Mauhalla,3) as Mauhalla,[Serial],[HHNo],[SocialCategory],[FamilyOccupation],StrConv([ChildName],3) as ChildName,StrConv([FathersName],3) as FathersName,[Gender],[DOBAvailable],[DOB],[AgeAson],[AgeProof],[EduationStatus],[School],[ReasonDO_NE],[MigrationDuration],[EnrolmentCategory], mst5Village.PanchayatCode,mst5Village.BlockCode,mst5Village.DistrictCode,mstSchool.Name,Status FROM (mst5Village INNER JOIN tblDTD ON mst5Village.VillageCode = tblDTD.VillageCode) left JOIN mstSchool ON tblDTD.School = mstSchool.SchoolCode where UniqueCode='" + ChildCode + "'  ";
                //DataTable dt = dbt.VGridFill(strQry);

                StudentTSInsertQuery = " INSERT INTO tblReEnrolment([UniqueChildCode],[VillageCode],[Serial],[Category],[Class],[Session],ChildName,FatherName,Gender,[SchoolCode],[EnrolmentDate],[DOB],AgeAson,AsOnDate,[Type],EnrollCategory,[Status],Createdate,CreateBy,HouseNo,[EnrollmentCode]) Values  ('" + UNICOde + "','" + Session["VillCode"].ToString() + "','" + strSerial + "'," + ddlScat + "," + dllClasss + "," + DateTime.Now.Year + ",'" + ChildName + "','" + FathersName + "'," + Gender + ",'" + Session["Schoolid"] + "','" + Adminision.ToString("yyyy-MM-dd") + "','" + DOB.ToString("yyyy-MM-dd") + "'," + Age + ",'" + DOB.ToString("yyyy-MM-dd") + "','" + ddlEduationStatus + "','" + ddlEnroll + "',2,'" + DateTime.Now.ToString("yyyy-MM-dd") + "','" + Session["username"].ToString() + "','" + HHNo + "','" + lblUniqueCode + "')";
                InsertTSEnroll = objMain.AddUpdate(StudentTSInsertQuery);

                StudentTSInsertQuery = "";

                StudentTSInsertQuery = "Update tblEnrolment set ReenrollStatus=1  where UniqueChildCode='" + lblUniqueCode + "' ";
                InsertTSEnroll = objMain.AddUpdate(StudentTSInsertQuery);


            }
        }
        if (flag == true)
        {
            if (InsertTSEnroll == true)
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Saved sucessfully')</script>", false);
                LoadData();
            }


        }
        else
        {
           // MessageBox.Show("Invalid SR. No.");

        }
    }


    private Boolean Validation()
    {
        try
        {

            for (int i = 0; i < gvnroll.Rows.Count; i++)
            {

                CheckBox Chk1 = ((CheckBox)gvnroll.Rows[i].FindControl("Chk1"));
                string HHNo = ((TextBox)gvnroll.Rows[i].FindControl("txtHHNo")).Text;
                string ChildName = ((TextBox)gvnroll.Rows[i].FindControl("txtChildName")).Text;
                string FathersName = ((TextBox)gvnroll.Rows[i].FindControl("txtFatherName")).Text;
                string strSerial = ((TextBox)gvnroll.Rows[i].FindControl("txtSrno")).Text;
                string txtDate = ((TextBox)gvnroll.Rows[i].FindControl("txtDate")).Text;
                string txtDob = ((TextBox)gvnroll.Rows[i].FindControl("txtDob")).Text;
                string dllClasss = ((DropDownList)gvnroll.Rows[i].FindControl("dllClass")).SelectedValue;
                string ddlScat = ((DropDownList)gvnroll.Rows[i].FindControl("ddlScat")).SelectedValue;
                string ddlEnroll = ((DropDownList)gvnroll.Rows[i].FindControl("ddlEnroll")).SelectedValue;
                string ddlEduationStatus = ((DropDownList)gvnroll.Rows[i].FindControl("ddlEduationStatus")).SelectedValue;

                if (Chk1.Checked ==true)
                {
                    string strQry = " Select [Serial] FROM tblReEnrolment where [Serial]='" + strSerial.Trim() + "' and  SchoolCode ='" + Session["Schoolid"].ToString() + "'";
                    DataTable dt = objMain.LoadData(strQry);

                    if (dt.Rows.Count > 0)
                    {
                      
                        ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('This  Serial No already exists in Database')</script>", false);
                       
                        return false;
                    }

                  
                    //if (cmbGender == "0")
                    //{
                    //    MessageBox.Show("Select Gender");
                    //    dHHNo.Style.BackColor = Color.Red;
                    //    return false;
                    //}
                     if (ChildName.Trim() == "")
                    {
                      
                        ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Enter Child name')</script>", false);
                       
                        return false;
                    }

                    else if (FathersName.Trim() == "")
                    {

                        ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Enter Father name')</script>", false);
                    
                       
                        //  dFathersName.Style.BackColor = Color.Red;
                        return false;
                    }

                     else if (dllClasss == "0")
                    {
                     
                        ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Select Class')</script>", false);
                    
                        return false;
                    }

                     else if (ddlScat == "0")
                    {
                     
                        ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Select SocialCategory')</script>", false);
                    
                        return false;
                    }

                    else if (ddlEnroll == "0")
                    {

                        ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Select EnrollmentCategory')</script>", false);
                    
                      
                        
                        return false;
                    }
                     string MainSarveyDate = txtDate;
                     string[] d = MainSarveyDate.Split('/');


                     string SarveyDate = d[2] + '-' + d[1] + '-' + d[0];

            
                     DateTime AdmissionDate = Convert.ToDateTime(SarveyDate);
                    Int32 fDate = ((AdmissionDate.Year) * 10000 + (AdmissionDate.Month) * 100 + (AdmissionDate.Day));
                    Int32 cYear = ((DateTime.Now.Year) * 10000 + (04) * 100 + (01));
                    if (cYear < fDate)
                    {
                        if (ddlEduationStatus == "0")
                        {
                         
                            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Select  Education status')</script>", false);
                    
                            return false;
                        }
                    }
                    else
                    {
                        if (ddlEduationStatus == "0")
                        {
                          
                            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Select  Education status')</script>", false);
                    
                            return false;
                        }
                        //if (ddlEduationStatus != "1")
                        //{
                        //    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please ensure that Education status Enrolled')</script>", false);
                    
                         
                        //    //  dEduationStatus.Style.BackColor = Color.Red;
                        //    return false;
                        //}
                    }


                    DateTime DOB;
                    DateTime AsDob;
                    Int32 Age = 0;

                    string DateSarveyDate = DateTime.Now.ToString("dd/MM/yyyy");
                    string[] b = DateSarveyDate.Split('/');

                    string DateB = txtDate;
                    string[] a = txtDob.Split('/');
                    string BithDate = a[2] + '-' + a[1] + '-' + a[0];



                    Age = Convert.ToInt32(b[2]) - Convert.ToInt32(a[2]);
                    DOB = Convert.ToDateTime(a[2] + '-' + a[1] + '-' + a[0]);

                    Int32 iyear = Convert.ToInt32(a[2]) + Age;
                    string dyear = iyear.ToString();
                    AsDob = Convert.ToDateTime(dyear + '-' + a[1] + '-' + a[0]);

                    if (Age < 3)
                    {

                        ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please ensure Age is between 3 and 14 years')</script>", false);


                     
                        return false;

                    }
                    if (Age > 14)
                    {
                        ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please ensure Age is between 3 and 14 years')</script>", false);


                    
                        return false;
                    }

                }

            }

          
            return true;

        }
        catch (Exception ex)
        {
           // MessageBox.Show(ex.Message, "EG", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return false;
        }
    }

}


