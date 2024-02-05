using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Card_SelectManager_Script : MonoBehaviour
{
    public static Card_SelectManager_Script instance;

    public GameObject[] Cards;

    public GameObject LastSelected { get; set; }
    public int LastSelectedIndex { get; set; }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void OnEnable()
    {
        StartCoroutine(SetSelectedAfterOneFrame());
    }

    private void Update()
    {
        //if we move right
        if (InputManager_Script.instance.NavigationInput.x > 0)
        {
            //select next card
            HandleNextCardSelection(1);
        }


        //if we move left
        if (InputManager_Script.instance.NavigationInput.x < 0)
        {
            //select prior card
            HandleNextCardSelection(-1);
        }

    }

    private IEnumerator SetSelectedAfterOneFrame()
    {
        yield return null;
        EventSystem.current.SetSelectedGameObject(Cards[0]);
    }

    private void HandleNextCardSelection(int addition)
    {
        if (EventSystem.current.currentSelectedGameObject == null && LastSelected != null)
        {
            int newIndex = LastSelectedIndex + addition;
            newIndex = Mathf.Clamp(newIndex, 0, Cards.Length - 1);
            EventSystem.current.SetSelectedGameObject(Cards[newIndex]);
        }
    }
}
