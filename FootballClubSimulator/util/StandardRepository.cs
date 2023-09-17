using FootballClubSimulator.repositories;

namespace FootballClubSimulator.util;

public abstract class StandardRepository<T>
{
    public readonly FileHandler FileHandler;

    protected StandardRepository(string fileName, string fileHeader)
    {
        FileHandler = new FileHandler(fileName, fileHeader);
    }

    public int GetHeaderIndexOfColumnName(string columnName)
    {
        string[] headerAsStringArray = FileHandler.FileHeader.Split(',');
        for (int i = 0; i < headerAsStringArray.Length; i++)
        {
            if (headerAsStringArray[i] == columnName)
            {
                return i;
            }
        }

        throw new Exception(
            $"Exception trying to get the index position of ColumnName: '{columnName}'\nFrom the Header: '{FileHandler.FileHeader}'\nWithin the file: '{FileHandler.FilePath}'");
    }
    
    public abstract List<T> ReadAll();

    public abstract void WriteAll(List<T> objectsToWrite);
}