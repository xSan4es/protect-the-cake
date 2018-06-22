using UnityEngine;
using System.Collections;

public class LifeTimeTrap : MonoBehaviour
{
    public float lifeTime;
    private float curTime;

    void Start()
    {
        curTime = lifeTime + 1;
    }

    void Update()
    {
        curTime -= Time.deltaTime;
        if (curTime <= 0) Destroy(gameObject);
    }
}
