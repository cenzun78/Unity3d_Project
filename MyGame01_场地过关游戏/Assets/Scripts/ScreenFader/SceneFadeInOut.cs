using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SceneFadeInOut : MonoBehaviour {
	public float fadeSpeed = 1.5f;					// 淡入淡出的变化速度
	private bool sceneStarting = true;						// 开始淡入
	private GUITexture gt;

	void Awake () {
		gt = GetComponent<GUITexture> ();
		// 把 GUI 盖住整个画面
		// Rect 是矩形, （x, y, width, height）
		gt.pixelInset = new Rect (0f, 0f, Screen.width, Screen.height);
	}

	void Update () {
		if (sceneStarting) {
			StartScene ();
		} 
	}

	// 清理画面切换到游戏画面
	public void FadeToClear () {
		gt.color = Color.Lerp (gt.color, Color.clear, fadeSpeed * Time.deltaTime);
	}

	// 切换到结束画面 黑屏
	public void FadeToBlack () {
		gt.color = Color.Lerp (gt.color, Color.black, fadeSpeed * Time.deltaTime);
	}

	// 开始游戏
	public void StartScene () {
		FadeToClear ();
		if (gt.color.a <= 0.05f) {
			gt.color = Color.clear;
			gt.enabled = false;
			sceneStarting = false;
		}
	}

	// 结束游戏
	public void EndScene () {
		gt.enabled = true;
		FadeToBlack ();
		if (gt.color.a >= 0.95f) {
			SceneManager.LoadScene (0);
		}
	}
}
