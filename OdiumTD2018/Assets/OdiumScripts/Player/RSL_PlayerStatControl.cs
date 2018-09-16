using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Devdog.InventoryPro.UI;
using Devdog.InventoryPro;
using Devdog.General;
using Invector.vCharacterController;

public class RSL_PlayerStatControl : MonoBehaviour {

    public vThirdPersonController playerController;
    public Animator playerAnim;
    public float startHealth;
    public float startThirst;
    public float startHunger;

    IStat healthStat;
    IStat thirstStat;
    IStat hungerStat;

    public float thirstTime;
    public float hungerTime;
    public float healthTime;
    public float healthDecrement;

    WaitForSeconds thirstWaitTime;
    WaitForSeconds hungerWaitTime;
    WaitForSeconds healthWaitTime;

    public bool thirsty = false;
    public bool hungry = false;

    private void Start() {
        playerController = GetComponent<vThirdPersonController>();
        playerAnim = GetComponent<Animator>();

        thirstWaitTime = new WaitForSeconds(thirstTime);
        hungerWaitTime = new WaitForSeconds(hungerTime);
        healthWaitTime = new WaitForSeconds(healthTime);

        var myPlayer = PlayerManager.instance.currentPlayer.inventoryPlayer;

        healthStat = myPlayer.stats.Get("Default", "Health");
        thirstStat = myPlayer.stats.Get("Default", "Thirst");
        hungerStat = myPlayer.stats.Get("Default", "Hunger");

        healthStat.SetCurrentValueRaw(startHealth);
        thirstStat.SetCurrentValueRaw(startThirst);
        hungerStat.SetCurrentValueRaw(startHunger);

        StartCoroutine(DegradeThirstOverTime());
        StartCoroutine(DegradeHungerOverTime());
        StartCoroutine(DegradeHealthOverTimer());

        thirstStat.OnValueChanged += ThirstStat_OnValueChanged;
        hungerStat.OnValueChanged += HungerStat_OnValueChanged;
    }

    private void HungerStat_OnValueChanged(IStat obj) {
        if (hungerStat.currentValue > 0 && hungry) {
            hungry = false;
            StartCoroutine(DegradeHungerOverTime());
        }
    }

    private void ThirstStat_OnValueChanged(IStat obj) {
        if (thirstStat.currentValue > 0 && thirsty) {
            thirsty = false;
            StartCoroutine(DegradeThirstOverTime());
        }
    }

    public void AdjustHealth() {
        healthStat.SetCurrentValueRaw(playerController.currentHealth);
    }

    protected IEnumerator DegradeThirstOverTime() {
        while (thirstStat.currentValue > 0) {
            yield return thirstWaitTime;
            thirstStat.ChangeCurrentValueRaw(-1);
        }

        if (thirstStat.currentValue <= 0) {
            thirsty = true;
            thirstStat.SetCurrentValueRaw(0);
        }
        //else thirsty = false;
    }

    protected IEnumerator DegradeHungerOverTime() {
        while(hungerStat.currentValue > 0) {
            yield return hungerWaitTime;
            hungerStat.ChangeCurrentValueRaw(-1);
        }

        if (hungerStat.currentValue <= 0) {
            hungry = true;
            hungerStat.SetCurrentValueRaw(0);
        }
        //else hungry = false;
    }

    protected IEnumerator DegradeHealthOverTimer() {
        while(healthStat.currentValue > 0) {
            yield return healthWaitTime;
            if (hungry && thirsty) {
                healthStat.ChangeCurrentValueRaw(-healthDecrement * 2);
                playerController.ChangeHealth((int)-healthDecrement * 2);
            }
            else if (hungry || thirsty) {
                healthStat.ChangeCurrentValueRaw(-healthDecrement);
                playerController.ChangeHealth((int)-healthDecrement);
            }
        }

        if (healthStat.currentValue < 0) {
            healthStat.SetCurrentValueRaw(0);
            //int curHealth = (int)playerController.currentHealth;
            //playerController.ChangeHealth(-curHealth);
        }
    }

}
