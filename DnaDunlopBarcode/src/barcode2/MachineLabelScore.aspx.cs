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


    public partial class MachineLabelScore : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ExcelButton.Visible = false;
            if (!IsPostBack)
            {
                //provide drop list of machines in finishing
                var db = new DunlopBarcodeDataContext(((Site)Master).GetConnectionStringSettings());
                var sql = new StringBuilder("select machine_id value, name text from machine_count_view");
                sql.Append(" WHERE department_id = 3");

                var machines = db.GetDataTable(sql.ToString());
                MachineDropDownList.DataSource = machines;
                MachineDropDownList.DataTextField = "text";
                MachineDropDownList.DataValueField = "value";
                MachineDropDownList.DataBind();
                MachineDropDownList.Items.Insert(0, new ListItem("All", "0"));

                //default to today
                StartDateTextBox.Text = DateTime.Now.AddDays(-15).ToShortDateString();
                EndDateTextBox.Text = DateTime.Now.ToShortDateString();
            }
        }

        protected void SearchButton_Click(object sender, EventArgs e)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select");
            sql.Append("    m.name machine_name, m.machine_id,");
            sql.Append("    sum(good_reads + bad_reads) reads, sum(good_reads) good_reads,");
            sql.Append("    sum(bad_reads) bad_reads,");
            sql.Append("    round(((sum(good_reads) / (sum(bad_reads) + sum(good_reads))) * 100), 2) pct");
            sql.Append(" from");
            sql.Append("    machine_scan_score t");
            sql.Append(" join");
            sql.Append("    event_log l on l.event_log_id = t.event_log_id");
            sql.Append(" join");
            sql.Append("    machine m on l.machine_id = m.machine_id");
            sql.Append(" where ");
            sql.Append("    m.department_id = 3");




            DateTime startDate = Convert.ToDateTime(StartDateTextBox.Text);
            DateTime endDate = Convert.ToDateTime(EndDateTextBox.Text);
            sql.AppendFormat("    AND l.created BETWEEN to_date('{0}','YYYY-MM-DD') AND to_date('{1}','YYYY-MM-DD')",
                startDate.ToString("yyyy-MM-dd"),
                endDate.ToString("yyyy-MM-dd"));

            if (MachineDropDownList.SelectedValue != "0")
            {
                //sql.AppendFormat(" AND operator_id = {0}", OperatorDropDownList.SelectedValue.SqlEscape());
                sql.AppendFormat(" and m.machine_id = {0}", MachineDropDownList.SelectedValue.SqlEscape());
            }
            sql.Append(" group by");
            sql.Append("    m.name, m.machine_id");
            sql.Append(" order by");
            sql.Append("    m.name");

            //Trace.IsEnabled = true;
            //Trace.Write("sql", sql.ToString());
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