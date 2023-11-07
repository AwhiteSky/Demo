using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrownPoint : MonoBehaviour
{
    public ThrownItem item;

    public Transform point;
    public float powerMulti;
    public Vector3 velocity;//46
    float time;
    public bool debug;
    void Update()
    {
        //if (UnityEngine.Input.GetKey(KeyCode.Mouse0))
        //{
        //    time += Time.deltaTime;
        //    velocity = transform.forward * powerMulti * time;
        //}


        if (UnityEngine.Input.GetKeyUp(KeyCode.Mouse0))
        {
            velocity = transform.forward * powerMulti;
            var clone = Instantiate(item, transform.position, transform.rotation);
            clone.Velocity = velocity;
            clone.active = true;
            velocity = Vector3.zero;
            time = 0;
            if (debug)
            {
                Debug.Break();
                debug = false;
            }
        }

    }
}
