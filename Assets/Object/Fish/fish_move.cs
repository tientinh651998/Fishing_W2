using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fish_move : MonoBehaviour
{
    private Vector3 PointA;
    private Vector3 PointB;
    public float speed = 1.5f;

    private Vector3 Target;
    // Start is called before the first frame update
    void Start()
    {
        Camera mainCamera = Camera.main;
        // Tính toán kích thước của camera (bao gồm tỷ lệ khung hình)
        float cameraHeight = 2f * mainCamera.orthographicSize;
        float cameraWidth = cameraHeight * mainCamera.aspect;

        PointA = new Vector3(mainCamera.transform.position.x - cameraWidth / 2, transform.position.y, 0);
        PointB = new Vector3(mainCamera.transform.position.x + cameraWidth / 2, transform.position.y, 0);

       // Debug.Log(" A : " + PointA);
        if (transform.localScale.x > 0)
            Target = PointB;
        else Target = PointA;


    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x == Target.x && transform.position.y == Target.y)
        {
            Vector3 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;

            if (Target == PointA)
            {
                Target = PointB;
            }
            else
            {
                Target = PointA;
            }
        }
        transform.position = Vector3.MoveTowards(transform.position, Target, speed * Time.deltaTime);
    }
}
