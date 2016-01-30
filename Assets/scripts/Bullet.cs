using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

	const float speed = 20f;

	void FixedUpdate()
	{
		transform.position += new Vector3(0, 0, speed) * Time.deltaTime;
	}

	void OnTriggerEnter(Collider other)
	{
        Destroy(other.gameObject);
    }
}
