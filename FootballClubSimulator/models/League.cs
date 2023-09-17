namespace FootballClubSimulator.models;

public class League
{
    
    // ___________________ STATIC PROPERTIES/FIELDS _________________________________
    public static readonly int FirstAmountOfRounds = 22;
    public static readonly int SecondAmountOfRounds = 10;
    
    
    
    // ___________________ IMPORTANT PROPERTIES/FIELDS _________________________________
    public string LeagueName { get; set; }

    private List<Club> _teams = new List<Club>();
    public List<Club> Teams
    {
        get { return _teams; }
        set
        {
            value.ForEach(team =>
            {
                bool foundNewLeagueTeam = true;
                int indexOfLeagueTeam = _teams.FindIndex(leagueTeam => leagueTeam.ClubNameAbbreviated == team.ClubNameAbbreviated);
                
                if (indexOfLeagueTeam >= 0)
                {
                    _teams.RemoveAt(indexOfLeagueTeam);
                    _teams.Insert(indexOfLeagueTeam, team);
                    foundNewLeagueTeam = false;
                }

                if (foundNewLeagueTeam)
                {
                    team.League = this;
                }
            });
        }
    }
    

    private List<Round[]> _firstRounds = new List<Round[]>();
    
    public List<Round[]>? FirstRounds
    {
        get { return _firstRounds; }
        private set { _firstRounds = value ?? _firstRounds; }
    }
    
    private List<Round[]> _secondRounds = new List<Round[]>();
    
    public List<Round[]>? SecondRounds
    { 
        get { return _secondRounds; } 
        private set { _secondRounds = value ?? _secondRounds; }
    }
    // ___________________ OTHER PROPERTIES/FIELDS _________________________________
    

    private List<Club> _upperClubFraction = new List<Club>();

    public List<Club>? UpperClubFraction
    {
        get { return _upperClubFraction; }
        set { _upperClubFraction = value ?? _upperClubFraction; }
    }

    private List<Club> _lowerClubFraction = new List<Club>();

    public List<Club> LowerClubFraction
    {
        get { return _lowerClubFraction; }
        set { _lowerClubFraction = value ?? _lowerClubFraction; }
    }
    
    
    // ___________________ CONSTRUCTORS _________________________________
    public League(string leagueName)
    {
        LeagueName = leagueName;
        Teams = new List<Club>();
    }

    public League(string leagueName, List<Club> teams)
    {
        LeagueName = leagueName;
        Teams = teams;
    }

    
    
    // ___________________ CSV CONVERTER PROPERTIES/METHODS _________________________________
    
    public League(string[] leagueValues, List<Club> teams, List<Round[]> firstRounds, List<Round[]> secondRounds)
    {
        LeagueName = leagueValues[0];
        Teams = teams;
        FirstRounds = firstRounds;
        SecondRounds = secondRounds;
    }
    
    public static string ConvertHeaderToCsvFormat = "LeagueName";
    public string ConvertToCsvFormat()
    {
        return $"{LeagueName}";
    }
    
    // ___________________ SPECIAL METHODS _________________________________
    public List<Round[]> GetAllRoundsList()
    {
        List<Round[]> allRound = new List<Round[]>(_firstRounds);
        foreach (Round[] round in _secondRounds)
        {
            allRound.Add(round);
        }

        return allRound;
    }
}