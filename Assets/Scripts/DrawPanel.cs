using System;
using System.Linq;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class DrawPanel : MonoBehaviour
{

    private RectTransform _rectTransform;
    private bool pointerIsOn = false;

    [SerializeField] private LinesRender _linesRender;

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
    }

    public static bool PointerIsOverPanel(Vector3 screenPos)
    {
        var hitObject = UIRaycast(ScreenPosToPointerData(screenPos));
        return hitObject != null && hitObject.layer == LayerMask.NameToLayer("DrawPanel");
    }

    static GameObject UIRaycast(PointerEventData pointerData)
    {
        var results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerData, results);
        return results.Count < 1 ? null : results[0].gameObject;
    }

    static PointerEventData ScreenPosToPointerData(Vector3 screenPos)
       => new(EventSystem.current) { position = screenPos };

    void Update()
    {
        if (Input.GetButton("Fire1"))
        {
            var screenPos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
            var pointerIsOverPanel = PointerIsOverPanel(screenPos);
            if (pointerIsOverPanel)
            {

                pointerIsOn = true;
                _linesRender.DrawLine();
            }
            else
            {
                if (pointerIsOn)
                {
                    pointerIsOn = false;
                    DrawIsFinished();
                }
            }
        }
        else if ((Input.GetButtonUp("Fire1")) && pointerIsOn)
        {
            //Debug.Log("pointer is up");
            pointerIsOn = false;
            DrawIsFinished();
        }
    }

    private void DrawIsFinished()
    {
        //if (OnDrawingFinished != null) OnDrawingFinished.Invoke(points);
        _linesRender.EndOfRender();
        //points.Clear();
    }
}
