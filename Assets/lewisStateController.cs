using Scripts;
using Unity.VisualScripting;
using UnityEngine;

public class lewisStateController : MonoBehaviour
{

    Animator animator;
    public Camera playerCamera;
    [SerializeField] private Chatbot chatGPTManager;
    private bool playerIsClose = false;

    // public float yOffset = 1.4f;
    // public float speed = 0.01f;
    public float playerInteractionDistance = 5.0f;

    private Vector3 initialDirection;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        initialDirection = transform.forward;
    }

    // Update is called once per frame
    void Update()
    {
        var distanceToPlayer = Vector3.Distance(playerCamera.transform.position, transform.position);
        // var playerDirection = playerCamera.transform.position - transform.position;
        if (distanceToPlayer < playerInteractionDistance)
        {
            playerIsClose = true;
        }
        else
        {
            // player was close, but is not anymore
            if (playerIsClose) {
                chatGPTManager.ClearMessages();
            }
            playerIsClose = false;
        }
    }
}
