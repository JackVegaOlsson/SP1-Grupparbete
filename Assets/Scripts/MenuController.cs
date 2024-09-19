using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    [SerializeField] private SavingApples _savingApples;
    [SerializeField] private Crystals _crystals;

    [SerializeField] private GameObject creditsPanel;

    private void Start()
    {
        _savingApples.collectedApples = 0;
        _crystals.hasRedCrystal = false;
        _crystals.hasPurpleCrystal = false;
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ShowCredits()
    {
        creditsPanel.SetActive(true);
    }

    public void CloseCredits()
    {
        creditsPanel.SetActive(false);
    }
}
