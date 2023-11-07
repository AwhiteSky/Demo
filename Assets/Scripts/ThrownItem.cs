using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrownItem : MonoBehaviour
{
    public Vector3 Velocity;
    public LayerMask layer;
    public Vector3 Gravity = Physics.gravity;
    public bool active;

    private Vector3 current_pos;//当前位置
    private Vector3 start;
    private float i = 1f;

    //额外视觉效果 使模型可以旋转
    public float rotateSpeed = 600f;
    public Transform model;
    Vector3 defaultRotate;
    void Start()
    {
        start = transform.position;
        current_pos = start;
        if (model) defaultRotate = model.eulerAngles;
    }


    private void Update()
    {
        if (active)
        {
            model.Rotate(Vector3.right * rotateSpeed * Time.deltaTime, Space.Self);//在空中旋转
        }
    }
    private void FixedUpdate()
    {
        CalculateThrownMove(Time.fixedDeltaTime);
    }

    /// <summary>
    /// 核心计算 
    /// </summary>
    void CalculateThrownMove(float tick)
    {
        if (active)
        {
            float time = tick * i;
            var gravity = Gravity * time * time / 2;//计算重力
            var nex = Velocity * time;//下一个位置
            Vector3 next_pos = start + nex + gravity; //下一个位置
            Debug.DrawLine(current_pos, next_pos, Color.yellow);
            transform.position = current_pos;
            if (Physics.Linecast(current_pos, next_pos, out RaycastHit hit, layer, QueryTriggerInteraction.Ignore))
            {
                model.eulerAngles = defaultRotate + new Vector3(Random.Range(1, 46), 0, 0); //更改旋转角度

                active = false;
                Debug.Log(hit.transform.name);//击中了 做一些判断
                Destroy(gameObject, 5);
            }
            current_pos = next_pos;
            i++; //很重要  
        }
    }
}
