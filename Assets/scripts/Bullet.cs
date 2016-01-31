using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

	public GameObject guardPrefab_;
	private ElementType element_type_;
	const float speed = 10f;

	public void setElementType(ElementType element_type)
	{
		element_type_ = element_type;
	}
	public ElementType getElementType()
	{
		return element_type_;
	}

	void FixedUpdate()
	{
		transform.position += new Vector3(0, 0, speed) * Time.deltaTime;

		if (transform.position.z > 10f) {
			Destroy(gameObject);
		}
	}

	void OnTriggerEnter(Collider other)
	{
		var enemy = other.GetComponent<Enemy>();
		if (enemy != null) {
			if (enemy.element_type_ != element_type_) {
				var go = Instantiate(guardPrefab_, transform.position, Quaternion.identity) as GameObject;
				var particle_system = go.GetComponent<ParticleSystem>();
				switch (element_type_)
				{
					case ElementType.Red:
						particle_system.startColor = new Color(1f, 0.25f, 0.25f);
						break;
					case ElementType.Blue:
						particle_system.startColor = new Color(0.25f, 0.25f, 1f);
						break;
					case ElementType.Green:
						particle_system.startColor = new Color(0.25f, 1f, 0.25f);
						break;
				}
			}
		}
        Destroy(gameObject);
    }
}
