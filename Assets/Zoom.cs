using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zoom : MonoBehaviour
{

    private void Start()
    {
        float screenRatio = (float)Screen.width / (float)Screen.height;
        float targetRatio = (float)1920 / 1080;

        if ( screenRatio>= targetRatio)
        {
            Camera.main.orthographicSize = Camera.main.orthographicSize * (targetRatio / screenRatio);
        }
 
    }
}
