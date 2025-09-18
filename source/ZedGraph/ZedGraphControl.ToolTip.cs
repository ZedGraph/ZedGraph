namespace ZedGraph
{
    using System.Drawing;

    partial class ZedGraphControl
    {
        #region Fields

        /// <summary>
        /// The tool tip for displaying the cursor and point values.
        /// </summary>
        private readonly IValuesToolTip tooltip;

        #endregion

        #region Methods

        /// <summary>
        /// Enables the tool tip.
        /// </summary>
        private void EnableToolTip()
        {
            this.tooltip.Enable();
        }

        /// <summary>
        /// Sets the tool tip.
        /// </summary>
        /// <param name="caption">The caption.</param>
        /// <param name="point">The point.</param>
        private void SetToolTip(string caption, Point point)
        {
            this.tooltip.Set(caption, point);
        }

        /// <summary>
        /// Disables the tool tip.
        /// </summary>
        private void DisableToolTip()
        {
            this.tooltip.Disable();
        }

        #endregion
    }
}
