using System.Collections;
using UnityEngine;

public class SlamEnemy : EnemyAI
{
    [SerializeField] float minAttackDistance = 60f;
    [SerializeField] float attackDelay = 5.0f;

    [SerializeField] AudioSource ambience;
    [SerializeField] float ambienceDelay = 4.0f;
    float currentTime;
    [SerializeField] AudioSource charging;
    [SerializeField] AudioSource attacking;
    [SerializeField] AudioSource pulse;

    [SerializeField] Material black;
    [SerializeField] Material outline;
    MeshRenderer mr;
    Animator anim;

    bool first = true;


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
        if (currentTime < 0 && charging.isPlaying == false)
        {
            ambience.Play();
            currentTime = ambienceDelay;
        }

        currentTime -= Time.deltaTime;
    }

    public override void DetectPlayer()
    {
        RaycastHit hit;
        if (Physics.Raycast(detectionOrigin.position, (PlayerMovement.playerPosition - detectionOrigin.position).normalized, out hit, detectionRange, layerMask))
        {
            if (hit.collider.gameObject.tag == "Player")
            {
                state = EnemyState.Navigation;
            }
        }
    }

    public override void NavigateToPlayer()
    {
        if (first)
        {
            first = false;  
            StartCoroutine(AttackSequence());
            return;
        }

        base.NavigateToPlayer();

        if (Mathf.Abs(Vector3.Distance(transform.position, PlayerMovement.playerPosition)) < minAttackDistance && canAttack)
        {
            StartCoroutine(AttackSequence());
        }
    }

    IEnumerator AttackSequence()
    {
        canAttack = false;

        //attack build up, speed reduced
        anim.SetTrigger("Charge");
        mr.material = outline;
        agent.speed = attackSpeed;
        charging.Play();

        yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length);

        attacking.Play();


        yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length);

        //speed restored
        agent.speed = movementSpeed;
        mr.material = black;

        yield return new WaitForSeconds(attackDelay);

        canAttack = true;
        yield break;
    }

    public void PulseSound()
    {
        pulse.Stop();
        pulse.Play();
    }
}
