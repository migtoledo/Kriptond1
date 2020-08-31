namespace Kriptond1
{
    /// <summary>
    /// Defines the <see cref="Globals" />.
    /// Screen (XX , YY) -> MathSpace (X , Y)
    /// MathSpace (X , Y) -> Screen (XX , YY).
    /// </summary>
    public static class Globals
    {

        public static bool ShowXYLabels = false;

        /// <summary>
        /// Defines the MouseLabelVisible.
        /// </summary>
        public static bool MouseLabelVisible = false;

        /// <summary>
        /// Defines the RebuildGridH.
        /// </summary>
        public static bool RebuildGridH = false;

        /// <summary>
        /// Defines the RebuildGridV.
        /// </summary>
        public static bool RebuildGridV = false;

        /// <summary>
        /// Defines the DT_BackgroundTimer.
        /// </summary>
        public static double DT_BackgroundTimer = 20;

        /// <summary>
        /// Defines the k1.
        /// </summary>
        public static double k1 = 2;

        /// <summary>
        /// Defines the k2.
        /// </summary>
        public static double k2 = 1 / k1;

        /// <summary>
        /// Defines the DT_ForegroundTimer.
        /// </summary>
        public static double DT_ForegroundTimer = DT_BackgroundTimer * k2;

        /// <summary>
        /// Defines the GraphicsCanvasWidth.
        /// </summary>
        public static double GraphicsCanvasWidth = 0;

        /// <summary>
        /// Defines the GraphicsCanvasHeight.
        /// </summary>
        public static double GraphicsCanvasHeight = 0;

        /// <summary>
        /// Defines the Width.
        /// </summary>
        public static double Width = 0;

        /// <summary>
        /// Defines the Height.
        /// </summary>
        public static double Height = 0;

        /// <summary>
        /// Defines the XXmin. Screen.........
        /// </summary>
        public static double XXmin = 0;

        /// <summary>
        /// Defines the YYmin. Screen........
        /// </summary>
        public static double YYmin = 0;

        /// <summary>
        /// Defines the XXmax. Screen........
        /// </summary>
        public static double XXmax = 0;

        /// <summary>
        /// Defines the YYmax. Screen........
        /// </summary>
        public static double YYmax = 0;

        /// <summary>
        /// Defines the OX.
        /// Relative X-Origin, in units.....
        /// </summary>
        internal static double OX = 0;

        /// <summary>
        /// Defines the OY.
        /// Relative Y-Origin, in units.....
        /// </summary>
        internal static double OY = 0;

        /// <summary>
        /// Defines the XX0. Position of the relative origin (0, 0)
        /// in pixels...
        /// </summary>
        internal static double XX0 = 0;

        /// <summary>
        /// Defines the YY0. Position of the relative origin (0, 0)
        /// in pixels...
        /// </summary>
        internal static double YY0 = 0;

        /// <summary>
        /// Defines the OXX. Screen........
        /// </summary>
        public static double OXX = 0;

        /// <summary>
        /// Defines the OYY. Screen........
        /// </summary>
        public static double OYY = 0;

        /// <summary>
        /// Defines the XMAX.
        /// Max. X Math Space.
        /// </summary>
        internal static double XMAX = 0;

        /// <summary>
        /// Defines the XMIN. Math Space.
        /// </summary>
        internal static double XMIN = 0;

        /// <summary>
        /// Defines the YMAX. Math Space.
        /// </summary>
        internal static double YMAX = 0;

        /// <summary>
        /// Defines the YMIN. Math Space.
        /// </summary>
        internal static double YMIN = 0;

        /// <summary>
        /// Defines the MouseX.
        /// </summary>
        public static double MouseX = 0;

        /// <summary>
        /// Defines the MouseY.
        /// </summary>
        public static double MouseY = 0;

        /// <summary>
        /// Defines the XX. Screen........
        /// </summary>
        public static double XX = 0;

        /// <summary>
        /// Defines the YY. Screen........
        /// </summary>
        public static double YY = 0;

        /// <summary>
        /// Defines the X. Math Space........
        /// </summary>
        public static double X = 0;

        /// <summary>
        /// Defines the Y. Math Space........
        /// </summary>
        public static double Y = 0;

        /// <summary>
        /// Defines the sx.
        /// X Scale : pixels / ux......
        /// </summary>
        public static double sx = 60;

        /// <summary>
        /// Defines the sy.
        /// Y Scale : pixels / uy......
        /// </summary>
        public static double sy = 60;

        /// <summary>
        /// The UpdateXYMaxMin.
        /// </summary>
        internal static void UpdateXYMaxMin()
        {
            XMAX = OX + (1 / sx) * OXX;

            YMAX = OY + (1 / sy) * OYY;

            XMIN = OX - (1 / sx) * OXX;

            YMIN = OY - (1 / sy) * OYY;
        }


        public static double GetX( double XX)
        {
            return (OX + (XX - OXX) / sx);
        }



        public static double GetY( double YY)
        {
            return (OY + (OYY - YY) / sy);
        }


        /// <summary>
        /// The UpdateXY.
        /// </summary>
        public static void UpdateXY()
        {

            UpdateXYMaxMin();

            XX = MouseX;
            YY = MouseY;

            X = OX + (XX - OXX) / sx;
            Y = OY + (OYY - YY) / sy;

            // Relative origin in pixels (0, 0)
            XX0 = OXX - sx * OX;
            YY0 = OYY + sy * OY;
        }

        /// <summary>
        /// Defines the ShowAxis.
        /// </summary>
        public static bool ShowAxis = true;

        /// <summary>
        /// Defines the ShowGridLines.
        /// </summary>
        public static bool ShowGridLines = false;

        /// <summary>
        /// Defines the XScaleChanged.
        /// </summary>
        public static bool XScaleChanged = false;

        /// <summary>
        /// Defines the YScaleChanged.
        /// </summary>
        public static bool YScaleChanged = false;

        /// <summary>
        /// Defines the naturalMaxSize.
        /// </summary>
        public static double naturalMaxSize = 0;

        /// <summary>
        /// Defines the BlackBackground.
        /// </summary>
        public static bool BlackBackground = false;

        /// <summary>
        /// The getNaturalMaxSize.
        /// </summary>
        /// <returns>The <see cref="double"/>.</returns>
        public static double GetNaturalMaxSize()
        {
            double val;
            naturalMaxSize = System.Math.Sqrt(System.Math.Pow(Globals.XXmax, 2) + System.Math.Pow(Globals.YYmax, 2));
            val = naturalMaxSize;
            return val;
        }

        /// <summary>
        /// Defines the RunTime.
        ///...
        /// </summary>
        public static double RunTime = -1;

        /// <summary>
        /// Defines the tt.
        /// Virtual Time in seconds..........
        /// </summary>
        public static double tt = 0;

        /// <summary>
        /// Defines the dtt.
        /// Virtual differential time in seconds..........
        /// </summary>
        public static double dtt = 0;
    }
}
