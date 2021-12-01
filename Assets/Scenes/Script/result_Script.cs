using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class result_Script : MonoBehaviour
{
    [SerializeField] GameObject result_Text;
    [SerializeField] Transform check_obj;
    // Start is called before the first frame update
    void Start()
    {
        var tmp = OVR_GameManager.instance.clear;
        
        for (int i = 0; i < check_obj.childCount; i++)
        {
            if (tmp.Contains(check_obj.GetChild(i).name.ToString()))
            {
                if (OVR_GameManager.instance.real_escape == true)
                {
                    if (check_obj.GetChild(i).gameObject.name.Equals("help") == true)
                    {
                        OVR_GameManager.instance.score += DBManager.Instance.score_data[3];
                    }
                    else if (check_obj.GetChild(i).gameObject.name.Equals("emegencybell") == true)
                    {
                        OVR_GameManager.instance.score += 5;
                    }
                    else if (check_obj.GetChild(i).gameObject.name.Equals("tool") == true)
                    {
                        OVR_GameManager.instance.score += DBManager.Instance.score_data[0];
                    }
                    else if (check_obj.GetChild(i).gameObject.name.Equals("cctv") == true)
                    {
                        OVR_GameManager.instance.score += DBManager.Instance.score_data[1];
                    }
                    else if (check_obj.GetChild(i).gameObject.name.Equals("police") == true)
                    {
                        OVR_GameManager.instance.score += DBManager.Instance.score_data[2];
                    }
                }
                check_obj.GetChild(i).GetComponent<Text>().text += "O";
            }
            else
            {
                check_obj.GetChild(i).GetComponent<Text>().text += "X";
            }
        }
        result_Text.GetComponent<Text>().text += OVR_GameManager.instance.score.ToString();
        if (OVR_GameManager.instance.real_escape == false)
        {
            result_Text.GetComponent<Text>().text += "\n*위험인물인지 확인하지 않았습니다.";
        }
        StartCoroutine(DBManager.Instance.update_userscore());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
