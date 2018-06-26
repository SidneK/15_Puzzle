using UnityEngine;
using UnityEngine.SceneManagement;

public class ActionButton : MonoBehaviour
{
	public void UseButton(string Name)
	{
		if (Name == "Reset")
			SceneManager.LoadScene("Main"); // load current scene
	}
}
