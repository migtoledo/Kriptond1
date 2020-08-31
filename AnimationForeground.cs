namespace Kriptond1
{
    using System.Windows.Controls;

    /// <summary>
    /// Defines the <see cref="AnimationForeground" />.
    /// </summary>
    internal class AnimationForeground
    {
        /// <summary>
        /// Defines the g.
        /// </summary>
        internal Canvas g;

        /// <summary>
        /// Defines the Armadillo.
        /// </summary>
        internal Boundary armadillo;

        /// <summary>
        /// Defines the reloj.
        /// </summary>
        internal Reloj reloj;

        /// <summary>
        /// Initializes a new instance of the <see cref="AnimationForeground"/> class.
        /// </summary>
        public AnimationForeground()
        {
            Init();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AnimationForeground"/> class.
        /// </summary>
        /// <param name="panel">The panel<see cref="Canvas"/>.</param>
        public AnimationForeground(Canvas panel)
        {
            SetCanvasPanel(panel);
            armadillo = new Boundary(panel);
            reloj = new Reloj(panel);
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
        /// The init.
        /// </summary>
        public void Init()
        {
            armadillo.Init();
            reloj.Init();
            reloj.SetR(100);
            reloj.SetR(Globals.GetNaturalMaxSize());
        }

        /// <summary>
        /// The addElements.
        /// </summary>
        public void AddElements()
        {
            armadillo.AddElements();
            reloj.AddElements();
        }

        /// <summary>
        /// The updateElements.
        /// </summary>
        internal void UpdateElements()
        {
            armadillo.Update();
            reloj.SetR(Globals.GetNaturalMaxSize()/10);
            reloj.Update();
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
