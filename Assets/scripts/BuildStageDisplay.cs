using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BuildStageDisplay : MonoBehaviour
{
    private TextMeshProUGUI text;

    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        GameState state = GameStateManager.CurrentGameState();


        if (state == GameState.Build_PlaneSelection)
        {
            text.text = "Please Select a Plane";
        }
        else if (state == GameState.Build_MarkerPlacement)
        {
            text.text = "Please Place Makers on Plane";
        }
        else if (state == GameState.Build_RealWorldObjectPlacement)
        {
            text.text = "Please Place Real-world Object on Plane";
        }
        else if (state == GameState.Build_AugmentVirtualObjectPlacement)
        {
            text.text = "Please Place Augmented Object on Plane";
        }
        else
        {
            text.text = "";
        }
    }
}
