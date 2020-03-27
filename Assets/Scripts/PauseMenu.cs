using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
	public SceneTransitionScript transition;
	public GameObject settings;
	public SetAudioLevels SAL;
	public Slider music;
	public Slider sfx;

	bool menuOpen = false;

	// Note: replace this input line with any other input you want
	//  for displaying the pause menu.
	private void Update()
	{
		// You can add conditionals other than key input, if necessary,
		//  for situations you'd rather inputs be applied rather than pausing
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			ToggleSettingsMenu(!menuOpen);
		}
	}

	public void ToggleSettingsMenu(bool opening)
	{
		settings.SetActive(opening);
		menuOpen = opening;
		if (opening == true) SetSliderValues(SAL);
	}

	public void ReturnToMenu()
	{
		transition.GoFade();
	}

	public void SetSliderValues(SetAudioLevels audioScript)
	{
		music.value = audioScript.GetMusicLevel();
		sfx.value = audioScript.GetSfxLevel();
	}
}
