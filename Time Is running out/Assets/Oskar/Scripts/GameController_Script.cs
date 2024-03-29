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
    [SerializeField] GameObject CardCanvas;
    [SerializeField] Card_Display_Script_Left LeftDisplay;
    [SerializeField] Card_Display_Script_Right RightDisplay;
    [SerializeField] Shooting_Script shooting_Script;
    public bool cardSelect;
    public bool cardSelected;
    int nextWave;
    public int Wave;
    Random RNG = new Random();

    [Header("Enemies")]
    [SerializeField] public List<GameObject> Enemies = new List<GameObject>();

    [Header("Enemies")]
    [SerializeField] public int enemiesAlive = 0;


    [Header("Timer")]
    public float MaxTime = 120f;
    [SerializeField] public float remainingTime;
    public TimerBar_Script timerBar;
    // l�gg in en cap p� 2 min WIP
    void Start()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;

        timerBar.SetMaxTime(MaxTime);

        CardCanvas.SetActive(false);
        CurrentGameState = GameState.Normal;
        NewWave();
        nextWave = Wave;
    }

    void Update()
    {

        enemiesAlive = GameObject.FindGameObjectsWithTag("Enemy").Length;

        if (cardSelect)
        {
            if (!cardSelected && nextWave == Wave)
            {
                nextWave = Wave + 1;
                Invoke("CardSelection", 1);
            }
            else if (cardSelected)
            {
                NewWave();
                CardCanvas.SetActive(false);
                cardSelect = false;
                cardSelected = false;
            }
        }
        else if (enemiesAlive <= 0)
        {
            cardSelect = true;
        }

        if (remainingTime >= 120f)
        {
            remainingTime = 120f;
        }
    }
    void CardSelection()
    {
        CardCanvas.SetActive(true);
        RightDisplay.RandomizeCard();
        LeftDisplay.RandomizeCard();

    }
    void NewWave()
    {
        Wave++;
        Vector2 spawnPoint = Vector2.zero;
        for (int i = 0; i < (Wave) + 1; i++)
        {
            do
            {
                float angle = RNG.Next(0, 360);
                angle = angle * math.PI / 180;
                spawnPoint = Player.transform.position + (Vector3)(new Vector2(math.cos(angle), math.sin(angle)) * RNG.Next(7, 9));
            }
            while (spawnPoint.x <= -22 || spawnPoint.x >= 24 || spawnPoint.y >= 12 || spawnPoint.y <= -12);
            if (Wave < Enemies.Count)
            {
                Instantiate(Enemies[RNG.Next(0, Wave)], spawnPoint, Quaternion.identity);
            }
            else
            {
                Instantiate(Enemies[RNG.Next(0, Enemies.Count)], spawnPoint, Quaternion.identity);
            }
        }
    }
}
