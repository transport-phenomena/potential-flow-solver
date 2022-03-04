
namespace potFlow_Meshing_v0._3
{
    partial class Form3
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.airfoilChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.dstPike1 = new potFlow_Meshing_v0._3.dstPike();
            ((System.ComponentModel.ISupportInitialize)(this.airfoilChart)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dstPike1)).BeginInit();
            this.SuspendLayout();
            // 
            // airfoilChart
            // 
            chartArea2.Name = "ChartArea1";
            this.airfoilChart.ChartAreas.Add(chartArea2);
            this.airfoilChart.Dock = System.Windows.Forms.DockStyle.Fill;
            legend2.Name = "Legend1";
            this.airfoilChart.Legends.Add(legend2);
            this.airfoilChart.Location = new System.Drawing.Point(0, 0);
            this.airfoilChart.Name = "airfoilChart";
            series2.ChartArea = "ChartArea1";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;
            series2.Legend = "Legend1";
            series2.Name = "Series1";
            this.airfoilChart.Series.Add(series2);
            this.airfoilChart.Size = new System.Drawing.Size(800, 450);
            this.airfoilChart.TabIndex = 0;
            this.airfoilChart.Text = "chart1";
            // 
            // dstPike1
            // 
            this.dstPike1.DataSetName = "dstPike";
            this.dstPike1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // Form3
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.airfoilChart);
            this.Name = "Form3";
            this.Text = "Form3";
            ((System.ComponentModel.ISupportInitialize)(this.airfoilChart)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dstPike1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart airfoilChart;
        private dstPike dstPike1;
    }
}