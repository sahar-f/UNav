using System.Collections;
using System.Collections.Generic;
using System.Data;

using UnityEngine;
using Mono.Data.Sqlite;
using static SYSTEM;
using System.IO;

public class DATABASE_MANAGER : MonoBehaviour
{
    
    private string connectionString;
    private IDbConnection dbConnection;
    public string DatabaseName;
   
   
    private string DB_get_connectionstring()
    {
        #if UNITY_EDITOR
            string dbPath = string.Format(@"Assets/StreamingAssets/{0}", DatabaseName);
        #else
            string filepath = string.Format("{0}/{1}", Application.persistentDataPath, DatabaseName);

        if (!File.Exists(filepath))
        {
            Debug.Log("Database not in Persistent path");
            #if UNITY_ANDROID
                var loadDb = new WWW("jar:file://" + Application.dataPath + "!/assets/" + DatabaseName);  // this is the path to your StreamingAssets in android
                while (!loadDb.isDone) { }   // CAREFUL here, for safety reasons you shouldn't let this while loop unattended, place a timer and error check
                                             // then save to Application.persistentDataPath
                File.WriteAllBytes(filepath, loadDb.bytes);
            #elif UNITY_IOS
                var loadDb = Application.dataPath + "/Raw/" + DatabaseName;  // this is the path to your StreamingAssets in iOS
                                                                              // then save to Application.persistentDataPath
                File.Copy(loadDb, filepath);
            #else
                var loadDb = Application.dataPath + "/StreamingAssets/" + DatabaseName;  // this is the path to your StreamingAssets in iOS
	                                                                                     // then save to Application.persistentDataPath
                File.Copy(loadDb, filepath);

            #endif

            Debug.Log("Database written");
        }

        string dbPath = filepath; 
        #endif




        return dbPath; 
    }

    private void ConnectToDatabase()
    {
        DatabaseName = "unavtestabc.db";
        string dbPath = DB_get_connectionstring();
        connectionString = "URI=file:" + dbPath;

        // Create a new database connection
        dbConnection = new SqliteConnection(connectionString);

        // Open the connection
        dbConnection.Open();
    }

    private void DisconnectFromDatabase()
    {
        // Close the connection
        dbConnection.Close();
        dbConnection = null;
    }

    public void Retreive_Map(ref Mapstruct map,int idx)
    {
        ConnectToDatabase();
        // Create a new database command
        IDbCommand dbCommand = dbConnection.CreateCommand();
        // Set the command text

        dbCommand.CommandText = $"SELECT * FROM MAPS WHERE ID = '{idx}'";
        //dbCommand.CommandText = "SELECT * FROM MAPS";
        IDataReader reader = dbCommand.ExecuteReader();
        reader.Read();
        map.location_x = reader.GetDouble(2);
        map.location_y = reader.GetDouble(3);
        map.location_z = reader.GetDouble(4);
        map.rotation_x= reader.GetDouble(5);
        map.rotation_y= reader.GetDouble(6);
        map.rotation_z= reader.GetDouble(7);
        map.scale_x=reader.GetDouble(8);
        map.scale_y= reader.GetDouble(9);
        map.scale_z= reader.GetDouble(10);
        map.refpoint1_x= reader.GetDouble(11);
        map.refpoint1_y= reader.GetDouble(12);
        map.refpoint1_z= reader.GetDouble(13);
        map.refpoint1_lat= reader.GetDouble(14);
        map.refpoint1_lon= reader.GetDouble(15);
        map.refpoint1_alt= reader.GetDouble(16);
        map.refpoint2_x= reader.GetDouble(17);
        map.refpoint2_y= reader.GetDouble(18);
        map.refpoint2_z= reader.GetDouble(19);
        map.refpoint2_lat = reader.GetDouble(20); 
        map.refpoint2_lon= reader.GetDouble(21);
        map.refpoint2_alt= reader.GetDouble(22);
        map.texture= reader.GetString(23);
        map.tag = reader.GetString(24);
        reader.Close();
       
        DisconnectFromDatabase();

    }
    public void Retreive_Camera(ref Camerastruct cam,int idx)
    {
        ConnectToDatabase();
        // Create a new database command
        IDbCommand dbCommand = dbConnection.CreateCommand();
        // Set the command text

        dbCommand.CommandText = $"SELECT * FROM CAMERAS WHERE ID = '{idx}'";
        IDataReader reader = dbCommand.ExecuteReader();
        reader.Read();
        cam.location_x = reader.GetDouble(2);
        cam.location_y = reader.GetDouble(3);
        cam.location_z = reader.GetDouble(4);
        cam.rotation_x = reader.GetDouble(5);
        cam.rotation_y = reader.GetDouble(6);
        cam.rotation_z = reader.GetDouble(7);
        cam.orthographic = reader.GetBoolean(8);
        cam.orthographicSize = reader.GetDouble(9);
        cam.tag = reader.GetString(10);

        reader.Close();
        DisconnectFromDatabase();
    }
    public void Retreive_Restaurant(ref Restaurantstruct rest, )
}
