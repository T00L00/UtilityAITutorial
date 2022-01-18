using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TL.UtilityAI;
using TL.Core;

namespace TL.UtilityAI.Actions
{
    [CreateAssetMenu(fileName = "Work", menuName = "UtilityAI/Actions/Work")]
    public class Work : Action
    {
        public override void Execute(NPCController npc)
        {
            npc.DoWork(3);
        }

        public override void SetRequiredDestination(NPCController npc)
        {
            float distance = Mathf.Infinity;
            Transform nearestResource = null;

            List<Transform> resources = npc.context.Destinations[DestinationType.resource];
            foreach (Transform resource in resources)
            {
                float distanceFromResource = Vector3.Distance(resource.position, npc.transform.position);
                if (distanceFromResource < distance)
                {
                    nearestResource = resource;
                    distance = distanceFromResource;
                }
            }

            RequiredDestination = nearestResource;
            npc.mover.destination = RequiredDestination;
        }
    }
}
