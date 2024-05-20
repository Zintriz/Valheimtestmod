using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Skills;
using UnityEngine;
using Jotunn.Managers;
using Logger = Jotunn.Logger;
using Jotunn.Entities;


namespace Valheimtestmod
{
    internal class SkillDisplay
    {
        public static Dictionary<SkillType, float> currentSkillLevels = new Dictionary<SkillType, float>();
        public static Dictionary<SkillType, int> skillCount = new Dictionary<SkillType, int>();
        public static CustomLocalization Localization = LocalizationManager.Instance.GetLocalization();


        public static void Display()
        {
            foreach (var skill in Player.m_localPlayer.m_skills.m_skillData)
            {
                if (!currentSkillLevels.ContainsKey(skill.Key))
                {
                    //Logger.LogInfo($"Adding {skill.Key} to currentSkillLevels");
                    currentSkillLevels.Add(skill.Key, skill.Value.GetLevelPercentage());
                    skillCount.Add(skill.Key, 1);
                    continue;
                }
                if (currentSkillLevels[skill.Key] != skill.Value.GetLevelPercentage())
                {

                    switch (skill.Key)
                    {
                        /*case SkillType.Jump:
                            //if (skillCount[skill.Key] < 5) { skillCount[skill.Key]++; continue; } else { skillCount[skill.Key] = 1; }
                            continue;*/
                        case SkillType.Run:
                            continue;
                        default:
                            if (skill.Key == SkillType.Jump) { Logger.LogInfo($"default skill: {skill.Key}"); }
                            break;
                    }
                    string skillname = $"$skill_{skill.Key}".ToLower();
                    float gain = Mathf.Max(0, 100 * (skill.Value.GetLevelPercentage() - currentSkillLevels[skill.Key]));
                    Sprite msgIcon = skill.Value.m_info.m_icon;
                    float level = skill.Value.m_level-1;
                    float levelPercentage = 100 * skill.Value.GetLevelPercentage();
                    string msgText = $"{skillname} [Lv {level:##0}] {levelPercentage:##0.0}% (+{gain:0.##}%)";
                    Patch.UpdateText(Localization.TryTranslate(skillname) + $" [Lv {level:##0}] {levelPercentage:##0.0}% (+{gain:0.##}%)");
                    Player.m_localPlayer.Message(MessageHud.MessageType.TopLeft, msgText, 1, msgIcon);

                    currentSkillLevels[skill.Key] = skill.Value.GetLevelPercentage();
                }
            }
        }
    }
}
