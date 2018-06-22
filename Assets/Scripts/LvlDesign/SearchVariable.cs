using UnityEngine;

public class SearchVariable : MonoBehaviour
{
    public static Transform cageTransform;
    public static Cage cageScript;
    public static Lose loseScript;
    public static bool isGodFinger;

    public void Awake()
    {
        if (Time.timeScale == 0) Time.timeScale = 1;

        cageTransform = GameObject.FindGameObjectWithTag("Player").transform;
        cageScript = cageTransform.GetComponent<Cage>();
        loseScript = Camera.main.GetComponent<Lose>();
    }
}
