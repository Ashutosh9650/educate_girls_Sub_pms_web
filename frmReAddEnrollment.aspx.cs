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
using System.Globalization;
public partial class frmReAddEnrollment : System.Web.UI.Page
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
            FillClass();
            FillSocialCat();
            FillENrollment();
            FillEduStauts();
             lblSchool.Text=   Session["SchoolName"].ToString();
            lblPhy.Text=   Session["PhanyName"].ToString();
            lblVillage.Text=  Session["Villageame"].ToString();

            txtDobDate.Text = DateTime.Now.ToString("dd/MM/yyyy");

            txtBirth.Text = DateTime.Now.ToString("dd/MM/yyyy");

            if (Convert.ToString(Session["UnquieId"]).Length > 6)
            {
                FillD2dData();
            }
            else
            {

                Session["UnquieId"] = "ss";
            }
        }
    }

    public void FillD2dData()
    {
        string strQry = " Select [UniqueChildCode],mstSchool.name,[tblReEnrolment].[VillageCode],EnrolmentDate as SurvayDate,Class,[Serial],[HouseNo],[Category],[ChildName] as ChildName,[FatherName] as FathersName,[Gender],[DOB],[AgeAson],Type as EduationStatus,[tblReEnrolment].[SchoolCode],[EnrollCategory], mst5Village.PanchayatCode,mst5Village.BlockCode,mst5Village.DistrictCode,[tblReEnrolment].Status FROM (mst5Village INNER JOIN [tblReEnrolment] ON mst5Village.VillageCode = [tblReEnrolment].VillageCode) left JOIN mstSchool ON [tblReEnrolment].SchoolCode = mstSchool.SchoolCode where UniqueChildCode='" + Session["UnquieId"].ToString() + "' ";
        DataTable dt = objMain.LoadData(strQry);


        if (dt.Rows.Count > 0)
        {

            //if (dt.Rows[0]["Status"].ToString() == "2")
            //{
            //    btnd2dSave.Enabled = false;
            //    btnD2Delete.Enabled = false;
            //}
            //else
            //{
            //    btnd2dSave.Enabled = true;
            //    btnD2Delete.Enabled = true;
            //}

            ddlGender.SelectedValue = dt.Rows[0]["Gender"].ToString();
         
           txtSrno.Text = dt.Rows[0]["Serial"].ToString();
            txtChildName.Text = dt.Rows[0]["ChildName"].ToString();
            txtFatherName.Text = dt.Rows[0]["FathersName"].ToString();

            //villagecode = dt.Rows[0]["VillageCode"].ToString();




            //DTPicker_DOB.Format = DateTimePickerFormat.Custom;
            //DTPicker_DOB.CustomFormat = "dd/MM/yyyy ";

            DateTime DOB = Convert.ToDateTime(dt.Rows[0]["DOB"].ToString());


            txtDobDate.Text = DOB.ToString("dd/MM/yyyy");


            DateTime SurvayDate = Convert.ToDateTime(dt.Rows[0]["SurvayDate"].ToString());
            txtBirth.Text = SurvayDate.ToString("dd/MM/yyyy");



            ddlScat.SelectedValue = dt.Rows[0]["Category"].ToString();
            ddlEduationStatus.SelectedValue = dt.Rows[0]["EduationStatus"].ToString();
            ddlEnroll.SelectedValue = dt.Rows[0]["EnrollCategory"].ToString();

        
            lblSchool.Text = dt.Rows[0]["name"].ToString();
            txtHHNo.Text = dt.Rows[0]["HouseNo"].ToString();
            dllClass.SelectedValue = dt.Rows[0]["Class"].ToString();
            //if (dt.Rows[0]["ReasonDO_NE"].ToString() == "0")
            //{
            //    txtReason.Text = "";

            //}
            //else
            //{
            //    txtReason.Text = dt.Rows[0]["ReasonDO_NE"].ToString();
            //}

           
                txtHHNo.Text = dt.Rows[0]["HouseNo"].ToString();
            
        }
    }
    
    protected void btnSerach_Click(object sender, EventArgs e)
    {
       
    }

  
    public void FillClass()
    {
        conditions = "";
        conditions = "LookupFlag ='ECL' and Active=1";
        objComman.BindDLL("mstLookup", "LookupCode,Description", conditions, "LookupCode", "asc", dllClass, "Description", "LookupCode", "Select");



    }


    public void FillSocialCat()
    {
        conditions = "";
        conditions = "LookupFlag ='CAT' and Active=1";
        objComman.BindDLL("mstLookup", "LookupCode,Description", conditions, "LookupCode", "asc", ddlScat, "Description", "LookupCode", "Select");



    }

    public void FillENrollment()
    {
        conditions = "";
        conditions = "LookupFlag ='ES' and Active=1 and LookupCode in(1,2,3) ";
        objComman.BindDLL("mstLookup", "LookupCode,Description", conditions, "LookupCode", "asc", ddlEnroll, "Description", "LookupCode", "Select");



    }

    public void FillEduStauts()
    {
        conditions = "";
        conditions = "LookupFlag ='EC' and Active=1";
        objComman.BindDLL("mstLookup", "LookupCode,Description", conditions, "LookupCode", "asc", ddlEduationStatus, "Description", "LookupCode", "Select");



    }
  
  
    protected void btSave_Click(object sender, EventArgs e)
    {
        if (!Validation())
            return;
        SaveData();
        
    }


    public void SaveData()
    {

       
            string strUnique = "0";
            string HHNo = txtHHNo.Text.Trim();
            string ChildName =  CultureInfo.CurrentCulture.TextInfo.ToTitleCase(txtChildName.Text.Trim());
            string FathersName =  CultureInfo.CurrentCulture.TextInfo.ToTitleCase(txtFatherName.Text.Trim());
            string strSerial = txtSrno.Text.Trim();
       
            string dllClasss =dllClass.SelectedValue;
            string Scat =  ddlScat.SelectedValue.ToString();


            Int32 Gender = Convert.ToInt32(ddlGender.SelectedValue);


            Int32 DoAv = 1;
            
            //if (cmbGender == "2")
            //{
            //    Gender = 2;
            //}
            //else
            //{
            //    Gender = 1;
            //}

            DateTime DOB;
            string AsDob;
            Int32 Age = 0;



            Int32 ymyear = Convert.ToInt32(Session["mYear"].ToString());
            string Adminision =txtBirth.Text;

            string[] b = Adminision.Split('/');
            string DateAdminision = b[2] + '-' + b[1] + '-' + b[0];

            DateTime DOBStudent = Convert.ToDateTime(txtDobDate.Text);
            DateTime dtason = DOBStudent;
            Age = ymyear - dtason.Year;

            DOB = DOBStudent;
           
            string[] c = txtDobDate.Text.Split('/');
            string ChildDOB = c[2] + '-' + c[1] + '-' + c[0];

            string DOB1 = DOBStudent.ToString();
            string[] words = DOB1.Split('/');
            Int32 iyear = Convert.ToInt32(dtason.Year) + Age;
            string lYear = iyear.ToString();
            AsDob = lYear + '-' + words[1] + '-' + words[0];
           // AsDob = words[2] + '-' + words[1] + '-' + iyear.ToString();
            string StudentTSInsertQuery = "";
            if (Session["UnquieId"].ToString().Length > 6)
            {

                StudentTSInsertQuery = " Update  [tblReEnrolment] set [Category]=" + ddlScat.SelectedValue + ",[Class]=" + dllClass.SelectedValue + ",Serial='" + strSerial + "',ChildName='" + ChildName + "',FatherName='" + FathersName + "',Gender=" + Gender + ",[SchoolCode]='" + Session["Schoolid"].ToString() + "',[EnrolmentDate]='" + DateAdminision + "',[DOB]='" + ChildDOB + "',AgeAson=" + Age + ",AsOnDate='" + AsDob + "',[Type]='" + ddlEduationStatus.SelectedValue + "',EnrollCategory='" + ddlEnroll.SelectedValue + "',ModifyDate='" + DateTime.Now.ToString("yyyy-MM-dd") + "',ModifyBy='" + Session["username"].ToString() + "',HouseNo='" + txtHHNo.Text.Trim() + "' where UniqueChildCode ='" + Session["UnquieId"].ToString() + "'";
                bool UpdateTs = objMain.AddUpdate(StudentTSInsertQuery);

              

                if (UpdateTs == true)
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Saved sucessfully')</script>", false);
                    txtChildName.Text = "";
                    txtFatherName.Text = "";
                    txtHHNo.Text = "";
                    txtSrno.Text = "";
                    txtHHNo.Focus();
                    txtBirth.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    ddlEduationStatus.SelectedIndex = 0;
                }
            }
            else
            {

                
            }
       
    }


    private Boolean Validation()
    {
        try
        {



            if (Session["UnquieId"].ToString().Length > 6)
            { }
            else
            {
                string strQry = " Select [Serial] FROM tblEnrolment where [Serial]='" + txtSrno.Text.ToString() + "' and  SchoolCode ='" + Session["Schoolid"].ToString() + "'";
                DataTable dt = objMain.LoadData(strQry);

                if (dt.Rows.Count > 0)
                {

                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('This  Serial No already exists in Database')</script>", false);

                    return false;
                }

            }
                    //if (cmbGender == "0")
                    //{
                    //    MessageBox.Show("Select Gender");
                    //    dHHNo.Style.BackColor = Color.Red;
                    //    return false;
                    //}
                    if (txtChildName.Text.Trim() == "")
                    {

                        ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Enter Child name')</script>", false);

                        return false;
                    }

                    else if (txtFatherName.Text.Trim() == "")
                    {

                        ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Enter Father name')</script>", false);


                        //  dFathersName.Style.BackColor = Color.Red;
                        return false;
                    }

                    else if (dllClass.SelectedIndex <= 0)
                    {

                        ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Select Class')</script>", false);

                        return false;
                    }

                    else if (ddlScat.SelectedIndex <= 0)
                    {

                        ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Select SocialCategory')</script>", false);

                        return false;
                    }

                    else if (ddlEnroll.SelectedIndex <= 0)
                    {

                        ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Select EnrollmentCategory')</script>", false);



                        return false;
                    }
                    DateTime AdmissionDate = Convert.ToDateTime(txtBirth.Text);
                    Int32 fDate = ((AdmissionDate.Year) * 10000 + (AdmissionDate.Month) * 100 + (AdmissionDate.Day));

                    Int32 cFyear = Convert.ToInt32(Session["mYear"].ToString());

                    Int32 cYear = ((cFyear) * 10000 + (04) * 100 + (01));
                    if (cYear < fDate)
                    {
                        if (Convert.ToInt32(ddlEnroll.SelectedValue) == 0)
                        {

                            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Select  Education status')</script>", false);

                            return false;
                        }
                    }
                    else
                    {
                        if (Convert.ToInt32(ddlEnroll.SelectedValue) == 0)
                        {

                            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Select  Education status')</script>", false);

                            return false;
                        }
                        //if (Convert.ToInt32(ddlEnroll.SelectedValue) == 1)
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

                    string DateB = txtDobDate.Text;
                    string[] a = txtDobDate.Text.Split('/');
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

             


            return true;

        }
        catch (Exception ex)
        {
            // MessageBox.Show(ex.Message, "EG", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return false;
        }
    }

}


