using UnityEngine;
using System.Collections;

public class Thorn : MonoBehaviour
{
    private Transform myTransform;
    private Vector3 direction;
    private bool isAttack;
    private bool isSpike;
    private Animator myAnimator;

    public GameObject spikesGroup;
    public float protectedStatusTime;
    public float unProtectedStatusTime;
    private float curTime;

    public int health = 8;
    public float moveSpeed = 3f;
    public int damage = 3;

    private float deltaTimeSawDamage;
    private float deltaTimeFreeze;
    private float speedMofifier;

    void Start()
    {
        myTransform = transform;
        myAnimator = myTransform.GetComponent<Animator>();
        direction = SearchVariable.cageTransform.position - myTransform.position;
        curTime = unProtectedStatusTime;
        direction.Normalize();
    }

    void FixedUpdate()
    {
        if (!isAttack) MoveToCage();

        if (deltaTimeSawDamage > 0) deltaTimeSawDamage -= Time.deltaTime;
        
        if (deltaTimeFreeze > 0) deltaTimeFreeze -= Time.deltaTime;
            else speedMofifier = 1f;

        if (curTime <= 0)
        {
            if (isSpike)
                curTime = unProtectedStatusTime;
            else
                curTime = protectedStatusTime;

            Protection();
        }
        else
            curTime -= Time.deltaTime;
    }

    private void Protection()
    {
        isSpike = !isSpike;

        if (isSpike)
            myAnimator.SetInteger("IsSpike", 0);
        else
            myAnimator.SetInteger("IsSpike", 1);
    }

    private void MoveToCage()
    {
        myTransform.position = myTransform.position + (speedMofifier * direction * moveSpeed / 100);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag.Contains("Player"))
        {
            isAttack = true;
            myAnimator.SetBool("IsAttack", true);

            spikesGroup.transform.GetChild(0).gameObject.SetActive(true);
            spikesGroup.transform.GetChild(1).gameObject.SetActive(true);
            spikesGroup.transform.GetChild(2).gameObject.SetActive(true);
            spikesGroup.transform.GetChild(3).gameObject.SetActive(true);

            isSpike = true;
        }
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.tag.Contains("IceTrap"))
        {
            deltaTimeFreeze = 5f;
            speedMofifier = 0.4f;
        }

        if (col.gameObject.tag.Contains("SawTrap"))
        {
            if (deltaTimeSawDamage <= 0)
            {
                deltaTimeSawDamage = 0.3f;
                if (isSpike) health--;
                if (health < 0) Destroy(gameObject);
            }
        }
    }

    public void SetDamageCage()
    {
        SearchVariable.cageScript.SetDamageCage(damage);
    }

    void OnMouseOver()
    {
        if(isSpike && Swipe.iDraw)
            myAnimator.SetBool("IsDead", true);
    }

    public void DeadThornMonster()
    {
        Destroy(gameObject);
    }

    public void SpikeCast()
    {
        myAnimator.SetInteger("IsSpike", 2);

        spikesGroup.transform.GetChild(0).gameObject.SetActive(!isSpike);
        spikesGroup.transform.GetChild(1).gameObject.SetActive(!isSpike);
        spikesGroup.transform.GetChild(2).gameObject.SetActive(!isSpike);
        spikesGroup.transform.GetChild(3).gameObject.SetActive(!isSpike);
    }

}
