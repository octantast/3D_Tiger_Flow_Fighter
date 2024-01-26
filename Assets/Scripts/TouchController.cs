using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;
public class TouchController : MonoBehaviour
{
    public PlayerController player;
    public GeneralController general;
    public Vector2 touchPos;
    public Vector2 touchPosWorld;

    private Vector2 swipeStartPos;

    public bool blocked;
    public bool touchbegan;
    public bool touchcontinues;

    public void Update()
    {
        if (!general.paused)
        {
            touchInput();
        }
    }

   public void touchInput()
    {
        //float screenWidth = Screen.width;
        //float screenHeight = Screen.height;

        if (Application.isEditor)
        {
            if (Input.GetMouseButtonDown(0))
            {
                touchPos = Input.mousePosition;
                touchPosWorld = Camera.main.ScreenToWorldPoint(touchPos);
                swipeStartPos = touchPos;
                startTouch();
            }
            if (Input.GetMouseButton(0))
            {
                touchPos = Input.mousePosition;
                touchPosWorld = Camera.main.ScreenToWorldPoint(touchPos);
                continueTouch();
            }
            if (Input.GetMouseButtonUp(0))
            {
                endTouch();
            }

        }

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:

                    touchPos = touch.position;
                    touchPosWorld = Camera.main.ScreenToWorldPoint(touchPos);
                    swipeStartPos = touchPos;
                    startTouch();
                    break;
                case TouchPhase.Moved:
                    touchPos = touch.position;
                    touchPosWorld = Camera.main.ScreenToWorldPoint(touchPos);
                    continueTouch();
                    break;
                case TouchPhase.Ended:
                    endTouch();

                    break;

            }
        }
    }

    public void startTouch()
    {
        if (!general.paused)
        {
            if (!blocked)
            {
                touchbegan = true;
                touchcontinues = false;

                Ray ray = Camera.main.ScreenPointToRay(touchPosWorld);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit) && hit.collider.gameObject.tag == "Crab")
                {
                    Debug.Log("Crab Tap");
                    blocked = true;
                }
                else
                {

                    // crabs
                    if (general.crabstodelete.Count > 0)
                    {
                        blocked = true;
                        foreach (Rigidbody crab in general.crabstodelete)
                        {
                            crab.isKinematic = false;
                            crab.AddForce(transform.forward * 2 + transform.up, ForceMode.Impulse);
                        }
                        general.ui.currentScore += general.crabstodelete.Count * general.ui.scoreIndex2;

                        general.crabstodelete.Clear();
                    }
                }
            }
        }
    }
    public void continueTouch()
    {
        if (!general.paused)
        {
            touchbegan = false;
            touchcontinues = true;
            if(general.shield.activeSelf)
            {
                blocked = true;
            }
        }
    }

    public void endTouch()
    {
        if (!general.paused)
        {
            if (!blocked)
            {
                touchbegan = false;
                touchcontinues = false;


                if (!player.ismoving && general.crabstodelete.Count == 0)
                {
                    if (touchPos.x > swipeStartPos.x)
                    {
                        player.animator.Play("Right");
                        //player.rb.AddForce(transform.up * 0.3f, ForceMode.Impulse);
                        Debug.Log("move right");
                        player.targetPosition = new Vector3(player.targetPosition.x + 0.25f, player.transform.position.y, player.transform.position.z);
                        if (player.currentplatform < 2)
                        {
                            player.currentplatform += 1;
                        }
                    }
                    else if (touchPos.x < swipeStartPos.x)
                    {
                        player.animator.Play("Left");
                        //player.rb.AddForce(transform.up * 0.3f, ForceMode.Impulse);
                        // player.rb.AddForce((transform.right * -1 * player.moveSpeed) + transform.up * 0.3f, ForceMode.Impulse);
                        Debug.Log("move left");
                        player.targetPosition = new Vector3(player.targetPosition.x - 0.25f, player.transform.position.y, player.transform.position.z);
                        if (player.currentplatform > -2)
                        {
                            player.currentplatform -= 1;
                        }
                    }
                    player.ismoving = true;
                }
                player.targetPosition.x = Mathf.Clamp(player.targetPosition.x, -0.5f, 0.5f);
            }

            // crabs
            

            general.shieldactive = false;
            general.shield.SetActive(false);
            general.shield.transform.localScale = Vector3.zero;

            if (!general.ui.settingScreen.activeSelf)
            {
                blocked = false;
            }
        }
    }

    
}
