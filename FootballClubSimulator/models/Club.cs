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

    public int Defense { set; get; }
    public int Offense { set; get; }
    
    // ___________________ OTHER PROPERTIES/FIELDS _________________________________
    private League? _league;
    public League League
    {
        set
        { 
            if (_league != null && LeagueName != value?.LeagueName) 
            { 
                Club foundPastLeagueTeam = _league.Teams.Find(team => team.ClubNameAbbreviated == this.ClubNameAbbreviated);
                if (foundPastLeagueTeam != null)
                {
                    _league.Teams.Remove(foundPastLeagueTeam);
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
        ClubName = clubValues[0];
        ClubNameAbbreviated = clubValues[1];
        Defense = Convert.ToInt32(clubValues[3]);
        Offense = Convert.ToInt32(clubValues[4]);
    }

    public static string ConvertHeaderToCsvFormat = "ClubName,ClubNameAbbreviated,LeagueName,Defense,Offense";
    public string ConvertToCsvFormat()
    {
        return $"{ClubName},{ClubNameAbbreviated},{LeagueName},{Defense},{Offense}";
    }
    
    
    
    // ___________________ SPECIAL METHODS _________________________________


 
    
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
