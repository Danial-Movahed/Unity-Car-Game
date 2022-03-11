using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowLap : Lap
{
    public Text lapText;
    void FixedUpdate()
    {
        lapText.text = lap.ToString();
    }
}
