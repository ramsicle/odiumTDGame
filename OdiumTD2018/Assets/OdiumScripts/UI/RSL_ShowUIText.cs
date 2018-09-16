using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Devdog.General.UI;
using TMPro;

public class RSL_ShowUIText : MonoBehaviour {

    public GameObject playerOG;
    public GameObject TMPTxt;
    public TextMeshPro tmpTxt;
    public UIWindow bankUI;

    public float distance;

    private void Start() {
        playerOG = GameObject.FindGameObjectWithTag("Player") as GameObject;

        UIWindow[] uiWindows = FindObjectsOfType<UIWindow>();
        foreach(var w in uiWindows) {
            if (w.name.Equals("Bank")) {
                bankUI = w;
            }
            if (w.name.Equals("BankWindow")) {
                bankUI = w;
            }
        }
    }

    private void OnTriggerStay(Collider other) {
        if (other.gameObject.CompareTag("Player")) {
            distance = Vector3.Distance(transform.position, playerOG.transform.position);
            if (distance < 5) {
                TMPTxt.SetActive(true);
            }
            else if (distance > 5) {
                TMPTxt.SetActive(false);
            }

            if (distance < 2) {
                tmpTxt.text = "Press E";
                if (Input.GetKeyDown(KeyCode.E)) {
                    bankUI.Show();
                }
            }
            if (distance > 2) {
                tmpTxt.text = "Storage";
            }
            
        }
    }

    private void OnTriggerExit(Collider other) {
        bankUI.Hide();
        TMPTxt.SetActive(false);
    }

}
