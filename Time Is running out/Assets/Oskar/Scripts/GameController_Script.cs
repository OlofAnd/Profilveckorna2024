using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = System.Random;

using Unity.Mathematics;

public class GameController_Script : MonoBehaviour
{
    [SerializeField] public enum GameState { Normal, Pause, GameOver }
    [SerializeField] public GameState CurrentGameState;
    [SerializeField] GameObject Player;
    Random RNG = new Random();

    [Header("Enemies")]
    [SerializeField] public List<GameObject> Enemies = new List<GameObject>();
    public int Wave = 0;

    [Header("Enemies")]
    [SerializeField] public int enemiesAlive = 0;


    [Header("Timer")]
    [SerializeField] public float remainingTime;
    void Start()
    {
        CurrentGameState = GameState.Normal;
        NewWave();
    }

    void Update()
    {
        enemiesAlive = GameObject.FindGameObjectsWithTag("Enemy").Length;
        if (enemiesAlive <= 0)
        {
            NewWave();
        }
        Debug.Log(enemiesAlive);
    }
    void NewWave()
    {
        Wave++;
        for (int i = 0; i < Wave * 2; i++)
        {
            float angle = RNG.Next(0, 360);
            angle = angle * math.PI / 180;
            Vector2 spawnPoint = Player.transform.position + (Vector3)(new Vector2(math.cos(angle), math.sin(angle)) * RNG.Next(7,9));
            Instantiate(Enemies[RNG.Next(0, Enemies.Count)], spawnPoint, Quaternion.identity);
        }
    }
}
