using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

public partial class Frm_Change_Password : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["username"] != null)
            {

                Txtuser.Text = Convert.ToString(Session["username"]);
                Txtpassword.Text = "";

            }
        }
    }

    // Session["username"]
        protected void btn_Save_Click(object sender, EventArgs e)
    {
        if (Session["username"] != null)
        {
            Change_Pwd("Sp_Change_User_Password");
        }
        else
        {
            Change_Pwd("Sp_Change_User_Password");
        }
    }

    public void Change_Pwd(string porcName)
    {
        try
        {
            if (Session["username"] != null)
            {
                if (Txtpasswordnew.Text.Trim() != "" && Txtpassword.Text.Trim() != "")
                {
                    string OldPassWord=Password.CreatePasswordHash(Txtpassword.Text.Trim()).ToString();
                    string NewPassWord = Password.CreatePasswordHash(Txtpasswordnew.Text.Trim()).ToString();
                    SqlParameter[] pr = new SqlParameter[] { 
                       new SqlParameter("@UserID",Session["username"].ToString()),
                       new SqlParameter("@oldpwd",OldPassWord),
                       new SqlParameter("@NewPwd",NewPassWord),
            };
                    int result = SqlHelper.ExecuteNonQuery(SqlHelper.mainConnectionString, CommandType.StoredProcedure, porcName, pr);
                    if (result > 0)
                    {
                        Clear_All();
                        ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Password changed successfully')</script>", false);

                       
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Password not changed')</script>", false);

                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Enter Password')</script>", false);

                }
            }
            else
            {
                
            }
        }
        catch (Exception ex)
        {
        }

    }

    public void Clear_All()
    {
        Txtpassword.Text = "";
        TxtPasswordconfirm.Text = "";
        Txtpasswordnew.Text = "";

    }

}