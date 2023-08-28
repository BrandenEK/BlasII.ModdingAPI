using Il2CppTGK.Game;
using Il2CppTMPro;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace BlasII.ModdingAPI
{
    internal class ModdingAPI : BlasIIMod
    {
        public ModdingAPI() : base(ModInfo.MOD_ID, ModInfo.MOD_NAME, ModInfo.MOD_AUTHOR, ModInfo.MOD_VERSION) { }

        protected internal override void OnInitialize()
        {
            
        }

        protected internal override void OnAllInitialized()
        {
            
        }

        protected internal override void OnDispose()
        {
            
        }

        protected internal override void OnUpdate()
        {

        }

        protected internal override void OnSceneLoaded(string sceneName)
        {
            if (sceneName == "MainMenu")
            {
                DisplayModListOnMenu();
            }
        }

        protected internal override void OnSceneUnloaded(string sceneName)
        {
            
        }

        private void DisplayModListOnMenu()
        {
            // Do this better

            //TextMeshProUGUI[] textObjects = Object.FindObjectsOfType<TextMeshProUGUI>();
            //GameObject textObject = textObjects[^1].gameObject;

            var tmp = Object.FindObjectOfType<TextMeshProUGUI>();
            LogWarning("Size: " + tmp.fontSize);
            LogWarning("Color: " + tmp.color); 
            TMP_FontAsset font = tmp.font;
            LogWarning("Min: " + tmp.rectTransform.anchorMin);
            LogWarning("Max: " + tmp.rectTransform.anchorMax);
            LogWarning("Pos: " + tmp.rectTransform.anchoredPosition);

            GameObject textObject = new GameObject("Text");

            var rect = textObject.AddComponent<RectTransform>();
            rect.SetParent(tmp.transform.parent, false);
            rect.anchorMin = new Vector2(0, 1);
            rect.anchorMax = new Vector2(0, 1);
            rect.anchoredPosition = new Vector2(-100, 200);
            LogWarning("Min: " + rect.anchorMin);
            LogWarning("Max: " + rect.anchorMax);
            LogWarning("Pos: " + rect.anchoredPosition);

            var text = textObject.AddComponent<TextMeshProUGUI>();
            text.font = font;
            text.text = "Test";
            text.fontSize = tmp.fontSize;
            text.color = tmp.color;

            var image = textObject.AddComponent<Image>();


            //GameObject newText = Object.Instantiate(textObject, textObject.transform.parent);
            //(newText.transform as RectTransform).anchoredPosition -= Vector2.right * 200;

            //foreach (TextMeshProUGUI obj in Object.FindObjectsOfType<TextMeshProUGUI>())
            //{
            //    if (obj.text.Contains("1.0.5"))
            //    {
            //        obj.rectTransform.anchoredPosition += Vector2.left * 200;
            //    }
            //}

            //GameObject versionText = GetVersionText();
            //(versionText.transform as RectTransform).anchoredPosition -= Vector2.right * 200;

            //foreach (TextMeshProUGUI obj in Object.FindObjectsOfType<TextMeshProUGUI>())
            //{
            //    if (obj.name == "textUnder" && obj.text.Contains("1.0.5"))
            //        LogWarning(obj.transform.parent.name);
            //}

            // Find the version text object
            //GameObject versionText = FindVersionText();
            //if (versionText == null)
            //    return;

            //RectTransform r = versionText.transform as RectTransform;
            //LogWarning("Min: " + r.anchorMin);
            //LogWarning("Max: " + r.anchorMax);
            //LogWarning("Pos: " + r.anchoredPosition);

            //// Calculate menu text
            //var sb = new StringBuilder("\n\n");
            //foreach (var mod in Main.ModLoader.AllMods)
            //{
            //    sb.AppendLine($"{mod.Name} v{mod.Version}");
            //}

            //// Create copy of version text
            //GameObject modText = Object.Instantiate(versionText, versionText.transform.parent);

            //RectTransform rect = modText.transform as RectTransform;
            //rect.anchoredPosition -= Vector2.right * 100;

            //TextMeshProUGUI text = modText.GetComponent<TextMeshProUGUI>();
            //text.text = sb.ToString();
            //text.alignment = TextAlignmentOptions.TopLeft;
        }

        private GameObject GetVersionText()
        {
            //foreach (TextMeshProUGUI obj in Object.FindObjectsOfType<TextMeshProUGUI>())
            //{
            //    if (obj.text.Contains("1.0.5"))
            //    {
            //        LogWarning(obj.transform.parent.name);
            //        return obj.gameObject;
            //    }
            //}
            return GameObject.Find("version");
            return null;
        }

        //private GameObject FindVersionText()
        //{
        //    Log("Finding");
        //    // Find canvas object
        //    //CanvasScaler canvas = Object.FindObjectOfType<CanvasScaler>();
        //    //if (canvas == null)
        //    //    return null;

        //    //// Find the version text
        //    //foreach (TextMeshProUGUI childText in canvas.gameObject.GetComponentsInChildren<TextMeshProUGUI>())
        //    //{
        //    //    //Log(childText.rectTransform.anchoredPosition);
        //    //    //if (childText.text.Contains("1.0.5"))
        //    //        return childText.gameObject;
        //    //}

        //    foreach (TextMeshProUGUI text in Object.FindObjectsOfType<TextMeshProUGUI>())
        //    {
        //        //LogError(text.name + ": " + text.text);
        //        if (text.text.Contains("1.0.5"))
        //            return text.gameObject;
        //    }

        //    return null;
        //}
    }
}
