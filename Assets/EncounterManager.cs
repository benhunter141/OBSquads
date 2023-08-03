using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EncounterManager : MonoBehaviour
{
    public TextMeshProUGUI countdownText;
    private IEnumerator Start()
    {
        yield return Countdown(3);
        //units start with 'idle' tactics until Fight:
        SetTacticsToChase();
    }

    public void SetTacticsToChase()
    {
        foreach (var unit in ServiceLocator.Instance.unitManager.unitList)
        {
            //set tactics to attack for all units for now
            unit.tactics = ServiceLocator.Instance.soManager.chase;

            //later: units get tactics selected by player or AI on a per-squad basis
            //class Squad { Tactics tactic }
        }
    }

    private IEnumerator Countdown(int seconds)
    {
        while(seconds > 0)
        {
            countdownText.text = seconds.ToString();
            seconds--;
            yield return FadeTextOverTime(countdownText, 1);
        }

        countdownText.text = "Fight!";
        StartCoroutine(FadeTextOverTime(countdownText, 2));
    }

    IEnumerator FadeTextOverTime(TextMeshProUGUI tmp, float time)
    {
        int frames = (int)(time * 50);
        float fraction = 1 / (float)frames;
        for(int i = 0; i < frames; i++)
        {
            Color color = tmp.color;
            color.a = 1f - fraction * i;
            tmp.color = color;
            yield return Helpers.EndOfFrame;
        }
    }


}
