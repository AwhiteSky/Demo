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
    public GameObject arrow;

    private float _power = 1;
    private float _speed = 5;

    private float _maxPower = 12;
    private bool _canJump = false;
    private float _maxDis = 10;
    private bool _isGameOver = false;

    private float testTime = 0;

    public Transform activeParabola;
    public Transform closeParabola;

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
                if (!arrow.activeSelf)
                    arrow.SetActive(true);
                _power += Time.deltaTime * _speed;
                if (_power > 12)
                    _power = 12;

                playTransform.localScale = new Vector3(1f, 1f - (0.8f * _power / _maxPower), 1f);

                
                var dis = gameCamera.transform.forward;
                dis.y = 0;
                dis = arrow.transform.position + dis.normalized;
                arrow.transform.LookAt(dis);
            }

            if (Input.GetMouseButtonUp(0))
            {
                if (arrow.activeSelf)
                    arrow.SetActive(false);
                if (_canJump)
                {
                    Vector3 d = gameCamera.transform.forward.normalized;
                    d.y = 1;

                    CreateParabola(cube.transform.position, _power * (d));
                    cube_rigidbody.velocity = _power * (d);
                }
                    
                //Debug.Log(_power);
                StartCoroutine(ChangeScaleBack(playTransform));
                _power = 1;
            }

            //testTime += Time.deltaTime;
            //if (testTime > 0.2f)
            //{
                
            //    testTime -= 0.2f;
            //    Debug.Log("velocity:  " + cube_rigidbody.velocity);
            //    Debug.Log("position:  " + cube.transform.position);
            //}
        }
    }

    // 碰撞开始
    void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag=="Ground")
        {
            _isGameOver = true;

            while(activeParabola.childCount > 0)
            {
                var a = activeParabola.GetChild(0);
                a.parent = closeParabola;
                a.position = Vector3.zero;
            }
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

    // 碰撞结束
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

    public void CreateParabola(Vector3 pos,Vector3 vel)
    {

        for (float t = 0; t < 2; t += 0.2f)
        {
            Vector3 newPostion = pos + new Vector3(vel.x * t, vel.y * t + t * t * (-9.8f) / 2, vel.z * t);
            if(activeParabola.childCount==10)
            {
                activeParabola.GetChild((int)(t / 0.2f)).transform.position = newPostion;
            }
            else
            {
                if(closeParabola.childCount>0)
                {
                    closeParabola.GetChild(0).position = newPostion;
                    closeParabola.GetChild(0).parent = activeParabola;
                }
                else
                {
                    GameObject Go = Instantiate(Resources.Load<GameObject>("Prefabs/Sphere"), activeParabola);
                    Go.transform.position = newPostion;
                }
                
            }

        }
    }

}
