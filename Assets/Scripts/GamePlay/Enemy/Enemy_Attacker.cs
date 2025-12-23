using UnityEngine;

public class Enemy_Attacker : Enemy_Base
{

    float acceleration;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        base.Start();
        acceleration = agent.acceleration;
    }

    // Update is called once per frame
    void Update()
    {
        if (!target)
            GetTarget();
        if(!inRange)
        {
            agent.speed = moveSpeed;
            agent.acceleration = acceleration;
            agent.updatePosition = true;
        }
        else
        {
            agent.speed = attackMoveSpeed;
            agent.acceleration = 0;
            agent.updatePosition = false;
        }
        MoveTowardsTarget();
    }


    //used by the attack trigger to move and stop the character when in range
    public void Move()
    {
        inRange = false;
    }
    public void NoMove()
    {
        inRange = true;
    }
}
