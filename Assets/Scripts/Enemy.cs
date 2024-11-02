using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Slider slider;

    [SerializeField]
    private bool shouldMove;

    [SerializeField]
    private float enemySpeed;

    private float standardSpeed;

    [SerializeField]
    private float enemyRunSpeed;

    private float yPos;

    private Transform transform1;
    private Transform transform2;

    private NewLevel _levelManager;

    private float maxHealth;

    private int currentSelected = 1;
    [SerializeField]
    private float health = 100f;

    //private int maskLayer = 6 << 8;
    private float damageDone = 0;

    [SerializeField]
    private int _currentRoom = 0;

    private Vector3 targetPosition;


    private void UpdateHealthBar(float currentValue, float maxValue)
    {
        slider.value = currentValue / maxValue;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Monster")
        {

        }
    }

    private void OnDrawGizmosSelected()
    {
        if(shouldMove)
        {
            if (transform1 != null) Gizmos.DrawIcon(transform1.position,"target.png",true,new Color(255,0,0,50));
            if (transform2 != null) Gizmos.DrawIcon(transform2.position, "target.png", true,new Color(255, 0, 0, 50));  
        }
    }

    private void ChangeRoom(int roomChange)
    {
        if (roomChange > 0)
        {
            roomChange = 1;
        }
        else
        {
            roomChange = -1;
        }

        if((_currentRoom+roomChange) < 0)
        {
            _currentRoom += 1;
            List<Transform> tempTransforms =  _levelManager.GetPositionsInRoom(_currentRoom);
            transform1 = tempTransforms[0];
            transform2 = tempTransforms[1];
           // targetPosition = transform1.position;
        }
        else if(_currentRoom + roomChange > (_levelManager.levelPositions.Count - 1))
        {
            _currentRoom -= 1;
            List<Transform> tempTransforms = _levelManager.GetPositionsInRoom(_currentRoom);
            transform1 = tempTransforms[0];
            transform2 = tempTransforms[1];
        }
        else if(((_currentRoom + roomChange) >= 0) && (_currentRoom + roomChange <= (_levelManager.levelPositions.Count - 1)))
        {
            _currentRoom += roomChange;
            List<Transform> tempTransforms = _levelManager.GetPositionsInRoom(_currentRoom);
            transform1 = tempTransforms[0];
            transform2 = tempTransforms[1];
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        _levelManager = FindObjectOfType<NewLevel>();
        standardSpeed = enemySpeed;
        List<Transform> tempTransforms = _levelManager.GetPositionsInRoom(-_currentRoom);
        transform1 = tempTransforms[0];
        transform2 = tempTransforms[1];
        maxHealth = health;
        yPos = transform1.position.y;
        transform1.position = new Vector3(transform1.position.x, yPos, transform1.position.z);
        transform2.position = new Vector3(transform2.position.x, yPos, transform2.position.z);
    }


    public void HurtPeople(float scareDamage,int changeDir)
    {
        health -= scareDamage;
        damageDone += scareDamage;
        UpdateHealthBar(maxHealth - damageDone, maxHealth);
        ChangeRoom(changeDir);
        enemySpeed = enemyRunSpeed;
        if (health <= 0)
        {
            _levelManager.CancelEnemy();
            Destroy(transform.parent.gameObject);
        }
    }

    void Update()
    {

        if (shouldMove)
        {
            
            if (currentSelected == 1)
            {
                
                transform.parent.position = Vector3.MoveTowards(transform.position, transform1.position, enemySpeed);
                if (transform.position == transform1.position)
                {
                    enemySpeed = standardSpeed;
                    currentSelected = 2;
                }
            }
            else if (currentSelected == 2)
            {
                
                transform.position = Vector3.MoveTowards(transform.position, transform2.position, enemySpeed);
                if (transform.parent.position == transform2.position)
                {
                    enemySpeed = standardSpeed;
                    currentSelected = 1;
                }
            }

        }
    }
}
