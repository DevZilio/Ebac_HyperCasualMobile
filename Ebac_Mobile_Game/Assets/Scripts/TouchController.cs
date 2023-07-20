using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class TouchController : MonoBehaviour
{
    public Vector2 pastPosition;
    public float velocity = 3f;

    public void Update()
    {


        if (Input.GetMouseButton(0))
        {
            //MousePosition AGORA - mousePosition PASSADO
            Move(Input.mousePosition.x - pastPosition.x);
        }
        pastPosition = Input.mousePosition;
    }


    public void Move(float speed)
    {
        {
            transform.position += Vector3.right * Time.deltaTime * speed * velocity;
        }
    }

//    public float speed = 1f;

//     public void Update()
//     {
//         float horizontalInput = Input.GetAxis("Horizontal");
//         Move(horizontalInput);
//     }

//     public void Move(float direction)
//     {
//         transform.position += Vector3.right * Time.deltaTime * direction * speed;
//     }

}
