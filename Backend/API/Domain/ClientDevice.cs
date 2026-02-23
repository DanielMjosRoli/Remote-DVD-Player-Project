
public class ClientDevice
{
    private int Id{get;}

    private string Name{get;}

    private string Ip{get;}

    private string Mac{get;}

    public ClientDevice(int id, string name, string ip, string mac)
    {
        Id = id;
        Name = name;
        Ip = ip;
        Mac = mac;
    }

}