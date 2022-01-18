using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TL.Core;

namespace TL.UtilityAI
{
    public abstract class Action : ScriptableObject
    {
        public string Name;
        private float _score;
        public float score
        {
            get { return _score; }
            set
            {
                this._score = Mathf.Clamp01(value);
            }
        }

        public Consideration[] considerations;

        public Transform RequiredDestination { get; protected set; }

        public virtual void Awake()
        {
            score = 0;
        }

        public abstract void Execute(NPCController npc);

        public virtual void SetRequiredDestination(NPCController npc) { }
    }
}
