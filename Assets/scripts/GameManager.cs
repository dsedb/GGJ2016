using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

	public GameObject enemy_;
	private EnemySpawnData enemy_spawn_data_;
	private float start_time_;

	void Awake()
	{
        DontDestroyOnLoad(transform.gameObject);
		enemy_spawn_data_ = new EnemySpawnData();
    }	

	void Start()
	{
		if (enemy_ == null) {
			Debug.Assert(false);
		}
		StartCoroutine(main_loop());
	}

	IEnumerator main_loop()
	{
		// game loop
		start_time_ = Time.time;

		for (;;) {
			float game_time = Time.time - start_time_;
			List<EnemySpawnDataUnit> spawn_list = enemy_spawn_data_.getSpawnList(game_time);
			foreach (var spawn in spawn_list) {
				var go = Instantiate(enemy_, spawn.position_, Quaternion.identity) as GameObject;
				var enemy = go.GetComponent<Enemy>();
				enemy.setSpeed(spawn.speed_);
			}
			yield return null;
		}
	}
}
