using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    public GameObject cube;
    public Rigidbody cube_rigidbody;
    public Transform playTransform;

    private float _power = 1;
    private float _speed = 5;

    private float _maxPower = 12;
    private bool _canJump = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            _power += Time.deltaTime * _speed;
            if (_power > 12)
                _power = 12;

            playTransform.localScale = new Vector3(1f,1f-(0.8f * _power/ _maxPower),1f);
        }

        if (Input.GetMouseButtonUp(0))
        {
            if(_canJump)
                cube_rigidbody.velocity = _power * Vector3.one;
            //Debug.Log(_power);
            StartCoroutine(ChangeScaleBack(playTransform));
            _power = 1;
        }
    }

    // Åö×²¿ªÊ¼
    void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag=="Ground")
        {
            if (_canJump == false) _canJump = true;
        }
    }

    // Åö×²½áÊø
    void OnCollisionExit(Collision collision)
    {
        _canJump = false;
    }

    public IEnumerator ChangeScaleBack(Transform playtransform, float time=0.01f)
    {

        while (playtransform.localScale.y<1)
        {
            var scale = playtransform.localScale;
            scale += new Vector3(0, 0.1f, 0);
            playtransform.localScale = scale;
            yield return new WaitForSeconds(time);
        }
    }

}
