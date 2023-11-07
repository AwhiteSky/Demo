using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrownItem : MonoBehaviour
{
    public Vector3 Velocity;
    public LayerMask layer;
    public Vector3 Gravity = Physics.gravity;
    public bool active;

    private Vector3 current_pos;//��ǰλ��
    private Vector3 start;
    private float i = 1f;

    //�����Ӿ�Ч�� ʹģ�Ϳ�����ת
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
            model.Rotate(Vector3.right * rotateSpeed * Time.deltaTime, Space.Self);//�ڿ�����ת
        }
    }
    private void FixedUpdate()
    {
        CalculateThrownMove(Time.fixedDeltaTime);
    }

    /// <summary>
    /// ���ļ��� 
    /// </summary>
    void CalculateThrownMove(float tick)
    {
        if (active)
        {
            float time = tick * i;
            var gravity = Gravity * time * time / 2;//��������
            var nex = Velocity * time;//��һ��λ��
            Vector3 next_pos = start + nex + gravity; //��һ��λ��
            Debug.DrawLine(current_pos, next_pos, Color.yellow);
            transform.position = current_pos;
            if (Physics.Linecast(current_pos, next_pos, out RaycastHit hit, layer, QueryTriggerInteraction.Ignore))
            {
                model.eulerAngles = defaultRotate + new Vector3(Random.Range(1, 46), 0, 0); //������ת�Ƕ�

                active = false;
                Debug.Log(hit.transform.name);//������ ��һЩ�ж�
                Destroy(gameObject, 5);
            }
            current_pos = next_pos;
            i++; //����Ҫ  
        }
    }
}
