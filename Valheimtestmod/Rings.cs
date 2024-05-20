using Jotunn.Configs;
using Jotunn.Entities;
using Jotunn.Managers;
using Jotunn.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
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
        ItemConfig itemConfig;
        SE_Stats effect;

        IncineratorConversionConfig incineratorConfig = new IncineratorConversionConfig();
        public Rings(string name)
        {
            this.name = name;
            this.teleportList = new List<string>{ "$item_copper", "$item_copperore","$item_copperscrap","$item_tin","$item_tinore"};

            this.itemConfig = new ItemConfig();
            this.effect = ScriptableObject.CreateInstance<SE_Stats>();

            itemConfig.Name = "$item_" + name.ToLower();
            itemConfig.Description = "$item_" + name.ToLower() + "_desc";
            //Sprite icon = AssetUtils.LoadSpriteFromFile($"ValheimMod/Assets/{name}.png");
            Sprite icon = AssetUtils.LoadSpriteFromFile($"Valheimtestmod/Assets/Madge.png");
            itemConfig.Icons = new Sprite[] { icon };
            CustomItem customItem = new CustomItem(name, "BeltStrength", itemConfig);
            ItemManager.Instance.AddItem(customItem);

            effect.name = name + "Effect";
            effect.m_name = "$" + name.ToLower() + "_effectname";
            effect.m_tooltip = "$" + name.ToLower() + "_tooltip";
            effect.m_startMessage = "$" + name.ToLower() + "_effectstart";
            effect.m_stopMessage = "$" + name.ToLower() + "_effectstop";
            effect.m_startMessageType = MessageHud.MessageType.Center;
            effect.m_stopMessageType = MessageHud.MessageType.Center;
            //effect.m_icon = AssetUtils.LoadSpriteFromFile($"ValheimMod/Assets/{name}.png");
            effect.m_icon = AssetUtils.LoadSpriteFromFile($"Valheimtestmod/Assets/Madge.png");
            effect.m_addMaxCarryWeight = 100;

            CustomStatusEffect CE = new CustomStatusEffect(effect, fixReference: false);
            ItemManager.Instance.AddStatusEffect(CE);
            ItemDrop itemDrop = customItem.ItemDrop;
            itemDrop.m_itemData.m_shared.m_equipStatusEffect = CE.StatusEffect;

        }
    }
}
