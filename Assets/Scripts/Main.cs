using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    public GameObject cube;
    public Rigidbody cube_rigidbody;

    private float _power = 1;
    private float _speed = 5;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButton(0))
        {
            _power += Time.deltaTime * _speed;
            
        }

        if (Input.GetMouseButtonUp(0))
        {
            cube_rigidbody.velocity = _power * Vector3.one;
            _power = 1;
        }
    }
}
