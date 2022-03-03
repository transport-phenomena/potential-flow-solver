
namespace potFlow_Solver_v0._1
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
			System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.rtbInfo = new System.Windows.Forms.RichTextBox();
			this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
			this.btnRun = new System.Windows.Forms.Button();
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.rtbMeshInfo = new System.Windows.Forms.RichTextBox();
			this.pbResult = new System.Windows.Forms.PictureBox();
			this.postProcChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
			this.splitContainer2 = new System.Windows.Forms.SplitContainer();
			this.gbSetup = new System.Windows.Forms.GroupBox();
			this.tbCrit = new System.Windows.Forms.TextBox();
			this.lbCrit = new System.Windows.Forms.Label();
			this.tbMaxIt = new System.Windows.Forms.TextBox();
			this.lbNumOfIt = new System.Windows.Forms.Label();
			this.gbResults = new System.Windows.Forms.GroupBox();
			this.ll_uVect = new System.Windows.Forms.LinkLabel();
			this.ll_streamlines = new System.Windows.Forms.LinkLabel();
			this.ll_psi = new System.Windows.Forms.LinkLabel();
			this.ll_p = new System.Windows.Forms.LinkLabel();
			this.ll_uy = new System.Windows.Forms.LinkLabel();
			this.ll_ux = new System.Windows.Forms.LinkLabel();
			this.ll_u = new System.Windows.Forms.LinkLabel();
			this.dataSet = new potFlow_Solver_v0._1.DataSet();
			this.btnImportMesh = new System.Windows.Forms.Button();
			this.splitContainer3 = new System.Windows.Forms.SplitContainer();
			this.btnDelete = new System.Windows.Forms.Button();
			this.btClearMesh = new System.Windows.Forms.Button();
			this.tableLayoutPanel1.SuspendLayout();
			this.tableLayoutPanel2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pbResult)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.postProcChart)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
			this.splitContainer2.Panel1.SuspendLayout();
			this.splitContainer2.Panel2.SuspendLayout();
			this.splitContainer2.SuspendLayout();
			this.gbSetup.SuspendLayout();
			this.gbResults.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dataSet)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).BeginInit();
			this.splitContainer3.Panel1.SuspendLayout();
			this.splitContainer3.Panel2.SuspendLayout();
			this.splitContainer3.SuspendLayout();
			this.SuspendLayout();
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.ColumnCount = 2;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel1.Controls.Add(this.rtbInfo, 1, 1);
			this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 1);
			this.tableLayoutPanel1.Controls.Add(this.splitContainer1, 1, 0);
			this.tableLayoutPanel1.Controls.Add(this.splitContainer2, 0, 0);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 2;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 230F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(1299, 691);
			this.tableLayoutPanel1.TabIndex = 0;
			// 
			// rtbInfo
			// 
			this.rtbInfo.Dock = System.Windows.Forms.DockStyle.Fill;
			this.rtbInfo.Location = new System.Drawing.Point(103, 464);
			this.rtbInfo.Name = "rtbInfo";
			this.rtbInfo.Size = new System.Drawing.Size(1193, 224);
			this.rtbInfo.TabIndex = 1;
			this.rtbInfo.Text = "";
			// 
			// tableLayoutPanel2
			// 
			this.tableLayoutPanel2.ColumnCount = 1;
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel2.Controls.Add(this.btnImportMesh, 0, 1);
			this.tableLayoutPanel2.Controls.Add(this.btnRun, 0, 0);
			this.tableLayoutPanel2.Controls.Add(this.splitContainer3, 0, 2);
			this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 464);
			this.tableLayoutPanel2.Name = "tableLayoutPanel2";
			this.tableLayoutPanel2.RowCount = 3;
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 60F));
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 40F));
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel2.Size = new System.Drawing.Size(94, 224);
			this.tableLayoutPanel2.TabIndex = 4;
			// 
			// btnRun
			// 
			this.btnRun.Dock = System.Windows.Forms.DockStyle.Fill;
			this.btnRun.Location = new System.Drawing.Point(3, 3);
			this.btnRun.Name = "btnRun";
			this.btnRun.Size = new System.Drawing.Size(88, 94);
			this.btnRun.TabIndex = 4;
			this.btnRun.Text = "Start Run";
			this.btnRun.UseVisualStyleBackColor = true;
			this.btnRun.Click += new System.EventHandler(this.btnRun_Click);
			// 
			// splitContainer1
			// 
			this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer1.Location = new System.Drawing.Point(103, 3);
			this.splitContainer1.Name = "splitContainer1";
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this.rtbMeshInfo);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.pbResult);
			this.splitContainer1.Panel2.Controls.Add(this.postProcChart);
			this.splitContainer1.Size = new System.Drawing.Size(1193, 455);
			this.splitContainer1.SplitterDistance = 172;
			this.splitContainer1.TabIndex = 5;
			// 
			// rtbMeshInfo
			// 
			this.rtbMeshInfo.Dock = System.Windows.Forms.DockStyle.Fill;
			this.rtbMeshInfo.Location = new System.Drawing.Point(0, 0);
			this.rtbMeshInfo.Name = "rtbMeshInfo";
			this.rtbMeshInfo.Size = new System.Drawing.Size(172, 455);
			this.rtbMeshInfo.TabIndex = 3;
			this.rtbMeshInfo.Text = "";
			// 
			// pbResult
			// 
			this.pbResult.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
			this.pbResult.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pbResult.Location = new System.Drawing.Point(0, 0);
			this.pbResult.Name = "pbResult";
			this.pbResult.Size = new System.Drawing.Size(1017, 455);
			this.pbResult.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.pbResult.TabIndex = 1;
			this.pbResult.TabStop = false;
			this.pbResult.Visible = false;
			// 
			// postProcChart
			// 
			this.postProcChart.Dock = System.Windows.Forms.DockStyle.Fill;
			legend2.Enabled = false;
			legend2.Name = "Legend1";
			this.postProcChart.Legends.Add(legend2);
			this.postProcChart.Location = new System.Drawing.Point(0, 0);
			this.postProcChart.Name = "postProcChart";
			this.postProcChart.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.None;
			this.postProcChart.Size = new System.Drawing.Size(1017, 455);
			this.postProcChart.TabIndex = 0;
			this.postProcChart.Text = "chart1";
			// 
			// splitContainer2
			// 
			this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer2.Location = new System.Drawing.Point(3, 3);
			this.splitContainer2.Name = "splitContainer2";
			this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// splitContainer2.Panel1
			// 
			this.splitContainer2.Panel1.Controls.Add(this.gbSetup);
			// 
			// splitContainer2.Panel2
			// 
			this.splitContainer2.Panel2.Controls.Add(this.gbResults);
			this.splitContainer2.Size = new System.Drawing.Size(94, 455);
			this.splitContainer2.SplitterDistance = 120;
			this.splitContainer2.TabIndex = 6;
			// 
			// gbSetup
			// 
			this.gbSetup.Controls.Add(this.tbCrit);
			this.gbSetup.Controls.Add(this.lbCrit);
			this.gbSetup.Controls.Add(this.tbMaxIt);
			this.gbSetup.Controls.Add(this.lbNumOfIt);
			this.gbSetup.Dock = System.Windows.Forms.DockStyle.Fill;
			this.gbSetup.Location = new System.Drawing.Point(0, 0);
			this.gbSetup.Name = "gbSetup";
			this.gbSetup.Size = new System.Drawing.Size(94, 120);
			this.gbSetup.TabIndex = 7;
			this.gbSetup.TabStop = false;
			this.gbSetup.Text = "Setup";
			// 
			// tbCrit
			// 
			this.tbCrit.Location = new System.Drawing.Point(22, 75);
			this.tbCrit.Name = "tbCrit";
			this.tbCrit.Size = new System.Drawing.Size(72, 20);
			this.tbCrit.TabIndex = 3;
			this.tbCrit.Text = "1e-6";
			// 
			// lbCrit
			// 
			this.lbCrit.AutoSize = true;
			this.lbCrit.Location = new System.Drawing.Point(0, 59);
			this.lbCrit.Name = "lbCrit";
			this.lbCrit.Size = new System.Drawing.Size(80, 13);
			this.lbCrit.TabIndex = 2;
			this.lbCrit.Text = "Conv. criterium:";
			// 
			// tbMaxIt
			// 
			this.tbMaxIt.Location = new System.Drawing.Point(22, 36);
			this.tbMaxIt.Name = "tbMaxIt";
			this.tbMaxIt.Size = new System.Drawing.Size(72, 20);
			this.tbMaxIt.TabIndex = 1;
			this.tbMaxIt.Text = "10000";
			// 
			// lbNumOfIt
			// 
			this.lbNumOfIt.AutoSize = true;
			this.lbNumOfIt.Location = new System.Drawing.Point(0, 20);
			this.lbNumOfIt.Name = "lbNumOfIt";
			this.lbNumOfIt.Size = new System.Drawing.Size(75, 13);
			this.lbNumOfIt.TabIndex = 0;
			this.lbNumOfIt.Text = "Max iterations:";
			// 
			// gbResults
			// 
			this.gbResults.Controls.Add(this.ll_uVect);
			this.gbResults.Controls.Add(this.ll_streamlines);
			this.gbResults.Controls.Add(this.ll_psi);
			this.gbResults.Controls.Add(this.ll_p);
			this.gbResults.Controls.Add(this.ll_uy);
			this.gbResults.Controls.Add(this.ll_ux);
			this.gbResults.Controls.Add(this.ll_u);
			this.gbResults.Dock = System.Windows.Forms.DockStyle.Fill;
			this.gbResults.Location = new System.Drawing.Point(0, 0);
			this.gbResults.Name = "gbResults";
			this.gbResults.Size = new System.Drawing.Size(94, 331);
			this.gbResults.TabIndex = 0;
			this.gbResults.TabStop = false;
			this.gbResults.Text = "Results";
			this.gbResults.Visible = false;
			// 
			// ll_uVect
			// 
			this.ll_uVect.AutoSize = true;
			this.ll_uVect.LinkColor = System.Drawing.Color.Black;
			this.ll_uVect.Location = new System.Drawing.Point(9, 94);
			this.ll_uVect.Name = "ll_uVect";
			this.ll_uVect.Size = new System.Drawing.Size(82, 13);
			this.ll_uVect.TabIndex = 6;
			this.ll_uVect.TabStop = true;
			this.ll_uVect.Text = "Velocity vectors";
			this.ll_uVect.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.ll_uVect_LinkClicked);
			// 
			// ll_streamlines
			// 
			this.ll_streamlines.AutoSize = true;
			this.ll_streamlines.LinkColor = System.Drawing.Color.Black;
			this.ll_streamlines.Location = new System.Drawing.Point(9, 81);
			this.ll_streamlines.Name = "ll_streamlines";
			this.ll_streamlines.Size = new System.Drawing.Size(61, 13);
			this.ll_streamlines.TabIndex = 5;
			this.ll_streamlines.TabStop = true;
			this.ll_streamlines.Text = "Streamlines";
			this.ll_streamlines.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.ll_streamlines_LinkClicked);
			// 
			// ll_psi
			// 
			this.ll_psi.AutoSize = true;
			this.ll_psi.LinkColor = System.Drawing.Color.Black;
			this.ll_psi.Location = new System.Drawing.Point(9, 68);
			this.ll_psi.Name = "ll_psi";
			this.ll_psi.Size = new System.Drawing.Size(81, 13);
			this.ll_psi.TabIndex = 4;
			this.ll_psi.TabStop = true;
			this.ll_psi.Text = "Stream function";
			this.ll_psi.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.ll_psi_LinkClicked);
			// 
			// ll_p
			// 
			this.ll_p.AutoSize = true;
			this.ll_p.LinkColor = System.Drawing.Color.Black;
			this.ll_p.Location = new System.Drawing.Point(9, 55);
			this.ll_p.Name = "ll_p";
			this.ll_p.Size = new System.Drawing.Size(48, 13);
			this.ll_p.TabIndex = 3;
			this.ll_p.TabStop = true;
			this.ll_p.Text = "Pressure";
			this.ll_p.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.ll_p_LinkClicked);
			// 
			// ll_uy
			// 
			this.ll_uy.AutoSize = true;
			this.ll_uy.LinkColor = System.Drawing.Color.Black;
			this.ll_uy.Location = new System.Drawing.Point(9, 42);
			this.ll_uy.Name = "ll_uy";
			this.ll_uy.Size = new System.Drawing.Size(55, 13);
			this.ll_uy.TabIndex = 2;
			this.ll_uy.TabStop = true;
			this.ll_uy.Text = "Velocity y ";
			this.ll_uy.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.ll_uy_LinkClicked);
			// 
			// ll_ux
			// 
			this.ll_ux.AutoSize = true;
			this.ll_ux.LinkColor = System.Drawing.Color.Black;
			this.ll_ux.Location = new System.Drawing.Point(9, 29);
			this.ll_ux.Name = "ll_ux";
			this.ll_ux.Size = new System.Drawing.Size(55, 13);
			this.ll_ux.TabIndex = 1;
			this.ll_ux.TabStop = true;
			this.ll_ux.Text = "Velocity x ";
			this.ll_ux.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.ll_ux_LinkClicked);
			// 
			// ll_u
			// 
			this.ll_u.AutoSize = true;
			this.ll_u.LinkColor = System.Drawing.Color.Black;
			this.ll_u.Location = new System.Drawing.Point(9, 16);
			this.ll_u.Name = "ll_u";
			this.ll_u.Size = new System.Drawing.Size(73, 13);
			this.ll_u.TabIndex = 0;
			this.ll_u.TabStop = true;
			this.ll_u.Text = "Velocity mag. ";
			this.ll_u.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.ll_u_LinkClicked);
			// 
			// dataSet
			// 
			this.dataSet.DataSetName = "dataSet";
			this.dataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
			// 
			// btnImportMesh
			// 
			this.btnImportMesh.Dock = System.Windows.Forms.DockStyle.Fill;
			this.btnImportMesh.Location = new System.Drawing.Point(3, 103);
			this.btnImportMesh.Name = "btnImportMesh";
			this.btnImportMesh.Size = new System.Drawing.Size(88, 44);
			this.btnImportMesh.TabIndex = 1;
			this.btnImportMesh.Text = "Import Mesh";
			this.btnImportMesh.UseVisualStyleBackColor = true;
			this.btnImportMesh.Click += new System.EventHandler(this.btnImportMesh_Click);
			// 
			// splitContainer3
			// 
			this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Top;
			this.splitContainer3.Location = new System.Drawing.Point(3, 153);
			this.splitContainer3.Name = "splitContainer3";
			this.splitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// splitContainer3.Panel1
			// 
			this.splitContainer3.Panel1.Controls.Add(this.btnDelete);
			// 
			// splitContainer3.Panel2
			// 
			this.splitContainer3.Panel2.Controls.Add(this.btClearMesh);
			this.splitContainer3.Panel2MinSize = 10;
			this.splitContainer3.Size = new System.Drawing.Size(88, 71);
			this.splitContainer3.SplitterDistance = 35;
			this.splitContainer3.TabIndex = 5;
			// 
			// btnDelete
			// 
			this.btnDelete.Dock = System.Windows.Forms.DockStyle.Fill;
			this.btnDelete.Location = new System.Drawing.Point(0, 0);
			this.btnDelete.Name = "btnDelete";
			this.btnDelete.Size = new System.Drawing.Size(88, 35);
			this.btnDelete.TabIndex = 5;
			this.btnDelete.Text = "Clear Results";
			this.btnDelete.UseVisualStyleBackColor = true;
			this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click_1);
			// 
			// btClearMesh
			// 
			this.btClearMesh.Dock = System.Windows.Forms.DockStyle.Fill;
			this.btClearMesh.Location = new System.Drawing.Point(0, 0);
			this.btClearMesh.Name = "btClearMesh";
			this.btClearMesh.Size = new System.Drawing.Size(88, 32);
			this.btClearMesh.TabIndex = 0;
			this.btClearMesh.Text = "Clear Mesh";
			this.btClearMesh.UseVisualStyleBackColor = true;
			this.btClearMesh.Click += new System.EventHandler(this.btClearMesh_Click);
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1299, 691);
			this.Controls.Add(this.tableLayoutPanel1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "Form1";
			this.Text = "PotFlow Solver";
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel2.ResumeLayout(false);
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
			this.splitContainer1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.pbResult)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.postProcChart)).EndInit();
			this.splitContainer2.Panel1.ResumeLayout(false);
			this.splitContainer2.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
			this.splitContainer2.ResumeLayout(false);
			this.gbSetup.ResumeLayout(false);
			this.gbSetup.PerformLayout();
			this.gbResults.ResumeLayout(false);
			this.gbResults.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.dataSet)).EndInit();
			this.splitContainer3.Panel1.ResumeLayout(false);
			this.splitContainer3.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).EndInit();
			this.splitContainer3.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.RichTextBox rtbInfo;
		private DataSet dataSet;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
		private System.Windows.Forms.Button btnRun;
		private System.Windows.Forms.SplitContainer splitContainer1;
		private System.Windows.Forms.RichTextBox rtbMeshInfo;
		private System.Windows.Forms.DataVisualization.Charting.Chart postProcChart;
		private System.Windows.Forms.SplitContainer splitContainer2;
		private System.Windows.Forms.GroupBox gbSetup;
		private System.Windows.Forms.TextBox tbCrit;
		private System.Windows.Forms.Label lbCrit;
		private System.Windows.Forms.TextBox tbMaxIt;
		private System.Windows.Forms.Label lbNumOfIt;
		private System.Windows.Forms.GroupBox gbResults;
		private System.Windows.Forms.LinkLabel ll_uVect;
		private System.Windows.Forms.LinkLabel ll_streamlines;
		private System.Windows.Forms.LinkLabel ll_psi;
		private System.Windows.Forms.LinkLabel ll_p;
		private System.Windows.Forms.LinkLabel ll_uy;
		private System.Windows.Forms.LinkLabel ll_ux;
		private System.Windows.Forms.LinkLabel ll_u;
		private System.Windows.Forms.PictureBox pbResult;
		private System.Windows.Forms.Button btnImportMesh;
		private System.Windows.Forms.SplitContainer splitContainer3;
		private System.Windows.Forms.Button btnDelete;
		private System.Windows.Forms.Button btClearMesh;
	}
}

