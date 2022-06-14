using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Popup : UI_Base
{
    public virtual void Init()
    {
        Managers.UI.SetCanvas(gameObject, true);
    }
    public virtual void ClosePopupUI()
    {
        Managers.UI.ClosePopupUI(this);
    }
}
