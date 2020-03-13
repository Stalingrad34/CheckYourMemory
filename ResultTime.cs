using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultTime : MonoBehaviour
{
    public Text minutes;
    public Text seconds;

    private void Start()
    {
        minutes.text = GamePlay.minutesTime.text;
        seconds.text = GamePlay.secondsTime.text;
    }
}
