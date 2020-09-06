using MelonLoader;
using System;
using System.Reflection;
using UnityEngine;

namespace Raicuparta.UnityScaleAdjuster
{
    public class UnityScaleAdjuster : MelonMod
    {
        const string CameraScaleArg = "cameraScale";
        const float ScaleUpMultiplier = 0.9f;
        const float ScaleDownMultiplier = 1.1f;
        Type CameraType;
        Type TransformType;
        Type Vector3Type;
        MethodInfo GetKeyDownMethod;
        MethodInfo Vector3MultiplyMethod;

        public override void OnApplicationStart()
        {
            base.OnApplicationStart();
            GetKeyDownMethod = GetInputType().GetMethod("GetKeyDown", new[] { typeof(string) });
            CameraType = GetUnityType("Camera");
            TransformType = GetUnityType("Transform");
            Vector3Type = GetUnityType("Vector3");
            Vector3MultiplyMethod = Vector3Type.GetMethod("op_Multiply", new[] { Vector3Type, typeof(float) });

        }

        public override void OnLevelWasLoaded(int level)
        {
            base.OnLevelWasLoaded(level);
            SetScaleToUser();
        }

        public override void OnUpdate()
        {
            base.OnUpdate();
            if (GetKeyDown("f5"))
            {
                SetScaleToUser();
            }
            if (GetKeyDown("f4"))
            {
                MultiplyCameraScale(ScaleUpMultiplier);
            }
            if (GetKeyDown("f3"))
            {
                MultiplyCameraScale(ScaleDownMultiplier);
            }
        }

        private void MultiplyCameraScale(float scale)
        {
            var mainCamera = GetMainCameraTransform();
            var currentScale = GetValue(TransformType, "localScale", mainCamera);
            var multipliedScale = MultiplyVector3(currentScale, scale);
            SetCameraScale(multipliedScale);
        }

        private float GetUserScale()
        {
            return GetCommandLineArgument(CameraScaleArg);
        }

        private void SetScaleToUser()
        {
            SetCameraScale(MultiplyVector3(GetValue(Vector3Type, "one"), GetUserScale()));
        }

        private object MultiplyVector3(object vector3, float multiplier)
        {
            return Vector3MultiplyMethod.Invoke(null, new object[] { vector3, multiplier });
        }

        private void SetCameraScale(object scale)
        {
            var mainCamera = GetMainCameraTransform();
            SetValue(TransformType, "localScale", scale, mainCamera);

            MelonLogger.Log($"Changed camera scale to {GetValue(Vector3Type, "x", scale)}");
        }

        private float GetCommandLineArgument(string name)
        {
            string[] args = System.Environment.GetCommandLineArgs();
            for (int i = 0; i < args.Length; i++)
            {
                if (args[i] == $"-{name}")
                {
                    return float.Parse(args[i + 1]);
                }
            }

            return 1;
        }

        private Type GetUnityType(string typeName, string moduleName = "CoreModule")
        {
            return Type.GetType($"UnityEngine.{typeName}, UnityEngine.{moduleName}, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null");
        }

        private Type GetInputType()
        {
            return GetUnityType("Input") ?? GetUnityType("Input", "InputLegacyModule");
        }

        private object GetValue(Type type, string propertyName, object instance = null)
        {
            var property = type.GetProperty(propertyName);
            if (property != null)
            {
                return property.GetValue(instance, null);
            }
            return type.GetField(propertyName).GetValue(instance);


        }

        private void SetValue(Type type, string propertyName, object value, object instance = null)
        {
            var property = type.GetProperty(propertyName);
            type.GetProperty(propertyName).SetValue(instance, value, null);
        }

        private bool GetKeyDown(string key)
        {
            return (bool)GetKeyDownMethod.Invoke(null, new[] { key });
        }

        private object GetMainCameraTransform()
        {
            var mainCamera = GetValue(CameraType, "main");
            return GetValue(CameraType, "transform", mainCamera);
        }
    }
}
