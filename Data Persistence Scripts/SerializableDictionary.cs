using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class SerializableDictionary<TKey, TValue> : Dictionary<TKey, TValue>, ISerializationCallbackReceiver
{ //Variables of data type Dictionary in GameData will reparent to this class, so that Json can read variables of data type Dictionary

    //Save the keys & its values on two seperate Lists
    [SerializeField] private List<TKey> keys = new List<TKey>();
    [SerializeField] private List<TValue> values = new List<TValue>();
    
    //Load the dictionary from Lists
    public void OnAfterDeserialize() //We can add translations in each of these to go to and from the dictionary type
    {
        this.Clear();

        //Keys & Values from the Lists must have the same count since it is acting like a Dictionary
        if(keys.Count != values.Count) Debug.LogError("The amount of keys "+keys.Count+" does not match the number of values "+values.Count);
        
        //Adds the key with its corresponding value
        for(int i = 0; i < keys.Count; i++)
        {
            this.Add(keys[i], values[i]);
        }
    }

    //Save the dictionary from Lists
    public void OnBeforeSerialize() //We can add translations in each of these to go to and from the dictionary type
    {
        keys.Clear();
        values.Clear();
        
        foreach(KeyValuePair<TKey, TValue> pair in this)
        {
            keys.Add(pair.Key);
            values.Add(pair.Value);
        }
    }

}
