namespace NewWave.FastTrader.Client.UI.Behaviors
{
    public class CloseWindowButtonBehavior : WindowButtonBehavior
    {
        protected override void OnButtonClicked()
        {
            AssociatedWindow.Close();
        }
    }
}
