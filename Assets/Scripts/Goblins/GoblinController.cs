using UnityEngine;

public class GoblinController : MonoBehaviour
{
    public GoblinClass GoblinClass;

    public GameObject Point;

    private float Speed = 1.6f;
    
    private Vector3 MovingVector;

    private readonly float Radius = 2.4f;

    private float TimeBeforeMoving = 0f;

    private readonly float MaxTimeForMoving = 30f;

    private readonly float MinTimeForMoving = 10f;

    private float TimeForMoving = 0f;

    private Animator Animator;

    private EnemySight Sight;

    private void Start()
    {
        this.Animator = GetComponentInChildren<Animator>();

        this.MoveInsidePoint();
        this.TimeForMoving = Random.Range(this.MinTimeForMoving, this.MaxTimeForMoving);

        this.Sight = GetComponent<EnemySight>();
    }

    private void Update()
    {
        if (this.Sight.playerHeard)
        {
            transform.LookAt(this.Sight.personalLastSighting);
        }

        if (this.GoblinClass == GoblinClass.Goblin)
        {
            this.BaseGoblin();
        }
    }

    private void BaseGoblinState()
    {
        
    }

    private void BaseGoblin()
    {
        if (MovingVector == transform.position)
        {
            this.Animator.SetBool("isWalking", false);

            if (this.TimeForMoving < this.TimeBeforeMoving)
            {
                this.TimeBeforeMoving = 0f;
                this.TimeForMoving = Random.Range(this.MinTimeForMoving, this.MaxTimeForMoving);
                this.MoveInsidePoint();
            }
            else
            {
                this.TimeBeforeMoving += Time.deltaTime;
            }
        }
        else
        {
            this.Animator.SetBool("isWalking", true);

            var step = this.Speed * Time.deltaTime;

            transform.position = Vector3.MoveTowards(transform.position, MovingVector, step);
            transform.LookAt(MovingVector);
        }
    }

    private void MoveInsidePoint()
    {
        var offset = Random.insideUnitSphere * Radius;
        var pos = this.Point.transform.position + offset;
        var fixedPos = new Vector3(pos.x, this.transform.position.y, pos.z);
        this.MovingVector = fixedPos;
    }
}