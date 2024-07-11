using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public event Action<Vector2> Look;
    public event Action<Vector2> Move;

    private void Update()
    {
        CheckLook();
        CheckMove();
    }

    private void CheckLook()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");
        Vector2 vec = new Vector2(mouseX, mouseY);
        Look?.Invoke(vec);
    }

    private void CheckMove()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        Vector2 vec = new Vector2(x, y);
        Move?.Invoke(vec);
    }
}
