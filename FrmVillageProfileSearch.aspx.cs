using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;

public partial class FrmVillageProfileSearch : System.Web.UI.Page
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

                TxtFromDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                txtDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            }
        }
        else
        {
            Response.Redirect("Login.aspx", false);

        }
    }
    public void LoadData()
    {

        conditions = "UserLevel=24";
        if (Session["user_level"].ToString() == "39" || Session["user_level"].ToString() == "30")
        {
            conditions = conditions + " and DistrictCode='" + Session["DistrictCode"].ToString() + "' ";
        }

        if (Session["user_level"].ToString() == "19")
        {
            conditions = conditions + " and BlockCode='" + Session["BlockCode"].ToString() + "' ";
        }
        if (Session["user_level"].ToString() == "24" || Session["user_level"].ToString() == "30" || Session["user_level"].ToString() == "61" || Session["user_level"].ToString() == "59")
        {
            conditions = conditions + " and UserName='assa' ";
        }

        objComman.BindDLL("MstUser", "UserName as UserId,[FristName]+' ('+ UserName +')' as [UserName] ", conditions, "", "", ddlUser, "UserName", "UserId", "Select");


        
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
    }
    protected void Gv_Profile_Search_OnRowCommand(object sender, GridViewRowEventArgs e)
    {
        //if (e.Row.RowType == DataControlRowType.Header)
        //{
        //               GridViewRow HeaderRow1 = new GridViewRow(1, 0, DataControlRowType.Header, DataControlRowState.Insert);
        //                 //HyperLinkField  HeaderCell = new HyperLinkField ();
        //                  TableCell HeaderCell = new TableCell();
        //                   DataTable dt = Session["dt"] as DataTable;
        //                    int icount=1;
        //                  foreach (DataColumn column in dt.Columns)

        //                      {
           

        //                        HeaderCell = new TableCell();
        //                     HeaderCell.Text = column.ColumnName;
                          
                           
        //                    HeaderRow1.Cells.Add(HeaderCell);
                         
        //             }

        //}
    }
    protected void btnApprove_Click(object sender, EventArgs e)
    {
        if (ddlUser.SelectedIndex <= 0)
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

        string condation = "";
        if (Session["user_level"].ToString() == "19")
        {
            condation = "ActivityDate between('" + afromDate + "') and '" + aToDate + "' and UserID='" + ddlUser.SelectedValue + "' and  UserEntry ='2'  ";
        }
        if (Session["user_level"].ToString() == "39")
        {
            condation = "ActivityDate between('" + afromDate + "') and '" + aToDate + "' and UserID='" + ddlUser.SelectedValue + "' and  UserEntry ='3' ";
        }

        DataTable dtApprove = objMain.LoadVillageActivtiyApprove(condation,1);

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

                    
                }
            }
            string Newcondation = "ActivityDate between('" + afromDate + "') and '" + aToDate + "' and UserID='" + ddlUser.SelectedValue + "'";
            MainResult = objMain.ActivityVillageStatusUpdate(Statas, Newcondation);
            if (MainResult > 0)
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
        Session["dt"] = null;

        if (ddlUser.SelectedIndex <= 0)
        {
          
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select User')</script>", false);
            return;
        }
      
      
        string fromDate = TxtFromDate.Text;
        string[] d = fromDate.Split('/');
        string afromDate = d[2] + '-' +d[1] + '-' + d[0];

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
        string con = "ActivityDate between('" + afromDate + "') and '" + aToDate + "' and UserID='" + ddlUser.SelectedValue + "' ";
        DataTable dtMain = objMain.LoadVillageActivtiy(afromDate, aToDate, ddlUser.SelectedValue, con);


      
        string condation = "";
        if (Session["user_level"].ToString() == "19")
        {
            condation = "ActivityDate between('" + afromDate + "') and '" + aToDate + "' and UserID='" + ddlUser.SelectedValue + "' and  UserEntry ='2'  ";
        }
        if (Session["user_level"].ToString() == "39")
        {
            condation = "ActivityDate between('" + afromDate + "') and '" + aToDate + "' and UserID='" + ddlUser.SelectedValue + "' and  UserEntry ='3' ";
        }

        DataTable dtApprove = objMain.LoadVillageActivtiyApprove(condation,1);

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
            Gv_Profile_Search.DataSource = dt;
            Gv_Profile_Search.DataBind();
            Gv_Profile_Search.HeaderRow.Cells[count].Text = "T.B.Hand Holding";
            DataRow[] drApp = null;
            for (int Index = 1; Index < Gv_Profile_Search.HeaderRow.Cells.Count; Index++)
            {
                #region ApproveBy
                var firstCell = Gv_Profile_Search.HeaderRow.Cells[Index];
                if (Index == 1)
                {
                    drApp = dtApprove.Select("ActivityDate='" + firstCell.Text + "'");
                    if (drApp.Length > 0)
                    {
                        if (drApp[0]["ApproveStatus"].ToString() == "B")
                        {
                            Gv_Profile_Search.HeaderRow.Cells[Index].BackColor = Color.Orange;
                        }
                        if (drApp[0]["ApproveStatus"].ToString() == "I")
                        {
                            Gv_Profile_Search.HeaderRow.Cells[Index].BackColor = Color.Green;
                        }
                    }
                }
                else if (Index == 2)
                {
                    drApp = dtApprove.Select("ActivityDate='" + firstCell.Text + "'");
                    if (drApp.Length > 0)
                    {
                        if (drApp[0]["ApproveStatus"].ToString() == "B")
                        {
                            Gv_Profile_Search.HeaderRow.Cells[Index].BackColor = Color.Orange;
                        }
                        if (drApp[0]["ApproveStatus"].ToString() == "I")
                        {
                            Gv_Profile_Search.HeaderRow.Cells[Index].BackColor = Color.Green;
                        }
                    }
                }
                else if (Index ==3)
                {
                    drApp = dtApprove.Select("ActivityDate='" + firstCell.Text + "'");
                    if (drApp.Length > 0)
                    {
                        if (drApp[0]["ApproveStatus"].ToString() == "B")
                        {
                            Gv_Profile_Search.HeaderRow.Cells[Index].BackColor = Color.Orange;
                        }
                        if (drApp[0]["ApproveStatus"].ToString() == "I")
                        {
                            Gv_Profile_Search.HeaderRow.Cells[Index].BackColor = Color.Green;
                        }
                    }
                }
                else if (Index ==4)
                {
                    drApp = dtApprove.Select("ActivityDate='" + firstCell.Text + "'");
                    if (drApp.Length > 0)
                    {
                        if (drApp[0]["ApproveStatus"].ToString() == "B")
                        {
                            Gv_Profile_Search.HeaderRow.Cells[Index].BackColor = Color.Orange;
                        }
                        if (drApp[0]["ApproveStatus"].ToString() == "I")
                        {
                            Gv_Profile_Search.HeaderRow.Cells[Index].BackColor = Color.Green;
                        }
                    }
                }
                else if (Index == 5)
                {
                    drApp = dtApprove.Select("ActivityDate='" + firstCell.Text + "'");
                    if (drApp.Length > 0)
                    {
                        if (drApp[0]["ApproveStatus"].ToString() == "B")
                        {
                            Gv_Profile_Search.HeaderRow.Cells[Index].BackColor = Color.Orange;
                        }
                        if (drApp[0]["ApproveStatus"].ToString() == "I")
                        {
                            Gv_Profile_Search.HeaderRow.Cells[Index].BackColor = Color.Green;
                        }
                    }
                }
                else if (Index == 6)
                {
                    drApp = dtApprove.Select("ActivityDate='" + firstCell.Text + "'");
                    if (drApp.Length > 0)
                    {
                        if (drApp[0]["ApproveStatus"].ToString() == "B")
                        {
                            Gv_Profile_Search.HeaderRow.Cells[Index].BackColor = Color.Orange;
                        }
                        if (drApp[0]["ApproveStatus"].ToString() == "I")
                        {
                            Gv_Profile_Search.HeaderRow.Cells[Index].BackColor = Color.Green;
                        }
                    }
                }
                else if (Index == 7)
                {
                    drApp = dtApprove.Select("ActivityDate='" + firstCell.Text + "'");
                    if (drApp.Length > 0)
                    {
                        if (drApp[0]["ApproveStatus"].ToString() == "B")
                        {
                            Gv_Profile_Search.HeaderRow.Cells[Index].BackColor = Color.Orange;
                        }
                        if (drApp[0]["ApproveStatus"].ToString() == "I")
                        {
                            Gv_Profile_Search.HeaderRow.Cells[Index].BackColor = Color.Green;
                        }
                    }
                }
                #endregion
                firstCell.Controls.Clear();
               
                firstCell.Controls.Add(new HyperLink { NavigateUrl = "./frmMobileVillageProfile.aspx?ID=" + firstCell.Text + "," + ddlUser.SelectedValue + "", Text = firstCell.Text });

            }
        }
        //foreach (DataColumn column in dtMain.Columns)
        //{
        //    Gv_Profile_Search.HeaderRow.Cells[count].Text = column.ColumnName;
        //    Gv_Profile_Search.HeaderRow.Cells[count].ResolveClientUrl("./frmMobileVillageProfile.aspx?ID=0");
        //    count = count + 1;
           
        //}
      //  Session["dt"]=dt;
        if (dtMain.Rows.Count == 0)
        {
            string strQry = "";
            strQry = "select '' as Flag,day(dateadd(d,number-1,'" + afromDate + "')) as TbDay,CONVERT(varchar,dateadd(d,number-1,'" + afromDate + "'),103) as VDate   from Numbers   ";
            strQry += "WHERE Number<=DATEDIFF(day,'" + afromDate + "',CONVERT(datetime,'" + aToDate + "')+1)";
            DataTable dtVillage = objMain.LoadData(strQry);
            DataTable dt = new DataTable();
            Int32 icount = 0;
            foreach (DataRow dr in dtVillage.Rows)
            {


                string Dateof = dr["VDate"].ToString();
                //string[] b = Dateof.Split('/');

                //string FcDate = b[2] + '-' + b[1] + '-' + b[0];

                DataColumn dcol = new DataColumn();
                if (icount == 0)
                {


                    dcol = new DataColumn("Village", typeof(System.String));
                    dt.Columns.Add(dcol);


                }
                icount = icount + 1;
                dcol = new DataColumn(Dateof, typeof(System.String));

                dt.Columns.Add(dcol);
              


            }
            //foreach (DataColumn column in dt.Columns)

            // {
            //     Console.Write(column.ColumnName);

            // }
            Int32 sr = 1;
            foreach (DataRow dr in dtVillage.Rows)
            {


                string FcDate = dr["VDate"].ToString();
                //string[] b = Dateof.Split('/');

                //string FcDate = b[2] + '-' + b[1] + '-' + b[0];

                if (sr == 1)
                {
                    DataRow Item;
                    Item = dt.NewRow();
                    dt.Rows.Add(Item);
                    //strQry = "   select 'Village Count' as Village, count (distinct VillageCode) as VillageCode  from tblActivityUpdate_Village   where UserID='" + ddlUser.SelectedValue + "'  and ActivityDate= '" + Convert.ToDateTime(FcDate).ToString("yyyy-MM-dd") + "' ";
                    //DataTable dtVillageActivtiy = objMain.LoadData(strQry);
                    //if (dtVillageActivtiy.Rows.Count > 0)
                    //{
                    //    dt.Rows[0]["FcDate"] = dtVillageActivtiy.Rows[0]["VillageCode"];
                    //}
                    //else
                    //{
                    //    dt.Rows[0]["FcDate"] = dtVillageActivtiy.Rows[0]["VillageCode"];

                    //}


                    Item["Village"] = "Village Count";
                    Item[FcDate] = "";

                    DataRow Item1;
                    Item1 = dt.NewRow();
                    dt.Rows.Add(Item1);

                    //strQry = "   select 'GGS' as GGS, count(GSS_Mtg) as GSS_Mtg  from tblActivityUpdate_Village   where LEN(GSS_Mtg)>0 and UserID='" + ddlUser.SelectedValue + "'  and ActivityDate= '" + Convert.ToDateTime(FcDate).ToString("yyyy-MM-dd") + "' ";
                    //DataTable dtGGS = objMain.LoadData(strQry);

                    Item1["Village"] = "GSS";
                    Item1[FcDate] = "";

                    DataRow Item2;
                    Item2 = dt.NewRow();
                    dt.Rows.Add(Item2);

                    Item2["Village"] = "Mauhalla Meeting";
                    Item2[FcDate] = "";

                    DataRow Item3;
                    Item3 = dt.NewRow();
                    dt.Rows.Add(Item3);

                    Item3["Village"] = "Other Community Meeting";
                    Item3[FcDate] = "";

                    DataRow Item4;
                    Item4 = dt.NewRow();
                    dt.Rows.Add(Item4);

                    Item4["Village"] = "Community Contact";
                    Item4[FcDate] = "";

                    DataRow Item5;
                    Item5 = dt.NewRow();
                    dt.Rows.Add(Item5);

                    Item5["Village"] = "Support";
                    Item5[FcDate] = "";

                    sr = sr + 1;
                }



            }
            DataView dataview = dt.DefaultView;
            dataview.Sort = "Village";
            DataTable dt1 = dataview.ToTable();
           
       
            Gv_Profile_Search.DataSource = dt1;
            Gv_Profile_Search.DataBind();
            Gv_Profile_Search.HeaderRow.Cells[0].Text = "T.B.Hand Holding";
            for (int Index = 1; Index < Gv_Profile_Search.HeaderRow.Cells.Count; Index++)
            {
                
                var firstCell = Gv_Profile_Search.HeaderRow.Cells[Index];
                firstCell.Controls.Clear();
                firstCell.Controls.Add(new HyperLink { NavigateUrl = "./frmMobileVillageProfile.aspx?ID=" + firstCell.Text + "," + ddlUser.SelectedValue + "", Text = firstCell.Text });

            }
        }

  
       
    }
    protected void ddlUser_SelectedIndexChanged(object sender, EventArgs e)
    {
        string strQry = "";
        if (ddlUser.SelectedIndex > 0)
        {
            strQry = "   select Villagecode  from MstUser   where UserName='" + ddlUser.SelectedValue + "' ";
            DataTable dtUserVillage = objMain.LoadData(strQry);

            string strVillage = dtUserVillage.Rows[0]["Villagecode"].ToString();

            conditions = "mst5Village.VillageCode in(" + strVillage + ") ";

         //   objComman.BindDLL("mst5Village", "VillageCode,VillageName ", conditions, "", "", ddlVilage, "VillageName", "VillageCode", "Select");


        }
    }
  

    protected void TestGridView_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            //for (int Index = 1; Index < Gv_Profile_Search.HeaderRow.Cells.Count; Index++)
            //{
            //    var firstCell = e.Row.Cells[Index];
            //    firstCell.Controls.Clear();
            //    firstCell.Controls.Add(new HyperLink { NavigateUrl = firstCell.Text, Text = firstCell.Text });
            //}
            //HyperLink newHyperLink = new HyperLink();
          
            //newHyperLink.NavigateUrl = "login.aspx";
            //newHyperLink.Text = "aa";
            //e.Row.Cells[1].Controls.Add(newHyperLink);
        }
    }
    //protected void Gv_Profile_Search_OnRowCommand(object sender, GridViewCommandEventArgs e)
    //{
    //    if (e.CommandName == "GVUIO")
    //    {
    //        int iIndex = Convert.ToInt32(e.CommandArgument);
    //        string VDate = Gv_Profile_Search.DataKeys[iIndex]["VDate"].ToString();
    //        Response.Redirect("./frmMobileVillageProfile.aspx?ID=" + ddlVilage.SelectedValue + "," + ddlUser.SelectedValue + "," + VDate + "");
    //    }
 
}