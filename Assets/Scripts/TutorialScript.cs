using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Playables;
using System;
using System.Linq;
public class TutorialScript : MonoBehaviour

{
    [SerializeField] private Button ExitButton;
    [SerializeField] private Button ForwardButton;
    [SerializeField] private Button BackButton;
    [SerializeField] private TMP_Text pageText;
    private int page = 0;
    private string firstText;

    public PlayableDirector director;
    public GameObject[] tutorials;


    void Start()
    {
        ExitButton.onClick.AddListener(BackToMap);
        ForwardButton.onClick.AddListener(GoForward);
        BackButton.onClick.AddListener(GoBack);
        BackButton.gameObject.SetActive(false);
        ForwardButton.gameObject.SetActive(true);
        firstText = pageText.text;
        
        
    }
    public void BackToMap()
    {
        Debug.Log("Quit");        
        SceneManager.LoadScene("Scenes/MapScene");        
        AudioManager.instance.PlayMusic("MapMusic");  
               
    }

    public void GoForward()
    {
        
        page++;
        if(page == 1)
        {
            BackButton.gameObject.SetActive(true);
        }
        else if(page==12)
        {
            //ForwardButton.gameObject.SetActive(false);
            BackToMap();
        }
        Debug.Log(page);
        IniciarSequencia();
        DisablePanels();
        tutorials[page -1].SetActive(true);
        ChangeText();
    }

    public void GoBack()
    {
        Debug.Log("Back");
        page--;
        if(page <= 0)
        {
            BackButton.gameObject.SetActive(false);
        }
        else if(page==10)
        {
            ForwardButton.gameObject.SetActive(true);
        }
        Debug.Log(page);
        IniciarSequencia();
        DisablePanels();
        if(page>1)tutorials[page -1].SetActive(true);        
        ChangeText();
    }

    private void ChangeText()
    {
        switch (page)
            {
                case  1:
                    pageText.text = "Sua missão é levar o Trem até a estação final "+
                                    "antes que seus inimigos destruam os trilhos.";  
                    tutorials[page -1].SetActive(true);
                    
                    break;

                case  2:    
                    pageText.text = "Construa a ferrovia utilizando os trilhos na loja ao lado. "+
                                    "Utilize o botão ESQUERDO do mouse para COMPRAR e ARRASTAR os trilhos.";
                                                                           
                    
                    break;
                case  3:
                    pageText.text = "Clique com o botão DIREITO do mouse para GIRAR os trilhos.";
                    break;

                case  4:
                    pageText.text = "Cuidado para não gastar todo o combustível.";
                    break;

                case  5:
                    pageText.text = "Para conseguir mais combustível, colete os recursos do mapa."+
                                    " Alguns recursos podem estar escondidos ";
                    break;

                case  6:
                    pageText.text = "A bomba pode destruir alguns obstáculos, mas nem todos";
                    break;

                case  7:
                    pageText.text = "A Ponte pode ser utilizada para atravessar obstáculos.";
                    break;

                case  8:
                    pageText.text = "Recursos especiais podem ser comprados para aumentar o tempo"+
                                    " ou a velocidade do trem.";
                    break;

                case  9:
                    pageText.text = " Para iniciar aperte o Play ou espere o tempo inicial zerar."+
                                    " Ao som do apito começa a contagem regressiva para chegar a Estação";                                    
                    break;

                case  10:
                    pageText.text = "Ficou sem combustível? Você pode vender o trilho de volta a loja.";
                    break;   

                case  11:
                    pageText.text = "Bom jogo e Boa Sorte!.";
                    break;                                                   

                case 0:
                    pageText.text = firstText;
                    break;          
            }
    }

     public void IniciarSequencia()
    {
        // Inicia a reprodução da Timeline
        director.Play();
    }

    public void PausarSequencia()
    {
        // Pausa no ponto atual
        director.Pause();
    }

    public void PararSequencia()
    {
        // Para e reseta o tempo para zero
        director.Stop();
    }
   
    private void DisablePanels()
    {
        for(int i = 0; i<tutorials.Length; i++)
        {
            tutorials[i].SetActive(false);
        }
    }
   
}


