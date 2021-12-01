using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class OVR_GameManager : MonoBehaviour
{
    public static OVR_GameManager instance = null;
    [SerializeField] public int Scene_Num = 0;
    [SerializeField] public int score = 0;
    [SerializeField] public List<string> clear =new List<string>();
    [SerializeField] public bool real_escape = false;
    // Start is called before the first frame update
    private void Awake()
    {
        if (OVR_GameManager.instance != null)
        {
            Destroy(this.gameObject);
        }
        if (instance == null)
        {
            instance = this;
        }
    }
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }
    public void ResetData()
    {
        score = 0;
        real_escape = false;
        clear = new List<string>();
    }

}
