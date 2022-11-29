using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollow : MonoBehaviour
{
    [SerializeField] Transform player;
    float distanceToFollowZ;
    float distanceToFollowY;
    public bool isRamping;
    void Start()
    {
        distanceToFollowZ = player.position.z - transform.position.z;
        distanceToFollowY = player.position.y - transform.position.y;
    }
    void Update()
    {
        if(!isRamping)
        transform.position =  new Vector3(transform.position.x, transform.position.y, player.transform.position.z - distanceToFollowZ);
        else
        {
            transform.position = new Vector3(transform.position.x, player.transform.position.y - distanceToFollowY, player.transform.position.z - distanceToFollowZ);
        }
    }
}
