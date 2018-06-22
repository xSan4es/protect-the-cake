using UnityEngine;
using System.Collections;

public class Lose : MonoBehaviour
{
    public GameObject loseImage;

	public void LoseGame()
    {
        Time.timeScale = 0;
        loseImage.SetActive(true);
    }
}
