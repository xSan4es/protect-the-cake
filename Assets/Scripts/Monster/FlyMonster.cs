using UnityEngine;

public class FlyMonster : MonoBehaviour
{
    private Transform myTransform;
    private Vector3 direction;
    private bool isAttack;
    private Animator myAnimator;

    public int health = 5;
    public float moveSpeed = 2f;
    public int damage = 5;

    private float deltaTimeSawDamage;
    private float deltaTimeFreeze;
    private float speedMofifier;

    void Start()
    {
        myTransform = transform;
        myAnimator = myTransform.GetComponent<Animator>();
        direction = SearchVariable.cageTransform.position - myTransform.position;
        direction.Normalize();
    }

    void FixedUpdate()
    {
        if(!isAttack) MoveToCage();
        if (deltaTimeSawDamage > 0) deltaTimeSawDamage -= Time.deltaTime;
            
        if (deltaTimeFreeze > 0) deltaTimeFreeze -= Time.deltaTime;
            else speedMofifier = 1f;
    }

    private void MoveToCage()
    {
        myTransform.position = myTransform.position + (speedMofifier * direction * moveSpeed / 100);
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.tag.Contains("Player")) 
        {
            isAttack = true;
            myAnimator.SetBool("IsAttack", true);
        }

        if (col.gameObject.tag.Contains("IceTrap"))
        {
            deltaTimeFreeze = 5f;
            speedMofifier = 0.2f;
        }

        if (col.gameObject.tag.Contains("SawTrap"))
        {
            if (deltaTimeSawDamage <= 0)
            {
                deltaTimeSawDamage = 0.3f;
                health--;
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
        if (Swipe.iDraw)
            myAnimator.SetBool("IsDead", true);
    }

    public void DeadFlyMonster()
    {
        Destroy(gameObject);
    }

}
