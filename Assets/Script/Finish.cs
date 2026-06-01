using UnityEngine;
using UnityEngine.SceneManagement;

public class Finish : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // Cek apakah yang menyentuh adalah Player
        if (other.CompareTag("Player"))
        {
            // Pindah ke scene Race2
            SceneManager.LoadScene("race2");
        }
    }
}