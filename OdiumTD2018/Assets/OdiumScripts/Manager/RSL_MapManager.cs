using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Devdog.General.UI;

public class RSL_MapManager : MonoBehaviour {

    public GameObject confirmWindow;
    public RSL_LoadSaveManager loadSaveManager;
    public UIWindow mapWindow;

    [SerializeField]
    int destinationID;

    public void ShowTravelConfirmWindow(int ID) {
        confirmWindow.SetActive(true);
        destinationID = ID;
    }

    public void YesBtnPressed() {
        confirmWindow.SetActive(false);
        loadSaveManager.FadeToNextScene(destinationID);

    }

    public void NoBtnPressed() {
        confirmWindow.SetActive(false);
    }

    IEnumerator HideMapWindow() {
        yield return new WaitForSeconds(3);
        mapWindow.Hide();
    }

}
