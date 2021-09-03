using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMovement : MonoBehaviour
{
    #region cometed

    /*
    public GameObject ball;
    Camera cam;
    Rigidbody rigidBody;
    public bool collidingWithWallsOrPillers;
    public float speed = 1f;
    Vector3 lastFrameBallPos;
    Vector3 currentFrameBallPos;
    public Vector3 Velocity;
    Vector3 towards;
    bool makeBallMove;

    void Start()
    {
        makeBallMove = false;
        cam = Camera.main;
        rigidBody = ball.GetComponent<Rigidbody>();
        collidingWithWallsOrPillers = false;
        lastFrameBallPos = ball.transform.localPosition;
    }
    private void Update()
    {
      //  towards = ball.transform.localPosition - new Vector3(cam.ScreenToWorldPoint(Input.mousePosition).x, cam.ScreenToWorldPoint(Input.mousePosition).y, lastFrameBallPos.z);

        Ray ray = new Ray(new Vector3(cam.ScreenToWorldPoint(Input.mousePosition).x, cam.ScreenToWorldPoint(Input.mousePosition).y, lastFrameBallPos.z), towards);
        Debug.DrawRay(ray.origin,ray.direction,Color.red);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (hit.transform.CompareTag("MazeWall") || hit.transform.CompareTag("MazePiller"))
            {
                collidingWithWallsOrPillers = true;
                print("123");

            }
            else
            { 
                collidingWithWallsOrPillers = false;
            
            }


        }


    }

    private void FixedUpdate()
    {
        if (makeBallMove)
        {
            BallFollowMousePointer();
        }
    }



    public void BallFollowMousePointer()
    {
        //ball.transform.localPosition = new Vector3(cam.ScreenToWorldPoint(Input.mousePosition).x, cam.ScreenToWorldPoint(Input.mousePosition).y, ball.transform.localPosition.z);
        // rigidBody.MovePosition(new Vector3(cam.ScreenToWorldPoint(Input.mousePosition).x, cam.ScreenToWorldPoint(Input.mousePosition).y, lastFrameBallPos.z) + transform.up * 10 * Time.deltaTime);

        if (Mathf.Sqrt(Mathf.Pow((ball.transform.localPosition.x - cam.ScreenToWorldPoint(Input.mousePosition).x), 2) + Mathf.Pow((ball.transform.localPosition.y - cam.ScreenToWorldPoint(Input.mousePosition).y), 2)) > 0.2f)
        { 
        
            rigidBody.velocity=new Vector3(cam.ScreenToWorldPoint(Input.mousePosition).x, cam.ScreenToWorldPoint(Input.mousePosition).y, ball.transform.localPosition.z) * speed * Time.deltaTime;
        }
            //rigidBody.MovePosition(new Vector3(cam.ScreenToWorldPoint(Input.mousePosition).x, cam.ScreenToWorldPoint(Input.mousePosition).y, ball.transform.localPosition.z)  * speed * Time.deltaTime);

        //    ball.transform.localPosition = Vector3.MoveTowards(ball.transform.localPosition, new Vector3(cam.ScreenToWorldPoint(Input.mousePosition).x, cam.ScreenToWorldPoint(Input.mousePosition).y, lastFrameBallPos.z), speed * Time.deltaTime);


        // lastFrameBallPos= ball.transform.localPosition;
    }

    private void OnMouseDrag()
    {

        if (!collidingWithWallsOrPillers)
        {
            makeBallMove = true;
        }
        else
        {
            makeBallMove = false;
        }

    }

    private void OnMouseUp()
    {
            makeBallMove = false;

    }
    /*

    private void OnCollisionEnter(Collision collision)
    {
        //  ball.transform.localPosition = lastFrameBallPos;
        if (collision.transform.gameObject.CompareTag("MazeWall") || collision.transform.gameObject.CompareTag("MazePiller"))
        {
            //  ball.transform.localPosition = Vector3.MoveTowards(ball.transform.localPosition, new Vector3(cam.ScreenToWorldPoint(Input.mousePosition).x, cam.ScreenToWorldPoint(Input.mousePosition).y, lastFrameBallPos.z), -2f * speed * Time.deltaTime);
            print("collision occured");
            rigidBody.velocity = Vector3.zero;
            collidingWithWallsOrPillers = true;
        }
    }
    

    private void OnCollisionExit(Collision collision)
    {
        if (collision.transform.gameObject.CompareTag("MazeWall") || collision.transform.gameObject.CompareTag("MazePiller"))
        {
           // collidingWithWallsOrPillers = false;
        }

    }

    //private void OnMouseDown()
    //{
    //    collidingWithWallsOrPillers = false;
    //}
*/
    #endregion

    private Vector3 touchPosition; 
    private Rigidbody rb;
    private Vector3 direction; 
    public  float moveSpeed = 10f; 
    bool canDrag; 
    public bool Dragging;
    Camera cam;
    Vector3 towards; 
    bool collidingWithWallsOrPillers;
    public bool stopTakingInput;
    public VariableJoystick variableJoystick;
    private void Start() { rb = GetComponent<Rigidbody>(); Dragging = false; cam = Camera.main; }

    private void Update()
    {        //if (Input.touchCount > 0)        //{        //    Touch touch = Input.GetTouch(0);        //    touchPosition = Camera.main.ScreenToWorldPoint(touch.position);        //    touchPosition.z = 0;        //    direction = (touchPosition - transform.position);        //    rb.velocity = new Vector2(direction.x, direction.y) * moveSpeed;        //    if (touch.phase == TouchPhase.Ended)        //        rb.velocity = Vector2.zero;        //}       


        if (stopTakingInput)
        { return; }



        if (Input.GetMouseButton(0))
        {

            towards = transform.localPosition - new Vector3(cam.ScreenToWorldPoint(Input.mousePosition).x, cam.ScreenToWorldPoint(Input.mousePosition).y, transform.localPosition.z);

            Ray ray = new Ray(new Vector3(cam.ScreenToWorldPoint(Input.mousePosition).x, cam.ScreenToWorldPoint(Input.mousePosition).y, transform.localPosition.z), towards);
            Debug.DrawRay(ray.origin, ray.direction, Color.red);
            

            //if (Physics.Raycast(ray, out RaycastHit hit)&& !Extensions.IsPointerOverUIObject())
            //{
            //    if (hit.transform.CompareTag("MazeWall") || hit.transform.CompareTag("MazePiller"))
            //    {
            //        collidingWithWallsOrPillers = true;
            //        print("123");

            //    }
            //    else
            //    {
            //        collidingWithWallsOrPillers = false;

            //    }


            //}

        }


        if (Input.GetMouseButton(0) && Dragging && collidingWithWallsOrPillers == false)
        {
            canDrag = true;
        }
        else
        {
            canDrag = false;
            rb.velocity = Vector2.zero;
        }



    }
    private void FixedUpdate()
    {
        if (stopTakingInput)
        { return; }

        if (canDrag)
        {
            touchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            touchPosition.z = 0;
            direction = (touchPosition - transform.position);
        }
            rb.velocity = new Vector2(variableJoystick.Horizontal, variableJoystick.Vertical) * moveSpeed;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.gameObject.CompareTag("MazeWall") || collision.transform.gameObject.CompareTag("MazePiller"))
        {
            print("collision occured");
            rb.velocity = Vector2.zero;
        }
    }

    private void OnMouseDrag()
    {
        Dragging = true;

    }
    private void OnMouseUp()
    {
        Dragging = false;

    }


}
