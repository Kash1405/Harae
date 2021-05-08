using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
	public static bool GameIsPaused = false;
	public GameObject pauseMenuUI;

	// Update is called once per frame
	void Update()
	{
		//uses the p button to pause and unpause the game
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			if (GameIsPaused)
			{
				Resume();
			}
			else
			{
				Pause();
			}
		}
	}

	// bring down pause menu, resume time, make pause variable false
	public void Resume()
	{
		//disable the game object
		pauseMenuUI.SetActive(false);

		// set time to normal
		Time.timeScale = 1f;

		// set pause variable to false
		GameIsPaused = false;

	}

	//bring up pause menu, freeze time, make pause variable true
	void Pause()
	{
		//enable the game object
		pauseMenuUI.SetActive(true);

		// set time to freeze the game
		Time.timeScale = 0f;

		// set pause variable to true
		GameIsPaused = true;
	}

	public void LoadMainMenu()
    {
		Time.timeScale = 1f;
		SceneManager.LoadScene("Menu");
    }

	public void QuitGame()
    {
		Application.Quit();
    }
}
