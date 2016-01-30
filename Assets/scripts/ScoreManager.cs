using UnityEngine;
using System.Collections;

public class ScoreManager : MonoBehaviour {
	private UnityEngine.UI.Scrollbar win_scrollbar_;
	private UnityEngine.UI.Scrollbar lose_scrollbar_;
	private int win_count_;
	private int lose_count_;
	private int total_num_;
	private float target_win_ratio_;
	private float target_lose_ratio_;

	public void setup(int total_num)
	{
		total_num_ = total_num;
		reset();
	}

	void Awake()
	{
		win_scrollbar_ = transform.Find("WinScrollbar").GetComponent<UnityEngine.UI.Scrollbar>();
		lose_scrollbar_ = transform.Find("LoseScrollbar").GetComponent<UnityEngine.UI.Scrollbar>();
	}

	private void reset()
	{
		win_count_ = 0;
		lose_count_ = 0;
		win_scrollbar_.size = 0f;
		lose_scrollbar_.size = 0f;
	}

	public void incWin()
	{
		++win_count_;
		target_win_ratio_ = (float)win_count_ / (float)total_num_;
	}

	public void incLose()
	{
		++lose_count_;
		target_lose_ratio_ = (float)lose_count_ / (float)total_num_;
	}

	public bool won()
	{
		return win_count_ >= lose_count_;
	}

	void Update()
	{
		win_scrollbar_.size = win_scrollbar_.size + (target_win_ratio_ - win_scrollbar_.size) * 0.1f;
		lose_scrollbar_.size = lose_scrollbar_.size + (target_lose_ratio_ - lose_scrollbar_.size) * 0.1f;
	}

}
