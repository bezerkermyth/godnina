using UnityEngine;
using System.Collections;
using System.IO;

public class Serializer : MonoBehaviour
{

    public static Serializer SerializerInstance = null;

    static readonly string SAVE_FILE = "player.dat";
                                               //01234567890123456789012345678901
    static readonly string JSON_ENCRYPTED_KEY = "#kJ83DAloWjkf39(#($%0_+[]:#dDA'a";

    public GameObject player;

    public SaveData data;
    private string filename;
    private Rijndael crypto;


   
    void Awake()
    {
       
        if (SerializerInstance == null)
            SerializerInstance = this;
       
        else if (SerializerInstance != this)

            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);

    }



    void Start()
    {

        filename = Path.Combine(Application.persistentDataPath, SAVE_FILE);

        SaveData();
        LoadData();
        
    }

    //---------------------------------------------------------------
    public void SaveData()
    {
        string json = JsonUtility.ToJson(data);

        crypto = new Rijndael();
        byte[] soup = crypto.Encrypt(json, JSON_ENCRYPTED_KEY);

        

        if (File.Exists(filename))
        {
            File.Delete(filename);
        }

        File.WriteAllBytes(filename, soup);
    }


    //---------------------------------------------------------------
    public void LoadData() {

        byte[] soupBackIn = File.ReadAllBytes(filename);
        string jsonFromFile = crypto.Decrypt(soupBackIn, JSON_ENCRYPTED_KEY);

        SaveData copy = JsonUtility.FromJson<SaveData>(jsonFromFile);
        Debug.Log(copy.health);

    }




    //---------------------------------------------------------------
}
