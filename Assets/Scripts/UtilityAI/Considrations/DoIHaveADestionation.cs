using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TL.Core;
using System;

namespace TL.UtilityAI.Considerations
{
    [CreateAssetMenu(fileName = "DoIHaveA_Destination", menuName = "UtilityAI/Considerations/Do I Have A Destination")]
    public class DoIHaveADestionation : Consideration
    {
        [SerializeField] private DestinationType destinationType;
        [SerializeField] private bool invertResponse = false;

        public override float ScoreConsideration(NPCController npc)
        {
            // if we have a destination, return a score of 1
            if (npc.mover.destination != null && npc.mover.destination.tag == destinationType.ToString())
            {
                score = Response(invertResponse, true);
            }

            // if we don't have a destination, return a score of 0
            else
            {
                score = Response(invertResponse, false);
            }

            return score;
        }

        private float Response(bool invertResponse, bool defaultValue)
        {
            if (invertResponse)
            {
                return Convert.ToInt32(!defaultValue);
            }
            return Convert.ToInt32(defaultValue);
        }
    }
}