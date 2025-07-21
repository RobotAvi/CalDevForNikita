namespace CalDavServer.Utils
{
    public static class WebDavHelper
    {
        public static string BuildPropFindResponse() => "<D:multistatus xmlns:D=\"DAV:\"></D:multistatus>";
        // TODO: Реализовать парсинг и генерацию WebDAV XML
    }
}