using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cat : MonoBehaviour
{
    [SerializeField]
    LineRenderer laser1;

    [SerializeField]
    LineRenderer laser2;
    

    [SerializeField]
    GameObject eye1;

    [SerializeField]
    GameObject eye2;

    void Awake()
    {
        laser1.SetPosition(0, eye1.transform.position);
        laser2.SetPosition(0, eye2.transform.position);
    }

    void Update()
    {
        laser1.SetPosition(0, eye1.transform.position);
        laser2.SetPosition(0, eye2.transform.position);
    }
}
