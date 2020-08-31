namespace Kriptond1
{
    using System;
    using System.Collections.Generic;
    using System.Windows.Controls;
    using System.Windows.Media;

    /// <summary>
    /// Defines the <see cref="AnimationBackground" />.
    /// </summary>
    internal class AnimationBackground
    {
        /// <summary>
        /// Defines the g.
        /// </summary>
        internal Canvas g;

        /// <summary>
        /// Defines the MouseLabelXY.
        /// </summary>
        internal Label MouseLabelXY;

        /// <summary>
        /// Defines the axis.
        /// </summary>
        internal readonly Axis axis;

        /// <summary>
        /// Defines the GridLines.
        /// </summary>
        public CoordSys GridLines;

       

        /// <summary>
        /// Initializes a new instance of the <see cref="AnimationBackground"/> class.
        /// </summary>
        public AnimationBackground()
        {
            Init();
        }

        /// <summary>
        /// The UpdateCanvasBackgroundColor.
        /// </summary>
        internal void UpdateCanvasBackgroundColor()
        {
            if (Globals.BlackBackground == true)
            {
                SetBackgroundColor(Brushes.Black);
            }
            else
            {
                SetBackgroundColor(Brushes.White);
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AnimationBackground"/> class.
        /// </summary>
        /// <param name="panel">The panel<see cref="Canvas"/>.</param>
        public AnimationBackground(Canvas panel)
        {
            SetCanvasPanel(panel);
            UpdateCanvasBackgroundColor();
            GridLines = new CoordSys(panel);
            axis = new Axis(panel);
            Init();
            MouseLabelXY = new Label();
        }

        /// <summary>
        /// The SetBackgroundColor.
        /// </summary>
        /// <param name="BackgroundColor">The BackgroundColor<see cref="SolidColorBrush"/>.</param>
        internal void SetBackgroundColor(SolidColorBrush BackgroundColor)
        {
            g.Background = BackgroundColor;
        }

        /// <summary>
        /// The UpdateMouseCursorTextColor.
        /// </summary>
        internal void UpdateMouseCursorTextColor()
        {
            if (Globals.BlackBackground)
            {
                MouseLabelXY.Foreground = Brushes.White;
            }
            else
                MouseLabelXY.Foreground = Brushes.Black;
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
            GridLines.Init();
            axis.Init();
            

        }



        
        
       




        /// <summary>
        /// The AddMouseCursorLabel.
        /// </summary>
        internal void AddMouseCursorLabel()
        {
            g.Children.Add(MouseLabelXY);
        }

        /// <summary>
        /// The AddElements.
        /// </summary>
        public void AddElements()
        {
            GridLines.AddElements();
            axis.AddElements();
            AddMouseCursorLabel();
           
        }

        /// <summary>
        /// Defines the mx.
        /// </summary>
        internal int mx = 20;

        /// <summary>
        /// Defines the my.
        /// </summary>
        internal int my = 20;

        /// <summary>
        /// The SetMarginMouseXY.
        /// </summary>
        internal void SetMarginMouseXY()
        {
            if (Globals.XX > Globals.GraphicsCanvasWidth - 40)
                mx = 140;
            else
                mx = 20;

            if (Globals.XX < 40)
                mx = -40;

            if (Globals.YY > Globals.GraphicsCanvasHeight - 40)
                my = -40;
            else
                my = 20;
        }

        /// <summary>
        /// The UpdateMouseXYPosition.
        /// </summary>
        internal void UpdateMouseXYPosition()
        {
            SetMarginMouseXY();
            Canvas.SetLeft(MouseLabelXY, Globals.XX - mx);
            Canvas.SetTop(MouseLabelXY, Globals.YY + my);

        }

        /// <summary>
        /// The UpdateMouseXYContent.
        /// </summary>
        internal void UpdateMouseXYContent()
        {
            MouseLabelXY.Content =
                " ( " + string.Format("{000:0.00}", Globals.X) +
                " | " + string.Format("{000:0.00}", Globals.Y) + " ) ";
        }

        void UpdateMouseLabelVisibility()
        {
            if (Globals.MouseLabelVisible)
                MouseLabelXY.Visibility = System.Windows.Visibility.Visible;
            else
                MouseLabelXY.Visibility = System.Windows.Visibility.Hidden;
        }

        /// <summary>
        /// The UpdateMouseCursor.
        /// </summary>
        internal void UpdateMouseCursor()
        {
            UpdateMouseXYPosition();
            UpdateMouseXYContent();
            UpdateMouseCursorTextColor();
            UpdateMouseLabelVisibility();
        }

        /// <summary>
        /// The update.
        /// Grid Lines Update only for events:
        /// Show/Hide Grid, Change XY Scale, Window Resize.
        /// </summary>
        public void Update()
        {
            
            UpdateCanvasBackgroundColor();
            axis.Update();
            UpdateMouseCursor();
           
        }
    }
}
