using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BepInEx;
using BepInEx.Configuration;
using UnityEngine;
using HarmonyLib;


namespace TouchMaid
{
    [BepInPlugin("org.fmnijk.plugins.touchmaid", "COM3D2.TouchMaid.Plugin", "1.0.0.1")]
    public class TouchMaid : BaseUnityPlugin
    {
        private ConfigEntry<KeyboardShortcut> TouchMaidChest { get; set; }

        public void Awake()
        {
            TouchMaidChest = Config.Bind("Hotkeys", "TouchMaid Hotkeys", new KeyboardShortcut(KeyCode.Keypad1, KeyCode.LeftShift));
        }

        public void Update()
        {
            try
            {
                if (TouchMaidChest.Value.IsDown())
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
                                vector.x += UnityEngine.Random.Range(-0.3f, 0.3f);
                                vector.y += UnityEngine.Random.Range(-0.3f, 0.3f);
                                vector.z += UnityEngine.Random.Range(-0.3f, 0.3f);
                                AccessTools.Field(typeof(jiggleBone), "dynamicPos").SetValue(m.body0.jbMuneL, vector);

                                vector = (Vector3)AccessTools.Field(typeof(jiggleBone), "dynamicPos").GetValue(m.body0.jbMuneR);
                                vector.x += UnityEngine.Random.Range(-0.3f, 0.3f);
                                vector.y += UnityEngine.Random.Range(-0.3f, 0.3f);
                                vector.z += UnityEngine.Random.Range(-0.3f, 0.3f);
                                AccessTools.Field(typeof(jiggleBone), "dynamicPos").SetValue(m.body0.jbMuneR, vector);
                                

                                vector = (Vector3)AccessTools.Field(typeof(jiggleBone), "vel").GetValue(m.body0.jbMuneL);
                                vector.x += UnityEngine.Random.Range(-0.1f, 0.1f);
                                vector.y += UnityEngine.Random.Range(-0.1f, 0.1f);
                                vector.z += UnityEngine.Random.Range(-0.1f, 0.1f);
                                AccessTools.Field(typeof(jiggleBone), "vel").SetValue(m.body0.jbMuneL, vector);

                                vector = (Vector3)AccessTools.Field(typeof(jiggleBone), "vel").GetValue(m.body0.jbMuneR);
                                vector.x += UnityEngine.Random.Range(-0.1f, 0.1f);
                                vector.y += UnityEngine.Random.Range(-0.1f, 0.1f);
                                vector.z += UnityEngine.Random.Range(-0.1f, 0.1f);
                                AccessTools.Field(typeof(jiggleBone), "vel").SetValue(m.body0.jbMuneR, vector);
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Debug.Log("TouchMaid Plug-In Error!");
                Debug.Log(e);
            }
        }
    }
}
