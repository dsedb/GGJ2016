using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	public GameObject bullet_;
	private int current_lane_;
	private Vector3 fire_point_x_;
	private Vector3 fire_point_c_;
	private Vector3 fire_point_v_;
	
	void Awake()
	{
		fire_point_x_ = new Vector3(-2f, 0.5f, 0f);
		fire_point_c_ = new Vector3( 0f, 0.5f, 0f);
		fire_point_v_ = new Vector3( 2f, 0.5f, 0f);
	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.X)) {
			Instantiate(bullet_, fire_point_x_, Quaternion.identity);
		}
		if (Input.GetKeyDown(KeyCode.C)) {
			Instantiate(bullet_, fire_point_c_, Quaternion.identity);
		}
		if (Input.GetKeyDown(KeyCode.V)) {
			Instantiate(bullet_, fire_point_v_, Quaternion.identity);
		}
		Input.GetAxis("Horizontal");
	}
}
