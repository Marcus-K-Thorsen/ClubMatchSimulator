namespace FootballClubSimulator.models;

// MatchOutcome is a record of a Match, between two Clubs and their score doing that match
public record MatchOutcome(Club? FirstClub, int FirstClubScore, Club? SecondClub, int SecondClubScore);
