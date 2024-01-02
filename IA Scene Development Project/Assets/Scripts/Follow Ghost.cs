using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowGhost : MonoBehaviour
{
    public GameObject Target;

    //Movement
    public float maxSpeed;
    public float maxRot;

    Vector3 direction;
    private float angle;

    void Start()
    {}

    void Update()
    {
        direction = Target.transform.position - transform.position;

        angle = Mathf.Rad2Deg * Mathf.Atan2(direction.x, direction.z);
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.up);

        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * maxRot);
        transform.position += transform.forward.normalized * maxSpeed * Time.deltaTime;
    }

}
