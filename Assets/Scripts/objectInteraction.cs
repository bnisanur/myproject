using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems; // EventSystem kullanımı için gerekli

public class ObjectInteraction : MonoBehaviour
{
    public float interactionDistance = 3f;
    public TextMeshProUGUI interactionText;
    public GameObject objectToInteract;
    public Button destroyButton, duplicateButton, moveZButton, moveUpButton, rotateButton;

    private bool isInRange = false;
    private Transform playerTransform;

    void Start()
    {
        destroyButton.interactable = true;

        // Kamera kontrolü
        if (Camera.main != null)
        {
            playerTransform = Camera.main.transform;
        }
        else
        {
            Debug.LogError("Hata: Main Camera bulunamadı.");
            return;
        }

        // Interaction metnini ve butonları başta gizle
        SetButtonsActive(false);
        if (interactionText != null)
        {
            interactionText.enabled = false;
        }
        else
        {
            Debug.LogError("Hata: interactionText atanmadı.");
        }

        // EventSystem kontrolü
        if (FindObjectOfType<EventSystem>() == null)
        {
            Debug.LogError("Hata: EventSystem sahnede bulunamadı.");
        }

        // Butonları kontrol et ve eventleri temizleyip tekrar ekle
        if (destroyButton && duplicateButton && moveZButton && moveUpButton && rotateButton)
        {
            destroyButton.onClick.RemoveAllListeners();
            duplicateButton.onClick.RemoveAllListeners();
            moveZButton.onClick.RemoveAllListeners();
            moveUpButton.onClick.RemoveAllListeners();
            rotateButton.onClick.RemoveAllListeners();

            // Butonlara event ekleyerek her buton tıklandığında fonksiyonların çalışmasını sağla
            destroyButton.onClick.AddListener(DestroyObject);
            duplicateButton.onClick.AddListener(DuplicateObject);
            moveZButton.onClick.AddListener(MoveObjectZ);
            moveUpButton.onClick.AddListener(MoveObjectUp);
            rotateButton.onClick.AddListener(RotateObject);

            Debug.Log("Buton eventleri başarıyla bağlandı.");
        }
        else
        {
            Debug.LogError("Hata: Biri veya daha fazla buton atanmadı. Inspector'dan kontrol et.");
        }
    }

void Update()
{
    if (playerTransform == null) return;

    float distance = Vector3.Distance(playerTransform.position, transform.position);

    if (distance <= interactionDistance)
    {
        if (!isInRange)
        {
            isInRange = true;
            if (interactionText != null)
            {
                interactionText.enabled = true;
                interactionText.text = "Press 'E' to interact";
            }
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            SetButtonsActive(true);
            if (interactionText != null) interactionText.enabled = false;
        }
    }
    else if (isInRange)
    {
        isInRange = false;
        if (interactionText != null) interactionText.enabled = false;
        SetButtonsActive(false);
    }
}

    public void DestroyObject()
    {
        Debug.Log("DestroyObject() butonu tıklandı.");
        if (objectToInteract != null)
        {
            Debug.Log("DestroyObject() fonksiyonu çalışıyor.");
            Destroy(objectToInteract);
        }
        else
        {
            Debug.LogError("DestroyObject() hatası: `objectToInteract` atanmadı! Lütfen Inspector'dan bir nesne atayın.");
        }
    }

    public void DuplicateObject()
    {
        Debug.Log("DuplicateObject() butonu tıklandı.");
        if (objectToInteract != null)
        {
            Instantiate(objectToInteract, objectToInteract.transform.position, objectToInteract.transform.rotation);
        }
        else
        {
            Debug.LogError("DuplicateObject() hatası: `objectToInteract` atanmadı! Lütfen Inspector'dan bir nesne atayın.");
        }
    }

    public void MoveObjectZ()
    {
        Debug.Log("MoveObjectZ() butonu tıklandı.");
        if (objectToInteract != null)
        {
            objectToInteract.transform.position += new Vector3(0, 0, 1);
        }
        else
        {
            Debug.LogError("MoveObjectZ() hatası: `objectToInteract` atanmadı! Lütfen Inspector'dan bir nesne atayın.");
        }
    }

    public void MoveObjectUp()
    {
        Debug.Log("MoveObjectUp() butonu tıklandı.");
        if (objectToInteract != null)
        {
            objectToInteract.transform.position += new Vector3(0, 1, 0);
        }
        else
        {
            Debug.LogError("MoveObjectUp() hatası: `objectToInteract` atanmadı! Lütfen Inspector'dan bir nesne atayın.");
        }
    }

    public void RotateObject()
    {
        Debug.Log("RotateObject() butonu tıklandı.");
        if (objectToInteract != null)
        {
            objectToInteract.transform.Rotate(0, 30, 0);
        }
        else
        {
            Debug.LogError("RotateObject() hatası: `objectToInteract` atanmadı! Lütfen Inspector'dan bir nesne atayın.");
        }
    }

    public void SetButtonsActive(bool isActive)
    {
        destroyButton?.gameObject.SetActive(isActive);
        duplicateButton?.gameObject.SetActive(isActive);
        moveZButton?.gameObject.SetActive(isActive);
        moveUpButton?.gameObject.SetActive(isActive);
        rotateButton?.gameObject.SetActive(isActive);

        Debug.Log("Butonlar aktif mi? " + isActive);        
    }
}