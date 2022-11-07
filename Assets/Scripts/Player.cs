using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 1.0f;

    public PathMover pathMover;

    public bool isMoving = true;

    private float startZ;

    void Start()
    {
        startZ = transform.position.z;
    }

    void Update()
    {
        if (isMoving)
        {
            transform.Translate(0, 0, speed * Time.deltaTime);
            var offset = transform.position.z - startZ;
            pathMover.setGlobalOffsetZ(offset);
        }
    }
}
