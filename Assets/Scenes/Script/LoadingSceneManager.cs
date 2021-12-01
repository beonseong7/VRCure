using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingSceneManager : MonoBehaviour { 
    public static string nextScene;
    [SerializeField]Image progressBar;
    AsyncOperation op;
    [SerializeField]GameObject Player;

    public void LoadScene(string sceneName) { 
        nextScene = sceneName;
        StartCoroutine(this.LoadScene());
    }
    IEnumerator LoadScene() 
    { yield return null;
    op = SceneManager.LoadSceneAsync(nextScene,LoadSceneMode.Additive); 
        op.allowSceneActivation = false;
        float timer = 0.0f;
        while (!op.isDone) {
            Debug.Log("load"+op.progress.ToString());
            yield return null;
            timer += Time.deltaTime; 
            if (op.progress < 0.9f) {
                progressBar.fillAmount = Mathf.Lerp(progressBar.fillAmount,op.progress , timer);
                if (progressBar.fillAmount >= op.progress ) {
                    timer = 0f; 
                } 
            } else 
            {
                progressBar.fillAmount = Mathf.Lerp(progressBar.fillAmount, 1f, timer); 
                if (progressBar.fillAmount >= 0.9f) 
                {
                    op.allowSceneActivation = true;
                    yield return new WaitForSeconds(3);
                    Player.GetComponent<Charac_Move>().fix_position();
                    yield return new WaitForSeconds(1);
                    Player.GetComponent<OVRPlayerController>().GravityModifier = 1;
                   
                    Destroy(this.gameObject);
                }
                } 
        }
    } 
}


