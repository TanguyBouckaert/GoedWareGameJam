using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private InputAction _LeftRight;

    private void OnEnable()
    {
        _LeftRight.Enable();
    }

    private void OnDisable()
    {
        _LeftRight.Disable();
    }


    // Update is called once per frame
    void Update()
    {
        if (_LeftRight.ReadValue<float>() > 0)
        {
            transform.Translate(new Vector2(-1,0));
        }
        if(_LeftRight.ReadValue<float>() < 0)
        {
            transform.Translate(new Vector2(1, 0));
        }
    }
}
