using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirPlatform : MonoBehaviour
{
    public Vector3 Turnpoint;
    Vector3 targetPosition, originosition;

    public float MoveSpeed;

    // Start is called before the first frame update
    void Start()
    {
        originosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position == originosition) {
            targetPosition = Turnpoint;
        }
        else if (transform.position == Turnpoint) {
            targetPosition = originosition;
        }


        transform.position = Vector3.MoveTowards(transform.position, targetPosition, MoveSpeed* Time.deltaTime);
    }
}
