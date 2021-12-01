using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Charac_Move : MonoBehaviour
{
    [SerializeField] GameObject Game_Text;
    [SerializeField] GameObject AI_People;
    [SerializeField] AudioSource move_sound;
    [SerializeField] Material dark_material;
    [SerializeField] Material normal_material;
    [SerializeField] Skybox skybox;
    void Start()
    {
        Application.targetFrameRate = 60;
        this.fix_position();
         if (OVR_GameManager.instance.Scene_Num == 0)
        {
            GameObject.Find("Load_obj").GetComponent<LoadingSceneManager>().LoadScene("PortalMap");
            skybox.material = normal_material;
        }
        else if (OVR_GameManager.instance.Scene_Num == 1)
        {
            GameObject.Find("Load_obj").GetComponent<LoadingSceneManager>().LoadScene("StalkerMap");
            DBManager.Instance.set_map_id("StalkerMap");
            skybox.material = dark_material;
            StartCoroutine(DBManager.Instance.map_DB_Load());
        }
        else if (OVR_GameManager.instance.Scene_Num == 2)
        {
            GameObject.Find("Load_obj").GetComponent<LoadingSceneManager>().LoadScene("ResultMap");
            skybox.material = normal_material;
        }

    }
    public void fix_position()
    {
    }

    void Update()
    {
        if (OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick).x!=0f && OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick).y!=0f)
        {
            if(move_sound.isPlaying==false)
            move_sound.Play();
        }
        else
        {
            if (move_sound.isPlaying == true)
                move_sound.Pause();
        }
    }//매 프레임마다 입력처리
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Portal")
        {
            OVR_GameManager.instance.Scene_Num = 1;
           SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        else if (other. gameObject.tag == "start_zone")
        {
            GameObject.Find("center/Explain_bot").GetComponent<person_AI>().ex_AI();
            StartCoroutine(GameObject.Find("center/stalker").GetComponent<Stalker_AI>().Stalker_Start());
            Destroy(other.gameObject);
        }
        else if (other.gameObject.tag == "AI_zone")
        {
            AI_People = GameObject.Find("People");
            for (int i = 0; i < AI_People.transform.childCount; i++)
            {
                AI_People.transform.GetChild(i).GetComponent<person_AI>().start_AI();
                
            }
            Destroy(other.gameObject);
        }
        else if (other.gameObject.tag == "Exit_zone")
        {
            OVR_GameManager.instance.Scene_Num = 2;
            if(other.gameObject.name!="not_safe")
                OVR_GameManager.instance.clear.Add("building");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        else if(other.gameObject.name == "Main_Gate")
        {
            OVR_GameManager.instance.Scene_Num = 0;
            OVR_GameManager.instance.score = 0;
            OVR_GameManager.instance.ResetData();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        else if(other.gameObject.name == "stalker")
        {
            if (OVR_GameManager.instance.real_escape == true)
            {
                OVR_GameManager.instance.Scene_Num = 2;
                OVR_GameManager.instance.score = -100;
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
            else
            {
                other.transform.Find("Canvas").gameObject.SetActive(true);
                StartCoroutine(other.gameObject.GetComponent<Stalker_AI>().start_talking());
            }
        }
    }//플레이어와 충돌시 각종 이벤트 발생 함수
    IEnumerator Sceneload( string tmp)
    {
        yield return SceneManager.LoadSceneAsync(tmp,LoadSceneMode.Additive);
    }

}
