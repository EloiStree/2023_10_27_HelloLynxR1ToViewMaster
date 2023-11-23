/**
 * @file LynxCaptureAPI.cs
 * 
 * @author Geoffrey Marhuenda
 * 
 * @brief API for video capture features.
 */

using AOT;
using System;
using UnityEngine;
using static Lynx.LynxCaptureLibraryInterface;

namespace Lynx
{
    public class LynxCaptureAPI
    {
        public enum ESensorType : byte
        {
            RGB,
            TRACKING,
            HANDTRACKING
        }

        // Current status of the capture
        public static bool IsCaptureRunning { get; private set; } = false;

        public delegate void OnFrameDelegate(LynxFrameInfo frameInfo);

        // To subscribe on Video capture callback
        public static OnFrameDelegate onRGBFrames = null;
        public static OnFrameDelegate onTrackingFrames = null;
        public static OnFrameDelegate onHandtrackingFrames = null;

        /// <summary>
        /// Initialize and start camera capture.
        /// </summary>
        /// <param name="maxFPS">Number of FPS to target.</param>
        /// <returns>False and an error log if it fails.</returns>
        public static bool StartCapture(ESensorType sensorType, int maxFPS = 30)
        {
#if !UNITY_EDITOR && UNITY_ANDROID
            LynxCaptureLibraryInterface.SetMaxFPS((byte)sensorType, maxFPS);
            if(sensorType == ESensorType.TRACKING)
                LynxCaptureLibraryInterface.SetCallback((byte)sensorType, MonochromeFramesCallback);

            else if (sensorType == ESensorType.HANDTRACKING)
                LynxCaptureLibraryInterface.SetCallback((byte)sensorType, HandtrackingFramesCallback);

            else
                LynxCaptureLibraryInterface.SetCallback((byte)sensorType, RGBFramesCallback);


            if (!LynxCaptureLibraryInterface.InitializeQXR())
            {
                Debug.LogError("Cannot initialize Video Capture");
                IsCaptureRunning = false;
                return false;
            }

            if(!LynxCaptureLibraryInterface.StartCamera((byte)sensorType))
            {
                Debug.LogError("Cannot start camera");
                IsCaptureRunning = false;
                return false;
            }

            IsCaptureRunning = true;

            LynxCaptureLibraryInterface.IntrinsicData intrinsic;
            LynxCaptureLibraryInterface.ExtrinsicData extrinsic;
            if(!ReadCameraParameters(sensorType, out intrinsic, out extrinsic))
                Debug.LogError("FAILED to read intrinsic data");

            LynxOpenCV.LynxCameraInitConfiguration(ref intrinsic);

            return true;
#else
            Debug.LogWarning("Cannot start capture in Editor");
            return false;
#endif
        }

        /// <summary>
        /// Stop opened camera.
        /// </summary>
        public static void StopCapture(ESensorType sensorType)
        {
#if !UNITY_EDITOR && UNITY_ANDROID
            LynxCaptureLibraryInterface.StopCamera((byte)sensorType);
#endif
            IsCaptureRunning = false;
        }

        /// <summary>
        /// Read first camera parameters and return the matching structure.
        /// </summary>
        /// <returns>Intrinsic parameters or null if it failed.</returns>
        public static bool ReadCameraParameters(ESensorType sensorType, out LynxCaptureLibraryInterface.IntrinsicData intrinsic, out LynxCaptureLibraryInterface.ExtrinsicData extrinsic)
        {
#if !UNITY_EDITOR

            if (!LynxCaptureLibraryInterface.ReadCameraParameters((byte)sensorType, 0, out intrinsic, out extrinsic))
            {
                Debug.LogError("Cannot read camera parameters (ensure camera is running).");
                return false;
            }
#else
            intrinsic = new LynxCaptureLibraryInterface.IntrinsicData();
            extrinsic = new LynxCaptureLibraryInterface.ExtrinsicData();
#endif

            return true;
        }

        /// <summary>
        /// Call back event when a frame is captured from the camera.
        /// </summary>
        /// <param name="width">Frame width</param>
        /// <param name="height">Frame height</param>
        /// <param name="data">Frame data buffer</param>
        [MonoPInvokeCallback(typeof(LynxCaptureLibraryInterface.frame_callback_function))]
        private static void RGBFramesCallback(LynxFrameInfo frameInfo)
        {
            onRGBFrames?.Invoke(frameInfo);
        }

        /// <summary>
        /// Call back event when a frame is captured from the camera.
        /// </summary>
        /// <param name="width">Frame width</param>
        /// <param name="height">Frame height</param>
        /// <param name="dataLeft">Frame data buffer for left eye</param>
        /// <param name="dataRight">Frame data buffer for right eye</param>
        [MonoPInvokeCallback(typeof(LynxCaptureLibraryInterface.frame_callback_function))]
        private static void MonochromeFramesCallback(LynxFrameInfo frameInfo)
        {
            onTrackingFrames?.Invoke(frameInfo);
        }

        /// <summary>
        /// Call back event when a frame is captured from the camera.
        /// </summary>
        /// <param name="width">Frame width</param>
        /// <param name="height">Frame height</param>
        /// <param name="dataLeft">Frame data buffer for left eye</param>
        /// <param name="dataRight">Frame data buffer for right eye</param>
        [MonoPInvokeCallback(typeof(LynxCaptureLibraryInterface.frame_callback_function))]
        private static void HandtrackingFramesCallback(LynxFrameInfo frameInfo)
        {
            onHandtrackingFrames?.Invoke(frameInfo);
        }

        public static void StopAllCameras()
        {
#if !UNITY_EDITOR && UNITY_ANDROID
            LynxCaptureLibraryInterface.StopAllCameras();
#endif
        }
    }
}