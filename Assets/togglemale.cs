using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class togglemale : MonoBehaviour
{
    public static togglemale Instance;
    public Toggle toggle;
    // Start is called before the first frame update
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        toggle = GetComponent<Toggle>();
        toggle.isOn = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (toggle.isOn == true) load_list_item.Instance.toneguide = "Male";
        else
            load_list_item.Instance.toneguide = "Female";
    }


    public void updateValue()
    {
    }
}
