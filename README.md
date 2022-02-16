# UtilityAITutorial
Complete project files for a prototype Utility AI built for my youtube tutorial <br>
(https://www.youtube.com/watch?v=ejKrvhusU1I&list=PLDpv2FF85TOp2KpIGcrxXY1POzfYLGWIb)

Each branch corresponds to the different parts of the tutorial.
For complete prototype with working village example, use main or Part6 branch.

## What is Utility-based AI and how can it be used for games?
It's an AI system that makes decisions by scoring and ranking actions instead of using simple if-then rules like in Finite State Machines or Behavior trees. For example, let's take a simple scenario of a villager who can work for money, eat to lower its hunger meter, or sleep to replenish its energy throughout the day. 

In finite state machines and/or behavior trees, the typical logic would be to just check if the villager stats reach a specific number before it performs a certain action. If hunger falls below x, then perform eat. If energy falls below y, then go sleep. And so on. This leads to very simple and predictable behavior, and the code (or graphs if you're using premade tools) can get pretty nasty as you add more and more behaviors.

With Utility AI, the villager can instead compare how urgent working, eating, and sleeping are to each other by looking at various game data in the world -- time of day, how much money does it have compared to a specific item at a merchant store, how far away is the food market, etc. Utility AI gives you the freedom to define what certain information means to the NPC, and the NPC will use that information to determine what action it should perform next. Because the game world is constantly evolving with time, the NPC will be able to make decisions dynamically and possibly give rise to interesting emergent behaviors you might not expect at first glance.

## How is this Utility AI is set up?
I go over this in depth in [Part 1](https://www.youtube.com/watch?v=ejKrvhusU1I). The NPC is given a set of possible actions it can perform. Every action has a list of considerations that determine how important that action is relative to all other actions. Each consideration is a way to score in-game data (e.g. NPC hunger, energy level, wealth, time of day, etc.) to see how much that in-game data affects the importance of the action. The list of consideration scores for one action will be combined to give an overall score for that action. Every decision cycle, the NPC will go down the list of possible actions and calculate a score for each action. The highest scoring action will then be performed.

## What's the architecture for this prototype Utility AI?
When creating this prototype, I wanted the user to be able to just drag and drop defined actions onto an `AIBrain` monobehavior then hit run. To accomplish this, I built this AI around using scriptable objects. The core classes are described below. 

### Consideration : ScriptableObject
`Consideration` is the parent class for all game information that is needed by the NPC to evaluate the urgency of an action. For example, Hunger level would be the game information needed to evaluate the urgency of the Eat action. So create a consideration called `HungerConsideration` that inherits from `Consideration` and override the `ScoreConsideration()` method with the logic that will score the NPC's hunger level.

```csharp
[CreateAssetMenu(fileName = "HungerConsideration", menuName = "UtilityAI/Considerations/Hunger Consideration")]
public class HungerConsideration : Consideration {

	public override float ScoreConsideration(NPCController npc) {
		//implement scoring logic here
		return score;
	};
}
```

#### How should I score a consideration?
Typically, considerations are all scored on a scale of 0 to 1. In other words, all your relevant game data should be converted from their original in-game numbers to a score between 0 and 1. This is necessary in order to compare apples to apples. An NPC's Hunger Level of 75 compared to its energy value of 16 doesn't mean anything useful. However, if we divide them by their maximum values, then we can start comparing apples to apples. Say the maximum Hunger value is 100. Then the `HungerConsideration` would give a score of 0.75. Say the NPC has a maximum energy value of 200. Then the `EnergyConsideration` would give a score of 16/200 = 0.08. 0.08 is much smaller than 0.75, which tells the NPC that it probably needs to go sleep to replenish that energy instead of going to eat something! A more in-depth explanation can be found in the tutorial's [Part 5](https://www.youtube.com/watch?v=sISJdLO3JYM "Part 5").

### Action : ScriptableObject
`Action` is the parent class for all actions you want your NPC to perform. Action inherits from ScriptableObject, so any actions you create will be scriptable objects that you can drag and drop onto the AIBrain list of possible actions. Simply inherit from this parent class and override the `Execute()` method where you can implement the code to run your action (e.g. play an animation, change a stat, destroy gameobject, etc). 

```csharp
[CreateAssetMenu(fileName = "Sleep", menuName = "UtilityAI/Actions/Sleep")]
public class Sleep : Action {

	public override void Execute(NPCController npc) {
		//play sleep animation
		//increase energy meter
	};
}
```

Every action you define will have a list of considerations that the AIBrain can use to evaluate how important that action is at the moment. It is important for you to decide what game data is important for the NPC to consider for an action. For example, the Sleep Action might need to consider the NPC's energy level, the time of day, and if it is still at work. That means, create 3 considerations, implement their scoring logic, then drag their scriptable objects onto the Sleep Action scriptable object.

### AIBrain : Monobehaviour
This monobehavior is where you drop all your definded action scriptable objects. `AIBrain` is responsible for performing the core Utility AI calculations, which are scoring considerations and ranking actions based on their scores. The logic is explained here in [Part 3](https://www.youtube.com/watch?v=c23PJLSNYXs "Part 3").

### NPCController : Monobehaviour
This monobehavior is the central place where the Utility AI is kickstarted using in-game data obtained through the NPC's perception of the world. You might have visual sensors, audio sensors, knowledge-base references all in `NPCController` so that the Utility AI can get the information it needs for decision-making. Notice that `Action` and `Consideration` calls `NPCController` for use in their `Execute()` and `ScoreConsideration()` methods.

## How does this prototype AI handle NPC movement?
In the project, I implemented a very simple FSM to control movement. The FSM has 3 states:
1. Decide State
	- Utility AI decision-making process
	- Select a target destination based on the best action that was selected
2. Move State
	- Move to your selected target destination
3. Execute State
	- Run the `Execute()` method of the best action that was selected
