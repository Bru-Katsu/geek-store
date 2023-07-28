namespace GeekStore.Core.Helpers
{
    public static class UrlHelper
    {
        public static bool IsValidImageUrl(string url)
        {
            if (Uri.TryCreate(url, UriKind.Absolute, out Uri uri))
                return uri.Scheme == Uri.UriSchemeHttp || uri.Scheme == Uri.UriSchemeHttps;

            return false;
        }
    }
}
