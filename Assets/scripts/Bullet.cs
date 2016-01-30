using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

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
        Destroy(gameObject);
    }
}
