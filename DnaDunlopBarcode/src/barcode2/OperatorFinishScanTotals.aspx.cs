using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using DunlopBarcode.Model;
using System.Web.Configuration;

namespace DunlopBarcode.Web
{


    public partial class OperatorFinishScanTotals : System.Web.UI.Page
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

                ////default to today
                StartDateTextBox.Text = DateTime.Now.AddDays(-30).ToShortDateString();
                EndDateTextBox.Text = DateTime.Now.AddDays(-15).ToShortDateString();
            }
        }

        protected void SearchButton_Click(object sender, EventArgs e)
        {
            StringBuilder sql = new StringBuilder();
            //sql.Append("select operator_id, operator_serial_number, employee_number, operator_name, work_date, shift, Total");
            //sql.Append("select employee_number, operator_name, Total");
            //sql.Append(" FROM operator_finish_scan_totals");
            sql.Append("select");
            sql.Append("    o.name operator_name, o.employee_number,");
            sql.Append("    count(build_part_id) build_total,");
            sql.Append("    count(finish_part_id) finish_total,");
            sql.Append("    round(((count(finish_part_id) / count(build_part_id)) * 100), 2) pct");
            sql.Append(" from");
            sql.Append("    operator_parts_build_finish t");
            sql.Append(" join");
            sql.Append("    part p on t.build_part_id = p.part_id");
            sql.Append(" join");
            sql.Append("    operator o on t.operator_id = o.operator_id");
            sql.Append(" where");

            DateTime startDate = Convert.ToDateTime(StartDateTextBox.Text);
            DateTime endDate = Convert.ToDateTime(EndDateTextBox.Text);
            sql.AppendFormat("    p.created BETWEEN to_date('{0}','YYYY-MM-DD') AND to_date('{1}','YYYY-MM-DD')",
                startDate.ToString("yyyy-MM-dd"),
                endDate.ToString("yyyy-MM-dd"));

            if (OperatorDropDownList.SelectedValue != "All")
            {
                //sql.AppendFormat(" AND operator_id = {0}", OperatorDropDownList.SelectedValue.SqlEscape());
                sql.AppendFormat(" and o.employee_number = {0}", OperatorDropDownList.SelectedValue.SqlEscape());
            }
            sql.Append(" group by");
            sql.Append("    o.name, o.employee_number");

            //Trace.IsEnabled = true;
            //Trace.Write("sql", sql.ToString());
            var db = new DunlopBarcodeDataContext(((Site)Master).GetConnectionStringSettings());
            var results = db.GetDataTable(sql.ToString());
            Trace.Write("result count", results.Rows.Count.ToString());
            ListView1.DataSource = results;
            ListView1.DataBind();
            if (ListView1.Items.Count > 0)
            {
                ExcelButton.Visible = true;
            }
        }
    }
}