# Agent Behavior Simulation (Bachelor Project)

This project is a Unity-based simulation focused on artificial intelligence and decision-making for Non-Player Characters (NPCs). It implements modular AI architectures, specifically Behavior Trees (BT) and Finite State Machines (FSM), combined with an interaction system and personality traits to create believable agent behaviors.



## 🎮 Live Demo

You can play the web-based demo directly in your browser here:
**[Play the Demo on GitHub Pages](https://phoeningurix.github.io/Bachelor-Project/)**

### Controls & How to Play
* **Select an Agent:** Click on any agent (Blob) to select it.
* **View Info:** Once an agent is selected, you can view its personality traits and current emotional state in the Info Bar.
* **Deselect:** Press `Escape` to clear your selection.
* **Scene Navigation:**
    * `n` - Go to the next scene.
    * `b` - Go to the previous scene.
    * `l` - Skip to the last scene in the build list.

---

## 🚀 Quickstart & Installation

To run or edit this project locally, you will need the Unity Game Engine.

**Prerequisites:**
* **Unity Version:** `6000.2.8f1` (Highly recommended to avoid serialization or package issues).
* **Git** installed on your machine.

**Installation Steps:**
1. Clone the repository to your local machine:
   ```bash
   git clone https://github.com/Phoeningurix/Bachelor-Project.git
   ```
2. Open Unity Hub, click on **Add project from disk**, and select the cloned repository folder.
3. Open the project using Unity `6000.2.8f1`.
4. Navigate to the `Scenes` folder in your Unity Project window and open the first scene to begin testing.

---

## 📁 Project Structure

The codebase is highly modular, separating AI logic, visual rendering, and state management. Here is a brief overview of the core directories:

* **`AgentLogic/`**: The core of the AI system.
    * **`AgentActions/`**: Contains the individual atomic tasks the agents can perform (e.g., Wander, PickUpObject, AnswerRequest).
    * **`BehaviorTree/` & `FSM/`**: The structural implementations of the Behavior Tree (Sequence, Selector, Weighted Nodes) and Finite State Machine architectures.
    * **`AgentBehaviorSuppliers/`**: ScriptableObject suppliers that allow designers to construct and assign specific AI behaviors via the Unity Inspector.
    * **`Blackboard.cs` & `BlobBrain.cs`**: The memory and central controller for the agents.
* **`Interactions/`**: Defines how agents interact with the world and each other (e.g., `AppleTreeInteractable`, `BlobInteractions`).
* **`Managers/`**: Global controllers handling the overall game state (`GameManager`) and user inputs (`BlobSelectionManager`).
* **`Renderers/` & `UI/`**: Handles the visual representation of agents, environment objects, floating speech bubbles, and the UI Info Bar.
* **`Utils/`**: Helper scripts, including a custom implementation of the Observer pattern (`ObservableValue`, `ObservableFloatRegistry`) to decouple data changes from UI updates.
* **`Editor/`**: Custom Unity Inspector scripts to make debugging and tweaking values easier for developers.
* **`Testing/`**: Isolated scripts used for testing specific mechanics like movement or manual emotion adjustments.

---

(This README.md was generated with Gemini 3.1 Pro)
