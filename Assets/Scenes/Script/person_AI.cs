using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class person_AI : MonoBehaviour
{
    public GameObject target;
    [SerializeField] Text ex_Text;
    [SerializeField] Text ex_Text2;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void start_AI()
    {
        if (this.gameObject.tag == "Move_AI")
        {
            this.GetComponent<NavMeshAgent>().destination = target.transform.position;
        }
        else if(this.gameObject.tag == "Stand_AI")
        {

        }

    }
    public void ex_AI()
    {
        if (this.gameObject.tag == "Explain_AI")
        {
            transform.Find("3").gameObject.SetActive(true);
            StartCoroutine(this.explain());
        }
    }
    IEnumerator Goto_Target()
    {
        yield return new WaitForSeconds(0.5f);
        this.GetComponent<NavMeshAgent>().destination = target.transform.position;
        StartCoroutine(this.Goto_Target());
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == target.gameObject)
        {
            Destroy(this.gameObject);
        }
    }
    IEnumerator explain()
    {
        ex_Text.gameObject.SetActive(true);
        ex_Text2.gameObject.SetActive(false);
        yield return new WaitForSeconds(5);
        ex_Text.gameObject.SetActive(false);
        ex_Text2.gameObject.SetActive(true);
        yield return new WaitForSeconds(5);
        Destroy(transform.Find("3").gameObject);
    }
}
