using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public GameObject MenuContainer;
    public GameObject StatisticContainer;
    
    public Text loseText;
    public Text wonText;

    public static int
        lose,
        won;

    private void Start()
    {
        
    }

    public void ResetStatistic()
    {
        lose = won = 0;
        UpdateStatistic();
    }

    public void UpdateStatistic()
    {
        loseText.text = "Losed: " + lose;
        wonText.text = "Won: " + won;
    }

    public void Back()
    {
        StatisticContainer.SetActive(false);
        MenuContainer.SetActive(true);
    }

    public void Play()
    {
        SceneManager.LoadScene(1);
    }

    public void Statistic() {
        StatisticContainer.SetActive(true);
        MenuContainer.SetActive(false);
    }

}
