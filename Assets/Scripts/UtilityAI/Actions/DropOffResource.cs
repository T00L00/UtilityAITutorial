using System.Collections;
using System.Collections.Generic;
using TL.Core;
using UnityEngine;

namespace TL.UtilityAI.Actions
{
    [CreateAssetMenu(fileName = "DropOffResource", menuName = "UtilityAI/Actions/DropOffResource")]
    public class DropOffResource : Action
    {
        public override void Execute(NPCController npc)
        {
            Debug.Log("Dropped Off Resource");
            npc.Inventory.RemoveAllResource();
            npc.stats.money += 20;
            npc.aiBrain.finishedExecutingBestAction = true;
        }

        public override void SetRequiredDestination(NPCController npc)
        {
            RequiredDestination = npc.context.storage.transform;
            npc.mover.destination = RequiredDestination;
        }
    }
}
