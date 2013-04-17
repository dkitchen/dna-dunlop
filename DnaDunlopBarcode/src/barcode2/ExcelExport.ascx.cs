using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.IO;

namespace DunlopBarcode.Web
{
    /// <summary>
    /// This Control renders the control specified in ListViewSource into an Excel Spreadsheet
    /// </summary>
    public partial class ExcelExport : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public string ListViewSource { get; set; }
                    

        /// <summary>
        /// Send ListView output to client as Excel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ExcelButton_Click(object sender, EventArgs e)
        {

            string filename = string.Format("{1}_{0}",
                DateTime.Now.ToString("yyyyMMddHHmmss"),
                Page.Title.Replace(" ", "_").Replace("|", ""));
            Response.Clear();
            Response.Buffer = true;
            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("content-disposition", "attachment; filename=" + filename + ".xls");
            Response.Charset = "";

            this.EnableViewState = false;
            StringWriter stringWriter = new StringWriter();
            HtmlTextWriter htmlTextWriter = new HtmlTextWriter(stringWriter);
            var listView = this.Parent.FindControl(ListViewSource) as ListView;
            //TODO: if ListViewSource is empty or null, then get first ListView we find on page!!
            listView.RenderControl(htmlTextWriter);
            Response.Write(stringWriter.ToString());
            Response.End();
        }

        

    }
}