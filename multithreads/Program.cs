using System.Diagnostics;

string fileNameBase = "file_";
string fileExtension = ".txt";
int numFiles = 10;
int numChars = 100000;

void deleteAllFiles()
{
    for (int i = 0; i < numFiles; i++)
    {
        string filePath = "./files/" +  i.ToString() + fileExtension;
        if (File.Exists(filePath))
        {
            File.Delete(filePath);
        }
    }
}

deleteAllFiles();

Random rand = new Random();
Stopwatch stopwatch = new Stopwatch();

Console.WriteLine("Création de " + numFiles + " fichiers de " + numChars + " caractères sans paralélisation ...");

stopwatch.Start();

for (int i = 0; i < numFiles; i++)
{
    string filePath = "./files/" +  i.ToString() + fileExtension;
    
    string randomText = "";   
    for (int j = 0; j < numChars; j++)
    {
        randomText += (char)rand.Next(32, 127);
    }

    using (StreamWriter sw = File.CreateText(filePath))
    {
        sw.WriteLine(randomText);
    }
}

stopwatch.Stop();
Console.WriteLine("Création des fichiers sans paralélisation  terminée en " + stopwatch.ElapsedMilliseconds + " ms.");

deleteAllFiles();

Console.WriteLine("Création de " + numFiles + " fichiers de " + numChars + " caractères avec paralélisation ...");
stopwatch.Restart();
Parallel.For(0, numFiles, i =>
{
    string filePath = "./files/" + i.ToString() + fileExtension;
    string randomText = "";
    
    for (int j = 0; j < numChars; j++)
    {
        randomText += (char) rand.Next(32, 127);
    }

    using (StreamWriter sw = File.CreateText(filePath))
    {
        sw.WriteLine(randomText);
    }
});
stopwatch.Stop();
Console.WriteLine("Création des fichiers avec paralélisation  terminée en " + stopwatch.ElapsedMilliseconds + " ms.");

Console.ReadLine();