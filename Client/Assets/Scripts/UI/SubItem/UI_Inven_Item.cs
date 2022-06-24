using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Inven_Item : UI_Base
{
    enum GameObjects
    {
        ItemIcon,
        ItemNameText,
    }

    string _name = "";

    public override void Init()
    {
        Bind<GameObject>(typeof(GameObjects));
        GameObject go = Get<GameObject>((int)GameObjects.ItemNameText);
        if (go)
        {
            TextMesh text = go.GetComponent<TextMesh>();
            if (text)
                text.text = _name;
        }

        Get<GameObject>((int)GameObjects.ItemIcon).BindEvent((PointerEventData) => { Debug.Log($"������ Ŭ��! {_name}"); });
    }

    public void SetInfo(string name)
    {
        _name = name;
    }
}
