using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class FireManager : MonoBehaviour
{
    public bool isActiceFire = false;
    [SerializeField]
    GameObject bullet;
    [SerializeField]
    Transform firePoint;
    public float damage;
    public Text DamageText;
    public Text SpeedText;
    public Text FireRateText;
    public float fireRate;
    



    bool isFireStarted;

    [SerializeField]
    EnemyFinder enemyFinder;

    float _enemyFinderTimer;

    bool isThereEnemy;

    [SerializeField]
    Transform spine1;

    public JoystickPlayerExample joystickPlayer;

    public AudioSource knifeSound;
    public AudioSource GunSound;
    public AudioClip knifeSoundClip;

    [SerializeField]
    Animator animator;

    [SerializeField]
    GameObject axe,gun,rageObject, RageButton, OpenCloseCanvas;

    [SerializeField]
    Transform axeTargetPoint;
    [SerializeField]
    Transform axePoint,axeStartPoint;

    [SerializeField]
    Transform handPoint;

    public Player player;

    private float checkInterval = 0.2f; // 0.5 saniye aralýklarla kontrol edilecek
    private float lastCheckedTime = 0f;

    private IEnumerator coroutine;


    public bool isThrowingAxe;
    public bool isRageSkill, isForceSkill;

    public GameObject GameUI1,GameUI1Of,GameUI2Of,GameUI2, GameUI3, GameUI3Of, GameUI4, GameUI4Of, GameUI5, GameUI5Of,GameUI6, GameUI6Of;

    private void Awake()
    {
    }
    private void Start()
    {
        // Coroutine'u baþlat
        coroutine = CheckMoney();
        StartCoroutine(coroutine);
    }
    private IEnumerator CheckMoney()
    {
        while (true)
        {
            // Kontrol kodlarý burada
            if (GameManager.instance.money >= 600)
            {
                GameUI4.SetActive(true);
                GameUI4Of.SetActive(false);
                GameUI5Of.SetActive(false);
                GameUI5.SetActive(true);
            }
            else
            {
                GameUI4.SetActive(false);
                GameUI4Of.SetActive(true);
                GameUI5Of.SetActive(true);
                GameUI5.SetActive(false);
            }

            if (GameManager.instance.money >= 250)
            {
                GameUI1.SetActive(true);
                GameUI1Of.SetActive(false);
                GameUI2.SetActive(true);
                GameUI2Of.SetActive(false);
            }
            else
            {
                GameUI2.SetActive(false);
                GameUI2Of.SetActive(true);
                GameUI1.SetActive(false);
                GameUI1Of.SetActive(true);
            }

            if (GameManager.instance.money >= 600)
            {
                GameUI3.SetActive(true);
                GameUI3Of.SetActive(false);
            }
            else
            {
                GameUI3.SetActive(false);
                GameUI3Of.SetActive(true);
            }

            yield return new WaitForSeconds(checkInterval);
        }
    }
    private void Update()
        {
        //if (Time.time - lastCheckedTime >= checkInterval)
        // {
        //lastCheckedTime = Time.time;

        //// Kontrol kodlarý burada
        //if (GameManager.instance.money >= 500)
        //{
        //    GameUI1.SetActive(true);
        //    GameUI1Of.SetActive(false);
        //    GameUI4.SetActive(true);
        //    GameUI4Of.SetActive(false);
        //    GameUI5Of.SetActive(false);
        //    GameUI5.SetActive(true);
        //}
        //if (GameManager.instance.money >= 200)
        //{
        //    GameUI2.SetActive(true);
        //    GameUI2Of.SetActive(false);
        //}
        //if (GameManager.instance.money >= 400)
        //{
        //    GameUI3.SetActive(true);
        //    GameUI3Of.SetActive(false);
        //}

        // }
        DamageText.text = damage.ToString();
        FireRateText.text = fireRate.ToString();
        SpeedText.text = joystickPlayer.speed.ToString();

        CheckEnemyFinder();

        if (isThereEnemy)
        {
            if (enemyFinder.enemiesInRange.Count > 0)
            {
                isActiceFire = true;

                Vector3 lTargetDir = enemyFinder.enemiesInRange[0].transform.position - transform.position;
                lTargetDir.y = 0.0f;
                transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(lTargetDir), Time.time * 1.50f);
             //   transform.LookAt(enemyFinder.enemiesInRange[0].transform);

                if (!isFireStarted)
                {
                    StartCoroutine(Fire(fireRate, enemyFinder.enemiesInRange[0].transform));

                    isFireStarted = true;
                    float randomPitch = Random.Range(0.8f, 1.1f);

                    GunSound.pitch = randomPitch;
                    GunSound.Play();


                }
            }

            
        }

        else
        {
            isActiceFire = false;
        }

    }

    public void BuyHealt()
    {
        if (GameManager.instance.money >= 200)
        {
            GameManager.instance.money -= 200;
            player.health += 2;
        }
        else
        {
            GameUI2Of.SetActive(true);
        }
    }
    public void BuyMaxHealt()
    {
        if (GameManager.instance.money >= 600)
        {
            GameManager.instance.money -= 600;
            player.health += 5;
        }
        else
        {
            GameUI3Of.SetActive(true);
        }
    }
    public void BuySpeed()
    {
        if (GameManager.instance.money >= 500)
        {
            GameManager.instance.money -= 500;
            joystickPlayer.prevSpeed+= 25;

        }
        else
        {
            GameUI4Of.SetActive(true);
        }
    }
    public void BuyNewWeapon()
    {
        if (GameManager.instance.money >= 600)
        {
            GameManager.instance.money -= 600;
            fireRate -= 0.2f;

        }
        else
        {
            GameUI5Of.SetActive(true);
        }
    }
    public void BuyDamage()
    {
        if(GameManager.instance.money >= 250)
        {
            damage += 0.15f;
            GameManager.instance.money -= 250;            
        }
        else
        {
       //     GameUI1.SetActive(true);
            GameUI1Of.SetActive(true);
        }
    }
    public void CheckEnemyFinder()
    {
       

        if (_enemyFinderTimer < Time.time)
        {
            _enemyFinderTimer = Time.time + 0.25f;

            if (enemyFinder.enemiesInRange.Count > 0)
            {
                isThereEnemy = true;


            }
            else
            {
                isThereEnemy = false;
            }

        }

    }

    public void FireAxe()
    {
        if (!isThrowingAxe)
        {
            StartCoroutine(WaitForceStop());
            knifeSound.Play();
            animator.SetLayerWeight(1, 0.5f);
            animator.SetBool("isFireAxe", true);
            isThrowingAxe = true;
            axe.SetActive(true);
            gun.SetActive(false);
      //      StartCoroutine(WaitForceStop());

            //if (joystickPlayer.speed != 0)
            //{
            //    joystickPlayer.prevSpeed = joystickPlayer.speed;
            //    joystickPlayer.ForceStop();
            //}
        }


    }
    public void RageSkill()
    {
        if (!isRageSkill)
        {
            
            animator.SetLayerWeight(1, 0.5f);
            animator.SetBool("isRageSkill", true);
            rageObject.SetActive(false);
            isRageSkill = true;
            axe.SetActive(true);
            gun.SetActive(false);
            StartCoroutine(UseRageSkill());

        }


    }
    IEnumerator WaitForceStop()
    {
        isForceSkill = true;
        yield return new WaitForSeconds(1.7f);   
        isForceSkill = false;



    }

    IEnumerator UseRageSkill()
    {

        // isRageSkill = false;
        player.health += 0.25f;
        RageButton.SetActive(false);
        rageObject.SetActive(true);
        yield return new WaitForSeconds(2f); 
        animator.SetBool("isRageSkill", false);
        isRageSkill = false;
        //rageObject.transform.DOScale(new Vector3(3, 3, 3), 0.2f).SetLoops(-1,LoopType.Restart);
        rageObject.transform.DOPunchScale(new Vector3(3, 3, 3),1,1,1);
        // rageObject.transform.DOShakeScale(0.5f, 0.5f);
        yield return new WaitForSeconds(2f);

        rageObject.SetActive(false);
        RageButton.SetActive(true);
        axe.SetActive(false);
        gun.SetActive(true);


    }


    public void ThrowTheAxe()
    {
        axe.transform.parent = transform;

        animator.SetBool("isFireAxe", false);
        
        axe.transform.DOLocalRotate(new Vector3(0, 0, 0), 0.2f);


        axe.transform.DOMove(axeTargetPoint.position,0.45f).SetEase(Ease.Linear).OnUpdate(()=> 
        {

            axe.transform.Rotate(0, 8, 0);

        }).OnComplete(()=> 
        {
           // StartCoroutine(WaitForceStop());

            MovePlayer(0.10f);
        });

    }

    void MovePlayer(float time)
    {
        if (axe.transform.position != axePoint.position)
        {
            axe.transform.DOMove(axePoint.position, time).SetEase(Ease.Linear).OnComplete(() =>
            {
                if (this != null)
                {
                    time -= 0.175f;
                    MovePlayer(time);
                }
            });
        }
        else
        {
            //StartCoroutine(WaitForceStop());

            //axe.transform.Rotate(0, 5, 0);

            animator.SetLayerWeight(1, 0);
            isThrowingAxe = false;
            axe.SetActive(false);
            gun.SetActive(true);
            axe.transform.parent = handPoint;
            axe.transform.SetPositionAndRotation(axeStartPoint.position, axeStartPoint.rotation);

        }

    }
        public IEnumerator Fire(float _fireRate, Transform target)
    {
        isActiceFire = true;

        if (target != null &&!isThrowingAxe)
        {
            GameObject poolObject = PoolManager.instance.Pull("Bullet", firePoint.position, Quaternion.identity);


            

            if (poolObject == null)
            {
                poolObject = Instantiate(bullet, firePoint.position, Quaternion.identity);

               
            }


            poolObject.GetComponent<ParticleCollisionInstance>().damage = damage;

            poolObject.GetComponent<ParticleCollisionInstance>().Setup(target);
        }

        else
        {
            isActiceFire = false;
        }

        yield return new WaitForSeconds(_fireRate);

            isFireStarted = false;
    
    }

}
