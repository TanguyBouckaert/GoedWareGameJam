using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladder : MonoBehaviour
{
    public PlayerMovement Pm;

    [SerializeField] private GameObject _player;

    private bool _ladderProximity;

    private float _playerSpeed;

    private void Start()
    {
        Pm.Down.performed += Down_performed;
    }

    private void Down_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        if (_ladderProximity)
            _player.transform.Translate(new Vector2(0, -_playerSpeed));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject hitObj = collision.gameObject;

        if (hitObj.layer == 6)
        {
            _playerSpeed = _player.GetComponent<PlayerMovement>()._playerSpeed;
            _ladderProximity = true;
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        GameObject hitObj = collision.gameObject;

        if (hitObj.layer == 6)
            _ladderProximity = false;

    }
}
