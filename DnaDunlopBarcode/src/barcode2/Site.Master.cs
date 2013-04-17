using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using DunlopBarcode.Model;
using System.Web.Configuration;
using System.Configuration;

namespace DunlopBarcode.Web
{

    public partial class Site : System.Web.UI.MasterPage
    {

        private const string DUNLOP_USER_SETTINGS_COOKIE_NAME = "DunlopUserSettings";
        private const string DB_NAME_COOKIE_KEY = "DbName";

        public ConnectionStringSettings GetConnectionStringSettings()
        {
            var settings = WebConfigurationManager.ConnectionStrings
                    .Cast<ConnectionStringSettings>()
                    .Where(i => i.Name.Contains("_"))
                    .OrderBy(i => i.Name);
            var setting =  settings
                    .FirstOrDefault(i => i.Name == ConnectionStringSettingDropDownList.SelectedValue);
            if (null == setting)
            {
                setting = settings.First();
            }
            return setting;
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            Trace.IsEnabled = Request.QueryString["trace"] == "true";

            //Provide a list of Database connection settings to choose from based on the config file
            ConnectionStringSettingDropDownList.DataSource = WebConfigurationManager.ConnectionStrings
                .Cast<ConnectionStringSettings>()
                .Where(i => i.Name.Contains("_"))
                //.OrderBy(i => i.Name) - go ahead with whatever order set in config
                .Select(i => new { Text = i.Name.Substring(0, i.Name.IndexOf("_")), Value = i.Name });
            ConnectionStringSettingDropDownList.DataTextField = "Text";
            ConnectionStringSettingDropDownList.DataValueField = "Value";
            ConnectionStringSettingDropDownList.DataBind();

            //see which DB this user used last time
            HttpCookie cookie = Request.Cookies[DUNLOP_USER_SETTINGS_COOKIE_NAME];
            if (cookie != null)
            {
                string db = cookie[DB_NAME_COOKIE_KEY];
                db.Trace("cookie shows last db");
                if (ConnectionStringSettingDropDownList.Items.FindByValue(db) != null)
                {
                    ConnectionStringSettingDropDownList.SelectedValue = db;
                }
            }
            else { "cookie is null".Trace("Cookie status"); }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

            if (Request.Browser.IsMobileDevice)
            {
                HeaderLiteral.Text = "<link href='css/handheld.css' rel='stylesheet' type='text/css' media='screen, projection' />";
            }
            else
            {
                var headScript = new StringBuilder();

                headScript.Append("<link href='css/screen.css' rel='stylesheet' type='text/css' media='screen, projection' />");
                headScript.Append("<!--[if IE]><link rel='stylesheet' href='css/ie.css' type='text/css' media='screen, projection'><![endif]-->");
                headScript.Append("<script src='js/jquery/jquery-1.3.1.min.js' type='text/javascript'></script><script src='js/jquery/jquery-ui-personalized-1.6rc6.min.js' type='text/javascript'></script>");
                HeaderLiteral.Text = headScript.ToString(); ;
            }
        }

        protected void DBSetButton_Click(object sender, EventArgs e)
        {
            //store which DB this user is using. Next time they connect we'll default to this one.
            Response.Cookies[DUNLOP_USER_SETTINGS_COOKIE_NAME][DB_NAME_COOKIE_KEY] = ConnectionStringSettingDropDownList.SelectedValue;
            Response.Cookies[DUNLOP_USER_SETTINGS_COOKIE_NAME].Expires = DateTime.MaxValue;

            Response.Redirect(Request.Url.PathAndQuery);
           
        }

       
    }
}