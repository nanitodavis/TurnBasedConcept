using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BattleState { START, PLAYERTURN, ENEMYTURN, WON, LOST}
public class BattleSystem : MonoBehaviour
{
    public GameObject player;
    public GameObject enemy;

    public Transform playerPod;
    public Transform enemyPod;

    public BattleState state;

    Stats playerStats;
    Stats enemyStats;

    public UiManager uiManager;

    private void Start()
    {
        state = BattleState.START;
        StartCoroutine(SetUpBattle());
    }

    IEnumerator SetUpBattle()
    {
        GameObject p = Instantiate(player, playerPod);
        playerStats = p.GetComponent<Stats>();
        GameObject e = Instantiate(enemy, enemyPod);
        enemyStats = e.GetComponent<Stats>();
        uiManager.SetUpPlayerHUD(playerStats);
        uiManager.SetUpEnemyHUD(enemyStats);
        uiManager.SetInformationText("Enemy " + enemyStats.name + " appeared!");
        yield return new WaitForSeconds(2f);
        state = BattleState.PLAYERTURN;
        NextTurn();
    }
    
    void PlayerTurn()
    {
        uiManager.SetInformationText("It's your turn");
        uiManager.playerOptions.SetActive(true);
    }

    void EnemyTurn()
    {
        //enemy AI goes here
        StartCoroutine(EnemyAttack());
    }

    IEnumerator PlayerAttack()
    {
        uiManager.SetInformationText("you attack "+enemyStats.name);
        yield return new WaitForSeconds(2f);
        bool isDead = enemyStats.TakeDamage(playerStats.attack);
        uiManager.UpdateHPSP(playerStats.currentHealth, playerStats.currentSpirit, enemyStats.currentHealth, enemyStats.currentSpirit);
        if (isDead)
        {
            uiManager.SetInformationText("Enemy defeated");
            state = BattleState.WON;
        }
        else
        {
            uiManager.SetInformationText("It's the enemy turn");
            state = BattleState.ENEMYTURN;
        }
        yield return new WaitForSeconds(2f);
        NextTurn();
    }

    //use the item selected by the player
    IEnumerator PlayerItem()
    {
        playerStats.RecoverHealth(15);
        uiManager.UpdateHPSP(playerStats.currentHealth, playerStats.currentSpirit, enemyStats.currentHealth, enemyStats.currentSpirit);
        uiManager.SetInformationText("you used an item");
        state = BattleState.ENEMYTURN;
        yield return new WaitForSeconds(2f);
        uiManager.SetInformationText("It's the enemy turn");
        yield return new WaitForSeconds(2f);
        NextTurn();
    }

    //To-Do
    IEnumerator PlayerSpiritualArt()
    {
        yield return new WaitForSeconds(1f);
        state = BattleState.ENEMYTURN;
        NextTurn();
    }

    IEnumerator PlayerRun()
    {
        uiManager.SetInformationText("you fled the battle!");
        yield return new WaitForSeconds(2f);
        uiManager.SetInformationText("pussy...!");
        EndBattle();
    }

    IEnumerator EnemyAttack()
    {
        uiManager.SetInformationText(enemyStats.name + " attacks!");
        yield return new WaitForSeconds(2f);
        bool isDead = playerStats.TakeDamage(enemyStats.attack);
        uiManager.UpdateHPSP(playerStats.currentHealth, playerStats.currentSpirit, enemyStats.currentHealth, enemyStats.currentSpirit);
        if (isDead)
        {
            uiManager.SetInformationText("You fainted");
            state = BattleState.LOST;
        }
        else
        {
            uiManager.SetInformationText("It's your turn");
            state = BattleState.PLAYERTURN;
        }
        yield return new WaitForSeconds(2f);
        NextTurn();
    }

    IEnumerator EnemySecondOption()
    {
        yield return new WaitForSeconds(1f);
    }

    void NextTurn()
    {
        //switch for executing the actual battle state
        switch (state)
        {
            case BattleState.START:
                return;
            case BattleState.PLAYERTURN:
                PlayerTurn();
                return;
            case BattleState.ENEMYTURN:
                EnemyTurn();
                return;
            case BattleState.WON:
                EndBattle();
                return;
            case BattleState.LOST:
                EndBattle();
                return;
            default:
                return;
        }
    }

    //what happens if the battle ended?
    void EndBattle()
    {
        if (state == BattleState.WON)
        {
            uiManager.SetInformationText("Congratulations, you won");
        }else if (state == BattleState.LOST)
        {
            uiManager.SetInformationText("You where defeated");
        }
        uiManager.GameOverWindow.SetActive(true);
    }

    public void OnActionButton(int action)
    {
        if (state != BattleState.PLAYERTURN)
            return;
        uiManager.playerOptions.SetActive(false);
        //switch for performing an action
        switch (action){
            case 1:
                StartCoroutine(PlayerAttack());
                return;
            case 2:
                uiManager.itemsMenu.SetActive(true);
                return;
            case 3:
                //display spiritual arts window
                return;
            case 4:
                StartCoroutine(PlayerRun());
                return;
            case 5:
                uiManager.itemsMenu.SetActive(false);
                StartCoroutine(PlayerItem());
                return;
            case 6:
                StartCoroutine(PlayerSpiritualArt());
                return;
            case 7:
                //reserved
                return;
            default:
                return;
        }
    }
}