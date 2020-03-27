using UnityEngine;
using System.Collections;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SetAudioLevels : MonoBehaviour {

	public AudioMixer mainMixer;                    //Used to hold a reference to the AudioMixer mainMixer

	//Call this function and pass in the float parameter musicLvl to set the volume of the AudioMixerGroup Music in mainMixer
	public void SetMusicLevel(float musicLvl)
	{
		mainMixer.SetFloat("musicVol", musicLvl);
	}

	//Call this function and pass in the float parameter sfxLevel to set the volume of the AudioMixerGroup SoundFx in mainMixer
	public void SetSfxLevel(float sfxLevel)
	{
		mainMixer.SetFloat("sfxVol", sfxLevel);
	}

	public float GetMusicLevel()
	{
		float vol = 0;
		mainMixer.GetFloat("musicVol", out vol);
		return vol;
	}

	public float GetSfxLevel()
	{
		float vol = 0;
		mainMixer.GetFloat("sfxVol", out vol);
		return vol;
	}

}
