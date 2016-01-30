using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	private int current_lane_;
	
	void Update()
	{
		if (Input.GetKeyDown(KeyCode.X)) {
		}
		Input.GetAxis("Horizontal");
	}
}
