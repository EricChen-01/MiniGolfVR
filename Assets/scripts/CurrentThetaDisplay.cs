using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CurrentThetaDisplay : MonoBehaviour
{
    private TextMeshProUGUI text;
    public GolfBallController golfBall;

    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
         text.text = "Current Swing Angle: " + golfBall.hitAngle;
    }
}
