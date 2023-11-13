using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoTest : MonoBehaviour
{
    // Start is called before the first frame update

    private static Sequence _sequence;
    Vector3[] wps = new Vector3[3] {  new Vector3(-75, 10, 30), new Vector3(-70, 10, 35), new Vector3(-75, 10, 30) };
    void Start()
    {
        _sequence = DOTween.Sequence();
        _sequence.Append(transform.DOPath(wps, 3f, PathType.CubicBezier));
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            _sequence.Restart();
        }
    }
}
