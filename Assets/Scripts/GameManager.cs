using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] float _timeLimitInSec;

    [SerializeField] Text _text;

    //Detect how many npc's there still are in the game
    private GameObject[] _npcs;

    private float _timer;

    private int _min, _sec;

    // Start is called before the first frame update
    void Start()
    {
        _npcs = GameObject.FindGameObjectsWithTag("NPC");
    }

    // Update is called once per frame
    void Update()
    {
        _timeLimitInSec -= Time.deltaTime;

         _min = Mathf.FloorToInt(_timeLimitInSec / 60F);
         _sec = Mathf.FloorToInt(_timeLimitInSec - _min * 60);

        _text.text = string.Format("{0:0}:{1:00}", _min, _sec);

        if (_npcs.Length == 0 )
            GameWinner();
        if (_timer > _timeLimitInSec)
            GameOver();
    }

    public void GameWinner()
    {
        SceneManager.LoadScene("EndScreen");
    }

    void GameOver()
    {
        SceneManager.LoadScene("FailScreen");
    }
}
