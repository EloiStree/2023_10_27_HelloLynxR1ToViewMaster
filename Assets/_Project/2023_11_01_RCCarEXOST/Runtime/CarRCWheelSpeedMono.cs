using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarRCWheelSpeedMono : MonoBehaviour
{
    public bool m_isWheelMotorOn;
    public bool m_isMotorRotatingInverse;
    public float m_rotationSpeedWhenOn = 180;
    public Transform m_whatToRotate;
    public Vector3 m_axisToRotate = Vector3.right;


    public void SetMotorOn(bool isOn) => m_isWheelMotorOn = isOn;
    public void SetMotorInverse(bool isInverse) => m_isMotorRotatingInverse = isInverse;


    void Update()
    {
        if (m_isWheelMotorOn) {
            m_whatToRotate.Rotate(m_axisToRotate, (m_isMotorRotatingInverse?-1:1)* m_rotationSpeedWhenOn * Time.deltaTime, Space.Self);
        }
        
    }
}
