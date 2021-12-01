using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;

public class Tool_Hand : MonoBehaviour
{
    [SerializeField] GameObject Equip;
    [SerializeField] GameObject ToolBox;
    [SerializeField] bool selecting;
    [SerializeField] GameObject Game_Text;
    [SerializeField] GameObject grabbing;
    [SerializeField] bool use=false;
    [SerializeField] GameObject Spray;
    [SerializeField] GameObject Phone_display;
    [SerializeField] RaycastHit hit;
    [SerializeField] float maxDistance=100f;
    [SerializeField] AudioSource audio_;

    // Start is called before the first frame update
    void Start()
    {
        grabbing = ToolBox.transform.Find("Spray").gameObject;
        grabbing.SetActive(false);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (OVRInput.Get(OVRInput.RawButton.RHandTrigger) == false)//VR 컨트롤러 입력처리
        {
            if (grabbing.activeSelf == true)
            {
                grabbing.SetActive(false);
                Equip.SetActive(true);
                Equip = null;
            }
        }
        if (OVRInput.Get(OVRInput.RawButton.RIndexTrigger) == true && use == false && grabbing.activeSelf==true)
        {
            use = true;
            if(grabbing.name== "Spray" )
            {
                this.tool_spray();
            }
            else if (grabbing.name == "Phone")
            {
                this.tool_Phone();
            }
        }
         if (OVRInput.Get(OVRInput.RawButton.RIndexTrigger) == true && grabbing.activeSelf == false)
        {
            this.talking();
        }
        if(OVRInput.Get(OVRInput.RawButton.RIndexTrigger) == false)
        {
            use = false;
        }
        
    }
    void talking()
    {
        if (Physics.Raycast(this.transform.position, this.transform.forward, out hit, maxDistance))
        {
            Game_Text.GetComponent<Text>().text = hit.collider.name.ToString();
            if (hit.collider.tag == "Drunken_AI")
            {
                audio_.Play();
                Destroy(hit.collider.gameObject);
            }
             if (hit.collider.tag == "Stand_AI" || hit.collider.tag == "Move_AI")
            {
                audio_.Play();
                if (OVR_GameManager.instance.clear.Contains("help") == false)
                {
                    OVR_GameManager.instance.clear.Add("help");
                }
                Destroy(hit.collider.gameObject);
            }
            if (hit.collider.tag == "Emegency")
            {
                audio_.Play();
                if (OVR_GameManager.instance.clear.Contains("emegencybell") == false)
                {
                    OVR_GameManager.instance.clear.Add("emegencybell");
                }
                hit.collider.transform.GetComponent<Animator>().SetBool("press", true);
            }
        }
    }
    void tool_spray()
    {
        Instantiate(Spray, this.transform.position, this.transform.rotation);
        if(Physics.Raycast(this.transform.position,this.transform.forward,out hit, maxDistance)){
            Game_Text.GetComponent<Text>().text = OVR_GameManager.instance.score.ToString();
            if (hit.collider.name == "stalker")
            {
                hit.collider.GetComponent<Stalker_AI>().set_anim(3);
                if (OVR_GameManager.instance.clear.Contains("tool") == false)
                {
                    OVR_GameManager.instance.clear.Add("tool");
                }
            }
        }
    }
    void tool_Phone()
    {
        if (Phone_display.activeSelf == true)
        {
            Phone_display.SetActive(false);
            OVR_GameManager.instance.clear.Remove("police");
        }
        else
        {
            Phone_display.SetActive(true);
           OVR_GameManager.instance.clear.Add("police");
        }
    }
    public void Tool_select(GameObject tmp)
    {
        Equip = tmp;
        Equip.SetActive(false);
        grabbing = ToolBox.transform.Find(Equip.gameObject.name.ToString()).gameObject;
        grabbing.SetActive(true);
        Game_Text.GetComponent<Text>().text = "sdfd";
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Tool")
        {
            selecting = false;
        }
    }
}
