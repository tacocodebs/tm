using BS_Utils.Utilities;
using Harmony;
using IPA;
using System;
using System.Collections;
using System.Linq;
using System.Reflection;
using BeatSaberMarkupLanguage;
using BeatSaberMarkupLanguage.Attributes;
using BeatSaberMarkupLanguage.ViewControllers;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using IPALogger = IPA.Logging.Logger;
using Networking;
using IPA.Config;

namespace BSPlugin1
{
    public class Plugin : IBeatSaberPlugin
    {
        internal string SongCoreHarmonyId;
        public static Config config;
        internal static object Name;

        public void Init(IPALogger logger)
        {
            try
            {
                HarmonyInstance harmony = HarmonyInstance.Create("com.blacc.BSPlugin1");
                harmony.PatchAll(Assembly.GetExecutingAssembly());
            }
            catch (Exception e)
            {
                Logger.log.Error(e.Message);
            }

            Logger.log = logger;
            BSEvents.menuSceneLoadedFresh += MenuLoadFresh;
        }

        public void MenuLoadFresh()
        {
            Resources.FindObjectsOfTypeAll<GameScenesManager>().FirstOrDefault().StartCoroutine(PresentTest());
        }

        public void OnSceneLoaded(Scene scene, LoadSceneMode sceneMode) { }
        public void OnActiveSceneChanged(Scene prevScene, Scene nextScene) { }
        public void OnApplicationStart() { }
        public void OnApplicationQuit() { }
        public void OnSceneUnloaded(Scene scene) { }
        public void OnUpdate() { }
        public void OnFixedUpdate() { }

        private IEnumerator PresentTest()
        {
            yield return new WaitForSeconds(1);
            ExampleViewController exampleViewController = BeatSaberUI.CreateViewController<ExampleViewController>();
            Resources.FindObjectsOfTypeAll<MainFlowCoordinator>().First().InvokeMethod("PresentViewController", new object[] { exampleViewController, null, false });
        }

        public class ExampleViewController : BSMLResourceViewController
        {
            public override string ResourceName => "BSPlugin1.example.bsml";

            [UIComponent("some-text")]
            private TextMeshProUGUI text;

            [UIAction("press")]
            private void ButtonPress()
            {
                text.text = "Hey look, the text changed";
            }
        }
    }
}