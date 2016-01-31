using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

	public ElementType element_type_;
	public AudioClip audioHit_;
	public AudioClip audioGuard_;
	private AudioSource audio_source_;
	private Vector3 speed_;
	private Vector3 internal_speed_;
	private PaperEffect paper_effect_;
	private bool dead_;

	void Awake()
	{
		audio_source_ = GetComponent<AudioSource>();
		paper_effect_ = GetComponent<PaperEffect>();
		paper_effect_.setValue(1f);
		dead_ = false;
	}

	void Start ()
	{
		StartCoroutine(loop());
	}
	
	private void set_speed(Vector3 speed)
	{
		speed_ = speed;
		internal_speed_ = speed;
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
		GetComponent<Collider>().enabled = false;
		float roll_value = 1f;
		paper_effect_.setValue(roll_value);
		float start_time = Time.time;
		internal_speed_ = Vector3.zero;
		transform.rotation = Quaternion.Euler(0f, 90f, 0f);
		for (;;) {
			internal_speed_ = Vector3.up * (float)System.Math.Sin((Time.time - start_time) * Mathf.PI*4) * 4f;
			roll_value += -2f * Time.deltaTime;
			bool escape = false;
			if (roll_value < 0f) {
				escape = true;
				roll_value = 0f;
			}
			paper_effect_.setValue(roll_value);
			if (escape) {
				transform.position = new Vector3(transform.position.x, 0.5f, transform.position.z);
				break;
			}
			yield return null;
			transform.Rotate(new Vector3(0f, 90f, 0f) * Time.deltaTime * 2f);
		}
		GetComponent<Collider>().enabled = true;

		internal_speed_ = speed_;
		for (;;) {
			if (transform.position.z < 0f) {
				GameManager.Instance.incLose();
				Destroy(this.gameObject);
				yield return null;
			}
			yield return null;

			if (dead_) {
				break;
			}
		}

		roll_value = 0f;
		paper_effect_.setValue(roll_value);
		start_time = Time.time;
		for (;;) {
			internal_speed_ = Vector3.up * Time.deltaTime * 8f;
			roll_value += -4f * Time.deltaTime;
			if (roll_value < -1f) {
				break;
			}
			paper_effect_.setValue(roll_value);

			if (Time.time - start_time > 0.5f) {
				break;
			}
			yield return null;
		}
		
		Destroy(gameObject);
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
		transform.position += internal_speed_ * Time.deltaTime;
	}

	void OnTriggerEnter(Collider other)
	{
		var bullet = other.GetComponent<Bullet>();
		if (element_type_ == bullet.getElementType()) {
			GameManager.Instance.incWin();
			audio_source_.clip = audioHit_;
			audio_source_.Play();
			dead_ = true;
		} else {
			audio_source_.clip = audioGuard_;
			audio_source_.Play();
			StartCoroutine(penalty_move());
		}
    }

}
