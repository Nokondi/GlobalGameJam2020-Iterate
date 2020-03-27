using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
	public SceneTransitionScript transition;
	public GameObject main;
	public GameObject settings;
    public GameObject controls;
    public GameObject credits;
    public GameObject mainCanvas;
    public GameObject iteratorCanvas;
	public Slider music;
	public Slider sfx;

	public void ToggleMainMenu()
	{
		main.SetActive(true);
		settings.SetActive(false);
        controls.SetActive(false);
        credits.SetActive(false);
	}

	public void ToggleSettingsMenu()
	{
        main.SetActive(false);
        settings.SetActive(true);
        controls.SetActive(false);
        credits.SetActive(false);
    }

    public void ToggleControlsMenu()
    {
        main.SetActive(false);
        settings.SetActive(false);
        controls.SetActive(true);
        credits.SetActive(false);
    }

    public void ToggleCreditsMenu()
    {
        main.SetActive(false);
        settings.SetActive(false);
        controls.SetActive(false);
        credits.SetActive(true);
    }

	public void StartGame()
	{
        iteratorCanvas.SetActive(true);
        mainCanvas.SetActive(false);
    }

    public void BackToMenu()
    {
        iteratorCanvas.SetActive(false);
        mainCanvas.SetActive(true);
    }

	public void QuitGame()
	{
		Application.Quit();
	}

	public void SetSliderValues(SetAudioLevels audioScript)
	{
		music.value = audioScript.GetMusicLevel();
		sfx.value = audioScript.GetSfxLevel();
	}

}
