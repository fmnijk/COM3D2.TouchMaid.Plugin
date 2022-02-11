using System.Collections.Generic;
using BepInEx;
using BepInEx.Configuration;
using UnityEngine;
using UnityEngine.EventSystems;
using HarmonyLib;
using System.Collections;

//TODO:使胸部晃動情況可大於100
//TODO:控制頭部
//TODO:滑鼠控制胸部
//TODO:控制頭髮
//TODO:控制裙子
//TODO:隨時控制燈光
//TODO:紅屁股復原
/*if (Input.GetMouseButton(0))
{
    var a = Input.mousePosition;
    List<Maid> maids = GameMain.Instance.CharacterMgr.GetStockMaidList();
    if (maids != null)
    {
        foreach (Maid m in maids)
        {
            if (m.Visible == true)
            {
                Vector3 vector;
                vector = (Vector3)AccessTools.Field(typeof(jiggleBone), "dynamicPos").GetValue(m.body0.jbMuneL);
                Debug.Log(vector);
            }
        }
    }
}*/

namespace TouchMaid
{
    [BepInPlugin("org.fmnijk.plugins.touchmaid", "COM3D2.TouchMaid.Plugin", "1.0.0.1")]
    public class TouchMaid : BaseUnityPlugin
    {
        private ConfigEntry<KeyboardShortcut> TouchMuneHotkey { get; set; }
        private ConfigEntry<KeyboardShortcut> TouchMuneLoopHotkey { get; set; }

        public void Awake()
        {
            TouchMuneHotkey = Config.Bind("Hotkeys", "TouchMuneHotkey", new KeyboardShortcut(KeyCode.D, KeyCode.LeftShift));
            TouchMuneLoopHotkey = Config.Bind("Hotkeys", "TouchMuneLoopHotkey", new KeyboardShortcut(KeyCode.F, KeyCode.LeftShift));
        }

        public void TouchHead()
        {
            List<Maid> maids = GameMain.Instance.CharacterMgr.GetStockMaidList();
            if (maids != null)
            {
                foreach (Maid m in maids)
                {
                    if (m.Visible == true)
                    {
                        Vector3 vector;

                        vector = (Vector3)AccessTools.Field(typeof(TBody), "HeadEulerAngle").GetValue(m.body0);
                        Debug.Log(vector);
                        vector += Random.insideUnitSphere * 30;
                        AccessTools.Field(typeof(TBody), "HeadEulerAngle").SetValue(m.body0, vector);
                    }
                }
            }
        }

        public void TouchMune()
        {
            List<Maid> maids = GameMain.Instance.CharacterMgr.GetStockMaidList();
            if (maids != null)
            {
                foreach (Maid m in maids)
                {
                    if (m.Visible == true)
                    {
                        Vector3 vector;

                        vector = (Vector3)AccessTools.Field(typeof(jiggleBone), "dynamicPos").GetValue(m.body0.jbMuneL);
                        vector += Random.insideUnitSphere / 2;
                        AccessTools.Field(typeof(jiggleBone), "dynamicPos").SetValue(m.body0.jbMuneL, vector);

                        vector = (Vector3)AccessTools.Field(typeof(jiggleBone), "dynamicPos").GetValue(m.body0.jbMuneR);
                        vector += Random.insideUnitSphere / 2;
                        AccessTools.Field(typeof(jiggleBone), "dynamicPos").SetValue(m.body0.jbMuneR, vector);

                        vector = (Vector3)AccessTools.Field(typeof(jiggleBone), "vel").GetValue(m.body0.jbMuneL);
                        vector += Random.insideUnitSphere / 10;
                        AccessTools.Field(typeof(jiggleBone), "vel").SetValue(m.body0.jbMuneL, vector);

                        vector = (Vector3)AccessTools.Field(typeof(jiggleBone), "vel").GetValue(m.body0.jbMuneR);
                        vector += Random.insideUnitSphere / 10;
                        AccessTools.Field(typeof(jiggleBone), "vel").SetValue(m.body0.jbMuneR, vector);
                    }
                }
            }
        }

        public void Update()
        {
            try
            {
                if (TouchMuneHotkey.Value.IsDown())
                {
                    TouchMune();
                }
                if (TouchMuneLoopHotkey.Value.IsDown())
                {
                    if (IsInvoking("TouchMune"))
                    {
                        CancelInvoke("TouchMune");
                    }
                    else
                    {
                        InvokeRepeating("TouchMune", 0f, 2.5f);
                    }
                }
            }
            catch (System.Exception e)
            {
                Debug.Log("TouchMaid Plug-In Error!");
                Debug.Log(e);
            }
        }
    }
}
