using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public class thuyen_meo : MonoBehaviour
{
    public static thuyen_meo Instance;
    public SkeletonAnimation skeletonAnimation;
    public Transform Target;
    public Transform PointA;
    public Transform PointB;
    public Transform PointC;
    public bool state;
    private float TimeMove = 2.5f;
    private float speed;
    private float distance;
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

    void Start()
    {
        distance = Vector3.Distance(PointA.position, PointB.position);
        speed = distance / TimeMove;
        state = false;
        skeletonAnimation = GetComponent<SkeletonAnimation>();
    }

    // Update is called once per frame
    void Update()
    {
        if (state == false)
        {
            transform.position = Vector3.MoveTowards(Target.position, PointB.position, speed * Time.deltaTime);
            if (Target.position == PointB.position) state = true;
        }
    }

}
