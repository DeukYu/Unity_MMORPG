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

    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    public override void Init()
    {
        Bind<GameObject>(typeof(GameObjects));
        GameObject go = Get<GameObject>((int)GameObjects.ItemNameText);
        if(go)
            go.GetComponent<TMPro.TextMeshPro>().text = _name;   

        Get<GameObject>((int)GameObjects.ItemIcon).BindEvent((PointerEventData) => { Debug.Log($"������ Ŭ��! {_name}"); });
    }

    public void SetInfo(string name)
    {
        _name = name;
    }
}
