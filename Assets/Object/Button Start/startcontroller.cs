using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class startcontroller : MonoBehaviour
{
    public GameObject btnstart;
    private SpriteRenderer spriteRenderer;
    private BoxCollider2D boxcollider2d;
    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log("Start in controller");
        spriteRenderer = btnstart.GetComponent<SpriteRenderer>();
        boxcollider2d = btnstart.GetComponent<BoxCollider2D>();
        spriteRenderer.enabled = false;
        boxcollider2d.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("Start controller");
        if (thuyen_meo.Instance.state == true && btn_start.Instance.isvisible == false && btn_start.Instance.isClick==false )
        {
           // btn_start.Instance.isvisible = true;
            StartCoroutine(Waitting());
        }
    }
    IEnumerator Waitting()
    {
        yield return new WaitForSecondsRealtime(1f);
        spriteRenderer.enabled = true;
        boxcollider2d.enabled = true;
        //Debug.Log("Update in controller");
    }


}
