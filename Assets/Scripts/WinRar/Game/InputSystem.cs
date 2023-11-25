using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputSystem : MonoBehaviour
{
    public bool Jump { get; private set; }
    public bool Crouch { get; private set; }

    void Update()
    {
        Jump = Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W);
        Crouch = Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S);
    }
}

