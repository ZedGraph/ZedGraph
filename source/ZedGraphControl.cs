namespace ZedGraph
{
    partial class ZedGraphControl
    {
        #region Methods

        private void EnableToolTip()
        {
            this.pointToolTip.Active = true;
        }

        private void SetToolTip(string caption)
        {
            this.pointToolTip.SetToolTip(this, caption);
        }

        private bool DisableToolTip()
        {
            return this.pointToolTip.Active = false;
        }

        #endregion
    }
}
