using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using DunlopBarcode.Model;
using System.IO;
using DunlopBarcode.Model.Properties;
using System.Web.Configuration;

namespace DunlopBarcode.Web
{
    public partial class EventSearch : System.Web.UI.Page
    {

        public enum SearchType
        {
            FULL,
            PART_SERIAL_ONLY,
            PARTS_NOT_CURED
        }

        protected SearchType CurrentSearchType
        {
            get
            {
                SearchType val = SearchType.FULL;
                //search type param = 't'
                //  part serial number value param = 'p'
                switch (Request["t"])
                {
                    case "p":
                        val = SearchType.PART_SERIAL_ONLY;
                        break;
                    case "woc":
                        val = SearchType.PARTS_NOT_CURED;
                        break;
                    default:
                        val = SearchType.FULL;
                        break;
                }
                return val;
            }
        }

        protected void BindFullSearch()
        {
            FullEventSearch.Visible = true;
            HeadLabel.Text = "Event Search";

            //default selectedd drop dowm list item - set to 'all items"
            var defaultItem = new ListItem("All", "0");

            //db
            var setting = ((Site)Master).GetConnectionStringSettings();
            setting.Name.Trace("ConnectionStringSettings.Name");
            var db = new DunlopBarcodeDataContext(setting);

            //provide drop list of events                
            //string sql = "select event_id value, name || ' (' || event_count || ')' text  from event_count_view";
            //dmk 2010-04-13 optimized
            string sql = "select distinct e.event_id, e.name from event e where exists (select 0 from event_log l where e.event_id = l.event_id)  order by name";
            var events = db.GetDataTable(sql);
            EventDropDownList.DataSource = events;
            EventDropDownList.DataTextField = "name";
            EventDropDownList.DataValueField = "event_id";
            EventDropDownList.DataBind();
            EventDropDownList.Items.Insert(0, defaultItem);

            //list of machines
            //sql = "select machine_id value, name text from machine_count_view";
            //optimized
            sql = "select machine_id, name from machine m where exists (select 0 from event_log l where m.machine_id = l.machine_id) order by name";
            var machines = db.GetDataTable(sql);
            MachineDropDownList.DataSource = machines;
            MachineDropDownList.DataTextField = "name";
            MachineDropDownList.DataValueField = "machine_id";
            MachineDropDownList.DataBind();
            MachineDropDownList.Items.Insert(0, defaultItem);

            //green tire numbers
            GreenTireNumberDropDownList.DataSource = db.GetGreenTireDropDownData();
            GreenTireNumberDropDownList.DataTextField = "green_tire_number";
            GreenTireNumberDropDownList.DataValueField = "green_tire_number";
            GreenTireNumberDropDownList.DataBind();
            GreenTireNumberDropDownList.Items.Insert(0, defaultItem);

            //operators
            OperaotrDropDownList.DataSource = db.GetOperatorDropDownData();
            OperaotrDropDownList.DataTextField = "text";
            OperaotrDropDownList.DataValueField = "value";
            OperaotrDropDownList.DataBind();
            OperaotrDropDownList.Items.Insert(0, defaultItem);

            //event data
            //sql = "select event_data_name text, event_data_id value from event_data_count_view where total_count > 5";
            //optimized
            sql = "select name, event_data_id from event_data d where exists (select 0 from event_log_data l where d.event_data_id = l.event_data_id) order by name";
            var eventData = db.GetDataTable(sql);
            EventDataDropDownList.DataSource = eventData;
            EventDataDropDownList.DataTextField = "name";
            EventDataDropDownList.DataValueField = "event_data_id";
            EventDataDropDownList.DataBind();
            EventDataDropDownList.Items.Insert(0, defaultItem);

            //shift
            sql = "select shift from shift_view";
            var shifts = db.GetDataTable(sql);
            ShiftDropDownList.DataSource = shifts;
            ShiftDropDownList.DataTextField = "shift";
            ShiftDropDownList.DataValueField = "shift";
            ShiftDropDownList.DataBind();
            ShiftDropDownList.Items.Insert(0, defaultItem);

            //hours
            var hours = new List<string>();
            for (int h = 0; h <= 23; h++)
            {
                hours.Add(h.ToString().PadLeft(2, '0'));
            }
            //minutes
            var minutes = new List<string>();
            for (int m = 0; m <= 59; m++)
            {
                minutes.Add(m.ToString().PadLeft(2, '0'));
            }
            StartHourDropDownList.DataSource = hours;
            EndHourDropDownList.DataSource = hours;
            StartMinuteDropDownList.DataSource = minutes;
            EndMinuteDropDownList.DataSource = minutes;
            StartHourDropDownList.DataBind();
            StartMinuteDropDownList.DataBind();
            EndHourDropDownList.DataBind();
            EndMinuteDropDownList.DataBind();

            //default to today
            StartDateTextBox.Text = DateTime.Now.ToShortDateString();
            EndDateTextBox.Text = StartDateTextBox.Text; //DateTime.Now.AddDays(1).ToShortDateString();

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            //show tracing?
            Trace.IsEnabled = Request.QueryString["trace"] == "true";
            TracingEnabledCheckBox.Checked = Trace.IsEnabled;

            //hide some buttons until needed
            ExcelButton.Visible = false;
            ClearButton.Visible = false;
            //TracingCheckbox.Visible = false;

            if (!IsPostBack)
            {
                //default sort
                SortExpressionHiddenField.Value = "event_log_id DESC";

                NoCuringDataDate.Visible = false;
                Trace.IsEnabled = (Request.QueryString["trace"] == "true");

                //if only a 'part serial' search, then only show part serial text box
                switch (CurrentSearchType)
                {
                    case SearchType.PART_SERIAL_ONLY:
                        SortExpressionHiddenField.Value = "event_log_id ASC";
                        FullEventSearch.Visible = false;
                        HeadLabel.Text = "Tire History";

                        //if tire serial was requested go ahead and run query
                        if (!string.IsNullOrEmpty(Request.QueryString["l"]))
                        {
                            PartSerialNumberTextBox.Text = Request.QueryString["l"];
                            SearchButton_Click(null, null);
                        }
                        break;
                    case SearchType.PARTS_NOT_CURED:
                        FullEventSearch.Visible = false;
                        PartSerialSearch.Visible = false;
                        NoCuringDataDate.Visible = true;
                        NoCuringStartTextBox.Text = DateTime.Now.AddDays(-5).ToShortDateString();
                        HeadLabel.Text = "Parts Without Curing Data";
                        break;
                    default:
                        BindFullSearch();
                        break;
                }

                PartSerialNumberTextBox.Focus();
            }
        }

        protected string GetSearchResultsSql()
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select * from (select Event_Log_Id, Event_Created, Work_Date, Work_Shift, Event_Name, Machine_Name, Part_Created,");
            sql.Append(" Part_Label, Green_Tire_Number, Goodyear_Serial_Number, Operator, Data_Name, Data_Value");
            sql.Append(" FROM event_log_view_with_shift");

            switch (CurrentSearchType)
            {
                case SearchType.PART_SERIAL_ONLY:
                    sql.AppendFormat(" WHERE part_label = '{0}'", PartSerialNumberTextBox.Text.Trim().SqlEscape());
                    break;
                case SearchType.PARTS_NOT_CURED:
                    sql = new StringBuilder();
                    sql.Append("select * from (select t.* from (select Event_Log_Id, Event_Created, Work_Date, Work_Shift, Event_Name, Machine_Name, Part_Created,");
                    sql.Append(" Part_Label, Green_Tire_Number, Goodyear_Serial_Number, Operator, Data_Name, Data_Value");
                    sql.Append(" FROM event_log_view_with_shift");
                    sql.Append(" l JOIN PARTS_NOT_CURED_VIEW nc ON l.part_id = nc.part_id");
                    sql.Append(" WHERE event_name = 'GoodTire'");
                    if (!string.IsNullOrEmpty(NoCuringStartTextBox.Text))
                    {
                        sql.AppendFormat(" AND l.work_date <= to_date('{0}','{1}')",
                            DateTime.Parse(NoCuringStartTextBox.Text).ToString(Settings.Default.DateFormat),
                            Settings.Default.OracleDateFormat);
                    }
                    sql.Append(" ORDER BY l.event_log_id DESC) t");
                    break;
                default:
                    //FULL SEARCH PARAMETERS ENABLED
                    sql.Append(" WHERE 1 = 1");
                    if (EventDropDownList.SelectedValue != "0")
                    {
                        sql.AppendFormat(" AND event_id = {0}", EventDropDownList.SelectedValue.SqlEscape());
                    }
                    if (MachineDropDownList.SelectedValue != "0")
                    {
                        sql.AppendFormat(" AND machine_id = {0}", MachineDropDownList.SelectedValue.SqlEscape());
                    }
                    if (ShiftDropDownList.SelectedValue != "0")
                    {
                        sql.AppendFormat(" AND work_shift = '{0}'", ShiftDropDownList.SelectedValue.SqlEscape());
                    }
                    if (GreenTireNumberDropDownList.SelectedValue != "0")
                    {
                        sql.AppendFormat(" AND green_tire_number = '{0}'", GreenTireNumberDropDownList.SelectedValue.SqlEscape());
                    }
                    if (OperaotrDropDownList.SelectedValue != "0")
                    {
                        //sql.AppendFormat(" AND operator_id = {0}", OperaotrDropDownList.SelectedValue.SqlEscape());
                        sql.AppendFormat(" AND employee_number = '{0}'", OperaotrDropDownList.SelectedValue.SqlEscape());
                    }
                    if (EventDataDropDownList.SelectedValue != "0")
                    {
                        sql.AppendFormat(" AND event_data_id = {0}", EventDataDropDownList.SelectedValue.SqlEscape());
                    }
                    if (!string.IsNullOrEmpty(PartSerialNumberTextBox.Text))
                    {
                        sql.AppendFormat(" AND part_label like '%{0}%'", PartSerialNumberTextBox.Text.Trim().SqlEscape());
                    }

                    //TODO: validate - catch errors - give user feedback if wrong
                    string startStr = string.Format("{0} {1}:{2}:00", StartDateTextBox.Text, StartHourDropDownList.SelectedValue, StartMinuteDropDownList.SelectedValue);
                    string EndStr = string.Format("{0} {1}:{2}:00", EndDateTextBox.Text, EndHourDropDownList.SelectedValue, EndMinuteDropDownList.SelectedValue);

                    //default to date range based on 'dunlop' aka. 'shift based' dates
                    string dateRangeSqlFormat = " AND work_date BETWEEN to_date('{0}','{2}') AND to_date('{1}', '{2}')";

                    //if hours or minutes were specified. don't query by "Dunlop Date" aka. "Shift" based dates
                    if (StartHourDropDownList.SelectedIndex > 0
                        || StartMinuteDropDownList.SelectedIndex > 0
                        || EndHourDropDownList.SelectedIndex > 0
                        || EndMinuteDropDownList.SelectedIndex > 0)
                    {

                        dateRangeSqlFormat = " AND event_created BETWEEN to_date('{0}','{2}') AND to_date('{1}', '{2}')";
                    }

                    sql.AppendFormat(dateRangeSqlFormat,
                        DateTime.Parse(startStr).ToString(Settings.Default.DateTimeFormat),
                        DateTime.Parse(EndStr).ToString(Settings.Default.DateTimeFormat),
                        Settings.Default.OracleDateTimeFormat);
                    break;
            }

            sql.AppendFormat(" ORDER BY {0}", SortExpressionHiddenField.Value.SqlEscape());
            sql.AppendFormat(") t1 WHERE rownum <= {0}", RowLmitDropDownList.SelectedValue.SqlEscape());

            return sql.ToString();
        }

        protected void SearchButton_Click(object sender, EventArgs e)
        {
            ClearButton.Visible = (CurrentSearchType == SearchType.FULL);
           
            var db = new DunlopBarcodeDataContext(((Site)Master).GetConnectionStringSettings());
            var sql = GetSearchResultsSql();
            Trace.Write("sql", sql);
            var results = db.GetDataTable(sql);
            ListView1.DataSource = results;
            ListView1.DataBind();
            if (ListView1.Items.Count > 0)
            {
                ExcelButton.Visible = true;
                //btnExcel.Dataview = results.DefaultView;
                //btnExcel.ExportType = PNayak.Web.UI.WebControls.ExportButton.ExportTypeEnum.CSV;
                //btnExcel.Separator = PNayak.Web.UI.WebControls.ExportButton.SeparatorTypeEnum.Comma;
            }
            //TracingCheckbox.Visible = true;
        }

        /// <summary>
        /// Got this tip from http://forums.asp.net/t/1245474.aspx
        /// Allows controls in grid to render out to Excel
        /// </summary>
        /// <param name="control"></param>
        public override void VerifyRenderingInServerForm(Control control)
        { }

        protected void ListView1_Sorting(object sender, ListViewSortEventArgs e)
        {
            var link = ListView1.FindControl("IDLinkButton") as LinkButton;

            if (SortExpressionHiddenField.Value == ("event_log_id ASC"))
            {
                SortExpressionHiddenField.Value = "event_log_id DESC";
                link.Text = "ID &#9660";
            }
            else
            {
                SortExpressionHiddenField.Value = "event_log_id ASC";
                link.Text = "ID  &#9650";
            }

            SearchButton_Click(null, null);
        }
    }
}