using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class LoadButtonEnabler : MonoBehaviour
{
    void Awake()
    {
        if (ProgressData.SaveFileExists())
        {
            string dataPath = Application.persistentDataPath + ProgressData.SavePath;
            FileStream fileStream = new FileStream(dataPath, FileMode.Open);
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            fileStream.Position = 0;
            ProgressData loadedData = (ProgressData) binaryFormatter.Deserialize(fileStream);
            fileStream.Close();

            if ( loadedData != null && loadedData.PlaceholderData != null )
            {
                gameObject.SetActive(true);
            }
            else
            {
                File.Delete(dataPath);
                gameObject.SetActive(false);
            }
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}
