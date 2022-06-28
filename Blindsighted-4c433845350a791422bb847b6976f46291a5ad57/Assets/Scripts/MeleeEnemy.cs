using UnityEngine;

public class MeleeEnemy : EnemyAI
{
    [SerializeField] float shriekDistance = 10f;
    [SerializeField] float ambienceDelay = 5.0f;
    [SerializeField] float shriekDelay = 5.0f;
    float currentTime1;
    float currentTime2;
    [SerializeField] AudioClip[] ambientSounds;
    [SerializeField] AudioSource ambience;
    [SerializeField] AudioSource shriek;

    public override void Update()
    {
        base.Update();

        if(Mathf.Abs(Vector3.Distance(transform.position, PlayerMovement.playerPosition)) < shriekDistance && !shriek.isPlaying && currentTime1 < 0)
        {
            shriek.Play();
            currentTime1 = shriekDelay;
        }

        if(currentTime2 < 0 && !shriek.isPlaying && !ambience.isPlaying)
        {
            ambience.clip = ambientSounds[Random.Range(0, ambientSounds.Length)];
            ambience.Play();
            currentTime2 = ambienceDelay;
        }
        currentTime1 -= Time.deltaTime;
        currentTime2 -= Time.deltaTime;
    }

    public override void DetectPlayer()
    {
        base.DetectPlayer();
    }

    public override void NavigateToPlayer()
    {
        base.NavigateToPlayer();


    }

    public override void AttackPlayer()
    {
        base.AttackPlayer();
    }
}
