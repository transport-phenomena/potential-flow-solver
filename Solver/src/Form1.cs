using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Windows.Forms.DataVisualization.Charting;
using System.Globalization;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Solvers;
using MathNet.Numerics.LinearAlgebra.Double;
using MathNet.Numerics.LinearAlgebra.Double.Solvers;
using System.Threading;
using System.Diagnostics;

namespace potFlow_Solver_v0._1
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
			btnRun.Enabled = false;
			btnDelete.Enabled = false;
			btClearMesh.Enabled = false;
		}

		#region constructors
		static string[] getPyCode()//string resFileLocation, string plotFileLocation)
		{
			int n = potFlow_Solver_v0._1.Properties.Resources.pyCode.Length;
			string[] pyCode = new string[n];
			pyCode = potFlow_Solver_v0._1.Properties.Resources.pyCode.Split('\r');

			return pyCode;
		}

		private string[] ImportMesh()
		{
			rtbInfo.AppendText("Importing Mesh...");
			rtbInfo.AppendText("\n");

			try
			{
				OpenFileDialog openFile = new OpenFileDialog();
				openFile.Filter = "(*.msh)|*.msh";
				var path = @"C:\Users";
				if (openFile.ShowDialog() == DialogResult.OK)
				{
					path = openFile.FileName;
				}
				string[] textLines = File.ReadAllLines(path);

				string[] meshInfo = new string[6];

				// write mesh info to meshInfo string vector
				for (int i = 0; i < 5; i++)
				{
					meshInfo[i] = textLines[i];
				}

				changeLine(rtbInfo, 1, "1%");

				// write and convert mesh coordinates to datatable
				int m = 7;
				string[] singleLineString;
				object[] singleLineCoord = new object[m];
				int j;
				int p, f, b, u, d;
				double x, y, nx, ny, BCval;
				string BC;
				bool connectivity = false;
				for (int i = 6; i < textLines.Length; i++)
				{
					if (i < textLines.Length / 4) { changeLine(rtbInfo, 1, "25%"); }
					else if (i > textLines.Length / 4 && i < textLines.Length / 2) { changeLine(rtbInfo, 1, "50%"); }
					else if (i > textLines.Length / 2 && i < (3 * textLines.Length) / 4) { changeLine(rtbInfo, 1, "75%"); }


					if (textLines[i] == "Connectivity")
					{
						connectivity = true;
						m = 5;
						singleLineCoord = new object[m];
						i++;
					}
					j = 0;
					singleLineString = textLines[i].Split('\t');
					while (j < m)
					{
						try
						{
							singleLineCoord[j] = Convert.ToDouble(singleLineString[j]);
							j++;
						}
						catch
						{
							if (connectivity == true)
							{
								try
								{
									singleLineCoord[j] = Convert.ToInt32(singleLineString[j]);
									j++;
								}
								catch
								{
									j++;
								}
							}
							else
							{
								singleLineCoord[j] = singleLineString[j];
								j++;
							}
						}
					}
					if (connectivity == true)
					{
						p = Convert.ToInt32(singleLineCoord[0]);
						f = Convert.ToInt32(singleLineCoord[1]);
						b = Convert.ToInt32(singleLineCoord[2]);
						u = Convert.ToInt32(singleLineCoord[3]);
						d = Convert.ToInt32(singleLineCoord[4]);
						dataSet.Tables[1].Rows.Add(p, f, b, u, d);
					}
					else
					{
						p = Convert.ToInt32(singleLineCoord[0]);
						x = Convert.ToDouble(singleLineCoord[1]);
						y = Convert.ToDouble(singleLineCoord[2]);
						BC = singleLineCoord[3].ToString();
						try
						{
							nx = Convert.ToDouble(singleLineCoord[4]);
							ny = Convert.ToDouble(singleLineCoord[5]);
						}
						catch
						{
							nx = Double.NaN;
							ny = Double.NaN;
						}
						try
						{
							BCval = Convert.ToDouble(singleLineCoord[6]);
						}
						catch
						{
							BCval = Double.NaN;
						}
						//add to dataset
						dataSet.Tables[0].Rows.Add(p, x, y, BC, nx, ny, BCval);
					}
				}
				return meshInfo;
			}
			catch(UnauthorizedAccessException)
			{
				Application.Restart();
				return null;
			}
		}

		private double getU()
		{
			double U;
			string Ustring = "0";
			string[] geometryLine = rtbMeshInfo.Lines[0].Split(' ');
			string geometry = geometryLine[0];
			if (geometry == "NACA")
			{
				string[] UstringLine = rtbMeshInfo.Lines[4].Split('\t');
				Ustring = UstringLine[2];
			}
			if (geometry == "Cylinder:")
			{
				string[] UstringLine = rtbMeshInfo.Lines[2].Split('\t');
				Ustring = UstringLine[2];
			}
			return U = Convert.ToDouble(Ustring);
		}

		private double getDx()
		{
			double dx;
			string meshStep = "0";
			string[] geometryLine = rtbMeshInfo.Lines[0].Split(' ');
			string geometry = geometryLine[0];
			if (geometry == "NACA")
			{
				string[] meshStepLine = rtbMeshInfo.Lines[3].Split('\t');
				meshStep = meshStepLine[3];
			}
			if (geometry == "Cylinder:")
			{
				string[] meshStepLine = rtbMeshInfo.Lines[1].Split('\t');
				meshStep = meshStepLine[1];
			}
			return dx = Convert.ToDouble(meshStep);
		}

		private DataTable BCTable()
		{
			int p;
			double x, y, nx, ny;
			string surface;
			int i = 1;
			int j = 1;
			foreach (DataRow dr in dataSet.Tables[0].Rows)
			{
				p = Convert.ToInt32(dr[0]);
				x = Convert.ToDouble(dr[1]);
				y = Convert.ToDouble(dr[2]);
				surface = dr[3].ToString();
				nx = Convert.ToDouble(dr[4]);
				ny = Convert.ToDouble(dr[5]);
				if (p == i) //check if domain boundary
				{
					dataSet.Tables[4].Rows.Add(p, x, y);
					i++;
				}
				if (surface == "geometry surface") //check if surface
				{
					dataSet.Tables[5].Rows.Add(p, nx, ny, x, y);
					j++;
				}
			}
			return dataSet.Tables[4];
		}
		Tuple<Matrix<double>, Vector<double>> GenerateSystemOfEq()
		{
			//int numOfNan = CountNanPoints();
			DataTable BCtable = BCTable();

			double dx = getDx();
			double U = getU();
			int dim = dataSet.Tables[1].Rows.Count - 2;
			Matrix<double> A = Matrix<double>.Build.Sparse(dim, dim);
			Vector<double> b = Vector<double>.Build.Sparse(dim);
			int p, sf, sb, su, sd;
			double nx, ny, y;
			/*
			int N = Convert.ToInt32((int)(((Convert.ToDouble(dataSet.Tables[0].Rows[Convert.ToInt32(dataSet.Tables[0].Rows.Count - 1)][1])) + (Math.Abs(Convert.ToDouble(dataSet.Tables[0].Rows[0][1])))) / dx)) + 1;
			int M = Convert.ToInt32((int)(Math.Abs(Convert.ToDouble(dataSet.Tables[0].Rows[0][2])) + Math.Abs(Convert.ToDouble(dataSet.Tables[0].Rows[Convert.ToInt32(dataSet.Tables[0].Rows.Count - 1)][2]))) / dx);
			int j = 0;
			*/
			int i = 0;
			foreach(DataRow dr in dataSet.Tables[1].Rows)
			{
				p = Convert.ToInt32(dr[0]) - 1;
				sf = Convert.ToInt32(dr[1]) - 1;
				sb = Convert.ToInt32(dr[2]) - 1;
				su = Convert.ToInt32(dr[3]) - 1;
				sd = Convert.ToInt32(dr[4]) - 1;

				y = Convert.ToDouble(BCtable.Rows[p][2]);

				if (sf == -2 || su == -2 || sb == -2 || sd == -2) //check if surface point
				{
					nx = Convert.ToDouble(dataSet.Tables[5].Rows[i][1]);
					ny = Convert.ToDouble(dataSet.Tables[5].Rows[i][2]);

					A[p, p] = ny - nx;//Math.Abs(ny) + Math.Abs(nx);

					/*
					if (ny > 0 || (ny == 0 && nx < 0))
					{
						if(sf != -2)
						{
							A[p, sf] = 1;
						}
					}
					else if(ny < 0)
					{
						if(sb != -2)
						{
							A[p, sb] = 1;
						}
					}
					*/
					/*
					if ((sf == -2 || sb == -2))
					{
						if(ny > 0)
						{
							A[p, su] = Math.Abs(nx);
						}
						else if(ny < 0)
						{
							A[p, sd] = Math.Abs(nx);
						}
						else if(ny == 0)
						{
							if(nx < 0)
							{
								A[p, sb] = Math.Abs(ny);
							}
							else if(nx > 0)
							{
								A[p, sf] = Math.Abs(ny);
							}
						}
					}
					else if (sf != -2 && sb != 0)
					{
						if (nx != 0 && ny != 0)
						{
							if (ny > 0)
							{
								A[p, sb] = Math.Abs(ny);
								A[p, su] = Math.Abs(nx);
							}
							else if (ny < 0)
							{
								A[p, sf] = Math.Abs(ny);
								A[p, sd] = Math.Abs(nx);
							}
						}
						else
						{
							if (ny > 0)
							{
								A[p, su] = Math.Abs(nx);
							}
							else if (ny < 0)
							{
								A[p, sd] = Math.Abs(nx);
							}
						}
					}
					*/
					i++;
				}
				else
				{
					if (sf <= dim && sb > dim && su > dim && sd <= dim) //upper left corner
					{
						A[p, p] = -4;
						A[p, sf] = 1;
						A[p, sd] = 1;

						b[p] = -(U * y) - (U * (y + dx));
					}
					else if (sf <= dim && sb <= dim && su > dim && sd <= dim) // upper middle
					{
						A[p, p] = -4;
						A[p, sf] = 1;
						A[p, sb] = 1;
						A[p, sd] = 1;

						b[p] = -U * (y + dx);
					}
					else if (sf > dim && sb <= dim && su > dim && sd <= dim) //upper right corner
					{
						A[p, p] = -3;
						A[p, sb] = 1;
						A[p, sd] = 1;

						b[p] = -U * (y + dx);
					}
					else if (sf <= dim && sb > dim && su <= dim && sd <= dim) //middle left
					{
						A[p, p] = -4;
						A[p, sf] = 1;
						A[p, sd] = 1;
						A[p, su] = 1;

						b[p] = -U * y;
					}
					else if (sf > dim && sb <= dim && su <= dim && sd <= dim) //middle right
					{
						A[p, p] = -3;
						A[p, sd] = 1;
						A[p, su] = 1;
						A[p, sb] = 1;
					}
					else if (sf <= dim && sb > dim && su <= dim && sd > dim) //lower left corner
					{
						A[p, p] = -4;
						A[p, sf] = 1;
						A[p, su] = 1;

						b[p] = -(U * y) - (U * (y - dx));
					}
					else if (sf <= dim && sb <= dim && su <= dim && sd > dim) //lower middle
					{
						A[p, p] = -4;
						A[p, sf] = 1;
						A[p, sb] = 1;
						A[p, su] = 1;

						b[p] = -U * (y - dx);
					}
					else if (sf > dim && sb <= dim && su <= dim && sd > dim) //lower right corner
					{
						A[p, p] = -3;
						A[p, sb] = 1;
						A[p, su] = 1;

						b[p] = -U * (y - dx);
					}
					else
					{
						A[p, p] = -4;
						A[p, sf] = 1;
						A[p, sb] = 1;
						A[p, sd] = 1;
						A[p, su] = 1;
					}
				}
			}
			
			return new Tuple<Matrix<double>, Vector<double>>(A, b);
		}

		private Vector<double> SolveSystemOfEq()
		{
			Stopwatch sw = new Stopwatch();
			sw.Start();

			int iter = Convert.ToInt32(tbMaxIt.Text);
			double crit = Convert.ToDouble(tbCrit.Text);

			rtbInfo.AppendText("\n=========================================");
			rtbInfo.AppendText("\nRUN STARTED: " + DateTime.Now);
			rtbInfo.AppendText("\n=========================================");

			var systemOfEq = GenerateSystemOfEq();
			Matrix<double> A = SparseMatrix.OfMatrix(systemOfEq.Item1);
			Vector<double> b = SparseVector.OfVector(systemOfEq.Item2);

			var monitor = new Iterator<double>(
							new IterationCountStopCriterion<double>(iter),
							new ResidualStopCriterion<double>(crit),
							new DivergenceStopCriterion<double>(0.08, 10));

			Vector<double> psi = A.SolveIterative(b, new BiCgStab(), monitor);

			if (monitor.Status != IterationStatus.Converged)
			{
				rtbInfo.AppendText("Solution was not found!");
				monitor.Reset();
			}
			else
			{
				rtbInfo.AppendText("\nSolver status: " + monitor.Status.ToString());

				//calculate residual
				Vector<double> residual = Vector<double>.Build.SameAs(psi);
				A.Multiply(psi, residual);
				residual.Multiply(-1, residual);
				residual.Add(b, residual);

				double RMS = 0;
				foreach(double res in residual)
				{
					RMS = RMS + (res*res);
				}
				RMS = Math.Sqrt(RMS / residual.Count);

				sw.Stop();
				TimeSpan ts = sw.Elapsed;
				string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
						   ts.Hours, ts.Minutes, ts.Seconds,
						   ts.Milliseconds / 10);

				rtbInfo.AppendText("\nRMS Residual = " + RMS.ToString());
				rtbInfo.AppendText("\nMaximum Residual = " + residual.AbsoluteMaximum().ToString() + " at Node no. " + residual.AbsoluteMaximumIndex().ToString());
				rtbInfo.AppendText("\n");
				rtbInfo.AppendText("\nRUN ENDED. Elapsed time: " + elapsedTime);
				rtbInfo.AppendText("\n-----------------------------------------");
			}
			return psi;
		}

		#endregion

		#region methods
		async void changeLine(RichTextBox rtbInfo, int line, string text)
		{
			await Task.Delay(100);
			int s1 = rtbInfo.GetFirstCharIndexFromLine(line);
			int s2 = line < rtbInfo.Lines.Count() - 1 ?
					  rtbInfo.GetFirstCharIndexFromLine(line + 1) - 1 :
					  rtbInfo.Text.Length;
			rtbInfo.Select(s1, s2 - s1);
			rtbInfo.SelectedText = text;
		}
		private void CreateVelocityField()
		{
			int p, f, b, u, d;
			double dx = getDx();
			int dim = dataSet.Tables[1].Rows.Count - 2;
			int N = Convert.ToInt32((int)(((Convert.ToDouble(dataSet.Tables[0].Rows[Convert.ToInt32(dataSet.Tables[0].Rows.Count - 1)][1])) + (Math.Abs(Convert.ToDouble(dataSet.Tables[0].Rows[0][1])))) / dx)) + 1;
			double x, y, psi, psiLeft, psiRight, psiUp, psiDown, vx, vy, v;
			for (int i = 0; i < dataSet.Tables[2].Rows.Count; i++)
			{
				p = Convert.ToInt32(dataSet.Tables[1].Rows[i][0]) - 1;
				f = Convert.ToInt32(dataSet.Tables[1].Rows[i][1]) - 1;
				b = Convert.ToInt32(dataSet.Tables[1].Rows[i][2]) - 1;
				u = Convert.ToInt32(dataSet.Tables[1].Rows[i][3]) - 1;
				d = Convert.ToInt32(dataSet.Tables[1].Rows[i][4]) - 1;

				x = Convert.ToDouble(dataSet.Tables[2].Rows[i][1]);
				y = Convert.ToDouble(dataSet.Tables[2].Rows[i][2]);
				psi = Convert.ToDouble(dataSet.Tables[2].Rows[i][3]);

				if(f == -2 || u == -2 || b == -2 || d == -2)
				{
					if(f == -2 && u != -2 && b != -2 && d != -2) //leftmost point 
					{
						psiLeft = Convert.ToDouble(dataSet.Tables[2].Rows[b][3]);
						psiDown = Convert.ToDouble(dataSet.Tables[2].Rows[d][3]);
						psiUp = Convert.ToDouble(dataSet.Tables[2].Rows[u][3]);

						vx = (psiUp - psiDown) / (2 * dx);
						vy = -(psi - psiLeft) / dx;

						v = Math.Sqrt((vx * vx + vy * vy));

						dataSet.Tables[3].Rows.Add(p, vx, vy, v);
					}
					else if(f == -2 && u != -2 && b != -2 && d == -2) //RL corner
					{
						psiLeft = Convert.ToDouble(dataSet.Tables[2].Rows[b][3]);
						psiUp = Convert.ToDouble(dataSet.Tables[2].Rows[u][3]);

						vx = (psiUp - psi) / dx;
						vy = -(psi - psiLeft) / dx;

						v = Math.Sqrt((vx * vx + vy * vy));

						dataSet.Tables[3].Rows.Add(p, vx, vy, v);
					}
					else if(f != -2 && u != -2 && b != -2 && d == -2) //upper points
					{
						psiRight = Convert.ToDouble(dataSet.Tables[2].Rows[f][3]);
						psiLeft = Convert.ToDouble(dataSet.Tables[2].Rows[b][3]);
						psiUp = Convert.ToDouble(dataSet.Tables[2].Rows[u][3]);

						vx = (psiUp - psi) / dx;
						vy = -(psiRight - psiLeft) / (2 * dx);

						v = Math.Sqrt((vx * vx + vy * vy));

						dataSet.Tables[3].Rows.Add(p, vx, vy, v);
					}
					else if(f != -2 && u != -2 && b == -2 && d == -2) //LL corner
					{
						psiRight = Convert.ToDouble(dataSet.Tables[2].Rows[f][3]);
						psiUp = Convert.ToDouble(dataSet.Tables[2].Rows[u][3]);

						vx = (psiUp - psi) / dx;
						vy = -(psiRight - psi) / dx;

						v = Math.Sqrt((vx * vx + vy * vy));

						dataSet.Tables[3].Rows.Add(p, vx, vy, v);
					}
					else if(f != -2 && u != -2 && b == -2 && d != -2)//rightmost point
					{
						psiRight = Convert.ToDouble(dataSet.Tables[2].Rows[f][3]);
						psiDown = Convert.ToDouble(dataSet.Tables[2].Rows[d][3]);
						psiUp = Convert.ToDouble(dataSet.Tables[2].Rows[u][3]);

						vx = (psiUp - psiDown) / (2 * dx);
						vy = -(psiRight - psi) / dx;

						v = Math.Sqrt((vx * vx + vy * vy));

						dataSet.Tables[3].Rows.Add(p, vx, vy, v);
					}
					else if(f == -2 && u == -2 && b != -2 && d != -2)//UR corner
					{
						psiLeft = Convert.ToDouble(dataSet.Tables[2].Rows[b][3]);
						psiDown = Convert.ToDouble(dataSet.Tables[2].Rows[d][3]);

						vx = (psi - psiDown) / dx;
						vy = -(psi - psiLeft) / dx;

						v = Math.Sqrt((vx * vx + vy * vy));

						dataSet.Tables[3].Rows.Add(p, vx, vy, v);
					}
					else if(f != -2 && u == -2 && b != -2 && d != -2)//lower points
					{
						psiRight = Convert.ToDouble(dataSet.Tables[2].Rows[f][3]);
						psiLeft = Convert.ToDouble(dataSet.Tables[2].Rows[b][3]);
						psiDown = Convert.ToDouble(dataSet.Tables[2].Rows[d][3]);

						vx = (psi - psiDown) / dx;
						vy = -(psiRight - psiLeft) / (2 * dx);

						v = Math.Sqrt((vx * vx + vy * vy));

						dataSet.Tables[3].Rows.Add(p, vx, vy, v);
					}
					else if(f != -2 && u == -2 && b == -2 && d != -2)//UL corner
					{
						psiRight = Convert.ToDouble(dataSet.Tables[2].Rows[f][3]);
						psiDown = Convert.ToDouble(dataSet.Tables[2].Rows[d][3]);

						vx = (psi - psiDown) / dx;
						vy = -(psiRight - psi) / dx;

						v = Math.Sqrt((vx * vx + vy * vy));

						dataSet.Tables[3].Rows.Add(p, vx, vy, v);
					}
				}
				else
				{
					// upper left
					if (f <= dim && b > dim && u > dim && d <= dim)//(i - 1 < 0 && i - N < 0)
					{
						psiRight = Convert.ToDouble(dataSet.Tables[2].Rows[f][3]);
						psiDown = Convert.ToDouble(dataSet.Tables[2].Rows[d][3]);

						vx = (psi - psiDown) / dx;
						vy = -(psiRight - psi) / dx;

						v = Math.Sqrt((vx * vx + vy * vy));

						dataSet.Tables[3].Rows.Add(p, vx, vy, v);
					}
					//upper middle
					else if (f <= dim && b <= dim && u > dim && d <= dim)
					{
						psiRight = Convert.ToDouble(dataSet.Tables[2].Rows[f][3]);
						psiLeft = Convert.ToDouble(dataSet.Tables[2].Rows[b][3]);
						psiDown = Convert.ToDouble(dataSet.Tables[2].Rows[d][3]);

						vx = (psi - psiDown) / dx;
						vy = -(psiRight - psiLeft) / (2 * dx);

						v = Math.Sqrt((vx * vx + vy * vy));

						dataSet.Tables[3].Rows.Add(p, vx, vy, v);
					}
					//upper right
					else if (f > dim && b <= dim && u > dim && d <= dim)
					{
						psiLeft = Convert.ToDouble(dataSet.Tables[2].Rows[b][3]);
						psiDown = Convert.ToDouble(dataSet.Tables[2].Rows[d][3]);

						vx = (psi - psiDown) / dx;
						vy = -(psi - psiLeft) / dx;

						v = Math.Sqrt((vx * vx + vy * vy));

						dataSet.Tables[3].Rows.Add(p, vx, vy, v);
					}
					//middle left
					else if (f <= dim && b > dim && u <= dim && d <= dim)
					{
						psiRight = Convert.ToDouble(dataSet.Tables[2].Rows[f][3]);
						psiDown = Convert.ToDouble(dataSet.Tables[2].Rows[d][3]);
						psiUp = Convert.ToDouble(dataSet.Tables[2].Rows[u][3]);

						vx = (psiUp - psiDown) / (2 * dx);
						vy = -(psiRight - psi) / dx;

						v = Math.Sqrt((vx * vx + vy * vy));

						dataSet.Tables[3].Rows.Add(p, vx, vy, v);
					}
					//middle right
					else if (f > dim && b <= dim && u <= dim && d <= dim)
					{
						psiLeft = Convert.ToDouble(dataSet.Tables[2].Rows[b][3]);
						psiDown = Convert.ToDouble(dataSet.Tables[2].Rows[d][3]);
						psiUp = Convert.ToDouble(dataSet.Tables[2].Rows[u][3]);

						vx = (psiUp - psiDown) / (2 * dx);
						vy = -(psi - psiLeft) / dx;

						v = Math.Sqrt((vx * vx + vy * vy));

						dataSet.Tables[3].Rows.Add(p, vx, vy, v);
					}
					//lower left
					else if (f <= dim && b > dim && u <= dim && d > dim)
					{
						psiRight = Convert.ToDouble(dataSet.Tables[2].Rows[f][3]);
						psiUp = Convert.ToDouble(dataSet.Tables[2].Rows[u][3]);

						vx = (psiUp - psi) / dx;
						vy = -(psiRight - psi) / dx;

						v = Math.Sqrt((vx * vx + vy * vy));

						dataSet.Tables[3].Rows.Add(p, vx, vy, v);
					}
					//lower middle
					else if (f <= dim && b <= dim && u <= dim && d > dim)
					{
						psiRight = Convert.ToDouble(dataSet.Tables[2].Rows[f][3]);
						psiLeft = Convert.ToDouble(dataSet.Tables[2].Rows[b][3]);
						psiUp = Convert.ToDouble(dataSet.Tables[2].Rows[u][3]);

						vx = (psiUp - psi) / dx;
						vy = -(psiRight - psiLeft) / (2 * dx);

						v = Math.Sqrt((vx * vx + vy * vy));

						dataSet.Tables[3].Rows.Add(p, vx, vy, v);
					}
					//lower right
					else if (f > dim && b <= dim && u <= dim && d > dim)
					{
						psiLeft = Convert.ToDouble(dataSet.Tables[2].Rows[b][3]);
						psiUp = Convert.ToDouble(dataSet.Tables[2].Rows[u][3]);

						vx = (psiUp - psi) / dx;
						vy = -(psi - psiLeft) / dx;

						v = Math.Sqrt((vx * vx + vy * vy));

						dataSet.Tables[3].Rows.Add(p, vx, vy, v);
					}
					else
					{
						psiLeft = Convert.ToDouble(dataSet.Tables[2].Rows[b][3]);
						psiUp = Convert.ToDouble(dataSet.Tables[2].Rows[u][3]);
						psiDown = Convert.ToDouble(dataSet.Tables[2].Rows[d][3]);
						psiRight = Convert.ToDouble(dataSet.Tables[2].Rows[f][3]);

						vx = (psiUp - psiDown) / (2 * dx);
						vy = -(psiRight - psiLeft) / (2 * dx);

						v = Math.Sqrt((vx * vx + vy * vy));

						dataSet.Tables[3].Rows.Add(p, vx, vy, v);
					}
				}

			}

			//create pressure field
			double U0 = getU();
			double pressure, vel;
			double rho = 1.2723; //air density at 0°C 1bar, gotten from ~~ KRAUT B., Krautov strojniški priročnik, Ljubljana: Buča, 2017 (sixteenth revised edition) ~~
			foreach(DataRow dr in dataSet.Tables[3].Rows)
			{
				vel = Convert.ToDouble(dr[3]);
				pressure = rho * 0.5 * (U0 * U0 - Math.Abs(vel) * Math.Abs(vel));

				dr[4] = pressure;
			}
		}

		private void WriteMeshInfo()
		{
			string[] meshCoordinates = ImportMesh();
			foreach (string line in meshCoordinates)
			{
				rtbMeshInfo.AppendText(line + "\n");
			}
		}

		private void PostProcess()
		{
			postProcChart.ChartAreas.Clear();
			postProcChart.Series.Clear();
			postProcChart.ChartAreas.Add("1");
			postProcChart.Series.Add("1");
			postProcChart.Series["1"].ChartType = SeriesChartType.Point;
			postProcChart.Series["1"].MarkerSize = 5;

			postProcChart.ChartAreas["1"].AxisX.Minimum = Convert.ToDouble(dataSet.Tables[0].Rows[0][1]);
			postProcChart.ChartAreas["1"].AxisX.Maximum = Convert.ToDouble(dataSet.Tables[0].Rows[Convert.ToInt32(dataSet.Tables[0].Rows.Count - 1)][1]);
			postProcChart.ChartAreas["1"].AxisY.Minimum = Convert.ToDouble(dataSet.Tables[0].Rows[Convert.ToInt32(dataSet.Tables[0].Rows.Count - 1)][2]);
			postProcChart.ChartAreas["1"].AxisY.Maximum = Convert.ToDouble(dataSet.Tables[0].Rows[0][2]);
			double x, y, psi, color;
			int i = 0;
			Color colorArgbRed, colorArgbBlue;

			foreach (DataRow dr in dataSet.Tables[2].Rows)
			{
				x = Convert.ToDouble(dr[1]);
				y = Convert.ToDouble(dr[2]);
				psi = Convert.ToDouble(dr[3]);
				color = psi * 2;
				if(Math.Abs(color) > 255)
				{
					colorArgbRed = Color.Red;
					colorArgbBlue = Color.Blue;
				}
				else
				{
					colorArgbRed = Color.FromArgb(Math.Abs((int)color), Color.Red);
					colorArgbBlue = Color.FromArgb(Math.Abs((int)color), Color.Blue);
				}
				postProcChart.Series["1"].Points.AddXY(x, y);

				if (color > 0)
				{
					postProcChart.Series["1"].Points[i].Color = colorArgbRed;
				}
				else
				{
					postProcChart.Series["1"].Points[i].Color = colorArgbBlue;
				}
				i++;
			}
			/*
			using (TextWriter tw = new StreamWriter("psi.txt"))
			{
				string dr1str, dr2str, dr3str;
				CultureInfo info = CultureInfo.InvariantCulture;//new CultureInfo("en-US");//var separator = CultureInfo.GetCultureInfo("en-US");
				//NumberStyles style = NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands | NumberStyles.AllowLeadingSign;
				foreach (DataRow dr in dataSet.Tables[2].Rows)
				{
					dr1str = Convert.ToDouble(dr[1]).ToString("r", info);
					dr2str = Convert.ToDouble(dr[2]).ToString("r", info);
					dr3str = Convert.ToDouble(dr[3]).ToString("r", info);

					//dr1 = Decimal.Parse(dr1str, style, info);
					//dr2 = Decimal.Parse(dr2str, style, info);
					//dr3 = Decimal.Parse(dr3str, style, info);

					tw.Write(dr1str + "\t" + dr2str + "\t" + dr3str);
					tw.WriteLine();
				}
			}
			*/
		}

		private void Run()
		{
			Vector<double> psi = SolveSystemOfEq();
			
			int p;
			double x, y, psiVal;

			for (int i = 0; i < psi.Count; i++)
			{
				p = Convert.ToInt32(dataSet.Tables[1].Rows[i][0]);
				x = Convert.ToDouble(dataSet.Tables[4].Rows[i][1]);
				y = Convert.ToDouble(dataSet.Tables[4].Rows[i][2]);
				psiVal = psi[i];

				dataSet.Tables[2].Rows.Add(p, x, y, psiVal);
			}

			CreateVelocityField();
			WriteResults();

		}
		public string pngPathFinal;
		private void WriteResults()
		{
			CultureInfo info = CultureInfo.InvariantCulture;
			string numOfPoints = Convert.ToInt32(dataSet.Tables[2].Rows[Convert.ToInt32(dataSet.Tables[2].Rows.Count - 1)][0]).ToString();
			bool square = false;
			string dr1, dr2, dr3, dr4;
			int p, f, b, u, d;
			double dx = getDx();
			int N = Convert.ToInt32((int)(((Convert.ToDouble(dataSet.Tables[0].Rows[Convert.ToInt32(dataSet.Tables[0].Rows.Count - 1)][1])) + (Math.Abs(Convert.ToDouble(dataSet.Tables[0].Rows[0][1])))) / dx) - 1);
			int M = Convert.ToInt32((int)(Math.Abs(Convert.ToDouble(dataSet.Tables[0].Rows[0][2])) + Math.Abs(Convert.ToDouble(dataSet.Tables[0].Rows[Convert.ToInt32(dataSet.Tables[0].Rows.Count - 1)][2]))) / dx) - 2;
			if (dataSet.Tables[2].Rows.Count > 0)
			{
				SaveFileDialog sfd = new SaveFileDialog();
				sfd.Filter = "VTU (*.vtu)|*.vtu";

				string[] geometryLine = rtbMeshInfo.Lines[0].Split(' ');
				string geometry = geometryLine[0];

				if(geometry == "Cylinder:")
				{
					string[] resFileName = rtbMeshInfo.Lines[0].Split();
					sfd.FileName = "Cylinder_" + resFileName[2] + "_RES";
				}
				else
				{
					string[] resFileName = rtbMeshInfo.Lines[0].Split();
					sfd.FileName = "NACA_" + resFileName[5] + "_RES";
				}
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
							string[] outputCsv = new string[12 * dataSet.Tables[2].Rows.Count + 20];
							outputCsv[0] = "<?xml version=\"1.0\"?>";
							outputCsv[1] = "<VTKFile type=\"UnstructuredGrid\" version=\"0.1\" byte_order=\"BigEndian\" compressor=\"vtkZLibDataCompressor\">";
							outputCsv[2] = "<UnstructuredGrid>";
							//outputCsv[3] = "<Piece NumberOfPoints=\"" + numOfPoints + "\" NumberOfCells=\"" + numOfPoints + "\">";
							outputCsv[4] = "<PointData Vectors=\"u\" Scalars=\"psi,ux,uy,p\">";

							outputCsv[5] = "<DataArray type=\"Float32\" Name=\"u\" NumberOfComponents=\"3\" format=\"ascii\">";
							int i = 6;
							foreach (DataRow dr in dataSet.Tables[3].Rows)
							{
								outputCsv[i] = Convert.ToDouble(dr[1]).ToString("r", info) + " " + Convert.ToDouble(dr[2]).ToString("r", info) + " " + "0.0";
								i++;
							}
							outputCsv[i] = "</DataArray>";

							outputCsv[i + 1] = "<DataArray type=\"Float32\" Name=\"psi\" format=\"ascii\">";
							i = i + 2;
							foreach (DataRow dr in dataSet.Tables[2].Rows)
							{
								outputCsv[i] = Convert.ToDouble(dr[3]).ToString("r", info);
								i++;
							}
							outputCsv[i] = "</DataArray>";

							outputCsv[i + 1] = "<DataArray type=\"Float32\" Name=\"ux\" format=\"ascii\">";
							i = i + 2;
							foreach (DataRow dr in dataSet.Tables[3].Rows)
							{
								outputCsv[i] = Convert.ToDouble(dr[1]).ToString("r", info);
								i++;
							}
							outputCsv[i] = "</DataArray>";

							outputCsv[i + 1] = "<DataArray type=\"Float32\" Name=\"uy\" format=\"ascii\">";
							i = i + 2;
							foreach (DataRow dr in dataSet.Tables[3].Rows)
							{
								outputCsv[i] = Convert.ToDouble(dr[2]).ToString("r", info);
								i++;
							}
							outputCsv[i] = "</DataArray>";

							outputCsv[i + 1] = "<DataArray type=\"Float32\" Name=\"p\" format=\"ascii\">";
							i = i + 2;
							foreach (DataRow dr in dataSet.Tables[3].Rows)
							{
								outputCsv[i] = Convert.ToDouble(dr[4]).ToString("r", info);
								i++;
							}
							outputCsv[i] = "</DataArray>";
							outputCsv[i + 1] = "</PointData>";

							outputCsv[i + 2] = "<Points>";
							outputCsv[i + 3] = "<DataArray type=\"Float32\" NumberOfComponents=\"3\" format=\"ascii\">";
							i = i + 4;
							foreach (DataRow dr in dataSet.Tables[2].Rows)
							{
								outputCsv[i] = Convert.ToDouble(dr[1]).ToString("r", info) + " " + Convert.ToDouble(dr[2]).ToString("r", info) + " " + "0.0";
								i++;
							}
							outputCsv[i] = "</DataArray>";
							outputCsv[i + 1] = "</Points>";
							outputCsv[i + 2] = "<Cells>";
							outputCsv[i + 3] = "<DataArray type=\"Int32\" Name=\"connectivity\" format=\"ascii\">";
							i = i + 4;
							int k = i;
							// SQUARE MESH
							square:
							if(square == true)
							{
								rtbInfo.AppendText("\nTriangular Cell Generation Failed");
								rtbInfo.AppendText("\n");
								rtbInfo.AppendText("\nSwitching to Square Mesh...");

								i = k;
								int g = 0;
								foreach (DataRow dr in dataSet.Tables[1].Rows)
								{
									p = Convert.ToInt32(dr[0]) - 1;
									f = Convert.ToInt32(dr[1]) - 1;
									b = Convert.ToInt32(dr[2]) - 1;
									u = Convert.ToInt32(dr[3]) - 1;
									d = Convert.ToInt32(dr[4]) - 1;

									if (f > Convert.ToInt32(numOfPoints) || d > Convert.ToInt32(numOfPoints) || f == -2 || d == -2 || (Convert.ToInt32(dataSet.Tables[1].Rows[g + 1][4]) - 1) == -2)
									{
										g++;
										continue;
									}
									else
									{
										dr1 = p.ToString();
										dr2 = f.ToString();
										dr3 = (Convert.ToInt32(dataSet.Tables[1].Rows[g + 1][4]) - 1).ToString();
										dr4 = d.ToString();

										outputCsv[i] = dr1 + " " + dr2 + " " + dr3 + " " + dr4;
										i++;

										g++;
									}
								}
								rtbInfo.AppendText("\nSquare Cells Generated Successfully");
							}
							else
							{
								rtbInfo.AppendText("\n");
								rtbInfo.AppendText("\nGENERATING RESULTS");
								rtbInfo.AppendText("\nTriangular Cell Generation Started...");

								// TRIANGULAR MESH
								for (int j = 0; j < Convert.ToInt32(dataSet.Tables[2].Rows.Count); j++)
								{
									p = Convert.ToInt32(dataSet.Tables[1].Rows[j][0]) - 1;
									f = Convert.ToInt32(dataSet.Tables[1].Rows[j][1]) - 1;
									b = Convert.ToInt32(dataSet.Tables[1].Rows[j][2]) - 1;
									u = Convert.ToInt32(dataSet.Tables[1].Rows[j][3]) - 1;
									d = Convert.ToInt32(dataSet.Tables[1].Rows[j][4]) - 1;
									//detect domain boundary
									if (f > Convert.ToInt32(numOfPoints) || d > Convert.ToInt32(numOfPoints))
									{
										continue;
									}
									else
									{
										if (f == -2 && b != -2 && u != -2 && d != -2)
										{
											dr1 = p.ToString();
											dr2 = d.ToString();
											dr3 = (d - 1).ToString();

											if (dr1 == "-2" || dr2 == "-2" || dr3 == "-2")
											{
												square = true;
												goto square;
											}

											outputCsv[i] = dr1 + " " + dr2 + " " + dr3;
											i++;

											continue;
										}
										if (d == -2)
										{
											if (Convert.ToInt32(dataSet.Tables[1].Rows[j + 1][4]) - 1 != -2)
											{
												dr1 = p.ToString();
												dr2 = f.ToString();
												dr3 = (Convert.ToInt32(dataSet.Tables[1].Rows[j + 1][4]) - 1).ToString();

												if (dr1 == "-2" || dr2 == "-2" || dr3 == "-2")
												{
													square = true;
													goto square;
												}

												outputCsv[i] = dr1 + " " + dr2 + " " + dr3;
												i++;

												continue;
											}
											else
											{
												continue;
											}
										}
										if (f == -2 && u == -2)
										{
											if (Convert.ToInt32(dataSet.Tables[1].Rows[j - 1][3]) - 1 == -2)
											{
												dr1 = p.ToString();
												dr2 = (d + 1).ToString();
												dr3 = d.ToString();

												if (dr1 == "-2" || dr2 == "-2" || dr3 == "-2")
												{
													square = true;
													goto square;
												}

												outputCsv[i] = dr1 + " " + dr2 + " " + dr3;
												i++;

												continue;
											}
											else
											{
												dr1 = p.ToString();
												dr2 = b.ToString();
												dr3 = (Convert.ToInt32(dataSet.Tables[1].Rows[j - 1][3]) - 1).ToString();

												if (dr1 == "-2" || dr2 == "-2" || dr3 == "-2")
												{
													square = true;
													goto square;
												}

												outputCsv[i] = dr1 + " " + dr2 + " " + dr3;
												i++;

												continue;
											}
										}
										if (b == -2 && u == -2)
										{
											dr1 = p.ToString();
											dr2 = d.ToString();
											dr3 = (d - 1).ToString();

											if (dr1 == "-2" || dr2 == "-2" || dr3 == "-2")
											{
												square = true;
												goto square;
											}

											outputCsv[i] = dr1 + " " + dr2 + " " + dr3;
											i++;

											goto cont;
										}
										cont:
										dr1 = p.ToString();
										dr2 = f.ToString();
										dr3 = d.ToString();

										if (dr1 == "-2" || dr2 == "-2" || dr3 == "-2")
										{
											square = true;
											goto square;
										}

										outputCsv[i] = dr1 + " " + dr2 + " " + dr3;
										i++;

										p = Convert.ToInt32(dataSet.Tables[1].Rows[j + 1][0]) - 1;
										f = Convert.ToInt32(dataSet.Tables[1].Rows[j + 1][1]) - 1;
										b = Convert.ToInt32(dataSet.Tables[1].Rows[j + 1][2]) - 1;
										u = Convert.ToInt32(dataSet.Tables[1].Rows[j + 1][3]) - 1;
										d = Convert.ToInt32(dataSet.Tables[1].Rows[j + 1][4]) - 1;

										if ((f == -2 && u != -2) || (d == -2))
										{
											continue;
										}

										dr1 = p.ToString();
										dr2 = d.ToString();

										if (dr1 == "-2" || dr2 == "-2" || dr3 == "-2")
										{
											square = true;
											goto square;
										}

										outputCsv[i] = dr1 + " " + dr2 + " " + dr3;
										i++;
									}
								}
								rtbInfo.AppendText("\nTriangular Cells Generated Successfully");
							}
							int l = i - k;
							outputCsv[3] = "<Piece NumberOfPoints=\"" + numOfPoints + "\" NumberOfCells=\"" + l + "\">";
							outputCsv[i] = "</DataArray>";

							outputCsv[i + 1] = "<DataArray type=\"UInt32\" Name=\"offsets\" format=\"ascii\">";
							i = i + 2;
							if(square == true)
							{
								for (int j = 1; j <= l; j++)
								{
									outputCsv[i] = (4 * j).ToString();
									i++;
								}
							}
							else
							{
								for (int j = 1; j <= l; j++)
								{
									outputCsv[i] = (3 * j).ToString();
									i++;
								}
							}
							outputCsv[i] = "</DataArray>";

							outputCsv[i + 1] = "<DataArray type=\"UInt32\" Name=\"types\" format=\"ascii\">";
							i = i + 2;
							if(square == true)
							{
								for (int j = 0; j < l; j++)
								{
									outputCsv[i] = "9";
									i++;
								}
							}
							else
							{
								for (int j = 0; j < l; j++)
								{
									outputCsv[i] = "5";
									i++;
								}

							}
							outputCsv[i] = "</DataArray>";
							outputCsv[i + 1] = "</Cells>";

							outputCsv[i + 2] = "</Piece>";
							outputCsv[i + 3] = "</UnstructuredGrid>";
							outputCsv[i + 4] = "</VTKFile>";

							File.WriteAllLines(sfd.FileName, outputCsv, Encoding.UTF8);

							// run paraview
							rtbInfo.AppendText("\n");
							rtbInfo.AppendText("\nRunning paraview...");
							string[] pyCode = getPyCode();

							pyCode[11] = "\nresult = XMLUnstructuredGridReader(registrationName='results', FileName=[r'" + sfd.FileName + "'])";

							string substring1 = "_RES.vtu";
							int indexOfSubstring1 = sfd.FileName.IndexOf(substring1);
							pngPathFinal = sfd.FileName.Remove(indexOfSubstring1, substring1.Length);

							pyCode[100] = "\nSaveScreenshot(r'" + pngPathFinal + "_u_scalarField.png', renderView1, ImageResolution=[3224, 1104], TransparentBackground=1)";
							pyCode[133] = "\nSaveScreenshot(r'" + pngPathFinal + "_ux_scalarField.png', renderView1, ImageResolution=[3224, 1104], TransparentBackground=1)";
							pyCode[166] = "\nSaveScreenshot(r'" + pngPathFinal + "_uy_scalarField.png', renderView1, ImageResolution=[3224, 1104], TransparentBackground=1)";
							pyCode[199] = "\nSaveScreenshot(r'" + pngPathFinal + "_p_scalarField.png', renderView1, ImageResolution=[3224, 1104], TransparentBackground=1)";
							pyCode[229] = "\nSaveScreenshot(r'" + pngPathFinal + "_psi_scalarField.png', renderView1, ImageResolution=[3224, 1104], TransparentBackground=1)";
							pyCode[307] = "\nSaveScreenshot(r'" + pngPathFinal + "_streamlines.png', renderView1, ImageResolution=[3224, 1104], TransparentBackground=1)";
							pyCode[393] = "\nSaveScreenshot(r'" + pngPathFinal + "_u_vectorField.png', renderView1, ImageResolution=[3224, 1104], TransparentBackground=1)";

							string substring = "_RES.vtu";
							int indexOfSubstring = sfd.FileName.IndexOf(substring);
							string pyCodeFilename = sfd.FileName.Remove(indexOfSubstring, substring.Length); 

							StreamWriter sw = new StreamWriter(pyCodeFilename + "_paraview_exec.py");
							foreach(string line in pyCode)
							{
								sw.WriteLine(line);
							}
							sw.Close();

							string command = "/C pvpython.exe " + pyCodeFilename + "_paraview_exec.py";
							System.Diagnostics.Process.Start("CMD.exe", command);

							rtbInfo.AppendText("Results generated successfully");

							gbResults.Visible = true;
						}
						catch (Exception ex)
						{
							MessageBox.Show("Error :" + ex.Message);
						}

					}
				}
			}
			else
			{
				MessageBox.Show("No Record To Export", "Info");
			}

			/*
			using (TextWriter tw = new StreamWriter("psi.txt"))
			{
				string dr1str, dr2str, dr3str;
				foreach (DataRow dr in dataSet.Tables[2].Rows)
				{
					dr1str = Convert.ToDouble(dr[1]).ToString("r", info);
					dr2str = Convert.ToDouble(dr[2]).ToString("r", info);
					dr3str = Convert.ToDouble(dr[3]).ToString("r", info);
					tw.Write(dr1str + "\t" + dr2str + "\t" + dr3str);
					tw.WriteLine();
				}
			}
			*/
		}

		#endregion

		#region form elements
		private async void btnImportMesh_Click(object sender, EventArgs e)
		{
			WriteMeshInfo();

			await Task.Delay(100);
			changeLine(rtbInfo, 1, "DONE");

			btnRun.Enabled = true;
			btClearMesh.Enabled = true;
		}


		private void btnRun_Click(object sender, EventArgs e)
		{
			Run();
			btnDelete.Enabled = true;
		}
		private void btnDelete_Click_1(object sender, EventArgs e)
		{
			var result = MessageBox.Show("Are you sure you want to delete current results?", "Clear results", MessageBoxButtons.YesNo);

			if (result == DialogResult.Yes)
			{
				rtbInfo.Clear();
				gbResults.Visible = false;
				pbResult.Image = null;
				dataSet.Tables[2].Clear();
				dataSet.Tables[3].Clear();
				dataSet.Tables[4].Clear();
				dataSet.Tables[5].Clear();

				btnDelete.Enabled = false;
			}
		}
		private void ll_u_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			pbResult.Image = null;
			pbResult.Visible = true;
			pbResult.Image = Image.FromFile(pngPathFinal + "_u_scalarField.png");
			pbResult.BackColor = Color.Black;
			ll_u.BackColor = Color.Red;
			ll_ux.BackColor = Color.Transparent;
			ll_uy.BackColor = Color.Transparent;
			ll_p.BackColor = Color.Transparent;
			ll_psi.BackColor = Color.Transparent;
			ll_streamlines.BackColor = Color.Transparent;
			ll_uVect.BackColor = Color.Transparent;
		}
		private void ll_ux_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			pbResult.Image = null;
			pbResult.Visible = true;
			pbResult.Image = Image.FromFile(pngPathFinal + "_ux_scalarField.png");
			pbResult.BackColor = Color.Black;
			ll_u.BackColor = Color.Transparent;
			ll_ux.BackColor = Color.Red;
			ll_uy.BackColor = Color.Transparent;
			ll_p.BackColor = Color.Transparent;
			ll_psi.BackColor = Color.Transparent;
			ll_streamlines.BackColor = Color.Transparent;
			ll_uVect.BackColor = Color.Transparent;
		}
		private void ll_uy_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			pbResult.Image = null;
			pbResult.Visible = true;
			pbResult.Image = Image.FromFile(pngPathFinal + "_uy_scalarField.png");
			pbResult.BackColor = Color.Black;
			ll_u.BackColor = Color.Transparent;
			ll_ux.BackColor = Color.Transparent;
			ll_uy.BackColor = Color.Red;
			ll_p.BackColor = Color.Transparent;
			ll_psi.BackColor = Color.Transparent;
			ll_streamlines.BackColor = Color.Transparent;
			ll_uVect.BackColor = Color.Transparent;
		}
		private void ll_p_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			pbResult.Image = null;
			pbResult.Visible = true;
			pbResult.Image = Image.FromFile(pngPathFinal + "_p_scalarField.png");
			pbResult.BackColor = Color.Black;
			ll_u.BackColor = Color.Transparent;
			ll_ux.BackColor = Color.Transparent;
			ll_uy.BackColor = Color.Transparent;
			ll_p.BackColor = Color.Red;
			ll_psi.BackColor = Color.Transparent;
			ll_streamlines.BackColor = Color.Transparent;
			ll_uVect.BackColor = Color.Transparent;
		}
		private void ll_psi_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			pbResult.Image = null;
			pbResult.Visible = true;
			pbResult.Image = Image.FromFile(pngPathFinal + "_psi_scalarField.png");
			pbResult.BackColor = Color.Black;
			ll_u.BackColor = Color.Transparent;
			ll_ux.BackColor = Color.Transparent;
			ll_uy.BackColor = Color.Transparent;
			ll_p.BackColor = Color.Transparent;
			ll_psi.BackColor = Color.Red;
			ll_streamlines.BackColor = Color.Transparent;
			ll_uVect.BackColor = Color.Transparent;
		}
		private void ll_streamlines_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			pbResult.Image = null;
			pbResult.Visible = true;
			pbResult.Image = Image.FromFile(pngPathFinal + "_streamlines.png");
			pbResult.BackColor = Color.White;
			ll_u.BackColor = Color.Transparent;
			ll_ux.BackColor = Color.Transparent;
			ll_uy.BackColor = Color.Transparent;
			ll_p.BackColor = Color.Transparent;
			ll_psi.BackColor = Color.Transparent;
			ll_streamlines.BackColor = Color.Red;
			ll_uVect.BackColor = Color.Transparent;
		}
		private void ll_uVect_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			pbResult.Image = null;
			pbResult.Visible = true;
			pbResult.Image = Image.FromFile(pngPathFinal + "_u_vectorField.png");
			pbResult.BackColor = Color.Black;
			ll_u.BackColor = Color.Transparent;
			ll_ux.BackColor = Color.Transparent;
			ll_uy.BackColor = Color.Transparent;
			ll_p.BackColor = Color.Transparent;
			ll_psi.BackColor = Color.Transparent;
			ll_streamlines.BackColor = Color.Transparent;
			ll_uVect.BackColor = Color.Red;
		}
		private void btClearMesh_Click(object sender, EventArgs e)
		{
			var result = MessageBox.Show("Are you sure you want to delete current mesh?", "Clear mesh", MessageBoxButtons.YesNo);

			if (result == DialogResult.Yes)
			{
				rtbInfo.Clear();
				rtbMeshInfo.Clear();
				gbResults.Visible = false;
				pbResult.Image = null;
				dataSet.Tables[0].Clear();
				dataSet.Tables[1].Clear();
				dataSet.Tables[2].Clear();
				dataSet.Tables[3].Clear();
				dataSet.Tables[4].Clear();
				dataSet.Tables[5].Clear();

				btnRun.Enabled = false;
				btnDelete.Enabled = false;
				btClearMesh.Enabled = false;
			}
		}

		#endregion
	}
}
