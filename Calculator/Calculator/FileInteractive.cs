using Calculator.Resources;

namespace Calculator;

public class FileInteractive
{
    public string[] FileRider(string filePath)
    {
        var fileLines = new List<string>();
        string[] result;

        try
        {
            using (var reader = new StreamReader(filePath))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    fileLines.Add(line);
                }
            }
        }
        catch (Exception ex)
        {
            switch (ex)
            {
                case NullReferenceException:
                    throw new NullReferenceException("Null Reference Exception");
                case FileNotFoundException:
                    throw new FileNotFoundException("File not found.");
            }
        }

        result = fileLines.ToArray();

        return result;
    }

    public string GetFileWithResults(string newFilePath, string[] expressions)
    {
        var lg = new Logic();

        try
        {
            using (FileStream file = new FileStream(newFilePath, FileMode.Append))
            {
                using (StreamWriter stream = new StreamWriter(file))
                {
                    foreach (var exp in expressions)
                    {
                        stream.WriteLine($"{exp} = {lg.GetResultOfCalculate(exp)}");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            switch (ex)
            {
                case NullReferenceException:
                    throw new NullReferenceException("Null Reference Exception");
                case FileNotFoundException:
                    throw new FileNotFoundException("File not found.");
            }
        }

        return CalculateResources.FileCreated;
    }
}