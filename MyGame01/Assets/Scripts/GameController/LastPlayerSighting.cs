using UnityEngine;
using System.Collections;

public class LastPlayerSighting : MonoBehaviour {
	public Vector3 position = new Vector3(1000f, 1000f, 1000f);                   	// 怪物检测到的 Player 默认位置   这时没反应
	public Vector3 resetposition = new Vector3(1000f, 1000f, 1000f);				// Player 移动后的位置
	public float lightHighIntensity = 0.25f;
	public float lightLowIntensity = 0f;
	public float fadeSpeed = 7f;
	public float musicFadeSpeed = 1f;

	private AlarmLight alarm;
	private Light mainLight;
	private AudioSource panicAudio;					// 被怪物发现时紧张的音乐
	private AudioSource[] sirens;					// 警报喇叭
	private AudioSource mainAudio; 

	void Awake () {
		alarm = GameObject.FindGameObjectWithTag(Tags.alarm).GetComponent<AlarmLight> ();
		mainLight = GameObject.FindGameObjectWithTag (Tags.mainLight).GetComponent<Light> ();
		panicAudio = GameObject.Find ("secondaryMusic").GetComponent<AudioSource> ();
		GameObject[] sirenGameObjects = GameObject.FindGameObjectsWithTag (Tags.siren);
		sirens = new AudioSource[sirenGameObjects.Length];
		mainAudio = GetComponent<AudioSource> ();
		for (int i = 0; i < sirens.Length; i++) {
			sirens [i] = sirenGameObjects [i].GetComponent<AudioSource>();
		}

	}

	void Update () {
		SwitchAlarms ();
		MusicFading ();
	}

	// 切换 mainLight, AlarmLight, sirens 的开关
	void SwitchAlarms () {
		alarm.alarmOn = (position != resetposition);

		float newIntensity;
		if (position != resetposition) {
			newIntensity = lightLowIntensity;
		} else {
			newIntensity = lightHighIntensity;
		}

		mainLight.intensity = Mathf.Lerp (mainLight.intensity, newIntensity, fadeSpeed * Time.deltaTime);

		for (int i = 0; i < sirens.Length; i++) {
			if (position != resetposition && !sirens[i].isPlaying) {
				sirens [i].Play ();
			} else if (position == resetposition) {
				sirens [i].Stop ();
			}
		}

	}

	void MusicFading () {
		if (position != resetposition) {
			mainAudio.volume = Mathf.Lerp (mainAudio.volume, 0f, fadeSpeed * Time.deltaTime);
			panicAudio.volume = Mathf.Lerp (panicAudio.volume, 0.8f, fadeSpeed * Time.deltaTime);
		} else {
			mainAudio.volume = Mathf.Lerp (mainAudio.volume, 0.8f, fadeSpeed * Time.deltaTime);
			panicAudio.volume = Mathf.Lerp (panicAudio.volume, 0f, fadeSpeed * Time.deltaTime);
		}
	}
}