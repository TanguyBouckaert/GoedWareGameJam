using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MoveAnimation : MonoBehaviour
{
    public PlayerMovement Pm;

    public Animator anim;

    private void Start()
    {
        Pm._leftRight.performed += _leftRight_performed;
    }

    private void _leftRight_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        if (obj.ReadValue<float>() > 0)
        {
            anim.SetFloat("MoveLeft", 1);
            anim.SetFloat("MoveRight", 0);
        }
           
        if (obj.ReadValue<float>() < 0)
        {
            anim.SetFloat("MoveLeft", 0);
            anim.SetFloat("MoveRight", 1);
        }
            

    }

}
