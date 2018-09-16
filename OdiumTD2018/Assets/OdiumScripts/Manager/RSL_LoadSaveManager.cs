using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Devdog.General.UI;

public class RSL_LoadSaveManager : MonoBehaviour {

    public Animator fadeScreenAnim;
    public float fadeOutScreenTime;
    public int currentSceneID;
    public int sceneToLoadID;
    public int previousSceneID;

    public bool newGame;
    public float nextSceneTimeToWait;
    public Vector3 playerNewGameStartPosition;
    public Vector3 playerScene01Position;
    public Vector3 playerScene02LeavePosition;
    public Vector3 playerScene02ReturnPosition;
    public Vector3 playerScene03Position;

    public Transform playerT;

    public GameObject mainMenuObjectsHolder;

    public GameObject[] scene01Objects;
    public GameObject scene01ObjectsHolder;

    public GameObject[] scene02Objects;
    public GameObject scene02ObjectsHolder;

    public GameObject[] scene03Objects;
    public GameObject scene03ObjectsHolder;

    public UIWindow mapWindow;

    WaitForSeconds nextSceneWait;
    WaitForSeconds fadeOutWait;

    //Scene scene01;
    //Scene scene02;

    GameObject[] sceneObjects;

    //private void Awake() {
    //    DontDestroyOnLoad(this.gameObject);
    //}

    private void Start() {
        nextSceneWait = new WaitForSeconds(nextSceneTimeToWait);
        fadeOutWait = new WaitForSeconds(fadeOutScreenTime);

        scene01Objects = SceneManager.GetSceneByBuildIndex(1).GetRootGameObjects();

        foreach (GameObject go in scene01Objects) {
            if (go.CompareTag("SceneObjects")) {
                scene01ObjectsHolder = go;
            }
        }

        scene02Objects = SceneManager.GetSceneByBuildIndex(2).GetRootGameObjects();

        foreach (GameObject go in scene02Objects) {
            if (go.CompareTag("SceneObjects")) {
                scene02ObjectsHolder = go;
            }
        }

        scene03Objects = SceneManager.GetSceneByBuildIndex(3).GetRootGameObjects();

        foreach (GameObject go in scene03Objects) {
            if (go.CompareTag("SceneObjects")) {
                scene03ObjectsHolder = go;
            }
        }

        sceneObjects = new GameObject[] {mainMenuObjectsHolder, scene01ObjectsHolder, scene02ObjectsHolder,
                                        scene03ObjectsHolder };
        
        for (int i=0; i<sceneObjects.Length; i++) {
            if (i == 0) sceneObjects[i].SetActive(true);
            else sceneObjects[i].SetActive(false);
        }
    }

    public void FadeToNextScene(int nextSceneID) {
        sceneToLoadID = nextSceneID;
        previousSceneID = currentSceneID;
        StartCoroutine(LoadNextScene());
    }

    protected IEnumerator LoadNextScene() {
        fadeScreenAnim.SetTrigger("FadeIn");
        previousSceneID = currentSceneID;
        yield return nextSceneWait;
        //SceneManager.LoadSceneAsync(sceneToLoadID);
        switch(sceneToLoadID) {
            case 1:
                for (int i = 0; i < sceneObjects.Length; i++) {
                    if (i == 1) sceneObjects[i].SetActive(true);
                    else sceneObjects[i].SetActive(false);
                }
                if (newGame) {
                    playerT.position = playerNewGameStartPosition;
                    newGame = false;
                } else {
                    playerT.position = playerScene01Position;
                }
                currentSceneID = 1;
                //RenderSettings.skybox.SetColor("Black", Color.black);
                //SceneManager.LoadSceneAsync(sceneToLoadID, LoadSceneMode.Additive);
                break;
            case 2:
                for (int i = 0; i < sceneObjects.Length; i++) {
                    if (i == 2) sceneObjects[i].SetActive(true);
                    else sceneObjects[i].SetActive(false);
                }
                if(previousSceneID > 1) {
                    playerT.position = playerScene02ReturnPosition;
                    playerT.rotation *= Quaternion.Euler(0, 180f, 0);
                    
                } else {
                    playerT.position = playerScene02LeavePosition;
                }
                currentSceneID = 2;
                //SceneManager.LoadSceneAsync(sceneToLoadID, LoadSceneMode.Additive);
                break;
            case 3:
                for (int i = 0; i < sceneObjects.Length; i++) {
                    if (i == 3) sceneObjects[i].SetActive(true);
                    else sceneObjects[i].SetActive(false);
                }
                playerT.position = playerScene03Position;
                currentSceneID = 3;
                //SceneManager.LoadSceneAsync(sceneToLoadID, LoadSceneMode.Additive);
                break;
        }
        //GameObject go = GameObject.FindGameObjectWithTag("Player") as GameObject;
        //go.transform.position = playerScenePosition;
        mapWindow.Hide();
        StartCoroutine(FadeOutScreen());
    }

    //private void OnLevelWasLoaded(int level) {
    //    GameObject go = GameObject.FindGameObjectWithTag("Player") as GameObject;
    //    go.transform.position = playerScene01StartPosition;
    //    StartCoroutine(FadeOutScreen());
    //}

    protected IEnumerator FadeOutScreen() {
        yield return fadeOutWait;
        fadeScreenAnim.SetTrigger("FadeOut");
    }

}
