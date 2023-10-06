using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Spine;
using Spine.Unity;
using UnityEngine.SceneManagement;
using TMPro;
public class load_list_item : MonoBehaviour
{
    public static load_list_item Instance;
    private Collider2D hitCollider;
    private GameObject item;
    private SkeletonAnimation skeletonAnimation;
    private float distance;

    public GameObject background;
    public thuyen_meo Boat;
    public Camera camera;
    public hook_move hook;
    public Transform pos_item;
    public Transform pos_ngang;
    public Transform out_half;
    public Transform out_site;

    public TextMeshProUGUI  text;
    public Transform pos_center1;
    public Transform pos_center2;
    private bool move_ngang, move_item;
    private bool is_outhalf, is_outsite;
    private bool center_1, center_2;
    private bool state_pick;
    public string toneguide;
    private float elapsedTime = 0f; // Biến để lưu thời gian đã trôi qua
    private int seconds = 0; // Biến để lưu số giây
    private bool moving;
    private bool playing;
    private bool playing_victory;
    private bool playing_horay;
    private bool ending;
    private bool play_guide;

    private int dem;

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
        text.SetText("");
        dem = 0;
        moving = false;
        move_ngang = false;
        move_item = false;
        state_pick = false;
        is_outhalf = false;
        is_outsite = false;
        center_1 = false;
        center_2 = false;
        playing = false;
        distance = 0;
        playing_victory = false;
        playing_horay = false;
        ending = false;
        seconds = 0;
        play_guide = false;
        toneguide = "Male";
    }
    // Update is called once per frame
    void Update()
    {
        if (!moving && hook_move.Instance.state)
        {
            elapsedTime += Time.deltaTime;
            // Kiểm tra nếu đã đủ thời gian để tăng số giây
            if (elapsedTime >= 1f)
            {
                seconds++;
                elapsedTime -= 1f; // Đặt lại elapsedTime để tính tiếp sau 1 giây
            }
        }
        // play guide khi bat dau
        if (!moving && hook_move.Instance.state && !play_guide)
        {
            play_guide = true;

            AudioManager.Instance.PlaySFX(toneguide);
        }
        // play guide khi 10s
        if (!moving && hook_move.Instance.state && seconds==10 && play_guide==true)
        {
            AudioManager.Instance.PlaySFX(toneguide);
            seconds = 0;
        }



        if (!moving && hook_move.Instance.state && Input.GetMouseButtonDown(0))
        {
            // Lấy vị trí chuột trong không gian thế giới
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            // Kiểm tra xem vị trí chuột có chạm vào Collider2D của đối tượng này không
            hitCollider = Physics2D.OverlapPoint(mousePosition);
            if (hitCollider != null)
            {
                AudioManager.Instance.sfxSource.Stop();
                // Lấy item
                item = hitCollider.gameObject;
                dem++;
                // reset state
                move_ngang = true;
                moving = true;
                pos_item.position = new Vector3(item.transform.position.x + 1.83f, item.transform.position.y - 1.6f, 0);
                pos_ngang.position = new Vector3(item.transform.position.x + 1.83f, hook.transform.position.y, 0);
                out_half.position = new Vector3(item.transform.position.x + 1.83f, item.transform.position.y - 1.6f + 4.5f, 0);
                out_site.position = new Vector3(item.transform.position.x + 1.83f, item.transform.position.y - 1.6f + 11f, 0);
                pos_center1.position = new Vector3(pos_center2.transform.position.x, item.transform.position.y - 1.6f + 11f, 0);
                pos_center2.position = hook.transform.position;
            }
        }
        // di chuyen sang ngang
        if (move_ngang && !move_item)
        {
            //Debug.Log("Move sang ngang");
            Move_Pick(hook.transform, pos_ngang, 0f, 5f);

            if (hook.transform.position == pos_ngang.position)
            {
                move_item = true;
                hook_move.Instance.skeletonAnimation.AnimationName="Moc gap do Open";
            }
        }
        // wait 0.2s
        // pick item
        if (move_item && !state_pick)
        {
            StartCoroutine(wait_02s_and_pick());
        }
        //delay 0.25s 
        // pick up half trong 0.5s
        if (state_pick && !is_outhalf)
        {
          
            StartCoroutine(wait_025s_and_movehalf());
        }
        //wait 0.4s move out site =>0.8
        // pick up out site
        if (is_outhalf && !is_outsite)
        {
            StartCoroutine(wait_04s_and_moveoutsite());
        }
        if (dem == 4 && is_outsite)

        {
            hook_move.Instance.meshRenderer.enabled = false;
            afternoon.Instance.spriteRenderer.enabled = true;
            if (!playing_victory)
            {
                AudioManager.Instance.PlaySFX("Victory");
                playing_victory = true;
            }
            StartCoroutine(Win());
        }
        // move hook to center 1
        if (dem!=4 &&is_outsite && !center_1)
        {
            playing = false;
            Move_Pick(hook.transform, pos_center1, 0f, 15f);
            if (hook.transform.position == pos_center1.position)
            {
                center_1 = true;
            }
        }
        //move hook to center 2.=> Start again

        if (dem != 4 &&  center_1 && !center_2)
        {
            background.SetActive(false);
            wave1.Instance.meshRenderer.enabled = false;
            Move_Pick(hook.transform, pos_center2, 0f, 8f);
            if (hook.transform.position == pos_center2.position)
            {
                moving = false;
                move_ngang = false;
                move_item = false;
                state_pick = false;
                is_outhalf = false;
                is_outsite = false;
                center_1 = false;
                center_2 = false;
                playing_victory = false;
                playing_horay = false;
                ending = false;
                seconds = 0;
            }
        }
    }
    IEnumerator wait02s_toplayguid()
    {
        yield return new WaitForSecondsRealtime(0.2f);
            AudioManager.Instance.PlaySFX(toneguide);
    }

    IEnumerator wait_04s_and_moveoutsite()
    {
        yield return new WaitForSecondsRealtime(1.2f);

        if (is_outhalf && is_outsite == false)
        {
           // Debug.Log("Move outsite");
            Move_Pick(hook.transform, out_site, 0.5f);
        }     
        if (hook.transform.position == out_site.position)
        {
            Destroy(item);
            is_outsite = true;
            playing = false;
            text.SetText("");
        }
    }

    IEnumerator Win()
    {
        yield return new WaitForSecondsRealtime(1f);

        Move_Pick(camera.transform, CameraMove.Instance.PosCamera.transform, 0f, 8f);
        if (camera.transform.position== CameraMove.Instance.PosCamera.transform.position && !ending)
        {
            thuyen_meo.Instance.skeletonAnimation.AnimationName = "Ending";
            thuyen_meo.Instance.skeletonAnimation.loop = false;
            StartCoroutine(Waithoray());
        }
    }
    IEnumerator Waithoray()
    {
        yield return new WaitForSecondsRealtime(2.5f);
        if (!playing_horay)
        {
            AudioManager.Instance.PlaySFX("Hooray");
            playing_horay = true;
        }
        //doi 0.5s horay => di chuyen ra
        StartCoroutine(wait_to_endgame());
    }
    IEnumerator wait_to_endgame()
    {
        yield return new WaitForSecondsRealtime(1.5f);
        ending = true;
        thuyen_meo.Instance.skeletonAnimation.AnimationName = "Bien mat";
        thuyen_meo.Instance.skeletonAnimation.loop = true;
        Move_Pick(Boat.transform, thuyen_meo.Instance.PointC.transform, 0f, 5f);
        if (Boat.transform.position == thuyen_meo.Instance.PointC.transform.position)
        {
            //SceneManager.LoadScene("Main");
            UnityEditor.EditorApplication.isPlaying = false;
        }
    }

        IEnumerator wait_02s_and_pick()
    {
        yield return new WaitForSecondsRealtime(0.2f);
        if (!playing) { AudioManager.Instance.PlaySFX("MocKeo"); playing = true; }
        Move_Pick(hook.transform, pos_item, 0.5f);
        if (hook.transform.position == pos_item.position)
        {
            StartCoroutine(wait025s());
        }
    }

    IEnumerator wait025s()
    {
        yield return new WaitForSecondsRealtime(0.25f);
        hook_move.Instance.skeletonAnimation.AnimationName = "Moc gap do Close";
        state_pick = true;
        AudioManager.Instance.sfxSource.Stop();
        playing = false;
        distance = 0;
    }



    IEnumerator wait_025s_and_movehalf()
    {
        yield return new WaitForSecondsRealtime(0.25f);
        item.transform.SetParent(hook.transform);
        if (is_outhalf==false)
        Move_Pick(hook.transform, out_half, 0.5f);
        if (hook.transform.position == out_half.position)
        {
            is_outhalf = true;
            text.SetText(item.name);

            if (playing == false)
            {
                AudioManager.Instance.PlaySFX(item.name);
                playing = true;
            }
        }
    }

        void Move_Pick(Transform a, Transform b, float time,float speed)
    {
        if (speed == 0)
        {
            distance = Vector3.Distance(a.position, b.position);
            speed = distance / time;
        }
        else
            a.position = Vector3.MoveTowards(a.position, b.position, speed * Time.deltaTime);
    }


    void Move_Pick(Transform a, Transform b, float time)
    {
        if (distance == 0)
            distance = Vector3.Distance(a.position, b.position);

        a.position = Vector3.MoveTowards(a.position, b.position, distance / time * Time.deltaTime);
    }

}

