using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance { get; private set; }

    // UI references
    public GameObject DialogueParent; // Main container for dialogue UI
    public TextMeshProUGUI DialogTitleText, DialogBodyText; // Text components for title and body
    public GameObject responseButtonPrefab; // Prefab for generating response buttons
    public Transform responseButtonContainer; // Container to hold response buttons
    public BusMovement busMovement;
    public int romancePoints; //total romance points given

    private void Awake()
    {
        // Singleton pattern to ensure only one instance of DialogueManager
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        // Initially hide the dialogue UI
        HideDialogue(null);
    }

    // Starts the dialogue with given title and dialogue node
    public void StartDialogue(string title, DialogueNode node, GameObject CharaterImg)
    {
        // Display the dialogue UI
        ShowDialogue(CharaterImg);

        // Set dialogue title and body text
        DialogTitleText.text = title;
        DialogBodyText.text = node.dialogueText;

        // Remove any existing response buttons
        foreach (Transform child in responseButtonContainer)
        {
            Destroy(child.gameObject);
        }

        // Create and setup response buttons based on current dialogue node
        foreach (DialogueResponse response in node.responses)
        {
            GameObject buttonObj = Instantiate(responseButtonPrefab, responseButtonContainer);
            buttonObj.GetComponentInChildren<TextMeshProUGUI>().text = response.responseText;

            // Setup button to trigger SelectResponse when clicked
            buttonObj.GetComponent<Button>().onClick.AddListener(() => SelectResponse(response, title, CharaterImg));
        }
    }

    // Handles response selection and triggers next dialogue node
    public void SelectResponse(DialogueResponse response, string title, GameObject CharaterImg)
    {
        // Check if there's a follow-up node
        if (!response.nextNode.IsLastNode())
        {
            romancePoints += response.responseValue;
            StartDialogue(title, response.nextNode, CharaterImg); // Start next dialogue
        }
        else
        {
            romancePoints += response.responseValue;
            // If no follow-up node, end the dialogue
            HideDialogue(CharaterImg);
        }
    }

    // Hide the dialogue UI
    public void HideDialogue(GameObject CharaterImg)
    {
        busMovement.move = true;
        DialogueParent.SetActive(false);
        CharaterImg.SetActive(false);
    }

    // Show the dialogue UI
    private void ShowDialogue(GameObject CharaterImg)
    {
        DialogueParent.SetActive(true);
        CharaterImg.SetActive(true);
    }

    // Check if dialogue is currently active
    public bool IsDialogueActive()
    {
        return DialogueParent.activeSelf;
    }
    public void ResetRomancePoints()
    {
        romancePoints = 0;
    }
}