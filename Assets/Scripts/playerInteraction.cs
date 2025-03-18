using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public float interactionDistance = 3f; // Etkileşim mesafesi
    public GameObject objectToInteract; // Etkileşim yapılacak nesne
    private Transform playerTransform;
    private bool isInRange = false; // Etkileşim mesafesinde olup olmadığını kontrol eder

    void Start()
    {
        playerTransform = Camera.main?.transform;

        if (playerTransform == null)
        {
            Debug.LogError("Hata: Main Camera bulunamadı! Lütfen sahnede bir kamera olduğundan emin olun.");
            return;
        }
    }

    void Update()
    {
        if (playerTransform != null && objectToInteract != null)
        {
            // Mesafeyi kontrol et
            float distance = Vector3.Distance(playerTransform.position, objectToInteract.transform.position);

            // Debug: Mesafeyi konsola yazdır
            // Debug.Log("Mesafe: " + distance);

            if (distance <= interactionDistance)
            {
                if (!isInRange)
                {
                    isInRange = true;
                    Debug.Log("Objeye yaklaşıldı. 'E' tuşuna basarak etkileşime girebilirsiniz.");
                }

                if (Input.GetKeyDown(KeyCode.E))
                {
                    // Etkileşim başlatılacak obje
                    ObjectInteraction objectInteraction = objectToInteract.GetComponent<ObjectInteraction>();

                    if (objectInteraction != null)
                    {
                        Debug.Log("Etkileşim başlatılıyor...");
                        objectInteraction.SetButtonsActive(true); // Butonları aktif et
                    }
                    else
                    {
                        Debug.LogError("Hata: ObjectInteraction script'i atanmadı!");
                    }
                }
            }
            else if (isInRange)
            {
                isInRange = false;
                Debug.Log("Objeden uzaklaşıldı.");
            }
        }
    }
}