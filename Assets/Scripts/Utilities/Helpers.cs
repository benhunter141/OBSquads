using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class Helpers
{

    static Dictionary<float, WaitForSeconds> _timeInterval = new Dictionary<float, WaitForSeconds>(100);

    static WaitForEndOfFrame _endOfFrame = new WaitForEndOfFrame();
    static WaitForSeconds _shortDelay = new WaitForSeconds(0.2f);
    static WaitForSeconds _oneSecond = new WaitForSeconds(1f);

    public static WaitForSeconds OneSecond
    {
        get { return _oneSecond; }
    }
    public static WaitForEndOfFrame EndOfFrame
    {
        get { return _endOfFrame; }
    }
    public static WaitForSeconds ShortDelay
    {
        get { return _shortDelay; }
    }

    static WaitForFixedUpdate _fixedUpdate = new WaitForFixedUpdate();
    public static WaitForFixedUpdate FixedUpdate
    {
        get { return _fixedUpdate; }
    }

    public static WaitForSeconds Get(float seconds) //caches whatever you do.. eg. call Helpers.WaitForSeconds ??? 
    {
        if (!_timeInterval.ContainsKey(seconds))
            _timeInterval.Add(seconds, new WaitForSeconds(seconds));
        return _timeInterval[seconds];
    }
    public static IEnumerator PauseForDuration(float duration) //DOES THIS WORK??
    {
        float pauseFrames = duration / Time.fixedDeltaTime;
        for (int i = 0; i < pauseFrames; i++)
        {
            yield return Helpers.FixedUpdate;
        }
    }

    public static void SetLayerRecursively(GameObject go, int layerNumber)
    {
        foreach (Transform trans in go.GetComponentsInChildren<Transform>(true))
        {
            trans.gameObject.layer = layerNumber;
        }
    }

    public static Vector2 PositionOnCircle(float theta, float radius)
    {
        float xCoord = Mathf.Cos(theta) * radius;
        float yCoord = Mathf.Sin(theta) * radius;
        return new Vector2(xCoord, yCoord);
    }
    public static Vector2 PositionOnCircle(float theta)
    {
        float xCoord = Mathf.Cos(theta);
        float yCoord = Mathf.Sin(theta);
        return new Vector2(xCoord, yCoord);
    }

    public static Vector3 PositionOnCircle(float theta, float radius, Vector3 xDirection, Vector3 yDirection)
    {
        Vector2 positionOn2DCircle = PositionOnCircle(theta, radius);
        Vector3 xComponent = xDirection * positionOn2DCircle.x;
        Vector3 yComponent = yDirection * positionOn2DCircle.y;
        return xComponent + yComponent;
    }

    public static Vector3 PositionOnCircle(float theta, Vector3 axis, Vector3 startingDisplacementFromCentre)
    {
        Vector3 xVector = startingDisplacementFromCentre; //this is (1,0) on the unit circle
        Vector3 yVector = Vector3.Cross(xVector, axis); //this is (0,1) on the unit circle
        Vector3 xComponent = xVector * Mathf.Cos(theta);
        Vector3 yComponent = yVector * Mathf.Sin(theta);
        return xComponent + yComponent;
    }

    public static Vector3 FlatForward(Vector3 forward) => new Vector3(forward.x, 0, forward.z);
    

}