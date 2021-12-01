using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Login_Script : MonoBehaviour
{
    [SerializeField] InputField input_;
    // Start is called before the first frame update
    void Start()
    {
        input_.text = "";
    }

    public void input(string tmp)
    {
        input_.text += tmp;
    }
    // Update is called once per frame
    public void submit()
    {
        StartCoroutine(this.start_login());
       

    }
    IEnumerator start_login()
    {
        string tmp = input_.text.ToString();
        if (tmp.Equals(""))
        {
            StartCoroutine(this.login_anim());
        }
        else
        {
            yield return StartCoroutine(DBManager.Instance.server_login(tmp));
            string result= DBManager.Instance.get_id_name().ToString();
            if (result.Equals("user not found"))
            {

            }
            else
            {
                input_.text = "환영합니다" + result.ToString();
                StartCoroutine(this.login_anim());
            }
        }
        yield return null;
    }
    public void cancel()
    {
        input_.text = "";
    }
    IEnumerator login_anim()
    {
        for (int i = 0; i < 520; i++)
        {
            yield return new WaitForSeconds(0.01f);
            this.transform.Translate(0, -0.01f, 0);
        }
        this.cancel();
    }
}
