using UnityEngine;
using UnityEngine.UI;

namespace Active.Util{
[RequireComponent(typeof (Text))]
public class FrameRateCounter : MonoBehaviour{

	public float period  = 1f;
	public string suffix = "[s]";
	//
	Text  text;
	int   start;
	float stamp;

	private void Start(){
		text  = GetComponent<Text>();
		start = Time.frameCount;
		stamp = Time.time;
		InvokeRepeating("Lap", period, period);
	}

	private void Lap(){
		var end       = Time.frameCount;
		var numFrames = end-start;
		var endTime   = Time.time;
		var interval  = endTime - stamp;
		start = end;
		stamp = endTime;
		text.text = $"{(int)(numFrames/interval)} fps {suffix}";
	}

}}
