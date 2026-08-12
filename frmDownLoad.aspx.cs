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
using System.Data.OleDb;
using System.IO;
using Ionic.Zip;
using System.IO.Compression;


public partial class frmDownLoad : System.Web.UI.Page
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
            //CreateMdB("EGE0723","");
         // CreateMdB(Session["username"].ToString(), Session["Password"].ToString());
            DataTable dt = objMain.LoadData("SELECT [SrNo] ,convert(varchar,[UpdateDate],103) as [UpdateDate] ,[Description]    ,[Path]  FROM [mstDownload] order by SrNo desc");

            if (dt.Rows.Count > 0)
            {
                GV_Report.DataSource = dt;
                GV_Report.DataBind();
            }

        }
    }
    public void BackUpInformation()
    {
        string path = Server.MapPath("~/DataBackup/EG.mdb");
        string path1 = Server.MapPath("~/DataBackup");
      string  Source =path;
        //Path where DataBase actually Stored
        
        //If Path is Not set
  
          
                File.Copy(Source, path1 + "\\DataBackUp.mdb", true);
                //Copying file to the Destination
            
           
    }

    //public DataTable VGridFill(string select,string con)
    //{
    //    OleDbConnection dbOleconnection = new OleDbConnection(con);

    //    try
    //    {
    //        if (dbOleconnection.State == ConnectionState.Closed)
    //        {
    //            dbOleconnection.Open();
    //        }
    //        DataTable dbOleDataTable = new DataTable();
    //        OleDbCommand dbOleCommand = new OleDbCommand();
    //        dbOleCommand.Connection = dbOleconnection;
    //        dbOleCommand.Parameters.Clear();
    //        dbOleCommand.CommandType = CommandType.Text;
    //        dbOleCommand.CommandText = select;
    //        OleDbDataAdapter dbOleDataAdapter = new OleDbDataAdapter();
    //        dbOleDataAdapter.SelectCommand = dbOleCommand;
    //        dbOleDataAdapter.Fill(dbOleDataTable);
    //        return dbOleDataTable;

    //    }
    //    catch (OleDbException sqlEx)
    //    {
    //        if (dbOleconnection.State == ConnectionState.Open)
    //        {
    //            dbOleconnection.Close();
    //        }
    //        throw sqlEx;
    //    }
    //    catch (Exception ex1)
    //    {
    //        if (dbOleconnection.State == ConnectionState.Open)
    //        {
    //            dbOleconnection.Close();
    //        }
    //        throw ex1;
    //    }
    //    finally
    //    {
    //        if (dbOleconnection.State == ConnectionState.Open)
    //        {
    //            dbOleconnection.Close();
    //        }

    //    }
    //}
    //public bool AddUpdate(string query,string con)
    //{

    //    using (OleDbCommand cmd = new OleDbCommand())
    //    {
    //        OleDbConnection mycon = new OleDbConnection(con);

    //        try
    //        {
    //            DataTable dtCode = new DataTable();
    //            if (mycon.State == ConnectionState.Closed)
    //            {
    //                mycon.Open();
    //            }
    //            cmd.CommandType = CommandType.Text;
    //            cmd.CommandText = query;
    //            cmd.Connection = mycon;
    //            cmd.ExecuteNonQuery();
    //            cmd.Dispose();
    //            return (true);
    //        }
    //        catch (Exception e)
    //        {
    //            throw e;
    //        }
    //        finally
    //        {
    //            mycon.Close();
    //        }
    //    }

    //}

    //public void CreateMdB(string UserName,string PassWord)
    //{
       
    //    try
    //    {

    //         Boolean checkpass = false;

    //    SqlParameter[] para1 = new SqlParameter[] { 
           
    //        new SqlParameter("@UserName",UserName),
    //         new SqlParameter("@Password",""),
           
    //        };


       
    //    string str;
    //    DataSet dtUserSet = new DataSet();

    //    dtUserSet = SqlHelper.GetDataSet(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "LoadMasterUser", para1);
    //    DataTable dtUser = dtUserSet.Tables[0].Copy();
    //    if (dtUser.Rows.Count > 0)
    //    {
    //        checkpass = Password.VerifyPassword("EGApp123", dtUser.Rows[0]["Password"].ToString());
    //    }
    //    if (checkpass == true)
    //    {
    //        string gPassword = "mw2Master1EG0";
    //        OleDbConnection conn = new OleDbConnection();
    //        //string path = Server.MapPath("~/DataBackup/EG.mdb");

    //        string path = Server.MapPath("~/DataBackup/EG.mdb");
    //        string path1 = Server.MapPath("~/DataBackup");
    //        string Source = path;
    //        //Path where DataBase actually Stored

    //        //If Path is Not set

    //        string UserPasth = UserName + "_" + DateTime.Now.ToString("yyyyMMddhhmmssfff") + ".mdb";
    //        File.Copy(Source, path1 + "\\" + UserPasth + "", true);
    //        string dbpatth = path1 + "\\" + UserPasth + "";
    //        //OleDbCommand cmd = new OleDbCommand();
    //        //conn.ConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;" +
    //        //       @"Data source=" + path;
    //        conn.ConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0; Data Source=" + dbpatth + "; Jet OLEDB:Database Password = " + gPassword + "; Persist Security Info=False";

    //        //conn.Open();
    //        //cmd.Connection = conn;
            
    //        string condition = "";
    //        SqlParameter[] para = new SqlParameter[] { 
           
    //        new SqlParameter("@UserName",UserName),
             
    //        };
    //        DataSet dsstate = SqlHelper.GetDataSet(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "LoadMasterState", para);

    //        if (dtUser.Rows[0]["Role_Level"].ToString() == "19" || dtUser.Rows[0]["Role_Level"].ToString() == "30" || dtUser.Rows[0]["Role_Level"].ToString() == "61" || dtUser.Rows[0]["Role_Level"].ToString() == "60" || dtUser.Rows[0]["Role_Level"].ToString() == "99" || dtUser.Rows[0]["Role_Level"].ToString() == "59" || dtUser.Rows[0]["Role_Level"].ToString() == "29" || dtUser.Rows[0]["Role_Level"].ToString() == "39" || dtUser.Rows[0]["Role_Level"].ToString() == "29")
    //        {



    //            DataRow[] drArr = null;
    //            DataRow[] drArr1 = null;
    //            DataRow[] drArr2 = null;
    //            DataRow[] drArr3 = null;
    //            DataRow[] drArr4 = null;

    //            //#region DIst Officer

    //            //DataTable dtState = dsstate.Tables[0].Copy();
    //            //drArr = dtState.Select("StateCode ='" + dtUser.Rows[0]["Statecode"] + "'  ");
    //            //string SState = "SELECT * from mst1State where StateCode ='" + dtUser.Rows[0]["Statecode"].ToString() + "' ";

    //            //DataTable SdtSState = VGridFill(SState, conn.ConnectionString);

                
              

    //            //if (SdtSState.Rows.Count > 0)
    //            //{
    //            //    //str = "update  mst1State set  [StateName]='" + SdtSState.Rows[0]["StateName"].ToString() + "' where Statecode= '" + dtUser.Rows[0]["Statecode"].ToString() + "' )";

    //            //    //bool res = dbt.AddUpdate(str);
    //            //}
    //            //else
    //            //{

    //            //    for (int j = 0; j < drArr.Length; j++)
    //            //    {

    //            //        str = "insert into mst1State (  [StateCode],[StateName] ,[StateShort] ,[NameLocalLng] ,[LangaugeCode]  ,[InterventionStart]   ,[Active]    )  values('" + drArr[j]["StateCode"] + "','" + drArr[j]["StateName"] + "','" + drArr[j]["StateShort"] + "','" + drArr[j]["NameLocalLng"] + "','" + drArr[j]["LangaugeCode"] + "' ,'" + drArr[j]["InterventionStart"] + "' ," + drArr[j]["Active"] + ")";

    //            //        bool res = AddUpdate(str, conn.ConnectionString);

    //            //    }
    //            //}
    //            //DataTable dtDisit = dsstate.Tables[1].Copy();
    //            //drArr1 = dtDisit.Select("StateCode ='" + dtUser.Rows[0]["Statecode"] + "'  and DistrictCode ='" + dtUser.Rows[0]["DistrictCode"] + "'  ");
    //            //string SDistrictCode = "SELECT * from mst2District where DistrictCode ='" + dtUser.Rows[0]["DistrictCode"].ToString() + "' ";

    //            //DataTable SdtDistrictCode = VGridFill(SDistrictCode, conn.ConnectionString);

    //            //if (SdtDistrictCode.Rows.Count > 0)
    //            //{
    //            //    //str = "update  mst2District set  [DistrictName]='" + SdtDistrictCode.Rows[0].["DistrictName"].ToString() + "' where DistrictCode= '" + dtUser.Rows[0]["DistrictCode"].ToString() + "' )";

    //            //    //bool res = dbt.AddUpdate(str);
    //            //}
    //            //else
    //            //{
    //            //    for (int j = 0; j < drArr1.Length; j++)
    //            //    {

    //            //        str = "insert into mst2District (  [StateCode],[DistrictCode] ,[DistrictName] ,[DistrictShort] ,[NameLocalLng]  ,[InterventionStart]   )  values('" + drArr1[j]["StateCode"] + "','" + drArr1[j]["DistrictCode"] + "','" + drArr1[j]["DistrictName"] + "','" + drArr1[j]["DistrictShort"] + "','" + drArr1[j]["NameLocalLng"] + "' ,'" + drArr1[j]["InterventionStart"] + "' )";

    //            //        bool res = AddUpdate(str, conn.ConnectionString);

    //            //    }

    //            //}




    //            //DataTable dtblock = dsstate.Tables[2].Copy();
    //            //drArr2 = dtblock.Select(" DistrictCode ='" + dtUser.Rows[0]["DistrictCode"] + "'  ");

    //            //for (int j = 0; j < drArr2.Length; j++)
    //            //{

    //            //    string StBlock = "SELECT * from mst3Block where BlockCode ='" + drArr2[j]["BlockCode"].ToString() + "' ";

    //            //    DataTable SdtBlockCode = VGridFill(StBlock, conn.ConnectionString);
    //            //    if (SdtBlockCode.Rows.Count > 0)
    //            //    {
    //            //        //str = "update mst3Block set BlockName='" + drArr2[j]["BlockName"] + "' where BlockCode='" + drArr2[j]["BlockCode"] + "' ";
    //            //        //bool res = dbt.AddUpdate(str);
    //            //    }
    //            //    else
    //            //    {

    //            //        str = "insert into mst3Block (  [StateCode],[DistrictCode] ,[BlockCode] ,[BlockName],[BlockShort] ,[NameLocalLng]  ,[InterventionStart]   )  values('" + drArr2[j]["StateCode"] + "','" + drArr2[j]["DistrictCode"] + "','" + drArr2[j]["BlockCode"] + "','" + drArr2[j]["BlockName"] + "','" + drArr2[j]["BlockShort"] + "','" + drArr2[j]["NameLocalLng"] + "' ,'" + drArr2[j]["InterventionStart"] + "' )";

    //            //        bool res = AddUpdate(str, conn.ConnectionString);
    //            //    }

    //            //}

    //            //DataTable dtphyant = dsstate.Tables[3].Copy();
    //            //drArr3 = dtphyant.Select(" DistrictCode ='" + dtUser.Rows[0]["DistrictCode"] + "'  ");

    //            //for (int j = 0; j < drArr3.Length; j++)
    //            //{
    //            //    string StPanchayat = "SELECT * from mstPanchayat where PanchayatCode ='" + drArr3[j]["PanchayatCode"].ToString() + "' ";

    //            //    DataTable SdttPanchayat = VGridFill(StPanchayat, conn.ConnectionString);

    //            //    if (SdttPanchayat.Rows.Count > 0)
    //            //    {
    //            //        //str = "update mstPanchayat set PanchayatName='" + drArr3[j]["PanchayatName"] + "' where PanchayatCode='" + drArr3[j]["PanchayatCode"] + "' ";
    //            //        //bool res = dbt.AddUpdate(str);
    //            //    }
    //            //    else
    //            //    {
    //            //        str = "insert into mstPanchayat (  [StateCode],[DistrictCode] ,[BlockCode] ,[PanchayatCode],[PanchayatName] ,[NameLocalLng]  ,[SarpanchName] ,[SarpanchContact]  )  values('" + drArr3[j]["StateCode"] + "','" + drArr3[j]["DistrictCode"] + "','" + drArr3[j]["BlockCode"] + "','" + drArr3[j]["PanchayatCode"] + "','" + drArr3[j]["PanchayatName"] + "','" + drArr3[j]["NameLocalLng"] + "' ,'" + drArr3[j]["SarpanchName"] + "','" + drArr3[j]["SarpanchContact"] + "' )";

    //            //        bool res = AddUpdate(str, conn.ConnectionString);
    //            //    }

    //            //}



    //            //DataTable dtdsvill = dsstate.Tables[4].Copy();
    //            //drArr4 = dtdsvill.Select("DistrictCode ='" + dtUser.Rows[0]["DistrictCode"] + "' ");

    //            //for (int j = 0; j < drArr4.Length; j++)
    //            //{

    //            //    //str = "insert into mst5Village ([StateCode] ,[DistrictCode] ,[BlockCode],[PanchayatCode],[VillageCode],[PanchayatShort], [VillageName],[NameLocalLng],[CensusCode],[ClusterCode],[SurveyNo],[SurveyorID],[SurveyDate],[Pincode],[FieldCoordinator],[DistanceDistrict],[DistanceGP],[NoWards],[CastesTribes],[MainOccupation],[SecondaryOccupation],[HealthFacilities],[Transport],[VillageAccess]) values('" + drArr4[j]["StateCode"] + "','" + drArr4[j]["DistrictCode"] + "','" + drArr4[j]["BlockCode"] + "','" + drArr4[j]["PanchayatCode"] + "','" + drArr4[j]["VillageCode"] + "','" + drArr4[j]["PanchayatShort"] + "','" + drArr4[j]["VillageName"] + "','" + drArr4[j]["NameLocalLng"] + "','" + drArr4[j]["CensusCode"] + "','" + drArr4[j]["ClusterCode"] + "','" + drArr4[j]["SurveyNo"] + "','" + drArr4[j]["SurveyorID"] + "','" + drArr4[j]["SurveyDate"] + "','" + drArr4[j]["Pincode"] + "','" + drArr4[j]["FieldCoordinator"] + "','" + drArr4[j]["DistanceDistrict"] + "','" + drArr4[j]["DistanceGP"] + "','" + drArr4[j]["NoWards"] + "','" + drArr4[j]["CastesTribes"] + "','" + drArr4[j]["MainOccupation"] + "','" + drArr4[j]["SecondaryOccupation"] + "','" + drArr4[j]["HealthFacilities"] + "','" + drArr4[j]["Transport"] + "','" + drArr4[j]["VillageAccess"] + "') ";
    //            //    string Stmst5Village = "SELECT * from mst5Village where VillageCode ='" + drArr4[j]["VillageCode"].ToString() + "' ";

    //            //    DataTable Sdtmst5Village = VGridFill(Stmst5Village, conn.ConnectionString);

    //            //    if (Sdtmst5Village.Rows.Count > 0)
    //            //    {
    //            //        //str = "update mst5Village set VillageName='" + drArr3[j]["VillageName"] + "' where VillageCode='" + drArr4[j]["VillageCode"] + "' ";
    //            //        //bool res = dbt.AddUpdate(str);
    //            //    }
    //            //    else
    //            //    {
    //            //        str = "insert into mst5Village ([StateCode] ,[DistrictCode] ,[BlockCode],[PanchayatCode],[VillageCode], [VillageName],[NameLocalLng],[ClusterCode]) values('" + drArr4[j]["StateCode"] + "','" + drArr4[j]["DistrictCode"] + "','" + drArr4[j]["BlockCode"] + "','" + drArr4[j]["PanchayatCode"] + "','" + drArr4[j]["VillageCode"] + "','" + drArr4[j]["VillageName"] + "','" + drArr4[j]["NameLocalLng"] + "','" + drArr4[j]["ClusterCode"] + "') ";

    //            //        bool res = AddUpdate(str, conn.ConnectionString);
    //            //    }


    //            //}
    //            /////Lookup
    //            /////
    //            ////DataTable dtLookUp = dsstate.Tables[5].Copy();
    //            ////if (dtLookUp.Rows.Count > 0)
    //            ////{
    //            ////    string Stmst5Village = "delete  from mstLookup ";

    //            ////    DataTable SdtmstSchool = dbt.VGridFill(Stmst5Village);
    //            ////    foreach (DataRow dr in dtLookUp.Rows)
    //            ////    {

    //            ////        //str = "insert into mst5Village ([StateCode] ,[DistrictCode] ,[BlockCode],[PanchayatCode],[VillageCode],[PanchayatShort], [VillageName],[NameLocalLng],[CensusCode],[ClusterCode],[SurveyNo],[SurveyorID],[SurveyDate],[Pincode],[FieldCoordinator],[DistanceDistrict],[DistanceGP],[NoWards],[CastesTribes],[MainOccupation],[SecondaryOccupation],[HealthFacilities],[Transport],[VillageAccess]) values('" + drArr4[j]["StateCode"] + "','" + drArr4[j]["DistrictCode"] + "','" + drArr4[j]["BlockCode"] + "','" + drArr4[j]["PanchayatCode"] + "','" + drArr4[j]["VillageCode"] + "','" + drArr4[j]["PanchayatShort"] + "','" + drArr4[j]["VillageName"] + "','" + drArr4[j]["NameLocalLng"] + "','" + drArr4[j]["CensusCode"] + "','" + drArr4[j]["ClusterCode"] + "','" + drArr4[j]["SurveyNo"] + "','" + drArr4[j]["SurveyorID"] + "','" + drArr4[j]["SurveyDate"] + "','" + drArr4[j]["Pincode"] + "','" + drArr4[j]["FieldCoordinator"] + "','" + drArr4[j]["DistanceDistrict"] + "','" + drArr4[j]["DistanceGP"] + "','" + drArr4[j]["NoWards"] + "','" + drArr4[j]["CastesTribes"] + "','" + drArr4[j]["MainOccupation"] + "','" + drArr4[j]["SecondaryOccupation"] + "','" + drArr4[j]["HealthFacilities"] + "','" + drArr4[j]["Transport"] + "','" + drArr4[j]["VillageAccess"] + "') ";


    //            ////        str = "insert into mstLookup ([LookupFlag] ,[LookupCode],[SeqNo] ,[Active],[Description],[Language], [IsDefault],[RequiresQualifier],Description1) values('" + dr["LookupFlag"] + "'," + dr["LookupCode"] + "," + dr["SeqNo"] + "," + dr["Active"] + ",'" + dr["Description"] + "','" + dr["Language"] + "'," + dr["IsDefault"] + ",'" + dr["RequiresQualifier"] + "','" + dr["Description1"] + "') ";

    //            ////        bool res = dbt.AddUpdate(str);



    //            ////    }
    //            ////}
    //            ////-------------School



    //            //DataTable dtSchool = dsstate.Tables[6].Copy();
    //            ////drArr4 = dtdsvill.Select("DistrictCode ='" + dtUser.Rows[0]["DistrictCode"] + "' ");
    //            //Int32 icount = 0;
    //            //if (dtSchool.Rows.Count > 0)
    //            //{
    //            //    foreach (DataRow dr in dtSchool.Rows)
    //            //    {

    //            //        //str = "insert into mst5Village ([StateCode] ,[DistrictCode] ,[BlockCode],[PanchayatCode],[VillageCode],[PanchayatShort], [VillageName],[NameLocalLng],[CensusCode],[ClusterCode],[SurveyNo],[SurveyorID],[SurveyDate],[Pincode],[FieldCoordinator],[DistanceDistrict],[DistanceGP],[NoWards],[CastesTribes],[MainOccupation],[SecondaryOccupation],[HealthFacilities],[Transport],[VillageAccess]) values('" + drArr4[j]["StateCode"] + "','" + drArr4[j]["DistrictCode"] + "','" + drArr4[j]["BlockCode"] + "','" + drArr4[j]["PanchayatCode"] + "','" + drArr4[j]["VillageCode"] + "','" + drArr4[j]["PanchayatShort"] + "','" + drArr4[j]["VillageName"] + "','" + drArr4[j]["NameLocalLng"] + "','" + drArr4[j]["CensusCode"] + "','" + drArr4[j]["ClusterCode"] + "','" + drArr4[j]["SurveyNo"] + "','" + drArr4[j]["SurveyorID"] + "','" + drArr4[j]["SurveyDate"] + "','" + drArr4[j]["Pincode"] + "','" + drArr4[j]["FieldCoordinator"] + "','" + drArr4[j]["DistanceDistrict"] + "','" + drArr4[j]["DistanceGP"] + "','" + drArr4[j]["NoWards"] + "','" + drArr4[j]["CastesTribes"] + "','" + drArr4[j]["MainOccupation"] + "','" + drArr4[j]["SecondaryOccupation"] + "','" + drArr4[j]["HealthFacilities"] + "','" + drArr4[j]["Transport"] + "','" + drArr4[j]["VillageAccess"] + "') ";
    //            //        string Stmst5Village = "SELECT * from mstSchool where SchoolCode ='" + dr["SchoolCode"].ToString() + "' ";

    //            //        DataTable SdtmstSchool = VGridFill(Stmst5Village, conn.ConnectionString);
    //            //        icount = icount + 1;
    //            //        if (SdtmstSchool.Rows.Count > 0)
    //            //        {
    //            //            //str = "update mst5Village set Name='" + dr["Name"] + "' where SchoolCode='" + dr["SchoolCode"] + "' ";
    //            //            //bool res = dbt.AddUpdate(str);
    //            //        }
    //            //        else
    //            //        {
    //            //            str = "insert into mstSchool ([VillageCode] ,[SchoolCode] ,[DISECode],[Name],[Address], [PrincipalName],[PrincipalContact]) values('" + dr["VillageCode"] + "','" + dr["SchoolCode"] + "','" + dr["DISECode"] + "','" + dr["Name"] + "','" + dr["Address"] + "','" + dr["PrincipalName"] + "','" + dr["PrincipalContact"] + "') ";

    //            //            bool res = AddUpdate(str, conn.ConnectionString);
    //            //        }


    //            //    }

    //            //}

    //            //DataTable dtUserDeatil = dsstate.Tables[7].Copy();

    //            //if (dtUserDeatil.Rows.Count > 0)
    //            //{
    //            //    foreach (DataRow dr in dtUserDeatil.Rows)
    //            //    {

    //            //        //str = "insert into mst5Village ([StateCode] ,[DistrictCode] ,[BlockCode],[PanchayatCode],[VillageCode],[PanchayatShort], [VillageName],[NameLocalLng],[CensusCode],[ClusterCode],[SurveyNo],[SurveyorID],[SurveyDate],[Pincode],[FieldCoordinator],[DistanceDistrict],[DistanceGP],[NoWards],[CastesTribes],[MainOccupation],[SecondaryOccupation],[HealthFacilities],[Transport],[VillageAccess]) values('" + drArr4[j]["StateCode"] + "','" + drArr4[j]["DistrictCode"] + "','" + drArr4[j]["BlockCode"] + "','" + drArr4[j]["PanchayatCode"] + "','" + drArr4[j]["VillageCode"] + "','" + drArr4[j]["PanchayatShort"] + "','" + drArr4[j]["VillageName"] + "','" + drArr4[j]["NameLocalLng"] + "','" + drArr4[j]["CensusCode"] + "','" + drArr4[j]["ClusterCode"] + "','" + drArr4[j]["SurveyNo"] + "','" + drArr4[j]["SurveyorID"] + "','" + drArr4[j]["SurveyDate"] + "','" + drArr4[j]["Pincode"] + "','" + drArr4[j]["FieldCoordinator"] + "','" + drArr4[j]["DistanceDistrict"] + "','" + drArr4[j]["DistanceGP"] + "','" + drArr4[j]["NoWards"] + "','" + drArr4[j]["CastesTribes"] + "','" + drArr4[j]["MainOccupation"] + "','" + drArr4[j]["SecondaryOccupation"] + "','" + drArr4[j]["HealthFacilities"] + "','" + drArr4[j]["Transport"] + "','" + drArr4[j]["VillageAccess"] + "') ";
    //            //        string Stremp = "SELECT * from tblemployeedetails where EmployeeID ='" + dr["EmployeeID"].ToString() + "' ";

    //            //        DataTable dtEmp = VGridFill(Stremp, conn.ConnectionString);
    //            //        icount = icount + 1;
    //            //        if (dtEmp.Rows.Count > 0)
    //            //        {
    //            //            //str = "update tblemployeedetails set Username='" + dr["Username"] + "', Firstname='" + dr["Firstname"] + "', Lastname='" + dr["Lastname"] + "' , Lastname='" + dr["EmaillID"] + "' where EmployeeID='" +dr["EmployeeID"] + "' ";
    //            //            //bool res = dbt.AddUpdate(str);
    //            //        }
    //            //        else
    //            //        {
    //            //            str = "insert into tblemployeedetails ([EmployeeID] ,[EmployeeType],[Username] ,[Firstname],[Lastname],[EmaillID], [MobileNo],Status) values('" + dr["EmployeeID"] + "','" + dr["EmployeeType"] + "','" + dr["Username"] + "','" + dr["Firstname"] + "','" + dr["Lastname"] + "','" + dr["EmaillID"] + "','" + dr["MobileNo"] + "','" + dr["Status"] + "') ";

    //            //            bool res = AddUpdate(str, conn.ConnectionString);
    //            //        }


    //            //    }
    //            //}
    //            //DataTable dtEMPDeatils = dsstate.Tables[8].Copy();

    //            //if (dtEMPDeatils.Rows.Count > 0)
    //            //{
    //            //    string Stremp = "delete from MstUserRight  ";

    //            //    DataTable dtEmp = VGridFill(Stremp, conn.ConnectionString);
    //            //    foreach (DataRow dr in dtEMPDeatils.Rows)
    //            //    {


    //            //        str = "insert into MstUserRight (Role_Id,[Module] ,[AddStatus],[view_status] ,[edit_status],[verify_Status], [Delete_status]) values(" + dr["Role_Id"] + ",'" + dr["Module"] + "'," + dr["AddStatus"] + "," + dr["view_status"] + "," + dr["edit_status"] + "," + dr["verify_Status"] + "," + dr["Delete_status"] + ") ";

    //            //        bool res = AddUpdate(str, conn.ConnectionString);


    //            //    }
    //            //}


    //            //#endregion
              
    //        }
    //    }
     

    //  }
    //    catch (Exception ex)
    //    {

    //        //continue;
    //    }
           
    //}
    protected void btnsave_Click(object sender, EventArgs e)
    {
     
     CreateMdB(Session["username"].ToString(), Session["Password"].ToString());
    }
    public void CreateMdB(string UserName, string sPassWord)
    {
        string sReturn = string.Empty;

        string mainPath = "";


        try
        {



            Comman objcomm = new Comman();
            Boolean checkpass = false;

            SqlParameter[] para1 = new SqlParameter[] { 
           
            new SqlParameter("@UserName",UserName),
             new SqlParameter("@Password",""),
           
            };



            string str;
            DataSet dtUserSet = new DataSet();

            dtUserSet = SqlHelper.GetDataSet(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "LoadMasterUser", para1);
            DataTable dtUser = dtUserSet.Tables[0].Copy();
            if (dtUser.Rows.Count > 0)
            {
                checkpass = Password.VerifyPassword(sPassWord, dtUser.Rows[0]["Password"].ToString());
            }
            if (checkpass == true)
            {
                string gPassword = "mw2Master1EG0";
                OleDbConnection conn = new OleDbConnection();

                string path = Server.MapPath("~/DataBackup/EG.mdb");
                string path1 = Server.MapPath("~/DataBackup");

                //string path = Server.MapPath("~/EG.mdb");
                //string path1 = Server.MapPath("");

                string Source = path;
                //Path where DataBase actually Stored

                //If Path is Not set

                string UserPasth = UserName + "_" + DateTime.Now.ToString("yyyyMMdd") + ".mdb";

                Session["UserPasth"] = UserPasth;
                File.Copy(Source, path1 + "\\" + UserPasth + "", true);
                string dbpatth = path1 + "\\" + UserPasth + "";
                //OleDbCommand cmd = new OleDbCommand();
                //conn.ConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;" +
                //       @"Data source=" + path;
                conn.ConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0; Data Source=" + dbpatth + "; Jet OLEDB:Database Password = " + gPassword + "; Persist Security Info=False";

                //conn.Open();
                //cmd.Connection = conn;


                SqlParameter[] para = new SqlParameter[] { 
           
            new SqlParameter("@UserName",UserName),
             
            };
                DataSet dsstate = SqlHelper.GetDataSet(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "LoadMasterState", para);

                SqlParameter[] para12 = new SqlParameter[] { 
           
            new SqlParameter("@UserName",UserName),
             
            };
                DataTable dtD2d = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "GetD2DAndEnrollNew", para12);


                if (dtUser.Rows[0]["Role_Level"].ToString() == "19" || dtUser.Rows[0]["Role_Level"].ToString() == "30" || dtUser.Rows[0]["Role_Level"].ToString() == "61" || dtUser.Rows[0]["Role_Level"].ToString() == "60" || dtUser.Rows[0]["Role_Level"].ToString() == "99" || dtUser.Rows[0]["Role_Level"].ToString() == "59" || dtUser.Rows[0]["Role_Level"].ToString() == "29" || dtUser.Rows[0]["Role_Level"].ToString() == "39" || dtUser.Rows[0]["Role_Level"].ToString() == "29")
                {



                    DataRow[] drArr = null;
                    DataRow[] drArr1 = null;
                    DataRow[] drArr2 = null;
                    DataRow[] drArr3 = null;
                    DataRow[] drArr4 = null;

                    #region DIst Officer

                    DataTable dtState = dsstate.Tables[0].Copy();
                    drArr = dtState.Select("StateCode ='" + dtUser.Rows[0]["Statecode"] + "'  ");
                   
                  

                        for (int j = 0; j < drArr.Length; j++)
                        {

                            str = "insert into mst1State (  [StateCode],[StateName] ,[StateShort] ,[NameLocalLng] ,[LangaugeCode]  ,[InterventionStart]   ,[Active]    )  values('" + drArr[j]["StateCode"] + "','" + drArr[j]["StateName"] + "','" + drArr[j]["StateShort"] + "','" + drArr[j]["NameLocalLng"] + "','" + drArr[j]["LangaugeCode"] + "' ,'" + drArr[j]["InterventionStart"] + "' ," + drArr[j]["Active"] + ")";

                            bool res = objcomm.AddUpdate(str, conn.ConnectionString);

                        }
                 
                    DataTable dtDisit = dsstate.Tables[1].Copy();
                    drArr1 = dtDisit.Select("StateCode ='" + dtUser.Rows[0]["StateCode"] + "'  ");
                    
                        for (int j = 0; j < drArr1.Length; j++)
                        {

                            str = "insert into mst2District (  [StateCode],[DistrictCode] ,[DistrictName] ,[DistrictShort] ,[NameLocalLng]  ,[InterventionStart]   )  values('" + drArr1[j]["StateCode"] + "','" + drArr1[j]["DistrictCode"] + "','" + drArr1[j]["DistrictName"] + "','" + drArr1[j]["DistrictShort"] + "','" + drArr1[j]["NameLocalLng"] + "' ,'" + drArr1[j]["InterventionStart"] + "' )";

                            bool res = objcomm.AddUpdate(str, conn.ConnectionString);

                        }

                   




                    DataTable dtblock = dsstate.Tables[2].Copy();

                    Export_Access(dtblock, conn.ConnectionString, "mst3Block");

                  
                    DataTable dtphyant = dsstate.Tables[3].Copy();
                   

                    Export_Access(dtphyant, conn.ConnectionString, "mstPanchayat");

                    DataTable dtdsvill = dsstate.Tables[4].Copy();
                 


                    Export_Access(dtdsvill, conn.ConnectionString, "mst5Village");

                    DataTable dtSchool = dsstate.Tables[6].Copy();
                 

                    Export_Access(dtSchool, conn.ConnectionString, "mstSchool");


                    Export_Access(dtD2d, conn.ConnectionString, "tblDTD");

                    DataTable dtUserDeatil = dsstate.Tables[7].Copy();

                    if (dtUserDeatil.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dtUserDeatil.Rows)
                        {

                            //str = "insert into mst5Village ([StateCode] ,[DistrictCode] ,[BlockCode],[PanchayatCode],[VillageCode],[PanchayatShort], [VillageName],[NameLocalLng],[CensusCode],[ClusterCode],[SurveyNo],[SurveyorID],[SurveyDate],[Pincode],[FieldCoordinator],[DistanceDistrict],[DistanceGP],[NoWards],[CastesTribes],[MainOccupation],[SecondaryOccupation],[HealthFacilities],[Transport],[VillageAccess]) values('" + drArr4[j]["StateCode"] + "','" + drArr4[j]["DistrictCode"] + "','" + drArr4[j]["BlockCode"] + "','" + drArr4[j]["PanchayatCode"] + "','" + drArr4[j]["VillageCode"] + "','" + drArr4[j]["PanchayatShort"] + "','" + drArr4[j]["VillageName"] + "','" + drArr4[j]["NameLocalLng"] + "','" + drArr4[j]["CensusCode"] + "','" + drArr4[j]["ClusterCode"] + "','" + drArr4[j]["SurveyNo"] + "','" + drArr4[j]["SurveyorID"] + "','" + drArr4[j]["SurveyDate"] + "','" + drArr4[j]["Pincode"] + "','" + drArr4[j]["FieldCoordinator"] + "','" + drArr4[j]["DistanceDistrict"] + "','" + drArr4[j]["DistanceGP"] + "','" + drArr4[j]["NoWards"] + "','" + drArr4[j]["CastesTribes"] + "','" + drArr4[j]["MainOccupation"] + "','" + drArr4[j]["SecondaryOccupation"] + "','" + drArr4[j]["HealthFacilities"] + "','" + drArr4[j]["Transport"] + "','" + drArr4[j]["VillageAccess"] + "') ";
                            
                                str = "insert into tblemployeedetails ([EmployeeID] ,[EmployeeType],[Username] ,[Firstname],[Lastname],[EmaillID], [MobileNo],Status) values('" + dr["EmployeeID"] + "','" + dr["EmployeeType"] + "','" + dr["Username"] + "','" + dr["Firstname"] + "','" + dr["Lastname"] + "','" + dr["EmaillID"] + "','" + dr["MobileNo"] + "','" + dr["Status"] + "') ";

                                bool res = objcomm.AddUpdate(str, conn.ConnectionString);
                           


                        }
                    }
                    DataTable dtEMPDeatils = dsstate.Tables[8].Copy();

                    if (dtEMPDeatils.Rows.Count > 0)
                    {
                       
                        foreach (DataRow dr in dtEMPDeatils.Rows)
                        {


                            str = "insert into MstUserRight (Role_Id,[Module] ,[AddStatus],[view_status] ,[edit_status],[verify_Status], [Delete_status]) values(" + dr["Role_Id"] + ",'" + dr["Module"] + "'," + dr["AddStatus"] + "," + dr["view_status"] + "," + dr["edit_status"] + "," + dr["verify_Status"] + "," + dr["Delete_status"] + ") ";

                            bool res = objcomm.AddUpdate(str, conn.ConnectionString);


                        }
                    }
                    #region User

                    for (int j = 0; j < dtUser.Rows.Count; j++)
                    {
                        string village = "";
                       
                    
                            string val_Role = dtUser.Rows[j]["Role"].ToString() == "" ? "NULL" : dtUser.Rows[j]["Role"].ToString();



                            str = "insert into MstUser ( [UserID],[UserName]  ,[Password]     ,[UserLevel]  ,[StaffID]  ,[Role] ,[Statecode]   ,[DistrictCode]  ,[BlockCode]      ,[Villagecode]) values(" + dtUser.Rows[j]["UserID"] + ",'" + dtUser.Rows[j]["UserName"] + "','" + dtUser.Rows[j]["Password"] + "','" + dtUser.Rows[j]["UserLevel"] + "','" + dtUser.Rows[j]["StaffID"] + "','" + dtUser.Rows[j]["Role"] + "'," + dtUser.Rows[j]["Statecode"] + ",'" + dtUser.Rows[j]["DistrictCode"] + "','" + dtUser.Rows[j]["BlockCode"] + "','" + village + "') ";
                            bool res = objcomm.AddUpdate(str, conn.ConnectionString);

                        

                    }
                    DataTable DtModuel = dsstate.Tables[9].Copy();
                    if (DtModuel.Rows.Count > 0)
                    {
                        
                        foreach (DataRow dr in DtModuel.Rows)
                        {


                            str = "insert into mstModuleLocking (FromName,[LockDay] ,[LockMonth]) values('" + dr["FromName"] + "'," + dr["LockDay"] + "," + dr["LockMonth"] + ") ";

                            bool res = objcomm.AddUpdate(str, conn.ConnectionString);


                        }
                    }
                 


                    #endregion

                    #endregion

                    crateZip();
                    //System.IO.FileInfo targetFile = new System.IO.FileInfo(dbpatth);
                    //if (targetFile.Exists)
                    //{
                    //    Response.Clear();
                    //    Response.AddHeader("Content-Disposition", "attachment; filename=" + targetFile.Name);
                    //    Response.AddHeader("Content-Length", targetFile.Length.ToString());
                    //    Response.ContentType = "application/octet-stream";
                    //    Response.WriteFile(targetFile.FullName);
                    //}
               
                    
                    //HttpContext.Current.Response.ContentType = "application/octet-stream";
                    //HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=" + System.IO.Path.GetFileName(dbpatth));
                    //HttpContext.Current.Response.Clear();
                    //HttpContext.Current.Response.WriteFile(dbpatth);
                    //HttpContext.Current.Response.End();


                    //Response.ContentType = "application/octet-stream";
                    //Response.AppendHeader("Content-Disposition", "attachment;filename=" + dbpatth);
                    //string aaa = Server.MapPath("~/DataBackup/" + UserPasth);
                    //Response.TransmitFile(Server.MapPath("~/DataBackup/" + UserPasth));
                }

            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Invaild UserName or Password')</script>", false);
 
            }
           
      
        }
        catch (Exception ex)
        {
          
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Internet Problem')</script>", false);
 
            
            //continue;
        }

    }

   

    public void crateZip()
    {
        FileStream fs = null;//, fs2=null;
        try
        {
            string path = Session["UserPasth"].ToString();
            string foldername = Server.MapPath("~/DataBackup/" + path + "");
            string datafolder = path.Substring(0, path.Length - 4);
           

            string fullPath = Request.MapPath("~/DataBackup/" + datafolder + "" + ".zip");
            using (ZipFile zip = new ZipFile())
            {
                zip.AddFile(foldername);
              //  zip.AddFiles(filenames, foldername);
                zip.Save(Server.MapPath("~/DataBackup/" + datafolder + "" + ".zip"));
            }


           
            HttpResponse Response = HttpContext.Current.Response; Response.Clear(); Response.ClearHeaders(); Response.Charset = "UTF-8";
            fs = File.Open(fullPath, FileMode.Open);
            byte[] bytBytes = new byte[(fs.Length)];
            fs.Read(bytBytes, 0, (int)fs.Length);
            fs.Close();
            Response.AddHeader("Content-disposition", "attachment; filename=" +fullPath);
            Response.ContentType = "application/octet-stream";
            Response.BinaryWrite(bytBytes);


           


            
            //if (File.Exists(fullPath))
            //{
            //    System.IO.File.Delete(fullPath);
            //}
           
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

    }
    public void Export_Access(DataTable dt,string con,string tableName)
    {
        if (dt.Rows.Count > 0)
        {
            csvfile(dt);
            
        }
        string s = "", statement = "";
        for (int j = 0; j <= dt.Columns.Count - 1; j++)
        {
            s = s + "[" + Convert.ToString(dt.Columns[j].ColumnName) + "]" + ",";
        }
        s = s.TrimEnd(',');
        //statement = "INSERT INTO mstPanchayat SELECT  " + s + " FROM " + "[Text;Database=" + Session["destFileName"] + ";HDR=YES]";

        statement = "INSERT INTO " + tableName + " SELECT  " + s + " FROM " + "[Text;Database=" + Session["savepath"] + ";HDR=YES]." + Session["FileName"] + "";

        int retVAL = 0;
           
        retVAL = SAVEDATAExport(statement, con);

        //if (File.Exists(Session["savepath"].ToString()))
        //{
        //    System.IO.File.Delete(Session["savepath"].ToString());
        //}
           
    }
    public int SAVEDATAExport(string strsql, string connStr)
    {
        try
        {
            int retval = 0;
            OleDbConnection dbOleconnection = new OleDbConnection(connStr);

            if (dbOleconnection.State != ConnectionState.Open)
            {
                dbOleconnection.Open();
            }
            OleDbCommand dbOleCommand = new OleDbCommand();
            dbOleCommand.Connection = dbOleconnection;
            dbOleCommand.CommandText = strsql;
            retval = dbOleCommand.ExecuteNonQuery();
            dbOleconnection.Close();
            return retval;

        }
        catch (Exception SB)
        {
            return -1;
        }


    }
    private void csvfile(DataTable dtset)
    {
        try
        {
            string filename = null;
            string savepath = null;
            savepath = Request.PhysicalApplicationPath + "DataBackup";
            filename = Request.PhysicalApplicationPath + "DataBackup\\TemplateData.csv";
            //FileInfo MyFile = new FileInfo(filename);
            string str = "TemplateData" + DateTime.Now.ToString("ddMMyyyyhhmmssfff") + ".csv";
            string destFileName = "\\" + str;
            string destFileNamepath = savepath + destFileName;

            Session["destFileName"] = destFileNamepath;
            Session["FileName"] =str ;
            Session["savepath"] = savepath;
            File.Copy(filename, destFileNamepath);
            //if (MyFile.Exists)
            //{
            //    System.IO.File.Delete(filename);
            //}
            System.Collections.ArrayList Scorpion = new System.Collections.ArrayList();
            System.Text.StringBuilder hdd = new System.Text.StringBuilder();
            int p = 0;
            for (p = 0; p <= dtset.Columns.Count - 1; p++)
            {
                hdd.Append('"' + dtset.Columns[p].ColumnName + '"' + ",");
            }
            Scorpion.Add(hdd);
            for (int i = 0; i <= dtset.Rows.Count - 1; i++)
            {
                System.Text.StringBuilder row = new System.Text.StringBuilder();
                Scorpion.Add(row);
                row.Append("\"");
                for (int y = 0; y <= dtset.Columns.Count - 1; y++)
                {
                    row.Append(dtset.Rows[i][y].ToString());
                    if (y != dtset.Columns.Count - 1)
                    {
                        row.Append("\",\"");
                    }
                    else
                    {
                        row.Append("\"");
                    }
                }
            }
            System.IO.StreamWriter sw = new System.IO.StreamWriter(destFileNamepath);
            for (int i = 0; i <= Scorpion.Count - 1; i++)
            {
                sw.WriteLine(Scorpion[i].ToString());
            }
            sw.Flush();
            sw.Close();
        }
        catch (Exception ex)
        {
            string mmsg = ex.Message;
        }

    }
}