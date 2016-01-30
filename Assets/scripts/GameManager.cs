using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

	public static GameManager Instance = null;
	public enum SceneType {
		Title,
		Main,
	}
	public SceneType scene_type_;
	public GameObject[] enemy_prefabs_;
	private EnemySpawnData enemy_spawn_data_;
	private ScoreManager score_manager_;
	private float start_time_;

	void Awake()
	{
		if (Instance == null) {
			Instance = this;
			DontDestroyOnLoad(transform.gameObject);
			enemy_spawn_data_ = new EnemySpawnData();
		} else {
			Destroy(gameObject);
		}
    }	

	void Start()
	{
		if (enemy_prefabs_ == null) {
			Debug.Assert(false);
		}
		if (enemy_prefabs_.Length != (int)ElementType.Max) {
			Debug.Assert(false);
		}
		StartCoroutine(main_loop());
	}

	IEnumerator main_loop()
	{
		for (;;) {

			switch (scene_type_) {
				// title loop
				case SceneType.Title:
					for (;;) {
						if (Input.anyKey) {
							Debug.Log("Input.anyKey");
							UnityEngine.SceneManagement.SceneManager.LoadScene("main", 
																			   UnityEngine.SceneManagement.LoadSceneMode.Single);
							break;
						}
						yield return null;
					}
					scene_type_ = SceneType.Main;
				break;
			
				// game start
				case SceneType.Main:

					if (score_manager_ == null) {
						score_manager_ = GameObject.Find("ScorePanel").GetComponent<ScoreManager>();
					}
					score_manager_.setup(enemy_spawn_data_.getTotalNum());
					start_time_ = Time.time;
					for (;;) {
						float game_time = Time.time - start_time_;
						List<EnemySpawnDataUnit> spawn_list = enemy_spawn_data_.getSpawnList(game_time);
						foreach (var spawn in spawn_list) {
							var go = Instantiate(enemy_prefabs_[(int)spawn.element_type_],
												 spawn.position_,
												 Quaternion.identity) as GameObject;
							var enemy = go.GetComponent<Enemy>();
							enemy.setup(spawn);
						}
						yield return null;
					}
					scene_type_ = SceneType.Title;
				break;
			}

			yield return null;
		}
	}

	public void incWin()
	{
		score_manager_.incWin();
	}

	public void incLose()
	{
		score_manager_.incLose();
	}
}
