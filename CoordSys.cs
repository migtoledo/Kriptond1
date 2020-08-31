namespace Kriptond1
{
    using System;
    using System.Collections.Generic;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media;
    using System.Windows.Shapes;

    /// <summary>
    /// Defines the <see cref="CoordSys" />.
    /// </summary>
    internal class CoordSys
    {
        /// <summary>
        /// Defines the g.
        /// </summary>
        internal Canvas g;

        /// <summary>
        /// Defines the itemstoremove.
        /// </summary>
        internal List<UIElement> GridXYLinesToRemove = new List<UIElement>();

        List<UIElement> XYLabelsToRemove = new List<UIElement>();

        /// <summary>
        /// Defines the Grid color....
        /// </summary>
        internal readonly Brush GridVerticalColor = Brushes.Brown;

        /// <summary>
        /// Defines the GridHorizontalColor.
        /// </summary>
        internal readonly Brush GridHorizontalColor = Brushes.Fuchsia;

        /// <summary>
        /// Defines the LinesThick.
        /// </summary>
        internal readonly double LinesThick = 0.2;

        /// <summary>
        /// Defines the HorizontalGridLines.
        /// </summary>
        internal List<Line> HorizontalGridLines;

        /// <summary>
        /// Defines the VerticalGridLines.
        /// </summary>
        internal List<Line> VerticalGridLines;

        /// <summary>
        /// Defines the Xlabels.
        /// </summary>
        internal List<Label> XLabels;

        /// <summary>
        /// Defines the YLabels.
        /// </summary>
        internal List<Label> YLabels;

        internal void UpdateXYLabelsTextColor()
        {
            if (Globals.BlackBackground == true)
            {
                SetXYLabelsWhiteColor();

            }
            else
            {
                SetXYLabelsBlackColor();
            }
        }

        void SetXYLabelsBlackColor()
        {
            if (XLabels != null)
            {
                foreach (Label tmp in XLabels)
                {
                    if (tmp != null)
                    {
                        tmp.Foreground = Brushes.Black;
                    }
                }
            }

            if (YLabels != null)
            {
                foreach (Label tmp in YLabels)
                {
                    if (tmp != null)
                    {
                        tmp.Foreground = Brushes.Black;
                    }
                }
            }
        }

        void SetXYLabelsWhiteColor()
        {
            if (XLabels != null)
            {
                foreach (Label tmp in XLabels)
                {
                    if (tmp != null)
                    {

                        tmp.Foreground = Brushes.White;
                    }
                }
            }

            if (YLabels != null)
            {
                foreach (Label tmp in YLabels)
                {
                    if (tmp != null)
                    {
                        tmp.Foreground = Brushes.White;
                    }
                }
            }
        }

        void InitXYLabels()
        {
            XLabels = new List<Label>();
            YLabels = new List<Label>();
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="CoordSys"/> class.
        /// </summary>
        public CoordSys()
        {
            Init();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CoordSys"/> class.
        /// </summary>
        /// <param name="panel">The panel<see cref="Canvas"/>.</param>
        public CoordSys(Canvas panel)
        {
            SetCanvasPanel(panel);
            Init();
        }


        void ShowVerticalGridLines()
        {
            if (VerticalGridLines != null)
            {
                foreach (Line tmp in VerticalGridLines)
                {
                    if (tmp != null)
                    {
                        tmp.Visibility = System.Windows.Visibility.Visible;
                    }
                }
            }
        }

        void ShowHorizontalGridLines()
        {
            if (HorizontalGridLines != null)
            {
                foreach (Line tmp in HorizontalGridLines)
                {
                    if (tmp != null)
                    {
                        tmp.Visibility = System.Windows.Visibility.Visible;
                    }
                }
            }
        }


        void ShowGridLines()
        {
            ShowHorizontalGridLines();
            ShowVerticalGridLines();
        }

        void ShowXLabels()
        {
            if (XLabels != null)
            {
                foreach (Label tmp in XLabels)
                {
                    if (tmp != null)
                    {
                        if (Globals.ShowXYLabels == true)
                        {
                            tmp.Visibility = System.Windows.Visibility.Visible;
                        }
                        else
                        {
                            tmp.Visibility = System.Windows.Visibility.Collapsed;
                        }
                    }
                }
            }
        }

        void ShowYLabels()
        {
            if (YLabels != null)
            {
                foreach (Label tmp in YLabels)
                {
                    if (tmp != null)
                    {
                        if (Globals.ShowXYLabels == true)
                        {
                            tmp.Visibility = System.Windows.Visibility.Visible;
                        }
                        else
                        {
                            tmp.Visibility = System.Windows.Visibility.Collapsed;
                        }
                    }
                }
            }
        }

        void ShowXYLabels()
        {
            ShowXLabels();
            ShowYLabels();
        }

        /// <summary>
        /// The Show.
        /// </summary>
        internal void Show()
        {
            ShowGridLines();
            ShowXYLabels();
        }



        void HideXLabels()
        {
            if (XLabels != null)
            {
                foreach (Label tmp in XLabels)
                {
                    if (tmp != null)
                    {
                        tmp.Visibility = System.Windows.Visibility.Collapsed;
                    }
                }
            }
        }

        void HideYLabels()
        {
            if (YLabels != null)
            {
                foreach (Label tmp in YLabels)
                {
                    if (tmp != null)
                    {
                        tmp.Visibility = System.Windows.Visibility.Collapsed;
                    }
                }
            }
        }


        void HideGridLines()
        {
            HideHorizontalGridLines();
            HideVerticalGridLines();
        }

        void HideXYLabels()
        {
            HideXLabels();
            HideYLabels();
        }


        /// <summary>
        /// The Hide.
        /// </summary>
        internal void Hide()
        {

            HideGridLines();
            HideXYLabels();



            CleanCanvasGrid();
            VerticalGridLines.Clear();
            HorizontalGridLines.Clear();
            XLabels.Clear();
            YLabels.Clear();

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
            Globals.RebuildGridH = false;
            Globals.RebuildGridV = false;
            GridXYLinesToRemove = new List<UIElement>();
            XYLabelsToRemove = new List<UIElement>();
            InitGridLines();
            InitXYLabels();

        }

        internal void ComputeXLabels()
        {
            XLabels.Clear();
            double n = Math.Round(Globals.OXX / Globals.sx);
            Label tmp;
            double stepHorizontal = Globals.sx;
            double xxright;
            double xxleft;
            for (int i = 0; i < n; i++)
            {
                xxright = i * stepHorizontal;
                tmp = new Label
                {
                    Content = " " + Globals.GetX(Globals.OXX + xxright) + " "
                };
                Canvas.SetLeft(tmp, Globals.OXX + xxright);
                Canvas.SetTop(tmp, Globals.OYY);
                tmp.Uid = "tmpLabel";
                if (Globals.BlackBackground)
                {
                    tmp.Foreground = Brushes.White;
                }
                else
                {
                    tmp.Foreground = Brushes.Black;
                }
                XLabels.Add(tmp);
            }
            for (int i = 0; i < n; i++)
            {
                xxleft = i * stepHorizontal;
                tmp = new Label
                {
                    Content = " " + Globals.GetX(Globals.OXX - xxleft) + " "
                };
                Canvas.SetLeft(tmp, Globals.OXX - xxleft);
                Canvas.SetTop(tmp, Globals.OYY);
                tmp.Uid = "tmpLabel";
                if (Globals.BlackBackground)
                {
                    tmp.Foreground = Brushes.White;
                }
                else
                {
                    tmp.Foreground = Brushes.Black;
                }
                XLabels.Add(tmp);
            }
        }

        internal void ComputeYLabels()
        {
            YLabels.Clear();
            double n = Math.Round(Globals.OYY / Globals.sy);
            Label tmp;
            double stepVertical = Globals.sy;
            double yytop;
            double yybottom;
            for (int i = 0; i < n; i++)
            {
                yytop = Globals.OYY - i * stepVertical;
                tmp = new Label
                {
                    Content = " " + Globals.GetY(yytop) + " "
                    // Content = "Hola"
                };
                Canvas.SetLeft(tmp, Globals.OXX);
                Canvas.SetTop(tmp, yytop);
                tmp.Uid = "tmpLabel";
                if (Globals.BlackBackground)
                {
                    tmp.Foreground = Brushes.White;
                }
                else
                {
                    tmp.Foreground = Brushes.Black;
                }
                YLabels.Add(tmp);
            }
            for (int i = 0; i < n; i++)
            {
                yybottom = Globals.OYY + (i) * stepVertical;
                tmp = new Label
                {
                    Content = " " + Globals.GetY(yybottom) + " "
                };
                Canvas.SetLeft(tmp, Globals.OXX);
                Canvas.SetTop(tmp, yybottom);
                tmp.Uid = "tmpLabel";
                if (Globals.BlackBackground)
                {
                    tmp.Foreground = Brushes.White;
                }
                else
                {
                    tmp.Foreground = Brushes.Black;
                }
                YLabels.Add(tmp);
            }
        }

        void ComputeXYLabels()
        {
            ComputeXLabels();
            ComputeYLabels();
        }



        /// <summary>
        /// The InitHorizontalGridLines.
        /// </summary>
        internal void InitHorizontalGridLines()
        {
            HorizontalGridLines = new List<Line>();
        }

        /// <summary>
        /// The ComputeVerticalGridLines.
        /// </summary>
        internal void ComputeVerticalGridLines()
        {
            VerticalGridLines.Clear();
            double n = Math.Round(Globals.OXX / Globals.sx);
            Line tmp;
            double stepHorizontal = Globals.sx;
            double xxright;
            double xxleft;
            for (int i = 0; i < n; i++)
            {
                xxright = i * stepHorizontal;
                tmp = new Line
                {
                    Stroke = GridVerticalColor,
                    StrokeThickness = LinesThick,
                    X1 = Globals.OXX + xxright,
                    Y1 = Globals.YYmin,
                    X2 = Globals.OXX + xxright,
                    Y2 = Globals.YYmax
                };
                tmp.Uid = "tmpGrid";
                VerticalGridLines.Add(tmp);
            }
            for (int i = 0; i < n; i++)
            {
                xxleft = i * stepHorizontal;
                tmp = new Line
                {
                    Stroke = GridVerticalColor,
                    StrokeThickness = LinesThick,
                    X1 = Globals.OXX - xxleft,
                    Y1 = Globals.YYmin,
                    X2 = Globals.OXX - xxleft,
                    Y2 = Globals.YYmax
                };
                tmp.Uid = "tmpGrid";
                VerticalGridLines.Add(tmp);
            }

            ComputeXYLabels();
        }

        /// <summary>
        /// The BuildVerticalGridLines.
        /// Only rebuilding if EVENT
        /// Change Screen Size, or Scale Changed is produced
        /// Trying to avoid overloading computations
        /// Optimizing performance for long time running
        ///.
        /// </summary>
        internal void BuildVerticalGridLines()
        {
            if (Globals.RebuildGridV == true)
            {
                ComputeVerticalGridLines();

            }
        }

        /// <summary>
        /// The ComputeHorizontalGridLines.
        /// </summary>
        internal void ComputeHorizontalGridLines()
        {
            HorizontalGridLines.Clear();
            double n = Math.Round(Globals.OYY / Globals.sy);
            Line tmp;
            double stepVertical = Globals.sy;
            double yytop;
            double yybottom;
            for (int i = 0; i < n; i++)
            {
                yytop = Globals.OYY - i * stepVertical;
                tmp = new Line
                {
                    Stroke = GridHorizontalColor,
                    StrokeThickness = LinesThick,
                    X1 = Globals.XXmin,
                    Y1 = yytop,
                    X2 = Globals.XXmax,
                    Y2 = yytop
                };
                tmp.Uid = "tmpGrid";
                HorizontalGridLines.Add(tmp);
            }
            for (int i = 0; i < n; i++)
            {
                yybottom = Globals.OYY + (i) * stepVertical;
                tmp = new Line
                {
                    Stroke = GridHorizontalColor,
                    StrokeThickness = LinesThick,
                    X1 = Globals.XXmin,
                    Y1 = yybottom,
                    X2 = Globals.XXmax,
                    Y2 = yybottom
                };
                tmp.Uid = "tmpGrid";
                HorizontalGridLines.Add(tmp);
            }
            ComputeXYLabels();
        }

        /// <summary>
        /// The BuildHorizontalGridLines.
        /// </summary>
        internal void BuildHorizontalGridLines()
        {
            if (Globals.RebuildGridH == true)
            {
                ComputeHorizontalGridLines();

            }
        }

        /// <summary>
        /// The InitVerticalGridLines.
        /// </summary>
        internal void InitVerticalGridLines()
        {
            VerticalGridLines = new List<Line>();
        }

        /// <summary>
        /// The InitGridLines.
        /// </summary>
        internal void InitGridLines()
        {
            InitHorizontalGridLines();
            InitVerticalGridLines();
        }



        void AddXLabels()
        {
            foreach (Label tmp in XLabels)
            {
                if (tmp != null)
                    g.Children.Add(tmp);
            }

        }

        void AddYLabels()
        {
            foreach (Label tmp in YLabels)
            {
                if (tmp != null)
                    g.Children.Add(tmp);
            }

        }

        void AddXYLabels()
        {
            AddXLabels();
            AddYLabels();
        }








        /// <summary>
        /// The AddGridLines.
        /// </summary>
        internal void AddGridLines()
        {
            CleanCanvasGrid();
            AddHorizontalGridLines();
            AddVerticalGridLines();
            //AddXYLabels();
            Globals.RebuildGridH = false;
            Globals.RebuildGridV = false;
        }

        /// <summary>
        /// The AddHorizontalGridLines.
        /// </summary>
        internal void AddHorizontalGridLines()
        {
            if (Globals.RebuildGridH == true)
            {
                if (HorizontalGridLines != null)
                {
                    foreach (Line tmp in HorizontalGridLines)
                    {
                        if (tmp != null)
                            g.Children.Add(tmp);
                    }
                }

            }
        }

        /// <summary>
        /// The CleanCanvasGrid.
        /// </summary>
        public void CleanCanvasGrid()
        {

            GridXYLinesToRemove.Clear();
            foreach (UIElement ui in g.Children)
            {
                if (ui.Uid == "tmpGrid")
                {
                    GridXYLinesToRemove.Add(ui);
                }
            }
            foreach (UIElement ui in GridXYLinesToRemove)
            {
                g.Children.Remove(ui);
            }
            GridXYLinesToRemove.Clear();


            XYLabelsToRemove.Clear();
            foreach (UIElement ui in g.Children)
            {
                if (ui.Uid == "tmpLabel")
                {
                    XYLabelsToRemove.Add(ui);
                }
            }
            foreach (UIElement ui in XYLabelsToRemove)
            {
                g.Children.Remove(ui);
            }
            XYLabelsToRemove.Clear();
        }

        /// <summary>
        /// The CleanVerticalGridLines.
        /// </summary>
        internal void HideVerticalGridLines()
        {
            if (VerticalGridLines != null)
            {
                foreach (Line tmp in VerticalGridLines)
                {
                    if (tmp != null)
                    {
                        tmp.Visibility = System.Windows.Visibility.Collapsed;
                    }
                }
                VerticalGridLines.Clear();
            }
        }

        /// <summary>
        /// The CleanHorizontalGridLines.
        /// </summary>
        internal void HideHorizontalGridLines()
        {
            if (HorizontalGridLines != null)
            {
                foreach (Line tmp in HorizontalGridLines)
                {
                    if (tmp != null)
                    {
                        tmp.Visibility = System.Windows.Visibility.Collapsed;
                    }
                }
                HorizontalGridLines.Clear();
            }
        }

        /// <summary>
        /// The AddVerticalGridLines.
        /// </summary>
        internal void AddVerticalGridLines()
        {
            if (Globals.RebuildGridV == true)
            {
                if (VerticalGridLines != null)
                {
                    foreach (Line tmp in VerticalGridLines)
                    {
                        if (tmp != null)
                            g.Children.Add(tmp);
                    }
                }
            }
        }

        /// <summary>
        /// The AddElements.
        /// </summary>
        public void AddElements()
        {
            AddGridLines();
        }

        /// <summary>
        /// The UpdateHorizontalGridLines.
        /// </summary>
        internal void UpdateHorizontalGridLines()
        {
            HideHorizontalGridLines();
            if (Globals.ShowGridLines == true && Globals.RebuildGridH == true)
            {
                BuildHorizontalGridLines();
                AddHorizontalGridLines();

                if (Globals.ShowXYLabels == true)
                {
                    ComputeXYLabels();
                    UpdateXYLabelsTextColor();
                    AddXYLabels();
                }


            }

            if (Globals.ShowGridLines == true)
            {
                Show();

            }
            else
            {
                Hide();
                CleanCanvasGrid();
                Globals.RebuildGridH = false;
                Globals.RebuildGridV = false;
            }
        }

        /// <summary>
        /// The UpdateVerticalGridLines.
        /// </summary>
        internal void UpdateVerticalGridLines()
        {
            HideVerticalGridLines();
            if (Globals.ShowGridLines == true && Globals.RebuildGridH == true)
            {
                BuildVerticalGridLines();
                AddVerticalGridLines();
                if (Globals.ShowXYLabels == true)
                {
                    ComputeXYLabels();
                    AddXYLabels();
                }
            }

            if (Globals.ShowGridLines == true)
            {
                Show();

            }
            else
            {
                Hide();
                CleanCanvasGrid();
                Globals.RebuildGridH = false;
                Globals.RebuildGridV = false;
                //Globals.ShowXYLabels = false;
            }
        }

        /// <summary>
        /// The UpdateGridLines.
        /// </summary>
        internal void UpdateGridLines()
        {
            if (Globals.ShowGridLines == true)
            {

                UpdateHorizontalGridLines();
                UpdateVerticalGridLines();
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
            CleanCanvasGrid();
            //UpdateXYLabelsTextColor();
            UpdateGridLines();

            Globals.XScaleChanged = false;
            Globals.YScaleChanged = false;
        }
    }
}
