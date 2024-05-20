using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jotunn.Entities;
using Logger = Jotunn.Logger;


namespace Valheimtestmod
{
    public class ToggleSkillPanelCommand : ConsoleCommand
    {
        public override string Name => "ToggleSkillPanel";

        public override string Help => "Toggles the skill panel on or off.";

        public override void Run(string[] args)
        {
            Patch.ToggleSkillPanel();


            /*Console.instance.Print("All players:");

            foreach (Player player in Player.GetAllPlayers())
            {
                Console.instance.Print(player.GetPlayerName());
            }*/
        }
    }
}
