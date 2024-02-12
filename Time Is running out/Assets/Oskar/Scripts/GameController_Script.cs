using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController_Script : MonoBehaviour
{
    //[Header("Game Globals")]
    [SerializeField] public enum GameState { Normal, Pause, GameOver }
    [SerializeField] public GameState CurrentGameState;

    [Header ("Enemies")]
    [SerializeField] public GameObject MeleeEnemy;
    [SerializeField] public GameObject RangeEnemy;
    [SerializeField] public GameObject BombEnemy;
    [SerializeField] public GameObject MudEnemy;

    public int Wave;

    [Header("Enemies")]
    [SerializeField] public int enemiesAlive = 0;


    [Header("Timer")]
    [SerializeField] public float remainingTime;

    void Start()
    {
        CurrentGameState = GameState.Normal;
    }

    void Update()
    {
        enemiesAlive = GameObject.FindGameObjectsWithTag("Enemy").Length;
        if(enemiesAlive <= 0)
        {
            SceneManager.LoadScene(1);
        }
    }
}
