using UnityEngine;
using UnityEngine.AI;

public class EnemySight : MonoBehaviour
{
    public float fieldOfViewAngle = 110f;
    public bool playerInSight;
    public bool playerHeard;
    public Vector3 personalLastSighting;

    private NavMeshAgent nav;
    private SphereCollider col;
    private Animator anim;

    //private LastPlayerSigting lastPlayerSighting;

    private GameObject player;
    private Animator playerAnim;

    //GET FROM CHARACTER STATS
    private float playerHealth;
    //private HashIDs hash;
    private Vector3 previousSighting;

    private void Awake()
    {
        this.nav = GetComponent<NavMeshAgent>();
        this.col = GetComponent<SphereCollider>();
        this.anim = GetComponent<Animator>();
        //this.lastPlayerSighting = GameObject.FindGameObjectWithTag("Player").GetComponent<LastPlayerSigting>();
        this.player = GameObject.FindGameObjectWithTag("Player");
        this.playerAnim = this.player.GetComponent<Animator>();
        //Get from stats
        this.playerHealth = 100;
        //this.hash = GameObject.FindGameObjectWithTag();

        this.personalLastSighting = new Vector3(1000f, 1000f, 1000f);
        this.previousSighting = new Vector3(1000f, 1000f, 1000f);
    }

    private void Update()
    {

    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject == player)
        {
            this.playerInSight = false;

            Vector3 direction = other.transform.position - transform.position;
            float angle = Vector3.Angle(direction, transform.forward);

            if (angle < this.fieldOfViewAngle * 0.5f)
            {
                RaycastHit hit;

                Debug.DrawRay(transform.position + transform.up, direction.normalized, Color.blue);

                if (Physics.Raycast(transform.position + transform.up, direction.normalized, out hit, this.col.radius))
                {
                    if (hit.collider.gameObject == this.player)
                    {
                        this.playerInSight = true;
                        this.personalLastSighting = player.transform.position;
                    }
                }
            }
            
            if (CalculatePathLenght(player.transform.position) <= col.radius)
            {
                this.playerHeard = true;
                personalLastSighting = player.transform.position;
                
            }

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
        {
            this.playerHeard = false;
            this.playerInSight = false;
        }
    }

    private float CalculatePathLenght(Vector3 targetPosition)
    {
        NavMeshPath path = new NavMeshPath();

        if (nav.enabled)
        {
            nav.CalculatePath(targetPosition, path);
        }

        Vector3[] allWayPoints = new Vector3[path.corners.Length + 2];

        allWayPoints[0] = transform.position;

        allWayPoints[allWayPoints.Length - 1] = targetPosition;

        for (int i = 0; i < path.corners.Length - 1; i++)
        {
            allWayPoints[i + 1] = path.corners[i];
        }

        float pathLenght = 0f;

        for (int i = 0; i < path.corners.Length - 1; i++)
        {
            pathLenght += Vector3.Distance(allWayPoints[i], allWayPoints[i + 1]);
        }

        return pathLenght;
    }
}
