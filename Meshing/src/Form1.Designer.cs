
namespace potFlow_Meshing_v0._3
{
    partial class Form1
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
			this.components = new System.ComponentModel.Container();
			System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
			System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
			System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
			System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
			System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
			System.Windows.Forms.DataVisualization.Charting.Series series4 = new System.Windows.Forms.DataVisualization.Charting.Series();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
			this.dsPoints = new System.Data.DataSet();
			this.dtPoints = new System.Data.DataTable();
			this.dataColumn1 = new System.Data.DataColumn();
			this.dataColumn2 = new System.Data.DataColumn();
			this.dataColumn3 = new System.Data.DataColumn();
			this.dtPikeBindingSource = new System.Windows.Forms.BindingSource(this.components);
			this.dstPike = new potFlow_Meshing_v0._3.dstPike();
			this.pDataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.xDataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.yDataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.pDataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.xDataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.yDataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.splitContainer2 = new System.Windows.Forms.SplitContainer();
			this.dgvPoints = new System.Windows.Forms.DataGridView();
			this.pDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.xDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.yDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.lbShowSurface = new System.Windows.Forms.Label();
			this.meshChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.btRun = new System.Windows.Forms.Button();
			this.rtbInfo = new System.Windows.Forms.RichTextBox();
			this.splitContainer3 = new System.Windows.Forms.SplitContainer();
			this.btnExport = new System.Windows.Forms.Button();
			this.btnClear = new System.Windows.Forms.Button();
			this.panel1 = new System.Windows.Forms.Panel();
			this.gbNumericalParameters = new System.Windows.Forms.GroupBox();
			this.tbMeshScale = new System.Windows.Forms.ComboBox();
			this.label6 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.tbDx = new System.Windows.Forms.TextBox();
			this.gbNACAAirfoil_radius = new System.Windows.Forms.GroupBox();
			this.lbChInfo = new System.Windows.Forms.Label();
			this.tbRe = new System.Windows.Forms.TextBox();
			this.lbRe = new System.Windows.Forms.Label();
			this.tbU = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.lbM = new System.Windows.Forms.Label();
			this.lbNACANum = new System.Windows.Forms.Label();
			this.cbTrailingEdge = new System.Windows.Forms.ComboBox();
			this.tbNACAn_radius = new System.Windows.Forms.TextBox();
			this.lbTrailingEdge = new System.Windows.Forms.Label();
			this.tbAlpha = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.lbAlpha = new System.Windows.Forms.Label();
			this.groupBox4 = new System.Windows.Forms.GroupBox();
			this.btnApplyGeo = new System.Windows.Forms.Button();
			this.cbGeometry = new System.Windows.Forms.ComboBox();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			((System.ComponentModel.ISupportInitialize)(this.dsPoints)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dtPoints)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dtPikeBindingSource)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dstPike)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
			this.splitContainer2.Panel1.SuspendLayout();
			this.splitContainer2.Panel2.SuspendLayout();
			this.splitContainer2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dgvPoints)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.meshChart)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).BeginInit();
			this.splitContainer3.Panel1.SuspendLayout();
			this.splitContainer3.Panel2.SuspendLayout();
			this.splitContainer3.SuspendLayout();
			this.panel1.SuspendLayout();
			this.gbNumericalParameters.SuspendLayout();
			this.gbNACAAirfoil_radius.SuspendLayout();
			this.groupBox4.SuspendLayout();
			this.tableLayoutPanel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// dsPoints
			// 
			this.dsPoints.DataSetName = "MojePike";
			this.dsPoints.Tables.AddRange(new System.Data.DataTable[] {
            this.dtPoints});
			// 
			// dtPoints
			// 
			this.dtPoints.Columns.AddRange(new System.Data.DataColumn[] {
            this.dataColumn1,
            this.dataColumn2,
            this.dataColumn3});
			this.dtPoints.TableName = "Points";
			// 
			// dataColumn1
			// 
			this.dataColumn1.ColumnName = "p";
			this.dataColumn1.DataType = typeof(int);
			// 
			// dataColumn2
			// 
			this.dataColumn2.ColumnName = "x";
			this.dataColumn2.DataType = typeof(double);
			// 
			// dataColumn3
			// 
			this.dataColumn3.ColumnName = "y";
			this.dataColumn3.DataType = typeof(double);
			// 
			// dtPikeBindingSource
			// 
			this.dtPikeBindingSource.DataMember = "dtPike";
			this.dtPikeBindingSource.DataSource = this.dstPike;
			// 
			// dstPike
			// 
			this.dstPike.DataSetName = "dstPike";
			this.dstPike.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
			// 
			// pDataGridViewTextBoxColumn1
			// 
			this.pDataGridViewTextBoxColumn1.DataPropertyName = "p";
			this.pDataGridViewTextBoxColumn1.HeaderText = "p";
			this.pDataGridViewTextBoxColumn1.Name = "pDataGridViewTextBoxColumn1";
			// 
			// xDataGridViewTextBoxColumn1
			// 
			this.xDataGridViewTextBoxColumn1.DataPropertyName = "x";
			this.xDataGridViewTextBoxColumn1.HeaderText = "x";
			this.xDataGridViewTextBoxColumn1.Name = "xDataGridViewTextBoxColumn1";
			// 
			// yDataGridViewTextBoxColumn1
			// 
			this.yDataGridViewTextBoxColumn1.DataPropertyName = "y";
			this.yDataGridViewTextBoxColumn1.HeaderText = "y";
			this.yDataGridViewTextBoxColumn1.Name = "yDataGridViewTextBoxColumn1";
			// 
			// pDataGridViewTextBoxColumn2
			// 
			this.pDataGridViewTextBoxColumn2.DataPropertyName = "p";
			this.pDataGridViewTextBoxColumn2.HeaderText = "p";
			this.pDataGridViewTextBoxColumn2.Name = "pDataGridViewTextBoxColumn2";
			// 
			// xDataGridViewTextBoxColumn2
			// 
			this.xDataGridViewTextBoxColumn2.DataPropertyName = "x";
			this.xDataGridViewTextBoxColumn2.HeaderText = "x";
			this.xDataGridViewTextBoxColumn2.Name = "xDataGridViewTextBoxColumn2";
			// 
			// yDataGridViewTextBoxColumn2
			// 
			this.yDataGridViewTextBoxColumn2.DataPropertyName = "y";
			this.yDataGridViewTextBoxColumn2.HeaderText = "y";
			this.yDataGridViewTextBoxColumn2.Name = "yDataGridViewTextBoxColumn2";
			// 
			// splitContainer2
			// 
			this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer2.Location = new System.Drawing.Point(3, 3);
			this.splitContainer2.Name = "splitContainer2";
			// 
			// splitContainer2.Panel1
			// 
			this.splitContainer2.Panel1.Controls.Add(this.dgvPoints);
			// 
			// splitContainer2.Panel2
			// 
			this.splitContainer2.Panel2.Controls.Add(this.lbShowSurface);
			this.splitContainer2.Panel2.Controls.Add(this.meshChart);
			this.splitContainer2.Size = new System.Drawing.Size(1132, 494);
			this.splitContainer2.SplitterDistance = 121;
			this.splitContainer2.TabIndex = 4;
			// 
			// dgvPoints
			// 
			this.dgvPoints.AutoGenerateColumns = false;
			this.dgvPoints.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dgvPoints.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.pDataGridViewTextBoxColumn,
            this.xDataGridViewTextBoxColumn,
            this.yDataGridViewTextBoxColumn});
			this.dgvPoints.DataSource = this.dtPikeBindingSource;
			this.dgvPoints.Dock = System.Windows.Forms.DockStyle.Fill;
			this.dgvPoints.Location = new System.Drawing.Point(0, 0);
			this.dgvPoints.Margin = new System.Windows.Forms.Padding(2);
			this.dgvPoints.Name = "dgvPoints";
			this.dgvPoints.ReadOnly = true;
			this.dgvPoints.RowHeadersWidth = 51;
			this.dgvPoints.RowTemplate.Height = 24;
			this.dgvPoints.Size = new System.Drawing.Size(121, 494);
			this.dgvPoints.TabIndex = 9;
			// 
			// pDataGridViewTextBoxColumn
			// 
			this.pDataGridViewTextBoxColumn.DataPropertyName = "p";
			this.pDataGridViewTextBoxColumn.HeaderText = "p";
			this.pDataGridViewTextBoxColumn.Name = "pDataGridViewTextBoxColumn";
			this.pDataGridViewTextBoxColumn.ReadOnly = true;
			// 
			// xDataGridViewTextBoxColumn
			// 
			this.xDataGridViewTextBoxColumn.DataPropertyName = "x";
			this.xDataGridViewTextBoxColumn.HeaderText = "x";
			this.xDataGridViewTextBoxColumn.Name = "xDataGridViewTextBoxColumn";
			this.xDataGridViewTextBoxColumn.ReadOnly = true;
			// 
			// yDataGridViewTextBoxColumn
			// 
			this.yDataGridViewTextBoxColumn.DataPropertyName = "y";
			this.yDataGridViewTextBoxColumn.HeaderText = "y";
			this.yDataGridViewTextBoxColumn.Name = "yDataGridViewTextBoxColumn";
			this.yDataGridViewTextBoxColumn.ReadOnly = true;
			// 
			// lbShowSurface
			// 
			this.lbShowSurface.AutoSize = true;
			this.lbShowSurface.Location = new System.Drawing.Point(875, 479);
			this.lbShowSurface.Name = "lbShowSurface";
			this.lbShowSurface.Size = new System.Drawing.Size(125, 13);
			this.lbShowSurface.TabIndex = 1;
			this.lbShowSurface.Text = "Show Geometry  Surface";
			this.lbShowSurface.Click += new System.EventHandler(this.lbShowSurface_Click);
			// 
			// meshChart
			// 
			chartArea1.AxisX.Enabled = System.Windows.Forms.DataVisualization.Charting.AxisEnabled.False;
			chartArea1.AxisX.IntervalType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Number;
			chartArea1.AxisY.Enabled = System.Windows.Forms.DataVisualization.Charting.AxisEnabled.False;
			chartArea1.AxisY.IntervalType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Number;
			chartArea1.BackImageWrapMode = System.Windows.Forms.DataVisualization.Charting.ChartImageWrapMode.Unscaled;
			chartArea1.Name = "ChartArea1";
			this.meshChart.ChartAreas.Add(chartArea1);
			legend1.Name = "Legend1";
			this.meshChart.Legends.Add(legend1);
			this.meshChart.Location = new System.Drawing.Point(0, 0);
			this.meshChart.Margin = new System.Windows.Forms.Padding(0);
			this.meshChart.Name = "meshChart";
			series1.ChartArea = "ChartArea1";
			series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastPoint;
			series1.Legend = "Legend1";
			series1.MarkerSize = 2;
			series1.Name = "Series1";
			series2.ChartArea = "ChartArea1";
			series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastPoint;
			series2.Legend = "Legend1";
			series2.MarkerSize = 2;
			series2.Name = "Series2";
			series3.ChartArea = "ChartArea1";
			series3.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;
			series3.Legend = "Legend1";
			series3.Name = "Series3";
			series4.ChartArea = "ChartArea1";
			series4.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
			series4.Legend = "Legend1";
			series4.Name = "Series4";
			this.meshChart.Series.Add(series1);
			this.meshChart.Series.Add(series2);
			this.meshChart.Series.Add(series3);
			this.meshChart.Series.Add(series4);
			this.meshChart.Size = new System.Drawing.Size(1000, 492);
			this.meshChart.TabIndex = 0;
			this.meshChart.Text = "chart1";
			// 
			// splitContainer1
			// 
			this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Top;
			this.splitContainer1.Location = new System.Drawing.Point(3, 503);
			this.splitContainer1.Name = "splitContainer1";
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this.btRun);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.rtbInfo);
			this.splitContainer1.Panel2.Controls.Add(this.splitContainer3);
			this.splitContainer1.Size = new System.Drawing.Size(1132, 150);
			this.splitContainer1.SplitterDistance = 104;
			this.splitContainer1.TabIndex = 3;
			// 
			// btRun
			// 
			this.btRun.Dock = System.Windows.Forms.DockStyle.Fill;
			this.btRun.Location = new System.Drawing.Point(0, 0);
			this.btRun.Name = "btRun";
			this.btRun.Size = new System.Drawing.Size(104, 150);
			this.btRun.TabIndex = 12;
			this.btRun.Text = "Generate\r\nMesh";
			this.btRun.UseVisualStyleBackColor = true;
			this.btRun.Click += new System.EventHandler(this.btRun_Click);
			// 
			// rtbInfo
			// 
			this.rtbInfo.Dock = System.Windows.Forms.DockStyle.Fill;
			this.rtbInfo.Font = new System.Drawing.Font("NSimSun", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.rtbInfo.Location = new System.Drawing.Point(85, 0);
			this.rtbInfo.Name = "rtbInfo";
			this.rtbInfo.Size = new System.Drawing.Size(939, 150);
			this.rtbInfo.TabIndex = 1;
			this.rtbInfo.Text = "";
			this.rtbInfo.TextChanged += new System.EventHandler(this.rtbInfo_TextChanged);
			// 
			// splitContainer3
			// 
			this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Left;
			this.splitContainer3.IsSplitterFixed = true;
			this.splitContainer3.Location = new System.Drawing.Point(0, 0);
			this.splitContainer3.Name = "splitContainer3";
			this.splitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// splitContainer3.Panel1
			// 
			this.splitContainer3.Panel1.Controls.Add(this.btnExport);
			// 
			// splitContainer3.Panel2
			// 
			this.splitContainer3.Panel2.Controls.Add(this.btnClear);
			this.splitContainer3.Size = new System.Drawing.Size(85, 150);
			this.splitContainer3.SplitterDistance = 81;
			this.splitContainer3.TabIndex = 0;
			// 
			// btnExport
			// 
			this.btnExport.Dock = System.Windows.Forms.DockStyle.Fill;
			this.btnExport.Location = new System.Drawing.Point(0, 0);
			this.btnExport.Name = "btnExport";
			this.btnExport.Size = new System.Drawing.Size(85, 81);
			this.btnExport.TabIndex = 0;
			this.btnExport.Text = "Export \r\nMesh\r\n to Folder";
			this.btnExport.UseVisualStyleBackColor = true;
			this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
			// 
			// btnClear
			// 
			this.btnClear.Dock = System.Windows.Forms.DockStyle.Fill;
			this.btnClear.Location = new System.Drawing.Point(0, 0);
			this.btnClear.Name = "btnClear";
			this.btnClear.Size = new System.Drawing.Size(85, 65);
			this.btnClear.TabIndex = 12;
			this.btnClear.Text = "Clear\r\nMesh";
			this.btnClear.UseVisualStyleBackColor = true;
			this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.gbNumericalParameters);
			this.panel1.Controls.Add(this.gbNACAAirfoil_radius);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
			this.panel1.Location = new System.Drawing.Point(1141, 3);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(470, 494);
			this.panel1.TabIndex = 0;
			// 
			// gbNumericalParameters
			// 
			this.gbNumericalParameters.Controls.Add(this.tbMeshScale);
			this.gbNumericalParameters.Controls.Add(this.label6);
			this.gbNumericalParameters.Controls.Add(this.label1);
			this.gbNumericalParameters.Controls.Add(this.label2);
			this.gbNumericalParameters.Controls.Add(this.tbDx);
			this.gbNumericalParameters.Dock = System.Windows.Forms.DockStyle.Top;
			this.gbNumericalParameters.Location = new System.Drawing.Point(0, 222);
			this.gbNumericalParameters.Name = "gbNumericalParameters";
			this.gbNumericalParameters.Size = new System.Drawing.Size(470, 93);
			this.gbNumericalParameters.TabIndex = 11;
			this.gbNumericalParameters.TabStop = false;
			this.gbNumericalParameters.Text = "Numerical Parameters";
			// 
			// tbMeshScale
			// 
			this.tbMeshScale.FormattingEnabled = true;
			this.tbMeshScale.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5"});
			this.tbMeshScale.Location = new System.Drawing.Point(99, 56);
			this.tbMeshScale.Name = "tbMeshScale";
			this.tbMeshScale.Size = new System.Drawing.Size(43, 21);
			this.tbMeshScale.Sorted = true;
			this.tbMeshScale.TabIndex = 14;
			this.tbMeshScale.Text = "2";
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(148, 30);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(15, 13);
			this.label6.TabIndex = 13;
			this.label6.Text = "m";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(3, 56);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(70, 13);
			this.label1.TabIndex = 3;
			this.label1.Text = "Mesh extents";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(3, 30);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(93, 13);
			this.label2.TabIndex = 1;
			this.label2.Text = "Mesh step (dx=dy)";
			// 
			// tbDx
			// 
			this.tbDx.Location = new System.Drawing.Point(99, 27);
			this.tbDx.Name = "tbDx";
			this.tbDx.Size = new System.Drawing.Size(43, 20);
			this.tbDx.TabIndex = 0;
			this.tbDx.Text = "0,01";
			this.tbDx.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbDx_KeyPress);
			// 
			// gbNACAAirfoil_radius
			// 
			this.gbNACAAirfoil_radius.Controls.Add(this.lbChInfo);
			this.gbNACAAirfoil_radius.Controls.Add(this.tbRe);
			this.gbNACAAirfoil_radius.Controls.Add(this.lbRe);
			this.gbNACAAirfoil_radius.Controls.Add(this.tbU);
			this.gbNACAAirfoil_radius.Controls.Add(this.label4);
			this.gbNACAAirfoil_radius.Controls.Add(this.label5);
			this.gbNACAAirfoil_radius.Controls.Add(this.lbM);
			this.gbNACAAirfoil_radius.Controls.Add(this.lbNACANum);
			this.gbNACAAirfoil_radius.Controls.Add(this.cbTrailingEdge);
			this.gbNACAAirfoil_radius.Controls.Add(this.tbNACAn_radius);
			this.gbNACAAirfoil_radius.Controls.Add(this.lbTrailingEdge);
			this.gbNACAAirfoil_radius.Controls.Add(this.tbAlpha);
			this.gbNACAAirfoil_radius.Controls.Add(this.label3);
			this.gbNACAAirfoil_radius.Controls.Add(this.lbAlpha);
			this.gbNACAAirfoil_radius.Dock = System.Windows.Forms.DockStyle.Top;
			this.gbNACAAirfoil_radius.Location = new System.Drawing.Point(0, 0);
			this.gbNACAAirfoil_radius.Name = "gbNACAAirfoil_radius";
			this.gbNACAAirfoil_radius.Size = new System.Drawing.Size(470, 222);
			this.gbNACAAirfoil_radius.TabIndex = 9;
			this.gbNACAAirfoil_radius.TabStop = false;
			this.gbNACAAirfoil_radius.Text = "NACA Airfoil";
			// 
			// lbChInfo
			// 
			this.lbChInfo.AutoSize = true;
			this.lbChInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lbChInfo.Location = new System.Drawing.Point(3, 187);
			this.lbChInfo.Name = "lbChInfo";
			this.lbChInfo.Size = new System.Drawing.Size(114, 13);
			this.lbChInfo.TabIndex = 19;
			this.lbChInfo.Text = "Chord length = 1 m";
			// 
			// tbRe
			// 
			this.tbRe.Location = new System.Drawing.Point(99, 158);
			this.tbRe.Name = "tbRe";
			this.tbRe.ReadOnly = true;
			this.tbRe.Size = new System.Drawing.Size(64, 20);
			this.tbRe.TabIndex = 16;
			// 
			// lbRe
			// 
			this.lbRe.AutoSize = true;
			this.lbRe.Location = new System.Drawing.Point(3, 161);
			this.lbRe.Name = "lbRe";
			this.lbRe.Size = new System.Drawing.Size(88, 13);
			this.lbRe.TabIndex = 17;
			this.lbRe.Text = "Re number (air) =";
			// 
			// tbU
			// 
			this.tbU.Location = new System.Drawing.Point(99, 106);
			this.tbU.Name = "tbU";
			this.tbU.Size = new System.Drawing.Size(43, 20);
			this.tbU.TabIndex = 10;
			this.tbU.Text = "50";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(148, 109);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(25, 13);
			this.label4.TabIndex = 12;
			this.label4.Text = "m/s";
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(3, 109);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(77, 13);
			this.label5.TabIndex = 11;
			this.label5.Text = "Inlet velocity U";
			// 
			// lbM
			// 
			this.lbM.AutoSize = true;
			this.lbM.Location = new System.Drawing.Point(148, 30);
			this.lbM.Name = "lbM";
			this.lbM.Size = new System.Drawing.Size(15, 13);
			this.lbM.TabIndex = 9;
			this.lbM.Text = "m";
			this.lbM.Visible = false;
			// 
			// lbNACANum
			// 
			this.lbNACANum.AutoSize = true;
			this.lbNACANum.Location = new System.Drawing.Point(3, 30);
			this.lbNACANum.Name = "lbNACANum";
			this.lbNACANum.Size = new System.Drawing.Size(74, 13);
			this.lbNACANum.TabIndex = 1;
			this.lbNACANum.Text = "NACA number";
			// 
			// cbTrailingEdge
			// 
			this.cbTrailingEdge.FormattingEnabled = true;
			this.cbTrailingEdge.Items.AddRange(new object[] {
            "close",
            "open"});
			this.cbTrailingEdge.Location = new System.Drawing.Point(71, 79);
			this.cbTrailingEdge.Name = "cbTrailingEdge";
			this.cbTrailingEdge.Size = new System.Drawing.Size(71, 21);
			this.cbTrailingEdge.TabIndex = 7;
			// 
			// tbNACAn_radius
			// 
			this.tbNACAn_radius.Location = new System.Drawing.Point(80, 27);
			this.tbNACAn_radius.Name = "tbNACAn_radius";
			this.tbNACAn_radius.Size = new System.Drawing.Size(62, 20);
			this.tbNACAn_radius.TabIndex = 0;
			this.tbNACAn_radius.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbNACAn_radius_KeyPress);
			// 
			// lbTrailingEdge
			// 
			this.lbTrailingEdge.AutoSize = true;
			this.lbTrailingEdge.Location = new System.Drawing.Point(3, 82);
			this.lbTrailingEdge.Name = "lbTrailingEdge";
			this.lbTrailingEdge.Size = new System.Drawing.Size(68, 13);
			this.lbTrailingEdge.TabIndex = 6;
			this.lbTrailingEdge.Text = "Trailing edge";
			// 
			// tbAlpha
			// 
			this.tbAlpha.Location = new System.Drawing.Point(99, 53);
			this.tbAlpha.Name = "tbAlpha";
			this.tbAlpha.Size = new System.Drawing.Size(43, 20);
			this.tbAlpha.TabIndex = 2;
			this.tbAlpha.Text = "0";
			this.tbAlpha.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbAlpha_KeyPress);
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(148, 56);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(25, 13);
			this.label3.TabIndex = 4;
			this.label3.Text = "deg";
			// 
			// lbAlpha
			// 
			this.lbAlpha.AutoSize = true;
			this.lbAlpha.Location = new System.Drawing.Point(3, 56);
			this.lbAlpha.Name = "lbAlpha";
			this.lbAlpha.Size = new System.Drawing.Size(89, 13);
			this.lbAlpha.TabIndex = 3;
			this.lbAlpha.Text = "Angle of attack α";
			// 
			// groupBox4
			// 
			this.groupBox4.Controls.Add(this.btnApplyGeo);
			this.groupBox4.Controls.Add(this.cbGeometry);
			this.groupBox4.Dock = System.Windows.Forms.DockStyle.Left;
			this.groupBox4.Location = new System.Drawing.Point(1141, 503);
			this.groupBox4.Name = "groupBox4";
			this.groupBox4.Size = new System.Drawing.Size(470, 594);
			this.groupBox4.TabIndex = 2;
			this.groupBox4.TabStop = false;
			this.groupBox4.Text = "Geometry";
			// 
			// btnApplyGeo
			// 
			this.btnApplyGeo.Location = new System.Drawing.Point(3, 44);
			this.btnApplyGeo.Name = "btnApplyGeo";
			this.btnApplyGeo.Size = new System.Drawing.Size(75, 23);
			this.btnApplyGeo.TabIndex = 10;
			this.btnApplyGeo.Text = "Apply";
			this.btnApplyGeo.UseVisualStyleBackColor = true;
			this.btnApplyGeo.Click += new System.EventHandler(this.btnApplyGeo_Click);
			// 
			// cbGeometry
			// 
			this.cbGeometry.FormattingEnabled = true;
			this.cbGeometry.Items.AddRange(new object[] {
            "NACA Airfoil",
            "Cylinder"});
			this.cbGeometry.Location = new System.Drawing.Point(3, 16);
			this.cbGeometry.Name = "cbGeometry";
			this.cbGeometry.Size = new System.Drawing.Size(150, 21);
			this.cbGeometry.TabIndex = 8;
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.ColumnCount = 2;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 1138F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 476F));
			this.tableLayoutPanel1.Controls.Add(this.groupBox4, 1, 1);
			this.tableLayoutPanel1.Controls.Add(this.panel1, 1, 0);
			this.tableLayoutPanel1.Controls.Add(this.splitContainer1, 0, 1);
			this.tableLayoutPanel1.Controls.Add(this.splitContainer2, 0, 0);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 2;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 500F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 600F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(1319, 661);
			this.tableLayoutPanel1.TabIndex = 0;
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1319, 661);
			this.Controls.Add(this.tableLayoutPanel1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.Name = "Form1";
			this.Text = "PotFlow Meshing";
			((System.ComponentModel.ISupportInitialize)(this.dsPoints)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dtPoints)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dtPikeBindingSource)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dstPike)).EndInit();
			this.splitContainer2.Panel1.ResumeLayout(false);
			this.splitContainer2.Panel2.ResumeLayout(false);
			this.splitContainer2.Panel2.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
			this.splitContainer2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.dgvPoints)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.meshChart)).EndInit();
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
			this.splitContainer1.ResumeLayout(false);
			this.splitContainer3.Panel1.ResumeLayout(false);
			this.splitContainer3.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).EndInit();
			this.splitContainer3.ResumeLayout(false);
			this.panel1.ResumeLayout(false);
			this.gbNumericalParameters.ResumeLayout(false);
			this.gbNumericalParameters.PerformLayout();
			this.gbNACAAirfoil_radius.ResumeLayout(false);
			this.gbNACAAirfoil_radius.PerformLayout();
			this.groupBox4.ResumeLayout(false);
			this.tableLayoutPanel1.ResumeLayout(false);
			this.ResumeLayout(false);

        }

        #endregion
        private System.Data.DataSet dsPoints;
        private System.Data.DataTable dtPoints;
        private System.Data.DataColumn dataColumn1;
        private System.Data.DataColumn dataColumn2;
        private System.Data.DataColumn dataColumn3;
        private System.Windows.Forms.BindingSource dtPikeBindingSource;
        private dstPike dstPike;
        private System.Windows.Forms.DataGridViewTextBoxColumn pDataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn xDataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn yDataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn pDataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn xDataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn yDataGridViewTextBoxColumn2;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.DataGridView dgvPoints;
        private System.Windows.Forms.DataGridViewTextBoxColumn pDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn xDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn yDataGridViewTextBoxColumn;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Button btRun;
        private System.Windows.Forms.RichTextBox rtbInfo;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox gbNumericalParameters;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbDx;
        private System.Windows.Forms.GroupBox gbNACAAirfoil_radius;
        private System.Windows.Forms.Label lbM;
        private System.Windows.Forms.Label lbNACANum;
        private System.Windows.Forms.ComboBox cbTrailingEdge;
        private System.Windows.Forms.TextBox tbNACAn_radius;
        private System.Windows.Forms.Label lbTrailingEdge;
        private System.Windows.Forms.TextBox tbAlpha;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lbAlpha;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button btnApplyGeo;
        private System.Windows.Forms.ComboBox cbGeometry;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		public System.Windows.Forms.DataVisualization.Charting.Chart meshChart;
		private System.Windows.Forms.Label lbShowSurface;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox tbU;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label lbChInfo;
		private System.Windows.Forms.TextBox tbRe;
		private System.Windows.Forms.Label lbRe;
		private System.Windows.Forms.ComboBox tbMeshScale;
	}
}

