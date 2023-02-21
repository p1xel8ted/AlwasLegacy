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
                if (Math.Abs(cameraScript.playerScreenPos.x - triggerValue) <= 3f)
                {
                    cameraScript.playerScreenPos.x = 0;
                }

                if (Math.Abs(cameraScript.playerScreenPos.x - (widthInt - triggerValue)) <= 3f)
                {
                    cameraScript.playerScreenPos.x = widthInt;
                }
            }
        }


        var infoPopup = GameObject.Find("Canvas/MoveCanvasAtStart/InfoPopup");
        if (infoPopup != null)
        {
            if (infoPopup.transform.localPosition.x == 0)
            {
                var position = infoPopup.transform.localPosition;
                position = new Vector3(-110f, position.y, position.z);
                infoPopup.transform.localPosition = position;
            }
        }
        
        var settingsOpenBackButton = GameObject.Find("Canvas/ParentNode/SettingsNode/BottomRightBG");
        if (settingsOpenBackButton != null)
        {
            var buttonText = GameObject.Find("Canvas/ParentNode/SettingsNode/ExitText");

            if (buttonText != null)
            {
                buttonText.transform.parent = settingsOpenBackButton.transform;
            }

            var localPosition = settingsOpenBackButton.transform.localPosition;
            if (Math.Abs(localPosition.x - -252) < 0.001f)
            {
                localPosition = new Vector3(-365f, localPosition.y, localPosition.z);
                settingsOpenBackButton.transform.localPosition = localPosition;
            }
        }
        

        var leftHud = GameObject.Find("Canvas/MoveCanvasAtStart/HudManager/ParentNode/Hud_Base");
        if (leftHud != null)
        {
            var block = GameObject.Find("Canvas/MoveCanvasAtStart/HudManager/ParentNode/Hud_icon_Block");
            var bubble = GameObject.Find("Canvas/MoveCanvasAtStart/HudManager/ParentNode/Hud_icon_Bubble");
            var light = GameObject.Find("Canvas/MoveCanvasAtStart/HudManager/ParentNode/Hud_icon_Light");

            if (block != null)
            {
                block.transform.parent = leftHud.transform;
            }

            if (bubble != null)
            {
                bubble.transform.parent = leftHud.transform;
            }

            if (light != null)
            {
                light.transform.parent = leftHud.transform;
            }

            var localPosition = leftHud.transform.localPosition;
            localPosition = new Vector3(-365f, localPosition.y, localPosition.z);
            leftHud.transform.localPosition = localPosition;
        }

        var rightHud = GameObject.Find("Canvas/MoveCanvasAtStart/HudManager/ParentNode/HealthDots");
        if (rightHud != null)
        {
            var localPosition = rightHud.transform.localPosition;
            localPosition = new Vector3(114f, localPosition.y, localPosition.z);
            rightHud.transform.localPosition = localPosition;
        }


        var settingsBack = GameObject.Find("Canvas/MoveCanvasAtStart/PauseNode/ParentNode/BottomRightPanel");

        if (settingsBack != null)
        {
            if (settingsBack.transform.localPosition.x == 0)
            {
                var position = settingsBack.transform.localPosition;
                position = new Vector3(110, position.y, position.z);
                settingsBack.transform.localPosition = position;
            }
        }

        var mainMenuSettingsHidden = GameObject.Find("Canvas/ParentNode/SettingsNode");
        if (mainMenuSettingsHidden != null)
        {
            if (Math.Abs(mainMenuSettingsHidden.transform.localPosition.x - 640) < 0.001)
            {
                var position = mainMenuSettingsHidden.transform.localPosition;
                position = new Vector3(10000, position.y, position.z);
                mainMenuSettingsHidden.transform.localPosition = position;
            }
        }


        var settingsBg = GameObject.Find("Canvas/MoveCanvasAtStart/PauseNode/ParentNode/BG");
        if (settingsBg != null)
        {
            settingsBg.transform.localScale = new Vector3(2000f, 1, 1);
        }

        var settingsVig = GameObject.Find("Canvas/MoveCanvasAtStart/PauseNode/ParentNode/Vignette");
        if (settingsVig != null)
        {
            settingsVig.transform.localScale = new Vector3(2000f, 1, 1);
        }

        var loadingScreenBackground = GameObject.Find("Canvas/ParentNode/LoadingScreen/BlackBG");
        if (loadingScreenBackground != null)
        {
            loadingScreenBackground.transform.localScale = new Vector3(2000f, 1, 1);
        }

        var topBottomBorders = GameObject.Find("Canvas/MoveCanvasAtStart/Borders");
        if (topBottomBorders != null)
        {
            topBottomBorders.transform.localScale = new Vector3(2000f, 1, 1);
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