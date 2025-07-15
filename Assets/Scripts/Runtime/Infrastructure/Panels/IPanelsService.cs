namespace Game.Runtime.Infrastructure.Panels
{
    public interface IPanelsService
    {
        public TPanel Open<TPanel>()
            where TPanel : PanelBase;

        public void Close<TPanel>()
            where TPanel : PanelBase;

        bool IsOpened<TPanel>(out TPanel popup)
            where TPanel : PanelBase;
    }
}