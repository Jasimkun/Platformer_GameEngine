using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OtherObjectMove : MonoBehaviour
{
    public GameObject otherObject;
    // Start is called before the first frame update
    void Start()
    {
        // 쓰고싶은말 쓰기
        // 주석입니다
        // transform = 본인
        // otherObject.transform = 다른 오브젝트

        transform.position = otherObject.transform.position - Vector3.forward * 5;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
