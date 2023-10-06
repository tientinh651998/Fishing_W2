using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public static CameraMove Instance;
    public Transform Target;

    public bool isCameramove;
    private float TimeMove = 1f;
    private float speed;
    private float distance;
    public Transform PosCamera;
    // Start is called before the first frame update
    void Start()
    {
        PosCamera.position = transform.position;
        distance = Vector3.Distance(Target.position, transform.position);
        speed = distance / TimeMove;
        isCameramove = false;
        //Debug.Log(speed);
    }

    // Update is called once per frame
    void Update()
    {
        if (btn_start.Instance.isClick == true && isCameramove==false)
        {
            transform.position = Vector3.MoveTowards(transform.position, Target.position, speed * Time.deltaTime);
            if (transform.position == Target.position) isCameramove = true;
        }
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
}
