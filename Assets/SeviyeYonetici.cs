using UnityEngine;
using UnityEngine.SceneManagement;




public class SeviyeYonetici : MonoBehaviour
{
    private int sonKaydedilenSeviye;
    private bool oyunBasladi = false;
    private void Start()
    {
        // Kaydedilen en son seviyeyi yükleyin
        sonKaydedilenSeviye = PlayerPrefs.GetInt("SonKaydedilenSeviye", 1);

        // Son kaydedilen seviyeyi yükle
       // YenidenBaslat();
    }

    private void OnDisable()
    {
        // Uygulama kapatıldığında veya sahne değiştirildiğinde kaydedilen seviyeyi saklayın
        PlayerPrefs.SetInt("SonKaydedilenSeviye", sonKaydedilenSeviye);
        PlayerPrefs.Save();
    }

    public void SeviyeTamamlandi(int tamamlananSeviye)
    {
        if (tamamlananSeviye > sonKaydedilenSeviye)
        {
            sonKaydedilenSeviye = tamamlananSeviye;
        }

        int siradakiSeviye = sonKaydedilenSeviye + 1;
        if (siradakiSeviye <= SceneManager.sceneCountInBuildSettings - 1)
        {
            sonKaydedilenSeviye = siradakiSeviye; // Güncellenen son kaydedilen seviyeyi tut
        }

        // Seviye tamamlandığında otomatik olarak bir sonraki seviyeye geçmeyin,
        // bu işlemi oyun başlama mantığına entegre edin.
    }



    public void BaslatmaMantigi()
    {
        if (!oyunBasladi)
        {
            oyunBasladi = true;
            YenidenBaslat();
        }
    }

    public void YenidenBaslat()
    {
        int seviyeIndex = Mathf.Clamp(sonKaydedilenSeviye, 1, SceneManager.sceneCountInBuildSettings - 1);
        SceneManager.LoadSceneAsync(seviyeIndex);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Tetikleyiciye temas edildi. Konum: " + other.transform.position);
            SeviyeYonetici seviyeYonetici = FindObjectOfType<SeviyeYonetici>();
            seviyeYonetici.SeviyeTamamlandi(SceneManager.GetActiveScene().buildIndex);
        }
    }

}

