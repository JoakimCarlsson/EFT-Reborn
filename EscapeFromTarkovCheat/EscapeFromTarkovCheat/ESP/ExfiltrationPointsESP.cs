﻿using UnityEngine;

namespace EscapeFromTarkovCheat.ESP
{
    public class ExfiltrationPointsESP
    {
        private readonly CheatBehaviour _cheatBehaviour;

        public ExfiltrationPointsESP(CheatBehaviour cheatBehaviour)
        {
            _cheatBehaviour = cheatBehaviour;
        }

        public void DrawExfiltrationPoints()
        {
            if (_cheatBehaviour.ExfiltrationPoints == null)
                return;

            foreach (var exfiltrationPoint in _cheatBehaviour.ExfiltrationPoints)
            {
                if (exfiltrationPoint == null)
                    continue;
                var boundingVector = Camera.main.WorldToScreenPoint(exfiltrationPoint.transform.position);
                if (boundingVector.z > 0.01)
                {
                    float distanceToObject = Vector3.Distance(Camera.main.transform.position, exfiltrationPoint.transform.position);
                    GUI.color = Color.green;
                    string boxText = $"{ExtractionNameToSimpleName(exfiltrationPoint.name)} - {(int)distanceToObject}m";
                    GUI.Label(new Rect(boundingVector.x - 50f, Screen.height - boundingVector.y, 100f, 50f), boxText);
                }
            }
        }

        private string ExtractionNameToSimpleName(string extractionName)
        {
            // Factory
            if (extractionName.Contains("exit (3)"))
                return "Cellars";
            if (extractionName.Contains("exit (1)"))
                return "Gate 3";
            if (extractionName.Contains("exit (2)"))
                return "Gate 0";
            if (extractionName.Contains("exit_scav_gate3"))
                return "Gate 3";
            if (extractionName.Contains("exit_scav_camer"))
                return "Blinking Light";
            if (extractionName.Contains("exit_scav_office"))
                return "Office";

            // Woods
            if (extractionName.Contains("eastg"))
                return "East Gate";
            if (extractionName.Contains("scavh"))
                return "House";
            if (extractionName.Contains("deads"))
                return "Dead Mans Place";
            if (extractionName.Contains("var1_1_constant"))
                return "Outskirts";
            if (extractionName.Contains("scav_outskirts"))
                return "Outskirts";
            if (extractionName.Contains("water"))
                return "Outskirts Water";
            if (extractionName.Contains("boat"))
                return "The Boat";
            if (extractionName.Contains("mountain"))
                return "Mountain Stash";
            if (extractionName.Contains("oldstation"))
                return "Old Station";
            if (extractionName.Contains("UNroad"))
                return "UN Road Block";
            if (extractionName.Contains("var2_1_const"))
                return "UN Road Block";
            if (extractionName.Contains("gatetofactory"))
                return "Gate to Factory";
            if (extractionName.Contains("RUAF"))
                return "RUAF Gate";

            // Shoreline
            if (extractionName.Contains("roadtoc"))
                return "Road to Customs";
            if (extractionName.Contains("lighthouse"))
                return "Lighthouse";
            if (extractionName.Contains("tunnel"))
                return "Tunnel";
            if (extractionName.Contains("wreckedr"))
                return "Wrecked Road";
            if (extractionName.Contains("deadend"))
                return "Dead End";
            if (extractionName.Contains("housefence"))
                return "Ruined House Fence";
            if (extractionName.Contains("gyment"))
                return "Gym Entrance";
            if (extractionName.Contains("southfence"))
                return "South Fence Passage";
            if (extractionName.Contains("adm_base"))
                return "Admin Basement";

            // Customs
            if (extractionName.Contains("administrationg"))
                return "Administration Gate";
            if (extractionName.Contains("factoryfar"))
                return "Factory Far Corner";
            if (extractionName.Contains("oldazs"))
                return "Old Gate";
            if (extractionName.Contains("milkp_sh"))
                return "Shack";
            if (extractionName.Contains("beyondfuel"))
                return "Beyond Fuel Tank";
            if (extractionName.Contains("railroadtom"))
                return "Railroad to Mil Base";
            if (extractionName.Contains("_pay_car"))
                return "V-Exit";
            if (extractionName.Contains("oldroadgate"))
                return "Old Road Gate";
            if (extractionName.Contains("sniperroad"))
                return "Sniper Road Block";
            if (extractionName.Contains("warehouse17"))
                return "Warehouse 17";
            if (extractionName.Contains("factoryshacks"))
                return "Factory Shacks";
            if (extractionName.Contains("railroadtotarkov"))
                return "Railroad to Tarkov";
            if (extractionName.Contains("trailerpark"))
                return "Trailer Park";
            if (extractionName.Contains("crossroads"))
                return "Crossroads";
            if (extractionName.Contains("railroadtoport"))
                return "Railroad to Port";

            // Interchange
            if (extractionName.Contains("NW_Exfil"))
                return "North West Extract";
            if (extractionName.Contains("SE_Exfil"))
                return "South East Extract";
            return extractionName;
        }
    }

}