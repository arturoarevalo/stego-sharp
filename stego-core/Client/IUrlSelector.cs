namespace Stego.Core.Client
{
    public interface IUrlSelector
    {
        string SelectNext (UrlList list);
    }
}