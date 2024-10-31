using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Slider slider;

    [SerializeField]
    private bool shouldMove;

    [SerializeField]
    private float enemySpeed;

    private float yPos;

    [SerializeField]
    private Transform transform1;
    [SerializeField]
    private Transform transform2;

    private float maxHealth;

    private int currentSelected = 1;
    [SerializeField]
    private float health = 100f;

    //private int maskLayer = 6 << 8;
    private float damageDone = 0;

    private void UpdateHealthBar(float currentValue, float maxValue)
    {
        slider.value = currentValue / maxValue;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.gameObject.name);
        if (collision.gameObject.name == "Monster")
        {
            HurtPeople(10f);
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

    // Start is called before the first frame update
    void Start()
    {
        maxHealth = health;
        yPos = transform1.position.y;
        transform1.position = new Vector3(transform1.position.x, yPos, transform1.position.z);
        transform2.position = new Vector3(transform2.position.x, yPos, transform2.position.z);
    }

    public void HurtPeople(float scareDamage)
    {
        health -= scareDamage;
        damageDone += scareDamage;
        Debug.Log(health);
        UpdateHealthBar(maxHealth - damageDone, maxHealth);
        if (health <= 0)
        {
            Debug.Log("Too scary");
            Destroy(transform.parent.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (shouldMove)
        {
            Debug.Log("Test1");
            if (currentSelected == 1)
            {
                Debug.Log("Test2");
                transform.position = Vector3.MoveTowards(transform.position, transform1.position, enemySpeed);
                if (transform.position == transform1.position)
                {
                    Debug.Log("Test4");
                    currentSelected = 2;
                }
            }
            else if (currentSelected == 2)
            {
                Debug.Log("Test3");
                transform.position = Vector3.MoveTowards(transform.position, transform2.position, enemySpeed);
                if (transform.position == transform2.position)
                {
                    Debug.Log("Test5");
                    currentSelected = 1;
                }
            }

        }
    }
}
