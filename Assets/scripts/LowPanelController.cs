using UnityEngine;
using System.Collections;

public class LowPanelController : MonoBehaviour {

	// UnityEngine.UI.Image image_;
	UnityEngine.UI.Image bg_image_red_;
	UnityEngine.UI.Image bg_image_blue_;
	UnityEngine.UI.Image bg_image_green_;
	// float alpha_;

	void Awake()
	{
		bg_image_red_ = transform.Find("bg_red").GetComponent<UnityEngine.UI.Image>();
		bg_image_red_.enabled = true;
		bg_image_blue_ = transform.Find("bg_blue").GetComponent<UnityEngine.UI.Image>();
		bg_image_blue_.enabled = false;
		bg_image_green_ = transform.Find("bg_green").GetComponent<UnityEngine.UI.Image>();
		bg_image_green_.enabled = false;
		// image_ = GetComponent<UnityEngine.UI.Image>();
		// alpha_ = image_.color.a;
	}

	public void setElementType(ElementType element_type)
	{
		switch (element_type) {
			case ElementType.Red:
				bg_image_red_.enabled = true;
				bg_image_blue_.enabled = false;
				bg_image_green_.enabled = false;
				// image_.color = new Color(1, 0, 0, alpha_);
				break;
			case ElementType.Blue:
				bg_image_red_.enabled = false;
				bg_image_blue_.enabled = true;
				bg_image_green_.enabled = false;
				// image_.color = new Color(0, 0, 1, alpha_);
				break;
			case ElementType.Green:
				bg_image_red_.enabled = false;
				bg_image_blue_.enabled = false;
				bg_image_green_.enabled = true;
				// image_.color = new Color(0, 1, 0, alpha_);
				break;
		}
	}

}
