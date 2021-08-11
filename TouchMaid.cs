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
    [BepInPlugin("org.fmnijk.plugins.touchmaid", "COM3D2.TouchMaid.Plugin", "1.0.0.0")]
    public class TouchMaid : BaseUnityPlugin
    {
        private ConfigEntry<KeyboardShortcut> TouchMaidChest { get; set; }

        public void Awake()
        {
            TouchMaidChest = Config.Bind("Hotkeys", "TouchMaid Hotkeys", new KeyboardShortcut(KeyCode.D, KeyCode.LeftShift));
        }

        public void Constructor()
        {
            
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
                                Vector3 vectorL = (Vector3)AccessTools.Field(typeof(jiggleBone), "dynamicPos").GetValue(m.body0.jbMuneL);
                                //Debug.Log("(" + vectorL.x.ToString() + ", " + vectorL.y.ToString() + ", " + vectorL.z.ToString() + ")");
                                vectorL.x += 0f;
                                vectorL.y += 0.5f;
                                vectorL.z += 0f;
                                AccessTools.Field(typeof(jiggleBone), "dynamicPos").SetValue(m.body0.jbMuneL, vectorL);

                                Vector3 vectorR = (Vector3)AccessTools.Field(typeof(jiggleBone), "dynamicPos").GetValue(m.body0.jbMuneR);
                                //Debug.Log("(" + vectorR.x.ToString() + ", " + vectorR.y.ToString() + ", " + vectorR.z.ToString() + ")");
                                vectorR.x += 0f;
                                vectorR.y += 0.5f;
                                vectorR.z += 0f;
                                AccessTools.Field(typeof(jiggleBone), "dynamicPos").SetValue(m.body0.jbMuneR, vectorR);
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
