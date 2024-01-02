using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlockingManager : MonoBehaviour
{
    public static FlockingManager FM;
    public GameObject Target;
    public GameObject bee; 
    public int num;
    public GameObject[] bees;
    public Vector3 Limits = new Vector3(5, 5, 5);
    public Vector3 WhereToGo = Vector3.zero;
    public Vector3 FollowDistance;
    [Range(0.0f,3.0f)]
    public float minspeed;
    [Range(0.0f, 3.0f)]
    public float maxspeed;

    [Range(1.0f, 20.0f)]
    public float neighbourDistance;
    [Range(1.0f, 5.0f)]
    public float rotationSpeed;


    void Start()
    {
        bees = new GameObject[num];
        for (int i = 0; i < num; i++)
        {
            Vector3 pos = this.transform.position + new Vector3(Random.Range(-Limits.x, Limits.x), Random.Range(-Limits.y, Limits.y), Random.Range(-Limits.z, Limits.z));
            WhereToGo = pos;
            bees[i] = (GameObject)Instantiate(bee, pos, Quaternion.identity);
            
        }
        FM = this;
    }
    void Update()
    {
        for(int i = 0; i < num; i++)
        {
            Vector3 TargetDistance = Target.transform.position - bees[i].transform.position;
            if (TargetDistance.x < FollowDistance.x&& TargetDistance.z < FollowDistance.z)
            {
                WhereToGo = Target.transform.position;
            }
        }      
    }
}
