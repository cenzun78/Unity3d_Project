using UnityEngine;
using System.Collections;

public class AlarmLight : MonoBehaviour {

	public float fadeSpeed = 2f;                // 是灯光渐渐 亮/暗 的速度
	public float highIntensity = 2f;            // 灯光最亮时的强度
	public float lowIntensity = 0.5f;			// 灯光最暗时的强度
	public float changMargin = 0.2f;			// 偏差值
	public bool alarmOn;						// 灯光的 开/关

	private float targetIntensity;
	private Light l;

	void Awake(){
		l = GetComponent<Light> ();
		l.intensity = 0f;
		targetIntensity = highIntensity;

	}

	void Update () {
		if (alarmOn) {
			// 使灯光的强度渐渐变为 targetIntensity
			l.intensity = Mathf.Lerp (l.intensity, targetIntensity, fadeSpeed * Time.deltaTime);
			CheckTargetIntensity ();
		} else {
			l.intensity = Mathf.Lerp (l.intensity, 0f, fadeSpeed * Time.deltaTime);
		}
	}

	// 灯光强度渐渐变为最高度时重新降到最小值  high ~ low ~ high ~ low ...
	void CheckTargetIntensity () {
		if (Mathf.Abs(targetIntensity - l.intensity) < changMargin) {
			if (targetIntensity == highIntensity) {
				targetIntensity = lowIntensity;
			} else {
				targetIntensity = highIntensity;
			}
		}
	}
}
 