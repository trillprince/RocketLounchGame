using System;

public interface IUIController
{
    public event Action<bool> OnUIActive;
    public void UIActive(bool isActive);
}