using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TL.Core;

namespace TL.UtilityAI
{
    public class AIBrain : MonoBehaviour
    {
        public Action bestAction { get; set; }
        private NPCController npc;

        // Start is called before the first frame update
        void Start()
        {
            npc = GetComponent<NPCController>();
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void DecideBestAction(Action[] actionsAvailable)
        {

        }

        public void ScoreAction(Action action)
        {

        }


    }
}
