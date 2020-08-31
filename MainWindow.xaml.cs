namespace Kriptond1
{
    using System;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;
    using System.Windows.Media;
    using System.Windows.Threading;

    /// <summary>
    /// Interaction logic for MainWindow.xaml.
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Defines the MouseXY.
        /// </summary>
        internal Point MouseXY = new Point();

        /// <summary>
        /// Defines the Animation.
        /// </summary>
        private readonly AnimationArea Animation;

        /// <summary>
        /// Defines the BackGroundTimer.
        /// </summary>
        internal DispatcherTimer BackGroundTimer;

        /// <summary>
        /// Defines the AnimationTimer.
        /// </summary>
        internal DispatcherTimer AnimationTimer;

        /// <summary>
        /// Defines the DT_BackGroundTimer.
        /// In ms..........
        /// </summary>
        private readonly double DT_BackGroundTimer = Globals.DT_BackgroundTimer;

        /// <summary>
        /// Defines the FPS.
        /// In Hz..........
        /// </summary>
        internal double FPS;

        /// <summary>
        /// Defines the RunTimeSeconds.
        /// </summary>
        internal double RunTimeSeconds = 0;

        /// <summary>
        /// Defines the DT_AnimationTimer.
        /// In ms..........
        /// </summary>
        internal double DT_AnimationTimer = Globals.DT_ForegroundTimer;

        /// <summary>
        /// Defines the DTT.
        /// In s
        /// Dual variable for the differential animation time 
        /// (DTT -> DT_AnimationTimer) in seconds..
        /// </summary>
        internal double DTT = 0;

        /// <summary>
        /// Defines the DTT_Input.
        /// </summary>
        internal double DTT_Input = 0;

        /// <summary>
        /// Defines the t.
        /// </summary>
        internal double t = 0;

        /// <summary>
        /// *---*
        /// Defines the tt.
        /// Dual variable for the animation time.
        /// It corresponds to the virtual time : 
        /// tt = tt + DTT
        /// ( t -> tt )
        /// ( DT -> DTT )
        /// Where :
        /// t = t + dt
        /// tt = tt + DTT.
        /// The evolution equations of the system are written
        /// using as independent variable the virtual time: tt
        /// and the virtual differential time: dtt
        /// 
        /// The ABSTRACT MAP (t, dt) ---> (tt, dtt)
        /// defines the kernel of the system
        /// *---*....................
        /// </summary>
        internal double tt = 0;

        /// <summary>
        /// Defines the BackGroundCycle.
        /// </summary>
        internal double BackGroundCycle = 0;

        /// <summary>
        /// Defines the AnimationCycle.
        /// </summary>
        private double AnimationCycle = 0;

        

        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindow"/> class.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            Animation = new AnimationArea(CanvasPanel);
        }

        /// <summary>
        /// The Window_Loaded.
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/>.</param>
        /// <param name="e">The e<see cref="RoutedEventArgs"/>.</param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            InitFonts();
            InitBackgroundTimer();
            InitAnimationTimer();
            UpdateGlobals();
            UpdateAnimation();
            
        }

        /// <summary>
        /// The GraphicsPanel_Loaded.
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/>.</param>
        /// <param name="e">The e<see cref="RoutedEventArgs"/>.</param>
        private void GraphicsPanel_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateGlobals();
            UpdateAnimation();
        }

        /// <summary>
        /// The initFonts.
        /// </summary>
        internal void InitFonts()
        {
            FontFamily = new FontFamily("Verdana");
            FontSize = 11;
        }

        /// <summary>
        /// The InitBackgroundTimer.
        /// </summary>
        internal void InitBackgroundTimer()
        {
            FPS = 1 / (DT_BackGroundTimer / 1000);
            BackGroundTimer = new DispatcherTimer
            {
                Interval = TimeSpan.FromMilliseconds(DT_BackGroundTimer)
            };
            BackGroundTimer.Tick += new EventHandler(BackGroundTimerTasks);
            BackGroundTimer.Start();
        }

        /// <summary>
        /// The BackGroundTimerTasks.
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/>.</param>
        /// <param name="e">The e<see cref="EventArgs"/>.</param>
        internal void BackGroundTimerTasks(object sender, EventArgs e)
        {
            UpdateGlobals();
            SetConsistency();
            UpdateRunTimeSeconds();
            UpdateTextInfoElements();
            UpdateAnimation();
        }

        /// <summary>
        /// The InitAnimationTimer.
        /// </summary>
        internal void InitAnimationTimer()
        {
            AnimationCycle = 0;
            AnimationTimer = new DispatcherTimer
            {
                Interval = TimeSpan.FromMilliseconds(DT_AnimationTimer)
            };
            AnimationTimer.Tick += new EventHandler(AnimationTimerTasks);
        }

        /// <summary>
        /// The AnimationTimerTasks.
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/>.</param>
        /// <param name="e">The e<see cref="EventArgs"/>.</param>
        internal void AnimationTimerTasks(object sender, EventArgs e)
        {
            UpdateAnimationTime();
            UpdateVirtualTime();
            UpdateGlobals();
            UpdateAnimation();
        }

        /// <summary>
        /// The SetDTTConsistency.
        /// Sets DTT as the effective virtual differential time in seconds.
        /// DTT can be:
        /// = 0 ; when the animation is not started, is stopped, or it is paused.
        /// Negative-Signature ( - ) ; when the animation runs backwards.
        /// Positive-Signature ( + ) ; when the animation runs onwards.
        /// </summary>
        internal void SetDTTConsistency()
        {
            if (AnimationTimer.IsEnabled == false && DTT_Input == 0)
            {
                DTT = 0;
            }
            else
            {
                if (DTT_Input == 0) DTT = DT_AnimationTimer / 1000;
            }
        }

        /// <summary>
        /// The SetConsistency.
        /// </summary>
        internal void SetConsistency()
        {
            SetDTTConsistency();
        }

        /// <summary>
        /// The UpdateGlobalsXYDimensions.
        /// </summary>
        internal void UpdateGlobalsXYDimensions()
        {
            Globals.XXmin = 0;
            Globals.YYmin = 0;

            Globals.GraphicsCanvasWidth = CanvasPanel.ActualWidth;
            Globals.GraphicsCanvasHeight = CanvasPanel.ActualHeight;

            Globals.Width = CanvasPanel.ActualWidth;
            Globals.Height = CanvasPanel.ActualHeight;

            Globals.XXmax = CanvasPanel.ActualWidth;
            Globals.YYmax = CanvasPanel.ActualHeight;

            Globals.OXX = CanvasPanel.ActualWidth / 2;
            Globals.OYY = CanvasPanel.ActualHeight / 2;
        }

        /// <summary>
        /// The UpdateGlobalsXY.
        /// </summary>
        internal void UpdateGlobalsXY()
        {
            Globals.UpdateXY();
        }

        /// <summary>
        /// The UpdateGlobalsVirtualTime.
        /// </summary>
        internal void UpdateGlobalsVirtualTime()
        {
            Globals.dtt = DTT;
            Globals.tt = tt;
        }

        /// <summary>
        /// The UpdateGlobals.
        /// </summary>
        internal void UpdateGlobals()
        {
            UpdateMousePositionCanvas();
            UpdateGlobalsXYDimensions();
            UpdateGlobalsXY();
            UpdateGlobalsVirtualTime();
            Globals.RunTime = t;
        }

        /// <summary>
        /// The UpdateElementsTopLine1.
        /// </summary>
        internal void UpdateElementsTopLine1()
        {
            labelDate.Content = DateTime.Now.ToString();
            labelRunTime.Content = "Run-Time : " + string.Format("{000:0.0}", RunTimeSeconds) + " s";
            labelFPS.Content = "FPS : " + FPS + " Hz";
            labelWindowSize.Content = "WINDOW : " + Math.Round(ActualWidth) +
                " x " + Math.Round(ActualHeight);
            labelGraphicsSize.Content = "GRAPHICS : " + Math.Round(Globals.GraphicsCanvasWidth) +
                " x " + Math.Round(Globals.GraphicsCanvasHeight);
        }

        /// <summary>
        /// The UpdateElementsTopLine2.
        /// </summary>
        internal void UpdateElementsTopLine2()
        {
            labelDT_AnimationTime.Content = "dt =  " + string.Format("{000:0}", DT_AnimationTimer) + " ms";
            labelAnimationTime.Content = "t =  " + string.Format("{000:0.00}", t) + " s";
            labelDTT_AnimationTime.Content = "dtt =  " + string.Format("{000:0}", DTT * 1000) + " ms";
            labelVirtualTime.Content = "tt =  " + string.Format("{000:0.00}", tt) + " s";
            labelAnimVirtualTime.Content = string.Format("{000:0}", DT_AnimationTimer) + " ms (DT) = " + string.Format("{000:0}", DTT * 1000) + " ms (DTT)";
        }

        /// <summary>
        /// The UpdateElementsTopLine3.
        /// </summary>
        internal void UpdateElementsTopLine3()
        {
            string strMousePosition = "[ " + string.Format("{000:0}", MouseXY.X) + "  " +
               " | " + string.Format("{000:0}", MouseXY.Y) + " ] ";

            labelMouseXY.Content = strMousePosition;

            string strXY = "( " +
               string.Format("{000:0.00}", Globals.X) + " u | " +
               string.Format("{000:0.00}", Globals.Y) + " u ) ";

            labelXY.Content = strXY;

            labelXscale.Content = "X Scale : " + Globals.sx + " px / u";
            labelYscale.Content = "Y Scale : " + Globals.sy + " px / u";

            LabelOX.Content = " OX : " + Globals.OX + " u";
            LabelOY.Content = " OY : " + Globals.OY + " u";

            labelXMin.Content = " X Min : " + string.Format("{000:0.00}", Globals.XMIN) + " u";

            labelXMax.Content = " X Max : " + string.Format("{000:0.00}", Globals.XMAX) + " u";

            labelYMin.Content = " Y Min : " + string.Format("{000:0.00}", Globals.YMIN) + " u";

            labelYMax.Content = " Y Max : " + string.Format("{000:0.00}", Globals.YMAX) + " u";



        }

        /// <summary>
        /// The UpdateTextInfoElements.
        /// </summary>
        internal void UpdateTextInfoElements()
        {
            UpdateElementsTopLine1();
            UpdateElementsTopLine2();
            UpdateElementsTopLine3();
        }

        /// <summary>
        /// The UpdateRunTimeSeconds.
        /// </summary>
        internal void UpdateRunTimeSeconds()
        {
            BackGroundCycle++;
            RunTimeSeconds = BackGroundCycle * DT_BackGroundTimer / 1000;
        }

        /// <summary>
        /// The UpdateAnimationTime.
        /// </summary>
        internal void UpdateAnimationTime()
        {
            AnimationCycle++;
            t += (DT_AnimationTimer / 1000);
        }

        /// <summary>
        /// The UpdateVirtualTime.
        /// </summary>
        private void UpdateVirtualTime()
        {
            tt += DTT;
        }

        /// <summary>
        /// The UpdateAnimation.
        /// </summary>
        internal void UpdateAnimation()
        {
            Animation.Update();
            //if (t > 1 && t<2)
            //  Animation.animBackground.axis.Update();

        }

        /// <summary>
        /// The Window_SizeChanged.
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/>.</param>
        /// <param name="e">The e<see cref="SizeChangedEventArgs"/>.</param>
        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            Globals.RebuildGridH = true;
            Globals.RebuildGridV = true;

            Animation.animForeground.armadillo.SetConsistency();
            UpdateGlobals();
            UpdateTextInfoElements();
            Animation.animBackground.GridLines.Update();
            Animation.animBackground.axis.Update();
            UpdateAnimation();

        }

        /// <summary>
        /// EVENTS.
        /// </summary>
        internal void QuitApp()
        {
            Application.Current.Shutdown();
        }

        /// <summary>
        /// The ButtonQuit_Click.
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/>.</param>
        /// <param name="e">The e<see cref="RoutedEventArgs"/>.</param>
        private void ButtonQuit_Click(object sender, RoutedEventArgs e)
        {
            QuitApp();
        }

        /// <summary>
        /// The StartAnim.
        /// </summary>
        internal void StartAnim()
        {
            t = 0;
            tt = 0;
            AnimationCycle = 0;
            AnimationTimer.Start();
        }

        /// <summary>
        /// The ButtonStart_Click.
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/>.</param>
        /// <param name="e">The e<see cref="RoutedEventArgs"/>.</param>
        private void ButtonStart_Click(object sender, RoutedEventArgs e)
        {
            StartAnim();
        }

        /// <summary>
        /// The StopAnim.
        /// </summary>
        internal void StopAnim()
        {
            t = 0;
            tt = 0;
            DTT_Input = 0;
            TextBox_InputDTT.Clear();
            AnimationCycle = 0;
            AnimationTimer.Stop();
        }

        /// <summary>
        /// The ButtonStop_Click.
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/>.</param>
        /// <param name="e">The e<see cref="RoutedEventArgs"/>.</param>
        private void ButtonStop_Click(object sender, RoutedEventArgs e)
        {
            StopAnim();
        }

        /// <summary>
        /// The PauseAnim.
        /// </summary>
        internal void PauseAnim()
        {
            if (AnimationTimer.IsEnabled == false)
            {
                AnimationTimer.Start();
                DTT = DTT_Input / 1000;
            }
            else
            {
                AnimationTimer.Stop();
                DTT = 0;
            }
        }

        /// <summary>
        /// The ButtonPause_Click.
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/>.</param>
        /// <param name="e">The e<see cref="RoutedEventArgs"/>.</param>
        private void ButtonPause_Click(object sender, RoutedEventArgs e)
        {
            PauseAnim();
        }

        /// <summary>
        /// The ContinueAnim.
        /// </summary>
        internal void ContinueAnim()
        {
            if (AnimationTimer.IsEnabled == false) AnimationTimer.Start();
            else AnimationTimer.Stop();
            DTT = DTT_Input / 1000;
        }

        /// <summary>
        /// The ButtonContinue_Click.
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/>.</param>
        /// <param name="e">The e<see cref="RoutedEventArgs"/>.</param>
        private void ButtonContinue_Click(object sender, RoutedEventArgs e)
        {
            ContinueAnim();
        }

        /// <summary>
        /// The RunBackwards.
        /// </summary>
        internal void RunBackwards()
        {
            DT_AnimationTimer = -DT_AnimationTimer;
            if (DTT_Input == 0)
            {
                DTT = DT_AnimationTimer / 1000;
            }
            else
            {
                DTT = -DTT;
            }
            if (AnimationTimer.IsEnabled == false) AnimationTimer.Start();
        }

        /// <summary>
        /// The buttonBackwards_Click.
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/>.</param>
        /// <param name="e">The e<see cref="RoutedEventArgs"/>.</param>
        private void ButtonBackwards_Click(object sender, RoutedEventArgs e)
        {
            RunBackwards();
        }

        /// <summary>
        /// The InputDTT.
        /// </summary>
        internal void InputDTT()
        {
            if (TextBox_InputDTT.Text.Length > 0)
            {
                DTT_Input = Convert.ToDouble(TextBox_InputDTT.Text);
                DTT = DTT_Input / 1000;
                UpdateTextInfoElements();
            }
        }

        /// <summary>
        /// The TextBox_InputDTT_TextChanged.
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/>.</param>
        /// <param name="e">The e<see cref="System.Windows.Controls.TextChangedEventArgs"/>.</param>
        private void TextBox_InputDTT_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            InputDTT();
        }

        /// <summary>
        /// The Synchronize.
        /// </summary>
        internal void Synchronize()
        {
            DTT = DT_AnimationTimer / 1000;
            DTT_Input = 0;
            TextBox_InputDTT.Clear();
        }

        /// <summary>
        /// The ButtonSync_Click.
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/>.</param>
        /// <param name="e">The e<see cref="RoutedEventArgs"/>.</param>
        private void ButtonSync_Click(object sender, RoutedEventArgs e)
        {
            Synchronize();
        }

        /// <summary>
        /// The ButtonBackground_Click.
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/>.</param>
        /// <param name="e">The e<see cref="RoutedEventArgs"/>.</param>
        private void ButtonBackground_Click(object sender, RoutedEventArgs e)
        {
            Globals.BlackBackground = !Globals.BlackBackground;
            Animation.animBackground.GridLines.Update();
            //UpdateScreen();
        }

        /// <summary>
        /// The ButtonHelp_Click.
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/>.</param>
        /// <param name="e">The e<see cref="RoutedEventArgs"/>.</param>
        private void ButtonHelp_Click(object sender, RoutedEventArgs e)
        {
        }

        /// <summary>
        /// The ButtonAbout_Click.
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/>.</param>
        /// <param name="e">The e<see cref="RoutedEventArgs"/>.</param>
        private void ButtonAbout_Click(object sender, RoutedEventArgs e)
        {
        }

        /// <summary>
        /// The UpdateGlobalsMouseXY.
        /// </summary>
        internal void UpdateGlobalsMouseXY()
        {
            MouseXY = Mouse.GetPosition(Animation.g);
            Globals.MouseX = MouseXY.X;
            Globals.MouseY = MouseXY.Y;
            Globals.XX = MouseXY.X;
            Globals.YY = MouseXY.Y;
        }

        /// <summary>
        /// The UpdateMousePositionCanvas.
        /// </summary>
        internal void UpdateMousePositionCanvas()
        {
            UpdateGlobalsMouseXY();
        }

        /// <summary>
        /// The TextBox_XScale_TextChanged.
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/>.</param>
        /// <param name="e">The e<see cref="System.Windows.Controls.TextChangedEventArgs"/>.</param>
        private void TextBox_XScale_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            if (TextBox_XScale.Text.Length > 0)
            {
                Globals.sx = Convert.ToDouble(TextBox_XScale.Text);
                UpdateTextInfoElements();
                Globals.XScaleChanged = true;
                Globals.RebuildGridH = true;
                Globals.RebuildGridV = true;
                this.Animation.animBackground.GridLines.Update();
            }
        }

        /// <summary>
        /// The TextBox_YScale_TextChanged.
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/>.</param>
        /// <param name="e">The e<see cref="System.Windows.Controls.TextChangedEventArgs"/>.</param>
        private void TextBox_YScale_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            if (TextBox_YScale.Text.Length > 0)
            {
                Globals.sy = Convert.ToDouble(TextBox_YScale.Text);
                Globals.YScaleChanged = true;
                Globals.RebuildGridH = true;
                Globals.RebuildGridV = true;
                UpdateTextInfoElements();
                Animation.animBackground.GridLines.Update();
            }
        }

        /// <summary>
        /// The ButtonShowAxis_Click.
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/>.</param>
        /// <param name="e">The e<see cref="RoutedEventArgs"/>.</param>
        private void ButtonShowAxis_Click(object sender, RoutedEventArgs e)
        {
            Globals.ShowAxis = !Globals.ShowAxis;
            Animation.animBackground.axis.Update();
        }

        private void ButtonShowGrid_Click(object sender, RoutedEventArgs e)
        {
            Globals.ShowGridLines = !Globals.ShowGridLines;
            Animation.animBackground.GridLines.Update();
        }

        private void ButtonRestore_Click(object sender, RoutedEventArgs e)
        {

        }


        void UpdateScreen()
        {
            Globals.RebuildGridH = true;
            Globals.RebuildGridV = true;

            Animation.animForeground.armadillo.SetConsistency();
            UpdateGlobals();
            UpdateTextInfoElements();
            Animation.animBackground.GridLines.Update();
            Animation.animBackground.axis.Update();
            UpdateAnimation();
        }


        void XYScaleConsistency()
        {
            if (Globals.sx < 2)
                Globals.sx = 1;
            if (Globals.sy < 2)
                Globals.sy = 1;
        }

        private void Window_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (e.Delta > 0)
            {
                Globals.sx++;
                Globals.sy++;
            }
            if (e.Delta < 0)
            {
                if (Globals.sx > 2)
                {
                    Globals.sx--;
                }

                if (Globals.sy > 2)
                {
                    Globals.sy--;
                }
            }
            XYScaleConsistency();
            UpdateScreen();
        }

        private void TextBox_OX_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            if (TextBox_OX.Text.Length > 0)
            {
                Globals.OX = Convert.ToDouble(TextBox_OX.Text);
                UpdateTextInfoElements();
                Globals.RebuildGridH = true;
                Globals.RebuildGridV = true;
                Animation.animBackground.GridLines.Update();
                UpdateScreen();
            }
        }

        private void TextBox_OY_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            if (TextBox_OY.Text.Length > 0)
            {
                Globals.OY = Convert.ToDouble(TextBox_OY.Text);
                UpdateTextInfoElements();
                Globals.RebuildGridH = true;
                Globals.RebuildGridV = true;
                Animation.animBackground.GridLines.Update();
                UpdateScreen();
            }
        }

        private void CanvasPanel_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Globals.MouseLabelVisible = !Globals.MouseLabelVisible;
        }

        private void ButtonShowXYLabels_Click(object sender, RoutedEventArgs e)
        {
            Globals.ShowXYLabels = !Globals.ShowXYLabels;
            Animation.animBackground.GridLines.Update();
           
        }
    }
}
