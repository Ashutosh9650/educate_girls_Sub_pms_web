using System;
using System.Collections.Generic;

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


            if (Request.QueryString["ID"] != null)
            {
                string IDImage = Request.QueryString["ID"];
                IDImage = Path.GetFileName(IDImage ?? "");

                if (string.IsNullOrEmpty(IDImage))
                {
                    Response.Write("No image specified.");
                    Response.End();
                    return;
                }

                string sFileDir = Server.MapPath(Comman.GetImagePath("Traning") + "/");
                string filename = Path.Combine(sFileDir, IDImage);

                if (File.Exists(filename))
                {
                    Response.Clear();
                    Response.ContentType = "image/jpeg";
                    Response.AddHeader("Content-Disposition", "attachment; filename=" + IDImage);
                    Response.TransmitFile(filename);
                    Response.End();
                }
                else
                {
                    Response.Write("Image not found.");
                    Response.End();
                }

            }
   



        }
    }

}