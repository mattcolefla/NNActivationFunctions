////////////////////////////////////////////////////////////////////////////////////////////////////
// file:	Form1.cs
//
// summary:	Implements the form 1 class
////////////////////////////////////////////////////////////////////////////////////////////////////

using ActivationFnBenchmarks;
using System;
using System.Drawing;
using System.Windows.Forms;
using ZedGraph;
using System.IO;
using System.Text.RegularExpressions;


namespace ActivationFunctionViewer
{
    /// <summary>   A form 1. </summary>
    public partial class Form1 : Form
    {
        /// <summary>   Default constructor. </summary>
        internal Form1()
        {
            InitializeComponent();
            PlotAllFunctions();
        }


        /// <summary>   Plot all functions. </summary>
        private void PlotAllFunctions()
        {
            // First, clear out any old GraphPane's from the MasterPane collection
            MasterPane master = zed.MasterPane;
            master.PaneList.Clear();

            // Display the MasterPane Title, and set the outer margin to 10 points
            master.Title.IsVisible = true;
            //master.Title.Text = "Activation Function Plots";
            master.Margin.All = 10;

            // Create some GraphPane's (normally you would add some curves too
            Plot(ActivationFunctions.LogisticApproximantSteep, "LogisticApproximantSteep");
            Plot(ActivationFunctions.LogisticFunctionSteep, "LogisticFunctionSteep");
            Plot(ActivationFunctions.PolynomialApproximant, "PolynomialApproximant");
            Plot(ActivationFunctions.QuadraticSigmoid, "QuadraticSigmoid");
            Plot(ActivationFunctions.SoftSign, "SoftSign");
            Plot(ActivationFunctions.ReLu, "ReLU");
            Plot(ActivationFunctions.LeakyReLu, "LeakyReLU");
            Plot(ActivationFunctions.LeakyReLuShifted, "LeakyReLUShifted");
            Plot(ActivationFunctions.SreLu, "S-ShapedReLU");
            Plot(ActivationFunctions.SreLuShifted, "S-ShapedReLUShifted");
            Plot(ActivationFunctions.ParametericReLU, "ParametericReLU");
            Plot(ActivationFunctions.ArcTan, "ArcTan");
            Plot(ActivationFunctions.TanH, "TanH");
            Plot(ActivationFunctions.ArcSinH, "ArcSinH");
            Plot(ActivationFunctions.ScaledElu, "ScaledExponentialLinearUnit");
            Plot(ActivationFunctions.MaxMinusOne, "MaxMinusOne");

            // Refigure the axis ranges for the GraphPanes
            zed.AxisChange();

            // Layout the GraphPanes using a default Pane Layout
            using (Graphics g = CreateGraphics())
            {
                master.SetLayout(g, PaneLayout.SquareColPreferred);
            }
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Plots. </summary>
        ///
        /// <param name="fn">       The function. </param>
        /// <param name="fnName">   The name. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        private void Plot(Func<double, double> fn, string fnName)
        {
            const double xmin = -2.0;
            const double xmax = 2.0;
            const int resolution = 2000;

            zed.IsShowPointValues = true;
            zed.PointValueFormat = "e";

            GraphPane pane = new GraphPane();
            MasterPane master = zed.MasterPane;

            pane.XAxis.IsVisible = true;
            pane.YAxis.IsVisible = true;
            pane.XAxis.MajorGrid.IsVisible = true;
            pane.YAxis.MajorGrid.IsVisible = true;
            pane.Title.Text = string.Join(" ", Regex.Split(fnName, @"(?<!^)(?=[A-Z](?![A-Z]|$))"));
            pane.YAxis.Title.Text = string.Empty;
            pane.XAxis.Title.Text = string.Empty;
            pane.XAxis.MajorTic.IsOpposite = false;
            pane.XAxis.MinorTic.IsOpposite = false;
            pane.YAxis.MajorTic.IsOpposite = false;
            pane.YAxis.MinorTic.IsOpposite = false;
            pane.YAxis.Scale.IsSkipFirstLabel = true;

            // Set the Y axis intersect the X axis at an X value of 0.0
            pane.YAxis.Cross = 0.0;
            // Turn off the axis frame and all the opposite side tics
            pane.Chart.Border.IsVisible = false;

            double[] xarr = new double[resolution];
            double[] yarr = new double[resolution];
            double incr = (xmax - xmin) / resolution;
            double x = xmin;

            for(int i=0; i < resolution; i++, x+=incr)
            {
                xarr[i] = x;
                yarr[i] = fn(x);
            }

            PointPairList list1 = new PointPairList(xarr, yarr);
            LineItem li = pane.AddCurve(string.Empty, list1, Color.Black, SymbolType.Diamond);
            li.Symbol.Fill = new Fill(Color.White);
            pane.Chart.Fill = new Fill(Color.White, Color.LightGoldenrodYellow, 45.0F);
            master.Add(pane);


            zed.AxisChange();
        }
    }
}
