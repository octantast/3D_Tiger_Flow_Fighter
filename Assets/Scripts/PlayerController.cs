using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GeneralController general;
    public TouchController touches; 

    public float moveSpeed;
    private CharacterController characterController;
    public Rigidbody rb;
    public bool ismoving;
    public Vector3 targetPosition;

    public int currentplatform;

    public Animator animator;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        characterController = GetComponent<CharacterController>();
    }
    void FixedUpdate()
    {
        if (ismoving)
        {
            MovePlayer();
        }
    }
    public void MovePlayer()
    {
        //transform.position = targetPosition;

        Vector3 newPosition = Vector3.Lerp(rb.position, targetPosition, moveSpeed * Time.deltaTime);
        rb.MovePosition(newPosition);
        if (Mathf.Abs(rb.position.x - targetPosition.x) < 0.02f)
        {
          //  rb.position = targetPosition;
               ismoving = false;


            if (general.ui.tutorial1 == 0 && Mathf.Abs(rb.position.x - general.lastwaveHolder.transform.position.x) < 0.3f)
            {
                general.ui.tutorial1 = 1;
                PlayerPrefs.SetInt("tutorial1", 1);
                PlayerPrefs.Save();
                general.ui.tipAnimator.enabled = false;
                general.ui.tipAnimator.Play("Tutor1");
                general.ui.tipAnimator.enabled = true;
            }
        }
    }
    public void OnCollisionEnter(Collision collision)
    {
       
        if (collision.gameObject.CompareTag("Ground"))
        {
            rb.velocity = Vector3.zero;
        }
    }
}
