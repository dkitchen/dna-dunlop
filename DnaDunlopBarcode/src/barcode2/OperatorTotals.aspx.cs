using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using DunlopBarcode.Model;
using DunlopBarcode.Model.Properties;
using System.Web.Configuration;

namespace DunlopBarcode.Web
{


    public partial class OperatorTotals : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ExcelButton.Visible = false;
            if (!IsPostBack)
            {
                //provide drop list of operators
                var db = new DunlopBarcodeDataContext(((Site)Master).GetConnectionStringSettings());
                OperatorDropDownList.DataSource = db.GetOperatorDropDownData();
                OperatorDropDownList.DataTextField = "text";
                OperatorDropDownList.DataValueField = "value";
                OperatorDropDownList.DataBind();
                OperatorDropDownList.Items.Insert(0, "All");

                //default to today
                StartDateTextBox.Text = DateTime.Now.ToShortDateString();
                EndDateTextBox.Text = DateTime.Now.ToShortDateString();
            }
        }

        protected void SearchButton_Click(object sender, EventArgs e)
        {
            StringBuilder sql = new StringBuilder();
            //sql.Append("select operator_id, operator_serial_number, employee_number, operator_name, work_date, shift, Total");
            sql.Append("select employee_number, operator_name, work_date, shift, Total");
            sql.Append(" FROM operator_good_tire_totals_view");

            sql.AppendFormat(" WHERE work_date BETWEEN to_date('{0}','{2}') AND to_date('{1}','{2}')",
                DateTime.Parse(StartDateTextBox.Text).ToString(Settings.Default.DateFormat),
                DateTime.Parse(EndDateTextBox.Text).ToString(Settings.Default.DateFormat),
                Settings.Default.OracleDateFormat);
            if (OperatorDropDownList.SelectedValue != "All")
            {
                //sql.AppendFormat(" AND operator_id = {0}", OperatorDropDownList.SelectedValue.SqlEscape());
                sql.AppendFormat(" AND employee_number = {0}", OperatorDropDownList.SelectedValue.SqlEscape());
            }

            var db = new DunlopBarcodeDataContext(((Site)Master).GetConnectionStringSettings());
            var results = db.GetDataTable(sql.ToString());
            ListView1.DataSource = results;
            ListView1.DataBind();
            if (ListView1.Items.Count > 0)
            {
                ExcelButton.Visible = true;
            }
        }
    }
}