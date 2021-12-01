using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class DBManager : MonoBehaviour
{
    private static DBManager instance = null;
    [SerializeField] string login_id;
    [SerializeField] private string id_name;
    [SerializeField]string map_id;
    string LoginURL = "http://192.168.0.2:9090/login.php";
    string MapURL = "http://192.168.0.2:9090/mapscore.php";
    string ScoreURL = "http://192.168.0.2:9090/userscore.php";
    string ScoreUpdateURL = "http://192.168.0.2:9090/updatescore.php";
    public int[] score_data = new int[5];
    void Start()
    {
        login_id = null;
        id_name=null;
    }
    void Awake()
    {
        if(null == instance)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    public static DBManager Instance
    {
        get
        {
            if (null == instance)
            {
                return null;
            }
            return instance;
        }
    }
     public string get_id_name()
    {
        return id_name;
    }
    public void set_map_id(string tmp)
    {
        map_id = tmp;
    }
    
    public IEnumerator server_login(string tmp)
    {
        login_id = tmp;
        WWWForm form = new WWWForm();
        form.AddField("usernamePost",login_id);

        WWW www = new WWW(LoginURL, form);

        yield return www;
        string itemDataString = www.text;
        string[] item = itemDataString.Split(';');
        string[] name = item[0].Split(':');
        id_name = name[1];
        print(id_name);
    }
    public IEnumerator update_userscore()
    {
        if (login_id.Equals("") == true)
        {

        }
        else
        {
            WWWForm form = new WWWForm();
            form.AddField("map_Name", map_id);
            form.AddField("user_Id", login_id);
            form.AddField("user_Score", OVR_GameManager.instance.score);
            WWW www = new WWW(ScoreURL, form);
            yield return www;
            if (www.Equals("success") == false)
            {
                www = new WWW(ScoreUpdateURL, form);

                yield return www;
            }
            string itemDataString = www.text;
            print(itemDataString);
        }
    }
    public IEnumerator map_DB_Load()
    {
        WWWForm form = new WWWForm();
        form.AddField("map_Name", map_id);
        WWW www = new WWW(MapURL, form);
        yield return www;
        string itemDataString = www.text;
        print(itemDataString);
        string[] item = itemDataString.Split(':');
        for(int i = 0; i < 5; i++)
        {
            score_data[i] = Int32.Parse(item[i]);
            print(score_data[i]);
        }
    }
}
