using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform playerTransform; // —сылка на Transform игрока

    void Start()
    {

    }

    void Update()
    {
        FollowPlayer();
    }

    void FollowPlayer()
    {
        Vector3 cameraPosition = transform.position;
        cameraPosition.y = playerTransform.position.y;
        transform.position = cameraPosition;
    }
}
