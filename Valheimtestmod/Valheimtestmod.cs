using BepInEx;
using HarmonyLib;
using Jotunn.Configs;
using Jotunn.Entities;
using Jotunn.Managers;
using Jotunn.Utils;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Logger = Jotunn.Logger;

namespace Valheimtestmod
{
    [BepInPlugin(PluginGUID, PluginName, PluginVersion)]
    [BepInDependency(Jotunn.Main.ModGuid)]
    //[NetworkCompatibility(CompatibilityLevel.EveryoneMustHaveMod, VersionStrictness.Minor)]
    internal class Valheimtestmod : BaseUnityPlugin
    {

        public const string PluginGUID = "com.Zintriz.testmod1";
        public const string PluginName = "Zintriz/testmod1";
        public const string PluginVersion = "0.0.1";
        private readonly Harmony harmony = new Harmony("test.ValheimMod");
        static List<string> normalHelmets = new List<string> { "HelmetBronze", "HelmetCarapace", "HelmetDrake", "HelmetFenring", "HelmetFishingHat", "HelmetMidsummerCrown", "HelmetOdin", "HelmetMage", "HelmetPadded", "HelmetLeather", "HelmetIron", "HelmetRoot", "HelmetTrollLeather", "HelmetYule" };

        // Use this class to add your own localization to the game
        // https://valheim-modding.github.io/Jotunn/tutorials/localization.html
        public static CustomLocalization Localization = LocalizationManager.Instance.GetLocalization();

        private void Awake()
        {
            CommandManager.Instance.AddConsoleCommand(new ToggleSkillPanelCommand());

            harmony.PatchAll(typeof(Patch));
            //harmony.PatchAll(typeof(Patch2));

            //Player.m_localPlayer.GetSkillFactor(Skills.SkillType.Knives);
            // Jotunn comes with its own Logger class to provide a consistent Log style for all mods using it
            Logger.LogInfo("Valheimtestmod123 has landed");
            //Jotunn.Logger.LogInfo(Player.m_localPlayer.GetSkillLevel(Skills.SkillType.Knives));

            // To learn more about Jotunn's features, go to
            // https://valheim-modding.github.io/Jotunn/tutorials/overview.html
            //PrefabManager.OnVanillaPrefabsAvailable += AddCustomItems;
            //PrefabManager.OnVanillaPrefabsAvailable += AddVariants;

        }
 

        private void AddVariants()
        {
            Sprite[] variants = {
                AssetUtils.LoadSpriteFromFile("Valheimtestmod/Assets/Images/test_var1.png"),
                AssetUtils.LoadSpriteFromFile("Valheimtestmod/Assets/Images/test_var2.png"),
                AssetUtils.LoadSpriteFromFile("Valheimtestmod/Assets/Images/test_var3.png"),
                AssetUtils.LoadSpriteFromFile("Valheimtestmod/Assets/Images/test_var4.png"),
            };
            Texture2D styleTex = AssetUtils.LoadTexture("Valheimtestmod/Assets/Images/test_varpaint.png");

            ItemConfig shieldConfig = new ItemConfig();
            shieldConfig.Name = "$lulz_shield";
            shieldConfig.Description = "$lulz_shield_desc";
            shieldConfig.AddRequirement(new RequirementConfig("Wood", 1));
            shieldConfig.Icons = variants;
            shieldConfig.StyleTex = styleTex;
            ItemManager.Instance.AddItem(new CustomItem("item_lulzshield", "ShieldWood", shieldConfig));

            ItemConfig swordConfig = new ItemConfig();
            swordConfig.Name = "$lulz_sword";
            swordConfig.Description = "$lulz_sword_desc";
            swordConfig.AddRequirement(new RequirementConfig("Stone", 1));
            swordConfig.Icons = variants;
            swordConfig.StyleTex = styleTex;
            ItemManager.Instance.AddItem(new CustomItem("item_lulzsword", "SwordBronze", swordConfig));

            // You want that to run only once, Jotunn has the item cached for the game session
            PrefabManager.OnVanillaPrefabsAvailable -= AddVariants;
        }
        void AddCustomItems()
        {
            try
            {
                Rings eikthyrRing = new Rings("EikthyrRing", "EikthyrRing Description", 
                    new List<string> { "$empty" },
                    new Dictionary<string, int> {{"Wood",1},{"Stone",1 } });

                eikthyrRing.effect.m_addMaxCarryWeight = 150;
                eikthyrRing.effect.m_attackStaminaUseModifier = -0.10f; //-10f

                Rings elderRing = new Rings("ElderRing", "ElderRing Description",
                    new List<string> { "$item_copper", "$item_copperore", "$item_copperscrap", "$item_tin", "$item_tinore" },
                    new Dictionary<string, int> { { "Wood", 1 }, { "Stone", 1 } });

                elderRing.effect.m_addMaxCarryWeight = 200;
                elderRing.effect.m_attackStaminaUseModifier = -0.10f; //-10f

                Rings bonemassRing = new Rings("BonemassRing", "BonemassRing Description",
                    new List<string> { "$item_iron", "item_ironscraps"},
                    new Dictionary<string, int> { { "Wood", 1 }, { "Stone", 1 } });

                bonemassRing.effect.m_addMaxCarryWeight = 250;
                bonemassRing.effect.m_attackStaminaUseModifier = -0.10f; //-10f


                Rings moderRing = new Rings("ModerRing", "ModerRing Description",
                    new List<string> { "$item_silver", "$item_silverore" },
                    new Dictionary<string, int> { { "Wood", 1 }, { "Stone", 1 } });
                moderRing.effect.m_addMaxCarryWeight = 300;
                moderRing.effect.m_attackStaminaUseModifier = -0.10f; //-10f

                Rings yagRing = new Rings("YagRing", "YagRing Description",
                    new List<string> { "$item_blackmetal", "$item_blackmetalscrap" },
                    new Dictionary<string, int> { { "Wood", 1 }, { "Stone", 1 } });

                yagRing.effect.m_addMaxCarryWeight = 350;
                yagRing.effect.m_attackStaminaUseModifier = -0.10f; //-10f

                Rings queenRing = new Rings("QueenRing", "QueenRing Description",
                    new List<string> { "$empty" },
                    new Dictionary<string, int> { { "Wood", 1 }, { "Stone", 1 } });

                queenRing.effect.m_addMaxCarryWeight = 400;
                queenRing.effect.m_attackStaminaUseModifier = -0.10f; //-10f

                /*foreach (var item in normalHelmets)
                {
                    Diadem x_diadem = new Diadem(item);
                }*/

            }
            catch (Exception ex)
            {
                Jotunn.Logger.LogError($"Error while adding cloned item: {ex}");
            }
            finally
            {
                // You want that to run only once, Jotunn has the item cached for the game session
                PrefabManager.OnVanillaPrefabsAvailable -= AddCustomItems;
            }
        }

    }
    public static class Patch
    {
        private static GameObject SkillPanel;
        private static GameObject textObject;
        public static void ToggleSkillPanel()
        {
            if (!SkillPanel)
            {
                if (GUIManager.Instance == null)
                {
                    Logger.LogError("GUIManager instance is null");
                    return;
                }

                if (!GUIManager.CustomGUIBack)
                {
                    Logger.LogError("GUIManager CustomGUI is null");
                    return;
                }
                SkillPanel = GUIManager.Instance.CreateWoodpanel(
                    parent: GUIManager.CustomGUIBack.transform,
                    anchorMin: new Vector2(0.5f, 0.5f),
                    anchorMax: new Vector2(0.5f, 0.5f),
                    /*anchorMin: new Vector2(0.5f, 0.5f),
                    anchorMax: new Vector2(0.5f, 0.5f),*/
                    position: new Vector2(800, 100),
                    width: 300,
                    height: 80,
                    draggable: false);
                SkillPanel.SetActive(false);

                textObject = GUIManager.Instance.CreateText(
                    text: "textObject",
                    parent: SkillPanel.transform,
                    anchorMin: new Vector2(0.5f, 1f),
                    anchorMax: new Vector2(0.5f, 1f),
                    /*anchorMin: new Vector2(0.5f, 1f),
                    anchorMax: new Vector2(0.5f, 1f),*/
                    position: new Vector2(0f, -40f),
                    font: GUIManager.Instance.AveriaSerifBold,
                    fontSize: 16,
                    color: Color.white,
                    outline: true,
                    outlineColor: Color.black,
                    width: 260f,
                    height: 40f,
                    addContentSizeFitter: false);
            }

            // Switch the current state
            bool state = !SkillPanel.activeSelf;
            // Set the active state of the panel
            SkillPanel.SetActive(state);
        }

        public static void UpdateText(string _text)
        {
            try
            {
                if (textObject)
                {
                    var textComponent = textObject.GetComponent<Text>();
                    textComponent.text = _text;
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex);
            }
        }
        [HarmonyPatch(typeof(Player), "RaiseSkill")]
        public static void Postfix()
        {
            SkillDisplay.UpdateDisplay();
        }
    }
    public static class Patch2
    {
        [HarmonyPatch(typeof(Player), "Interact")]
        public static bool Prefix(GameObject go, bool hold, bool alt, Player __instance)
        {

            if (__instance.InAttack() || __instance.InDodge() || (hold && Time.time - __instance.m_lastHoverInteractTime < 0.05f))
            {
                return false;
            }

            Interactable componentInParent = go.GetComponentInParent<Interactable>();
            if (componentInParent != null)
            {
                __instance.m_lastHoverInteractTime = Time.time;
                if (componentInParent.Interact(__instance, hold, alt))
                {

                    __instance.DoInteractAnimation(go.transform.position);
                }
            }
            // access “this” in the original
            //__instance.SomeOtherMethod();

            // use originals input parameters
            //Log(bar);

            // return your own result

            // return false to skip the original

            return false;
        }
    }
}

