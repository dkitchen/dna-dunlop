using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using DunlopBarcode.Model;
using System.Text;
using System.Web.Configuration;

namespace DunlopBarcode.Web
{


    public partial class FinishingPivot : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ExcelButton.Visible = false;
            if (!IsPostBack)
            {
                DateTextBox.Text = DateTime.Now.ToShortDateString();

                //default
                var defaultItem = new ListItem("All", "0");

                //list of machines
                var db = new DunlopBarcodeDataContext(((Site)Master).GetConnectionStringSettings());
                //Only show finishing machines
                var sql = "select machine_id value, name text from machine_count_view where department_id = 3";
                var machines = db.GetDataTable(sql);
                MachineDropDownList.DataSource = machines;
                MachineDropDownList.DataTextField = "text";
                MachineDropDownList.DataValueField = "value";
                MachineDropDownList.DataBind();
                MachineDropDownList.Items.Insert(0, defaultItem);

                //green tire numbers
                GreenTireNumberDropDownList.DataSource = db.GetGreenTireDropDownData();
                GreenTireNumberDropDownList.DataTextField = "green_tire_number";
                GreenTireNumberDropDownList.DataValueField = "green_tire_number";
                GreenTireNumberDropDownList.DataBind();
                GreenTireNumberDropDownList.Items.Insert(0, defaultItem);

            }
        }

        protected void SearchButton_Click(object sender, EventArgs e)
        {
            StringBuilder sql = new StringBuilder();
            //sql.Append("select machine_name, green_tire_number, created, part_serial_number, tire_grade, upper_mag, static_angle, bottom_peak, top_peak, radial_1st_harm, radial_peak, static_mag, lower_mag, couple_mag");
            sql.Append("select * ");
            sql.Append(" FROM finishing_pivot_view");


            DateTime date = DateTime.Parse(DateTextBox.Text);

            sql.AppendFormat(" WHERE created BETWEEN to_date('{0}','YYYY-MM-DD') AND to_date('{1}','YYYY-MM-DD')",
                date.ToString("yyyy-MM-dd"),
                date.AddDays(1).ToString("yyyy-MM-dd"));

            if (MachineDropDownList.SelectedValue != "0")
            {
                sql.AppendFormat(" AND machine_id = '{0}'", MachineDropDownList.SelectedValue.SqlEscape());
            }
            if (GreenTireNumberDropDownList.SelectedValue != "0")
            {
                sql.AppendFormat(" AND green_tire_number = '{0}'",
                    GreenTireNumberDropDownList.SelectedItem.Text);
            }
            //sql.Append(" ORDER BY created");
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