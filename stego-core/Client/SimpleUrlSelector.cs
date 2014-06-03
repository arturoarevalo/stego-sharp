namespace Stego.Core.Client
{
    public class SimpleUrlSelector : IUrlSelector
    {
        private int index;

        public string SelectNext (UrlList list)
        {
            return list [index++ % list.Count];
        }
    }
}