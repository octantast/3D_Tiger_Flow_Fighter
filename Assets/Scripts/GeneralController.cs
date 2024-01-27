using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GeneralController : MonoBehaviour
{
    public PlayerController player;
    public UI ui;
    public TouchController touches;

    public List<Rigidbody> crabstodelete;
    public GameObject waveprefab;
    public GameObject crabprefab;


    // forwaves
    private float spawnTimer;
    public float spawnTimerMax;
    public float spawnTimerMin;

    public GameObject lastwaveHolder;
    public GameObject nextwaveHolder; // to predict
    public float minpositionToSpawn;
    public float maxpositionToSpawn;
    public List<GameObject> holdersToSpawn;
    public List<GameObject> staticholdersToSpawn;
    private GameObject lastwave;

    public bool shieldactive;
    public GameObject shield;

    public bool paused;

    public float impulseUp;
    public float impulseZ;

    private float howmanyCrabs = 1;


    public GameObject arrowLeft;
    public GameObject arrowRight;

    // effects

    public List<ParticleSystem> effects;
    public void Start()
    {
        if (ui.tutorial1 != 0)
        {
            nextwaveHolder = holdersToSpawn[Random.Range(2, holdersToSpawn.Count)];
            holdersToSpawn.Remove(nextwaveHolder);
            holdersToSpawn.Insert(0, nextwaveHolder);
        }
        else
        {
            nextwaveHolder = holdersToSpawn[3];
            holdersToSpawn.Remove(nextwaveHolder);
            holdersToSpawn.Insert(0, nextwaveHolder);
        }
    }
    public void Update()
    {
        if (!paused)
        {
            spawnTimer -= Time.deltaTime;
            if (spawnTimer <= 0f)
            {
                WaveSpawner();
                spawnTimer = Random.Range(spawnTimerMin, spawnTimerMax);
            }

            // skills
            if (ui.a1timer > 0)
            {
                ui.a1timer -= Time.deltaTime;
                ui.a1activeskale.fillAmount = ui.a1timer / ui.a1timerMax;
            }
            else if (ui.a1active)
            {
                ui.a1activeskale.fillAmount = 0;
                ui.a1active = false;
                arrowRight.SetActive(false);
                arrowLeft.SetActive(false);
            }

            if (ui.a2timer > 0)
            {
                ui.a2timer -= Time.deltaTime;
                ui.a2activeskale.fillAmount = ui.a2timer / ui.a2timerMax;
                ui.scoreIndex2 = 2;
            }
            else if (ui.a2active)
            {
                ui.a2activeskale.fillAmount = 0;
                ui.a2active = false;
                ui.scoreIndex2 = 1;
            }

            if(ui.a1active)
            {
                if(lastwaveHolder.transform.localPosition.x > player.transform.position.x && lastwaveHolder.transform.localPosition.x - player.transform.position.x > 0.1f)
                {
                    arrowRight.SetActive(true);
                    arrowLeft.SetActive(false);
                }
                else if (lastwaveHolder.transform.localPosition.x < player.transform.position.x && lastwaveHolder.transform.localPosition.x - player.transform.position.x < -0.1f)
                {
                    arrowRight.SetActive(false);
                    arrowLeft.SetActive(true);
                }
                else
                {
                    if (nextwaveHolder != lastwaveHolder && nextwaveHolder.transform.childCount > 0)
                    {
                        if (nextwaveHolder.transform.localPosition.x > player.transform.position.x && nextwaveHolder.transform.localPosition.x - player.transform.position.x > 0.1f)
                        {
                            arrowRight.SetActive(true);
                            arrowLeft.SetActive(false);
                        }
                        else if (nextwaveHolder.transform.localPosition.x < player.transform.position.x && nextwaveHolder.transform.localPosition.x - player.transform.position.x < -0.1f)
                        {
                            arrowRight.SetActive(false);
                            arrowLeft.SetActive(true);
                        }
                        else
                        {
                            arrowRight.SetActive(false);
                            arrowLeft.SetActive(false);
                        }
                    }
                    else
                    {
                        arrowRight.SetActive(false);
                        arrowLeft.SetActive(false);
                    }
                }


            }
        }
        else
        {
            arrowRight.SetActive(false);
            arrowLeft.SetActive(false);
        }

        player.animator.SetBool("shield", shieldactive);
        player.animator.SetBool("jump", player.ismoving);
    }
    public void WaveSpawner()
    {
        // different parents (z axis)
        lastwaveHolder = nextwaveHolder;

        nextwaveHolder = holdersToSpawn[Random.Range(2, holdersToSpawn.Count)];
        holdersToSpawn.Remove(nextwaveHolder);
        holdersToSpawn.Insert(0, nextwaveHolder);

        GameObject wave = Instantiate(waveprefab, transform.position, Quaternion.identity, lastwaveHolder.transform);
        wave.transform.localPosition = new Vector3(0, -0.2f, Random.Range(minpositionToSpawn, maxpositionToSpawn));


        WaveController script = wave.GetComponent<WaveController>();
        script.general = this;

        script.currentPlatform = staticholdersToSpawn.IndexOf(lastwaveHolder) - 2; 
    }

    public void waveHere(GameObject wave, WaveController script)
    {
        if (lastwave != wave)
        {
            lastwave = wave;

            // harder
            ui.wavespeedlevel += 0.005f;
            ui.wavespeed = ui.wavespeedlevel;

            ui.sounds[3].Play();

            // particles
            ParticleSystem particle = Instantiate(effects[1], transform.position, Quaternion.identity);
            particle.gameObject.transform.position = wave.transform.position;

            if (shieldactive && script.currentPlatform == player.currentplatform)
            {
                ui.currentScore += 1 * ui.scoreIndex2;
                Debug.Log("wave caught");
                // crabs
                int spawnCount = (int)howmanyCrabs;
                howmanyCrabs += 0.5f;
                howmanyCrabs = Mathf.Clamp(howmanyCrabs, 0, 6);

                for (int i = 0; i < spawnCount; i++)
                {
                    GameObject crab = Instantiate(crabprefab, transform.position, Quaternion.identity, wave.transform);
                    crab.transform.localPosition = new Vector3(0, 0.3f, 0);
                    crab.transform.SetParent(null);
                    Rigidbody crabrb = crab.GetComponent<Rigidbody>();
                    Vector3 impulse = new Vector3(Random.Range(-0.2f, 0.2f), impulseUp, Random.Range(impulseZ-0.1f, impulseZ));
                    crabrb.AddForce(impulse, ForceMode.Impulse);
                    Crab scriptcrab = crab.GetComponent<Crab>();
                    scriptcrab.general = this;
                }

                if (ui.tutorial2 == 0)
                {
                    ui.tutorial2 = 1;
                    PlayerPrefs.SetInt("tutorial2", 1);
                    PlayerPrefs.Save();
                    ui.tipAnimator.enabled = false;
                    ui.tipAnimator.Play("CrabTutor");
                    ui.tipAnimator.enabled = true;
                }

            }
            else
            {

                ui.sounds[4].Play();
                // count with bonuses
                ui.hpLost();
            }

            Destroy(wave);
        }
        else
        {
            lastwave = null;
        }

    }

    public void crabDestroyed(GameObject crab)
    {
        // effect
        ParticleSystem particle = Instantiate(effects[0], transform.position, Quaternion.identity);
        particle.gameObject.transform.position = crab.transform.position;
        // sound
        ui.sounds[2].Play();

    }


 

}
