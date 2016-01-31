using UnityEngine;
using System.Collections;
using System.Text;

public class TitleManager : MonoBehaviour {

	private ElementUI enelemnt_red_;
	private ElementUI enelemnt_blue_;
	private ElementUI enelemnt_green_;
	private ActivateKeyEffect key_z_effect_;
	private ActivateKeyEffect key_x_effect_;
	private ActivateKeyEffect key_c_effect_;
	private UnityEngine.UI.Text message_text_;
	
	void Awake()
	{
		enelemnt_red_ = GameObject.Find("ElementRed").GetComponent<ElementUI>();
		enelemnt_blue_ = GameObject.Find("ElementBlue").GetComponent<ElementUI>();
		enelemnt_green_ = GameObject.Find("ElementGreen").GetComponent<ElementUI>();
		key_z_effect_ = GameObject.Find("KeyZ").GetComponent<ActivateKeyEffect>();
		key_x_effect_ = GameObject.Find("KeyX").GetComponent<ActivateKeyEffect>();
		key_c_effect_ = GameObject.Find("KeyC").GetComponent<ActivateKeyEffect>();
		message_text_ = GameObject.Find("Message").GetComponent<UnityEngine.UI.Text>();
	}

	void Update()
	{
		var text = new StringBuilder();
		text.Append("Press ");

		if (Input.GetKey(KeyCode.Z)) {
			text.Append("<color=red>");
		}
		text.Append("Z ");
		if (Input.GetKey(KeyCode.Z)) {
			text.Append("</color>");
		}

		if (Input.GetKey(KeyCode.X)) {
			text.Append("<color=red>");
		}
		text.Append("X ");
		if (Input.GetKey(KeyCode.X)) {
			text.Append("</color>");
		}

		if (Input.GetKey(KeyCode.C)) {
			text.Append("<color=red>");
		}
		text.Append("C ");
		if (Input.GetKey(KeyCode.C)) {
			text.Append("</color>");
		}

		if (Input.GetKey(KeyCode.O)) {
			text.Append("<color=red>");
		}
		text.Append("O ");
		if (Input.GetKey(KeyCode.O)) {
			text.Append("</color>");
		}

		if (Input.GetKey(KeyCode.K)) {
			text.Append("<color=red>");
		}
		text.Append("K ");
		if (Input.GetKey(KeyCode.K)) {
			text.Append("</color>");
		}

		if (Input.GetKey(KeyCode.M)) {
			text.Append("<color=red>");
		}
		text.Append("M ");
		if (Input.GetKey(KeyCode.M)) {
			text.Append("</color>");
		}
		text.Append("keys!");
		message_text_.text = text.ToString();

		enelemnt_red_.setSelected(Input.GetKey(KeyCode.O));
		enelemnt_blue_.setSelected(Input.GetKey(KeyCode.K));
		enelemnt_green_.setSelected(Input.GetKey(KeyCode.M));
		key_z_effect_.setActive(Input.GetKey(KeyCode.Z));
		key_x_effect_.setActive(Input.GetKey(KeyCode.X));
		key_c_effect_.setActive(Input.GetKey(KeyCode.C));


	}
}
