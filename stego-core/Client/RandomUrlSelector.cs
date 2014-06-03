namespace Stego.Core.Client
{
    using System;
    public class RandomUrlSelector : IUrlSelector
    {
        private Random randomGenerator = new Random();

        public string SelectNext (UrlList list)
        {
            return list [randomGenerator.Next (0, list.Count)];
        }
    }
}