using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target, farBackground;

    // private float lastXPos, lastYPos;
    private Vector2 lastPos;

    public float minHeight, maxHeight;


    // Start is called before the first frame update
    void Start()
    {
        lastPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        FollowTarget();    
    }

    void FollowTarget() {
        /*
            transform.position = new Vector3(target.position.x, transform.position.y, transform.position.z);

            float clampedY = Mathf.Clamp(transform.position.y, minHeight, maxHeight);   // makes sure that the value of trans.position.y will not go over the limits

            transform.position = new Vector3(transform.position.x, clampedY, transform.position.z);
        */    

        transform.position = new Vector3(target.position.x, Mathf.Clamp(target.position.y, minHeight, maxHeight), transform.position.z);

        Vector2 amountToMove = new Vector2(transform.position.x - lastPos.x, transform.position.y - lastPos.y);

        farBackground.position += new Vector3(amountToMove.x, amountToMove.y, 0f);

        lastPos = transform.position;

    }
}
