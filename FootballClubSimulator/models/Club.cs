namespace FootballClubSimulator.models;

public class Club
{
    
    // ___________________ IMPORTANT PROPERTIES/FIELDS _________________________________
    public string ClubName { private set; get; }

    public string ClubNameAbbreviated { private set; get; }
   
    public string LeagueName
    {
        get {
            if (_league == null)
            {
                return "Missing League";
            }
            return _league.LeagueName;
        }
    }
    
    public char SpecialRankingThisYear { set; get; }
    public char SpecialRankingLastYear { set; get; }

    public int Defense { set; get; }
    public int Offense { set; get; }
    
    
    private League? _league;
        public League League
        {
            set
            { 
                if (_league != null && LeagueName != value?.LeagueName) 
                { 
                    Club foundPastLeagueTeam = _league?.Teams.Find(team => team.ClubNameAbbreviated == this.ClubNameAbbreviated);
                    if (foundPastLeagueTeam != null)
                    {
                        _league?.Teams.Remove(foundPastLeagueTeam);
                    }
                }
                if (value != null)
                {
                    bool foundNewLeagueTeam = !value.Teams.Exists(team => team.ClubNameAbbreviated == this.ClubNameAbbreviated);
                    if (foundNewLeagueTeam) 
                    {
                        value.Teams.Add(this);
                    }
                }
                _league = value;
            }
            get { return _league; }
        }
    // ___________________ OTHER PROPERTIES/FIELDS _________________________________
    
    // Standing variables
    private List<char> _streak = new List<char>();

    public List<char> Streak
    {
        get
        {
            return _streak;
        }
    }

    public int PositionInTable { get; set; }
    public int MatchesPlayed { get; set; }
    public int GamesWon { get; set; }
    public int GamesDrawn { get; set; }
    public int GamesLost { get; set; }
    public int GoalsFor { get; set; }
    public int GoalsAgainst { get; set; }
    public int GoalDifference
    {
        get => GoalsFor - GoalsAgainst;
    }
    public int Points { get; set; }

    // ___________________ CONSTRUCTORS _________________________________
    public Club(string clubName, string clubNameAbbreviated, League league)
    {
        ClubName = clubName;
        ClubNameAbbreviated = clubNameAbbreviated;
        League = league;
        Defense = RollRandomStat();
        Offense = RollRandomStat();

    }
    public Club(string clubName, string clubNameAbbreviated, League league, int defense, int offense)
    {
        ClubName = clubName;
        ClubNameAbbreviated = clubNameAbbreviated;
        League = league;
        Defense = LimitStat(defense);
        Offense = LimitStat(offense);
    }

    
    
    // ___________________ CSV CONVERTER PROPERTIES/METHODS _________________________________
    
    public Club(string[] clubValues)
    {
        ClubNameAbbreviated = clubValues[0];
        ClubName = clubValues[1];

        SpecialRankingLastYear = Convert.ToChar(clubValues[3]);
        Defense = Convert.ToInt32(clubValues[4]);
        Offense = Convert.ToInt32(clubValues[5]);
    }

    public static readonly string ConvertHeaderToCsvFormat = "ClubNameAbbreviated,ClubName,LeagueName,SpecialRankingLastYear,Defense,Offense";
    public string ConvertToCsvFormat()
    {
        return $"{ClubNameAbbreviated},{ClubName},{LeagueName},{SpecialRankingThisYear},{Defense},{Offense}";
    }
    
    
    
    // ___________________ SPECIAL METHODS _________________________________

    public void CalculateStreak(char streakChar)
    {
        if (Streak.Count >= 5)
        {
            Streak.RemoveAt(0);
        }
        Streak.Add(streakChar);
    }

    public string GetStreak()
    {
        string streak = "";
        if (Streak.Count == 0)
        {
            return "-";
        }
        foreach (char streakChar in Streak)
        {
            streak += $"{streakChar}.";
        }

        return streak;
    }

    public void ResetStandings()
    {
        Streak.Clear();
        PositionInTable = 0;
        MatchesPlayed = 0;
        GamesWon = 0;
        GamesDrawn = 0;
        GamesLost = 0;
        GoalsFor = 0;
        GoalsAgainst = 0;
        Points = 0;
    }
 
    
    private int LimitStat(int stat)
    {
        if (stat > 10)
        {
            return 10;
        }
        else if (stat < 1)
        {
            return 1;
        }
        else
        {
            return stat;
        }
    }

    private int RollRandomStat()
    {
        int maxStat = 10;
        Random random = new Random();
        int randomStat = random.Next(maxStat) + 1;
        return randomStat;
    }
}
