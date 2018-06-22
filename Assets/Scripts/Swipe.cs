using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Swipe : MonoBehaviour
{
    public Color color1 = Color.yellow;
    public Color color2 = Color.red;

    private GameObject lineGO;
    private LineRenderer lineRenderer;
    private int i = 0;
    public static bool iDraw;
    private float timeDraw = 1f;

    void Start()
    {
        lineGO = new GameObject("Line");
        lineGO.AddComponent<LineRenderer>();
        lineRenderer = lineGO.GetComponent<LineRenderer>();
        lineRenderer.material = new Material(Shader.Find("Mobile/Particles/Additive"));
        lineRenderer.SetColors(color1, color2);
        lineRenderer.SetWidth(0.3F, 0);
        lineRenderer.SetVertexCount(0);
        //lineRenderer.useWorldSpace = true;
    }

    void Update()
    {
        if (Input.anyKey && SearchVariable.isGodFinger && timeDraw > 0)
        {
            lineRenderer.SetVertexCount(i + 1);
            Vector3 mPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 1f);
            lineRenderer.SetPosition(i, Camera.main.ScreenToWorldPoint(mPosition));
            i++;
            iDraw = true;
            timeDraw -= Time.deltaTime;
        }

        else
        {
            lineRenderer.SetVertexCount(0);
            i = 0;
            if (iDraw)
            {
                SearchVariable.isGodFinger = false;
                iDraw = false;
                timeDraw = 1f;
            }
        }
    }

    /*void Update()
    {
        if (Input.touchCount > 0 && SearchVariable.isGodFinger && timeDraw > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Moved)
            {
                lineRenderer.SetVertexCount(i + 1);
                Vector3 mPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 1f);
                lineRenderer.SetPosition(i, Camera.main.ScreenToWorldPoint(mPosition));
                i++;
                iDraw = true;
                timeDraw -= Time.deltaTime;
            }

            if (touch.phase == TouchPhase.Ended)
            {
                lineRenderer.SetVertexCount(0);
                i = 0;
                if (iDraw)
                {
                    SearchVariable.isGodFinger = false;
                    iDraw = false;
                    timeDraw = 1f;
                }
            }
        }
    }*/
}
