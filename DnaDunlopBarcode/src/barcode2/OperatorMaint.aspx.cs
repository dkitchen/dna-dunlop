using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DunlopBarcode.Model;
using System.Data.Common;

namespace DunlopBarcode.Web
{
    public partial class OperatorMaint : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindList();
            }
        }


        private void BindList()
        {
            var setting = ((Site)Master).GetConnectionStringSettings();
            setting.Name.Trace("ConnectionStringSettings.Name");
            var db = new DunlopBarcodeDataContext(setting);
            //employee number should never change, but when badges are lost, the serial_number changes
            ListView1.DataSource = db.GetDataTable("Select operator_id, serial_number, name, employee_number from operator order by name, employee_number");
            ListView1.DataBind();
        }

        protected void ListView1_ItemEditing(object sender, ListViewEditEventArgs e)
        {
            ListView1.EditIndex = e.NewEditIndex;
            BindList();
        }

        protected void ListView1_ItemCanceling(object sender, ListViewCancelEventArgs e)
        {
            ListView1.EditIndex = -1;
            BindList();
        }

        protected void ListView1_ItemUpdating(object sender, ListViewUpdateEventArgs e)
        {
            var serialNumTextBox = ListView1.Items[e.ItemIndex].FindControl("SerialNumberTextBox") as TextBox;

            (serialNumTextBox == null).Trace("Serial Number Text Box is null?");
            if (serialNumTextBox != null)
            {
                string serialNum = serialNumTextBox.Text;
                serialNum.Trace("SerialNum");
                string opId = ListView1.DataKeys[e.ItemIndex].Value.ToString();
                opId.Trace("Operator ID?");
                var db = new DunlopBarcodeDataContext(((Site)Master).GetConnectionStringSettings());
                string sql = string.Format("UPDATE OPERATOR SET SERIAL_NUMBER = '{0}' WHERE OPERATOR_ID = {1}",
                   serialNum.SqlEscape(),
                   opId.SqlEscape());
                sql.Trace("sql");

                try
                {
                    db.ExecuteNonQuery(sql);
                    ListView1.EditIndex = -1;
                    BindList();
                    MessagePanel.CssClass = "message success";
                    MessageLiteral.Text = "Operator Record Updated";
                }
                catch (Exception ex)
                {

                    MessagePanel.CssClass = "message error";
                    MessageLiteral.Text = ex.Message;
                }

            }

        }

    }
}