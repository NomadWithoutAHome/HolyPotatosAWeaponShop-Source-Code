using UnityEngine;

public class VideoController : MonoBehaviour
{
	private UITexture text;

	private MovieTexture movie;

	private AudioSource audio;

	private void Start()
	{
		text = GetComponent<UITexture>();
		text.height = 720;
		movie = GetComponent<UITexture>().material.mainTexture as MovieTexture;
		audio = GetComponent<AudioSource>();
		audio.clip = movie.audioClip;
		movie.Play();
		int @int = PlayerPrefs.GetInt("musicOnOff", 1);
		if (@int == 1)
		{
			float @float = PlayerPrefs.GetFloat("musicVol", 1f);
			if (base.gameObject.name == "DaylightSplashVideo")
			{
				GameObject.Find("AudioController").GetComponent<AudioController>().setDaylightSplashVolume(@float);
			}
			else if (base.gameObject.name == "DaedalicSplashVideo")
			{
				GameObject.Find("AudioController").GetComponent<AudioController>().setDaedalicSplashVolume(@float);
			}
			audio.Play();
		}
	}

	public void stopVideo()
	{
		movie.Stop();
		audio.Stop();
	}
}
