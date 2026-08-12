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
public partial class frmAddEnrollmentFrom6 : System.Web.UI.Page
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
            if (Session["StateCode"].ToString() == "8")
            {
                Div9.Visible = false;
            }
            else
            {
                Div9.Visible = true;
            }
             lblSchool.Text=   Session["SchoolName"].ToString();
           
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
        string strQry = " Select [UniqueChildCode],VillagenameOther,SamgraID,ChildCode,mstSchool.name,tblEnrolment.[VillageCode],EnrolmentDate as SurvayDate,Class,AsOnDate,[Serial],[HouseNo],[Category],[ChildName] as ChildName,[FatherName] as FathersName,[Gender],[DOBAvailable],[DOB],[AgeAson],Type as EduationStatus,tblEnrolment.[SchoolCode],[EnrollCategory], mst5Village.PanchayatCode,mst5Village.BlockCode,mst5Village.DistrictCode,tblEnrolment.Status FROM (mst5Village INNER JOIN tblEnrolment ON mst5Village.VillageCode = tblEnrolment.VillageCode) left JOIN mstSchool ON tblEnrolment.SchoolCode = mstSchool.SchoolCode where UniqueChildCode='" + Session["UnquieId"].ToString() + "' ";
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

            txtSamgra.Text = dt.Rows[0]["SamgraID"].ToString();
            txtSurveyVillage.Text = dt.Rows[0]["VillagenameOther"].ToString();
            //villagecode = dt.Rows[0]["VillageCode"].ToString();




            //DTPicker_DOB.Format = DateTimePickerFormat.Custom;
            //DTPicker_DOB.CustomFormat = "dd/MM/yyyy ";

            DateTime DOB = Convert.ToDateTime(dt.Rows[0]["DOB"].ToString());

            if (dt.Rows[0]["DOB"].ToString() == "01/01/1900 00:00:00")
            {
                 txtDobDate.Text ="";
            }
            else
            {
                  txtDobDate.Text = DOB.ToString("dd/MM/yyyy");
            }
          



            DateTime SurvayDate = Convert.ToDateTime(dt.Rows[0]["SurvayDate"].ToString());
            if (dt.Rows[0]["DOB"].ToString() == "01/01/1900 00:00:00")
            {
                txtBirth.Text = "";
            }
            else
            {
                txtBirth.Text = SurvayDate.ToString("dd/MM/yyyy");
            }
        



            ddlScat.SelectedValue = dt.Rows[0]["Category"].ToString();
            //ddlEduationStatus.SelectedValue = Convert.ToInt32(dt.Rows[0]["EnrollCategory"].ToString()).ToString();
            //ddlEnroll.SelectedValue = dt.Rows[0]["EduationStatus"].ToString();

        
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
        if (Session["Schoolid"].ToString() == "99" || Session["SchoolCodeID"].ToString() == "0")
        {
            conditions = "LookupFlag ='ECL'  and lookupcode not in(1,2,3,4,5)";
        }
        else
        {
            if (Session["ManagementType"].ToString() == "1")
            {
                conditions = "LookupFlag ='ECL'  and lookupcode not in(1,2,3,4,5)";
            }
            else
            {
                conditions = "LookupFlag ='ECL'";
            }
        }
     
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
            DateTime AsDob;
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
            AsDob = Convert.ToDateTime(DateTime.Today); ;
           // AsDob = words[2] + '-' + words[1] + '-' + iyear.ToString();
            string StudentTSInsertQuery = "";
            if (Session["UnquieId"].ToString().Length > 6)
            {

                StudentTSInsertQuery = " Update  tblEnrolment set SamgraID ='" + txtSamgra.Text + "',[Category]=" + ddlScat.SelectedValue + ",[Class]=" + dllClass.SelectedValue + ",Serial='" + strSerial + "',ChildName='" + ChildName + "',FatherName='" + FathersName + "',Gender=" + Gender + ",[EnrolmentDate]='" + DateAdminision + "',DOBAvailable=" + DoAv + ",[DOB]='" + ChildDOB + "',AgeAson=" + Age + ",AsOnDate='" + AsDob.ToString("yyyy-MM-dd") + "',ModifyDate='" + DateTime.Now.ToString("yyyy-MM-dd") + "',ModifyBy='" + Session["username"].ToString() + "',HouseNo='" + txtHHNo.Text.Trim() + "' where UniqueChildCode ='" + Session["UnquieId"].ToString() + "'";
                bool UpdateTs = objMain.AddUpdate(StudentTSInsertQuery);

                string D2StudentTSInsertQuery = "";
                if (Convert.ToString(Session["EnStatus"]) == "1")
                {
                    D2StudentTSInsertQuery = " Update tblDTD set   [HHNo]='" + txtHHNo.Text.Trim() + "',SurvayDate='" + Convert.ToDateTime(DateAdminision).ToString("yyyy-MM-dd") + "',[SocialCategory]=" + ddlScat.SelectedValue + ",[ChildName]='" + ChildName + "',[FathersName]='" + FathersName + "',[Gender]=" + Gender + ",[DOB]='" + ChildDOB + "',[AsOnDate]='" + AsDob + "',[AgeAson]=" + Age + ",    DoChild=" + dllClass.SelectedValue + " ,ModifyDate='" + DateTime.Now.ToString("yyyy-MM-dd") + "',ModifyBy='" + Session["username"].ToString() + "' where UniqueCode ='" + Session["UnquieId"].ToString() + "' ";
                    bool UpdateD2d = objMain.AddUpdate(D2StudentTSInsertQuery);
                }


                if (UpdateTs == true)
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Saved sucessfully')</script>", false);
                   
                    txtChildName.Text = "";
                    txtFatherName.Text = "";
                    txtHHNo.Text = "";
                    txtSrno.Text = "";
                    txtSamgra.Text = "";
                    txtSurveyVillage.Text = "";
                   
                    txtHHNo.Focus();
                    txtBirth.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    ddlEduationStatus.SelectedIndex = 0;
                  //  Response.Write("<script>window.close();</" + "script>");

                    //Response.End();
                   // Response.Redirect("~/frmEnrollmentForm6.aspx?ID=1");
                //    Page.ClientScript.RegisterClientScriptBlock(typeof(Page), "ClosePage", "window.close();", true);

                    
                }
            }
            else
            {
                string VillageCodwwwe = "  VillageCode in(select VillageCode from mst5Village where EGVillageCode in(select EGVillageCode from mst5Village where VillageCode='" + Session["VillCode"].ToString() + "')) ";

                Int32 ssNo = 0;
                string strQry = " select isnull(Max(Serial),0)+1 Serial from [tblDTD]  where " + VillageCodwwwe + " ";
                DataTable dt = objMain.LoadData(strQry);
                if (dt.Rows.Count > 0)
                {
                    ssNo = Convert.ToInt32(dt.Rows[0]["Serial"].ToString());
                }

                string UNICOde = objMain.Generate_RandomString(8);

     
                string UCOde = objComman.Generate_RandomString(8);

                //StudentTSInsertQuery = " INSERT INTO tblEnrolment([UniqueChildCode],[ChildCode],[VillageCode],[Serial],[Category],[Class],[Session],ChildName,FatherName,Gender,[SchoolCode],[EnrolmentDate],DOBAvailable,[DOB],AgeAson,AsOnDate,[Type],EnrollCategory,[Status],Createdate,CreateBy,HouseNo,DeleteFlag,VillagenameOther,SamgraID) Values  ('" + UNICOde + "','" + 0 + "','" + Session["VillCode"].ToString() + "','" + txtSrno.Text + "'," + ddlScat.SelectedValue + "," + dllClass.SelectedValue + ",'" + Session["mYear"].ToString() + "','" + ChildName + "','" + FathersName + "'," + Gender + ",'" + Session["Schoolid"].ToString() + "','" +  Convert.ToDateTime( Adminision).ToString("yyyy-MM-dd") + "'," + DoAv + ",'" + ChildDOB + "'," + Age + ",'" + AsDob.ToString("yyyy-MM-dd") + "','" + ddlEduationStatus.SelectedValue + "','" + ddlEnroll.SelectedValue + "',1,'" + DateTime.Now.ToString("yyyy-MM-dd") + "','" + Session["username"].ToString() + "','" + txtHHNo.Text + "',1,'" + txtSurveyVillage.Text + "','" + txtSamgra.Text + "')";
                //bool InsertTSEnroll = objMain.AddUpdate(StudentTSInsertQuery);

                //StudentTSInsertQuery = "";
                //StudentTSInsertQuery = " INSERT INTO tblDTD([UniqueChildCode],[UniqueCode],[VillageCode],[Serial],[SocialCategory],[SocialCategory1],[SocialCategory2],[ChildName],[ChildName1],[ChildName2],[FathersName],[FathersName1],[FathersName2],[Gender],[Gender1],[Gender2],[DOBAvailable],[DOBAvailable1],[DOBAvailable2],[DOB],[DOB1],[DOB2],[AgeAson],[AgeAson1],[AgeAson2],[School],[School1],[School2],EnrolmentCategory,[EnrolmentCategory1],[EnrolmentCategory2],HHNo,HHNo1,HHNo2,DoChild,DoChild1,DoChild2,SWType,Status,AsOnDate,AsOnDate1,AsOnDate2,Createdate,CreateBy,SurvayDate,EnrollCode,EnrollStatus,DeleteFlag)Values  ('" + UCOde + "','" + UNICOde + "','" + Session["VillCode"].ToString() + "','" + ssNo + "'," + ddlScat.SelectedValue + "," + ddlScat.SelectedValue + "," + ddlScat.SelectedValue + ",'" + ChildName + "','" + ChildName + "','" + ChildName + "','" + FathersName + "','" + FathersName + "','" + FathersName + "'," + Gender + "," + Gender + "," + Gender + "," + DoAv + "," + DoAv + "," + DoAv + ",'" + Convert.ToDateTime(ChildDOB).ToString("yyyy-MM-dd") + "','" + Convert.ToDateTime(ChildDOB).ToString("yyyy-MM-dd") + "','" + Convert.ToDateTime(ChildDOB).ToString("yyyy-MM-dd") + "'," + Age + "," + Age + "," + Age + ",'" + Session["Schoolid"].ToString() + "','" + Session["Schoolid"].ToString() + "','" + Session["Schoolid"].ToString() + "'," + ddlEnroll.SelectedValue + "," + ddlEnroll.SelectedValue + "," + ddlEnroll.SelectedValue + ",'" + txtHHNo.Text.Trim() + "','" + txtHHNo.Text.Trim() + "','" + txtHHNo.Text.Trim() + "'," + dllClass.SelectedValue + "," + dllClass.SelectedValue + "," + dllClass.SelectedValue + ",3,4,'" + AsDob.ToString("yyyy-MM-dd") + "','" + AsDob.ToString("yyyy-MM-dd") + "','" + AsDob.ToString("yyyy-MM-dd") + "','" + DateTime.Now.ToString("yyyy-MM-dd") + "','" + Session["username"].ToString() + "','" + AsDob.ToString("yyyy-MM-dd") + "','" + UNICOde + "',3,1)";
                //bool InsertTS = objMain.AddUpdate(StudentTSInsertQuery);



                //if (InsertTS == true)
                //{

                //    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Saved sucessfully')</script>", false);
                //    //if (UnquieId.Length > 6)
                //    //{
                //    //}
                //    //else
                //    //{
                //    //    Program.EnrollDate = DTPicker_Sur.Value;
                //    //    Program.Esc = Convert.ToInt32(cmbCategory.SelectedValue);
                //    //    Program.Escatory = Convert.ToInt32(cmbEnrollCat.SelectedValue);
                //    //    Program.Gender = Convert.ToInt32(cmbGender.SelectedIndex);

                //    //}
                //    txtChildName.Text = "";
                //    txtFatherName.Text = "";
                //    txtHHNo.Text = "";
                //    txtSrno.Text = "";
                //    txtSamgra.Text = "";
                //    txtSurveyVillage.Text = "";
                //    txtHHNo.Focus();
                //    txtBirth.Text = DateTime.Now.ToString("dd/MM/yyyy");
                //    ddlEduationStatus.SelectedIndex = 0;
                //    //this.Close();
                //}
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
                   
                     if (txtSrno.Text.Trim() == "")
                    {

                        ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Enter Serial No')</script>", false);

                        return false;
                    }

                    if (txtChildName.Text.Trim() == "")
                    {

                        ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Enter Child name')</script>", false);

                        return false;
                    }

                    else if (txtFatherName.Text.Trim() == "")
                    {

                        ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Enter Father name')</script>", false);
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

                    if (txtSamgra.Text.Trim() == "")
                    {

                        ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Enter sangram ID')</script>", false);

                        return false;
                    }
                    if (txtSamgra.Text.Trim().Length<8)
                    {

                        ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('sangram ID should be 8 or 9 digits')</script>", false);

                        return false;
                    }

                    if (txtBirth.Text.Trim() == "")
                    {

                        ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Enter Admission Date')</script>", false);

                        return false;
                    }

                    if (txtDobDate.Text.Trim() == "")
                    {

                        ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Enter DOB')</script>", false);

                        return false;
                    }
                    DateTime AdmissionDate = Convert.ToDateTime(txtBirth.Text);
                    Int32 fDate = ((AdmissionDate.Year) * 10000 + (AdmissionDate.Month) * 100 + (AdmissionDate.Day));

                    Int32 cFyear = Convert.ToInt32(Session["mYear"].ToString());

                    Int32 cYear = ((cFyear) * 10000 + (04) * 100 + (01));
                   
                    DateTime DOB;
                    DateTime AsDob;
                    Int32 Age = 0;

                    string DateSarveyDate = txtBirth.Text;
                    string[] b = DateSarveyDate.Split('/');

                    string DateB = txtDobDate.Text;
                    string[] a = txtDobDate.Text.Split('/');
                    string BithDate = a[2] + '-' + a[1] + '-' + a[0];



                    Age = Convert.ToInt32(b[2]) - Convert.ToInt32(a[2]);
                    DOB = Convert.ToDateTime(a[2] + '-' + a[1] + '-' + a[0]);

                    Int32 iyear = Convert.ToInt32(a[2]) + Age;
                    string dyear = iyear.ToString();
                    if (Convert.ToDateTime(txtBirth.Text.ToString()) <= Convert.ToDateTime(txtDobDate.Text))
                    {
                        ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please ensure Date of admission is subsequent to DOB')</script>", false);
                        return false;
                    }
                   
                        if (Age < 5)
                        {

                            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please ensure Age is between 5 and 16 years')</script>", false);
                            return false;

                        }
                        if (Age > 16)
                        {
                            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please ensure Age is between 5 and 16 years')</script>", false);
                            return false;
                        }

                        if (Convert.ToInt32(Session["mYear"].ToString()) > Convert.ToInt32(AdmissionDate.Year))
                        {
                            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please ensure enrollment date should be in current year')</script>", false);

                            //dDOB.Style.BackColor = Color.Red;
                            return false;
                        }
                    
                  

                    string strQr1y = " Select mstClassValdation.[Operator], mstClassValdation.[Class], mstLookup.SeqNo AS SeqNoCode FROM mstClassValdation, mstLookup where LookupFlag ='ECL' and LookupCode=" + dllClass.SelectedValue + " and  [Age]=" + Age + " ";
                    DataTable dtNew = objMain.LoadData(strQr1y);


                    if (Convert.ToInt32(dllClass.SelectedValue) <= 5)
                    {
                        if (Convert.ToString(Session["EnStatus"])== "2")
                        {
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select valid class')</script>", false);


                            //dDOB.Style.BackColor = Color.Red;
                            return false;
                        }
                    }
                    else
                    {
                        if (dtNew.Rows.Count > 0)
                        {
                            Int32 Iclass = Convert.ToInt32(dtNew.Rows[0]["Class"].ToString());
                            Int32 SeqNoCode = Convert.ToInt32(dtNew.Rows[0]["SeqNoCode"].ToString());
                            string Op = dtNew.Rows[0]["Operator"].ToString();
                            if (Convert.ToInt32(Iclass) < SeqNoCode)
                            {
                                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('You can  select max " + Iclass + " class')</script>", false);

                                return false;
                            }
                        }
                    }

                    if (Session["Schoolid"].ToString() == "99" || Session["SchoolCodeID"].ToString() == "0")
                    {
                    }
                    else
                    {
                        if (Session["SchoolLevel"].ToString() == "5")
                        {
                            if (Convert.ToInt32(ddlGender.SelectedValue) == 1)
                            {
                                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('You can add only female child in KGBV School')</script>", false);

                                return false;
                            }
                        }


                        if (Convert.ToInt32(dllClass.SelectedValue) <= 5 && Convert.ToString(Session["EnStatus"]) == "2")
                        {
                        }
                        else
                        {

                            string strQr1yC = " Select  mstLookup.SeqNo AS SeqNoCode FROM mstLookup where LookupFlag ='ECL' and LookupCode=" + dllClass.SelectedValue + " ";
                            DataTable dtNewC = objMain.LoadData(strQr1yC);
                            Int32 MainClass = Convert.ToInt32(dtNewC.Rows[0]["SeqNoCode"].ToString());
                            if (Session["SchoolLevel"].ToString() == "5")
                            {
                                string strQr1y1 = " Select MaxClass FROM mstClassValdation where  SchoolType=" + Session["SchoolLevel"].ToString() + " ";
                                DataTable dtNew1 = objMain.LoadData(strQr1y1);
                                if (MainClass < 6)
                                {
                                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select Class 6 to 12 School')</script>", false);

                                    return false;
                                }
                                else
                                {
                                    Int32 MaxClass = Convert.ToInt32(dtNew1.Rows[0]["MaxClass"].ToString());
                                    if (MainClass > MaxClass)
                                    {
                                        ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select Class 6 to 12 School')</script>", false);

                                        return false;
                                    }
                                }
                            }

                            else if (Session["SchoolLevel"].ToString() == "7")
                            {
                                string strQr1y1 = " Select MaxClass FROM mstClassValdation where  SchoolType=" + Session["SchoolLevel"].ToString() + " ";
                                DataTable dtNew1 = objMain.LoadData(strQr1y1);
                                if (MainClass < 6)
                                {
                                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select Class 6 to 12 School')</script>", false);

                                    return false;
                                }
                                else
                                {
                                    Int32 MaxClass = Convert.ToInt32(dtNew1.Rows[0]["MaxClass"].ToString());
                                    if (MainClass > MaxClass)
                                    {
                                        ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select Class 6 to 12 School')</script>", false);

                                        return false;
                                    }
                                }
                            }
                            else
                            {
                                string strQr1y1 = " Select MaxClass FROM mstClassValdation where  SchoolType=" + Session["SchoolLevel"].ToString() + " ";
                                DataTable dtNew1 = objMain.LoadData(strQr1y1);
                                if (dtNew1.Rows.Count > 0)
                                {
                                    Int32 MaxClass = Convert.ToInt32(dtNew1.Rows[0]["MaxClass"].ToString());
                                    if (MainClass > MaxClass)
                                    {
                                        if (Session["SchoolLevel"].ToString() == "1")
                                        {
                                            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Class 1 to 5')</script>", false);
                                            return false;
                                        }
                                        else if (Session["SchoolLevel"].ToString() == "2")
                                        {
                                            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Class 1 to 8')</script>", false);

                                            return false;
                                        }
                                        else if (Session["SchoolLevel"].ToString() == "3")
                                        {
                                            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Class 1 to 10')</script>", false);


                                            return false;
                                        }
                                        
                                        else
                                        {
                                            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Class 1 to 12 ')</script>", false);

                                            return false;
                                        }
                                    }
                                }
                                else
                                {
                                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select valid School')</script>", false);


                                    //dDOB.Style.BackColor = Color.Red;
                                    return false;
                                }
                            }
                        }
                    }
                    DateTime date1 = Convert.ToDateTime(txtDobDate.Text);
                    DateTime date2 = Convert.ToDateTime(txtBirth.Text);
                    // int daysDiff = ((TimeSpan)(date2 - date1)).Days;
                    TimeSpan timeSpan = date2 - date1;

                    decimal finalResult = 0;
                    finalResult = Convert.ToDecimal(timeSpan.TotalDays / 365);
                    if (finalResult < 3)
                    {
                        ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please ensure Date of Birth and Date of Enrollment difference of 3 years')</script>", false);

                        //dDOB.Style.BackColor = Color.Red;
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


