using UnityEngine;
using UnityEngine.UI;

public class EducationPanelManager : MonoBehaviour
{
    public GameManager gameManager;

    // ================= EDUCATION =================
    public Button tradeSchoolButton;
    public Button gedButton;
    public Button bachelorsButton;
    public Button mbaButton;
    public Button phdButton;
    public Button mdButton;

    // ================= CAREERS =================
    public Button internButton;
    public Button administratorButton;
    public Button salesButton;
    public Button teamLeadButton;
    public Button managerButton;
    public Button directorButton;
    public Button vicePresidentButton;
    public Button cooButton;
    public Button boardButton;
    public Button daButton;
    public Button surgeonGeneralButton;

    // ================= TESTS =================
    public Button gedExamButton;
    public Button undergradExamButton;
    public Button mbaExamButton;
    public Button phdExamButton;
    public Button barExamButton;
    public Button mdExamButton;

    void Start()
    {
        // EDUCATION
        tradeSchoolButton.onClick.AddListener(TradeSchool);
        gedButton.onClick.AddListener(GetGED);
        bachelorsButton.onClick.AddListener(GetBachelors);
        mbaButton.onClick.AddListener(GetMBA);
        phdButton.onClick.AddListener(GetPHD);
        mdButton.onClick.AddListener(GetMD);

        // CAREERS
        internButton.onClick.AddListener(Intern);
        administratorButton.onClick.AddListener(Administrator);
        salesButton.onClick.AddListener(Sales);
        teamLeadButton.onClick.AddListener(TeamLead);
        managerButton.onClick.AddListener(Manager);
        directorButton.onClick.AddListener(Director);
        vicePresidentButton.onClick.AddListener(VicePresident);
        cooButton.onClick.AddListener(COO);
        boardButton.onClick.AddListener(BoardOfDirectors);
        daButton.onClick.AddListener(DistrictAttorney);
        surgeonGeneralButton.onClick.AddListener(SurgeonGeneral);

        // TESTS
        gedExamButton.onClick.AddListener(GEDExam);
        undergradExamButton.onClick.AddListener(UndergradExam);
        mbaExamButton.onClick.AddListener(MBAExam);
        phdExamButton.onClick.AddListener(PHDExam);
        barExamButton.onClick.AddListener(BarExam);
        mdExamButton.onClick.AddListener(MDExam);
    }

    bool CanAct() => gameManager != null && !gameManager.gameOver;

    // ================= CORE ACTION =================
    void Process(string failMsg, float cost, float lifeGain, float repGain, System.Action success)
    {
        if (!CanAct()) return;

        if (gameManager.money < cost)
        {
            gameManager.PrintMessage("Not enough money.");
            return;
        }

        if (failMsg != null)
        {
            gameManager.PrintMessage(failMsg);
            return;
        }

        gameManager.AddMoney(-cost);
        gameManager.IncreaseMaxAge(lifeGain);
        gameManager.AddReputation(repGain);
        success?.Invoke();
    }

    // ================= EDUCATION =================
    void TradeSchool()
    {
        if (gameManager.wentToTradeSchool)
        {
            gameManager.PrintMessage("You already went to trade school.");
            return;
        }

        Process(
            null,
            100,
            4f,
            100,
            () =>
            {
                gameManager.wentToTradeSchool = true;
                gameManager.PrintMessage("Go to trade school.");
            }
        );
    }

    void GetGED()
    {
        Process(
            gameManager.passedGEDExam ? null : "Must pass GED test.",
            100,
            2f,
            100,
            () =>
            {
                gameManager.hasGED = true;
                gameManager.PrintMessage("Obtain GED.");
            }
        );
    }

    void GetBachelors()
    {
        Process(
            gameManager.passedUndergradExam ? null : "Must pass Undergrad test.",
            100,
            4f,
            100,
            () =>
            {
                gameManager.hasBachelors = true;
                gameManager.PrintMessage("Obtain Bachelor's Degree.");
            }
        );
    }

    void GetMBA()
    {
        Process(
            gameManager.passedMBAExam ? null : "Must pass MBA test.",
            100,
            3f,
            100,
            () =>
            {
                gameManager.hasMBA = true;
                gameManager.PrintMessage("Obtain MBA.");
            }
        );
    }

    void GetPHD()
    {
        Process(
            gameManager.passedPHDExam ? null : "Must pass PHD exam.",
            100,
            6f,
            100,
            () =>
            {
                gameManager.hasPHD = true;
                gameManager.PrintMessage("Obtain PHD.");
            }
        );
    }

    void GetMD()
    {
        Process(
            gameManager.passedMDExam ? null : "Must pass MD exam.",
            100,
            8f,
            100,
            () =>
            {
                gameManager.hasMD = true;
                gameManager.PrintMessage("Obtain MD.");
            }
        );
    }

    // ================= CAREERS =================
    void Career(string fail, string msg)
    {
        Process(fail, 100, 1f, 100, () => gameManager.PrintMessage(msg));
    }

    void Intern() =>
        Career(gameManager.wentToTradeSchool && gameManager.reputation >= 50 ? null : "Must go to trade school + reputation 50", "Become an intern.");

    void Administrator() =>
        Career(gameManager.wentToTradeSchool ? null : "Must go to trade school.", "Become an administrator.");

    void Sales() =>
        Career(gameManager.wentToTradeSchool ? null : "Must obtain GED.", "Become a sales person.");

    void TeamLead() =>
        Career(gameManager.hasGED && gameManager.reputation >= 60 ? null : "Must obtain GED + reputation 60", "Become a team lead.");

    void Manager() =>
        Career(gameManager.hasBachelors ? null : "Must obtain Bachelor's degree.", "Become the manager.");

    void Director() =>
        Career(gameManager.hasBachelors ? null : "Must obtain Bachelor's degree.", "Become the director.");

    void VicePresident() =>
        Career(gameManager.hasMBA && gameManager.reputation >= 70 ? null : "Must pass MBA + reputation 70", "Become the vice president.");

    void COO() =>
        Career(gameManager.hasMBA && gameManager.reputation >= 75 ? null : "Must pass MBA + reputation 75", "Become the COO.");

    void BoardOfDirectors() =>
        Career(gameManager.hasPHD && gameManager.reputation >= 80 ? null : "Must pass PHD + reputation 80", "Become Board of Directors.");

    void DistrictAttorney() =>
        Career(gameManager.passedBarExam && gameManager.reputation >= 85 ? null : "Must pass JD + reputation 85", "Become DA.");

    void SurgeonGeneral() =>
        Career(gameManager.hasMD && gameManager.reputation >= 90 ? null : "Must pass MD + reputation 90", "Become Surgeon General.");

    // ================= TESTS =================
    void Exam(string fail, System.Action success)
    {
        Process(fail, 100, 0f, 0f, success);
    }

    void GEDExam() =>
        Exam(gameManager.wentToTradeSchool ? null : "Must go to trade school.", () =>
        {
            gameManager.passedGEDExam = true;
            gameManager.PrintMessage("GED unlocked.");
        });

    void UndergradExam() =>
        Exam(gameManager.hasGED ? null : "Must obtain GED.", () =>
        {
            gameManager.passedUndergradExam = true;
            gameManager.PrintMessage("Undergraduate unlocked.");
        });

    void MBAExam() =>
        Exam(gameManager.hasBachelors ? null : "Must obtain Bachelor.", () =>
        {
            gameManager.passedMBAExam = true;
            gameManager.PrintMessage("MBA unlocked.");
        });

    void PHDExam() =>
        Exam(gameManager.hasMBA ? null : "Must obtain MBA.", () =>
        {
            gameManager.passedPHDExam = true;
            gameManager.PrintMessage("PHD unlocked.");
        });

    void BarExam() =>
        Exam(gameManager.hasPHD ? null : "Must obtain PHD.", () =>
        {
            gameManager.passedBarExam = true;
            gameManager.PrintMessage("JD unlocked.");
        });

    void MDExam() =>
        Exam(gameManager.passedBarExam ? null : "Must obtain JD.", () =>
        {
            gameManager.passedMDExam = true;
            gameManager.PrintMessage("MD unlocked.");
        });
}
