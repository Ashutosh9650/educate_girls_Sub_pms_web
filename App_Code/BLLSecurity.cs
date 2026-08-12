using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Text.RegularExpressions;
using System.Text;
using System.Security.Cryptography;

/// <summary>
/// Summary description for BLLSecurity
/// </summary>
//namespace BLLSecurity
//{

    public class SqlInjection : IHttpModule//CommonBLL,
    {        
        
        //Defines the set of characters that will be checked. 
        //You can add to this list, or remove items from this list, as appropriate for your site. 
 
        public static string[] blackList = {";--","<script>",
                                               "/*","*/","@@",
                                           " alter ","alter ",
                                           " begin ","begin ",
                                           " create table ","create table ",
                                           " cursor ","cursor ",
                                           " declare ","declare ",
                                           " delete ","delete ",
                                           " delete table ","delete table ",
                                           " drop ","drop ",
                                           " drop table ","drop table ",
                                           "<script>",
                                           "http://lilupophilupop.com",
                                           " exec ","exec ",
                                           " execute ","execute ",
                                           " fetch ","fetch ",
                                           " insert into ","insert into ",
                                           " kill ","kill ",
                                           " sys.","sys. ",
                                           "sysobjects",
                                           " table "," table",
                                           " update ","update ",
                                           "&lt;","&gt;","&#40;","&#41;","&#35;","&amp;","&quot;","--","=","'"};//,"table "

    public static string[] blackListNew = {";--","<script>",
                                               "/*","*/","@@",
                                           " alter ","alter ",
                                           " begin ","begin ",
                                           " create table ","create table ",
                                           " cursor ","cursor ",
                                           " declare ","declare ",
                                           " delete ","delete ",
                                           " delete table ","delete table ",
                                           " drop ","drop ",
                                           " drop table ","drop table ",
                                           "<script>",
                                           "http://lilupophilupop.com",
                                           " exec ","exec ",
                                           " execute ","execute ",
                                           " fetch ","fetch ",
                                           " insert into ","insert into ",
                                           " kill ","kill ",
                                           " sys.","sys. ",
                                           "sysobjects",
                                           " table "," table",
                                           " update ","update ",
                                           "&lt;","&gt;","&#40;","&#41;","&#35;","&amp;","&quot;","--","="};//,"table "

    public void Dispose()
        {
            //no-op  
        }

        //Tells ASP.NET that there is code to run during BeginRequest 
        public void Init(HttpApplication app)
        {
            app.BeginRequest += new EventHandler(app_BeginRequest);
            app.PreRequestHandlerExecute += new EventHandler(app_PreRequestHandlerExecute);
        }
    public bool CheckInputBoolNew(string parameter)
    {
        bool chkStatus = false;

        for (int i = 0; i < blackListNew.Length; i++)
        {
            if (parameter.IndexOf(blackListNew[i], StringComparison.OrdinalIgnoreCase) >= 0)
            {
                chkStatus = true;
            }
        }

        //if (chkStatus)
        //{
        //    HttpContext.Current.Session["SQlInjQuery"] = parameter;
        //    Page page = HttpContext.Current.Handler as Page;
        //    ScriptManager.RegisterStartupScript(page, page.GetType(), "err_msg", "alert('Spurious input detected, action rejected.');", true);
        //}
        return chkStatus;
    }

    void app_PreRequestHandlerExecute(object sender, EventArgs e)
        {
            HttpApplication context = (sender as HttpApplication);
            context.Response.ApplyAppPathModifier("www.mycii.in");
            //throw new NotImplementedException();
        }

        //For each incoming request, check the query-string, form and cookie values for suspicious values. 
        void app_BeginRequest(object sender, EventArgs e)
        {   
            HttpRequest Request = (sender as HttpApplication).Context.Request;

            HttpApplication context = (sender as HttpApplication);

            foreach (string key in Request.Cookies)
                CheckInput(Request.Cookies[key].Value);
            foreach (string key in Request.QueryString)
                CheckInput(Request.QueryString[key]);

            //context.Response.ApplyAppPathModifier("www.mycii.in");
            ////foreach (string key in Request.Form)
            ////    CheckInput(Request.Form[key]);   
        }

        //The utility method that performs the blacklist comparisons 
        //You can change the error handling, and error redirect location to whatever makes sense for your site. 

        public void CheckInput(string parameter)
        {
            //string strChceklist = "--|;--|;|@@|@|char|nchar|varchar|nvarchar|alter|begin|cast|create|cursor|declare|delete|drop|end|exec|execute|fetch|insert|kill|open|sys|sysobjects|syscolumns|table|update";
            //Regex rr = new Regex(strChceklist, RegexOptions.IgnoreCase);

            //if (Regex.IsMatch(parameter, strChceklist, RegexOptions.IgnoreCase))
            //{
                //HttpContext.Current.Response.Write(rr.Replace(parameter, "'--"));
            //}

            bool chkCond = false;

            for (int i = 0; i < blackList.Length; i++)
            {
                if ((parameter.IndexOf(blackList[i], StringComparison.OrdinalIgnoreCase) >= 0))
                {
                    chkCond = true;
                    //parameter = blackList[i].ToString().Trim();
                    //HttpContext.Current.Response.Write(rr.Replace(parameter, "'--"));
                    
                }
            }
            if (chkCond)
            {
                //HttpContext.Current.Session["SQLFlag"] = chkCond;
                //HttpContext.Current.Session["SQlInjQuery1"] = parameter;
                //HttpContext.Current.Session["SQlInjQuery"] = " alter ";
                HttpContext.Current.Response.Redirect("~/ADMIN/Security", false);
            }
        }

        public bool CheckInputBool(string parameter)
        {
            bool chkStatus = false;

            for (int i = 0; i < blackList.Length; i++)
            {
                if (parameter.IndexOf(blackList[i], StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    chkStatus = true;
                }
            }

            //if (chkStatus)
            //{
            //    HttpContext.Current.Session["SQlInjQuery"] = parameter;
            //    Page page = HttpContext.Current.Handler as Page;
            //    ScriptManager.RegisterStartupScript(page, page.GetType(), "err_msg", "alert('Spurious input detected, action rejected.');", true);
            //}
            return chkStatus;
        }

    private readonly byte[] key = Encoding.UTF8.GetBytes("EGKeyAESAdityaEG"); //replace with your own key

    public string Encrypt(string plainText)
    {
        byte[] encryptedBytes;
        using (Aes aesAlg = Aes.Create())
        {
            aesAlg.Key = key;
            aesAlg.Mode = CipherMode.ECB;
            aesAlg.Padding = PaddingMode.PKCS7;

            ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);
            byte[] plainBytes = Encoding.UTF8.GetBytes(plainText);

            encryptedBytes = encryptor.TransformFinalBlock(plainBytes, 0, plainBytes.Length);
        }
        return Convert.ToBase64String(encryptedBytes);
    }

    public string Decrypt(string encryptedText)
    {
        byte[] decryptedBytes;
        using (Aes aesAlg = Aes.Create())
        {
            aesAlg.Key = key;
            aesAlg.Mode = CipherMode.ECB;
            aesAlg.Padding = PaddingMode.PKCS7;

            ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);
            byte[] encryptedBytes = Convert.FromBase64String(encryptedText);

            decryptedBytes = decryptor.TransformFinalBlock(encryptedBytes, 0, encryptedBytes.Length);
        }
        return Encoding.UTF8.GetString(decryptedBytes);
    }


}
//}
