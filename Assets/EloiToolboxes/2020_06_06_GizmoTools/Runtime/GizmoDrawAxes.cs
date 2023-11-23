using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GizmoDrawAxes : MonoBehaviour
{

    [SerializeField] GizmoDrawingDefault drawingParameters= new GizmoDrawingDefault();
    [Header("Lag Drawing")]
    public bool m_useLagDrawing;
    public float m_lagDrawingTime = 2f;
    public float m_lagDrawingTimeFrame = 0.1f;
    private float m_drawCountDown;
    void Update()
    {
        if (m_useLagDrawing)
        { 
            m_drawCountDown -= Time.deltaTime;
            if (m_drawCountDown < 0)
            {
                m_drawCountDown = m_lagDrawingTimeFrame;
                GizmoDrawer.DrawAxis(drawingParameters, m_lagDrawingTime);

            }
        }

        if (drawingParameters.m_axisDirection == null)
            return;
        GizmoDrawer.DrawAxis(drawingParameters , Time.deltaTime);
    }
    private void Reset()
    {
        drawingParameters.m_axisDirection = transform;
    }
}


