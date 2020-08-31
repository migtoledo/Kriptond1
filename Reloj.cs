namespace Kriptond1
{
    using System;
    using System.Windows.Controls;
    using System.Windows.Media;
    using System.Windows.Shapes;

    /// <summary>
    /// Defines the <see cref="Reloj" />.
    /// </summary>
    internal class Reloj
    {
        /// <summary>
        /// Defines the g.
        /// </summary>
        internal Canvas g;

        /// <summary>
        /// Defines the aguja.
        /// </summary>
        internal Line aguja;

        /// <summary>
        /// Defines the agujaColor.
        /// </summary>
        internal Brush agujaColor;

        /// <summary>
        /// Defines the R.
        /// </summary>
        internal double R = 0;

        /// <summary>
        /// Defines the x.
        /// </summary>
        internal double x = 0;

        /// <summary>
        /// Defines the y.
        /// </summary>
        internal double y = 0;

        /// <summary>
        /// Initializes a new instance of the <see cref="Reloj"/> class.
        /// </summary>
        public Reloj()
        {
            Init();
        }

        /// <summary>
        /// The setR.
        /// </summary>
        /// <param name="val">The val<see cref="double"/>.</param>
        public void SetR(double val)
        {
            R = val;
        }

        /// <summary>
        /// The setX.
        /// </summary>
        /// <param name="val">The val<see cref="double"/>.</param>
        internal void SetX(double val)
        {
            x = val;
            aguja.X1 = x;
        }

        /// <summary>
        /// The setY.
        /// </summary>
        /// <param name="val">The val<see cref="double"/>.</param>
        internal void SetY(double val)
        {
            y = val;
            aguja.Y1 = y;
        }

        /// <summary>
        /// The setXY.
        /// </summary>
        /// <param name="_x">The _x<see cref="double"/>.</param>
        /// <param name="_y">The _y<see cref="double"/>.</param>
        internal void SetXY(double _x, double _y)
        {
            SetX(_x);
            SetY(_y);
            UpdateAguja();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Reloj"/> class.
        /// </summary>
        /// <param name="panel">The panel<see cref="Canvas"/>.</param>
        public Reloj(Canvas panel)
        {
            SetCanvasPanel(panel);
            Init();
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
        /// The initLine1.
        /// </summary>
        internal void InitReloj()
        {
            agujaColor = Brushes.Pink;
            aguja = new Line
            {
                Stroke = agujaColor,
                StrokeThickness = 1,
            };
            UpdatePosition(Globals.OXX, Globals.OYY);
        }

        /// <summary>
        /// The UpdatePosition.
        /// </summary>
        /// <param name="_x">The _x<see cref="double"/>.</param>
        /// <param name="_y">The _y<see cref="double"/>.</param>
        public void UpdatePosition(double _x, double _y)
        {
            SetXY(_x, _y);
        }

        /// <summary>
        /// The UpdateAguja.
        /// </summary>
        internal void UpdateAguja()
        {
            double w = 2 * Math.PI;
            aguja.X2 = aguja.X1 + R * Math.Cos(w * Globals.tt);
            aguja.Y2 = aguja.Y1 + R * Math.Sin(w * Globals.tt);
        }

        /// <summary>
        /// The init.
        /// </summary>
        public void Init()
        {
            SetR(100);
            InitReloj();
        }

        /// <summary>
        /// The addLine1.
        /// </summary>
        internal void AddReloj()
        {
            g.Children.Add(aguja);
        }

        /// <summary>
        /// The addElements.
        /// </summary>
        public void AddElements()
        {
            AddReloj();
        }

        /// <summary>
        /// The updateLine1.
        /// </summary>
        internal void UpdateReloj()
        {
            UpdateAguja();
        }

        /// <summary>
        /// The updateElements.
        /// </summary>
        internal void UpdateElements()
        {
            UpdatePosition(Globals.OXX, Globals.OYY);
            UpdateReloj();
        }

        /// <summary>
        /// The Update.
        /// </summary>
        public void Update()
        {
            UpdateElements();
        }
    }
}
