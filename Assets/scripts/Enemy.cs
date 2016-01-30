using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

	private Vector3 speed_;

	void Start ()
	{
		StartCoroutine(loop());
	}
	
	public void setSpeed(Vector3 speed)
	{
		speed_ = speed;
	}
	public void setSpeed(float speed)
	{
		setSpeed(new Vector3(0, 0, -speed));
	}

	IEnumerator loop()
	{
		for (;;) {
			if (transform.position.z < -1f) {
				Destroy(this.gameObject);
				yield return null;
			}
			yield return null;
		}
	}

	void FixedUpdate ()
	{
		transform.position += speed_ * Time.deltaTime;
	}
}
