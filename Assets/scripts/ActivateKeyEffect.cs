using UnityEngine;
using System.Collections;

public class ActivateKeyEffect : MonoBehaviour {

	private bool active_;
	private UnityEngine.UI.Image image_;
	private UnityEngine.UI.Image image_selected_;
	private UnityEngine.UI.Text text_;

	void Awake()
	{
		image_ = transform.Find("Image").GetComponent<UnityEngine.UI.Image>();
		image_selected_ = transform.Find("ImageSelected").GetComponent<UnityEngine.UI.Image>();
		text_ = transform.Find("Text").GetComponent<UnityEngine.UI.Text>();
		setActive(true);
		setActive(false);
	}

	public void setActive(bool flg)
	{
		if (active_ == flg) {
			return;
		}
		active_ = flg;
		if (active_) {
			image_.enabled = false;
			image_selected_.enabled = true;
			text_.color = Color.black;
		} else {
			image_.enabled = true;
			image_selected_.enabled = false;
			text_.color = Color.white;
		}
	}

}
