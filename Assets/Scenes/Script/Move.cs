using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Move : MonoBehaviour
{
	public string currentMapName;

	double x;
	double y;
	private string Test = "sdfvb";
	[SerializeField] int speed;
	Rigidbody Charigid;
	GameObject Clicked_Obj;
	// Use this for initialization
	void Start()
	{
		DontDestroyOnLoad(this.gameObject);
		Charigid = GetComponent<Rigidbody>();
		Cursor.lockState = CursorLockMode.Locked;
	}
	public string Get_Test()
	{
		return Test;
	}
	// Update is called once per frame
	void Update()
	{
		Camera.main.transform.position = this.transform.position;
		move();
		Obj_move();
		if (Input.GetKeyDown("q"))
		{
			Cursor.lockState = CursorLockMode.Locked;
		}
		if (Input.GetKeyDown("e"))
		{
			Cursor.lockState = CursorLockMode.None;
		}
	}
	void move()
	{
		Vector3 dir;
		transform.localRotation = Camera.main.transform.localRotation;
		transform.localRotation = new Quaternion(0, transform.localRotation.y, 0, transform.localRotation.w);
		dir = Vector3.zero;
		if (Input.GetKey("w"))
		{
			dir = Vector3.forward;
		}
		if (Input.GetKey("s"))
		{
			dir = Vector3.back;
		}
		if (Input.GetKey("a"))
		{
			dir = Vector3.left;
		}
		if (Input.GetKey("d"))
		{
			dir = Vector3.right;
		}
		gameObject.transform.Translate(dir * speed * Time.deltaTime);
	}
	void Obj_move()
	{
		if (Input.GetMouseButtonDown(0))
		{
			Clicked_Obj = Get_Clicked_Obj();
			if (Clicked_Obj.tag == "Box" && Clicked_Obj != null)
			{
				Clicked_Obj.GetComponent<Rigidbody>().useGravity = false;
				Clicked_Obj.GetComponent<Rigidbody>().isKinematic = true;
				Clicked_Obj.transform.SetParent(Camera.main.transform);
			}
		}
		if (Input.GetMouseButtonDown(1))
		{
			if (Clicked_Obj != null)
			{
				Clicked_Obj.GetComponent<Rigidbody>().useGravity = true;
				Clicked_Obj.GetComponent<Rigidbody>().isKinematic = false;
				Clicked_Obj.transform.SetParent(null);
				Clicked_Obj = null;
			}
		}
	}
	GameObject Get_Clicked_Obj()
	{
		RaycastHit hit;
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		Physics.Raycast(ray, out hit);
		if (hit.collider != null)
		{
			return hit.collider.gameObject;
		}
		else
		{
			return null;
		}
	}
	void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.name == "Exit")
		{
			PlayerPrefs.SetInt("result", 0);
			SceneManager.LoadScene("Result_Scene");
		}
		else if (collision.gameObject.tag == "Enemy")
		{
			PlayerPrefs.SetInt("result", 1);
			SceneManager.LoadScene("Result_Scene");
		}
	}
}
