using UnityEngine;
using System.Collections;

public class ElementUI : MonoBehaviour {

	GameObject normal_;
	GameObject selected_;

	void Awake()
	{
		normal_ = transform.Find("Image").gameObject;
		selected_ = transform.Find("ImageSelected").gameObject;
	}
	
	public void setSelected(bool flg)
	{
		if (flg) {
			normal_.SetActive(false);
			selected_.SetActive(true);
		} else {
			normal_.SetActive(true);
			selected_.SetActive(false);
		}
	}
}
