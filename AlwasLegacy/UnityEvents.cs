using System;
using HarmonyLib;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace AlwasLegacy;

[HarmonyPatch]
public partial class Plugin
{
    public void LateUpdate()
    {
        var dialogUiIntro = GameObject.Find("Canvas/DialogGUI");
        if (dialogUiIntro != null)
        {
            dialogUiIntro.transform.localScale = new Vector3(_dialogScale.Value, _dialogScale.Value, _dialogScale.Value);
        }

        var dialogUI = GameObject.Find("Canvas/MoveCanvasAtStart/DialogGUI");
        if (dialogUI != null)
        {
            dialogUI.transform.localScale = new Vector3(_dialogScale.Value, _dialogScale.Value, _dialogScale.Value);
        }

        var itemPickup = GameObject.Find("Canvas/MoveCanvasAtStart/ItemPickup");
        if (itemPickup != null)
        {
            itemPickup.transform.localScale = new Vector3(_dialogScale.Value, _dialogScale.Value, _dialogScale.Value);
        }

        if (CameraScript.instance != null)
        {
            var cameraScript = CameraScript.instance;
            if (cameraScript != null)
            {
                //  _log.LogWarning("Found CameraScript!");
                var res = SettingsManager.instance.resolutionTexts[SystemSaveSettings.instance.resolutionIndex];
                var widthStr = res.Split('x')[0];
                var heightStr = res.Split('x')[1];
                var widthInt = int.Parse(widthStr);
                var heightInt = int.Parse(heightStr);
                const float ratio = 16f / 9f;
                var subValue = ratio * heightInt;
                var triggerValue = (widthInt - subValue) / 2f;
                // _log.LogWarning("TriggerValue: " + triggerValue);
                if (Math.Abs(cameraScript.playerScreenPos.x - triggerValue) < 25f)
                {
                    cameraScript.playerScreenPos.x = 0;
                }

                if (Math.Abs(cameraScript.playerScreenPos.x - (widthInt - triggerValue)) < 25f)
                {
                    cameraScript.playerScreenPos.x = widthInt;
                }
            }
        }

        //1633.734 720 -0.8627
        //1280 720 0

        var infoPopup = GameObject.Find("Canvas/MoveCanvasAtStart/InfoPopup");
        if (infoPopup != null)
        {
            if (Math.Abs(infoPopup.transform.position.x - 1720) < 1 || Math.Abs(infoPopup.transform.position.x - 1340) < 1)
            {
                var position = infoPopup.transform.position;
                position = new Vector3(1280, position.y, position.z);
                infoPopup.transform.position = position;
            }
        }

        var leftHud = GameObject.Find("Canvas/MoveCanvasAtStart/HudManager/ParentNode/Hud_Base");
        if (leftHud != null)
        {
            leftHud.transform.position = new Vector3(270f, 1330f, 0f);
        }

        var rightHud = GameObject.Find("Canvas/MoveCanvasAtStart/HudManager/ParentNode/HealthDots");
        if (rightHud != null)
        {
            rightHud.transform.position = new Vector3(2165f, 720f, 0f);
        }

        var settingsBg = GameObject.Find("Canvas/MoveCanvasAtStart/PauseNode/ParentNode/BG");
        if (settingsBg != null)
        {
            settingsBg.transform.localScale = new Vector3(200f, 1, 1);
        }

        var settingsVig = GameObject.Find("Canvas/MoveCanvasAtStart/PauseNode/ParentNode/Vignette");
        if (settingsVig != null)
        {
            settingsVig.transform.localScale = new Vector3(200f, 1, 1);
        }

        var settingsBack = GameObject.Find("Canvas/MoveCanvasAtStart/PauseNode/ParentNode/BottomRightPanel");

        if (settingsBack != null)
        {
            if (Math.Abs(settingsBack.transform.position.x - 1720) < 0.001)
            {
                var position = settingsBack.transform.position;
                position = new Vector3(2160, position.y, position.z);
                settingsBack.transform.position = position;
            }
        }


        var loadingScreenBackground = GameObject.Find("Canvas/ParentNode/LoadingScreen/BlackBG");
        if (loadingScreenBackground != null)
        {
            loadingScreenBackground.transform.localScale = new Vector3(200f, 1, 1);
        }

        var mainMenuSettingsHidden = GameObject.Find("Canvas/ParentNode/SettingsNode");
        if (mainMenuSettingsHidden != null)
        {
            if (Math.Abs(mainMenuSettingsHidden.transform.position.x - 4280) < 0.001)
            {
                var position = mainMenuSettingsHidden.transform.position;
                position = new Vector3(10000, position.y, position.z);
                mainMenuSettingsHidden.transform.position = position;
            }
        }

        var topBottomBorders = GameObject.Find("Canvas/MoveCanvasAtStart/Borders");
        if (topBottomBorders != null)
        {
            topBottomBorders.transform.localScale = new Vector3(200f, 1, 1);
        }

        if (!SceneManager.GetActiveScene().name.Contains("Main"))
        {
            if (Camera.main != null) Camera.main.backgroundColor = Color.black;
        }

        var canvasScalers = FindObjectsOfType<CanvasScaler>();
        foreach (var canvasScaler in canvasScalers)
        {
            // canvasScaler.referenceResolution = new Vector2(640, 350);
            canvasScaler.screenMatchMode = CanvasScaler.ScreenMatchMode.Expand;
            canvasScaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        }
    }
}