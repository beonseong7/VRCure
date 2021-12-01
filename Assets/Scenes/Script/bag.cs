using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class bag : MonoBehaviour
{
   [SerializeField] GameObject Equip;

    [SerializeField] bool selecting;
    public RaycastHit hit;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (OVRInput.Get(OVRInput.RawButton.LHandTrigger) == false)//VR 컨트롤러 입력처리
        {
            if (Equip.activeSelf == true)
            {
                Equip.SetActive(false);
                selecting = false;
            }
        }
        else if (OVRInput.Get(OVRInput.RawButton.LHandTrigger) == true && selecting == true)//VR 컨트롤러 입력처리
        {
            Equip.transform.position = this.transform.position;
            Equip.transform.rotation = this.transform.rotation;
            Equip.SetActive(true);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Movable")
        {
            selecting=true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Movable")
        {
            selecting = false;
        }
    }
}
