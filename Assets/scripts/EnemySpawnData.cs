using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public struct EnemySpawnDataUnit
{
	public float time_;
	public Vector3 position_;
	public float speed_;

	public EnemySpawnDataUnit(float time, Vector3 position, float speed)
	{
		time_ = time;
		position_ = position;
		speed_ = speed;
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
		list_.Add(new EnemySpawnDataUnit(1f, new Vector3(0f, 0.5f, 10f), 1f));
		list_.Add(new EnemySpawnDataUnit(2f, new Vector3(0f, 0.5f, 10f), 1f));
		list_.Add(new EnemySpawnDataUnit(3f, new Vector3(0f, 0.5f, 10f), 1f));
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
