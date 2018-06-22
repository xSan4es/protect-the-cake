using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public AudioListener audioListener;

    //
    public Image iceTrapImage;
    public GameObject iceTrap;
    public float coolDownIceTrap;
    private float curTimeIceTrap;

    public Image sawTrapImage;
    public GameObject sawTrap;
    public float coolDownSawTrap;
    private float curTimeSawTrap;

    public Image GodFingerImage;
    public float coolDownGodFinger;
    private float curTimeGodFinger;

    private int targetTrap;
    private Vector3 dodVector3 = new Vector3();
    private Camera camera;
    private bool blockClick;

    void Start()
    {
        camera = GetComponent<Camera>();
    }

    void Update()
    {
        if (curTimeIceTrap > 0)
        {
            curTimeIceTrap -= Time.deltaTime;
            iceTrapImage.fillAmount = (coolDownIceTrap - curTimeIceTrap) / coolDownIceTrap;
        }

        if (curTimeSawTrap > 0) 
        {
            curTimeSawTrap -= Time.deltaTime;
            sawTrapImage.fillAmount = (coolDownSawTrap - curTimeSawTrap) / coolDownSawTrap;
        }

        if (curTimeGodFinger > 0 && !SearchVariable.isGodFinger)
        {
            curTimeGodFinger -= Time.deltaTime;
            GodFingerImage.fillAmount = (coolDownGodFinger - curTimeGodFinger) / coolDownGodFinger;
        }

        if (curTimeGodFinger <= 0 && SearchVariable.isGodFinger) SearchVariable.isGodFinger = true;

        if (Swipe.iDraw)
        {
            curTimeGodFinger = coolDownGodFinger;
        } 

        if ((Input.touchCount > 0 || Input.GetKeyUp(KeyCode.Mouse0)) && !blockClick)
        {
            switch (targetTrap)
            {
                case 1:
                    {
                        if (curTimeIceTrap <= 0)
                        {
                            dodVector3 = camera.ScreenToWorldPoint(Input.mousePosition);
                            dodVector3.z = 0;
                            Instantiate(iceTrap, dodVector3, Quaternion.identity);
                            curTimeIceTrap = coolDownIceTrap;
                        }
                        break;
                    }

                case 2:
                    {
                        if (curTimeSawTrap <= 0)
                        {
                            dodVector3 = camera.ScreenToWorldPoint(Input.mousePosition);
                            dodVector3.z = 0;
                            Instantiate(sawTrap, dodVector3, Quaternion.identity);
                            curTimeSawTrap = coolDownSawTrap;
                        }
                        break;
                    }
                default: break;
            }
        }
        else blockClick = false;


    }

    public void Pause()
    {
        if (Time.timeScale != 0)
        {
            Time.timeScale = 0;
            audioListener.enabled = false;
        }
        else
        {
            Time.timeScale = 1;
            audioListener.enabled = true;
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void TargetIceTrap()
    {
        SearchVariable.isGodFinger = false;
        blockClick = true;
        targetTrap = 1;
    }

    public void TargetSawTrap()
    {
        SearchVariable.isGodFinger = false;
        blockClick = true;
        targetTrap = 2;
    }

    public void TargetGodFinger()
    {
        if (curTimeGodFinger <= 0) SearchVariable.isGodFinger = true;
        targetTrap = 3;
    }
}
