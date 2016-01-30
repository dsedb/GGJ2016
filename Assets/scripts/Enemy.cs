using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

	public ElementType element_type_;
	private Vector3 speed_;

	void Start ()
	{
		StartCoroutine(loop());
	}
	
	private void set_speed(Vector3 speed)
	{
		speed_ = speed;
	}
	private void set_speed(float speed)
	{
		set_speed(new Vector3(0, 0, -speed));
	}
	private void set_element_type(ElementType element_type)
	{
		element_type_ = element_type;
	}

	public void setup(EnemySpawnDataUnit unit)
	{
		set_speed(unit.speed_);
		set_element_type(unit.element_type_);
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

	IEnumerator penalty_move()
	{
		float start_time = Time.time;
		float penalty_speed = -8f;
		while (Time.time - start_time < 0.1f) {
			transform.position += new Vector3(0, 0, penalty_speed * Time.deltaTime);
			yield return null;
		}
	}

	void FixedUpdate ()
	{
		transform.position += speed_ * Time.deltaTime;
	}

	void OnTriggerEnter(Collider other)
	{
		var bullet = other.GetComponent<Bullet>();
		if (element_type_ == bullet.getElementType()) {
			Destroy(gameObject);
		} else {
			Debug.Log("wrong element!");
			StartCoroutine(penalty_move());
		}
    }

}
