using UnityEngine;
public abstract class BaseView : MonoBehaviour{
    public bool isPopup = false;
    protected UIManager _uiManager;
    public virtual void Initialize(UIManager manager){ _uiManager = manager; }

    public virtual void Show(){ gameObject.SetActive(true); }
    public virtual void Hide(){ gameObject.SetActive(false); }
}