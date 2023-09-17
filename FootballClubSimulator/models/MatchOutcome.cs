namespace FootballClubSimulator.models;

// MatchOutcome is a record of a Match, between two Clubs and their score doing that match
public record MatchOutcome(string HomeClubAbbreviated, int HomeClubScore, string AwayClubAbbreviated, int AwayClubScore);
