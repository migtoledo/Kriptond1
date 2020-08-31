namespace Kriptond1
{
    using System.Windows.Controls;
    using System.Windows.Media;
    using System.Windows.Shapes;

    /// <summary>
    /// Defines the <see cref="Boundary" />.
    /// </summary>
    internal class Boundary
    {
        /// <summary>
        /// Defines the g.
        /// </summary>
        internal Canvas g;

        /// <summary>
        /// Defines the rect.
        /// </summary>
        internal Rectangle rect;

        /// <summary>
        /// Defines the rectColor.
        /// </summary>
        internal Brush rectColor;

        /// <summary>
        /// Defines the length.
        /// </summary>
        internal double length = 0;

        /// <summary>
        /// Defines the reloj.
        /// </summary>
        internal Reloj reloj;

        /// <summary>
        /// Defines the ds.
        /// </summary>
        internal double ds = 0;

        /// <summary>
        /// Defines the x.
        /// </summary>
        internal double x = 0;

        /// <summary>
        /// Defines the y.
        /// </summary>
        internal double y = 0;

        /// <summary>
        /// Defines the v.
        /// </summary>
        internal double v = 0;

        /// <summary>
        /// Defines the vx.
        /// </summary>
        internal double vx = 0;

        /// <summary>
        /// Defines the vy.
        /// </summary>
        internal double vy = 0;

        /// <summary>
        /// Defines the dx.
        /// </summary>
        internal double dx = 0;

        /// <summary>
        /// Defines the dy.
        /// </summary>
        internal double dy = 0;

        /// <summary>
        /// DISTANCES COMPUTATIONS
        /// YUHU!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        /// It is not obvious, because we do NOT
        /// know, "when" the user will resize
        /// the window frame, modifying the
        /// "NOW" boundaries-reference system.
        /// The "tier" called "armadillo" must
        /// remain the closest possible to
        /// the boundary-frame..., independently
        /// how big that frame is, uiuiuiu.
        /// </summary>
        /// <returns>The <see cref="bool"/>.</returns>
        internal bool IsMyTierInFrame()
        {
            bool retval;
            if (x == ds || y == ds || x + length == Globals.XXmax - ds || y + length == Globals.YYmax - ds)
            {
                retval = true;
            }
            else
            {
                retval = false;
            }
            return retval;
        }

        /// <summary>
        /// The ComputeDistanceTop.
        /// </summary>
        /// <returns>The <see cref="double"/>.</returns>
        internal double ComputeDistanceTop()
        {
            double val = 0;
            if (dy == 0)
            {
                val = y;
            }
            return val;
        }

        /// <summary>
        /// The ComputeDistanceBottom.
        /// </summary>
        /// <returns>The <see cref="double"/>.</returns>
        internal double ComputeDistanceBottom()
        {
            double val = 0;
            if (dy == 0)
            {
                val = System.Math.Abs(Globals.YYmax - y);
            }
            return val;
        }

        /// <summary>
        /// The ComputeDistanceLeft.
        /// </summary>
        /// <returns>The <see cref="double"/>.</returns>
        internal double ComputeDistanceLeft()
        {
            double val = 0;
            if (dx == 0)
            {
                val = x;
            }
            return val;
        }

        /// <summary>
        /// The ComputeDistanceRight.
        /// </summary>
        /// <returns>The <see cref="double"/>.</returns>
        internal double ComputeDistanceRight()
        {
            double val = 0;
            if (dx == 0)
            {
                val = Globals.XXmax - (x + length);
            }
            return val;
        }

        /// <summary>
        /// The ComeBackMyTier.
        /// </summary>
        internal void ComeBackMyTier()
        {
            if (IsMyTierInFrame() == false)
            {
                if (ComputeDistanceTop() > ds) y = ds;
                if (ComputeDistanceBottom() > ds) y = Globals.YYmax - ds - length;
                if (ComputeDistanceLeft() > ds) x = ds;
                if (ComputeDistanceRight() > ds) x = Globals.XXmax - length - ds;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Boundary"/> class.
        /// </summary>
        public Boundary()
        {
            Init();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Boundary"/> class.
        /// </summary>
        /// <param name="panel">The panel<see cref="Canvas"/>.</param>
        public Boundary(Canvas panel)
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
        /// The InitRect.
        /// </summary>
        internal void InitRect()
        {
            rectColor = Brushes.MediumSeaGreen;
            rect = new Rectangle
            {
                Width = length,
                Height = length,
                Stroke = rectColor,
                StrokeThickness = 1

            };
            InitXY();
        }

        /// <summary>
        /// The InitXY.
        /// </summary>
        internal void InitXY()
        {
            v = length;
            vx = v;
            vy = 0;
            x = ds;
            y = ds;
            dx = 0;
            dy = 0;
        }

        /// <summary>
        /// The ComputeXY.
        /// </summary>
        internal void ComputeXY()
        {
            ComputeVXY();
            ComputeDXY();
            SetConsistency();
            x += dx;
            y += dy;
        }

        /// <summary>
        /// The SetConsistency.
        /// </summary>
        public void SetConsistency()
        {
            ComeBackMyTier();
        }

        /// <summary>
        /// The Right.
        /// </summary>
        internal void Right()
        {
            if (dx > 0 && x + length + ds > Globals.XXmax)
            {
                vx = 0;
                vy = v;
                x = Globals.XXmax - length - ds;
            }
        }

        /// <summary>
        /// The Left.
        /// </summary>
        internal void Left()
        {
            if (dx < 0 && x < ds)
            {
                vx = 0;
                vy = -v;
                x = ds;
            }
        }

        /// <summary>
        /// The Bottom.
        /// </summary>
        internal void Bottom()
        {
            if (dy > 0 && y + length + ds > Globals.YYmax)
            {
                vy = 0;
                vx = -v;
                y = Globals.YYmax - length - ds;
            }
        }

        /// <summary>
        /// The Top.
        /// </summary>
        internal void Top()
        {
            if (dy < 0 && y < ds)
            {
                vy = 0;
                vx = v;
                y = ds;
            }
        }

        /// <summary>
        /// The ComputeVXY.
        /// </summary>
        internal void ComputeVXY()
        {
            if (Globals.tt == 0)
            {
                InitXY();
            }
            else
            {
                Right();
                Left();
                Bottom();
                Top();
            }
        }

        /// <summary>
        /// The ComputeDXY.
        /// </summary>
        internal void ComputeDXY()
        {
            dx = vx * Globals.dtt;
            dy = vy * Globals.dtt;
        }

        /// <summary>
        /// The init.
        /// </summary>
        public void Init()
        {
            ds = 2;
            length = 20;
            v = length;
            InitRect();
            reloj = new Reloj(g);
            reloj.SetR(length / 2 - 2);
            reloj.UpdatePosition(x + length / 2, y + length / 2);
        }

        /// <summary>
        /// The AddRect.
        /// </summary>
        internal void AddRect()
        {
            g.Children.Add(rect);
        }

        /// <summary>
        /// The addElements.
        /// </summary>
        public void AddElements()
        {
            AddRect();
            reloj.AddElements();
        }

        /// <summary>
        /// The UpdateRectDimensions.
        /// </summary>
        internal void UpdateRectDimensions()
        {
            if (Globals.tt != 0)
            {
                length = Globals.GetNaturalMaxSize() / 100;
                rect.Width = length;
                rect.Height = length;
                v = length;
            }
        }

        /// <summary>
        /// The UpdateRect.
        /// </summary>
        internal void UpdateRect()
        {
            UpdateRectDimensions();
            ComputeXY();
            Canvas.SetLeft(rect, x);
            Canvas.SetTop(rect, y);
        }

        /// <summary>
        /// The updateElements.
        /// </summary>
        internal void UpdateElements()
        {
            UpdateRect();
            v = length;
            reloj.SetR(length / 2 - 2);
            reloj.UpdatePosition(x + length / 2, y + length / 2);
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
