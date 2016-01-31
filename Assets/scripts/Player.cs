using UnityEngine;
using System.Collections;

public enum ElementType {
	Red,
	Blue,
	Green,
	// Brown,
	// Purple,
	Max,
}

public class Player : MonoBehaviour {

	public GameObject bulletRed_;
	public GameObject bulletBlue_;
	public GameObject bulletGreen_;
	// public GameObject bulletBrown_;
	// public GameObject bulletPurple_;
	public AudioClip audioFire_;
	public AudioClip audioDamage_;

	private AudioSource audio_source_;
	private LowPanelController low_panel_controller_;
	private ElementType element_type_;
	private Vector3 fire_point_z_;
	private Vector3 fire_point_x_;
	private Vector3 fire_point_c_;
	private ElementUI enelemnt_red_;
	private ElementUI enelemnt_blue_;
	private ElementUI enelemnt_green_;
	private GameObject current_bullet_prefab_;
	private ActivateKeyEffect key_z_effect_;
	private ActivateKeyEffect key_x_effect_;
	private ActivateKeyEffect key_c_effect_;

	public bool InPlay { get; set; }
	
	void Awake()
	{
		InPlay = false;
		audio_source_ = GetComponent<AudioSource>();
		element_type_ = ElementType.Red;
		fire_point_z_ = new Vector3(-1f, 0.5f, 0f);
		fire_point_x_ = new Vector3( 0f, 0.5f, 0f);
		fire_point_c_ = new Vector3( 1f, 0.5f, 0f);
		enelemnt_red_ = GameObject.Find("ElementRed").GetComponent<ElementUI>();
		enelemnt_blue_ = GameObject.Find("ElementBlue").GetComponent<ElementUI>();
		enelemnt_green_ = GameObject.Find("ElementGreen").GetComponent<ElementUI>();
		current_bullet_prefab_ = bulletRed_;
		key_z_effect_ = GameObject.Find("KeyZ").GetComponent<ActivateKeyEffect>();
		key_x_effect_ = GameObject.Find("KeyX").GetComponent<ActivateKeyEffect>();
		key_c_effect_ = GameObject.Find("KeyC").GetComponent<ActivateKeyEffect>();
	}

	void Start()
	{
		var go = GameObject.Find("LowPanel");
		Debug.Assert(go != null);
		low_panel_controller_ = go.GetComponent<LowPanelController>();
		select_red();
	}


	private void select_red()
	{
		element_type_ = ElementType.Red;
		current_bullet_prefab_ = bulletRed_;
		enelemnt_red_.setSelected(true);
		enelemnt_blue_.setSelected(false);
		enelemnt_green_.setSelected(false);
		low_panel_controller_.setElementType(element_type_);
	}

	private void select_blue()
	{
		element_type_ = ElementType.Blue;
		current_bullet_prefab_ = bulletBlue_;
		enelemnt_red_.setSelected(false);
		enelemnt_blue_.setSelected(true);
		enelemnt_green_.setSelected(false);
		low_panel_controller_.setElementType(element_type_);
	}

	private void select_green()
	{
		element_type_ = ElementType.Green;
		current_bullet_prefab_ = bulletGreen_;
		enelemnt_red_.setSelected(false);
		enelemnt_blue_.setSelected(false);
		enelemnt_green_.setSelected(true);
		low_panel_controller_.setElementType(element_type_);
	}

	// private void select_brown()
	// {
	// 	element_type_ = ElementType.Brown;
	// 	current_bullet_prefab_ = bulletBrown_;
	// 	enelemnt_red_.setSelected(false);
	// 	enelemnt_blue_.setSelected(false);
	// 	enelemnt_green_.setSelected(false);
	// 	low_panel_controller_.setElementType(element_type_);
	// }

	// private void select_purple()
	// {
	// 	element_type_ = ElementType.Purple;
	// 	current_bullet_prefab_ = bulletPurple_;
	// 	enelemnt_red_.setSelected(false);
	// 	enelemnt_blue_.setSelected(false);
	// 	enelemnt_green_.setSelected(false);
	// 	low_panel_controller_.setElementType(element_type_);
	// }

	private void fire(Vector3 fire_point)
	{
		audio_source_.clip = audioFire_;
		audio_source_.Play();
		var go = Instantiate(current_bullet_prefab_, fire_point, Quaternion.identity) as GameObject;
		var bullet = go.GetComponent<Bullet>();
		bullet.setElementType(element_type_);
	}

	public void setDamage()
	{
		audio_source_.clip = audioDamage_;
		audio_source_.Play();
	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.O)) {
			select_red();
		}
		if (Input.GetKeyDown(KeyCode.K)) {
			select_blue();
		}
		if (Input.GetKeyDown(KeyCode.M)) {
			select_green();
		}
		if (!InPlay) {
			return;
		}
		key_z_effect_.setActive(Input.GetKey(KeyCode.Z));
		key_x_effect_.setActive(Input.GetKey(KeyCode.X));
		key_c_effect_.setActive(Input.GetKey(KeyCode.C));

		if (Input.GetKeyDown(KeyCode.Z)) {
			fire(fire_point_z_);
		}
		if (Input.GetKeyDown(KeyCode.X)) {
			fire(fire_point_x_);
		}
		if (Input.GetKeyDown(KeyCode.C)) {
			fire(fire_point_c_);
		}
	}
}
