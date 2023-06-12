using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class BuyMineMachine : MonoBehaviour,IBuyable
{


    [SerializeField] Text priceText;
    [SerializeField] int price;
    [SerializeField] Image frameBar;
    [SerializeField] GameObject obstackle;
    [SerializeField] GameObject coinPrefab;
    [SerializeField] Transform cointEndPoint;
    [SerializeField] GameObject robotPrefab;
    [SerializeField] Transform robotSpawnPoint;

    [SerializeField]
    bool isPlayerHere;
    [SerializeField] int currentPrice;
    Sequence mySequence;

    [SerializeField] GameObject[] openAreas;
    [SerializeField] GameObject[] closeAreas;

    [SerializeField]
    Transform startPoint, unloadPoint;
    [SerializeField]
    Transform[] path;

    private void Start()
    {
        currentPrice = price;
        priceText.text = currentPrice.ToString();


    }


    public void OnBuyArea(bool isPlayerHere, Vector3 coinSpawnPoint)
    {
        this.isPlayerHere = isPlayerHere;
        if (isPlayerHere)
        {
            StartCoroutine(Buy(coinSpawnPoint));
        }

    }

    IEnumerator Buy(Vector3 coinSpawnPoint)
    {
        float timer = 1;
        float multiplier = 1;

        while (currentPrice > 0 && isPlayerHere && GameManager.instance.money > 1)
        {
            for (int i = 0; i < (int)timer; i++)
            {
                if (currentPrice > 0 && GameManager.instance.money > 0)
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

            coin.transform.DOShakePosition(0.924f, 1, 10, 90);

            coin.transform.DOMove(cointEndPoint.position, 0.925f).SetEase(Ease.InOutCubic).OnComplete(() =>
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
            multiplier += Random.Range(0, 0.5f);
            timer += multiplier;

            if (currentPrice == 0)
            {
                yield return new WaitForSeconds(1);

                GameObject newRobot = Instantiate(robotPrefab,robotSpawnPoint.position, robotSpawnPoint.rotation);

                newRobot.GetComponent<MiningMachine>().TakePoints(unloadPoint, startPoint, path);

                frameBar.fillAmount = 0;

                price = price * 2;

                currentPrice = price;

                isPlayerHere = false;

                float newPrice = 50;

                DOTween.To(() => newPrice, x => newPrice = x, currentPrice, 0.36f).OnUpdate(() =>
                {
                    priceText.text = currentPrice.ToString();

                });


            }
        }




    }

}
