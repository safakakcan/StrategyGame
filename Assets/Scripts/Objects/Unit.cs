using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Soldier Unit. It is a selectable, movable and attacker object.
/// </summary>
public class Unit : Entity, IMovable, ISelectable, IAttacker
{
    private int movementSpeed = 1;
    private int attackSpeed = 1;
    private int attackDistance = 1;
    private int damage = 1;

    private Coroutine moving = null;
    private Coroutine attacking = null;

    public Vector2Int Destination { get; set; }
    public Entity Target { get; set; }
    
    public int MovementSpeed
    {
        get { return movementSpeed; }
        set { movementSpeed = value; }
    }
    public int Damage { get { return damage; } }
    public int AttackSpeed { get { return attackSpeed; } }
    public int AttackDistance { get { return attackDistance; } }

    protected override void OnEnable()
    {
        base.OnEnable();

        if (Information != null && Information is UnitInfo)
        {
            movementSpeed = (Information as UnitInfo).movementSpeed;
            attackSpeed = (Information as UnitInfo).attackSpeed;
            attackDistance = (Information as UnitInfo).attackDistance;
            damage = (Information as UnitInfo).damage;
        }
    }

    // Update is called once per frame
    protected override void Update()
    {
        if (moving == null)
        {
            Move();
            moving = StartCoroutine(MovementDelay(1f / MovementSpeed)); // Start a Coroutine for movement
        }

        if (attacking == null)
        {
            Attack();
            attacking = StartCoroutine(AttackDelay(1f / AttackSpeed)); // Start a Coroutine for attacking
        }
    }

    /// <summary>
    /// An enumerator method for movement delays
    /// </summary>
    /// <param name="seconds"></param>
    /// <returns></returns>
    IEnumerator MovementDelay(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        moving = null;
    }

    /// <summary>
    /// An enumerator method for attacking delays
    /// </summary>
    /// <param name="seconds"></param>
    /// <returns></returns>
    IEnumerator AttackDelay(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        attacking = null;
    }

    /// <summary>
    /// This method moves the unit to next position
    /// </summary>
    public void Move()
    {
        if (Position != Destination)
        {
            var path = PathFind.Pathfinding.FindPath(GameManager.Instance.GridMap.Grid,
                new PathFind.Point(Position.x, Position.y), new PathFind.Point(Destination.x, Destination.y));
            
            if (path.Count > 0)
            {
                var nextPosition = new Vector2Int(path[0].x, path[0].y);
                
                if (GameManager.Instance.GridMap.CanBePlaced(nextPosition, Size))
                {
                    Position = nextPosition;
                }
            }
        }
    }

    /// <summary>
    /// Method for attacking to available target
    /// </summary>
    public void Attack()
    {
        if (Target != null && IsTargetInRange(Target))
        {
            Target.Health -= (int)(Damage * Random.Range(0.8f, 1.2f)); // Randomize damage

            if (Target.Health <= 0)
                Target = null;
        }
    }

    /// <summary>
    /// If target in attack distance, method returns True
    /// </summary>
    /// <param name="target"></param>
    /// <returns></returns>
    bool IsTargetInRange(ILocator target)
    {
        for (int x = 0; x < target.Size.x; x++)
        {
            for (int y = 0; y < target.Size.y; y++)
            {
                var distance = Vector2Int.Distance(Position, target.Position + new Vector2Int(x, y));

                if (distance <= AttackDistance)
                {
                    return true;
                }
            }
        }

        return false;
    }

    /// <summary>
    /// Select the unit
    /// </summary>
    public override void Select()
    {
        base.Select();
    }
}
