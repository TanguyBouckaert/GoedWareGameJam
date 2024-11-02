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
    private int _chosenEnemy = -1;
    [SerializeField]
    private float attackCooldown = 100f;
    private float attackTimer = 0;

    public InputAction _leftRight;

    [SerializeField] private InputAction _leftMouse;
    private bool _leftMousePressed = false;

    public InputAction Up, Down;

    private bool _ladder = false;

    [SerializeField]
    private float maxDistance = 1f;

    [SerializeField]
    private GameManager gameManager;

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
        float distance = 0;
        Debug.Log(enemies.Count);
        
        for(int i = 0; i < enemies.Count; i++)
        {
            float tempDistance = Vector3.Distance(transform.position, enemies[i].transform.parent.position);
            if (_chosenEnemy == -1)
            {
                _chosenEnemy = i;
                distance = tempDistance;
            }
            else if(distance == 0)
            {
                _chosenEnemy = i;
                distance = tempDistance;
            }
            else if (tempDistance < distance)
            {
                _chosenEnemy = i;
                distance = tempDistance;
            }
        }
        if (enemies.Count <= 0)
        {
            gameManager.GameWinner();
            return null;
        }
        return enemies[_chosenEnemy];
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

    public void DeleteEnemy()
    {
        enemies.RemoveAt(_chosenEnemy);
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
            Debug.Log(Vector3.Distance(closestEnemy.transform.parent.position, transform.position));
            Debug.Log(maxDistance);
            Debug.Log(Vector3.Distance(closestEnemy.transform.parent.position, transform.position) < maxDistance);
            if (Vector3.Distance(closestEnemy.transform.parent.position,transform.position) < maxDistance)
            {
                Vector3 dir = transform.position - closestEnemy.transform.parent.position;
                closestEnemy.GetComponent<Enemy>().HurtPeople(10f, Convert.ToInt32(dir.x));
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
