using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    #region player UI
    public TextMeshProUGUI playerNamne;
    public TextMeshProUGUI playerLvl;
    public Slider playerHpSlider;
    public Slider playerSpSlider;
    #endregion

    #region enemy UI
    public TextMeshProUGUI enemyNamne;
    public TextMeshProUGUI enemyLvl;
    public Slider enemyHpSlider;
    public Slider enemySpSlider;
    #endregion

    #region General UI Elements
    public TextMeshProUGUI informationText;
    public GameObject playerOptions;
    public GameObject itemsMenu;
    public GameObject spiritualArtMenu;
    public GameObject GameOverWindow;
    #endregion

    public void SetUpPlayerHUD(Stats stats)
    {
        playerNamne.text = stats.name;
        playerLvl.text = "Lvl: "+stats.lvl;
        playerHpSlider.maxValue = stats.maxHealth;
        playerHpSlider.value = stats.currentHealth;
        playerSpSlider.maxValue = stats.maxSpirit;
        playerSpSlider.value = stats.currentSpirit;
    }

    public void SetUpEnemyHUD(Stats stats)
    {
        enemyNamne.text = stats.name;
        enemyLvl.text = "Lvl: " + stats.lvl;
        enemyHpSlider.maxValue = stats.maxHealth;
        enemyHpSlider.value = stats.currentHealth;
        enemySpSlider.maxValue = stats.maxSpirit;
        enemySpSlider.value = stats.currentSpirit;
    }

    public void UpdateHPSP(int playerHp, int playerSp, int enemyHp, int enemySp)
    {
        playerHpSlider.value = playerHp;
        playerSpSlider.value = playerSp;
        enemyHpSlider.value = enemyHp;
        enemySpSlider.value = enemySp;
    }

    public void SetInformationText(string textToAssign)
    {
        informationText.text = textToAssign;
    }
}
