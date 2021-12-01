using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Camerachoose : MonoBehaviour
{
    [SerializeField] GameObject bag;
    // Start is called before the first frame update
    void Start()
    {
        this.GetComponent<Canvas>().worldCamera = GameObject.Find("OVRPlayerController/OVRCameraRig/TrackingSpace/CenterEyeAnchor").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (this.gameObject.name == "tool_canvas")
        {
            if (bag.gameObject.activeSelf == true)
            {
                transform.Find("stalker_tools").gameObject.SetActive(true);
                if (this.gameObject.name == "tool_canvas")
                {
                    this.transform.position = bag.transform.position;
                    this.transform.rotation = bag.transform.rotation;
                }
            }
            else
            {
                transform.Find("stalker_tools").gameObject.SetActive(false);
            }
        }

    }
}
