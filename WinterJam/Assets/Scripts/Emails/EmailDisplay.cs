using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EmailDisplay : MonoBehaviour {
    // [SerializeField] public LineRenderer lineRenderer;
    [SerializeField] private GameObject mapPoint;

    [SerializeField] private Text headerText;
    [SerializeField] private Text senderText;
    [SerializeField] private Text bodyText;

    /*
    // May switch to implementation with arrays if optimization is needed
    [SerializeField]
    private List<GameObject> cornerDestinations = new List<GameObject>();
    [SerializeField]
    private List<double?> cornerDestinationDistances = new List<double?>() { 10000, 10000, 10000, 10000};
    [SerializeField]
    private List<GameObject> corners = new List<GameObject>();

    private GameObject mainCorner;
    */

    [SerializeField] private Animator animator;

    [SerializeField] private Button openButton;
    [SerializeField] private Button closeButton;
    [SerializeField] private Button deleteButton;
    [SerializeField] private Button replyButton;
    [SerializeField] private Button yesButton;
    [SerializeField] private Button noButton;

    [SerializeField] private Sprite yesImage;
    [SerializeField] private Sprite noImage;

    public Email emailData;

    public void LoadFromEmail() {
        headerText.text = emailData.Title;
        senderText.text = emailData.Sender;
        bodyText.text = emailData.BodyText;
    }

    // Start is called before the first frame update
    void Start() {
        // lineRenderer.positionCount = 2;
    }

    // Update is called once per frame
    void Update() {
        
    }

    // Open the email
    public void Open() {
        openButton.interactable = false;
        closeButton.interactable = true;
        // SelectMainCorner();
        animator.SetBool("EmailOpen", true);
        // StartCoroutine(Open(10 / 60));
    }

    /*
    private IEnumerator Open(float seconds) {
        // Wait for animation to move to viewable position
        yield return new WaitForSeconds(seconds);
        // lineRenderer.enabled = true;

        // Update position of line renderer every frame
        for (int i = 0; i < 20; i++) {
            // UpdateLineVertices();
            yield return new WaitForSeconds(1 / 60);
        }
    }
    */

    // Close the email
    public void Close() {
        openButton.interactable = true;
        closeButton.interactable = false;
        // Start animation to close and disable line renderer
        animator.SetBool("EmailOpen", false);
        // lineRenderer.enabled = false;
    }

    public void Delete() {
        replyButton.interactable = false;
        closeButton.interactable = false;
        deleteButton.interactable = false;
        Close();
        GameManager.EnvironmentScore += emailData.IgnoreScore;
        StartCoroutine(waitBeforeDeletion());
    }

    public void Reply() {
        replyButton.interactable = false;
        closeButton.interactable = false;
        deleteButton.interactable = false;
        // Open window
        animator.SetBool("Reply", true);
    }

    public void Yes() {
        yesButton.gameObject.GetComponent<Image>().sprite = yesImage;
        yesButton.interactable = false;
        noButton.interactable = false;
        GameManager.EnvironmentScore += emailData.AcceptScore;
        StartCoroutine(waitAfterReply());
    }

    public void No() {
        noButton.gameObject.GetComponent<Image>().sprite = noImage;
        yesButton.interactable = false;
        noButton.interactable = false;
        GameManager.EnvironmentScore += emailData.DenyScore;
        StartCoroutine(waitAfterReply());
    }

    private IEnumerator waitAfterReply() {
        yield return new WaitForSeconds(1f);
        Close();
        StartCoroutine(waitBeforeDeletion());
    }

    private IEnumerator waitBeforeDeletion() {
        animator.SetBool("Deleting", true);
        yield return new WaitForSeconds(2f);
        Destroy(transform.parent.gameObject);
    }

    /*
    public void UpdateLineVertices() {
        // Set the vertices of the line renderer to the corner and map point
        Vector3[] vertices = new Vector3[] { mainCorner.transform.position, mapPoint.transform.position };
        lineRenderer.positionCount = 2;
        lineRenderer.SetPositions(vertices);
    }

    // Choose which corner of the email is the closest to the map point when opened
    public void SelectMainCorner() {
        // Default to top left corner
        int closestCorner = 0;

        for (int i = 0; i < 4; i++) {
            // Calculate distance of the corner from the map point
            cornerDestinationDistances[i] = Vector3.Distance(cornerDestinations[i].transform.position, mapPoint.transform.position);
            // Make corner main corner if closer
            if (cornerDestinationDistances[closestCorner] > cornerDestinationDistances[i]) {
                closestCorner = i; 
            }
        }

        mainCorner = corners[closestCorner];
    }
    */

    // Unused; use to "type out" text
    /*
    public IEnumerator TypeText(string sentence, Text textDisplay) {
        textDisplay.text = "";
        char[] _sentence_array = sentence.ToCharArray();
        for (int i = 0; i < _sentence_array.Length; i++) {
            char letter = _sentence_array[i];
            textDisplay.text += letter;
            if (i % 2 == 1) {
                yield return null;
            }
        }
    }
    */
}
