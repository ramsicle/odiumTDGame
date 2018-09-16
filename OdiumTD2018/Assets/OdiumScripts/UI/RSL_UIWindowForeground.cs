using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using Devdog.General.UI;

public partial class RSL_UIWindowForeground : MonoBehaviour {

    public bool onWindowShowBringToForeground = true;
    public int maxForegroundIndex = 10;

    //private RectTransform _rectTransform;
    private UIWindow _window;
    //private UIWindow currentWindow;

    public UIWindow window {
        get {
            if (_window == null)
                _window = GetComponent<UIWindow>();

            return _window;
        }
        set { _window = value; }
    }

    public void Awake() {
        //_rectTransform = GetComponent<RectTransform>();
        if (onWindowShowBringToForeground) {
            window.OnShow += MoveToForeground;
        }
    }

    /// <summary>
    /// Bring this draggable window all the way to the front (maxSiblingIndex)
    /// </summary>
    public virtual void MoveToForeground() {
        //if (currentWindow == this)
        //    return; // Already top window.

        transform.SetSiblingIndex(maxForegroundIndex);
        //currentWindow = this;
    }
}