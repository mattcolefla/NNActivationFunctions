
////////////////////////////////////////////////////////////////////////////////////////////////////
// file:	ActivationFunctions.cs
//
// summary:	Activation Functions
////////////////////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Runtime.CompilerServices;

namespace ActivationFunctionViewer
{
    /// <summary>   A functions. </summary>
    internal static class ActivationFunctions
    {
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Logistic function steep. </summary>
        ///
        /// <param name="x">    The x coordinate. </param>
        ///
        /// <returns>   A double. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double LogisticFunctionSteep(double x)
        {
            return 1.0 / (1.0 + Math.Exp(-4.9 * x));
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Logistic approximant steep. </summary>
        ///
        /// <param name="x">    The x coordinate. </param>
        ///
        /// <returns>   A double. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double LogisticApproximantSteep(double x)
        {
            return 1.0 / (1.0 + Exp(-4.9 * x));
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Logistic sigmoid. </summary>
        ///
        /// <param name="x">    The x coordinate. </param>
        ///
        /// <returns>   A double. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double LogisticSigmoid(double x)
        {
            if (x < -40.0)
                return 0.0;
            if (x > 40.0)
                return 1.0;
            return 1.0 / (1.0 + Math.Exp(-x));
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Soft sign. </summary>
        ///
        /// <param name="x">    The x coordinate. </param>
        ///
        /// <returns>   A double. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double SoftSign(double x)
        {
            return 0.5 + (x / (2.0 * (0.2 + Math.Abs(x))));
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary> PolynomialApproximant </summary>
        ///
        /// <param name="x">    The x coordinate. </param>
        ///
        /// <returns>   A double. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double PolynomialApproximant(double x)
        {
            // Very close approximation to LogisticFunctionSteep that avoids exp.
            x = x * 4.9;
            double x2 = x * x;
            double e = 1.0 + Math.Abs(x) + x2 * 0.555 + x2 * x2 * 0.143;
            double f = (x > 0) ? (1.0 / e) : e;
            return 1.0 / (1.0 + f);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Quadratic sigmoid. </summary>
        ///
        /// <param name="x">    The x coordinate. </param>
        ///
        /// <returns>   A double. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double QuadraticSigmoid(double x)
        {
            const double t = 0.999;
            const double a = 0.00001;
            double y;
            double sign = Math.Sign(x);

            x = Math.Abs(x);

            if (x >= 0 && x < t)
            {
                y = t - ((x - t) * (x - t));
            }
            else //if (x >= t) 
            {
                y = t + (x - t) * a;
            }

            return (y * sign * 0.5) + 0.5;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Rectified linear activation unit (ReLU). 
        /// </summary>
        ///
        /// <param name="x">    . </param>
        ///
        /// <returns>   A double. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double ReLu(double x)
        {
            return (x > 0.0 ? x : 0.0);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Leaky rectified linear activation unit (ReLU). 
        /// </summary>
        ///
        /// <param name="x">    . </param>
        ///
        /// <returns>   A double. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double LeakyReLu(double x)
        {
            return ((x > 0.0) ? x : x * 0.001);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Leaky rectified linear activation unit (ReLU).
        /// </summary>
        ///
        /// <param name="x">    . </param>
        ///
        /// <returns>   A double. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double LeakyReLuShifted(double x)
        {
            const double offset = 0.5;
            return ((x + offset > 0.0) ? x + offset : (x + offset) * 0.001);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// S-shaped rectified linear activation unit (SReLU). 
        /// </summary>
        ///
        /// <param name="x">    . </param>
        ///
        /// <returns>   A double. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double SreLu(double x)
        {
            const double tl = 0.001; // threshold (left).
            const double tr = 0.999; // threshold (right).
            double y;

            if (x > tl && x < tr)
            {
                y = x;
            }
            else if (x <= tl)
            {
                y = tl + (x - tl) * 0.00001;
            }
            else
            {
                y = tr + (x - tr) * 0.00001;
            }

            return y;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Relu shifted. </summary>
        ///
        /// <param name="x">    The x coordinate. </param>
        ///
        /// <returns>   A double. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double SreLuShifted(double x)
        {
            const double tl = 0.001; // threshold (left).
            const double tr = 0.999; // threshold (right).
            const double offset = 0.5;

            double y;
            if (x + offset > tl && x + offset < tr)
            {
                y = x + offset;
            }
            else if (x + offset <= tl)
            {
                y = tl + ((x + offset) - tl) * 0.00001;
            }
            else
            {
                y = tr + ((x + offset) - tr) * 0.00001;
            }

            return y;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Arc tangent. </summary>
        ///
        /// <param name="x">    The x coordinate. </param>
        ///
        /// <returns>   A double. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double ArcTan(double x)
        {
            return (Math.Atan(x) + Math.PI / 2.0) * 1.0 / Math.PI;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   HTangent. </summary>
        ///
        /// <param name="x">    The x coordinate. </param>
        ///
        /// <returns>   A double. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double TanH(double x)
        {
            return (Math.Tanh(x) + 1.0) * 0.5;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Arcsinh. </summary>
        ///
        /// <param name="x">    The x coordinate. </param>
        ///
        /// <returns>   A double. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double ArcSinH(double x)
        {
            return 1.2567348023993685 * ((Asinh(x) + 1.0) * 0.5);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Fast exp approximation, from: https://stackoverflow.com/a/412988/15703
        /// https://pdfs.semanticscholar.org/35d3/2b272879a2018a2d33d982639d4be489f789.pdf (A Fast,
        /// Compact Approximation of the Exponential Function)
        /// </summary>
        ///
        /// <param name="val">  The value. </param>
        ///
        /// <returns>   A double. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static double Exp(double val)
        {
            long tmp = (long)(1512775 * val + (1072693248 - 60801));
            return BitConverter.Int64BitsToDouble(tmp << 32);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Scaled Exponential Linear Unit (SELU).
        /// 
        /// From:
        ///     Self-Normalizing Neural Networks https://arxiv.org/abs/1706.02515
        /// 
        /// Original source code (including parameter values):
        ///     https://github.com/bioinf-jku/SNNs/blob/master/selu.py.
        /// </summary>
        ///
        /// <param name="x">    The x coordinate. </param>
        ///
        /// <returns>   A double. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double ScaledElu(double x)
        {
            double alpha = 1.6732632423543772848170429916717;
            double scale = 1.0507009873554804934193349852946;

            return ((x >= 0) ? scale * x : scale * ((alpha * Math.Exp(x)) - alpha));
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Maximum minus one. </summary>
        ///
        /// <param name="x">    The x coordinate. </param>
        ///
        /// <returns>   A double. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double MaxMinusOne(double x)
        {
            return (x > -1) ? x : -1;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Binary step. </summary>
        ///
        /// <param name="x">    The x coordinate. </param>
        ///
        /// <returns>   A double. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double BinaryStep(double x)
        {
            return x < 0 ? 0 : 1;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Parameteric Rectified Linear Unit. </summary>
        ///
        /// <param name="x">    The x coordinate. </param>
        /// <param name="a">    A double to process. </param>
        ///
        /// <returns>   A double. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double ParametericReLu(double x, double a)
        {
            return x < 0 ? a * x : x;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Bent identity. </summary>
        ///
        /// <param name="x">    The x coordinate. </param>
        ///
        /// <returns>   A double. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double BentIdentity(double x)
        {
            return (((Math.Sqrt(Math.Pow(x, 2) + 1)) - 1) / 2) + x;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Hyperbolic Area Sine. </summary>
        ///
        /// <param name="value">    The real value. </param>
        ///
        /// <returns>   The hyperbolic angle, i.e. the area of its hyperbolic sector. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static double Asinh(double value)
        {
            return Math.Log(value + Math.Sqrt((value * value) + 1), Math.E);
        }


        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Parameteric Rectified Linear Unit. </summary>
        ///
        /// <param name="x">    The x coordinate. </param>
        /// <param name="a">    A double to process. </param>
        ///
        /// <returns>   A double. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double ParametericReLU(double x)
        {
            return x < 0 ? 2.365 * x : x;
        }
    }
}
