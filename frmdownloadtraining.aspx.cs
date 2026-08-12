using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;
using System.Drawing;
using System.IO;
using System.Data;
using System.Data.SqlClient;
public partial class frmdownloadtraining : System.Web.UI.Page
{
    clsMain objMain = new clsMain();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            if (Convert.ToString(Session["username"]) != "")
            {
                if (!string.IsNullOrEmpty(Request.QueryString["ID"]))
                {
                    string fileName = Path.GetFileName(Request.QueryString["ID"]); // Prevent path traversal
                    string filePath = Server.MapPath("~/Traning/" + fileName);

                    if (File.Exists(filePath))
                    {
                        Response.Clear();
                        Response.ContentType = "application/octet-stream";
                        Response.AppendHeader("Content-Disposition",
                            "attachment; filename=" + fileName);

                        Response.TransmitFile(filePath);
                        Response.End();
                    }
                    else
                    {
                        Response.Write("File not found.");
                    }
                }
            }




        }
    }
    public void LoadData()
    {
        string filename = "";
        string IDImage = "";
        string strQry2 = "";
        strQry2 += " select PicName from tblRandomSessionPhototemp where SchoolCode='930F1813F0F44E799D2AE08E9'  ";
        
        DataTable dtUseryyy = objMain.LoadData(strQry2);
        var kk = 0;
        for (kk = 0; kk < dtUseryyy.Rows.Count; kk++)
        {
            IDImage = dtUseryyy.Rows[kk]["PicName"].ToString();
            string sFileDir = Server.MapPath("~/TabletImage/");
            filename = sFileDir + "TabletImage\\" + IDImage;
            filename = sFileDir + IDImage;



            if (!File.Exists(filename))
            {

            }
            else
            {
                Response.ContentType = ".jpg";
                Response.AddHeader("Content-Disposition", "attachment; filename=" + IDImage + "");

                Response.TransmitFile(filename);
                Response.End();
            }
        }
    }
}