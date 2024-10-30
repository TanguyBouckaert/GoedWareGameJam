using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Staires : MonoBehaviour
{
    public PlayerMovement Pm;

    [SerializeField] private Collider2D _stairColl;

    private bool _playerInRange = false;

    private void Start()
    {
        Pm.Up.performed += EnableStaires;
        Pm.Down.performed += EnableStaires;
    }

    private void EnableStaires(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        if(_playerInRange)
            _stairColl.enabled = true;
        if(!_playerInRange)
            _stairColl.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject hitObj = collision.gameObject;

        if(hitObj.layer == 6)
            _playerInRange = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        GameObject hitObj = collision.gameObject;

        if (hitObj.layer == 6)
        {
            _playerInRange = false;
            _stairColl.enabled = false;
        }
            
    }
}
