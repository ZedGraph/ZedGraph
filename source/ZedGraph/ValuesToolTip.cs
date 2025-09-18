namespace ZedGraph
{
    using System;
    using System.Drawing;
    using System.Windows.Forms;

    public class ValuesToolTip : IValuesToolTip
    {
        #region Fields

        /// <summary>
        /// The last caption that was set.
        /// </summary>
        private string lastCaption;

        /// <summary>
        /// The last point a tool tip caption was set at.
        /// </summary>
        private Point lastPoint;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ValuesToolTip"/> class.
        /// </summary>
        /// <param name="control">The control.</param>
        /// <param name="activeCallback">The active callback.</param>
        /// <param name="setToolTipCallback">The set tool tip callback.</param>
        /// <exception cref="System.ArgumentNullException">
        /// control
        /// or
        /// activeCallback
        /// or
        /// setToolTipCallback
        /// </exception>
        public ValuesToolTip(Control control, Action<bool> activeCallback, Action<Control, string> setToolTipCallback)
        {
            if (control == null)
            {
                throw new ArgumentNullException("control");
            }

            if (activeCallback == null)
            {
                throw new ArgumentNullException("activeCallback");
            }

            if (setToolTipCallback == null)
            {
                throw new ArgumentNullException("setToolTipCallback");
            }

            this.ActiveCallback = activeCallback;
            this.SetCallback = setToolTipCallback;
            this.Control = control;
        }

        #endregion

        #region Properties and Indexers

        /// <summary>
        /// Gets the delegate that is called when the active state of the tool
        /// tip is changed.
        /// </summary>
        /// <value>
        /// The active state delegate callback.
        /// </value>
        public Action<bool> ActiveCallback { get; private set; }

        /// <summary>
        /// Gets the control that this tool tip instance handles.
        /// </summary>
        /// <value>
        /// The control that this tool tip instance handles.
        /// </value>
        public Control Control { get; private set; }

        /// <summary>
        /// Gets the callback delegate to call when the caption is set.
        /// </summary>
        /// <value>
        /// The callback delegate to call when the caption is set.
        /// </value>
        public Action<Control, string> SetCallback { get; private set; }

        #endregion

        #region Methods

        /// <summary>
        /// Disables the tool tip.
        /// </summary>
        public void Disable()
        {
            this.ActiveCallback(false);
        }

        /// <summary>
        /// Enables the tool tip.
        /// </summary>
        public void Enable()
        {
            this.ActiveCallback(true);
        }

        /// <summary>
        /// Sets the specified caption.
        /// </summary>
        /// <param name="caption">The caption.</param>
        public void Set(string caption)
        {
            this.Set(caption, this.lastPoint);
        }

        /// <summary>
        /// Sets the caption for the tool tip at the specified point.
        /// </summary>
        /// <param name="caption">The caption.</param>
        /// <param name="point">The point.</param>
        public void Set(string caption, Point point)
        {
            if (point != this.lastPoint || caption != this.lastCaption)
            {
                this.SetCallback(this.Control, caption);
                this.lastPoint = point;
                this.lastCaption = caption;
            }
        }

        /// <summary>
        /// Creates a <see cref="ValuesToolTip"/> for the specified control,
        /// using the supplied tooltip to display values.
        /// </summary>
        /// <param name="control">The control.</param>
        /// <param name="toolTip">The tool tip.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException">toolTip</exception>
        public static ValuesToolTip Create(Control control, ToolTip toolTip)
        {
            if (toolTip == null)
            {
                throw new ArgumentNullException("toolTip");
            }

            return new ValuesToolTip(control, active => toolTip.Active = active, toolTip.SetToolTip);
        }

        #endregion
    }
}
