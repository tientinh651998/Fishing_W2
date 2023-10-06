using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Setting : MonoBehaviour
{
    public GameObject Panel;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Click()
    {
        if (Panel.active == true) Panel.SetActive(false);
        else Panel.SetActive(true);

    }
}
