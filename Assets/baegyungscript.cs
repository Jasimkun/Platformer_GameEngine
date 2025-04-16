using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class baegyungscript : MonoBehaviour
{
    public Transform player;
    [SerializeField] private float a = 0.3f;

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(player.position.x, player.position.y + a, player.position.z);
    }
}
