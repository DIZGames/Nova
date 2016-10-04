using System;
using System.Collections.Generic;

[Serializable]
public class SaveData {
    public string saveName { get; set; }

    Dictionary<string, bool> _boolFields = null;
    Dictionary<string, int> _intFields = null;
    Dictionary<string, float> _floatFields = null;
    Dictionary<string, string> _stringFields = null;

    List<SaveGameObject> _rootgameObjects = null;

    public SaveData(string saveName)
    {
        this.saveName = saveName;
    }

    public Dictionary<string, bool> BoolFields
    {
        get
        {
            if (_boolFields == null)
                _boolFields = new Dictionary<string, bool>();
            return _boolFields;
        }
    }

    public Dictionary<string, int> IntFields
    {
        get
        {
            if (_intFields == null)
                _intFields = new Dictionary<string, int>();
            return _intFields;
        }
    }

    public Dictionary<string, float> FloatFields
    {
        get
        {
            if (_floatFields == null)
                _floatFields = new Dictionary<string, float>();
            return _floatFields;
        }
    }

    public Dictionary<string, string> StringFields
    {
        get
        {
            if (_stringFields == null)
                _stringFields = new Dictionary<string, string>();
            return _stringFields;
        }
    }

    public List<SaveGameObject> RootGameObjetcs
    {
        get
        {
            if (_rootgameObjects == null)
                _rootgameObjects = new List<SaveGameObject>();
            return _rootgameObjects;
        }
    }
}
