using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridMove : MonoBehaviour
{
    public float moveSpeed = 2;
    public Transform moveTarget; //Empty sprite moves towards
    public LayerMask whatStopsMovement; //holds colliders for boundaries
    public LayerMask hazards; //holds colliders for tiles that trigger gameover
    public GameObject frog;
    public bool together;

    public GameObject MainManager;

    private bool moved;

    private MainManager MM;

    void Start()
    {
        together = false;
        moveTarget.parent = null;
        moved = false;
        MM = GetComponent<MainManager>();
    }

    // Update is called once per frame
    void Update()
    {
        //move to moveTarget
        transform.position = Vector3.MoveTowards(transform.position, moveTarget.position, moveSpeed * Time.deltaTime);

        //check sprites are on top of each other
        if ((transform.position.x == frog.transform.position.x) && (transform.position.y == frog.transform.position.y))
        {
            together = true;
            //turtle disappears(trigger
            GetComponent<SpriteRenderer>().enabled = false;
            //
        }
        else { together = false; }
        //sto
        if ((Vector3.Distance(transform.position, moveTarget.position) <= 0.5f))
        {
            //gets horizontal
            if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) == 1 && !moved)
            {
                moved = true;


                //moves if no collisions ahead
                if (!Physics2D.OverlapCircle(moveTarget.position + new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f), .2f, whatStopsMovement))
                {
                    //triggers game over if there is a hazard ahead
                    if (Physics2D.OverlapCircle(moveTarget.position + new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f), .2f, hazards))
                    {
                        Destroy(this);
                    }
                    //moves to moveTarget
                    moveTarget.position += new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f);
                    //okami's rotate/flip sprite script
                    if (Mathf.Sign(Input.GetAxisRaw("Horizontal")) == 1) { transform.eulerAngles = new Vector3(0, 0, -90); }
                    else { transform.eulerAngles = new Vector3(0, 0, 90); }


                }
            }

            //gets vertical
            else if (Mathf.Abs(Input.GetAxisRaw("Vertical")) == 1 && !moved)
            {
                moved = true;


                //moves if no collisions ahead
                if (!Physics2D.OverlapCircle(moveTarget.position + new Vector3(0f, Input.GetAxisRaw("Vertical"), 0f), .2f, whatStopsMovement))
                {
                    //triggers game over if there is a hazard ahead
                    if (Physics2D.OverlapCircle(moveTarget.position + new Vector3(0f, Input.GetAxisRaw("Vertical"), 0f), .2f, hazards))
                    {
                        Destroy(this);
                    }
                    //moves to moveTarget
                    moveTarget.position += new Vector3(0f, Input.GetAxisRaw("Vertical"), 0f);
                    //okami's rotate/flip sprite script
                    if (Mathf.Sign(Input.GetAxisRaw("Vertical")) == 1) { transform.eulerAngles = new Vector3(0, 0, 0); }
                    else { transform.eulerAngles = new Vector3(0, 0, 180); }

                }
            }

            else moved = false;
        }
    }
}


