using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Devdog.General.UI;

public class RSL_MapControl : MonoBehaviour {

    public UIWindow mapWindow;

    public float showMapTimer;
    public RSL_LoadSaveManager loadSaveManager;

    WaitForSeconds showMapWait;

    private void Start() {
        showMapWait = new WaitForSeconds(showMapTimer);
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("MapTrigger")) StartCoroutine(ShowMapTimer());
        if (other.gameObject.CompareTag("BunkerInside")) StartCoroutine(GoOutside());
        if (other.gameObject.CompareTag("BunkerOutside")) StartCoroutine(GoInside());
    }

    private void OnTriggerExit(Collider other) {
        if (other.gameObject.CompareTag("MapTrigger")) mapWindow.Hide();
        if (other.gameObject.CompareTag("BunkerInside")) mapWindow.Hide();
        if (other.gameObject.CompareTag("BunkerOutside")) mapWindow.Hide();
    }

    protected IEnumerator ShowMapTimer() {
        yield return showMapWait;
        mapWindow.Show();
    }

    IEnumerator GoOutside() {
        yield return showMapWait;
        loadSaveManager.FadeToNextScene(2);
        mapWindow.Hide();
    }

    IEnumerator GoInside() {
        yield return showMapWait;
        loadSaveManager.FadeToNextScene(1);
        mapWindow.Hide();
    }

}
