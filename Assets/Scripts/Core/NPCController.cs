using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TL.UtilityAI;

namespace TL.Core
{
    public enum State
    {
        decide,
        move,
        execute
    }

    public class NPCController : MonoBehaviour
    {
        public MoveController mover { get; set; }
        public AIBrain aiBrain { get; set; }
        public NPCInventory Inventory { get; set; }
        public Stats stats { get; set; }

        public Context context;        

        public State currentState { get; set; }

        // Start is called before the first frame update
        void Start()
        {
            mover = GetComponent<MoveController>();
            aiBrain = GetComponent<AIBrain>();
            Inventory = GetComponent<NPCInventory>();
            stats = GetComponent<Stats>();
            currentState = State.decide;
        }

        // Update is called once per frame
        void Update()
        {
            FSMTick();
        }

        public void FSMTick()
        {
            if (currentState == State.decide)
            {
                aiBrain.DecideBestAction();

                if (Vector3.Distance(aiBrain.bestAction.RequiredDestination.position, this.transform.position) < 2f)
                {
                    currentState = State.execute;
                }
                else
                {
                    currentState = State.move;
                }
            }
            else if (currentState == State.move)
            {
                float distance = Vector3.Distance(aiBrain.bestAction.RequiredDestination.position, this.transform.position);
                Debug.Log($"Destination: {mover.destination.name} | Distance: {distance}");
                if ( distance < 2f)
                {
                    currentState = State.execute;
                }
                else
                {
                    Debug.Log("Still moving!");
                    mover.MoveTo(aiBrain.bestAction.RequiredDestination.position);
                }
            }
            else if (currentState == State.execute)
            {
                if (aiBrain.finishedExecutingBestAction == false)
                {
                    Debug.Log("Executing action");
                    aiBrain.bestAction.Execute(this);
                }
                else if (aiBrain.finishedExecutingBestAction == true)
                {
                    Debug.Log("Exit execute state");
                    currentState = State.decide;
                }
            }
        }

        #region Workhorse methods

        public void OnFinishedAction()
        {
            aiBrain.DecideBestAction();
        }

        public bool AmIAtRestDestination()
        {
            return Vector3.Distance(this.transform.position, context.home.transform.position) <= context.MinDistance;
        }

        #endregion

        #region Coroutine

        public void DoWork(int time)
        {
            StartCoroutine(WorkCoroutine(time));
        }

        public void DoSleep(int time)
        {
            StartCoroutine(SleepCoroutine(time));
        }

        IEnumerator WorkCoroutine(int time)
        {
            int counter = time;
            while (counter > 0)
            {
                yield return new WaitForSeconds(1);
                counter--;
            }

            Debug.Log("I AM WORKING!");
            // Logic to update things involved with work
            Inventory.AddResource(ResourceType.wood, 10);

            // Decide our new best action after you finished this one
            //OnFinishedAction();
            aiBrain.finishedExecutingBestAction = true;
            yield break;
        }

        IEnumerator SleepCoroutine(int time)
        {
            int counter = time;
            while (counter > 0)
            {
                yield return new WaitForSeconds(1);
                counter--;
            }

            Debug.Log("I slept and gained 1 energy!");
            // Logic to update energy
            stats.energy += 5;

            // Decide our new best action after you finished this one
            //OnFinishedAction();
            aiBrain.finishedExecutingBestAction = true;
            yield break;
        }


        #endregion
    }
}
