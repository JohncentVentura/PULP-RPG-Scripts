using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class DataFileHandler
{
    private string persistentDataPath; //The directory path to the profileId folders
    private string dataFileName; //The name of the file containing a saved data
    private bool isEncrypted; //If the file can be edited in a NotePad, or encrypted to secure the saved data
    private readonly string encryptionCodeWord = "word";
    private readonly string backupExtension = ".bak"; //.bak is the extension of the backup file

    public DataFileHandler(string persistentDataPath, string dataFileName, bool isEncrypted)
    {
        this.persistentDataPath = persistentDataPath;
        this.dataFileName = dataFileName;
        this.isEncrypted = isEncrypted;
    }

    public GameData LoadDataFile(string profileId, bool useBackup = true)
    {
        if (profileId == null) //If profileId is null, return null immediately
        {
            return null;
        }

        GameData loadedGameData = null;

        //fullPath is the path to the dataFileName, Path.Combine concatenates the parameters to create a directory to dataFileName
        string fullPath = Path.Combine(persistentDataPath, profileId, dataFileName);

        if (File.Exists(fullPath)) //If dataFileName exists in fullPath
        {
            try //If dataFileName exists in fullPath and is not corrupted
            {
                string dataToLoad = ""; //Placeholder of the deserialized data from dataFileName

                //use using() when reading or writing a file, to ensure the connection to a file is closed after reading or writing it
                using (FileStream stream = new FileStream(fullPath, FileMode.Open)) //FileMode.Open allows us to read the file
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        dataToLoad = reader.ReadToEnd(); //ReadToEnd() reads a file text and return it as a string
                    }
                }

                if (DataPersistenceManager.instance.useEncryption) //old is isEncrypted
                {
                    dataToLoad = EncryptAndDecrypt(dataToLoad); //Decrypts the data after reading the file
                }

                loadedGameData = JsonUtility.FromJson<GameData>(dataToLoad); //Deserialize the data from Json to a C# script
            }
            catch (Exception e) //If dataFileName exists in fullPath but is corrupted
            {
                if (useBackup)
                {
                    Debug.LogWarning("Failed to load data file. Attempting to load backup file.\n" + e);
                    bool isBackupSuccess = BackupData(fullPath);

                    // If backing up is successful, calls the LoadDataFile again but backup is false this time to avoid recursion of backing up unsuccessfully
                    if (isBackupSuccess)
                    {
                        loadedGameData = LoadDataFile(profileId, false);
                    }
                    // If backing up is unsuccessful
                    else
                    {
                        Debug.LogError("Error occured when trying to load file at path: " + fullPath + " and backup did not work.\n" + e);
                    }
                }
            }
        }

        return loadedGameData; //returns a deserialized data from dataFileName
    }

    public void SaveDataFile(GameData gameData, string profileId)
    {
        if (profileId == null)
        {
            return; //If profileId is null, return immediately
        }

        //fullPath is the path to the dataFileName, Path.Combine concatenates the parameters to create a directory to dataFileName
        string fullPath = Path.Combine(persistentDataPath, profileId, dataFileName);
        string backupFilePath = fullPath + backupExtension; //The directory path to the backup file

        try
        {
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath)); //Create a directory where the file will be saved if it doesn't exist

            string dataToStore = JsonUtility.ToJson(gameData, true); //Placeholder of the serialized data from GameData, true means we format the Json Data

            if (DataPersistenceManager.instance.useEncryption)
            {
                dataToStore = EncryptAndDecrypt(dataToStore); //Encrypts the data before writing it to a the file
            } 

            //use using() when reading or writing a file, to ensure the connection to a file is closed after reading or writing it
            using (FileStream stream = new FileStream(fullPath, FileMode.Create)) //FileMode.Create allows us to write to a file
            {
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    writer.Write(dataToStore); //Passes in dataToStore which we want to write that file
                }
            }

            //Verify the new saved file if it can loaded successfuly
            GameData verifiedGameData = LoadDataFile(profileId);

            //Copy() copies the fullPath to backupFilePath, true means the fullPath overrides the data file from backupFilePath
            if (verifiedGameData != null) File.Copy(fullPath, backupFilePath, true);
            else throw new Exception("Save file could not be verified and backup could not be created.");
        }
        catch (Exception e)
        {
            Debug.LogError("Error occured when trying to save data to file: " + fullPath + "\n" + e);
        }
    }

    private bool BackupData(string fullPath) //Returns a bool that determines either to backup or not
    {
        bool success = false; //If backing up data is a success or not

        string backupFilePath = fullPath + backupExtension; //The directory path to the backup file

        try
        {
            if (File.Exists(backupFilePath)) //If there is a backup file
            {
                //Copy() copies the backupFilePath to fullPath, true means the backupFilePath overrides the data file from fullPath
                File.Copy(backupFilePath, fullPath, true);
                success = true; //Backing up is a success
            }
            else //If there is no backup file
            {
                throw new Exception("No existing backup file.");
            }
        }
        catch (Exception e)
        {
            Debug.LogError("Error occured when trying to backup at: " + backupFilePath + "\n" + e);
        }

        return success;
    }

    public string GetMostRecentlyUpdatedProfileId() //Returns the recent profileId that is loaded and saved, use for Scene Transitioning
    {
        string mostRecentProfileId = null;
        Dictionary<string, GameData> profileGameData = LoadAllProfiles();

        //Iterate through each profileGameData to get its profileId & GameData
        foreach (KeyValuePair<string, GameData> pair in profileGameData)
        {
            string profileId = pair.Key;
            GameData gameData = pair.Value;

            //Skip this GameData if it is null and continue
            if (gameData == null)
            {
                continue;
            }

            //If this is the first profileId containing a GameData, this profileId is the most recent so far
            if (mostRecentProfileId == null)
            {
                mostRecentProfileId = profileId;
            }
            else //Else if there is another profileId containing a GameData, compare which time is the most recent
            {
                DateTime mostRecentDateTime = DateTime.FromBinary(profileGameData[mostRecentProfileId].lastUpdated);
                DateTime newDateTime = DateTime.FromBinary(gameData.lastUpdated);

                //The greatest DateTime value is the most recent profileId
                if (newDateTime > mostRecentDateTime)
                {
                    mostRecentProfileId = profileId;
                }
            }
        }
        return mostRecentProfileId; //If there is no save data, mostRecentProfileId will return null
    }
    
    public Dictionary<string, GameData> LoadAllProfiles() //Returns a Dictionary containing keys of profileId and each of its value of GameData
    {
        Dictionary<string, GameData> profileDictionary = new Dictionary<string, GameData>();

        //EnumerateDirectories returns a collection of DirectoryInfo of persistentDataPath that we can use for iteration
        IEnumerable<DirectoryInfo> directoryInfos = new DirectoryInfo(persistentDataPath).EnumerateDirectories();

        foreach (DirectoryInfo directoryInfo in directoryInfos)
        {
            string profileId = directoryInfo.Name;

            //check if the data file exists, if it doesn't then this folder isn't a profile and should be skipped
            string fullPath = Path.Combine(persistentDataPath, profileId, dataFileName);

            //If there are no data in this fullPath, continue to the next dirInfo from the foreach loop
            if (!File.Exists(fullPath))
            {
                Debug.LogWarning("Skipping directory when loading all profiles because it does not contain data");
                continue;
            }

            //Stores the GameData of the profileId to profileData, & add it to the profileDictionary
            GameData profileData = LoadDataFile(profileId);

            if (profileData != null)
            {
                profileDictionary.Add(profileId, profileData); //Checking if GameData is null first
            }
            else
            {
                Debug.LogError("Tried to load profile but something went wrong. ProfileId: " + profileId);
            }
        }

        return profileDictionary;
    }

    private string EncryptAndDecrypt(string data) //Simple XOR Encryption
    {
        string modifiedData = "";
        for (int i = 0; i < data.Length; i++)
        {
            modifiedData += (char)(data[i] ^ encryptionCodeWord[i % encryptionCodeWord.Length]);
        }
        return modifiedData;
    }
}
