using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class player : MonoBehaviour
{
    public GameObject cube;
    public Rigidbody cube_rigidbody;
    public Transform playTransform;
    public Camera gameCamera;
    public GameObject Aim;

    private float _power = 1;
    private float _speed = 5;

    private float _maxPower = 12;
    private bool _canJump = false;
    private float _maxDis = 10;
    private bool _isGameOver = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!_isGameOver)
        {
            if (Input.GetMouseButton(0))
            {
                _power += Time.deltaTime * _speed;
                if (_power > 12)
                    _power = 12;

                playTransform.localScale = new Vector3(1f, 1f - (0.8f * _power / _maxPower), 1f);
            }

            if (Input.GetMouseButtonUp(0))
            {
                if (_canJump)
                {
                    Vector3 d = gameCamera.transform.forward.normalized;
                    d.y = 1;
                    cube_rigidbody.velocity = _power * (d);
                }
                    
                //Debug.Log(_power);
                StartCoroutine(ChangeScaleBack(playTransform));
                _power = 1;
            }
        }
    }

    // Åö×²¿ªÊ¼
    void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag=="Ground")
        {
            _isGameOver = true;
        }
        else if(collision.transform.tag == "Step")
        {
            if (_canJump == false) _canJump = true;
        }
        Vector3 playpos = transform.position;
        playpos.y -= 0.5f;
        float dis = Vector3.Distance(gameCamera.transform.position, playpos);
        if(dis>_maxDis)
        {
            var d = (playpos - Aim.transform.position) * ((dis - _maxDis) / dis);
            var newCameraPos = Aim.transform.position + d;
            Aim.transform.DOMove(newCameraPos, 1);
            //gameCamera.transform.DOLookAt(transform.position, 1);
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

    public void NewGame()
    {
        _isGameOver = false;
        Aim.transform.position = new Vector3(1.96f, 5.95f, -12.79f);
        transform.position = new Vector3(-3.16f, 4.45f, -12.75f);
    }

}
