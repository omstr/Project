using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StatsButtonController : MonoBehaviour
{
    // Start is called before the first frame update
    private RectTransform g1Obj;
    private RectTransform g2Obj;
    private RectTransform g3Obj;
    private RectTransform homePage;
    private RectTransform perfPanel;
    private TextMeshProUGUI successText;
    private TextMeshProUGUI successText1;
    private TextMeshProUGUI successText2;
    private TextMeshProUGUI successText3;
    private Image background;
    private Image background1;
    private Image background2;
    private Image background3;
    private RectTransform windowGraph1;
    private RectTransform windowGraph2;
    private RectTransform windowGraph3;
    
   
    private void Awake()
    {
        g1Obj = transform.Find("G1Obj").GetComponent<RectTransform>();
        g2Obj = transform.Find("G2Obj").GetComponent<RectTransform>();
        g3Obj = transform.Find("G3Obj").GetComponent<RectTransform>();
        homePage = transform.Find("HomePage").GetComponent<RectTransform>();

    }

    public void LoadHomeMethods()
    {
        BestGameStat bGS = new BestGameStat();
        bGS.BestGame();
        AveragePerformance avgPerf = new AveragePerformance();
        
        avgPerf.CalculateAverages();
    }
    public void LoadHomeProfile()
    {

        g1Obj.gameObject.SetActive(false);
        g2Obj.gameObject.SetActive(false);
        g3Obj.gameObject.SetActive(false);
        homePage.gameObject.SetActive(true);
        perfPanel = homePage.Find("AveragePerfPanel").GetComponent<RectTransform>();
        successText = perfPanel.Find("AverageSuccessText").GetComponent<TextMeshProUGUI>();
        background = perfPanel.Find("Image1").GetComponent<Image>();
        AveragePerformance aVP = new AveragePerformance();
        aVP.CalculateAverages();
    }
    public void LoadGame1ProfileCor()
    {
        StartCoroutine(LoadGame1Profile());
    }
    IEnumerator LoadGame1Profile()
    {

        g1Obj.gameObject.SetActive(true);
        g2Obj.gameObject.SetActive(false);
        g3Obj.gameObject.SetActive(false);
        homePage.gameObject.SetActive(false);
        AveragePerformance1 aVP = new AveragePerformance1();
        aVP.CalculateG1Averages();
        yield return new WaitForSeconds(0.1f);
        GraphPlotter1 gP1 = new GraphPlotter1();
        //gP1.DisplayGraphFloat(DataHandler.game1SessionSuccessRate);
    }
    public void LoadGame2ProfileCor()
    {
        StartCoroutine(LoadGame2Profile());
    }
    IEnumerator LoadGame2Profile()
    {

        g1Obj.gameObject.SetActive(false);
        g2Obj.gameObject.SetActive(true);
        g3Obj.gameObject.SetActive(false);
        homePage.gameObject.SetActive(false);
        AveragePerformance aVP = new AveragePerformance();
        aVP.CalculateG2Averages();
        yield return new WaitForSeconds(0.1f);
        
        //gP1.DisplayGraphFloat(DataHandler.game1SessionSuccessRate);
    }
    public void LoadGame3ProfileCor()
    {
        StartCoroutine(LoadGame3Profile());
    }
    IEnumerator LoadGame3Profile()
    {

        g1Obj.gameObject.SetActive(false);
        g2Obj.gameObject.SetActive(false);
        g3Obj.gameObject.SetActive(true);
        homePage.gameObject.SetActive(false);
        AveragePerformance aVP = new AveragePerformance();
        aVP.CalculateG3Averages();
        yield return new WaitForSeconds(0.1f);
        
        //gP1.DisplayGraphFloat(DataHandler.game1SessionSuccessRate);
    }


}
