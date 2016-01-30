using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

	public ElementType element_type_;
	public AudioClip audioHit_;
	public AudioClip audioGuard_;
	private AudioSource audio_source_;
	private Vector3 speed_;

	void Awake()
	{
		audio_source_ = GetComponent<AudioSource>();
	}

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
			if (transform.position.z < 0f) {
				GameManager.Instance.incLose();
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
			GameManager.Instance.incWin();
			audio_source_.clip = audioHit_;
			audio_source_.Play();
			Destroy(gameObject, 0.5f);
		} else {
			audio_source_.clip = audioGuard_;
			audio_source_.Play();
			StartCoroutine(penalty_move());
		}
    }

}
