using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class left_card_select_script : MonoBehaviour
{
    Card_Information_Script cardInfo;
    [SerializeField] GameController_Script GameController;

    [SerializeField] Card_Display_Script_Right cardDisplay;

    [SerializeField] Player_Script playerScript;
    [SerializeField] PlayerBullet_Script playerBulletScript;
    [SerializeField] Shooting_Script shootingScript;

    [SerializeField] FreezingAura_Script freezeAuraScript;
    [SerializeField] PheonixAbility_Script phoenixScript;
    [SerializeField] Rage_Script rageScript;
    [SerializeField] Tank_Script tankScript;

    public void Update()
    {
        Debug.Log("maxhp " + playerScript.maxHealth);
        Debug.Log("hp " + playerScript.currentHealth);
    }
    public void LeftButtonPressed()
    {
        cardInfo = GetComponent<Card_Display_Script_Right>().card_right;

        // tier 1
        if (cardInfo.name == "Heal")
            cardInfo.healToMax = true;
        else if (cardInfo.name == "Bullet Speed")
            cardInfo.bulletSpeed = true;
        else if (cardInfo.name == "Range")
            cardInfo.attackRange = true;
        // tier 2
        else if (cardInfo.name == "Bomb Rounds")
            cardInfo.bombRounds = true;
        else if (cardInfo.name == "Bullet spread")
            cardInfo.bulleSpread = true;
        else if (cardInfo.name == "Damage")
            cardInfo.damage = true;
        else if (cardInfo.name == "Max Hp")
            cardInfo.maxHp = true;
        else if (cardInfo.name == "Piercing Rounds")
            cardInfo.piercingRounds = true;
        // tier 3
        else if (cardInfo.name == "Freezing Aura")
            cardInfo.freezingAura = true;
        else if (cardInfo.name == "Phoenix")
            cardInfo.phoenix = true;
        else if (cardInfo.name == "rage")
            cardInfo.rage = true;
        else if (cardInfo.name == "Tank")
            cardInfo.tank = true;




        // tier 1
        if (cardInfo.healToMax)
        {
            playerScript.currentHealth = playerScript.maxHealth;
            Debug.Log(playerScript.currentHealth);
            cardInfo.healToMax = false;
        }
        else if (cardInfo.bulletSpeed)
        {
            playerBulletScript.force++; // lägg till mer sen beroende på hur mycket den ska addera

            Debug.Log(cardInfo.bulletSpeed);
            cardInfo.bulletSpeed = false;
        }
        else if (cardInfo.fireRate)
        {
            shootingScript.timeBetweenFiring -= 0.02f;

            Debug.Log(cardInfo.fireRate);
            cardInfo.fireRate = false;
        }
        else if (cardInfo.attackRange)
        {
            playerBulletScript.timeAlive += 0.5f;

            Debug.Log(cardInfo.attackRange);
            cardInfo.attackRange = false;
        }
        // tier 2
        else if (cardInfo.damage)
        {
            playerBulletScript.bulletDamage++;

            Debug.Log(cardInfo.damage);
            cardInfo.damage = false;
        }
        else if (cardInfo.maxHp)
        {
            playerScript.maxHealth += 25;
            playerScript.currentHealth = playerScript.maxHealth;
            cardInfo.maxHp = false;
        }
        else if (cardInfo.bombRounds)
        {
            shootingScript.playerBombChance += 10;

            Debug.Log(cardInfo.bombRounds);
            cardInfo.bombRounds = false;
        }
        else if (cardInfo.bulleSpread)
        {
            shootingScript.numberOfBullets++;

            Debug.Log(cardInfo.bulleSpread);
            cardInfo.bulleSpread = false;
        }
        else if (cardInfo.piercingRounds)
        {
            playerBulletScript.AmountPierce++;

            Debug.Log(cardInfo.piercingRounds);
            cardInfo.piercingRounds = false;
        }
        // tier 3
        else if (cardInfo.freezingAura)
        {
            freezeAuraScript.isFreezingAuraActive = true;

            Debug.Log(cardInfo.freezingAura);
        }
        else if (cardInfo.phoenix)
        {
            phoenixScript.isPheonixActive = true;

            Debug.Log(cardInfo.phoenix);
        }
        else if (cardInfo.rage)
        {
            rageScript.isRageActive = true;

            Debug.Log(cardInfo.rage);
        }
        else if (cardInfo.tank)
        {
            tankScript.isTankActive = true;
            playerScript.currentHealth = playerScript.maxHealth;
            Debug.Log(cardInfo.tank);
        }

        GameController.cardSelected = true;
    }
}
