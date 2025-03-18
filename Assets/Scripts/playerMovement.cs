using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public float speed = 5f;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;
    private Vector3 velocity;

    public Transform groundCheck;
    public float groundDistance = 0.5f; // Arttırıldı (0.4 -> 0.5)
    public LayerMask groundMask;
    private bool isGrounded;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        // 1. Yerde olup olmadığını kontrol et
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        // 2. Debug: Yerde olup olmadığını Console'a yaz
        // Debug.Log("isGrounded: " + isGrounded);

        // 3. Eğer yerdeyse, düşmeyi durdur
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; // -1 yerine daha stabil bir değer
        }

        // 4. Hareket girdilerini al
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        // 5. Hareket vektörünü oluştur
        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * speed * Time.deltaTime);

        // Zıplama işlemi (Yalnızca zemin temas halinde)
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity); // Zıplama için hızı hesapla
        }
        else if (velocity.y < 0)
        {
            velocity.y = 0;
        }

            velocity.y += gravity * Time.deltaTime;  // Havadayken yerçekimini uygula
        // 8. Hareketi uygula
        controller.Move(velocity * Time.deltaTime);
    }
}