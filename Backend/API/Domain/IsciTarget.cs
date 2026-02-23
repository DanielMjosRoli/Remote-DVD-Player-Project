public class IsciTarget
{
    private int Id{get;}

    private string IQN{get;}

    private int ImageId{get;}

    private bool ReadOnly{get;}

    private bool Status{get; set;}

    public IsciTarget(int id, string iqn, int imageid)
    {
        Id = id;
        IQN = iqn;
        ImageId = imageid;
        ReadOnly = true;
        Status = false;
    }

    public void Activate()
    {
        Status = true;
    }

    public void Deactivate()
    {
        Status = false;
    }
}