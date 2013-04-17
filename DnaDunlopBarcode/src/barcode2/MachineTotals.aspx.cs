using System;
using System.Data;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;
using DunlopBarcode.Model;
using DunlopBarcode.Model.Properties;
using System.Web.Configuration;

namespace DunlopBarcode.Web
{



    public partial class MachineTotals : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ExcelButton.Visible = false;
            TotalPanel.Visible = false;

            if (!IsPostBack)
            {
                //default to 'all items"
                var db = new DunlopBarcodeDataContext(((Site)Master).GetConnectionStringSettings());
                //list of departments
                var sql = "select department_id value, name text from department";
                var departments = db.GetDataTable(sql);
                DepartmentDropDownList.DataSource = departments;
                DepartmentDropDownList.DataTextField = "text";
                DepartmentDropDownList.DataValueField = "value";
                DepartmentDropDownList.DataBind();

                //trigger change of department selection event so that machine drop down is 
                //  bound
                DepartmentDropDownList_SelectedIndexChanged(null, null);

                //default to today
                StartDateTextBox.Text = DateTime.Now.ToShortDateString();
                EndDateTextBox.Text = DateTime.Now.ToShortDateString();
            }
        }

        protected void SearchButton_Click(object sender, EventArgs e)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select machine_id, machine_name, work_date, shift, total");


            if (DepartmentDropDownList.SelectedValue == "1")
            {
                //ONLY COUNT 'GoodTIre' events fore building dept.
                sql.Append(" FROM machine_good_tire_totals_view");
            }
            else
            {
                sql.Append(" FROM machine_totals_view");
            }

            sql.AppendFormat(" WHERE work_date BETWEEN to_date('{0}','{2}') AND to_date('{1}','{2}')",
                DateTime.Parse(StartDateTextBox.Text).ToString(Settings.Default.DateFormat),
                DateTime.Parse(EndDateTextBox.Text).ToString(Settings.Default.DateFormat),
                Settings.Default.OracleDateFormat);

            sql.AppendFormat(" AND department_id = {0}", DepartmentDropDownList.SelectedValue.SqlEscape());

            if (MachineDropDownList.SelectedValue != "0")
            {
                sql.AppendFormat(" AND machine_id = '{0}'", MachineDropDownList.SelectedValue.SqlEscape());
            }

            var db = new DunlopBarcodeDataContext(((Site)Master).GetConnectionStringSettings());
            var results = db.GetDataTable(sql.ToString());

            var rows = results.Rows.Cast<DataRow>();

            var dailyTotals = from r in rows
                              where r["shift"].ToString() == ""
                              select r;

            var shiftATotal = (from r in rows
                               where r["shift"].ToString() == "A"
                               select Convert.ToInt32(r["total"])).Sum();
            var shiftBTotal = (from r in rows
                               where r["shift"].ToString() == "B"
                               select Convert.ToInt32(r["total"])).Sum();
            var shiftCTotal = (from r in rows
                               where r["shift"].ToString() == "C"
                               select Convert.ToInt32(r["total"])).Sum();

            AShiftTotalLabel.Text = shiftATotal.ToString();
            BShiftLabel.Text = shiftBTotal.ToString();
            CShiftLabel.Text = shiftCTotal.ToString();

            var dailyTotal = dailyTotals.Sum(r => Convert.ToInt32(r["Total"]));
            DailyTotalLabel.Text = dailyTotal.ToString();

            ListView1.DataSource = results;
            ListView1.DataBind();
            ListView1.Visible = true;
            if (ListView1.Items.Count > 0)
            {
                ExcelButton.Visible = true;
                TotalPanel.Visible = true;
            }
        }

        protected void DepartmentDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            //refresh machine drop down to only those in this department
            var db = new DunlopBarcodeDataContext(((Site)Master).GetConnectionStringSettings());
            var sql = new StringBuilder("select machine_id value, name text from machine_count_view");

            if (DepartmentDropDownList.SelectedValue != "0")
            {
                sql.AppendFormat(" WHERE department_id = {0}", DepartmentDropDownList.SelectedValue.SqlEscape());
            }
            var machines = db.GetDataTable(sql.ToString());
            MachineDropDownList.DataSource = machines;
            MachineDropDownList.DataTextField = "text";
            MachineDropDownList.DataValueField = "value";
            MachineDropDownList.DataBind();
            MachineDropDownList.Items.Insert(0, new ListItem("All", "0"));

            ListView1.Visible = false;
        }
    }
}