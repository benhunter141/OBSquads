using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EncounterManager : MonoBehaviour
{
    public TextMeshProUGUI countdownText;
    public int countDownTime;
    private IEnumerator Start()
    {
        yield return Countdown(countDownTime);
        IssueOrders();
    }

    public void CheckForVictory()
    {
        Squad first = ServiceLocator.Instance.unitManager.firstSquad;
        Squad second = ServiceLocator.Instance.unitManager.secondSquad;
        bool firstDead = first.IsDead();
        bool secondDead = second.IsDead();

        if (firstDead && secondDead)
        {
            DeclareDraw();
        }
        else if (firstDead)
        {
            DeclareVictoryFor(second);
        }
        else if (secondDead)
        {
            DeclareVictoryFor(first);
        }

    }

    void DeclareVictoryFor(Squad squad)
    {
        Debug.Log($"squad {squad.squadName} won");
        foreach(var u in squad.units)
        {
            u.tactics = ServiceLocator.Instance.soManager.idle;
        }
        countdownText.text = "Victory!";
        StartCoroutine(FadeTextOverTime(countdownText, 5));
    }

    void DeclareDraw()
    {
        Debug.Log("draw!");
    }

    public void IssueOrders()
    {
        Squad first = ServiceLocator.Instance.unitManager.firstSquad;
        Squad second = ServiceLocator.Instance.unitManager.secondSquad;
        first.IssueOrders();
        second.IssueOrders();
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
