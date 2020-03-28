using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class frogMove : MonoBehaviour
{
    public float moveSpeed = 2;
    public Transform moveTarget;
    public LayerMask whatStopsMovement;
    public LayerMask hazards; //holds colliders for tiles that trigger gameover
    public LayerMask characters; //holds colliders for other characters

    public bool stopTurtle;

    public GameObject MainManager;

    private bool moved;

    private MainManager MM;

    void Start()
    {
        moveTarget.parent = null;
        moved = false;
        MM = MainManager.GetComponent<MainManager>();
    }

    // Update is called once per frame
    void Update()
    {
        //move to moveTarget
        transform.position = Vector3.MoveTowards(transform.position, moveTarget.position, moveSpeed * Time.deltaTime);
        
        if (Vector3.Distance(transform.position, moveTarget.position) <= 0.5f)
        {
            if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) == 1 && !moved)
            {
                moved = true;

                do //frog goes one direction until it collides
                {
                    //triggers game over if there is a hazard ahead
                    if (Physics2D.OverlapCircle(moveTarget.position + new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f), .2f, hazards))
                    {
                        MM.Defeat();
                    }

                    //detects contact with other characters
                    if (Physics2D.OverlapCircle(moveTarget.position + new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f), .2f, characters))
                    {
                        MM.CheckVictory();
                    }

                    //detects collions
                    if (!Physics2D.OverlapCircle(moveTarget.position + new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f), .2f, whatStopsMovement))
                    {

                        moveTarget.position += new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f);
                        //okami's rotate/flip sprite script
                        if (Mathf.Sign(Input.GetAxisRaw("Horizontal")) == 1) { transform.eulerAngles = new Vector3(0, 0, -90); }
                        else { transform.eulerAngles = new Vector3(0, 0, 90); }
                        //

                    }
                } while (!Physics2D.OverlapCircle(moveTarget.position + new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f), .2f, whatStopsMovement));


            }
            else if (Mathf.Abs(Input.GetAxisRaw("Vertical")) == 1 && !moved)
            {
                moved = true;

                do //frog goes one direction until it collides
                {
                    //triggers game over if there is a hazard ahead
                    if (Physics2D.OverlapCircle(moveTarget.position + new Vector3(Input.GetAxisRaw("Vertical"), 0f, 0f), .2f, hazards))
                    {
                        MM.Defeat();
                    }

                    if (!Physics2D.OverlapCircle(moveTarget.position + new Vector3(0f, Input.GetAxisRaw("Vertical"), 0f), .2f, whatStopsMovement))
                    {
                        stopTurtle = true;
                        moveTarget.position += new Vector3(0f, Input.GetAxisRaw("Vertical"), 0f);
                        //okami's rotate/flip sprite script
                        if (Mathf.Sign(Input.GetAxisRaw("Vertical")) == 1) { transform.eulerAngles = new Vector3(0, 0, 0); }
                        else { transform.eulerAngles = new Vector3(0, 0, 180); }
                        //

                    }
                } while (!Physics2D.OverlapCircle(moveTarget.position + new Vector3(0f, Input.GetAxisRaw("Vertical"), 0f), .2f, whatStopsMovement));

                //detects contact with other characters
                if (Physics2D.OverlapCircle(moveTarget.position + new Vector3(Input.GetAxisRaw("Vertical"), 0f, 0f), .2f, characters))
                {
                    MM.CheckVictory();
                }

            }

            else moved = false;
        }
    }
}
