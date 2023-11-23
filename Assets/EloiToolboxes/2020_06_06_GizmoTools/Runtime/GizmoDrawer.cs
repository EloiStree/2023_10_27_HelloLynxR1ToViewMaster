
using UnityEngine;

[System.Serializable]
public class GizmoDrawingDefault
{
    public Transform m_axisDirection;
    public float m_rayDistance = 1f;
    public bool m_scaleAffectDistance = true;
    public bool m_useAxeX = true;
    public bool m_useAxeY = true;
    public bool m_useAxeZ = true;
}

public class GizmoDrawer
{

    public static void DrawAxis(GizmoDrawingDefault drawingInfo, float timeDisplay)
    {
        float scale = 1f;
        if (drawingInfo.m_useAxeX)
        {
            if (drawingInfo.m_scaleAffectDistance)
                scale = drawingInfo.m_axisDirection.lossyScale.x * drawingInfo.m_rayDistance;
            else scale = drawingInfo.m_rayDistance;
            Debug.DrawLine(drawingInfo.m_axisDirection.position, drawingInfo.m_axisDirection.position + drawingInfo.m_axisDirection.right * scale, Color.red, timeDisplay);
        }
        if (drawingInfo.m_useAxeY)
        {
            if (drawingInfo.m_scaleAffectDistance)
                scale = drawingInfo.m_axisDirection.lossyScale.y * drawingInfo.m_rayDistance;
            else scale = drawingInfo.m_rayDistance;
            Debug.DrawLine(drawingInfo.m_axisDirection.position, drawingInfo.m_axisDirection.position + drawingInfo.m_axisDirection.up * scale, Color.green, timeDisplay);
        }
        if (drawingInfo.m_useAxeZ)
        {
            if (drawingInfo.m_scaleAffectDistance)
                scale = drawingInfo.m_axisDirection.lossyScale.z * drawingInfo.m_rayDistance;
            else scale = drawingInfo.m_rayDistance;
            Debug.DrawLine(drawingInfo.m_axisDirection.position, drawingInfo.m_axisDirection.position + drawingInfo.m_axisDirection.forward * scale, Color.blue, timeDisplay);
        }
    }
}