using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour {

	public void StartGame(){
		Application.LoadLevel ("level1");
	}

	public void GoToSite(){
		// Awesome website #hireMe
		Application.OpenURL ("http://filipevargas.com");
	}

	public void QuitGame(){
		Application.Quit();
	}
}
