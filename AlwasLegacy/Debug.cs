// using HarmonyLib;
// using Rewired.Data.Mapping;
// using UnityEngine;
// using UnityEngine.SceneManagement;
//
// namespace AlwasLegacy;
//
// [HarmonyPatch]
// public partial class Plugin
// {
//     [HarmonyPrefix]
//     [HarmonyPatch(typeof(TransitionManager), nameof(TransitionManager.Start))]
//     public static bool TransitionManager_Start(ref TransitionManager __instance)
//     {
//         __instance.plMove = pl_Movement.instance;
//         __instance.myCamera = CameraScript.instance;
//         __instance.plStartPos = new Vector2(__instance.plMove.gameObject.transform.position.x, __instance.plMove.gameObject.transform.position.y);
//         __instance.camStartPos = new Vector3(__instance.myCamera.transform.position.x, __instance.myCamera.transform.position.y, -10f);
//         Scene activeScene = SceneManager.GetActiveScene();
//         if (SaveManager.instance.nextArea != "")
//         {
//             _log.LogWarning("Entering new area");
//             int nextAreaIndex = SaveManager.instance.nextAreaIndex;
//             int num = __instance.entrances[nextAreaIndex].startDirection;
//             if (Mathf.Abs(num) != 1)
//             {
//                 num = 1;
//             }
//
//             __instance.plStartPos = new Vector2(__instance.entrances[nextAreaIndex].playerPosition.x, __instance.entrances[nextAreaIndex].playerPosition.y);
//             __instance.camStartPos = new Vector3(__instance.entrances[nextAreaIndex].cameraPosition.x, __instance.entrances[nextAreaIndex].cameraPosition.y, -10f);
//             pl_Movement.instance.gameObject.transform.localScale = new Vector3((float) num, 1f, 1f);
//             if (CameraScript.instance.parallaxObject != null)
//             {
//                 CameraScript.instance.parallaxObject.transform.position = new Vector2(__instance.entrances[nextAreaIndex].parallaxPosition.x, __instance.entrances[nextAreaIndex].parallaxPosition.y);
//                 CameraScript.instance.parStart = new Vector2(__instance.entrances[nextAreaIndex].parallaxPosition.x, __instance.entrances[nextAreaIndex].parallaxPosition.y);
//                 CameraScript.instance.parEnd = new Vector2(__instance.entrances[nextAreaIndex].parallaxPosition.x, __instance.entrances[nextAreaIndex].parallaxPosition.y);
//             }
//
//             if (CameraScript.instance.secondParallaxObject != null)
//             {
//                 CameraScript.instance.secondParallaxObject.transform.position = new Vector2(__instance.entrances[nextAreaIndex].secondParallaxPosition.x, __instance.entrances[nextAreaIndex].secondParallaxPosition.y);
//                 CameraScript.instance.secondParStart = new Vector2(__instance.entrances[nextAreaIndex].secondParallaxPosition.x, __instance.entrances[nextAreaIndex].secondParallaxPosition.y);
//                 CameraScript.instance.secondParEnd = new Vector2(__instance.entrances[nextAreaIndex].secondParallaxPosition.x, __instance.entrances[nextAreaIndex].secondParallaxPosition.y);
//             }
//
//             if (CameraScript.instance.thirdParallaxObject != null)
//             {
//                 CameraScript.instance.thirdParallaxObject.transform.position = new Vector2(__instance.entrances[nextAreaIndex].thirdParallaxPosition.x, __instance.entrances[nextAreaIndex].thirdParallaxPosition.y);
//                 CameraScript.instance.thirdParStart = new Vector2(__instance.entrances[nextAreaIndex].thirdParallaxPosition.x, __instance.entrances[nextAreaIndex].thirdParallaxPosition.y);
//                 CameraScript.instance.thirdParEnd = new Vector2(__instance.entrances[nextAreaIndex].thirdParallaxPosition.x, __instance.entrances[nextAreaIndex].thirdParallaxPosition.y);
//             }
//
//             if (CameraScript.instance.fourthParallaxObject != null)
//             {
//                 CameraScript.instance.fourthParallaxObject.transform.position = new Vector2(__instance.entrances[nextAreaIndex].fourthParallaxPosition.x, __instance.entrances[nextAreaIndex].fourthParallaxPosition.y);
//                 CameraScript.instance.fourthParStart = new Vector2(__instance.entrances[nextAreaIndex].fourthParallaxPosition.x, __instance.entrances[nextAreaIndex].fourthParallaxPosition.y);
//                 CameraScript.instance.fourthParEnd = new Vector2(__instance.entrances[nextAreaIndex].fourthParallaxPosition.x, __instance.entrances[nextAreaIndex].fourthParallaxPosition.y);
//             }
//
//             if (CameraScript.instance.fiveParallaxObject != null)
//             {
//                 CameraScript.instance.fiveParallaxObject.transform.position = new Vector2(__instance.entrances[nextAreaIndex].fifthParallaxPosition.x, __instance.entrances[nextAreaIndex].fifthParallaxPosition.y);
//                 CameraScript.instance.fiveParStart = new Vector2(__instance.entrances[nextAreaIndex].fifthParallaxPosition.x, __instance.entrances[nextAreaIndex].fifthParallaxPosition.y);
//                 CameraScript.instance.fiveParEnd = new Vector2(__instance.entrances[nextAreaIndex].fifthParallaxPosition.x, __instance.entrances[nextAreaIndex].fifthParallaxPosition.y);
//             }
//
//             if (__instance.entrances[nextAreaIndex].saveGameWhenEntering)
//             {
//                 __instance.StartCoroutine(__instance.SaveOnEntrance());
//             }
//
//             SaveManager.instance.nextArea = "";
//             SaveManager.instance.nextAreaIndex = 0;
//         }
//         else
//         {
//             if (SystemSaveSettings.instance.assistModeRespawn == 1 && SaveManager.instance.assistArea != "")
//             {
//                 _log.LogWarning("assist mode spawn");
//                 __instance.plStartPos = new Vector2(SaveManager.instance.assistPlayerStartX, SaveManager.instance.assistPlayerStartY);
//                 __instance.camStartPos = new Vector2(SaveManager.instance.assistCameraStartX, SaveManager.instance.assistCameraStartY);
//               //  __instance.Invoke("DisplayDeathCount", 1f);
//                 __instance.DisplayDeathCount();
//                 if (SaveManager.instance.assistGravity == 1)
//                 {
//                     pl_Movement.instance.upsideDown = true;
//                 }
//
//                 if (SaveManager.instance.assistClimbing)
//                 {
//                     pl_Movement.instance.StartCoroutine(pl_Movement.instance.DelayedPutZoeOnLadder());
//                 }
//
//                 if (SaveManager.instance.assistBubble)
//                 {
//                     pl_AttackMagic.instance.SpawnAssistBubble();
//                 }
//
//                 if (SaveManager.instance.assistBlockWaterUpgrade)
//                 {
//                     _log.LogWarning("Spawned assist block");
//                     pl_AttackMagic.instance.SpawnAssistBlock();
//                 }
//
//                 if (SaveManager.instance.assistDirection != 0)
//                 {
//                     pl_Movement.instance.gameObject.transform.localScale = new Vector3((float) SaveManager.instance.assistDirection, 1f, 1f);
//                 }
//
//                 if (CameraScript.instance.parallaxObject != null)
//                 {
//                     CameraScript.instance.parallaxObject.transform.position = new Vector2(SaveManager.instance.assistParallaxX, SaveManager.instance.assistParallaxY);
//                     CameraScript.instance.parStart = new Vector2(SaveManager.instance.assistParallaxX, SaveManager.instance.assistParallaxY);
//                     CameraScript.instance.parEnd = new Vector2(SaveManager.instance.assistParallaxX, SaveManager.instance.assistParallaxY);
//                 }
//
//                 if (CameraScript.instance.secondParallaxObject != null)
//                 {
//                     CameraScript.instance.secondParallaxObject.transform.position = new Vector2(SaveManager.instance.assistSecondParallaxX, SaveManager.instance.assistSecondParallaxY);
//                     CameraScript.instance.secondParStart = new Vector2(SaveManager.instance.assistSecondParallaxX, SaveManager.instance.assistSecondParallaxY);
//                     CameraScript.instance.secondParEnd = new Vector2(SaveManager.instance.assistSecondParallaxX, SaveManager.instance.assistSecondParallaxY);
//                 }
//
//                 if (CameraScript.instance.thirdParallaxObject != null)
//                 {
//                     CameraScript.instance.thirdParallaxObject.transform.position = new Vector2(SaveManager.instance.assistThirdParallaxX, SaveManager.instance.assistThirdParallaxY);
//                     CameraScript.instance.thirdParStart = new Vector2(SaveManager.instance.assistThirdParallaxX, SaveManager.instance.assistThirdParallaxY);
//                     CameraScript.instance.thirdParEnd = new Vector2(SaveManager.instance.assistThirdParallaxX, SaveManager.instance.assistThirdParallaxY);
//                 }
//
//                 if (CameraScript.instance.fourthParallaxObject != null)
//                 {
//                     CameraScript.instance.fourthParallaxObject.transform.position = new Vector2(SaveManager.instance.assistFourthParallaxX, SaveManager.instance.assistFourthParallaxY);
//                     CameraScript.instance.fourthParStart = new Vector2(SaveManager.instance.assistFourthParallaxX, SaveManager.instance.assistFourthParallaxY);
//                     CameraScript.instance.fourthParEnd = new Vector2(SaveManager.instance.assistFourthParallaxX, SaveManager.instance.assistFourthParallaxY);
//                 }
//
//                 if (CameraScript.instance.fiveParallaxObject != null)
//                 {
//                     CameraScript.instance.fiveParallaxObject.transform.position = new Vector2(SaveManager.instance.assistFifthParallaxX, SaveManager.instance.assistFifthParallaxY);
//                     CameraScript.instance.fiveParStart = new Vector2(SaveManager.instance.assistFifthParallaxX, SaveManager.instance.assistFifthParallaxY);
//                     CameraScript.instance.fiveParEnd = new Vector2(SaveManager.instance.assistFifthParallaxX, SaveManager.instance.assistFifthParallaxY);
//                 }
//             }
//             else if (SaveManager.instance.cp_area != "" && SaveManager.instance.cp_area == activeScene.name)
//             {
//                 _log.LogWarning("started by a checkpoint");
//                 __instance.plStartPos = new Vector2(SaveManager.instance.playerStartX - 2f, SaveManager.instance.playerStartY);
//                 __instance.camStartPos = new Vector3(SaveManager.instance.cameraStartX, SaveManager.instance.cameraStartY, -10f);
//                 if (SaveManager.instance.playerDirection != 0)
//                 {
//                     pl_Movement.instance.gameObject.transform.localScale = new Vector3((float) SaveManager.instance.playerDirection, 1f, 1f);
//                 }
//
//                 if (CameraScript.instance.parallaxObject != null)
//                 {
//                     CameraScript.instance.parallaxObject.transform.position = new Vector2(SaveManager.instance.parallaxX, SaveManager.instance.parallaxY);
//                     CameraScript.instance.parStart = new Vector2(SaveManager.instance.parallaxX, SaveManager.instance.parallaxY);
//                     CameraScript.instance.parEnd = new Vector2(SaveManager.instance.parallaxX, SaveManager.instance.parallaxY);
//                 }
//
//                 if (CameraScript.instance.secondParallaxObject != null)
//                 {
//                     CameraScript.instance.secondParallaxObject.transform.position = new Vector2(SaveManager.instance.secondParallaxX, SaveManager.instance.secondParallaxY);
//                     CameraScript.instance.secondParStart = new Vector2(SaveManager.instance.secondParallaxX, SaveManager.instance.secondParallaxY);
//                     CameraScript.instance.secondParEnd = new Vector2(SaveManager.instance.secondParallaxX, SaveManager.instance.secondParallaxY);
//                 }
//
//                 if (CameraScript.instance.thirdParallaxObject != null)
//                 {
//                     CameraScript.instance.thirdParallaxObject.transform.position = new Vector2(SaveManager.instance.thirdParallaxX, SaveManager.instance.thirdParallaxY);
//                     CameraScript.instance.thirdParStart = new Vector2(SaveManager.instance.thirdParallaxX, SaveManager.instance.thirdParallaxY);
//                     CameraScript.instance.thirdParEnd = new Vector2(SaveManager.instance.thirdParallaxX, SaveManager.instance.thirdParallaxY);
//                 }
//
//                 if (CameraScript.instance.fourthParallaxObject != null)
//                 {
//                     CameraScript.instance.fourthParallaxObject.transform.position = new Vector2(SaveManager.instance.fourthParallaxX, SaveManager.instance.fourthParallaxY);
//                     CameraScript.instance.fourthParStart = new Vector2(SaveManager.instance.fourthParallaxX, SaveManager.instance.fourthParallaxY);
//                     CameraScript.instance.fourthParEnd = new Vector2(SaveManager.instance.fourthParallaxX, SaveManager.instance.fourthParallaxY);
//                 }
//
//                 if (CameraScript.instance.fiveParallaxObject != null)
//                 {
//                     CameraScript.instance.fiveParallaxObject.transform.position = new Vector2(SaveManager.instance.fifthParallaxX, SaveManager.instance.fifthParallaxY);
//                     CameraScript.instance.fiveParStart = new Vector2(SaveManager.instance.fifthParallaxX, SaveManager.instance.fifthParallaxY);
//                     CameraScript.instance.fiveParEnd = new Vector2(SaveManager.instance.fifthParallaxX, SaveManager.instance.fifthParallaxY);
//                 }
//
//                 if (EnterAreaScript.instance != null)
//                 {
//                     EnterAreaScript.instance.parentNode.SetActive(false);
//                 }
//
//                 if (SaveManager.instance.deathCount == 2)
//                 {
//                     AssistModePopup.instance.ShowPopup();
//                 }
//
//                 if (SaveManager.instance.warpArea == "")
//                 {
//                     __instance.DisplayDeathCount();
//                 }
//
//                 HudManager.instance.UpdateHealth();
//             }
//
//             if (SaveManager.instance.warpArea != "")
//             {
//                 _log.LogWarning("Warped");
//                 __instance.plStartPos = new Vector2(SaveManager.instance.playerStartX, SaveManager.instance.playerStartY);
//                 __instance.camStartPos = new Vector3(SaveManager.instance.cameraStartX, SaveManager.instance.cameraStartY, -10f);
//                 if (CameraScript.instance.parallaxObject != null)
//                 {
//                     CameraScript.instance.parallaxObject.transform.position = new Vector2(SaveManager.instance.parallaxX, SaveManager.instance.parallaxY);
//                     CameraScript.instance.parStart = new Vector2(SaveManager.instance.parallaxX, SaveManager.instance.parallaxY);
//                     CameraScript.instance.parEnd = new Vector2(SaveManager.instance.parallaxX, SaveManager.instance.parallaxY);
//                 }
//
//                 if (CameraScript.instance.secondParallaxObject != null)
//                 {
//                     CameraScript.instance.secondParallaxObject.transform.position = new Vector2(SaveManager.instance.secondParallaxX, SaveManager.instance.secondParallaxY);
//                     CameraScript.instance.secondParStart = new Vector2(SaveManager.instance.secondParallaxX, SaveManager.instance.secondParallaxY);
//                     CameraScript.instance.secondParEnd = new Vector2(SaveManager.instance.secondParallaxX, SaveManager.instance.secondParallaxY);
//                 }
//
//                 if (CameraScript.instance.thirdParallaxObject != null)
//                 {
//                     CameraScript.instance.thirdParallaxObject.transform.position = new Vector2(SaveManager.instance.thirdParallaxX, SaveManager.instance.thirdParallaxY);
//                     CameraScript.instance.thirdParStart = new Vector2(SaveManager.instance.thirdParallaxX, SaveManager.instance.thirdParallaxY);
//                     CameraScript.instance.thirdParEnd = new Vector2(SaveManager.instance.thirdParallaxX, SaveManager.instance.thirdParallaxY);
//                 }
//
//                 if (CameraScript.instance.fourthParallaxObject != null)
//                 {
//                     CameraScript.instance.fourthParallaxObject.transform.position = new Vector2(SaveManager.instance.fourthParallaxX, SaveManager.instance.fourthParallaxY);
//                     CameraScript.instance.fourthParStart = new Vector2(SaveManager.instance.fourthParallaxX, SaveManager.instance.fourthParallaxY);
//                     CameraScript.instance.fourthParEnd = new Vector2(SaveManager.instance.fourthParallaxX, SaveManager.instance.fourthParallaxY);
//                 }
//
//                 if (CameraScript.instance.fiveParallaxObject != null)
//                 {
//                     CameraScript.instance.fiveParallaxObject.transform.position = new Vector2(SaveManager.instance.fifthParallaxX, SaveManager.instance.fifthParallaxY);
//                     CameraScript.instance.fiveParStart = new Vector2(SaveManager.instance.fifthParallaxX, SaveManager.instance.fifthParallaxY);
//                     CameraScript.instance.fiveParEnd = new Vector2(SaveManager.instance.fifthParallaxX, SaveManager.instance.fifthParallaxY);
//                 }
//
//                 SaveManager.instance.warpArea = "";
//             }
//         }
//
//         __instance.plMove.transform.position = __instance.plStartPos;
//         __instance.myCamera.InstantlyMoveCamera(__instance.camStartPos.x, __instance.camStartPos.y);
//         if (SaveManager.instance.assistSceneBeforeTransition != activeScene.name)
//         {
//             CameraScript.instance.SaveAssistVariables(__instance.plStartPos);
//             SaveManager.instance.assistSceneBeforeTransition = activeScene.name;
//         }
//
//         __instance.plMove.GetComponent<Animator>().SetBool("Grounded", true);
//         return false;
//     }
// }