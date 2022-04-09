using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using MEC;

public abstract class EnemyCharacter : Character
{
    // AI
    public Transform target;
    public float nextWaypointDistance = 3f;
    Path path;
    int currentWaypoint = 0;
    bool reachedEndOfPath = false;
    Seeker seeker;

    [SerializeField] protected float attackDistance;
    [SerializeField] protected float attackCooldown;
    [SerializeField] protected float attackDamage;
    protected bool isOnCooldown = false;

    Vector2 movementDirection;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        seeker = GetComponent<Seeker>();

        InvokeRepeating("UpdatePath", 0f, 0.5f);
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();

        Vector2 attackVector = (Vector2)target.position - characterRb.position;
        float distanceToTarget = attackVector.magnitude;
        if (distanceToTarget > attackDistance)
        {
            HandlePathing();
            Move(movementDirection);
        }
        else if (!isOnCooldown)
        {
            Timing.RunCoroutine(_Attack(attackVector));
        }
    }

    protected abstract IEnumerator<float> _Attack(Vector2 attackVector);

    public override void OnDeath()
    {
        throw new System.NotImplementedException();
    }

    #region AI
    void UpdatePath()
    {
        if (seeker.IsDone())
        {
            seeker.StartPath(characterRb.position, target.position, OnPathComplete);
        }
    }

    void HandlePathing()
    {
        if (path == null)
        {
            return;
        }

        if (currentWaypoint >= path.vectorPath.Count)
        {
            reachedEndOfPath = true;
            return;
        }
        else
        {
            reachedEndOfPath = false;
        }

        movementDirection = ((Vector2)path.vectorPath[currentWaypoint] - characterRb.position).normalized;
        characterAnim.SetFloat("LookDirection", movementDirection.x);

        float distance = Vector2.Distance(characterRb.position, path.vectorPath[currentWaypoint]);

        if (distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }
    }

    void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }
    #endregion

}
