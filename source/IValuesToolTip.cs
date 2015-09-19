namespace ZedGraph
{
    using System.Drawing;

    public interface IValuesToolTip
    {
        /// <summary>
        /// Disables the tool tip.
        /// </summary>
        void Disable();

        /// <summary>
        /// Enables the tool tip.
        /// </summary>
        void Enable();

        /// <summary>
        /// Sets the caption for the tool tip at the specified point.
        /// </summary>
        /// <param name="caption">The caption.</param>
        /// <param name="point">The point.</param>
        void Set(string caption, Point point);
    }
}
