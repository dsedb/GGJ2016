using UnityEngine;
using System.Collections;

public class WidgetActor : MonoBehaviour {

	private Vector3 original_position_;
	private Vector3 target_position_;
	private Coroutine coroutine_;
	private RectTransform rt_;
	
	void Awake()
	{
		rt_ = GetComponent<RectTransform>();
		GetComponent<UnityEngine.UI.Image>().enabled = false;
	}

	private void reset()
	{
		rt_.position = original_position_ + new Vector3(1000f, 0, 0);
	}

	// private void reset(string text)
	// {
	// 	reset();
	// 	transform.Find("Text").GetComponent<UnityEngine.UI.Text>().text = text;
	// }

	void Start()
	{
		original_position_ = rt_.position;
		target_position_ = original_position_;
	}

	// public void beginMessage(string text)
	// {
	// 	if (coroutine_ != null) {
	// 		StopCoroutine(coroutine_);
	// 	}
	// 	reset(text);
	// 	coroutine_ = StartCoroutine(loop_message());
	// }
	
	public void beginResult()
	{
		if (coroutine_ != null) {
			StopCoroutine(coroutine_);
		}
		reset();
		coroutine_ = StartCoroutine(loop_score());
	}
	
	// private IEnumerator loop_message()
	// {
	// 	target_position_ = original_position_;
	// 	yield return new WaitForSeconds(1);
	// 	target_position_ = original_position_ + new Vector3(-1000f, 0f, 0f);
	// }

	private IEnumerator loop_score()
	{
		target_position_ = original_position_ + new Vector3(0f, -400f, 0f);
		yield return new WaitForSeconds(3);
		target_position_ = original_position_;
	}

	public void begin()
	{
		if (coroutine_ != null) {
			StopCoroutine(coroutine_);
		}
		reset();
		coroutine_ = StartCoroutine(loop());
	}

	private IEnumerator loop()
	{
		GetComponent<UnityEngine.UI.Image>().enabled = true;
		target_position_ = original_position_;
		yield return new WaitForSeconds(1);
		target_position_ = original_position_ + new Vector3(-1000f, 0f, 0f);
		yield return new WaitForSeconds(3);
		GetComponent<UnityEngine.UI.Image>().enabled = false;
	}

	void Update()
	{
		rt_.position = Vector3.Lerp(rt_.position, target_position_, 0.2f);
	}
}
