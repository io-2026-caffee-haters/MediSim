public abstract class HUDView : BaseView
{
    // HUD nie blokuje kliknięć pod spodem, więc domyślnie 
    // upewniamy się, że nie zachowuje się jak popup.
    public override void Initialize(UIManager manager)
    {
        base.Initialize(manager);
        SetInteractable(false); // Blokujemy interakcję całego tła, klikalne będą tylko przyciski wewnątrz
    }
}