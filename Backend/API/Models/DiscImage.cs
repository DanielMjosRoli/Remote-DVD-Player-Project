public class DiscImage
{
    private int Id{get;}

    private string Name{get;}

    private string FilePath{get;}

    private string Sha256{get;}

    private DateTime UploadDate{get;}

    public DiscImage(int id, string name, string filepath, string sha256)
    {
        Id = id;
        Name = name;
        FilePath = filepath;
        Sha256 = sha256;
        UploadDate = DateTime.Now;
    }
    public bool Validate()
    {
        return false;
    }
}