using UnityEngine;

public class MenuStart : MonoBehaviour
{
    [SerializeField] private GameObject menuCanvas; // arraste o Canvas aqui no Inspector
    [SerializeField] private GameObject otherCanvas; // Canvas que deve ser desativado quando o menu abrir



    void Start()
    {
        // Quando o jogo iniciar, abre o menu e pausa tudo
        menuCanvas.SetActive(true);
        otherCanvas.SetActive(false); // desliga o outro canvas

        Time.timeScale = 0f;
    }

    public void StartGame()
    {
        // Fecha o menu
        menuCanvas.SetActive(false);

        otherCanvas.SetActive(true);

      


        // Despausa o jogo
        Time.timeScale = 1f;
    }
}
