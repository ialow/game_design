using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private InputManager userInput;

    [Header("Pause UI")]
    [SerializeField] private List<GameObject> disenableUI;
    [SerializeField] private GameObject enableUI;

    private void Awake()
    {
        userInput.PauseEvent += HandlerPause;
        userInput.ResetEvent += HandlerResume;
    }

    private void HandlerPause()
    {
        var countDisenable = disenableUI.Count;

        Time.timeScale = 0;
        enableUI.SetActive(true);
        for (var i = 0; i < countDisenable; i++)
            disenableUI[i].SetActive(false);
    }

    private void HandlerResume()
    {
        var countDisenable = disenableUI.Count;

        Time.timeScale = 1;
        enableUI.SetActive(false);
        for (var i = 0; i < countDisenable; i++)
            disenableUI[i].SetActive(true);
    }
}
