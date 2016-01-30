using UnityEngine;
using System.Collections;

public class LowPanelController : MonoBehaviour {

	UnityEngine.UI.Image image_;
	float alpha_;

	void Awake()
	{
		image_ = GetComponent<UnityEngine.UI.Image>();
		alpha_ = image_.color.a;
	}

	public void setBulletType(Player.BulletType bullet_type)
	{
		switch (bullet_type) {
			case Player.BulletType.Red:
				image_.color = new Color(1, 0, 0, alpha_);
				break;
			case Player.BulletType.Blue:
				image_.color = new Color(0, 0, 1, alpha_);
				break;
			case Player.BulletType.Green:
				image_.color = new Color(0, 1, 0, alpha_);
				break;
		}
	}

}
