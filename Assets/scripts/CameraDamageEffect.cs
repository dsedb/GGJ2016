using UnityEngine;
using System.Collections;

public class CameraDamageEffect : MonoBehaviour {

	private Vector3 original_position_;
	private Vector3 target_position_;
	private float end_time_;
	private float power_;

	private IEnumerator loop()
	{
		for (;;) {
			if (end_time_ > Time.time) {
				target_position_ = original_position_ + new Vector3(Random.Range(-power_, power_),
																	Random.Range(-power_, power_),
																	0f);
			} else {
				target_position_ = original_position_;
			}
			yield return null;
		}
	}

	void Start()
	{
		original_position_ = transform.position;
		target_position_ = original_position_;
		StartCoroutine(loop());
	}
	
	void Update()
	{
		transform.position = Vector3.Lerp(transform.position, target_position_, 0.2f);
	}

	public void startDamage(float duration, float power)
	{
		end_time_ = Time.time + duration;
		power_ = power;
	}
}
