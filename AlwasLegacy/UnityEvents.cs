using System;
using HarmonyLib;
using Rewired.Libraries.SharpDX;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace AlwasLegacy;

[HarmonyPatch]
public partial class Plugin
{
    public void LateUpdate()
    {
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

        if (Camera.current != null)
        {
            var cameraScript = Camera.current.gameObject.GetComponentInChildren<CameraScript>();
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

        var stuff3 = GameObject.Find("Canvas/MoveCanvasAtStart/CutsceneBorders");
        if (stuff3 != null)
        {
            stuff3.SetActive(false);
        }

        var stuff4 = GameObject.Find("Canvas/MoveCanvasAtStart/InfoPopup");
        if (stuff4 != null)
        {
            if (Math.Abs(stuff4.transform.position.x - 1720) < 0.001)
            {
                var position = stuff4.transform.position;
                position = new Vector3(1620, position.y, position.z);
                stuff4.transform.position = position;
            }
        }

        var cutsceneBorders = FindObjectsOfType<CutsceneBorders>();
        foreach (var cutsceneBorder in cutsceneBorders)
        {
            cutsceneBorder.enabled = false;
        }

        if (!SceneManager.GetActiveScene().name.Contains("Main"))
        {
            if (Camera.current != null) Camera.current.backgroundColor = Color.black;
        }

        var canvasScalers = FindObjectsOfType<CanvasScaler>();
        foreach (var canvasScaler in canvasScalers)
        {
            canvasScaler.referenceResolution = new Vector2(640, 350);
            canvasScaler.screenMatchMode = CanvasScaler.ScreenMatchMode.Expand;
            canvasScaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        }
    }
}