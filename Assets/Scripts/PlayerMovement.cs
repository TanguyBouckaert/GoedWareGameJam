using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] public float _playerSpeed = 0.5f;

    [SerializeField]
    private float attackCooldown = 100f;
    private float attackTimer = 0;

    public InputAction _leftRight;

    [SerializeField] private InputAction _leftMouse;
    private bool _leftMousePressed = false;

    public InputAction Up, Down;

    private bool _ladder = false;

    [SerializeField]
    private float maxDistance = 5f;

    [SerializeField]
    private MoveAnimation _moveAnimation;

    private List<GameObject> enemies = new List<GameObject>();

    private void Start()
    {
        foreach (Enemy go in FindObjectsOfType<Enemy>())
        {
            enemies.Add(go.gameObject);
        }
    }

    private GameObject FindClosestEnemy()
    {
        int chosenEnemy = -1;
        float distance = 0;
        for(int i = 0; i < enemies.Count; i++)
        {
            float tempDistance = Vector3.Distance(transform.position, enemies[i].transform.parent.position);
            if (chosenEnemy == -1)
            {
                chosenEnemy = i;
                distance = tempDistance;
            }
            else if (tempDistance < distance)
            {
                chosenEnemy = i;
                distance = tempDistance;
            }
        }
        return enemies[chosenEnemy];
    }

    private void OnEnable()
    {
        _leftMouse.Enable();
        _leftRight.Enable();
        Up.Enable();
        Down.Enable();
    }

    private void OnDisable()
    {
        _leftMouse.Disable();
        _leftRight.Disable();
        Up.Disable();
        Down.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        attackTimer++;

        //LeftRight movement
        if (_leftRight.ReadValue<float>() > 0)
        {
           transform.Translate(new Vector2(-_playerSpeed, 0));
        }
           
            
        if (_leftRight.ReadValue<float>() < 0)
        {
            transform.Translate(new Vector2(_playerSpeed, 0));
        }
            

        if (Up.ReadValue<float>() == 1 && _ladder)
            transform.Translate(new Vector2(0, _playerSpeed));

        if(_leftMouse.ReadValue<float>() > 0 && attackTimer > attackCooldown)
        {
            attackTimer = 0;
            GameObject closestEnemy = FindClosestEnemy();
            if (closestEnemy == null) return;
            if(Vector3.Distance(closestEnemy.transform.parent.position,transform.position) < maxDistance)
            {
                closestEnemy.GetComponent<Enemy>().HurtPeople(5f);
            }

            //Vector2 direction = Vector2.zero;
            //if (_moveAnimation.anim.GetFloat("MoveLeft") == 1)
            //{
            //    direction = (transform.position - new Vector3(1, 0, 0)) - transform.position;
            //}
            //else
            //{
            //    direction = (transform.position+new Vector3(1,0,0)) - transform.position;
            //}
            //RaycastHit2D hit = Physics2D.Raycast (transform.position, direction);
            //Debug.DrawRay(transform.position, direction*hit.distance, Color.red);
            //if (hit.collider != null)
            //{
            //    Debug.Log(hit.collider.gameObject.name);
            //    if(hit.collider.gameObject.name == " ActualEnemy")
            //    {
            //        Debug.Log("Woah");
            //        hit.collider.gameObject.GetComponent<Enemy>().HurtPeople(5f);
            //    }
            //}
            //  Debug.DrawRay(transform.position, mousePos - transform.position,Color.black,10f);
            _leftMousePressed = true;
        }
        else
        {
            _leftMousePressed = false;
        }

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
