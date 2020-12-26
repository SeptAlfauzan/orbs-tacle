using UnityEngine;

public class PlayerLaunch : MonoBehaviour
{
    private readonly Vector2 LAUNCH_VELOCITY = new Vector2(20f, 10f);
    private readonly Vector2 INITIAL_POSITION = Vector2.zero;
    private readonly Vector2 GRAVITY = new Vector2(0f, -240f);
    private const float DELAY_UNTIL_LAUNCH = 1f;
    private int NUM_DOTS_TO_SHOW = 30;
    private float DOT_TIME_STEP = 0.05f;

    private bool launched = false;
    private float timeUntilLaunch = DELAY_UNTIL_LAUNCH;
    private Rigidbody2D rigidBody;

    public GameObject trajectoryDotPrefab;
    GameObject[] pointsDot;
    float force;

    Vector2 Direction;

    float distance;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        pointsDot = new GameObject[NUM_DOTS_TO_SHOW];

        for (int i = 0; i < NUM_DOTS_TO_SHOW; i++)
        {
            pointsDot[i] = Instantiate(trajectoryDotPrefab, transform.position, Quaternion.identity);
            
            // trajectoryDot.transform.position = CalculatePosition(DOT_TIME_STEP * i);
        }
    }

    private void Update()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 playerPos = transform.position;

        Direction = -(mousePos) + playerPos;
        // Debug.Log(mousePos);
        // Debug.Log(-mousePos);

        distance = Vector2.Distance(mousePos, rigidBody.position);
        force = distance*1.5f;//force power will base on distance mouse between player

        // timeUntilLaunch -= Time.deltaTime;

        // if (!launched && timeUntilLaunch <= 0)
        // {
        //     Launch();
        // }

        for (int i = 0; i < pointsDot.Length; i++)
        {
            pointsDot[i].transform.position = PointPosition(i * 0.05f);
        }


        if(Input.GetMouseButtonDown(0)){
            Time.timeScale = 0.3f;
        }
        else if(Input.GetMouseButtonUp(0)){
            Time.timeScale = 1f;
            Debug.Log(distance);
            rigidBody.velocity = Direction.normalized * force;
        }
    }

    private void FixedUpdate() {
    }

    Vector2 PointPosition(float time){
        Vector2 position = (Vector2)transform.position + (Direction.normalized * force * time) + 0.5f * Physics2D.gravity * (time*time);
        return position;
    }
    
}