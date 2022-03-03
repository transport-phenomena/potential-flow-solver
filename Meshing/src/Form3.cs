using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Sql;

namespace potFlow_Meshing_v0._3
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
            airfoilChart.Series[0].IsVisibleInLegend = false;
            //create new table for camber in dataset
            /*
            DataTable dtCamber = new DataTable("Camber");
            dtCamber.Columns.Add("xc");
            dtCamber.Columns.Add("yc");
            DataSet dsCamber = new DataSet("Camber");
            dsCamber.Tables.Add(dtCamber);
            */

            airfoilChart.Series[0].Points.Clear();
            foreach (DataRow dr in dstPike1.Tables[1].Rows)
            {
                airfoilChart.Series[0].Points.AddXY(dr[1], dr[2]);
            }
        }

        public string modifyName
        {
            get { return this.Text; }
            set { this.Text = value; }
        }
    }
}
