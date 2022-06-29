using System.Collections;
using UnityEngine;

public class ChargeEnemy : EnemyAI
{
    [SerializeField] float chargeRange = 40f;
    [SerializeField] float maxChargeTimer = 8f;
    [SerializeField] float chargeDelay = 6f;

    float currentTime;

    [SerializeField] AudioClip[] ambientSounds;
    [SerializeField] float ambienceDelay;    
    [SerializeField] AudioSource ambience;
    [SerializeField] AudioSource charge;
    private Animator anim;
    bool canAttack = true;

    public override void Start()
    {
        base.Start();

        anim = GetComponent<Animator>();
    }

    public override void Update()
    {
        base.Update();

        if(!ambience.isPlaying && currentTime < 0 && !charge.isPlaying)
        {
            ambience.clip = ambientSounds[Random.Range(0, ambientSounds.Length)];
            ambience.Play();
            currentTime = ambienceDelay;
        }

        currentTime -= Time.deltaTime;
    }

    public override void NavigateToPlayer()
    {
        base.NavigateToPlayer();

        if(Mathf.Abs(Vector3.Distance(transform.position, PlayerMovement.playerPosition)) < chargeRange && canAttack)
        {
            StartCoroutine(AttackSequence());
        }
    }

    IEnumerator AttackSequence()
    {
        canAttack = false;

        //play charge sfx, change speed
        charge.Play();
        charge.loop = true;
        agent.speed = attackSpeed;

        anim.SetBool("Charging", true);
        anim.SetBool("Idle", false);

        yield return new WaitForSeconds(maxChargeTimer);
        
        charge.loop = false;
        agent.speed = movementSpeed;

        anim.SetBool("Charging", false);
        anim.SetBool("Idle", true);

        yield return new WaitForSeconds(chargeDelay);

        canAttack = true;

        yield break;
    }

    public override void AttackPlayer()
    {
        base.AttackPlayer();
    }
}
