using Devdog.General.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RSL_GameManager : MonoBehaviour {

    public RSL_LoadSaveManager loadSaveManager;

    public UIWindow mainMenuWindow;
    public float startNewGameTime;

    WaitForSeconds startNewGameWait;

    private void Start() {
        startNewGameWait = new WaitForSeconds(startNewGameTime);
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.X)) {
            mainMenuWindow.Hide();
        }
    }

    public void NewGameBtnPressed() {
        StartCoroutine(StartNewGame());
        loadSaveManager.newGame = true;
    }

    protected IEnumerator StartNewGame() {
        loadSaveManager.FadeToNextScene(1);
        yield return startNewGameWait;
        mainMenuWindow.Hide();
    }

}
