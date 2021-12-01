using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransferMap : MonoBehaviour
{
    public string TransferMapName;
    private Move thePlayer;
    // Start is called before the first frame update
    void Start()
    {
        thePlayer = FindObjectOfType<Move>();

    }
    /*void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            thePlayer.currentMapName = TransferMapName;
            SceneManager.LoadScene(TransferMapName);
        }
    }*/
}