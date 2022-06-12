using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Button : UI_Base
{
    enum Buttons
    {
        PointButton
    }

    enum Texts
    {
        PointText,
        ScoreText
    }

    enum GameObjects
    {
        TestObject,
    }

    private void Start()
    {
        Bind<Button>(typeof(Buttons));
        Bind<Text>(typeof(Texts));
        Bind<GameObject>(typeof(GameObjects));

        if (null != Get<Text>((int)Texts.ScoreText))
            Get<Text>((int)Texts.ScoreText).text = "Bind Test";
    }

    int _score = 0;
    public void OnButtonClicked()
    {
        _score++;
    }
}
