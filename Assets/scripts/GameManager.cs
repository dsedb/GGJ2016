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
	private CameraDamageEffect camera_damage_effect_;
	private WidgetActor message_actor_;
	private WidgetActor score_actor_;
	private float start_time_;
	private Player player_;

	void Awake()
	{
		if (Instance == null) {
			Instance = this;
			DontDestroyOnLoad(transform.gameObject);
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
						if (true &&
							Input.GetKey(KeyCode.O) &&
							Input.GetKey(KeyCode.K) &&
							Input.GetKey(KeyCode.M) && 
							Input.GetKey(KeyCode.Z) &&
							Input.GetKey(KeyCode.X) &&
							Input.GetKey(KeyCode.C) &&
							true) {
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
					if (camera_damage_effect_ == null) {
						camera_damage_effect_ = GameObject.Find("Main Camera").GetComponent<CameraDamageEffect>();
					}
					if (message_actor_ == null) {
						message_actor_ = GameObject.Find("MessagePanel").GetComponent<WidgetActor>();
					}
					if (score_actor_ == null) {
						score_actor_ = GameObject.Find("ScorePanel").GetComponent<WidgetActor>();
					}
					if (player_ == null) {
						player_ = GameObject.Find("player").GetComponent<Player>();
					}
					yield return null;
					GetComponent<AudioSource>().Play();
					enemy_spawn_data_ = new EnemySpawnData();
					enemy_spawn_data_.createData();
					score_manager_.setup(enemy_spawn_data_.getTotalNum());
					start_time_ = Time.time;
					message_actor_.beginMessage("START!");
					player_.InPlay = true;
					for (;;) {
						float game_time = Time.time - start_time_;
						bool runout;
						List<EnemySpawnDataUnit> spawn_list = enemy_spawn_data_.getSpawnList(game_time, out runout);
						foreach (var spawn in spawn_list) {
							Debug.Assert(enemy_prefabs_[(int)spawn.element_type_] != null);
							var go = Instantiate(enemy_prefabs_[(int)spawn.element_type_],
												 spawn.position_,
												 Quaternion.identity) as GameObject;
							var enemy = go.GetComponent<Enemy>();
							enemy.setup(spawn);
						}
						yield return null;
						if (runout && GameObject.FindGameObjectsWithTag("Enemy").Length <= 0) {
							break;
						}
					}
					player_.InPlay = false;
					yield return new WaitForSeconds(1);
					message_actor_.beginMessage("FINISH!");
					yield return new WaitForSeconds(1);
					GetComponent<AudioSource>().Stop();
					
					score_actor_.beginResult();
					yield return new WaitForSeconds(2);
					if (score_manager_.won()) {
						message_actor_.beginMessage("WIN!");
					} else {
						message_actor_.beginMessage("LOSE");
					}
					yield return new WaitForSeconds(3);

					scene_type_ = SceneType.Title;
					UnityEngine.SceneManagement.SceneManager.LoadScene("title", 
																	   UnityEngine.SceneManagement.LoadSceneMode.Single);
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
		camera_damage_effect_.startDamage(0.5f /* duration */, 0.5f /* power */);
		player_.setDamage();
	}
}
