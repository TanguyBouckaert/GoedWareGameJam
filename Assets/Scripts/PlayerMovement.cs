using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] public float _playerSpeed = 0.5f;

    [SerializeField] private InputAction _leftRight;

    public InputAction Up, Down;

    private bool _ladder = false;

    private void OnEnable()
    {
        _leftRight.Enable();
        Up.Enable();
        Down.Enable();
    }

    private void OnDisable()
    {
        _leftRight.Disable();
        Up.Disable();
        Down.Disable();
    }


    // Update is called once per frame
    void Update()
    {
        //LeftRight movement
        if (_leftRight.ReadValue<float>() > 0)
            transform.Translate(new Vector2(-_playerSpeed,0));
        if(_leftRight.ReadValue<float>() < 0)
            transform.Translate(new Vector2(_playerSpeed, 0));

        if (Up.ReadValue<float>() == 1 && _ladder)
            transform.Translate(new Vector2(0, _playerSpeed));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject hitObj = collision.gameObject;

        if(hitObj.layer == 8)
            _ladder = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        GameObject hitObj = collision.gameObject;

        if (hitObj.layer == 8)
            _ladder = false;
    }
}
