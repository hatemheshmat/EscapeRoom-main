# EscapeRoom-main
EscapeRoom Session 10


The video provided is a software tutorial for the **Unity Game Engine**. It teaches a specific design pattern called **"Scriptable Objects"**.

This manual converts that video into a comprehensive step-by-step guide. Since the user asked for "wiring," please note that in Unity, **"wiring" means connecting scripts and assets in the Inspector window** (dragging and dropping files), not physical electrical wiring.

-----

# Student Task: Unity Scriptable Objects

**Project Goal:** Create a shared data container so multiple objects in a game (like two different Cubes) can share the same settings (Jump Force and Acceleration). Changing the setting in one place updates everyone instantly.

-----

## Phase 1: The "Hardware" Setup (Scene Creation)

*Think of this as building the physical box for your project.*

1.  **Open Unity** and create a new **3D Project**.
2.  **Create the Floor:**
      * Right-click in the **Hierarchy** window (left side).
      * Select **3D Object** -\> **Plane**.
      * (Optional) Scale it up to `(2, 1, 2)` in the Inspector so it's big.
3.  **Create the Cubes:**
      * Right-click in Hierarchy -\> **3D Object** -\> **Cube**. Name it `Cube A`.
      * Right-click in Hierarchy -\> **3D Object** -\> **Cube**. Name it `Cube B`.
      * Move them apart using the Move Tool (press `W` key) so they aren't touching.
4.  **Add Physics:**
      * Select both `Cube A` and `Cube B`.
      * In the **Inspector** (right side), click **Add Component**.
      * Search for **Rigidbody** and select it. (This gives them gravity).

-----

## Phase 2: The "chip" (Scriptable Object Script)

*This is the special data container. It is not a script you put on a generic object; it is a blueprint for a data file.*

1.  **Create the Script:**
      * In the **Project** window (bottom), Right-click -\> **Create** -\> **C\# Script**.
      * **CRITICAL:** Name it exactly `CubeJumpData`. (The filename must match the class name inside).
2.  **Write the Code:**
      * Double-click the script to open it. Delete everything and paste this:

<!-- end list -->

```csharp
using UnityEngine;

// This line adds an entry to your Right-Click menu in Unity
[CreateAssetMenu(fileName = "NewJumpData", menuName = "ScriptableObjects/CubeJumpData", order = 1)]
public class CubeJumpData : ScriptableObject // Note: Inherits from ScriptableObject, NOT MonoBehaviour
{
    // These are the "wires" or dials we can twist
    public float jumpForce = 5f;
    public float acceleration = 1f;
}
```

  * **Explanation:**
      * `ScriptableObject`: Tells Unity this is a data asset, not a component for a character.
      * `[CreateAssetMenu]`: This creates the button in the Unity menu that lets us manufacture this "chip".

-----

## Phase 3: The "Logic Board" (Behavior Script)

*This is the script that tells the Cubes how to read the data from the chip we just made.*

1.  **Create the Script:**
      * In the **Project** window, Right-click -\> **Create** -\> **C\# Script**.
      * Name it `CubeBehavior`.
2.  **Write the Code:**
      * Double-click and paste this:

<!-- end list -->

```csharp
using UnityEngine;

public class CubeBehavior : MonoBehaviour
{
    // 1. INPUT SLOT: This creates a slot in the Inspector to plug in our Data Asset
    public CubeJumpData jumpData; 

    private Rigidbody rb;

    void Start()
    {
        // Get the physics component attached to this cube
        rb = GetComponent<Rigidbody>();
        
        // Use the data from our "chip" to perform an action
        PerformJump();
    }

    void PerformJump()
    {
        // Safety Check: Ensure the wire is connected!
        if (jumpData == null)
        {
            Debug.LogError("No Jump Data connected to " + gameObject.name);
            return;
        }

        // READ data from the Scriptable Object
        // Notice we don't type "5" or "10". We ask "jumpData" what the value is.
        Vector3 upwardForce = Vector3.up * jumpData.jumpForce;
        
        // Apply physics
        rb.AddForce(upwardForce, ForceMode.Impulse);
        
        Debug.Log(gameObject.name + " jumped with force: " + jumpData.jumpForce);
    }
}
```

-----

## Phase 4: The "Wiring" (Connecting it all)

*This is the most critical step where beginners get stuck. We will connect the Data to the Objects.*

### Step A: Manufacture the Data Chip

1.  Go to your **Project Window**.
2.  Right-click in the empty space.
3.  Hover over **Create**.
4.  You will see a new category (because of the code we wrote in Phase 2). Look for **ScriptableObjects** -\> **CubeJumpData**.
5.  Click it. A new file appears. Name it `SuperJumpProfile`.
6.  Click on `SuperJumpProfile`. In the Inspector, you will see the `Jump Force` and `Acceleration` fields. Set `Jump Force` to **10**.

### Step B: Wire the Cubes

1.  Select `Cube A` in the Hierarchy.
2.  Drag the `CubeBehavior` script onto `Cube A`.
3.  Look at the **Cube Behavior** component in the Inspector. You will see a field named **Jump Data** that says "None (Cube Jump Data)".
4.  **The Wiring:** Drag your `SuperJumpProfile` file from the Project window into that "None" slot.
5.  Repeat this for `Cube B`.

-----

## Phase 5: Testing & Live Tuning

1.  Press the **Play Button** (Top center).
2.  Both cubes will jump using the force of **10** (defined in your asset).
3.  **The Magic Trick:**
      * Keep the game playing. Don't stop it.
      * Click on your `SuperJumpProfile` file in the Project window.
      * Change `Jump Force` to **20**.
      * (If you added a repeating jump logic, they would instantly jump higher. Since our code only jumps on Start, stop the game and start it again).
      * *Note: In the video, the user implies real-time updates. If you want real-time updates without restarting, change `void Start()` to `void Update()` and use a key press like `if (Input.GetKeyDown(KeyCode.Space)) PerformJump();`.*

## Summary of Why We Did This

  * **No Spagetti Code:** `Cube A` does not know `Cube B` exists. They only know about the `SuperJumpProfile`.
  * **Safety:** You can delete `Cube B`, and `Cube A` will keep working perfectly.
  * **Persistence:** If you change the numbers in the `SuperJumpProfile` while playing, **it saves**\! (Normal scripts reset when you stop playing; Scriptable Objects remember your changes).


Here is the **completely updated Student Manual**.

I have rewritten **Phase 3** and **Phase 5** so the default behavior is now "Press Space to Jump." This allows you to test the "Live Tuning" features immediately without modifying code later.

-----

# (Real-Time Edition)

**Project Goal:** Create two cubes that share the same "Jump Data" chip. We will make them jump by pressing **Space**, allowing us to change their jump height while the game is running to see instant updates.

-----

## Phase 1: The "Hardware" Setup (Scene Creation)

*Think of this as building the physical box for your project.*

1.  **Open Unity** and create a new **3D Project**.
2.  **Create the Floor:**
      * Right-click in the **Hierarchy** window (left side).
      * Select **3D Object** -\> **Plane**.
      * In the Inspector, set Scale to `(2, 1, 2)` so it is big.
3.  **Create the Cubes:**
      * Right-click in Hierarchy -\> **3D Object** -\> **Cube**. Name it `Cube A`.
      * Right-click in Hierarchy -\> **3D Object** -\> **Cube**. Name it `Cube B`.
      * Use the **Move Tool** (press `W`) to separate them so they aren't touching.
4.  **Add Physics:**
      * Select **both** `Cube A` and `Cube B`.
      * In the **Inspector** (right side), click **Add Component**.
      * Search for **Rigidbody** and click it. (This adds gravity).

-----

## Phase 2: The "Chip" (Scriptable Object Script)

*This is the blueprint for our data file. It is NOT a script that goes on an object.*

1.  **Create the Script:**
      * In the **Project** window (bottom), Right-click -\> **Create** -\> **C\# Script**.
      * **NAME IT EXACTLY:** `CubeJumpData` (Case sensitive\!).
2.  **Write the Code:**
      * Double-click the script. Delete all code. Paste this:

<!-- end list -->

```csharp
using UnityEngine;

// This line adds a shortcut to the Unity Right-Click menu
[CreateAssetMenu(fileName = "NewJumpData", menuName = "ScriptableObjects/CubeJumpData", order = 1)]
public class CubeJumpData : ScriptableObject 
{
    // These are the settings we want to share
    public float jumpForce = 10f;
    public float acceleration = 5f;
}
```

-----

## Phase 3: The "Logic Board" (Behavior Script)

*This script goes on the Cubes. It tells them to listen for the Spacebar and read the data from the chip.*

1.  **Create the Script:**
      * In the **Project** window, Right-click -\> **Create** -\> **C\# Script**.
      * Name it `CubeBehavior`.
2.  **Write the Code:**
      * Double-click and paste this **updated code**:

<!-- end list -->

```csharp
using UnityEngine;

public class CubeBehavior : MonoBehaviour
{
    // 1. INPUT SLOT: We create a slot in the Inspector to drag our data file into
    public CubeJumpData jumpData; 

    private Rigidbody rb;

    void Start()
    {
        // Find the Rigidbody attached to this specific cube
        rb = GetComponent<Rigidbody>();
    }

    // Update runs every single frame (approx. 60 times a second)
    void Update()
    {
        // 2. TRIGGER: If the user presses Space, try to jump
        if (Input.GetKeyDown(KeyCode.Space))
        {
            PerformJump();
        }
    }

    void PerformJump()
    {
        // Safety Check: Did we forget to plug the wire in?
        if (jumpData == null)
        {
            Debug.LogError("Hey! You forgot to attach the Jump Data to " + gameObject.name);
            return;
        }

        // 3. READ DATA: We ask the external file "How high should I jump?"
        Vector3 upwardForce = Vector3.up * jumpData.jumpForce;
        
        // Reset speed so we don't fly into space if we spam the button
        rb.velocity = Vector3.zero; 

        // Apply the physical push
        rb.AddForce(upwardForce, ForceMode.Impulse);
    }
}
```

-----

## Phase 4: The "Wiring" (Connecting it all)

*This is where we connect the code to the objects.*

### Step A: Manufacture the Data Chip

1.  Go to your **Project Window**.
2.  Right-click in empty space -\> **Create**.
3.  Select **ScriptableObjects** -\> **CubeJumpData**. (This option exists because of the code in Phase 2).
4.  
5.  Name the new file `SuperJumpProfile`.
6.  Click on `SuperJumpProfile`. In the Inspector, verify `Jump Force` is **10**.

### Step B: Wire the Cubes

1.  Select `Cube A` in the Hierarchy.
2.  Drag the `CubeBehavior` script onto `Cube A` in the Inspector.
3.  **The Wiring:** You will see a slot named **Jump Data** that says "None". Drag your `SuperJumpProfile` file into that slot.
4.  
5.  Repeat these steps for `Cube B`.

-----

## Phase 5: Testing & Live Tuning (The Final Exam)

*Now we see why this method is better than normal variables.*

1.  Press the **Play Button** at the top.
2.  **Action:** Press the **Space Bar**.
      * *Result:* Both cubes jump slightly.
3.  **Live Tuning (The Magic Trick):**
      * **Do not stop the game.** Keep it playing.
      * Click on the `SuperJumpProfile` file in your Project window.
      * Change the **Jump Force** from `10` to `30`.
      * Click back on the Game Screen.
4.  **Action:** Press the **Space Bar** again.
      * *Result:* Both cubes now jump **three times higher** instantly.

_____________________________________________________


-----

# Student Task 2: The "Scene Runner" Project

**Project Goal:** Create a 4-scene loop. The player enters their name, plays through two levels by hitting objects, and sees a personalized "You Win" screen at the end.

## 🛠️ Prerequisites

  * **Concepts:** `SceneManager`, `PlayerPrefs` (Unity's way of saving simple data), Colliders.
  * **Assets:** Just basic Cubes, Planes, and UI.

-----

## Phase 1: The "Map" (Scene Setup)

*Unity doesn't know your scenes exist until you register them.*

1.  **Create 4 Scenes:**
      * In the **Project Window**, Right-click -\> **Create** -\> **Scene**.
      * Name them exactly: `Menu`, `Level1`, `Level2`, `WinScreen`.
2.  **Register them in Build Settings:**
      * Go to **File** (top left) -\> **Build Settings**.
      * Drag all 4 scenes from your Project Window into the "Scenes In Build" list.
      * **Crucial Order:** Ensure they are ordered:
          * 0: `Menu`
          * 1: `Level1`
          * 2: `Level2`
          * 3: `WinScreen`

-----

## Phase 2: Scene 1 (The Menu & Input)

*Goal: Save the user's name and switch to the game.*

1.  **Open Scene:** Double-click `Menu`.
2.  **Create the UI:**
      * Right-click in Hierarchy -\> **UI** -\> **Input Field - TextMeshPro** (Import TMP Essentials if asked).
      * Right-click in Hierarchy -\> **UI** -\> **Button - TextMeshPro**.
3.  **The Script (MenuManager):**
      * Create a script named `MenuManager`.
      * Paste this code:

<!-- end list -->

```csharp
using UnityEngine;
using UnityEngine.SceneManagement; // Needed to change scenes
using TMPro; // Needed to read the Input Field

public class MenuManager : MonoBehaviour
{
    public TMP_InputField nameInput;

    public void StartGame()
    {
        // 1. SAVE DATA: Store the text typed in the box into a key called "SavedName"
        PlayerPrefs.SetString("SavedName", nameInput.text);
        
        // 2. CHANGE SCENE: Load the next scene (Level1)
        SceneManager.LoadScene("Level1");
    }
}
```

4.  **Wiring:**
      * Create an **Empty GameObject** (Right-click -\> Create Empty), name it `Manager`.
      * Drag the `MenuManager` script onto `Manager`.
      * Drag your **Input Field** from the Hierarchy into the **Name Input** slot in the Inspector.
      * Select your **Button**. In the Inspector, scroll to **On Click ()**.
      * Click `+`. Drag the `Manager` object into the slot.
      * Select the function: `MenuManager` -\> `StartGame`.

-----

## Phase 3: The Player Movement (Shared Script)

*We need a simple script to move the player in Level 1 and 2.*

1.  Create a script named `SimpleMove`.
2.  Paste this code:

<!-- end list -->

```csharp
using UnityEngine;

public class SimpleMove : MonoBehaviour
{
    public float speed = 10f;

    void Update()
    {
        // Basic arrow key movement
        float x = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        float z = Input.GetAxis("Vertical") * speed * Time.deltaTime;

        transform.Translate(x, 0, z);
    }
}
```

-----

## Phase 4: Scene 2 (Level 1)

*Goal: Move player to hit a cube to advance.*

1.  **Open Scene:** Double-click `Level1`.
2.  **Setup World:**
      * Add a **Plane** (Floor).
      * Add a **Cube** (The Player). Tag it as "Player" (optional, but good practice).
      * Add the `SimpleMove` script to the Player Cube.
3.  **The Portal (The Trigger):**
      * Add another **Cube**. Color it Red (create a material if you want).
      * **CRITICAL:** In the Box Collider component, check the box **Is Trigger**. (This makes it a ghost you can walk through).
4.  **The Script (LevelLoader):**
      * Create a script `LevelLoader`.
      * Paste this code:

<!-- end list -->

```csharp
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public string nextSceneName; // We can type the scene name in the Inspector

    // This runs when something enters the "Is Trigger" zone
    void OnTriggerEnter(Collider other)
    {
        // Load whatever name we wrote in the Inspector
        SceneManager.LoadScene(nextSceneName);
    }
}
```

5.  **Wiring:**
      * Drag `LevelLoader` onto your **Red Portal Cube**.
      * In the Inspector, find "Next Scene Name" and type `Level2`.

-----

## Phase 5: Scene 3 (Level 2)

*Goal: Same logic, different destination.*

1.  **Open Scene:** Double-click `Level2`.
2.  **Shortcut:**
      * Setup the Plane and Player Cube with `SimpleMove` just like before.
      * Create the Red Portal Cube (Ensure **Is Trigger** is on).
      * Add the `LevelLoader` script to the Portal.
3.  **The Change:**
      * In the "Next Scene Name" slot on the script, type `WinScreen`.

-----

## Phase 6: Scene 4 (Win Screen)

*Goal: Retrieve the saved name and display it.*

1.  **Open Scene:** Double-click `WinScreen`.
2.  **Create UI:**
      * Right-click -\> **UI** -\> **Text - TextMeshPro**.
      * Make it big and center it.
3.  **The Script (WinDisplay):**
      * Create a script named `WinDisplay`.
      * Paste this code:

<!-- end list -->

```csharp
using UnityEngine;
using TMPro;

public class WinDisplay : MonoBehaviour
{
    public TMP_Text messageText;

    void Start()
    {
        // 1. RETRIEVE DATA: Get the name we saved in Scene 1
        // If no name was saved, default to "Player"
        string username = PlayerPrefs.GetString("SavedName", "Player");

        // 2. DISPLAY IT
        messageText.text = "YOU WIN, " + username + "!";
    }
}
```

4.  **Wiring:**
      * Attach `WinDisplay` to your **Text** object (or an empty manager).
      * Drag the **Text** object itself into the **Message Text** slot.

-----

## Final Checklist for the Student

1.  **Play Scene 1:** Type "SuperStudent". Click Button.
2.  **Level 1:** Move cube into the portal. -\> *Should load Level 2.*
3.  **Level 2:** Move cube into the portal. -\> *Should load WinScreen.*
4.  **WinScreen:** Should read "YOU WIN, SuperStudent\!".

### Why this is powerful

You just learned how to **pass data across time and space**. Scene 1 and Scene 4 never touch each other, but `PlayerPrefs` acts as a bridge to carry the information (the name) across the gap.
