using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jelly_fish_move : MonoBehaviour
{
    public float speed = 1.5f;
    private Vector3 bottomLeft;
    private Vector3 bottomRight ;
    private Vector3 topLeft ;
    private Vector3 topRight;

    private Vector3 Target;
    // Start is called before the first frame update
    void Start()
    {
        if (transform.localScale.x > 0 && transform.localScale.y > 0)
        {
            bottomLeft = transform.position;
            topRight = new Vector3(bottomLeft.x + 16, bottomLeft.y + 9, 0);
            Target = topRight;
        }

        else
        {
            bottomRight = transform.position;
            topLeft = new Vector3(bottomRight.x - 16, bottomRight.y + 9, 0);
            Target = topLeft;
        }


    }
    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, Target, speed * Time.deltaTime);   

        if (transform.position==Target)
        {
            // đảo chiều 
            Vector3 scale = transform.localScale;
            scale.x *= -1; scale.y *= -1;
            transform.localScale = scale;


            if (Target == topRight)
            {
                Target = bottomLeft;
            }
            else
            if (Target == bottomLeft)
            {
                Target = topRight;
            }


            if (Target == topLeft)
            {
                Target = bottomRight;
            }
            else
            if (Target == bottomRight)
            {
                Target = topLeft;
            }

        }
        

    }
}
