using UnityEngine;
using UnityEngine.UI;

public class Cage : MonoBehaviour
{
    public int maxHealthCage = 10;
    private int currentHealthCage;
    private bool isAttackedCake;
    private float timePeace;
    private bool isDestroyCage;
    public Animator cageAnim;
    public Slider healthSlider;
    public Sprite beatenCage;

    //костыль
    private bool check;

    void Start()
    {
        currentHealthCage = maxHealthCage;
    }

    void FixedUpdate()
    {
        if(isAttackedCake)
        {
            timePeace += Time.fixedDeltaTime;

            if (timePeace >= 3)
            {
                isAttackedCake = false;
                cageAnim.SetBool("isAttacked", false);
            }
        }
    }

    public void SetDamageCage(int damage)
    {
        isAttackedCake = true;
        cageAnim.SetBool("isAttacked", true);
        timePeace = 0;

        currentHealthCage -= damage;
        healthSlider.value = (float)currentHealthCage / maxHealthCage;

        //как нормально подставить разбитую клетку только раз?
        if (currentHealthCage < maxHealthCage/2 && !check)
        {
            check = true;
            transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = beatenCage;
        }

        if (isDestroyCage)
            SearchVariable.loseScript.LoseGame();


        if (currentHealthCage <= 0)
        {
            isDestroyCage = true;
            transform.GetChild(0).gameObject.SetActive(false);
            transform.GetChild(1).gameObject.SetActive(false);
        }
    }
}
