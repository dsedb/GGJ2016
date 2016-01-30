using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public struct EnemySpawnDataUnit
{
	public float time_;
	public Vector3 position_;
	public float speed_;
	public ElementType element_type_;

	public EnemySpawnDataUnit(float time, Vector3 position, float speed, ElementType element_type)
	{
		time_ = time;
		position_ = position;
		speed_ = speed;
		element_type_ = element_type;
	}

	public EnemySpawnDataUnit initialize(float time, Vector3 position, float speed, ElementType element_type)
	{
		time_ = time;
		position_ = position;
		speed_ = speed;
		element_type_ = element_type;
		return this;
	}

	public EnemySpawnDataUnit initialize(float time, int lane_index, float speed, ElementType element_type)
	{
		const float fixed_y = 0.5f;
		const float fixed_z = 15f;
		Vector3 position = new Vector3(-1f, fixed_y, fixed_z);
		switch (lane_index) {
			case 0:
				position.x = -2f;
				break;
			case 1:
				position.x = 0f;
				break;
			case 2:
				position.x = 2f;
				break;
		}
		return initialize(time, position, speed, element_type);
	}
}

public class EnemySpawnData
{
	List<EnemySpawnDataUnit> list_;
	int current_index_;

	public EnemySpawnData()
	{
		current_index_ = 0;
		list_ = new List<EnemySpawnDataUnit>();
		var unit = new EnemySpawnDataUnit(0, Vector3.zero, 0f, ElementType.Red);
		float time = 0f;
		time += 1f;
		float speed = 2f;
		for (int i = 0; i < 10; ++i) {
			list_.Add(unit.initialize(time, 0, speed, ElementType.Red));
			time += 1f;
			list_.Add(unit.initialize(time, 1, speed, ElementType.Blue));
			time += 1f;
			list_.Add(unit.initialize(time, 2, speed, ElementType.Green));
			time += 1f;
		}
	}

	public List<EnemySpawnDataUnit> getSpawnList(float game_time)
	{
		var spawn_list = new List<EnemySpawnDataUnit>();
		int avoid_inifinity_loop_count = 0;
		while (current_index_ < list_.Count) {
			float time = list_[current_index_].time_;
			if (time < game_time) {
				spawn_list.Add(list_[current_index_]);
				++current_index_;
				if (current_index_ >= list_.Count) {
					Debug.Log("SPAWN DATA runs out!");
					break;
				}
			}
			++avoid_inifinity_loop_count;
			if (avoid_inifinity_loop_count > 100)
				break;
		}
		return spawn_list;
	}
}
