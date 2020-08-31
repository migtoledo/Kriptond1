namespace Kriptond1
{
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;

    /// <summary>
    /// Defines the <see cref="AnimationArea" />.
    /// </summary>
    internal class AnimationArea
    {
        /// <summary>
        /// Defines the g.
        /// </summary>
        public Canvas g;

        /// <summary>
        /// Defines the animBackground.
        /// </summary>
        internal AnimationBackground animBackground;

        /// <summary>
        /// Defines the animForeground.
        /// </summary>
        internal AnimationForeground animForeground;

        /// <summary>
        /// Initializes a new instance of the <see cref="AnimationArea"/> class.
        /// </summary>
        /// <param name="panel">The panel<see cref="DockPanel"/>.</param>
        public AnimationArea(Canvas panel)
        {
            SetCanvasPanel(panel);
            Init();
        }

        void SetCanvasPanel(Canvas panel)
        {
            g = panel;
        }

        void Init()
        {
            InitBackgroundElements();
            InitForegroundElements();
        }
        

        /// <summary>
        /// The InitBackgroundElements.
        /// </summary>
        internal void InitBackgroundElements()
        {
            animBackground = new AnimationBackground(g);
            AddBackgroundElements();
        }

        /// <summary>
        /// The AddBackgroundElements.
        /// </summary>
        internal void AddBackgroundElements()
        {
            animBackground.AddElements();
        }

        /// <summary>
        /// The UpdateBackgroundElements.
        /// </summary>
        private void UpdateBackgroundElements()
        {
            animBackground.Update();
        }

        /// <summary>
        /// The UpdateBackground.
        /// </summary>
        internal void UpdateBackground()
        {
            UpdateBackgroundElements();
        }

        /// <summary>
        /// The InitForegroundElements.
        /// </summary>
        private void InitForegroundElements()
        {
            animForeground = new AnimationForeground(g);
            AddForegroundElements();
        }

        /// <summary>
        /// The AddForegroundElements.
        /// </summary>
        private void AddForegroundElements()
        {
            animForeground.AddElements();
        }

        /// <summary>
        /// The UpdateForegroundElements.
        /// </summary>
        /// <param name="dt">The dt<see cref="double"/>.</param>
        private void UpdateForegroundElements()
        {
            animForeground.Update();
        }

        /// <summary>
        /// The UpdateForeground.
        /// </summary>
        /// <param name="dt">The dt<see cref="double"/>.</param>
        public void UpdateForeground()
        {
            UpdateForegroundElements();
        }

        


        /// <summary>
        /// The Update.
        /// </summary>
        /// <param name="dt">The dt<see cref="double"/>.</param>
        public void Update()
        {
            UpdateBackground();
            UpdateForeground();
        }
    }
}
