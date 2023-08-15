using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LowVelocityStopper
{
    //if rb.velocity is below a threshold for an amount of time, stop it
    BaseUnit unit;
    Rigidbody rb;
    float threshold;
    float timeRequired;
    float timeElapsed;
    public LowVelocityStopper(BaseUnit _unit, Rigidbody _rb, float _threshold, float _timeRequired)
    {
        unit = _unit;
        rb = _rb;
        threshold = _threshold;
        timeRequired = _timeRequired;
    }

    public void StopIfLowVelocity()
    {
        if (ServiceLocator.Instance.stopGoManager.gameState == GameState.stopped) return;
        if (rb.velocity.sqrMagnitude < threshold * threshold)
        {
            Debug.Log("Below threshold!");
            timeElapsed += Time.deltaTime;
            if (timeElapsed > timeRequired)
            {
                Debug.Log("Stopped!");
                rb.velocity = Vector3.zero;
                ServiceLocator.Instance.stopGoManager.ReportStoppedUnit(unit);
                timeElapsed = 0;
            }
        }
        else
        {
            timeElapsed = 0;
        }
    }

}
