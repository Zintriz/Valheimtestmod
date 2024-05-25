using Jotunn.Configs;
using Jotunn.Entities;
using Jotunn.Managers;
using Jotunn.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Instrumentation;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Logger = Jotunn.Logger;

namespace Valheimtestmod
{
    internal class Rings
    {

/*        private void AddElderRing()
        {
            string _name = "ElderRing";
            // Create and add a custom item based on SwordBlackmetal
            ItemConfig IC = new ItemConfig();
            IC.Name = "$item_" + _name.ToLower();
            IC.Description = "$item_" + _name.ToLower() + "_desc";
            Sprite icon = AssetUtils.LoadSpriteFromFile($"ValheimMod/Assets/{_name}.png");
            IC.Icons = new Sprite[] { icon };
            CustomItem customItem = new CustomItem(_name, "BeltStrength", IC);
            ItemManager.Instance.AddItem(customItem);

            var incineratorConfig = new IncineratorConversionConfig();

            incineratorConfig.Requirements.Add(new IncineratorRequirementConfig("EikthyrRing", 1));
            incineratorConfig.Requirements.Add(new IncineratorRequirementConfig("TrophyTheElder", 1));
            incineratorConfig.Requirements.Add(new IncineratorRequirementConfig("Thunderstone", 1));
            incineratorConfig.ProducedItems = 1;
            incineratorConfig.RequireOnlyOneIngredient = false;  // true = only one of the requirements is needed to produce the output
            incineratorConfig.Priority = 10;                      // Higher priorities get preferred when multiple requirements are met
            incineratorConfig.ToItem = _name;
            ItemManager.Instance.AddItemConversion(new CustomItemConversion(incineratorConfig));

            // Add our custom status effect to it
            ItemDrop itemDrop = customItem.ItemDrop;

            itemDrop.m_itemData.m_shared.m_movementModifier = 1;

            // add effect
            SE_Stats effect = ScriptableObject.CreateInstance<SE_Stats>();
            effect.name = _name + "Effect";
            effect.m_name = "$" + _name.ToLower() + "_effectname";
            effect.m_tooltip = "$" + _name.ToLower() + "_tooltip";
            effect.m_startMessage = "$" + _name.ToLower() + "_effectstart";
            effect.m_stopMessage = "$" + _name.ToLower() + "_effectstop";
            effect.m_startMessageType = MessageHud.MessageType.Center;
            effect.m_stopMessageType = MessageHud.MessageType.Center;
            effect.m_icon = AssetUtils.LoadSpriteFromFile($"ValheimMod/Assets/{_name}.png");

            effect.m_addMaxCarryWeight = 200;
            effect.m_staminaRegenMultiplier = 2;
            effect.m_jumpStaminaUseModifier = -0.3f;
            effect.m_runStaminaDrainModifier = -0.3f;
            effect.m_fallDamageModifier = -0.1f;

            CustomStatusEffect CE = new CustomStatusEffect(effect, fixReference: false);  // We dont need to fix refs here, because no mocks were used
            ItemManager.Instance.AddStatusEffect(CE);

            itemDrop.m_itemData.m_shared.m_equipStatusEffect = CE.StatusEffect;
        }
*/        
        string name;
        string description;
        List<string> teleportList;
        public SE_Stats effect;
        string iconName; //Valheimtestmod/Assets/Images/test.png
        Dictionary<string,int> requirements;
        bool useIncinerator;

        public Rings(string name, string description, List<string> teleportList, Dictionary<string, int> requirements, bool useIncinerator = false)
        {
            this.name = name;
            this.description = description;
            this.teleportList = teleportList; //this.teleportList = new List<string>{ "$item_copper", "$item_copperore","$item_copperscrap","$item_tin","$item_tinore"};
            this.requirements = requirements;
            this.useIncinerator = useIncinerator;
            
            string nameLower = name.ToLower();

            var itemConfig = new ItemConfig();
            itemConfig.Name = name;
            itemConfig.Description = description;
            //Sprite icon = AssetUtils.LoadSpriteFromFile($"ValheimMod/Assets/{name}.png");
            //Sprite icon = AssetUtils.LoadSpriteFromFile($"Valheimtestmod/Assets/Images/Madge.png");
            Sprite[] variants = {
                AssetUtils.LoadSpriteFromFile("Valheimtestmod/Assets/Images/test_var1.png"),
                AssetUtils.LoadSpriteFromFile("Valheimtestmod/Assets/Images/test_var2.png"),
                //AssetUtils.LoadSpriteFromFile("Valheimtestmod/Assets/Images/test_var3.png"),
                //AssetUtils.LoadSpriteFromFile("Valheimtestmod/Assets/Images/test_var4.png"),
                PrefabManager.Cache.GetPrefab<Sprite>("TrophyBoar"),
                PrefabManager.Cache.GetPrefab<Sprite>("TrophyDragonQueen")
            };

            Texture2D styleTex = AssetUtils.LoadTexture("Valheimtestmod/Assets/Images/test_varpaint.png");
            itemConfig.Icons = variants;//new Sprite[] { icon };
            itemConfig.StyleTex = styleTex;//AssetUtils.LoadSpriteFromFile($"Valheimtestmod/Assets/Images/Madge.png");

            if (useIncinerator)
            {
                IncineratorConversionConfig incineratorConfig = new IncineratorConversionConfig();
                foreach (var requirement in requirements)
                {
                    incineratorConfig.Requirements.Add(new IncineratorRequirementConfig(requirement.Key,requirement.Value));
                }
                ItemManager.Instance.AddItemConversion(new CustomItemConversion(incineratorConfig));
            }
            else
            {
                foreach (var requirement in requirements)
                {
                    itemConfig.AddRequirement(new RequirementConfig(requirement.Key,requirement.Value));
                }
                itemConfig.CraftingStation = "piece_workbench";
            }
            
            CustomItem customItem = new CustomItem(name, "ShieldWood", itemConfig);
            //CustomItem customItem = new CustomItem(name, "BeltStrength", itemConfig);
            ItemManager.Instance.AddItem(customItem);
            effect = ScriptableObject.CreateInstance<SE_Stats>();
            effect.name = name + "Effect";
            effect.m_name = "$" + nameLower + "_effectname";
            effect.m_tooltip = "$" + nameLower + "_tooltip";
            effect.m_startMessage = "$" + nameLower + "_effectstart";
            effect.m_stopMessage = "$" + nameLower + "_effectstop";
            effect.m_startMessageType = MessageHud.MessageType.Center;
            effect.m_stopMessageType = MessageHud.MessageType.Center;
            //effect.m_icon = AssetUtils.LoadSpriteFromFile($"ValheimMod/Assets/{name}.png");
            effect.m_icon = AssetUtils.LoadSpriteFromFile($"Valheimtestmod/Assets/Images/Madge.png");



            ItemDrop itemDrop = customItem.ItemDrop;
            if ( name == "ModerRing")
            {
                var moder = PrefabManager.Cache.GetPrefab<StatusEffect>("GP_Moder");
                itemDrop.m_itemData.m_shared.m_equipStatusEffect = moder;
                return;
            }
            CustomStatusEffect CE = new CustomStatusEffect(effect, fixReference: false);
            ItemManager.Instance.AddStatusEffect(CE);
            itemDrop.m_itemData.m_shared.m_equipStatusEffect = CE.StatusEffect;

            /*RecipeConfig addItself = new RecipeConfig();
            addItself.Item = name;
            addItself.AddRequirement(new RequirementConfig(name,1));
            addItself.CraftingStation = "piece_workbench";
            ItemManager.Instance.AddRecipe(new CustomRecipe(addItself));*/
        }

    }
}
