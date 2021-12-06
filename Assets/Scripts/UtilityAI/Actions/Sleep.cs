using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TL.UtilityAI;
using TL.Core;

namespace TL.UtilityAI.Actions
{
    [CreateAssetMenu(fileName = "Sleep", menuName = "UtilityAI/Actions/Sleep")]
    public class Sleep : Action
    {
        public override void Execute(NPCController npc)
        {
            npc.DoSleep(3);
        }
    }
}
