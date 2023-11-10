using EzySlice;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class meshtest : MonoBehaviour
{

    //public GameObject sourceGo;//�и������
    //public GameObject slicerGo;//��Ƭ����
    //public Material sectionMat;//�������
    // Start is called before the first frame update

    public Material cross;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.A))
        //{
        //    SlicedHull hull = sourceGo.Slice(slicerGo.transform.position, slicerGo.transform.up);
        //    GameObject upper = hull.CreateUpperHull(sourceGo, sectionMat);
        //    GameObject lower = hull.CreateLowerHull(sourceGo, sectionMat);
        //    sourceGo.SetActive(false);
        //}
        //����������ƶ�
        float mx = Input.GetAxis("Mouse X");
        transform.Rotate(mx * 2, 0, 0);
        if (Input.GetMouseButtonDown(0))
        {
            //�������߼��
            Collider[] colliders = Physics.OverlapBox(transform.position,
                           new Vector3(3.64f, 0.005f, 2.76f),
                           transform.rotation,
                           ~LayerMask.GetMask("Solied"));
            //��ÿһ����⵽�Ľ����и�
            foreach (Collider c in colliders)
            {
                Destroy(c.gameObject);
                //GameObject[] objs=c.gameObject.SliceInstantiate(transform.position, transform.up);

                //����и���Ĳ���
                //�и���ر�Ƥ
                SlicedHull hull = c.gameObject.Slice(c.transform.position, c.transform.up);
                //print(hull);
                if (hull != null)
                {
                    GameObject lower = hull.CreateLowerHull(c.gameObject, cross);
                    GameObject upper = hull.CreateUpperHull(c.gameObject, cross);
                    GameObject[] objs = new GameObject[] { lower, upper };

                    foreach (GameObject o in objs)
                    {
                        o.AddComponent<Rigidbody>();
                        //��Ϊ�и�֮���ǲ��������壬����Ҫѡ�� MeshCollider��������ײ��
                        //���һ��MeshCollider�Ǹ��壬Ҫ��������ײ��һ��Ҫ��convex��true
                        //Unity�Ĺ涨���������γ�һ��͹�����壬ֻ��͹����������Ǹ���
                        o.AddComponent<MeshCollider>().convex = true;
                    }
                }
            }
        }
    }
}
