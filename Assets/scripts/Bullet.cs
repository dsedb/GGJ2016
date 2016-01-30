using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

	const float speed = 20f;

	void FixedUpdate()
	{
		transform.position += new Vector3(0, 0, speed) * Time.deltaTime;

		if (transform.position.z > 30f) {
			Destroy(gameObject);
		}
	}

	void OnTriggerEnter(Collider other)
	{
        Destroy(other.gameObject);
    }
}
