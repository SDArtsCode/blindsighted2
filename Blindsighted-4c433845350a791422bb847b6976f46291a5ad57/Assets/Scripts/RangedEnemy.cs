using System.Collections;
using UnityEngine;

public class RangedEnemy : EnemyAI
{
    [SerializeField] float minAttackDistance = 60f;
    [SerializeField] float attackBuildUp = 1.5f;
    [SerializeField] float attackDelay = 5.0f;
    [SerializeField] float bulletVelocity = 30f;

    [SerializeField] AudioSource ambience;
    [SerializeField] float ambienceDelay = 4.0f;
    float currentTime;
    [SerializeField] AudioSource rampUp;
    [SerializeField] Transform enemyProjectile;

    [SerializeField] Material black;
    [SerializeField] Material outline;
    MeshRenderer mr;
    Animator anim;


    bool canAttack = true;

    public override void Start()
    {
        base.Start();

        mr = GetComponent<MeshRenderer>();
        anim = GetComponent<Animator>();
    }

    public override void Update()
    {
        base.Update();

        //plays ambience if not ramping up and delay has passed
        if (currentTime < 0 && rampUp.isPlaying == false) 
        {
            ambience.Play();
            currentTime = ambienceDelay;
        }

        currentTime -= Time.deltaTime;
    }

    public override void DetectPlayer()
    {
        base.DetectPlayer();
    }

    public override void NavigateToPlayer()
    {
        base.NavigateToPlayer();

        RaycastHit hit;
        if (Physics.Raycast(detectionOrigin.position, (PlayerMovement.playerPosition - detectionOrigin.position).normalized, out hit, Mathf.Infinity, layerMask))
        {
            if (hit.collider.gameObject.tag == "Player" && Mathf.Abs(Vector3.Distance(transform.position, PlayerMovement.playerPosition)) < minAttackDistance && canAttack)
            {
                StartCoroutine(AttackSequence());    
            }
        }
    }

    IEnumerator AttackSequence()
    {
        canAttack = false;


        //attack build up, speed reduced
        anim.SetBool("Attacking", true);
        mr.material = outline;
        agent.speed = attackSpeed;
        rampUp.Play();
        
        yield return new WaitForSeconds(attackBuildUp);

        //projectile fired
        var projectile = Instantiate(enemyProjectile, transform.position, Quaternion.identity);
        projectile.GetComponent<Rigidbody>().velocity = (PlayerMovement.playerPosition - transform.position).normalized * bulletVelocity;

        //speed restored
        agent.speed = movementSpeed;
        anim.SetBool("Attacking", false);
        mr.material = black;

        yield return new WaitForSeconds(attackDelay);

        canAttack = true;
        yield break;
    }
}
