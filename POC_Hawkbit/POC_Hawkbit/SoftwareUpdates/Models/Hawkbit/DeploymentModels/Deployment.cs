namespace POC_Hawkbit.SoftwareUpdates.Models.Hawkbit.DeploymentModels;

public class Deployment
{
    
}

/*
// Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class Artifact
    {
        public string filename { get; set; }
        public Hashes hashes { get; set; }
        public int size { get; set; }
        public Links _links { get; set; }
    }

    public class Chunk
    {
        public string part { get; set; }
        public string version { get; set; }
        public string name { get; set; }
        public List<Artifact> artifacts { get; set; }
    }

    public class Deployment
    {
        public string download { get; set; }
        public string update { get; set; }
        public List<Chunk> chunks { get; set; }
    }

    public class DownloadHttp
    {
        public string href { get; set; }
    }

    public class Hashes
    {
        public string sha1 { get; set; }
        public string md5 { get; set; }
        public string sha256 { get; set; }
    }

    public class Links
    {
        [JsonProperty("download-http")]
        public DownloadHttp downloadhttp { get; set; }

        [JsonProperty("md5sum-http")]
        public Md5sumHttp md5sumhttp { get; set; }
    }

    public class Md5sumHttp
    {
        public string href { get; set; }
    }

    public class Root
    {
        public string id { get; set; }
        public Deployment deployment { get; set; }
    }


*/