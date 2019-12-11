using System.Collections.Generic;

public static class ServerSettings
{
    static Settings _settings;
    public static Settings Settings { 
        get 
        { 
            if(_settings == null)
            {
                _settings = new Settings();
            }
            return _settings;
        }
        set
        {
            _settings = value;
        }
    }
}

public class Settings
{
    public int bufferSize { get; set; }
    public int serverPort { get; set; }
}