using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Stalker_AI : MonoBehaviour
{
    Animator st_anim;
    [SerializeField] NavMeshAgent st_navmesh;
    [SerializeField] NavMeshSurface map_navmesh;
    [SerializeField] GameObject target;
    // Start is called before the first frame update
    void Start()
    {
        st_anim = this.GetComponent<Animator>();
        st_navmesh = this.GetComponent<NavMeshAgent>();
        Debug.Log("sdddddg");
        map_navmesh = GameObject.FindWithTag("navmesh").GetComponent<NavMeshSurface>();
        Debug.Log("sdddddg");
        target = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    public void set_anim(int tmp)
    {
        if (st_navmesh.isStopped == false)
        {
            st_anim.SetInteger("Stalker_int", tmp);
            if (tmp == 3)
            {
                StartCoroutine(this.stopped());
            }
            else if (tmp == 1)
            {
                st_anim.SetInteger("Stalker_int", 2);
            }
        }
    }
    IEnumerator stopped()
    {
        st_navmesh.isStopped = true;
        yield return new WaitForSeconds(3f);
        st_navmesh.isStopped = false;
        st_anim.SetInteger("Stalker_int", 2);
    }
    public IEnumerator Stalker_Start()
{
        yield return new WaitForSeconds(10);
        map_navmesh.BuildNavMesh();
        set_anim(2);
        StartCoroutine(this.start_Target());
    }
    public IEnumerator start_talking()
    {
        st_anim.SetInteger("Stalker_int", 1);
        this.GetComponent<CapsuleCollider>().enabled = false;
        yield return null;
        Time.timeScale = 0;
        
    }
    public void run_corutine(string tmp)
    {
        StartCoroutine(tmp);
    }
    public IEnumerator real_stalking()
    {
        Time.timeScale = 1;
        this.GetComponent<BoxCollider>().enabled = false;
        yield return StartCoroutine(this.stopped());
        this.GetComponent<BoxCollider>().enabled = true;
        OVR_GameManager.instance.real_escape = true;
        this.GetComponent<CapsuleCollider>().enabled = true;
    }
    IEnumerator start_Target()
    {
        st_navmesh.destination = target.transform.position;
        yield return new WaitForSeconds(0.25f);
        StartCoroutine(this.start_Target());
    }
}
