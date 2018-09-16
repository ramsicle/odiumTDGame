using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VIDE_Data;

public class UIManager : MonoBehaviour {

    public GameObject containerNPC;
    public GameObject containerPlayer;
    public Text textNPC;
    public Text[] textChoices;

    private void Start() {
        containerNPC.SetActive(false);
        containerPlayer.SetActive(false);
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.E)) {
            if (!VD.isActive) {
                Begin();
            } else {
                VD.Next();
            }
        }
    }

    private void Begin() {
        VD.OnNodeChange += UpdateUI;
        VD.OnEnd += End;
        VD.BeginDialogue(GetComponent<VIDE_Assign>());
    }
    
    private void UpdateUI(VD.NodeData data) {
        containerNPC.SetActive(false);
        containerPlayer.SetActive(false);

        if (data.isPlayer) {
            Debug.Log("data is player");
            containerPlayer.SetActive(true);

            for(int i=0; i<textChoices.Length; i++) {
                if (i < data.comments.Length) {
                    textChoices[i].transform.parent.gameObject.SetActive(true);
                    textChoices[i].text = data.comments[i];
                } else {
                    textChoices[i].transform.parent.gameObject.SetActive(false);
                }
            }
            textChoices[0].transform.parent.GetComponent<Button>().Select();
        } else {
            Debug.Log("Data is NPC");
            containerNPC.SetActive(true);
            textNPC.text = data.comments[data.commentIndex];
        }
    }

    private void End(VD.NodeData data) {
        containerNPC.SetActive(false);
        containerPlayer.SetActive(false);
        VD.OnNodeChange -= UpdateUI;
        VD.OnEnd -= End;
        VD.EndDialogue();
    }

    private void OnDisable() {
        if (containerNPC != null) {
            End(null);
        }
    }

    public void SetPlayerChoice(int choice) {
        VD.nodeData.commentIndex = choice;
        VD.Next();
        //if (Input.GetMouseButtonDown(0)) {
        //    VD.Next();
        //}
    }
}
