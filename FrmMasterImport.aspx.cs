					  
using System;
using System.Collections.Generic;
							
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data.OleDb;
using System.Data;
using System.Data.SqlClient;

using ClosedXML.Excel;
using ExcelDataReader;

public partial class FrmMasterImport : System.Web.UI.Page
{
    Comman obj = new Comman();
    clsMain Objcls = new clsMain();
    Comman objComman = new Comman();

    string conditions = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadYear();
            FillCBState();
        }
    }
    public void FillCBState()
    {
        conditions = "";
        objComman.BindDLL("[PMS].[dbo].mst1State", "StateCode,dbo.TitleCase(upper(StateName)) as StateName ", conditions, "StateName", "asc", ddlState, "StateName", "StateCode", "--Select--");

        ddlState.SelectedIndex = 2;
        ddlState_SelectedIndexChanged(ddlState, null);
    }  
    protected void ddlYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlState_SelectedIndexChanged(ddlDistrict, null);
    }
    protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillCBDist();
    }
    public void FillCBDist()
    {

        conditions = "";


        conditions = "StateCode ='" + ddlState.SelectedValue + "' and mst2District.FYear ='" + ddlYear.SelectedItem.Text + "'";
        if (Convert.ToInt32(ddlYear.SelectedValue) >= 2026)
        {
            objComman.BindDLL("mst2District", "DistrictCode,dbo.TitleCase(upper(DistrictName)) as DistrictName ", conditions, "DistrictName", "asc", ddlDistrict, "DistrictName", "DistrictCode", "Select");

        }
        else
        {

            objComman.BindDLL("[PMS].[dbo].mst2District", "DistrictCode,dbo.TitleCase(upper(DistrictName)) as DistrictName ", conditions, "DistrictName", "asc", ddlDistrict, "DistrictName", "DistrictCode", "Select");
        }


    }

    protected void btnImport_Click(object sender, EventArgs e)
    {
       // GenerateExcelData();
    }
    protected void btnCSV_Click(object sender, EventArgs e)
    {
    }
    public Boolean BulkCopyTempDistProfile(DataTable dt)
    {
        try
        {


            SqlBulkCopy bulkCopy = new SqlBulkCopy(SqlHelper.mainConnectionString);
            bulkCopy.BatchSize = 100;
            bulkCopy.BulkCopyTimeout = 5;

            bulkCopy.DestinationTableName = "MasterDataUpload";
            bulkCopy.NotifyAfter = 200;
            bulkCopy.WriteToServer(dt);
            return true;
        }
        catch
        {
            return false;
        }
    }


    private static object Val(object v)
    {
        if (v == null || v == DBNull.Value) return DBNull.Value;
        string s = v.ToString().Trim();
        return s.Length == 0 ? (object)DBNull.Value : s;
    }
    private void GenerateExcelData()
    {
        string sDirectory1 = Server.MapPath("~/Mou//ErrorLoag.txt");
       
        string FilePathError = sDirectory1 ;
        OleDbConnection oledbConn = new OleDbConnection();
        try
        {
            // need to pass relative path after deploying on server
            string path = System.IO.Path.GetFullPath(Server.MapPath(FileUpload1.FileName));
            /* connection string  to work with excel file. HDR=Yes - indicates 
               that the first row contains columnnames, not data. HDR=No - indicates 
               the opposite. "IMEX=1;" tells the driver to always read "intermixed" 
               (numbers, dates, strings etc) data columns as text. 
            Note that this option might affect excel sheet write access negative. */
            string sDirectory = Server.MapPath("~/Mou//");
            int ManagementType = 0, WorkingStatus = 0;
            bool res = false;
            string FilePath = sDirectory + FileUpload1.FileName;
            FileUpload1.PostedFile.SaveAs(FilePath);
            ViewState["FileName"] = FileUpload1.FileName + "_" + DateTime.Now.ToString("dd_MM_yyyy_hh_mm_ss");

            // instance a memory stream and pass the

            if (Path.GetExtension(path) == ".xls")
            {

                oledbConn = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + FilePath + ";Extended Properties=Excel 4.0;Persist Security Info=False;IMEX=1");
            }
            else if (Path.GetExtension(path) == ".xlsx")
            {

                oledbConn = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + FilePath + ";Extended Properties=Excel 8.0;Persist Security Info=False;IMEX=1");
            }
            else
            {

            }
            DataSet ds = new DataSet();
            DataTable dt;
            using (var stream = File.Open(FilePath, FileMode.Open, FileAccess.Read))
            {
                var reader = ExcelReaderFactory.CreateReader(stream);
                var ds1 = reader.AsDataSet();
                 dt = ds1.Tables[0].Copy();
                dt.Rows[0].Delete();
                dt.AcceptChanges();
                dt.Columns[0].ColumnName = "StateCode";
                dt.Columns[1].ColumnName = "StateName";
                dt.Columns[2].ColumnName = "Admin State Name";
                dt.Columns[3].ColumnName = "Admin State Code";
                dt.Columns[4].ColumnName = "DistrictCode";
                dt.Columns[5].ColumnName = "DistrictName";
                dt.Columns[6].ColumnName = "AdminDistrictCode";
                dt.Columns[7].ColumnName = "AdminDistrictName";
                dt.Columns[8].ColumnName = "EGBlockCode";
                dt.Columns[9].ColumnName = "EG_Block";
                dt.Columns[10].ColumnName = "BlockCode";
                dt.Columns[11].ColumnName = "BlockName";
                dt.Columns[12].ColumnName = "ClusterCode";
                dt.Columns[13].ColumnName = "ClusterName";
                dt.Columns[14].ColumnName = "GP_CODE";
                dt.Columns[15].ColumnName = "GramPanchyat";
                dt.Columns[16].ColumnName = "VillageCode";
                dt.Columns[17].ColumnName = "VillageName";
                dt.Columns[18].ColumnName = "SchoolName";
                dt.Columns[19].ColumnName = "DISECODE";
                dt.Columns[20].ColumnName = "GOVTDISECODE";
                dt.Columns[21].ColumnName = "SchoolType";
                dt.Columns[22].ColumnName = "OPERATIONAL";
                dt.Columns[23].ColumnName = "Management";
                dt.Columns[24].ColumnName = "MergeVillageCOde";
            }

         
            //oledbConn.Open();
            //OleDbCommand cmd = new OleDbCommand(); ;





            // string Q = "SELECT Sno,StateName,StateCode,DistrictName,DistrictCode,BlockName,BlockCode,EGBlock,EGBlockCode,GramPanchyat,GP_CODE,ClusterName,ClusterCode,VillageName,VillageCode,SchoolName,GOVTDISECODE,DISECODE,Operational_NON_Operational,Management,SchoolType  FROM [JHALAWAR DATA$]";
            //string Q = "SELECT * FROM [Sheet1$]";
            //OleDbDataAdapter oleda = new OleDbDataAdapter(Q, oledbConn);
            //oleda.Fill(ds);

            DataTable dtState = new DataTable();
            DataTable dtDistrict = new DataTable();
            DataTable dtBlock = new DataTable();
            DataTable dtCluster = new DataTable();
            DataTable dtPanchayat = new DataTable();
            DataTable dtVillage = new DataTable();
            DataTable dtSchool = new DataTable();
            // DataTable dt = ds.Tables[0];
            //int count1 = dt.Rows.Count;
            //lbl_messages.Text = count1.ToString();
            //ModalAlert.Show();
            int count = 0;
            foreach (DataColumn dc in dt.Columns)
            {


                if (dc.ColumnName == "StateCode")
                {
                    count++;
                }
                if (dc.ColumnName == "SchoolType")
                {
                    count++;
                }

            }
            if (count == 2)
            {

                //if (dt.Rows.Count > 0)
                //{
                //   foreach(DataRow dr in dt.Rows)
                //   {
                //       if (dr["StateCode"].ToString() != "" && dr["DistrictCode"].ToString() != "")
                //       {
                //           if (dr["OPERATIONAL"].ToString() == "OPERATIONAL")
                //           {
                //               WorkingStatus = 1;
                //           }
                //           else if (dr["OPERATIONAL"].ToString() == "NON OPERATIONAL")
                //           {
                //               WorkingStatus = 2;
                //           }
                //           if (dr["Management"].ToString() == "GOVERNMENT")
                //           {
                //               ManagementType = 1;

                //           }
                //           else if (dr["Management"].ToString() == "PRIVATE")
                //           {
                //               ManagementType = 2;

                //           }

                //           string str1 = "insert into Temp_District_ImportExcel ([StateCode],[StateName],DistrictCode, DistrictName,BlockCode, BlockName, EGBlockCode, EG_Block,ClusterName,ClusterCode,GP_CODE, GramPanchayat,VillageCode, VillageName,DISECODE, SchoolName, SchoolType, Managament, [Operational], GOVTDISECODE ) values ('" + dr["StateCode"] + "','" + dr["StateName"] + "','" + dr["DistrictCode"] + "','" + dr["DistrictName"] + "','" + dr["BlockCode"] + "','" + dr["BlockName"] + "','" + dr["EGBlockCode"] + "','" + dr["EG_Block"] + "','" + dr["ClusterName"] + "','" + dr["ClusterCode"] + "','" + dr["GP_CODE"] + "','" + dr["GramPanchyat"] + "','" + dr["VillageCode"] + "','" + dr["VillageName"] + "','" + dr["DISECODE"] + "','" + dr["SchoolName"].ToString().Replace("'", "''") + "'," + dr["SchoolType"] + "," + ManagementType + "," + WorkingStatus + ",'" + dr["GOVTDISECODE"] + "' )";
                //           res = Objcls.AddUpdate(str1);
                //       }
                //   }

                //}
                //if (res == true)
                //{


                //}

                String[] arColoumn = { "StateCode", "StateName" };
                dtState = dt.DefaultView.ToTable(true, arColoumn);
                String[] arColoumn1 = { "StateCode", "DistrictCode", "DistrictName" };
                dtDistrict = dt.DefaultView.ToTable(true, arColoumn1);
                String[] arColoumn2 = { "StateCode", "DistrictCode", "EGBlockCode", "EG_Block" };
                dtBlock = dt.DefaultView.ToTable(true, arColoumn2);
                String[] arColoumn3 = { "StateCode", "DistrictCode", "EGBlockCode", "ClusterName", "ClusterCode" };
                dtCluster = dt.DefaultView.ToTable(true, arColoumn3);
                String[] arColoumn4 = { "StateCode", "DistrictCode", "EGBlockCode", "GP_CODE", "GramPanchyat" };
                dtPanchayat = dt.DefaultView.ToTable(true, arColoumn4);
                String[] arColoumn5 = { "StateCode", "DistrictCode", "EGBlockCode", "BlockCode", "BlockName", "ClusterCode", "GP_CODE", "GramPanchyat", "VillageCode", "VillageName", "AdminDistrictCode", "AdminDistrictName", "MergeVillageCOde", "Admin State Code", "Admin State Name" };
                dtVillage = dt.DefaultView.ToTable(true, arColoumn5);
                String[] arColoumn6 = { "VillageCode", "DISECODE", "SchoolName", "SchoolType", "Management", "Operational", "GOVTDISECODE" };
                dtSchool = dt.DefaultView.ToTable(true, arColoumn6);
                // ********************** Insert State
                String[] arColoumn7 = { "DistrictCode", "EGBlockCode", "GP_CODE", "VillageCode", "DISECODE" };
                DataTable dtTemp = dt.DefaultView.ToTable(true, arColoumn7);

                string str = "";

               
                SqlParameter[] cmdParameters = new SqlParameter[]
      {
            new SqlParameter("@Condition", "")
      };
                int ivv= SqlHelper.ExecuteNonQuery(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "usp_Import_ResetStaging", cmdParameters);
                //str = "Truncate Table T_mstState Truncate Table T_mstBlock Truncate Table T_mstSchool Truncate Table T_mstVillage";
                //res = Objcls.AddUpdate(str);
                //str = "Truncate Table Temp_District_ImportExcel_Error ";
                //res = Objcls.AddUpdate(str);
                if (dtState.Rows.Count > 0)
                {
                    //TempDistProfile
                    //                    Truncate Table T_mstBlock
                    //Truncate Table T_mstDistrict
                    //Truncate Table T_mstCluster 
                    //Truncate Table T_mstPanchayat 
                    //Truncate Table T_mstVillage 
                    //Truncate Table TempDistProfile
                    //str = "Truncate Table TempDistProfile ";
                    //res = Objcls.AddUpdate(str);
                    //   objComman.BulkCopyTempDistProfile(dtTemp);
                    foreach (DataRow dr in dtState.Rows)
                    {
                        DataTable DtCheckState = obj.LoadData("Select StateCode from T_mstState where StateCode='" + dr["StateCode"].ToString() + "'");

                        if (dr["StateCode"].ToString() != "")
                        {
                            //if (DtCheckState.Rows.Count > 0)
                            //{
                            //    str = "update [T_mstState] set [StateName]='" + (dr["StateName"]).ToString() + "' where Statecode= '" + dr["Statecode"].ToString() + "'";
                            //    res = Objcls.AddUpdate(str);
                            //}

                            //else
                            //{
                            if (dtState.Rows.Count > 0)
                            {
                               
                                    SqlParameter[] cmdParameters1 = new SqlParameter[]
                                    {
                                    new SqlParameter("@StateCode", SqlDbType.NVarChar, 50)  { Value = Val(dr["StateCode"]) },
                                    new SqlParameter("@StateName", SqlDbType.NVarChar, 250) { Value = Val(dr["StateName"]) }
                                    };

                                    SqlHelper.ExecuteNonQuery(SqlHelper.mainConnectionString,
                                                              CommandType.StoredProcedure,
                                                              "usp_mstState_Insert", cmdParameters1);
                                }
                            
                        }
                    }
                }
                // ********************** Insert District 
               
                if (dtDistrict.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtDistrict.Rows)
                    {
                        DataTable DtCheckDistrict = obj.LoadData("Select DistrictCode from [T_mstDistrict] where DistrictCode='" + dr["DistrictCode"].ToString() + "'");

                        if (dr["DistrictCode"].ToString() != "")
                        {
                            SqlParameter[] cmdParameters3 = new SqlParameter[]
                                 {
                                    new SqlParameter("@StateCode",    SqlDbType.NVarChar, 50)  { Value = Val(dr["StateCode"])    },
                                    new SqlParameter("@DistrictCode", SqlDbType.NVarChar, 50)  { Value = Val(dr["DistrictCode"]) },
                                    new SqlParameter("@DistrictName", SqlDbType.NVarChar, 250) { Value = Val(dr["DistrictName"]) }
                                 };

                                                    SqlHelper.ExecuteNonQuery(SqlHelper.mainConnectionString,
                                                                              CommandType.StoredProcedure,
                                                                              "usp_mstDistrict_Insert", cmdParameters3);

                        }
                    }
                }
                // ********************** Insert Block
              
                if (dtBlock.Rows.Count > 0)
                {

                    foreach (DataRow dr in dtBlock.Rows)
                    {
                        //  DataTable DtCheckBlock = obj.LoadData("Select BlockCode from [T_mstBlock] where BlockCode='" + dr["EGBlockCode"].ToString() + "'");
                        if (dr["EGBlockCode"].ToString() != "")
                        {
                            SqlParameter[] cmdParameters4 = new SqlParameter[]
        {
            new SqlParameter("@StateCode",    SqlDbType.NVarChar, 50)  { Value = Val(dr["StateCode"])    },
            new SqlParameter("@DistrictCode", SqlDbType.NVarChar, 50)  { Value = Val(dr["DistrictCode"]) },
            new SqlParameter("@BlockCode",    SqlDbType.NVarChar, 50)  { Value = Val(dr["EGBlockCode"])  },
            new SqlParameter("@BlockName",    SqlDbType.NVarChar, 250) { Value = Val(dr["EG_Block"])     }
        };

                            SqlHelper.ExecuteNonQuery(SqlHelper.mainConnectionString,
                                                      CommandType.StoredProcedure,
                                                      "usp_mstBlock_Insert", cmdParameters4);
                        }


                    }
                }
                // ********************** Insert Cluster
                //str = "Truncate Table T_mstCluster ";
                //res = Objcls.AddUpdate(str);
                //if (dtCluster.Rows.Count > 0)
                //{

                //    foreach (DataRow dr in dtCluster.Rows)
                //    {
                //        // DataTable DtCheckCluster = obj.LoadData("Select ClusterCode from [T_mstCluster] where ClusterCode='" + dr["ClusterCode"].ToString() + "'");
                //        if (dr["ClusterCode"].ToString() != "")
                //        {
                //            //if (DtCheckCluster.Rows.Count > 0)
                //            //{
                //            //    str = "update  [T_mstCluster] set  StateCode='" + dr["StateCode"].ToString() + "', DistrictCode='" + dr["DistrictCode"].ToString() + "',BlockCode='" + dr["EGBlockCode"].ToString() + "', [ClusterName]='" + dr["ClusterName"] + "' where ClusterCode= '" + dr["ClusterCode"].ToString() + "'";
                //            //    res = Objcls.AddUpdate(str);
                //            //}

                //            //else
                //            //{
                //            str = "insert into [T_mstCluster] (  [StateCode],[DistrictCode] ,[BlockCode] ,[ClusterCode],[ClusterName] )  values('" + dr["StateCode"] + "','" + dr["DistrictCode"] + "','" + dr["EGBlockCode"] + "','" + dr["ClusterCode"] + "','" + dr["ClusterName"] + "' )";
                //            res = Objcls.AddUpdate(str);
                //            //}
                //        }


                //    }
                //}
                //using (StreamWriter sw = File.AppendText(FilePathError))
                //{


                //    sw.WriteLine("T_mstBlock");
            
                //}
                // ********************** Insert Panchayat
               
                if (dtPanchayat.Rows.Count > 0)
                {

                    //foreach (DataRow dr in dtPanchayat.Rows)
                    //{
                    //    // string PanChahayatCode = "", PanChahayatName="";
                    //    //DataTable DtCheckPanchayat = obj.LoadData("Select PanchayatCode from [T_mstPanchayat] where PanchayatCode='" + dr["GP_CODE"].ToString() + "'");
                    //    //if (dr["GramPanchyat"].ToString() == dr["VillageName"].ToString())
                    //    //{
                    //    //    PanChahayatCode = dr["VillageCode"].ToString();
                    //    //}
                    //    //else
                    //    //{
                    //    //    PanChahayatCode = dr["GP_CODE"].ToString();
                    //    //}
                    //    //if (dr["GP_CODE"].ToString() == dr["VillageCode"].ToString())
                    //    //{
                    //    //    PanChahayatName = dr["VillageName"].ToString();
                    //    //}
                    //    //else
                    //    //{
                    //    //    PanChahayatName = dr["GramPanchyat"].ToString();
                    //    //}
                    //    if (dr["GP_CODE"].ToString() != "")
                    //    {
                    //        //if (DtCheckPanchayat.Rows.Count > 0)
                    //        //{
                    //        //    if (dr["OldPanchayatCode"].ToString().Length > 3)
                    //        //    {


                    //        //        str = "update  [T_mstPanchayat] set  StateCode='" + dr["StateCode"].ToString() + "', OldPanchayatCode='" + dr["OldPanchayatCode"].ToString() + "'  where PanchayatCode= '" + dr["GP_CODE"] + "' ";
                    //        //        res = Objcls.AddUpdate(str);
                    //        //    }
                    //        //}

                    //        //else
                    //        //{
                    //        str = "insert into [T_mstPanchayat] (  [StateCode],[DistrictCode] ,[BlockCode] ,[PanchayatCode],[PanchayatName],EGPanchayatCode )  values('" + dr["StateCode"] + "','" + dr["DistrictCode"] + "','" + dr["EGBlockCode"] + "','" + dr["GP_CODE"] + "','" + dr["GramPanchyat"] + "' ,'" + dr["GP_CODE"] + "' )";
                    //        res = Objcls.AddUpdate(str);
                    //        //}
                    //    }


                    //}
                    BulkCopySchoolPanchat(dtPanchayat);
                }
                // ********************** Insert Village
                using (StreamWriter sw = File.AppendText(FilePathError))
                {


                    sw.WriteLine("T_mstPanchayat");

                   
                }
               
                if (dtVillage.Rows.Count > 0)
                {

                    //foreach (DataRow dr in dtVillage.Rows)
                    //{

                    //    // DataTable DtCheckVillage = obj.LoadData("Select VillageCode from [T_mstVillage] where VillageCode='" + dr["VillageCode"].ToString() + "'");

                    //    if (dr["VillageCode"].ToString() != "")
                    //    {
                    //        //if (DtCheckVillage.Rows.Count > 0)
                    //        //{
                    //        //    str = "update  [T_mstVillage] set  StateCode='" + dr["StateCode"].ToString() + "', DistrictCode='" + dr["DistrictCode"].ToString() + "',BlockCode='" + dr["EGBlockCode"].ToString() + "',[PanchayatCode]='" + dr["GP_CODE"] + "', [VillageName]='" + dr["VillageName"] + "' where VillageCode= '" + dr["VillageCode"].ToString() + "'";
                    //        //    res = Objcls.AddUpdate(str);
                    //        //}

                    //        //else
                    //        //{
                    //        str = "insert into [T_mstVillage] (  [StateCode],[DistrictCode] ,[BlockCode] ,[MainBlockCode],[MainBlockName],[ClusterCode],[PanchayatCode],[VillageCode],[VillageName] ,AdminDistrictCode,AdminDistrictName,EGVillageCode,MergeVillageCOde,[EG State Name],[EGState Code])  values('" + dr["StateCode"] + "','" + dr["DistrictCode"] + "','" + dr["EGBlockCode"] + "','" + dr["BlockCode"] + "','" + dr["BlockName"] + "','" + dr["ClusterCode"] + "','" + dr["GP_CODE"] + "', '" + dr["VillageCode"] + "','" + dr["VillageName"] + "' , '" + dr["AdminDistrictCode"] + "', '" + dr["AdminDistrictName"] + "', '" + dr["VillageCode"] + "', '" + dr["MergeVillageCOde"] + "', '" + dr["Admin State Name"] + "', '" + dr["Admin State Code"] + "')";
                    //        res = Objcls.AddUpdate(str);
                    //        // }
                    //    }


                    //}
                    BulkCopySchoolVillage(dtVillage);
                    //string  RowAffect = INSERT_ImportDataSingle(dtVillage, "[IMPORTTestVillage]", "T_mstVillage", "True");
                }

                using (StreamWriter sw = File.AppendText(FilePathError))
                {


                    sw.WriteLine("T_mstVillage");


                }
                // ********** Insert Scholl
              
                WorkingStatus = 0;
                if (dtSchool.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtSchool.Rows)
                    {

                        // DataTable DtCheckschool = obj.LoadData("Select SchoolCode from [T_mstSchool] where SchoolCode='" + dr["DISECODE"].ToString() + "'");
                        if (dr["DISECODE"].ToString() != "")
                        {
                            if (dr["OPERATIONAL"].ToString().Trim() == "OPERATIONAL")
                            {
                                dr["OPERATIONAL"] = 1;
                                WorkingStatus = 1;
                            }

                            if (dr["OPERATIONAL"].ToString() == "Close")
                            {
                                dr["OPERATIONAL"] = 3;
                                WorkingStatus = 3;
                            }
                            if (dr["OPERATIONAL"].ToString() == "Marge")
                            {
                                dr["OPERATIONAL"] = 4;
                                WorkingStatus = 4;
                            }
                            if (dr["OPERATIONAL"].ToString() == "")
                            {
                                dr["OPERATIONAL"] = 0;
                                WorkingStatus = 0;
                            }

                            else if (dr["OPERATIONAL"].ToString() == "NON OPERATIONAL" || dr["OPERATIONAL"].ToString() == "NON-OPERATIONAL")
                            {
                                dr["OPERATIONAL"] = 2;
                                WorkingStatus = 2;
                            }
                            if (dr["Management"].ToString() == "GOVERNMENT")
                            {
                                ManagementType = 1;
                                dr["Management"] = 1;
                            }
                            else if (dr["Management"].ToString() == "PRIVATE")
                            {
                                ManagementType = 2;
                                dr["Management"] = 2;
                            }
                            else if (dr["Management"].ToString() == "GOVERNMENT AIDED")
                            {
                                ManagementType = 3;
                                dr["Management"] = 3;
                            }
                            else if (dr["Management"].ToString() == "MADARSA WITH FLN")
                            {
                                ManagementType = 4;
                                dr["Management"] = 4;
                            }
                            else if (dr["Management"].ToString() == "")
                            {
                                ManagementType = 0;
                                dr["Management"] = 0;
                            }
                            dr["SchoolName"] = dr["SchoolName"].ToString().Replace("'", "''");
                            //if (DtCheckschool.Rows.Count > 0)
                            //{
                            //    str = "update [T_mstSchool] set  WorkingStatus=" + WorkingStatus + ",ManagementType=" + ManagementType + ", [VillageCode]='" + dr["VillageCode"] + "',[SchoolCodeID]='" + dr["DISECODE"] + "',[DISECode]='" + dr["DISECODE"] + "',[DISECode1]='" + dr["DISECODE"] + "',[DISECode2]='" + dr["DISECODE"] + "',[Name]='" + dr["SchoolName"].ToString().Replace("'", "''") + "',[Name1]='" + dr["SchoolName"].ToString().Replace("'", "''") + "',[Name2]='" + dr["SchoolName"].ToString().Replace("'", "''") + "',[SchoolLevel]='" + dr["SchoolType"] + "',[SchoolLevel1]='" + dr["SchoolType"] + "',[SchoolLevel2]='" + dr["SchoolType"] + "', [SchoolCodeTemp]='" + dr["GOVTDISECODE"] + "' where [SchoolCode]='" + dr["DISECODE"] + "'";
                            //    res = Objcls.AddUpdate(str);
                            //}
                            //else
                            //{
                            //str = "insert into [T_mstSchool] (WorkingStatus,ManagementType,[VillageCode],[SchoolCode],[SchoolCodeID],[DISECode],[DISECode1],[DISECode2],[Name],[Name1],[Name2],[SchoolLevel],[SchoolLevel1],[SchoolLevel2],[SchoolCodeTemp]) values(" + WorkingStatus + "," + ManagementType + ",'" + dr["VillageCode"] + "','" + dr["DISECODE"] + "','" + dr["DISECODE"] + "','" + dr["DISECODE"] + "','" + dr["DISECODE"] + "','" + dr["DISECODE"] + "','" + dr["SchoolName"].ToString().Replace("'", "''") + "','" + dr["SchoolName"].ToString().Replace("'", "''") + "','" + dr["SchoolName"].ToString().Replace("'", "''") + "','" + dr["SchoolType"] + "','" + dr["SchoolType"] + "','" + dr["SchoolType"] + "','" + dr["GOVTDISECODE"] + "') ";
                            //res = Objcls.AddUpdate(str);
                            ////}
                        }
                    }
                    BulkCopySchool(dtSchool);
                }
                using (StreamWriter sw = File.AppendText(FilePathError))
                {


                    sw.WriteLine("T_mstSchool");


                }
                // string RowAffect1 = INSERT_ImportDataSingle(dtVillage, "[IMPORTTestSchool]", "T_mstSchool", "True");
                if (res == true)
                {
                    using (StreamWriter sw = File.AppendText(FilePathError))
                    {


                        sw.WriteLine("True");


                    }
                    string Result = "";
                    DataSet RowAffected = new DataSet();
                    RowAffected = SP_Check_District_Excel_Import();


                    for (int i = 0; i < RowAffected.Tables.Count; i++)
                    {
                        if (RowAffected.Tables[i].Rows.Count > 0)
                        {
                            if (RowAffected.Tables[i].Rows[0]["RetValue"].ToString() != null)
                            {
                                Result = RowAffected.Tables[i].Rows[0]["RetValue"].ToString();

                            }
                        }

                    }
                    //lbl_messages.Text = "Import Successfull...";
                    //                ModalAlert.Show();
                    using (StreamWriter sw = File.AppendText(FilePathError))
                    {


                        sw.WriteLine("True1");


                    }
                    if (Result == "")
                    {
                        using (StreamWriter sw = File.AppendText(FilePathError))
                        {


                            sw.WriteLine("True3");


                        }
                        btnApprove.Visible = true;
                        lbl_messages.Text = "Data Verify successfully please approve";
                        ModalAlert.Show();

                    }
                    else if (RowAffected.Tables.Count > 0)
                    {
                        using (StreamWriter sw = File.AppendText(FilePathError))
                        {


                            sw.WriteLine("True4");


                        }
                        DataTable ErrorData = Objcls.ExcelErrorData();
                        MultipuExeclProcess(ErrorData);
                        //ExporttoExcel(ErrorData);
                    }

                }
                // binding form data with grid view
                //GV.DataSource = ds.Tables[0].DefaultView;
                //GV.DataBind();


            }
            else
            {
                lbl_messages.Text = "Invalid Excel....";
                ModalAlert.Show();
            }
        }
        // need to catch possible exceptions
        catch (Exception ex)
        {
            var line = Environment.NewLine + Environment.NewLine;

           string  ErrorlineNo = ex.StackTrace.Substring(ex.StackTrace.Length - 7, 7);
            string Errormsg = ex.GetType().Name.ToString();
            string extype = ex.GetType().ToString();
         
            string ErrorLocation = ex.Message.ToString();
            using (StreamWriter sw = File.AppendText(FilePathError))
            {
                // string error = "Log Written Date:" + " " + DateTime.Now.ToString() + line + "Error Line No :" + " " + ErrorlineNo + line + "Error Message:" + " " + Errormsg + line + "Exception Type:" + " " + extype + line + "Error Location :" + " " + ErrorLocation + line + "" ;
                string error = "ABC";
                sw.WriteLine(line);
                sw.WriteLine(error);

                sw.WriteLine(line);
                sw.Flush();
                sw.Close();

            }
            lbl_messages.Text = ex.ToString();
            ModalAlert.Show();

        }
        finally
        {
            oledbConn.Close();
        }
    }
	public Boolean BulkCopySchoolVillage(DataTable dt)
    {
        try
        {
     		SqlBulkCopyColumnMapping mapping01 = new SqlBulkCopyColumnMapping("StateCode", "StateCode");
            SqlBulkCopyColumnMapping mapping02 = new SqlBulkCopyColumnMapping("DistrictCode", "DistrictCode");
            SqlBulkCopyColumnMapping mapping03 = new SqlBulkCopyColumnMapping("EGBlockCode", "BlockCode");
            SqlBulkCopyColumnMapping mapping04 = new SqlBulkCopyColumnMapping("BlockCode", "MainBlockCode");
            SqlBulkCopyColumnMapping mapping05 = new SqlBulkCopyColumnMapping("BlockName", "MainBlockName");
            SqlBulkCopyColumnMapping mapping06 = new SqlBulkCopyColumnMapping("ClusterCode", "ClusterCode");
            SqlBulkCopyColumnMapping mapping08 = new SqlBulkCopyColumnMapping("GP_CODE", "PanchayatCode");
            SqlBulkCopyColumnMapping mapping09 = new SqlBulkCopyColumnMapping("VillageCode", "VillageCode");
            SqlBulkCopyColumnMapping mapping10 = new SqlBulkCopyColumnMapping("VillageName", "VillageName");
            SqlBulkCopyColumnMapping mapping11 = new SqlBulkCopyColumnMapping("AdminDistrictCode", "AdminDistrictCode");
            SqlBulkCopyColumnMapping mapping12 = new SqlBulkCopyColumnMapping("AdminDistrictName", "AdminDistrictName");
            SqlBulkCopyColumnMapping mapping13 = new SqlBulkCopyColumnMapping("VillageCode", "EGVillageCode");
            SqlBulkCopyColumnMapping mapping14 = new SqlBulkCopyColumnMapping("MergeVillageCOde", "MergeVillageCOde");
            SqlBulkCopyColumnMapping mapping15 = new SqlBulkCopyColumnMapping("Admin State Name", "EG State Name");
            SqlBulkCopyColumnMapping mapping16 = new SqlBulkCopyColumnMapping("Admin State Code", "EGState Code");
            SqlBulkCopy bulkCopy = new SqlBulkCopy(SqlHelper.mainConnectionString);
            bulkCopy.BatchSize = 20000;
            bulkCopy.BulkCopyTimeout = 0;
            bulkCopy.ColumnMappings.Add(mapping01);
            bulkCopy.ColumnMappings.Add(mapping02);
            bulkCopy.ColumnMappings.Add(mapping03);
            bulkCopy.ColumnMappings.Add(mapping04);
            bulkCopy.ColumnMappings.Add(mapping05);
            bulkCopy.ColumnMappings.Add(mapping06);
            bulkCopy.ColumnMappings.Add(mapping08);
            bulkCopy.ColumnMappings.Add(mapping09);
            bulkCopy.ColumnMappings.Add(mapping10);
            bulkCopy.ColumnMappings.Add(mapping11);
            bulkCopy.ColumnMappings.Add(mapping12);
            bulkCopy.ColumnMappings.Add(mapping13);
            bulkCopy.ColumnMappings.Add(mapping14);
            bulkCopy.ColumnMappings.Add(mapping15);
            bulkCopy.ColumnMappings.Add(mapping16);
            bulkCopy.DestinationTableName = "T_mstvillage";
            bulkCopy.NotifyAfter = 200;
            bulkCopy.WriteToServer(dt);
            return true;
        }
        catch
        {
            return false;
        }

    }
    public Boolean BulkCopySchoolPanchat(DataTable dt)
    {
        try
        {
            //
            SqlBulkCopyColumnMapping mapping01 = new SqlBulkCopyColumnMapping("StateCode", "StateCode");
            SqlBulkCopyColumnMapping mapping02 = new SqlBulkCopyColumnMapping("DistrictCode", "DistrictCode");
            SqlBulkCopyColumnMapping mapping03 = new SqlBulkCopyColumnMapping("EGBlockCode", "BlockCode");
            SqlBulkCopyColumnMapping mapping04 = new SqlBulkCopyColumnMapping("GP_CODE", "PanchayatCode");
            SqlBulkCopyColumnMapping mapping05 = new SqlBulkCopyColumnMapping("GramPanchyat", "PanchayatName");
            SqlBulkCopyColumnMapping mapping06 = new SqlBulkCopyColumnMapping("GP_CODE", "EGPanchayatCode");
            SqlBulkCopy bulkCopy = new SqlBulkCopy(SqlHelper.mainConnectionString);
            bulkCopy.BatchSize = 5000;
            bulkCopy.BulkCopyTimeout = 0;
            bulkCopy.ColumnMappings.Add(mapping01);
            bulkCopy.ColumnMappings.Add(mapping02);
            bulkCopy.ColumnMappings.Add(mapping03);
            bulkCopy.ColumnMappings.Add(mapping04);
            bulkCopy.ColumnMappings.Add(mapping05);
            bulkCopy.ColumnMappings.Add(mapping06);
            bulkCopy.DestinationTableName = "T_mstPanchayat";
            bulkCopy.NotifyAfter = 200;
            bulkCopy.WriteToServer(dt);
            return true;
        }
        catch
        {
            return false;
        }

    }
    public Boolean BulkCopySchool(DataTable dt)
    {
        try
        {
            SqlBulkCopyColumnMapping mapping01 = new SqlBulkCopyColumnMapping("VillageCode", "VillageCode");
            SqlBulkCopyColumnMapping mapping02 = new SqlBulkCopyColumnMapping("DISECODE", "SchoolCode");
            SqlBulkCopyColumnMapping mapping03 = new SqlBulkCopyColumnMapping("DISECODE", "DISECode");
            SqlBulkCopyColumnMapping mapping04 = new SqlBulkCopyColumnMapping("DISECODE", "SchoolCodeID");
            SqlBulkCopyColumnMapping mapping05 = new SqlBulkCopyColumnMapping("SchoolName", "Name");
            SqlBulkCopyColumnMapping mapping06 = new SqlBulkCopyColumnMapping("SchoolType", "SchoolLevel");
            SqlBulkCopyColumnMapping mapping07 = new SqlBulkCopyColumnMapping("SchoolType", "SchoolLevel1");
            SqlBulkCopyColumnMapping mapping08 = new SqlBulkCopyColumnMapping("SchoolType", "SchoolLevel2");
            SqlBulkCopyColumnMapping mapping09 = new SqlBulkCopyColumnMapping("GOVTDISECODE", "Govt_DiseCode");
            SqlBulkCopyColumnMapping mapping10 = new SqlBulkCopyColumnMapping("Management", "ManagementType");
            SqlBulkCopyColumnMapping mapping11 = new SqlBulkCopyColumnMapping("Operational", "WorkingStatus");
            SqlBulkCopy bulkCopy = new SqlBulkCopy(SqlHelper.mainConnectionString);
            bulkCopy.BatchSize = 10000;
            bulkCopy.BulkCopyTimeout = 0;
            bulkCopy.ColumnMappings.Add(mapping01);
            bulkCopy.ColumnMappings.Add(mapping02);
            bulkCopy.ColumnMappings.Add(mapping03);
            bulkCopy.ColumnMappings.Add(mapping04);
            bulkCopy.ColumnMappings.Add(mapping05);
            bulkCopy.ColumnMappings.Add(mapping06);
            bulkCopy.ColumnMappings.Add(mapping07);
            bulkCopy.ColumnMappings.Add(mapping08);
            bulkCopy.ColumnMappings.Add(mapping09);
            bulkCopy.ColumnMappings.Add(mapping10);
            bulkCopy.ColumnMappings.Add(mapping11);

            bulkCopy.DestinationTableName = "T_mstSchool";
            bulkCopy.NotifyAfter = 200;
            bulkCopy.WriteToServer(dt);
            return true;
        }
        catch
        {
            return false;
        }
    }

    public DataTable LoadMaster()
    {
        SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("@gg",""),

        };
        DataTable dt = null;


        dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rptLoadMasterData]", cmdParameters);
        return dt;
    }
    public DataSet SP_Check_District_Excel_Import()
    {
        SqlConnection sqlConnection = new SqlConnection(SqlHelper.mainConnectionString);
        try
        {
            if (sqlConnection.State != ConnectionState.Open)
            {
                sqlConnection.Open();
            }

            DataSet dbSqlDataSet = new DataSet();
            SqlCommand sqlcmd = new SqlCommand();
            sqlcmd.Connection = sqlConnection;
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.CommandText = "[SP_Check_District_Excel_Import]";
            SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
            da.Fill(dbSqlDataSet);
            return dbSqlDataSet;
        }
        catch (SqlException e)
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

            throw;
        }
        finally
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

        }
    }

    public DataSet SP_Check_District_Excel_Import_IN_Maintable()
    {
        SqlConnection sqlConnection = new SqlConnection(SqlHelper.mainConnectionString);
        try
        {
            if (sqlConnection.State != ConnectionState.Open)
            {
                sqlConnection.Open();
            }

            DataSet dbSqlDataSet = new DataSet();
            SqlCommand sqlcmd = new SqlCommand();
            sqlcmd.Connection = sqlConnection;
            sqlcmd.CommandTimeout = 0;
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.CommandText = "SP_Check_District_Excel_Import_MainTable";
            SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
            da.Fill(dbSqlDataSet);
            return dbSqlDataSet;
        }
        catch (SqlException e)
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

            throw;
        }
        finally
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

        }
    }

    public DataSet rptUinqueGenerate()
    {
        SqlConnection sqlConnection = new SqlConnection(SqlHelper.mainConnectionString);
        try
        {
            if (sqlConnection.State != ConnectionState.Open)
            {
                sqlConnection.Open();
            }

            DataSet dbSqlDataSet = new DataSet();
            SqlCommand sqlcmd = new SqlCommand();
            sqlcmd.Connection = sqlConnection;
            sqlcmd.CommandTimeout = 0;
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.CommandText = "rptUinqueGenerate";
            SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
            da.Fill(dbSqlDataSet);
            return dbSqlDataSet;
        }
        catch (SqlException e)
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

            throw;
        }
        finally
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

        }
    }
    protected void btnApprove_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlYear.SelectedIndex <= 0)
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Select Year')</script>", false);

                return;

            }
            DataSet RowAffected2 = new DataSet();
            RowAffected2 = obj.rptUpdateUniqueCode();

            DataSet RowAffected3 = new DataSet();
            int icount = rptUinqueGenerateSave();


            DataSet RowAffected1 = new DataSet();
            RowAffected1 = SP_Check_District_Excel_Import_IN_Maintable();


            if (RowAffected1 != null)
            {
                lbl_messages.Text = "Data Import Success..";
                ModalAlert.Show();
            }
        }
        catch (Exception ex)
        {
            lbl_messages.Text = ex.ToString();
            ModalAlert.Show();

        }
        finally
        {

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
        int GivenYear = GivenDate.Year ;
        int m = GivenDate.Month;

        DataTable dt = null;
        //ddlYear.Items.Add("--Select--","0");
        int y = GivenDate.Year;


        DateTime GivenDate1 = DateTime.Now;
        int GivenYear1 = GivenDate1.Year;
        DataTable dtYear = CreateDataTable();
        DataRow dr;
        if (ddlYear.SelectedIndex < 0)
        {

            string mYear1 = GivenYear1.ToString();
            for (int j = 0; j < 1; j++)
            {
                if (m > 3)
                {
                    dr = dtYear.NewRow();
                    dr["Type"] = GivenYear.ToString() + "-" + Convert.ToString((GivenYear + 1));
                    dr["ID"] = y;
                    dtYear.Rows.Add(dr);
                    dr = dtYear.NewRow();
                    dr["Type"] = GivenYear - 1 + "-" + Convert.ToString((GivenYear - 1 + 1));
                    dr["ID"] = y - 1;
                    dtYear.Rows.Add(dr);
                    //get last  two digits (eg: 10 from 2010);

                }
                else
                {

                    Int32 m7 = y + 1;
                    dr = dtYear.NewRow();
                    dr["Type"] = Convert.ToString((y)) + "-" + m7.ToString();
                    //y = y - 1;
                    dr["ID"] = y;
                    dtYear.Rows.Add(dr);
                    dr = dtYear.NewRow();
                    dr["Type"] = Convert.ToString((y - 1)) + "-" + y.ToString();
                    //y = y - 1;
                    dr["ID"] = y - 1;

                    dtYear.Rows.Add(dr);


                }

            }

        }
        //DataTable dtYear = objComman.Generate_Financial_Year();

        objComman.BindDLLMasterTable("mstSchool", "Type,ID", dtYear, conditions, "Type", "asc", ddlYear, "Type", "ID", "Select");

        ddlYear.SelectedIndex = 1;
    }
    public int rptUinqueGenerateSave()
    {
        SqlParameter[] cmdParameters = new SqlParameter[]
            {
              new SqlParameter("@Fyear", "2026-2027"),
        };
    int icount = SqlHelper.ExecuteNonQuery(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "rptUinqueGenerate", cmdParameters);

        return icount;
    }

    public void MultipuExeclProcess(DataTable table)
    {
        string StartupPath = Server.MapPath("~/Mou");
        string filepath = "";
        XLWorkbook wb = new XLWorkbook();
        wb = new XLWorkbook(StartupPath + "\\ErrorFile.xlsx");
        var ws = wb.Worksheet(1);
        //DataTable dt1 = dtMain1.Tables[1];

        //dt1.Columns.Remove("RowNo");
        ws.Cell(3, 1).InsertData(table.Rows);
        Int32 ii = Convert.ToInt32(table.Rows.Count) + 1;
        string str = "A3:Q" + ii;
        ws.Range(str).Style.Border.SetTopBorder(XLBorderStyleValues.Thin);
        ws.Range(str).Style.Border.SetBottomBorder(XLBorderStyleValues.Thin);

        ws.Range(str).Style.Border.SetLeftBorder(XLBorderStyleValues.Thin);
        ws.Range(str).Style.Border.SetRightBorder(XLBorderStyleValues.Thin);
        filepath = StartupPath + "\\ErrorFile " + "_" + System.DateTime.Now.ToString("hhssmmfff") + ".xlsx";
        wb.SaveAs(filepath);
        Response.ContentType = ContentType;
        Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(filepath));
        Response.WriteFile(filepath);

        Response.End();
        if (File.Exists(filepath))
        {
            System.IO.File.Delete(filepath);
        }

    }

    private void ExporttoExcel(DataTable table)
    {


        HttpContext.Current.Response.Clear();
        HttpContext.Current.Response.ClearContent();
        HttpContext.Current.Response.ClearHeaders();
        HttpContext.Current.Response.Buffer = true;
        HttpContext.Current.Response.ContentType = "application/ms-excel";
        HttpContext.Current.Response.Write(@"<!DOCTYPE HTML PUBLIC ""-//W3C//DTD HTML 4.0 Transitional//EN"">");
        string Fullfilename = "" + "ErrorReport" + "_" + DateTime.Now.ToString("dd_MM_yyyy_hh_mm_ss") + ".xls";

        HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=" + Fullfilename + " ");

        HttpContext.Current.Response.Charset = "utf-8";
        HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("windows-1250");
        //sets font
        HttpContext.Current.Response.Write("<font style='font-size:10.0pt; font-family:Calibri;'>");
        HttpContext.Current.Response.Write("<BR><BR><BR>");
        //sets the table border, cell spacing, border color, font of the text, background, foreground, font height
        HttpContext.Current.Response.Write("<Table border='1' bgColor='#ffffff' " +
          "borderColor='#000000' cellSpacing='0' cellPadding='0' " +
          "style='font-size:10.0pt; font-family:Calibri; background:white;'><TR> <TD colspan='13' style='font-size:13.0pt; text-align:center; color:blue; font-family:Calibri;' ><B>" + ViewState["FileName"] + "</B><TD></TR> <TR>");
        //am getting my grid's column headers
        int columnscount = table.Columns.Count;


        foreach (DataColumn dc in table.Columns)
        {      //write in new column
            HttpContext.Current.Response.Write("<Td>");
            //Get column headers  and make it as bold in excel columns
            HttpContext.Current.Response.Write("<B>");
            HttpContext.Current.Response.Write(dc.ColumnName);
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



    private void ExporttoExcelDist(DataTable table)
    {


        HttpContext.Current.Response.Clear();
        HttpContext.Current.Response.ClearContent();
        HttpContext.Current.Response.ClearHeaders();
        HttpContext.Current.Response.Buffer = true;
        HttpContext.Current.Response.ContentType = "application/ms-excel";
        HttpContext.Current.Response.Write(@"<!DOCTYPE HTML PUBLIC ""-//W3C//DTD HTML 4.0 Transitional//EN"">");
        string Fullfilename = "" + "DistProfile" +".xls";

        HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=" + Fullfilename + " ");

        HttpContext.Current.Response.Charset = "utf-8";
        HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("windows-1250");
        //sets font
        HttpContext.Current.Response.Write("<font style='font-size:10.0pt; font-family:Calibri;'>");
        HttpContext.Current.Response.Write("<BR><BR><BR>");
        //sets the table border, cell spacing, border color, font of the text, background, foreground, font height
        HttpContext.Current.Response.Write("<Table border='1' bgColor='#ffffff' " +
           "borderColor='#000000' cellSpacing='0' cellPadding='0' " +
           "style='font-size:10.0pt; font-family:Calibri; background:white;'> <TR>");        //am getting my grid's column headers
        int columnscount = table.Columns.Count;


        foreach (DataColumn dc in table.Columns)
        {      //write in new column
            HttpContext.Current.Response.Write("<Td>");
            //Get column headers  and make it as bold in excel columns
            HttpContext.Current.Response.Write("<B>");
            HttpContext.Current.Response.Write(dc.ColumnName);
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


    protected void LnkExport_Click(object sender, EventArgs e)
    {
        string filePath = Server.MapPath("~/Export/GovtTarget_Formate.xlsx");
        Response.ContentType = ContentType;
        Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(filePath));
        Response.WriteFile(filePath);
        Response.End();

    }
    protected void btnNewImport_Click(object sender, EventArgs e)
    {
        conditions = "";
        if (ddlYear.SelectedIndex > 0)
        {
            conditions += "  v.Fyear = '" + ddlYear.SelectedItem.Text + "' ";

        }
        if (ddlState.SelectedIndex > 0)
        {
            conditions += " and  v.StateCode = '" + ddlState.SelectedValue + "' ";           
        }
        if (ddlDistrict.SelectedIndex > 0)
        {
            conditions += " and v.DistrictCode = '" + ddlDistrict.SelectedValue + "' ";      
        }
        DataTable dt = Objcls.LoadMasterImport(conditions);

        MultipuExeclTrack(dt);
    }

    protected void btnNewImport1_Click(object sender, EventArgs e)
    {
        if (ddlDistrict.SelectedIndex<= 0)
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select District')</script>", false);

        }

        int icount = SaveDataInsertUpdate();
        if (icount > 0)
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Delete Successfully')</script>", false);            
        }
    }
    public int SaveDataInsertUpdate()
    {
        int Icount = 0;
        try
        {

            SqlParameter[] cmdParameters = new SqlParameter[]
            {
            new SqlParameter("@Dist", ddlDistrict.SelectedValue),          

            };
            Icount = SqlHelper.ExecuteNonQuery(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "DeleteMainmaster", cmdParameters);
        }
        catch
        {

        }
        return Icount;
    }

    public void MultipuExeclTrack(DataTable dt)
    {
        string StartupPath = Server.MapPath("~/Export");
        string filepath = "";
        XLWorkbook wb = new XLWorkbook();
        wb = new XLWorkbook(StartupPath + "\\DistUpload.xlsx");
        var ws = wb.Worksheet(1);
        //var ws1 = wb.Worksheet(2);
        //var ws3 = wb.Worksheet(3);

        //dt.Columns.Remove("rownNO");
        //DataTable dt1 = dtMain1.Tables[1];

        //dt1.Columns.Remove("rownNO");
        ws.Cell(2, 1).InsertData(dt.Rows);
        Int32 ii = Convert.ToInt32(dt.Rows.Count) + 1;
        string str = "A2:Y" + ii;
        ws.Range(str).Style.Border.SetTopBorder(XLBorderStyleValues.Thin);
        ws.Range(str).Style.Border.SetBottomBorder(XLBorderStyleValues.Thin);

        ws.Range(str).Style.Border.SetLeftBorder(XLBorderStyleValues.Thin);
        ws.Range(str).Style.Border.SetRightBorder(XLBorderStyleValues.Thin);






        filepath = StartupPath + "\\DistUpload" + "_" + System.DateTime.Now.ToString("hhssmmfff") + ".xlsx";
        wb.SaveAs(filepath);
        Response.ContentType = ContentType;
        Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(filepath));
        Response.WriteFile(filepath);

        Response.End();
        if (File.Exists(filepath))
        {
            System.IO.File.Delete(filepath);
        }

    }

    public string INSERT_ImportDataSingle(DataTable dt, string strSP_Name, string strParentTable_Name, string Flag)
    {
        string getresult = "";
        string R_Import = string.Empty;
        string strtemptblmstGroupChk = "IF OBJECT_ID('tempdb.#temp_" + strParentTable_Name + "') IS NOT NULL DROP TABLE #temp_" + strParentTable_Name + "";
        string strtemptblmstGroup = string.Empty;
        SqlConnection ConStr = new SqlConnection();
        ConStr = new SqlConnection(SqlHelper.mainConnectionString);
        if (strParentTable_Name == "T_mstSchool")
        {
            strtemptblmstGroup = "";
            strtemptblmstGroup += " SELECT WorkingStatus,ManagementType,[VillageCode],[SchoolCode],[SchoolCodeID],[DISECode],[DISECode1],[DISECode2],[Name],[Name1],[Name2],[SchoolLevel],[SchoolLevel1],[SchoolLevel2],[SchoolCodeTemp],OldSchoolUniqueCode,OldVillageUniqueCode ";
      

            strtemptblmstGroup += " INTO #temp_" + strParentTable_Name + " FROM " + strParentTable_Name + " ";
            strtemptblmstGroup += " where DISECode is null ";
            // ConStr = new SqlConnection("Data Source=EducateGirls.db.3975866.hostedresource.com;Initial Catalog=EducateGirls;User Id=educategirls;Password=mw2Master1EG0!");

        }
     
        if (strParentTable_Name == "T_mstVillage")
        {
            strtemptblmstGroup = "";
            strtemptblmstGroup += " SELECT  [StateCode],[DistrictCode] ,[BlockCode] ,[MainBlockCode],[MainBlockName],[ClusterCode],[GP_CODE],[VillageCode],[VillageName],OldUniqueCode ";


            strtemptblmstGroup += " INTO #temp_" + strParentTable_Name + " FROM " + strParentTable_Name + " ";
            strtemptblmstGroup += " where VillageCode is null ";
            // ConStr = new SqlConnection("Data Source=EducateGirls.db.3975866.hostedresource.com;Initial Catalog=EducateGirls;User Id=educategirls;Password=mw2Master1EG0!");

        }      
        getresult = objComman.INSERT_ImportDataSingleSP(dt, strSP_Name, strParentTable_Name, strtemptblmstGroupChk, strtemptblmstGroup, Flag, ConStr);
        return getresult;
    }
}