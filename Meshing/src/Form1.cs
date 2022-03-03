using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Globalization;
using System.Drawing.Drawing2D;
using System.Windows.Forms.DataVisualization.Charting;
using System.Threading;
using System.IO;

namespace potFlow_Meshing_v0._3
{
    public partial class Form1 : Form
    {
        //global parameters
        int A = 1;
        int B = 2;
        int C = 1;
        double D = 1;

        double xmin, xmax, ymin, ymax, obx, oby;

        Stopwatch stopWatch = new Stopwatch();
        public Form1()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.FixedSingle;

            gbNACAAirfoil_radius.Visible = false;
            lbShowSurface.Visible = false;

            //chart settings
            meshChart.ChartAreas[0].AxisX.MajorGrid.LineWidth = 0;
            meshChart.ChartAreas[0].AxisY.MajorGrid.LineWidth = 0;
            meshChart.ChartAreas[0].AxisY.LabelStyle.Format = "#";
            meshChart.ChartAreas[0].AxisX.LabelStyle.Format = "#";
            meshChart.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastPoint;
            meshChart.Series[0].Color = Color.Red;
            meshChart.Series[0].IsVisibleInLegend = false;
            meshChart.ChartAreas[0].AxisX.ScaleView.Zoomable = true;
            meshChart.ChartAreas[0].AxisY.ScaleView.Zoomable = true;
            meshChart.MouseWheel += MeshChart_MouseWheel;
            meshChart.Series[2].Enabled = false;

            meshChart.Series[1].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;
            meshChart.Series[1].Color = Color.Blue;
            meshChart.Series[1].IsVisibleInLegend = false;

            meshChart.Series[2].Color = Color.Black;
            meshChart.Series[2].IsVisibleInLegend = false;

            meshChart.Series[3].Color = Color.Black;
            meshChart.Series[3].IsVisibleInLegend = false;


            //table layout panel settings
            tableLayoutPanel1.AutoSize = false;
            splitContainer2.IsSplitterFixed = true;
            splitContainer2.FixedPanel = FixedPanel.Panel1;
            splitContainer1.IsSplitterFixed = true;
            splitContainer1.FixedPanel = FixedPanel.Panel1;

        }

        private void CreateNormalVectors()
		{
            double dx = Convert.ToDouble(tbDx.Text);

            double nx, ny, x, y;
            double yfirst;
            double dist;
            double deltaX = 1;
            double deltaY = 1;
            int p, i;
            int j;

            bool fNan, bNan, uNan, dNan;

            //define top row starting point
            double xs = Convert.ToDouble(dstPike.Tables[7].Rows[0][1]);
            double ys = Convert.ToDouble(dstPike.Tables[7].Rows[0][2]);

            int up = (int)((A * D + B * D + D) / dx) + 1;

            //NEW
            if (cbGeometry.Text == "NACA Airfoil")
            {
                //foreach (DataRow dr in dstPike.Tables[7].Rows)
                for (int n = 0; n < dstPike.Tables[7].Rows.Count;)
                {
                    y = Convert.ToDouble(dstPike.Tables[7].Rows[n][2]);
                    x = Convert.ToDouble(dstPike.Tables[7].Rows[n][1]);
                    p = Convert.ToInt32(dstPike.Tables[7].Rows[n][0]);

                    //take surface points from the same y and append nx ny for each point

                    //check how many points on the same y in both directions
                    i = 0;
                    yfirst = y;
                    try
                    {
                        while (y == yfirst && p + i == Convert.ToDouble(dstPike.Tables[7].Rows[n + i][0]))
                        {
                            y = Convert.ToDouble(dstPike.Tables[7].Rows[n + i][2]);
                            i++;
                        }
                    }
                    catch
                    { }
                    fNan = Double.IsNaN(Convert.ToDouble(dstPike.Tables[5].Rows[p + i][1]));
                    bNan = Double.IsNaN(Convert.ToDouble(dstPike.Tables[5].Rows[p - 1][1]));
                    uNan = Double.IsNaN(Convert.ToDouble(dstPike.Tables[5].Rows[p - up][1]));
                    dNan = Double.IsNaN(Convert.ToDouble(dstPike.Tables[5].Rows[p + up][1]));

                    if (fNan == true && dNan == true && bNan == false && uNan == false)
                    {
                        if(i == 1)
						{
                            deltaX = dx;
                            deltaY = dx;
                            dist = 1 / Math.Sqrt((deltaX * deltaX) + (deltaY * deltaY));
                            nx = -dist * deltaY;
                            ny = dist * deltaX;
                            dstPike.Tables[5].Rows[p][4] = nx;
                            dstPike.Tables[5].Rows[p][5] = ny;
                            n++;

                            continue;
                        }
                        else
						{
                            deltaX = dx;
                            deltaY = dx;
                            dist = 1 / Math.Sqrt((deltaX * deltaX) + (deltaY * deltaY));
                            nx = -dist * deltaY;
                            ny = dist * deltaX;
                            dstPike.Tables[5].Rows[p][4] = nx;
                            dstPike.Tables[5].Rows[p][5] = ny;
                            n++;
                            j = 1;
                            for (int k = p + 1; k < p + i - 1; k++)
                            {
                                nx = 0;
                                ny = 1;

                                dstPike.Tables[5].Rows[p + j][4] = nx;
                                dstPike.Tables[5].Rows[p + j][5] = ny;

                                n++;
                                j++;
                            }
                            deltaX = dx;
                            deltaY = dx;
                            dist = 1 / Math.Sqrt((deltaX * deltaX) + (deltaY * deltaY));
                            nx = -dist * deltaY;
                            ny = dist * deltaX;
                            dstPike.Tables[5].Rows[p + j][4] = nx;
                            dstPike.Tables[5].Rows[p + j][5] = ny;
                            n++;
                        }
                        continue;
                    }
                    else if (fNan == false && bNan == true && uNan == false && dNan == true)
                    {
                        if(i == 1)
						{
                            deltaX = dx;
                            deltaY = dx;
                            dist = 1 / Math.Sqrt((deltaX * deltaX) + (deltaY * deltaY));
                            nx = dist * deltaY;
                            ny = dist * deltaX;
                            dstPike.Tables[5].Rows[p][4] = nx;
                            dstPike.Tables[5].Rows[p][5] = ny;
                            n++;

                            continue;
                        }
                        else
						{
                            deltaX = dx;
                            deltaY = dx;
                            dist = 1 / Math.Sqrt((deltaX * deltaX) + (deltaY * deltaY));
                            nx = dist * deltaY;
                            ny = dist * deltaX;
                            dstPike.Tables[5].Rows[p][4] = nx;
                            dstPike.Tables[5].Rows[p][5] = ny;
                            n++;
                            j = 1;
                            for (int k = p + 1; k < p + i - 1; k++)
                            {
                                nx = 0;
                                ny = 1;

                                dstPike.Tables[5].Rows[p + j][4] = nx;
                                dstPike.Tables[5].Rows[p + j][5] = ny;

                                n++;
                                j++;
                            }
                            deltaX = dx;
                            deltaY = dx;
                            dist = 1 / Math.Sqrt((deltaX * deltaX) + (deltaY * deltaY));
                            nx = dist * deltaY;
                            ny = dist * deltaX;
                            dstPike.Tables[5].Rows[p + j][4] = nx;
                            dstPike.Tables[5].Rows[p + j][5] = ny;
                            n++;
                        }
                        continue;
                    }
                    else if (fNan == true && bNan == false && uNan == true && dNan == false)
                    {
                        if(i == 1)
						{
                            deltaX = dx;
                            deltaY = dx;
                            dist = 1 / Math.Sqrt((deltaX * deltaX) + (deltaY * deltaY));
                            nx = -dist * deltaY;
                            ny = -dist * deltaX;
                            dstPike.Tables[5].Rows[p][4] = nx;
                            dstPike.Tables[5].Rows[p][5] = ny;
                            n++;

                            continue;
                        }
                        else
						{
                            deltaX = dx;
                            deltaY = dx;
                            dist = 1 / Math.Sqrt((deltaX * deltaX) + (deltaY * deltaY));
                            nx = -dist * deltaY;
                            ny = -dist * deltaX;
                            dstPike.Tables[5].Rows[p][4] = nx;
                            dstPike.Tables[5].Rows[p][5] = ny;
                            n++;
                            j = 1;
                            for (int k = p + 1; k < p + i - 1; k++)
                            {
                                nx = 0;
                                ny = -1;

                                dstPike.Tables[5].Rows[p + j][4] = nx;
                                dstPike.Tables[5].Rows[p + j][5] = ny;

                                n++;
                                j++;
                            }
                            deltaX = dx;
                            deltaY = dx;
                            dist = 1 / Math.Sqrt((deltaX * deltaX) + (deltaY * deltaY));
                            nx = -dist * deltaY;
                            ny = -dist * deltaX;
                            dstPike.Tables[5].Rows[p + j][4] = nx;
                            dstPike.Tables[5].Rows[p + j][5] = ny;
                            n++;
                        }
                        continue;
                    }
                    else if (bNan == true && fNan == false && uNan == true && dNan == false)
                    {
                        if(i == 1)
						{
                            deltaX = dx;
                            deltaY = dx;
                            dist = 1 / Math.Sqrt((deltaX * deltaX) + (deltaY * deltaY));
                            nx = dist * deltaY;
                            ny = -dist * deltaX;
                            dstPike.Tables[5].Rows[p][4] = nx;
                            dstPike.Tables[5].Rows[p][5] = ny;
                            n++;

                            continue;
                        }
                        else
						{
                            deltaX = dx;
                            deltaY = dx;
                            dist = 1 / Math.Sqrt((deltaX * deltaX) + (deltaY * deltaY));
                            nx = dist * deltaY;
                            ny = -dist * deltaX;
                            dstPike.Tables[5].Rows[p][4] = nx;
                            dstPike.Tables[5].Rows[p][5] = ny;
                            n++;
                            j = 1;
                            for (int k = p + 1; k < p + i - 1; k++)
                            {
                                nx = 0;
                                ny = -1;

                                dstPike.Tables[5].Rows[p + j][4] = nx;
                                dstPike.Tables[5].Rows[p + j][5] = ny;

                                n++;
                                j++;
                            }
                            deltaX = dx;
                            deltaY = dx;
                            dist = 1 / Math.Sqrt((deltaX * deltaX) + (deltaY * deltaY));
                            nx = dist * deltaY;
                            ny = -dist * deltaX;
                            dstPike.Tables[5].Rows[p + j][4] = nx;
                            dstPike.Tables[5].Rows[p + j][5] = ny;
                            n++;
                        }
                        continue;
                    }
                    else if ((fNan == true && bNan == true) || (fNan == false && bNan == false))
                    {

                        if (y < ys && (fNan == true && bNan == true))
                        {
                            deltaX = dx;
                            deltaY = dx;
                            dist = 1 / Math.Sqrt((deltaX * deltaX) + (deltaY * deltaY));
                            nx = dist * deltaY;
                            ny = -dist * deltaX;
                            dstPike.Tables[5].Rows[p][4] = nx;
                            dstPike.Tables[5].Rows[p][5] = ny;
                            n++;
                            j = 1;
                            for (int k = p + 1; k < p + i - 1; k++)
                            {
                                nx = 0;
                                ny = -1;

                                dstPike.Tables[5].Rows[p + j][4] = nx;
                                dstPike.Tables[5].Rows[p + j][5] = ny;

                                n++;
                                j++;
                            }
                            deltaX = dx;
                            deltaY = dx;
                            dist = 1 / Math.Sqrt((deltaX * deltaX) + (deltaY * deltaY));
                            nx = -dist * deltaY;
                            ny = -dist * deltaX;
                            dstPike.Tables[5].Rows[p + j][4] = nx;
                            dstPike.Tables[5].Rows[p + j][5] = ny;
                            n++;
                        }
                        else if (y < ys && (fNan == false && bNan == false))
                        {
                            deltaX = dx;
                            deltaY = dx;
                            dist = 1 / Math.Sqrt((deltaX * deltaX) + (deltaY * deltaY));
                            nx = -dist * deltaY;
                            ny = -dist * deltaX;
                            dstPike.Tables[5].Rows[p][4] = nx;
                            dstPike.Tables[5].Rows[p][5] = ny;
                            n++;
                            j = 1;
                            for (int k = p + 1; k < p + i - 1; k++)
                            {
                                nx = 0;
                                ny = -1;

                                dstPike.Tables[5].Rows[p + j][4] = nx;
                                dstPike.Tables[5].Rows[p + j][5] = ny;

                                n++;
                                j++;
                            }
                            deltaX = dx;
                            deltaY = dx;
                            dist = 1 / Math.Sqrt((deltaX * deltaX) + (deltaY * deltaY));
                            nx = dist * deltaY;
                            ny = -dist * deltaX;
                            dstPike.Tables[5].Rows[p + j][4] = nx;
                            dstPike.Tables[5].Rows[p + j][5] = ny;
                            n++;
                        }
                        else
                        {
                            deltaX = dx;
                            deltaY = dx;
                            dist = 1 / Math.Sqrt((deltaX * deltaX) + (deltaY * deltaY));
                            nx = -dist * deltaY;
                            ny = dist * deltaX;
                            dstPike.Tables[5].Rows[p][4] = nx;
                            dstPike.Tables[5].Rows[p][5] = ny;
                            n++;
                            j = 1;
                            for (int k = p + 1; k < p + i - 1; k++)
                            {
                                nx = 0;
                                ny = 1;

                                dstPike.Tables[5].Rows[p + j][4] = nx;
                                dstPike.Tables[5].Rows[p + j][5] = ny;

                                n++;
                                j++;
                            }
                            deltaX = dx;
                            deltaY = dx;
                            dist = 1 / Math.Sqrt((deltaX * deltaX) + (deltaY * deltaY));
                            nx = dist * deltaY;
                            ny = dist * deltaX;
                            dstPike.Tables[5].Rows[p + j][4] = nx;
                            dstPike.Tables[5].Rows[p + j][5] = ny;
                            n++;
                        }
                        continue;
                    }
                    else if (fNan == true && bNan == false && uNan == false && dNan == false)
                    {
                        j = 0;
                        for (int k = p; k < p + i; k++)
                        {
                            nx = -1;
                            ny = 0;

                            dstPike.Tables[5].Rows[p + j][4] = nx;
                            dstPike.Tables[5].Rows[p + j][5] = ny;

                            n++;
                            j++;
                        }
                    }
                    else if (fNan == false && bNan == true && uNan == false && dNan == false)
                    {
                        j = 0;
                        for (int k = p; k < p + i; k++)
                        {
                            nx = 1;
                            ny = 0;

                            dstPike.Tables[5].Rows[p + j][4] = nx;
                            dstPike.Tables[5].Rows[p + j][5] = ny;

                            n++;
                            j++;
                        }
                    }
                }
            }

            else if (cbGeometry.Text == "Cylinder") //code is exactly the same as for NACA airfoil; separated for future modifications
            {
                //foreach (DataRow dr in dstPike.Tables[7].Rows)
                for (int n = 0; n < dstPike.Tables[7].Rows.Count;)
                {
                    y = Convert.ToDouble(dstPike.Tables[7].Rows[n][2]);
                    x = Convert.ToDouble(dstPike.Tables[7].Rows[n][1]);
                    p = Convert.ToInt32(dstPike.Tables[7].Rows[n][0]);

                    //take surface points from the same y and append nx ny for each point

                    //check how many points on the same y in both directions
                    i = 0;
                    yfirst = y;
                    try
                    {
                        while (y == yfirst && p + i == Convert.ToDouble(dstPike.Tables[7].Rows[n + i][0]))
                        {
                            y = Convert.ToDouble(dstPike.Tables[7].Rows[n + i][2]);
                            i++;
                        }
                    }
                    catch
                    { }
                    fNan = Double.IsNaN(Convert.ToDouble(dstPike.Tables[5].Rows[p + i][1]));
                    bNan = Double.IsNaN(Convert.ToDouble(dstPike.Tables[5].Rows[p - 1][1]));
                    uNan = Double.IsNaN(Convert.ToDouble(dstPike.Tables[5].Rows[p - up][1]));
                    dNan = Double.IsNaN(Convert.ToDouble(dstPike.Tables[5].Rows[p + up][1]));

                    if (fNan == true && dNan == true && bNan == false && uNan == false)
                    {
                        if (i == 1)
                        {
                            deltaX = dx;
                            deltaY = dx;
                            dist = 1 / Math.Sqrt((deltaX * deltaX) + (deltaY * deltaY));
                            nx = -dist * deltaY;
                            ny = dist * deltaX;
                            dstPike.Tables[5].Rows[p][4] = nx;
                            dstPike.Tables[5].Rows[p][5] = ny;
                            n++;

                            continue;
                        }
                        else
                        {
                            deltaX = dx;
                            deltaY = dx;
                            dist = 1 / Math.Sqrt((deltaX * deltaX) + (deltaY * deltaY));
                            nx = -dist * deltaY;
                            ny = dist * deltaX;
                            dstPike.Tables[5].Rows[p][4] = nx;
                            dstPike.Tables[5].Rows[p][5] = ny;
                            n++;
                            j = 1;
                            for (int k = p + 1; k < p + i - 1; k++)
                            {
                                nx = 0;
                                ny = 1;

                                dstPike.Tables[5].Rows[p + j][4] = nx;
                                dstPike.Tables[5].Rows[p + j][5] = ny;

                                n++;
                                j++;
                            }
                            deltaX = dx;
                            deltaY = dx;
                            dist = 1 / Math.Sqrt((deltaX * deltaX) + (deltaY * deltaY));
                            nx = -dist * deltaY;
                            ny = dist * deltaX;
                            dstPike.Tables[5].Rows[p + j][4] = nx;
                            dstPike.Tables[5].Rows[p + j][5] = ny;
                            n++;
                        }
                        continue;
                    }
                    else if (fNan == false && bNan == true && uNan == false && dNan == true)
                    {
                        if (i == 1)
                        {
                            deltaX = dx;
                            deltaY = dx;
                            dist = 1 / Math.Sqrt((deltaX * deltaX) + (deltaY * deltaY));
                            nx = dist * deltaY;
                            ny = dist * deltaX;
                            dstPike.Tables[5].Rows[p][4] = nx;
                            dstPike.Tables[5].Rows[p][5] = ny;
                            n++;

                            continue;
                        }
                        else
                        {
                            deltaX = dx;
                            deltaY = dx;
                            dist = 1 / Math.Sqrt((deltaX * deltaX) + (deltaY * deltaY));
                            nx = dist * deltaY;
                            ny = dist * deltaX;
                            dstPike.Tables[5].Rows[p][4] = nx;
                            dstPike.Tables[5].Rows[p][5] = ny;
                            n++;
                            j = 1;
                            for (int k = p + 1; k < p + i - 1; k++)
                            {
                                nx = 0;
                                ny = 1;

                                dstPike.Tables[5].Rows[p + j][4] = nx;
                                dstPike.Tables[5].Rows[p + j][5] = ny;

                                n++;
                                j++;
                            }
                            deltaX = dx;
                            deltaY = dx;
                            dist = 1 / Math.Sqrt((deltaX * deltaX) + (deltaY * deltaY));
                            nx = dist * deltaY;
                            ny = dist * deltaX;
                            dstPike.Tables[5].Rows[p + j][4] = nx;
                            dstPike.Tables[5].Rows[p + j][5] = ny;
                            n++;
                        }
                        continue;
                    }
                    else if (fNan == true && bNan == false && uNan == true && dNan == false)
                    {
                        if (i == 1)
                        {
                            deltaX = dx;
                            deltaY = dx;
                            dist = 1 / Math.Sqrt((deltaX * deltaX) + (deltaY * deltaY));
                            nx = -dist * deltaY;
                            ny = -dist * deltaX;
                            dstPike.Tables[5].Rows[p][4] = nx;
                            dstPike.Tables[5].Rows[p][5] = ny;
                            n++;

                            continue;
                        }
                        else
                        {
                            deltaX = dx;
                            deltaY = dx;
                            dist = 1 / Math.Sqrt((deltaX * deltaX) + (deltaY * deltaY));
                            nx = -dist * deltaY;
                            ny = -dist * deltaX;
                            dstPike.Tables[5].Rows[p][4] = nx;
                            dstPike.Tables[5].Rows[p][5] = ny;
                            n++;
                            j = 1;
                            for (int k = p + 1; k < p + i - 1; k++)
                            {
                                nx = 0;
                                ny = -1;

                                dstPike.Tables[5].Rows[p + j][4] = nx;
                                dstPike.Tables[5].Rows[p + j][5] = ny;

                                n++;
                                j++;
                            }
                            deltaX = dx;
                            deltaY = dx;
                            dist = 1 / Math.Sqrt((deltaX * deltaX) + (deltaY * deltaY));
                            nx = -dist * deltaY;
                            ny = -dist * deltaX;
                            dstPike.Tables[5].Rows[p + j][4] = nx;
                            dstPike.Tables[5].Rows[p + j][5] = ny;
                            n++;
                        }
                        continue;
                    }
                    else if (bNan == true && fNan == false && uNan == true && dNan == false)
                    {
                        if (i == 1)
                        {
                            deltaX = dx;
                            deltaY = dx;
                            dist = 1 / Math.Sqrt((deltaX * deltaX) + (deltaY * deltaY));
                            nx = dist * deltaY;
                            ny = -dist * deltaX;
                            dstPike.Tables[5].Rows[p][4] = nx;
                            dstPike.Tables[5].Rows[p][5] = ny;
                            n++;

                            continue;
                        }
                        else
                        {
                            deltaX = dx;
                            deltaY = dx;
                            dist = 1 / Math.Sqrt((deltaX * deltaX) + (deltaY * deltaY));
                            nx = dist * deltaY;
                            ny = -dist * deltaX;
                            dstPike.Tables[5].Rows[p][4] = nx;
                            dstPike.Tables[5].Rows[p][5] = ny;
                            n++;
                            j = 1;
                            for (int k = p + 1; k < p + i - 1; k++)
                            {
                                nx = 0;
                                ny = -1;

                                dstPike.Tables[5].Rows[p + j][4] = nx;
                                dstPike.Tables[5].Rows[p + j][5] = ny;

                                n++;
                                j++;
                            }
                            deltaX = dx;
                            deltaY = dx;
                            dist = 1 / Math.Sqrt((deltaX * deltaX) + (deltaY * deltaY));
                            nx = dist * deltaY;
                            ny = -dist * deltaX;
                            dstPike.Tables[5].Rows[p + j][4] = nx;
                            dstPike.Tables[5].Rows[p + j][5] = ny;
                            n++;
                        }
                        continue;
                    }
                    else if ((fNan == true && bNan == true) || (fNan == false && bNan == false))
                    {

                        if (y < ys && (fNan == true && bNan == true))
                        {
                            deltaX = dx;
                            deltaY = dx;
                            dist = 1 / Math.Sqrt((deltaX * deltaX) + (deltaY * deltaY));
                            nx = dist * deltaY;
                            ny = -dist * deltaX;
                            dstPike.Tables[5].Rows[p][4] = nx;
                            dstPike.Tables[5].Rows[p][5] = ny;
                            n++;
                            j = 1;
                            for (int k = p + 1; k < p + i - 1; k++)
                            {
                                nx = 0;
                                ny = -1;

                                dstPike.Tables[5].Rows[p + j][4] = nx;
                                dstPike.Tables[5].Rows[p + j][5] = ny;

                                n++;
                                j++;
                            }
                            deltaX = dx;
                            deltaY = dx;
                            dist = 1 / Math.Sqrt((deltaX * deltaX) + (deltaY * deltaY));
                            nx = -dist * deltaY;
                            ny = -dist * deltaX;
                            dstPike.Tables[5].Rows[p + j][4] = nx;
                            dstPike.Tables[5].Rows[p + j][5] = ny;
                            n++;
                        }
                        else if (y < ys && (fNan == false && bNan == false))
                        {
                            deltaX = dx;
                            deltaY = dx;
                            dist = 1 / Math.Sqrt((deltaX * deltaX) + (deltaY * deltaY));
                            nx = -dist * deltaY;
                            ny = -dist * deltaX;
                            dstPike.Tables[5].Rows[p][4] = nx;
                            dstPike.Tables[5].Rows[p][5] = ny;
                            n++;
                            j = 1;
                            for (int k = p + 1; k < p + i - 1; k++)
                            {
                                nx = 0;
                                ny = -1;

                                dstPike.Tables[5].Rows[p + j][4] = nx;
                                dstPike.Tables[5].Rows[p + j][5] = ny;

                                n++;
                                j++;
                            }
                            deltaX = dx;
                            deltaY = dx;
                            dist = 1 / Math.Sqrt((deltaX * deltaX) + (deltaY * deltaY));
                            nx = dist * deltaY;
                            ny = -dist * deltaX;
                            dstPike.Tables[5].Rows[p + j][4] = nx;
                            dstPike.Tables[5].Rows[p + j][5] = ny;
                            n++;
                        }
                        else
                        {
                            deltaX = dx;
                            deltaY = dx;
                            dist = 1 / Math.Sqrt((deltaX * deltaX) + (deltaY * deltaY));
                            nx = -dist * deltaY;
                            ny = dist * deltaX;
                            dstPike.Tables[5].Rows[p][4] = nx;
                            dstPike.Tables[5].Rows[p][5] = ny;
                            n++;
                            j = 1;
                            for (int k = p + 1; k < p + i - 1; k++)
                            {
                                nx = 0;
                                ny = 1;

                                dstPike.Tables[5].Rows[p + j][4] = nx;
                                dstPike.Tables[5].Rows[p + j][5] = ny;

                                n++;
                                j++;
                            }
                            deltaX = dx;
                            deltaY = dx;
                            dist = 1 / Math.Sqrt((deltaX * deltaX) + (deltaY * deltaY));
                            nx = dist * deltaY;
                            ny = dist * deltaX;
                            dstPike.Tables[5].Rows[p + j][4] = nx;
                            dstPike.Tables[5].Rows[p + j][5] = ny;
                            n++;
                        }
                        continue;
                    }
                    else if (fNan == true && bNan == false && uNan == false && dNan == false)
                    {
                        j = 0;
                        for (int k = p; k < p + i; k++)
                        {
                            nx = -1;
                            ny = 0;

                            dstPike.Tables[5].Rows[p + j][4] = nx;
                            dstPike.Tables[5].Rows[p + j][5] = ny;

                            n++;
                            j++;
                        }
                    }
                    else if (fNan == false && bNan == true && uNan == false && dNan == false)
                    {
                        j = 0;
                        for (int k = p; k < p + i; k++)
                        {
                            nx = 1;
                            ny = 0;

                            dstPike.Tables[5].Rows[p + j][4] = nx;
                            dstPike.Tables[5].Rows[p + j][5] = ny;

                            n++;
                            j++;
                        }
                    }
                }
            }
        }

        private void SetDGV()
		{
            DataPointCollection dpMeshBaseline = meshChart.Series[0].Points;
            DataPointCollection Mesh = dpMeshBaseline;
            D = Convert.ToDouble(tbMeshScale.Text) * 0.5;
            double U = Convert.ToDouble(tbU.Text);

            double dx = Convert.ToDouble(tbDx.Text);
            int i = 0;
            int k = 0;
            int j = 0;
            int p;
            double x, y, BCval;
            int N = (int)((A * D + B * D + D) / dx) + 1;
            int M = (int)((2 * C * D) / dx);

            //domain boundries
            double xInlet = Convert.ToDouble(dstPike.Tables[0].Rows[0][1]);
            double yTop = Convert.ToDouble(dstPike.Tables[0].Rows[0][2]);
            double xOutlet = ((B * D) + D);
            double yBottom = -yTop;

            bool surface, dirichlet, neumann;

            foreach (DataPoint dp in Mesh)
            {
                // add to finalMesh datatable
                p = i;
                x = dp.XValue;
                y = dp.YValues[0];

                if(Double.IsNaN(dp.XValue))
				{
                    p = -1;
                    dstPike.Tables[5].Rows.Add(p, x, y);
                    j++;
                    continue;
                }                

                dstPike.Tables[5].Rows.Add(p, x, y);
                //condition statements
                try
				{
                    surface = Convert.ToInt32(dstPike.Tables[7].Rows[k][0]) == p + j;
                }
                catch
				{
                    surface = false;
				}
                dirichlet = y == yTop || y == yBottom || x == xInlet;
                neumann = (x < xOutlet + (dx / 100)) && x > xOutlet - (dx / 100);

                if (surface == true && dirichlet == false && neumann == false)
                {
                    dstPike.Tables[5].Rows[p + j][3] = "geometry surface";
                    k++;
                    i++;
                    continue;
                }
                else if (dirichlet == true && neumann == false)
                {
                    dstPike.Tables[5].Rows[p + j][3] = "domain dirichlet";
                    BCval = y * U;
                    dstPike.Tables[5].Rows[p + j][6] = BCval;
                    i++;
                    continue;
                }
                else if ((neumann == true && dirichlet == true) || (neumann == true && dirichlet == false))
                {
                    dstPike.Tables[5].Rows[p + j][3] = "domain neumann";
                    i++;
                    continue;
                }
                i++;
            }

            int domainStart = N + 1;
            int boundaryStart = Convert.ToInt32(dstPike.Tables[5].Rows[Convert.ToInt32(dstPike.Tables[5].Rows.Count - 1)][0]) + 2 - (2 * (N - 1)) - (2 * M);// - Convert.ToInt32(dstPike.Tables[7].Rows.Count);
            k = domainStart;
            int l = boundaryStart;
            j = 1;
            string BC;
            //renumber mesh points
            foreach(DataRow dr in dstPike.Tables[5].Rows)
			{
                BC = dr[3].ToString();

                if (Double.IsNaN(Convert.ToDouble(dr[1])))
                {
                    continue;
                }
                else if (BC == "domain dirichlet" || BC == "domain neumann")
                {
                    dr[0] = l;
                    l++;
                }
                else
                {
                    dr[0] = j;
                    j++;
                }
            }
            // Data Grid View settings
            dgvPoints.DataSource = dstPike.Tables[5];
            dgvPoints.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvPoints.RowHeadersVisible = false;
            dgvPoints.AutoGenerateColumns = true;
            dgvPoints.Columns["boundary"].Visible = false;
            dgvPoints.Columns["nx"].Visible = false;
            dgvPoints.Columns["ny"].Visible = false;
            dgvPoints.Columns["BC value"].Visible = false;
        }


        private void MeshChart_MouseWheel(object sender, MouseEventArgs e)
        {
            var chart = meshChart; //(Chart)sender;
            var xAxis = chart.ChartAreas[0].AxisX;
            var yAxis = chart.ChartAreas[0].AxisY;

            try
            {
                if (e.Delta < 0) // Scrolled down.
                {
                    xAxis.ScaleView.ZoomReset();
                    yAxis.ScaleView.ZoomReset();
                }
                else if (e.Delta > 0) // Scrolled up.
                {
                    var xMin = xAxis.ScaleView.ViewMinimum;
                    var xMax = xAxis.ScaleView.ViewMaximum;
                    var yMin = yAxis.ScaleView.ViewMinimum;
                    var yMax = yAxis.ScaleView.ViewMaximum;

                    var posXStart = xAxis.PixelPositionToValue(e.Location.X) - (xMax - xMin) / 4;
                    var posXFinish = xAxis.PixelPositionToValue(e.Location.X) + (xMax - xMin) / 4;
                    var posYStart = yAxis.PixelPositionToValue(e.Location.Y) - (yMax - yMin) / 4;
                    var posYFinish = yAxis.PixelPositionToValue(e.Location.Y) + (yMax - yMin) / 4;

                    xAxis.ScaleView.Zoom(posXStart, posXFinish);
                    yAxis.ScaleView.Zoom(posYStart, posYFinish);
                }
            }
            catch { }
        }

        private void calculateMesh()
        {
            D = Convert.ToDouble(tbMeshScale.Text) * 0.5;
            //calculate mesh points
            int p = 0;
            double x;
            double y;
            double dx = Convert.ToDouble(tbDx.Text);
            double dy = dx;
            int numOfElemX = (int)((((1 + B) * D) - (-A * D)) / dx); // number of elements in x direction
            int numOfElemY = (int)(((-C * D) - (C * D)) / dy); //number of elements in y direction

            //approximate number of points
            double approxNo = (((A * D) + (B * D) + D) / dx) * ((2 * C * D) / dx);
            double currentNo = 0;
            double percentage = 0;
            string percentageString;

            xmax = -1e300; xmin = 1e300; ymax = -1e300; ymin = 1e300;
            // x loop
            for (int j = 0; j <= Math.Abs(numOfElemY); j++)
            {
                // y loop
                for (int i = 0; i <= Math.Abs(numOfElemX); i++)
                {
                    x = (-A * D) + i * dx; //calculating points
                    y = (C * D) - j * dy;

                    dstPike.Tables[0].Rows.Add(p, x, y); //dataset for data grid view

                    if (x > xmax) xmax = x;
                    if (x < xmin) xmin = x;
                    if (y > ymax) ymax = y;
                    if (y < ymin) ymin = y;
                }
                currentNo = dstPike.Tables[0].Rows.Count;
                percentage = (int)((currentNo / approxNo) * 100);
                percentageString = Convert.ToString(percentage) + "%";
                changeLine(rtbInfo, 4, percentageString);
            }

            percentageString = Convert.ToString(percentage) + "%";
            if (percentage > 100)
            {
                changeLine(rtbInfo, 4, "100%");
            }
            else
            {
                changeLine(rtbInfo, 4, percentageString);
            }
            obx = xmax - xmin;
            oby = ymax - ymin;

        }

        private void rtbInfo_TextChanged(object sender, EventArgs e)
        {
            // set the current caret position to the end
            rtbInfo.SelectionStart = rtbInfo.Text.Length;
            // scroll it automatically
            rtbInfo.ScrollToCaret();
        }

        private void tbNACAn_radius_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != ','))
            {
                e.Handled = true;
            }
        }

        private void tbAlpha_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
            (e.KeyChar != ',') && (e.KeyChar != '-'))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if (((e.KeyChar == ',') && ((sender as TextBox).Text.IndexOf(',') > -1)) || (e.KeyChar == '-') && ((sender as TextBox).Text.IndexOf('-') > -1))
            {
                e.Handled = true;
            }
        }

        private void tbSt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
            (e.KeyChar != ','))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == ',') && ((sender as TextBox).Text.IndexOf(',') > -1))
            {
                e.Handled = true;
            }
        }
        private void tbMeshScale_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void tbDx_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
            (e.KeyChar != ','))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == ',') && ((sender as TextBox).Text.IndexOf(',') > -1))
            {
                e.Handled = true;
            }
        }

        private void DeleteInnerPoints1()
        {
            double dx = Convert.ToDouble(tbDx.Text);
            double meshScale = Convert.ToDouble(tbMeshScale.Text) * 0.5;
            double st = dx / 10;
            DataPointCollection dpMesh = meshChart.Series[0].Points;                                                    // dataPointCollection of baseline mesh
            DataPoint dpsy = dpMesh.FindByValue(0, "Y");                                                                // data point start with y=0
            DataPoint dpsM = dpMesh.FindByValue(0, "X", dpMesh.IndexOf(dpsy));                                          // data point start mesh x=0, y=0
            int startMesh = dpMesh.IndexOf(dpsM);                                                                       // index of dspM
            DataPoint UdpsA = dpMesh.FindByValue(0, "Y", startMesh + (int)(((B * D) + D) / dx) + 1);                    // upper data point start airfoil with y=0
            int UstartAirfoil = dpMesh.IndexOf(UdpsA);                                                                  // index of UdpsA
            DataPoint LdpsA = dpMesh.FindByValue(0, "Y", UstartAirfoil + 1);                                            // lower data point start airfoil with y=0
			int LstartAirfoil = dpMesh.IndexOf(LdpsA);                                                                  // index of LdpsA
            bool resx;
            int z;

            // move one tier up the mesh
            int up = (int)((A * D + B * D + D) / dx) + 1;

            // define starting points
            int i = startMesh;
            int j = UstartAirfoil;
            int k = LstartAirfoil;
            int m;

            while (dpMesh[i].XValue <= dpMesh[dpMesh.Count - 1].XValue)     // delete upper mesh points as long as meshPoint x value is less than last point of airfoil x value
            {
                // delete points above y=0
                while (dpMesh[j].XValue <= dpMesh[i].XValue)                // move along upper airfoil until meshPoint x value is reached
                {
                    j++;
                }
                while (dpMesh[k].XValue <= dpMesh[i].XValue)                // move along lower airfoil until meshPoint x value is reached
                {
                    k++;
                }

                m = 1;
                if (dpMesh[i].YValues[0] < dpMesh[j].YValues[0] && dpMesh[i].YValues[0] > dpMesh[k].YValues[0]) // check if point is inside of airfoil
                {
                    while (dpMesh[i - m * up].YValues[0] < dpMesh[j].YValues[0])                                // delete points if they are below upper suface
                    {
                        dpMesh[i - m * up].XValue = Double.NaN;
                        dpMesh[i - m * up].YValues[0] = Double.NaN;
                        m++;
                    }
                }
                else                                                                                            //if meshPoints are not inside of aifoil, move up until they are
                {
                    while (dpMesh[i - m * up].YValues[0] < dpMesh[k].YValues[0])
                    {
                        m++;
                    }

                    while (dpMesh[i - m * up].YValues[0] < dpMesh[j].YValues[0])                                // when they are inside of airfoil, start deleting them
                    {
                        dpMesh[i - m * up].XValue = Double.NaN;
                        dpMesh[i - m * up].YValues[0] = Double.NaN;
                        m++;
                    }
                }
                i++;
            }

            i = startMesh;
            j = UstartAirfoil;
            k = LstartAirfoil;

            while (dpMesh[i].XValue <= dpMesh[dpMesh.Count - 1].XValue)     // delete lower mesh points as long as meshPoint x value is less than last point of airfoil x value
            {
                // delete points below y = 0
                while (dpMesh[j].XValue <= dpMesh[i].XValue)                // move along upper airfoil until meshPoint x value is reached
                {
                    j++;
                }
                while (dpMesh[k].XValue <= dpMesh[i].XValue)                // move along lower airfoil until meshPoint x value is reached
                {
                    k++;
                }

                m = 1;
                if (dpMesh[i].YValues[0] < dpMesh[j].YValues[0] && dpMesh[i].YValues[0] > dpMesh[k].YValues[0])  // check if point is inside of airfoil
                {
                    while (dpMesh[i + m * up].YValues[0] > dpMesh[k].YValues[0])                               // delete points if they are above lower suface
                    {
                        dpMesh[i + m * up].XValue = Double.NaN;
                        dpMesh[i + m * up].YValues[0] = Double.NaN;
                        m++;
                    }
                }
                else                                                                                            //if meshPoints are not inside of aifoil, move down until they are
                {
                    while (dpMesh[i + m * up].YValues[0] > dpMesh[j].YValues[0])
                    {
                        m++;
                    }

                    while (dpMesh[i + m * up].YValues[0] > dpMesh[k].YValues[0])                               // delete points if they are above lower suface
                    {
                        dpMesh[i + m * up].XValue = Double.NaN;
                        dpMesh[i + m * up].YValues[0] = Double.NaN;
                        m++;
                    }
                }
                i++;
            }

            if (cbGeometry.Text == "Cylinder")
			{
                i = startMesh + 1;
            }
            else
			{
                i = startMesh;
            }
            j = UstartAirfoil;
            k = LstartAirfoil;

            while (dpMesh[i].XValue <= dpMesh[dpMesh.Count - 1].XValue)     // delete middle mesh points as long as meshPoint x value is less than last point of airfoil x value
            {
                // delete points @ y = 0
                while (dpMesh[j].XValue <= dpMesh[i].XValue)                // move along upper airfoil until meshPoint x value is reached
                {
                    j++;
                }
                while (dpMesh[k].XValue <= dpMesh[i].XValue)                // move along lower airfoil until meshPoint x value is reached
                {
                    k++;
                }

                if (dpMesh[i].YValues[0] < dpMesh[j].YValues[0] && dpMesh[i].YValues[0] > dpMesh[k].YValues[0])  // check if point is inside of airfoil
                {
                    dpMesh[i].XValue = Double.NaN;
                    dpMesh[i].YValues[0] = Double.NaN;
                }
                i++;
            }

            //convert and delete surface points with double.Nan
            int n = UstartAirfoil;
            while (n < dpMesh.Count) 
            {
                /*
                dpMesh[n].XValue = Double.NaN;
                dpMesh[n].YValues[0] = Double.NaN;
                */
                dpMesh.Remove(dpMesh[n]);
            }

            // mark surface points
            if (meshScale < 1)
            {
                z = 0;
            }
            else
            {
                z = startMesh - (int)((1 / dx) * up);
            }

            double x, y;
            for (int l = z; l <= dpMesh.Count - 1; l++)
            {
                resx = Double.IsNaN(dpMesh[i].XValue);
                try
                {
                    if (resx == false && dpMesh[l].YValues[0] > (-A * D) + dx && dpMesh[l].YValues[0] < (A * D) - dx && (Double.IsNaN(dpMesh[l + up].XValue) == true || Double.IsNaN(dpMesh[l - up].XValue) == true || Double.IsNaN(dpMesh[l + 1].XValue) == true || Double.IsNaN(dpMesh[l - 1].XValue) == true))
                    {
                        x = dpMesh[l].XValue;
                        y = dpMesh[l].YValues[0];
                        dstPike.Tables[7].Rows.Add(l, x, y);
                    }
                }
                catch
                {
                }

            }
        }

        private void drawMesh()
        {
            //meshChart.Series[0].Points.Clear();
            foreach (DataRow dr in dstPike.Tables[0].Rows)
            {
                double dr1 = Convert.ToDouble(dr[1]);
                double dr2 = Convert.ToDouble(dr[2]);
                meshChart.Series[0].Points.AddXY(dr1, dr2);
            }
            meshChart.ChartAreas[0].AxisX.Minimum = xmin - obx / 10;
            meshChart.ChartAreas[0].AxisY.Minimum = ymin - oby / 10;
            meshChart.ChartAreas[0].AxisX.Maximum = xmax + obx / 10;
            meshChart.ChartAreas[0].AxisY.Maximum = ymax + oby / 10;

            foreach (DataRow dr in dstPike.Tables[3].Rows)
			{
                double dr0 = Convert.ToDouble(dr[0]);
                double dr1 = Convert.ToDouble(dr[1]);
                meshChart.Series[2].Points.AddXY(dr0, dr1);
            }

            foreach (DataRow dr in dstPike.Tables[4].Rows)
            {
                double dr0 = Convert.ToDouble(dr[0]);
                double dr1 = Convert.ToDouble(dr[1]);
                meshChart.Series[2].Points.AddXY(dr0, dr1);
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Are you sure you want to clear current mesh?", "Clear Mesh", MessageBoxButtons.YesNo);

            if (result == DialogResult.Yes)
            {
                rtbInfo.Clear();

                meshChart.Series[0].Points.Clear();
                meshChart.Series[1].Points.Clear();
                meshChart.Series[2].Points.Clear();
                meshChart.Series[3].Points.Clear();

                dgvPoints.DataSource = null;
                dgvPoints.Rows.Clear();

                dstPike.Clear();
            }
        }

        private void CreateConnectivity()
        {
            double dx = Convert.ToDouble(tbDx.Text);
            double dy = dx;
            int f, b, u, d;
            int up = (int)((A * D + B * D + D) / dx) + 1;
            int p;

            string BC;
            int j = 0;
            foreach (DataRow dr in dstPike.Tables[5].Rows)
            {
                BC = dr[3].ToString();
                p = Convert.ToInt32(dr[0]);

                if(BC == "domain dirichlet" || BC == "domain neumann" || p == -1)
				{
                    j++;
                    continue;
				}
                //connectivity
                f = Convert.ToInt32(dstPike.Tables[5].Rows[j + 1][0]); //front neighbor
                b = Convert.ToInt32(dstPike.Tables[5].Rows[j - 1][0]);  //back neighbor
                u = Convert.ToInt32(dstPike.Tables[5].Rows[j - up][0]); //upper neighbor
                d = Convert.ToInt32(dstPike.Tables[5].Rows[j + up][0]); //lower neighbor

                dstPike.Tables[6].Rows.Add(f, b, u, d);
                j++;
            }
            
        }

        private void btnExport_Click(object sender, EventArgs e)
		{
            if (cbGeometry.Text == "NACA Airfoil")
			{
                if (dgvPoints.Rows.Count > 0)
                {
                    SaveFileDialog sfd = new SaveFileDialog();

                    sfd.Filter = "TXT (*.txt)|*.txt";

                    sfd.FilterIndex = 1;

                    sfd.FileName = "NACA_" + Convert.ToString(tbNACAn_radius.Text) + "_" + Convert.ToString(tbAlpha.Text) + "deg" + "_" + "dx" + Convert.ToString(tbDx.Text);
                    string fileName = sfd.FileName.ToString();
                    //string pathString = Path.Combine(sfd.FileName, fileName + "_mesh");
                    bool fileError = false;
                    if (sfd.ShowDialog() == DialogResult.OK)
                    {
						if (File.Exists(sfd.FileName))
                        {
                            try
                            {
                                File.Delete(sfd.FileName);
                            }
                            catch (IOException ex)
                            {
                                fileError = true;
                                MessageBox.Show("It wasn't possible to write data to disk" + ex.Message);
                            }
                        }
                        if (!fileError)
                        {
                            try
                            {
                                // write mesh coordinates
                                int columnCount = dgvPoints.Columns.Count;
                                string columnNames = "";
                                string[] outputCsv = new string[dgvPoints.Rows.Count + 1 + 5 + dstPike.Tables[6].Rows.Count + 2];
                                for (int i = 0; i < columnCount; i++)
                                {
                                    columnNames += dgvPoints.Columns[i].HeaderText.ToString() + "\t";
                                }
                                
                                outputCsv[0] = "NACA airfoil: " + "\t\t\t" + tbNACAn_radius.Text;
                                outputCsv[1] = "Angle of attack alpha == " + "\t" + tbAlpha.Text + " deg";
                                outputCsv[2] = "Trailing edge: " + "\t\t\t" + cbTrailingEdge.Text;
                                outputCsv[3] = "Mesh step == " + "\t\t\t" + tbDx.Text;
                                outputCsv[4] = "Inlet velocity ==" + "\t\t" + tbU.Text;
                                
                                outputCsv[5] += columnNames;

                                for (int i = 1; i < dgvPoints.Rows.Count; i++)
                                {
                                    for (int j = 0; j < columnCount; j++)
                                    {
                                        outputCsv[i + 5] += dgvPoints.Rows[i - 1].Cells[j].Value.ToString() + "\t";
                                    }
                                }

                                // write connectivity
                                outputCsv[dgvPoints.Rows.Count + 5] = "Connectivity";
                                for (int i = 1; i < dstPike.Tables[6].Rows.Count + 1; i++)
								{
                                    outputCsv[dgvPoints.Rows.Count + 5 + i] = (i).ToString() + "\t";
                                    for (int j = 0; j < dstPike.Tables[6].Columns.Count ; j++)
									{
                                        outputCsv[dgvPoints.Rows.Count + 5 + i] += dstPike.Tables[6].Rows[i - 1][j].ToString() + "\t"; 
									}
								}

                                File.WriteAllLines(sfd.FileName, outputCsv, Encoding.UTF8);
                                rtbInfo.AppendText("\n");
                                rtbInfo.AppendText("\n");
                                rtbInfo.AppendText("Mesh Exported Successfully");
                            }
                            
                            catch (Exception ex)
                            {
                                MessageBox.Show("Error :" + ex.Message);
                            }
                            string getFullPath = Path.GetFullPath(sfd.FileName);
                            Directory.CreateDirectory(getFullPath + "_directory");
                            string pathDestination = Path.Combine(getFullPath + "_directory", fileName + "_MESH" + ".msh");
                            File.Copy(getFullPath, pathDestination, true);
                            File.Delete(getFullPath);


                            //string fileAsal = Path.Combine(fileName, getFullPath);

                            //FileInfo fi = new FileInfo(sfd.FileName);
                            //string nameFolder = fileName + "_directory";
                            //Directory.CreateDirectory(@fi + nameFolder);

                            //string path = Path.Combine(nameFolder, getFullPath);
                            //string pathDestination = Path.Combine(path, Path.GetFileName(sfd.FileName));
                            //File.Copy(getFullPath, pathDestination, true);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("No Record To Export", "Info");
                }
            }
            else if(cbGeometry.Text == "Cylinder")
			{
                if (dgvPoints.Rows.Count > 0)
                {
                    SaveFileDialog sfd = new SaveFileDialog();
                    sfd.Filter = "TXT (*.txt)|*.txt";
                    sfd.FileName = "Cylinder_" + Convert.ToString(tbNACAn_radius.Text) + "_" + "dx" + Convert.ToString(tbDx.Text);
                    string fileName = sfd.FileName.ToString();
                    bool fileError = false;
                    if (sfd.ShowDialog() == DialogResult.OK)
                    {
                        if (File.Exists(sfd.FileName))
                        {
                            try
                            {
                                File.Delete(sfd.FileName);
                            }
                            catch (IOException ex)
                            {
                                fileError = true;
                                MessageBox.Show("It wasn't possible to write the data to the disk" + ex.Message);
                            }
                        }
                        if (!fileError)
                        {
                            try
                            {
                                int columnCount = dgvPoints.Columns.Count;
                                string columnNames = "";
                                string[] outputCsv = new string[dgvPoints.Rows.Count + 1 + 5 + dstPike.Tables[6].Rows.Count + 2];
                                for (int i = 0; i < columnCount; i++)
                                {
                                    columnNames += dgvPoints.Columns[i].HeaderText.ToString() + "\t";
                                }
                                outputCsv[0] = "Cylinder: " + "\t" + tbNACAn_radius.Text;
                                outputCsv[1] = "Mesh step == " + "\t" + tbDx.Text;
                                outputCsv[2] = "Inlet velocity ==" + "\t\t" + tbU.Text;

                                outputCsv[5] += columnNames;

                                for (int i = 1; i < dgvPoints.Rows.Count; i++)
                                {
                                    for (int j = 0; j < columnCount; j++)
                                    {
                                        outputCsv[i + 5] += dgvPoints.Rows[i - 1].Cells[j].Value.ToString() + "\t";
                                    }
                                }

                                // write connectivity
                                outputCsv[dgvPoints.Rows.Count + 5] = "Connectivity";
                                for (int i = 1; i < dstPike.Tables[6].Rows.Count + 1; i++)
                                {
                                    outputCsv[dgvPoints.Rows.Count + 5 + i] = i.ToString() + "\t";
                                    for (int j = 0; j < dstPike.Tables[6].Columns.Count; j++)
                                    {
                                        outputCsv[dgvPoints.Rows.Count + 5 + i] += dstPike.Tables[6].Rows[i - 1][j].ToString() + "\t";
                                    }
                                }

                                File.WriteAllLines(sfd.FileName, outputCsv, Encoding.UTF8);
                                rtbInfo.AppendText("\n");
                                rtbInfo.AppendText("\n");
                                rtbInfo.AppendText("Mesh Exported Successfully");
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Error :" + ex.Message);
                            }
                        }
                        string getFullPath = Path.GetFullPath(sfd.FileName);
                        Directory.CreateDirectory(getFullPath + "_directory");
                        string pathDestination = Path.Combine(getFullPath + "_directory", fileName + "_MESH" + ".msh");
                        File.Copy(getFullPath, pathDestination, true);
                        File.Delete(getFullPath);
                    }
                }
                else
                {
                    MessageBox.Show("No Record To Export", "Info");
                }
            }
        }

        private void DeleteNanPoints()
		{
            for (int i = 0; i < dstPike.Tables[5].Rows.Count - 1; i++)
            {
                while (Double.IsNaN(Convert.ToDouble(dstPike.Tables[5].Rows[i][1])) == true)
                {
                    dstPike.Tables[5].Rows.RemoveAt(i);
                }
            }
            //renumber mesh points
            for (int i = 0; i < dstPike.Tables[5].Rows.Count - 1; i++)
            {
                dstPike.Tables[5].Rows[i][0] = i;
            }
        }

        private void btnApplyGeo_Click(object sender, EventArgs e)
        {
            int G = cbGeometry.SelectedIndex;

            if (G == 1)
            {
                gbNACAAirfoil_radius.Visible = true;
                gbNACAAirfoil_radius.Text = "Cylinder";
                lbNACANum.Text = "Radius";
                lbAlpha.Visible = false;
                lbTrailingEdge.Visible = false;
                lbM.Visible = true;
                lbChooseGeomerty.Visible = false;
                tbAlpha.Visible = false;
                cbTrailingEdge.Visible = false;
                label3.Visible = false;
                //tbSt.Visible = false;
                //lbSt.Visible = false;
            }
            else
            {
                gbNACAAirfoil_radius.Visible = true;
                gbNACAAirfoil_radius.Text = "NACA Airfoil";
                lbNACANum.Text = "NACA number";
                lbAlpha.Visible = true;
                lbTrailingEdge.Visible = true;
                lbM.Visible = false;
                lbChooseGeomerty.Visible = false;
                tbAlpha.Visible = true;
                cbTrailingEdge.Visible = true;
                label3.Visible = true;
                //tbSt.Visible = true;
                //lbSt.Visible = true;
            }
        }

		private void lbShowSurface_Click(object sender, EventArgs e) // show or hide geometry surface points
		{
            if (lbShowSurface.Text == "Hide Geometry Surface")
			{
                meshChart.Series[2].Enabled = false;
                lbShowSurface.Text = "Show Geometry Surface";
            }
            else
            {
                meshChart.Series[2].Enabled = true;
                lbShowSurface.Text = "Hide Geometry Surface";
            }
        }

        /*
		private void lbA_MouseHover(object sender, EventArgs e)
		{
            ToolTip tt = new ToolTip();
            tt.SetToolTip(lbA, "Domain size ahead of trailing edge (default value = 1)");
        }

		private void lbB_MouseHover(object sender, EventArgs e)
		{
            ToolTip tt = new ToolTip();
            tt.SetToolTip(lbB, "Domain size behind trailing edge (default value = 2)");
        }

		private void lbC_MouseHover(object sender, EventArgs e)
		{
            ToolTip tt = new ToolTip();
            tt.SetToolTip(lbC, "Domain size above/beneath trailing edge (default value = 1)");
        }
        */

        public static int[] NACAseries(int NACAn)
        {
            string stringNACAn = NACAn.ToString("0000");
            char[] charNACAn;
            charNACAn = stringNACAn.ToCharArray(0, 4);
            int[] arrNACAn = Array.ConvertAll(charNACAn, c => (int)char.GetNumericValue(c));

            return arrNACAn;
        }


		public void cylinderKoordinate()
		{
            double r = Convert.ToDouble(tbNACAn_radius.Text);
            double st = Convert.ToDouble(tbDx.Text) / 100;
            double xu = 0;
            double yu;
            int p = dstPike.Tables[0].Rows.Count;

            // calculate upper cylinder surface
            while (xu <= (2 * r))
			{
                yu = Math.Sqrt((2 * r * xu) - xu * xu);
                dstPike.Tables[3].Rows.Add(xu, yu);
                dstPike.Tables[0].Rows.Add(p, xu, yu);
                xu = xu + st;
			}
            changeLine(rtbInfo, 6, "50%");

            //calculate lower cylinder surface
            double xl = 0;
            double yl;
            while (xl <= (2 * r))
			{
                yl = -Math.Sqrt((2 * r * xl) - xl * xl);
                dstPike.Tables[4].Rows.Add(xl, yl);
                dstPike.Tables[0].Rows.Add(p, xl, yl);
                xl = xl + st;
            }
            changeLine(rtbInfo, 6, "100%");
        }

        public void NACAkoordinate()
        {
            //NACA airfoil surface coordiantes
            int[] arrNACA = NACAseries(Convert.ToInt32(tbNACAn_radius.Text));
            int N1 = arrNACA[0];
            int N2 = arrNACA[1];
            int N3 = arrNACA[2];
            int N4 = arrNACA[3];
            int N34 = int.Parse(N3.ToString() + N4.ToString());
            //double st = Convert.ToDouble(tbSt.Text);
            double st = Convert.ToDouble(tbDx.Text)/100;

            double M = (double)N1 / 100;
            double P = (double)N2 / 10;
            double T = (double)N34 / 100;

            string percentageString;

            //camberline, gradient and theta
            double xc = 0;
            double yc = 0;
            double dycdx, theta;
            while (xc < P)
            {
                yc = ((M / (P * P)) * ((2 * P * xc) - (xc * xc)));
                dycdx = ((2 * M) / (P * P)) * (P - xc);
                theta = Math.Atan(dycdx);

                dstPike.Tables[1].Rows.Add(xc, yc, dycdx, theta);
                
                xc = xc + st;
            }
            percentageString = "20%";
            changeLine(rtbInfo, 8, percentageString);

            while (xc < 1)
            {
                yc = (M / ((1 - P) * (1 - P))) * ((1 - (2 * P)) + (2 * P * xc) - (xc * xc));
                dycdx = ((2 * M) / ((1 - P) * (1 - P))) * (P - xc);
                theta = Math.Atan(dycdx);

                dstPike.Tables[1].Rows.Add(xc, yc, dycdx, theta);

                xc = xc + st;
            }

            percentageString = "40%";
            changeLine(rtbInfo, 8, percentageString);

            //thickness distribution
            double a0 = 0.2969;
            double a1 = -0.1260;
            double a2 = -0.3516;
            double a3 = 0.2843;
            double a4 = 0;

            if (cbTrailingEdge.Text == "open")
            {
                a4 = -0.1015;
            }
            else if (cbTrailingEdge.Text == "close")
            {
                a4 = -0.1036;
            }

            double xt = 0;
            double yt = 0;
            double term0, term1, term2, term3, term4;
            while (xt < 1)
            {
                term0 = a0 * Math.Sqrt(xt);
                term1 = a1 * xt;
                term2 = a2 * xt * xt;
                term3 = a3 * xt * xt * xt;
                term4 = a4 * xt * xt * xt * xt;

                yt = 5 * T * (term0 + term1 + term2 + term3 + term4);
                dstPike.Tables[2].Rows.Add(xt, yt);

                xt = xt + st;
            }

            changeLine(rtbInfo, 8, "50%");

            int p = dstPike.Tables[0].Rows.Count;

            // upper surface points
            double xu = 0;
            double yu = 0;
            double xcFromTable = 0;
            double ycFromTable;
            double ytFromTable;
            double thetaFromTable;
            double xSaved, ySaved;
            double alphaRadian = (Math.PI / 180) * Convert.ToDouble(tbAlpha.Text);
            //double center = P / 2;
            int i = 0;
            foreach (DataRow dr in dstPike.Tables[1].Rows)
            {
                p++;
                ycFromTable = Convert.ToDouble(dstPike.dtCamber.Rows[i][1]);
                ytFromTable = Convert.ToDouble(dstPike.dtThickness.Rows[i][1]);
                thetaFromTable = Convert.ToDouble(dstPike.dtCamber.Rows[i][3]);

                xu = (xcFromTable - (ytFromTable * Math.Sin(thetaFromTable)));
                yu = (ycFromTable + (ytFromTable * Math.Cos(thetaFromTable)));

                xSaved = xu;
                ySaved = yu;

                //rotation
                xu = (Math.Cos(alphaRadian) * xu) - (Math.Sin(alphaRadian) * yu);
                yu = (Math.Sin(alphaRadian) * xSaved) + (Math.Cos(alphaRadian) * ySaved);
                
                dstPike.Tables[3].Rows.Add(xu, yu); //write NACA upper surface coordinates to new dataTable
                dstPike.Tables[0].Rows.Add(p, xu, yu); //write NACA upper surface coordinates to dtPike

                xcFromTable = xcFromTable + st;
                i++;
            }

            percentageString = "75%";
            changeLine(rtbInfo, 8, percentageString);

            //lower surface points
            double xl = 0;
            double yl = 0;
            xcFromTable = 0;
            int j = 0;
            foreach (DataRow dr in dstPike.Tables[1].Rows)
            {
                p++;
                ycFromTable = Convert.ToDouble(dstPike.dtCamber.Rows[j][1]);
                ytFromTable = Convert.ToDouble(dstPike.dtThickness.Rows[j][1]);
                thetaFromTable = Convert.ToDouble(dstPike.dtCamber.Rows[j][3]);

                xl = (xcFromTable + (ytFromTable * Math.Sin(thetaFromTable)));
                yl = (ycFromTable - (ytFromTable * Math.Cos(thetaFromTable)));

                xSaved = xl;
                ySaved = yl;

                //rotation
                xl = (Math.Cos(alphaRadian) * xl) - (Math.Sin(alphaRadian) * yl);
                yl = (Math.Sin(alphaRadian) * xSaved) + (Math.Cos(alphaRadian) * ySaved);

                dstPike.Tables[4].Rows.Add(xl, yl);     //write NACA lower surface coordinates to new dataTable
                dstPike.Tables[0].Rows.Add(p, xl, yl);  //write NACA lower surface coordinates to dtPike

                xcFromTable = xcFromTable + st;
                j++;
            }

            percentageString = "100%";
            changeLine(rtbInfo, 8, percentageString);

        }

        private void drawAirfoil()
        {
            /*
            //draw camber line
            foreach (DataRow dr in dstPike.Tables[1].Rows)
            {
                double dr0 = Convert.ToDouble(dr[0]);
                double dr1 = Convert.ToDouble(dr[1]);
                meshChart.Series[1].Points.AddXY(dr0, dr1);
            }
            */
            /* //draw thickness
            foreach (DataRow dr in dstPike.Tables[2].Rows)
            {
                double dr0 = Convert.ToDouble(dr[0]);
                double dr1 = Convert.ToDouble(dr[1]);
                meshChart.Series[2].Points.AddXY(dr0, dr1);
            }
            */

            // draw upper surface
            foreach (DataRow dr in dstPike.Tables[3].Rows)
            {
                double dr0 = Convert.ToDouble(dr[0]);
                double dr1 = Convert.ToDouble(dr[1]);
                meshChart.Series[2].Points.AddXY(dr0, dr1);
            }

            // draw lower surface
            foreach (DataRow dr in dstPike.Tables[4].Rows)
            {
                double dr0 = Convert.ToDouble(dr[0]);
                double dr1 = Convert.ToDouble(dr[1]);
                meshChart.Series[3].Points.AddXY(dr0, dr1);
            }
        }

        private void CheckInputData()
		{
            double NACAn = Convert.ToDouble(tbNACAn_radius.Text);
            string te = cbTrailingEdge.Text;
            bool restart = false;

            if (cbGeometry.Text == "NACA Airfoil")
			{
                if (NACAn > 9999 | NACAn < 0001)
                {
                    tbNACAn_radius.BackColor = Color.Red;
                    tbNACAn_radius.ForeColor = Color.White;
                    MessageBox.Show("Invalid NACA series of digits!");
                    restart = true;
                }

                if (te == "")
                {
                    cbTrailingEdge.BackColor = Color.Red;
                    cbTrailingEdge.ForeColor = Color.White;
                    MessageBox.Show("Trailing edge undefined!");
                    restart = true;
                }

                if (tbMeshScale.Text == "0")
                {
                    tbMeshScale.BackColor = Color.Red;
                    tbMeshScale.ForeColor = Color.White;
                    MessageBox.Show("Mesh scale factor must be greater than 0!");
                    restart = true;
                }

            }

            if (cbGeometry.Text == "Cylinder")
			{
                if (NACAn <= 0)
				{
                    tbNACAn_radius.BackColor = Color.Red;
                    tbNACAn_radius.ForeColor = Color.White;
                    MessageBox.Show("Invalid cylinder radius!");
                    restart = true;
                }
            }

            if (restart == true)
            {
                Application.Restart();
            }

        }

        async void btRun_Click(object sender, EventArgs e)
        {
            try
            {
                CheckInputData();

                if (cbGeometry.Text == "NACA Airfoil")
                {
                    if (dgvPoints.Rows.Count > 1)
					{
                        btnClear.PerformClick();
                    }

                    rtbInfo.AppendText("Points loaded successfully_");
                    rtbInfo.AppendText("\n");
                    rtbInfo.AppendText("\n");

                    rtbInfo.AppendText("MESH GENERATION STARTED");
                    rtbInfo.AppendText("\n");
                    rtbInfo.AppendText("Calculating mesh points...");
                    rtbInfo.AppendText("\n");
                    rtbInfo.AppendText("0%");

                    stopWatch.Reset();
                    stopWatch.Start();

                    await Task.Delay(100);

                    calculateMesh(); // calculate points of baseline mesh

                    await Task.Delay(100);
                    rtbInfo.AppendText("\n");
                    rtbInfo.AppendText("(Done_)");
                    rtbInfo.AppendText("\n");
                    rtbInfo.AppendText("\n");
                    rtbInfo.AppendText("Creating airfoil surface...");
                    rtbInfo.AppendText("\n");
                    rtbInfo.AppendText("0%");

                    await Task.Delay(100);

                    NACAkoordinate();                           // calculate points of NACA airfoil surface and write them to: 1.) new dataTable, 2.) dtPike

                    await Task.Delay(100);
                    rtbInfo.AppendText("\n");
                    rtbInfo.AppendText("(Done_)");
                    rtbInfo.AppendText("\n");
                    rtbInfo.AppendText("\n");
                    rtbInfo.AppendText("Drawing mesh...");
                    await Task.Delay(100);

                    drawMesh();                                 // draw combined points to chart

                    await Task.Delay(100);
                    rtbInfo.AppendText("\n");
                    rtbInfo.AppendText("(Done_)");
                    rtbInfo.AppendText("\n");
                    rtbInfo.AppendText("\n");
                    rtbInfo.AppendText("Finishing up...");
                    await Task.Delay(100);

                    DeleteInnerPoints1();                       // delete points inside of airfoil
                    SetDGV();
                    CreateConnectivity();
                    CreateNormalVectors();

                    await Task.Delay(100);
                    rtbInfo.AppendText("\n");
                    rtbInfo.AppendText("(Done_)");
                    rtbInfo.AppendText("\n");
                    rtbInfo.AppendText("\n");
                    rtbInfo.AppendText("MESH GENERATED SUCCESSFULLY");
                    rtbInfo.AppendText("\n");
                    rtbInfo.AppendText("Total number of nodes: " + Convert.ToString(dstPike.Tables[5].Rows.Count));
                    await Task.Delay(100);

                    lbShowSurface.Visible = true;
                    stopWatch.Stop();
                    TimeSpan ts = stopWatch.Elapsed;
                    rtbInfo.AppendText("\n");
                    rtbInfo.AppendText("Elapsed time: " + ts);
                }
                else if (cbGeometry.Text == "Cylinder")
                {
                    if (dgvPoints.Rows.Count > 1)
                    {
                        btnClear.PerformClick();
                    }

                    rtbInfo.AppendText("Points loaded successfully_");
                    rtbInfo.AppendText("\n");
                    rtbInfo.AppendText("\n");

                    rtbInfo.AppendText("MESH GENERATION STARTED");
                    rtbInfo.AppendText("\n");
                    rtbInfo.AppendText("Calculating mesh points...");
                    rtbInfo.AppendText("\n");
                    rtbInfo.AppendText("0%");

                    stopWatch.Reset();
                    stopWatch.Start();

                    await Task.Delay(100);

                    calculateMesh(); // calculate points of baseline mesh

                    await Task.Delay(100);
                    rtbInfo.AppendText("\n");
                    rtbInfo.AppendText("(Done_)");
                    rtbInfo.AppendText("\n");
                    rtbInfo.AppendText("\n");
                    rtbInfo.AppendText("Creating cylinder surface...");
                    rtbInfo.AppendText("\n");
                    rtbInfo.AppendText("0%");

                    await Task.Delay(100);

                    cylinderKoordinate();

                    await Task.Delay(100);
                    rtbInfo.AppendText("\n");
                    rtbInfo.AppendText("(Done_)");
                    rtbInfo.AppendText("\n");
                    rtbInfo.AppendText("\n");
                    rtbInfo.AppendText("Drawing mesh...");
                    await Task.Delay(100);

                    drawMesh();                                 // draw combined points to chart

                    await Task.Delay(100);
                    rtbInfo.AppendText("\n");
                    rtbInfo.AppendText("(Done_)");
                    rtbInfo.AppendText("\n");
                    rtbInfo.AppendText("\n");
                    rtbInfo.AppendText("Finishing up...");
                    await Task.Delay(100);

                    DeleteInnerPoints1();                       // delete points inside of cylinder
                    SetDGV();
                    CreateConnectivity();
                    CreateNormalVectors();

                    await Task.Delay(100);
                    rtbInfo.AppendText("\n");
                    rtbInfo.AppendText("(Done_)");
                    rtbInfo.AppendText("\n");
                    rtbInfo.AppendText("\n");
                    rtbInfo.AppendText("MESH GENERATED SUCCESSFULLY");
                    rtbInfo.AppendText("\n");
                    rtbInfo.AppendText("Total number of nodes: " + Convert.ToString(dstPike.Tables[5].Rows.Count));
                    await Task.Delay(100);

                    lbShowSurface.Visible = true;
                    stopWatch.Stop();
                    TimeSpan ts = stopWatch.Elapsed;
                    rtbInfo.AppendText("\n");
                    rtbInfo.AppendText("Elapsed time: " + ts);
                }
		}
            catch (Exception ex)
            {
                MessageBox.Show("Error: unable to create mesh with such parameters, try changing mesh step");
                MessageBox.Show(ex.Message);
                Application.Restart();
            }

        }
        void changeLine(RichTextBox rtbInfo, int line, string text)
        {
            //await Task.Delay(10);
            int s1 = rtbInfo.GetFirstCharIndexFromLine(line);
            int s2 = line < rtbInfo.Lines.Count() - 1 ?
                      rtbInfo.GetFirstCharIndexFromLine(line + 1) - 1 :
                      rtbInfo.Text.Length;
            rtbInfo.Select(s1, s2 - s1);
            rtbInfo.SelectedText = text;
        }
    }
}
