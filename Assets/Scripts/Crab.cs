using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crab : MonoBehaviour
{
    public GeneralController general;
    public float speed;

    private bool isLevitating = false;
    private RaycastHit hit;

    private Rigidbody rb;
    private Collider collidr;

    private bool touched;

    private void Start()
    {
        collidr = GetComponent<Collider>();
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (Application.isEditor)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Vector2 touchposition = Input.mousePosition;
                Ray ray = Camera.main.ScreenPointToRay(touchposition);
                if (Physics.Raycast(ray, out hit) && hit.collider.gameObject == gameObject)
                {
                    if (!isLevitating)
                    {
                        general.touches.blocked = true;
                        Debug.Log("crab touched");
                        general.crabstodelete.Add(rb);
                        rb.isKinematic = true;
                        StartLevitation();

                    }
                }
            }
            }

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(touch.position);
                if (Physics.Raycast(ray, out hit) && hit.collider.gameObject == gameObject)
                {
                    if (!isLevitating)
                    {
                        general.crabstodelete.Add(rb);
                        general.touches.blocked = true;
                        Debug.Log("crab touched");
                        rb.isKinematic = true;
                        StartLevitation();
                    }
                }
            }
        }

        if (isLevitating)
        {
            Levitate();
        }
    }

    void StartLevitation()
    {
        isLevitating = true;
    }

    void Levitate()
    {
        if (rb.isKinematic)
        {
            float lerpValue = Mathf.PingPong(Time.time * 0.5f, 1);
            float targetValue = Mathf.Lerp(0.7f, 0.71f, lerpValue);
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, new Vector3(transform.localPosition.x, targetValue, transform.localPosition.z), speed * Time.deltaTime);

        }
        else
        {
            isLevitating = false;
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Sea"))
        {
            general.crabDestroyed(gameObject);
            Destroy(gameObject);
        }
        if (collision.gameObject.CompareTag("Ground"))
        {
            rb.velocity = Vector3.zero;
        }
    }
}
