using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	public GameObject bulletRed_;
	public GameObject bulletBlue_;
	public GameObject bulletGreen_;

	enum BulletType {
		Red,
		Blue,
		Green,
	}
	private BulletType bullet_type_;
	private Vector3 fire_point_x_;
	private Vector3 fire_point_c_;
	private Vector3 fire_point_v_;
	private ElementUI enelemnt_y_;
	private ElementUI enelemnt_h_;
	private ElementUI enelemnt_n_;
	private GameObject current_bullet_prefab_;
	
	void Awake()
	{
		bullet_type_ = BulletType.Red;
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
		select_y();
	}

	private void select_y()
	{
		bullet_type_ = BulletType.Red;
		current_bullet_prefab_ = bulletRed_;
		enelemnt_y_.setSelected(true);
		enelemnt_h_.setSelected(false);
		enelemnt_n_.setSelected(false);
	}

	private void select_h()
	{
		bullet_type_ = BulletType.Blue;
		current_bullet_prefab_ = bulletBlue_;
		enelemnt_y_.setSelected(false);
		enelemnt_h_.setSelected(true);
		enelemnt_n_.setSelected(false);
	}

	private void select_n()
	{
		bullet_type_ = BulletType.Green;
		current_bullet_prefab_ = bulletGreen_;
		enelemnt_y_.setSelected(false);
		enelemnt_h_.setSelected(false);
		enelemnt_n_.setSelected(true);
	}

	private void fire(Vector3 fire_point)
	{
		Instantiate(current_bullet_prefab_, fire_point, Quaternion.identity);
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
