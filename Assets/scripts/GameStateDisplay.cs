using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameStateDisplay : MonoBehaviour
{
    private TextMeshProUGUI text;

    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        GameMode mode = GameStateManager.CurrentGameMode();


        if (mode == GameMode.Build)
        {
            text.text = "Build Mode";
        }else if(mode == GameMode.Game)
        {
            text.text = "Game Mode";
        }else if(mode == GameMode.Edit)
        {
            text.text = "Edit Mode";
        }
    }
}
