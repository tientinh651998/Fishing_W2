using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIController : MonoBehaviour
{
    public Slider sliderVolume;
    public void Volume()
    {
        AudioManager.Instance.Volume(sliderVolume.value);
    }
}
