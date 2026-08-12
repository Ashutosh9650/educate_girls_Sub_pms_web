using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class frmdownloadGkp : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["ID"] != null)
            {
                string IDImage = Request.QueryString["ID"];
                string filename = "";
                string sFileDir = Server.MapPath("~/GKP/");
                filename = sFileDir + "GKP\\" + IDImage;
                filename = sFileDir + IDImage;


                Response.ContentType = ".jpg";
                Response.AddHeader("Content-Disposition", "attachment; filename=" + IDImage + "");

                Response.TransmitFile(filename);
                Response.End();

            }


        }
    }
}