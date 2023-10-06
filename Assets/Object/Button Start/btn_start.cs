using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class btn_start : MonoBehaviour
{
    public static btn_start Instance;
    private SpriteRenderer spriteRenderer;
    private BoxCollider2D boxcollider2d;
    public bool isvisible;
    public bool state;
    public bool isClick;
    //public bool state = false;
    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log("Start in btn Start");
        isvisible = false;
        isClick = false;
        state = false;
        spriteRenderer = Instance.GetComponent<SpriteRenderer>();
        boxcollider2d = Instance.GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isClick==true)
        {
            spriteRenderer.enabled = false;
            boxcollider2d.enabled = false;
        }

    }
    private void OnMouseDown()
    {
        StartCoroutine(Waitting());
    }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
           // DontDestroyOnLoad(gameObject); // Đảm bảo GameObject này không bị hủy khi chuyển scene
        }
        else
        {
            Destroy(gameObject);
        }
    }
    IEnumerator Waitting()
    {
        spriteRenderer.enabled = false;
        boxcollider2d.enabled = false;
        yield return new WaitForSecondsRealtime(0.5f);

        state = true;
        isClick = true;
        AudioManager.Instance.musicSource.Stop();
        AudioManager.Instance.PlayMusic("Underwater");

    }

}
