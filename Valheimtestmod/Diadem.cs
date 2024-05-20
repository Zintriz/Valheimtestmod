using Jotunn;
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

namespace Valheimtestmod
{
    //HelmetDverger HelmetDrake HelmetCarapace HelmetBronze HelmetFenring
    internal class Diadem
    {
        //var moder = PrefabManager.Cache.GetPrefab<StatusEffect>("HelmetDverger");
        
        public Diadem(string helmetName) {
            // Create and add a custom item based on SwordBlackmetal
            var name = helmetName + "Light";
            var itemConfig = new ItemConfig();
            itemConfig.Name = name;
            itemConfig.Description = "Light";

            var incineratorConfig = new IncineratorConversionConfig();
            incineratorConfig.Requirements.Add(new IncineratorRequirementConfig(helmetName, 1));
            incineratorConfig.Requirements.Add(new IncineratorRequirementConfig("HelmetDverger", 1));
            incineratorConfig.ToItem = name;
            ItemManager.Instance.AddItemConversion(new CustomItemConversion(incineratorConfig));

            var customItem = new CustomItem(name, "HelmetDverger", itemConfig);
            ItemManager.Instance.AddItem(customItem);
            var tempItem = new CustomItem("tempHelmet", helmetName, itemConfig);
            ItemManager.Instance.AddItem(tempItem);

            /*effect = PrefabManager.Cache.GetPrefab<StatusEffect>("GP_Bonemass");
            CustomStatusEffect CE = new CustomStatusEffect(effect, fixReference: false);
            ItemManager.Instance.AddStatusEffect(CE);*/
            var itemDrop = new ItemDrop();
            itemDrop = customItem.ItemDrop;
            //var originalItem = PrefabManager.Cache.GetPrefab<ItemDrop>(helmetName);
            itemDrop.m_itemData.m_shared = tempItem.ItemDrop.m_itemData.m_shared;
            itemDrop.m_itemData.m_shared.m_name = name;

            /*itemDrop.m_itemData.m_shared.m_equipStatusEffect = CE.StatusEffect;*/
        }
    }
}
