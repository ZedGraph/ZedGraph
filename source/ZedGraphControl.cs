namespace ZedGraph
{
    partial class ZedGraphControl
    {
        #region Methods

        private void EnableToolTip()
        {
            this.pointToolTip.Active = true;
        }

        private bool DisableToolTip()
        {
            return this.pointToolTip.Active = false;
        }

        #endregion
    }
}
