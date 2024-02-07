using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Card_Display_Script_Left : MonoBehaviour
{

    [SerializeField] public Card_Information_Script card_left;

    public TMP_Text nameText;
    public Image artWorkImage;

    void Start()
    {
        nameText.text = card_left.name; // ändra så att nameText är ett image
        artWorkImage.sprite = card_left.artwork;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            //Card_Information_Script.
        }
    }
}
