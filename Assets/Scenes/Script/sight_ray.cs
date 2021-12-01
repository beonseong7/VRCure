using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class sight_ray : MonoBehaviour
{
    [SerializeField] GameObject Game_Text;
    [SerializeField] RaycastHit hit;
    [SerializeField] float maxDistance = 150f;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        if (Physics.Raycast(this.transform.position, this.transform.forward, out hit, maxDistance))
        {
            Game_Text.GetComponent<Text>().text = hit.collider.name.ToString();
            if (hit.collider.tag == "CCTV")
            {
                if (OVR_GameManager.instance.real_escape == true)
                    OVR_GameManager.instance.score += DBManager.Instance.score_data[1];
                OVR_GameManager.instance.clear.Add("cctv");
                this.GetComponent<sight_ray>().enabled = false;
            }
        }
    }
}
