using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class afternoon : MonoBehaviour
{

    public static afternoon Instance;
    public SpriteRenderer spriteRenderer;

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
    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log("start");
        spriteRenderer = this.GetComponent<SpriteRenderer>();
        spriteRenderer.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
