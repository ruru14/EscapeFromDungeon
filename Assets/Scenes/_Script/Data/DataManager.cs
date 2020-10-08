using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;

public class DataManager
{
    public static object locker = new object();


    private DataManager()
    {

    }

    public static void BinarySerialize<T>(T t, string filePath)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        if (!Directory.Exists(DataFilePath.DataDirectory))
        {
            Directory.CreateDirectory(DataFilePath.DataDirectory);
        }
        FileStream stream = new FileStream(DataFilePath.DataDirectory + "/" + filePath, FileMode.Create);
        formatter.Serialize(stream, t);
        stream.Close();
    }

    public static T BinaryDeserialize<T>(string filePath)
    {
        T t;
        try
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(DataFilePath.DataDirectory + filePath, FileMode.Open);
            t = (T)formatter.Deserialize(stream);
            stream.Close();
        }
        catch (FileNotFoundException)
        {
            return default(T);
        }
        catch (SerializationException)
        {
            return default(T);
        }

        return t;
    }
}
