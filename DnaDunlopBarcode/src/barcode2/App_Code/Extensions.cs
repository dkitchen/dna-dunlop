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
using DunlopBarcode.Model;
using DunlopBarcode.Model.Properties;
using System.Web.Configuration;

namespace DunlopBarcode.Web
{

    public static class Extensions
    {
        public static string SqlEscape(this string input)
        {
            string output = input;
            string[] dirty = new string[] { "\"", "\\", "/", "*", "'", "=", "-", "#", ";", "<", ">", "+", "%" };
            foreach (string dirt in dirty)
            {
                output = output.Replace(dirt, "");
            }
            return output;
        }

        /// <summary>
        /// Returns two column datatable: text = name or 'unknown (e#), value = employee_number 
        /// good for drop downs
        /// </summary>
        /// <param name="db"></param>
        /// <returns></returns>
        public static DataTable GetOperatorDropDownData(this DunlopBarcodeDataContext db)
        {
            //string sql = "select operator_id value, case when name = 'UNKNOWN' THEN name || '  (' || serial_number || ')' ELSE name END text  from operator_good_tire_count_view";
            string sql = "select distinct employee_number value, name text from operator_good_tire_count_view where employee_number is not null order by name";
            var table = db.GetDataTable(sql);
            return table;
        }


        public static DataTable GetGreenTireDropDownData(this DunlopBarcodeDataContext db)
        {
            //green tires - TODO: filter by event_count > x when needed
            //string sql = "select green_tire_number from green_tire_count_view where length(trim(green_tire_number)) = 5 and substr(green_tire_number,1,1) = '5'";
            string sql = "select distinct green_tire_number from part p where isnumeric(green_tire_number) = 1 and green_tire_number > '50000' and green_tire_number < '60000' order by green_tire_number";
            var table = db.GetDataTable(sql);
            return table;
        }

    }
}