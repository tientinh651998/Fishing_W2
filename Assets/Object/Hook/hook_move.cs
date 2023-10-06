using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine;
using Spine.Unity;
public class hook_move : MonoBehaviour
{
    public static hook_move Instance;
    public Transform Target;
    private float speed = 8f;
    public MeshRenderer meshRenderer;
    public bool state;
    public SkeletonAnimation skeletonAnimation;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        meshRenderer = this.gameObject.GetComponent<MeshRenderer>();
        skeletonAnimation = GetComponent<SkeletonAnimation>();
        meshRenderer.enabled = false;
        state = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (CameraMove.Instance.isCameramove == true && btn_start.Instance.isClick == true && state==false)
         StartCoroutine(Waitting());

    }
    IEnumerator Waitting()
    {
        //Debug.Log("hook move");
        yield return new WaitForSecondsRealtime(0.5f);
        meshRenderer.enabled = true;
        if (state==false)
        transform.position = Vector3.MoveTowards(transform.position, Target.position, speed * Time.deltaTime);
        if (transform.position== Target.position)
        {
            state = true;
        }

    }
}
