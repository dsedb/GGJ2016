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
	public LowPanelController low_panel_controller_;

	private ElementType element_type_;
	private Vector3 fire_point_x_;
	private Vector3 fire_point_c_;
	private Vector3 fire_point_v_;
	private ElementUI enelemnt_y_;
	private ElementUI enelemnt_h_;
	private ElementUI enelemnt_n_;
	private GameObject current_bullet_prefab_;
	
	void Awake()
	{
		element_type_ = ElementType.Red;
		fire_point_x_ = new Vector3(-2f, 0.5f, 0f);
		fire_point_c_ = new Vector3( 0f, 0.5f, 0f);
		fire_point_v_ = new Vector3( 2f, 0.5f, 0f);
		enelemnt_y_ = GameObject.Find("ElementY").GetComponent<ElementUI>();
		enelemnt_h_ = GameObject.Find("ElementH").GetComponent<ElementUI>();
		enelemnt_n_ = GameObject.Find("ElementN").GetComponent<ElementUI>();
		current_bullet_prefab_ = bulletRed_;
	}

	void Start()
	{
		var go = GameObject.Find("LowPanel");
		Debug.Assert(go != null);
		low_panel_controller_ = go.GetComponent<LowPanelController>();
		select_y();
	}

	private void select_y()
	{
		element_type_ = ElementType.Red;
		current_bullet_prefab_ = bulletRed_;
		enelemnt_y_.setSelected(true);
		enelemnt_h_.setSelected(false);
		enelemnt_n_.setSelected(false);
		low_panel_controller_.setElementType(element_type_);
	}

	private void select_h()
	{
		element_type_ = ElementType.Blue;
		current_bullet_prefab_ = bulletBlue_;
		enelemnt_y_.setSelected(false);
		enelemnt_h_.setSelected(true);
		enelemnt_n_.setSelected(false);
		low_panel_controller_.setElementType(element_type_);
	}

	private void select_n()
	{
		element_type_ = ElementType.Green;
		current_bullet_prefab_ = bulletGreen_;
		enelemnt_y_.setSelected(false);
		enelemnt_h_.setSelected(false);
		enelemnt_n_.setSelected(true);
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
		if (Input.GetKeyDown(KeyCode.X)) {
			fire(fire_point_x_);
		}
		if (Input.GetKeyDown(KeyCode.C)) {
			fire(fire_point_c_);
		}
		if (Input.GetKeyDown(KeyCode.V)) {
			fire(fire_point_v_);
		}
		if (Input.GetKeyDown(KeyCode.Y)) {
			select_y();
		}
		if (Input.GetKeyDown(KeyCode.H)) {
			select_h();
		}
		if (Input.GetKeyDown(KeyCode.N)) {
			select_n();
		}
		Input.GetAxis("Horizontal");
	}
}
