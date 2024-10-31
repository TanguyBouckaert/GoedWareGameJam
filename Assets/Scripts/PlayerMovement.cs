using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerMovement : MonoBehaviour
{
    public Image m_Image;

    public Sprite[] MovingRightAnimation, MovingLeftAnimation;

    [SerializeField] public float _playerSpeed = 0.5f, m_AnimationSpeed = .02f;

    [SerializeField] private InputAction _leftRight;

    public InputAction Up, Down;

    private bool _ladder = false, IsDone;

    private int m_IndexSprite;

    Coroutine m_CorotineAnim;

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
        //if (_leftRight.ReadValue<float>() == 0)
        //{
        //    StopCoroutine(Func_PlayAnimUI(MovingRightAnimation));
        //    StartCoroutine(Func_PlayAnimUI(MovingLeftAnimation));
        //}

        //LeftRight movement
        if (_leftRight.ReadValue<float>() > 0)
        {
           transform.Translate(new Vector2(-_playerSpeed, 0));

            //StopCoroutine(Func_PlayAnimUI(MovingRightAnimation));
            //StartCoroutine(Func_PlayAnimUI(MovingLeftAnimation));
        }
           
            
        if (_leftRight.ReadValue<float>() < 0)
        {
            transform.Translate(new Vector2(_playerSpeed, 0));

            //StopCoroutine(Func_PlayAnimUI(MovingLeftAnimation));
            //StartCoroutine(Func_PlayAnimUI(MovingRightAnimation));
        }
            

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

    //IEnumerator Func_PlayAnimUI(Sprite[] m_SpriteArray)
    //{
    //    yield return new WaitForSeconds(m_AnimationSpeed);
    //    if (m_IndexSprite >= m_SpriteArray.Length)
    //    {
    //        m_IndexSprite = 0;
    //    }
    //    m_Image.sprite = m_SpriteArray[m_IndexSprite];
    //    m_IndexSprite += 1;
    //    if (IsDone == false)
    //        m_CorotineAnim = StartCoroutine(Func_PlayAnimUI(m_SpriteArray));
    //}
}
