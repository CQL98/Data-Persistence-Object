using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class MenuUIHandler : MonoBehaviour
{
    [SerializeField] private GameObject principalPanel;
    [SerializeField] private GameObject scorePanel;
    [SerializeField] private TMP_InputField nameInput;
    [SerializeField] private TextMeshProUGUI dataTable;
    [SerializeField] private TextMeshProUGUI scoreText;

    private bool isPrincipalPanel = true;

    private const string dummyRegister = "|DD/MM/YYYY|     ------    |00000|\n";
    // Start is called before the first frame update
    void Start()
    {
        LoadAllScores();
        PlayerScore playerScore = DataManager.Instance.GetTopUser();
        scoreText.text = "Best Score : " + playerScore.nameX + " : " + playerScore.scoreX;

    }

    // Update is called once per frame
    void Update()
    {

    } 
    public void ChangePanel()
    {
        isPrincipalPanel = !isPrincipalPanel;
        principalPanel.SetActive(isPrincipalPanel);
        scorePanel.SetActive(!isPrincipalPanel);
        if (!isPrincipalPanel)
        {
            LoadAllScores();
        }
    }
    public void StartGame()
    {
        if (nameInput.text.Length == 0)
        {
            nameInput.text = "NO NAME";
        }
        DataManager.Instance.actualPlayerName = nameInput.text; 
        ChangeScene(1);
    }
    public void ChangeScene(int indexScene)
    {
        SceneManager.LoadScene(indexScene);
    }

    public void LoadAllScores()
    {
        DataManager.Instance.LoadUsers();
        List<PlayerScore> topScores = DataManager.Instance.listPlayerScore;
        int dummyRegister = topScores.Count - DataManager.Instance.numberOfTopPlayers;
        string auxTable = "";
        foreach (var scoreItem in topScores)
        { 
            auxTable += "|" + scoreItem.dateX + "|" + DataManager.FormatName(scoreItem.nameX) + "|" + DataManager.FormatScore(scoreItem.scoreX) + "|" + "\n";
        }
        if (dummyRegister < 0)
        {
            for (int i = 0; i < -dummyRegister; i++)
            { 
                auxTable += MenuUIHandler.dummyRegister;
            }
        }
        dataTable.text = auxTable;
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
        Application.Quit(); 
#endif
    }

}
