using UnityEngine;
using System.Collections;

public enum ElementType {
	Red,
	Blue,
	Green,
	Brown,
	Purple,
	Max,
}

public class Player : MonoBehaviour {

	public GameObject bulletRed_;
	public GameObject bulletBlue_;
	public GameObject bulletGreen_;
	public GameObject bulletBrown_;
	public GameObject bulletPurple_;

	private LowPanelController low_panel_controller_;
	private ElementType element_type_;
	private Vector3 fire_point_z_;
	private Vector3 fire_point_x_;
	private Vector3 fire_point_c_;
	private ElementUI enelemnt_red_;
	private ElementUI enelemnt_blue_;
	private ElementUI enelemnt_green_;
	private ElementUI enelemnt_brown_;
	private ElementUI enelemnt_purple_;
	private GameObject current_bullet_prefab_;
	
	void Awake()
	{
		element_type_ = ElementType.Red;
		fire_point_z_ = new Vector3(-2f, 0.5f, 0f);
		fire_point_x_ = new Vector3( 0f, 0.5f, 0f);
		fire_point_c_ = new Vector3( 2f, 0.5f, 0f);
		enelemnt_red_ = GameObject.Find("ElementRed").GetComponent<ElementUI>();
		enelemnt_blue_ = GameObject.Find("ElementBlue").GetComponent<ElementUI>();
		enelemnt_green_ = GameObject.Find("ElementGreen").GetComponent<ElementUI>();
		enelemnt_brown_ = GameObject.Find("ElementBrown").GetComponent<ElementUI>();
		enelemnt_purple_ = GameObject.Find("ElementPurple").GetComponent<ElementUI>();
		current_bullet_prefab_ = bulletRed_;
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
		enelemnt_brown_.setSelected(false);
		enelemnt_purple_.setSelected(false);
		low_panel_controller_.setElementType(element_type_);
	}

	private void select_blue()
	{
		element_type_ = ElementType.Blue;
		current_bullet_prefab_ = bulletBlue_;
		enelemnt_red_.setSelected(false);
		enelemnt_blue_.setSelected(true);
		enelemnt_green_.setSelected(false);
		enelemnt_brown_.setSelected(false);
		enelemnt_purple_.setSelected(false);
		low_panel_controller_.setElementType(element_type_);
	}

	private void select_green()
	{
		element_type_ = ElementType.Green;
		current_bullet_prefab_ = bulletGreen_;
		enelemnt_red_.setSelected(false);
		enelemnt_blue_.setSelected(false);
		enelemnt_green_.setSelected(true);
		enelemnt_brown_.setSelected(false);
		enelemnt_purple_.setSelected(false);
		low_panel_controller_.setElementType(element_type_);
	}

	private void select_brown()
	{
		element_type_ = ElementType.Brown;
		current_bullet_prefab_ = bulletBrown_;
		enelemnt_red_.setSelected(false);
		enelemnt_blue_.setSelected(false);
		enelemnt_green_.setSelected(false);
		enelemnt_brown_.setSelected(true);
		enelemnt_purple_.setSelected(false);
		low_panel_controller_.setElementType(element_type_);
	}

	private void select_purple()
	{
		element_type_ = ElementType.Purple;
		current_bullet_prefab_ = bulletPurple_;
		enelemnt_red_.setSelected(false);
		enelemnt_blue_.setSelected(false);
		enelemnt_green_.setSelected(false);
		enelemnt_brown_.setSelected(false);
		enelemnt_purple_.setSelected(true);
		low_panel_controller_.setElementType(element_type_);
	}

	private void fire(Vector3 fire_point)
	{
		var go = Instantiate(current_bullet_prefab_, fire_point, Quaternion.identity) as GameObject;
		var bullet = go.GetComponent<Bullet>();
		bullet.setElementType(element_type_);
	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Z)) {
			fire(fire_point_z_);
		}
		if (Input.GetKeyDown(KeyCode.X)) {
			fire(fire_point_x_);
		}
		if (Input.GetKeyDown(KeyCode.C)) {
			fire(fire_point_c_);
		}
		if (Input.GetKeyDown(KeyCode.Alpha4)) {
			select_red();
		}
		if (Input.GetKeyDown(KeyCode.R)) {
			select_blue();
		}
		if (Input.GetKeyDown(KeyCode.F)) {
			select_green();
		}
		if (Input.GetKeyDown(KeyCode.V)) {
			select_brown();
		}
		if (Input.GetKeyDown(KeyCode.Space)) {
			select_purple();
		}
		Input.GetAxis("Horizontal");
	}
}
