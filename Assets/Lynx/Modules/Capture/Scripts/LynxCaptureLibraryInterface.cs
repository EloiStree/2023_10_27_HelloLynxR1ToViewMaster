/**
 * @file LynxCaptureLibraryInterface.cs
 * 
 * @author Geoffrey Marhuenda
 * 
 * @brief Entry points for Lynx Video Capture library.
 */

using System;
using System.Runtime.InteropServices;

namespace Lynx
{
    public class LynxCaptureLibraryInterface
    {
        #region LIBRARY ENTRY POINTS
        private const string LIB_NAME = "LynxCapture";

        [DllImport(LIB_NAME)]
        public static extern bool InitializeQXR();

        [DllImport(LIB_NAME)]
        public static extern bool StartCamera(byte sensorType);

        [DllImport(LIB_NAME)]
        public static extern void StopCamera(byte sensorType);

        [DllImport(LIB_NAME)]
        public static extern void StopAllCameras();

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void frame_callback_function(LynxFrameInfo frameInfo);

        [DllImport(LIB_NAME)]
        public static extern void SetCallback(byte sensorType, frame_callback_function callback);


        [DllImport(LIB_NAME)]
        public static extern bool ReadCameraParameters(byte sensorType, int idx, out IntrinsicData intrinsics, out ExtrinsicData extrinsics);


        [DllImport(LIB_NAME)]
        public static extern void SetMaxFPS(byte sensorType, int fps);
        #endregion

        #region QXR STRUCTS
        public enum XrDistortionModelQTI
        {
            XR_DISTORTION_MODEL_QTI_LINEAR,                       /**< Only the linear projection parameters (principal_point and focal_length) are used. */
            XR_DISTORTION_MODEL_QTI_RADIAL_2_PARAMS,              /**< A radial distortion model using 2 parameters of radial_distortion to form a second-order polynomial. */
            XR_DISTORTION_MODEL_QTI_RADIAL_3_PARAMS,              /**< A radial distortion model using 3 parameters of radial_distortion to form a third-order polynomial. */
            XR_DISTORTION_MODEL_QTI_RADIAL_6_PARAMS,              /**< A radial distortion model using 6 parameters of radial_distortion to form a rational function. */
            XR_DISTORTION_MODEL_QTI_FISHEYE_1_PARAM,              /**< A radial distortion model using the first parameter of radial_distortion, combining arctan with a linear model. */
            XR_DISTORTION_MODEL_QTI_FISHEYE_4_PARAMS,             /**< A radial distortion model using 4 parameters of radial_distortion, combining arctan with a forth-order polynomial. */
            XR_DISTORTION_MODEL_QTI_MAX_ENUM = 0x7fffffff
        }


        [StructLayout(LayoutKind.Sequential)]
        public struct IntrinsicData
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public Int32[] size;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public double[] principalPoint;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public double[] focalLength;
            public double skew;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 7)]
            public double[] radialDistortion;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public double[] tangentialDistortion;
            public XrDistortionModelQTI distortionModel;
        };


        [StructLayout(LayoutKind.Sequential)]
        public struct ExtrinsicData
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public double[] orientation;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
            public double[] position;
        };

        [StructLayout(LayoutKind.Sequential)]
        public struct LynxFrameInfo
        {
            public UInt32 version;
            public UInt32 width;
            public UInt32 height;
            public UInt32 bufferSize;
            public IntPtr leftEyeBuffer;
            public IntPtr rightEyeBuffer;
        };
        #endregion
    }
}