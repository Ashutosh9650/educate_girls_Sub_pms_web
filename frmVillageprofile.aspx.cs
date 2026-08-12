using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Globalization;
using System.Drawing.Imaging;
using System.Drawing;
using System.IO;
using System.Data.SqlClient;

public partial class frmVillageprofile : System.Web.UI.Page
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
               
                LoadYear();
                LoadUserLeavel();
               
               
                fillcombos("C", ddlMainCaste1);
                fillcombos("C", ddlMainCastes2);
                fillcombos("C", ddlMainCastes3);
                fillcombos("O", ddlPrimaryOccupation);
                fillcombos("O", ddlSecondaryOccupation);
                fillcombos("O", ddlOtherOccupation);
                fillcombos("S", ddlConectivityfromMainRoad);
                fillcombos("S", ddlElect);
                fillcombos("S", ddlAvailablity);
                fillcombos("WS", ddlSourceofdrinkingwater);
                fillcombos("TM", ddlModeoftrans);

                ValdateUserLavel();
               
            //                 string strQry = " WITH TempEmp (Name,serial,duplicateRecCount) ";
            //strQry += "AS   ";
            //strQry += "(  ";

            //strQry += "SELECT villagecode,serial,ROW_NUMBER() OVER(PARTITION by villagecode, serial ORDER BY villagecode)  ";
            //strQry += "AS duplicateRecCount  ";
            //strQry += "FROM dbo.tblDTD)  ";

            //strQry += "select * FROM TempEmp  ";
        ////        string strQry = "";
        ////        string strQry1 = "";
        //// strQry += "select UniqueCode,TblDTD.VillageCode from TblDTD inner join mst5Village on mst5Village.VillageCode=TblDTD.VillageCode where mst5Village.Fyear='2018-2019' and EnrollStatus=3 ";
        //// strQry += " and mst5Village.DistrictCode in('E421AA06278E498DB71B2008D') and TempCode is null ";
        //////      strQry += " '9FB8162671E74496A7E7D9CD1','C11F29F9A2A34535A6E26015F','DF6B2F4279794DACA197F2217','E421AA06278E498DB71B2008D') ";

                //string strQry = "select distinct tbldtd.VillageCode  from tbldtd inner join mst5Village on mst5Village.VillageCode=tbldtd.VillageCode  where mst5Village.EGVillageCode in(select distinct EGVillageCode from mst5Village where   OldUniqueCode='' ) and EnrollStatus=3  and Fyear='2019-2020'";
                //string strQry = " select tbldtd.VillageCode,UniqueCode from tbldtd with(nolock) where TempCode='77777'  order by VillageCode  ";
                //DataTable dtRole = objMain.LoadData(strQry);
                //foreach (DataRow dr in dtRole.Rows)
                //{
                //    //    string strQry2 = "select UniqueCode from tbldtd where VillageCode ='" + dr["VillageCode"] + "' and EnrollStatus=3";

                //    //    DataTable dtRole5 = objMain.LoadData(strQry2);
                //    //    foreach (DataRow dr1 in dtRole5.Rows)
                //    //    {
                //    Int32 icount = 0;
                //    string strQry1 = "Select MAX(serial) + 1  as SrNo from tblDTD where  VillageCode in(select VillageCode from mst5Village where EGVillageCode in(select EGVillageCode from mst5Village where VillageCode='" + dr["VillageCode"].ToString() + "'))   ";

                //    DataTable dtRole2 = objMain.LoadData(strQry1);
                //    icount = Convert.ToInt32(dtRole2.Rows[0]["SrNo"]);

                //    string StudentTSInsertQuery = " Update tblDTD set Serial =" + icount + ",TempCode=77 where UniqueCode ='" + dr["UniqueCode"].ToString() + "' and EnrollStatus=3 ";
                //    bool InsertTS = objMain.AddUpdate(StudentTSInsertQuery);
                //    //    }

                //}


        ////            Int32 icount = 0;
        ////    foreach (DataRow dr in dtRole.Rows)
        ////    {


        ////        strQry1 = "Select MAX(serial) + 1  as SrNo from tblDTD where  VillageCode in(select VillageCode from mst5Village where OldUniqueCode in(select OldUniqueCode from mst5Village where VillageCode='" + dr["VillageCode"].ToString() + "'))   ";
        ////            DataTable dtRole2 = objMain.LoadData(strQry1);
        ////            icount = Convert.ToInt32(dtRole2.Rows[0]["SrNo"]);



        ////            string StudentTSInsertQuery = "";
        ////            StudentTSInsertQuery += " Update tblDTD set Serial =" + icount + ",TempCode=4 where UniqueCode ='" + dr["UniqueCode"].ToString() + "' and EnrollStatus=3 ";
        ////            bool InsertTS = objMain.AddUpdate(StudentTSInsertQuery);
        ////        }

//}
       //           SqlParameter[] parm = new SqlParameter[]
       //     {
       //new SqlParameter("@ff",  conditions),
   
      
       //          };
       // DataTable dtRole = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "TempLat", parm);

                //string strQry = " with a as( ";

                //strQry += "   SELECT  Row_number() OVER (PARTITION BY GUID_School ORDER BY GUID_School)R,tblActivityUpdate_SchoolTemp. * from tblActivityUpdate_SchoolTemp ) ";
                //strQry += "   select * from a where R>1 ";
               //string strQry = "select Village_GeoLocation,EGVillageCode  from mst5Village where DistrictCode='0CBCEA2413C14499A96F10F8B' ";
               // DataTable dtRole = objMain.LoadData(strQry);
               // foreach (DataRow dr in dtRole.Rows)
               // {
               //     string villeo = dr["dd"].ToString();
               //     string[] a = villeo.Split(',');


               //     string StudentTSInsertQuery = " UPDATE EnrollmentAchiment SET LatLogNew =( geography::STPointFromText('POINT('+ CAST(" + a[0] + " AS VARCHAR(20)) + ' ' + CAST(" + a[1] + " AS VARCHAR(20)) + ')'  , 4326)) where VillageCode ='" + dr["EGVillagecode"].ToString() + "'";
               //     bool InsertTS = objMain.AddUpdate(StudentTSInsertQuery);
               // }
                //foreach (DataRow dr in dtRole.Rows)
                //{

                //    string StudentTSInsertQuery = " delete  from tblActivityUpdate_SchoolTemp where UID ='" + dr["UID"].ToString() + "'   ";
                //    bool InsertTS = objMain.AddUpdate(StudentTSInsertQuery);
                // }
               // string strQry = " with a as( ";

               // strQry += "SELECT [RN] = Row_number() OVER (PARTITION BY tblDTD.villagecode,tblDTD.serial ORDER BY tblDTD.villagecode,tblDTD.serial) ";
               // strQry += ",tblDTD.villagecode,UniqueCode,tblDTD.Serial ";
              // string  strQry ="";
    //            strQry = "select mst5Village.EGVillageCode,Serial,mst5Village.VillageCode,UniqueCode,mst5Village.OldUniqueCode  from tblDTD ";
    //            strQry += " inner join mst5Village on mst5Village.VillageCode=tblDTD.VillageCode ";
    //            strQry += " where tblDTD.VillageCode in('F3D1BA5027074D9CBBFA575FA','F54BB71283634BA2BEA02BC12','F663BD59023245FAAD0291886','F668D170C18D4C79A1154C031','F6A67BE7E49B40279D18EF7FA','F6FA07C6D0024DE3893C71ABE','F729A9AB63F04103B6EE194A2','F74FAC0C30F044C49C06C4C47','F7607EC7ECA047CB901458FDD','F88E932E9B7E4C3DB7D9F4C6B','F975C5AFC91946AE8080176A5','FA270D93AE694F5ABBD92DA7D','FA2C058E89E04C3C8B0470BD6','FB9C08B0D3B04BC2A7402105C','FCB50C7E37A44AF19C69948D2','FCB6EA4C9B7F4BF8B44C24723','FD29BBE522614252B2F2AD59C','FD77895646414FFA8F3AF90AD','FDA93E8EC8F64C9D9F5E59118','FE6022FEBF784D67A6F44B171','FEA35CB5D9014860B054D904D','FEC499AB13F342E7B12FB3479','FF14FAEFF57148E49846F86FD','FF434D216B6444E2AA3A9B07C','FF4C16EAD3424B329A69C09B6','FF695A0EBD374243B620C03F6','FF6CA6276D7E4A3EB0E6F5F95','FFBF51BA68D34ED0B37B9A949','FFDB533DED9745328DD96D089' )";
 

    //strQry += " order by mst5Village.VillageCode";

               // strQry += "from tblDTD ";
               // strQry += " inner join ";
               // strQry += " (select VillageCode,Serial ,count(serial)cnt  from tblDTD where villagecode in (select villagecode from mst5village where DistrictCode='5DF805DEA50343589EB6FB923') group by VillageCode,Serial having COUNT(serial)>1)dup on tblDTD.VillageCode=dup.villagecode and tblDTD.Serial=dup.serial  ) ";
               // strQry += "  select * from a where RN>1 ";
               // DataTable dtRole = objMain.LoadData(strQry);

               // ////string strQry = "select UniqueCode,VillageCode,Serial from tbldtd where  Serial=0";
            //  DataTable dtRole = objMain.LoadData(strQry);

            //    int icount = 0;
            //    string villagecode = string.Empty;
            //    foreach (DataRow dr in dtRole.Rows)
            //    {
            //    string str = "";
            //    if (dr["OldUniqueCode"].ToString().Length > 1)
            //    {
            //        if (icount == 0)
            //        {
            //              string strQry1 = "Select villagecode,MAX(serial) + 1  as SrNo from tblDTD where VillageCode ='" + dr["OldUniqueCode"] + "' group by VillageCode  ";
            //            DataTable dtRole1 = objMain.LoadData(strQry1);
            //            icount = Convert.ToInt32(dtRole1.Rows[0]["SrNo"]);
                   
                        
            //        }
            //        if (villagecode == dr["VillageCode"].ToString())
            //        {
                      
            //                 icount = icount + 1;
                        
            //        }
            //        else
            //        {
            //            #region VillageChange
            //            string strQry1 = "Select villagecode,MAX(serial) + 1  as SrNo from tblDTD where VillageCode ='" + dr["OldUniqueCode"] + "' group by VillageCode  ";
            //            DataTable dtRole1 = objMain.LoadData(strQry1);
            //            icount = Convert.ToInt32(dtRole1.Rows[0]["SrNo"]);
                   
            //            #endregion
            //        }
            //        villagecode = dr["VillageCode"].ToString();


            //        string StudentTSInsertQuery = "";
            //        StudentTSInsertQuery += " Update tblDTD set Serial =" + icount + ",TempCode='159' where UniqueCode ='" + dr["UniqueCode"].ToString() + "'  ";
            //        bool InsertTS = objMain.AddUpdate(StudentTSInsertQuery);
            //    }

            //}  

                ////foreach (DataRow dr1 in dtRole.Rows)
                ////{
                //Int32 icount = 0;
                //string strQry1 = "Select * from tbldtd where Serial=0  ";
                //DataTable dtRole1 = objMain.LoadData(strQry1);
                //foreach (DataRow dr in dtRole1.Rows)
                //{

                //    strQry1 = "Select MAX(serial) + 1  as SrNo from tblDTD  left join  mst5Village  on mst5Village.VillageCode=[tblDTD].VillageCode or mst5Village.OldUniqueCode=[tblDTD].VillageCode   or mst5Village.RefVillageCode=[tblDTD].VillageCode  where tblDTD.VillageCode ='" + dr["Villagecode"] + "'   ";
                //    DataTable dtRole2 = objMain.LoadData(strQry1);
                //    icount = Convert.ToInt32(dtRole2.Rows[0]["SrNo"]);



                //    string StudentTSInsertQuery = "";
                //    StudentTSInsertQuery += " Update tblDTD set Serial =" + icount + " where UniqueCode ='" + dr["UniqueCode"].ToString() + "' and EnrollStatus <>2 ";
                //    bool InsertTS = objMain.AddUpdate(StudentTSInsertQuery);
                //}

                ////}

            }
            else
            {
                Response.Redirect("Login.aspx", false);
                  
            }
        }

    }
    public void SaveImage()
    {
        #region UploadImage 
        string Fullfilename = "";
        string Fullfilename1 = "";
        if (FileuploadAttach.PostedFile != null && FileuploadAttach.PostedFile.FileName != "")
        {
            string ext = System.IO.Path.GetExtension(FileuploadAttach.PostedFile.FileName).ToLower();
            if (FileuploadAttach.PostedFile.ContentLength < 202400)
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
            Fullfilename = "" + txtVillageCode.Text + "_" + DateTime.Now.ToString("ddMMyyyy_hhmmss") + exten;

            string sFileDir = Server.MapPath("~/DataBackup/");
            string fullpathh = sFileDir + Fullfilename;
            if (FileuploadAttach.PostedFile != null && FileuploadAttach.PostedFile.FileName != "")
            {
              
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

            string sFileDir1 = Server.MapPath("~/ImportExcel/");
            Fullfilename1 = sFileDir1 + Fullfilename;
            Bitmap bmp1 = new Bitmap(fullpathh);


            ImageCodecInfo jgpEncoder = GetEncoder(ImageFormat.Jpeg);


            System.Drawing.Imaging.Encoder myEncoder =
            System.Drawing.Imaging.Encoder.Quality;

            EncoderParameters myEncoderParameters = new EncoderParameters(1);

            EncoderParameter myEncoderParameter = new EncoderParameter(myEncoder, 50L);

            myEncoderParameter = new EncoderParameter(myEncoder, 0L);
            myEncoderParameters.Param[0] = myEncoderParameter;

            bmp1.Save(Fullfilename1, jgpEncoder, myEncoderParameters);

        }



        #endregion


    }
    private ImageCodecInfo GetEncoder(ImageFormat format)
    {

        ImageCodecInfo[] codecs = ImageCodecInfo.GetImageDecoders();

        foreach (ImageCodecInfo codec in codecs)
        {
            if (codec.FormatID == format.Guid)
            {
                return codec;
            }
        }
        return null;
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
          
           
        }
        else
        {
            ddlState.SelectedIndex = 0;
            ddlDistrict.Items.Clear();
            ddlBlock.Items.Clear();
            ddlPanchayat.Items.Clear();
           
        }

     
       
    }
    public void Locking()
    {
        if (ddlYear.SelectedIndex > 0)
        {

            btnsave.Enabled = true;
            btnSUmbit.Enabled = true;
            string strQry;
            strQry = "Select * from mstModuleLocking  where [FromName]='VIP' and DistrictCode='" + ddlDistrict.SelectedValue + "' and Fyear='" + ddlYear.SelectedItem.Text + "'";

            if (Session["FinYear"].ToString() != ddlYear.SelectedItem.Text)
            {
                DataTable dtModel = objMain.LoadData(strQry);
                if (dtModel.Rows.Count > 0)
                {
                    if (Convert.ToInt32(dtModel.Rows[0]["LockMonth"].ToString()) < DateTime.Today.Month)
                    {
                        btnsave.Enabled = false;
                        btnSUmbit.Enabled = false;
                        btnDelete.Enabled = false;

                    }

                }

            }
        }
    }
    public void ValdateUserLavel()
    {

        string strQry = "";
        string Cond = "Module='VIllageProfile' ";
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

            btnDelete.Visible = true;
        }

        if (vADD == true)
        {
            btnAdd.Enabled = true;
            btnsave.Enabled = true;
            lblMain.Text = "Village Profile";
        }
        else
        {
            btnAdd.Enabled = false;
            btnsave.Enabled = false;
        }
       
        if (vVerify == true)
        {

            btnsave.Enabled = true;

            lblMain.Text = "Village Information(Verify)";
          
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
            conditions = "StateCode ='" + ddlState.SelectedValue + "' and Fyear= '" + ddlYear.SelectedItem.Text + "'  ";
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

            ddlDistrict.SelectedIndex = 1;
            ddlDistrict_SelectedIndexChanged(ddlDistrict, null);
            //ddlDistrict.SelectedIndex = 1;
            //conditions = "";
            //conditions = "DistrictCode ='" + ddlDistrict.SelectedValue + "' and BlockCode ='" + Session["BlockCode"].ToString() + "' ";
            //objComman.BindDLL("mst3Block", "BlockCode,dbo.TitleCase(upper(BlockName)) as BlockName  ", conditions, "BlockName", "asc", ddlBlock, "BlockName", "BlockCode", "--Select--");

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
        DateTime GivenDate = DateTime.Now;
        int GivenYear = GivenDate.Year;
        int m = GivenDate.Month;

        DataTable dt = null;
        //ddlYear.Items.Add("--Select--","0");
        int y = GivenDate.Year;
        string strQry = "";
        if (Session["NewDistrictCode"].ToString() == "7E673ED1107241C696C6954C2" ||  Session["NewDistrictCode"].ToString() == "B2CDC6AC58C741749E866A3BA" || Session["NewDistrictCode"].ToString() == "51A6384B637749D399E599219")
        {
            strQry = "Select Min(Fyear) as Type,0 as ID from mst2District  where [DistrictCode]='" + Session["NewDistrictCode"].ToString() + "'   ";
        }
        else
        {
            strQry = "Select Min(Fyear) as Type,0 as ID from mst2District  where [DistrictCode] in(" + Session["DistrictCode"].ToString() + ")   ";
        }

        DataTable dtFyear = objMain.LoadData(strQry);
        //DateTime GivenDate1 = DateTime.Now;
        //int GivenYear1 = GivenDate1.Year;
        DataTable dtYear = CreateDataTable();
        //    DataTable dtYear=null;
        DataRow dr;
        if (dtFyear.Rows.Count > 0)
        {
            if (ddlYear.SelectedIndex < 0)
            {

                //string mYear1 = GivenYear1.ToString();
                for (int i = 0; i < dtFyear.Rows.Count; i++)
                {
                    string[] LineData;
                    string MfYear = dtFyear.Rows[i]["Type"].ToString();
                    char Seperator = '-';
                    LineData = MfYear.Split(Seperator);
                    int idd = 0;
                    
                    dr = dtYear.NewRow();
                    dr["Type"] = dtFyear.Rows[i]["Type"].ToString();
                    if (LineData[0].ToString() == "")
                    {
                    }
                    else
                    {
                        dr["ID"] = LineData[0];
                    }
                 
                    dtYear.Rows.Add(dr);
                    //if (m > 3)
                    //{
                    //    dr = dtYear.NewRow();
                    //    dr["Type"] = GivenYear.ToString() + "-" + Convert.ToString((GivenYear + 1));
                    //    dr["ID"] = y;
                    //    dtYear.Rows.Add(dr);
                    //    dr = dtYear.NewRow();
                    //    dr["Type"] = GivenYear - 1 + "-" + Convert.ToString((GivenYear - 1 + 1));
                    //    dr["ID"] = y - 1;
                    //    dtYear.Rows.Add(dr);
                    //    //get last  two digits (eg: 10 from 2010);

                    //}
                    //else
                    //{
                    //    dr = dtYear.NewRow();
                    //    dr["Type"] = Convert.ToString((y - 1)) + "-" + y.ToString();
                    //    //y = y - 1;
                    //    dr["ID"] = y - 1;

                    //    dtYear.Rows.Add(dr);


                    //}

                }

            }
        }
        objComman.BindDLLMasterTable("mstSchool", "Type,ID", dtYear, conditions, "Type", "asc", ddlYear, "Type", "ID", "Select");
        if (dtYear.Rows.Count > 0)
        {
            ddlYear.SelectedIndex = 1;
            //}

            ddlYear_SelectedIndexChanged(ddlYear, null);
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
            conditions = "StateCode ='" + ddlState.SelectedValue + "'   and mst2District.FYear ='" + ddlYear.SelectedItem.Text + "'";
        }
        else
        {
            conditions = "StateCode ='" + ddlState.SelectedValue + "' and DistrictCode in(" + Session["DistrictCode"].ToString() + ") and mst2District.FYear ='" + ddlYear.SelectedItem.Text + "' ";


        }


        objComman.BindDLL("mst2District", "DistrictCode,dbo.TitleCase(upper(DistrictName)) as DistrictName ", conditions, "DistrictName", "asc", ddlDistrict, "DistrictName", "DistrictCode", "Select");



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
        objComman.BindDLL("mstPanchayat", "PanchayatCode,dbo.TitleCase(upper(PanchayatName)) as PanchayatName", conditions, "PanchayatName", "asc", ddlPanchayat, "PanchayatName", "PanchayatCode", "--Select--");
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
            str = str + " and mst5Village.DistrictCode='" + ddlDistrict.SelectedValue.ToString() + "'";
        }

        if (ddlBlock.SelectedValue != null && ddlBlock.SelectedIndex > 0)
        {
            str = str + " and mst5Village.BlockCode='" + ddlBlock.SelectedValue.ToString() + "'";
        }

        if (ddlPanchayat.SelectedValue != null && ddlPanchayat.SelectedIndex > 0)
        {
            str = str + " and mst5Village.PanchayatCode='" + ddlPanchayat.SelectedValue.ToString() + "'";
        }

       
        DataTable dtVilllage = null;
        dtVilllage = objMain.LoadData("Select EGVillageCode as VillageCode, VillageCode as NewVillageCode,VillageName  from mst5Village " + str + " ");
        GvVillage.DataSource = dtVilllage;
        GvVillage.DataBind();
        ViewState["Serach"] = dtVilllage;
      
    }
    private void RefreshControl()
    {
        txtDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
        txtSarpanch.Text = "";  
         TxtDistance.Text = ""; ddlMainCaste1.SelectedIndex = 0; ddlMainCastes2.SelectedIndex = 0; ddlMainCastes3.SelectedIndex = 0; ddlPrimaryOccupation.SelectedIndex = 0;
        ddlSecondaryOccupation.SelectedIndex = 0; ddlOtherOccupation.SelectedIndex = 0; txtTotalHouseholds.Text = ""; txtNoofAnganwari.Text = "";  ddlConectivityfromMainRoad.SelectedIndex = 0;
        TxtGovt1.Text = ""; TxtGovt2.Text = ""; TxtGovt3.Text = ""; TxtGovt4.Text = ""; TxtGovt5.Text = ""; TxtPvt1.Text = ""; TxtPvt2.Text = ""; TxtPvt3.Text = ""; TxtPvt4.Text = "";
        TxtPvt5.Text = ""; TxtPvt6.Text = ""; TxtCont.Text = ""; TxtHall.Text = ""; TxtHospital.Text = ""; TxtMarket.Text = ""; ddlElect.SelectedIndex = 0; ddlSourceofdrinkingwater.SelectedIndex = 0;
        TxtYouth.Text = ""; ddlAvailablity.SelectedIndex = 0; TxtBank.Text = ""; TextBox1.Text = "";
        ddlModeoftrans.SelectedIndex = 0;
        TxtDhani.Text = "";
        txtTotalpopulation.Text = "";
        ViewState["ImagePath"] = null;

    }
    public void SavaData()
    {

        string distcode = "", blockcode = "", clustercode = "", panchayat = "", villcode = "", villname = "", sarpanchname = "", sarpanchno = string.Empty, year = "", bankname = "", nearestmarket = "";
        int caste1 = 0, caste2 = 0, caste3 = 0, primryoccupation = 0, secondaryoccupation = 0, otheroccupation = 0, totaganbadi = 0, tothousehold = 0, roadconnectivity = 0, transportmode = 0, Govt1 = 0, Pvt1 = 0, Govt2 = 0, Pvt2 = 0, Govt3 = 0, Pvt3 = 0, Govt4 = 0, Pvt4 = 0, GovtT = 0, PvtTo = 0, TotalSchool = 0, electricity = 0, watersource = 0, commmunitycount = 0, youthgrp = 0, femalegroupavail = 0;
        decimal distdistance = 0, schooldist = 0, disthospital = 0;
        int Totalpopulation = 0, Dhani = 0;
        #region UploadImage
        string Fullfilename = "";
        string Fullfilename1 = "";
        if (Convert.ToString(ViewState["ImagePath"]).Length > 7)
        {
            Fullfilename = ViewState["ImagePath"].ToString();
        }
        if (FileuploadAttach.PostedFile != null && FileuploadAttach.PostedFile.FileName != "")
        {
            string ext = System.IO.Path.GetExtension(FileuploadAttach.PostedFile.FileName).ToLower();
            if (FileuploadAttach.PostedFile.ContentLength < 302400)
            {
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Image size must be less than 2MB')</script>", false);
                return;
            }
            if (ext != ".jpeg" && ext != ".jpg" && ext != ".png" && ext != ".gif")
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Invalid Images')</script>", false);
                return;
            }
            string exten = Path.GetExtension(FileuploadAttach.PostedFile.FileName);
            Fullfilename = "" + txtVillageCode.Text + "_" + DateTime.Now.ToString("ddMMyyyy_hhmmss") + exten;

            string sFileDir = Server.MapPath("~/DataBackup/");
            string fullpathh = sFileDir + Fullfilename;
            if (FileuploadAttach.PostedFile != null && FileuploadAttach.PostedFile.FileName != "")
            {

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

            string sFileDir1 = Server.MapPath("~/ImportExcel/");
            Fullfilename1 = sFileDir1 + Fullfilename;
            Bitmap bmp1 = new Bitmap(fullpathh);


            ImageCodecInfo jgpEncoder = GetEncoder(ImageFormat.Jpeg);


            System.Drawing.Imaging.Encoder myEncoder =
            System.Drawing.Imaging.Encoder.Quality;

            EncoderParameters myEncoderParameters = new EncoderParameters(1);

            EncoderParameter myEncoderParameter = new EncoderParameter(myEncoder, 50L);

            myEncoderParameter = new EncoderParameter(myEncoder, 0L);
            myEncoderParameters.Param[0] = myEncoderParameter;

            bmp1.Save(Fullfilename1, jgpEncoder, myEncoderParameters);

        }



        #endregion
        if (txtTotalpopulation.Text != "")
        {
            Totalpopulation = Convert.ToInt32(txtTotalpopulation.Text);
        }
        if (TxtDhani.Text != "")
        {
            Dhani = Convert.ToInt32(TxtDhani.Text);
        }
        string Villageinsertupdate = "", insertupdatevillTs = "";
        if (ddlPanchayat.SelectedIndex > 0)
        {
            panchayat = ddlPanchayat.SelectedValue.ToString();
        }

        if (ddlYear.SelectedIndex > 0)
        {
            year = ddlYear.SelectedValue;
        }

        if (ddlDistrict.SelectedIndex > 0)
        {
            distcode = ddlDistrict.SelectedValue.ToString();
        }
        if (TxtVillageName.Text != "")
        {
            villname = TxtVillageName.Text;

        }
        if (ddlBlock.SelectedIndex > 0)
        {
            blockcode = ddlBlock.SelectedValue.ToString();
        }

       

        if (txtSarpanch.Text != "")
        {
            sarpanchname = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(txtSarpanch.Text.Trim());
        }

       
        if (TxtCont.Text != "")
        {
            sarpanchno = TxtCont.Text;
        }
        if (ddlMainCaste1.SelectedIndex > 0)
        {
            caste1 = Convert.ToInt32(ddlMainCaste1.SelectedValue);
        }
        if (ddlMainCastes2.SelectedIndex > 0)
        {
            caste2 = Convert.ToInt32(ddlMainCastes2.SelectedValue);
        }
        if (ddlMainCastes3.SelectedIndex > 0)
        {
            caste3 = Convert.ToInt32(ddlMainCastes3.SelectedValue);
        }
        if (TextBox1.Text != "")
        {
            schooldist = Convert.ToDecimal(TextBox1.Text);
        }
        if (ddlPrimaryOccupation.SelectedIndex > 0)
        {
            primryoccupation = Convert.ToInt32(ddlPrimaryOccupation.SelectedValue);
        }
        if (ddlSecondaryOccupation.SelectedIndex > 0)
        {
            secondaryoccupation = Convert.ToInt32(ddlSecondaryOccupation.SelectedValue);
        }
        if (ddlOtherOccupation.SelectedIndex > 0)
        {
            otheroccupation = Convert.ToInt32(ddlOtherOccupation.SelectedValue);
        }
        if (TxtDistance.Text != "")
        {
            distdistance = Convert.ToDecimal(TxtDistance.Text);
        }

        if (txtNoofAnganwari.Text != "")
        {
            totaganbadi = Convert.ToInt32(txtNoofAnganwari.Text);
        }

        if (txtTotalHouseholds.Text != "")
        {
            tothousehold = Convert.ToInt32(txtTotalHouseholds.Text);
        }

        if (ddlConectivityfromMainRoad.Text != "")
        {
            roadconnectivity = Convert.ToInt32(ddlConectivityfromMainRoad.SelectedValue.ToString());
        }

        if (ddlModeoftrans.Text != "")
        {
            transportmode = Convert.ToInt32(ddlModeoftrans.SelectedValue.ToString());
        }
        
        if (TxtGovt1.Text != "")
        {
            Govt1 = Convert.ToInt32(TxtGovt1.Text);
        }
        
        if (TxtPvt1.Text != "")
        {
            Pvt1 = Convert.ToInt32(TxtPvt1.Text);
        }
        if (TxtGovt2.Text != "")
        {
            Govt2 = Convert.ToInt32(TxtGovt2.Text);
        }
       
        if (TxtPvt2.Text != "")
        {
            Pvt2 = Convert.ToInt32(TxtPvt2.Text);
        }
        if (TxtGovt3.Text != "")
        {
            Govt3 = Convert.ToInt32(TxtGovt3.Text);
        }
        if (TxtPvt3.Text != "")
        {
            Pvt3 = Convert.ToInt32(TxtPvt3.Text);
        }

        if (TxtGovt4.Text != "")
        {
            Govt4 = Convert.ToInt32(TxtGovt4.Text);
        }
        if (TxtGovt5.Text != "")
        {
            Govt4 = Convert.ToInt32(TxtGovt5.Text);
        }
        if (TxtPvt4.Text != "")
        {
            Pvt4 = Convert.ToInt32(TxtPvt4.Text);

        }
        if (TxtPvt5.Text != "")
        {
            PvtTo = Convert.ToInt32(TxtPvt5.Text);
        }
            

        if (ddlElect.SelectedIndex > 0)
        {
            electricity = Convert.ToInt32(ddlElect.SelectedValue.ToString());
        }
        if (ddlSourceofdrinkingwater.SelectedIndex > 0)
        {
            watersource = Convert.ToInt32(ddlSourceofdrinkingwater.SelectedValue.ToString());
        }
        if (TxtHall.Text != "")
        {
            commmunitycount = Convert.ToInt32(TxtHall.Text);
        }
        if (TxtYouth.Text != "")
        {
            youthgrp = Convert.ToInt32(TxtYouth.Text);
        }
        if (ddlAvailablity.SelectedIndex > 0)
        {
            femalegroupavail = Convert.ToInt32(ddlAvailablity.SelectedValue.ToString());
        }
        if (TxtHospital.Text != "")
        {
            disthospital = Convert.ToDecimal(TxtHospital.Text);
        }
        if (TxtBank.Text != "")
        {
            bankname = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(TxtBank.Text.Trim());
        }
        if (TxtMarket.Text != "")
        {
            nearestmarket = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(TxtMarket.Text.Trim());
        }

        bool insert=false;
        if (ViewState["hdnFlag"].ToString() == "U")
        {
            //if (vADD == true)
            //{
            //    insertupdatevillTs = "Update mstVillageTS set [Year]=" + year + ",SarpanchName='" + sarpanchname + "',SarpanchName1='" + sarpanchname + "',SarpanchName2='" + sarpanchname + "',SarpanchContact='" + sarpanchno + "',MainCaste11=" + caste1 + ",SarpanchContact1='" + sarpanchno + "',SarpanchContact2='" + sarpanchno + "',MainCaste1=" + caste1 + ",MainCaste2=" + caste2 + ",MainCaste3=" + caste3 + ",MainCaste22=" + caste2 + ",MainCaste33=" + caste3 + ",Occupation1=" + primryoccupation + ",Occupation2=" + secondaryoccupation + ",Occupation3= " + otheroccupation + ",DistanceDistrictHQ=" + distdistance + ",DistanceDistrictHQ1=" + distdistance + ",DistanceDistrictHQ2=" + distdistance + ",NoAnganwadi=" + totaganbadi + ",NoAnganwadi1=" + totaganbadi + ",NoAnganwadi2=" + totaganbadi + ",TotalHH=" + tothousehold + ",TotalHH1=" + tothousehold + ",TotalHH2=" + tothousehold + ",ConnectivityMainRoad=" + roadconnectivity + ",ConnectivityMainRoad1=" + roadconnectivity + ",ConnectivityMainRoad2=" + roadconnectivity + ",ModeTransport=" + transportmode + ",ModeTransport1=" + transportmode + ",ModeTransport2=" + transportmode + ",DistanceSchool=" + schooldist + ", DistanceSchool1=" + schooldist + ",DistanceSchool2=" + schooldist + ",Govt_PS='" + Govt1 + "',Govt_PS1='" + Govt1 + "',Govt_PS2='" + Govt2 + "',Govt_UPS='" + Govt2 + "',Govt_UPS1='" + Govt2 + "',Govt_UPS2='" + Govt2 + "',Govt_SS1='" + Govt3 + "',Govt_SS2='" + Govt3 + "',Govt_SS='" + Govt3 + "',Govt_USS='" + Govt4 + "',Govt_USS1='" + Govt4 + "',Govt_USS2='" + Govt4 + "',Govt_Total='" + GovtT + "',Govt_Total1='" + GovtT + "',Govt_Total2='" + GovtT + "',Pvt_PS='" + Pvt1 + "',Pvt_PS1='" + Pvt1 + "',Pvt_PS2='" + Pvt1 + "',Pvt_UPS='" + Pvt2 + "',Pvt_UPS1='" + Pvt2 + "',Pvt_UPS2='" + Pvt2 + "',Pvt_SS='" + Pvt3 + "',Pvt_SS1='" + Pvt3 + "',Pvt_SS2='" + Pvt3 + "',Pvt_USS='" + Pvt4 + "',Pvt_USS1='" + Pvt4 + "',Pvt_USS2='" + Pvt4 + "',Pvt_Total='" + PvtTo + "',Pvt_Total1='" + PvtTo + "',Pvt_Total2='" + PvtTo + "',Electricity='" + electricity + "',Electricity1='" + electricity + "',Electricity2='" + electricity + "',DrinkingWaterSource='" + watersource + "',DrinkingWaterSource1='" + watersource + "',DrinkingWaterSource2='" + watersource + "',NoCommunityCentre='" + commmunitycount + "',NoCommunityCentre1='" + commmunitycount + "',NoCommunityCentre2='" + commmunitycount + "',NoYouthGroup='" + youthgrp + "',NoYouthGroup1='" + youthgrp + "',NoYouthGroup2='" + youthgrp + "',AvailabilityFemaleGroup='" + femalegroupavail + "',AvailabilityFemaleGroup1='" + femalegroupavail + "',AvailabilityFemaleGroup2='" + femalegroupavail + "',DistanceHospital='" + disthospital + "',DistanceHospital1='" + disthospital + "',DistanceHospital2='" + disthospital + "',NearestBank='" + bankname + "',NearestBank1='" + bankname + "',NearestBank2='" + bankname + "',NearestMarket='" + nearestmarket + "',NearestMarket1='" + nearestmarket + "',NearestMarket2='" + nearestmarket + "'   where VillageCode ='" + ViewState["VillageCode"] + "'";
            //    insert= objMain.AddUpdate(insertupdatevillTs);
            //}
            //else if (vVerify == true)
            //{
            string DeleteVillageinsertupdate = "delete from  mstVillageDhani  where VillageCode='" + ViewState["VillageCode"].ToString() + "' ";
            bool InsertD = objMain.AddUpdate(DeleteVillageinsertupdate);
            DataTable dtFali = ViewState["ECurrentTable"] as DataTable;
            for (int i = 0; i < dtFali.Rows.Count-1; i++)
            {

                string UNICOde = objMain.Generate_RandomString(8);
                string InsertVillageDhabniinsertupdate = "insert into mstVillageDhani(VillageCode,DhaniName,VillageGUID) values('" + ViewState["VillageCode"].ToString() + "','" + dtFali.Rows[i]["DhaniName"].ToString() + "','" + UNICOde + "' ) ";
                bool habni = objMain.AddUpdate(InsertVillageDhabniinsertupdate);

            }

                string Val = "2";
                insertupdatevillTs = "Update mstVillageTS set ImagePath='" + Fullfilename + "', [TotalFali]=" + Dhani + ",Totalpopulation =" + Totalpopulation + ",SarpanchName1='" + sarpanchname + "',MainCaste1=" + caste1 + ",MainCaste11=" + caste1 + ",MainCaste22=" + caste2 + ",MainCaste33=" + caste3 + ",Occupation1=" + primryoccupation + ",DistanceDistrictHQ1=" + distdistance + ",NoAnganwadi1=" + totaganbadi + ",TotalHH1=" + tothousehold + ",ConnectivityMainRoad1=" + roadconnectivity + ",ModeTransport1=" + transportmode + ",Occupation11=" + primryoccupation + ",Occupation22=" + secondaryoccupation + ",Occupation33= " + otheroccupation + ",DistanceSchool1=" + schooldist + ",Govt_PS1='" + Govt1 + "',Govt_UPS1='" + Govt2 + "',Govt_SS1='" + Govt3 + "',Govt_USS1='" + Govt4 + "',Govt_Total1='" + GovtT + "',Pvt_PS1='" + Pvt1 + "',Pvt_UPS1='" + Pvt2 + "',Pvt_SS1='" + Pvt3 + "',Pvt_USS1='" + Pvt4 + "',Pvt_Total1='" + PvtTo + "',Electricity1='" + electricity + "',DrinkingWaterSource1='" + watersource + "',NoCommunityCentre1='" + commmunitycount + "',NoYouthGroup1='" + youthgrp + "',AvailabilityFemaleGroup1='" + femalegroupavail + "',DistanceHospital1='" + disthospital + "',NearestBank1='" + bankname + "',NearestMarket1='" + nearestmarket + "',Status='" + Val + "'   where VillageCode ='" + ViewState["VillageCode"] + "'";
                insert = objMain.AddUpdate(insertupdatevillTs);
            //}

             

            //Villageinsertupdate = "Update mst5Village set  [VillageName]='" + villname + "' where VillageCode='" + ViewState["VillageCode"] + "' ";
            //bool InsertTS = objMain.AddUpdate(Villageinsertupdate);
            if ( insert == true)
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Saved sucessfully')</script>", false);  
            }
        }

        if (ViewState["hdnFlag"].ToString() == "I")
        {


            insertupdatevillTs = "INSERT INTO mstVillageTS(VillageCode,[Year],SarpanchName,SarpanchName1,SarpanchName2,SarpanchContact,SarpanchContact1,SarpanchContact2,MainCaste11,MainCaste1,MainCaste2,MainCaste22,MainCaste3,MainCaste33,Occupation11,Occupation1,Occupation22,Occupation2,Occupation33,Occupation3,DistanceDistrictHQ,DistanceDistrictHQ1,DistanceDistrictHQ2,NoAnganwadi,NoAnganwadi1,NoAnganwadi2,TotalHH,TotalHH1,TotalHH2,ConnectivityMainRoad,ConnectivityMainRoad1,ConnectivityMainRoad2,ModeTransport,ModeTransport1,ModeTransport2,DistanceSchool,DistanceSchool1,DistanceSchool2,Govt_PS,Govt_PS1,Govt_PS2,Govt_UPS,Govt_UPS1,Govt_UPS2,Govt_SS,Govt_SS1,Govt_SS2,Govt_USS,Govt_USS1,Govt_USS2,Govt_Total,Govt_Total1,Govt_Total2,Pvt_PS,Pvt_PS1,Pvt_PS2,Pvt_UPS,Pvt_UPS1,Pvt_UPS2,Pvt_SS,Pvt_SS1,Pvt_SS2,Pvt_USS,Pvt_USS1,Pvt_USS2,Pvt_Total,Pvt_Total1,Pvt_Total2,Electricity,Electricity1,Electricity2,DrinkingWaterSource,DrinkingWaterSource1,DrinkingWaterSource2,NoCommunityCentre,NoCommunityCentre1,NoCommunityCentre2,NoYouthGroup,NoYouthGroup1,NoYouthGroup2,AvailabilityFemaleGroup,AvailabilityFemaleGroup1,AvailabilityFemaleGroup2,DistanceHospital,DistanceHospital1,DistanceHospital2,NearestBank,NearestBank1,NearestBank2,NearestMarket,NearestMarket1,NearestMarket2,Status,TotalFali,Totalpopulation,ImagePath) values('" + ViewState["VillageCode"].ToString() + "','" + year + "','" + sarpanchname + "','" + sarpanchname + "','" + sarpanchname + "','" + sarpanchno + "','" + sarpanchno + "','" + sarpanchno + "','" + caste1 + "','" + caste1 + "','" + caste2 + "','" + caste2 + "','" + caste3 + "','" + caste3 + "','" + primryoccupation + "','" + primryoccupation + "','" + secondaryoccupation + "','" + secondaryoccupation + "','" + otheroccupation + "','" + otheroccupation + "','" + distdistance + "','" + distdistance + "','" + distdistance + "','" + totaganbadi + "','" + totaganbadi + "','" + totaganbadi + "','" + tothousehold + "','" + tothousehold + "','" + tothousehold + "','" + roadconnectivity + "','" + roadconnectivity + "','" + roadconnectivity + "','" + transportmode + "','" + transportmode + "','" + transportmode + "','" + schooldist + "','" + schooldist + "','" + schooldist + "','" + Govt1 + "','" + Govt1 + "','" + Govt1 + "','" + Govt2 + "','" + Govt2 + "','" + Govt2 + "','" + Govt3 + "','" + Govt3 + "','" + Govt3 + "','" + Govt4 + "','" + Govt4 + "','" + Govt4 + "','" + GovtT + "','" + GovtT + "','" + GovtT + "','" + Pvt1 + "','" + Pvt1 + "','" + Pvt1 + "','" + Pvt2 + "','" + Pvt2 + "','" + Pvt2 + "','" + Pvt3 + "','" + Pvt3 + "','" + Pvt3 + "','" + Pvt4 + "','" + Pvt4 + "','" + Pvt4 + "','" + PvtTo + "','" + PvtTo + "','" + PvtTo + "','" + electricity + "','" + electricity + "','" + electricity + "','" + watersource + "','" + watersource + "','" + watersource + "','" + commmunitycount + "','" + commmunitycount + "','" + commmunitycount + "','" + youthgrp + "','" + youthgrp + "','" + youthgrp + "','" + femalegroupavail + "','" + femalegroupavail + "','" + femalegroupavail + "','" + disthospital + "','" + disthospital + "','" + disthospital + "','" + bankname + "','" + bankname + "','" + bankname + "','" + nearestmarket + "','" + nearestmarket + "','" + nearestmarket + "','" + 1 + "','" + Dhani + "','" + Totalpopulation + "','" + Fullfilename + "')";
             insert = objMain.AddUpdate(insertupdatevillTs);


             string DeleteVillageinsertupdate = "delete from  mstVillageDhani  where VillageCode='" + ViewState["VillageCode"].ToString() + "' ";
            bool InsertD = objMain.AddUpdate(DeleteVillageinsertupdate);
            DataTable dtFali = ViewState["ECurrentTable"] as DataTable;
            for (int i = 0; i < dtFali.Rows.Count-1; i++)
            {

                string UNICOde = objMain.Generate_RandomString(8);
                string InsertVillageDhabniinsertupdate = "insert into mstVillageDhani(VillageCode,DhaniName,VillageGUID) values('" + ViewState["VillageCode"].ToString() + "','" + dtFali.Rows[i]["DhaniName"].ToString() + "','" + UNICOde + "' ) ";
                bool habni = objMain.AddUpdate(InsertVillageDhabniinsertupdate);

            }
            if (insert == true)
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Saved sucessfully')</script>", false);
            }
            ViewState["hdnFlag"] = "U";
         
            
        }
    }
    #region -------- Button Click Event  ---------
    protected void btnSerach_Click(object sender, EventArgs e)
    {
        RefreshControl();
        pnlMain.Enabled = true;
        GVMainBind();
        ViewState["hdnFlag"] = "I";
      
    }   
    protected void btnAdd_Click(object sender, EventArgs e)
    {
       
        RefreshControl();
        ViewState["hdnFlag"] = "I";
        txtSarpanch.Focus();
    }
    protected void btnsave_Click(object sender, EventArgs e)
    {        
        SavaData();
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        if (ViewState["VillageCode"].ToString().Length > 3)
        {

         string   insertupdatevillTs = "delete from mstVillageTS  where VillageCode ='" + ViewState["VillageCode"].ToString() + "'";
          bool  insert = objMain.AddUpdate(insertupdatevillTs);
            if (insert == true)
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Delete sucessfully')</script>", false);
                RefreshControl();
                ViewState["hdnFlag"] = "I";
            }
        }
    }
    protected void btnSumbit_Click(object sender, EventArgs e)
    {
        btnsave_Click(sender, e);
    }
    protected void btnYearAdd_Click(object sender, EventArgs e)
    {
    }
    #endregion
    #region  -------- SelectedIndexChangedEvent  ----------
    protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
    {
        pnlMain.Enabled = false;
        pnlMain1.Enabled = false;
        FillCBDist();
    }
    protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        Locking();
        pnlMain.Enabled = false;
        pnlMain1.Enabled = false;
        FillCBBock();
    }
    protected void ddlBlock_SelectedIndexChanged(object sender, EventArgs e)
    {
        pnlMain.Enabled = false;
        pnlMain1.Enabled = false;
        FillCBCluster();
    }
    protected void ddlPanchayat_SelectedIndexChanged(object sender, EventArgs e)
    {
        pnlMain.Enabled = false;
        pnlMain1.Enabled = false;
       
    }
    protected void ddlVillage_SelectedIndexChanged(object sender, EventArgs e)
    {
        pnlMain.Enabled = false;
        pnlMain1.Enabled = false;
       // Unique();
    }
   
    #endregion
    #region GvVillage Events
    protected void GvVillage_OnRowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "GV_VIO")
        {GV_name_Add.DataSource = null;
        GV_name_Add.DataBind();
        ViewState["ECurrentTable"] = null;
            int iIndex = Convert.ToInt32(e.CommandArgument);
            string VillageCode = GvVillage.DataKeys[iIndex]["NewVillageCode"].ToString();
            ViewState["hdnFlag"] = "U";
            ViewState["VillageCode"] = VillageCode;

            fillGridName();
            FillControls(VillageCode);
           
        }
    }
   
    private void FillControls(string VillageCode)
    {
        
        string strQry = string.Empty;
        if (vVerify == true)
        {
            strQry = " Select mst5Village.VillageName,ImagePath,mst5Village.DistrictCode,TotalFali,Totalpopulation,mst5Village.BlockCode,mst5Village.PanchayatCode,ClusterCode, mstVillageTS.VillageCode,Year,SarpanchName1 as SarpanchName,SarpanchContact1 as SarpanchContact,MainCaste11 as MainCaste1,MainCaste22 as MainCaste2,MainCaste33 as MainCaste3,Occupation11 as Occupation1,Occupation22 as Occupation2,Occupation33 as Occupation3,DistanceDistrictHQ1 as DistanceDistrictHQ,NoAnganwadi1 as NoAnganwadi ,TotalHH1 as TotalHH,ConnectivityMainRoad1 as ConnectivityMainRoad,ModeTransport1 as ModeTransport,DistanceSchool1 as DistanceSchool,Govt_PS1 as Govt_PS,Govt_UPS1 as Govt_UPS,Govt_SS1 as Govt_SS,Govt_USS1 as Govt_USS,Govt_Total1 as Govt_Total,Pvt_PS1 as Pvt_PS,Pvt_UPS1 as Pvt_UPS,Pvt_SS1 as Pvt_SS,Pvt_USS1 as Pvt_USS,Pvt_Total1 as Pvt_Total,Electricity1 as Electricity,DrinkingWaterSource1 as DrinkingWaterSource,NoCommunityCentre1 as NoCommunityCentre,NoYouthGroup1 as NoYouthGroup,AvailabilityFemaleGroup1 as AvailabilityFemaleGroup,DistanceHospital1 as DistanceHospital,NearestBank1 as NearestBank,NearestMarket1 as NearestMarket, Status from mstVillageTS inner join mst5Village on mst5Village.villagecode=mstVillageTS.Villagecode where mstVillageTS.VillageCode='" + VillageCode + "' ";
        }
        else
        {
            strQry = " Select mst5Village.VillageName,ImagePath,mst5Village.DistrictCode,TotalFali,Totalpopulation,mst5Village.BlockCode,mst5Village.PanchayatCode,ClusterCode, mstVillageTS.VillageCode,Year,SarpanchName,SarpanchContact,MainCaste1,MainCaste2,MainCaste3,Occupation1,Occupation2,Occupation3,DistanceDistrictHQ,NoAnganwadi,TotalHH,ConnectivityMainRoad,ModeTransport,DistanceSchool,Govt_PS,Govt_UPS,Govt_SS,Govt_USS,Govt_Total,Pvt_PS,Pvt_UPS,Pvt_SS,Pvt_USS,Pvt_Total,Electricity,DrinkingWaterSource,NoCommunityCentre,NoYouthGroup,AvailabilityFemaleGroup,DistanceHospital,NearestBank,NearestMarket,Status from mstVillageTS inner join mst5Village on mst5Village.villagecode=mstVillageTS.Villagecode where mstVillageTS.VillageCode='" + VillageCode + "' ";

        }
        DataTable dtvillageTS = objMain.LoadData(strQry);
        string strQryMain = " Select EGVillageCode as VillageCode,VillageName,DistrictCode,BlockCode,PanchayatCode,ClusterCode from mst5Village  where VillageCode='" + VillageCode + "' ";
        DataTable dtvillageMain = objMain.LoadData(strQryMain);

        strQry = "";
        strQry = " Select VillageCode,DhaniName,VillageGUID  from mstVillageDhani where mstVillageDhani.VillageCode='" + VillageCode + "' ";

        DataTable dtFali = objMain.LoadData(strQry);
        if (dtFali.Rows.Count > 0)
        {
            GV_name_Add.DataSource = dtFali;
            GV_name_Add.DataBind();
            ViewState["ECurrentTable"] = dtFali;
        }
        if (dtvillageMain.Rows.Count > 0)
        {
            txtVillageCode.Text = dtvillageMain.Rows[0]["Villagecode"].ToString();
            TxtVillageName.Text = dtvillageMain.Rows[0]["VillageName"].ToString();
        }
        if (dtvillageTS.Rows.Count > 0)
        {
            try
            {


                //if (Convert.ToBoolean(ViewState["vADD"].ToString()) == true || Convert.ToBoolean(ViewState["vVerify"].ToString()) == true)
                //{
                //    if (Convert.ToBoolean(ViewState["vADD"].ToString()) == true)
                //    {

                //        btnsave.Enabled = true;
                           
                //    }
                //    if (Convert.ToBoolean(ViewState["vVerify"].ToString()) == true)
                //    {
                //        btnsave.Enabled = true;
                //    }
                //}
                //else
                //{
                //    btnsave.Enabled = false;
                //}

            }
            catch (Exception e)
            {
            }
           
            
            if (dtvillageMain.Rows[0]["ClusterCode"].ToString() != "")
            {
               // ddlPanchayat.SelectedValue = dtvillageMain.Rows[0]["ClusterCode"].ToString();
            }
            else
            {
               // ddlPanchayat.SelectedIndex = -1;
            }
        }
        if (dtvillageTS.Rows.Count > 0)
        {
            if (dtvillageTS.Rows[0]["Year"].ToString() != "0")
            {
                ddlYear.Text = dtvillageTS.Rows[0]["Year"].ToString();
            }
            else
            {
                ddlYear.SelectedIndex = 0;
            }
            if (dtvillageTS.Rows[0]["ImagePath"].ToString() != "")
            {
                //string sFileDir = Server.MapPath("~/images/" + dtmstM.Rows[0]["ImagePath"].ToString().Trim() + "");
                //string sFileDir = Request.PhysicalApplicationPath + "images\\";
                string imagename = dtvillageTS.Rows[0]["ImagePath"].ToString().Trim();
                ViewState["ImagePath"] = imagename;
                imgMKS.ImageUrl = ResolveUrl("~/ImportExcel/" + imagename);
            }
            else
            {
                ViewState["ImagePath"] = "";

                imgMKS.ImageUrl = null;
            }
            ViewState["hdnFlag"] = "U";
            txtSarpanch.Text = dtvillageTS.Rows[0]["SarpanchName"].ToString();
            TxtCont.Text = dtvillageTS.Rows[0]["SarpanchContact"].ToString();
            TextBox1.Text = dtvillageTS.Rows[0]["distanceSchool"].ToString(); 
            TxtDhani.Text = dtvillageTS.Rows[0]["TotalFali"].ToString();
            txtTotalpopulation.Text = dtvillageTS.Rows[0]["Totalpopulation"].ToString(); 


                       if (dtvillageTS.Rows[0]["MainCaste1"].ToString() != "0")
            {
                ddlMainCaste1.SelectedValue = dtvillageTS.Rows[0]["MainCaste1"].ToString();
            }
            else
            {
                ddlMainCaste1.SelectedIndex = 0;
            }

            if (dtvillageTS.Rows[0]["MainCaste2"].ToString() != "0")
            {
                ddlMainCastes2.SelectedValue = dtvillageTS.Rows[0]["MainCaste2"].ToString();
            }
            else
            {
                ddlMainCastes2.SelectedIndex = 0;
            }
            if (dtvillageTS.Rows[0]["MainCaste3"].ToString() != "0")
            {
                ddlMainCastes3.SelectedValue = dtvillageTS.Rows[0]["MainCaste3"].ToString();
            }
            else
            {
                ddlMainCastes3.SelectedIndex = 0;
            }
            if (dtvillageTS.Rows[0]["Occupation1"].ToString() != "0")
            {
                ddlPrimaryOccupation.SelectedValue = dtvillageTS.Rows[0]["Occupation1"].ToString();
            }
            else
            {
                ddlPrimaryOccupation.SelectedIndex = 0;
            }
            if (dtvillageTS.Rows[0]["Occupation2"].ToString() != "0")
            {
                ddlSecondaryOccupation.SelectedValue = dtvillageTS.Rows[0]["Occupation2"].ToString();
            }
            else
            {
                ddlSecondaryOccupation.SelectedIndex = 0;
            }
            if (dtvillageTS.Rows[0]["Occupation3"].ToString() != "0")
            {
                ddlOtherOccupation.SelectedValue = dtvillageTS.Rows[0]["Occupation3"].ToString();
            }
            else
            {
                ddlOtherOccupation.SelectedIndex = 0;
            }
            TxtDistance.Text = dtvillageTS.Rows[0]["DistanceDistrictHQ"].ToString();
            txtNoofAnganwari.Text = dtvillageTS.Rows[0]["NoAnganwadi"].ToString();
            txtTotalHouseholds.Text = dtvillageTS.Rows[0]["TotalHH"].ToString();
            if (dtvillageTS.Rows[0]["ConnectivityMainRoad"].ToString() != "0")
            {
                ddlConectivityfromMainRoad.SelectedValue = dtvillageTS.Rows[0]["ConnectivityMainRoad"].ToString();
            }
            else
            {
                ddlConectivityfromMainRoad.SelectedIndex = 0;
            }
            if (dtvillageTS.Rows[0]["ModeTransport"].ToString() != "0")
            {
                ddlModeoftrans.SelectedValue = dtvillageTS.Rows[0]["ModeTransport"].ToString();
            }
            else
            {
                ddlModeoftrans.SelectedIndex = 0;
            }
           // txtschooldist.Text = dtvillageTS.Rows[0]["DistanceSchool"].ToString();



            TxtGovt1.Text = dtvillageTS.Rows[0]["Govt_PS"].ToString();
            TxtPvt1.Text = dtvillageTS.Rows[0]["Pvt_PS"].ToString();
            TxtGovt2.Text = dtvillageTS.Rows[0]["Govt_UPS"].ToString();
            TxtPvt2.Text = dtvillageTS.Rows[0]["Pvt_UPS"].ToString();
            TxtGovt3.Text = dtvillageTS.Rows[0]["Govt_SS"].ToString();

            TxtPvt3.Text = dtvillageTS.Rows[0]["Pvt_SS"].ToString();

            TxtGovt4.Text = dtvillageTS.Rows[0]["Govt_USS"].ToString();
            TxtPvt4.Text = dtvillageTS.Rows[0]["Pvt_USS"].ToString();

            TxtGovt5.Text = dtvillageTS.Rows[0]["Govt_Total"].ToString();
            TxtPvt5.Text = dtvillageTS.Rows[0]["Pvt_Total"].ToString();


            if (dtvillageTS.Rows[0]["Electricity"].ToString() != "0")
            {
                ddlElect.SelectedValue = dtvillageTS.Rows[0]["Electricity"].ToString();
            }
            else
            {
                ddlElect.SelectedIndex = 0;
            }
            if (dtvillageTS.Rows[0]["DrinkingWaterSource"].ToString() != "0")
            {
                ddlSourceofdrinkingwater.SelectedValue = dtvillageTS.Rows[0]["DrinkingWaterSource"].ToString();
            }
            else
            {
                ddlSourceofdrinkingwater.SelectedIndex = 0;
            }
            if (dtvillageTS.Rows[0]["AvailabilityFemaleGroup"].ToString() != "0")
            {
                ddlAvailablity.SelectedValue = dtvillageTS.Rows[0]["AvailabilityFemaleGroup"].ToString();
            }
            else
            {
                ddlAvailablity.SelectedIndex = 0;
            }
            SumTotal();

            TxtHall.Text = dtvillageTS.Rows[0]["NoCommunityCentre"].ToString();
            TxtYouth.Text = dtvillageTS.Rows[0]["NoYouthGroup"].ToString();

            TxtHospital.Text = dtvillageTS.Rows[0]["DistanceHospital"].ToString();
            TxtBank.Text = dtvillageTS.Rows[0]["NearestBank"].ToString();
            TxtMarket.Text = dtvillageTS.Rows[0]["NearestMarket"].ToString();

        }
        else
        {
            RefreshControl();
          //  btnsave.Enabled = true;
           
            ViewState["hdnFlag"] = "I";
            
        }
    }
    public void SumTotal()
    {

        int govprimaryschool = 0, pvtprimaryschool = 0, govupperprimary = 0, pvtupperprimary = 0, govsec = 0, pvtsec = 0, govsensec = 0, pvtsensec = 0, govtot = 0, pvttot = 0, totalschool = 0;

        if (TxtGovt1.Text != "")
        {
            govprimaryschool = Convert.ToInt32(TxtGovt1.Text);
        }
        if (TxtPvt1.Text != "")
        {
            pvtprimaryschool = Convert.ToInt32(TxtPvt1.Text);
        }
        if (TxtGovt2.Text != "")
        {
            govupperprimary = Convert.ToInt32(TxtGovt2.Text);
        }
        if (TxtPvt2.Text != "")
        {
            pvtupperprimary = Convert.ToInt32(TxtPvt2.Text);
        }
        if (TxtGovt3.Text != "")
        {
            govsec = Convert.ToInt32(TxtGovt3.Text);
        }

        if (TxtPvt3.Text != "")
        {
            pvtsec = Convert.ToInt32(TxtPvt3.Text);
        }
        if (TxtGovt4.Text != "")
        {
            govsensec = Convert.ToInt32(TxtGovt4.Text);
        }
        if (TxtPvt4.Text != "")
        {
            pvtsensec = Convert.ToInt32(TxtPvt4.Text);
        }

        govtot = govprimaryschool + govupperprimary + govsec + govsensec;
        if (govtot > 0)
        {
            TxtGovt5.Text = govtot.ToString();
        }
        pvttot = pvtprimaryschool + pvtupperprimary + pvtsec + pvtsensec;
        if (pvttot > 0)
        {
            TxtPvt5.Text = pvttot.ToString();
        }

        totalschool = govtot + pvttot;
        if (totalschool > 0)
        {
            TxtPvt6.Text = totalschool.ToString();
        }

    }
    protected void GvVillage_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GvVillage.PageIndex = e.NewPageIndex;
        if (ViewState["Serach"] != null)
        {
            DataTable dt = ViewState["Serach"] as DataTable;
            GvVillage.DataSource = dt;
            GvVillage.DataBind();
        }
    }
    #endregion
    public void fillcombos(string LookupFlag, DropDownList dropdown)
    {
        conditions = "";
        conditions = "LookupFlag ='" + LookupFlag + "'";
        objComman.BindDLL("mstLookup", "LookupCode,Description,SeqNo", conditions, "SeqNo", "asc", dropdown, "Description", "LookupCode", "--Select--");



    }

    public void fillGridName()
    {

        DataTable dtType = new DataTable();
        DataRow dr;
        dtType.Columns.Add("VillageCode", System.Type.GetType("System.String"));
        dtType.Columns.Add("DhaniName", System.Type.GetType("System.String"));

        dtType.Columns.Add("VillageGUID", System.Type.GetType("System.String"));

       

        dr = dtType.NewRow();
        dr["VillageCode"] = "0";
        dr["DhaniName"] = "";
        dr["VillageGUID"] = "";

        dtType.Rows.Add(dr);


        GV_name_Add.DataSource = dtType;
        GV_name_Add.DataBind();
        ViewState["ECurrentTable"] = dtType;
    }
    protected void btnAdd_Click1(object sender, EventArgs e)
    {

        EAddNewRowToGrid();

        // ddllevel_selectindexchange(sender, e);
    }
    private void EAddNewRowToGrid()
    {
        int rowIndex = 0;

        if (ViewState["ECurrentTable"] != null)
        {
            DataTable dtCurrentTable = (DataTable)ViewState["ECurrentTable"];
            DataRow drCurrentRow = null;
            if (dtCurrentTable.Rows.Count > 0)
            {
                for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
                {
                    //extract the TextBox values

                    TextBox box1 = (TextBox)GV_name_Add.Rows[rowIndex].Cells[0].FindControl("lblName");
             
                    drCurrentRow = dtCurrentTable.NewRow();
                    //drCurrentRow["RowNumber"] = i + 1;txtno




                    if (box1.Text == "")
                    { dtCurrentTable.Rows[i - 1]["DhaniName"] = DBNull.Value; }
                    else
                    { dtCurrentTable.Rows[i - 1]["DhaniName"] = box1.Text; }

                



                    rowIndex++;
                }
                dtCurrentTable.Rows.Add(drCurrentRow);



                ViewState["ECurrentTable"] = dtCurrentTable;

                GV_name_Add.DataSource = dtCurrentTable;
                GV_name_Add.DataBind();
            }
        }
        else
        {
            // Response.Write("ViewState is null");
        }


        ESetPreviousData();
    }
    private void ESetPreviousData()
    {
        int rowIndex = 0;
        if (ViewState["ECurrentTable"] != null)
        {
            DataTable dt = (DataTable)ViewState["ECurrentTable"];
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {


                    TextBox box1 = (TextBox)GV_name_Add.Rows[rowIndex].Cells[0].FindControl("lblName");


                    box1.Text = dt.Rows[i]["DhaniName"].ToString();
                  
                    rowIndex++;
                }
            }
        }
    }
    protected void Img_btn_delete_Click(object sender, EventArgs e)
    {
        //EAddNewRowToGrid();
        DataTable dt_AfterDelete = (DataTable)ViewState["ECurrentTable"];
        ImageButton Img_btn_delete = sender as ImageButton;
        GridViewRow row = Img_btn_delete.NamingContainer as GridViewRow;
        int index = row.RowIndex;
        DataRow dr = dt_AfterDelete.Rows[index];

        dr.Delete();
        dt_AfterDelete.AcceptChanges();
        string Id;
      
        //int output = objBLL.GetResultFromQueryOut("delete from tblcommunityMembers where Id = '" + Id + "'");
        // fillGridName();
        if (dt_AfterDelete.Rows.Count == 0)
        {
            fillGridName();
            GV_name_Add.DataSource = (DataTable)ViewState["ECurrentTable"];
            GV_name_Add.DataBind();
        }
        else
        {
            GV_name_Add.DataSource = dt_AfterDelete;
            GV_name_Add.DataBind();
        }
    }
}