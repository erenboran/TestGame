using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;


public class BuyNewArea : MonoBehaviour,IBuyable
{
    [SerializeField] Text priceText;
    [SerializeField] int price;
    [SerializeField] Image frameBar;
    [SerializeField] GameObject obstackle;
    [SerializeField] GameObject coinPrefab;
    [SerializeField] Transform cointEndPoint;
    
    [SerializeField]
    bool isPlayerHere;
    int currentPrice;
    Sequence mySequence;

    public ParticleSystem Confeti;

    [SerializeField] GameObject[] openAreas;
    [SerializeField] GameObject[] closeAreas;

    [SerializeField] AudioSource WinSound;






    private void Start()
    {
        currentPrice = price;
        priceText.text = currentPrice.ToString();
       

    }


    public void OnBuyArea(bool isPlayerHere,Vector3 coinSpawnPoint)
    {
        this.isPlayerHere = isPlayerHere;
        if (isPlayerHere)
        {
            StartCoroutine(Buy(coinSpawnPoint));
        }
        
    }

    IEnumerator Buy(Vector3 coinSpawnPoint)
    {
        float timer=1;
        float multiplier = 1;
      
        while (currentPrice > 0 && isPlayerHere && GameManager.instance.money>1)
        {
            for (int i = 0; i < (int)timer; i++)
            {
                if (currentPrice>0 && GameManager.instance.money>0)
                {
                    currentPrice--;
                    
                    GameManager.instance.money--;
                }
            }
          
          
            frameBar.fillAmount = (float)(price - currentPrice) / price;
            

            GameObject coin = PoolManager.instance.Pull("Coin", coinSpawnPoint, Quaternion.identity);

            if (coin == null)
            {
                coin = Instantiate(coinPrefab, coinSpawnPoint, Quaternion.identity);
            }

            coin.transform.DOShakePosition(0.924f,1,10,90);
            
            coin.transform.DOMove(cointEndPoint.position, 0.925f).SetEase(Ease.InOutCubic).OnComplete(()=>
            {
                PoolManager.instance.Push(coin, "Coin");
                int a = int.Parse(priceText.text);
                mySequence.Kill();
                mySequence = DOTween.Sequence();
             
               
                mySequence.Append(DOTween.To(() => a, x => a = x, currentPrice, 0.36f).OnUpdate(() =>
                {
                    priceText.text = a.ToString();
                   
                }));



                priceText.text = currentPrice.ToString();

            });
         

            yield return new WaitForSeconds(0.125f);
            multiplier += Random.Range(0,0.5f);
            timer += multiplier;

            if (currentPrice == 0)
            {
                yield return new WaitForSeconds(1);
              
                float localScale = 1;

                Confeti.Play();

                WinSound.Play();

                for (int i = 0; i < openAreas.Length; i++)
                {
                    if (i== openAreas.Length-1)
                    {
                        openAreas[i].transform.DOScale(new Vector3(1, 1, 1), 0.125f).OnComplete(() =>
                        {

                            Destroy(obstackle);
                            // openArea.transform.DOShakeScale(0.25f, 0.2f, 20, 0);
                            float waitTimer = 0;

                            DOTween.To(() => 0, x => waitTimer = x, 50000, 0.125f).OnComplete(() =>
                            {
                                openAreas[i].transform.DOShakeScale(0.5f, 0.5f);
                                openAreas[i].transform.DOShakePosition(0.5f, 0.5f);
                                openAreas[i].transform.DOShakeRotation(0.5f, 0.5f);
                            });

                        });
                    }
                    else
                    {
                        openAreas[i].transform.DOScale(new Vector3(1, 1, 1), 0.125f);
                    }

                    

                }

               

                DOTween.To(() => 1, x => localScale = x, 0.0125f, 0.75f).OnUpdate(() => 
                {
                    
                    for (int i = 0; i < closeAreas.Length; i++)
                    {
                        closeAreas[i].transform.localScale = new Vector3(localScale, localScale, localScale);
                    }

                }).OnComplete(()=> {

                    for (int i = 0; i < closeAreas.Length; i++)
                    {
                        closeAreas[i].transform.localScale = new Vector3(0, 0, 0);
                        Destroy(closeAreas[i]);
                    }

                    

                });


            }
        }

       

      
    }
}
