using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public struct EnemySpawnDataUnit
{
	public float time_;
	public Vector3 position_;
	public float speed_;
	public ElementType element_type_;

	public EnemySpawnDataUnit(float time, int lane_index, float speed, ElementType element_type)
	{
		time_ = time;
		speed_ = speed;
		element_type_ = element_type;
		const float fixed_y = 0.5f;
		const float fixed_z = 10f;
		position_ = new Vector3(-1f, fixed_y, fixed_z);
		switch (lane_index) {
			case 0:
				position_.x = -1f;
				break;
			case 1:
				position_.x = 0f;
				break;
			case 2:
				position_.x = 1f;
				break;
		}
	}
}

public class EnemySpawnData
{
	List<EnemySpawnDataUnit> list_;
	int current_index_;

	public EnemySpawnData()
	{
		list_ = new List<EnemySpawnDataUnit>();
		current_index_ = 0;
	}

	private void add(float time, int lane_index, float speed, ElementType element_type)
	{
		list_.Add(new EnemySpawnDataUnit(time, lane_index, speed, element_type));
	}

	public void createData()
	{
		list_.Clear();
		float time = 0f;
		time += 1f;
		float speed = 1f;
#if TEST
		for (int i = 0; i < 1; ++i) {
			add(time, 0, speed, (ElementType)Random.Range(0f, 3f));
			time += 1f;
			add(time, 1, speed, (ElementType)Random.Range(0f, 3f));
			time += 1f;
			add(time, 2, speed, (ElementType)Random.Range(0f, 3f));
			time += 1f;
			// list_.Add(unit.initialize(time, 0, speed, (ElementType)Random.Range(0f, 3f)));
			// time += 1f;
			// list_.Add(unit.initialize(time, 1, speed, (ElementType)Random.Range(0f, 3f)));
			// time += 1f;
			// list_.Add(unit.initialize(time, 2, speed, (ElementType)Random.Range(0f, 3f)));
			// time += 1f;
		}
#else
        add(time, 0, speed, ElementType.Red);
        time += 1f;
        add(time, 1, speed, ElementType.Red);
        time += 1f;
        add(time, 2, speed, ElementType.Red);
        time += 2f;

        add(time, 0, speed, ElementType.Red);
        time += 1.5f;
        add(time, 1, speed, ElementType.Blue);
        time += 2f;
        add(time, 2, speed, ElementType.Red);
        time += 2f;

        add(time, 0, speed, ElementType.Blue);
        time += 1f;
        add(time, 1, speed, ElementType.Red);
        time += 2f;
        add(time, 2, speed, ElementType.Red);
        time += 2f;

        add(time, 0, speed, ElementType.Red);
        time += 1f;
        add(time, 1, speed, ElementType.Blue);
        time += 1f;
        add(time, 2, speed, ElementType.Blue);
        time += 1f;

        add(time, 0, speed, ElementType.Green);
        time += 1f;
        add(time, 1, speed, ElementType.Red);
        time += 2f;
        add(time, 2, speed, ElementType.Red);
        time += 3f;

        add(time, 0, speed, ElementType.Red);
        time += 1f;
        add(time, 1, speed, ElementType.Blue);
        time += 1f;
        add(time, 2, speed, ElementType.Green);
        time += 1f;

        add(time, 0, speed, ElementType.Red);
        time += 1f;
        add(time, 1, speed, ElementType.Green);
        time += 2f;
        add(time, 2, speed, ElementType.Green);
        time += 1f;

        add(time, 0, speed, ElementType.Blue);
        time += 1f;
        add(time, 1, speed, ElementType.Red);
        time += 1f;
        add(time, 2, speed, ElementType.Green);
        time += 1f;

        add(time, 0, speed, ElementType.Red);
        time += 1f;
        add(time, 1, speed, ElementType.Blue);
        time += 1f;
        add(time, 2, speed, ElementType.Red);
        time += 1f;

        add(time, 0, speed, ElementType.Green);
        time += 1f;
        add(time, 1, speed, ElementType.Blue);
        time += 1f;
        add(time, 2, speed, ElementType.Red);
        time += 1f;

        add(time, 0, speed, ElementType.Red);
        time += 1f;
        add(time, 1, speed, ElementType.Green);
        time += 1f;
        add(time, 2, speed, ElementType.Blue);
        time += 1f;

        add(time, 0, speed, ElementType.Green);
        time += 1f;
        add(time, 1, speed, ElementType.Blue);
        time += 1f;
        add(time, 2, speed, ElementType.Red);
        time += 1f;

        add(time, 0, speed, ElementType.Green);
        time += 1f;
        add(time, 1, speed, ElementType.Blue);
        time += 1f;
        add(time, 2, speed, ElementType.Red);
        time += 1f;
#endif
	}

	public int getTotalNum()
	{
		return list_.Count;
	}

	public List<EnemySpawnDataUnit> getSpawnList(float game_time, out bool runout)
	{
		var spawn_list = new List<EnemySpawnDataUnit>();
		int avoid_inifinity_loop_count = 0;
		while (current_index_ < list_.Count) {
			float time = list_[current_index_].time_;
			if (time < game_time) {
				spawn_list.Add(list_[current_index_]);
				++current_index_;
				if (current_index_ >= list_.Count) {
					break;
				}
			}
			++avoid_inifinity_loop_count;
			if (avoid_inifinity_loop_count > 100)
				break;
		}
		runout = (current_index_ >= list_.Count);
		return spawn_list;
	}
}
