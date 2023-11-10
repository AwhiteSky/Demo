using EzySlice;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class meshtest : MonoBehaviour
{

    //public GameObject sourceGo;//切割的物体
    //public GameObject slicerGo;//切片物体
    //public Material sectionMat;//切面材质
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
        //检测鼠标横向移动
        float mx = Input.GetAxis("Mouse X");
        transform.Rotate(mx * 2, 0, 0);
        if (Input.GetMouseButtonDown(0))
        {
            //盒子射线检测
            Collider[] colliders = Physics.OverlapBox(transform.position,
                           new Vector3(3.64f, 0.005f, 2.76f),
                           transform.rotation,
                           ~LayerMask.GetMask("Solied"));
            //将每一个检测到的进行切割
            foreach (Collider c in colliders)
            {
                Destroy(c.gameObject);
                //GameObject[] objs=c.gameObject.SliceInstantiate(transform.position, transform.up);

                //添加切割面的材质
                //切割并返回表皮
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
                        //因为切割之后是不规则物体，所以要选择 MeshCollider（网格碰撞）
                        //如果一个MeshCollider是刚体，要想正常碰撞，一定要将convex设true
                        //Unity的规定：这样会形成一个凸多面体，只有凸多面体才能是刚体
                        o.AddComponent<MeshCollider>().convex = true;
                    }
                }
            }
        }
    }
}
