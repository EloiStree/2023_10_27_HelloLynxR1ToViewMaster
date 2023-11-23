using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ExostCarRCDefaultMono : MonoBehaviour
{

    public bool m_buttonLeftFrontOn;
    public bool m_buttonRightFrontOn;
    public bool m_buttonLeftBackOn;
    public bool m_buttonRightBackOn;


    public float m_fullMoveForwardPerSecond = 1;
    public float m_fullRotationAngleDegreePerSecond = 360;

    public Transform m_whatToMove;
    public Transform m_exostCarToMoveDirection;
    public Transform m_pivotLeftFront;
    public Transform m_pivotRightFront;
    public Transform m_pivotLeftBack;
    public Transform m_pivotRightBack;

    public CarRCWheelSpeedMono m_motorLeftFrontOn;
    public CarRCWheelSpeedMono m_motorRightFrontOn;
    public CarRCWheelSpeedMono m_motorLeftBackOn;
    public CarRCWheelSpeedMono m_motorRightBackOn;




    [System.Serializable]
    public class UnityEventBool : UnityEvent<bool> { }
    //public AnimationCurve m_motorStartResistance;

    public void SetWheelRotationSpeed(float rotationSpeed) {

        m_motorLeftFrontOn.m_rotationSpeedWhenOn = rotationSpeed;
        m_motorRightFrontOn.m_rotationSpeedWhenOn = rotationSpeed;
        m_motorLeftBackOn.m_rotationSpeedWhenOn = rotationSpeed;
        m_motorRightBackOn.m_rotationSpeedWhenOn = rotationSpeed;
    }

    public void SetWheelFront() => SetWheelState(1, 1, 1, 1);
    public void SetWheelBack() => SetWheelState(-1, -1, -1, -1);
    public void SetWheelStop() => SetWheelState(0,0,0,0);

    public void SetWheelState(
        short leftFront,
        short rightFront,
        short leftBack,
        short rightBack
        )
    {
        SetWheel(leftFront, m_motorLeftFrontOn);
        SetWheel(rightFront, m_motorRightFrontOn);
        SetWheel(leftBack, m_motorLeftBackOn);
        SetWheel(rightBack, m_motorRightBackOn);

    }

    public void SetWheel(short state, CarRCWheelSpeedMono wheel) {
        if (state == -1) {
            wheel.SetMotorInverse(true); wheel.SetMotorOn(true);
        }
        else if (state == 0) { 
        wheel.SetMotorInverse(false); wheel.SetMotorOn(false);
        } 
        else if (state == 1) {
        wheel.SetMotorInverse(false); wheel.SetMotorOn(true);
        }
    }

    void Update()
    {

        if (ImpossibleSituationOfButtons())
            return;


        m_fullMoveForwardPerSecond = Vector3.Distance(m_pivotLeftFront.position, m_pivotRightFront.position)*2*(float)Math.PI ;

        SetWheelState(0,0,0,0);
        if (MoveForward()) {
            SetWheelState(1, 1, 1, 1);
            m_whatToMove.Translate(m_exostCarToMoveDirection.forward * Time.deltaTime * m_fullMoveForwardPerSecond, Space.World); }
        else if (MoveBackward()) {

            SetWheelState(-1, -1, -1, -1);
            m_whatToMove.Translate(m_exostCarToMoveDirection.forward * Time.deltaTime * -m_fullMoveForwardPerSecond, Space.World); }
        else if (TurnLeftHard())
        {
            SetWheelState(-1, 1, -1, 1);
            m_whatToMove.Rotate(Vector3.up, Time.deltaTime * m_fullRotationAngleDegreePerSecond * 2, Space.Self);
        }
        else if (TurnRightHard())
        {
            SetWheelState(1, -1, 1, -1);
            m_whatToMove.Rotate(Vector3.up, Time.deltaTime * -m_fullRotationAngleDegreePerSecond*2, Space.Self);
        }
        
        else if (TurnLeftLight()) {

            SetWheelState(0, 1, 0, 1); 
            RotateAround(RotateType.LeftFront); }
        else if (TurnRightLight()) {

            SetWheelState(1, 0, 1, 0);
            RotateAround(RotateType.RightFront); }

        else if (TurnLeftLightBackward()) {

            SetWheelState(0, -1, 0, -1);
            RotateAround(RotateType.LeftBack); }
        else if (TurnRightLightBackward()) {

            SetWheelState(-1, 0, -1, 0);
            RotateAround(RotateType.RightBack); }

    }

    

  

    private bool MoveBackward()
    {

        
        return m_buttonLeftBackOn && m_buttonRightBackOn && !m_buttonLeftFrontOn && !m_buttonRightFrontOn;
    }

    private bool MoveForward()
    {
        return !m_buttonLeftBackOn && !m_buttonRightBackOn && m_buttonLeftFrontOn && m_buttonRightFrontOn;
    }
    private bool TurnRightHard()
    {

        return !m_buttonLeftBackOn && m_buttonRightBackOn && m_buttonLeftFrontOn && !m_buttonRightFrontOn;
    }
    private bool TurnLeftHard()
    {
        return m_buttonLeftBackOn && !m_buttonRightBackOn && !m_buttonLeftFrontOn && m_buttonRightFrontOn;
    }


    private bool TurnLeftLight()
    {

        return !m_buttonLeftBackOn && !m_buttonRightBackOn && !m_buttonLeftFrontOn && m_buttonRightFrontOn;
    }

    public enum RotateType { LeftFront, RightFront, LeftBack, RightBack}
    private void RotateAround(RotateType rotateType)
    {

        Transform whatToMove = m_whatToMove;
        Transform rotationPoint = null;
        bool inverseAngle = false;
        bool isBack = false;
        switch (rotateType)
        {
            case RotateType.LeftFront:
                rotationPoint = m_pivotLeftFront;
                break;
            case RotateType.RightFront:
                rotationPoint = m_pivotRightFront;
                inverseAngle = true;


                break;
            case RotateType.LeftBack:
                rotationPoint = m_pivotLeftBack;
                isBack = true;
                break;
            case RotateType.RightBack:
                rotationPoint = m_pivotRightBack;
                inverseAngle = true;
                isBack = true;
                break;
            default:
                break;
        }

        if (rotationPoint!=null) {

            // Calculate the relative position of the object to the rotation point.
            Vector3 relativePosition = m_whatToMove.position - rotationPoint.position;

            // Perform the rotation using Unity's built-in functions.
            m_whatToMove.RotateAround(rotationPoint.position, Vector3.up, (isBack?1:-1)*m_fullRotationAngleDegreePerSecond * Time.deltaTime * (inverseAngle?-1:1));

            // Translate the object back to its original position (if necessary).
            //m_whatToMove.position = rotationPoint.position + relativePosition;

        }
    }

    private bool TurnRightLight()
    {

        return !m_buttonLeftBackOn && !m_buttonRightBackOn && m_buttonLeftFrontOn && !m_buttonRightFrontOn;
    }

    private bool TurnRightLightBackward()
    {

        return m_buttonLeftBackOn && !m_buttonRightBackOn && !m_buttonLeftFrontOn && !m_buttonRightFrontOn;
    }

    private bool TurnLeftLightBackward()
    {

        
        return !m_buttonLeftBackOn && m_buttonRightBackOn && !m_buttonLeftFrontOn && !m_buttonRightFrontOn;
    }




    private bool ImpossibleSituationOfButtons()
    {
        return (m_buttonRightBackOn && m_buttonRightFrontOn) || (m_buttonLeftBackOn && m_buttonLeftFrontOn);
    }

    public void SetMotorLeftFront(bool isMotorOn)
    {
        m_buttonLeftFrontOn = isMotorOn; 
    }
    public void SetMotorRightFront(bool isMotorOn)
    {
       m_buttonRightFrontOn = isMotorOn;
    }
    public void SetMotorLeftBack(bool isMotorOn)
    {
        m_buttonLeftBackOn = isMotorOn;
    }
    public void SetMotorRightBack(bool isMotorOn)
    {
        m_buttonRightBackOn = isMotorOn;
    }
}
