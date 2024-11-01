using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stairs : MonoBehaviour
{
    [SerializeField] private Collider2D _stairColl;

    private PlayerMovement _pm;
    private bool _playerInRange = false;

    private void Start()
    {
        _pm = GameObject.Find("Monster").GetComponent<PlayerMovement>();

        if (_pm == null)
            Debug.Log("No playermovement script found!");

        _pm.Up.performed += EnableStaires;
        _pm.Down.performed += EnableStaires;
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
