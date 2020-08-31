namespace Kriptond1
{
    using System.Windows.Controls;
    using System.Windows.Media;
    using System.Windows.Shapes;

    /// <summary>
    /// Defines the <see cref="Axis" />.
    /// </summary>
    internal class Axis
    {
        /// <summary>
        /// Defines the g.
        /// </summary>
        internal Canvas g;

        /// <summary>
        /// Defines the AxisColor.
        /// </summary>
        internal readonly Brush AxisColor = Brushes.Red;

        internal readonly Brush Axis2Color = Brushes.Blue;

        /// <summary>
        /// Defines the AxisThick.
        /// </summary>
        internal readonly double AxisThick = 2;

        internal readonly double Axis2Thick = 2;

        /// <summary>
        /// Defines the xAxis.
        /// </summary>
        internal Line xAxis;

        internal Line xAxis2;

        /// <summary>
        /// Defines the yAxis.
        /// </summary>
        internal Line yAxis;

        internal Line yAxis2;

        /// <summary>
        /// Initializes a new instance of the <see cref="Axis"/> class.
        /// </summary>
        public Axis()
        {
            Init();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Axis"/> class.
        /// </summary>
        /// <param name="panel">The panel<see cref="Canvas"/>.</param>
        public Axis(Canvas panel)
        {
            SetCanvasPanel(panel);

            Init();
        }

        void Show()
        {
            xAxis.Visibility = System.Windows.Visibility.Visible;
            yAxis.Visibility = System.Windows.Visibility.Visible;

           xAxis2.Visibility = System.Windows.Visibility.Visible;
           yAxis2.Visibility = System.Windows.Visibility.Visible;

        }

        void Hide()
        {
            xAxis.Visibility = System.Windows.Visibility.Hidden;
            yAxis.Visibility = System.Windows.Visibility.Hidden;

            xAxis2.Visibility = System.Windows.Visibility.Hidden;
            yAxis2.Visibility = System.Windows.Visibility.Hidden;
        }

        /// <summary>
        /// The SetCanvasPanel.
        /// </summary>
        /// <param name="panel">The panel<see cref="Canvas"/>.</param>
        internal void SetCanvasPanel(Canvas panel)
        {
            g = panel;
        }

        /// <summary>
        /// The Init.
        /// </summary>
        internal void Init()
        {
            InitAxis();
        }

        /// <summary>
        /// The initHorizontalAxis.
        /// </summary>
        internal void InitHorizontalAxis()
        {
            xAxis = new Line
            {
                Stroke = AxisColor,
                StrokeThickness = AxisThick,
                X1 = Globals.XXmin,
                Y1 = Globals.OYY,
                X2 = Globals.XXmax,
                Y2 = Globals.OYY
            };
            // Relative origin / Translated
            xAxis2 = new Line
            {
                Stroke = Axis2Color,
                StrokeThickness = Axis2Thick,
                X1 = Globals.XXmin,
                Y1 = Globals.YY0,
                X2 = Globals.XXmax,
                Y2 = Globals.YY0
            };



        }

        /// <summary>
        /// The initVerticalAxis.
        /// </summary>
        internal void InitVerticalAxis()
        {
            yAxis = new Line
            {
                Stroke = AxisColor,
                StrokeThickness = AxisThick,
                X1 = Globals.OXX,
                Y1 = Globals.YYmin,
                X2 = Globals.OXX,
                Y2 = Globals.YYmax
            };
            // Relative origin / translated
            yAxis2 = new Line
            {
                Stroke = Axis2Color,
                StrokeThickness = Axis2Thick,
                X1 = Globals.XX0,
                Y1 = Globals.YYmin,
                X2 = Globals.XX0,
                Y2 = Globals.YYmax
            };


        }

        /// <summary>
        /// The initAxis.
        /// </summary>
        internal void InitAxis()
        {
            InitHorizontalAxis();
            InitVerticalAxis();
        }

        /// <summary>
        /// The addAxis.
        /// </summary>
        internal void AddAxis()
        {
            AddHorizontalAxis();
            AddVerticalAxis();
        }

        /// <summary>
        /// The addHorizontalAxis.
        /// </summary>
        internal void AddHorizontalAxis()
        {
            g.Children.Add(xAxis);
            g.Children.Add(xAxis2);
        }

        /// <summary>
        /// The addVerticalAxis.
        /// </summary>
        internal void AddVerticalAxis()
        {
            g.Children.Add(yAxis);
            g.Children.Add(yAxis2);
        }

        /// <summary>
        /// The AddElements.
        /// </summary>
        public void AddElements()
        {
            AddAxis();
        }

        /// <summary>
        /// The updateHorizontalAxis.
        /// </summary>
        internal void UpdateHorizontalAxis()
        {
            xAxis.X1 = 0;
            xAxis.Y1 = g.ActualHeight / 2;
            xAxis.X2 = g.ActualWidth;
            xAxis.Y2 = g.ActualHeight / 2;

            xAxis2.X1 = 0;
            xAxis2.Y1 = Globals.YY0;
            xAxis2.X2 = g.ActualWidth;
            xAxis2.Y2 = Globals.YY0;

        }

        /// <summary>
        /// The updateVerticalAxis.
        /// </summary>
        internal void UpdateVerticalAxis()
        {
            yAxis.X1 = g.ActualWidth / 2;
            yAxis.Y1 = 0;
            yAxis.X2 = g.ActualWidth / 2;
            yAxis.Y2 = g.ActualHeight;

            yAxis2.X1 = Globals.XX0;
            yAxis2.Y1 = 0;
            yAxis2.X2 = Globals.XX0;
            yAxis2.Y2 = g.ActualHeight;

        }

        /// <summary>
        /// The updateAxis.
        /// </summary>
        internal void UpdateAxis()
        {
            UpdateHorizontalAxis();
            UpdateVerticalAxis();
            if (Globals.ShowAxis==true)
            {
                Show();
            }
            else
            {
                Hide();
            }
        }

        /// <summary>
        /// The update.
        /// </summary>
        public void Update()
        {
            UpdateAxis();
        }
    }
}
