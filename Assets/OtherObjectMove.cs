using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OtherObjectMove : MonoBehaviour
{
    public GameObject otherObject;
    // Start is called before the first frame update
    void Start()
    {
        // ��������� ����
        // �ּ��Դϴ�
        // transform = ����
        // otherObject.transform = �ٸ� ������Ʈ

        transform.position = otherObject.transform.position - Vector3.forward * 5;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
